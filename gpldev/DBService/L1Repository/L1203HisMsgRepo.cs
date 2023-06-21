using DBService.Base;

namespace DBService.L1Repository
{
    public class L1203HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_203>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_203);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_203.Date), nameof(L2L1MsgDBModel.L2L1_203.Time) };


        public L1203HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
