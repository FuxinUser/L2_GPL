using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Core.Define;
using Core.Util;
using DataMod.Common;
using DataModel.HMIServerCom.Msg;
using LogSender;
using MSMQ;
using MSMQ.Core.MSMQ;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/8
 * Description: PC Com
 * Reference: 
 * Modified: 
 */
namespace PcComm
{
    public class PCcom : BaseActor
    {

        private IActorRef _selfActor;

        /// <summary> Save the ActorSelection of connected server </summary>
        private ActorSelection _hmiClient;
        /// <summary> Save the dictionary that save clients </summary>
        private Dictionary<Guid, IActorRef> _clientList = null;
        /// <summary> Save the dictionary that save Timestamps of clients </summary>
        private Dictionary<Guid, DateTime> _clientTimestamps = null;

        private ConcurrentDictionary<string, IActorRef> _clients;

        public PCcom(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            //_selfActor = akkaManager.GetActor(GetType().Name);
            //_hmiClient = Context.ActorSelection("akka.tcp://GPLHMI@192.168.0.200:8401/user/hmiclient");

            _selfActor = akkaManager.GetActor(GetType().Name);
            _clients = new ConcurrentDictionary<string, IActorRef>();


            MQPool.ReceiveFromPCCom(x => _selfActor.Tell(x));

            //Receive<HMIToServerMsg.ClientAliveMsg>(message => HandleClientToServer(message));
            Receive<MQPool.MQMessage>(x => Handle_MQ_Message(x));


            // 
            Receive<ClientAliveMsg>(message => ClientAliveMsg(message));     
            // 重新要求排程,通知Server發送電文給MMS要求下發最新排程
            Receive<CS01_AckSchedule>(message => CS01_AckSchedulePro(message));
            // 通知Server發送電文給MMS要求指定鋼捲PDI資料
            Receive<CS02_AckPDI>(message => CS02_AckPDIPro(message));
            // 通知Server更新排程給MMS及WMS
            Receive<CS03_ScheduleChange>(message => CS03_ScheduleChangePro(message));
            // 入口段鋼捲ID更正
            Receive<CS04_RenameCoil>(message => CS04_RenameCoilPro(message));
            // CLIENT紀錄退料實績相關資料到資料庫, 通知SERVER發送退料實績（MMS）及退料要求（WMS）
            Receive<CS05_RejectCoil>(message => CS05_RejectCoilPro(message));
            // 操作確認上傳鋼捲PDO
            Receive<CS06_SendMMSPDO>(message => CS06_SendMMSPDOPro(message));
            // 操作要求手動列印標籤, CLIENT通知SERVER列印標籤
            Receive<CS07_PrintLabel>(message => CS07_PrintLabelPro(message));
            // 操作手動輸入秤重資料, CLIENT通知SERVER更新鋼捲秤重資料（毛重）
            Receive<CS08_WeightInput>(message => CS08_WeightInputPro(message));
            // 停付機
            Receive<CS09_LineFaultData>(message => CS09_LineFaultDataPro(message));
            // 產線開始/停止供料確認
            Receive<CS10_Coil_AutoFeedModeChange>(message => CS10_Coil_AutoFeedModeChange(message));
            // 手動入料 :  直接發入料要求
            Receive<CS11_Coil_ManualFeed>(message => CS11_Coil_ManualFeed(message));
            // 操作於指定鞍座上操作入料指示
            Receive<CS12_Coil_SkidFeed>(message => CS12_Coil_SkidFeed(message));
            // 刪除鞍座鋼卷號
            Receive<CS13_DeleteSidCoil>(message => CS13_DeleteSidCoil(message));
            // 出口段出料
            Receive<CS14_DeliveryCoilOut>(CS14_DeliveryCoilOut);

            //能耗
            Receive<CS15_Utility>(message => CS15_Utility(message));

            // 天車入料時選擇此ID
            Receive<CS18_CarneEntryCoilSelect>(message => CS18_CarneEntryCoilSelect(message));



            // 操作端完成匯入排程，通知Server發送Preset40筆
            Receive<CS16_FinishLoadSchedule>(message => CS16_FinishLoadSchedule(message));
            // 操作端完成匯入PDI，通知Server發送Preset40筆
            Receive<CS17_FinishLoadPDI>(message => CS17_FinishLoadPDI(message));
   
            // 操作端過渡捲清單請求
            Receive<CS19_RequestDummy>(message => CS19_RequestDummy(message));
            // 操作端過渡捲刪除
            Receive<CS20_DeleteDummy>(message => CS20_DeleteDummy(message));

            // Deliverty Skid 2 出料準備確定
            Receive<CS21_DeliveryCoilReady>(message => CS21_DeliveryCoilReady(message));

            Receive<CS22_POR_PresetL1>(message => CS22_POR_PresetL1(message));


            //HMI通知Server修改POR捲號
            Receive<CS23_POR_StripBreakModify>(message => CS23_POR_StripBreakModify(message));



            Receive<string>(message => HandleString(message));

            ReceiveAny(message => RcvObject(message));

        }

