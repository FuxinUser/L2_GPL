using Akka.Actor;
using AkkaSysBase.Base;
using Controller;
using Controller.MsgPro;
using Controller.Sys;
using Core.Define;
using Core.Help;
using Core.Util;
using DataModel.HMIServerCom.Msg;
using LogSender;
using MsgConvert.DBTable;
using MSMQ.Core.MSMQ;
using System;


/**
 * Author: ICSC 余士鵬
 * Date: 2019/9/19
 * Description: 負責解析Plc發送資料為Model. 並轉發其他App處理(Msg Edit角色)
 * Reference: 
 * Modified: 
 */

namespace PLCComm.Actor
{
    public class PlcRcvEdit : BaseActor
    {
     
        private ISysController _sysService;             // System Process Service
        private IMsgProController _msgProService;       // Msg Process Service

        private ICancelable _tmrDetectL1Alive;          // L2 Alive 發送Timer
        private DateTime _preAliveMsgRcvTime;           // 前一筆Alive接收時間

        public PlcRcvEdit(ISysController sysService,  IMsgProController msgProService, ILog log) : base(log)
        {
         
            _sysService = sysService;
            _sysService.SetLog(log);
            _msgProService = msgProService;
            _msgProService.SetLog(log);


            //_preAliveMsgRcvTime = DateTime.MinValue;
            //DetectL1ConnectStatusTmr(3, EventDef.CMDSET.DETECT_L1_ALIVE);

            Receive<EventDef.CMDSET>(msg => ProEventCmd(msg));
            Receive<byte[]>(message => ProReceiveMessage(message));
            ReceiveAny(message => RcvObject(message));

        }

        private void ProReceiveMessage(byte[] message)
        {
            var msgID = MsgAnalUtil.GetMsgID(message);
            var ty = MsgRefToObjHelp.Instance.GetPlcStructClassType(msgID);
            object msgObject = null;
            bool insertLogOK = false;   //存Msg歷史DB

            //DumpFile
            //message.DumpDataToFile(L2SystemDef.MSGDumpPath);

            // 反序列化
            try
            {
             

                msgObject = MsgAnalUtil.RawDeserialize(message, ty);
                if (msgObject == null)
                {
                    //Analysis Fail Dump File
                    _log.E("報文解析失敗", $"解析接收訊號失敗 MsgID為{msgID}");
                    _log.E("報文解析失敗", $"解析接收訊號失敗 MsgID為{msgID}");
                    return;
                }
            }
            catch (Exception e)
            {
                _log.E("報文解析失敗", e.ToString().CleanInvalidChar());
            }


            // 存DB Log && DumpFile(101不存)
            if (msgID != PlcSysDef.RcvMsgCode.L1101Alive)
            {
                // DB Log
                try
                {
                    var L1DBModel = msgObject.ConvertL1DBModel(msgID);
                    insertLogOK = _msgProService.CreateMsgToL1HistoryDB("L1L2_" + msgID, L1DBModel);
                }
                catch (Exception e)
                {
                    _log.E($"報文{msgID}存取Log失敗", e.ToString());

                }
            }

            _log.D("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");

            Router(msgID, msgObject);

        }

