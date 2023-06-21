using System;

namespace DataMod.Common
{
    [Serializable]
    public class SpectPresetModel
    {

        /// <summary>
        /// 鋼捲ID
        /// </summary>
        public string CoilID { get; set; }

        /// <summary>
        /// 鞍座位置ID
        /// </summary>
        public int SKPosID { get; set; }

        /// <summary>
        /// 計畫號
        /// </summary>
        public string PlanNo { get; set; }

    }
}
