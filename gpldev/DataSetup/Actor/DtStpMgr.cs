using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Core.Util;
using DataMod.Common;
using LogSender;
using MsgConvert;
using MsgConvert.Msg;
using MsgStruct;
using MSMQ;
using MSMQ.Core.MSMQ;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Timers;
using System.Threading;

namespace DataSetup.Actor
{
    public class DtStpMgr : BaseActor
    {

        private IActorRef _selfActor;
        private ICoilController _coilService;


        // Thread 機制 -Preset  Log 要Save取消狀況
        private int MAX_COUNTER = 40;
        private ConcurrentQueue<string> _coilScedules;
        private System.Timers.Timer _tmrSend;  
        private int _queueIndex;

        public DtStpMgr(ISysAkkaManager akkaManager, ICoilController coilService, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _coilService = coilService;
            _coilService.SetLog(log);

            _coilScedules = new ConcurrentQueue<string>();

            if(_tmrSend==null) _tmrSend = new System.Timers.Timer(200);
            _queueIndex = 0;
            _tmrSend.Elapsed += TmrSendElapsed;             //Register

            MQPool.GetMQ("DtStpMgr").Receive(x =>
            {
                _selfActor.Tell(x);
            });

            Receive<MQPool.MQMessage>(message => TryFlow(() => ParsingMQID(message)));
        }

        /// <summary>
        /// 解析MQ ID
        /// </summary>     
        private void ParsingMQID(MQPool.MQMessage msg)
        {
            // 排程Preset
            if (msg.ID == InfoDataSetup.CoilSchedIDTo_204_205Msg.Event)
            {
                ClearStatus();

                var entryCoilIDs = msg.Data as List<string>;
                _log.I("Preset資料", $"產生204,205-Preset報文處理");

                foreach (string coilID in entryCoilIDs)
                    _coilScedules.Enqueue(coilID);

                _tmrSend.Start();

                return;
            }

            if (msg.ID == InfoDataSetup.CoilIDTo202Msg.Event)
            {

                var preset202PDI = msg.Data as Preset202PDI;
                var pdi = _coilService.GetPDI(preset202PDI.CoilNo);

                if (pdi == null)
                {
                    _log.A("組Preset202", $"組Preset202失敗,無{preset202PDI.CoilNo}PDI");
                    return;
                }

                var presetL1202 = pdi.ConvertToPreset202Msg(preset202PDI.SkNo);
                MQPoolService.SendToL1(InfoL1.SndPreset202Msg.Data(presetL1202));



                return;
            }

            if (msg.ID == InfoDataSetup.SpecificIDTo_204_205Msg.Event)
            {
                var preset = msg.Data as SpectPresetModel;
                var coilID = preset.CoilID;
                var skPOSID = preset.SKPosID;

                var pdi = _coilService.GetPDI(coilID);
                if (pdi == null)                
                    _log.E("無PDI資料", $"無鋼捲{coilID} PDI資料");
                
                // 存 Grind PlanHistory
                var beltPlans = _coilService.QueryBeltPlans(coilID);
                var planNo = pdi == null ? string.Empty : pdi.Plan_No;
                _coilService.CreateGrindPlanHistory(coilID, planNo, beltPlans);

                // 發Preset
                if (!coilID.Trim().Equals(string.Empty))
                { 
                   SndPresetPro(coilID, skPOSID);
                }



            }

            // TEST 測試重新發送使用
            if (msg.ID == InfoDataSetup.ResendL1Msg.Event)
            {
                var entryCoilID = msg.Data as string;
                // 發Preset
                SndPresetPro(entryCoilID, 0, true);
            }

        }

        private void SndPresetPro(string coilID, int pos, bool isResend = false)
        {
            if (coilID.Trim().Equals(string.Empty))
            {
                _log.E("SndPresetPro", $"無鋼捲{coilID} 資料");
                return;
            }

            var presetL1204 = new L2L1Snd.Msg_204_PDI_TM3();
            var presetL1205 = new L2L1Snd.Msg_205_PDI_TM3_2();

            var pdi = _coilService.GetPDI(coilID);

            if (pdi != null)
            {

                var lkTable = _coilService.GetPreset204LkTableData(pdi);
                var belPattern = _coilService.QueryBeltPatterns(pdi.In_Coil_ID);

                presetL1204 = pdi.ConvertToPreset204Msg(pos, lkTable);
                presetL1205 = pdi.ConvertToPreset205Msg(pos);

                if (belPattern != null)
                    GrindProFactory.LoadBeltPattern(belPattern, ref presetL1204, ref presetL1205);
            }
            else
            {
                presetL1204 = L1MsgFactory.Empty204PresetMsg(coilID);
                presetL1205 = L1MsgFactory.Empty205PresetMsg();

            }

            // TESTUSING -> Resend L1SnedEdit
            if (isResend)
            {
                MQPoolService.SendToL1(InfoL1.ReSndPreset204Msg.Data(presetL1204));
                MQPoolService.SendToL1(InfoL1.ReSndPreset205Msg.Data(presetL1205));
                return;
            }          
            MQPoolService.SendToL1(InfoL1.SndPreset204Msg.Data(presetL1204));
            MQPoolService.SendToL1(InfoL1.SndPreset205Msg.Data(presetL1205));
        }
     
        // 取出Queu資料組Preset
        private void TmrSendElapsed(object sender, ElapsedEventArgs e)
        {
            _queueIndex += 1;
            
            if (_queueIndex > MAX_COUNTER)
            {
                ClearStatus();
                return;
            }

            string coilID;
            _coilScedules.TryDequeue(out coilID);
            if(coilID!=null) SndPresetPro(coilID, _queueIndex);


            //var coilID = _coilScedules.Count!=0 ? _coilScedules.Dequeue(): "";
            //SndPresetPro(coilID, _queueIndex);

        }

        private void ClearStatus()
        {
            _tmrSend.Close();
            _coilScedules.Clear();           
            _queueIndex = 0;         
        }


        // Actor Prestart (重啟時處理)
        protected override void PreRestart(Exception reason, object message)
        {
            _log.E("AThread生命週期",  Context.System.Name + " PreRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);


            if (_tmrSend != null)
            {  
                ClearStatus();
                _tmrSend.Elapsed -= TmrSendElapsed;             //UnRegister
            }
            
        }


    }
}
