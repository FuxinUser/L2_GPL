using DBService.Base;
using static DBService.Repository.LookupTblGrindLevel.LkUpTableGrindLevelEntity;

namespace DBService.Repository.LookupTblGrindLevel
{
    public class LkUpTableGrindLevelRepo : BaseRepository<TBL_LookupTableGrindLevel>
    {
        protected override string TableName => nameof(TBL_LookupTableGrindLevel);

        protected override string[] PKName => new string[] {  nameof(TBL_LookupTableGrindLevel.Code)
                                                             ,nameof(TBL_LookupTableGrindLevel.InnerGrade)
                                                             ,nameof(TBL_LookupTableGrindLevel.OuterGrade)};

        public LkUpTableGrindLevelRepo(string connStr) : base(connStr)
        {

        }


    }
}
