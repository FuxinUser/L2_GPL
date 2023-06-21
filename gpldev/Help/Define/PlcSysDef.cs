namespace Core.Define
{
    public static class PlcSysDef
    {

        public class DataCode
        {

        }
        public class SndMsgCode
        {
            public const string L1201Alive = "201";                     // Alive Message
            public const string L1202PDI = "202";
            public const string L1203EntryScnResult = "203";
            public const string L1204Preset = "204";
            public const string L1205Preset = "205";
            public const string L1206BeltInfoMsg = "206";
            public const string L1210DelCoil = "210";
            public const string L1211ModifyCoilID = "211";              // Modify Coil ID

            public const string L1207EntryTakeOverStartCMD = "207";     // Entry Take Over Start CMD
            public const string L1208DelveryToCMD = "208";              // Delivery To CMD      
            public const string L1209DeliveryScnResult = "209";
        }

        public class RcvMsgCode
        {
            public const string L1101Alive = "101";                     // Alive Msg0
            public const string L1102PDOInfo = "102";                   // Alive Msg
            public const string L1104ProcessData = "104";               // Process Data
            public const string L1105TrackMap = "105";                  // TRACKING_MAP
            public const string L1106CoilWeldData = "106";              // COIL WELD DATA
            public const string L1107GrindRecords = "107";              // Grind Records
            public const string L1108DefectData = "108";                // Defect Data
            public const string L1109BeltAccLength = "109";             // Grind Acc 數(里程數)
            public const string L1110UpdateWt = "110";                  // 更新PDO毛重. 並重新計算淨重         
            public const string L1111LineFault = "111";                 // LineFault 
            public const string L1112Utility = "112";                   // 能源實績
            public const string L1113BeltChange = "113";                // 皮帶更換
            public const string L1114CoilMount = "114";                 // Coil Mount
            public const string L1115CoilUnMount = "115";               // Coil UnMount
            public const string L1116CoilWeight = "116";                // Coil Weight
            public const string L1117CoilSplit = "117";                 // Coil Split
            public const string L1118EntryStartCondition = "118";       // Entry Start Condition
            public const string L1119EntryTakeOverStart = "119";        // Entry Take Over Start
            public const string L1120EntryTakeOverEndEntry = "120";     // Entry Take Over End Entry
            public const string L1121DeliveryStartCondition = "121";    // Delivery Start Condition
            public const string L1122DeliveryTakeOverStartDelivery = "122";    // Delivery Take Over Start Delivery
            public const string L1123DeliveryTakeOverEnd = "123";       // Delivery Take Over End
            public const string L1124StripBrakeSignal = "124";          // Strip Brake Signal
            public const string L1125ShareCutData = "125";              // Share Cut Data
            public const string L1126CoilUmountPOR = "126";              // 
            public const string L1127CoilIDModifyReply = "127";          // Coil ID Modify Reply
        }
        public class Cmd
        {
            // L1 Defect Level Def
            public const string DefectGradeA = "A";
            public const string DefectGradeB = "B";
            public const string DefectGradeC = "C";
            public const string DefectGradeD = "D";
            public const string DefectGradeE = "E";

            // L1 Sleeve, Paper Installed
            public const int Install_No = 0;
            public const int Install_Sleeve = 1;
            public const int InstallPaper = 1;

            public const int EntryConditionNG = 0;
            public const int EntryConditionREADY = 1;

            // Use, Not Use Int Def
            public const short Use = 1;
            public const short NotUse = 0;

            // UncoilDirection
            public const int UnCoilUp = 0;      // 上開
            public const int UnCoilDown = 1;      // 下開

            public const int LineStatusStop = 0;
            public const int LineStatusRun = 1;

            public const short CCWType = 0;
            public const short CWType = 1;

            public const short Inside = 0;
            public const short Outside = 1;

            // Cut Mode
            public const short ShareCutWeldCut = 16;
            public const short ShareCutSplitCut = 1;

        }

        public class Pos
        {
            // Preset position
            //public const int ETOP = 3;
            public const int SK1 = 2;
            public const int POR = 1;

            public const int Preset202ETOP = 1;
            public const int Preset202SK1= 2;


            // Trk POS
            public const short L1211TRPos = 5;
            public const short L1211PORPos = 1;
            public const short L1211SK1Pos = 2;

            public const short ESK01 = 2;
            public const short ETOP = 3;
            public const short DSK01 = 6;
            public const short DSK02 = 7;
            public const short DTOP = 8;

            // Preset 204 205 Line Preset
            public const int L1204_205LinePresetPos = 0;
        }
    }
}
