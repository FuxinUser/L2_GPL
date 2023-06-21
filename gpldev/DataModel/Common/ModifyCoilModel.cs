using System;

namespace DataMod.Common
{
    public class ModifyCoilModel
    {
        [Serializable]
        public class ModifyResult
        {
            /*
               1:POR
               2:No.1 entry skid
               3:Entry TOP
               4:Entry lift car
               5:TR
               6:No.1 delivery skid
               7:No.2 delivery skid
               8:Delivery TOP
               9:Delivery lift car
           */
            public short ModifyPosition { get; set; }
            public string CoilId { get; set; }
            

            public ModifyResult(short pos, string coilId)
            {
                ModifyPosition = pos;
                CoilId = coilId;
                
            }
        }


        [Serializable]
        public class DeleteResult
        {
            /*
             1:POR
             2:No.1 entry skid
             3:Entry TOP
             4:Entry lift car
             5:TR
             6:No.1 delivery skid
             7:No.2 delivery skid
             8:Delivery TOP
             9:Delivery lift car
         */
            public short DelPosition { get; set; }
            public string CoilId { get; set; }

            public DeleteResult(short pos, string coilId)
            {
                DelPosition = pos;
                CoilId = coilId;

            }
        }
    }
}
