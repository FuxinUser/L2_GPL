using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.WieldRecord
{
    public class WeldRecordEntity
    {
        public class TBL_WeldRecords : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }

            public string Plan_No { get; set; }
            [PrimaryKey]
            public DateTime ReceiveTime { get; set; }
            public double Voltage_Set { get; set; }
            public double Current_Set { get; set; }
            public double Current_Actual { get; set; }
            public double WireSpeed_Set { get; set; }
            public int WeldSpeed_Set { get; set; }
            public int WeldSpeed_Actual { get; set; }
            public double WeldGap { get; set; }
            public int StartPaddleTime { get; set; }
            public int EndPaddleTime { get; set; }
            public short ScheduleNumber { get; set; }
        }



    }
}
