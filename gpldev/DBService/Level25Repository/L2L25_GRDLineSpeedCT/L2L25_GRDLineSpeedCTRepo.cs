using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_GRDLineSpeedCT
{
    public class L2L25_GRDLineSpeedCTRepo : BaseRepository<L2L25_GRDLineSpeedCT>
    {
        protected override string TableName => nameof(L2L25_GRDLineSpeedCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_GRDLineSpeedCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
