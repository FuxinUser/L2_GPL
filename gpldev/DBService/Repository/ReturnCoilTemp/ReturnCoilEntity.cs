using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.ReturnCoil
{
    public class ReturnCoilEntity
    {
        /// <summary>
        /// 鋼卷回退實績暫存表
        /// </summary>
        [Serializable]
        public class TBL_RetrunCoil_Temp : BaseRepositoryModel
        {
            /// <summary>
            /// 回退鋼卷號
            /// </summary>
            [PrimaryKey]
            public string Reject_Coil_No { get; set; }

            /// <summary>
            /// 入口卷號
            /// </summary>
            public string Entry_CoilNo { get; set; }

            //20211012 new 
            /// <summary>
            /// 原PDI的出口鋼卷號
            /// </summary>
            public string OriPDI_Out_Coil_ID { get; set; }

            /// <summary>
            /// 計畫號
            /// </summary>
            public string Plan_No { get; set; }

            /// <summary>
            /// 回退方式
            /// </summary>
            public string Mode_Of_Reject { get; set; }

            /// <summary>
            /// 回退卷長
            /// </summary>
            public string Length_Of_Rejected_Coil { get; set; }

            //20211012 new 
            /// <summary>
            /// 回退卷寬度
            /// </summary>
            public string Width_Of_RejectedCoil { get; set; }

            /// <summary>
            /// 回退卷重
            /// </summary>
            public string Weight_Of_Rejected_Coil { get; set; }

            /// <summary>
            /// 回退卷內徑
            /// </summary>
            public string Inner_Diameter_Of_RejectedCoil { get; set; }

            /// <summary>
            /// 回退卷外徑
            /// </summary>
            public string Outer_Diameter_Of_RejectedCoil { get; set; }

            /// <summary>
            /// 回退原因代碼 01：设备故障 02：入料错误 03：更改作业排程 04：半卷分切
            /// </summary>
            public string Reason_Of_Reject { get; set; }

            /// <summary>
            /// 回退時間
            /// </summary>
            public string Time_Of_Reject { get; set; }

            /// <summary>
            /// 班次
            /// </summary>
            public string Shift_Of_Reject { get; set; }

            /// <summary>
            /// 班別
            /// </summary>
            public string Turn_Of_Reject { get; set; }

            /// <summary>
            /// 墊紙方式
            /// </summary>
            public string Paper_exit_Code { get; set; }

            /// <summary>
            /// 墊紙類型
            /// </summary>
            public string Paper_Type { get; set; }

            //20211012 new 
            /// <summary>
            ///  最后钢卷标记0：非最终分卷 1：最终卷
            /// </summary>
            public string Final_Coil_Flag { get; set; }

            //20211012 new 
            /// <summary>
            /// 头部垫纸长度
            /// </summary>
            public string Head_Paper_Length { get; set; }

            //20211012 new 
            /// <summary>
            /// 头部垫纸宽度
            /// </summary>
            public string Head_Paper_Width { get; set; }

            //20211012 new 
            /// <summary>
            /// 尾部垫纸长度
            /// </summary>
            public string Tail_Paper_Length { get; set; }

            //20211012 new 尾部垫纸宽度
            /// <summary>
            /// 尾部垫纸宽度
            /// </summary>
            public string Tail_Paper_Width { get; set; }

            //20211012 new 
            /// <summary>
            /// 退料鞍座
            /// </summary>
            public string Reject_Skid { get; set; }

            /// <summary>
            /// 修改人員職工編號
            /// </summary>
            public string UserID { get; set; }

            /// <summary>
            /// 修改日期時間
            /// </summary>
            public DateTime CreateTime { get; set; }
        }
    }
}
