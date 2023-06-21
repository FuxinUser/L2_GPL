using DBService.Base;
using static DBService.Base.DBAttributes;


namespace DBService.Repository.LineStatus
{
    public class ProcessDataEntity 
    {
        public class TBL_ProcessData : BaseRepositoryModel
        {
            [PrimaryKey]
            public System.DateTime Receive_Time { get; set; }
            public int Line_Speed { get; set; }
            public int Tension_Reel_Speed { get; set; }
            public int Threading_Speed { get; set; }
            public double Line_Tension { get; set; }
            public int Line_run_direction { get; set; }
            public double GR1_BeltMotor_Current { get; set; }
            public int GR1_Belt_Speed { get; set; }
            public double GR2_BeltMotor_Current { get; set; }
            public int GR2_Belt_Speed { get; set; }
            public double GR3_BeltMotor_Current { get; set; }
            public double GR4_BeltMotor_Current { get; set; }
            public double GR5_BeltMotor_Current { get; set; }
            public double GR6_BeltMotor_Current { get; set; }
            public double CoolantTank_Temp { get; set; }
            public double AlkaliTank_Temp { get; set; }
            public double PrimaryRinseTank_Temp { get; set; }
            public double FinishRinseTank_Temp { get; set; }
            public double StripDryerTemp { get; set; }
            public double BrushRoll1_Current { get; set; }
            public double BrushRoll2_Current { get; set; }
        }
    }
}
