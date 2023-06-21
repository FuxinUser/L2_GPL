using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CurrentPassNumberCT
{
    public class L2L25_CurrentPassNumberCTRepo : BaseRepository<L2L25_CurrentPassNumberCT>
    {
        protected override string TableName => nameof(L2L25_CurrentPassNumberCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CurrentPassNumberCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
