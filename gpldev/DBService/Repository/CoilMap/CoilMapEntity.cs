using DBService.Base;
using System;
using static Core.Define.L2SystemDef;

namespace DBService.Repository
{
    public class CoilMapEntity
    {
        [Serializable]
        public class TBL_CoilMap : BaseRepositoryModel
        {

            public override DateTime UpdateTime { get; set; }
            public string Entry_Car { get; set; }
            public string Entry_TOP { get; set; }
            public string Entry_SK01 { get; set; }
            public string POR { get; set; }
            public string TR { get; set; } 
            public string Delivery_SK01 { get; set; } 
            public string Delivery_SK02 { get; set; } 
            public string Delivery_TOP { get; set; } 
            public string Delivery_Car { get; set; } 

            //Invaild          
            public bool IsSameCoilNo(string coilID, SKPOS pos)
            {
                bool isTheSame = true;

                switch(pos){
                    case SKPOS.Entry_Car:
                        isTheSame = Entry_Car.Trim().Equals(coilID);
                        break;
                    case SKPOS.Entry_SK01:
                        isTheSame = Entry_SK01.Trim().Equals(coilID);
                        break;
                    case SKPOS.EntryTOP:
                        isTheSame = Entry_TOP.Trim().Equals(coilID);
                        break;
                    case SKPOS.Delivery_SK01:
                        isTheSame = Delivery_SK01.Trim().Equals(coilID);
                        break;
                    case SKPOS.Delivery_SK02:
                        isTheSame = Delivery_SK02.Trim().Equals(coilID);
                        break;
                    case SKPOS.DeliveryTop:
                        isTheSame = Delivery_TOP.Trim().Equals(coilID);
                        break;
                    case SKPOS.Delivery_Car:
                        isTheSame = Delivery_Car.Trim().Equals(coilID);
                        break;
                }
                return isTheSame;
            }
            public string GetCoilNoFromPOS(SKPOS pos)
            {
                string coilNo = "";

                switch (pos)
                {
                    case SKPOS.Entry_Car:
                        coilNo = this.Entry_Car.Replace(" ", string.Empty);
                        break;
                    case SKPOS.Entry_SK01:
                        coilNo = this.Entry_SK01.Replace(" ",string.Empty);
                        break;                   
                    case SKPOS.EntryTOP:
                        coilNo = this.Entry_TOP.Replace(" ", string.Empty);
                        break;
                    case SKPOS.Delivery_SK01:
                        coilNo = this.Delivery_SK01.Replace(" ", string.Empty);
                        break;
                    case SKPOS.Delivery_SK02:
                        coilNo = this.Delivery_SK02.Replace(" ", string.Empty);
                        break;
                    case SKPOS.DeliveryTop:
                        coilNo = this.Delivery_TOP.Replace(" ", string.Empty);
                        break;
                    case SKPOS.Delivery_Car:
                        coilNo = this.Delivery_Car.Replace(" ", string.Empty);
                        break;
                }
                return coilNo;
            }

            public bool IsPosEmpty(SKPOS pos)
            {
                bool isPosEmpty = true;

                isPosEmpty = Entry_SK01.Trim().Equals("");
                switch (pos)
                {
                    case SKPOS.Entry_Car:
                        isPosEmpty = string.IsNullOrEmpty(Entry_Car.Trim());
                        break;
                    case SKPOS.Entry_SK01:
                        isPosEmpty = string.IsNullOrEmpty(Entry_SK01.Trim());
                        break;
                    case SKPOS.EntryTOP:
                        isPosEmpty = string.IsNullOrEmpty(Entry_TOP.Trim());
                        break;
                    
                }

                return isPosEmpty;
            }
        }     
    }
}
