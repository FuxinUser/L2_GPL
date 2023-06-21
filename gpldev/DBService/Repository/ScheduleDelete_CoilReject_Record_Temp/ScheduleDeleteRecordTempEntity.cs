using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository
{
    public class ScheduleDeleteRecordTempEntity
    {
		/// <summary>
		/// 排程跳軋/鋼捲退料暫存記錄
		/// </summary>
		public class L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp : BaseRepositoryModel
		{
			/// <summary>
			/// 鋼卷編號
			/// </summary>
			[PrimaryKey]
			public string CoilNo { get; set; }


			/// <summary>
			/// 建立人員職工編號
			/// </summary>
			public string OperatorId { get; set; }

			public string ReasonCode { get; set; }
			public string Remarks { get; set; }
			public override DateTime CreateTime { get; set; }
		}
	}
}
