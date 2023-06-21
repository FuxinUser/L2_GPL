namespace Core.Define
{
    public static class MMSSysDef
    {
        public const string SysName = "MMS";
        public const string SysCode = "MM";
        public const string MMS = "MM";
        public const byte EndTag = 0x0a;
        public const int EndTagLength = 1;
        public const int CoilIDLength = 20;
        public const int MsgLenLength = 4;

        public class DataCode
        {

            public const string Accept = "A";
            public const string NotAccept = "B";
            public const string DataMsg = "D";
            public const string HeartMsg = "C";

        }

        public class RcvMsgCode
        {
            public const string HeartBeatCode = "999999";                // 心跳Code
            

            public const string CoilScheduleCode = "MMG101";             // 鋼捲生產時機
            public const string CoilPDICode = "MMG102";                  // PDI
            public const string ReqDeletePlanNoCode = "MMG103";          // 作業計畫刪除請求
            public const string CoilDummyCode = "MMG104";                // 鋼捲過度捲
            public const string CoilDummyResCode = "MMG105";                // 鋼捲過度捲回應
            public const string ReqProResultCode = "MMG106";             // 生产实绩请求
            public const string CoilRejectResultCode = "MMG107";         // 鋼捲刪除/回退實績應答
            public const string ResForNoCoilCode = "MMG108";             // 無鋼捲生產命令回覆
            public const string ResForNoCoilPDICode = "MMG109";          // 無鋼捲PDI回覆
            public const string SleeveValueSynCode = "MMG115";          // 套筒静态数据同步
            public const string PaperValueSyncCode = "MMG116";          // 垫纸静态数据同步
            public const string ResRcvPDO = "MMG110";                   // PDO是否接收回覆   added 2023.04.21
        }
        public class SndMsgCode
        {        
        


            public const string ReqForCoilSchedCode = "G1MM01";          // 钢卷生产命令请求
            public const string ReqForPDICode = "G1MM02";                // 鋼捲PDI請求
            public const string ResForCoilSchedCode = "G1MM03";          // 钢卷生产命令应答
            public const string ResForCoilPDICode = "G1MM04";            // 钢卷PDI应答
            public const string CoilRejectDataCode = "G1MM05";           // 鋼捲回退實績
            public const string CoilLoadedSkidCode = "G1MM06";           // 鋼捲上安座通知
            public const string CoilPDOCode = "G1MM07";                  // PDI資料
            public const string EqDownResultCode = "G1MM08";              // 停復機紀錄
            public const string EnergyConsumptionInfoCode = "G1MM09";    // 能源消耗訊息    
            public const string CoilScheduleChangedCode = "G1MM10";      // 鋼捲生產時機         
            public const string ReqDummyCoilCode = "G1MM15";             // 過渡捲清單請求
            public const string DelDummyCoilCode = "G1MM16";             // 過渡捲刪除
            public const string CoilScheduleDeleteCode = "G1MM18";       // 鋼捲生產命令刪除
            public const string ResDeletePlanNoResultCode = "G1MM25";    // 回覆整計畫刪除電文
        }
        public class Cmd
        {
            public const string SyncValueInsert = "I";                 // 同步-新增
            public const string SyncValueUpdate = "U";                 // 同步-修改
            public const string SyncValueDelete = "D";                 // 同步-刪除

            public const string ProOK = "0";
            public const string ProNG = "1";

            public const string NotUse = "0";
            public const string Use = "1";

            public const string DefectClassL = "L";     // A,B
            public const string DefectClassM = "M";     // C
            public const string DefectClassH = "H";     // D
            public const string DefectClassS = "S";     // E

            public const string UnCoilUpStr = "U";      // 上開
            public const string UnCoilDownStr = "L";      // 下開

            public const string InSide = "I";
            public const string OutSide = "O";

            // GR BeltPattern Pos
            public const string PassSectionHead = "H";
            public const string PassSectionCenter = "M";
            public const string PassSectionTail = "T";

            // 鋼捲生產命令判定
            public const string InsertAllCoilSchedule = "0";                // 鋼捲生產命令, 插入所有入口鋼捲至CoilScheduleTable)

            // L1 Defect position
            public const int DefectDriveSide = 1;
            public const int DefectCenter = 2;
            public const int DefectWorkSide = 3;

            // MMS Defect Position Width
            public const string DefectPosWidthDriveSide = "D";
            public const string DefectPosWidthCenter = "C";
            public const string DefectPosWidthWorkSide = "W";

            // 
            public const string DelScheduleByPlanNo = "MMS PLAN DELETE";
            public const string DelSchedulePlanNoReject = "部份計畫鋼捲已要求入料或已上鋼捲";

            public const string NoCoilSchedule = "0";

        }

    }
}
