using System;
using System.Data;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public class UserSetupHandler
    {
        // 帳號、密碼、權限等級
        public string UserID;
        public string Authority_Class;
        public string Authority_Class_Show;
        public string Frm_0_2, Frm_1_1, Frm_1_2, Frm_1_3, Frm_2_1, Frm_2_2, Frm_3_1, Frm_3_2, Frm_3_3, Frm_3_4, Frm_4_1, Frm_4_2, Frm_4_3, Frm_4_4, Frm_4_5, Frm_5_1, Frm_5_2, Frm_5_3, Frm_5_4, Frm_5_5, Frm_5_6;

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly UserSetupHandler INSTANCE = new UserSetupHandler();
        }

        public static UserSetupHandler Instance { get { return SingletonHolder.INSTANCE; } }

        /// <summary>
        /// 搜尋帳號資料
        /// </summary>
        public void UserLogin(string getPassword)
        {
            //紀錄登入帳號密碼
            UserID = PublicForms.Login.Cob_User.Text;

            DataTable dtGetUser = new DataTable();
            string strSql = SqlFactory.SelectUserLoginInfo(getPassword);
            dtGetUser = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"Login","0-2");

            if (dtGetUser == null) return;
           
            // 登入成功
            if (!dtGetUser.Rows.Count.Equals(0))
            {
                PublicForms.Login.bolLogin = true;

                // 給預設值 都給N ,避免該使用者沒建到某畫面權限,後續執行會 Exception(參考CPL)
                Frm_0_2 = Frm_1_1= Frm_1_2= Frm_1_3= Frm_2_1= Frm_2_2= Frm_3_1= Frm_3_2= Frm_3_3= Frm_3_4= Frm_4_1= Frm_4_2= Frm_4_3= Frm_4_4= Frm_4_5= Frm_5_1= Frm_5_2= Frm_5_3= Frm_5_4= Frm_5_5= Frm_5_6 = "N";

                Authority_Class = dtGetUser.Rows[0][nameof(TBL_AuthorityData.Authority_Class)].ToString() ?? string.Empty;
                Authority_Class_Show = Fun_Authority_ClassChangeShow(Authority_Class);

                for (int Index = 0; Index < dtGetUser.Rows.Count; Index++)
                {
                    switch (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_ID)].ToString().Trim())
                    {
                        case "0-2":
                            Frm_0_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "1-1":
                            Frm_1_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "1-2":
                            Frm_1_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "1-3":
                            Frm_1_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "2-1":
                            Frm_2_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "2-2":
                            Frm_2_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "3-1":
                            Frm_3_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "3-2":
                            Frm_3_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "3-3":
                            Frm_3_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "3-4":
                            Frm_3_4 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "4-1":
                            Frm_4_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "4-2":
                            Frm_4_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "4-3":
                            Frm_4_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "4-4":
                            Frm_4_4 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "4-5":
                            Frm_4_5 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "5-1":
                            Frm_5_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "5-2":
                            Frm_5_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "5-3":
                            Frm_5_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "5-4":
                            Frm_5_4 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "5-5":
                            Frm_5_5 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        case "5-6":
                            Frm_5_6 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString() ?? string.Empty;
                            break;
                        default:
                            break;
                    }
                }

                //SetupForm();
            }
            // 登入失敗or查無帳號
            else if (dtGetUser.Rows.Count.Equals(0))
            {
                PublicForms.Login.bolLogin = false;
                return;
            }
        }

        private string Fun_Authority_ClassChangeShow(string strKey)
        {
            string strShow;
            switch (strKey)
            {
                case "1":
                    strShow = "Administrator";
                    break;
                case "2":
                    strShow = "Manager";
                    break;
                case "3":
                    strShow = "Operator";
                    break;
                default:
                    strShow = "";
                    break;
            }
            return strShow;
        }
        /// <summary>
        /// 設定權限禁止畫面
        /// </summary>
        public void SetupBanForm()
        {
            #region 'Menu'
            PublicForms.Main.tsMenuItem_0.Enabled = !Frm_0_2.Equals("N");
            #endregion

            #region '1-1'
            PublicForms.Main.tsMenuItem_1_1.Enabled = !Frm_1_1.Equals("N");
            PublicForms.Menu.Btn_1_1.Enabled = !Frm_1_1.Equals("N");
            #endregion

            #region '1-2'
            if (Frm_1_2.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_1_2.Enabled = false;
                PublicForms.Menu.Btn_1_2.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_1_2.Enabled = true;
                PublicForms.Menu.Btn_1_2.Enabled = true;
            }
            #endregion

            #region '1-3'
            PublicForms.Main.tsMenuItem_1_3.Enabled = !Frm_1_3.Equals("N");
            PublicForms.Menu.Btn_1_3.Enabled = !Frm_1_3.Equals("N");
            #endregion

            #region '2-1'
            PublicForms.Main.tsMenuItem_2_1.Enabled = !Frm_2_1.Equals("N");
            PublicForms.Menu.Btn_2_1.Enabled = !Frm_2_1.Equals("N");
            #endregion

            #region '2-2'
            PublicForms.Main.tsMenuItem_2_2.Enabled = !Frm_2_2.Equals("N");
            PublicForms.Menu.Btn_2_2.Enabled = !Frm_2_2.Equals("N");
            #endregion

            #region '3-1'
            PublicForms.Main.tsMenuItem_3_1.Enabled = !Frm_3_1.Equals("N");
            PublicForms.Menu.Btn_3_1.Enabled = !Frm_3_1.Equals("N");
            #endregion

            #region '3-2'
            PublicForms.Main.tsMenuItem_3_2.Enabled = !Frm_3_2.Equals("N");
            PublicForms.Menu.Btn_3_2.Enabled = !Frm_3_2.Equals("N");
            #endregion

            #region '3-3'
            PublicForms.Main.tsMenuItem_3_3.Enabled = !Frm_3_3.Equals("N");
            PublicForms.Menu.Btn_3_3.Enabled = !Frm_3_3.Equals("N");
            #endregion

            #region '3-4'
            //PublicForms.Main.tsMenuItem_3_4.Enabled = !Frm_3_4.Equals("N");
            //PublicForms.Menu.Btn_3_4.Enabled = !Frm_3_4.Equals("N");
            #endregion

            #region '4-1'
            PublicForms.Main.tsMenuItem_4_1.Enabled = !Frm_4_1.Equals("N");
            PublicForms.Menu.Btn_4_1.Enabled = !Frm_4_1.Equals("N");
            #endregion

            #region '4-2'
            PublicForms.Main.tsMenuItem_4_2.Enabled = !Frm_4_2.Equals("N");
            PublicForms.Menu.Btn_4_2.Enabled = !Frm_4_2.Equals("N");
            #endregion

            #region '4-3'
            PublicForms.Main.tsMenuItem_4_3.Enabled = !Frm_4_3.Equals("N");
            PublicForms.Menu.Btn_4_3.Enabled = !Frm_4_3.Equals("N");
            #endregion

            #region '4-4'
            PublicForms.Main.tsMenuItem_4_4.Enabled = !Frm_4_4.Equals("N");
            PublicForms.Menu.Btn_4_4.Enabled = !Frm_4_4.Equals("N");
            #endregion

            #region '4-5'
            PublicForms.Main.tsMenuItem_4_5.Enabled = !Frm_4_5.Equals("N");
            PublicForms.Menu.Btn_4_5.Enabled = !Frm_4_5.Equals("N");
            #endregion

            #region '5-1'
            PublicForms.Main.tsMenuItem_5_1.Enabled = !Frm_5_1.Equals("N");
            PublicForms.Menu.Btn_5_1.Enabled = !Frm_5_1.Equals("N");
            #endregion

            #region '5-2'
            PublicForms.Main.tsMenuItem_5_2.Enabled = !Frm_5_2.Equals("N");
            PublicForms.Menu.Btn_5_2.Enabled = !Frm_5_2.Equals("N");
            #endregion

            #region '5-3'
            PublicForms.Main.tsMenuItem_5_3.Enabled = !Frm_5_3.Equals("N");
            PublicForms.Menu.Btn_5_3.Enabled = !Frm_5_3.Equals("N");
            #endregion

            #region '5-4'
            PublicForms.Main.tsMenuItem_5_4.Enabled = !Frm_5_4.Equals("N");
            PublicForms.Menu.Btn_5_4.Enabled = !Frm_5_4.Equals("N");
            #endregion

            #region '5-5'
            PublicForms.Main.tsMenuItem_5_5.Enabled = !Frm_5_5.Equals("N");
            PublicForms.Menu.Btn_5_5.Enabled = !Frm_5_5.Equals("N");
            #endregion

            #region '5-6'
            PublicForms.Main.tsMenuItem_5_6.Enabled = !Frm_5_6.Equals("N");
            PublicForms.Menu.Btn_5_6.Enabled = !Frm_5_6.Equals("N");
            #endregion
        }

        /// <summary>
        /// 畫面可讀不可寫設定
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Frame_Funtion"></param>
        public void FrameSectionBanSetup(Form Form, string Frame_Funtion)
        {
            if (Frame_Funtion.Equals("R"))
            {
                switch (Form.Name)
                {
                    case "Frm_1_1_PDISchl":
                        #region '入料鋼卷排程作業'
                        PublicForms.PDISchl.Btn_First.Enabled = false; //第一位
                        PublicForms.PDISchl.Btn_Up.Enabled = false; //上一位
                        PublicForms.PDISchl.Btn_Last.Enabled = false; //最後一位
                        PublicForms.PDISchl.Btn_Down.Enabled = false; //下一位
                        PublicForms.PDISchl.Btn_MovePdi.Enabled = false; //確定排程
                        PublicForms.PDISchl.Btn_OutSchedule.Enabled = false; //刪除排程
                        PublicForms.PDISchl.Btn_ImportPDI.Enabled = false; //匯入PDI
                        PublicForms.PDISchl.Btn_ImportSchedule.Enabled = false; //匯入排程
                        PublicForms.PDISchl.Btn_RequestPDI.Enabled = false; //要求PDI
                        PublicForms.PDISchl.Btn_NewSchdl.Enabled = false; //要求排程刷新
                        PublicForms.PDISchl.Btn_InsertDummy.Enabled = false; //插入過渡卷
                        #endregion
                        break;
                    case "Frm_1_2_PDIDetail":
                        #region '入料鋼卷詳細資料'
                        PublicForms.PDIDetail.Btn_New.Enabled = false; //複製新增
                        PublicForms.PDIDetail.Btn_Delete.Enabled = false; //刪除
                        PublicForms.PDIDetail.Btn_New.Enabled = false; //新增
                        PublicForms.PDIDetail.Btn_Edit.Enabled = false; //修改
                        #endregion
                        break;
                    case "frm_2_1_Tracking":
                        #region '鋼卷追蹤'
                        PublicForms.Tracking.Btn_AutoCoilFeed.Enabled = false; //進料模式
                        PublicForms.Tracking.Btn_ManualFeed.Enabled = false; //入料
                        PublicForms.Tracking.Btn_ESK01_Entry.Enabled = false; //ESK01 入料
                        PublicForms.Tracking.Btn_ESK01_Return.Enabled = false; //ESK01 退料
                        PublicForms.Tracking.Btn_ETOP_Return.Enabled = false; //ETOP 退料
                        PublicForms.Tracking.Btn_ESK01_PrintLabel.Enabled = false; //ESK01 列印標籤
                        PublicForms.Tracking.Btn_ETOP_PrintLabel.Enabled = false; //ETOP 列印標籤
                        PublicForms.Tracking.Btn_ESK01_Del.Enabled = false; //ESK01 刪除
                        PublicForms.Tracking.Btn_ETOP_Del.Enabled = false; //ETOP 刪除
                        PublicForms.Tracking.Btn_PORReturn.Enabled = false; //開捲機 退料
                        PublicForms.Tracking.Btn_PDO.Enabled = false; //PDO確認
                        PublicForms.Tracking.Btn_DSK02Weight.Enabled = false; //DSK02 秤重
                        PublicForms.Tracking.Btn_DSK01_CoilOut.Enabled = false; //DSK01 出料
                        PublicForms.Tracking.Btn_DSK02_CoilOut.Enabled = false; //DSK02 出料
                        PublicForms.Tracking.Btn_DSK01_PrintLabel.Enabled = false; //DSK01 列印標籤
                        PublicForms.Tracking.Btn_DSK02_PrintLabel.Enabled = false; //DSK02 列印標籤
                        PublicForms.Tracking.Btn_DTOP_PrintLabel.Enabled = false; //DTOP 列印標籤
                        PublicForms.Tracking.Btn_DSK01_Del.Enabled = false; //DSK01 刪除
                        PublicForms.Tracking.Btn_DSK02_Del.Enabled = false; //DSK02 刪除
                        PublicForms.Tracking.Btn_DTOP_Del.Enabled = false; //DTOP 刪除
                        if (Frm_2_2.Equals("N")) PublicForms.Tracking.Btn_LinkFrm_2_2.Enabled = false; //研磨設定
                        PublicForms.Tracking.AckPDIToolStripMenuItem.Enabled = false; //鞍座要求PDI
                        #endregion
                        break;
                    case "Frm_3_2_PDODetail":
                        #region '產品鋼卷詳細資料'
                        PublicForms.PDODetail.Btn_CopyNew.Enabled = false; //複製新增
                        PublicForms.PDODetail.Btn_New.Enabled = false; //新增
                        PublicForms.PDODetail.Btn_Update.Enabled = false; //修改
                        PublicForms.PDODetail.Btn_MMS.Enabled = false; //上傳MMS
                        #endregion
                        break;
                    case "Frm_4_1_RollCurrent":
                        #region '研磨計畫'
                        PublicForms.GrindPlan.Btn_BeltPattern_Insert.Enabled = false; //BeltPattern 新增
                        PublicForms.GrindPlan.Btn_BeltPattern_Update.Enabled = false; //BeltPattern 修改

                        PublicForms.GrindPlan.Btn_GradeGroup_Insert.Enabled = false; //GradeGroup 新增
                        PublicForms.GrindPlan.Btn_GradeGroup_Update.Enabled = false; //GradeGroup 修改

                        PublicForms.GrindPlan.Btn_ThicknessRelction_Insert.Enabled = false; //ThicknessRelction 新增
                        PublicForms.GrindPlan.Btn_ThicknessRelction_Update.Enabled = false; //ThicknessRelction 修改
                        #endregion
                        break;
                    case "Frm_4_2_Belts":
                        #region '皮帶管理'
                        PublicForms.Belts.Btn_Belts_New.Enabled = false; //皮帶管理 新增
                        PublicForms.Belts.Btn_Belts_Edit.Enabled = false; //皮帶管理 修改

                        PublicForms.Belts.Btn_Materials_Add.Enabled = false; //材料 新增
                        PublicForms.Belts.Btn_Materials_Edit.Enabled = false; //材料 修改

                        PublicForms.Belts.Btn_Suppliers_Add.Enabled = false; //供應商 新增
                        PublicForms.Belts.Btn_Suppliers_Edit.Enabled = false; //供應商 修改
                        #endregion
                        break;
                    case "Frm_4_3_DeviceParameters":
                        #region '設備參數'
                        //PublicForms._DeviceParameters.btn_TensionInsert.Enabled = false; //張力機 新增
                        //PublicForms._DeviceParameters.btn_TensionUpdate.Enabled = false; //張力機 修改

                        //PublicForms._DeviceParameters.btn_Flattener_Insert.Enabled = false; //整平機(熱軋) 新增
                        //PublicForms._DeviceParameters.btn_Flattener_Update.Enabled = false; //整平機(熱軋) 修改

                        //PublicForms._DeviceParameters.btn_Flattener_C_Insert.Enabled = false; //整平機(冷軋) 新增
                        //PublicForms._DeviceParameters.btn_Flattener_C_Update.Enabled = false; //整平機(冷軋) 修改
                        #endregion
                        break;
                    case "Frm_4_4_LineDelayRecord":
                        #region '停復機紀錄'
                        PublicForms.LineDelayRecord.Btn_New.Enabled = false; //新增
                        PublicForms.LineDelayRecord.Btn_Edit.Enabled = false; //修改
                        PublicForms.LineDelayRecord.Btn_Delete.Enabled = false; //刪除
                        #endregion
                        break;
                    case "Frm_5_2_LineDelayCode":
                        //todo 比照CPL
                        break;
                    case "Frm_5_3_UserSetup":
                        #region '人員設定'
                        PublicForms.UserSetup.Btn_New.Enabled = false; //新增
                        PublicForms.UserSetup.Btn_Edit.Enabled = false; //修改
                        PublicForms.UserSetup.Btn_Delete.Enabled = false; //刪除
                        #endregion
                        break;
                    case "frm_5_4_CrewMaintainance":
                        #region '排班'
                        PublicForms.CrewMaintenance.Btn_ArrangeCrew.Enabled = false; //排班
                        #endregion
                        break;
                    case "frm_5_5_Language":
                        #region '中英文'
                        PublicForms.Language.Btn_Edit.Enabled = false; //修改
                        #endregion
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 帳號清單
        /// </summary>
        public void UserID_List(ComboBox cboUser)
        {
            DataTable dtUserList = new DataTable();
            string strSql = SqlFactory.SelectUserList();

            try
            {
                dtUserList = Data_Access.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                EventLogHandler.Instance.LogInfo("0-1", $"账号清单查询资料库", $"账号清单查询资料库成功");
                PublicComm.ClientLog.Info($"訊息名稱:帳號清單查詢資料庫 訊息:帳號清單查詢資料庫成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"信息名称:账号清单查询资料库 信息:账号清单查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("0-1", $"账号清单查询资料库", $"账号清单查询资料库失败:{ex}");
                PublicComm.ClientLog.Info($"訊息名稱:帳號清單查詢資料庫 訊息:帳號清單查询资料库失败:{ex}");
            }

            if (dtUserList == null) return;
            if (dtUserList.Rows.Count.Equals(0)) return;

            cboUser.DisplayMember = nameof(TBL_AuthorityData.User_ID);
            cboUser.ValueMember = nameof(TBL_AuthorityData.User_ID);
            cboUser.DataSource = dtUserList;
        }

        /// <summary>
        /// 畫面可讀不可寫設定
        /// </summary>
        /// <param name="ControlList"></param>
        /// <param name="FrameFuntion"></param>
        /// <param name="AuthorityClass"></param>
        public void Fun_SetControlAuthority(Control[] ControlList, string FrameFuntion, Form Form = null)
        {
            bool bolEnable = true;
            if (FrameFuntion.Equals("R"))
            {
                bolEnable = false;
                if (PublicForms.Tracking != null && Form != null)
                    if (Form.Name.Equals("frm_2_1_Tracking"))
                    {
                        if (Frm_2_2.Equals("N"))
                            PublicForms.Tracking.Btn_LinkFrm_2_2.Enabled = false;
                        PublicForms.Tracking.AckPDIToolStripMenuItem.Enabled = bolEnable;
                    }
            }
            foreach (Control ctr in ControlList)
            {
                ctr.Enabled = bolEnable;
            }
        }

    }
}
