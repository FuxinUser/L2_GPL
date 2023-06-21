using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No2GRAbrasiveBeltMotorCurrentCT
{
    public class L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo : BaseRepository<L2L25_No2GRAbrasiveBeltMotorCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No2GRAbrasiveBeltMotorCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
