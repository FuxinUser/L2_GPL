using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_ENGC
{
    public class L2L25_ENGCRepo : BaseRepository<L2L25_ENGC>
    {
        protected override string TableName => nameof(L2L25_ENGC);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_ENGCRepo(string connStr) : base(connStr)
        {

        }
    }
}
