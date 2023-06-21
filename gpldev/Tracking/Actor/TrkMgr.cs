using Akka.Actor;
using MsgStruct;
using System.Collections.Generic;
using Core.Define;
using DataMod.WMS.LogicModel;
using Controller.Track;
using Controller.Coil;
using DataModel.HMIServerCom.Msg;
using System;
using MSMQ;
using MSMQ.Core.MSMQ;
using Controller;
using DBService;
using Core.Util;
using AkkaSysBase.Base;
using AkkaSysBase;
using static DataMod.Common.ModifyCoilModel;
using System.Timers;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using DataMod.Common;
using DBService.AggregationModel;
using LogSender;

namespace Tracking.Actor
{
    /**
     Author:ICSC 余士鵬
     Date:2019/11/5
     Desc:鋼捲追蹤與位置相關處理
    **/
    public class TrkMgr : BaseActor
    {
        private IActorRef _selfActor;
        private ITrackingController _trkController;
        private ICoilController _coilController;


        private Timer _tmrCheckEntryStart;
        private Timer _tmrCheckDeliveryStart;

        private int _entryStartConditionFlag;
        private int _deliveryStartConditionFlag;
        private int _hmiDeliveryCoilReady;

        public TrkMgr(ISysAkkaManager akkaManager, ITrackingController trkController, ICoilController coilController, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _trkController = trkController;
            _coilController = coilController;


            _coilController.SetLog(log);
            _trkController.SetLog(log);

            // 設置EntryStart Flag
            _entryStartConditionFlag = PlcSysDef.Cmd.EntryConditionNG;
            if (_tmrCheckEntryStart == null) _tmrCheckEntryStart = new Timer(1000);
            _tmrCheckEntryStart.Elapsed += TmrEntryCheckElapsed;             //Register

            // 設置DeliveryStart Flag
            _deliveryStartConditionFlag = PlcSysDef.Cmd.EntryConditionNG;
            _hmiDeliveryCoilReady = PlcSysDef.Cmd.EntryConditionNG;
            if (_tmrCheckDeliveryStart == null) _tmrCheckDeliveryStart = new Timer(1000);
            _tmrCheckDeliveryStart.Elapsed += TmrDeliveryCheckElapsed;             //Register



            MQPool.ReceiveFromTrk(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });

            // 自動進料狀態切換
            Receive<CS10_Coil_AutoFeedModeChange>(message => ChekCoilProdLineEnterStatus(message));
            // ID掃碼確認，入口段鋼捲ID更正
            Receive<CS04_RenameCoil>(message => RenameEntryCoil(message));
            // 手動入料 :  直接發入料要求
            Receive<CS11_Coil_ManualFeed>(message => SndEntryCoilReqMsg(message));
            // 鋼捲生產入料(退料)作業 - 鋼捲生產[退料]作業
            Receive<CS05_RejectCoil>(message => SndRejectCoilData(message));
            // 在鞍座上手動操作入料 發送202 
            Receive<CS12_Coil_SkidFeed>(message => PreSndPresetMsg(message));
            // 手動刪除鞍座上鋼卷號通知210給L1
            Receive<CS13_DeleteSidCoil>(message => InfoL1210Msg(message));
            // 出口段出料
            Receive<CS14_DeliveryCoilOut>(message => TryFlow(() => DeliveryCoilOut(message)));

            // 天車入料時選擇鋼捲ID
            Receive<CS18_CarneEntryCoilSelect>(message => TryFlow(() => FinishEntryFlow(message.coilID, message.coilID, true, PlcSysDef.Pos.Preset202ETOP, true)));

            // 操作出料確認
            Receive<CS21_DeliveryCoilReady>(message => _hmiDeliveryCoilReady = PlcSysDef.Cmd.EntryConditionREADY);

            // GPL PLC Msg

            // Trk ID追蹤
            Receive<L1L2Rcv.Msg_105_Trk_Map>(message => TryFlow(() => UpdateTrkMap(message)));          
            // Coil Mount
            Receive<L1L2Rcv.Msg_114_Coil_Mount>(message => TryFlow(() => ProCoilMount(message)));
            // 入料開始條件
            Receive<L1L2Rcv.Msg_118_Entry_Start_Condition>(message => _entryStartConditionFlag = message.ConditionFlag);
            // 出料開始條件
            Receive<L1L2Rcv.Msg_121_Delivery_Start_Condition>(message => _deliveryStartConditionFlag = message.StartCondition);

