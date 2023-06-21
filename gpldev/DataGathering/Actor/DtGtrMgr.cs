using Akka.Actor;
using MsgStruct;
using Controller.DtGtr;
using MSMQ;
using Core.Util;
using AkkaSysBase.Base;
using AkkaSysBase;
using Core.Define;
using System;
using Core.Help;
using DataMod.Common;
using LogSender;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using Controller;
using MSMQ.Core.MSMQ;
using System.Timers;
namespace DataGathering.Config.Actor
{


    public class DtGtrMgr : BaseActor
    {
        private IActorRef _selfActor;
        private IDataGatheringController _dtgtrService;
        private ICancelable _tmrCheckCorssShift;        // 跨班確認
        //private Timer _downTimeTimer;               // 停機跨班開關
        private int _LineStopRun = PlcSysDef.Cmd.LineStatusRun;


        public DtGtrMgr(ISysAkkaManager akkaManager, IDataGatheringController dtgtrService, ILog log) : base(log)
        {
            _tmrCheckCorssShift?.Cancel();

            _selfActor = akkaManager.GetActor(GetType().Name);
            _dtgtrService = dtgtrService;
            _dtgtrService.SetLog(log);

            // Init Timer
            //InitDownTimeTimer();
            //  var Ts = new TimeSpan(lineFaultRecord.stop_end_time.Ticks - lineFaultRecord.stop_start_time.Ticks);
            //var ts = new TimeSpan(DateTime.Now.Ticks - DateTime.Now.AddDays(-1).Ticks);


            // 開啟時確認是否仍在停機
            PreCheckCrossShift();

            MQPool.ReceiveFromDtGtr(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });

            // 確定停復機是否有跨班狀況
            PreCheckCrossShift();

            // 接收焊接訊號
            Receive<L1L2Rcv.Msg_106_Weld_Data>(message=>TryFlow(() => SaveCoilWeld(message)));
            // 接收研磨道次
            Receive<L1L2Rcv.Msg_107_Grd_Rpt>(message=> TryFlow(() => SaveGrdRpt(message)));
            // 接收Process Data
            Receive<L1L2Rcv.Msg_104_ProData>(message => TryFlow(() => SaveProcessData(message)));
            // 存取Utility Data
            Receive<L1L2Rcv.Msg_112_Utility>(message => TryFlow(() => SaveUtility(message)));
            // 存取斷帶訊號資料
            Receive<L1L2Rcv.Msg_124_StripBrakeSignal>(message => TryFlow(() => { SaveStripBrakeSignal(message); SaveUmountRecord(message); }));
            // 停機紀錄
            Receive<L1L2Rcv.Msg_111_LineFault>(message => TryFlow(() => SaveLineFault(message)));
            // 切鋼捲紀錄
            Receive<L1L2Rcv.Msg_125_Share_Cut_Data>(message => TryFlow(() => SaveCoilCutRecord(message)));
            // Umount POR訊息
            Receive<L1L2Rcv.Msg_126_Coil_Unmount_POR>(message => TryFlow(() => SaveUmountRecord(message)));

            // 上傳停復機資訊
            Receive<CS09_LineFaultData>(message => TryFlow(() => UploadLineFault(message)));

            Receive<CheckCrossShiftModel>(message => TryFlow(() => ProCrossShift(message)));
            

            ReceiveAny(message => RcvObject(message));


            //測試
            //var test = _dtgtrService.GetScheduleByTime(DateTime.Now);
            //test = _dtgtrService.GetScheduleByTime(DateTime.Now.AddHours(-9).AddMinutes(-14));
            //var testMsg = new L1L2Rcv.Msg_112_Utility();
            //testMsg.VaildObjectNull("msg", "存取 2.5 Utility資料失敗");
            //_dtgtrService.CreateL25Engc(testMsg);
        }


        //private void InitDownTimeTimer()
        //{
        //    _downTimeTimer = new Timer();
        //    _downTimeTimer.Enabled = false;
        //    _downTimeTimer.Interval = 1000;
        //}
        //private void PreCheckDownTimeTimer()
        //{
        //    if (_LineStopRun == PlcSysDef.Cmd.LineStatusStop)
        //    {
        //        _downTimeTimer.Start();
        //        _downTimeTimer.Elapsed += new ElapsedEventHandler(Timer_DownTime);
        //    }
        //}

