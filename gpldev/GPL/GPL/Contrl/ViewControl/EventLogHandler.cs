//using Common;
using System;
using System.Windows.Forms;

namespace GPLManager
{
    public enum System_ID
    {
        System = 1,
        Client = 2
    }
    public enum Event_Type
    {
        Error = 1,
        Alarm = 2,
        Infro = 3,
        Debug = 4
    }
    public class EventLogHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly EventLogHandler INSTANCE = new EventLogHandler();
        }
        public static EventLogHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public void LogDebug(string FrameNO, string Event_Description, string Command)
        {
            Log(System_ID.Client, FrameNO, Event_Type.Debug, Event_Description, Command);
        }

        public void LogInfo(string FrameNO, string Event_Description, string Command)
        {
            Log(System_ID.Client, FrameNO, Event_Type.Infro, Event_Description, Command);
        }

        public void LogInfo(string FrameNO, string Event_Description, string Command, bool bolIsFormName )
        {
            if (bolIsFormName)
            {
                string strNameGetNo = "";
                #region 依Form Name 給 FrameNO
                switch (FrameNO)
                {
                    case nameof(Frm_0_0_Main): strNameGetNo = "0-0";  break;
                    case nameof(Frm_0_1_Login): strNameGetNo = "0-1"; break;                       
                    case nameof(Frm_0_2_Menu): strNameGetNo = "0-2"; break;

                    case nameof(Frm_1_1_PDISchl): strNameGetNo = "1-1"; break;
                    case nameof(Frm_1_2_PDIDetail): strNameGetNo = "1-2"; break;
                    case nameof(Frm_1_3_DeleteScheduleRecord): strNameGetNo = "1-3"; break;

                    case nameof(frm_2_1_Tracking): strNameGetNo = "2-1"; break;
                    case nameof(Frm_2_2_GPLSetupHistory): strNameGetNo = "2-2"; break;

                    case nameof(Frm_3_1_ProdCoilList): strNameGetNo = "3-1"; break;
                    case nameof(Frm_3_2_ProdDetail):strNameGetNo = "3-2";  break;                        
                    case nameof(Frm_3_3_GrindActualPerformance): strNameGetNo = "3-3"; break;
                    case nameof(Frm_3_3_Report): strNameGetNo = "3-4"; break;

                    case nameof(Frm_4_1_LineDelayRecord): strNameGetNo = "4-1"; break;
                    case nameof(frm_4_2_Utility): strNameGetNo = "4-2"; break;
                    case nameof(Frm_4_3_DeviceParameters): strNameGetNo = "4-3"; break;
                    case nameof(Frm_4_4_GrindPlan): strNameGetNo = "4-4"; break;
                    case nameof(Frm_4_5_Belts): strNameGetNo = "4-5"; break;                   

                    case nameof(Frm_5_1_EventLog): strNameGetNo = "5-1"; break;
                    case nameof(Frm_5_2_CodeMaintain): strNameGetNo = "5-2"; break;
                    case nameof(Frm_5_3_UserSetup): strNameGetNo = "5-3"; break;
                    case nameof(frm_5_4_CrewMaintenance): strNameGetNo = "5-4"; break;
                    case nameof(frm_5_5_Language): strNameGetNo = "5-5"; break;
                    case nameof(frm_5_6_NetworkStatus): strNameGetNo = "5-6"; break;

                    default:strNameGetNo = ""; break;
                }
                #endregion

                Log(System_ID.Client, strNameGetNo, Event_Type.Infro, Event_Description, Command);
            }
            else
            {
                Log(System_ID.Client, FrameNO, Event_Type.Infro, Event_Description, Command);
            }
            //Log(System_ID.Client, FrameNO, Event_Type.Infro, Event_Description, Command);
        }
        /// <summary>
        /// LOG字串
        /// </summary>
        /// <param name="System_ID"></param>
        /// <param name="FrameGroup_NO"></param>
        /// <param name="Frame_NO"></param>
        /// <param name="Event_Type"></param>
        public void Log(System_ID SystemID, string FrameNO, Event_Type EventType, string Event_Description, string Command)
        {
            string strSql = $@" Insert INTO [TBL_EventLog] 
                   ([System_ID],[Function_Block],[FrameGroup_No],[Frame_No],[Event_Type],[Event_Description],[Command],[CreateTime])
                   values ('{(int)SystemID }','','{System.Security.Principal.WindowsIdentity.GetCurrent().Name}','{FrameNO}','{(int)EventType}',N'{Event_Description}',N'{Command}','{GlobalVariableHandler.Instance.getTime}')";
            Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
        }
        public void EventPush_Message(string msg)
        {
            msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {msg}";
            if (PublicForms.Main.cboEvnMsg.Items.Count >= 10) GlobalVariableHandler.Instance.dtEventPush.Clear();
            PublicForms.Main.cboEvnMsg.Items.Add(msg);
            PublicForms.Main.cboEvnMsg.SelectedIndex = PublicForms.Main.cboEvnMsg.Items.Count - 1;

            PublicForms.Main.cboEvnMsg.DropDownStyle = ComboBoxStyle.DropDownList;
            PublicForms.Main.cboEvnMsg.AutoCompleteMode = AutoCompleteMode.None;// SuggestAppend;
        }
    }
}
