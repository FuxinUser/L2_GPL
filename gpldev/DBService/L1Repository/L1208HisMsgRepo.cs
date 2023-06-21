using DBService.Base;

namespace DBService.L1Repository
{
    public class L1208HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_208>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_208);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_208.Date), nameof(L2L1MsgDBModel.L2L1_208.Time) };


        public L1208HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
