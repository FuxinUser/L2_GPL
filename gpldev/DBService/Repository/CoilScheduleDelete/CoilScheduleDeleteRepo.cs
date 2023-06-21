using DBService.Base;
using static DBService.Repository.CoilScheduleDelete.CoilScheduleDeleteEntity;

namespace DBService.Repository.CoilScheduleDelete
{
    public class CoilScheduleDeleteRepo : BaseRepository<TBL_CoilScheduleDelete>
    {
        protected override string TableName => nameof(TBL_CoilScheduleDelete);

        protected override string[] PKName => new string[] { nameof(TBL_CoilScheduleDelete.CoilNo) };

        public CoilScheduleDeleteRepo(string connStr) : base(connStr)
        {

        }
    }
}
