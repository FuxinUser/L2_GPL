using DBService.Base;

namespace DBService.Repository.UmountRecord
{
    public class UmountRecordRepo : BaseRepository<UmountRecordEntity.TBL_UnmountRecord>
    {

        public UmountRecordRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(UmountRecordEntity.TBL_UnmountRecord);

        protected override string[] PKName => new string[] { nameof(UmountRecordEntity.TBL_UnmountRecord.CoilID)};
    }
}
