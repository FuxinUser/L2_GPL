using Akka.Event;
using DBService.Repository.EventLog;
using MSMQ;
using MSMQ.Core.MSMQ;
using System;

namespace LogSender
{
    public class Log : ILog
    {
        private ILoggingAdapter _nlog;       // Nlog

        private bool _isConsoleLogOn;
        private bool _sndDB;

        private string _System_ID;          // 系統編號 
        private string _FunctionBlock;      // Server子系統編號 1:Server 2:Client
        private string _FrameGroupNo;       // Client畫面群組編號
        private string _FarmeNo;            // Clinet畫面編號

        public Log(string system_ID, string functionBlock, ILoggingAdapter nlog = null, bool sndDB = true, bool consoleLogOn = false)
        {
            _System_ID = system_ID;
            _FunctionBlock = functionBlock;
            _nlog = nlog;
            _isConsoleLogOn = consoleLogOn;
            _sndDB = sndDB;
        }


        public Log()
        {
            _System_ID = "1";
            _isConsoleLogOn = false;
            _sndDB = true;
        }


        public void E(string title, string content)
        {
            if (_sndDB) SndToLogMgr(LogDef.ERROR, title, content);
            if (_nlog != null) _nlog.Error("【" + title + "】" + ":" + content);
            if (_isConsoleLogOn) System.Diagnostics.Debug.WriteLine("【" + title + "】" + ":" + content);
        }
        public void A(string title, string content)
        {
            if (_sndDB) SndToLogMgr(LogDef.ALARM, title, content);
            if (_nlog != null) _nlog.Warning("【" + title + "】" + ":" + content);
            if (_isConsoleLogOn) System.Diagnostics.Debug.WriteLine("【" + title + "】" + ":" + content);
        }
        public void I(string title, string content)
        {
            if (_sndDB) SndToLogMgr(LogDef.INFO, title, content);
            if (_nlog != null) _nlog.Info("【" + title + "】" + ":" + content);
            if (_isConsoleLogOn) System.Diagnostics.Debug.WriteLine("【" + title + "】" + ":" + content);
        }
        public void D(string title, string content)
        {
            if (_sndDB) SndToLogMgr(LogDef.DEBUG, title, content);
            if (_nlog != null) _nlog.Debug("【" + title + "】" + ":" + content);
            if (_isConsoleLogOn) System.Diagnostics.Debug.WriteLine("【" + title + "】" + ":" + content);
        }

        private void SndToLogMgr(string logLevel, string title, string content)
        {
            var log = new EventLogEntity.TBL_EventLog()
            {
                System_ID = _System_ID,
                Function_Block = _FunctionBlock,
                FrameGroup_No = _FrameGroupNo,
                Frame_No = _FarmeNo,
                Event_Type = logLevel,
                Event_Description = "[" + title + "]",
                Command = content,
                CreateTime = DateTime.Now
            };

            MQPool.SendToLog(InfoLog.SaveLogMsg.Event, log);
        }
    }
}