        private void HandleString(string msg)
        {
            _log.I("測試用",msg);
        }

        private void Handle_MQ_Message(MQPool.MQMessage msg)
        {

           
            // 入口段掃碼結果
            if( msg.ID == InfoHMI.BarcodeScanResult.Event)
            {

                _log.D("Server通知HMI", "入口段掃碼結果");
                _log.D("Server通知HMI", $"{JsonUtil.ToJson(msg)}");

                _log.I("通知HMI", "入口段掃碼結果");
                _log.I("通知HMI", $"{JsonUtil.ToJson(msg)}");
                //TODO 通知HMI
                BroadCast(msg.Data);
                return;
            }
            // 通知HMI Event Push
            if( msg.ID == InfoHMI.EventPush.Event)
            {
                var eventPush = msg.Data as SCCommMsg.SC03_EventPush;
                _log.I("事件發生通知通知HMI", eventPush.EventName+" "+ eventPush.EventMsg);
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                BroadCast(msg.Data);
                return;
            }
            // 通知HMI 排程資料庫已變更
            if(msg.ID == InfoHMI.ScheduleChangeNotice.Event)
            {
                _log.I("通知HMI", "鋼捲排程資料庫已更新通知HMI");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                BroadCast(msg.Data);
                return;
            }
            // 通知HMI 鋼捲是否成捲
            if (msg.ID == InfoHMI.StripBrakeMessage.Event)
            {
                _log.I("通知HMI", $"通知鋼捲是否成捲");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                BroadCast(msg.Data);
                return;
            }
            // 通知HMI 通知天車入料給予天車入料ID
            if (msg.ID == InfoHMI.CraneEntryCoil.Event)
            {
                _log.I("通知HMI", $"通知天車入料給予天車入料ID");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                BroadCast(msg.Data);
                return;
            }
            if (msg.ID == InfoHMI.PdoUploadedReply.Event)
            {
                _log.I("通知HMI", "顯示上傳PDO的回覆資訊");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                BroadCast(msg.Data);
                return;
            }

        }

