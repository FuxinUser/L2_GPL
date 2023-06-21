using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_LineRunDirectionCT
{
    public class L2L25_LineRunDirectionCTRepo : BaseRepository<L2L25_LineRunDirectionCT>
    {
        protected override string TableName => nameof(L2L25_LineRunDirectionCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_LineRunDirectionCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
