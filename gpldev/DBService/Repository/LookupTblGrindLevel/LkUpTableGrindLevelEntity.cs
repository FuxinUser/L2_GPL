using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LookupTblGrindLevel
{
    public class LkUpTableGrindLevelEntity
    {
		public class TBL_LookupTableGrindLevel : BaseRepositoryModel
		{
			[PrimaryKey]
			public string Code { get; set; }
			[PrimaryKey]
			public string OuterGrade { get; set; }
			[PrimaryKey]
			public string InnerGrade { get; set; }
		}
	}
}
