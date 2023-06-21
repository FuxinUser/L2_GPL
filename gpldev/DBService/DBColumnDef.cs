using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.PDI.PDIEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.SystemSetting.SystemSettingEntity;

namespace DBService
{
    public class DBColumnDef
    {
 
        // L3L2_Production_Schedule
        public const string CoilSchedTbl = nameof(TBL_Production_Schedule);
        public const string CoilSchedCoilID = nameof(TBL_Production_Schedule.Coil_ID);
        public const string CoilSchedSeqNo = nameof(TBL_Production_Schedule.Seq_No);
        public const string CoilSchedUpdateSource = nameof(TBL_Production_Schedule.Update_Source);
        public const string CoilSchedUpdateTime = nameof(TBL_Production_Schedule.UpdateTime);
        public const string CoilSchedScheduleStatus = nameof(TBL_Production_Schedule.Schedule_Status);

        // L3L2_PDI
        public const string PDITbl = nameof(TBL_PDI);
        public const string PDIEntryMatNo = nameof(TBL_PDI.In_Coil_ID);
        public const string PDIIsDummyCoil = nameof(TBL_PDI.Is_Dummy_Coil);
        public const string PDIPlanNo = nameof(TBL_PDI.Plan_No);
        public const string Is_Info_WMS_Down = nameof(TBL_PDI.Is_Info_WMS_Down);

        // L3L2_PDO
        public const string PDOTbl = nameof(TBL_PDO);
        public const string PDOOutMatNo = nameof(TBL_PDO.Out_Coil_ID);

        // SystemSetting
        public const string SysParameter = nameof(TBL_SystemSetting.Parameter);


    }
}