        private void SaveCoilWeld(L1L2Rcv.Msg_106_Weld_Data msg)
        {
            _log.I("資料蒐集", $"存取{msg.CoilIDNo}報文至CoilWeld資料庫 ");
            _dtgtrService.CreateCoilWeld(msg);
        }
        private void SaveGrdRpt(L1L2Rcv.Msg_107_Grd_Rpt msg)
        {
            _log.I("資料蒐集", $"存取{msg.CoilID.ToStr()}研磨道次資料 ");
            _dtgtrService.CreateGrdRpt(msg);
        }

        private void SaveProcessData(L1L2Rcv.Msg_104_ProData msg)
        {
            _log.I("資料蒐集", $"存取鋼卷Process資料 ");
            _dtgtrService.CreateProData(msg);
        }

        private void SaveUtility(L1L2Rcv.Msg_112_Utility msg)
        {
            _log.I("資料蒐集", $"存取Utility能源耗用實績資料");
            _dtgtrService.CreateL25Engc(msg);



            var workSchedule = _dtgtrService.GetScheduleByTime(DateTime.Now);
            if (workSchedule == null)
            {
                _log.A($"無班次股別可撈取", $"日期{DateTime.Now}無對應班次股別可撈取 ");
                _log.E($"撈取目前班次股別", $"【班次】: Error 【班次】: Error");

                _dtgtrService.CreateUtility(msg, string.Empty, string.Empty);
                return;
            }
            _dtgtrService.CreateUtility(msg, workSchedule.Shift.ToString(), workSchedule.Team);

        }

        private void SaveStripBrakeSignal(L1L2Rcv.Msg_124_StripBrakeSignal msg)
        {
            _log.I("資料蒐集", $"存取一級斷帶資訊");
            _dtgtrService.CreateStripBrakeSignal(msg);            
        }

        private void SaveUmountRecord(L1L2Rcv.Msg_124_StripBrakeSignal msg)
        {
            _log.I("資料蒐集", $"存取斷帶資料至Umount資料庫 ");
            _dtgtrService.CreateUmountRecord(msg);
        }

        private void SaveLineFault(L1L2Rcv.Msg_111_LineFault msg)
        {

            // 關閉CrossShift檢查
            _tmrCheckCorssShift?.Cancel();

            if (msg.LineStatus == PlcSysDef.Cmd.LineStatusStop)
            {
                //目前產線是否為停機
                if (_LineStopRun == PlcSysDef.Cmd.LineStatusStop) 
                {
                    return;
                }

                var nowShift = ShiftHelp.GetShiftNo(DateTime.Now);
                var workSchedule = _dtgtrService.GetScheduleByTime(DateTime.Now);

                if (workSchedule == null)
                {
                    _log.E($"無班次股別可撈取", $"日期{DateTime.Now}無對應班次股別可撈取 ");
                    _log.E($"撈取目前班次股別", $"【班次】: Error 【班次】: Error");
                    _dtgtrService.CreateStopLineFaultStart(msg, string.Empty, nowShift);
                }
                else
                {

                    _log.I($"撈取目前班次股別", $"【班次】: {workSchedule.Shift} 【班次】:{workSchedule.Team}");
                    _dtgtrService.CreateStopLineFaultStart(msg, workSchedule.Team, workSchedule.Shift);

                }
                _LineStopRun = PlcSysDef.Cmd.LineStatusStop;


                #region 開啟跨班Timer檢查先省略待確定
                var shiftInfo = new CheckCrossShiftModel
                {
                    FaultCode = msg.FaultCode,
                    Shift = nowShift,
                    StopStartTime = msg.DateTime,
                };

                SetCrossShiftCheckTmr(shiftInfo);
                #endregion

                return;
            }


            // 覆機
            if (msg.LineStatus == PlcSysDef.Cmd.LineStatusRun)
            {
                var UpdateOK  = _dtgtrService.UpdateStopLineFaultEnd(msg);
                if (UpdateOK)
                {
                    _LineStopRun = PlcSysDef.Cmd.LineStatusRun;
                }

            }
         

        }

        private void SaveCoilCutRecord(L1L2Rcv.Msg_125_Share_Cut_Data msg)
        {
            _log.I("資料蒐集", $"存取Coil Cut Record至資料庫 ");
            _dtgtrService.CreateCutRecord(msg);
        }

