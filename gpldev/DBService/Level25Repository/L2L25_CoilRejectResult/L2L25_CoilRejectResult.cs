using DBService.Base;

namespace DBService.Level25Repository.L2L25_CoilRejectResult
{
    public class L2L25_CoilRejectResult : BaseRepositoryModel
	{
		public string Reject_Coil_No { get; set; }
		public string Entry_CoilNo { get; set; }
		public string Mode_Of_Reject { get; set; }
		public string Length_Of_Rejected_Coil { get; set; }
		public string Weight_Of_Rejected_Coil { get; set; }
		public string Inner_Diameter_Of_RejectedCoil { get; set; }
		public string Outer_Diameter_Of_RejectedCoil { get; set; }
		public string Reason_Of_Reject { get; set; }
		public string Time_Of_Reject { get; set; }
		public string Shift_Of_Reject { get; set; }
		public string Turn_Of_Reject { get; set; }
		
	}
}
