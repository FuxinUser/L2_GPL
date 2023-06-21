using System;

namespace DataMod.Common
{
    [Serializable]
    public class ProcessModel
    {
        public string TotalLength { get; set; } = string.Empty;
        public int DataCnt { get; set; } = 0;
        public string LinespeedStr { get; set; } = string.Empty;
        public string LinetensionStr { get; set; } = string.Empty;

        public string LinerundirectionStr { get; set; } = string.Empty;

        public string TensionReelSpeedStr { get; set; } = string.Empty;

        public string ThreadingSpeedStr { get; set; } = string.Empty;

        public string No1GRAbrasiveBeltMotorCurrentStr { get; set; } = string.Empty;

        public string No1GRAbrasiveBeltSpeedStr { get; set; } = string.Empty;

        public string No2GRAbrasiveBeltMotorCurrentStr { get; set; } = string.Empty;

        public string No2GRAbrasiveBeltSpeedStr { get; set; } = string.Empty;

        public string No3GRAbrasiveBeltMotorCurrentStr { get; set; } = string.Empty;

        public string No4GRAbrasiveBeltMotorCurrentStr { get; set; } = string.Empty;

        public string No5GRAbrasiveBeltMotorCurrentStr { get; set; } = string.Empty;

        public string No6GRAbrasiveBeltMotorCurrentStr { get; set; } = string.Empty;

        public string CoolantOilTankTemperatureStr { get; set; } = string.Empty;

        public string AlkaliSolutionTankTemperatureStr { get; set; } = string.Empty;

        public string PrimaryRinseWaterTankTemperatureStr { get; set; } = string.Empty;

        public string FinishRinseTankTemperatureStr { get; set; } = string.Empty;

        public string StripDryerTemperatureStr { get; set; } = string.Empty;

        public string BrushRollCurrent1Str { get; set; } = string.Empty;

        public string BrushRollCurrent2Str { get; set; } = string.Empty;

    }
}
