using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.DefectData
{
    public class CoilDefectDataEntity
    {

        [Serializable]
        public class L3L2_TBL_DefectData : BaseRepositoryModel
        {
            [PrimaryKey]
            public string CoilID { get; set; } = string.Empty;

            public int PassNumber { get; set; }

            public int CurrentSession { get; set; }
            
            public int DefectGroupNo { get; set; }

            [PrimaryKey]
            public int DefectCode { get; set; }
            public string DefectOrigin { get; set; } = string.Empty;
            public string DefectSide { get; set; } = string.Empty;

            [PrimaryKey]
            public string DefectPositionWidthDirection { get; set; }
            public float DefectPosLengthStartDirection { get; set; } 
            public float DefectPoLengthEndDirection { get; set; }
            
            [PrimaryKey]
            public string DefectLevel { get; set; } = string.Empty;
            public string DefectPercent { get; set; } = string.Empty;
            public string PDOFlag { get; set; } = string.Empty;
            public override DateTime CreateTime { get; set; }

         
        }
    }


}
