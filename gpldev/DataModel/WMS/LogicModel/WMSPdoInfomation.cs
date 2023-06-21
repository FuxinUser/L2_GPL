using System;

namespace DataMod.WMS.LogicModel
{

    /// <summary>
    /// 鋼捲產出資訊
    /// </summary>
    [Serializable]
    public class WMSPdoInfomation
    {
        // 鋼捲號碼
        public string Out_Mat_No { get; set; }

        // 鋼捲入口捲號
        public string In_Mat_No { get; set; }

        // 訂單編號
        public string OrderNo { get; set; }
        // 包裝方式?
        // 內徑
        public int Out_Mat_Inner { get; set; }
        // 外徑
        public int Out_Mat_Outer_Diameter { get; set; }
        // 鋼捲厚度
        public double Out_Mat_Thick { get; set; }
        // 鋼捲寬度
        public double Out_Mat_Width { get; set; }
        // 鋼捲重量
        public int Out_Mat_Wt { get; set; }
        // 鋼捲收捲方向 0：上收捲/1：下收捲
        public string Winding_Dire { get; set; }
        // 鋼捲是否含油 0：N0/1：Yes
        public string Oil_Flag { get; set; }
        //套筒內徑
        public string Sleeve_InnerDia { get; set; }
        //套筒厚度
        public string Sleeve_Thick { get; set; }
        //套筒寬度
        public string Sleeve_Width { get; set; }
    }
}
