using DBService.Base;
using static DBService.Repository.ScheduleDeleteRecordTempEntity;

namespace DBService.Repository.ScheduleDelete_CoilReject_Record_Temp
{
    public class ScheduleDeleteRecordTempRepo : BaseRepository<L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp>
    {
        protected override string TableName => nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp);

        protected override string[] PKName => new string[] { nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp.CoilNo) };

        public ScheduleDeleteRecordTempRepo(string connStr) : base(connStr)
        {

        }
    }
}
