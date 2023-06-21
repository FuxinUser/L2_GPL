using Akka.Actor;
using MsgStruct;
using System.Collections.Generic;
using Controller.Coil;
using DataModel.HMIServerCom.Msg;
using MSMQ;
using MSMQ.Core.MSMQ;
using Controller;
using Core.Define;
using Controller.Track;
using Core.Util;
using static DataMod.Common.MMSMsgProResultModel;
using DataMod.Common;
using AkkaSysBase.Base;
using AkkaSysBase;
using MsgConvert;
using Controller.Sys;
using CoilManager.Actor;
using static MsgStruct.MMSL2Rcv;
using static DBService.Repository.Belt.BeltAccEntity;
using static DBService.Repository.PDI.PDIEntity;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using LogSender;
using static MsgStruct.L1L2Rcv;
using static DBService.Repository.LookupTblSleeve.LkUpTableSleeveEntity;
using DataMod.WMS.LogicModel;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/03
 * Description: Coil Process 鋼捲狀態與內容變動相關處理
 * Reference: 
 * Modified: 
 */
namespace CoilManager
{
    public class CoilMgr : BaseActor
    {

        private IActorRef _selfActor;
        private IActorRef _coilProActor;

        private ICoilController _coilController;
        private ITrackingController _trkController;
        private ISysController _sysController;

        public CoilMgr(ISysAkkaManager akkaManager,
                       ITrackingController trkController,
                       ICoilController coilController,
                       ISysController sysController,
                       ILog log) : base(log)
        {
            _coilProActor = akkaManager.GetActor(nameof(CoilProActor));
            _selfActor = akkaManager.GetActor(GetType().Name);
            _trkController = trkController;
            _coilController = coilController;
            _sysController = sysController;

            _coilController.SetLog(log);
            _trkController.SetLog(log);
            _sysController.SetLog(log);

            MQPool.GetMQ(nameof(CoilMgr)).Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });

            #region 接收HMI訊息

            // 2、鋼捲生產排程順序調整與刪除作業
            Receive<CS03_ScheduleChange>(message => TryFlow(() => AdjustOrDeleteCoilSchedule(message)));
            // 上傳PDO給MMS
            Receive<CS06_SendMMSPDO>(message => TryFlow(() => SndPDO(message)));
            // 要求MMS下發排程
            Receive<CS01_AckSchedule>(message => TryFlow(() => AskMMSSndCoilSchedule(message)));
            // PDI請求
            Receive<CS02_AckPDI>(message => TryFlow(() => AskMMSSndPDI(message)));
            // HMI 完成匯入排程，通知Server發送Preset40筆
            Receive<CS16_FinishLoadSchedule>(message => TryFlow(() => SndPresetInfo()));
            // HMI 完成匯入PDI，通知Server發送Preset40筆
            Receive<CS17_FinishLoadPDI>(message => TryFlow(() => SndPresetInfo()));
            // HMI 通知刪除Dummy鋼捲
            Receive<CS20_DeleteDummy>(message => TryFlow(() => InfoDelDummyCoil(message)));

            Receive<CS23_POR_StripBreakModify>(message => TryFlow(() => POR_StripBreakModify(message)));

            #endregion

            #region 接受MMS訊息

            // 接收PDI處理（MMG102）
            Receive<Msg_PDI>(message => TryFlow(() => SaveCoilPDI(message)));
            // 鋼捲排程處理 (MMG101)
            Receive<Msg_Coil_Schedule>(message => TryFlow(() => ProCoilSchedule(message)));
            // 鋼捲回退回應（MMG107）
            Receive<Msg_Res_For_Coil_Reject_Result>(message => TryFlow(() => ProCoilRejectReuslt(message)));
            // 生產實積请求
            Receive<Msg_Product_Result_Request>(message => TryFlow(() => SndPDO(message)));
            // 作業計畫刪除請求
            Receive<Msg_Req_Delete_Schedule_Plan>(message => TryFlow(() => DeleteScheduleByPlanNo(message)));
            // 插入Dummy鋼捲
            Receive<Msg_Dummy_Coil_List>(message => TryFlow(() => SaveDummyCoil(message)));
            //Receive<Msg_Dummy_Coil_List>(message => TryFlow(() => _coilController.CreateDummyCoil(message)));
            // 套筒静态数据同步
            Receive<Msg_Sleeve_Value_Synchronize>(message => TryFlow(() => SynchronizeSleeveData(message)));
            // 墊紙静态数据同步
            Receive<Msg_Paper_Value_Synchronize>(message => TryFlow(() => SynchronizePaperData(message)));
            // 无钢卷PDI应答
            Receive<Msg_Res_For_No_Coil_PDI>(message => TryFlow(() => InfoHMINoPDI(message)));
            // 无钢卷生產应答
            Receive<Msg_Res_For_No_Coil_Schedule>(message => TryFlow(() => InfoHMINoCoil(message)));
            // Dummy鋼捲刪除回應 
            Receive<Msg_Res_of_Dummy_Coil_List_Req>(message => TryFlow(() => ProDummyCoilRejectReuslt(message)));

            //三級是否接收PDO回應處理  added 2023.04.25
            Receive <Msg_Res_RcvPDO>(message => TryFlow(() => ProResponRcvPDO(message)));

            #endregion

            #region 接收一級訊息

            // 更新Belt Acc Length
            Receive<Msg_109_Belt_ACC_Length>(message => TryFlow(() => UpdateBeltAccLength(message)));
            // 焊接訊號更新PDI時間
            Receive<Msg_106_Weld_Data>(message => TryFlow(() => ProWeldRecordToGenPDO(message)));
            // 更換皮帶
            Receive<Msg_113_Belt_Change>(message => TryFlow(() => ChangeCoilBelt(message)));
            // 紀錄Defect Data G
            Receive<Msg_108_Defect_Data>(message => TryFlow(() => RecordDefectData(message)));
            // 結算PDO
            //Receive<L1L2Rcv.Msg_102_PDO>(message => AccountPDO(message));
            Receive<Msg_102_PDO>(message => TryFlow(() => _coilProActor.Tell(message)));

            // 更新PDO毛重. 並重新計算淨重 
            Receive<Msg_110_Coil_Weight>(message => TryFlow(() => UpdatePDOCoilWeight(message)));
            // 斷帶資料處理
            Receive<Msg_124_StripBrakeSignal>(message => TryFlow(() => ProStripBrake(message)));
            // 鋼捲分切
            Receive<Msg_117_Split>(message => TryFlow(() => SplitCoil(message)));

            // 通知L1 修改TR ID
            Receive<Msg_125_Share_Cut_Data>(message => TryFlow(() => SndOutCoilIDToTR(message)));

            #endregion


            ReceiveAny(message => RcvObject(message));
            //--------測試用--------
            //var msgtest = new Msg_125_Share_Cut_Data();
            //msgtest.CutMode = 1;
            //msgtest.CoilID = ("CM200000030000").ToByteArray();
            //msgtest.RecoilerCoilTheoreticalWeight = 19900;
            //SndOutCoilIDToTR(msgtest);

            //var pdoMsgtest = new Msg_102_PDO();
            //pdoMsgtest.CoilNo = ("CG200000031000").ToByteArray();
            //pdoMsgtest.Date = 20220803;
            //pdoMsgtest.Time = 091500;
            //_coilProActor.Tell(pdoMsgtest);