            // WMS完成動作通知 (WG01)
            Receive<WMS_L2_Rcv.WGx1_CompleteOfFeeding>(message => TryFlow(() => WMSFinishMsgProcess(message)));
            // WMS 入料/出料/退料要求回復訊息
            Receive<WMS_L2_Rcv.WGx3_RequestResponse>(message => TryFlow(() => WMSResReqMsgProcess(message)));

            //test
            //var testMsg = new CS05_RejectCoil();
            //testMsg.CoilID = "CG200000030000";
            //testMsg.PlanNo = "A12345";
            //SndRejectCoilData(testMsg);
        }

        // 鋼卷上POR : 發Preset to L1
        private void ProCoilMount(L1L2Rcv.Msg_114_Coil_Mount msg)
        {
            var porCoilNo = msg.CoilID.ToStr();

            var preset = new SpectPresetModel()
            {
                CoilID = porCoilNo,
                SKPosID = PlcSysDef.Pos.L1204_205LinePresetPos,
            };


            if (porCoilNo.IsEmpty())
            {
                _log.E("鋼卷上POR通知錯誤", $"鋼卷號為空{porCoilNo}");
                var eventPush = new SC03_EventPush("鋼卷上POR通知錯誤", $"鋼卷號為空{porCoilNo}");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
                return;
            }

            
            _log.I("鋼卷上POR通知", $"鋼卷上POR{porCoilNo}");
            MQPoolService.SendToDtStp(InfoDataSetup.SpecificIDTo_204_205Msg.Data(preset));

            var coilMap = _trkController.GetTrackMap();            
            var porPDI = _coilController.GetPDI(porCoilNo);
            if (coilMap == null || porPDI == null)
                return;
            // POR 紀錄開始時間
            //if(porPDI.Is_Dummy_Coil.Equals(EventDef.TRUE) && coilMap.TR.Trim().Equals(string.Empty))
            //_coilController.UpdatePDIStarTime(porCoilNo);
            //變更鋼捲狀態 P
            _coilController.UpdateScheduleStatuts(porCoilNo, CoilDef.ScheduleStatuts.Producing_Statuts);
        }


