using DBService.Base;

namespace DBService.L1Repository
{
    public class L1206HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_206>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_206);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_206.Date), nameof(L2L1MsgDBModel.L2L1_206.Time) };


        public L1206HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
