using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CoilMap
{
    public class L2L25_CoilMapRepo : BaseRepository<L2L25_CoilMap>
    {
        protected override string TableName => nameof(L2L25_CoilMap);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CoilMapRepo(string connStr) : base(connStr)
        {

        }
    }
}
