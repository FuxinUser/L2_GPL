using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No1BrushRollCurrentCT
{
    public class L2L25_No1BrushRollCurrentCTRepo : BaseRepository<L2L25_No1BrushRollCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No1BrushRollCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No1BrushRollCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
