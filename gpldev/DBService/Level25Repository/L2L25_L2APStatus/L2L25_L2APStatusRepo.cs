using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_L2APStatus
{
    public class L2L25_L2APStatusRepo : BaseRepository<L2L25_L2APStatus>
    {
        protected override string TableName => nameof(L2L25_L2APStatus);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_L2APStatusRepo(string connStr) : base(connStr)
        {

        }
    }
}
