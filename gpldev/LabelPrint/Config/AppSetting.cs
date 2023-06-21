using Core.Help;
using System.Configuration;

namespace LabelPrint.Config
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

        public AppSetting()
        {
            // Akka Setting
            //AkkaSysName = GetAppConfigValue("ActorSystemName");
            //LocalIp = GetAppConfigValue("LocalIp");
            //LocalPort = GetAppConfigIntVaule("LocalPort");
            //RemoteIp = GetAppConfigValue("RemoteIP");
            //RemotePort = GetAppConfigIntVaule("RemotePort");

            AkkaSysName = IniSystemHelper.Instance.PrinterApp;
            RemoteIp = IniSystemHelper.Instance.PrinterRemoteIP;
            RemotePort = IniSystemHelper.Instance.PrinterRemotePort;

            // Akka Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "LprMgrLog";
            RcvLog = "";
            RcvEditLog = "";
            SndLog = "LprSndLog";
            SndEditLog = "LprSndEditLog";

        }
        private string GetAppConfigValue(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }
        private int GetAppConfigIntVaule(string value)
        {
            return int.Parse(ConfigurationManager.AppSettings[value]);
        }
    }
}
