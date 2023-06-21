using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No1GRAB_beltMotorCurrentCT
{
    public class L2L25_No1GRAB_beltMotorCurrentCTRepo : BaseRepository<L2L25_No1GRAB_beltMotorCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No1GRAB_beltMotorCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No1GRAB_beltMotorCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
