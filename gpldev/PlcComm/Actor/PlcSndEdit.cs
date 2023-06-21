using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.MsgPro;
using Core.Define;
using Core.Util;
using DataMod.Common;
using DataModel.Common;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.Belt;
using LogSender;
using MsgConvert;
using MsgConvert.DBTable;
using MsgStruct;
using MSMQ;
using MSMQ.Core.MSMQ;
using PLCComm.Model;
using System;
using static DataMod.Common.ModifyCoilModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;


/**
 * Author: ICSC 余士鵬
 * Date: 2019/9/19
 * Description: 序列化解析L2發送資料.並轉發Snd角色
 * Reference: 
 * Modified: 
 */
namespace PLCComm.Actor
{
    public class PlcSndEdit : BaseActor
    {

        private IActorRef _sndActor;                    // Snd發送角色
        private IActorRef _selfActor;                   // PlcSndEdit 角色

        private IMsgProController _msgProService;

        private AggregateService _aggregateService;

        public PlcSndEdit(ISysAkkaManager akkaManager, IMsgProController msgProService, AggregateService aggregateService , ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(nameof(PlcSndEdit));
            _sndActor = akkaManager.GetActor(nameof(PlcSnd));

            _aggregateService = aggregateService;
            _msgProService = msgProService;
            _msgProService.SetLog(log);

            // MQ接收資料
            MQPool.ReceiveFromL1(x =>{
                // 接收到資料轉接給SndEdit處理
                _selfActor.Tell(x);
            });

            // 接手MQ資料
            Receive<MQPool.MQMessage>(message => ProMQMsg(message));

            ReceiveAny(message => RcvObject(message));
        }



