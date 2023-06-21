using Core.Define;
using Core.Util;

namespace Core.Help
{
    public class IniSystemHelper
    {
        public IniUtil IniManager { get; set; }

        public static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly IniSystemHelper INSTANCE = new IniSystemHelper()
            {

                IniManager = new IniUtil(SystemFileDef.iniPath)
            };
        }
        public static IniSystemHelper Instance { get { return SingletonHolder.INSTANCE; } }

        public string DBConn { get => IniManager.ReadIni(SystemFileDef.IniDBSection, SystemFileDef.IniDBKey); }

        public string HisDBConn { get => IniManager.ReadIni(SystemFileDef.IniDBSection, SystemFileDef.IniHISDBKey); }

        public string Level2_5_DBConn { get => IniManager.ReadIni(SystemFileDef.IniDBSection, SystemFileDef.Ini2_5DBKey); }

        // MMS
        public string MMSApp { get => IniManager.ReadIni(SystemFileDef.IniMMSSection, SystemFileDef.Ini_System_Name_Key); }
        public string MMSLocalIP { get => IniManager.ReadIni(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Socket_Local_IP_Key); }
        public int MMSLocalPort { get { return GetIniValue(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Socket_Local_Port_Key); } }
        public string MMSRemoteIP { get => IniManager.ReadIni(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Socket_Remote_IP_Key); }
        public int MMSRemotePort { get { return GetIniValue(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Socket_Remote_Port_Key); } }

        public bool MMSDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Rcv_RawData_Key); } }
        public bool MMSDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Snd_RawData_Key); } }
        public bool MMSDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Fail_RawData_Key); } }
        public bool MMSDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniMMSSection, SystemFileDef.Ini_Debug_RawData_Key); } }

        // WMS
        public string WMSApp { get => IniManager.ReadIni(SystemFileDef.IniWMSSection, SystemFileDef.Ini_System_Name_Key); }
        public string WMSLocalIP { get => IniManager.ReadIni(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Socket_Local_IP_Key); }
        public int WMSLocalPort { get { return GetIniValue(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Socket_Local_Port_Key); } }
        public string WMSRemoteIP { get => IniManager.ReadIni(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Socket_Remote_IP_Key); }
        public int WMSRemotePort { get { return GetIniValue(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Socket_Remote_Port_Key); } }

        public bool WMSDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Rcv_RawData_Key); } }
        public bool WMSDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Snd_RawData_Key); } }
        public bool WMSDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Fail_RawData_Key); } }
        public bool WMSDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniWMSSection, SystemFileDef.Ini_Debug_RawData_Key); } }

        // PLC
        public string PLCApp { get => IniManager.ReadIni(SystemFileDef.IniPLCSection, SystemFileDef.Ini_System_Name_Key); }
        public string PLCLocalIP { get => IniManager.ReadIni(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Socket_Local_IP_Key); }
        public int PLCLocalPort { get { return GetIniValue(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Socket_Local_Port_Key); } }
        public string PLCCycleLocalIP { get => IniManager.ReadIni(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Socket_Cycle_Local_IP_Key); }
        public int PLCCycleLocalPort { get { return GetIniValue(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Socket_Cycle_Local_Port_Key); } }
        public string PLCRemoteIP { get => IniManager.ReadIni(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Socket_Remote_IP_Key); }
        public int PLCRemotePort { get { return GetIniValue(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Socket_Remote_Port_Key); } }

        public bool PLCDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Rcv_RawData_Key); } }
        public bool PLCDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Snd_RawData_Key); } }
        public bool PLCDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Fail_RawData_Key); } }
        public bool PLCDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniPLCSection, SystemFileDef.Ini_Debug_RawData_Key); } }


        // Printer
        public string PrinterApp { get => IniManager.ReadIni(SystemFileDef.IniPrinterSection, SystemFileDef.Ini_System_Name_Key); }
        public string PrinterRemoteIP { get => IniManager.ReadIni(SystemFileDef.IniPrinterSection, SystemFileDef.Ini_Socket_Remote_IP_Key); }
        public int PrinterRemotePort { get { return GetIniValue(SystemFileDef.IniPrinterSection, SystemFileDef.Ini_Socket_Remote_Port_Key); } }


        // BarCode
        public string BarCodeApp { get => IniManager.ReadIni(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_System_Name_Key); }
        public string BarCodeLocalIP { get => IniManager.ReadIni(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_Socket_Local_IP_Key); }
        public int BarCodeLocalPort { get { return GetIniValue(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_Socket_Local_Port_Key); } }

        public bool BarCodeDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_Rcv_RawData_Key); } }
        public bool BarCodeDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_Snd_RawData_Key); } }
        public bool BarCodeDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_Fail_RawData_Key); } }
        public bool BarCodeDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.IniBarCodeSection, SystemFileDef.Ini_Debug_RawData_Key); } }

        private int GetIniValue(string section, string key)
        {
            return int.Parse(IniManager.ReadIni(section, key));
        }

        private bool GetConfigBoolVaule(string section, string key)
        {
            return bool.Parse(IniManager.ReadIni(section, key));
        }

    }
}
