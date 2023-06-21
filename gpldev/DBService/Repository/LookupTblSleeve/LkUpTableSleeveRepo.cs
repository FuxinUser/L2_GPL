using DBService.Base;

namespace DBService.Repository.LookupTblSleeve
{
    public class LkUpTableSleeveRepo : BaseRepository<LkUpTableSleeveEntity.TBL_LookupTable_Sleeve>
    {
        protected override string TableName => nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve);

        protected override string[] PKName => new string[] { nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code) };

        public LkUpTableSleeveRepo(string connStr) : base(connStr)
        {

        }
    }
}
