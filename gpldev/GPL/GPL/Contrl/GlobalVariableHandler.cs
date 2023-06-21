using Akka.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GPLManager
{
    /// <summary>
    /// 自動入料狀態
    /// </summary>
    public enum AutoFeedStatus
    {
        /// <summary>
        /// 手動
        /// </summary>
        Manual,
        /// <summary>
        /// 自動
        /// </summary>
        Auto
    }
    /// <summary>
    /// 排程調整
    /// </summary>
    public enum Seq_No_Change
    {
        Top,
        Up,
        Down,
        Bottom
    }
    /// <summary>
    /// 刪除/退料代碼群組號
    /// </summary>
    public enum Delete_RejectCode
    {
        /// <summary>
        /// 刪除
        /// </summary>
        Delete = 0,
        /// <summary>
        /// 退料
        /// </summary>
        Reject = 1
    }
    public class GlobalVariableHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly GlobalVariableHandler INSTANCE = new GlobalVariableHandler();
        }
        public DataTable dtEventPush = new DataTable();

        public DataRow dr_EventPush = null;

        public static GlobalVariableHandler Instance { get { return SingletonHolder.INSTANCE; } }
        public string strConn_GPL = ConfigurationManager.ConnectionStrings["FUXIN_GPL"].ConnectionString;
        public string strConn_GPL_HISTORY = ConfigurationManager.ConnectionStrings["FUXIN_GPL_HISTORY"].ConnectionString;
        public string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public string getTime = "";
        public string time_CHAR14 = DateTime.Now.ToString("yyyyMMddHHmmss");
        public string proLine = ConfigurationManager.AppSettings["ProLineName"];

        public static string Printer_IP = ConfigurationManager.AppSettings["Printer_IP"];


        public static int Printer_Port = 0;
        bool bolPrinter_Port = int.TryParse(ConfigurationManager.AppSettings["Printer_Port"], out Printer_Port);

        //public static int Printer_Port = int.Parse(ConfigurationManager.AppSettings["Printer_Port"]);

        public IPAddress getIpAdderss = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);


    }

}