        private void ProMQMsg(MQPool.MQMessage msg)
        {
            // 202 Preset
            if (msg.ID == InfoL1.SndPreset202Msg.Event)
            {
                _log.I("Prset202通知", "發送Preset202通知");
                _log.D("Prset202通知", JsonUtil.ToJson(msg.Data));
                SendToSndActor(PlcSysDef.SndMsgCode.L1202PDI, msg.Data);
                return;
            }
            // 204 Preset
            if (msg.ID == InfoL1.SndPreset204Msg.Event)
            { 
                //_log.Info("Prset204通知", $"發送Preset204通知");
                SendToSndActor(PlcSysDef.SndMsgCode.L1204Preset, msg.Data);
                return;
            }
            // 205 Preset
            if (msg.ID == InfoL1.SndPreset205Msg.Event)
            {
                //_log.Info("Prset205通知", $"發送Preset205通知");
                SendToSndActor(PlcSysDef.SndMsgCode.L1205Preset, msg.Data);
                return;
            }
            // 210 Del Coil
            if (msg.ID == InfoL1.SndDelCoil210Msg.Event)
            {
                var delResult = msg.Data as DeleteResult;
                var sndMsg = delResult.ConvertToDelCoilIDMsg();
                _log.I("Del Coil210通知", $"發送Del Coil210通知 Del {delResult.CoilId}");
                _log.D("Del Coil210通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1210DelCoil, sndMsg);
                return;
            }
            // 203 En Scn Result
            if (msg.ID == InfoL1.SndPDITM2203Msg.Event)
            {
                var scnCoilCheckMsg = msg.Data as SC01_ScnBarcodeID;
                var check = scnCoilCheckMsg.ScanResult == BCScanResult.Sucess ? (short)1 : (short)0;
                var sndMsg = L1MsgFactory.ConvertToCoilScnCheckResult(check);
                _log.I("Scn Result通知", "發送Scn Result 203通知");
                _log.D("Scn Result通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1203EntryScnResult, sndMsg);
                return;
            }
            // 206 New Belt Info
            if (msg.ID == InfoL1.SndNewBeltInfo206Msg.Event)
            {
                var beltInfo = msg.Data as BeltAccEntity.TBL_Belts;
                
                var sndMsg = beltInfo.ConvertToBeltInfo();
                sndMsg.BeltExistFlag = sndMsg.ABbeltID.ToStr().IsEmpty() ? (short)0 : (short)1;

                _log.I("Belt Info通知", "發送Belt Info 206通知");
                _log.D("Belt Info通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1206BeltInfoMsg, sndMsg);

                return;
            }
            // 211 Modify Coil ID 
            if (msg.ID == InfoL1.SndModifyCoilID211Msg.Event)
            {
                var modifyCoilInfo = msg.Data as ModifyCoilModel.ModifyResult;
                var sndMsg = modifyCoilInfo.ConvertToModifyCoilInfo();
                _log.I("鋼捲更名通知", $"發送鋼捲{modifyCoilInfo.CoilId}更名通知");
                _log.D("鋼捲更名通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1211ModifyCoilID, sndMsg);
                return;
            }


            // 207 Entry Take Over Start CMD
            if (msg.ID == InfoL1.SndEntryTakeOVerStarCmd207Msg.Event)
            {
                var sndMsg = L1MsgFactory.ConvertEntryTakeOverStartCM();
                _log.I("Entry Take Over通知", "發送Entry Take Over 207通知");
                _log.D("Entry Take Over通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1207EntryTakeOverStartCMD, sndMsg);
                return;
            }

            // 208 Delivery Take Over Start
            if (msg.ID == InfoL1.SndDeliveryToCmd208Msg.Event)
            {
                var sndMsg = L1MsgFactory.ConvertDelTakeOverStartCM();
                _log.I("Delivery Take Over Start通知", "發送Delivery Take Over Start 208通知");
                _log.D("Delivery Take Over Start通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1208DelveryToCMD, sndMsg);
                return;
            }

            // 209 Delivery Scn Result
            if (msg.ID == InfoL1.SndDeliveryBCConfirm209Msg.Event)
            {
                var scnCoilCheckMsg = msg.Data as SCCommMsg.SC01_ScnBarcodeID;
                var check = scnCoilCheckMsg.ScanResult == BCScanResult.Sucess ? (short)1 : (short)0;
                var sndMsg = L1MsgFactory.ConvertToDeliveryCoilScnCheckResult(check);
                _log.I("Scn Result通知", "發送Scn Result 209通知");
                _log.D("Scn Result通知", JsonUtil.ToJson(sndMsg));
                SendToSndActor(PlcSysDef.SndMsgCode.L1209DeliveryScnResult, sndMsg);
                return;
            }


            // TESTUSING

            // 204 Preset
            if (msg.ID == InfoL1.ReSndPreset204Msg.Event)
            {
                var preset204 = msg.Data as L2L1Snd.Msg_204_PDI_TM3;

                _log.I("SndEdit Prset204通知", $"發送{preset204.CoilId.ToStr()} Preset204通知");
                _log.D("SndEdit Prset204通知", JsonUtil.ToJson(msg.Data));

                SendToSndActor(PlcSysDef.SndMsgCode.L1204Preset, msg.Data, true);
                return;
            }
            // 205 Preset
            if (msg.ID == InfoL1.ReSndPreset205Msg.Event)
            {
                _log.I("SndEdit Prset205通知", $"發送Preset205通知");
                _log.D("SndEdit Prset205通知", JsonUtil.ToJson(msg.Data));

                SendToSndActor(PlcSysDef.SndMsgCode.L1205Preset, msg.Data, true);
                return;
            }

         
        }
        /// <summary>
        /// 發送給Snd Actor
        /// </summary>
        private void SendToSndActor(string msgID, object msgObject, bool isResend = false)
        {

            var bytes = msgObject.RawSerialize(false);
            if (bytes == null)
            {
                _log.E("報文序列化編碼失敗", $"MsgID : {msgID} 序列化失敗");
                return;
            }

            _aggregateService.DumpSndRawData(bytes);
        
            //3.發送至SndActor:              
            var sndMsg = new CommonMsg(bytes.Length.ToString(), msgID, bytes);
            //_log.Debug("報文序列化編碼成功", $"Msg ID{msgID}, Length : {bytes.Length}");
            _sndActor.Tell(sndMsg);

            // 存DB Log
            if (!isResend)
                try
                {
                    var L1DBModel = msgObject.ConvertL1DBModel(msgID);
                    _msgProService.CreateMsgToL1HistoryDB("L2L1_" + msgID, L1DBModel);
                    //_msgProService.CreateMsgToL1HistoryDB("TBL_PresetHis_" + msgID, L1DBModel);
                }
                catch (Exception e)
                {
                    _log.E($"報文{msgID}存取Log失敗", e.Message.CleanInvalidChar());
                }


        }
        /// <summary>
        /// 角色接收無法解析資料事件
        /// </summary>
        private void RcvObject(object msg)
        {
            _log.E("ATell接收資料", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

    }
}
