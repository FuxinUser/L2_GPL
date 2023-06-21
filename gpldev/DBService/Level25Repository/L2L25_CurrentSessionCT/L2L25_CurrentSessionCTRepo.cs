using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CurrentSessionCT
{
    public class L2L25_CurrentSessionCTRepo : BaseRepository<L2L25_CurrentSessionCT>
    {
        protected override string TableName => nameof(L2L25_CurrentSessionCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CurrentSessionCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
