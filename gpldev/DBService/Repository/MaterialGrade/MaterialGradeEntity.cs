using DBService.Base;
using System;

namespace DBService.Repository.MaterialGrade
{
    public class MaterialGradeEntity
    {
		public class TBL_SteelNoToMaterialGrade : BaseRepositoryModel
		{
			public string St_No { get; set; }
			public string Material_Grade { get; set; }
			public string YS { get; set; }

			public string TS { get; set; }

			public override DateTime CreateTime { get; set; }
		}
	}
}
