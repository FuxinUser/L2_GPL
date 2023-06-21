using DBService.Base;
using static DBService.Repository.BeltPatternsRecords.BeltPatternsRecordEntity;

namespace DBService.Repository.BeltPatternsRecords
{
    public class BeltPatternsRecordRepo : BaseRepository<TBL_BeltPatterns_Records>
    {
        protected override string TableName => nameof(TBL_BeltPatterns_Records);

        protected override string[] PKName => new string[] { nameof(TBL_BeltPatterns_Records.Coil_ID)};

        public BeltPatternsRecordRepo(string connStr) : base(connStr)
        {

        }
    }
}