        // Trk ID追蹤:更新TrkMap
        public void UpdateTrkMap(L1L2Rcv.Msg_105_Trk_Map msg)
        {
            var preCoilMap = _trkController.GetTrackMap();

            if (preCoilMap == null)
            {
                _log.I("撈取Tracking Map失敗", "抓Trk值失敗，請檢察DB連線");
                return;
            }

            var _preExitSK2 = preCoilMap.Delivery_SK02.Trim();
            var _preEntryTopCoilID = preCoilMap.Entry_TOP.Trim();
            var _preExitTOP = preCoilMap.Delivery_TOP.Trim();

            var ETopCoil = msg.Entry_TOP;
            var DSK02Coil = msg.Delivery_SK02;
            var DTOPCoil = msg.Delivery_TOP;

            var updateOk = _trkController.UpdateTrackMap(msg);
            _trkController.Create25CoilMap(msg);
            if (!updateOk)
            {
                _log.E("更新TrackMap失敗", "更新鋼卷追蹤鋼卷號失敗");
                return;
            }


            #region 鋼卷到達DTOP : 標籤列印

            if(IsCoilIn(_preExitSK2, DSK02Coil))
            {
                // 通知列印
                _log.I("鋼捲生產出料作業", $"鋼捲{DSK02Coil}到出口位置");
                MQPoolService.SendToLpr(InfoLpr.CoilInExitSK2.Data(DSK02Coil));

                // 開啟 
                _tmrCheckDeliveryStart.Start();

            }


            if (IsCoilIn(_preExitTOP, DTOPCoil))
            {
                // GW05
                _log.I("鋼捲生產出料作業", $"通知WMS出料{DTOPCoil}");
                var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSDeliveryCoil, DTOPCoil, WMSSysDef.SkPos.DTOP);
                MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));

                //通知HMI 顯示自動出料資訊
                var eventPush = new SC03_EventPush("自動出料", $"發送出料要求給WMS,捲號 {DTOPCoil.Trim()}");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            }

            #endregion
  

            
        }
    
        // 自動進料狀態切換
        private void ChekCoilProdLineEnterStatus(SCCommMsg.CS10_Coil_AutoFeedModeChange msg)
        {
          
            //// 判定Auto Flag是否為自動進料
            //if (_trkController.IsSystemAutoValueOn(L2SystemDef.GPLGroup, DBParaDef.SysParaAutoInputFlag))
            //{
            //    //// 判定EntryTop 是否有鋼捲
            //    if (!_trkController.InvaildHasEntryTopCoilID())
            //    {
            //        //排程若有入料要求狀態鋼捲則不發入料要求
            //        var ReqCoilID = _coilController.QueryScheduleRequestCoils(10);
            //        if (ReqCoilID.Count > 0)
            //        {
            //            //_log.I("目前已有鋼捲排程狀態為R[要求入料]", $"共 {ReqCoilID.count}筆");
            //            return;

            //        }

            //        //取得排程第一颗钢卷 排除P(生產中),F(已入料),D(已產出),C(回退)
            //        var FirstCoil = _coilController.GetFirstCoilSchedule();
            //        if (!FirstCoil.Trim().Equals(string.Empty))
            //        {
            //           // 發GW15 To WMS 入料要求
            //           var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSEntryCoil, FirstCoil.Trim(), WMSSysDef.SkPos.ETOP);
            //            _log.I("自動入料要求", "發送入料要求給WMS");
            //            MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));

            //            // 更新旗標
            //            _coilController.UpdateIsInfoWMSDown(true, FirstCoil.Trim());

            //            // 更新鋼捲狀態為要求入料
            //            _coilController.UpdateScheduleStatuts(FirstCoil.Trim(), CoilDef.ScheduleStatuts.RequestEntryCoil_Statuts);

            //        };


            //        //// 撈取PDI比對是否已發送過期標
            //        //var pdi = _coilController.GetPDI(ReqCoilID[0].Trim());
            //        //if (pdi == null || pdi.Is_Info_WMS_Down.Equals(EventDef.TRUE))
            //        //    return;




                   


            //    };


            //    //if (!_trkService.HasEntryTopCoilID())
            //    //{      
            //    //   



            //    //MQPoolUtil.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(new ProdLineCoilReq(CoilLogicDef.ReqWMSEntryCoil, "")));
            //    //     return;
            //    //}            
            //    return;
            //}

       

        }
        
        // 手動入料 :  直接發入料要求
        private void SndEntryCoilReqMsg(SCCommMsg.CS11_Coil_ManualFeed msg)
        {
            var coilPDI = _coilController.GetPDI(msg.CoilID);
            string coilTurn = string.Empty;
            switch (coilPDI.Uncoil_Direction)
            {
                //上开卷 带尾朝南
                case "U":
                    coilTurn = "2";
                    break;
                //下开卷 带尾朝北
                case "L":
                    coilTurn = "5";
                    break;
                default:
                    coilTurn = "";
                    break;
            }

            // 更新旗標
            _coilController.UpdateIsInfoWMSDown(true, msg.CoilID);


            _log.I("HMI手動入料要求", "發送入料要求給WMS");
            // 發GW15 To WMS 入料要求
            var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSEntryCoil, msg.CoilID.Trim(), WMSSysDef.SkPos.ETOP, coilTurn);
            MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));

            //通知HMI 顯示手動入料資訊
            var eventPush = new SC03_EventPush("HMI手動入料", $"發送入料要求給WMS,捲號 {msg.CoilID.Trim()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

        }
       
        // 物流[入料/出料/退料] 完成通知 
        private void WMSFinishMsgProcess(WMS_L2_Rcv.WGx1_CompleteOfFeeding msg)
        {                     
            var flag = msg.Flag.ToStr();

            // 1:入料  2:出料 3:退料
            switch (flag)
            {
                case WMSSysDef.Cmd.FinishEntryCoil:
                  
                    if (msg.ScanNoID.Trim().Equals(string.Empty))  //台車入料
                    {
                        if (msg.SkidNoID.Equals(WMSSysDef.SkPos.ETOP))
                        {
                            FinishEntryCoil(msg, false);
                        };
                    }

                    else  //天車入料
                    { 
                        if (msg.SkidNoID.Equals(WMSSysDef.SkPos.ESK01))
                        {
                            FinishEntryCoil(msg, true);
                        };
                        if (msg.SkidNoID.Equals(WMSSysDef.SkPos.ETOP))
                        {
                            FinishEntryCoil(msg, true);
                        };                             
                    }
                      
                  

                    break;
                case WMSSysDef.Cmd.FinishDeliveryCoil:
                    FinishDeliveryCoil(msg);
                    break;
                case WMSSysDef.Cmd.FinishRejectCoil:
                    FinishRetrunCoil(msg);
                    break;
            }

           

        }

        // [入料]完成
        private void FinishEntryCoil(WMS_L2_Rcv.WGx1_CompleteOfFeeding msg, bool isCraneEntry)
        {
            var coilNo = msg.CoilNoID;
            var scanNoID = msg.ScanNoID;
          

            // 臺車入料
            if (!isCraneEntry)
            {
                _log.I("台車入料", $"鋼捲{msg.CoilNoID}由台車入TOP點");
                FinishEntryFlow(coilNo, scanNoID, false, PlcSysDef.Pos.Preset202ETOP, false);
                return;
            }

            // 天車入料
            if (coilNo.Equals(scanNoID))
            {
                switch (msg.SkidNoID)
                {
                    case WMSSysDef.SkPos.ESK01:
                        _log.I("天車入料", $"鋼捲{msg.CoilNoID}由天車入ESK01點");
                        //FinishEntryFlow(coilNo, "2", true, msg.L1202PresetPos, true);
                        break;
                    case WMSSysDef.SkPos.ETOP:
                        _log.I("天車入料", $"鋼捲{msg.CoilNoID}由天車入ETOP點");
                        //FinishEntryFlow(coilNo, "1", true, msg.L1202PresetPos, true);
                        break;
                };
              
                return;
            }


            // 天車入料(臺車入料基本上不用掃描)-Special Case?
            if (scanNoID.Equals(string.Empty))
            {
                _log.I("天車入料", $"鋼捲{msg.CoilNoID}由天車入SK1點, 掃描鋼捲為空");
                FinishEntryFlow(coilNo, scanNoID, false, msg.L1202PresetPos, false);
                return;
            }


            // 通知HMI 天車入料鋼捲ID (CoilNo為空. Or CoilNo ~= ScanNo)
            _log.I("天車入料", $"鋼捲{msg.CoilNoID}由天車入SK1點, 掃描鋼捲與實際鋼捲編號不同,通知操作確認");
            var craneEntryCoil = new SC06_CraneEntryCoil(coilNo, msg.CoilSKPos());
            MQPoolService.SendToPCCom(InfoHMI.CraneEntryCoil.Data(craneEntryCoil));

        }

        private void FinishEntryFlow (string coilNo, string scanID, bool scanOK, int presetSKNo, bool isCraneEntry) {

            // 通知HMI           
            var eventPush = new SC03_EventPush(EventDef.EntryCoilDone, coilNo + "入料完成");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            // 通知L1 Preset202
            _log.I("鋼捲入料完成", $"發Preset202 通知 L1 ");
            var preset202PDI = new Preset202PDI {
                CoilNo = coilNo,
                SkNo = presetSKNo
            };
            MQPoolService.SendToDtStp(InfoDataSetup.CoilIDTo202Msg.Data(preset202PDI));

            //update coil status
            

            // 通知MMS 鋼捲上鞍座
            _log.I("鋼捲入料完成", $"通知MMS鋼捲{coilNo}已上鞍座");
            MQPoolService.SendToMMS(InfoMMS.CoilLoadedOnSk.Data(coilNo));

            _coilController.CreateCoilScheduleDelRecords(coilNo);
            MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));

            // 撈取40筆Pro Schedule
            var coilScheduleIDs = _coilController.QueryCoilScheduleIDs(40);
            if (coilScheduleIDs == null)
                return;

            // 通知WMS排程資訊
            MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilScheduleIDs));

            // 更新鋼捲狀態為已入料
            _coilController.UpdateScheduleStatuts(coilNo, CoilDef.ScheduleStatuts.EntryCoilDone_Statuts);

            // 從排程移出已入料鋼捲
            //var isDelOK = _coilController.DeleteCoilSchedule(coilNo);
            //if (scanOK)
            //{
            //    _coilController.CreateCoilScheduleDelRecords(coilNo);
            //    MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));

            //    // 撈取40筆Pro Schedule
            //    var coilScheduleIDs = _coilController.QueryCoilScheduleIDs(40);
            //    if (coilScheduleIDs == null)
            //        return;

            //    // 通知WMS排程資訊
            //    MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilScheduleIDs));

            //    // 更新鋼捲狀態為已入料
            //    _coilController.UpdateScheduleStatuts(coilNo, CoilDef.ScheduleStatuts.EntryCoilDone_Statuts);

            //}


            if (isCraneEntry)
            {
                // 通知Preset203
                var isScanOK = scanOK ? BCScanResult.Sucess : BCScanResult.Error;
                var scnResult = new SC01_ScnBarcodeID(isScanOK, scanID, scanID, -1);
                MQPoolService.SendToL1(InfoL1.SndPDITM2203Msg.Data(scnResult));
                // 紀錄PDI Coil Check
                var updateOK = _coilController.UpdateEntryScanCoilInfo(scanID, scanOK);
            }
       

            // 開啟Check Timer
            _tmrCheckEntryStart?.Start();

        }

        // [出料]完成
        private void FinishDeliveryCoil(WMS_L2_Rcv.WGx1_CompleteOfFeeding msg)
        {
            var coilNo = msg.CoilNoID;
            var skID = msg.SkidNoID;

            // 通知HMI           
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.DeliveryCoilDone, coilNo + "出料完成");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            // 更新CoilMap (Delivery)
            //_log.Info("鋼捲出料完成", $"清空CoilMap ID = {coilNo}");
            //_trkService.UpdateCoilMapPOSCoilID("", msg.GetDeliveryPOS());

            // 告至L1發210
            short pos;
            switch (msg.SkidNoID)
            {
                case WMSSysDef.SkPos.DSK01:
                    pos = PlcSysDef.Pos.DSK01;
                    break;
                case WMSSysDef.SkPos.DSK02:
                    pos = PlcSysDef.Pos.DSK02;           
                    break;
                case WMSSysDef.SkPos.DTOP:
                    pos = PlcSysDef.Pos.DTOP;
                    break;
                default:
                     pos = Convert.ToInt16(skID);
                    break;
            };
            var delResult = new DeleteResult(pos, coilNo);
            MQPoolService.SendToL1(InfoL1.SndDelCoil210Msg.Data(delResult));
        }

        // [退料]完成
        private void FinishRetrunCoil(WMS_L2_Rcv.WGx1_CompleteOfFeeding msg)
        {
            var coilNo = msg.CoilNoID;
            var skID = msg.SkidNoID;

            // 通知HMI           
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.RejectCoilDone, coilNo + "退料完成");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            //// 更新CoilMap
            //_log.Info("物流退料完成 ", $"清空CoilMap {msg.CoilNoID}");
            //_trkService.UpdateCoilMapPOSCoilID("", msg.GetEntryPOS());



            // 告至L1發210
            short pos;
            switch (msg.SkidNoID)
            {
                case WMSSysDef.SkPos.ESK01:
                    pos = PlcSysDef.Pos.ESK01;
                    break;
                case WMSSysDef.SkPos.ETOP:
                    pos = PlcSysDef.Pos.ETOP;
                    break;
                default:
                    pos = Convert.ToInt16(skID);
                    break;
            };
            var delResult = new DeleteResult(pos, coilNo);
            //var delResult = new DeleteResult(Convert.ToInt16(skID), coilNo);
            MQPoolService.SendToL1(InfoL1.SndDelCoil210Msg.Data(delResult));
        }

        // WMS 入料/出料/退料要求回復訊息
        private void WMSResReqMsgProcess(WMS_L2_Rcv.WGx3_RequestResponse msg)
        {
            var coilNo = msg.CoilNo.ToStr();

            // 入料回應
            if (msg.PosFlag.ToStr().Equals(CoilDef.EntryCoil))
            {

                if (msg.ProcessFlag.ToStr().Equals(WMSSysDef.Cmd.No))
                {
                    // 更新鋼捲狀態為N-新鋼捲  
                    //_coilController.UpdateScheduleStatuts(coilNo, CoilDef.ScheduleStatuts.NewCoil_Statuts);
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, $"{coilNo}無法入料原因:{msg.Reason.ToStr()}")));
                    return;
                }
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, $"接收{coilNo}入料要求")));
                return;
            }

            // 出料回應
            if (msg.PosFlag.ToStr().Equals(CoilDef.DeliveryCoil))
            {
                if (msg.ProcessFlag.ToStr().Equals(WMSSysDef.Cmd.No))
                {
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, "無法出料")));
                    return;
                }
            }

            // 退料回應
            if (msg.PosFlag.ToStr().Equals(CoilDef.RejectCoil))
            {
                if (msg.ProcessFlag.ToStr().Equals(WMSSysDef.Cmd.No))
                {
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, "無法退料")));
                    return;
                }
            }

            var eventMsg = msg.ProcessFlag.Equals(WMSSysDef.Cmd.Yes) ? $"WMS回復" : $"{msg.ActionResultStr}{coilNo}要求";
            var eventPush = new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, eventMsg);
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }


        // 發送鋼捲回退實績 POR
        private void SndRejectCoilData(CS05_RejectCoil msg)
        {
            //var coilOperateReject = _coilController.GetCoilRejectResult(msg.CoilID);
            var returnCoilInfo = _coilController.GetCoilRejctTemp(msg.CoilID,msg.PlanNo);
            if (returnCoilInfo == null)
            {
                _log.E("發鋼捲回退實績", $"無{msg.CoilID},{msg.PlanNo}操作退料實績");
                return;
            }


            var parentCoil = _coilController.GetSplitParentCoilNo(msg.CoilID);
            var coilNo = parentCoil.Equals(string.Empty) ? msg.CoilID : parentCoil;
            var coilDefect = _coilController.QueryDefectData(coilNo, 40);
            var coilReject = new CoilRejectReultModel(returnCoilInfo, coilDefect);
            MQPoolService.SendToMMS(InfoMMS.CoilRejectResult.Data(coilReject));



            //// TODO 待座判定TOP 才需要通知WMS
            //_log.I("發鋼捲回退實績", $"通知WMS鋼捲{msg.CoilID}退料要求");
            //var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSRejectCoil, msg.CoilID, msg.WMSPos);
            //MQPoolService.SendToWMS(InfoWMS.RejectCoilReqMsg.Data(reqMsg));


            if (!returnCoilInfo.Reject_Coil_No.Trim().Equals(returnCoilInfo.Entry_CoilNo.Trim()))
                // 如果卷號有修改，通知一級修改回退鋼捲ID
                MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyResult(PlcSysDef.Pos.L1211SK1Pos, returnCoilInfo.Reject_Coil_No.Trim())));


        }
        
        // ID掃碼確認，入口段鋼捲ID更正
        private void RenameEntryCoil(SCCommMsg.CS04_RenameCoil msg)
        {
            // 更新PDI Entry CheckID          
            _coilController.UpdateEntryScanCoilInfo(msg.Coil_ID, true);

            // 重發204 205
            var coilScheduleIDs = new List<string>();
            coilScheduleIDs.Add(msg.Coil_ID);
            MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilScheduleIDs));
        }


        // Client通知出口段出料
        private void DeliveryCoilOut(CS14_DeliveryCoilOut msg)
        {
           // _log.I("操作通知出口段出料", $"通知一級刪除出口段位置{msg.PosStr}之鋼捲{msg.CoilID}");

            _log.I("操作手动通知出料", $"通知WMS出料{msg.CoilID}");
            var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSDeliveryCoil, msg.CoilID, msg.Pos.ToString());
            MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));


            //通知HMI 顯示手動出料資訊
            var eventPush = new SC03_EventPush("HMI手動出料", $"發送出料要求給WMS,捲號 {msg.CoilID.Trim()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            //_log.I("操作通知出口段出料", "通知210給L1");
            //var delResult = new DeleteResult(msg.L1PosDef, msg.CoilID);
            //// 告知L1發210
            //MQPoolService.SendToL1(InfoL1.SndDelCoil210Msg.Data(delResult));

        }



        // ReqTrackMap
        private void PreSndPresetMsg(SCCommMsg.CS12_Coil_SkidFeed msg)
        {
            var coilNo = msg.Coil_ID;

            var coilScheduleIDs = new List<string>();
            coilScheduleIDs.Add(coilNo);


            // 更新鋼捲狀態為已入料
            _coilController.UpdateScheduleStatuts(coilNo, CoilDef.ScheduleStatuts.EntryCoilDone_Statuts);

            // 通知MMS 鋼捲上鞍座
            MQPoolService.SendToMMS(InfoMMS.CoilLoadedOnSk.Data(coilNo));

            // 通知Setup 組202
            var preset202PDI = new Preset202PDI
            {
                CoilNo = coilNo,
                SkNo = msg.L1PresetPos
            };
            MQPoolService.SendToDtStp(InfoDataSetup.CoilIDTo202Msg.Data(preset202PDI));

        }

        // Info L1 DelCoil
        private void InfoL1210Msg(SCCommMsg.CS13_DeleteSidCoil msg)
        {
            var coilID = msg.Coil_ID.Trim();

            _log.I("手動刪除鞍座上鋼卷號", "通知210給L1");

            // 鋼捲狀態轉Idle
            _coilController.UpdateScheduleStatuts(coilID, CoilDef.ScheduleStatuts.NewCoil_Statuts);

            var delResult = new DeleteResult(msg.DelPos, msg.Coil_ID);
            // 告知L1發210
            MQPoolService.SendToL1(InfoL1.SndDelCoil210Msg.Data(delResult));
        }

        // 前筆有鋼捲 後筆為空 CoilOut
        private bool IsCoilOut(string preCoil, string NowCoil)
        {
            return (!preCoil.Equals(string.Empty) && NowCoil.Equals(string.Empty));
        }
        
        // 前筆為空 後筆有鋼捲 CoilIn
        private bool IsCoilIn(string preCoil, string NowCoil)
        {
            return (preCoil.Equals(string.Empty) && !NowCoil.Equals(string.Empty));
        }

        // Entry入料條件確認
        private void TmrEntryCheckElapsed(object sender, ElapsedEventArgs e)
        {

            if (_trkController.GetTrackMap().Entry_TOP.Trim().IsEmpty())
            {
                _entryStartConditionFlag = 0;
                _tmrCheckEntryStart?.Close();
                return;
            }

            if (_entryStartConditionFlag == PlcSysDef.Cmd.EntryConditionNG)
                return;

            _entryStartConditionFlag = 0;
            _tmrCheckEntryStart?.Close();
            MQPoolService.SendToL1(InfoL1.SndEntryTakeOVerStarCmd207Msg.Data(string.Empty));           
        }

        // Delivery出料條件確認
        private void TmrDeliveryCheckElapsed(object sender, ElapsedEventArgs e)
        {

             if (_trkController.GetTrackMap().Delivery_SK02.Trim().IsEmpty())
            {
                _deliveryStartConditionFlag = PlcSysDef.Cmd.EntryConditionNG;
                _hmiDeliveryCoilReady = PlcSysDef.Cmd.EntryConditionNG;
                _tmrCheckDeliveryStart?.Close();
                return;
            }


            if (_deliveryStartConditionFlag == PlcSysDef.Cmd.EntryConditionNG || _hmiDeliveryCoilReady == PlcSysDef.Cmd.EntryConditionNG)
                return;

            _deliveryStartConditionFlag = PlcSysDef.Cmd.EntryConditionNG;
            _hmiDeliveryCoilReady = PlcSysDef.Cmd.EntryConditionNG;
            _tmrCheckDeliveryStart?.Close();
            MQPoolService.SendToL1(InfoL1.SndDeliveryToCmd208Msg.Data(string.Empty));
        }


        // Actor Prestart (重啟時處理)
        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);

            if (_tmrCheckEntryStart != null)
            {
                _tmrCheckEntryStart.Close();
                _entryStartConditionFlag = PlcSysDef.Cmd.EntryConditionNG;
                _tmrCheckEntryStart.Elapsed -= TmrEntryCheckElapsed;             //UnRegister
            }


            if (_tmrCheckDeliveryStart != null)
            {
                _tmrCheckDeliveryStart.Close();
                _deliveryStartConditionFlag = PlcSysDef.Cmd.EntryConditionNG;
                _tmrCheckDeliveryStart.Elapsed -= TmrDeliveryCheckElapsed;             //UnRegister
            }
        }
    }
}
