using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.BeltPatterns
{
    public class BeltPatternsEntity
    {
        public class TBL_BeltPatterns : BaseRepositoryModel
        {
            [PrimaryKey]
            public string BeltPattern { get; set; }
            [PrimaryKey]
            public short Pass_From { get; set; }
            [PrimaryKey]
            public short Pass_To { get; set; }
            [PrimaryKey]
            public short GR_NO { get; set; }
            public double GR_Current { get; set; }
            public string Belt_MaterialCode { get; set; }
            public short Belt_ParticalNumber { get; set; }
            public byte Belt_RotateDir { get; set; }
            public int Belt_Speed { get; set; }

            // Join Table 使用
            [IgnoreReflction]
            public string Pass_Section { get; set; }
            [IgnoreReflction]
            public int LineSpeed { get; set; }
            [IgnoreReflction]
            public int PassNumber { get; set; }
        }

    }
}
