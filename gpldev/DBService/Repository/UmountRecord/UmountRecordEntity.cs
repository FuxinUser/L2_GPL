using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.UmountRecord
{
    public class UmountRecordEntity
    {
		public class TBL_UnmountRecord : BaseRepositoryModel
		{
			[PrimaryKey]
			public string CoilID { get; set; }
			public double CoilWeight { get; set; }
			public int CoilLength { get; set; }
			public double Diameter { get; set; }
			public double CoiInsideDiam { get; set; }
			public double Width { get; set; }

			public override DateTime CreateTime { get; set; }
		}

	}
}
