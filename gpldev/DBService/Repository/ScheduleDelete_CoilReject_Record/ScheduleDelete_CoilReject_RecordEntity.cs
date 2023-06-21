using DBService.Base;
using System;

namespace DBService.Repository.ScheduleDelete_CoilReject_Record
{
    public class ScheduleDelete_CoilReject_RecordEntity
    {
        public class L3L2_TBL_ScheduleDelete_CoilReject_Record : BaseRepositoryModel
        { 
            public string Coil_ID { get; set; } = string.Empty;
            public string ScheduleDelete_CoilReject_GroupNo { get; set; } = string.Empty;
            public string ScheduleDelete_CoilReject_Code { get; set; } = string.Empty;
            public string Remarks { get; set; } = string.Empty;
            public string Create_UserID { get; set; } = string.Empty;
            public DateTime CreateTime { get; set; }

        }
    }
}
