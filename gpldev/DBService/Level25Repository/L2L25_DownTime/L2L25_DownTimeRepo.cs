using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_DownTime
{
    public class L2L25_DownTimeRepo : BaseRepository<L2L25_DownTime>
    {
        protected override string TableName => nameof(L2L25_DownTime);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_DownTimeRepo(string connStr) : base(connStr)
        {

        }
    }
}
