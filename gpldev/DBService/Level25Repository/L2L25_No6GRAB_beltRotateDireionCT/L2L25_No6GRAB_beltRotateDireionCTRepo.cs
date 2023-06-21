using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No6GRAB_beltRotateDirectionCT
{
    public class L2L25_No6GRAB_beltRotateDirectionCTRepo : BaseRepository<L2L25_No6GRAB_beltRotateDirectionCT>
    {
        protected override string TableName => nameof(L2L25_No6GRAB_beltRotateDirectionCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No6GRAB_beltRotateDirectionCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
