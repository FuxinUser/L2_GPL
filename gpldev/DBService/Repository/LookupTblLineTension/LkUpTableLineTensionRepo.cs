using DBService.Base;
using DBService.Repository.LookupTbl;

namespace DBService.Repository.LookupTblLineTension
{
    public class LkUpTableLineTensionRepo : BaseRepository<LkUpTableLineTensionEntity.TBL_LookupTable_LineTension>
    {
        protected override string TableName => nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension);

        protected override string[] PKName => throw new System.NotImplementedException();


        public LkUpTableLineTensionRepo(string connStr) : base(connStr)
        {

        }
    }
}
