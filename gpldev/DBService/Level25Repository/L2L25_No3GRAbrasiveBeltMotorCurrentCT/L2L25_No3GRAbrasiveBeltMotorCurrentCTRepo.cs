using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No3GRAbrasiveBeltMotorCurrentCT
{
    public class L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo : BaseRepository<L2L25_No3GRAbrasiveBeltMotorCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No3GRAbrasiveBeltMotorCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
