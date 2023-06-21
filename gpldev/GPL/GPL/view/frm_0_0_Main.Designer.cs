namespace GPLManager
{
    partial class Frm_0_0_Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_0_0_Main));
            this.Pnl_Main = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.Pic_Main_Picture = new System.Windows.Forms.PictureBox();
            this.msTitleArea = new System.Windows.Forms.MenuStrip();
            this.tsMenuItem_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_1_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_1_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_1_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_2_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_2_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_3_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_3_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_3_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_4_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_4_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_4_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_4_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_4_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem_5_7 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblLoginUser_header = new System.Windows.Forms.Label();
            this.lblLoginUser = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.Btn_MinWindow = new System.Windows.Forms.Button();
            this.Btn_Logout = new System.Windows.Forms.Button();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.lblUserIP = new System.Windows.Forms.Label();
            this.Btn_Print = new System.Windows.Forms.Button();
            this.cboEvnMsg = new System.Windows.Forms.ComboBox();
            this.lblEvnMsg = new System.Windows.Forms.Label();
            this.Btn_Language = new System.Windows.Forms.Button();
            this.TimerLogCleaner = new System.Windows.Forms.Timer(this.components);
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.PrintPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.TimerForClock = new System.Windows.Forms.Timer(this.components);
            this.Pnl_Main.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Main_Picture)).BeginInit();
            this.msTitleArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Main
            // 
            this.Pnl_Main.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Pnl_Main.Controls.Add(this.pnlMenu);
            this.Pnl_Main.Controls.Add(this.lblUserIP);
            this.Pnl_Main.Controls.Add(this.Btn_Print);
            this.Pnl_Main.Controls.Add(this.cboEvnMsg);
            this.Pnl_Main.Controls.Add(this.lblEvnMsg);
            this.Pnl_Main.Controls.Add(this.Btn_Language);
            resources.ApplyResources(this.Pnl_Main, "Pnl_Main");
            this.Pnl_Main.Name = "Pnl_Main";
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.Gray;
            this.pnlMenu.Controls.Add(this.Pic_Main_Picture);
            this.pnlMenu.Controls.Add(this.msTitleArea);
            this.pnlMenu.Controls.Add(this.lblLoginUser_header);
            this.pnlMenu.Controls.Add(this.lblLoginUser);
            this.pnlMenu.Controls.Add(this.lblClock);
            this.pnlMenu.Controls.Add(this.Btn_MinWindow);
            this.pnlMenu.Controls.Add(this.Btn_Logout);
            this.pnlMenu.Controls.Add(this.Btn_Exit);
            resources.ApplyResources(this.pnlMenu, "pnlMenu");
            this.pnlMenu.Name = "pnlMenu";
            // 
            // Pic_Main_Picture
            // 
            resources.ApplyResources(this.Pic_Main_Picture, "Pic_Main_Picture");
            this.Pic_Main_Picture.BackColor = System.Drawing.Color.White;
            this.Pic_Main_Picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pic_Main_Picture.Image = global::GPLManager.Properties.Resources.ICON_GPL_Main;
            this.Pic_Main_Picture.Name = "Pic_Main_Picture";
            this.Pic_Main_Picture.TabStop = false;
            // 
            // msTitleArea
            // 
            resources.ApplyResources(this.msTitleArea, "msTitleArea");
            this.msTitleArea.GripMargin = new System.Windows.Forms.Padding(3);
            this.msTitleArea.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msTitleArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem_0,
            this.tsMenuItem_1,
            this.tsMenuItem_2,
            this.tsMenuItem_3,
            this.tsMenuItem_4,
            this.tsMenuItem_5});
            this.msTitleArea.Name = "msTitleArea";
            // 
            // tsMenuItem_0
            // 
            resources.ApplyResources(this.tsMenuItem_0, "tsMenuItem_0");
            this.tsMenuItem_0.Name = "tsMenuItem_0";
            this.tsMenuItem_0.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_1
            // 
            this.tsMenuItem_1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem_1_1,
            this.tsMenuItem_1_2,
            this.tsMenuItem_1_3});
            resources.ApplyResources(this.tsMenuItem_1, "tsMenuItem_1");
            this.tsMenuItem_1.Name = "tsMenuItem_1";
            // 
            // tsMenuItem_1_1
            // 
            this.tsMenuItem_1_1.Name = "tsMenuItem_1_1";
            resources.ApplyResources(this.tsMenuItem_1_1, "tsMenuItem_1_1");
            this.tsMenuItem_1_1.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_1_2
            // 
            this.tsMenuItem_1_2.Name = "tsMenuItem_1_2";
            resources.ApplyResources(this.tsMenuItem_1_2, "tsMenuItem_1_2");
            this.tsMenuItem_1_2.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_1_3
            // 
            this.tsMenuItem_1_3.Name = "tsMenuItem_1_3";
            resources.ApplyResources(this.tsMenuItem_1_3, "tsMenuItem_1_3");
            this.tsMenuItem_1_3.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_2
            // 
            this.tsMenuItem_2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem_2_1,
            this.tsMenuItem_2_2});
            resources.ApplyResources(this.tsMenuItem_2, "tsMenuItem_2");
            this.tsMenuItem_2.Name = "tsMenuItem_2";
            // 
            // tsMenuItem_2_1
            // 
            this.tsMenuItem_2_1.Name = "tsMenuItem_2_1";
            resources.ApplyResources(this.tsMenuItem_2_1, "tsMenuItem_2_1");
            this.tsMenuItem_2_1.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_2_2
            // 
            this.tsMenuItem_2_2.Name = "tsMenuItem_2_2";
            resources.ApplyResources(this.tsMenuItem_2_2, "tsMenuItem_2_2");
            this.tsMenuItem_2_2.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_3
            // 
            this.tsMenuItem_3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem_3_1,
            this.tsMenuItem_3_2,
            this.tsMenuItem_3_3});
            resources.ApplyResources(this.tsMenuItem_3, "tsMenuItem_3");
            this.tsMenuItem_3.Name = "tsMenuItem_3";
            // 
            // tsMenuItem_3_1
            // 
            this.tsMenuItem_3_1.Name = "tsMenuItem_3_1";
            resources.ApplyResources(this.tsMenuItem_3_1, "tsMenuItem_3_1");
            this.tsMenuItem_3_1.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_3_2
            // 
            this.tsMenuItem_3_2.Name = "tsMenuItem_3_2";
            resources.ApplyResources(this.tsMenuItem_3_2, "tsMenuItem_3_2");
            this.tsMenuItem_3_2.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_3_3
            // 
            this.tsMenuItem_3_3.Name = "tsMenuItem_3_3";
            resources.ApplyResources(this.tsMenuItem_3_3, "tsMenuItem_3_3");
            this.tsMenuItem_3_3.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_4
            // 
            this.tsMenuItem_4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem_4_1,
            this.tsMenuItem_4_2,
            this.tsMenuItem_4_3,
            this.tsMenuItem_4_4,
            this.tsMenuItem_4_5});
            resources.ApplyResources(this.tsMenuItem_4, "tsMenuItem_4");
            this.tsMenuItem_4.Name = "tsMenuItem_4";
            // 
            // tsMenuItem_4_1
            // 
            this.tsMenuItem_4_1.Name = "tsMenuItem_4_1";
            resources.ApplyResources(this.tsMenuItem_4_1, "tsMenuItem_4_1");
            this.tsMenuItem_4_1.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_4_2
            // 
            this.tsMenuItem_4_2.Name = "tsMenuItem_4_2";
            resources.ApplyResources(this.tsMenuItem_4_2, "tsMenuItem_4_2");
            this.tsMenuItem_4_2.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_4_3
            // 
            this.tsMenuItem_4_3.Name = "tsMenuItem_4_3";
            resources.ApplyResources(this.tsMenuItem_4_3, "tsMenuItem_4_3");
            this.tsMenuItem_4_3.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_4_4
            // 
            this.tsMenuItem_4_4.Name = "tsMenuItem_4_4";
            resources.ApplyResources(this.tsMenuItem_4_4, "tsMenuItem_4_4");
            this.tsMenuItem_4_4.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_4_5
            // 
            this.tsMenuItem_4_5.Name = "tsMenuItem_4_5";
            resources.ApplyResources(this.tsMenuItem_4_5, "tsMenuItem_4_5");
            this.tsMenuItem_4_5.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5
            // 
            this.tsMenuItem_5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem_5_1,
            this.tsMenuItem_5_2,
            this.tsMenuItem_5_3,
            this.tsMenuItem_5_4,
            this.tsMenuItem_5_5,
            this.tsMenuItem_5_6,
            this.tsMenuItem_5_7});
            resources.ApplyResources(this.tsMenuItem_5, "tsMenuItem_5");
            this.tsMenuItem_5.Name = "tsMenuItem_5";
            // 
            // tsMenuItem_5_1
            // 
            this.tsMenuItem_5_1.Name = "tsMenuItem_5_1";
            resources.ApplyResources(this.tsMenuItem_5_1, "tsMenuItem_5_1");
            this.tsMenuItem_5_1.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5_2
            // 
            this.tsMenuItem_5_2.Name = "tsMenuItem_5_2";
            resources.ApplyResources(this.tsMenuItem_5_2, "tsMenuItem_5_2");
            this.tsMenuItem_5_2.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5_3
            // 
            this.tsMenuItem_5_3.Name = "tsMenuItem_5_3";
            resources.ApplyResources(this.tsMenuItem_5_3, "tsMenuItem_5_3");
            this.tsMenuItem_5_3.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5_4
            // 
            this.tsMenuItem_5_4.Name = "tsMenuItem_5_4";
            resources.ApplyResources(this.tsMenuItem_5_4, "tsMenuItem_5_4");
            this.tsMenuItem_5_4.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5_5
            // 
            this.tsMenuItem_5_5.Name = "tsMenuItem_5_5";
            resources.ApplyResources(this.tsMenuItem_5_5, "tsMenuItem_5_5");
            this.tsMenuItem_5_5.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5_6
            // 
            this.tsMenuItem_5_6.Name = "tsMenuItem_5_6";
            resources.ApplyResources(this.tsMenuItem_5_6, "tsMenuItem_5_6");
            this.tsMenuItem_5_6.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsMenuItem_5_7
            // 
            this.tsMenuItem_5_7.Name = "tsMenuItem_5_7";
            resources.ApplyResources(this.tsMenuItem_5_7, "tsMenuItem_5_7");
            this.tsMenuItem_5_7.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // lblLoginUser_header
            // 
            this.lblLoginUser_header.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblLoginUser_header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblLoginUser_header, "lblLoginUser_header");
            this.lblLoginUser_header.ForeColor = System.Drawing.Color.White;
            this.lblLoginUser_header.Name = "lblLoginUser_header";
            // 
            // lblLoginUser
            // 
            this.lblLoginUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblLoginUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblLoginUser, "lblLoginUser");
            this.lblLoginUser.ForeColor = System.Drawing.Color.Blue;
            this.lblLoginUser.Name = "lblLoginUser";
            // 
            // lblClock
            // 
            this.lblClock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblClock, "lblClock");
            this.lblClock.ForeColor = System.Drawing.Color.Blue;
            this.lblClock.Name = "lblClock";
            // 
            // Btn_MinWindow
            // 
            this.Btn_MinWindow.BackColor = System.Drawing.Color.DeepSkyBlue;
            resources.ApplyResources(this.Btn_MinWindow, "Btn_MinWindow");
            this.Btn_MinWindow.ForeColor = System.Drawing.Color.Black;
            this.Btn_MinWindow.Name = "Btn_MinWindow";
            this.Btn_MinWindow.UseVisualStyleBackColor = false;
            this.Btn_MinWindow.Click += new System.EventHandler(this.Btn_MiniWindow_Click);
            // 
            // Btn_Logout
            // 
            this.Btn_Logout.BackColor = System.Drawing.Color.LimeGreen;
            resources.ApplyResources(this.Btn_Logout, "Btn_Logout");
            this.Btn_Logout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Logout.Name = "Btn_Logout";
            this.Btn_Logout.UseVisualStyleBackColor = false;
            this.Btn_Logout.Click += new System.EventHandler(this.Btn_Logout_Click);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.Btn_Exit, "Btn_Exit");
            this.Btn_Exit.ForeColor = System.Drawing.Color.White;
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.UseVisualStyleBackColor = false;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // lblUserIP
            // 
            this.lblUserIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblUserIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblUserIP, "lblUserIP");
            this.lblUserIP.ForeColor = System.Drawing.Color.Blue;
            this.lblUserIP.Name = "lblUserIP";
            // 
            // Btn_Print
            // 
            this.Btn_Print.BackColor = System.Drawing.Color.Orange;
            this.Btn_Print.BackgroundImage = global::GPLManager.Properties.Resources.Print;
            resources.ApplyResources(this.Btn_Print, "Btn_Print");
            this.Btn_Print.Name = "Btn_Print";
            this.Btn_Print.UseVisualStyleBackColor = false;
            this.Btn_Print.Click += new System.EventHandler(this.Btn_Print_Click);
            // 
            // cboEvnMsg
            // 
            this.cboEvnMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.cboEvnMsg, "cboEvnMsg");
            this.cboEvnMsg.FormattingEnabled = true;
            this.cboEvnMsg.Name = "cboEvnMsg";
            // 
            // lblEvnMsg
            // 
            this.lblEvnMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblEvnMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblEvnMsg, "lblEvnMsg");
            this.lblEvnMsg.Name = "lblEvnMsg";
            // 
            // Btn_Language
            // 
            this.Btn_Language.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(156)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.Btn_Language, "Btn_Language");
            this.Btn_Language.Name = "Btn_Language";
            this.Btn_Language.UseVisualStyleBackColor = false;
            this.Btn_Language.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font22_14);
            this.Btn_Language.Click += new System.EventHandler(this.Btn_Language_Click);
            // 
            // TimerLogCleaner
            // 
            this.TimerLogCleaner.Enabled = true;
            this.TimerLogCleaner.Interval = 3600000;
            // 
            // PrintDocument1
            // 
            this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // PrintPreviewDialog1
            // 
            resources.ApplyResources(this.PrintPreviewDialog1, "PrintPreviewDialog1");
            this.PrintPreviewDialog1.Name = "PrintPreviewDialog1";
            // 
            // TimerForClock
            // 
            this.TimerForClock.Enabled = true;
            this.TimerForClock.Interval = 500;
            this.TimerForClock.Tick += new System.EventHandler(this.TimerForClock_Tick_1);
            // 
            // Frm_0_0_Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.Pnl_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "Frm_0_0_Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Frm_0_0_Main_Shown);
            this.Pnl_Main.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Main_Picture)).EndInit();
            this.msTitleArea.ResumeLayout(false);
            this.msTitleArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel Pnl_Main;
        internal System.Windows.Forms.Panel pnlMenu;
        internal System.Windows.Forms.MenuStrip msTitleArea;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_0;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_1;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_1_1;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_1_2;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_2;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_2_1;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_2_2;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_3;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_3_1;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_3_2;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_4;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_4_4;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_4_5;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_4_1;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_1;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_2;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_3;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_4;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_5;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_6;
        internal System.Windows.Forms.Label lblLoginUser_header;
        internal System.Windows.Forms.Label lblLoginUser;
        internal System.Windows.Forms.Label lblClock;
        internal System.Windows.Forms.Button Btn_MinWindow;
        internal System.Windows.Forms.Button Btn_Logout;
        internal System.Windows.Forms.Button Btn_Exit;
        internal System.Windows.Forms.Label lblUserIP;
        internal System.Windows.Forms.Button Btn_Print;
        internal System.Windows.Forms.ComboBox cboEvnMsg;
        internal System.Windows.Forms.Label lblEvnMsg;
        internal System.Windows.Forms.Button Btn_Language;
        
        internal System.Windows.Forms.Timer TimerLogCleaner;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        internal System.Windows.Forms.PrintPreviewDialog PrintPreviewDialog1;
        internal System.Windows.Forms.Timer TimerForClock;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_5_7;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_4_3;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_3_3;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_1_3;
        internal System.Windows.Forms.ToolStripMenuItem tsMenuItem_4_2;
        private System.Windows.Forms.PictureBox Pic_Main_Picture;
    }
}

