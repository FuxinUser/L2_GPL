﻿using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_No1GRAB_beltSpeedCT
{
    public class L2L25_No1GRAB_beltSpeedCTRepo : BaseRepository<L2L25_No1GRAB_beltSpeedCT>
    {
        protected override string TableName => nameof(L2L25_No1GRAB_beltSpeedCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_No1GRAB_beltSpeedCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
