using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No5GRAbrasiveBeltMotorCurrentCT
{
    public class L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo : BaseRepository<L2L25_No5GRAbrasiveBeltMotorCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No5GRAbrasiveBeltMotorCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
