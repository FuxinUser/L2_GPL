using DBService.Base;

namespace DBService.Repository.LookupTblPaper
{
    public class LkUpTablePaperRepo : BaseRepository<LkUpTablePaperEntity.TBL_LookupTable_Paper>
    {
        protected override string TableName => nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper);

        protected override string[] PKName => new string[] { nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code) };

        public LkUpTablePaperRepo(string connStr) : base(connStr)
        {

        }
    }
}
