using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No1GRAbrasiveBeltSpeedCT
{
    public class L2L25_No1GRAbrasiveBeltSpeedCTRepo : BaseRepository<L2L25_No1GRAbrasiveBeltSpeedCT>
    {
        protected override string TableName => nameof(L2L25_No1GRAbrasiveBeltSpeedCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No1GRAbrasiveBeltSpeedCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
