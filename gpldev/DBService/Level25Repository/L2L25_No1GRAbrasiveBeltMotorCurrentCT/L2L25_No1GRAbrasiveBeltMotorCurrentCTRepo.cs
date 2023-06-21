using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No1GRAbrasiveBeltMotorCurrentCT
{
    public class L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo : BaseRepository<L2L25_No1GRAbrasiveBeltMotorCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No1GRAbrasiveBeltMotorCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
