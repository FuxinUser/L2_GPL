namespace GPLManager
{
    partial class Frm_5_1_EventLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Chk_Keyword = new System.Windows.Forms.CheckBox();
            this.Cob_EventType = new System.Windows.Forms.ComboBox();
            this.Chk_EventType = new System.Windows.Forms.CheckBox();
            this.Lbl_DateTime_Ranage = new System.Windows.Forms.Label();
            this.Pnl_Show = new System.Windows.Forms.Panel();
            this.Txt_SelectTop = new System.Windows.Forms.TextBox();
            this.Lbl_SelectTop_Title = new System.Windows.Forms.Label();
            this.Dtp_Start_Time = new System.Windows.Forms.DateTimePicker();
            this.Lbl_DateTime_Title = new System.Windows.Forms.Label();
            this.Txt_Keyword = new System.Windows.Forms.TextBox();
            this.Dtp_Finish_Time = new System.Windows.Forms.DateTimePicker();
            this.Cob_ComputerName = new System.Windows.Forms.ComboBox();
            this.Chk_ComputerName = new System.Windows.Forms.CheckBox();
            this.Cob_System_ID = new System.Windows.Forms.ComboBox();
            this.Chk_System = new System.Windows.Forms.CheckBox();
            this.Btn_Query = new System.Windows.Forms.Button();
            this.Dgv_EventLog = new System.Windows.Forms.DataGridView();
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            this.Pnl_Show.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // Chk_Keyword
            // 
            this.Chk_Keyword.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Keyword.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Keyword.ForeColor = System.Drawing.Color.Black;
            this.Chk_Keyword.Location = new System.Drawing.Point(20, 53);
            this.Chk_Keyword.Name = "Chk_Keyword";
            this.Chk_Keyword.Size = new System.Drawing.Size(162, 32);
            this.Chk_Keyword.TabIndex = 9;
            this.Chk_Keyword.Text = "内容关键字";
            this.Chk_Keyword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Chk_Keyword.UseVisualStyleBackColor = false;
            // 
            // Cob_EventType
            // 
            this.Cob_EventType.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_EventType.ForeColor = System.Drawing.Color.Black;
            this.Cob_EventType.Items.AddRange(new object[] {
            "警告",
            "事件",
            "錯誤",
            "未列管",
            "全部"});
            this.Cob_EventType.Location = new System.Drawing.Point(767, 10);
            this.Cob_EventType.Name = "Cob_EventType";
            this.Cob_EventType.Size = new System.Drawing.Size(249, 32);
            this.Cob_EventType.TabIndex = 12;
            // 
            // Chk_EventType
            // 
            this.Chk_EventType.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_EventType.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_EventType.ForeColor = System.Drawing.Color.Black;
            this.Chk_EventType.Location = new System.Drawing.Point(605, 10);
            this.Chk_EventType.Name = "Chk_EventType";
            this.Chk_EventType.Size = new System.Drawing.Size(162, 33);
            this.Chk_EventType.TabIndex = 14;
            this.Chk_EventType.Text = "类别";
            this.Chk_EventType.UseVisualStyleBackColor = false;
            // 
            // Lbl_DateTime_Ranage
            // 
            this.Lbl_DateTime_Ranage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DateTime_Ranage.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DateTime_Ranage.Location = new System.Drawing.Point(372, 11);
            this.Lbl_DateTime_Ranage.Name = "Lbl_DateTime_Ranage";
            this.Lbl_DateTime_Ranage.Size = new System.Drawing.Size(22, 29);
            this.Lbl_DateTime_Ranage.TabIndex = 16;
            this.Lbl_DateTime_Ranage.Text = "~";
            // 
            // Pnl_Show
            // 
            this.Pnl_Show.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Show.Controls.Add(this.Txt_SelectTop);
            this.Pnl_Show.Controls.Add(this.Lbl_SelectTop_Title);
            this.Pnl_Show.Controls.Add(this.Dtp_Start_Time);
            this.Pnl_Show.Controls.Add(this.Lbl_DateTime_Title);
            this.Pnl_Show.Controls.Add(this.Txt_Keyword);
            this.Pnl_Show.Controls.Add(this.Dtp_Finish_Time);
            this.Pnl_Show.Controls.Add(this.Cob_ComputerName);
            this.Pnl_Show.Controls.Add(this.Chk_ComputerName);
            this.Pnl_Show.Controls.Add(this.Cob_System_ID);
            this.Pnl_Show.Controls.Add(this.Chk_System);
            this.Pnl_Show.Controls.Add(this.Btn_Query);
            this.Pnl_Show.Controls.Add(this.Chk_Keyword);
            this.Pnl_Show.Controls.Add(this.Cob_EventType);
            this.Pnl_Show.Controls.Add(this.Chk_EventType);
            this.Pnl_Show.Controls.Add(this.Lbl_DateTime_Ranage);
            this.Pnl_Show.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Show.Name = "Pnl_Show";
            this.Pnl_Show.Size = new System.Drawing.Size(1910, 100);
            this.Pnl_Show.TabIndex = 301;
            // 
            // Txt_SelectTop
            // 
            this.Txt_SelectTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_SelectTop.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_SelectTop.Location = new System.Drawing.Point(1198, 53);
            this.Txt_SelectTop.MaxLength = 15;
            this.Txt_SelectTop.Name = "Txt_SelectTop";
            this.Txt_SelectTop.Size = new System.Drawing.Size(194, 33);
            this.Txt_SelectTop.TabIndex = 1131;
            this.Txt_SelectTop.Text = "50";
            // 
            // Lbl_SelectTop_Title
            // 
            this.Lbl_SelectTop_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_SelectTop_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_SelectTop_Title.Location = new System.Drawing.Point(1036, 53);
            this.Lbl_SelectTop_Title.Name = "Lbl_SelectTop_Title";
            this.Lbl_SelectTop_Title.Size = new System.Drawing.Size(162, 33);
            this.Lbl_SelectTop_Title.TabIndex = 1130;
            this.Lbl_SelectTop_Title.Text = "查询资料笔数";
            this.Lbl_SelectTop_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Dtp_Start_Time
            // 
            this.Dtp_Start_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Start_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Start_Time.Location = new System.Drawing.Point(181, 10);
            this.Dtp_Start_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Start_Time.Name = "Dtp_Start_Time";
            this.Dtp_Start_Time.Size = new System.Drawing.Size(190, 33);
            this.Dtp_Start_Time.TabIndex = 1124;
            // 
            // Lbl_DateTime_Title
            // 
            this.Lbl_DateTime_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_DateTime_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_DateTime_Title.Location = new System.Drawing.Point(20, 10);
            this.Lbl_DateTime_Title.Name = "Lbl_DateTime_Title";
            this.Lbl_DateTime_Title.Size = new System.Drawing.Size(162, 32);
            this.Lbl_DateTime_Title.TabIndex = 1129;
            this.Lbl_DateTime_Title.Text = "事件日期时间";
            this.Lbl_DateTime_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_Keyword
            // 
            this.Txt_Keyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Keyword.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_Keyword.Location = new System.Drawing.Point(181, 52);
            this.Txt_Keyword.Name = "Txt_Keyword";
            this.Txt_Keyword.Size = new System.Drawing.Size(404, 33);
            this.Txt_Keyword.TabIndex = 1128;
            // 
            // Dtp_Finish_Time
            // 
            this.Dtp_Finish_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Finish_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Finish_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Finish_Time.Location = new System.Drawing.Point(396, 10);
            this.Dtp_Finish_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Finish_Time.Name = "Dtp_Finish_Time";
            this.Dtp_Finish_Time.Size = new System.Drawing.Size(190, 33);
            this.Dtp_Finish_Time.TabIndex = 1126;
            // 
            // Cob_ComputerName
            // 
            this.Cob_ComputerName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_ComputerName.ForeColor = System.Drawing.Color.Black;
            this.Cob_ComputerName.Items.AddRange(new object[] {
            "警告",
            "事件",
            "錯誤",
            "未列管",
            "全部"});
            this.Cob_ComputerName.Location = new System.Drawing.Point(767, 53);
            this.Cob_ComputerName.Name = "Cob_ComputerName";
            this.Cob_ComputerName.Size = new System.Drawing.Size(249, 32);
            this.Cob_ComputerName.TabIndex = 1123;
            // 
            // Chk_ComputerName
            // 
            this.Chk_ComputerName.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_ComputerName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_ComputerName.ForeColor = System.Drawing.Color.Black;
            this.Chk_ComputerName.Location = new System.Drawing.Point(605, 53);
            this.Chk_ComputerName.Name = "Chk_ComputerName";
            this.Chk_ComputerName.Size = new System.Drawing.Size(162, 33);
            this.Chk_ComputerName.TabIndex = 1122;
            this.Chk_ComputerName.Text = "电脑名称";
            this.Chk_ComputerName.UseVisualStyleBackColor = false;
            this.Chk_ComputerName.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font14_12);
            // 
            // Cob_System_ID
            // 
            this.Cob_System_ID.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_System_ID.ForeColor = System.Drawing.Color.Black;
            this.Cob_System_ID.Items.AddRange(new object[] {
            "警告",
            "事件",
            "錯誤",
            "未列管",
            "全部"});
            this.Cob_System_ID.Location = new System.Drawing.Point(1198, 10);
            this.Cob_System_ID.Name = "Cob_System_ID";
            this.Cob_System_ID.Size = new System.Drawing.Size(194, 32);
            this.Cob_System_ID.TabIndex = 1121;
            // 
            // Chk_System
            // 
            this.Chk_System.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_System.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_System.ForeColor = System.Drawing.Color.Black;
            this.Chk_System.Location = new System.Drawing.Point(1036, 10);
            this.Chk_System.Name = "Chk_System";
            this.Chk_System.Size = new System.Drawing.Size(162, 32);
            this.Chk_System.TabIndex = 1120;
            this.Chk_System.Text = "系统";
            this.Chk_System.UseVisualStyleBackColor = false;
            // 
            // Btn_Query
            // 
            this.Btn_Query.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Query.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Query.ForeColor = System.Drawing.Color.Black;
            this.Btn_Query.Location = new System.Drawing.Point(1412, 19);
            this.Btn_Query.Name = "Btn_Query";
            this.Btn_Query.Size = new System.Drawing.Size(150, 60);
            this.Btn_Query.TabIndex = 320;
            this.Btn_Query.Text = "查询";
            this.Btn_Query.UseVisualStyleBackColor = false;
            this.Btn_Query.Click += new System.EventHandler(this.Btn_QueryOK_Click);
            // 
            // Dgv_EventLog
            // 
            this.Dgv_EventLog.AllowUserToAddRows = false;
            this.Dgv_EventLog.AllowUserToDeleteRows = false;
            this.Dgv_EventLog.AllowUserToResizeColumns = false;
            this.Dgv_EventLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_EventLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_EventLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_EventLog.Location = new System.Drawing.Point(5, 155);
            this.Dgv_EventLog.Name = "Dgv_EventLog";
            this.Dgv_EventLog.ReadOnly = true;
            this.Dgv_EventLog.RowHeadersVisible = false;
            this.Dgv_EventLog.RowHeadersWidth = 51;
            this.Dgv_EventLog.RowTemplate.Height = 24;
            this.Dgv_EventLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_EventLog.Size = new System.Drawing.Size(1910, 822);
            this.Dgv_EventLog.TabIndex = 300;
            this.Dgv_EventLog.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_EventLog_CellMouseEnter);
            this.Dgv_EventLog.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_EventLog_RowPostPaint);
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.BackColor = System.Drawing.Color.Gray;
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_MainTitle.TabIndex = 1781;
            this.Lbl_MainTitle.Text = "5-1 事件记录";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_5_1_EventLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Lbl_MainTitle);
            this.Controls.Add(this.Pnl_Show);
            this.Controls.Add(this.Dgv_EventLog);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_5_1_EventLog";
            this.Text = "frm_6_1_EventLog";
            this.Shown += new System.EventHandler(this.Frm_5_1_EventLog_Shown);
            this.Pnl_Show.ResumeLayout(false);
            this.Pnl_Show.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EventLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.CheckBox Chk_Keyword;
        internal System.Windows.Forms.ComboBox Cob_EventType;
        internal System.Windows.Forms.CheckBox Chk_EventType;
        internal System.Windows.Forms.Label Lbl_DateTime_Ranage;
        internal System.Windows.Forms.Panel Pnl_Show;
        internal System.Windows.Forms.Button Btn_Query;
        internal System.Windows.Forms.DataGridView Dgv_EventLog;
        internal System.Windows.Forms.ComboBox Cob_ComputerName;
        internal System.Windows.Forms.CheckBox Chk_ComputerName;
        internal System.Windows.Forms.ComboBox Cob_System_ID;
        internal System.Windows.Forms.CheckBox Chk_System;
        internal System.Windows.Forms.TextBox Txt_Keyword;
        internal System.Windows.Forms.DateTimePicker Dtp_Start_Time;
        internal System.Windows.Forms.DateTimePicker Dtp_Finish_Time;
        internal System.Windows.Forms.Label Lbl_MainTitle;
        private System.Windows.Forms.Label Lbl_DateTime_Title;
        internal System.Windows.Forms.TextBox Txt_SelectTop;
        private System.Windows.Forms.Label Lbl_SelectTop_Title;
    }
}