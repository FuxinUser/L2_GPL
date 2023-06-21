using DBService.Base;

namespace DBService.L1Repository
{
    public class L1209HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_209>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_209);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_209.Date), nameof(L2L1MsgDBModel.L2L1_209.Time) };


        public L1209HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
