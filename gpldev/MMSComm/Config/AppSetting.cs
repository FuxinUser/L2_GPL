using Core.Help;
using System.Configuration;

namespace MMSComm.Config
{
    public class AppSetting
    {
        public static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly AppSetting INSTANCE = new AppSetting();
        }
        public static AppSetting Instance { get { return SingletonHolder.INSTANCE; } }

        // Akka System Setting
        public string AkkaSysName { get; private set; }
        public string LocalIp { get; private set; }
        public int LocalPort { get; private set; }
        public string RemoteIp { get; private set; }
        public int RemotePort { get; private set; }

        // Akka System Log Setting
        public string AkkaSysLog { get; private set; }
        public string MgrLog { get; private set; }
        public string RcvLog { get; private set; }
        public string RcvEditLog { get; private set; }
        public string SndLog { get; private set; }
        public string SndEditLog { get; private set; }

        // Dump Msg File
        public string RcvMsgFilePath { get; private set; }
        public string SndMsgFilePath { get; private set; }
        public string FailMsgFilePath { get; private set; }
        public string DebugFilePath { get; private set; }

        // Crash Log Path
        public string CrashLogPath { get; private set; }

        // Dump Switch
        public bool DumpRcvMsgSwitchOn { get; private set; }
        public bool DumpSndMsgSwitchOn { get; private set; }
        public bool DumpDebugMsgSwitchOn { get; private set; }
        public bool DumpFailMsgSwitchOn { get; private set; }

        // 應答設置
        public int ReSndCnt { get; private set; }
        public int ReSndTimeOut { get; private set; }
        public int DetectSndQueeTime { get; private set; }
        public bool IsDetectAck { get; private set; }

        // 心跳 && Ack
        public bool isSndHeartBeat { get; private set; }


        public AppSetting()
        {
            // Akka Setting
            AkkaSysName = IniSystemHelper.Instance.MMSApp;
            LocalIp = IniSystemHelper.Instance.MMSLocalIP;
            LocalPort = IniSystemHelper.Instance.MMSLocalPort;
            RemoteIp = IniSystemHelper.Instance.MMSRemoteIP;
            RemotePort = IniSystemHelper.Instance.MMSRemotePort;


            // Dump Setting
            RcvMsgFilePath = GetConfigValue("RcvMsgFilePath");
            SndMsgFilePath = GetConfigValue("SndMsgFilePath");
            FailMsgFilePath = GetConfigValue("FailMsgFilePath");
            DebugFilePath = GetConfigValue("DebugMsgFilePath");
            CrashLogPath = GetConfigValue("CrashLogFilePath");

            DumpRcvMsgSwitchOn = IniSystemHelper.Instance.MMSDumpRcvMsgSwitch;
            DumpSndMsgSwitchOn = IniSystemHelper.Instance.MMSDumpSndMsgSwitch;
            DumpFailMsgSwitchOn = IniSystemHelper.Instance.MMSDumpFailMsgSwitch;
            DumpDebugMsgSwitchOn = IniSystemHelper.Instance.MMSDumpDebugMsgSwitch;


            ReSndCnt = GetConfigIntVaule("ReSndCnt");
            ReSndTimeOut = GetConfigIntVaule("ReSndTimeOut");
            DetectSndQueeTime = GetConfigIntVaule("DetectSndQueeTime");
            IsDetectAck = GetConfigBoolVaule("IsDetectAck");

            isSndHeartBeat = GetConfigBoolVaule("IsSndHeartbeat");

            // Akka Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "MMSMgrLog";
            RcvLog = "MMSRcvLog";
            RcvEditLog = "MMSRcvEditLog";
            SndLog = "MMSSndLog";
            SndEditLog = "MMSSndEditLog";

        }
        private string GetConfigValue(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }
        private int GetConfigIntVaule(string value)
        {
            return int.Parse(ConfigurationManager.AppSettings[value]);
        }
        private bool GetConfigBoolVaule(string value)
        {
            return bool.Parse(ConfigurationManager.AppSettings[value]);
        }
    }
}
