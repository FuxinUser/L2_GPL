using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CoilRejectResult
{
    public class L2L25_CoilRejectResultRepo : BaseRepository<L2L25_CoilRejectResult>
    {
        protected override string TableName => nameof(L2L25_CoilRejectResult);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CoilRejectResultRepo(string connStr) : base(connStr)
        {

        }
    }
}
