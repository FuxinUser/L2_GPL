using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository
{
    public class CoilScheduleEntity
    {
        public class TBL_Production_Schedule : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }
            public short Seq_No { get; set; }
            // 排程狀態
            // N = 新鋼捲 ; R = 要求入料 ; F = 已入料 ; I = 身分確認成功 ; P = 生產中 ; D = 已產出 ; 
            public string Schedule_Status { get; set; } = "";
            public string Update_Source { get; set; } = "";
            public override DateTime UpdateTime { get; set; } = DateTime.Now;      
        }

    }
}
