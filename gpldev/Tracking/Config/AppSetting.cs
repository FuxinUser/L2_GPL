﻿using System.Configuration;

namespace Tracking.Config
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


        // Akka System Log Setting
        public string AkkaSysLog { get; private set; }
        public string MgrLog { get; private set; }
        public string ScnLog { get; private set; }
        public AppSetting()
        {
            // Akka Setting
            AkkaSysName = GetAppConfigValue("ActorSystemName");
        
            // Akka Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "TrkMgrLog";
            ScnLog = "TrkScanLog";
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
