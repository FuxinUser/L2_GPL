using DBService.Base;
using System;

namespace DBService.Repository.LookupTbl
{
    public class LkUpTableLineTensionEntity
    {
        public class TBL_LookupTable_LineTension : BaseRepositoryModel
        {
            public string Material_Grade { get; set; } = "";
            public int Coil_Width_Min { get; set; } = 0;
            public int Coil_Width_Max { get; set; } = 0;
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            public float Coil_Thickness_Max{ get; set; } = 0.0f;
            public int LineSpeed { get; set; } = 0;
            public float TRTension { get; set; } = 0.0f;
            public string ProcessType { get; set; } = "";
            public override DateTime UpdateTime { get; set; }          
        }
    }
}
