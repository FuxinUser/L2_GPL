using DBService.Base;

namespace DBService.Level25Repository.L2L25_ENGC
{
    public class L2L25_ENGC : BaseRepositoryModel
	{
		public string Message_Length { get; set; }
		public string Message_Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string CompressedAir { get; set; }
		public string IndirectCollingWater { get; set; }
	}
}
