using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_L1L2DisConnection
{
    public class L2L25_L1L2DisConnectionRepo : BaseRepository<L2L25_L1L2DisConnection>
    {
        protected override string TableName => nameof(L2L25_L1L2DisConnection);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_L1L2DisConnectionRepo(string connStr) : base(connStr)
        {

        }
    }
}
