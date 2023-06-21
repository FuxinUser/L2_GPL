using DBService.Base;

namespace DBService.Level25Repository.L2L25_DownTime
{
    public class L2L25_DownTime : BaseRepositoryModel
	{
		public string Message_Length { get; set; }
		public string Message_Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string op_flag { get; set; }
		public string unit_code { get; set; }
		public string prod_time { get; set; }
		public string prod_shift_no { get; set; }
		public string prod_shift_group { get; set; }
		public string stop_start_time { get; set; }
		public string stop_end_time { get; set; }
		public string delay_location { get; set; }
		public string delay_location_desc { get; set; }
		public string stop_elased_timey { get; set; }
		public string delay_reason_code { get; set; }
		public string delay_reason_desc { get; set; }
		public string delay_remark { get; set; }
		public string resp_depart_delay_time_m { get; set; }
		public string resp_depart_delay_time_e { get; set; }
		public string resp_depart_delay_time_c { get; set; }
		public string resp_depart_delay_time_p { get; set; }
		public string resp_depart_delay_time_u { get; set; }
		public string resp_depart_delay_time_o { get; set; }
		public string resp_depart_delay_time_r { get; set; }
		public string resp_depart_delay_time_rs { get; set; }
		public string deceleration_cause { get; set; }
		public string deceleration_code { get; set; }
	}
}
