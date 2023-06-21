using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LineFaultRecords
{
    public class LineFaultDelRecordsEntity
    {
		[Serializable]
		public class TBL_LineFaultDelRecords : BaseRepositoryModel
        {
			public string unit_code { get; set; }
			[PrimaryKey]
			public DateTime prod_time { get; set; }
			public string prod_shift_no { get; set; }
			public string prod_shift_group { get; set; }
			[PrimaryKey]
			public DateTime stop_start_time { get; set; }
			//[PrimaryKey]
			public DateTime stop_end_time { get; set; }
			public string delay_location { get; set; }
			public string delay_location_desc { get; set; } = default;
			public string stop_elased_timey { get; set; }
			public int stop_category { get; set; }
			public string delay_reason_code { get; set; }
			public string delay_reason_desc { get; set; } = default;
			public string delay_remark { get; set; }
			public string resp_depart_delay_time_m { get; set; }
			public string resp_depart_delay_time_e { get; set; }
			public string resp_depart_delay_time_c { get; set; }
			public string resp_depart_delay_time_p { get; set; }
			public string resp_depart_delay_time_u { get; set; }
			public string resp_depart_delay_time_o { get; set; }
			public string resp_depart_delay_time_r { get; set; }
			public string resp_depart_delay_time_rs { get; set; }

			/// <summary>
			/// 降速原因
			/// </summary>
			public string deceleration_cause { get; set; }

			/// <summary>
			/// 降速代码
			/// </summary>
			public string deceleration_code { get; set; }

			public override DateTime CreateTime { get; set; }

            public override DateTime UpdateTime { get; set; }

			public string UploadMMS { get; set; }

		}

	}
}
