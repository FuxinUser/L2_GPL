using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.CoilScheduleDelete
{
    public class CoilScheduleDeleteEntity
	{
		public class TBL_CoilScheduleDelete : BaseRepositoryModel
		{
			[PrimaryKey]
			public string CoilNo { get; set; }
			public string OperatorId { get; set; }
			public string ReasonCode { get; set; }
			public string Remarks { get; set; }
			public override DateTime CreateTime { get; set; }
		}
	}
}
