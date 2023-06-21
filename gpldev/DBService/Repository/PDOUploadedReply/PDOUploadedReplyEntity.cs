using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBService.Base.DBAttributes;

namespace DBService.Repository
{
    public class PDOUploadedReplyEntity
    {
        /// <summary>
        /// 鋼卷回退實績
        /// </summary>
        [Serializable]
        public class TBL_PDOUploadedReply : BaseRepositoryModel
        {
            /// <summary>
            /// 材料號
            /// </summary>
            [PrimaryKey]
            public string Out_Coil_ID { get; set; }

            /// <summary>
            /// 計畫號
            /// </summary>
            public string Plan_No { get; set; }

            /// <summary>
            /// 處理成功標記
            /// </summary>
            public string Succ_Flag { get; set; }

            /// <summary>
            /// 失敗原因
            /// </summary>
            public string Err_Msg { get; set; }

            public override DateTime CreateTime { get => base.CreateTime; set => base.CreateTime = value; }

            public override DateTime UpdateTime { get => base.UpdateTime; set => base.UpdateTime = value; }


        }       

    }
}
