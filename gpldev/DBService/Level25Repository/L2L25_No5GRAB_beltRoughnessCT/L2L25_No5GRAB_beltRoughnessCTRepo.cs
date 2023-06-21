using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No5GRAB_beltRoughnessCT
{
    public class L2L25_No5GRAB_beltRoughnessCTRepo : BaseRepository<L2L25_No5GRAB_beltRoughnessCT>
    {
        protected override string TableName => nameof(L2L25_No5GRAB_beltRoughnessCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No5GRAB_beltRoughnessCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
