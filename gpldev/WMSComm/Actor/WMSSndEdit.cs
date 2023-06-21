using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Core.Util;
using Core.Define;
using DataModel.Common;
using MsgConvert;
using MSMQ;
using MSMQ.Core.MSMQ;
using System.Collections.Generic;
using DataMod.WMS.LogicModel;
using Controller.MsgPro;
using MsgStruct;
using DataMod.BarCode;
using WMSComm.Model;
using LogSender;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace WMSComm.Actor
{
    public class WMSSndEdit : BaseActor
    {
  
        private IActorRef _sndActor;
        private IActorRef _selfActor;

        private IMsgProController _msgProService;       // Msg Process Service

        private AggregateService _agService;

        public WMSSndEdit(ISysAkkaManager akkaManager, IMsgProController msgProService, AggregateService agService, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _sndActor = akkaManager.GetActor(nameof(WMSSnd));
            _agService = agService;
            _msgProService = msgProService;

            _msgProService.SetLog(log);

            MQPool.ReceiveFromWMS(x => _selfActor.Tell(x));

            Receive<MQPool.MQMessage>(x => ProMQMsg(x));
            ReceiveAny(message => RcvObject(message));
        }


        private void ProMQMsg(MQPool.MQMessage msg)
        {
            // GW01 傳送排程資訊
            if (msg.ID == InfoWMS.InfoCoilScheduleMsg.Event)
            {
                var coilScheduleIDs = msg.Data as List<string>;
                var gw01 = WMSMsgFactory.GW01ScheduleMsg(coilScheduleIDs);
                _log.I($"傳送排程資訊", $" MsgID : {gw01.MsgID}, CoilNo : {gw01.CoilNos}");
                SendToSndActor(gw01.MsgID, gw01);
                return;
            }

            // GW02 接收產線入口/出口 Tracking
            if(msg.ID == InfoWMS.InfoTrackMap.Event)
            {
                var trkInfo = msg.Data as L1L2Rcv.Msg_105_Trk_Map;
                var gw02 = trkInfo.GW02TrackInfo();
                _log.I($"傳送Track訊息", $" MsgID : {gw02.MsgID}");
                
                SendToSndActor(gw02.MsgID, gw02);
                return;
            }

            // GW05 傳送產線入料要求
            if (msg.ID == InfoWMS.InfoCoilEntryOrDeliveryReq.Event)
            {
                var prodCoilReq = msg.Data as ProdLineCoilReq;
                var gw05 = WMSMsgFactory.GW05ReqMsg(prodCoilReq);
                _log.I($"傳送產線{prodCoilReq.ActionStr}要求", $"MsgID : {gw05.MsgID}, CoilNo : {gw05.CoilIDNo}");
                SendToSndActor(gw05.MsgID, gw05);
                return;
            }

            // GW03 傳送鋼卷PDO
            if (msg.ID == InfoWMS.InfoiCoilPDOMsg.Event)
            {

                var wmsPDO = msg.Data as WMSPdoInfomation;
                var gw03 = wmsPDO.GW03CoilInfo();
                _log.I($"傳送PDO資料", $"MsgID : {gw03.MsgID}, CoilNo : {gw03.CoilIDNo}");
                SendToSndActor(gw03.MsgID, gw03);

                //PDO Send To WMS
                var eventPush = new DataModel.HMIServerCom.Msg.SCCommMsg.SC03_EventPush(EventDef.SndPDOtoWMS, "已發送PDO資料給WMS");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                return;
            }


            // GW05 傳送產線退料要求
            if (msg.ID == InfoWMS.RejectCoilReqMsg.Event)
            {
                var prodCoilReq = msg.Data as ProdLineCoilReq;
                var gw05 = WMSMsgFactory.GW05ReqMsg(prodCoilReq);
                _log.I($"傳送產線退料要求", $"MsgID : {gw05.MsgID}, CoilNo: {gw05.CoilIDNo}");
                SendToSndActor(gw05.MsgID, gw05);
            }

            // GW06 
            if(msg.ID == InfoWMS.InfoBCSScanID.Event)
            {
                var scanResult = msg.Data as ScanResult;
                var gw06 = WMSMsgFactory.GW06ScanCoil(scanResult.SKID, scanResult.CoilID);
                _log.I($"傳送產線退料要求", $"MsgID : {gw06.MsgID}, CoilNo: {gw06.CoilIDNo}");
                SendToSndActor(gw06.MsgID, gw06);

            }

        }

        private void RcvObject(object msg)
        {
            _log.E("ATell接收資料", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

     
        private void SendToSndActor(string msgID, object data)
        {
            var bytes = MsgAnalUtil.RawSerialize(data);

            if (bytes == null)
            {
                _log.E("報文序列化編碼失敗", $"MsgID : {msgID} 序列化失敗");
                return;
            }

            _log.D("報文序列化編碼成功", $"Msg ID{msgID}, Length : {bytes.Length}");
            var comMsg = new CommonMsg(id: msgID, bytes: bytes);
            _sndActor.Tell(comMsg);
            _agService.DumpSndRawData(bytes);
            _msgProService.CreateMMSWMSMsg("TBL_WMS_SendRecord", comMsg);

        }
    }
}
