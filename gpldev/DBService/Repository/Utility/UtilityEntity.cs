using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.Utility
{
    public class UtilityEntity
    {

        public class TBL_Utility : BaseRepositoryModel
        {
            [PrimaryKey]
            public DateTime Receive_Time { get; set; }
            //[IgnoreReflction]
            public string Shift { get; set; }
            //[IgnoreReflction]
            public string Team { get; set; }
            public double CompressedAir { get; set; }
            public double Steam { get; set; }
            public double RinseWater { get; set; }
            public double IndirectCoolingWater { get; set; }
        }

    }
}
