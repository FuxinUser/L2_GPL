namespace GPLManager
{
    partial class Frm_4_2_WeighingRecord
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
            this.Pnl_Show = new System.Windows.Forms.Panel();
            this.Chk_Coil = new System.Windows.Forms.CheckBox();
            this.Cob_Entry_Coil_No = new System.Windows.Forms.ComboBox();
            this.Lbl_Time_Range = new System.Windows.Forms.Label();
            this.Chk_Time = new System.Windows.Forms.CheckBox();
            this.Dtp_Start_Time = new System.Windows.Forms.DateTimePicker();
            this.Dtp_Finish_Time = new System.Windows.Forms.DateTimePicker();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Dgv_DeleteSchedule = new System.Windows.Forms.DataGridView();
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            this.Pnl_Show.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DeleteSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Show
            // 
            this.Pnl_Show.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Show.Controls.Add(this.Chk_Coil);
            this.Pnl_Show.Controls.Add(this.Cob_Entry_Coil_No);
            this.Pnl_Show.Controls.Add(this.Lbl_Time_Range);
            this.Pnl_Show.Controls.Add(this.Chk_Time);
            this.Pnl_Show.Controls.Add(this.Dtp_Start_Time);
            this.Pnl_Show.Controls.Add(this.Dtp_Finish_Time);
            this.Pnl_Show.Controls.Add(this.Btn_Search);
            this.Pnl_Show.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Show.Name = "Pnl_Show";
            this.Pnl_Show.Size = new System.Drawing.Size(1910, 100);
            this.Pnl_Show.TabIndex = 350;
            // 
            // Chk_Coil
            // 
            this.Chk_Coil.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Coil.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Coil.Location = new System.Drawing.Point(1380, 36);
            this.Chk_Coil.Name = "Chk_Coil";
            this.Chk_Coil.Size = new System.Drawing.Size(160, 33);
            this.Chk_Coil.TabIndex = 1778;
            this.Chk_Coil.Text = "入口钢卷号";
            this.Chk_Coil.UseVisualStyleBackColor = false;
            this.Chk_Coil.Visible = false;
            // 
            // Cob_Entry_Coil_No
            // 
            this.Cob_Entry_Coil_No.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_Entry_Coil_No.FormattingEnabled = true;
            this.Cob_Entry_Coil_No.Location = new System.Drawing.Point(1540, 36);
            this.Cob_Entry_Coil_No.Name = "Cob_Entry_Coil_No";
            this.Cob_Entry_Coil_No.Size = new System.Drawing.Size(200, 32);
            this.Cob_Entry_Coil_No.TabIndex = 1772;
            this.Cob_Entry_Coil_No.Visible = false;
            // 
            // Lbl_Time_Range
            // 
            this.Lbl_Time_Range.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Time_Range.Location = new System.Drawing.Point(380, 36);
            this.Lbl_Time_Range.Name = "Lbl_Time_Range";
            this.Lbl_Time_Range.Size = new System.Drawing.Size(24, 24);
            this.Lbl_Time_Range.TabIndex = 1769;
            this.Lbl_Time_Range.Text = "~";
            // 
            // Chk_Time
            // 
            this.Chk_Time.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Time.Location = new System.Drawing.Point(20, 32);
            this.Chk_Time.Name = "Chk_Time";
            this.Chk_Time.Size = new System.Drawing.Size(160, 33);
            this.Chk_Time.TabIndex = 1766;
            this.Chk_Time.Text = "日期区间";
            this.Chk_Time.UseVisualStyleBackColor = false;
            // 
            // Dtp_Start_Time
            // 
            this.Dtp_Start_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Start_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Start_Time.Location = new System.Drawing.Point(180, 32);
            this.Dtp_Start_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Start_Time.Name = "Dtp_Start_Time";
            this.Dtp_Start_Time.Size = new System.Drawing.Size(200, 33);
            this.Dtp_Start_Time.TabIndex = 1764;
            // 
            // Dtp_Finish_Time
            // 
            this.Dtp_Finish_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Finish_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Finish_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Finish_Time.Location = new System.Drawing.Point(404, 32);
            this.Dtp_Finish_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Finish_Time.Name = "Dtp_Finish_Time";
            this.Dtp_Finish_Time.Size = new System.Drawing.Size(200, 33);
            this.Dtp_Finish_Time.TabIndex = 1765;
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Search.Location = new System.Drawing.Point(624, 18);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search.TabIndex = 291;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Dgv_DeleteSchedule
            // 
            this.Dgv_DeleteSchedule.AllowUserToAddRows = false;
            this.Dgv_DeleteSchedule.AllowUserToDeleteRows = false;
            this.Dgv_DeleteSchedule.AllowUserToResizeColumns = false;
            this.Dgv_DeleteSchedule.AllowUserToResizeRows = false;
            this.Dgv_DeleteSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_DeleteSchedule.Location = new System.Drawing.Point(5, 156);
            this.Dgv_DeleteSchedule.Name = "Dgv_DeleteSchedule";
            this.Dgv_DeleteSchedule.ReadOnly = true;
            this.Dgv_DeleteSchedule.RowTemplate.Height = 24;
            this.Dgv_DeleteSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_DeleteSchedule.Size = new System.Drawing.Size(1910, 821);
            this.Dgv_DeleteSchedule.TabIndex = 351;
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.BackColor = System.Drawing.Color.Gray;
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_MainTitle.TabIndex = 1767;
            this.Lbl_MainTitle.Text = "4-2 校磅记录";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_4_2_WeighingRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Dgv_DeleteSchedule);
            this.Controls.Add(this.Lbl_MainTitle);
            this.Controls.Add(this.Pnl_Show);
            this.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_4_2_WeighingRecord";
            this.Text = "frm_1_3_DeleteCoilRecord";
            this.Load += new System.EventHandler(this.Frm_4_2_WeighingRecord_Load);
            this.Shown += new System.EventHandler(this.Frm_4_2_WeighingRecord_Shown);
            this.Pnl_Show.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DeleteSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel Pnl_Show;
        internal System.Windows.Forms.Button Btn_Search;
        internal System.Windows.Forms.Label Lbl_Time_Range;
        internal System.Windows.Forms.DataGridView Dgv_DeleteSchedule;
        internal System.Windows.Forms.CheckBox Chk_Time;
        internal System.Windows.Forms.CheckBox Chk_Coil;
        internal System.Windows.Forms.ComboBox Cob_Entry_Coil_No;
        internal System.Windows.Forms.DateTimePicker Dtp_Start_Time;
        internal System.Windows.Forms.DateTimePicker Dtp_Finish_Time;
        internal System.Windows.Forms.Label Lbl_MainTitle;
    }
}