using System.IO;
using System.Text;

namespace Core.Define
{
    /// <summary>
    /// 系統檔案相關設置定義
    /// </summary>
    public class SystemFileDef
    {
        #region -- Ini --

        public static string iniPath = Path.Combine(IniPath(), "SystemConfig.ini");

        public static string IniPath()
        {
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var pathlist = exePath.Split('\\');
            var pathStr = new StringBuilder();
            for (int i = 0; i < pathlist.Length - 4; i++)
                pathStr.Append(pathlist[i] + "\\");

            return pathStr.ToString();
        }

        public static string IniDBSection = "MsSql-Serve";
        public static string IniDBKey = "DbConn";
        public static string IniHISDBKey = "His-DbConn";
        public static string Ini2_5DBKey = "Level2.5-DbConn";

        public static string IniMMSSection = "MMS-App";
        public static string IniWMSSection = "WMS-App";
        public static string IniPLCSection = "PLC-App";
        public static string IniPrinterSection = "Printer-App";
        public static string IniBarCodeSection = "BarCode-App";

        public static string Ini_System_Name_Key = "System-Name";
        public static string Ini_Socket_Local_IP_Key = "Socket-Local-IP";       // Socket Loacal IP
        public static string Ini_Socket_Local_Port_Key = "Socket-Local-Port";     // Socket Loacal Port
        public static string Ini_Socket_Cycle_Local_IP_Key = "Socket-Cycle-Local-IP";       // Socket Cycle Loacal IP
        public static string Ini_Socket_Cycle_Local_Port_Key = "Socket-Cycle-Local-Port";     // Socket Cycle Loacal Port
        public static string Ini_Socket_Remote_IP_Key = "Socket-Remote-IP";       // Socket Loacal IP
        public static string Ini_Socket_Remote_Port_Key = "Socket-Remote-Port";     // Socket Loacal Port

        public static string Ini_Rcv_RawData_Key = "Rcv-RawData";
        public static string Ini_Snd_RawData_Key = "Snd-RawData";
        public static string Ini_Fail_RawData_Key = "Fail-RawData";
        public static string Ini_Debug_RawData_Key = "Debug-RawData";

        #endregion
    }
}
