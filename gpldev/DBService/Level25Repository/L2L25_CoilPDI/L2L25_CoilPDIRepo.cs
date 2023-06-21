using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CoilPDI
{
    public class L2L25_CoilPDIRepo : BaseRepository<L2L25_CoilPDI>
    {
        protected override string TableName => nameof(L2L25_CoilPDI);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CoilPDIRepo(string connStr) : base(connStr)
        {

        }
    }
}
