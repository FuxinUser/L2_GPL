using DBService.Base;

namespace DBService.Level25Repository.L2L25_No5GRAB_beltRoughnessCT
{
    public class L2L25_No5GRAB_beltRoughnessCT : BaseRepositoryModel
	{
		public string Message_Length { get; set; }
		public string Message_Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string out_mat_no { get; set; }
		public string plan_no { get; set; }
		public int CurrentPassNumber { get; set; } = 0;
		public int CurrentSession { get; set; } = 0;
		public string lineCode { get; set; }
		public string seqUnit { get; set; }
		public string dataCode { get; set; }
		public string dataDesc { get; set; }
		public string resultUnit { get; set; }
		public string genBeginDate { get; set; }
		public string genBeginTime { get; set; }
		public string genStopDate { get; set; }
		public string genStopTime { get; set; }
		public string frequency { get; set; }
		public string DataCount { get; set; }
		public string DataString { get; set; }

	}
}
