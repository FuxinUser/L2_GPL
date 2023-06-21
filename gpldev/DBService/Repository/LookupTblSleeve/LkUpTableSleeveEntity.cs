using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LookupTblSleeve
{
    public class LkUpTableSleeveEntity
    {
        public class TBL_LookupTable_Sleeve : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Sleeve_Code { get; set; }
            public string Sleeve_Material { get; set; }
            public int Sleeve_Width { get; set; }
            public int Sleeve_Thick { get; set; }
            public float Sleeve_Weight { get; set; }
            public int Out_Mat_Inner_Dia { get; set; }
            public float Out_Mat_Width_Min { get; set; }
            public float Out_Mat_Width_Max { get; set; }


        }
    }
}
