using DBService.Base;
using System;

namespace DBService.Repository.LookupTblFlattener
{
    public class LkUpTableFlattenerRepo : BaseRepository<LkUpTableFlattenerEntity.TBL_LookupTable_Flattener>
    {
        protected override string TableName => nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener);

        protected override string[] PKName => throw new NotImplementedException();

        public LkUpTableFlattenerRepo(string connStr) : base(connStr)
        {

        }

      

    }
}
