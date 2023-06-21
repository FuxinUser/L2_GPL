using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.L1Repository
{
    public class L1205HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_205>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_205);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_205.Date), nameof(L2L1MsgDBModel.L2L1_205.Time) };


        public L1205HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
