using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using ExcelDataReader;
using GPLManager.Util;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_1_1_PDISchl : Form
    {
        #region 變數
        /// <summary>
        /// 鋼卷排程資訊
        /// </summary>
        public DataTable dtPdiSchl;

        DataTable dt_ForSearch = new DataTable();
        DataTable dt_cob = new DataTable();
        DataTable dt_Seq = new DataTable();
        //DataTable dtUpdateTime = new DataTable();
        DataTable dtGetDM = new DataTable();

        int intTopCanNotMove = 3;
        public DataTable dtSelected = new DataTable();
        public DataTable dtCopy = new DataTable();
        string strStartChangeTime = string.Empty ;
        //語系
        private LanguageHandler LanguageHand;
        DataTable dtLangSwitch_Column;

        #endregion

        public Frm_1_1_PDISchl()
        {
            InitializeComponent();
        }
        private void Frm_1_1_PDISchl_Load(object sender, EventArgs e)
        {
            LanguageHandler.Instance.Current();

            PublicForms.PDISchl = this;
            Control[] Frm_1_1_Control = new Control[] {
            Btn_First,//第一位
            Btn_Up,//上一位
            Btn_Last,//最後一位
            Btn_Down,//下一位
            Btn_MovePdi,//確定排程
            Btn_OutSchedule,//刪除單捲排程
            Btn_ImportPDI,//匯入PDI
            Btn_ImportSchedule,//匯入排程
            Btn_RequestPDI,//要求PDI
            Btn_NewSchdl,//要求排程刷新
            Btn_InsertDummy,//插入過渡卷
            Btn_DelDummy,//過度捲刪除
            Btn_RequestDummy //過度捲要求
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_1_Control, UserSetupHandler.Instance.Frm_1_1);
            Btn_MovePdi.Enabled = false;

            //鋼卷排程資訊
            try
            {
                Fun_InitialDataGridView();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
            }

            //过渡卷资讯
            try
            {
                Fun_SearchDM();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"过渡卷资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"過渡卷資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"過渡卷資訊查詢資料庫失敗:{ex}");
            }

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        /// <summary>
        /// 从其他画面切回刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_1_1_PDISchl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                Fun_ReLoad();
        }

        private void Frm_1_1_PDISchl_Shown(object sender, EventArgs e)
        {          
            //LanguageHandler.Instance.Fun_FindControl();
        }

       
        #region DGV資料

        #region For Dgv_Info

        /// <summary>
        /// DGV資料
        /// </summary>
        private void Fun_InitialDataGridView()
        {
            if (!Fun_TopScheduleLock())
                intTopCanNotMove = 0;
            else
                intTopCanNotMove = 3;

            string strSql;//= string.Empty

            //dt = new DataTable();

            //Dgv_Info.DataSource = dt;

            #region 鋼卷排程資訊SQL
            strSql = SqlFactory.Frm_1_1_SelectSchedule_InitialDataGridView_DB_Schedule_PDI();
            dtPdiSchl = Data_Access.Fun_SelectDate(strSql,GlobalVariableHandler.Instance.strConn_GPL,"钢卷排程","1-1");
            #endregion

            //鋼卷數量統計
            Txt_Count_ForSchedule.Text = dtPdiSchl != null ? dtPdiSchl.Rows.Count.ToString() : "0";

            #region 紀錄原有Seq_No
            strSql = SqlFactory.Frm_1_1_SelectSchedule_SeqNoRecords_DB_Schedule();
            dt_Seq = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"记录顺序号", "1-1");
            #endregion

            #region 记录更新时间
            //strSql = SqlFactory.Frm_1_1_SelectSchedule_UpdateTimeRecords_DB_Schedule();
            //dtUpdateTime = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"记录钢卷排程时间", "1-1");
            #endregion

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dtPdiSchl);
            DGVColumnsHandler.Instance.Frm_1_1PDISchedule_Dgv_ScheduleColumns(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);
           
            Dgv_Info.ClearSelection();

            PublicComm.ClientLog.Info($"DataGridView排程清單");

            if (dt_Seq.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }
            //if (dtUpdateTime.IsNull())
            //{
            //    //EventLogHandler.Instance.EventPush_Message($"无排程");
            //    return;
            //}           
        }

        private void Dgv_Info_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dtPdiSchl.IsNull())
            {
                //EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }
            if (Dgv_Info.DgvIsNull())
            {
                //EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            for (int RowIndex = 0; RowIndex < dtPdiSchl.Rows.Count; RowIndex++)
            {
                if (RowIndex <= intTopCanNotMove - 1)
                {
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.DimGray;
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    //Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = dt.Rows[RowIndex][0].ToString() == null || dt.Rows[RowIndex][0].ToString().IsEmpty() ? Color.Gold : Color.White;
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Dgv_Info.Rows[RowIndex].Cells[nameof(PDIEntity.TBL_PDI.Plan_No)].Value.ToString().IsEmpty() ? Color.FromArgb(255, 90, 82) : Color.White;
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                //Dgv_Info.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        #endregion

        #region For Dgv_Search

        private void Fun_SearchDataGridView()
        {
            var SelectCheckStatus = new StringBuilder();
            SelectCheckStatus.Append("附加搜寻条件:");
            //查詢生產完成
            if (Chk_Status.Checked)
            {
                SelectCheckStatus.Append(" 生产完成 ");
            }
            //查詢已上線
            else if (Chk_Online.Checked)
            {
                SelectCheckStatus.Append(" 已上线 ");
            }
            //查詢未上线 
            else if (Chk_NotOnline.Checked)
            {
                SelectCheckStatus.Append(" 未上线  ");
            }
            else
            {
                SelectCheckStatus.Append(" 无 ");
            }


            EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}執行查詢", $"{SelectCheckStatus}");
            string strSql = SqlFactory.Frm_1_1_CoilSearch_DataGridView_DB_PDI_Map();
            //查詢生產完成
            if (Chk_Status.Checked)
            {
                strSql += $@" From  [{nameof(PDIEntity.TBL_PDI)}] a
                              LEFT JOIN [{nameof(PDOEntity.TBL_PDO)}] b on a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ";
            }
            //查詢已上線
            else if (Chk_Online.Checked)
            {
                strSql += $@" From  [{nameof(CoilMapEntity.TBL_CoilMap)}]  b 
                              Left join  [{nameof(PDIEntity.TBL_PDI)}] a on  a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)}]
                              Or a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)}]
                              Or a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.POR)}]
                              Or a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01)}]
                              Or a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02)}] 
                              Or a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP)}] 
                              Or a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.TR)}] ";
            } 
            //查詢未上线 
            else if (Chk_NotOnline.Checked)
            {
                strSql += $@" From  [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] s
                              LEFT JOIN  [{nameof(PDIEntity.TBL_PDI)}] a on a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = s.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] ";
            }
            else
            {
                strSql += $" From  [{nameof(PDIEntity.TBL_PDI)}] a";
            }

            strSql += $" Where  a.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] IS NOT NULL AND  a.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] != '' ";

            if (Chk_Plan_No.Checked)
            {
                strSql += $" And a.[{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{Cob_Plan_No.Text}'"; //單選計畫號
            }

            if (Chk_Entry_Coil_No.Checked)
            {
                strSql += $" And a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] ='{Cob_Entry_Coil_No.Text}'"; //單選入口鋼卷號
            }


            dt_ForSearch = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "钢卷资讯查询", "1-1");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Search, dt_ForSearch);
            DGVColumnsHandler.Instance.Frm_1_1PDISchedule_Dgv_PDISearchColumns(Dgv_Search);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Search);

            //鋼卷數量統計
            Txt_Count_ForSearch.Text = dt_ForSearch.Rows.Count.ToString();
        }

        #region CheckBox 單選
        /// <summary>
        /// 未上線狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_NotOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_NotOnline.Checked)
            {
                Chk_Status.Checked = false;
                Chk_Online.Checked = false;
            }
        }

        /// <summary>
        /// 已上線狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Online_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Online.Checked)
            {
                Chk_Status.Checked = false;
                Chk_NotOnline.Checked = false;
            }
        }

        /// <summary>
        /// 已生產狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Status_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Status.Checked)
            {
                Chk_Online.Checked = false;
                Chk_NotOnline.Checked = false;
            }
        }

        #endregion

        #endregion

        #region For DGV_DM

        private void Fun_SearchDM(bool bolSearch = false)
        {
            string strSql = SqlFactory.Frm_1_1_SelectDM_DB_PDI(bolSearch);
            dtGetDM = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"过渡卷清单", "1-1");

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_DM, dtGetDM);
            DGVColumnsHandler.Instance.Frm_1_1PDISchedule_Dgv_DMColumns(Dgv_DM);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_DM);

            //鋼卷數量統計
            Txt_Count_ForDummy.Text = dtGetDM.Rows.Count.ToString();
        }

        #endregion

        #endregion

        #region ComboBox
      
        /// <summary>
        /// ComboBox Items List
        /// </summary>
        /// <param name="cobName"></param>
        /// <param name="CboDt"></param>
        private void ComboBoxItems(string cobName, DataTable CboDt)
        {
            switch (cobName)
            {
                case nameof(Cob_Plan_No):
                    Cob_Plan_No.DisplayMember = nameof(PDIEntity.TBL_PDI.Plan_No);
                    Cob_Plan_No.ValueMember = nameof(PDIEntity.TBL_PDI.Plan_No);
                    Cob_Plan_No.DataSource = CboDt;
                    break;
                case nameof(Cob_Entry_Coil_No_Dummy):
                    Cob_Entry_Coil_No_Dummy.DisplayMember = nameof(PDIEntity.TBL_PDI.In_Coil_ID);
                    Cob_Entry_Coil_No_Dummy.ValueMember = nameof(PDIEntity.TBL_PDI.In_Coil_ID);
                    Cob_Entry_Coil_No_Dummy.DataSource = CboDt;
                    break;
                case nameof(Cob_Entry_Coil_No):
                    Cob_Entry_Coil_No.DisplayMember = nameof(PDIEntity.TBL_PDI.In_Coil_ID);
                    Cob_Entry_Coil_No.ValueMember = nameof(PDIEntity.TBL_PDI.In_Coil_ID);
                    Cob_Entry_Coil_No.DataSource = CboDt;
                    break;
            }
        }
        #endregion

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            ////檢查搜尋條件
            //if (Ckb_Plan_No.Checked == false && Ckb_Entry_Coil_No.Checked == false) 
            //{ 
            //    EventLogHandler.Instance.EventPush_Message($"请选择要搜寻之作业计划或入口卷号");
            //}
            //else if (Ckb_Plan_No.Checked && Cob_Plan_No.Text.IsEmpty()) 
            //{ 
            //    EventLogHandler.Instance.EventPush_Message($"请选择要搜寻的作业计划"); 
            //}
            //else if (Ckb_Entry_Coil_No.Checked && Cob_Entry_Coil_No.Text.IsEmpty()) 
            //{ 
            //    EventLogHandler.Instance.EventPush_Message($"请选择要搜寻的入口卷号"); 
            //}
            //else 
            //{ 
            Fun_SearchDataGridView();
            //}
        }

        /// <summary>
        /// 删除排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OutSchedule_Click(object sender, EventArgs e)
        {
            
            if (Dgv_Info.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程可删除");
                return;
            }
            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取欲单卷删除之排程");
                return;
            }

            if (Dgv_Info.CurrentRow.Index <= intTopCanNotMove - 1)
            {
                EventLogHandler.Instance.EventPush_Message($"前三笔排程禁止单卷删除");
                return;
            }

            string strDelCoil = Dgv_Info.CurrentRow.Cells["Coil_ID"].Value.ToString().Trim();
            string strMessage = $"删除{Dgv_Info.CurrentRow.Cells["Coil_ID"].Value.ToString().Trim()}之排程";


            Frm_Reason fzr = new Frm_Reason();
            fzr.Fun_CatchTitle("删除单卷排程", strMessage, "请选择删除原因");
            fzr.ShowDialog();
            fzr.Dispose();

            if (fzr.DialogResult == DialogResult.OK)
            {              
                Frm_0_0_Main frm_0_0 = new Frm_0_0_Main();
                SCCommMsg.CS03_ScheduleChange Msg = new SCCommMsg.CS03_ScheduleChange
                {
                    Source = "GPL_HMI",
                    SchStatus = SCCommMsg.ScheduleStatus.DELETE,
                    EntryCoilID = strDelCoil,
                    OperatorID = PublicForms.Main.lblLoginUser.Text.Trim(),// frm_0_0.lblLoginUser.Text.Trim(),
                    ReasonCode = fzr.StrReasonCode
                };

                PublicComm.Client.Tell(Msg);              

                string strLogInfo = $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 要求排程单卷删除";
                string strLogInfo2 = $"已通知Server删除[{strDelCoil}]单卷排程，删除原因[{ fzr.StrReasonCode}]{fzr.StrReason}";

                EventLogHandler.Instance.LogInfo("1-1", strLogInfo, $"{strLogInfo},{strLogInfo2}");
                //EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}删除单卷排程 钢卷编号:{strDelCoil}排程", $"删除单卷排程钢卷:{strDelCoil}原因代码:{fzr.StrReasonCode}原因:{fzr.StrReason}");
                //EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text} 通知Server:排程单卷删除", $"钢卷编号:{strDelCoil} 人员:{PublicForms.Main.lblLoginUser.Text.Trim()} 原因代码: {fzr.StrReasonCode} 原因:{fzr.StrReason}");
                ////pnl_DelSch.Visible = false;

                EventLogHandler.Instance.EventPush_Message(strLogInfo2);
                PublicComm.ClientLog.Info(strLogInfo2);
                PublicComm.akkaLog.Info(strLogInfo2);

                //EventLogHandler.Instance.EventPush_Message($"已通知Server删除[{strDelCoil}]单卷排程，删除原因[{ fzr.StrReasonCode}]{fzr.StrReason}");
                //PublicComm.ClientLog.Info($"已通知Server删除[{strDelCoil}]单卷排程，删除原因[{ fzr.StrReasonCode}]{fzr.StrReason}");
                //PublicComm.akkaLog.Info($"已通知Server删除[{strDelCoil}]单卷排程，删除原因[{ fzr.StrReasonCode}]{fzr.StrReason}");
                ////PublicComm.ClientLog.Info($"通知Server排程单卷删除");
                ////PublicComm.akkaLog.Info($"通知Server排程单卷删除");
                ///

                //鋼卷排程資訊
                try
                {
                    Fun_InitialDataGridView();
                }
                catch (Exception ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                    EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                }
            }
        }

      

        private void Tab_PDI_Control_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_PDI_Control, e);
        }

        private void Btn_InsertDummy_Click(object sender, EventArgs e)
        {
            DataRow dr;
            string strCoil_ID;

            //如果排程已鎖前三顆,先檢查排程是否有三顆以上
            //如果 排程只有1-2顆,就跳提示,請使用者先要求新排程(新排程沒有鎖定前三顆就可以插入過度捲)
            //
            if (intTopCanNotMove == 3)
            {
                if (dtPdiSchl.Rows.Count < intTopCanNotMove && dtPdiSchl.Rows.Count > 0)
                {
                    EventLogHandler.Instance.EventPush_Message($"排程已鎖定前三顆，请先要求新排程后，再插入过度卷！");
                    return;
                }
            }

           //排程沒鎖定
            if (dtPdiSchl.Rows.Count > 0)
            {
                //取得插入位置
                if (Dgv_Info.SelectedRows.Count <= 0)
                    dr = dtPdiSchl.Rows[Dgv_Info.CurrentRow.Index];
                else
                    dr = Fun_GetDataRowFromCurrentRow(Dgv_Info, dtPdiSchl);

                strCoil_ID = dr[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString().Trim();
            }
            else
            {
                //空排程
                strCoil_ID = "";
            }                                     

            PublicForms._Dummy = new Frm_Dummy();
            PublicForms._Dummy.FunGetDataTable(dtPdiSchl, strCoil_ID);
            PublicForms._Dummy.ShowDialog();
        }

        #region 排程異動

        #region 排序
        /// <summary>
        /// 第一筆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_First_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Top);
        }
        /// <summary>
        /// 往上一筆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Up_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Up);
        }
        /// <summary>
        /// 往下一筆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Down_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Down);
        }
        /// <summary>
        /// 最後一筆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Last_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Bottom);
        }
        #endregion

        /// <summary>
        /// 檢查是否可以調整前3顆排程
        /// </summary>
        /// <returns>false = 0:未鎖 ; true = 1:鎖定  </returns>
        private bool Fun_TopScheduleLock()
        {
            bool bolLock = true;
            string strSql = SqlFactory.Frm_1_1_SQL_Select_SystemSetting_TopScheduleLock();
            DataTable dtTopLock = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "TopScheduleLock");
            if (dtTopLock != null && dtTopLock.Rows.Count > 0)
            {
                bolLock = dtTopLock.Rows[0]["Value"].ToString().Trim() == "0" ? false : true;
            }
            return bolLock;
        }
        private void Fun_ScheduleMove(int move)
        {
            if (!Fun_TopScheduleLock())
                intTopCanNotMove = 0;
            else
                intTopCanNotMove = 3;

            if (Dgv_Info.CurrentRow.Index > intTopCanNotMove - 1)
            {
                //Fun_RecordSelected(Dgv_Info);
                Fun_SortingAlgorithm(move);
            }
            else if (Dgv_Info.CurrentRow.Index <= intTopCanNotMove - 1)
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷排程前三笔无法更动!");
                PublicComm.ClientLog.Info($"排程調整，鋼卷號:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value}為排程前三筆，禁止異動!");
                return;
            }
           
        }

        /// <summary>
        /// 0:移至第一筆 / 1:往上一筆 / 2:往下一筆 / 3:移至最後一筆
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Dgv_Info"></param>
        /// <param name="dt"></param>
        public void Fun_SortingAlgorithm(int Number)
        {
            //int Rowsindex = 0;

            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            //調整排程前,紀錄當下時間
            if (string.IsNullOrEmpty(strStartChangeTime) )
                strStartChangeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            dtCopy = dtPdiSchl.Clone();

            switch (Number)
            {
                //移至最上面
                case 0:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        Fun_ScheduleFirst();
                        #region Old

                        //if (dtSelected.Rows.Count.Equals(1))
                        //{
                        //    Rowsindex = Dgv_Info.CurrentRow.Index;
                        //    if (Rowsindex <= 2) { return; }//前三筆不能異動

                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;//先把要置頂的列 存起來
                        //    for (int intN = Rowsindex; intN >= 3; intN--)
                        //        dt.Rows[intN].ItemArray = dt.Rows[intN - 1].ItemArray;
                        //    dt.Rows[3].ItemArray = _rowData;
                        //    Dgv_Info.CurrentCell = Dgv_Info[Dgv_Info.CurrentCell.ColumnIndex, 3];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]移动至第四位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value}移动至第四位");
                        //}
                        //else
                        //{
                        //    //如果群組內包含第一位~第三位就跳出
                        //    if (Dgv_Info.Rows[0].Selected) return;
                        //    else if (Dgv_Info.Rows[1].Selected) return;
                        //    else if (Dgv_Info.Rows[2].Selected) return;
                        //    dtCopy.ImportRow(dt.Rows[0]);
                        //    dtCopy.ImportRow(dt.Rows[1]);
                        //    dtCopy.ImportRow(dt.Rows[2]);
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Selected)
                        //        {
                        //            dtCopy.ImportRow(dt.Rows[i]);
                        //        }
                        //    }
                        //    for (int i = 3; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Selected.Equals(false)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }

                        //    Dgv_Info.DataSource = dtCopy;
                        //}
                        #endregion
                    }
                    break;
                //往上移一位
                case 1:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        Fun_ScheduleIndexUp();
                        #region Old

                        //if (dtSelected.Rows.Count == 1)
                        //{
                        //    Rowsindex = Dgv_Info.CurrentRow.Index;
                        //    if (Rowsindex <= 2) { return; }//前三筆不能異動

                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;
                        //    if (Rowsindex - 1 <= 2)//防止非前三筆的資料行可上移
                        //    {
                        //        EventLogHandler.Instance.EventPush_Message($"排程前三笔禁止异动");
                        //        return;
                        //    }
                        //    dt.Rows[Rowsindex].ItemArray = dt.Rows[Rowsindex - 1].ItemArray;
                        //    dt.Rows[Rowsindex - 1].ItemArray = _rowData;
                        //    Dgv_Info.CurrentCell = Dgv_Info[Dgv_Info.CurrentCell.ColumnIndex, Dgv_Info.CurrentCell.RowIndex - 1];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]往上移动一位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}往上移动一位");
                        //}
                        //else
                        //{
                        //    //如果群組內包含第一位~第三位就跳出
                        //    if (Dgv_Info.Rows[0].Selected) return;
                        //    else if (Dgv_Info.Rows[1].Selected) return;
                        //    else if (Dgv_Info.Rows[2].Selected) return;
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Selected)
                        //        {
                        //            Rowsindex = Dgv_Info.Rows[i].Index;
                        //            break;
                        //        }
                        //    }
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Index == Rowsindex)
                        //        {
                        //            for (int j = 0; j < dt.Rows.Count; j++)
                        //            {
                        //                if (Dgv_Info.Rows[j].Selected) dtCopy.ImportRow(dt.Rows[j]);
                        //            }
                        //            if (Rowsindex - 1 <= 2)//防止非前三筆的資料行可上移
                        //            {
                        //                EventLogHandler.Instance.EventPush_Message($"排程前三笔禁止异动");
                        //                return;
                        //            }
                        //            dtCopy.ImportRow(dt.Rows[Rowsindex - 1]);
                        //        }
                        //        else if (Dgv_Info.Rows[i].Selected.Equals(false) && !Dgv_Info.Rows[i].Index.Equals(Rowsindex - 1)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }

                        //    Dgv_Info.DataSource = dtCopy;
                        //}
                        #endregion
                    }

                    break;
                //往下移一位
                case 2:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        Fun_ScheduleIndexDown();
                        #region Old
                        //if (dtSelected.Rows.Count == 1)
                        //{
                        //    Rowsindex = Dgv_Info.CurrentRow.Index;
                        //    if (Rowsindex.Equals(dt.Rows.Count - 1)) { return; }
                        //    if (Rowsindex <= 2)//前三筆不能異動
                        //    {
                        //        return;
                        //    }
                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;
                        //    dt.Rows[Rowsindex].ItemArray = dt.Rows[Rowsindex + 1].ItemArray;
                        //    dt.Rows[Rowsindex + 1].ItemArray = _rowData;
                        //    Dgv_Info.CurrentCell = Dgv_Info[Dgv_Info.CurrentCell.ColumnIndex, Dgv_Info.CurrentCell.RowIndex + 1];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]往下移动一位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}往下移动一位");
                        //}
                        //else
                        //{
                        //    //如果群組包含最後一位就跳出
                        //    if (Dgv_Info.Rows[dt.Rows.Count - 1].Selected) return;
                        //    //如果群組內包含第一位~第三位就跳出
                        //    if (Dgv_Info.Rows[0].Selected) return;
                        //    else if (Dgv_Info.Rows[1].Selected) return;
                        //    else if (Dgv_Info.Rows[2].Selected) return;

                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Selected)
                        //        {
                        //            Rowsindex = Dgv_Info.Rows[i].Index;
                        //            //break;
                        //        }
                        //    }
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Index.Equals(Rowsindex + 1))
                        //        {
                        //            dtCopy.ImportRow(dt.Rows[Rowsindex + 1]);
                        //            for (int j = 0; j < dt.Rows.Count; j++)
                        //            {
                        //                if (Dgv_Info.Rows[j].Selected) dtCopy.ImportRow(dt.Rows[j]);
                        //            }
                        //        }
                        //        else if (Dgv_Info.Rows[i].Selected.Equals(false) && !Dgv_Info.Rows[i].Index.Equals(Rowsindex + 1)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }

                        //    Dgv_Info.DataSource = dtCopy;
                        //}
                        #endregion
                    }

                    break;
                //移至最下面
                case 3:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        Fun_ScheduleIndexLast();
                        #region Old
                        //if (dtSelected.Rows.Count.Equals(1))
                        //{
                        //    Int32 intLast = Dgv_Info.Rows.Count - 1;
                        //    Rowsindex = Dgv_Info.CurrentRow.Index;
                        //    if (Rowsindex.Equals(dt.Rows.Count - 1)) { return; }
                        //    if (Rowsindex <= 2) { return; }//前三筆不能異動
                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;//先把要置底的列 存起來
                        //    for (int intN = Rowsindex; intN < intLast; intN++)
                        //        dt.Rows[intN].ItemArray = dt.Rows[intN + 1].ItemArray;

                        //    dt.Rows[intLast].ItemArray = _rowData;
                        //    Dgv_Info.CurrentCell = Dgv_Info[Dgv_Info.CurrentCell.ColumnIndex, intLast];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]移动至最后一位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}移动至最后一位");
                        //}
                        //else
                        //{
                        //    if (Dgv_Info.Rows[0].Selected) return;
                        //    else if (Dgv_Info.Rows[1].Selected) return;
                        //    else if (Dgv_Info.Rows[2].Selected) return;
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Selected.Equals(false)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_Info.Rows[i].Selected) dtCopy.ImportRow(dt.Rows[i]);
                        //    }
                        //    Dgv_Info.DataSource = dtCopy;
                        //}
                        #endregion
                    }

                    break;
                default:
                    return;
            }

            //Btn_ReLoad.Enabled = false;
            Btn_OutSchedule.Enabled = false;
            Btn_ImportSchedule.Enabled = false;
            Btn_ImportPDI.Enabled = false;
            Btn_MovePdi.Enabled = true;
            Btn_CancelMove.Visible = true;

            if (dtCopy.Rows.Count != 0)
                dtPdiSchl = dtCopy;
        }

        /// <summary>
        /// 移至第一位
        /// </summary>
        private void Fun_ScheduleFirst()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            try
            {
                //---  從上到下掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
                for (int RowIndex = 0; RowIndex < Dgv_Info.Rows.Count; RowIndex++)
                {
                    if (Dgv_Info.Rows[RowIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (RowIndex <= intTopCanNotMove - 1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            try
            {
                //---  將選取的鋼捲號碼移至最前面  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
                {
                    drGetRow = dtPdiSchl.NewRow();
                    int nSripIndex = SelectedCoilIndexList[nCount];
                    drGetRow.ItemArray = dtPdiSchl.Rows[nSripIndex].ItemArray;
                    dtPdiSchl.Rows.RemoveAt(nSripIndex);
                    dtPdiSchl.Rows.InsertAt(drGetRow, intTopCanNotMove + nCount);
                    drGetRow = null;
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_Info.ClearSelection();

            //---  上移的鋼捲號碼改為選取狀態  ---//
            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                Dgv_Info.Rows[intTopCanNotMove + nCount].Selected = true;
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_Info.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_Info.SelectedRows[0].Index;

                Dgv_Info.FirstDisplayedScrollingRowIndex = 0;

            }

        }

        /// <summary>
        /// 往上移一位
        /// </summary>
        private void Fun_ScheduleIndexUp()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            try
            {
                //---  從上到下掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
                for (int RowIndex = 0; RowIndex < Dgv_Info.Rows.Count; RowIndex++)
                {
                    if (Dgv_Info.Rows[RowIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (RowIndex <= intTopCanNotMove - 1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            try
            {
                //---  將選取的鋼捲號碼往上移一格  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
                {
                    drGetRow = dtPdiSchl.NewRow();
                    int nSripIndex = SelectedCoilIndexList[nCount];

                    if (nSripIndex - 1 <= intTopCanNotMove - 1)
                    {
                        EventLogHandler.Instance.EventPush_Message($"排程禁止异动至前三笔");
                        break;
                    }

                    drGetRow.ItemArray = dtPdiSchl.Rows[nSripIndex].ItemArray;
                    dtPdiSchl.Rows.RemoveAt(nSripIndex);
                    dtPdiSchl.Rows.InsertAt(drGetRow, nSripIndex - 1);
                    drGetRow = null;
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_Info.ClearSelection();

            //---  上移的鋼捲號碼改為選取狀態  ---//
            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                if ((SelectedCoilIndexList[nCount] - 1) >= intTopCanNotMove)
                {
                    Dgv_Info.Rows[SelectedCoilIndexList[nCount] - 1].Selected = true;
                }

            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_Info.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_Info.SelectedRows[0].Index;
                //int nShowRowPage = 14;
                int intNowScrollRowIndex = Dgv_Info.FirstDisplayedScrollingRowIndex;

                if (nSelRowIndex <= intNowScrollRowIndex + 1)
                {
                    if (intNowScrollRowIndex == 0)
                        Dgv_Info.FirstDisplayedScrollingRowIndex = intNowScrollRowIndex;
                    else
                        Dgv_Info.FirstDisplayedScrollingRowIndex = intNowScrollRowIndex - 1;
                }
                else
                {
                    Dgv_Info.FirstDisplayedScrollingRowIndex = intNowScrollRowIndex;
                }
            }

        }

        /// <summary>
        /// 往下移一位
        /// </summary>
        private void Fun_ScheduleIndexDown()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            //---  從下到上掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
            try
            {
                for (int nIndex = Dgv_Info.Rows.Count - 1; nIndex > -1; nIndex--)
                {
                    if (Dgv_Info.Rows[nIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (nIndex <= intTopCanNotMove - 1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(nIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            try
            {
                //---  將選取的鋼捲號碼往下移一格  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
                {
                    drGetRow = dtPdiSchl.NewRow();
                    int nSripIndex = SelectedCoilIndexList[nCount];

                    if (nSripIndex + 1 >= Dgv_Info.Rows.Count)
                    {
                        EventLogHandler.Instance.EventPush_Message($"排程异动已至底");
                        break;
                    }


                    drGetRow.ItemArray = dtPdiSchl.Rows[nSripIndex].ItemArray;
                    dtPdiSchl.Rows.RemoveAt(nSripIndex);
                    dtPdiSchl.Rows.InsertAt(drGetRow, nSripIndex + 1);
                    drGetRow = null;
                }

            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_Info.ClearSelection();

            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                if ((SelectedCoilIndexList[nCount] + 1) >= Dgv_Info.Rows.Count)
                {
                    continue;
                }
                Dgv_Info.Rows[SelectedCoilIndexList[nCount] + 1].Selected = true;
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_Info.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_Info.SelectedRows[0].Index;
                int nShowRowPerPage = 25;

                if (nSelRowIndex + SelectedCoilIndexList.Count >= nShowRowPerPage)
                {
                    Dgv_Info.FirstDisplayedScrollingRowIndex = Dgv_Info.FirstDisplayedScrollingRowIndex + 1;
                }
                else
                {
                    Dgv_Info.FirstDisplayedScrollingRowIndex = Dgv_Info.FirstDisplayedScrollingRowIndex;
                }
            }
        }

        /// <summary>
        /// 移至最後一位
        /// </summary>
        private void Fun_ScheduleIndexLast()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            //---  從下到上掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
            #region 检查前三筆不能異動 ---重复
            try
            {
                for (int nIndex = Dgv_Info.Rows.Count - 1; nIndex > -1; nIndex--)
                {
                    if (Dgv_Info.Rows[nIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (nIndex <= intTopCanNotMove - 1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(nIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }
            #endregion

            try
            {
                //---  將選取的鋼捲號碼移至最後  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
                {
                    drGetRow = dtPdiSchl.NewRow();

                    int nSripIndex = SelectedCoilIndexList[nCount];

                    if (nSripIndex >= Dgv_Info.Rows.Count - SelectedCoilIndexList.Count)
                    {
                        EventLogHandler.Instance.EventPush_Message($"排程异动已至底");
                        break;
                    }

                    drGetRow.ItemArray = dtPdiSchl.Rows[nSripIndex].ItemArray;
                    dtPdiSchl.Rows.RemoveAt(nSripIndex);
                    dtPdiSchl.Rows.InsertAt(drGetRow, Dgv_Info.Rows.Count - nCount);
                    drGetRow = null;
                }

            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_Info.ClearSelection();

            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                Dgv_Info.Rows[Dgv_Info.Rows.Count - 1 - nCount].Selected = true;
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_Info.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_Info.SelectedRows[0].Index;
                int nShowRowPerPage = 14;

                if (nSelRowIndex >= nShowRowPerPage)
                {
                    Dgv_Info.FirstDisplayedScrollingRowIndex = nSelRowIndex - nShowRowPerPage;
                }
            }
        }

        /// <summary>
        /// 紀錄選取行
        /// </summary>
        public DataTable Dt_RecordSelected()
        {
            DataTable dtSelected = dtPdiSchl.Clone();

            for (int i = 0; i < Dgv_Info.RowCount; i++)
            {
                if (Dgv_Info.Rows[i].Selected) dtSelected.ImportRow(dtPdiSchl.Rows[i]);
            }

            return dtSelected;
        }

        /// <summary>
        /// 排序邏輯，0:移至第一筆 / 1:往上一筆 / 2:往下一筆 / 3:移至最後一筆
        /// </summary>
        private void Fun_JumpToRecord(int Number, DataTable dtSelected)
        {
            DataTable dtCopy = new DataTable();

            int Rowsindex = 0;

            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            dtCopy = dtPdiSchl.Clone();

            switch (Number)
            {
                //移至最上面
                case 0:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        if (dtSelected.Rows.Count == 1)
                        {
                            Rowsindex = Dgv_Info.CurrentRow.Index;

                            if (Rowsindex <= 2) { return; }//前三筆不能異動

                            object[] _rowData = dtPdiSchl.Rows[Rowsindex].ItemArray;//先把要置頂的列 存起來

                            for (int intN = Rowsindex; intN >= 3; intN--)
                                dtPdiSchl.Rows[intN].ItemArray = dtPdiSchl.Rows[intN - 1].ItemArray;

                            dtPdiSchl.Rows[3].ItemArray = _rowData;

                            Dgv_Info.ClearSelection();
                            Dgv_Info.Rows[3].Selected = true;
                            Dgv_Info.CurrentCell = Dgv_Info.Rows[3].Cells[0];

                            EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]移动至第四位");
                            PublicComm.ClientLog.Info($"排程調整，鋼卷號:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}移動至第四位");
                        }
                        else
                        {
                            //如果群組內包含第一位~第三位就跳出
                            if (Dgv_Info.Rows[0].Selected) return;
                            else if (Dgv_Info.Rows[1].Selected) return;
                            else if (Dgv_Info.Rows[2].Selected) return;
                            dtCopy.ImportRow(dtPdiSchl.Rows[0]);
                            dtCopy.ImportRow(dtPdiSchl.Rows[1]);
                            dtCopy.ImportRow(dtPdiSchl.Rows[2]);

                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Selected)
                                {
                                    dtCopy.ImportRow(dtPdiSchl.Rows[i]);
                                }
                            }

                            for (int i = 3; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Selected == false) dtCopy.ImportRow(dtPdiSchl.Rows[i]);
                            }

                            Dgv_Info.DataSource = dtCopy;
                            EventLogHandler.Instance.EventPush_Message($"群组移动至第四位");
                            PublicComm.ClientLog.Info($"排程調整，群組移動至第四位");
                        }
                    }
                    break;
                //往上移一位
                case 1:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        if (dtSelected.Rows.Count == 1)
                        {
                            Rowsindex = Dgv_Info.CurrentRow.Index;

                            if (Rowsindex <= 2) { return; }//前三筆不能異動

                            object[] _rowData = dtPdiSchl.Rows[Rowsindex].ItemArray;

                            if (Rowsindex - 1 <= 2)//防止非前三筆的資料行可上移
                            {
                                EventLogHandler.Instance.EventPush_Message($"排程前三笔禁止异动");
                                return;
                            }

                            dtPdiSchl.Rows[Rowsindex].ItemArray = dtPdiSchl.Rows[Rowsindex - 1].ItemArray;
                            dtPdiSchl.Rows[Rowsindex - 1].ItemArray = _rowData;

                            Dgv_Info.ClearSelection();
                            Dgv_Info.Rows[Rowsindex-1].Selected = true;
                            Dgv_Info.CurrentCell = Dgv_Info.Rows[Rowsindex - 1].Cells[0];

                            EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]往上移动一位");
                            PublicComm.ClientLog.Info($"排程調整，鋼卷號:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}往上移動一位");
                        }
                        else
                        {
                            //如果群組內包含第一位~第三位就跳出
                            if (Dgv_Info.Rows[0].Selected) return;
                            else if (Dgv_Info.Rows[1].Selected) return;
                            else if (Dgv_Info.Rows[2].Selected) return;

                            //找出群组第一位
                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Selected)
                                {
                                    Rowsindex = Dgv_Info.Rows[i].Index;
                                    break;
                                }
                            }

                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Index == Rowsindex)
                                {
                                    for (int j = 0; j < dtPdiSchl.Rows.Count; j++)
                                    {
                                        if (Dgv_Info.Rows[j].Selected)
                                            dtCopy.ImportRow(dtPdiSchl.Rows[j]);
                                    }
                                    if (Rowsindex - 1 <= 2)//防止非前三筆的資料行可上移
                                    {
                                        EventLogHandler.Instance.EventPush_Message($"排程前三笔禁止异动");
                                        return;
                                    }
                                    dtCopy.ImportRow(dtPdiSchl.Rows[Rowsindex - 1]);
                                }
                                else if (Dgv_Info.Rows[i].Selected == false && Dgv_Info.Rows[i].Index != Rowsindex - 1)
                                {
                                    dtCopy.ImportRow(dtPdiSchl.Rows[i]);
                                } 
                            }

                            Dgv_Info.DataSource = dtCopy;

                            EventLogHandler.Instance.EventPush_Message($"群组往上移动一位");
                            PublicComm.ClientLog.Info($"排程調整，群組往上移動一位");
                        }
                    }
                    break;
                //往下移一位
                case 2:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        if (dtSelected.Rows.Count == 1)
                        {
                            Rowsindex = Dgv_Info.CurrentRow.Index;

                            if (Rowsindex == dtPdiSchl.Rows.Count - 1) return;

                            if (Rowsindex <= 2)//前三筆不能異動
                            {
                                return;
                            }

                            object[] _rowData = dtPdiSchl.Rows[Rowsindex].ItemArray;
                            dtPdiSchl.Rows[Rowsindex].ItemArray = dtPdiSchl.Rows[Rowsindex + 1].ItemArray;
                            dtPdiSchl.Rows[Rowsindex + 1].ItemArray = _rowData;


                            Dgv_Info.ClearSelection();
                            Dgv_Info.Rows[Rowsindex + 1].Selected = true;
                            Dgv_Info.CurrentCell = Dgv_Info.Rows[Rowsindex + 1].Cells[0];

                            EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]往下移动一位");
                            PublicComm.ClientLog.Info($"排程調整，鋼卷號:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}往下移動一位");
                        }
                        else
                        {
                            //如果群組包含最後一位就跳出
                            if (Dgv_Info.Rows[dtPdiSchl.Rows.Count - 1].Selected) return;
                            //如果群組內包含第一位~第三位就跳出
                            if (Dgv_Info.Rows[0].Selected) return;
                            else if (Dgv_Info.Rows[1].Selected) return;
                            else if (Dgv_Info.Rows[2].Selected) return;

                            //找出群组第一位
                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Selected)
                                {
                                    Rowsindex = Dgv_Info.Rows[i].Index;
                                    //break;
                                }
                            }

                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Index == Rowsindex + 1)
                                {
                                    dtCopy.ImportRow(dtPdiSchl.Rows[Rowsindex + 1]);
                                    for (int j = 0; j < dtPdiSchl.Rows.Count; j++)
                                    {
                                        if (Dgv_Info.Rows[j].Selected) dtCopy.ImportRow(dtPdiSchl.Rows[j]);
                                    }
                                }
                                else if (Dgv_Info.Rows[i].Selected == false && Dgv_Info.Rows[i].Index != Rowsindex + 1) dtCopy.ImportRow(dtPdiSchl.Rows[i]);
                            }

                            Dgv_Info.DataSource = dtCopy;
                            EventLogHandler.Instance.EventPush_Message($"群组往下移动一位");
                            PublicComm.ClientLog.Info($"排程調整，群組往下移動一位");
                        }
                    }
                    break;
                //移至最下面
                case 3:
                    if (!Dgv_Info.CurrentIsNull())
                    {
                        if (dtSelected.Rows.Count == 1)
                        {
                            int intLast = Dgv_Info.Rows.Count - 1;
                            Rowsindex = Dgv_Info.CurrentRow.Index;

                            if (Rowsindex == dtPdiSchl.Rows.Count - 1) return;

                            if (Rowsindex <= 2) { return; }//前三筆不能異動

                            object[] _rowData = dtPdiSchl.Rows[Rowsindex].ItemArray;//先把要置底的列 存起來

                            for (int intN = Rowsindex; intN < intLast; intN++)
                                dtPdiSchl.Rows[intN].ItemArray = dtPdiSchl.Rows[intN + 1].ItemArray;

                            dtPdiSchl.Rows[intLast].ItemArray = _rowData;

                            Dgv_Info.ClearSelection();
                            Dgv_Info.Rows[intLast].Selected = true;
                            Dgv_Info.CurrentCell = Dgv_Info.Rows[intLast].Cells[0];

                            EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]移动至最后一位");
                            PublicComm.ClientLog.Info($"排程調整，鋼卷號:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}移動至最後一位");
                        }
                        else
                        {
                            if (Dgv_Info.Rows[0].Selected) return;
                            else if (Dgv_Info.Rows[1].Selected) return;
                            else if (Dgv_Info.Rows[2].Selected) return;

                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Selected == false) dtCopy.ImportRow(dtPdiSchl.Rows[i]);
                            }

                            for (int i = 0; i < dtPdiSchl.Rows.Count; i++)
                            {
                                if (Dgv_Info.Rows[i].Selected) dtCopy.ImportRow(dtPdiSchl.Rows[i]);
                            }

                            Dgv_Info.DataSource = dtCopy;
                            EventLogHandler.Instance.EventPush_Message($"群组往下移动一位");
                            PublicComm.ClientLog.Info($"排程調整，群組往下移動一位");
                        }
                    }
                    break;
                default:
                    return;
            }

            Fun_SetBottonEnabled(Btn_ReLoad, false);
            Fun_SetBottonEnabled(Btn_OutSchedule, false);
            Fun_SetBottonEnabled(Btn_ImportSchedule,false);
            Fun_SetBottonEnabled(Btn_ImportPDI,false);

            Btn_MovePdi.Enabled = true;
            Btn_CancelMove.Visible = true;
        }

        /// <summary>
        /// 取消調整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CancelMove_Click(object sender, EventArgs e)
        {
            PublicComm.ClientLog.Info($"取消調整動作");
            Btn_MovePdi.Enabled = false;
            Btn_CancelMove.Visible = false;

            //清空調整排程時紀錄的當下時間
            strStartChangeTime = string.Empty;

            Fun_SetBottonEnabled(Btn_ReLoad, true);
            Fun_SetBottonEnabled(Btn_OutSchedule, true);
            Fun_SetBottonEnabled(Btn_ImportSchedule, true);
            Fun_SetBottonEnabled(Btn_ImportPDI, true);
            Fun_InitialDataGridView();
            EventLogHandler.Instance.EventPush_Message($"已恢復成原排序");
            PublicComm.ClientLog.Info($"取消調整並恢復成原排序");
        }

        /// <summary>
        /// 取消調整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MovePdi_EnabledChanged(object sender, EventArgs e)
        {
            Btn_CancelMove.Visible = Btn_MovePdi.Enabled ? Btn_CancelMove.Visible = true : Btn_CancelMove.Visible = false;

            //鋼卷排程資訊
            try
            {
                Fun_InitialDataGridView();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
            }
        }

        /// <summary>
        /// 排程確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MovePdi_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无排程不可进行确认排程", "确定排程", null, 0);

                return;
            }

            PublicComm.ClientLog.Info($"排程確定動作");

            DataTable dt_ComparisonUpdateTime = new DataTable();

            try
            {
                int Correct = 0;
                int Error = 0;
                string strSql = string.Empty;
               
                for (int i = 0; i < dt_Seq.Rows.Count; i++)
                {
                    strSql = SqlFactory.Frm_1_1_MoveScheduleUpdateTimeCheck_DB_Schedule(Dgv_Info.Rows[i].Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString());
                    try
                    {
                        dt_ComparisonUpdateTime = Data_Access.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                        PublicComm.ClientLog.Info($"排程異動檢查更新時間成功");
                    }
                    catch (Exception ex)
                    {
                        EventLogHandler.Instance.LogDebug("1-1", $"排程异动检查更新时间", $"排程异动检查更新时间失败:{ex}");
                        EventLogHandler.Instance.EventPush_Message($"排程异动检查更新时间失败:{ex}");
                        PublicComm.ClientLog.Debug($"排程異動檢查更新時間失敗:{ex}");
                        PublicComm.ExceptionLog.Debug($"排程異動檢查更新時間失敗:{ex}");
                        return;
                    }
                    if (!string.IsNullOrEmpty(strStartChangeTime))
                    {
                        if (dt_ComparisonUpdateTime != null && dt_ComparisonUpdateTime.Rows.Count > 0)
                        {
                            string strUpdateTime1 = dt_ComparisonUpdateTime.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)].ToString();
                            string strUpdateTime2 = strStartChangeTime;// dtUpdateTime.Rows[i][nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)].ToString();
                            DateTime.TryParse(strUpdateTime1, out DateTime dTime_1);
                            DateTime.TryParse(strUpdateTime2, out DateTime dTime_2);

                            if (dTime_1 <= dTime_2)
                            {
                                Correct += 1;
                            }
                            else
                            {
                                Error += 1;
                            }

                            //if (DateTime.Parse(dt_ComparisonUpdateTime.Rows[0][nameof(TBL_Production_Schedule.UpdateTime)].ToString())
                            //<= DateTime.Parse(dtUpdateTime.Rows[i][nameof(TBL_Production_Schedule.UpdateTime)].ToString()))
                            //{
                            //    Correct += 1;
                            //}
                            //else
                            //{
                            //    Error += 1;
                            //}
                        }
                    }

                   

                }

                if (Error == 0)
                {
                    //修改Schedule
                    for (int i = 0; i < dt_Seq.Rows.Count; i++)
                    {
                        string strCoil_Id = Dgv_Info.Rows[i].Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString();
                        string strSeq_No = dt_Seq.Rows[i][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString();
                        strSql = SqlFactory.Frm_1_1_UpdateSchedule_DB_Schedule(strSeq_No, strCoil_Id);
                        try
                        {
                            Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                          
                            string strLogInfo = $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 排程异动成功,钢卷号[{strCoil_Id}]移至 {strSeq_No} ";
                            //EventLogHandler.Instance.LogInfo(this.Name, strLogInfo, "排程异动成功", true);
                            PublicComm.ClientLog.Info(strLogInfo);
                        }
                        catch (Exception ex)
                        {
                            EventLogHandler.Instance.EventPush_Message($"修改排程处理失败:{ex}");
                            EventLogHandler.Instance.LogDebug("1-1", $"修改排程", $"修改排程处理失败:{ex}");
                            PublicComm.ClientLog.Debug($"排程異動失敗:{ex}");
                            PublicComm.ExceptionLog.Debug($"排程異動失敗:{ex}");
                            return;
                        }

                    }

                    string strSql_TopLock = SqlFactory.Frm_1_1_SQL_Update_SystemSetting_TopScheduleLock("1");
                    if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql_TopLock, GlobalVariableHandler.Instance.strConn_GPL, "TopLock"))
                    {
                        //EventLogHandler.Instance.EventPush_Message($"");
                        return;
                    }
                  
                    Fun_InitialDataGridView();

                    SCCommMsg.CS03_ScheduleChange Msg = new SCCommMsg.CS03_ScheduleChange
                    {
                        Source = "GPL_HMI",
                        SchStatus = SCCommMsg.ScheduleStatus.ADJUST,
                        OperatorID = string.Empty,
                        ReasonCode = string.Empty,
                        EntryCoilID = string.Empty
                    };
                    PublicComm.Client.Tell(Msg);

                    EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 调整排程", "通知Server排程已調整");
                    DialogHandler.Instance.Fun_DialogShowOk("通知Server排程已調整", "确定排程", null, 4);
                    //EventLogHandler.Instance.LogInfo( "1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}通知Server排程已调整", "告知Server将已调整完毕的Schedule捞出");

                    PublicComm.ClientLog.Debug($"已通知Server排程调整");
                    PublicComm.akkaLog.Debug($"已通知Server排程调整");

                    EventLogHandler.Instance.EventPush_Message($"排程调整完成");
                    PublicComm.ClientLog.Info("排程調整完成");
                }
                else
                {
                    EventLogHandler.Instance.EventPush_Message($"排程资讯有刷新，请刷新页面再进行调整");
                    DialogHandler.Instance.Fun_DialogShowOk("排程资讯有刷新，请刷新页面再进行调整", "确定排程", null, 0);
                    EventLogHandler.Instance.EventPush_Message($"排程資訊有刷新，請刷新頁面");
                    EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}排程调整", "排程资讯有刷新，请刷新页面再进行调整");
                    PublicComm.ClientLog.Info($"排程資訊有刷新，請刷新頁面");
                }

                Fun_SetBottonEnabled(Btn_ReLoad, true);
                Fun_SetBottonEnabled(Btn_OutSchedule, true);
                Fun_SetBottonEnabled(Btn_ImportSchedule, true);
                Fun_SetBottonEnabled(Btn_ImportPDI, true);

                //清空調整排程時紀錄的當下時間
                strStartChangeTime = string.Empty;

                Btn_CancelMove.Visible = false;
                Btn_MovePdi.Enabled = false;
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"調整排程有誤");
                EventLogHandler.Instance.LogDebug("1-1", "調整排程有誤", $"{ex}");
                PublicComm.ClientLog.Debug($"排程調整有誤:{ex}");
                PublicComm.ExceptionLog.Debug($"排程調整有誤:{ex}");
            }


        }

        /// <summary>
        /// 通知排程已刷新
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC04_ScheduleChangeNotice(SCCommMsg.SC04_ScheduleChangeNotice msg)
        {
            Lbl_UpdateMsg.Text = $"排程已更新 时间:{GlobalVariableHandler.Instance.getTime}";

            Pnl_NewSchMessage.Visible = true;
           
            PublicComm.ClientLog.Info($"收到排程刷新通知");
            PublicComm.akkaLog.Debug($"收到排程刷新通知");

            Timer_NewSchMag.Start();
        }

        #endregion

        #region 開啟詳細資料

        /// <summary>
        /// 開啟詳細資料-排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PdiDet_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            string strCoilNo = Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString();//dt.Rows[Dgv_Info.CurrentRow.Index][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString();

            Fun_SelectedConfirm(Dgv_Info, strCoilNo);

            EventLogHandler.Instance.EventPush_Message($"开启[{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]详细资料");
            PublicComm.ClientLog.Info($"開啟詳細資料，鋼卷號:{Dgv_Info.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}");
        }

        /// <summary>
        /// 开启详细资料-查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PdiDet_ForSearch_Click(object sender, EventArgs e)
        {
            if (Dgv_Search.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_SelectedConfirm(Dgv_Search,Dgv_Search.CurrentRow.Cells[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].Value.ToString());
            EventLogHandler.Instance.EventPush_Message($"开启[{Dgv_Search.CurrentRow.Cells[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].Value.ToString().Trim()}]详细资料");
            PublicComm.ClientLog.Info($"開啟詳細資料，鋼卷號:{Dgv_Search.CurrentRow.Cells[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].Value.ToString().Trim()}");
        }

        /// <summary>
        /// 開啟詳細資料-選取確認
        /// </summary>
        /// <param name="dgv"></param>
        private void Fun_SelectedConfirm(DataGridView dgv,string Coil_ID)
        {
            if (dgv.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"请选择要显示详细资料的钢卷");
                return;
            }
            if (dgv.SelectedRows.Count <= 0)
            {
                EventLogHandler.Instance.EventPush_Message($"请选择要显示详细资料的钢卷");
                return;
            }

            string strPlan_No = dgv.CurrentRow.Cells[nameof(PDIEntity.TBL_PDI.Plan_No)].Value.ToString();//dgv.SelectedRows[0].Cells[""].Value.ToString();
            
            if (!Coil_ID.IsEmpty())
            {
                Frm_1_2_PDIDetailOpen(Coil_ID, strPlan_No);
            }
            //Event Log
            if (dgv.Name.Equals(Dgv_Info.Name))
            {
                EventLogHandler.Instance.LogInfo( "1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}排程钢卷开启详细资料", $"开启{dgv.CurrentRow.Cells[0].Value}的PDI详细资料");
            }
            else if (dgv.Name.Equals(Dgv_Search))
            {
                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}钢卷资讯开启详细资料", $"开启{dgv.CurrentRow.Cells[0].Value}的PDI详细资料");
            }
        }

        /// <summary>
        /// 开启详细资料
        /// </summary>
        /// <param name="DgvCurrentCoil"></param>
        private void Frm_1_2_PDIDetailOpen(string DgvCurrentCoil, string strPlan_No)
        {
            PublicForms.Main.tsMenuItem_1_2.PerformClick();
            PublicForms.PDIDetail.Cob_EntryCoilID.Text = DgvCurrentCoil;
            PublicForms.PDIDetail.Fun_SelectedData(DgvCurrentCoil, strPlan_No);
            PublicForms.PDIDetail.Fun_DisplayPDIDetail();
            PublicForms.PDIDetail.Fun_DisplayDefectData();
        }
        #endregion

        /// <summary>
        /// 要求PDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_RequestPDI_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无排程不可要求PDI", "要求PDI",null, 0);
                return;
            }
            if (Dgv_Info.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取欲要求PDI钢卷", "要求PDI", null, 0);
                EventLogHandler.Instance.EventPush_Message("请选取欲要求PDI钢卷");
                return;
            }

            PublicComm.ClientLog.Info($"要求PDI動作");

            string strCoil_ID = Dgv_Info.CurrentRow.Cells["Coil_ID"].Value.ToString().Trim();//TBL_Production_Schedule.Coil_ID

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"是否要求[{strCoil_ID}]PDI?", $"要求[{strCoil_ID}]PDI", Properties.Resources.dialogQuestion, 1);

            if (dialogR.Equals(DialogResult.OK))
            {
                SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI
                {
                    Source = "GPL_HMI",
                    ID = "AckPDI",
                    Coil_ID = strCoil_ID
                };

                PublicComm.Client.Tell(Msg);

                //SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI()
                //{
                //    Source = "GPL_HMI",
                //    ID = "AckPDI",
                //    Coil_ID = Dgv_Info.CurrentRow.Cells[2].Value.ToString().Trim()
                //};

                //PublicComm.client.Tell(Msg);

                string strMessage = $"已通知Server要求[{strCoil_ID}]PDI";

                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}通知Server要求钢卷编号:[{strCoil_ID}]之PDI", $"钢卷编号:{strCoil_ID}");
                EventLogHandler.Instance.EventPush_Message(strMessage);
                PublicComm.ClientLog.Info(strMessage);
                PublicComm.akkaLog.Info(strMessage);
                DialogHandler.Instance.Fun_DialogShowOk(strMessage, "要求PDI",null, 4);
            }
            else
            {
                PublicComm.ClientLog.Info($"取消要求PDI");
                //DialogHandler.Instance.Fun_DialogShowOk("取消要求PDI", "要求PDI", 4);
            }
        }

        /// <summary>
        /// 要求排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_NewRec_Click(object sender, EventArgs e)
        {
            EventLogHandler.Instance.EventPush_Message($"要求排程动作");
            PublicComm.ClientLog.Info($"要求排程動作");
            SCCommMsg.CS01_AckSchedule Msg = new SCCommMsg.CS01_AckSchedule();

            //if (Dgv_Info.CurrentRow.Index < 3)
            //{
            //    EventLogHandler.Instance.EventPush_Message("排程前三笔禁止要求排程刷新!");
            //    return;
            //}

            //SCCommMsg.CS01_AckSchedule Msg = new SCCommMsg.CS01_AckSchedule();

            if (Dgv_Info.SelectedRows.Count > 1)
            {
                DialogHandler.Instance.Fun_DialogShowOk("选取行禁止大于一笔", "要求排程", null, 0);
                EventLogHandler.Instance.EventPush_Message("选取行禁止大于一笔");
                return;
            }
            //if (Dgv_Info.CurrentRow != null)
            //{
            //    //需多判斷為有選取的狀態，不然會導致無選取時要求全部排程被擋住
            //    if (Dgv_Info.CurrentRow.Index <= intTopCanNotMove - 1 && !Dgv_Info.CurrentIsNull())
            //    {
            //        DialogHandler.Instance.Fun_DialogShowOk("前三笔排程禁止要求排程", "要求排程",null, 2);
            //        return;
            //    }
            //}

            if (Dgv_Info.CurrentIsNull() || Dgv_Info.Rows.Count.Equals(0))
            {
                DialogResult dialogResult = DialogHandler.Instance.Fun_DialogShowOkCancel("是否要求全部排程?", "要求排程资讯确认", Properties.Resources.dialogQuestion, 1);

                if (dialogResult == DialogResult.OK)
                    Msg.CoilID = "0";
                else
                    return;

            }
            else
            {
                string strReCoilNo = Dgv_Info.CurrentRow.Cells["Coil_ID"].Value.ToString().Trim();

                StringBuilder sbText = new StringBuilder();
                sbText.AppendLine($"请选择 ");
                sbText.AppendLine($"按下 [  是  ]:要求钢卷号{strReCoilNo}排程 ");
                sbText.AppendLine($"按下 [  否  ]:要求全部排程 ");
                sbText.AppendLine($"按下 [ 取消 ]:不动作 ");

                DialogResult dialogResult = DialogHandler.Instance.Fun_DialogShowSelectOk(sbText.ToString(), "是否以钢卷号要求排程");

                if (dialogResult == DialogResult.Cancel) return;

                if (dialogResult == DialogResult.Yes)
                {
                    Msg.CoilID = strReCoilNo;
                }
                else if (dialogResult == DialogResult.No)
                {
                    Msg.CoilID = "0";
                }
            }

            PublicComm.Client.Tell(Msg);
            EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}通知Server要求排程", "向MMS要求排程");
            DialogHandler.Instance.Fun_DialogShowOk("已通知Server要求排程", "要求排程",null, 4);
            EventLogHandler.Instance.EventPush_Message($"已通知Server要求排程");
            PublicComm.ClientLog.Info($"通知Server要求排程");
            PublicComm.akkaLog.Info($"通知Server要求排程");

            Btn_MovePdi.Enabled = false;
            Btn_CancelMove.Visible = false;
            Btn_OutSchedule.Enabled = true;

            Fun_InitialDataGridView();

        }

        /// <summary>
        /// 刷新頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReLoad_Click(object sender, EventArgs e)
        {
            Fun_ReLoad();
            EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}页面重新刷新", "MMS下抛新排程由使用者自行刷新");
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Fun_ReLoad()
        {
            //鋼卷排程資訊
            try
            {
                Fun_InitialDataGridView();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Info($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Info($"鋼卷排程資訊查詢資料庫失敗:{ex}");
            }

            //过渡卷资讯
            try
            {
                Fun_SearchDM();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"过渡卷资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Info($"過渡卷資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Info($"過渡卷資訊查詢資料庫失敗:{ex}");
            }

            Chk_Plan_No.Checked = false;
            Chk_Entry_Coil_No.Checked = false;
            //Dgv_Info.ClearSelection();
        }
               
        private void Timer_NewSchMag_Tick(object sender, EventArgs e)
        {
            Fun_ReLoad();

            Pnl_NewSchMessage.Visible = false;
            Btn_CancelMove.Visible = false;
            Btn_OutSchedule.Enabled = true;
            Btn_ImportSchedule.Enabled = true;
            Btn_ImportPDI.Enabled = true;
            Btn_ReLoad.Enabled = true;

            ((Timer)sender).Stop();
        }

        //匯入排程
        private void Btn_InsertSchedule_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dtGetExcel = SelectExcelToDataTable(dialog.FileName);
                    string strSql = string.Empty;

                    if (dtGetExcel.IsNull())
                    {
                        EventLogHandler.Instance.EventPush_Message($"资料表无资料");
                        return;
                    } 

                    List<string> importList = new List<string>();
                    int row = 0;
                    int col = 0;
                    foreach (DataRow datarow in dtGetExcel.Rows)
                    {
                        row++;
                        if (row < 4) continue;

                        if (datarow[0].ToString() != "")
                        {
                            foreach (DataColumn dc in dtGetExcel.Columns)
                            {
                                col++;
                                if (col < 3) continue;

                                if (datarow[dc].ToString() != "")
                                {
                                    importList.Add(datarow[dc].ToString());
                                }
                            }
                        }
                    }
                    #region Old
                    //int row = 0;
                    //foreach (DataRow datarow in dtGetExcel.Rows)
                    //{
                    //    row++;

                    //    if (row < 4)
                    //    {
                    //        continue;
                    //    }

                    //    if (!string.IsNullOrEmpty(datarow[0].ToString()) && !string.IsNullOrEmpty(datarow[6].ToString()))
                    //    {
                    //        importList.Add(datarow[6].ToString());
                    //    }
                    //}
                    #endregion

                    if (importList.Count.Equals(0))
                    {
                        EventLogHandler.Instance.EventPush_Message($"资料表无资料");
                        return;
                    }

                    // 刪除資料庫(TBL_CoilSchedule)
                    strSql = SqlFactory.Frm_1_1_ClearSchedule();
                    InsertSchedulePDI(strSql, "清空排程");

                    // 寫入資料庫(TBL_CoilSchedule)
                    for (int Index = 0; Index < importList.Count; Index++)
                    {
                        strSql = SqlFactory.Frm_1_1_ImportSchedule(Index + 1, importList[Index].ToString());
                        InsertSchedulePDI(strSql, "排程汇入");
                    }

                    EventLogHandler.Instance.LogDebug("1-1", "排程汇入", "排程汇入成功");
                    PublicComm.ClientLog.Info($"排程匯入成功");
                }
                catch (Exception ex)
                { 
                    EventLogHandler.Instance.EventPush_Message($"排程汇入失败{ex}");
                    EventLogHandler.Instance.LogDebug( "1-1", "排程汇入", $"排程汇入失败:{ex}");
                    PublicComm.ClientLog.Debug($"排程匯入失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"排程匯入失敗:{ex}");
                    return;
                }
                EventLogHandler.Instance.EventPush_Message($"排程汇入成功");
                SCCommMsg.CS16_FinishLoadSchedule Msg = new SCCommMsg.CS16_FinishLoadSchedule();
                PublicComm.Client.Tell(Msg);
                PublicComm.ClientLog.Debug($"已通知Server汇入排程");
                PublicComm.akkaLog.Debug($"已通知Server汇入排程");

                //鋼卷排程資訊
                try
                {
                    Fun_InitialDataGridView();
                }
                catch (Exception ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                    EventLogHandler.Instance.LogDebug( "1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                }
               
            }
        }

        //匯入PDI
        private void Btn_InsertPDI_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strfileName = dialog.FileName;
                    if (!PublicForms.IsLoadExcel(strfileName))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{strfileName}正在开启中", "汇入PDI", null, 2);
                        return;
                    }

                    DataTable dt = SelectExcelToDataTable(strfileName);

                    if (dt.IsNull())
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel转换结果无资料", "汇入PDI", null, 0);
                        EventLogHandler.Instance.EventPush_Message($"Excel挡案转DataTable失败");
                        EventLogHandler.Instance.LogInfo("1-1", $"汇入PDI", $"Excel挡案转DataTable失败");
                        PublicComm.ClientLog.Debug($"Excel檔案轉DataTable失敗");
                        return;
                    }

                    

                    //DataTable dtNew = Fun_TurnTable(dt);

                    //if (dtNew.IsNull())
                    //{
                    //    DialogHandler.Instance.Fun_DialogShowOk("Excel资料异常，汇入失败", "汇入PDI", null, 3);
                    //    EventLogHandler.Instance.EventPush_Message($"DataTable转向失败");
                    //    EventLogHandler.Instance.LogInfo("1-1", $"汇入PDI", $"DataTable转向失败");
                    //    PublicComm.ClientLog.Debug($"DataTable轉向失敗");
                    //    return;
                    //}

                    Fun_SetPdiData(dt);

                    ////存進DB
                    //ImportPDI_InsertUpdate(dtNew);
                    //EventLogHandler.Instance.LogDebug("1-1", "PDI汇入", "PDI汇入成功");

                    ////更新下方事件訊息
                    //DialogHandler.Instance.Fun_DialogShowOk("PDI汇入成功", "汇入PDI", null, 4);
                    //EventLogHandler.Instance.EventPush_Message($"PDI汇入成功");
                    //PublicComm.ClientLog.Info($"PDI匯入成功");
                }
                catch (Exception ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"PDI汇入失败{ex}");
                    EventLogHandler.Instance.LogDebug("1-1", "PDI汇入", $"PDI汇入失败:{ex}");
                    PublicComm.ClientLog.Debug($"PDI匯入失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"PDI匯入失敗:{ex}");
                    return;

                }
                SCCommMsg.CS17_FinishLoadPDI Msg = new SCCommMsg.CS17_FinishLoadPDI();

                PublicComm.Client.Tell(Msg);
                PublicComm.ClientLog.Info($"已通知Server PDI匯入");
                PublicComm.akkaLog.Info($"已通知Server PDI匯入");

                //鋼卷排程資訊
                try
                {
                    Fun_InitialDataGridView();
                }
                catch (Exception ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"查询资料库失败:{ex}");
                    EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                }

            }
        }

        private void Fun_SetPdiData(DataTable dtExcel)
        {
            #region 取得 TBL_PDI 欄位 ; 順便檢查是否已有相同PDI
            string strCoil = dtExcel.Rows[4][2].ToString().Trim();
            string strPlanNo = dtExcel.Rows[1][2].ToString().Trim();

            string strSql = SqlFactory.Frm_1_2_SelectedData_DB_PDI(strCoil, strPlanNo);
                //$"SELECT * FROM  {nameof(PDIEntity.TBL_PDI)} WHERE {nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{strCoil}' ";
            DataTable dtPdi_have = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDI", "1-1");

            DataTable dtExPdi = dtPdi_have.Clone();
            dtExPdi.Columns[nameof(PDIEntity.TBL_PDI.CreateTime)].DataType = typeof(string);//將欄位改為可存字串
            if (dtExPdi.Rows.Count <= 0)
            {
                DataRow dr = dtExPdi.NewRow();
                try
                {
                    dtExPdi.LoadDataRow(dr.ItemArray, false);
                }
                catch { }
            }
            else { }
            #endregion 取得 TBL_PDI 欄位 end

            string strPdi_Updated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");// HMIConst.DateFormat_17en);//"yyyy-MM-dd HH:mm:ss.fff"
            try
            {
                #region Set TBL_PDI 欄位值
                //dtExPdi.Rows[0][nameof(APLB_PDI.id)] = dtExcel.Rows[0][2].ToString();  //
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_No)] = dtExcel.Rows[1][2].ToString();  //计划号_1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Mat_Seq_No)] = Fun_To_int(dtExcel.Rows[2][2].ToString());  //材料命令顺序号
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_Type)] = dtExcel.Rows[3][2].ToString();  //计划类型
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_ID)] = dtExcel.Rows[4][2].ToString();  //入口材料号
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Thick)] = Fun_To_decimal(dtExcel.Rows[5][2].ToString());  //入口材料厚度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Width)] = Fun_To_decimal(dtExcel.Rows[6][2].ToString());  //入口材料宽度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Wt)] = Fun_To_int(dtExcel.Rows[7][2].ToString());  //入口材料重量
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Length)] = Fun_To_int(dtExcel.Rows[8][2].ToString());  //入口材料长度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)] = Fun_To_int(dtExcel.Rows[9][2].ToString());  //入口材料内径
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)] = Fun_To_int(dtExcel.Rows[10][2].ToString());  //入口材料外径

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)] = dtExcel.Rows[11][2].ToString();  //入口垫纸要求代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Paper_Code)] = dtExcel.Rows[12][2].ToString();  //入口垫纸类型代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Paper_Length)] = Fun_To_int(dtExcel.Rows[13][2].ToString());  //头部垫纸长度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Paper_Width)] = Fun_To_decimal(dtExcel.Rows[14][2].ToString());  //头部垫纸宽度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)] = Fun_To_int(dtExcel.Rows[15][2].ToString());  //尾部垫纸长度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)] = Fun_To_decimal(dtExcel.Rows[16][2].ToString());  //尾部垫纸宽度

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)] = dtExcel.Rows[17][2].ToString();  //入口套筒类型代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)] = Fun_To_int(dtExcel.Rows[18][2].ToString());  //入口套筒内径
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.St_No)] = dtExcel.Rows[19][2].ToString();  //出钢记号
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Density)] = Fun_To_int(dtExcel.Rows[20][2].ToString());  //密度

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)] = dtExcel.Rows[21][2].ToString();  //实际表面精度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)] = dtExcel.Rows[22][2].ToString();  //订单表面精度代码

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)] = Fun_To_int(dtExcel.Rows[23][2].ToString());  //平坦度平均值
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)] = dtExcel.Rows[24][2].ToString();  //好面朝向代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Uncoil_Direction)] = dtExcel.Rows[25][2].ToString();  //开卷方向
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Origin_Code)] = dtExcel.Rows[26][2].ToString();  //来源代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Pack_Mode)] = Fun_To_int(dtExcel.Rows[27][2].ToString());  //打捆方式

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)] = dtExcel.Rows[28][2].ToString();  //出口垫纸要求
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Paper_Code)] = dtExcel.Rows[29][2].ToString();  //出口垫纸类型
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)] = dtExcel.Rows[30][2].ToString();  //出口套筒类型
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)] = Fun_To_int(dtExcel.Rows[31][2].ToString());  //出口套筒内径
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_ID)] = dtExcel.Rows[32][2].ToString();  //出口材料号

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Order_No)] = dtExcel.Rows[33][2].ToString();  //合同号
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Order_Cust_Code)] = dtExcel.Rows[34][2].ToString();  //订货用户代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)] = dtExcel.Rows[35][2].ToString();  //前全程工序代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)] = dtExcel.Rows[36][2].ToString();  //后全程工序代码

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)] = dtExcel.Rows[37][2].ToString();  //头部导带标记
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Hole_Position)] = Fun_To_decimal(dtExcel.Rows[38][2].ToString());  //头部打孔位置
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)] = Fun_To_decimal(dtExcel.Rows[39][2].ToString());  //头部导带厚度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Width)] = Fun_To_decimal(dtExcel.Rows[40][2].ToString());  //头部导带宽度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Length)] = Fun_To_int(dtExcel.Rows[41][2].ToString());  //头部导带长度

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)] = dtExcel.Rows[42][2].ToString();  //尾部导带标记
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)] = Fun_To_decimal(dtExcel.Rows[43][2].ToString());  //尾部打孔位置
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)] = Fun_To_decimal(dtExcel.Rows[44][2].ToString());  //尾部导带厚度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)] = Fun_To_decimal(dtExcel.Rows[45][2].ToString());  //尾部导带宽度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)] = Fun_To_int(dtExcel.Rows[46][2].ToString());  //尾部导带长度

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)] = dtExcel.Rows[47][2].ToString();  //头部导带钢种
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)] = dtExcel.Rows[48][2].ToString();  //尾部导带钢种


                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)] = Fun_To_decimal(dtExcel.Rows[49][2].ToString());  //出口材料厚度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)] = Fun_To_decimal(dtExcel.Rows[50][2].ToString());  //出口材料最小厚度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)] = Fun_To_decimal(dtExcel.Rows[51][2].ToString());  //出口材料最大厚度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Width)] = Fun_To_decimal(dtExcel.Rows[52][2].ToString());  //出口材料宽度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)] = Fun_To_int(dtExcel.Rows[53][2].ToString());  //出口材料内径

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Grind_Flag)] = dtExcel.Rows[54][2].ToString();  //研磨标记_1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Pack_Type_Code)] = dtExcel.Rows[55][2].ToString();  //包装类型代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Repair_Type)] = dtExcel.Rows[56][2].ToString();  //返修类型

                #region 缺陷 
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Code)] = dtExcel.Rows[57][2].ToString();  //缺陷代码1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Origin)] = dtExcel.Rows[58][2].ToString();  //缺陷来源1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Sid)] = dtExcel.Rows[59][2].ToString();  //缺陷所在表面1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_W)] = dtExcel.Rows[60][2].ToString();  //缺陷宽度方向位置1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[61][2].ToString());  //缺陷长向起始位置1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)] = Fun_To_int(dtExcel.Rows[62][2].ToString());  //缺陷长向终止位置1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Level)] = dtExcel.Rows[63][2].ToString();  //缺陷等级1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Percent)] = Fun_To_int(dtExcel.Rows[64][2].ToString());  //缺陷比例1
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D01_QGrade)] = dtExcel.Rows[65][2].ToString();  //缺陷质量等级1

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Code)] = dtExcel.Rows[66][2].ToString();  //缺陷代码2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Origin)] = dtExcel.Rows[67][2].ToString();  //缺陷来源2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Sid)] = dtExcel.Rows[68][2].ToString();  //缺陷所在表面2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_W)] = dtExcel.Rows[69][2].ToString();  //缺陷宽度方向位置2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[70][2].ToString());  //缺陷长向起始位置2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)] = Fun_To_int(dtExcel.Rows[71][2].ToString());  //缺陷长向终止位置2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Level)] = dtExcel.Rows[72][2].ToString();  //缺陷等级2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Percent)] = Fun_To_int(dtExcel.Rows[73][2].ToString());  //缺陷比例2
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D02_QGrade)] = dtExcel.Rows[74][2].ToString();  //缺陷质量等级2

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Code)] = dtExcel.Rows[75][2].ToString();  //缺陷代码3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Origin)] = dtExcel.Rows[76][2].ToString();  //缺陷来源3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Sid)] = dtExcel.Rows[77][2].ToString();  //缺陷所在表面3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_W)] = dtExcel.Rows[78][2].ToString();  //缺陷宽度方向位置3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[79][2].ToString());  //缺陷长向起始位置3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)] = Fun_To_int(dtExcel.Rows[80][2].ToString());  //缺陷长向终止位置3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Level)] = dtExcel.Rows[81][2].ToString();  //缺陷等级3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Percent)] = Fun_To_int(dtExcel.Rows[82][2].ToString());  //缺陷比例3
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D03_QGrade)] = dtExcel.Rows[83][2].ToString();  //缺陷质量等级3

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Code)] = dtExcel.Rows[84][2].ToString();  //缺陷代码4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Origin)] = dtExcel.Rows[85][2].ToString();  //缺陷来源4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Sid)] = dtExcel.Rows[86][2].ToString();  //缺陷所在表面4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_W)] = dtExcel.Rows[87][2].ToString();  //缺陷宽度方向位置4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[88][2].ToString());  //缺陷长向起始位置4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)] = Fun_To_int(dtExcel.Rows[89][2].ToString());  //缺陷长向终止位置4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Level)] = dtExcel.Rows[90][2].ToString();  //缺陷等级4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Percent)] = Fun_To_int(dtExcel.Rows[91][2].ToString());  //缺陷比例4
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D04_QGrade)] = dtExcel.Rows[92][2].ToString();  //缺陷质量等级4

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Code)] = dtExcel.Rows[93][2].ToString();  //缺陷代码5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Origin)] = dtExcel.Rows[94][2].ToString();  //缺陷来源5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Sid)] = dtExcel.Rows[95][2].ToString();  //缺陷所在表面5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_W)] = dtExcel.Rows[96][2].ToString();  //缺陷宽度方向位置5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[97][2].ToString());  //缺陷长向起始位置5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)] = Fun_To_int(dtExcel.Rows[98][2].ToString());  //缺陷长向终止位置5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Level)] = dtExcel.Rows[99][2].ToString();  //缺陷等级5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Percent)] = Fun_To_int(dtExcel.Rows[100][2].ToString());  //缺陷比例5
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D05_QGrade)] = dtExcel.Rows[101][2].ToString();  //缺陷质量等级5

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Code)] = dtExcel.Rows[102][2].ToString();  //缺陷代码6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Origin)] = dtExcel.Rows[103][2].ToString();  //缺陷来源6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Sid)] = dtExcel.Rows[104][2].ToString();  //缺陷所在表面6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_W)] = dtExcel.Rows[105][2].ToString();  //缺陷宽度方向位置6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[106][2].ToString());  //缺陷长向起始位置6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)] = Fun_To_int(dtExcel.Rows[107][2].ToString());  //缺陷长向终止位置6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Level)] = dtExcel.Rows[108][2].ToString();  //缺陷等级6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Percent)] = Fun_To_int(dtExcel.Rows[109][2].ToString());  //缺陷比例6
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D06_QGrade)] = dtExcel.Rows[110][2].ToString();  //缺陷质量等级6

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Code)] = dtExcel.Rows[111][2].ToString();  //缺陷代码7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Origin)] = dtExcel.Rows[112][2].ToString();  //缺陷来源7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Sid)] = dtExcel.Rows[113][2].ToString();  //缺陷所在表面7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_W)] = dtExcel.Rows[114][2].ToString();  //缺陷宽度方向位置7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[115][2].ToString());  //缺陷长向起始位置7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)] = Fun_To_int(dtExcel.Rows[116][2].ToString());  //缺陷长向终止位置7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Level)] = dtExcel.Rows[117][2].ToString();  //缺陷等级7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Percent)] = Fun_To_int(dtExcel.Rows[118][2].ToString());  //缺陷比例7
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D07_QGrade)] = dtExcel.Rows[119][2].ToString();  //缺陷质量等级7

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Code)] = dtExcel.Rows[120][2].ToString();  //缺陷代码8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Origin)] = dtExcel.Rows[121][2].ToString();  //缺陷来源8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Sid)] = dtExcel.Rows[122][2].ToString();  //缺陷所在表面8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_W)] = dtExcel.Rows[123][2].ToString();  //缺陷宽度方向位置8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[124][2].ToString());  //缺陷长向起始位置8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)] = Fun_To_int(dtExcel.Rows[125][2].ToString());  //缺陷长向终止位置8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Level)] = dtExcel.Rows[126][2].ToString();  //缺陷等级8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Percent)] = Fun_To_int(dtExcel.Rows[127][2].ToString());  //缺陷比例8
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D08_QGrade)] = dtExcel.Rows[128][2].ToString();  //缺陷质量等级8

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Code)] = dtExcel.Rows[129][2].ToString();  //缺陷代码9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Origin)] = dtExcel.Rows[130][2].ToString();  //缺陷来源9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Sid)] = dtExcel.Rows[131][2].ToString();  //缺陷所在表面9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_W)] = dtExcel.Rows[132][2].ToString();  //缺陷宽度方向位置9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[133][2].ToString());  //缺陷长向起始位置9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)] = Fun_To_int(dtExcel.Rows[134][2].ToString());  //缺陷长向终止位置9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Level)] = dtExcel.Rows[135][2].ToString();  //缺陷等级9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Percent)] = Fun_To_int(dtExcel.Rows[136][2].ToString());  //缺陷比例9
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D09_QGrade)] = dtExcel.Rows[137][2].ToString();  //缺陷质量等级9

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Code)] = dtExcel.Rows[138][2].ToString();  //缺陷代码10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Origin)] = dtExcel.Rows[139][2].ToString();  //缺陷来源10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Sid)] = dtExcel.Rows[140][2].ToString();  //缺陷所在表面10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_W)] = dtExcel.Rows[141][2].ToString();  //缺陷宽度方向位置10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)] = Fun_To_int(dtExcel.Rows[142][2].ToString());  //缺陷长向起始位置10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)] = Fun_To_int(dtExcel.Rows[143][2].ToString());  //缺陷长向终止位置10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Level)] = dtExcel.Rows[144][2].ToString();  //缺陷等级10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Percent)] = Fun_To_int(dtExcel.Rows[145][2].ToString());  //缺陷比例10
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.D10_QGrade)] = dtExcel.Rows[146][2].ToString();  //缺陷质量等级10
                #endregion 缺陷end

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Test_Plan_No)] = dtExcel.Rows[147][2].ToString();  //测试计划号
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Qc_Rmark)] = dtExcel.Rows[148][2].ToString();  //QC备注
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)] = Fun_To_int(dtExcel.Rows[149][2].ToString());  //头部未轧延长度
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)] = Fun_To_int(dtExcel.Rows[150][2].ToString());  //尾部未轧延长度

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)] = dtExcel.Rows[151][2].ToString();  //钢卷上道研磨面
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)] = Fun_To_int(dtExcel.Rows[152][2].ToString());  //钢卷外表面研磨次数
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Grinding_Count_In)] = Fun_To_int(dtExcel.Rows[153][2].ToString());  //钢卷内表面研磨次数
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)] = dtExcel.Rows[154][2].ToString();  //钢卷指定研磨面

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)] = dtExcel.Rows[155][2].ToString();  //内表面精度代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)] = dtExcel.Rows[156][2].ToString();  //外表面精度代码


                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Repair_Remark)] = dtExcel.Rows[157][2].ToString();  //返修注释
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Skim_Flag)] = dtExcel.Rows[158][2].ToString();  //是否脱脂
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Polishing_Type)] = dtExcel.Rows[159][2].ToString();  //抛光类型
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Sg_Sign)] = dtExcel.Rows[160][2].ToString();  //牌号（钢级）

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Process_Code)] = dtExcel.Rows[161][2].ToString();  //工序代码
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Ys_Stand)] = Fun_To_decimal(dtExcel.Rows[162][2].ToString());  //屈服实际值
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Ys_Max)] = Fun_To_decimal(dtExcel.Rows[163][2].ToString());  //屈服强度最大值
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Ys_Min)] = Fun_To_decimal(dtExcel.Rows[164][2].ToString());  //屈服强度最小值

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Order_Cust_Ename)] = dtExcel.Rows[165][2].ToString();  //订货用户英文名称
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Order_Cust_Cname)] = dtExcel.Rows[166][2].ToString();  //订货客户中文名称

                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Order)] = dtExcel.Rows[167][2].ToString();  //订单表面精度描述
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Desc_Acture)] = dtExcel.Rows[168][2].ToString();  //来料表面精度描述
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_In)] = dtExcel.Rows[169][2].ToString();  //内表面精度描述
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Out)] = dtExcel.Rows[170][2].ToString();  //外表面精度描述


                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Is_Delete)] = "0";
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Is_Dummy_Coil)] = "0";
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Is_Info_WMS_Down)] = "0";
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.CreateTime)] = strPdi_Updated; //
                dtExPdi.Rows[0][nameof(PDIEntity.TBL_PDI.Create_UserID)] = PublicForms.Main.lblLoginUser.Text.Trim();  //

                #endregion Set APLB_PDI 欄位值 end
            }
            catch (Exception ee)
            {

            }
           

         
            string strPdi_Sql;
            if (dtPdi_have.IsNull())
            {
                strPdi_Sql = SqlFactory.Fun_GetInsertSqlFromDataTable(dtExPdi, nameof(PDIEntity.TBL_PDI));
            }
            else
            {
                //PRIMARY KEY
                string[] strArr = { nameof(PDIEntity.TBL_PDI.In_Coil_ID) , nameof(PDIEntity.TBL_PDI.Plan_No) };
                strPdi_Sql = SqlFactory.Fun_GetUpdateSqlFromDataRow(dtExPdi.Rows[0], nameof(PDIEntity.TBL_PDI), strArr);
            }

            try
            {
                if (!Data_Access.GetInstance().ExecuteNonQuery(strPdi_Sql, GlobalVariableHandler.Instance.strConn_GPL))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"匯入PDI失败", "匯入PDI", null, 3);
                    return;
                }

                EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 匯入PDI 钢卷号[{strCoil}]成功");
                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text} 匯入PDI 钢卷号[{strCoil}]成功", $"匯入PDI");
                PublicComm.ClientLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text} 匯入PDI 钢卷号[{strCoil}]成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 匯入PDI 欄位資料有誤:{ex}]");
                PublicComm.ClientLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text} 匯入PDI 儲存失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text} 匯入PDI 儲存失敗:{ex}");
            }


        }

        private int Fun_To_int(string strvalue)
        {
            int intV = 0;

            int.TryParse(strvalue,out intV);

            return intV;
        }

        private decimal Fun_To_decimal(string strvalue)
        {
            decimal intV = 0;

            decimal.TryParse(strvalue, out intV);

            return intV;
        }

        private void ImportPDI_InsertUpdate(DataTable dtImportPDI)
        {
            string strSql = string.Empty;

            //檢查鋼卷是否已存在PDI
            foreach (DataRow dr in dtImportPDI.Rows)
            {
                string strCoil = dr[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString().Trim();

                strSql = SqlFactory.Frm_1_1_SearchCoil_DB_PDI(strCoil);
                DataTable dtSearch = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"檢查{strCoil}是否存在PDI", "1-1");

                int Index = dtImportPDI.Rows.IndexOf(dr);
                //已存在
                if (dtSearch.Rows.Count > 0)
                {
                    strSql = SqlFactory.Frm_1_1_UpdateCoilPDI_DB_PDI(dtImportPDI,Index);
                }
                //不存在
                else if (dtSearch.Rows.Count == 0)
                {
                    strSql = SqlFactory.Frm_1_1_InsertCoilPDI_DB_PDI(dtImportPDI,Index);
                }

                InsertSchedulePDI(strSql, "PDI汇入动作完成");
            }
        }

        /// <summary>
        /// 判斷字串文字是否皆為中文
        /// </summary>
        /// <param name="strChinese">中文字串</param>
        /// <returns>若字串皆為中文 :true 含有中文以外的文字 :false</returns>
        private static bool IsChinese(string strChinese)
        {
            bool flag = true;
            int dRange = 0;
            int dstringmax = Convert.ToInt32("9fff", 16);
            int dstringmin = Convert.ToInt32("4e00", 16);
            for (int i = 0; i < strChinese.Length; i++)
            {
                dRange = Convert.ToInt32(Convert.ToChar(strChinese.Substring(i, 1)));
                if (dRange >= dstringmin && dRange < dstringmax)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        // 把Table 直向 轉 橫向
        private DataTable Fun_TurnTable(DataTable dt)
        {
            DataTable dtNew = new DataTable();
            try
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    dtNew.Columns.Add(dt.Rows[i][0].ToString());
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataRow dr = dtNew.NewRow();
                    dtNew.Rows.Add(dr);
                }

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    for (int j = 2; j < dt.Columns.Count; j++)
                    {
                        dtNew.Rows[j - 1][i - 1] = dt.Rows[i][j].ToString();
                    }
                }

                List<DataRow> deletedRows = new List<DataRow>();

                foreach (DataRow dr in dtNew.Rows)
                {
                    if (string.IsNullOrEmpty(dr[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString()))
                    {
                        deletedRows.Add(dr);
                    }
                    else
                    {
                        if(IsChinese(dr[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString())) deletedRows.Add(dr);
                    }
                }

                foreach (DataRow dataRow in deletedRows)
                {
                    dataRow.Delete();
                }

            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"DataTable直式转横式失败{ex}");
                PublicComm.ClientLog.Debug($"DataTable直式轉橫式失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"DataTable直式轉橫式失敗:{ex}");
            }
            return dtNew;
        }

       

        private void InsertSchedulePDI(string strSql, string Message = "")
        {
            if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, Message, "1-1"))
            {
                EventLogHandler.Instance.EventPush_Message($"{Message}失败");
                return;
            }
        }

        // 把Excel轉成DataTable
        private DataTable SelectExcelToDataTable(string FileName)
        {
            DataTable dt = new DataTable();
            try
            {
                FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader;

                if (Path.GetExtension(FileName) == ".xls")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                DataSet result = excelReader.AsDataSet();
                dt = result.Tables[0];
                stream.Dispose();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"Excel转DataTable失败{ex}");
                PublicComm.ClientLog.Debug($"Excel轉DataTable失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"Excel轉DataTable失敗:{ex}");
            }

            return dt;
        }

        /// <summary>
        /// 過渡卷搜尋
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Dummy_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty( Cob_Entry_Coil_No_Dummy.Text))
                Fun_SearchDM(false);
            else
                Fun_SearchDM(true);
        }

        /// <summary>
        /// 過渡卷清單要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DM_Ask_Click(object sender, EventArgs e)
        {
            Frm_RequestPdi RequestPdi = new Frm_RequestPdi();

            RequestPdi.Fun_CatchTitle("要求过渡卷",string.Empty,"请输入过渡卷号");
            RequestPdi.ShowDialog();
            RequestPdi.Dispose();

            if (RequestPdi.DialogResult.Equals(DialogResult.Cancel)) return;

            SCCommMsg.CS19_RequestDummy Msg = new SCCommMsg.CS19_RequestDummy()
            {
                DummyCoil = RequestPdi.StrRequestCoilNo.Trim()
            };

            PublicComm.Client.Tell(Msg);

            EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}过渡卷清单要求", "通知Server要求过渡卷");
            EventLogHandler.Instance.EventPush_Message($"已通知Server过渡卷要求");
            PublicComm.ClientLog.Info($"已通知Server過渡卷要求");
            PublicComm.akkaLog.Info($"已通知Server過渡卷要求");
        }
        
        /// <summary>
        /// 過渡卷刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DM_Delete_Click(object sender, EventArgs e)
        {
            if (dtGetDM.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无过度卷清单");
                return;
            }

            if (Dgv_DM.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取过度卷");
                return;
            }

            //DataRow dr = dtGetDM.Rows[Dgv_DM.CurrentRow.Index];
            DataRow dr = Fun_GetDataRowFromCurrentRow(Dgv_DM, dtGetDM);
           
            string strDelCoil = dr[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString().Trim();
            string strMessage = $"删除过渡卷[{strDelCoil}]";

            Frm_Reason fzr = new Frm_Reason();
            fzr.Fun_CatchTitle("删除过度卷", strMessage, "请选择删除原因");
            fzr.ShowDialog();
            fzr.Dispose();

            if (fzr.DialogResult.Equals(DialogResult.OK))
            {
                //检查是否有删除记录
                string strSql = SqlFactory.Frm_1_1_SelectDeleteScheduleRecord(strDelCoil);
                DataTable dtCheckRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除过渡卷记录", "1-1");

                if (!dtCheckRecord.IsNull())
                {
                    EventLogHandler.Instance.EventPush_Message($"此过渡卷[{strDelCoil}]已有删除记录，请重新确认");
                    return;
                }


                //刪除排程紀錄
                strSql = SqlFactory.Frm_1_1_DeleteDummyRecord();

                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除过渡卷", "1-1"))
                {
                    EventLogHandler.Instance.EventPush_Message($"删除过渡卷失败");
                    return;
                }


                SCCommMsg.CS20_DeleteDummy Msg = new SCCommMsg.CS20_DeleteDummy()
                {
                    DummyCoil = strDelCoil,
                    ReasonCode = fzr.StrReasonCode
                };

                PublicComm.Client.Tell(Msg);

                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}过渡卷删除", $"已通知Server删除过渡卷:[{strDelCoil}]");
                EventLogHandler.Instance.EventPush_Message($"已通知Server删除过渡卷:[{strDelCoil}]");
                PublicComm.ClientLog.Info($"已通知Server過渡卷刪除");
                PublicComm.akkaLog.Info($"已通知Server過渡卷刪除");

            }
           
        }

        /// <summary>
        /// 确认删除过渡卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DelDummy_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"请确认是否删除过渡卷:[{Dgv_DM.CurrentRow.Cells[1].Value.ToString().Trim()}]?", "删除过渡卷提示", Properties.Resources.dialogQuestion, 1); //MessageBox.Show($"请确认是否删除过渡卷:[{Dgv_DM.CurrentRow.Cells[1].Value.ToString().Trim()}]?", "提示", MessageBoxButtons.OKCancel);

            if (dialogR.Equals(DialogResult.OK))
            {
                string strSql = string.Empty;

                //检查是否有删除记录
                strSql = SqlFactory.Frm_1_1_SelectDeleteScheduleRecord(Lbl_DummyCoil.Text);
                DataTable dtCheckRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除过渡卷记录", "1-1");

                if (!dtCheckRecord.IsNull())
                {
                    EventLogHandler.Instance.EventPush_Message($"此过渡卷[{Lbl_DummyCoil.Text.Trim()}]已有删除记录，请重新确认");
                    return;
                } 


                //刪除排程紀錄
                strSql = SqlFactory.Frm_1_1_DeleteDummyRecord();

                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除过渡卷", "1-1"))
                {
                    EventLogHandler.Instance.EventPush_Message($"删除过渡卷失败");
                    return;
                }


                SCCommMsg.CS20_DeleteDummy Msg = new SCCommMsg.CS20_DeleteDummy()
                { 
                    DummyCoil = Lbl_DummyCoil.Text,
                    ReasonCode = Cob_DummyDelReason.SelectedValue.ToString()
                };

                PublicComm.Client.Tell(Msg);

                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}过渡卷删除", $"已通知Server删除过渡卷:[{Dgv_DM.CurrentRow.Cells[1].Value}]");
                EventLogHandler.Instance.EventPush_Message($"已通知Server删除过渡卷:[{Dgv_DM.CurrentRow.Cells[1].Value.ToString().Trim()}]");
                PublicComm.ClientLog.Info($"已通知Server過渡卷刪除");
                PublicComm.akkaLog.Info($"已通知Server過渡卷刪除");

                Grb_Dummy.Visible = false;
            }
        }

        /// <summary>
        /// 删除过渡卷取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DelDummyCancel_Click(object sender, EventArgs e)
        {
            Grb_Dummy.Visible = false;
        }
      
        /// <summary>
        /// 過渡卷刪除原因
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_DummyDelReason_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(Cob_DummyDelReason);
        }

        /// <summary>
        /// 计划号刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Plan_No_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_1_1_CoilSearch_SearchComboBoxItems_DB_PDI($" distinct [{nameof(PDIEntity.TBL_PDI.Plan_No)}] ");
            dt_cob = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "钢卷资讯计划号清单", "1-1");

            if (dt_cob.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无计划号清单");
                return;
            }    

            ComboBoxItems(Cob_Plan_No.Name, dt_cob);
        }

        /// <summary>
        /// 钢卷号刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Entry_Coil_No_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_1_1_CoilSearch_SearchComboBoxItems_DB_PDI(nameof(PDIEntity.TBL_PDI.In_Coil_ID));
            dt_cob = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "钢卷资讯钢卷号清单", "1-1");

            if (dt_cob.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无钢卷号清单");
                return;
            }

            ComboBoxItems(Cob_Entry_Coil_No.Name, dt_cob);
        }

        /// <summary>
        /// 过渡卷清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_DM_Click(object sender, EventArgs e)
        {
            string strSql = $"{SqlFactory.Frm_1_1_CoilSearch_SearchComboBoxItems_DB_PDI(nameof(PDIEntity.TBL_PDI.In_Coil_ID))} Where [{nameof(PDIEntity.TBL_PDI.Is_Dummy_Coil)}] = '1'";
            DataTable dtGetDM = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "过渡卷清单过渡卷号清单", "1-1");

            if (dtGetDM.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无过度卷清单");
                return;
            }

            ComboBoxItems(Cob_Entry_Coil_No_Dummy.Name, dtGetDM);
        }

        /// <summary>
        /// 將選中的 DataGridViewRow 轉成 DataRow.
        /// </summary>
        /// <param name="dgv">來源DataGridView</param>
        /// <param name="dt">來源DataTable</param>
        /// <returns></returns>
        public DataRow Fun_GetDataRowFromCurrentRow(DataGridView dgv, DataTable dtDt)
        {
            if (dgv.CurrentRow == null) { return null; }
            if (dgv.SelectedRows.Count <= 0) { return null; }
            DataRowView drv = dgv.SelectedRows[0].DataBoundItem as DataRowView;
            int index = dtDt.Rows.IndexOf(drv.Row);
            DataRow dr = dtDt.Rows[index];

            return dr;
        }

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.Gold : Color.LightGray;
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {
            Lbl_UpdateMsg.Text = $"排程已更新 时间:{GlobalVariableHandler.Instance.getTime}";

            Pnl_NewSchMessage.Visible = true;

            PublicComm.ClientLog.Info($"收到排程刷新通知");
            PublicComm.akkaLog.Debug($"收到排程刷新通知");

            Timer_NewSchMag.Start();
        }

       
    }
}
