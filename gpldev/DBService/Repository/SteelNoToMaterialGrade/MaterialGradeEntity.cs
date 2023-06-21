using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.SteelNoToMaterialGrade
{
    public class MaterialGradeEntity
    {
        public class TBL_SteelNoToMaterialGrade : BaseRepositoryModel
        {
            [PrimaryKey]
            public string St_No { get; set; }
            public string Ys { get; set; }
            public string Ts { get; set; }
            public string Material_Grade { get; set; }
            public DateTime CreateTime { get; set; }
        }
    }
}
