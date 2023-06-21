using DBService.Base;

namespace DBService.L1Repository
{
    public class L1207HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_207>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_207);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_207.Date), nameof(L2L1MsgDBModel.L2L1_207.Time) };


        public L1207HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
