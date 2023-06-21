using Core.Help;
using System.Configuration;

namespace BCScnMgr.Config
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
        public string ConnLog { get; private set; }
        public string RcvEditLog { get; private set; }
        //public string SndLog { get; private set; }
        public string SndEditLog { get; private set; }

        public AppSetting()
        {
            // Akka Setting
            AkkaSysName = IniSystemHelper.Instance.BarCodeApp;
            LocalIp = IniSystemHelper.Instance.BarCodeLocalIP;
            LocalPort = IniSystemHelper.Instance.BarCodeLocalPort;
            //AkkaSysName = GetAppConfigValue("ActorSystemName");
            //LocalIp = GetAppConfigValue("LocalIp");
            //LocalPort = GetAppConfigIntVaule("LocalPort");
            //RemoteIp = GetAppConfigValue("RemoteIP");
            //RemotePort = GetAppConfigIntVaule("RemotePort");

            // Akka Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "BCScnMgrLog";
            ConnLog = "BCScnConnLog";
            RcvEditLog = "BCScnRcvEditLog";
            //SndLog = "BCScnSndLog";
            SndEditLog = "BCScnSndEditLog";

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
