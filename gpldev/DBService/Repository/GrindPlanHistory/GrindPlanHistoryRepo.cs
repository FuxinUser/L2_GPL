using DBService.Base;

namespace DBService.Repository.GrindPlanHistory
{
    public class GrindPlanHistoryRepo : BaseRepository<GrindPlanHistoryEntity.TBL_GrindPlanHistory>
    {
        protected override string TableName => nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory);

        protected override string[] PKName => throw new System.NotImplementedException();

        public GrindPlanHistoryRepo(string connStr) : base(connStr)
        {

        }

    }
}
