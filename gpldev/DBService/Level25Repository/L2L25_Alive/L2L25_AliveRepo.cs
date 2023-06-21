using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_Alive
{
    public class L2L25_AliveRepo : BaseRepository<L2L25_Alive>
    {
        protected override string TableName => nameof(L2L25_Alive);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_AliveRepo(string connStr) : base(connStr)
        {

        }
    }
}
