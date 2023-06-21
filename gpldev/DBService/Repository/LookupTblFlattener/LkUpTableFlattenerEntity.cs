using DBService.Base;

namespace DBService.Repository.LookupTblFlattener
{
    public class LkUpTableFlattenerEntity
    {
        public class TBL_LookupTable_Flattener : BaseRepositoryModel
        {
            public string Material_Grade { get; set; } = string.Empty;
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            public float Coil_Thickness_Max { get; set; } = 0.0f;
            public int Strip_Yield_Stress_Min { get; set; } = 0;
            public int Strip_Yield_Stress_Max { get; set; } = 0;
            public float Intermesh_Num1 { get; set; } = 0.0f;
            public float Intermesh_Num2 { get; set; } = 0.0f;
        }

    }
}
