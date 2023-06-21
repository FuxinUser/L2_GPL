namespace Core.Define
{
    public class DeviceDef
    {
        public const string EntryShear = "1";
        public const string ExitShear = "2";

        public const float UncoilerTensionMax = 1.0f;
        public const float UncoilerTensionMin = 1.0f;

        public const float RecoilerTensionMax = 1.0f;
        public const float RecoilerTensionMin = 1.0f;

        // CPL: 600mm GPL : 600mm
        public const int HeadHolePosition = 600;
        public const int TailPunchHolePosition = 600;

        // 導帶密度
        public const float LeaderDensity = 7930;    // 編號SUS304 單位 kg/m^3  

        // 下收
        public const string WindingDirectionDown = "L";
        // 上收
        public const string WindingDirectionUp = "U";

        // BarCode機
        public const string BCSScanCoil = "BS01";           // BarCode機 傳送鋼捲身分識別要求
        public const string CompareScanResultId = "SB01";     
        public const string ScanResultLength = "37";        // BCSScnResult Model最大長度

        // BarCode機 位置定義 :  pos 1:ESK01 2:ETOP 3:DSK01 4:DSK02 5:DTOP
        public const string BCSDefPOS_ESK01 = "1";
        public const string BCSDefPOS_ETOP = "2";
        public const string BCSDefPOS_DSK01 = "3";
        public const string BCSDefPOS_DSK02 = "4";
        public const string BCSDefPOS_DTOP = "5";

        public const string WMS = "WM";
        public const string MMS = "MM";
    }
}
