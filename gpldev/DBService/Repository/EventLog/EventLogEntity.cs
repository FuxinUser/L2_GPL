using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.EventLog
{
    public class EventLogEntity
    {
        [Serializable]
        public class TBL_EventLog  : BaseRepositoryModel
        {
            /// <summary>
            /// 系統編號
            /// 1:Server 2:Client
            /// </summary>
            public string System_ID { get; set; }
            /// <summary>
            /// Server 子系統名稱
            /// Ex: CoilMgr, PlcSndedit..
            /// </summary>
            public string Function_Block { get; set; }
            /// <summary>
            /// Client 畫面群組編號
            /// Ex:1
            /// </summary>
            public string FrameGroup_No { get; set; }
            /// <summary>
            /// Client 畫面編號 (EX:1-1)
            /// </summary>
            public string Frame_No { get; set; }
            /// <summary>
            /// 事件類別
            /// Server 1:Error, 2:Alarm, 3:Info, 4.Debug
            /// Client 1:Send, 2:Insert, 3:Update, 4.Delete
            /// </summary>
            public string Event_Type { get; set; }
            /// <summary>
            /// 事件描述
            /// </summary>
            public string Event_Description { get; set; }
            /// <summary>
            /// 事件語法
            /// </summary>
            public string Command { get; set; }
            /// <summary>
            /// 建立日期時間
            /// </summary>
            public override DateTime CreateTime { get; set; }
        }

    }
}
