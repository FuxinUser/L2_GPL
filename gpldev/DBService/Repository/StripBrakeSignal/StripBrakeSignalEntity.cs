using DBService.Base;
using System;

namespace DBService.Repository.StripBrakeSignal
{
    public class StripBrakeSignalEntity
    {
        public class TBL_StripBrakeSignal : BaseRepositoryModel
        {
            public string UncoilerCoil_No { get; set; }
            public double UncoilerCoil_Thick { get; set; }
            public int UncoilerCoil_Width { get; set; }
            public int UncoilerCoil_Length { get; set; }
            public int UncoilerCoil_InnerDiameter { get; set; }
            public int UncoilerCoil_OuterDiameter { get; set; }
            public int UncoilerCoil_TheorticalWt { get; set; }
            public string RecoilerCoil_No { get; set; }
            public double RecoilerCoil_Thick { get; set; }
            public int RecoilerCoil_Width { get; set; }
            public int RecoilerCoil_Length { get; set; }
            public int RecoilerCoil_InnerDiameter { get; set; }
            public int RecoilerCoil_OuterDiameter { get; set; }
            public int RecoilerCoil_TheoreticalWt { get; set; }
            public  DateTime ReceiveTime { get; set; }
            public override DateTime CreateTime { get; set; }

        }

    }
}
