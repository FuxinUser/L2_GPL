using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.BeltPatternsRecords
{
    public class BeltPatternsRecordEntity
    {
		public class TBL_BeltPatterns_Records : BaseRepositoryModel
		{
			public string BeltPattern { get; set; }
			public short Pass_From { get; set; }
			public short Pass_To { get; set; }
			public short GR_NO { get; set; }
			public double GR_Current { get; set; }
			public string Belt_MaterialCode { get; set; }
			public short Belt_ParticalNumber { get; set; }
			public byte Belt_RotateDir { get; set; }
			public int Belt_Speed { get; set; }
			public string Pass_Section { get; set; }
			public string Plan_No { get; set; }
			public string Coil_ID { get; set; }
	
			public override DateTime CreateTime { get; set; }
		}
	}
}
