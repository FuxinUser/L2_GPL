using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_LineTensionCT
{
    public class L2L25_LineTensionCTRepo : BaseRepository<L2L25_LineTensionCT>
    {
        protected override string TableName => nameof(L2L25_LineTensionCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_LineTensionCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
