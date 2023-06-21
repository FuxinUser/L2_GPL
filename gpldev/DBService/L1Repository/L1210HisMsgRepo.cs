using DBService.Base;

namespace DBService.L1Repository
{
    public class L1210HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_210>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_210);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_210.Date), nameof(L2L1MsgDBModel.L2L1_210.Time) };


        public L1210HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
