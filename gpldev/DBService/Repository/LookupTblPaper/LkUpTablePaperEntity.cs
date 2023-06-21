using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LookupTblPaper
{
    public class LkUpTablePaperEntity
    {
        public class TBL_LookupTable_Paper : BaseRepositoryModel
        {
            public string Deal_Flag { get; set; } = string.Empty;
            [PrimaryKey]
            public string Paper_Code { get; set; } = string.Empty;
            public int Paper_Base_Weight { get; set; } = 0;
            public int Paper_Width { get; set; } = 0;
            public int Paper_Thick { get; set; } = 0;
            public int Paper_Min_Thick { get; set; } = 0;
            public int Paper_Max_Thick { get; set; } = 0;
        }
    }
}
