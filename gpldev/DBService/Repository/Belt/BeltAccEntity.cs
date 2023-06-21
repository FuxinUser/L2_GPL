using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.Belt
{
    public class BeltAccEntity
    {
        [Serializable]
        public class TBL_Belts : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Belt_No { get; set; } = string.Empty;  // 0 Not Found
            public string Belt_Type { get; set; } = string.Empty;
            public short Belt_Particle_Number { get; set; } = 0;
            public string Suppler_Code { get; set; } = string.Empty;
            public string Material_Code { get; set; } = string.Empty;
            public float Total_Grind_Length_Belt { get; set; } = 0;
            public float Total_Grind_Length_Strip { get; set; } = 0;
            public short Mount_GR_No { get; set; } = 0;
        }

    }
}
