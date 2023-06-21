using DBService.Base;

namespace DBService.Level25Repository.L2L25_CoilMap
{
    public class L2L25_CoilMap : BaseRepositoryModel
	{
		public string Message_Length { get; set; }
		public string Message_Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string POR { get; set; }
		public string Entry_SK01 { get; set; }
		public string EntryTOP { get; set; }
		public string Entry_Car { get; set; }
		public string TR { get; set; }
		public string Delivery_SK01 { get; set; }
		public string Delivery_SK02 { get; set; }
		public string Delivery_TOP { get; set; }
		public string Delivery_Car { get; set; }
	}
}
