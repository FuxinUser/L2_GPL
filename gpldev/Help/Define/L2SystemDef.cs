
namespace Core.Define
{
    public static class L2SystemDef
    {
        public enum SKPOS
        {
            Entry_Car, EntryTOP, Entry_SK01, POR, TR, Delivery_SK01, Delivery_SK02, DeliveryTop, Delivery_Car
        }


        public const string TRUE = "1";
        public const string FALSE = "0";

        //public const string SourceServer = "GPL Server";
        public const string SourceSystem = "G1";

        // 機主號
        public const string SystemIDCode = "G1";

        public const string GPLGroup = "GPL";

        public const string GPLSysNumber = "GP";

        // Parameter Def
        //public const string AutoInputFlagPara = "AutoCoilFeed";
        //public const string L1AliveLastTimePara = "L1_ALIVE_LastTime";

        public const string AutoInputOn = "1";
        public const string AutoInputOff = "0";

        public const string L2 = "G1";


        // Use , Not Use Str Def
        public const string UseStr = "Y";
        public const string NotUseStr = "N";


        public const string CheckCoilNo = "1";
        public const string UnCheckedCoilNo = "0";


        // Schedule Update Source
        public const string UpdateSourceMMS = "0";
        public const string UpdateSourceL2 = "1";

        public const string NotUse = "0";
        public const string Use = "1";
    }
}
