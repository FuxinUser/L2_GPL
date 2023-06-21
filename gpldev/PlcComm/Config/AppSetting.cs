using Core.Help;
using System.Configuration;



/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: App.Config
 * Reference: 
 * Modified: 
 */

namespace PLCComm.Config
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

        public string CycleRcvLocalIp { get; private set; }
        public int CycleRcvLocalPort { get; private set; }

        public string RemoteIp { get; private set; }
        public int RemotePort { get; private set; }


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

        // Akka System Log Setting
        public string AkkaSysLog { get; private set; }
        public string MgrLog { get; private set; }
        public string RcvLog { get; private set; }
        public string CycleRcvRcvLog { get; private set; }
        public string RcvEditLog { get; private set; }
        public string SndLog { get; private set; }
        public string SndEditLog { get; private set; }

        // 應答設置
        public int ReSndCnt { get; private set; }
        public int ReSndTimeOut { get; private set; }
        public int DetectSndQueeTime { get; private set; }

        public bool IsDetectAck { get; private set; }

        public AppSetting()
        {
            // Sys Setting
            AkkaSysName = IniSystemHelper.Instance.PLCApp;
            LocalIp = IniSystemHelper.Instance.PLCLocalIP;
            LocalPort = IniSystemHelper.Instance.PLCLocalPort;
            CycleRcvLocalPort = IniSystemHelper.Instance.PLCCycleLocalPort; 
            CycleRcvLocalIp = IniSystemHelper.Instance.PLCCycleLocalIP;
            RemoteIp = IniSystemHelper.Instance.PLCRemoteIP;
            RemotePort = IniSystemHelper.Instance.PLCRemotePort;

            // Dump Setting
            RcvMsgFilePath = GetConfigValue("RcvMsgFilePath");
            SndMsgFilePath = GetConfigValue("SndMsgFilePath");
            FailMsgFilePath = GetConfigValue("FailMsgFilePath");
            DebugFilePath = GetConfigValue("DebugMsgFilePath");
            CrashLogPath = GetConfigValue("CrashLogFilePath");

            DumpRcvMsgSwitchOn = IniSystemHelper.Instance.PLCDumpRcvMsgSwitch;
            DumpSndMsgSwitchOn = IniSystemHelper.Instance.PLCDumpSndMsgSwitch;
            DumpFailMsgSwitchOn = IniSystemHelper.Instance.PLCDumpFailMsgSwitch;
            DumpDebugMsgSwitchOn = IniSystemHelper.Instance.PLCDumpDebugMsgSwitch;


            ReSndCnt = GetConfigIntVaule("ReSndCnt");
            ReSndTimeOut = GetConfigIntVaule("ReSndTimeOut");
            DetectSndQueeTime = GetConfigIntVaule("DetectSndQueeTime");

            IsDetectAck = GetConfigBoolVaule("IsDetectAck");


            // Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "PlcMgrLog";
            RcvLog = "PlcRcvLog";
            RcvEditLog = "PlcRcvEditLog";
            SndLog = "PlcSndLog";
            SndEditLog = "PlcSndEditLog";
            CycleRcvRcvLog = "PlcCycleRcvLog";
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
