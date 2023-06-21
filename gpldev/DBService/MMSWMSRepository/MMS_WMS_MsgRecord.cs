using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.MMSRepository
{
    public class MMS_WMS_MsgRecord :BaseRepositoryModel
    {
		[IgnoreReflction]
		public int Id { get; set; }

		public string Header { get; set; }

		public string Length { get; set; }
		public string Data { get; set; }
		public string IsAck { get; set; }

		public override DateTime CreateTime { get; set; }


	}
}
