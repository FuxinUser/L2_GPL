using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_LineSpeedCT
{
    public class L2L25_LineSpeedCTRepo : BaseRepository<L2L25_LineSpeedCT>
    {
        protected override string TableName => nameof(L2L25_LineSpeedCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_LineSpeedCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
