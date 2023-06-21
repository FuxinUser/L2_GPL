using System;

namespace DBService.Repository.DelayLocation
{
    public class DelayLocationEntity
    {
        public class TBL_DelayLocation_Definition
        { 
            public int Index_No { get; set; }
            public string Delay_LocationCode { get; set; }
            public string Delay_LocationName { get; set; }
            public string Create_UserID { get; set; }
            public DateTime CreateTime { get; set; }
        }
    }
}