            //msgtest.CutMode = 16;
            //msgtest.CoilID = ("CM200000030000").ToByteArray();
            //msgtest.RecoilerCoilTheoreticalWeight = 5000;
            //SndOutCoilIDToTR(msgtest);

            //pdoMsgtest.CoilNo = ("CG200000030000").ToByteArray();
            //pdoMsgtest.Date = 20220810;
            //pdoMsgtest.Time = 080100;
            //_coilProActor.Tell(pdoMsgtest);
            //var SendMMSPDOmsg = new CS06_SendMMSPDO();
            //SendMMSPDOmsg.Coil_ID = "CG200000030000";
            //SendMMSPDOmsg.In_Coil_ID = "CM200000030000";
            //SendMMSPDOmsg.Plan_No = "A12345";
            //SndPDO(SendMMSPDOmsg);
            //var test = _coilController.GetPDIPlanNo("CG220041581000");

            //var testMsg = new Msg_Res_For_Coil_Reject_Result();
            //testMsg.Requested_Coil_No = ("CG200000030000").ToByteArray();
            //testMsg.Process_Result = ("0").ToByteArray();
            //testMsg.Reject_Cause = (" ").ToByteArray();
            //ProCoilRejectReuslt(testMsg);
            //_coilController.VaildHasScheduleTemp(testMsg.RequestedCoilNo);
            //if (_coilController.VaildHasCoilMap(testMsg.RequestedCoilNo, PlcSysDef.Pos.ESK01))
            //{
            //    _log.I("發鋼捲回退實績", $"通知WMS,鋼捲{testMsg.RequestedCoilNo.Trim()}退料要求,位置：ESK01");

            //}
            //else
            //{
            //    if (_coilController.VaildHasCoilMap(testMsg.RequestedCoilNo, PlcSysDef.Pos.ETOP))
            //    {
            //        _log.I("發鋼捲回退實績", $"通知WMS,鋼捲{testMsg.RequestedCoilNo.Trim()}退料要求,位置：ETOP");

            //    }
            //}
            //var testMsg = new Msg_Res_RcvPDO();
            //testMsg.Respon_Coil_No = ("CG200000042000").ToByteArray();
            //testMsg.Respon_Plan_No = ("A12345").ToByteArray();
            //testMsg.Success_Flag = ("1").ToByteArray();
            //ProResponRcvPDO(testMsg);

            ////測試PDO反饋報文
            //var testMsg = new Msg_Res_RcvPDO();
            //testMsg.Respon_Coil_No = ("CG220129870000").ToByteArray();
            //testMsg.Respon_Plan_No = ("GP2C0119").ToByteArray();
            //testMsg.Success_Flag = ("1").ToByteArray();
            //testMsg.Error_Reason = ("此鋼捲还未上抛").ToByteArray();
            //ProResponRcvPDO(testMsg);

            //--------測試用--------

