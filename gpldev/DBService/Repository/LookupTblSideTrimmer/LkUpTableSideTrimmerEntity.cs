using DBService.Base;

namespace DBService.Repository.LookupTblSideTrimmer
{
    public class LkUpTableSideTrimmerEntity
    {
        public class TBL_LookupTable_SideTrimmer : BaseRepositoryModel
        {
            public string SteelGrade { get; set; } = "";
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            public float Coil_Thickness_Max { get; set; } = 0.0f;
            public float KnifeGap { get; set; } = 0.0f;
            public float KnifeLap { get; set; } = 0.0f;
        }

    }
}
