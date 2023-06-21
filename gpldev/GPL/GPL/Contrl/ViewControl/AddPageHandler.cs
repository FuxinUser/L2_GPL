using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using GPLManager.Util;
using DBService.L1Repository;

namespace GPLManager
{
    public class AddPageHandler
    {
        string GetBeltPatterns = "";
        DataTable dtGetBeltPatterns = new DataTable();
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly AddPageHandler INSTANCE = new AddPageHandler();
        }

        public static AddPageHandler AddPageInstance { get { return SingletonHolder.INSTANCE; } }

        /// <summary>
        /// 最高九個道次
        /// 判斷頭、中、尾段各需要新增幾個TabPage
        /// </summary>
        /// <param name="tab">頭、中、尾段的TabPage</param>
        /// <param name="intPassNumber">總道次(需要新增的TabPage數量)</param>
        /// <param name="PassSection">頭、中、尾段(H、M、T)</param>
        /// <param name="dt">要填入的資料(204&205)</param>
        public void AddPage(TabControl tab, int intPassNumber, string PassSection, DataTable dt)
        {
            if (dt.IsNull()) return;
            // string Coil_ID = dt.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.CoilId)].ToString();
            string strPassSection;
            switch (PassSection)
            {
                case "H":
                    strPassSection = "Head";
                    for (int i = 1; i <= intPassNumber; i++)
                    {
                        TabPage page_H_1 = new TabPage();
                        frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                        page_H_1.Text = $"道次:{i}";
                        AddPage_Final(tab, page_H_1, control_H_1);
                        SelectGR_Info(control_H_1, dt, i, strPassSection);
                    }
                    break;

                case "M":
                    strPassSection = "Center";
                    for (int i = 1; i <= intPassNumber; i++)
                    {
                        TabPage page_H_1 = new TabPage();
                        frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                        page_H_1.Text = $"道次:{i}";
                        AddPage_Final(tab, page_H_1, control_H_1);
                        // SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);
                        SelectGR_Info(control_H_1, dt, i, strPassSection);
                    }
                    break;

                case "T":
                    strPassSection = "Tail";
                    for (int i = 1; i <= intPassNumber; i++)
                    {
                        TabPage page_H_1 = new TabPage();
                        frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                        page_H_1.Text = $"道次:{i}";
                        AddPage_Final(tab, page_H_1, control_H_1);
                        // SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);
                        SelectGR_Info(control_H_1, dt, i, strPassSection);
                    }
                    break;

                default:
                    strPassSection = "Head";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrPage">頭、中、尾段的TabPage</param>
        /// <param name="dt">要填入的資料來源(204&205)</param>
        /// <param name="intPassNum">道次(組字串取資料用)</param>
        /// <param name="strPassSection">頭、中、尾段(組字串取資料用)</param>
        private void SelectGR_Info(frm_2_2_UserControl ctrPage, DataTable dt, int intPassNum, string strPassSection)
        {
            if (dt.IsNull()) {  return;  }
            //填資料
            for (int i = 1; i <= 6; i++)
            {
                string strDire = dt.Rows[0][$"No{i}GrAbBeltRotatingDirection{intPassNum}{strPassSection}"].ToString() == "0" ? "正转" : "反转";
                ((TextBox)(ctrPage.Controls.Find($"GR_{i}_MaterialCode", true)[0])).Text = dt.Rows[0][$"No{i}GrAbBeltKind{intPassNum}{strPassSection}"].ToString();
                ((TextBox)(ctrPage.Controls.Find($"GR_{i}_ParticleNumber", true)[0])).Text = dt.Rows[0][$"No{i}GrAbBeltRoughness{intPassNum}{strPassSection}"].ToString();
                ((TextBox)(ctrPage.Controls.Find($"GR_{i}_Direction", true)[0])).Text = strDire;
                ((TextBox)(ctrPage.Controls.Find($"GR_{i}_Current", true)[0])).Text = dt.Rows[0][$"No{i}GrGrinderMotorCurrentSet{intPassNum}{strPassSection}"].ToString();

                if (i < 3)
                    ((TextBox)(ctrPage.Controls.Find($"GR_{i}_Speed", true)[0])).Text = dt.Rows[0][$"No{i}GrAbBeltSpeed{intPassNum}{strPassSection}"].ToString();
            }
        }

        /// <summary>
        /// TabControl /
        /// TabPage /
        /// UserControl
        /// 將UserControl加入Page，在將Page加入至TabControl
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="page"></param>
        /// <param name="control"></param>
        private void AddPage_Final(TabControl tab, TabPage page, frm_2_2_UserControl control)
        {
            page.Controls.Add(control);
            tab.TabPages.Add(page);
        }

        #region Old_適合原查GrindPlanRecords , SectionGrindPlanHistory 使用

        /// <summary>
        /// 最高九個道次
        /// 判斷頭、中、尾段各需要新增幾個TabPage
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void AddPage(TabControl tab,DataTable dt, string PassSection,string BeltPattern,string Coil_ID)
        {
            if (dt.IsNull()) return;
            GetBeltPatterns = BeltPattern;
            switch (dt.Rows.Count)
            {
                case 0:
                case 1:
                    OnePage(tab, dt,PassSection, Coil_ID);
                    break;
                case 2:
                    TwoPages(tab, dt, PassSection, Coil_ID);
                    break;
                case 3:
                    ThreePages(tab, dt, PassSection, Coil_ID);
                    break;
                case 4:
                    FourPages(tab, dt, PassSection, Coil_ID);
                    break;
                case 5:
                    FivePages(tab, dt, PassSection, Coil_ID);
                    break;
                case 6:
                    SixPages(tab, dt, PassSection, Coil_ID);
                    break;
                case 7:
                    SevenPages(tab, dt, PassSection, Coil_ID);
                    break;
                case 8:
                    EightPages(tab, dt, PassSection, Coil_ID);
                    break;
                case 9:
                    NinePages(tab, dt, PassSection, Coil_ID);
                    break;
                default:
                    break;
            }
        }
              
        /// <summary>
        /// 一組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void OnePage(TabControl tab, DataTable dt,string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 兩組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void TwoPages(TabControl tab, DataTable dt, string PassSection, string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1,Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 三組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void ThreePages(TabControl tab, DataTable dt, string PassSection, string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);
                    AddPage_Final(tab, page_H_1, control_H_1);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);
                    AddPage_Final(tab, page_H_2, control_H_2);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);
                    AddPage_Final(tab, page_H_3, control_H_3);

                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);
                    AddPage_Final(tab, page_M_1, control_M_1);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);
                    AddPage_Final(tab, page_M_2, control_M_2);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);
                    AddPage_Final(tab, page_M_3, control_M_3);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);
                    AddPage_Final(tab, page_T_1, control_T_1);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);
                    AddPage_Final(tab, page_T_2, control_T_2);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);
                    AddPage_Final(tab, page_T_3, control_T_3);

                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 四組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void FourPages(TabControl tab, DataTable dt, string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_H_3, control_H_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);

                    TabPage page_H_4 = new TabPage();
                    frm_2_2_UserControl control_H_4 = new frm_2_2_UserControl();
                    page_H_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_H_4, control_H_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_H_4, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_M_3, control_M_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);

                    TabPage page_M_4 = new TabPage();
                    frm_2_2_UserControl control_M_4 = new frm_2_2_UserControl();
                    page_M_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_M_4, control_M_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_M_4, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_T_3, control_T_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);

                    TabPage page_T_4 = new TabPage();
                    frm_2_2_UserControl control_T_4 = new frm_2_2_UserControl();
                    page_T_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_T_4, control_T_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_T_4, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 五組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void FivePages(TabControl tab, DataTable dt, string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_H_3, control_H_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);

                    TabPage page_H_4 = new TabPage();
                    frm_2_2_UserControl control_H_4 = new frm_2_2_UserControl();
                    page_H_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_H_4, control_H_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_H_4, Coil_ID);

                    TabPage page_H_5 = new TabPage();
                    frm_2_2_UserControl control_H_5 = new frm_2_2_UserControl();
                    page_H_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_H_5, control_H_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_H_5, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_M_3, control_M_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);

                    TabPage page_M_4 = new TabPage();
                    frm_2_2_UserControl control_M_4 = new frm_2_2_UserControl();
                    page_M_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_M_4, control_M_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_M_4, Coil_ID);

                    TabPage page_M_5 = new TabPage();
                    frm_2_2_UserControl control_M_5 = new frm_2_2_UserControl();
                    page_M_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_M_5, control_M_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_M_5, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_T_3, control_T_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);

                    TabPage page_T_4 = new TabPage();
                    frm_2_2_UserControl control_T_4 = new frm_2_2_UserControl();
                    page_T_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_T_4, control_T_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_T_4, Coil_ID);

                    TabPage page_T_5 = new TabPage();
                    frm_2_2_UserControl control_T_5 = new frm_2_2_UserControl();
                    page_T_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_T_5, control_T_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_T_5, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 六組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void SixPages(TabControl tab, DataTable dt, string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_H_3, control_H_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);

                    TabPage page_H_4 = new TabPage();
                    frm_2_2_UserControl control_H_4 = new frm_2_2_UserControl();
                    page_H_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_H_4, control_H_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_H_4, Coil_ID);

                    TabPage page_H_5 = new TabPage();
                    frm_2_2_UserControl control_H_5 = new frm_2_2_UserControl();
                    page_H_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_H_5, control_H_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_H_5, Coil_ID);

                    TabPage page_H_6 = new TabPage();
                    frm_2_2_UserControl control_H_6 = new frm_2_2_UserControl();
                    page_H_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_H_6, control_H_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_H_6, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_M_3, control_M_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);

                    TabPage page_M_4 = new TabPage();
                    frm_2_2_UserControl control_M_4 = new frm_2_2_UserControl();
                    page_M_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_M_4, control_M_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_M_4, Coil_ID);

                    TabPage page_M_5 = new TabPage();
                    frm_2_2_UserControl control_M_5 = new frm_2_2_UserControl();
                    page_M_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_M_5, control_M_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_M_5, Coil_ID);

                    TabPage page_M_6 = new TabPage();
                    frm_2_2_UserControl control_M_6 = new frm_2_2_UserControl();
                    page_M_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_M_6, control_M_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_M_6, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_T_3, control_T_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);

                    TabPage page_T_4 = new TabPage();
                    frm_2_2_UserControl control_T_4 = new frm_2_2_UserControl();
                    page_T_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_T_4, control_T_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_T_4, Coil_ID);

                    TabPage page_T_5 = new TabPage();
                    frm_2_2_UserControl control_T_5 = new frm_2_2_UserControl();
                    page_T_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_T_5, control_T_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_T_5, Coil_ID);

                    TabPage page_T_6 = new TabPage();
                    frm_2_2_UserControl control_T_6 = new frm_2_2_UserControl();
                    page_T_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_T_6, control_T_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_T_6, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 七組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void SevenPages(TabControl tab, DataTable dt, string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_H_3, control_H_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);

                    TabPage page_H_4 = new TabPage();
                    frm_2_2_UserControl control_H_4 = new frm_2_2_UserControl();
                    page_H_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_H_4, control_H_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_H_4, Coil_ID);

                    TabPage page_H_5 = new TabPage();
                    frm_2_2_UserControl control_H_5 = new frm_2_2_UserControl();
                    page_H_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_H_5, control_H_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_H_5, Coil_ID);

                    TabPage page_H_6 = new TabPage();
                    frm_2_2_UserControl control_H_6 = new frm_2_2_UserControl();
                    page_H_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_H_6, control_H_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_H_6, Coil_ID);

                    TabPage page_H_7 = new TabPage();
                    frm_2_2_UserControl control_H_7 = new frm_2_2_UserControl();
                    page_H_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_H_7, control_H_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_H_7, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_M_3, control_M_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);

                    TabPage page_M_4 = new TabPage();
                    frm_2_2_UserControl control_M_4 = new frm_2_2_UserControl();
                    page_M_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_M_4, control_M_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_M_4, Coil_ID);

                    TabPage page_M_5 = new TabPage();
                    frm_2_2_UserControl control_M_5 = new frm_2_2_UserControl();
                    page_M_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_M_5, control_M_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_M_5, Coil_ID);

                    TabPage page_M_6 = new TabPage();
                    frm_2_2_UserControl control_M_6 = new frm_2_2_UserControl();
                    page_M_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_M_6, control_M_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_M_6, Coil_ID);

                    TabPage page_M_7 = new TabPage();
                    frm_2_2_UserControl control_M_7 = new frm_2_2_UserControl();
                    page_M_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_M_7, control_M_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_M_7, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_T_3, control_T_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);

                    TabPage page_T_4 = new TabPage();
                    frm_2_2_UserControl control_T_4 = new frm_2_2_UserControl();
                    page_T_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_T_4, control_T_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_T_4, Coil_ID);

                    TabPage page_T_5 = new TabPage();
                    frm_2_2_UserControl control_T_5 = new frm_2_2_UserControl();
                    page_T_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_T_5, control_T_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_T_5, Coil_ID);

                    TabPage page_T_6 = new TabPage();
                    frm_2_2_UserControl control_T_6 = new frm_2_2_UserControl();
                    page_T_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_T_6, control_T_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_T_6, Coil_ID);

                    TabPage page_T_7 = new TabPage();
                    frm_2_2_UserControl control_T_7 = new frm_2_2_UserControl();
                    page_T_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_T_7, control_T_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_T_7, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 八組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void EightPages(TabControl tab, DataTable dt, string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_H_3, control_H_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);

                    TabPage page_H_4 = new TabPage();
                    frm_2_2_UserControl control_H_4 = new frm_2_2_UserControl();
                    page_H_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_H_4, control_H_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_H_4, Coil_ID);

                    TabPage page_H_5 = new TabPage();
                    frm_2_2_UserControl control_H_5 = new frm_2_2_UserControl();
                    page_H_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_H_5, control_H_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_H_5, Coil_ID);

                    TabPage page_H_6 = new TabPage();
                    frm_2_2_UserControl control_H_6 = new frm_2_2_UserControl();
                    page_H_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_H_6, control_H_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_H_6, Coil_ID);

                    TabPage page_H_7 = new TabPage();
                    frm_2_2_UserControl control_H_7 = new frm_2_2_UserControl();
                    page_H_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_H_7, control_H_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_H_7, Coil_ID);

                    TabPage page_H_8 = new TabPage();
                    frm_2_2_UserControl control_H_8 = new frm_2_2_UserControl();
                    page_H_8.Text = $"道次区间:{dt.Rows[7][0]}~{dt.Rows[7][1]}";
                    AddPage_Final(tab, page_H_8, control_H_8);
                    SelectGR_Info(dt.Rows[7][0].ToString(), dt.Rows[7][1].ToString(), control_H_8, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_M_3, control_M_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);

                    TabPage page_M_4 = new TabPage();
                    frm_2_2_UserControl control_M_4 = new frm_2_2_UserControl();
                    page_M_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_M_4, control_M_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_M_4, Coil_ID);

                    TabPage page_M_5 = new TabPage();
                    frm_2_2_UserControl control_M_5 = new frm_2_2_UserControl();
                    page_M_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_M_5, control_M_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_M_5, Coil_ID);

                    TabPage page_M_6 = new TabPage();
                    frm_2_2_UserControl control_M_6 = new frm_2_2_UserControl();
                    page_M_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_M_6, control_M_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_M_6, Coil_ID);

                    TabPage page_M_7 = new TabPage();
                    frm_2_2_UserControl control_M_7 = new frm_2_2_UserControl();
                    page_M_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_M_7, control_M_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_M_7, Coil_ID);

                    TabPage page_M_8 = new TabPage();
                    frm_2_2_UserControl control_M_8 = new frm_2_2_UserControl();
                    page_M_8.Text = $"道次区间:{dt.Rows[7][0]}~{dt.Rows[7][1]}";
                    AddPage_Final(tab, page_M_8, control_M_8);
                    SelectGR_Info(dt.Rows[7][0].ToString(), dt.Rows[7][1].ToString(), control_M_8, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_T_3, control_T_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);

                    TabPage page_T_4 = new TabPage();
                    frm_2_2_UserControl control_T_4 = new frm_2_2_UserControl();
                    page_T_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_T_4, control_T_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_T_4, Coil_ID);

                    TabPage page_T_5 = new TabPage();
                    frm_2_2_UserControl control_T_5 = new frm_2_2_UserControl();
                    page_T_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_T_5, control_T_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_T_5, Coil_ID);

                    TabPage page_T_6 = new TabPage();
                    frm_2_2_UserControl control_T_6 = new frm_2_2_UserControl();
                    page_T_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_T_6, control_T_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_T_6, Coil_ID);

                    TabPage page_T_7 = new TabPage();
                    frm_2_2_UserControl control_T_7 = new frm_2_2_UserControl();
                    page_T_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_T_7, control_T_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_T_7, Coil_ID);

                    TabPage page_T_8 = new TabPage();
                    frm_2_2_UserControl control_T_8 = new frm_2_2_UserControl();
                    page_T_8.Text = $"道次区间:{dt.Rows[7][0]}~{dt.Rows[7][1]}";
                    AddPage_Final(tab, page_T_8, control_T_8);
                    SelectGR_Info(dt.Rows[7][0].ToString(), dt.Rows[7][1].ToString(), control_T_8, Coil_ID);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 九組道次区间
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="dt"></param>
        /// <param name="PassSection"></param>
        public void NinePages(TabControl tab, DataTable dt, string PassSection,string Coil_ID)
        {
            if (dt.IsNull()) return;

            switch (PassSection)
            {
                case "H":
                    TabPage page_H_1 = new TabPage();
                    frm_2_2_UserControl control_H_1 = new frm_2_2_UserControl();
                    page_H_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_H_1, control_H_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_H_1, Coil_ID);

                    TabPage page_H_2 = new TabPage();
                    frm_2_2_UserControl control_H_2 = new frm_2_2_UserControl();
                    page_H_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_H_2, control_H_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_H_2, Coil_ID);

                    TabPage page_H_3 = new TabPage();
                    frm_2_2_UserControl control_H_3 = new frm_2_2_UserControl();
                    page_H_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_H_3, control_H_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_H_3, Coil_ID);

                    TabPage page_H_4 = new TabPage();
                    frm_2_2_UserControl control_H_4 = new frm_2_2_UserControl();
                    page_H_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_H_4, control_H_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_H_4, Coil_ID);

                    TabPage page_H_5 = new TabPage();
                    frm_2_2_UserControl control_H_5 = new frm_2_2_UserControl();
                    page_H_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_H_5, control_H_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_H_5, Coil_ID);

                    TabPage page_H_6 = new TabPage();
                    frm_2_2_UserControl control_H_6 = new frm_2_2_UserControl();
                    page_H_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_H_6, control_H_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_H_6, Coil_ID);

                    TabPage page_H_7 = new TabPage();
                    frm_2_2_UserControl control_H_7 = new frm_2_2_UserControl();
                    page_H_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_H_7, control_H_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_H_7, Coil_ID);

                    TabPage page_H_8 = new TabPage();
                    frm_2_2_UserControl control_H_8 = new frm_2_2_UserControl();
                    page_H_8.Text = $"道次区间:{dt.Rows[7][0]}~{dt.Rows[7][1]}";
                    AddPage_Final(tab, page_H_8, control_H_8);
                    SelectGR_Info(dt.Rows[7][0].ToString(), dt.Rows[7][1].ToString(), control_H_8, Coil_ID);

                    TabPage page_H_9 = new TabPage();
                    frm_2_2_UserControl control_H_9 = new frm_2_2_UserControl();
                    page_H_9.Text = $"道次区间:{dt.Rows[8][0]}~{dt.Rows[8][1]}";
                    AddPage_Final(tab, page_H_9, control_H_9);
                    SelectGR_Info(dt.Rows[8][0].ToString(), dt.Rows[8][1].ToString(), control_H_9, Coil_ID);
                    break;
                case "M":
                    TabPage page_M_1 = new TabPage();
                    frm_2_2_UserControl control_M_1 = new frm_2_2_UserControl();
                    page_M_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_M_1, control_M_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_M_1, Coil_ID);

                    TabPage page_M_2 = new TabPage();
                    frm_2_2_UserControl control_M_2 = new frm_2_2_UserControl();
                    page_M_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_M_2, control_M_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_M_2, Coil_ID);

                    TabPage page_M_3 = new TabPage();
                    frm_2_2_UserControl control_M_3 = new frm_2_2_UserControl();
                    page_M_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_M_3, control_M_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_M_3, Coil_ID);

                    TabPage page_M_4 = new TabPage();
                    frm_2_2_UserControl control_M_4 = new frm_2_2_UserControl();
                    page_M_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_M_4, control_M_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_M_4, Coil_ID);

                    TabPage page_M_5 = new TabPage();
                    frm_2_2_UserControl control_M_5 = new frm_2_2_UserControl();
                    page_M_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_M_5, control_M_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_M_5, Coil_ID);

                    TabPage page_M_6 = new TabPage();
                    frm_2_2_UserControl control_M_6 = new frm_2_2_UserControl();
                    page_M_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_M_6, control_M_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_M_6, Coil_ID);

                    TabPage page_M_7 = new TabPage();
                    frm_2_2_UserControl control_M_7 = new frm_2_2_UserControl();
                    page_M_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_M_7, control_M_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_M_7, Coil_ID);

                    TabPage page_M_8 = new TabPage();
                    frm_2_2_UserControl control_M_8 = new frm_2_2_UserControl();
                    page_M_8.Text = $"道次区间:{dt.Rows[7][0]}~{dt.Rows[7][1]}";
                    AddPage_Final(tab, page_M_8, control_M_8);
                    SelectGR_Info(dt.Rows[7][0].ToString(), dt.Rows[7][1].ToString(), control_M_8, Coil_ID);

                    TabPage page_M_9 = new TabPage();
                    frm_2_2_UserControl control_M_9 = new frm_2_2_UserControl();
                    page_M_9.Text = $"道次区间:{dt.Rows[8][0]}~{dt.Rows[8][1]}";
                    AddPage_Final(tab, page_M_9, control_M_9);
                    SelectGR_Info(dt.Rows[8][0].ToString(), dt.Rows[8][1].ToString(), control_M_9, Coil_ID);
                    break;
                case "T":
                    TabPage page_T_1 = new TabPage();
                    frm_2_2_UserControl control_T_1 = new frm_2_2_UserControl();
                    page_T_1.Text = $"道次区间:{dt.Rows[0][0]}~{dt.Rows[0][1]}";
                    AddPage_Final(tab, page_T_1, control_T_1);
                    SelectGR_Info(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), control_T_1, Coil_ID);

                    TabPage page_T_2 = new TabPage();
                    frm_2_2_UserControl control_T_2 = new frm_2_2_UserControl();
                    page_T_2.Text = $"道次区间:{dt.Rows[1][0]}~{dt.Rows[1][1]}";
                    AddPage_Final(tab, page_T_2, control_T_2);
                    SelectGR_Info(dt.Rows[1][0].ToString(), dt.Rows[1][1].ToString(), control_T_2, Coil_ID);

                    TabPage page_T_3 = new TabPage();
                    frm_2_2_UserControl control_T_3 = new frm_2_2_UserControl();
                    page_T_3.Text = $"道次区间:{dt.Rows[2][0]}~{dt.Rows[2][1]}";
                    AddPage_Final(tab, page_T_3, control_T_3);
                    SelectGR_Info(dt.Rows[2][0].ToString(), dt.Rows[2][1].ToString(), control_T_3, Coil_ID);

                    TabPage page_T_4 = new TabPage();
                    frm_2_2_UserControl control_T_4 = new frm_2_2_UserControl();
                    page_T_4.Text = $"道次区间:{dt.Rows[3][0]}~{dt.Rows[3][1]}";
                    AddPage_Final(tab, page_T_4, control_T_4);
                    SelectGR_Info(dt.Rows[3][0].ToString(), dt.Rows[3][1].ToString(), control_T_4, Coil_ID);

                    TabPage page_T_5 = new TabPage();
                    frm_2_2_UserControl control_T_5 = new frm_2_2_UserControl();
                    page_T_5.Text = $"道次区间:{dt.Rows[4][0]}~{dt.Rows[4][1]}";
                    AddPage_Final(tab, page_T_5, control_T_5);
                    SelectGR_Info(dt.Rows[4][0].ToString(), dt.Rows[4][1].ToString(), control_T_5, Coil_ID);

                    TabPage page_T_6 = new TabPage();
                    frm_2_2_UserControl control_T_6 = new frm_2_2_UserControl();
                    page_T_6.Text = $"道次区间:{dt.Rows[5][0]}~{dt.Rows[5][1]}";
                    AddPage_Final(tab, page_T_6, control_T_6);
                    SelectGR_Info(dt.Rows[5][0].ToString(), dt.Rows[5][1].ToString(), control_T_6, Coil_ID);

                    TabPage page_T_7 = new TabPage();
                    frm_2_2_UserControl control_T_7 = new frm_2_2_UserControl();
                    page_T_7.Text = $"道次区间:{dt.Rows[6][0]}~{dt.Rows[6][1]}";
                    AddPage_Final(tab, page_T_7, control_T_7);
                    SelectGR_Info(dt.Rows[6][0].ToString(), dt.Rows[6][1].ToString(), control_T_7, Coil_ID);

                    TabPage page_T_8 = new TabPage();
                    frm_2_2_UserControl control_T_8 = new frm_2_2_UserControl();
                    page_T_8.Text = $"道次区间:{dt.Rows[7][0]}~{dt.Rows[7][1]}";
                    AddPage_Final(tab, page_T_8, control_T_8);
                    SelectGR_Info(dt.Rows[7][0].ToString(), dt.Rows[7][1].ToString(), control_T_8, Coil_ID);

                    TabPage page_T_9 = new TabPage();
                    frm_2_2_UserControl control_T_9 = new frm_2_2_UserControl();
                    page_T_9.Text = $"道次区间:{dt.Rows[8][0]}~{dt.Rows[8][1]}";
                    AddPage_Final(tab, page_T_9, control_T_9);
                    SelectGR_Info(dt.Rows[8][0].ToString(), dt.Rows[8][1].ToString(), control_T_9, Coil_ID);
                    break;
                default:
                    break;
            }
        }
       
        private void SelectGR_Info(string PassFrom,string PassTo,frm_2_2_UserControl Control,string Coil_ID)
        {
            string strSql = SqlFactory.Frm_2_2_SelectBeltPatternsRecords_DB_TBL_BeltPatterns_Records(GetBeltPatterns, PassFrom,PassTo, Coil_ID);
            dtGetBeltPatterns = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"GR BeltPattrens","2-2");

            if (dtGetBeltPatterns.IsNull())
            {
                strSql = SqlFactory.Frm_2_2_SelectBeltPatterns_DB_TBL_BeltPatterns(GetBeltPatterns, PassFrom, PassTo); ;
                dtGetBeltPatterns = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "GR BeltPattrens", "2-2");
            }

            #region 填資料
            if (dtGetBeltPatterns.Rows.Count.Equals(0)) return;

            #region GR_1
            Control.GR_1_MaterialCode.Text = dtGetBeltPatterns.Rows[0][2].ToString();
            Control.GR_1_Current.Text = dtGetBeltPatterns.Rows[0][1].ToString();
            Control.GR_1_ParticleNumber.Text = dtGetBeltPatterns.Rows[0][3].ToString();
            Control.GR_1_Direction.Text = dtGetBeltPatterns.Rows[0][4].ToString() == "0" ? Control.GR_1_Direction.Text ="正转" : Control.GR_1_Direction.Text = "反转";
            Control.GR_1_Speed.Text = dtGetBeltPatterns.Rows[0][5].ToString();
            #endregion

            #region GR_2
            Control.GR_2_MaterialCode.Text = dtGetBeltPatterns.Rows[1][2].ToString();
            Control.GR_2_Current.Text = dtGetBeltPatterns.Rows[1][1].ToString();
            Control.GR_2_ParticleNumber.Text = dtGetBeltPatterns.Rows[1][3].ToString();
            Control.GR_2_Direction.Text = dtGetBeltPatterns.Rows[1][4].ToString() == "0" ? Control.GR_2_Direction.Text = "正转" : Control.GR_2_Direction.Text = "反转";
            Control.GR_2_Speed.Text = dtGetBeltPatterns.Rows[1][5].ToString();
            #endregion

            #region GR_3
            Control.GR_3_MaterialCode.Text = dtGetBeltPatterns.Rows[2][2].ToString();
            Control.GR_3_Current.Text = dtGetBeltPatterns.Rows[2][1].ToString();
            Control.GR_3_ParticleNumber.Text = dtGetBeltPatterns.Rows[2][3].ToString();
            Control.GR_3_Direction.Text = dtGetBeltPatterns.Rows[2][4].ToString() == "0" ? Control.GR_3_Direction.Text = "正转" : Control.GR_3_Direction.Text = "反转";
            #endregion

            #region GR_4
            Control.GR_4_MaterialCode.Text = dtGetBeltPatterns.Rows[3][2].ToString();
            Control.GR_4_Current.Text = dtGetBeltPatterns.Rows[3][1].ToString();
            Control.GR_4_ParticleNumber.Text = dtGetBeltPatterns.Rows[3][3].ToString();
            Control.GR_4_Direction.Text = dtGetBeltPatterns.Rows[3][4].ToString() == "0" ? Control.GR_4_Direction.Text = "正转" : Control.GR_4_Direction.Text = "反转";
            #endregion

            #region GR_5
            Control.GR_5_MaterialCode.Text = dtGetBeltPatterns.Rows[4][2].ToString();
            Control.GR_5_Current.Text = dtGetBeltPatterns.Rows[4][1].ToString();
            Control.GR_5_ParticleNumber.Text = dtGetBeltPatterns.Rows[4][3].ToString();
            Control.GR_5_Direction.Text = dtGetBeltPatterns.Rows[4][4].ToString() == "0" ? Control.GR_5_Direction.Text = "正转" : Control.GR_5_Direction.Text = "反转";
            #endregion

            #region GR_6
            Control.GR_6_MaterialCode.Text = dtGetBeltPatterns.Rows[5][2].ToString();
            Control.GR_6_Current.Text = dtGetBeltPatterns.Rows[5][1].ToString();
            Control.GR_6_ParticleNumber.Text = dtGetBeltPatterns.Rows[5][3].ToString();
            Control.GR_6_Direction.Text = dtGetBeltPatterns.Rows[5][4].ToString() == "0" ? Control.GR_6_Direction.Text = "正转" : Control.GR_6_Direction.Text = "反转";
            #endregion

            #endregion

        }
        #endregion Old_適合原查GrindPlanRecords , SectionGrindPlanHistory 使用_end


    }
}
