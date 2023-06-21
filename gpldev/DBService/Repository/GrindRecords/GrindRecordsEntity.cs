using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.GrindRecords
{
    public class GrindRecordsEntity
    {
        public class TBL_GrindRecords : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }

            public string Plan_No { get; set; }
            [PrimaryKey]
            public short Current_Pass { get; set; }
            [PrimaryKey]
            public short Current_Senssion { get; set; }
            public int Line_Speed { get; set; }
            public string GR1_BELT_KIND { get; set; }
            public short GR1_BELT_PARTICLE_NO { get; set; }
            public short GR1_BELT_ROTATE_DIR { get; set; }
            public double GR1_MOTOR_CURRENT { get; set; }
            public int GR1_BELT_SPEED { get; set; }
            public string GR2_BELT_KIND { get; set; }
            public short GR2_BELT_PARTICLE_NO { get; set; }
            public short GR2_BELT_ROTATE_DIR { get; set; }
            public double GR2_MOTOR_CURRENT { get; set; }
            public int GR2_BELT_SPEED { get; set; }
            public string GR3_BELT_KIND { get; set; }
            public short GR3_BELT_PARTICLE_NO { get; set; }
            public short GR3_BELT_ROTATE_DIR { get; set; }
            public double GR3_MOTOR_CURRENT { get; set; }
            public string GR4_BELT_KIND { get; set; }
            public short GR4_BELT_PARTICLE_NO { get; set; }
            public short GR4_BELT_ROTATE_DIR { get; set; }
            public double GR4_MOTOR_CURRENT { get; set; }
            public string GR5_BELT_KIND { get; set; }
            public short GR5_BELT_PARTICLE_NO { get; set; }
            public short GR5_BELT_ROTATE_DIR { get; set; }
            public double GR5_MOTOR_CURRENT { get; set; }
            public string GR6_BELT_KIND { get; set; }
            public short GR6_BELT_PARTICLE_NO { get; set; }
            public short GR6_BELT_ROTATE_DIR { get; set; }
            public double GR6_MOTOR_CURRENT { get; set; }
            public System.DateTime Receive_Time { get; set; }
        }
    }
}
