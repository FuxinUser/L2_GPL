namespace Core.Define
{
    public static class WMSSysDef
    {

        //public const string SysCode = "WM";
        public const string WMS = "WM";
        public class DataCode
        {

            public const string WMSErrorMsgID = "AA00";
            public const string WMSErrorMsgLength = "AA01";
        }

        
        public class RcvMsgCode
        {
            public const string ProDone = "WG01";                // 入料/出料/退料完成訊息
            public const string ProResRequest = "WG03";          // 入料/出料/退料要求回覆
        }
        public class SndMsgCode
        {
            public const string HeartBeatCode = "TT01";                // 心跳Code
            public const string Ack = "DL00";
            public const string CoilScheduleInfo = "GW01";           // 接收排程資訊
            public const string EntryDeliveryTrk = "GW02";           // 產線入口/出口Tracking
            public const string CoilPDO = "GW03";                    // 鋼捲產出資訊
            public const string ProdLineCoilCancel = "GW04";         // 產線入料/出料/退料取消
            public const string ProdLineCoilReq = "GW05";            // 產線入料/出料/退料要求
            public const string InfoScanID = "GW06";                 // 掃描ID通知
        }
        public class Cmd
        {

            public const string No = "0";
            public const string Yes = "1";

            public const string WMSWindingDirectionUp = "0";
            public const string WMSWindingDirectionDown = "1";

            // WPX1 入料/出料/退料完成旗標
            public const string FinishEntryCoil = "1";      //入料完成
            public const string FinishDeliveryCoil = "2";   //出料完成
            public const string FinishRejectCoil = "3";     //退料完成

            // WPX5
            public const string ReqWMSEntryCoil = "1";           //入料
            public const string ReqWMSDeliveryCoil = "2";       //出料
            public const string ReqWMSRejectCoil = "3";         //退料


            // WMS WG02 Entry Exit Def
            public const string Wx02TrkNoUse = "0";
            public const string Wx02TrkEntry = "1";
            public const string Wx02TrkDelivery = "2";
            public const string Wx02TrkEntryDelivery = "3";
        }

        public class SkPos
        {
            public const string ESK01 = "1";
            public const string ETOP = "2";
            public const string DSK01 = "3";
            public const string DSK02 = "4";
            public const string DTOP = "5";


            public const int ESK01No = 1;
            public const int ETOPNo = 2;

            public const int WMSDSk01Def = 3;
            public const int WMSDSk02Def = 4;
            public const int WMSDTopDef = 5;


   


        }
    }
}
