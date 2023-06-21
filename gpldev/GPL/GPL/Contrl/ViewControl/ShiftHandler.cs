using DBService.Repository.WorkSchedule;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GPLManager
{
    public class ShiftHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly ShiftHandler INSTANCE = new ShiftHandler();
        }

        public static ShiftHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public class ScheduleData
        {
            
            public string Night;
           
            public string Morning;
           
            public string Afternoon;
            
            public string Rest;
        }

        string Night_Start;
        string Night_End;

        string Morning_Start;
        string Morning_End;

        string Afternoon_Start;
        string Afternoon_End;

        Dictionary<int, ScheduleData> DictionyTeam;

        public void Fun_ShiftLableDisplay(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                int intDay = Convert.ToInt32(dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)].ToString().Remove(0, 6));
                string strShift = Fun_ChangeShift(dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift)].ToString());

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Equals(nameof(WorkScheduleEntity.TBL_WorkSchedule.Team)))
                    {
                        string controlName; //Lbl_1_N 格式
                        controlName = "Lbl_" + intDay + "_" + strShift;
                        var control = PublicForms.CrewMaintenance.Pnl_Group.Controls.Find(controlName, true).FirstOrDefault();
                        if (string.IsNullOrEmpty(control.Text))
                        {
                            if (string.IsNullOrWhiteSpace(dr[dc].ToString()))
                            {
                                control.Text = "_ ";
                            }
                            else
                            {
                                control.Text = dr[dc].ToString();//不转换甲乙丙丁,直接显示ABCD //Fun_TeamChangeWord(dr[dc].ToString());
                            }
                        }
                        else
                        {
                            string resultText = string.Empty;
                            if (string.IsNullOrWhiteSpace(dr[dc].ToString().Trim()))
                            { resultText = control.Text.Trim() + "  |  " + "_ "; }
                            else { resultText = control.Text.Trim() + "  |  " + dr[dc].ToString().Trim(); }

                            //無資料
                            if (resultText.StartsWith("_ ") && resultText.EndsWith("_ ")) { resultText = string.Empty; }

                            control.Text = resultText;

                            //control.Text = control.Text.Trim() + "  |  " + dr[dc].ToString();//不转换甲乙丙丁,直接显示ABCD //Fun_TeamChangeWord(dr[dc].ToString()); 
                            //if (control.Text == "  |  ") { control.Text = string.Empty; }
                        }
                        //((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find("lbl_" + intDay + "_" + strShift, false)[0])).Text = TeamWordChange(dr[dc].ToString());
                    }
                }
            }
        }

        public bool Fun_DeleteShift(string ShiftDate)
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

        public bool Fun_InsertShift(DataTable dt)
        {
            dt.Columns.Remove(nameof(WorkScheduleEntity.TBL_WorkSchedule.SerNo));//自动给值
            dt.Columns.Remove(nameof(WorkScheduleEntity.TBL_WorkSchedule.CreateTime));//自动给值

            string strInsElem_New = SqlFactory.Fun_GetInsertSqlFromDataTable(dt, nameof(WorkScheduleEntity.TBL_WorkSchedule));

            try
            {
                Data_Access.GetInstance().Fun_ExecuteQuery(strInsElem_New, GlobalVariableHandler.Instance.strConn_GPL, "新增排班", "5-4");
                //EventLogHandler.Instance.LogInfo("5-4", $"新增排班", $"新增排班");
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

        public string Fun_ChangeShift(string strShift)
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

        /// <summary>
        /// 計算日期為星期幾
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="DaysInMonth"></param>
        public void Fun_SettingDayOfWeek(int Year , int Month)
        {
            Fun_ClearDayAndWeek();

            DateTime Date = new DateTime(Year, Month, 1);
            int intDayInMonth = DateTime.DaysInMonth(Year, Month);

            for (int days = 0; days < intDayInMonth; days++)
            {
                ((Label)PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_Week_{days + 1}", false)[0]).Text = Fun_WeekEnToZH(Date.AddDays(days).DayOfWeek.ToString());

                ((Label)PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_Day_{days + 1}", false)[0]).Text = (days + 1).ToString();

                Fun_SetLableVisible(days + 1, true);
            }

            //多餘的欄位 => Visible = false 
            for (int intDayLbl = 31; intDayLbl > intDayInMonth; intDayLbl--)
            {
                Fun_SetLableVisible(intDayLbl, false);
            }
        }

        private void Fun_SetLableVisible(int intLbl, bool bolShow)
        {
            ((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_Day_{intLbl }", false)[0])).Visible = bolShow;//日期:1
            ((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_Week_{intLbl}", false)[0])).Visible = bolShow;//星期
            ((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_{intLbl }_N", false)[0])).Visible = bolShow;//夜
            ((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_{intLbl }_M", false)[0])).Visible = bolShow;//早
            ((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_{intLbl }_A", false)[0])).Visible = bolShow;//中
            ((Label)(PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_{intLbl}_R", false)[0])).Visible = bolShow;//休
        }

        private void Fun_ClearDayAndWeek()
        {
            for (int days = 0; days < 31; days++)
            {
                ((Label)PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_Week_{days + 1}", false)[0]).Text = string.Empty;

                ((Label)PublicForms.CrewMaintenance.Pnl_Group.Controls.Find($"Lbl_Day_{days + 1}", false)[0]).Text = string.Empty;
            }
        }

        private string Fun_WeekEnToZH(string Week)
        {
            //判斷中英文狀態
            string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;

            switch (Week)
            {
                case "Monday": Week = userUICulture.Equals("en") ? "Mon" : "星期一"; break;
                case "Tuesday": Week = userUICulture.Equals("en") ? "Tue" : "星期二"; break;
                case "Wednesday": Week = userUICulture.Equals("en") ? "Wed" : "星期三"; break;
                case "Thursday": Week = userUICulture.Equals("en") ? "Thu" : "星期四"; break;
                case "Friday": Week = userUICulture.Equals("en") ? "Fri" : "星期五"; break;
                case "Saturday": Week = userUICulture.Equals("en") ? "Sat" : "星期六"; break;
                case "Sunday": Week = userUICulture.Equals("en") ? "Sun" : "星期日"; break;

                //case "Monday":  Week = userUICulture.Equals(LanguageHandler.English) ? "Mon" : "星期一"; break;
                //case "Tuesday": Week = userUICulture.Equals(LanguageHandler.English) ? "Tues" : "星期二"; break;
                //case "Wednesday": Week = userUICulture.Equals(LanguageHandler.English) ? "Wed" : "星期三"; break;
                //case "Thursday":  Week = userUICulture.Equals(LanguageHandler.English) ? "Thur" : "星期四"; break;
                //case "Friday": Week = userUICulture.Equals(LanguageHandler.English) ? "Fri" : "星期五"; break;
                //case "Saturday": Week = userUICulture.Equals(LanguageHandler.English) ? "Sat" : "星期六"; break;
                //case "Sunday": Week = userUICulture.Equals(LanguageHandler.English) ? "Sun" : "星期日"; break;
            }
            return Week;
        }
                
        /// <summary>
        /// 把Excel轉成DataTable
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public DataTable Fun_ExcelToDataTable(string FileName)
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

        public string[] Fun_GetShiftTimeRange(int intShift)
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


        #region 2022-08-19 整理 暂时 无用
        //private string Fun_FindShiftStartEndTime(int Shift, bool IsStartTime)
        //{
        //    string getTime = string.Empty;
        //    switch (Shift)
        //    {
        //        case 1:
        //            getTime = IsStartTime ? Night_Start : Night_End;
        //            break;
        //        case 2:
        //            getTime = IsStartTime ? Morning_Start : Morning_End;
        //            break;
        //        case 3:
        //            getTime = IsStartTime ? Afternoon_Start : Afternoon_End;
        //            break;
        //        case 4:
        //            getTime = string.Empty;
        //            break;
        //        default:
        //            getTime = string.Empty;
        //            break;
        //    }
        //    return getTime;
        //}
        //private DataTable Fun_DataTableColumnsSet(DataTable dt)
        //{
        //    DataColumn colString;

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.Team));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.Mode));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftStartTime));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftPerson));
        //    dt.Columns.Add(colString);

        //    colString = new DataColumn(nameof(WorkScheduleEntity.TBL_WorkSchedule.CreateTime));
        //    dt.Columns.Add(colString);

        //    return dt;
        //}
        ////2022/08 改用汇入Excel
        //public void Fun_Shift(DateTime date, string getMode, string getTeam, string getDays)
        //{
        //    DataTable dtGetWorkSch = new DataTable();

        //    dtGetWorkSch = Fun_DataTableColumnsSet(dtGetWorkSch);

        //    // 排班開始日期:Dtp_SatrtDate
        //    DateTime dateTime_S = date;
        //    int intYear = dateTime_S.Year;
        //    int intMonth = dateTime_S.Month;

        //    //取得 該月有幾天  內建fuction
        //    int intDayInMonth = DateTime.DaysInMonth(intYear, intMonth);

        //    Fun_SettingDayOfWeek(intYear, intMonth);

        //    string strMode = Fun_FindMode(getMode);

        //    //起始天數
        //    int Days = Convert.ToInt32(getDays);

        //    //依據選擇 變更排班LIST   //固定從夜班排起
        //    IEnumerable<int> teamList;
        //    int TargetDictionyTeamKey;


        //    if (strMode.Equals("1"))
        //    {
        //        teamList = DictionyTeam.Where(x => x.Value.Morning == getTeam).OrderByDescending(x => x.Key).Select(x => x.Key);
        //    }
        //    else
        //    {
        //        teamList = DictionyTeam.Where(x => x.Value.Night == getTeam).OrderByDescending(x => x.Key).Select(x => x.Key);
        //    }

        //    TargetDictionyTeamKey = teamList.ElementAt(Days - 1);


        //    //循環天數
        //    int Cycle = strMode.Equals("4") ? 24 : 12;

        //    DateTime getShifDate;

        //    //本月排班LIST
        //    for (int i = 0; i < intDayInMonth; i++)
        //    {
        //        var target = DictionyTeam.Where(x => x.Key == (i + 1 + TargetDictionyTeamKey - 1) % Cycle)?.FirstOrDefault();

        //        if (target.Value.Value == null)  //餘數0
        //        {
        //            target = DictionyTeam.Where(x => x.Key == Cycle).FirstOrDefault();
        //        }

        //        getShifDate = new DateTime(intYear, intMonth, i + 1);

        //        for (int Shift = 1; Shift < 5; Shift++)  //夜、早、中、休
        //        {

        //            if (strMode.Equals("1"))
        //            {
        //                if (Shift.Equals(1)) continue;
        //            }
        //            else if (strMode.Equals("2") || strMode.Equals("3"))
        //            {
        //                if (Shift.Equals(3)) continue;
        //            }


        //            DataRow drAry = dtGetWorkSch.NewRow();
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift)] = Shift;
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.Team)] = GetTeam(Shift, target);
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)] = getShifDate.ToString("yyyyMMdd");
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.Mode)] = Convert.ToInt32(strMode);
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftStartTime)] = Fun_FindShiftStartEndTime(Shift, true);
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime)] = Fun_FindShiftStartEndTime(Shift, false);
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftPerson)] = PublicForms.Main.lblLoginUser.Text.Trim();
        //            drAry[nameof(WorkScheduleEntity.TBL_WorkSchedule.CreateTime)] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //            dtGetWorkSch.Rows.Add(drAry);

        //        }

        //    }

        //    Fun_ShiftLableDisplay(dtGetWorkSch);

        //    if (!Fun_DeleteShift($"{intYear.ToString() + intMonth.ToString().PadLeft(2, '0') }"))
        //    {
        //        EventLogHandler.Instance.EventPush_Message($"删除排班失败");
        //        return;
        //    }

        //    if (!Fun_InsertShift(dtGetWorkSch))
        //    {
        //        EventLogHandler.Instance.EventPush_Message($"新增排班失败");
        //        return;
        //    }

        //    Fun_ShiftTimeDisplay();

        //    PublicForms.CrewMaintenance.Lbl_NowMonth.Text = PublicForms.CrewMaintenance.Dtp_SatrtDate.Value.ToString("yyyy 年 MM 月");

        //    PublicForms.CrewMaintenance.Lbl_Mode_Show.Text = $"排班原则:{getMode}";
        //}
        //public void Fun_ShiftTimeDisplay()
        //{
        //    PublicForms.CrewMaintenance.Lbl_Night.Text =
        //        PublicForms.CrewMaintenance.Lbl_Night_1.Text =
        //        $"{Night_Start}-{Night_End}";

        //    PublicForms.CrewMaintenance.Lbl_Morning.Text =
        //        PublicForms.CrewMaintenance.Lbl_Morning_1.Text =
        //        $"{Morning_Start}-{Morning_End}";

        //    PublicForms.CrewMaintenance.Lbl_Afternoon.Text =
        //        PublicForms.CrewMaintenance.Lbl_Afternoon_1.Text =
        //        $"{Afternoon_Start}-{Afternoon_End}";
        //}
        //public string Fun_ModeWordChange(string Mode)
        //{
        //    switch (Mode)
        //    {
        //        case "1":
        //            Mode = "Case_1";
        //            break;
        //        case "2":
        //            Mode = "Case_2";
        //            break;
        //        case "3":
        //            Mode = "Case_3";
        //            break;
        //        case "4":
        //            Mode = "Case_4";
        //            break;
        //        default:
        //            break;
        //    }
        //    return Mode;
        //}
        //public string Fun_FindMode(string Mode)
        //{
        //    string strMode = string.Empty;
        //    switch (Mode)
        //    {
        //        case "Case_1":
        //            Fun_ShiftCase_1();
        //            ShiftTime_Case_1();
        //            strMode = "1";
        //            break;

        //        case "Case_2":
        //            Fun_ShiftCase_2();
        //            ShiftTime_Case_2();
        //            strMode = "2";
        //            break;

        //        case "Case_3":
        //            Fun_ShiftCase_3();
        //            ShiftTime_Case_3();
        //            strMode = "3";
        //            break;
        //        case "Case_4":
        //            Fun_ShiftCase_4();
        //            ShiftTime_Case_4();
        //            strMode = "4";
        //            break;
        //        default:
        //            break;
        //    }
        //    return strMode;
        //}
        //public void Fun_ShiftCase_1()
        //{
        //    DictionyTeam = new Dictionary<int, ScheduleData>
        //        {
        //            {1,new ScheduleData{ Night = string.Empty, Morning = "A", Afternoon = "B", Rest = "C"} },

        //            {2,new ScheduleData{ Night = string.Empty, Morning = "A", Afternoon = "B", Rest = "C"} },

        //            {3,new ScheduleData{ Night = string.Empty, Morning = "A", Afternoon = "C", Rest = "B"} },

        //            {4,new ScheduleData{ Night = string.Empty, Morning = "A", Afternoon = "C", Rest = "B"} },

        //            {5,new ScheduleData{ Night = string.Empty, Morning = "B", Afternoon = "C", Rest = "A"} },

        //            {6,new ScheduleData{ Night = string.Empty, Morning = "B", Afternoon = "C", Rest = "A"} },

        //            {7,new ScheduleData{ Night = string.Empty, Morning = "B", Afternoon = "A", Rest = "C"} },

        //            {8,new ScheduleData{ Night = string.Empty, Morning = "B", Afternoon = "A", Rest = "C"} },

        //            {9,new ScheduleData{ Night = string.Empty, Morning = "C", Afternoon = "A", Rest = "B"} },

        //            {10,new ScheduleData{ Night = string.Empty, Morning = "C", Afternoon = "A", Rest = "B"} },

        //            {11,new ScheduleData{ Night = string.Empty, Morning = "C", Afternoon = "B", Rest = "A"} },

        //            {12,new ScheduleData{ Night = string.Empty, Morning = "C", Afternoon = "B", Rest = "A"} },

        //        };
        //}
        //public void Fun_ShiftCase_2()
        //{
        //    DictionyTeam = new Dictionary<int, ScheduleData>
        //        {
        //            {1,new ScheduleData{ Night = "B", Morning = "A", Afternoon = string.Empty, Rest = "C"} },

        //            {2,new ScheduleData{ Night = "B", Morning = "A", Afternoon = string.Empty, Rest = "C"} },

        //            {3,new ScheduleData{ Night = "C", Morning = "A", Afternoon = string.Empty, Rest = "B"} },

        //            {4,new ScheduleData{ Night = "C", Morning = "A", Afternoon = string.Empty, Rest = "B"} },

        //            {5,new ScheduleData{ Night = "C", Morning = "B", Afternoon = string.Empty, Rest = "A"} },

        //            {6,new ScheduleData{ Night = "C", Morning = "B", Afternoon = string.Empty, Rest = "A"} },

        //            {7,new ScheduleData{ Night = "A", Morning = "B", Afternoon = string.Empty, Rest = "C"} },

        //            {8,new ScheduleData{ Night = "A", Morning = "B", Afternoon = string.Empty, Rest = "C"} },

        //            {9,new ScheduleData{ Night = "A", Morning = "C", Afternoon = string.Empty, Rest = "B"} },

        //            {10,new ScheduleData{ Night = "A", Morning = "C", Afternoon = string.Empty, Rest = "B"} },

        //            {11,new ScheduleData{ Night = "B", Morning = "C", Afternoon = string.Empty, Rest = "A"} },

        //            {12,new ScheduleData{ Night = "B", Morning = "C", Afternoon = string.Empty, Rest = "A"} },

        //        };
        //}
        //public void Fun_ShiftCase_3()
        //{
        //    DictionyTeam = new Dictionary<int, ScheduleData>
        //        {
        //            {1,new ScheduleData{ Night = "B", Morning = "A", Afternoon = string.Empty, Rest = "C"} },

        //            {2,new ScheduleData{ Night = "B", Morning = "A", Afternoon = string.Empty, Rest = "C"} },

        //            {3,new ScheduleData{ Night = "A", Morning = "C", Afternoon = string.Empty, Rest = "B"} },

        //            {4,new ScheduleData{ Night = "A", Morning = "C", Afternoon = string.Empty, Rest = "B"} },

        //            {5,new ScheduleData{ Night = "C", Morning = "B", Afternoon = string.Empty, Rest = "A"} },

        //            {6,new ScheduleData{ Night = "C", Morning = "B", Afternoon = string.Empty, Rest = "A"} },

        //            {7,new ScheduleData{ Night = "B", Morning = "A", Afternoon = string.Empty, Rest = "C"} },

        //            {8,new ScheduleData{ Night = "B", Morning = "A", Afternoon = string.Empty, Rest = "C"} },

        //            {9,new ScheduleData{ Night = "A", Morning = "C", Afternoon = string.Empty, Rest = "B"} },

        //            {10,new ScheduleData{ Night = "A", Morning = "C", Afternoon = string.Empty, Rest = "B"} },

        //            {11,new ScheduleData{ Night = "C", Morning = "B", Afternoon = string.Empty, Rest = "A"} },

        //            {12,new ScheduleData{ Night = "C", Morning = "B", Afternoon = string.Empty, Rest = "A"} },

        //        };
        //}
        //public void Fun_ShiftCase_4()
        //{
        //    DictionyTeam = new Dictionary<int, ScheduleData>
        //        {
        //            {1,new ScheduleData{ Night = "A", Morning = "C", Afternoon = "D", Rest = "B"} },

        //            {2,new ScheduleData{ Night = "A", Morning = "C", Afternoon = "D", Rest = "B"} },

        //            {3,new ScheduleData{ Night = "A", Morning = "B", Afternoon = "D", Rest = "C"} },

        //            {4,new ScheduleData{ Night = "A", Morning = "B", Afternoon = "D", Rest = "C"} },

        //            {5,new ScheduleData{ Night = "A", Morning = "B", Afternoon = "C", Rest = "D"} },

        //            {6,new ScheduleData{ Night = "A", Morning = "B", Afternoon = "C", Rest = "D"} },

        //            {7,new ScheduleData{ Night = "D", Morning = "B", Afternoon = "C", Rest = "A"} },

        //            {8,new ScheduleData{ Night = "D", Morning = "B", Afternoon = "C", Rest = "A"} },

        //            {9,new ScheduleData{ Night = "D", Morning = "A", Afternoon = "C", Rest = "B"} },

        //            {10,new ScheduleData{ Night = "D", Morning = "A", Afternoon = "C", Rest = "B"} },

        //            {11,new ScheduleData{ Night = "D", Morning = "A", Afternoon = "B", Rest = "C"} },

        //            {12,new ScheduleData{ Night = "D", Morning = "A", Afternoon = "B", Rest = "C"} },

        //            {13,new ScheduleData{ Night = "C",Morning = "A",Afternoon = "B", Rest = "D"} },

        //            {14,new ScheduleData{ Night = "C",Morning = "A",Afternoon = "B", Rest = "D"} },

        //            {15,new ScheduleData{ Night = "C",Morning = "D",Afternoon = "B", Rest = "A"} },

        //            {16,new ScheduleData{ Night = "C",Morning = "D",Afternoon = "B", Rest = "A"} },

        //            {17,new ScheduleData{ Night = "C",Morning = "D",Afternoon = "A", Rest = "B"} },

        //            {18,new ScheduleData{ Night = "C",Morning = "D",Afternoon = "A", Rest = "B"} },

        //            {19,new ScheduleData{ Night = "B",Morning = "D",Afternoon = "A", Rest = "C"} },

        //            {20,new ScheduleData{ Night = "B",Morning = "D",Afternoon = "A", Rest = "C"} },

        //            {21,new ScheduleData{ Night = "B",Morning = "C",Afternoon = "A", Rest = "D"} },

        //            {22,new ScheduleData{ Night = "B",Morning = "C",Afternoon = "A", Rest = "D"} },

        //            {23,new ScheduleData{ Night = "B",Morning = "C",Afternoon = "D", Rest = "A"} },

        //            {24,new ScheduleData{ Night = "B",Morning = "C",Afternoon = "D", Rest = "A"} },
        //        };
        //}
        //public void ShiftTime_Case_1()
        //{
        //    Night_Start = string.Empty;
        //    Night_End = string.Empty;

        //    Morning_Start = "08:00";
        //    Morning_End = "16:00";

        //    Afternoon_Start = "16:00";
        //    Afternoon_End = "00:00";
        //}
        //public void ShiftTime_Case_2()
        //{
        //    Night_Start = "20:00";
        //    Night_End = "08:00";

        //    Morning_Start = "08:00";
        //    Morning_End = "20:00";

        //    Afternoon_Start = string.Empty;
        //    Afternoon_End = string.Empty;
        //}
        //public void ShiftTime_Case_3()
        //{
        //    Night_Start = "20:00";
        //    Night_End = "08:00";

        //    Morning_Start = "08:00";
        //    Morning_End = "20:00";

        //    Afternoon_Start = string.Empty;
        //    Afternoon_End = string.Empty;
        //}
        //public void ShiftTime_Case_4()
        //{
        //    Night_Start = "00:00";
        //    Night_End = "08:00";

        //    Morning_Start = "08:00";
        //    Morning_End = "16:00";

        //    Afternoon_Start = "16:00";
        //    Afternoon_End = "24:00";
        //}
        ////取得對應的班組  夜、早、中、休
        //private string GetTeam(int interval, KeyValuePair<int, ScheduleData>? keyValuePair)
        //{
        //    switch (interval)
        //    {
        //        case 1:
        //            {
        //                return keyValuePair.Value.Value.Night;
        //            }
        //        case 2:
        //            {
        //                return keyValuePair.Value.Value.Morning;
        //            }
        //        case 3:
        //            {
        //                return keyValuePair.Value.Value.Afternoon;
        //            }
        //        case 4:
        //            {
        //                return keyValuePair.Value.Value.Rest;
        //            }
        //        default: { return string.Empty; }
        //    }
        //}
        //private string Fun_TeamChangeWord(string team)
        //{
        //    string changeWord = "R";
        //    switch (team)
        //    {
        //        case "A": changeWord = "甲"; break;
        //        case "B": changeWord = "乙"; break;
        //        case "C": changeWord = "丙"; break;
        //        case "D": changeWord = "丁"; break;

        //        case "甲": changeWord = "A"; break;
        //        case "乙": changeWord = "B"; break;
        //        case "丙": changeWord = "C"; break;
        //        case "丁": changeWord = "D"; break;

        //        default: changeWord = string.Empty; break;
        //    }
        //    return changeWord;
        //}

        #endregion
    }
}
