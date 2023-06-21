using GPLManager.Contrl;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using DataModel.HMIServerCom.Msg;


namespace GPLManager
{
    public partial class Frm_0_0_Main : Form
    {
        private string path = string.Empty;
        // 子視窗寬度
        public const int SubForm_Width = 1920;
        // 子視窗高度
        public const int SubForm_Height = 982;

        // 子視窗起始X座標
        public const int SubForm_StartX = 0;
        // 子視窗起始Y座標
        public const int SubForm_StartY = 55;

        //int intNews = 0;
        private LanguageHandler LanguageHand;

        public Frm_0_0_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 畫面顯示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_0_0_Main_Shown(object sender, EventArgs e)
        {
            GlobalVariableHandler.Instance.dtEventPush.Columns.Add(new DataColumn("Value"));
            GlobalVariableHandler.Instance.dtEventPush.Columns.Add(new DataColumn("msg"));
            CheckForIllegalCrossThreadCalls = false;
            lblUserIP.Text = GlobalVariableHandler.Instance.getIpAdderss.ToString();
            PublicComm.Start();
            PublicForms.Main = this;

            LanguageHand = new LanguageHandler();

            LanguageHandler.Instance.Current();

            // Timer For Time Clock
            TimerForClock.Enabled = true;
            CommonDef.MyLog.WriteLog("◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤系統啟動◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            PublicComm.ClientLog.Info($"◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤系統啟動◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤");
            PublicComm.ExceptionLog.Info($"◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤系統啟動◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤◥◤");

            try
            {
                Frm_0_1_Login FrmChild = new Frm_0_1_Login
                {
                    MdiParent = this,
                    Parent = Pnl_Main,
                    StartPosition = FormStartPosition.Manual,
                    Location = new Point(SubForm_StartX, SubForm_StartY),
                    Width = SubForm_Width,
                    Height = SubForm_Height
                };
                FrmChild.Show();
                FrmChild.Focus();

               // lblUserIP.Text = PublicComm.portal.UIHand.GetIpAddresses().ToString();
            }
            catch
            {
                Close();
            }
        }

        /// <summary>
        ///     ''' 當使用者關閉表單，並於表單關閉前指定關閉原因時發生
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Frm_0_0_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        ///     ''' 當使用者關閉表單，並於表單關閉後指定關閉原因時發生
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Frm_0_0_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonDef.MyLog.WriteLog("◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣系統關閉◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            PublicComm.ClientLog.Info($"◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣系統關閉◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣");
            PublicComm.ExceptionLog.Info($"◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣系統關閉◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣◢◣");

        }

        ///// <summary>
        /////     ''' 依"登入使用者"設定各畫面權限
        /////     ''' </summary>
        /////     ''' <remarks></remarks>
        //public void Initial()
        //{
        //}

        /// <summary>
        ///     ''' 登入後顯示畫面
        ///     ''' </summary>
        ///     ''' <remarks></remarks>
        public void ShowMenu()
        {
           // Initial();
            Frm_0_2_Menu FrmChild = new Frm_0_2_Menu();
            //PublicForms.ShowForm<frm_0_2_Menu>(FrmChild, panel1);
            foreach (Form f in Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
            {
                //如果子視窗已經存在
                if (f.Name == FrmChild.Name)
                {
                    //將該子視窗設為焦點
                    f.Focus();
                    f.Visible = true;
                    return;
                }
                else
                {
                    f.Visible = false;
                }
            }

            FrmChild.MdiParent = this;
            FrmChild.Parent = Pnl_Main;
            FrmChild.StartPosition = FormStartPosition.Manual;
            FrmChild.Location = new Point(SubForm_StartX, SubForm_StartY);
            FrmChild.Width = SubForm_Width;
            FrmChild.Height = SubForm_Height;
            FrmChild.Show();
            FrmChild.BringToFront();
            pnlMenu.Enabled = true;
        }

        /// <summary>
        /// 点击标题列Menu的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form FrmChild = null;
                ToolStripMenuItem tsmItem = sender as ToolStripMenuItem;
                switch (tsmItem.Name)
                {
                    case "tsMenuItem_0":
                        {
                            FrmChild = new Frm_0_2_Menu();
                            break;
                        }

                    case "tsMenuItem_1_1":
                        {
                            FrmChild = new Frm_1_1_PDISchl();
                            break;
                        }
                    case "tsMenuItem_1_2":
                        {
                            FrmChild = new Frm_1_2_PDIDetail();
                            break;
                        }
                    case "tsMenuItem_1_3":
                        {
                            FrmChild = new Frm_1_3_DeleteScheduleRecord();
                            break;
                        }
                    case "tsMenuItem_2_1":
                        {
                            FrmChild = new frm_2_1_Tracking();
                            break;
                        }
                    case "tsMenuItem_2_2":
                        {
                            FrmChild = new Frm_2_2_GPLSetupHistory();
                            break;
                        }
                    case "tsMenuItem_3_1":
                        {
                            FrmChild = new Frm_3_1_ProdCoilList();
                            break;
                        }

                    case "tsMenuItem_3_2":
                        {
                            FrmChild = new Frm_3_2_ProdDetail();
                            break;
                        }
                    case "tsMenuItem_3_3":
                        {
                            FrmChild = new Frm_3_3_GrindActualPerformance();
                            break;
                        }
                    case "tsMenuItem_4_1":
                        {
                            FrmChild = new Frm_4_1_LineDelayRecord();
                            break;
                        }

                    case "tsMenuItem_4_2":
                        {
                            FrmChild = new Frm_4_2_WeighingRecord();// frm_4_2_Utility();
                            break;
                        }
                    case "tsMenuItem_4_3":
                        {
                            FrmChild = new Frm_4_3_DeviceParameters();
                            break;
                        }
                    case "tsMenuItem_4_4":
                        {
                            FrmChild = new Frm_4_4_GrindPlan();
                            break;
                        }
                    case "tsMenuItem_4_5":
                        {
                            FrmChild = new Frm_4_5_Belts();
                            break;
                        }
                    case "tsMenuItem_5_1":
                        {
                            FrmChild = new Frm_5_1_EventLog();
                            break;
                        }
                    case "tsMenuItem_5_2":
                        {
                            FrmChild = new Frm_5_2_CodeMaintain();
                            break;
                        }
                    case "tsMenuItem_5_3":
                        {
                            FrmChild = new Frm_5_3_UserSetup();
                            break;
                        }
                    case "tsMenuItem_5_4":
                        {
                            FrmChild = new frm_5_4_CrewMaintenance();
                            break;
                        }
                    case "tsMenuItem_5_5":
                        {
                            FrmChild = new frm_5_5_Language();
                            break;
                        }
                    case "tsMenuItem_5_6":
                        {
                            FrmChild = new frm_5_6_NetworkStatus();
                            break;
                        }
                    case "tsMenuItem_5_7":
                        {
                            //FrmChild = new frm_5_7_DeleteReasonCode();
                            break;
                        }
                    default:
                        break;
                }
                // 關閉已開啟的子畫面
                foreach (Form fx in Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
                {

                    try
                    {
                        if (fx.Name != FrmChild.Name)
                        {
                            fx.Visible = false;
                        }
                        else
                        {
                            fx.Focus();
                            fx.Visible = true;
                            return;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                FrmChild.MdiParent = this;
                FrmChild.Parent = Pnl_Main;
                FrmChild.StartPosition = FormStartPosition.Manual;
                FrmChild.Location = new Point(SubForm_StartX, SubForm_StartY);
                FrmChild.Width = SubForm_Width;
                FrmChild.Height = SubForm_Height;
                FrmChild.Show();
                FrmChild.Focus();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     ''' 登出功能處理
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Btn_Logout_Click(object sender, EventArgs e)
        {
            try
            {
                List<Control> listCtl = new List<Control>();
                listCtl.AddRange(Pnl_Main.Controls.OfType<Form>());
                for (int i = 0; i < listCtl.Count; i++)
                {
                    // 關閉已開啟的子畫面
                    //foreach (Form fx in MdiChildren)
                    foreach (Form fx in Pnl_Main.Controls.OfType<Form>())
                    {
                        if (listCtl[i].Name == fx.Name)
                        {
                            try
                            {
                                fx.Close();
                                CommonDef.MyLog.WriteLog("關閉子畫面<" + fx.Name + ">完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Trace);
                                PublicComm.ClientLog.Info($"訊息名稱:關閉子畫面 訊息:關閉<{fx.Name}>子畫面");
                            }
                            catch (Exception ex)
                            {
                                CommonDef.MyLog.WriteLog("關閉子畫面<" + fx.Name + ">失敗", CommonDef.LogMode.Exception, CommonDef.LogLevel.Fatal, ex);
                                PublicComm.ClientLog.Debug($"訊息名稱:關閉子畫面 訊息:關閉<{fx.Name}>子畫面失敗:{ex}");
                            }
                        }
                    }
                }
                Frm_0_1_Login FrmChild = new Frm_0_1_Login();
               // pnlMenu.Enabled = false;
                Btn_Language.Enabled = false;
                Btn_Logout.Enabled = false;
                Btn_Print.Enabled = false;
                lblLoginUser.Text = "";
                CommonDef.LoginUser = "";
                FrmChild.MdiParent = this;
                FrmChild.StartPosition = FormStartPosition.Manual;
                FrmChild.Location = new Point(SubForm_StartX, SubForm_StartY);
                FrmChild.Width = SubForm_Width;
                FrmChild.Height = SubForm_Height;
                FrmChild.Show();
                PublicForms.ShowForm<Frm_0_1_Login>(FrmChild, Pnl_Main);
            }
            catch
            {
            }
        }

        /// <summary>
        ///     '''  縮小操作視窗
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Btn_MiniWindow_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        ///     ''' 關閉程式
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 画面语系切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Language_Click(object sender, EventArgs e)
        {
            LanguageHandler.Instance.DefaultLanguage = !LanguageHandler.Instance.DefaultLanguage;
            //Btn_Language.Text = LanguageHandler.Instance.DefaultLanguage ? "简" : "EN";
            //LanguageHandler.Instance.Current();

            LanguageHand.ChangeLang();
        }

        /// <summary>
        /// 画面截图功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Print_Click(object sender, EventArgs e)
        {
            string ss = AppDomain.CurrentDomain.BaseDirectory;
            if (Directory.Exists("PrintView"))
            {
                //資料夾存在
            }
            else
            {
                //資料夾不存在,新增資料夾
                Directory.CreateDirectory(@"PrintView/");
            }

            // Allow the user to select a file.
            OpenFileDialog ofd = new OpenFileDialog();
            byte[] PIC = GetScreen();
            path = "PrintView/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";

            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write(PIC, 0, PIC.Length); //寫入文件
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                PublicComm.ClientLog.Debug($"訊息名稱:畫面列印 訊息:畫面列印錯誤:{ex}");
            }

            if (File.Exists(path))
            {
                try
                {
                    FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.Read);
                }
                catch (Exception ex)
                {
                    PublicComm.ClientLog.Debug($"訊息名稱:畫面列印 訊息:畫面列印錯誤:{ex}");
                }
            }


            PrintDocument1.DefaultPageSettings.Landscape = true;

            PrintPreviewDialog1.Document = PrintDocument1;
            //PrintPreviewDialog1.AutoSizeMode = AutoSizeMode.
            if (PrintPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                PrintDocument1.Print();
            }
        }

        /// <summary>
        ///     ''' 取得目前 Screen資訊
        ///     ''' </summary>
        ///     ''' <returns>目前Screen資訊</returns>
        ///     ''' <remarks></remarks>
        private byte[] GetScreen()
        {
            int Height = Screen.PrimaryScreen.Bounds.Height;
            int Width = Screen.PrimaryScreen.Bounds.Width;
            Bitmap screenshot = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int FixWidth = 1100;

            Graphics graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(0, 0, 0, 0, new Size(Width, Height), CopyPixelOperation.SourceCopy);

            // //設定新圖檔的解晰度,寬度              
            int FixHeight = Convert.ToInt32((double.Parse(Height.ToString()) / double.Parse(Width.ToString())) * FixWidth);

            // //計算高度            
            Bitmap bmp = new Bitmap(screenshot, new Size(FixWidth, FixHeight));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            // //把圖檔用新的解晰度去儲存  
            screenshot.Dispose();
            graph.Dispose();
            bmp.Dispose();
            return ms.GetBuffer();
        }

        /// <summary>
        ///     ''' 每次列印時發生
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //把圖檔用新的解晰度去儲存  
            Graphics g;
            Image newImage = Image.FromFile(path);
            g = e.Graphics;
            g.DrawImage(newImage, 0, 0);
            g.Dispose();
        }          
       
        /// <summary>
        ///     ''' Timer For Time，顯示目前時間
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void TimerForClock_Tick_1(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            GlobalVariableHandler.Instance.getTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 事件讯息推播
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC03_EventPush(SCCommMsg.SC03_EventPush msg)
        {
            var eventName = msg.EventName.Equals(string.Empty) ?string.Empty : $"信息名称:{msg.EventName}";
            var eventMsg = msg.EventMsg.Equals(string.Empty) ? string.Empty : $"信息:{msg.EventMsg}";
            EventLogHandler.Instance.EventPush_Message(eventName+" "+eventMsg);
        }        

        /// <summary>
        /// 调整画面文字大小(中英切换)22←→14
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_LanguageIsEn_Font22_14(object sender, EventArgs e)
        {
            FontStyle fs = ((Button)sender).Font.Style;
            FontFamily ffm = ((Button)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Button)sender).Font = new Font(ffm, (float)22, fs);
            else
                ((Button)sender).Font = new Font(ffm, (float)14.25, fs);
        }
    }
}