        private void ClientAliveMsg(SCCommMsg.ClientAliveMsg msg)
        {
            //_log.D("HMI Alive訊號", $"接收到HMI Alive Msg");

            var id = msg.Client_IP_Port;

            if (_clients.ContainsKey(id))
            {
                IActorRef refActor;

                if (_clients.TryRemove(id, out refActor))
                {
                    if (!_clients.TryAdd(id, Sender))
                    {
                        _log.E("加入HMI Group失敗", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
                        return;
                    }

                    _log.I("加入HMI Group成功", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
                    _clients[id].Tell(new ServerAckClientAliveMsg());
                }
                return;
            }

            if (!_clients.TryAdd(id, Sender))
            {
                _log.E("加入HMI Group失敗", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
                return;
            }

            _log.I("加入HMI Group成功", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
            _clients[id].Tell(new ServerAckClientAliveMsg());
        }
        private void CS01_AckSchedulePro(SCCommMsg.CS01_AckSchedule msg)
        {
            _log.I("要求MMS下發排程", $"通知MMS下發鋼捲{msg.CoilID}最新排程");
            _log.D("要求MMS下發排程", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.AskCoilSchedule.Data(msg));
        }
        private void CS02_AckPDIPro(SCCommMsg.CS02_AckPDI msg)
        {
            _log.I("要求MMS下發PDI", $"通知MMS下發鋼捲{msg.Coil_ID}PDI");
            _log.D("要求MMS下發PDI", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.AskPDI.Data(msg));
        }
        private void CS03_ScheduleChangePro(SCCommMsg.CS03_ScheduleChange msg)
        {
            _log.I("通知鋼捲排程變更", $"鋼捲號{msg.EntryCoilID}排程更新訊息給MMS及WMS");
            _log.I("通知鋼捲排程變更",$"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.UpdateCoilSchedule.Data(msg));
        }
        private void CS04_RenameCoilPro(SCCommMsg.CS04_RenameCoil msg)
        {
            _log.I("HMI通知", "入口段鋼捲ID更正");
            _log.D("HMI通知", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.ScnRenameCoil.Data(msg));
        }
        private void CS05_RejectCoilPro(SCCommMsg.CS05_RejectCoil msg)
        {
            _log.I("鋼捲回退通知", "CLIENT紀錄退料實績相關資料到資料庫");
            _log.D("鋼捲回退通知", $"{JsonUtil.ToJson(msg)}");
                MQPoolService.SendToTrk(InfoTrk.ReturnCoil.Data(msg));
        }
        private void CS06_SendMMSPDOPro(SCCommMsg.CS06_SendMMSPDO msg)
        {
            _log.I("HMI通知", "操作確認，上傳鋼捲PDO");
            _log.D("HMI通知", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.AskSndPDO.Data(msg));
            MQPoolService.SendToDtProGtr(InfoCoil.AskSndPDO.Data(msg));
        }
        private void CS07_PrintLabelPro(SCCommMsg.CS07_PrintLabel msg)
        {
            _log.I("HMI通知", $"操作要求手動列印標籤, 鋼捲{msg.CoilID}列印標籤");
            _log.D("HMI通知", $"{JsonUtil.ToJson(msg)}");
            // TODO 標籤機列印
            MQPoolService.SendToLpr(InfoLpr.ManualPrint.Data(msg));
        }
        private void CS08_WeightInputPro(SCCommMsg.CS08_WeightInput msg)
        {
            _log.I("HMI通知", "操作手動輸入秤重資料, CLIENT通知SERVER更新鋼捲秤重資料（毛重）");
            _log.D("HMI通知", $"{JsonUtil.ToJson(msg)}");
            // TODO 操作手動輸入秤重資料

        }
        private void CS09_LineFaultDataPro(SCCommMsg.CS09_LineFaultData msg)
        {
            _log.I("HMI通知", "停復機紀錄");
            _log.D("HMI通知", $"{JsonUtil.ToJson(msg)}");
            //_hmiClient.Tell(msg);
            MQPoolService.SendToDtGtr(InfoDtGtr.UploadLineFault.Data(msg));
        }
        private void CS10_Coil_AutoFeedModeChange(SCCommMsg.CS10_Coil_AutoFeedModeChange msg)
        {
            _log.I("自動入料模式變更", "收到自動入料模式變更，通知Server進行模式確認");
            _log.D("自動入料模式變更", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.CheckCoilEnterInfo.Data(msg));
        }
        private void CS11_Coil_ManualFeed(SCCommMsg.CS11_Coil_ManualFeed msg)
        {
            _log.I("手動操作通知Server可以入料", "收到手動操作通知Server可以入料通知");
            _log.D("手動操作通知Server可以入料", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.SndEntryCoilReqMsg.Data(msg));
        }
        private void CS12_Coil_SkidFeed(SCCommMsg.CS12_Coil_SkidFeed msg)
        {
            _log.I("鞍座上手動操作入料", "鞍座上手動操作入料通知");
            _log.D("鞍座上手動操作入料", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.SndSkidFeedMsg.Data(msg));
        }

        private void CS13_DeleteSidCoil(SCCommMsg.CS13_DeleteSidCoil msg)
        {
            _log.I("手動刪除鞍座鋼卷號", $"手動刪除鞍座鋼卷號{msg.Coil_ID}");
            _log.D("手動刪除鞍座鋼卷號", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.InfoL1DelCoilIDOnSk.Data(msg));
        }

        private void CS14_DeliveryCoilOut(SCCommMsg.CS14_DeliveryCoilOut msg)
        {
            _log.I("出口段出料", $"出口鋼捲{msg.CoilID}段出料通知 ");
            _log.D("出口段出料", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.DeliveryCoilOut.Data(msg));
        }

        private void CS15_Utility(SCCommMsg.CS15_Utility msg)
        {
            _log.I("能源消耗上傳", $"能源消耗上傳 班次:{msg.ShiftName} 班別:{msg.GroupName} 冷卻水:{msg.TotalCoolingWater} 壓縮空氣:{msg.TotalCompressedAir} 蒸氣:{msg.TotalSteam} 清洗水:{msg.TotalRinseWater}");
            _log.D("能源消耗上傳", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToMMS(InfoMMS.UploadEnergyConsumptionInfo.Data(msg));

        }


        private void CS16_FinishLoadSchedule(SCCommMsg.CS16_FinishLoadSchedule msg)
        {
            _log.I("完成匯入排程", "完成匯入排程通知");
            _log.D("完成匯入排程", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.SndPresetInfo.Data(msg));
        }

        private void CS17_FinishLoadPDI(SCCommMsg.CS17_FinishLoadPDI msg)
        {
            _log.I("完成匯入PDI", "完成匯入PDI通知");
            _log.D("完成匯入PDI", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.SndPresetInfo.Data(msg));
        }

        private void CS18_CarneEntryCoilSelect(SCCommMsg.CS18_CarneEntryCoilSelect msg)
        {
            _log.I("天車入料選擇", $"天車入料選擇鋼捲{msg.coilID}");
            _log.D("天車入料選擇", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.CarneEntryCoilSelect.Data(msg));
        }

        private void CS19_RequestDummy(SCCommMsg.CS19_RequestDummy msg)
        {
            _log.I("操作端過渡捲清單請求", "操作端過渡捲清單請求");
            _log.D("操作端過渡捲清單請求", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToMMS(InfoMMS.RequestDummyCoil.Data(msg));
        }
        private void CS20_DeleteDummy(SCCommMsg.CS20_DeleteDummy msg)
        {
            _log.I("操作端過渡捲刪除", "操作端過渡捲刪除");
            _log.D("操作端過渡捲刪除", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.InfoMMSDelDummyResult.Data(msg));
        }


        private void CS21_DeliveryCoilReady(SCCommMsg.CS21_DeliveryCoilReady msg)
        {
            _log.I("操作出料確認", $"操作{msg.Coil_ID.Trim()}出料確認");
            _log.D("操作出料確認", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.DeliveryCoilReady.Data(msg));
        }

        private void CS22_POR_PresetL1(CS22_POR_PresetL1 msg)
        {
            var coilID = msg.Coil_ID.Trim();
            _log.I("操作發POR Preset", $"操作{coilID}發送POR Preset");
            _log.D("操作發POR Preset", $"{JsonUtil.ToJson(msg)}");

            var preset = new SpectPresetModel()
            {
                CoilID = msg.Coil_ID,
                SKPosID = PlcSysDef.Pos.L1204_205LinePresetPos,
                PlanNo = msg.Plan_No,
            };

            MQPoolService.SendToDtStp(InfoDataSetup.SpecificIDTo_204_205Msg.Data(preset));

        }
        private void CS23_POR_StripBreakModify(CS23_POR_StripBreakModify msg)
        {
            _log.I("操作通知修改POR子捲號", $"操作通知修改POR子捲號{msg.Coil_ID}");
            _log.D("操作通知修改POR子捲號", $"{JsonUtil.ToJson(msg)}");

            MQPoolService.SendToCoil(InfoCoil.ModifyPORCoilID.Data(msg));
        }
        private void BroadCast(object msg)
        {
            foreach (var client in _clients)
                client.Value.Tell(msg);
        }

        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }


    }
}
