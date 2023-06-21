using DBService.Base;

namespace DBService.L1Repository
{
    public class L1211HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_211>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_211);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_211.Date), nameof(L2L1MsgDBModel.L2L1_211.Time) };


        public L1211HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
