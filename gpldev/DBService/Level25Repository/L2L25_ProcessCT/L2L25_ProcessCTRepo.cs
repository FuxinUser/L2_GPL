using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_ProcessCT
{
    public class L2L25_ProcessCTRepo : BaseRepository<L2L25_ProcessCT>
    {
        protected override string TableName => nameof(L2L25_ProcessCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_ProcessCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
