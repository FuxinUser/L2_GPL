﻿using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No4GRAB_beltCurrentCT
{
    public class L2L25_No4GRAB_beltCurrentCTRepo : BaseRepository<L2L25_No4GRAB_beltCurrentCT>
    {
        protected override string TableName => nameof(L2L25_No4GRAB_beltCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No4GRAB_beltCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
