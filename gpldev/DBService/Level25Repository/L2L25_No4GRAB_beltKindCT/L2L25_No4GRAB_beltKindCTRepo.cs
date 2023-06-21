using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No4GRAB_beltKindCT
{
    public class L2L25_No4GRAB_beltKindCTRepo : BaseRepository<L2L25_No4GRAB_beltKindCT>
    {
        protected override string TableName => nameof(L2L25_No4GRAB_beltKindCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No4GRAB_beltKindCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
