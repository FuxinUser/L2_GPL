using DBService.Base;
using DBService.Repository.CutReocrd;

namespace DBService.Repository.CoilCutReocrd
{
    public class CoilCutRecordRepo : BaseRepository<CoilCutRecordEntity.TBL_Coil_CutRecord>
    {
        protected override string TableName => nameof(CoilCutRecordEntity.TBL_Coil_CutRecord);

        protected override string[] PKName => new string[] { nameof(CoilCutRecordEntity.TBL_Coil_CutRecord.CoilID) };

        public CoilCutRecordRepo(string connStr) : base(connStr)
        {

        }
    }
}
