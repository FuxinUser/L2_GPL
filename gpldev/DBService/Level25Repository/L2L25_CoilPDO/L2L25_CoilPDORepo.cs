using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CoilPDO
{
    public class L2L25_CoilPDORepo : BaseRepository<L2L25_CoilPDO>
    {
        protected override string TableName => nameof(L2L25_CoilPDO);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CoilPDORepo(string connStr) : base(connStr)
        {

        }
    }
}