        private void SaveUmountRecord(L1L2Rcv.Msg_126_Coil_Unmount_POR msg)
        {
            _log.I("資料蒐集", $"存取Coil Umount POR至資料庫 ");
            _dtgtrService.CreateUmountRecord(msg);
        }

        private void UploadLineFault(CS09_LineFaultData msg)
        {
            //msg.op_flag 區分
            if (msg.Op_Flag.Trim() == "") //1 - 新增 ;
            {
                var lineFault = _dtgtrService.GetLineFaultRecord(msg.prod_time, msg.stop_start_time,msg.stop_end_time);

                if (lineFault != null)
                {
                    MQPoolService.SendToMMS(InfoMMS.UploadLineFaultRecord.Data(lineFault));
                    _dtgtrService.UpdateLineFaultUploadFlag(lineFault.prod_time, lineFault.stop_start_time, lineFault.stop_end_time, true);
                    _dtgtrService.CreateL25DownTime(lineFault);
                }

            }
            if (msg.Op_Flag.Trim() == "2") // 2 - 删除 ;
            {
                // var lineFault = _dtgtrService.GetLineFaultDelRecord(msg.prod_time, msg.stop_start_time);
                //if (lineFault != null)
                //{
                //    MQPoolService.SendToMMS(InfoMMS.UploadLineFaultRecord.Data(lineFault));

                //}


            }
            if (msg.Op_Flag.Trim() == "3") // 3 - 修改 ;
            {
                var lineFault = _dtgtrService.GetLineFaultRecord(msg.prod_time, msg.stop_start_time, msg.stop_end_time);
              
                if (lineFault != null)
                {
                MQPoolService.SendToMMS(InfoMMS.UploadLineFaultRecord.Data(lineFault));
                _dtgtrService.UpdateLineFaultUploadFlag(lineFault.prod_time, lineFault.stop_start_time, lineFault.stop_end_time, true);
                //_dtgtrService.CreateL25DownTime(lineFault);
                }
            }

           
        }

        private void ProCrossShift(CheckCrossShiftModel checkCrossShift)
        {

            //判定是否為停機
            if (_LineStopRun == PlcSysDef.Cmd.LineStatusStop)
            { 
                var nowShift = ShiftHelp.GetShiftNo(DateTime.Now);
                var workSchedule = _dtgtrService.GetScheduleByTime(DateTime.Now);
                //取得停機資訊
                var DowntimeInfo = _dtgtrService.GetLastLineFaultUnfinshRecord();
                if (DowntimeInfo != null )
                {
                    var UpdateOK = _dtgtrService.AutoUpdateShiftRecord(nowShift,workSchedule.Team);               
                    // if (lineFaultRecord.prod_shift_no != nowShift.ToString())
                }

            }
            //// 發生跨班
            //if(checkCrossShift.Shift != nowShift)
            //{
            //    // 結算
            //    var upOk = _dtgtrService.UpdateStopLineFaultEnd(checkCrossShift);

            //    // 重新新增一筆
            //    if (upOk)
            //        _dtgtrService.SaveStopLineFaultStart(checkCrossShift, nowShift, DateTime.Now.Date.ToString("yyyyMMdd"));

            //};

        }
        
        private void PreCheckCrossShift()
        {
            var shiftInfo = _dtgtrService.GetLastLineFaultUnfinshRecord();

            if (shiftInfo == null)
               return;
            //if (_LineStopRun == PlcSysDef.Cmd.LineStatusStop)
            //{ 
            //    var nowShift = ShiftHelp.NowShift();
            //    var UpdateOK = _dtgtrService.AutoUpdateShiftRecord(nowShift);   
            //}


            // 確定有最後一筆資料，且Stop end time為Null
            SetCrossShiftCheckTmr(shiftInfo);

        }

        private void RcvObject(object message)
        {

            _log.E("接收資料", $"Received an unhandled message!!! type:{message.GetType().ToString()} from Sender:{Sender.Path.ToString()}");
        }

        private void SetCrossShiftCheckTmr(CheckCrossShiftModel checkCrossShift)
        {
            _tmrCheckCorssShift?.Cancel();
            var interval = TimeSpan.FromSeconds(1);
            var initDelay = TimeSpan.FromSeconds(1);
            _tmrCheckCorssShift = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(initDelay, interval, Self, checkCrossShift, Self);
        }



    }
}
