using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GPLManager
{
    public class PublicForms
    {
        private static readonly int _headerHeight = 56;        // Header height
        private static readonly int _formWidth = 1920;          // Form width
        private static readonly int _formHeight = 980;         // Form height


        public static Frm_0_0_Main Main = null;
        public static Frm_0_1_Login Login = null;
        public static Frm_0_2_Menu Menu = null;
        public static Frm_1_1_PDISchl PDISchl = null;
        public static Frm_1_2_PDIDetail PDIDetail = null;
        public static Frm_1_3_DeleteScheduleRecord DeleteScheduleRecord = null;
        public static frm_2_1_Tracking Tracking = null;
        public static Frm_2_2_GPLSetupHistory GPLSetupHistory = null;
        public static Frm_3_1_ProdCoilList ProdCoilList = null;
        public static Frm_3_2_ProdDetail PDODetail = null;
        public static Frm_3_3_GrindActualPerformance GrindActualPerformance = null;
        public static Frm_4_1_LineDelayRecord LineDelayRecord = null;
        public static frm_4_2_Utility Utility = null;
        public static Frm_4_2_WeighingRecord Weighing = null;
        public static Frm_4_3_DeviceParameters DeviceParameters = null;
        public static Frm_4_4_GrindPlan GrindPlan = null;
        public static Frm_4_5_Belts Belts = null;
        public static Frm_3_3_Report Report = null;
        public static Frm_5_1_EventLog EventLog = null;
        public static Frm_5_2_CodeMaintain CodeMaintain = null;
        public static Frm_5_3_UserSetup UserSetup = null;
        public static frm_5_4_CrewMaintenance CrewMaintenance = null;
        public static frm_5_5_Language Language = null;
        public static frm_5_6_NetworkStatus NetworkStatus = null;
        //public static frm_5_7_DeleteReasonCode deleteReasonCode = null;
        public static frm_Entry _Entry = null;
        //public static frm_PDOConfirm _PDOConfirm = null;
        //public static frm_Scan _Scan = null;
        public static Frm_Dummy _Dummy = null;

        public static Frm_DialogReject DialogReject = null;

        public static void ShowForm<T>(T form, Panel panel) where T : Form
        {
            foreach (Form f in panel.Controls.Cast<Control>().Where(x => x is Form))
            {
                //如果子視窗已經存在
                if (f.Name == form.Name)
                {
                    //將該子視窗設為焦點
                    f.Focus();
                    return;
                }
            }
            // Setup form properties
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Location = new Point(0, _headerHeight);

            // Show form
            form.Show();
            form.TopMost = true;//最上層

            // Add to panel
            panel.AutoScrollPosition = new Point(0, 0);
            panel.Controls.Add(form);

            // Reset size
            form.Width = _formWidth;
            form.Height = _formHeight;
            //form.Dock = DockStyle.Fill;
        }

        public static bool IsLoadExcel(string fileFullName)
        {
            //  嘗試開啟檔案
            try
            {
                //  開啟檔案取得資料流
                var s = File.Open(fileFullName, FileMode.Open, FileAccess.Read, FileShare.None);

                //  關閉檔案
                s.Close();

                //  檔案可載入
                return true;
            }
            catch
            {
                //  檔案不可載入
                return false;
            }
        }
    }
}
