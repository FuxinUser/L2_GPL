using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Controller.MsgPro;
using Core.Define;
using Core.Util;
using DataModel.Common;
using DataModel.HMIServerCom.Msg;
using DBService.AggregationModel;
using LogSender;
using MMSComm.Service;
using MsgConvert;
using MsgStruct;
using MSMQ;
using MSMQ.Core.MSMQ;
using System.Collections.Generic;
using static DataMod.Common.MMSMsgProResultModel;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace MMSComm.Actor
{
    public class MMSSndEdit : BaseActor
    {

        private IActorRef _sndActor;
        private IActorRef _selfActor;

        private ICoilController _coilService;
        private IMsgProController _msgProService;       // Msg Process Service

        private AggregateService _agService;

        public MMSSndEdit(ISysAkkaManager akkaManager, ICoilController coilService, IMsgProController msgProService, AggregateService agService, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _sndActor = akkaManager.GetActor(nameof(MMSSnd));
            _coilService = coilService;
            _msgProService = msgProService;
            _agService = agService;

            _msgProService.SetLog(log);
            _coilService.SetLog(log);

            MQPool.ReceiveFromMMS(x =>
            {
                _selfActor.Tell(x);
            });

            Receive<MQPool.MQMessage>(message => ProMQMsg(message));


            ReceiveAny(message => RcvObject(message));

            //測試
            //var tblPdo = _coilService.GetPDOonly("A12345", "CG200000030000");
            //var sndMsg = tblPdo.ToMMSPDOMsg();

        }

        private void ProMQMsg(MQPool.MQMessage message)
        {
            // 鋼捲排程調整通知
            if (message.ID == InfoMMS.CoilSceduleChanged.Event)
            {
                var coilScheduleIDs = message.Data as List<string>;
                var sndMsg = MMSMsgFactory.CoilScheduleChangedMsg(coilScheduleIDs);
                _log.I($"鋼捲排程調整通知={sndMsg.MsgID}", sndMsg.MsgID + " " + sndMsg.CoilNo);
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 鋼捲上鞍做通知
            if (message.ID == InfoMMS.CoilLoadedOnSk.Event)
            {
                var coiNo = message.Data as string;

                var planNo = _coilService.GetPDIPlanNo(coiNo);
                if (planNo.Equals(string.Empty))
                    return;

                var sndMsg = MMSMsgFactory.CoilLoadSkidMsg(planNo, coiNo);
                _log.I($"傳送鋼捲上鞍做通知={sndMsg.MsgID}", $"{sndMsg.MsgID}，鋼捲{coiNo}上鞍座通知");
                SendToSndActor(sndMsg.MsgID, sndMsg);
            }

            // 鋼捲回退實績通知
            if (message.ID == InfoMMS.CoilRejectResult.Event)
            {
                var coilReject = message.Data as CoilRejectReultModel;
                var sndMsg = MMSMsgFactory.CoilRejectResult(coilReject);
                _log.I($"鋼捲回退實績通知={sndMsg.MsgID}", sndMsg.MsgID);
                SendToSndActor(sndMsg.MsgID, sndMsg);
            }

            // HMI要求PDO上傳MMS
            if (message.ID == InfoMMS.ClientInfoSndPDO.Event)
            {
                var msg = message.Data as CS06_SendMMSPDO;
                var tblPdo = _coilService.GetPDOonly(msg.Plan_No, msg.Coil_ID);

                if (tblPdo == null)
                {
                    _log.E("撈取PDO資料失敗", $"無此鋼捲{msg.Coil_ID},{msg.Plan_No}");
                    return;
                }

                _log.I("撈取PDO資料成功", $"撈取鋼捲號{msg.Coil_ID},{msg.Plan_No} 成功");
                var sndMsg = tblPdo.ToMMSPDOMsg();
                _log.I($"MMS PDO上傳={sndMsg.MsgID}", $"上傳鋼捲PDO {msg.Coil_ID},{msg.Plan_No}資料");
                SendToSndActor(sndMsg.MsgID, sndMsg);

                var eventPush = new SCCommMsg.SC03_EventPush(EventDef.SndPDO, "已發送PDO資料給三級");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                return;
            }

            // 回覆接收排程電文
            if (message.ID == InfoMMS.ResCoilSchedResult.Event)
            {

                var proResult = message.Data as ProResult;
                var sndMsg = MMSMsgFactory.CoilSchedRes(proResult);
                _log.I($"回覆接收排程電文={sndMsg.MsgID}", $"處理結果{proResult.Result}, 原因{proResult.RejectCause}");
                SendToSndActor(sndMsg.MsgID, sndMsg);
            }

            // 回復接收PDI電文
            if (message.ID == InfoMMS.SndPDIProResult.Event)
            {
                var proResult = message.Data as ProResult;
                var sndMsg = MMSMsgFactory.CoilPDIProRes(proResult);
                _log.I($"鋼捲PDI回覆:{sndMsg.MsgID}", $"結果:{proResult.Result}, 原因:{proResult.RejectCause}");               
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 生产实绩请求
            if (message.ID == InfoMMS.MMSInfoSndkPDO.Event)
            {
                var coilID = message.Data as string;
                var tblPdo = _coilService.GetPDO(coilID);
                if (tblPdo == null)
                    return;
                var sndMsg = MMSMsgFactory.ToMMSPDOMsg(tblPdo);
                _log.I($"MMS PDO上傳={sndMsg.MsgID}", $"Code:{sndMsg.MsgID}，上傳鋼捲PDO{coilID}資料");
                //_log.Debug("MMS PDO上傳", JsonUtil.ToJson(sndMsg));
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 要求生產排程
            if (message.ID == InfoMMS.AskCoilSchedule.Event)
            {
                var coilID = message.Data as string;

                var sndMsg = MMSMsgFactory.ReqCoilSchedule(coilID);
                _log.I($"要求生產排程={sndMsg.MsgID}", $"Code:{sndMsg.MsgID}，要求鋼捲{coilID}生產排程資料");
                //_log.Info("要求生產排程", JsonUtil.ToJson(sndMsg));
                SendToSndActor(sndMsg.MsgID, sndMsg);
            }

            // 鋼捲PDI請求
            if (message.ID == InfoMMS.AskPDI.Event)
            {
                var coilID = message.Data as string;
                var sndMsg = MMSMsgFactory.ReqCoilPDI(coilID);
                _log.I($"鋼捲PDI請求={sndMsg.MsgID}", $"Code:{sndMsg.MsgID}，要求MMS下發鋼捲{coilID}PDI");
                //_log.Debug("鋼捲PDI請求", JsonUtil.ToJson(sndMsg));
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 回覆整計畫刪除電文
            if (message.ID == InfoMMS.ResPlanNoShedDelResult.Event)
            {
                var proResult = message.Data as ProResult;
                var sndMsg = MMSMsgFactory.ResPlanNoDelete(proResult);
                _log.I($"回覆整計畫刪除電文={sndMsg.MsgID}", $"處理結果{proResult.Result}, 原因{proResult.RejectCause}");
                //_log.Debug("回覆整計畫刪除電文", JsonUtil.ToJson(sndMsg));
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 回復能源消耗訊息
            if (message.ID == InfoMMS.SndEnergyConsumptionInfo.Event)
            {
                var energyInfo = message.Data as L1L2Rcv.Msg_112_Utility;
                var sndMsg = MMSMsgFactory.EnergyConsumptionInfo(energyInfo);
                //_log.I($"回復能源消耗訊息={sndMsg.MsgID}",$"");
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 上傳能源消耗訊息
            if (message.ID == InfoMMS.UploadEnergyConsumptionInfo.Event)
            {
                var msg = message.Data as CS15_Utility;
                var sndMsg = MMSMsgFactory.UploadEnergyConsumptionInfo(msg);
                _log.I($"上傳能源消耗訊息={sndMsg.MsgID}", $"");
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }

            // 發送鋼捲生產命令刪除通知
            if (message.ID == InfoMMS.CoilSceduleDeleted.Event)
            {
                var scheduleDelete = message.Data as SCCommMsg.CS03_ScheduleChange;
                var sndMsg = MMSMsgFactory.CoilSchDelMsg(scheduleDelete);
                _log.I($"發送鋼捲生產命令刪除通知={sndMsg.MsgID}", $"刪除{sndMsg.CoilNo}");
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }       
            
            // 發送過度卷要求
            if(message.ID == InfoMMS.RequestDummyCoil.Event)
            {
                var reqDummyCoil = message.Data as SCCommMsg.CS19_RequestDummy;
                var sndMsg = reqDummyCoil.ConvertReqDummyMsg();
                _log.I($"發送過度卷請求通知={sndMsg.MsgID}", $"請求過度卷{sndMsg.CoilNo}");
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return; 
            }

            // 發送過度卷刪除
            if (message.ID == InfoMMS.DeleteDummyCoil.Event)
            {
                var delDummyCoil = message.Data as SCCommMsg.CS20_DeleteDummy;
                var sndMsg = delDummyCoil.ConvertDelDummyMsg();
                _log.I($"發送過度卷刪除通知={sndMsg.MsgID}", $"刪除過度卷{sndMsg.CoilNo}");
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }
        
            // 發停復機紀錄
            if(message.ID == InfoMMS.UploadLineFaultRecord.Event)
            {
                var msg = message.Data as TBL_LineFaultRecords;
                var sndMsg = msg.ToEquipmentDownResult("1");
                _log.I($"停機實績=G1MM08","");
                SendToSndActor(sndMsg.MsgID, sndMsg);
                return;
            }
        }

        private void SendToSndActor(string msgID, object data)
        {

            var bytes = MsgAnalUtil.RawSerialize(data);
            if (bytes == null)
            {
                // DumpFile              
                _log.E("報文序列化編碼失敗", $"MsgID : {msgID} 序列化失敗");
                return;
            }

            // 結尾符                
            var container = new List<byte>(bytes);
            container.Add(10);
            bytes = container.ToArray();
            var sndActualLength = bytes.Length.ToString();

            _log.D("報文序列化編碼成功", $"Msg ID{msgID}");
            var comMsg = new CommonMsg(length: sndActualLength, id: msgID, bytes: bytes);

            _sndActor.Tell(comMsg);

            _agService.DumpSndRawData(bytes);

            _msgProService.CreateMMSWMSMsg("TBL_MMS_SendRecord", comMsg);

        }

        private void RcvObject(object msg)
        {
            _log.E("ATell接收資料", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

    }
}
