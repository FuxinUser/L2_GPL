using System;

namespace DBService.Repository.FaultCode
{
    public class FaultCodeEntity
    {
        public class TBL_FaultCode
        { 
            public int Sequence_No { get; set; }
            public string Fault_Code { get; set; }
            public string Fault_Description { get; set; }
            public DateTime CreateTime { get; set; }
        }
    }
}
