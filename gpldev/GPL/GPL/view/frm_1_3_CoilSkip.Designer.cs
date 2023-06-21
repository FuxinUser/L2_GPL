namespace GPLManager
{
    partial class frm_1_3_CoilSkip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_1_3_CoilSkip));
            this.btnClosePanel = new System.Windows.Forms.Button();
            this.cboCoilID = new System.Windows.Forms.ComboBox();
            this.cboSkipReasonCode = new System.Windows.Forms.ComboBox();
            this.lblSkipReasonCode = new System.Windows.Forms.Label();
            this.lblSkipComment = new System.Windows.Forms.Label();
            this.btnSetNotSkip = new System.Windows.Forms.Button();
            this.dtpTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeStart = new System.Windows.Forms.DateTimePicker();
            this.rdoSkipReason = new System.Windows.Forms.RadioButton();
            this.rdoSkipCoilID = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblNormalWord = new System.Windows.Forms.Label();
            this.pnlCoilSkip = new System.Windows.Forms.Panel();
            this.pnlQueryData = new System.Windows.Forms.Panel();
            this.btnDoQuery = new System.Windows.Forms.Button();
            this.cboSkipReason = new System.Windows.Forms.ComboBox();
            this.rdoSkipTime = new System.Windows.Forms.RadioButton();
            this.rdoSLSRange = new System.Windows.Forms.RadioButton();
            this.txtSkipCoilID = new System.Windows.Forms.TextBox();
            this.txtSeqnum = new System.Windows.Forms.TextBox();
            this.txtSchlnum = new System.Windows.Forms.TextBox();
            this.txtLinenum = new System.Windows.Forms.TextBox();
            this.dgCoilSkip = new System.Windows.Forms.DataGridView();
            this.txtSkipComment = new System.Windows.Forms.TextBox();
            this.btnDeleteData = new System.Windows.Forms.Button();
            this.pnlShow = new System.Windows.Forms.Panel();
            this.btnQueryData = new System.Windows.Forms.Button();
            this.lblCoilID = new System.Windows.Forms.Label();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.pnlCoilSkip.SuspendLayout();
            this.pnlQueryData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCoilSkip)).BeginInit();
            this.pnlShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClosePanel
            // 
            this.btnClosePanel.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnClosePanel.Location = new System.Drawing.Point(813, 4);
            this.btnClosePanel.Name = "btnClosePanel";
            this.btnClosePanel.Size = new System.Drawing.Size(40, 40);
            this.btnClosePanel.TabIndex = 308;
            this.btnClosePanel.Text = "X";
            this.btnClosePanel.UseVisualStyleBackColor = true;
            this.btnClosePanel.Click += new System.EventHandler(this.BtnClosePanel_Click);
            // 
            // cboCoilID
            // 
            this.cboCoilID.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboCoilID.FormattingEnabled = true;
            this.cboCoilID.Location = new System.Drawing.Point(152, 20);
            this.cboCoilID.Name = "cboCoilID";
            this.cboCoilID.Size = new System.Drawing.Size(192, 32);
            this.cboCoilID.TabIndex = 356;
            // 
            // cboSkipReasonCode
            // 
            this.cboSkipReasonCode.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboSkipReasonCode.FormattingEnabled = true;
            this.cboSkipReasonCode.Location = new System.Drawing.Point(444, 20);
            this.cboSkipReasonCode.Name = "cboSkipReasonCode";
            this.cboSkipReasonCode.Size = new System.Drawing.Size(192, 32);
            this.cboSkipReasonCode.TabIndex = 355;
            // 
            // lblSkipReasonCode
            // 
            this.lblSkipReasonCode.AutoSize = true;
            this.lblSkipReasonCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSkipReasonCode.Location = new System.Drawing.Point(383, 27);
            this.lblSkipReasonCode.Name = "lblSkipReasonCode";
            this.lblSkipReasonCode.Size = new System.Drawing.Size(54, 21);
            this.lblSkipReasonCode.TabIndex = 354;
            this.lblSkipReasonCode.Text = "原因";
            // 
            // lblSkipComment
            // 
            this.lblSkipComment.AutoSize = true;
            this.lblSkipComment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSkipComment.Location = new System.Drawing.Point(78, 74);
            this.lblSkipComment.Name = "lblSkipComment";
            this.lblSkipComment.Size = new System.Drawing.Size(54, 21);
            this.lblSkipComment.TabIndex = 353;
            this.lblSkipComment.Text = "備註";
            // 
            // btnSetNotSkip
            // 
            this.btnSetNotSkip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetNotSkip.BackgroundImage")));
            this.btnSetNotSkip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetNotSkip.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.btnSetNotSkip.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetNotSkip.Location = new System.Drawing.Point(1454, 29);
            this.btnSetNotSkip.Name = "btnSetNotSkip";
            this.btnSetNotSkip.Size = new System.Drawing.Size(115, 75);
            this.btnSetNotSkip.TabIndex = 297;
            this.btnSetNotSkip.Text = "取消\r\n跳軋";
            this.btnSetNotSkip.UseVisualStyleBackColor = true;
            // 
            // dtpTimeEnd
            // 
            this.dtpTimeEnd.CustomFormat = "yyyy/MM/dd";
            this.dtpTimeEnd.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeEnd.Location = new System.Drawing.Point(457, 35);
            this.dtpTimeEnd.Name = "dtpTimeEnd";
            this.dtpTimeEnd.Size = new System.Drawing.Size(209, 43);
            this.dtpTimeEnd.TabIndex = 304;
            // 
            // dtpTimeStart
            // 
            this.dtpTimeStart.CustomFormat = "yyyy/MM/dd";
            this.dtpTimeStart.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeStart.Location = new System.Drawing.Point(183, 35);
            this.dtpTimeStart.Name = "dtpTimeStart";
            this.dtpTimeStart.Size = new System.Drawing.Size(209, 43);
            this.dtpTimeStart.TabIndex = 303;
            // 
            // rdoSkipReason
            // 
            this.rdoSkipReason.AutoSize = true;
            this.rdoSkipReason.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdoSkipReason.ForeColor = System.Drawing.Color.Black;
            this.rdoSkipReason.Location = new System.Drawing.Point(64, 133);
            this.rdoSkipReason.Name = "rdoSkipReason";
            this.rdoSkipReason.Size = new System.Drawing.Size(86, 31);
            this.rdoSkipReason.TabIndex = 305;
            this.rdoSkipReason.TabStop = true;
            this.rdoSkipReason.Text = "原因";
            this.rdoSkipReason.UseVisualStyleBackColor = true;
            // 
            // rdoSkipCoilID
            // 
            this.rdoSkipCoilID.AutoSize = true;
            this.rdoSkipCoilID.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdoSkipCoilID.ForeColor = System.Drawing.Color.Black;
            this.rdoSkipCoilID.Location = new System.Drawing.Point(64, 87);
            this.rdoSkipCoilID.Name = "rdoSkipCoilID";
            this.rdoSkipCoilID.Size = new System.Drawing.Size(142, 31);
            this.rdoSkipCoilID.TabIndex = 300;
            this.rdoSkipCoilID.Text = "鋼捲編號";
            this.rdoSkipCoilID.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdate.BackgroundImage")));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpdate.Location = new System.Drawing.Point(1328, 29);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(115, 75);
            this.btnUpdate.TabIndex = 294;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // lblNormalWord
            // 
            this.lblNormalWord.AutoSize = true;
            this.lblNormalWord.BackColor = System.Drawing.Color.Transparent;
            this.lblNormalWord.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNormalWord.ForeColor = System.Drawing.Color.Black;
            this.lblNormalWord.Location = new System.Drawing.Point(411, 41);
            this.lblNormalWord.Name = "lblNormalWord";
            this.lblNormalWord.Size = new System.Drawing.Size(40, 27);
            this.lblNormalWord.TabIndex = 307;
            this.lblNormalWord.Text = "到";
            // 
            // pnlCoilSkip
            // 
            this.pnlCoilSkip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCoilSkip.Controls.Add(this.pnlQueryData);
            this.pnlCoilSkip.Controls.Add(this.dgCoilSkip);
            this.pnlCoilSkip.Location = new System.Drawing.Point(15, 185);
            this.pnlCoilSkip.Name = "pnlCoilSkip";
            this.pnlCoilSkip.Size = new System.Drawing.Size(1883, 784);
            this.pnlCoilSkip.TabIndex = 351;
            // 
            // pnlQueryData
            // 
            this.pnlQueryData.Controls.Add(this.btnDoQuery);
            this.pnlQueryData.Controls.Add(this.btnClosePanel);
            this.pnlQueryData.Controls.Add(this.dtpTimeEnd);
            this.pnlQueryData.Controls.Add(this.dtpTimeStart);
            this.pnlQueryData.Controls.Add(this.cboSkipReason);
            this.pnlQueryData.Controls.Add(this.rdoSkipReason);
            this.pnlQueryData.Controls.Add(this.lblNormalWord);
            this.pnlQueryData.Controls.Add(this.rdoSkipTime);
            this.pnlQueryData.Controls.Add(this.rdoSLSRange);
            this.pnlQueryData.Controls.Add(this.txtSkipCoilID);
            this.pnlQueryData.Controls.Add(this.rdoSkipCoilID);
            this.pnlQueryData.Controls.Add(this.txtSeqnum);
            this.pnlQueryData.Controls.Add(this.txtSchlnum);
            this.pnlQueryData.Controls.Add(this.txtLinenum);
            this.pnlQueryData.Location = new System.Drawing.Point(1007, 20);
            this.pnlQueryData.Name = "pnlQueryData";
            this.pnlQueryData.Size = new System.Drawing.Size(855, 206);
            this.pnlQueryData.TabIndex = 348;
            this.pnlQueryData.Visible = false;
            // 
            // btnDoQuery
            // 
            this.btnDoQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDoQuery.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDoQuery.ForeColor = System.Drawing.Color.Black;
            this.btnDoQuery.Location = new System.Drawing.Point(681, 149);
            this.btnDoQuery.Name = "btnDoQuery";
            this.btnDoQuery.Size = new System.Drawing.Size(172, 55);
            this.btnDoQuery.TabIndex = 309;
            this.btnDoQuery.Text = "開始查詢";
            this.btnDoQuery.UseVisualStyleBackColor = true;
            // 
            // cboSkipReason
            // 
            this.cboSkipReason.DropDownWidth = 600;
            this.cboSkipReason.Enabled = false;
            this.cboSkipReason.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboSkipReason.FormattingEnabled = true;
            this.cboSkipReason.Items.AddRange(new object[] {
            "請選擇跳軋代碼原因"});
            this.cboSkipReason.Location = new System.Drawing.Point(156, 133);
            this.cboSkipReason.Name = "cboSkipReason";
            this.cboSkipReason.Size = new System.Drawing.Size(456, 35);
            this.cboSkipReason.TabIndex = 306;
            // 
            // rdoSkipTime
            // 
            this.rdoSkipTime.AutoSize = true;
            this.rdoSkipTime.Checked = true;
            this.rdoSkipTime.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdoSkipTime.ForeColor = System.Drawing.Color.Black;
            this.rdoSkipTime.Location = new System.Drawing.Point(64, 41);
            this.rdoSkipTime.Name = "rdoSkipTime";
            this.rdoSkipTime.Size = new System.Drawing.Size(122, 31);
            this.rdoSkipTime.TabIndex = 302;
            this.rdoSkipTime.TabStop = true;
            this.rdoSkipTime.Text = "時間 從";
            this.rdoSkipTime.UseVisualStyleBackColor = true;
            // 
            // rdoSLSRange
            // 
            this.rdoSLSRange.AutoSize = true;
            this.rdoSLSRange.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdoSLSRange.ForeColor = System.Drawing.Color.Black;
            this.rdoSLSRange.Location = new System.Drawing.Point(416, 91);
            this.rdoSLSRange.Name = "rdoSLSRange";
            this.rdoSLSRange.Size = new System.Drawing.Size(196, 31);
            this.rdoSLSRange.TabIndex = 301;
            this.rdoSLSRange.Text = "SCH-LIN-SEQ";
            this.rdoSLSRange.UseVisualStyleBackColor = true;
            // 
            // txtSkipCoilID
            // 
            this.txtSkipCoilID.BackColor = System.Drawing.Color.White;
            this.txtSkipCoilID.Enabled = false;
            this.txtSkipCoilID.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSkipCoilID.Location = new System.Drawing.Point(212, 87);
            this.txtSkipCoilID.MaxLength = 12;
            this.txtSkipCoilID.Name = "txtSkipCoilID";
            this.txtSkipCoilID.Size = new System.Drawing.Size(180, 35);
            this.txtSkipCoilID.TabIndex = 290;
            // 
            // txtSeqnum
            // 
            this.txtSeqnum.BackColor = System.Drawing.Color.White;
            this.txtSeqnum.Enabled = false;
            this.txtSeqnum.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSeqnum.Location = new System.Drawing.Point(742, 87);
            this.txtSeqnum.MaxLength = 3;
            this.txtSeqnum.Name = "txtSeqnum";
            this.txtSeqnum.Size = new System.Drawing.Size(60, 35);
            this.txtSeqnum.TabIndex = 299;
            // 
            // txtSchlnum
            // 
            this.txtSchlnum.BackColor = System.Drawing.Color.White;
            this.txtSchlnum.Enabled = false;
            this.txtSchlnum.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSchlnum.Location = new System.Drawing.Point(620, 87);
            this.txtSchlnum.MaxLength = 3;
            this.txtSchlnum.Name = "txtSchlnum";
            this.txtSchlnum.Size = new System.Drawing.Size(60, 35);
            this.txtSchlnum.TabIndex = 292;
            // 
            // txtLinenum
            // 
            this.txtLinenum.BackColor = System.Drawing.Color.White;
            this.txtLinenum.Enabled = false;
            this.txtLinenum.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtLinenum.Location = new System.Drawing.Point(681, 87);
            this.txtLinenum.MaxLength = 3;
            this.txtLinenum.Name = "txtLinenum";
            this.txtLinenum.Size = new System.Drawing.Size(60, 35);
            this.txtLinenum.TabIndex = 298;
            // 
            // dgCoilSkip
            // 
            this.dgCoilSkip.AllowUserToAddRows = false;
            this.dgCoilSkip.AllowUserToDeleteRows = false;
            this.dgCoilSkip.AllowUserToResizeColumns = false;
            this.dgCoilSkip.AllowUserToResizeRows = false;
            this.dgCoilSkip.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgCoilSkip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCoilSkip.Location = new System.Drawing.Point(4, 4);
            this.dgCoilSkip.Name = "dgCoilSkip";
            this.dgCoilSkip.RowTemplate.Height = 24;
            this.dgCoilSkip.Size = new System.Drawing.Size(1873, 774);
            this.dgCoilSkip.TabIndex = 349;
            this.dgCoilSkip.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgCoilSkip_CellClick);
            this.dgCoilSkip.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgCoilSkip_CellMouseDoubleClick);
            // 
            // txtSkipComment
            // 
            this.txtSkipComment.BackColor = System.Drawing.SystemColors.Window;
            this.txtSkipComment.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSkipComment.Location = new System.Drawing.Point(151, 67);
            this.txtSkipComment.MaxLength = 50;
            this.txtSkipComment.Name = "txtSkipComment";
            this.txtSkipComment.Size = new System.Drawing.Size(1000, 35);
            this.txtSkipComment.TabIndex = 348;
            // 
            // btnDeleteData
            // 
            this.btnDeleteData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteData.BackgroundImage")));
            this.btnDeleteData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteData.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.btnDeleteData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteData.Location = new System.Drawing.Point(1580, 29);
            this.btnDeleteData.Name = "btnDeleteData";
            this.btnDeleteData.Size = new System.Drawing.Size(115, 75);
            this.btnDeleteData.TabIndex = 299;
            this.btnDeleteData.Text = "確認\r\n跳軋";
            this.btnDeleteData.UseVisualStyleBackColor = true;
            // 
            // pnlShow
            // 
            this.pnlShow.BackColor = System.Drawing.Color.GreenYellow;
            this.pnlShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlShow.Controls.Add(this.cboCoilID);
            this.pnlShow.Controls.Add(this.cboSkipReasonCode);
            this.pnlShow.Controls.Add(this.lblSkipReasonCode);
            this.pnlShow.Controls.Add(this.lblSkipComment);
            this.pnlShow.Controls.Add(this.btnSetNotSkip);
            this.pnlShow.Controls.Add(this.btnUpdate);
            this.pnlShow.Controls.Add(this.txtSkipComment);
            this.pnlShow.Controls.Add(this.btnDeleteData);
            this.pnlShow.Controls.Add(this.btnQueryData);
            this.pnlShow.Controls.Add(this.lblCoilID);
            this.pnlShow.Location = new System.Drawing.Point(15, 54);
            this.pnlShow.Name = "pnlShow";
            this.pnlShow.Size = new System.Drawing.Size(1883, 125);
            this.pnlShow.TabIndex = 350;
            // 
            // btnQueryData
            // 
            this.btnQueryData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQueryData.BackgroundImage")));
            this.btnQueryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQueryData.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.btnQueryData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQueryData.Location = new System.Drawing.Point(1202, 29);
            this.btnQueryData.Name = "btnQueryData";
            this.btnQueryData.Size = new System.Drawing.Size(115, 75);
            this.btnQueryData.TabIndex = 291;
            this.btnQueryData.Text = "查詢";
            this.btnQueryData.UseVisualStyleBackColor = true;
            this.btnQueryData.Click += new System.EventHandler(this.BtnQueryData_Click);
            // 
            // lblCoilID
            // 
            this.lblCoilID.AutoSize = true;
            this.lblCoilID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCoilID.Location = new System.Drawing.Point(34, 27);
            this.lblCoilID.Name = "lblCoilID";
            this.lblCoilID.Size = new System.Drawing.Size(98, 21);
            this.lblCoilID.TabIndex = 349;
            this.lblCoilID.Text = "鋼捲編號";
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.lblMainTitle.Location = new System.Drawing.Point(850, 13);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(141, 25);
            this.lblMainTitle.TabIndex = 349;
            this.lblMainTitle.Text = "2-3 跳軋資訊";
            // 
            // frm_2_3_CoilSkip
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1910, 981);
            this.Controls.Add(this.pnlCoilSkip);
            this.Controls.Add(this.pnlShow);
            this.Controls.Add(this.lblMainTitle);
            this.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_2_3_CoilSkip";
            this.Text = "frm_2_3_CoilSkip";
            this.Shown += new System.EventHandler(this.Frm_2_3_CoilSkip_Shown);
            this.pnlCoilSkip.ResumeLayout(false);
            this.pnlQueryData.ResumeLayout(false);
            this.pnlQueryData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCoilSkip)).EndInit();
            this.pnlShow.ResumeLayout(false);
            this.pnlShow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnClosePanel;
        internal System.Windows.Forms.ComboBox cboCoilID;
        internal System.Windows.Forms.ComboBox cboSkipReasonCode;
        internal System.Windows.Forms.Label lblSkipReasonCode;
        internal System.Windows.Forms.Label lblSkipComment;
        internal System.Windows.Forms.Button btnSetNotSkip;
        internal System.Windows.Forms.DateTimePicker dtpTimeEnd;
        internal System.Windows.Forms.DateTimePicker dtpTimeStart;
        internal System.Windows.Forms.RadioButton rdoSkipReason;
        internal System.Windows.Forms.RadioButton rdoSkipCoilID;
        internal System.Windows.Forms.Button btnUpdate;
        internal System.Windows.Forms.Label lblNormalWord;
        internal System.Windows.Forms.Panel pnlCoilSkip;
        internal System.Windows.Forms.Panel pnlQueryData;
        internal System.Windows.Forms.Button btnDoQuery;
        internal System.Windows.Forms.ComboBox cboSkipReason;
        internal System.Windows.Forms.RadioButton rdoSkipTime;
        internal System.Windows.Forms.RadioButton rdoSLSRange;
        internal System.Windows.Forms.TextBox txtSkipCoilID;
        internal System.Windows.Forms.TextBox txtSeqnum;
        internal System.Windows.Forms.TextBox txtSchlnum;
        internal System.Windows.Forms.TextBox txtLinenum;
        internal System.Windows.Forms.DataGridView dgCoilSkip;
        internal System.Windows.Forms.TextBox txtSkipComment;
        internal System.Windows.Forms.Button btnDeleteData;
        internal System.Windows.Forms.Panel pnlShow;
        internal System.Windows.Forms.Button btnQueryData;
        internal System.Windows.Forms.Label lblCoilID;
        internal System.Windows.Forms.Label lblMainTitle;
    }
}