            //
        }


        #region L1

        private void SndOutCoilIDToTR(Msg_125_Share_Cut_Data msg)
        {
            var cutMode = msg.CutMode;
            //Weld Cut
            // 撈出口捲給一級
            if (cutMode == PlcSysDef.Cmd.ShareCutWeldCut)
            {

                var coilID = msg.CoilID.ToStr();
                //尋找分切紀錄
                var SplitCnt = _coilController.GetCntSplitRec(coilID);
                var TRCoilWeight = msg.TRCoilWeight;
                var ProductCoilWeight = _sysController.GetSysProCoilWeight(); //鋼捲成捲重量限制
                var parentCoilID = _coilController.GetSplitParentCoilNo(coilID);
                coilID = parentCoilID.Equals(string.Empty) ? coilID : parentCoilID;
                var pdi = _coilController.GetPDI(coilID);
                var outCoilID = pdi.Out_Coil_ID;

                //更新PDI 鋼捲生產結束時間
                //_coilController.UpdatePDIEndTime(coilID.Trim());


                //重量小於500kg 不成捲
                if (TRCoilWeight < (float)(ProductCoilWeight))
                {
                    _log.I("重量小於500kg", "不成捲");
                    return;
                }


                //更新PDI 鋼捲生產結束時間  by out_coilID
                _coilController.UpdatePDIEndTime(outCoilID.Trim());

                if (SplitCnt == 0)  //無分切紀錄
                {
                    _log.I("無分切紀錄", $"SplitCnt:{SplitCnt.ToString()}");

                    var mqMsg = new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, outCoilID);
                    MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(mqMsg));

                }

                if (SplitCnt > 0)
                {
                    _log.I("有分切紀錄", $"SplitCnt:{SplitCnt.ToString()}");
                    var ChildrenCoilNo = _coilController.GenSplitChildrenCoilNo(coilID);
                    if (ChildrenCoilNo != null)
                    {
                        //有無產出PDO
                        var pdo = _coilController.GetPDO(ChildrenCoilNo);
                        if (pdo == null)
                        {
                            var inserOK = _coilController.VaildNewChildCoilNoData(ChildrenCoilNo, coilID);
                            if (inserOK)
                                MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, ChildrenCoilNo)));
                        }
                    }
                }
                return;
            }

            // Split Cut 處理
            if (cutMode == PlcSysDef.Cmd.ShareCutSplitCut)
            {
                var coilID = msg.CoilID.ToStr();
                var TRCoilWeight = msg.TRCoilWeight;
                var ProductCoilWeight = _sysController.GetSysProCoilWeight(); //鋼捲成捲重量限制
                var pdi = _coilController.GetPDI(coilID); //取得pdi資料
                var outCoilID = pdi.Out_Coil_ID;
                var SplitCnt = _coilController.GetCntSplitRec(coilID);//尋找分切紀錄

                //更新PDI 鋼捲生產結束時間
                _coilController.UpdatePDIEndTime(outCoilID.Trim());


                // ReCoil Weight 判斷                     
                if (TRCoilWeight < (float)(ProductCoilWeight))
                {
                    // ReCoil Weight 小於 500kg 不處理
                    _log.I("報文125 Split Cut處理", $"重量 < {ProductCoilWeight.ToString()}  TRCoilWeight:{TRCoilWeight.ToString()}");

                }
                else // ReCoil Weight 大於 500kg
                {
                    _log.I("報文125 Split Cut處理", $"重量 > {ProductCoilWeight.ToString()}  TRCoilWeight:{TRCoilWeight.ToString()}");


                    if (pdi == null)
                    {
                        _log.E("無PDI資料", $"TR上無{coilID}此筆鋼卷PDI資料");
                        return;
                    }

                    var RemainWeight = (float)(pdi.In_Coil_Wt) - TRCoilWeight;//取得剩餘重量
                    if (RemainWeight <= 0)
                    {
                        _log.E("PDI入料重量有誤", $"CoilID:{coilID} Weight:{pdi.In_Coil_Wt.ToString()}");
                        return;
                    }

                    // 狀況一、剩餘重量小於500kg   -> 剩餘廢切
                    if (RemainWeight < (float)(ProductCoilWeight))
                    {

                        _log.I("報文125 Split Cut處理", $"剩餘未收卷重量:{RemainWeight.ToString()}");
                        if (SplitCnt == 0)  //無分切紀錄
                        {
                            _log.I("無分切紀錄", $"SplitCnt:{SplitCnt.ToString()}");

                            var mqMsg = new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, outCoilID);
                            MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(mqMsg));

                        }

                        if (SplitCnt > 0)
                        {
                            _log.I("有分切紀錄", $"SplitCnt:{SplitCnt.ToString()}");
                            var ChildrenCoilNo = _coilController.GenSplitChildrenCoilNo(coilID);
                            if (ChildrenCoilNo != null)
                            {
                                //有無產出PDO
                                var pdo = _coilController.GetPDO(ChildrenCoilNo);
                                if (pdo == null)
                                {
                                    var inserOK = _coilController.VaildNewChildCoilNoData(ChildrenCoilNo, coilID);
                                    if (inserOK)
                                        MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, ChildrenCoilNo)));
                                }
                            }
                        }

                    }
                    else // 狀況二、剩餘重量大於500kg 建立分切紀錄,並給子捲號
                    {
                        _log.I("報文125 Split Cut處理", $"剩餘未收卷重量:{RemainWeight.ToString()}");
                        if (SplitCnt == 0)  //無分切紀錄
                        {
                            _log.I("無分切紀錄", $"SplitCnt:{SplitCnt.ToString()}");

                            //分切,取得子捲號
                            var ChildrenCoilNo = _coilController.GenSplitChildrenCoilNo(coilID);

                            if (ChildrenCoilNo != null)
                            {
                                //有無產出PDO
                                var pdo = _coilController.GetPDO(ChildrenCoilNo);
                                if (pdo == null)
                                {
                                    var inserOK = _coilController.VaildNewChildCoilNoData(ChildrenCoilNo, coilID);
                                    if (inserOK)
                                        MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, ChildrenCoilNo)));
                                }
                            }

                        }

                        if (SplitCnt > 0)
                        {
                            _log.I("有分切紀錄", $"SplitCnt:{SplitCnt.ToString()}");
                            var ChildrenCoilNo = _coilController.GenSplitChildrenCoilNo(coilID);
                            if (ChildrenCoilNo != null)
                            {
                                //有無產出PDO
                                var pdo = _coilController.GetPDO(ChildrenCoilNo);
                                if (pdo == null)
                                {
                                    var inserOK = _coilController.VaildNewChildCoilNoData(ChildrenCoilNo, coilID);
                                    if (inserOK)
                                        MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, ChildrenCoilNo)));
                                }
                            }
                        }
                    }

                }
                return;
            }
        }

        private void UpdateBeltAccLength(L1L2Rcv.Msg_109_Belt_ACC_Length msg)
        {
            for (int grNo = 1; grNo < 7; grNo++)
            {
                var beltLength = msg.GetBeltAccLength(grNo);
                var stLength = msg.GetStAccLength(grNo);
                _coilController.UpdateBeltAccLength(grNo, beltLength, stLength);
            }
        }

        private void ProWeldRecordToGenPDO(L1L2Rcv.Msg_106_Weld_Data msg)
        {

            var curTRCoil = _trkController.GetTrackMap().TR.Trim(); // TR鋼捲      
            var curPORCoil = msg.CoilIDNo;                      // POR鋼捲 

            var PORPDI = _coilController.GetPDI(curPORCoil);
            var TRPDI = _coilController.GetPDI(curTRCoil);

            if (PORPDI == null)
            {
                _log.E("無PDI資料", $"POR上無{curPORCoil}此筆鋼卷PDI資料");
                return;
            }
            //if (TRPDI == null)
            //{
            //    _log.Error("無PDI資料", $"TR上無{curTRCoil}此筆鋼卷PDI資料");
            //    return;
            //}

            // TR 紀錄PDO結束時間(至PDI)
            //_coilService.UpdatePDOFinishTime(curTRCoil, DateTime.Now);
            //if (TRPDI != null)
            //    _coilController.UpdatePDIEndTime(curTRCoil);

            // POR 產生PDI生產開始時間 
            //_coilService.GenEmptyPDO(PORPDI);
            //_coilController.UpdatePDIStarTime(curPORCoil);

            //產生PDI生產開始時間 
            _coilController.UpdatePDIStarTime(curPORCoil);

        }

        private void ChangeCoilBelt(L1L2Rcv.Msg_113_Belt_Change msg)
        {
            // 更新

            // 更新舊皮帶 : Belt Length && Strip Length && GR No = 0
            var updateOldBeltLength = _coilController.UpdateAccLengthByBeltNo(msg.OldBeltNo, msg.OldABBeltAccGriBeltLength, msg.OldABBeltAccGriStLength);
            var updateOldBeltGrNo = _coilController.UpdateGrNoByBeltNo(msg.OldBeltNo, 0);
            // 更新新皮帶 : GrNo = msg.GRNo
            var updateNewBeltGrNo = _coilController.UpdateGrNoByBeltNo(msg.NewBeltNo, msg.GRNo);

            // 索取新皮帶資訊
            var belt = _coilController.GetBelt(msg.NewBeltNo);

            if (belt == null || !Pro113BeltChangeInfoOk(updateOldBeltLength, updateOldBeltGrNo, updateNewBeltGrNo))
            {
                _log.E("更新皮帶失敗", $"新皮帶編號{msg.NewABBeltID.ToStr()}");
                _log.E("更新皮帶失敗", $"舊皮帶編號{msg.OldABBeltID.ToStr()} Belt長度{msg.OldABBeltAccGriBeltLength} St長度{msg.OldABBeltAccGriStLength}");
                MQPoolService.SendToL1(InfoL1.SndNewBeltInfo206Msg.Data(new TBL_Belts()));

                //通知HMI
                var eventPush = new SC03_EventPush("更新皮帶失敗", $"新皮帶編號{msg.NewABBeltID.ToStr()} 舊皮帶編號{msg.OldABBeltID.ToStr()}  Belt長度{msg.OldABBeltAccGriBeltLength} St長度{msg.OldABBeltAccGriStLength}");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                return;
            }

            MQPoolService.SendToL1(InfoL1.SndNewBeltInfo206Msg.Data(belt));

            //// 索取新皮帶資訊
            //var belt = _coilService.GetBelt(msg.NewBeltNo);
            //// 傳至L1 發206報文
            //if (belt != null && Pro113BeltChangeInfoOk(updateOldBeltLength, updateOldBeltGrNo, updateNewBeltGrNo))
            //    MQPoolService.SendToL1(InfoL1.SndNewBeltInfo206Msg.Data(belt));
        }

        private bool Pro113BeltChangeInfoOk(bool updateOldBeltLength, bool updateOldBeltGrNo, bool updateNewBeltGrNo)
        {
            return updateOldBeltLength && updateOldBeltGrNo && updateNewBeltGrNo;
        }

        private void RecordDefectData(L1L2Rcv.Msg_108_Defect_Data msg)
        {

            var preDefectData = _coilController.GetPreDefectDataByPlcMsg(msg);

            var curLengthFromHeadEnd = msg.LengthFromHeadEnd;

            //var preLastLength = _coilService.GetDefectRecordLastLength();


            // 不同Defect資料, 直接新增一筆
            if (preDefectData == null)
            {
                _coilController.CreateDefectData(msg);
                return;
            }



            // 同筆Defect資料，判斷Overlap
            if (IsIntervalLengthLessOneMeter(preDefectData.DefectPoLengthEndDirection, curLengthFromHeadEnd))
            {
                _coilController.UpdateDefectData(msg, preDefectData.DefectPosLengthStartDirection);
            }
            else
            {
                _coilController.CreateDefectData(msg);
            }


        }


        private bool IsIntervalLengthLessOneMeter(float preLength, float curLength)
        {
            return curLength - preLength <= 1;   //單位為米      
        }

        //private bool IsSameDefect(L3L2_TBL_DefectData preDefectData, L1L2Rcv.Msg_108_Defect_Data curDefectData)
        //{
        //    var DefectPositionWidthDirection = curDefectData.DefectPosition.TransferMMSDeffectPosWidthType();

        //    var isSameDirection = preDefectData.DefectPositionWidthDirection.Equals(DefectPositionWidthDirection);
        //    var isSameLevel = preDefectData.DefectLevel == curDefectData.DefectLevel;
        //    var isSameCode = preDefectData.DefectCode == curDefectData.DefectKind;
        //    var isSameCoil = preDefectData.CoilID.Equals(curDefectData.CoilID);

        //    return isSameDirection && isSameLevel && isSameCode && isSameCoil;
        //}

        /// <summary>
        /// 更新PDO毛重. 並重新計算淨重 
        /// </summary>
        private void UpdatePDOCoilWeight(L1L2Rcv.Msg_110_Coil_Weight msg)
        {
            _log.I("接收CoilWeightScale", "接收到CoilWeightScale重新結算PDO重量");
            _log.D("接收CoilWeightScale", JsonUtil.ToJson(msg));
            _coilController.UpdatePDOWeight(msg.CoilIDNo, msg.CoilWeight);
        }

        private void ProStripBrake(L1L2Rcv.Msg_124_StripBrakeSignal msg)
        {
            var ProcessingCoil = msg.PORCoilNo;

            var TRCoil = msg.TRCoilNo;
            var TRCoilWeight = msg.TRCoilWeight;
            var ProductCoilWeight = _sysController.GetSysProCoilWeight();


            // 狀況一、B卷-A卷 => 成卷 各自收卷處理
            if (!ProcessingCoil.Equals(TRCoil))
            {
                _log.I("成捲，各自收卷處裡", $"POR:{ProcessingCoil}  TR:{TRCoil}");
                return;
            }

            // 狀況二、A卷-A卷 且TR不成捲 => TR重量小於500 : 紀錄廢料重量至PDI
            if (TRCoilWeight < ProductCoilWeight)
            {
                //_log.Info("紀錄廢料", $"POR:{PORCoil}  TR:{TRCoil}");
                //_coilService.UpdatePDIScrapedWeight(TRCoilWeight, TRCoil);
                return;
            }

            // 狀況三、A卷-A卷 且TR成捲(TR重量>500) => Gen New ID 並存取 && 發211
            _log.I("POR與TR鋼卷相同", $"POR : {ProcessingCoil} TR:{TRCoil}");

            // 分切 - A-A(TR) TR鋼捲做分切 然後給新捲號
            var coilNewID = _coilController.GenSplitChildrenCoilNo(TRCoil);

            var inserOK = _coilController.VaildNewChildCoilNoData(coilNewID, TRCoil);

            if (!inserOK)
            {
                _log.E("分切紀錄失敗", "鋼卷分切記錄到SplitCoil資料庫失敗");
                return;
            }


            _coilController.UpdatePDIEndTime(TRCoil);

            MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, coilNewID)));

        }

        private void SplitCoil(L1L2Rcv.Msg_117_Split msg)
        {
            var parentCoil = msg.CoilID.ToStr();
            var coilNewID = _coilController.GenSplitChildrenCoilNo(parentCoil);
            SaveSplitCoilAndSndToL1(coilNewID, parentCoil);
        }

        private void SaveSplitCoilAndSndToL1(string childCoil, string parentCoil)
        {
            var childCoilNo = _coilController.GenSplitChildrenCoilNo(parentCoil);
            var inserOK = _coilController.VaildNewChildCoilNoData(childCoilNo, parentCoil);
            if (inserOK)
                MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211TRPos, childCoil)));
            //if (!childCoil.Equals(string.Empty))
            //    _coilService.NewChildCoilNoData(childCoil, parentCoil);
            //MQPoolUtil.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(TrackMapDef.POSTR, childCoil)));
        }

        #endregion

        #region MMS相關

        private void InfoDelDummyCoil(CS20_DeleteDummy message)
        {

            _log.I("過度捲刪除訊息", $"刪除鋼捲{message.DummyCoil}排程訊息,原因{message.ReasonCode}");

            var saveOk = _coilController.CreateTempCoilScheduleDelRecord(message.DummyCoil);

            if (!saveOk)
            {
                var eventPush = new SC03_EventPush("刪除過度鋼捲程序失敗,存取鋼捲刪除暫存失敗");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
                return;
            }

            // 發送鋼捲刪除命令(G1MM18)           
            MQPoolService.SendToMMS(InfoMMS.DeleteDummyCoil.Data(message));

        }
        private void POR_StripBreakModify(CS23_POR_StripBreakModify message)
        {
            var ParentCoilID = message.Coil_ID.Trim();
            //給予子捲號(B捲)ID
            var coilNewID = _coilController.GenSplitChildrenCoilNo(ParentCoilID);


            _log.I("斷帶作業-更改POR捲號", $"修改捲號為{coilNewID}");
            MQPoolService.SendToL1(InfoL1.SndModifyCoilID211Msg.Data(new ModifyCoilModel.ModifyResult(PlcSysDef.Pos.L1211PORPos, coilNewID)));


            var eventPush = new SC03_EventPush("斷帶作業-更改POR捲號", $"修改捲號為{ coilNewID }");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
            return;

        }

        /// <summary>
        /// 結算PDO 
        /// </summary>
        private void AccountPDO(L1L2Rcv.Msg_102_PDO msg)
        {
            _log.I("結算PDO", "收到CoilDismount訊息，開始結算PDO");
            _log.D("結算PDO", msg.ToJson());

            var pdo = _coilController.CalculatePDOResult(msg);
            if (pdo == null)
            {
                _log.E("PDO產生失敗", "PDO計算失敗");
                return;
            }

            var updateOK = _coilController.UpdatePDO(pdo);
            //// 存DB
            //if (_coilService.HasPDO(msg.CoilIDNo))
            //{
            //    _coilService.UpdatePDO(pdo);
            //}
            //else
            //{
            //    _coilService.InsertPDO(pdo);
            //}

            // 更新鋼捲狀態 - Done
            _coilController.UpdateScheduleStatuts(pdo.In_Coil_ID, CoilDef.ScheduleStatuts.ProduceDone_Statuts);

            // GW13 鋼捲產出資訊 - PDO資訊
            if (!updateOK)
            {
                _log.E("PDO存取DB失敗", "PDO產生成功，但存取失敗，請重新操作");
                return;
            }

            var SleeveData = _coilController.GetSleeveData(pdo.Out_Sleeve_Type_Code);
            var wmsPdoInfo = pdo.ConvertWMSPdoInfo(SleeveData.Out_Mat_Inner_Dia.ToString(), SleeveData.Sleeve_Thick.ToString("0.000"), SleeveData.Sleeve_Width.ToString("0.0"));
            MQPoolService.SendToWMS(InfoWMS.InfoiCoilPDOMsg.Data(wmsPdoInfo));



        }

        /// <summary>
        /// 鋼捲生產排程順序調整與刪除作業
        /// </summary>
        /// <param name="msg"></param>
        private void AdjustOrDeleteCoilSchedule(CS03_ScheduleChange msg)
        {
            // 鋼捲生產排程順序[調整]
            if (msg.SchStatus == ScheduleStatus.ADJUST)
            {
                //ConsoleOut.Info("收到HMI排程調整訊息");
                _log.I("鋼捲排程調整", $"調整鋼捲{msg.EntryCoilID}排程訊息");

                // 撈取40筆Pro Schedule
                var coilScheduleIDs = _coilController.QueryCoilScheduleIDs(40);
                if (coilScheduleIDs == null)
                    return;

                // 通知WMS排程資訊
                MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilScheduleIDs));

                // 通知MMS鋼捲排程改變 - 發送生產命令順序變更通知（G1MM10）
                MQPoolService.SendToMMS(InfoMMS.CoilSceduleChanged.Data(coilScheduleIDs));

                // 重發Preset給L1
                MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilScheduleIDs));


                return;
            }

            // 鋼捲生產排程[刪除]
            if (msg.SchStatus == SCCommMsg.ScheduleStatus.DELETE)
            {

                _log.I("鋼捲刪除訊息", $"刪除鋼捲{msg.EntryCoilID}排程訊息，人員 {msg.OperatorID},原因{msg.ReasonCode}");

                var saveOk = _coilController.CreateTempCoilScheduleDelRecord(msg.EntryCoilID, msg.OperatorID, msg.ReasonCode);

                if (!saveOk)
                {
                    var eventPush = new SC03_EventPush("刪除鋼捲程序失敗,存取鋼捲刪除暫存失敗");
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
                    return;
                }

                // 發送鋼捲刪除命令(G1MM18)           
                MQPoolService.SendToMMS(InfoMMS.CoilSceduleDeleted.Data(msg));

                //// 撈取40筆排程鋼卷                
                //var coilScheduleIDs = _coilService.GetCollScheduleIDs(40);
                //if (coilScheduleIDs == null)
                //    return;

                //// 通知WMS排程資訊
                //MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilScheduleIDs));

                //// 重發Preset給L1
                //MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilScheduleIDs));
            }
        }

        private void SndPDO(CS06_SendMMSPDO msg)
        {

            var coilMap = _trkController.GetTrackMap();
            var coilNo = msg.Coil_ID;  //out_coil_id
            var planNo = msg.Plan_No;

            // 告知MMSSndEdit發送PDO
            _log.I("要求PDO上傳MMS", $"要求上傳{coilNo}的PDO");
            MQPoolService.SendToMMS(InfoMMS.ClientInfoSndPDO.Data(msg));


            var eventPush = new SC03_EventPush("PDO已上傳給MMS", $"捲號:{coilNo},計畫號:" + planNo);
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            //// 通知WMS鋼卷生產實績
            ////var pdo = _coilController.GetPDO(coilNo);
            var pdo = _coilController.GetPDOonly(planNo, coilNo);
            //var SleeveData = _coilController.GetSleeveData(pdo.Out_Sleeve_Type_Code);
            //var wmsPdoInfo = pdo.ConvertWMSPdoInfo(SleeveData.Out_Mat_Inner_Dia.ToString(), SleeveData.Sleeve_Thick.ToString("0.000"), SleeveData.Sleeve_Width.ToString("0.0"));
            //MQPoolService.SendToWMS(InfoWMS.InfoiCoilPDOMsg.Data(wmsPdoInfo));

            //eventPush = new SC03_EventPush("通知WMS鋼捲生產實績", $"捲號:{coilNo},計畫號:" + planNo);
            //MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));


            // MMS
            //_log.I("更新PDO Upload旗標", "");
            //_coilController.UpdateUploadFlag(planNo, coilNo, true);

            // 刪除鋼捲排程
            _coilController.DeleteCoilScheduleOnly(pdo.In_Coil_ID);


            ////尋找是否有分切 找母材鋼捲
            //var ParentCoilNo = _coilController.GetSplitParentCoilNo(coilNo);

            //if (ParentCoilNo.Equals(string.Empty)) //parentCoilNo 空白
            //{
            //    //新增PDI至25
            //    var pdi = _coilController.GetPDIOnly(planNo, pdo.In_Coil_ID);
            //    _coilController.CreateL25PDI(pdi);
            //}
            //else //有parentCoilNo,給L25母材pdi 
            //{
            //    //新增PDI至25
            //    var pdi = _coilController.GetPDIOnly(planNo, ParentCoilNo);
            //    _coilController.CreateL25PDI(pdi);
            //}

            //// 新增PDO至25
            //_coilController.CreateL25PDO(planNo, coilNo);


            // 發送208
            if (coilMap != null && coilMap.TR.Trim().Equals(msg.Coil_ID.Trim()))
                MQPoolService.SendToL1(InfoL1.SndDeliveryToCmd208Msg.Data(string.Empty));

            // 補刪除排程
            _log.I("補刪除排程", $"補刪除排程{msg.In_Coil_ID}");
            var isDelOk = _coilController.DeleteCoilSchedule(msg.In_Coil_ID);
            if (isDelOk)
                _coilController.CreateCoilScheduleDelRecords(msg.In_Coil_ID, "HMI", "", "已产出PDO");


            //Dummy Coil update   將PDO資料更新到PDI
            if (pdo.Order_No.Trim().Equals("DUMMY"))
            {
                _coilController.UpdateDummyPDI(pdo.In_Coil_ID, pdo);
            }
        }

        /// <summary>
        /// 流程1 : 鋼捲生產排程接收流程 (存取MMS下發的PDI資料)
        /// </summary>
        private void SaveCoilPDI(Msg_PDI mmsPDI)
        {
            int infNum;
            var entryCoil = mmsPDI.EntryCoilNo;
            var planNo = mmsPDI.PlanNo;
            var matSeqNo = mmsPDI.MatSeqNo;

            //if (_coilController.VaildHasPDI(entryCoil))
            //    // 有:更新PDI
            //    infNum = _coilController.UpdatePDI(entryCoil, mmsPDI);
            //else
            //    // 無:新增PDI
            //    infNum = _coilController.CreatePDI(mmsPDI);

            if (_coilController.VaildHasPDIandPlanNo(entryCoil, planNo))
                // 有:更新PDI
                infNum = _coilController.UpdatePDI(entryCoil, mmsPDI);
            else
                // 無:新增PDI
                infNum = _coilController.CreatePDI(mmsPDI);


            // 通知MMS
            var proOk = infNum != 0 ? EventDef.processOk : EventDef.processError;
            var rejCode = proOk == EventDef.processOk ? "OK" : "Save Fail";
            MQPoolService.SendToMMS(InfoMMS.SndPDIProResult.Data(new ProResult(mmsPDI.EntryCoilNo, proOk, rejCode)));

            // 通知HMI
            var proStr = proOk.Equals(EventDef.processOk) ? "成功" : "失敗" + "因為" + rejCode;
            var proContent = "計畫號:" + planNo + "|材料命令順序號:" + matSeqNo + "|鋼捲ID:" + mmsPDI.EntryCoilNo;
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.ReceiveMMSPDI + proStr, proContent);
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            // 發送Preset
            //if (proOk == EventDef.processOk)
            //    SndPresetInfo();

            //var pdi = _coilController.GetPDI(entryCoil);
            //_coilController.CreateL25PDI(pdi);

        }
        private void SaveDummyCoil(Msg_Dummy_Coil_List dummyCoil)
        {

            var entryCoil = dummyCoil.CoilNoID;
            if (_coilController.VaildHasDummy(entryCoil))
            {
                // 有:更新PDI  先刪除,後新增
                _coilController.DeleteDummy(entryCoil);
                _coilController.CreateDummyCoil(dummyCoil);

            }
            else
            {
                // 無:新增PDI
                _coilController.CreateDummyCoil(dummyCoil);
            }


        }

        /// <summary>
        /// 鋼捲排程處理 (MMG101)
        /// </summary>
        private void ProCoilSchedule(Msg_Coil_Schedule mmsSchedule)
        {
            // TODO 比數ID對不起來回拒絕, 最後一筆是不是空白
            var coilSchedProOk = false;
            var coilCont = mmsSchedule.CoilCount;
            var entryCoils = mmsSchedule.EntryCoilNos;
            var isProOk = string.Empty;
            var rejectCause = string.Empty;

            // 通知HMI已收到幾筆鋼捲
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.ReceiveMMSCoilSchedule, $"已收到{coilCont}筆鋼卷");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            // CoilID:0 -> 整筆刪除,整筆插入
            if (mmsSchedule.CoilNo.Equals(MMSSysDef.Cmd.InsertAllCoilSchedule))
            {

                _sysController.UpdateSysValue(L2SystemDef.GPLGroup, DBParaDef.SysTopScheduleLock, DBParaDef.NOTUSE);

                // 刪除所有鋼捲
                //_coilController.DeleteAllCoilSchedule();
                // 刪除所有idle鋼捲
                _coilController.DeleteAllIdleSchedule();




                // 直接插入所有鋼捲
                coilSchedProOk = _coilController.SequenceCreateSchedule(entryCoils, coilCont);

                //重新將目前排程SeqNo依序排列


                // 回應處理結果               
                CoilScheduleInsertDoneRes(entryCoils, coilSchedProOk);

                return;
            }

            // CoilID: Schedule有此Coil，此Coil以下刪除，重新插入鋼捲
            if (_coilController.VaildHasCoilSchedule(mmsSchedule.CoilNo))
            {
                // TODO 判斷是否已進產線(CoilMap比對)-暫留

                // 刪除此CoilID以下的資料
                coilSchedProOk = _coilController.RemoveScheduleCoilIDs(mmsSchedule.CoilNo);

                // 順序插入
                if (coilSchedProOk)
                    coilSchedProOk = _coilController.SequenceCreateSchedule(mmsSchedule.EntryCoilNos, mmsSchedule.CoilCount);

                // 回應處理結果             
                CoilScheduleInsertDoneRes(mmsSchedule.CoilNo, coilSchedProOk);

                return;
            }

            //// CoilID: Schedule無此ID，直接插入所有鋼捲 (Append)
            //coilSchedProOk = _coilService.SequentialInsertInDB(entryCoils, coilCont);
            //// 回應處理結果
            //CoilScheduleInsertDoneRes(mmsSchedule.CoilNo, coilSchedProOk);

            CoilScheduleInsertDoneRes(mmsSchedule.CoilNo, false, EventDef.NoSchedule);



        }

        /// <summary>
        /// 過度卷鋼捲刪除回應
        /// </summary>       
        public void ProDummyCoilRejectReuslt(Msg_Res_of_Dummy_Coil_List_Req msg)
        {
            //通知HMI  
            var isSucess = msg.Dummy_Deal_Reult.ToStr().Equals(MMSSysDef.Cmd.ProNG) ? EventDef.DummyCoilRejectFail : EventDef.DummyCoilRejectSuccess;
            var dummyCoilNo = msg.Dummy_Coil_No.ToStr();

            var planNo = _coilController.GetPDIPlanNo(dummyCoilNo);


            //var eventPush = new SC03_EventPush(isSucess+ dummyCoilNo);
            var eventPush = new SC03_EventPush("過度卷刪除回應" + isSucess, dummyCoilNo + " ");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));


            _log.I("過度卷刪除回應", $"捲號{dummyCoilNo.Trim()} 审核结果{isSucess.Trim()}");

            if (msg.Dummy_Deal_Reult.ToStr().Equals(MMSSysDef.Cmd.ProNG))
                return;

            // 刪除流程
            var isDelOk = _coilController.DeleteCoilSchedule(dummyCoilNo);
            if (isDelOk)
            {
                //_coilController.DelCoilScheduleDelTempRecord(dummyCoilNo);
                _coilController.CreateCoilScheduleDelRecords(dummyCoilNo, "MMS Response", "", "過度捲单捲删除");
            }
        }


        /// <summary>
        /// 鋼捲回退(刪除)回應
        /// </summary>       
        public void ProCoilRejectReuslt(Msg_Res_For_Coil_Reject_Result msg)
        {

            var coilNo = msg.RequestedCoilNo;
            //找planNo
            var planNo = _coilController.GetPDIPlanNo(coilNo);

            //通知HMI  
            var isSucess = msg.ProcessResult.Equals(MMSSysDef.Cmd.ProNG) ? EventDef.CoilRejectFail : EventDef.CoilRejectSuccess;
            var eventPush = new SC03_EventPush("刪除回退回應" + isSucess, coilNo + " " + msg.RejectCause);
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            _log.I("三級刪除回退回應", $"捲號{coilNo.Trim()} 审核结果{isSucess.Trim()} {msg.RejectCause.Trim()}");
            if (msg.ProcessResult.Equals(MMSSysDef.Cmd.ProNG))
            {
                //_coilController.DelCoilScheduleDelTempRecord(coilNo);
                _coilController.DelCoilRejectTempRecord(coilNo, planNo);
                return;
            }

            if (_coilController.VaildHasCoilSchedule(coilNo))
            {
                // 刪除流程
                var isDelOk = _coilController.DeleteCoilSchedule(coilNo);
                if (isDelOk)
                {
                    //_coilController.DelCoilScheduleDelTempRecord(coilNo);              
                    _coilController.CreateCoilScheduleDelRecords(coilNo, "MMS Response", "", "回退纪录");
                }


                // 撈取40筆Pro Schedule
                var coilScheduleIDs = _coilController.QueryCoilScheduleIDs(40);
                if (coilScheduleIDs == null)
                    return;
                // 通知WMS排程資訊
                MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilScheduleIDs));

                //通知WMS退料       
                if (_coilController.VaildHasCoilMap(coilNo, PlcSysDef.Pos.ESK01))
                {
                    _log.I("發鋼捲回退實績", $"通知WMS鋼捲{coilNo.Trim()}退料要求,位置：ESK01");
                    var eventPush_RejectCoil = new SC03_EventPush("鋼捲回退-通知WMS退料", $"捲號：{ coilNo },位置：ESK01");
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush_RejectCoil));

                    var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSRejectCoil, coilNo, "1");
                    MQPoolService.SendToWMS(InfoWMS.RejectCoilReqMsg.Data(reqMsg));


                }
                else
                {
                    if (_coilController.VaildHasCoilMap(coilNo, PlcSysDef.Pos.ETOP))
                    {
                        _log.I("發鋼捲回退實績", $"通知WMS鋼捲{coilNo.Trim()}退料要求,位置：ETOP");
                        var eventPush_RejectCoil = new SC03_EventPush("鋼捲回退-通知WMS退料", $"捲號：{ coilNo },位置：ETOP");
                        MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush_RejectCoil));

                        var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSRejectCoil, coilNo, "2");
                        MQPoolService.SendToWMS(InfoWMS.RejectCoilReqMsg.Data(reqMsg));

                    }
                    else
                    {
                        _log.I("發鋼捲回退實績", $"鋼捲{coilNo.Trim()}退料要求位置不在ESK01或ETOP上");
                    }
                }

                // 重發Preset給L1
                MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilScheduleIDs));

                // 通知HMI排程已更新
                MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("鋼捲排程刪除", "已處理完MMS排程訊息")));



            }
            else
            {
                _log.I("無排程紀錄", $"捲號：{coilNo.Trim()}");
                return;
            }


            // 回退流程-將Temp copy至 TBL_CoilRejectResult 
            _coilController.SyncCoilRejectData(coilNo, planNo);


        }

        /// <summary>
        /// 生產實積請求
        /// </summary>
        public void SndPDO(Msg_Product_Result_Request msg)
        {
            _log.I("要求生產實績(PDO)", $"要求鋼捲{msg.CoilNoID}PDO資料");
            MQPoolService.SendToMMS(InfoMMS.MMSInfoSndkPDO.Data(msg.CoilNoID));
        }

        /// <summary>
        /// 要求MMS下發排程
        /// </summary>
        public void AskMMSSndCoilSchedule(CS01_AckSchedule msg)
        {
            _log.I("要求MMS下發排程", $"要求MMS下發鋼捲{msg.CoilID}排程");
            MQPoolService.SendToMMS(InfoMMS.AskCoilSchedule.Data(msg.CoilID));
        }

        /// <summary>
        /// PDI請求
        /// </summary>
        public void AskMMSSndPDI(CS02_AckPDI msg)
        {
            _log.I("要求MMS下發PDI", $"要求MMS下發鋼捲{msg.Coil_ID}PDI");
            MQPoolService.SendToMMS(InfoMMS.AskPDI.Data(msg.Coil_ID));
        }

        /// <summary>
        /// 作業計畫刪除請求
        /// </summary>
        public void DeleteScheduleByPlanNo(Msg_Req_Delete_Schedule_Plan msg)
        {
            var delSchedulePlanSucess = true;

            var scheduleCoilSets = new HashSet<string>();
            var coilPDI = _coilController.QueryCoilScheduleByPlanNo(msg.PlanNo);
            var planNo = msg.PlanNo;

            if (coilPDI == null)
            {
                // 回覆整計畫刪除電文 : 失敗
                _log.E("PDI整計畫刪除", "無此計畫");
                MQPoolService.SendToMMS(InfoMMS.ResPlanNoShedDelResult.Data(new ProResult("", MMSSysDef.Cmd.ProNG, "No Plan Number")));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SCCommMsg.SC03_EventPush("三級通知整計畫刪除失敗", "無此鋼卷鋼卷計畫")));
                return;
            }

            // 撈取已上線鋼卷
            TryFlow(() =>
            {
                // 排程前三筆鋼捲
                //var schedulingCoil = _coilService.GetCollScheduleIDs(3);
                var schedulingCoil = new List<string>();
                schedulingCoil = LoadOnLineCoilNo(schedulingCoil);

                foreach (string coil in schedulingCoil)
                    scheduleCoilSets.Add(coil);

                // 釋放
                schedulingCoil = null;
            });


            // 判定是否
            foreach (TBL_PDI coil in coilPDI)
            {
                var coilID = coil.In_Coil_ID.Trim();

                // 為線上鋼捲 || 未發過入料要求
                if (scheduleCoilSets.Contains(coilID) || coil.Is_Info_WMS_Down.Equals(L2SystemDef.Use))
                {
                    delSchedulePlanSucess = false;
                    continue;
                }

            }


            if (delSchedulePlanSucess)
            {
                // 計畫號鋼捲無 1.鋼捲在Tracking上, 3.無入料要求紀錄
                foreach (TBL_PDI coil in coilPDI)
                {
                    var coilID = coil.In_Coil_ID.Trim();
                    var insertOk = _coilController.DeleteCoilSchedule(coilID);
                    if (insertOk)
                        _coilController.CreateCoilScheduleDelRecords(coilID, msg.OperatorID, msg.ReasonCode, "整计划删除");
                }

                // 回覆整計畫刪除電文
                MQPoolService.SendToMMS(InfoMMS.ResPlanNoShedDelResult.Data(new ProResult(msg.PlanNo, EventDef.processOk, MMSSysDef.Cmd.DelScheduleByPlanNo)));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("整計畫刪除成功", $"已刪除計畫號{planNo}全鋼卷")));
                SndPresetAndUnscheduleCoilToWMS();

            }
            else
            {
                // 計畫號鋼捲有 1.鋼捲在Tracking上, 3.無入料要求紀錄 其中一個狀態
                MQPoolService.SendToMMS(InfoMMS.ResPlanNoShedDelResult.Data(new ProResult(msg.PlanNo, EventDef.processError, MMSSysDef.Cmd.DelSchedulePlanNoReject)));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("整計畫刪除失敗", $"計畫號{planNo}部份鋼捲已要求入料或已上鋼捲")));

                foreach (TBL_PDI coil in coilPDI)
                {
                    var coilID = coil.In_Coil_ID.Trim();

                    if (scheduleCoilSets.Contains(coilID) && coil.Is_Info_WMS_Down.Equals(L2SystemDef.Use))
                        continue;

                    var saveOk = _coilController.CreateTempCoilScheduleDelRecord(coilID);

                    if (saveOk)
                    {
                        var infoDelCoilSchedule = new CS03_ScheduleChange(L2SystemDef.SourceSystem, ScheduleStatus.DELETE, coilID, msg.OperatorID, msg.ReasonCode);
                        // 發送鋼捲刪除命令(G1MM18)           
                        MQPoolService.SendToMMS(InfoMMS.CoilSceduleDeleted.Data(infoDelCoilSchedule));
                    }
                }
            }
        }

        private List<string> LoadOnLineCoilNo(List<string> schedulingCoil)
        {
            var trackMap = _trkController.GetTrackMap();
            schedulingCoil.Add(trackMap.Entry_TOP.Trim());
            schedulingCoil.Add(trackMap.Entry_SK01.Trim());
            schedulingCoil.Add(trackMap.Entry_Car.Trim());
            schedulingCoil.Add(trackMap.POR.Trim());
            schedulingCoil.Add(trackMap.Delivery_Car.Trim());
            schedulingCoil.Add(trackMap.Delivery_SK02.Trim());
            schedulingCoil.Add(trackMap.Delivery_SK01.Trim());
            schedulingCoil.Add(trackMap.Delivery_TOP.Trim());

            return schedulingCoil;
        }

        private void CoilScheduleInsertDoneRes(string coilID, bool isCoilSchedProOk, string rejectCause = "")
        {

            // 回應處理結果
            var isProOk = isCoilSchedProOk ? EventDef.processOk : EventDef.processError;
            //var rejectCause = isCoilSchedProOk ? string.Empty : EventDef.ProSchedFail;

            // 回覆接收排程電文 
            MQPoolService.SendToMMS(InfoMMS.ResCoilSchedResult.Data(new ProResult(coilID, isProOk, rejectCause)));


            if (!isProOk.Equals(EventDef.processOk))
            {
                _log.E("鋼卷接收排程處理失敗", "鋼卷接收排程處理失敗");
                return;
            }

            // 處理成功

            // 通知HMI排程已更新
            MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SCCommMsg.SC04_ScheduleChangeNotice("Server", "")));
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("鋼捲排程已更新", "已處理完MMS排程訊息")));

            // 將未上線排程鋼捲ID傳送至DataSetup
            SndPresetAndUnscheduleCoilToWMS();

        }

        /// <summary>
        /// 將未上線排程鋼捲ID傳送至DataSetup && 並傳送鋼卷排程給WMS
        /// </summary>
        private void SndPresetAndUnscheduleCoilToWMS()
        {

            var coilIDs = _coilController.QueryUnscheduleCoils(40);

            if (coilIDs == null)
            {
                _log.E("通知DataSetup", "撈取40筆鋼捲失敗");
                return;
            }


            // 傳送40筆給Stp
            _log.I("通知DataSetup", "通知DataSetup組40筆Preset資料發送給L1");
            MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilIDs));
            // 傳送排程給WMS
            _log.I("傳送排程資訊給WMS", "通知WMS排程資訊");
            MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilIDs));

        }

        private void SndPresetInfo()
        {

            var coilIDs = _coilController.QueryUnscheduleCoils(40);


            if (coilIDs == null || coilIDs.Count == 0)
            {
                _log.I("撈取40筆鋼捲", "無未上線鋼卷");
                return;
            }

            _log.I("通知DataSetup", "通知組40筆Preset資料發送給L1");
            // 傳送40筆給Stp
            MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilIDs));

        }

        /// <summary>
        /// 无钢卷PDI应答
        /// </summary>
        private void InfoHMINoPDI(MMSL2Rcv.Msg_Res_For_No_Coil_PDI noPdiMsg)
        {
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.MMSNoPDI, $"無{noPdiMsg.Mat_No.ToStr()}鋼卷PDI");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }

        /// <summary>
        /// 无钢卷生產应答
        /// </summary>
        private void InfoHMINoCoil(MMSL2Rcv.Msg_Res_For_No_Coil_Schedule msg)
        {
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.MMSNoCoil, $"無{msg.Mat_No.ToStr()}鋼卷");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));


            var coilNo = msg.Mat_No.ToStr();

            if (coilNo.Equals(MMSSysDef.Cmd.NoCoilSchedule))
                _coilController.DeleteAllCoilSchedule();
        }

        /// <summary>
        /// 套筒同步
        /// </summary>
        private void SynchronizeSleeveData(Msg_Sleeve_Value_Synchronize msg)
        {
            var syncOk = _coilController.SyncSleeveValue(msg);
            var eventPush = new SCCommMsg.SC03_EventPush($"套筒資料同步{msg.Action}{syncOk.ToStr()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }

        /// <summary>
        /// 墊紙同步
        /// </summary>
        private void SynchronizePaperData(Msg_Paper_Value_Synchronize msg)
        {
            var syncOk = _coilController.SyncPaperValue(msg);
            var eventPush = new SCCommMsg.SC03_EventPush($"墊紙資料同步{msg.Action}{syncOk.ToStr()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }

        /// <summary>
        /// 三級是否接收PDO回應處理  added 2023.04.25
        /// </summary>
        private void ProResponRcvPDO(Msg_Res_RcvPDO msg)
        {
            var coilNo = msg.ResponCoil_No;
            var planNo = msg.ResponPlan_No;
            var succFlag = msg.Success_Flag.ToStr();
            var errMsg = $"{(string.IsNullOrEmpty(msg.Error_Reason.ToStr()) ? "" : ",")}{msg.Error_Reason.ToStr()}";
            var eventPush = new SC03_EventPush("[反饋PDO是否處理成功]三級回應" + $"處理結果(0拒绝/1成功)： {succFlag} " + $"鋼捲號： {coilNo},計劃號：{planNo} ," + $"原因：{errMsg} ");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            if (_coilController.CreatePdoUploadedReply(msg)) 
            if (msg.SuccessFlag.Equals(EventDef.RcvPdoOk))  
            {
                //三級成功接收PDO後續處理
                //_log.I("三級成功接收PDO", $"捲號:{coilNo},計畫號:" + planNo);
                _log.I($"[反饋PDO是否處理成功]三級回應 成功 接收PDO,捲號:{coilNo},計畫號:{planNo}", "");

                //更新PDO flag
                _log.I("更新PDO Upload旗標", "True");
                _coilController.UpdateUploadFlag(planNo, coilNo, true);

                //PDO 給 WMS
                // 通知WMS鋼卷生產實績
                var pdo = _coilController.GetPDOonly(planNo, coilNo);
                var SleeveData = _coilController.GetSleeveData(pdo.Out_Sleeve_Type_Code);
                var wmsPdoInfo = pdo.ConvertWMSPdoInfo(SleeveData.Out_Mat_Inner_Dia.ToString(), SleeveData.Sleeve_Thick.ToString("0.000"), SleeveData.Sleeve_Width.ToString("0.0"));
                MQPoolService.SendToWMS(InfoWMS.InfoiCoilPDOMsg.Data(wmsPdoInfo));

                eventPush = new SC03_EventPush("通知WMS鋼捲生產實績", $"捲號:{coilNo},計畫號:" + planNo);
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
                //PDO,PDI 給 L2.5
                //尋找是否有分切 找母材鋼捲
                var ParentCoilNo = _coilController.GetSplitParentCoilNo(coilNo);

                if (ParentCoilNo.Equals(string.Empty)) //parentCoilNo 空白
                {
                    //新增PDI至25
                    var pdi = _coilController.GetPDIOnly(planNo, pdo.In_Coil_ID);
                    _coilController.CreateL25PDI(pdi);
                }
                else //有parentCoilNo,給L25母材pdi 
                {
                    //新增PDI至25
                    var pdi = _coilController.GetPDIOnly(planNo, ParentCoilNo);
                    _coilController.CreateL25PDI(pdi);
                }

                // 新增PDO至25
                _coilController.CreateL25PDO(planNo, coilNo);
                var reply = new SCCommMsg.SC07_PdoUploadedReply($"三級成功接收PDO(鋼捲號：{coilNo},計劃號：{planNo}),原因：{errMsg}");
                MQPoolService.SendToPCCom(InfoHMI.PdoUploadedReply.Data(reply));
                //MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(reply));
                }
            else
            {
                //三級拒絕接收PDO後續處理
                _log.I($"[反饋PDO是否處理成功]三級回應 拒絕 接收PDO,捲號:{coilNo},計畫號:{planNo},原因{errMsg}", "");

                _log.I("更新PDO Upload旗標", "False");
                var reply = new SCCommMsg.SC07_PdoUploadedReply($"三級拒絕接收PDO(鋼捲號：{coilNo},計劃號：{planNo}),原因：{errMsg}");
                MQPoolService.SendToPCCom(InfoHMI.PdoUploadedReply.Data(reply));
               //MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(reply));
                }


        }

        #endregion

        private void RcvObject(object message)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{message.GetType()} From Sender:{Sender.Path}");
        }
    }
}