        private void Router(string msgID, dynamic message)
        {
            switch (msgID)
            {
                // 110 報文
                case PlcSysDef.RcvMsgCode.L1101Alive:
                    _preAliveMsgRcvTime = DateTime.Now;
                    _sysService.UpdateL1LastAliveTime(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    break;

                // 102 報文
                case PlcSysDef.RcvMsgCode.L1102PDOInfo:
                    _log.I("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
                    MQPoolService.SendToCoil(InfoCoil.AccountPDO.Data(message));
                    break;

                // 104 報文
                case PlcSysDef.RcvMsgCode.L1104ProcessData:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1104ProcessData.Data(message));
                    break;

                // 105 報文
                case PlcSysDef.RcvMsgCode.L1105TrackMap:
                    MQPoolService.SendToTrk(InfoTrk.UpdateTrkMap.Data(message));
                    MQPoolService.SendToWMS(InfoWMS.InfoTrackMap.Data(message));
                    break;

                // 106 報文
                case PlcSysDef.RcvMsgCode.L1106CoilWeldData:
                    _log.I("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1106WeldData.Data(message));
                    MQPoolService.SendToCoil(InfoCoil.ProWeldRecordGenPDO.Data(message));
                    break;

                // 107 報文
                case PlcSysDef.RcvMsgCode.L1107GrindRecords:
                    _log.I("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1107GrindRecords.Data(message));
                    break;

                // 108 報文
                case PlcSysDef.RcvMsgCode.L1108DefectData:
                    _log.I("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
                    MQPoolService.SendToCoil(InfoCoil.SaveDefectData.Data(message));
                    break;
                    
                // 109 報文
                case PlcSysDef.RcvMsgCode.L1109BeltAccLength:
                    MQPoolService.SendToCoil(InfoCoil.UpdateBeltAccLength.Data(message));
                    break;

                // 110 報文
                case PlcSysDef.RcvMsgCode.L1110UpdateWt:
                    MQPoolService.SendToCoil(InfoCoil.UpdateOutMatPureWT.Data(message));
                    break;

                // 111 報文
                case PlcSysDef.RcvMsgCode.L1111LineFault:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1111LineFault.Data(message));
                    break;

                // 112 報文
                case PlcSysDef.RcvMsgCode.L1112Utility:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL112Utility.Data(message));
                    //MQPoolService.SendToMMS(InfoMMS.SndEnergyConsumptionInfo.Data(message));
                    break;

                // 113 報文
                case PlcSysDef.RcvMsgCode.L1113BeltChange:
                    MQPoolService.SendToCoil(InfoCoil.ChangeBelt.Data(message));
                    break;

                // 114 報文
                case PlcSysDef.RcvMsgCode.L1114CoilMount:
                    _log.I("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
                    MQPoolService.SendToTrk(InfoTrk.CoilMount.Data(message));
                    break;

                // 118 報文
                case PlcSysDef.RcvMsgCode.L1118EntryStartCondition:
                    MQPoolService.SendToTrk(InfoTrk.InfoEntryStartCondition.Data(message));
                    break;

                // 121 報文
                case PlcSysDef.RcvMsgCode.L1121DeliveryStartCondition:
                    MQPoolService.SendToTrk(InfoTrk.InfoEntryStartCondition.Data(message));
                    break;

                // 124 報文
                case PlcSysDef.RcvMsgCode.L1124StripBrakeSignal:
                    _log.I("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1124StripBrakeSignal.Data(message));
                    MQPoolService.SendToCoil(InfoCoil.StripBakeInfo.Data(message));
                    break;

                // 125 報文
                case PlcSysDef.RcvMsgCode.L1125ShareCutData:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1125CoilShareCutData.Data(message));
                    MQPoolService.SendToCoil(InfoCoil.InfoSndL1TRCoilID.Data(message));

                    break;

                // 126 報文
                case PlcSysDef.RcvMsgCode.L1126CoilUmountPOR:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1126CoilUmountPOR.Data(message));
                    break;

                // 117 報文
                case PlcSysDef.RcvMsgCode.L1117CoilSplit:
                    MQPoolService.SendToCoil(InfoCoil.CoilSplt.Data(message));
                    break;

            }
        }

        /// <summary>
        /// 每5秒檢查一次，若最後更新時間與本次檢查的時間差超過10秒
        /// 則判斷為一級斷線，須紀錄EVENTLOG並通知CLIENT
        /// </summary>
        private void DetectL1ConnectStatusTmr(int second, object message)
        {
            var interval = TimeSpan.FromSeconds(second);
            _tmrDetectL1Alive = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, message, Self);
        }

        /// <summary>
        /// Cmd事件觸發
        /// </summary>       
        private void ProEventCmd(EventDef.CMDSET cmd)
        {
            switch (cmd)
            {
                case EventDef.CMDSET.DETECT_L1_ALIVE:
                    MonitorL1Alive();
                    break;
            }
        }

        /// <summary>
        /// 監控是否收到Alive
        /// </summary>   
        private void MonitorL1Alive()
        {

            // 未收到第一筆
            if (_preAliveMsgRcvTime == DateTime.MinValue)
            {

                _log.E("L1 Alive連線檢查", "未收到第一筆L1 Alive資訊");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SCCommMsg.SC03_EventPush(EventDef.L1DisConn,"未收到第一筆L1 Alive資訊")));
                return;
            }
            // 收到第一筆後做比較
            var diffInSeconds = (DateTime.Now - _preAliveMsgRcvTime).TotalSeconds;
            if (diffInSeconds > 10)
            {
                // 紀錄Log
                _log.E("L1 Alive連線檢查", "L1已斷線!!!");
                // 通知HMI             
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SCCommMsg.SC03_EventPush("L1 Alive連線檢查 ,L1已斷線!!!")));
            }
        }

        /// <summary>
        /// 角色接收無法解析資料事件
        /// </summary>
        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }
}
