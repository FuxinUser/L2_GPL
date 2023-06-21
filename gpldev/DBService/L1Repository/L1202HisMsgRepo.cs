using DBService.Base;

namespace DBService.L1Repository
{
    public class L1202HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_202>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_202);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_202.Date), nameof(L2L1MsgDBModel.L2L1_202.Time) };


        public L1202HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
