using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Define
{

    /// <summary>
    /// 鋼捲相關邏輯定義
    /// </summary>
    public class CoilDef
    {

        // WMS:WPX1 入料/出料/退料完成旗標
        public const string EntryCoil = "1";      //入料
        public const string DeliveryCoil = "2";   //出料
        public const string RejectCoil = "3";     //退料
        // WMS: WPX5  要求入料/出料/退料 入料/出料/退料
        public const string ReqWMSEntryCoil = "1";           //入料
        public const string ReqWMSDeliveryCoil = "2";       //出料
        public const string ReqWMSRejectCoil = "3";         //退料

        public const int UnitCoilIDMsgCharLen = 20;

        public static class ScheduleStatuts
        {
            // 鋼卷狀態  N-新鋼捲  R-要求入料  F-已入料  I-身分確認成功  P-生產中  D-已產出 C-回退
            public static string NewCoil_Statuts = "N";
            public static string RequestEntryCoil_Statuts = "R";
            public static string EntryCoilDone_Statuts = "F";
            public static string IdentifyOK_Statuts = "I";
            public static string Producing_Statuts = "P";
            public static string ReturnCoil_Statuts = "C";
            public static string ProduceDone_Statuts = "D";
            public static short ScheduleDoneSeqDef = -1;

        }


    }
}
