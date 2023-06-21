using DBService.Repository.WorkSchedule;
using ExcelDataReader;
using GPLManager.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;


namespace GPLManager
{
    public partial class frm_5_4_CrewMaintenance : Form
    {
        DataTable dtGetShift;
        DataTable dtBeforeEdit;

        string[] strArr_Shift;
        string[] strArr_Team;
        //語系
        private LanguageHandler LanguageHand;
        public frm_5_4_CrewMaintenance()
        {
            InitializeComponent();
        }
        private void Frm_5_4_CrewMaintenance_Load(object sender, EventArgs e)
        {
            PublicForms.CrewMaintenance = this;
            Control[] Frm_5_4_Control = new Control[] {
               Btn_Import 
            };//, Btn_ArrangeCrew
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_4_Control, UserSetupHandler.Instance.Frm_5_4);
            
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_Team);

            Dtp_SearchDate.Value = DateTime.Now;
            Fun_Search();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
               
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_Search();
        }
        /// <summary>
        /// 汇入Excel班表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Import_Click(object sender, EventArgs e)
        {
            string strEx_YearMonth = "";
            string strEx_Year = "";
            string strEx_Month = "";
            int intDayInMonth = 31;//預設31天

            //匯入排班
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileName = dialog.FileName;

                    if (!PublicForms.IsLoadExcel(fileName))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{fileName}正在开启中", "汇入排班", null, 2);
                        return;
                    }

                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，汇入来源:{fileName}");
                    EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，汇入来源:{fileName}。");

                    DataTable dtGetExcel = ShiftHandler.Instance.Fun_ExcelToDataTable(fileName);
                    string strSql = string.Empty;

                    if (dtGetExcel.IsNull())
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel转换结果无资料", "汇入排班", null, 3);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，Excel转换结果无资料。");
                        return;
                    }

                    //List<string> importList = new List<string>();
                    int row = 0;
                    //int col = 0;
                    DataTable dtExTo = dtGetShift.Clone();

                    foreach (DataRow datarow in dtGetExcel.Rows)
                    {
                        row++;
                        if (row == 1)
                        {
                            if (!string.IsNullOrEmpty(datarow[2].ToString()))
                                strEx_Year = datarow[2].ToString();//取得汇入 年
                            if (!string.IsNullOrEmpty(datarow[4].ToString()))
                                strEx_Month = datarow[4].ToString();//取得汇入 月
                            strEx_YearMonth = $"{strEx_Year}{strEx_Month.PadLeft(2, '0')}";

                            if (!string.IsNullOrEmpty(strEx_Year) && !string.IsNullOrEmpty(strEx_Month))
                                intDayInMonth = DateTime.DaysInMonth(Convert.ToInt32(strEx_Year), Convert.ToInt32(strEx_Month));
                        }

                        if (row < 4) continue;//第4行開始取班表
                        if (row >= 4 + intDayInMonth) continue;// 超過 該月份天數不取值

                        // 7 = 晚2早2中2休1
                        for (int i = 1; i <= 7; i++)
                        {
                            DataRow dr = dtExTo.NewRow();
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)] = $"{strEx_YearMonth}{datarow[0].ToString().PadLeft(2, '0')}";//yyyyMMdd

                            string strShift = "0";
                            switch (i)
                            {
                                //晚
                                case 1:
                                case 2: strShift = "1"; break;
                                //早
                                case 3:
                                case 4: strShift = "2"; break;
                                //中
                                case 5:
                                case 6: strShift = "3"; break;
                                //休
                                case 7: strShift = "4"; break;
                            }

                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift)] = strShift;
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Team)] = datarow[1 + i].ToString();//ABCD
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Mode)] = "1";//欄位暫時無用,先給預設值 1
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftStartTime)] = ShiftHandler.Instance.Fun_GetShiftTimeRange(i)[0];// "00:00"
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime)] = ShiftHandler.Instance.Fun_GetShiftTimeRange(i)[1];// "04:00"
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftPerson)] = PublicForms.Main.lblLoginUser.Text.Trim();
                            //dr["CreateTime"] = DateTime.Now;  //資料表直接給值 getdate()
                            dtExTo.Rows.Add(dr);
                        }
                    }

                    if (dtExTo.Rows.Count.Equals(0))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel资料转换异常", "汇入排班", null, 0);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，Excel资料转换异常。");
                        return;
                    }

                    string strMessage = $"{strEx_Year}年{strEx_Month}月排班资料";
                    // 清空匯入的 yyyy 年 MM 月 排班 ---刪除資料庫(TBL_WorkSchedule)                    
                    if (ShiftHandler.Instance.Fun_DeleteShift(strEx_YearMonth))
                    {
                        //成功不跳视窗
                        // DialogHandler.Instance.Fun_DialogShowOk($"清除{strMessage}成功", "汇入排班", null,4);
                        //EventLogHandler.Instance.LogInfo("5-4", $"删除排班", $"清除{strMessage}，成功。");
                    }
                    else
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"清除{strMessage}失败", "汇入排班", null, 3);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，清除{strMessage}失败。");
                    }


                    // 寫入資料庫(TBL_WorkSchedule)
                    if (ShiftHandler.Instance.Fun_InsertShift(dtExTo))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"汇入 {strMessage}成功", "汇入排班", null, 4);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入{strMessage}，成功。");
                        EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入 {strMessage}，成功。");
                    }
                    else
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"汇入{strMessage}失败", "汇入排班", null, 3);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，汇入{strMessage}失败。");
                    }

                }
                catch (Exception ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    EventLogHandler.Instance.LogDebug("5-4", "汇入排班", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    PublicComm.ClientLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    PublicComm.ExceptionLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    return;
                }

                Dtp_SearchDate.Value = new DateTime(Convert.ToInt32(strEx_Year), Convert.ToInt32(strEx_Month), 1);
                Fun_Search();
            }

        }
        //查询 实做
        private void Fun_Search()
        {
            //查詢條件空白，不執行後續程式
            if (Dtp_SearchDate.Value.ToString().IsEmpty()) { return; }

            string strS_Date = Dtp_SearchDate.Value.ToString("yyyyMM");

            //依條件查詢資料
            string strSql = SqlFactory.Frm_5_4_SelectShift(strS_Date);
            dtGetShift = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "班表", "5-4");

            if (dtGetShift.IsNull()) 
            {               
                //清除栏位资料
                Fun_DataClear();
                string strSearchDate = Dtp_SearchDate.Value.ToString("yyyy/MM");
                DialogHandler.Instance.Fun_DialogShowOk($"查询{strSearchDate} 班表:\r\n因尚未建立{strSearchDate} 资料,无资料显示!", "查询班表", null, 0);
                // return;
            }
            else
            {
                //清除栏位资料
                Fun_DataClear(); 
                //栏位给值
                ShiftHandler.Instance.Fun_ShiftLableDisplay(dtGetShift);

                #region 目前不使用 Mode 
                //string strMode = ShiftHandler.Instance.Fun_ModeWordChange(dtGetShift.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.Mode)].ToString());
                //Lbl_NowMonth.Text = Dtp_SearchDate.Value.ToString("yyyy 年 MM 月");
                //Lbl_Mode_Show.Text = $"排班原则:{strMode}";
                //ShiftHandler.Instance.Fun_FindMode(strMode);
                //ShiftHandler.Instance.Fun_ShiftTimeDisplay();
                #endregion
                #region 搬到 ShiftHandler.Fun_ShiftLableDisplay(DataTable dt)
                //foreach (DataRow dr in dtGetShift.Rows)
                //{
                //    int intDay = Convert.ToInt32(dr[nameof(TBL_WorkSchedule.ShiftDate)].ToString().Remove(0, 6));
                //    string strShift = ShiftHandler.Instance.Fun_ChangeShift(dr[nameof(TBL_WorkSchedule.Shift)].ToString());

                //    foreach (DataColumn dc in dtGetShift.Columns)
                //    {
                //        if (dc.ColumnName.Equals(nameof(TBL_WorkSchedule.Team)))
                //        {
                //            string controlName; //Lbl_1_N 格式
                //            controlName = "Lbl_" + intDay + "_" + strShift;
                //            var control = Pnl_Group.Controls.Find(controlName, true).FirstOrDefault();
                //            if (string.IsNullOrEmpty(control.Text))
                //            {
                //                if (string.IsNullOrWhiteSpace(dr[dc].ToString()))
                //                {
                //                    control.Text = " ";
                //                }
                //                else
                //                {
                //                    control.Text = dr[dc].ToString();//不转换甲乙丙丁,直接显示ABCD //Fun_TeamChangeWord(dr[dc].ToString());
                //                }
                //            }
                //            else
                //            {
                //                control.Text = control.Text.Trim() + "  |  " + dr[dc].ToString();//不转换甲乙丙丁,直接显示ABCD //Fun_TeamChangeWord(dr[dc].ToString()); 
                //                if (control.Text == "  |  ") { control.Text = string.Empty; }
                //            }

                //        }
                //    }
                //}
                #endregion
            }
            Lbl_NowMonth.Text = Dtp_SearchDate.Value.ToString("yyyy / MM ");
            ShiftHandler.Instance.Fun_SettingDayOfWeek(short.Parse(Dtp_SearchDate.Value.ToString("yyyy")), short.Parse(Dtp_SearchDate.Value.ToString("MM")));
        }
        //清空畫面欄位資料
        private void Fun_DataClear()
        {
            for (int i = 1; i <= 31; i++)
            {
                ((Label)(Pnl_Group.Controls.Find($"Lbl_Day_{i }", false)[0])).Text = "";//日期:1
                ((Label)(Pnl_Group.Controls.Find($"Lbl_Week_{i}", false)[0])).Text = "";//星期
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_N", false)[0])).Text = string.Empty;
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_M", false)[0])).Text = string.Empty;
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_A", false)[0])).Text = string.Empty;
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_R", false)[0])).Text = string.Empty;
            }

            Lbl_NowMonth.Text = "     /    "; //"　   　年   　月";
            //Lbl_Mode_Show.Text = $"排班原则:";
            //Lbl_Night.Text = Lbl_Night_1.Text = string.Empty;
            //Lbl_Morning.Text = Lbl_Morning_1.Text = string.Empty;   
            //Lbl_Afternoon.Text = Lbl_Afternoon_1.Text = string.Empty;            
        }
            
        #region 語系切換,文字調整 Fun_LanguageIsEn_Font
        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }
        #endregion Fun_LanguageIsEn_Font End
              
        #region 2022-08 停用排班功能 

        private void Fun_SetShiftTeam()
        {

            DataTable dtSelectOne = new DataTable();
            dtSelectOne = dtGetShift.Clone();

            // 排班模式:做六休二 Nud_Work  Nud_Rest
            int intWork = Convert.ToInt32(Nud_Work.Value);
            int intRest = Convert.ToInt32(Nud_Rest.Value);

            // 班次:Shift  1-夜，2-早，3-中，4-休
            int intShift = Cob_Shift.SelectedIndex;
            Fun_SetShiftArray(intShift);

            // 班别:Team   A-甲，B-乙，C-丙，D-丁
            int intTeam = Cob_Team.SelectedIndex;
            Fun_SetTeamArray(intTeam);

            // 排班開始日期:Dtp_SatrtDate
            DateTime dateTime_S = Dtp_SatrtDate.Value;

            //取得 年 月 日
            int intYear = dateTime_S.Year;
            int intMonth = dateTime_S.Month;
            int intDays = dateTime_S.Day;
            //取得 該月有幾天
            int intDayInMonth = DateTime.DaysInMonth(intYear, intMonth);

            int intCount_D = 0;
            int intCount_S = 0;
            int intCount_T = 0;

            DateTime dTimeUpdated = DateTime.Now;

            //夜:lbl_1_N     早:lbl_1_M     中:lbl_1_A    休:lbl_1_R

            // 每日 ABCD
            for (int i = intDays; i <= intDayInMonth; i++)
            {
                intCount_D += 1;

                for (int r = 0; r < strArr_Shift.Length; r++)
                {
                    if (intCount_S > 3) { intCount_S = 0; }
                    for (int j = 0; j < strArr_Team.Length; j++)
                    {
                        ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[r], false)[0])).Text = Fun_TeamChangeWord(strArr_Team[j].ToString());

                        DataRow drAry = dtSelectOne.NewRow();
                        //todo 日期
                        drAry[nameof(TBL_WorkSchedule.ShiftDate)] = intYear.ToString() + intMonth.ToString().PadLeft(2, '0') + i.ToString().PadLeft(2, '0');
                        drAry[nameof(TBL_WorkSchedule.Shift)] = ShiftHandler.Instance.Fun_ChangeShift(strArr_Shift[r].ToString());
                        drAry[nameof(TBL_WorkSchedule.Team)] = strArr_Team[j].ToString();
                        drAry[nameof(TBL_WorkSchedule.CreateTime)] = GlobalVariableHandler.Instance.getTime;
                        drAry[nameof(TBL_WorkSchedule.ShiftPerson)] = PublicForms.Main.lblLoginUser.Text;
                        dtSelectOne.Rows.Add(drAry);

                        r += 1;
                        intCount_T += 1;
                    }
                    intCount_S += 1;
                }

                if ((intCount_D % intRest) == 0)
                {
                    if (intTeam < 3)
                    {
                        intTeam += 1;
                        Fun_SetTeamArray(intTeam);
                    }
                    else
                    {
                        intTeam = 0;
                        Fun_SetTeamArray(intTeam);
                    }
                    intCount_D = 0;
                }

            }

            string strSql = SqlFactory.Frm_5_4_DeleteOldWorkSchedule($"{intYear.ToString() + intMonth.ToString().PadLeft(2, '0') }");
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除原排班", "5-4");

            string strInsElem_New = SqlFactory.Fun_GetInsertSqlFromDataTable(dtSelectOne, nameof(TBL_WorkSchedule));
            Data_Access.GetInstance().Fun_ExecuteQuery(strInsElem_New, GlobalVariableHandler.Instance.strConn_GPL, "排班作业", "5-4");

        }
        /// <summary>
        /// 排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ArrangeCrew_Click(object sender, EventArgs e)
        {
            //Fun_DataClear();

            //if (Cob_Mode.Text.IsEmpty() || Cob_Team.Text.IsEmpty() || cobDays.Text.IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message("请确认排班模式、班组及起始天数是否有正确选填");
            //    return;
            //}

            //ShiftHandler.Instance.Fun_Shift(Dtp_SatrtDate.Value, Cob_Mode.Text, Cob_Team.SelectedValue.ToString(), cobDays.Text);
        }
        //班別
        private void Fun_SetShiftArray(int intShift)
        {
            switch (intShift)
            {
                case 0: strArr_Shift = new string[] { "N", "M", "A", "R" }; break;
                case 1: strArr_Shift = new string[] { "M", "A", "R", "N" }; break;
                case 2: strArr_Shift = new string[] { "A", "R", "N", "M" }; break;
                case 3: strArr_Shift = new string[] { "R", "N", "M", "A" }; break;
            }
        }
        //班次
        private void Fun_SetTeamArray(int intTeam)
        {
            switch (intTeam)
            {
                case 0: strArr_Team = new string[] { "A", "B", "C", "D" }; break;
                case 1: strArr_Team = new string[] { "B", "C", "D", "A" }; break;
                case 2: strArr_Team = new string[] { "C", "D", "A", "B" }; break;
                case 3: strArr_Team = new string[] { "D", "A", "B", "C" }; break;
            }

        }
        private string Fun_ChangeShift(string strShift)
        {
            string strNewShift = "R";
            switch (strShift)
            {
                case "1": strNewShift = "N"; break;
                case "2": strNewShift = "M"; break;
                case "3": strNewShift = "A"; break;
                case "4": strNewShift = "R"; break;

                case "N": strNewShift = "1"; break;
                case "M": strNewShift = "2"; break;
                case "A": strNewShift = "3"; break;
                case "R": strNewShift = "4"; break;

                default: strNewShift = string.Empty; break;
            }
            return strNewShift;
        }
        private string Fun_TeamChangeWord(string team)
        {
            string strChangeWord = string.Empty;
            switch (team)
            {
                case "A": strChangeWord = "甲"; break;
                case "B": strChangeWord = "乙"; break;
                case "C": strChangeWord = "丙"; break;
                case "D": strChangeWord = "丁"; break;

                case "甲": strChangeWord = "A"; break;
                case "乙": strChangeWord = "B"; break;
                case "丙": strChangeWord = "C"; break;
                case "丁": strChangeWord = "D"; break;

                default: strChangeWord = ""; break;
            }
            return strChangeWord;
        }
        private void CobMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cobDays.Items.Clear();
            //cobDays.Items.Add("1");
            //cobDays.Items.Add("2");
            //cobDays.Items.Add("3");
            //cobDays.Items.Add("4");
            //if (Cob_Mode.Text.Equals("Case_4"))
            //{
            //    cobDays.Items.Add("5");
            //    cobDays.Items.Add("6");
            //}
        }

        private void Btn_ModeSpare_Click(object sender, EventArgs e)
        {
            if (Pnl_Spare.Visible)
            {
                Pnl_Spare.Visible = false;
                return;
            }

            Pnl_Spare.Visible = true;
            Pnl_Spare.Size = new System.Drawing.Size(1085, 756);
            Pnl_Spare.Location = new System.Drawing.Point(560, 130);

        }

        private void Btn_ClosePanel_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }
        #endregion

        #region 搬到ShiftHandler
        // 把Excel轉成DataTable
        private DataTable Fun_ExcelToDataTable(string FileName)
        {
            FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;

            if (Path.GetExtension(FileName).Equals(".xls"))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            DataSet result = excelReader.AsDataSet();
            DataTable dt = result.Tables[0];
            stream.Dispose();

            return dt;
        }
        private string[] Fun_GetShiftTimeRange(int intShift)
        {
            string[] strAr = new string[] { "" };
            switch (intShift)
            {
                case 1: strAr = new string[] { "00:00", "04:00" }; break;
                case 2: strAr = new string[] { "04:00", "08:00" }; break;
                case 3: strAr = new string[] { "08:00", "12:00" }; break;
                case 4: strAr = new string[] { "12:00", "16:00" }; break;
                case 5: strAr = new string[] { "16:00", "20:00" }; break;
                case 6: strAr = new string[] { "20:00", "24:00" }; break;
                case 7: strAr = new string[] { "00:00", "24:00" }; break;
            }
            return strAr;
        }
        private bool Fun_DeleteShift(string ShiftDate)
        {
            string strSql = SqlFactory.Frm_5_4_DeleteOldWorkSchedule(ShiftDate);
            try
            {
                Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除排班", "5-4");
                EventLogHandler.Instance.LogInfo("5-4", $"删除排班", $"删除排班成功");
                PublicComm.ClientLog.Info($"刪除排班成功");
                return true;
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"删除排班失败:{ex}");
                EventLogHandler.Instance.LogDebug("5-4", $"删除排班", $"删除排班失败:{ex}");
                PublicComm.ClientLog.Info($"刪除排班失败:{ex}");
                return false;
            }
        }
        private bool Fun_InsertShift(DataTable dt)
        {
            dt.Columns.Remove("SerNo");
            dt.Columns.Remove("CreateTime");
            string strInsElem_New = SqlFactory.Fun_GetInsertSqlFromDataTable(dt, nameof(WorkScheduleEntity.TBL_WorkSchedule));

            try
            {
                Data_Access.GetInstance().Fun_ExecuteQuery(strInsElem_New, GlobalVariableHandler.Instance.strConn_GPL, "新增排班", "5-4");
                EventLogHandler.Instance.LogInfo("5-4", $"新增排班", $"新增排班");
                PublicComm.ClientLog.Info($"訊息名稱:新增排班 訊息:新增排班成功");
                return true;
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"删除排班失败:{ex}");
                EventLogHandler.Instance.LogDebug("5-4", $"新增排班", $"新增排班失败:{ex}");
                PublicComm.ClientLog.Info($"新增排班失败:{ex}");
                return false;
            }
        }
        private void Fun_InsertWorkSchedule(string strSqlComm, string Message = "")
        {
           if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSqlComm, GlobalVariableHandler.Instance.strConn_GPL, Message, "5-4"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Message}失败", Message,null, 3);

                return;
            }
        }
        #endregion

        private void Btn_Search_TextChanged(object sender, EventArgs e)
        {
            //特別處理:中英切換時,重新取得星期X
            Fun_Search();
        }
    }
}
