namespace Core.Define
{
    public static class EventDef
    {
        public enum TCPCMDSET
        {
            CLIENT_TRY_CONNECTION
        }

        public enum CMDSET
        {
            ACK_TIMEOUT,
            SND_HEARTBEAT,
            CHECK_ACK,
            DETECT_L1_ALIVE,
            DEBUG_LOG_CLOSE,
            DEBUG_LOG_OPEN,
            HEART_BEAT_OPEN,
            HEART_BEAT_CLOSE,
            ACK_OPEN,
            ACK_CLOSE,
            TRKSCAN,
            ENTRY_CONDITION_CHECK,
            DETECT_SND_QUEU,
            L25Alive,
        }

      
        public enum LOGSW
        {
            DEBUG_LOG_CLOSE,
            DEBUG_LOG_OPEN,
        }

        // Server Event 
        public const string CoilRejectFail = "失敗";
        public const string CoilRejectSuccess = "成功";
        public const string DummyCoilRejectFail = "三級過度鋼捲生產刪除失敗";
        public const string DummyCoilRejectSuccess = "三級過度鋼捲生產刪除成功";
        public const string ReceiveMMSPDI = "接收三級PDI資料";
        public const string ReceiveWMSCancelMsg = "接收WMS回復";
        public const string ReceiveMMSCoilSchedule = "收到鋼卷生產命令";
        public const string MMSNoPDI = "MMS通知無鋼捲PDI回應";
        public const string MMSNoCoil = "MMS通知無鋼捲生產回應";
        public const string L1DisConn = "L1斷線";
        public const string ProSchedFail = "Process coil schedule fail";
        public const string DeliveryCoilDone = "出料完成";
        public const string EntryCoilDone = "入料完成";
        public const string RejectCoilDone = "退料完成";
        public const string SndPDO = "Server上傳PDO";
        public const string SndPDOtoWMS = "PDO Send To WMS";
        public const string processOk = "0";
        public const string processError = "1";
        public const string NoSchedule = "No shcedule ID";
        public const string RcvPdoOk = "1";

        public const string TRUE = "1";
        public const string FALSE = "0";

        public const string L1EventPORCoilIn = "POR CoilIn";
        // For Test
        public const string L1ResentHisPresetMsg = "Resent His Preset Msg";


    }
}
