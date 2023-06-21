using DBService.Base;

namespace DBService.Repository.WorkSchedule
{
    public class WorkScheduleRepo : BaseRepository<WorkScheduleEntity.TBL_WorkSchedule>
    {
        protected override string TableName => nameof(WorkScheduleEntity.TBL_WorkSchedule);

        protected override string[] PKName => new string[] { nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift), nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate) };

        public WorkScheduleRepo(string connStr) : base(connStr)
        {

        }
    }
}
