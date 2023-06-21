namespace MMSComm.View
{
    partial class MMSFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MMSFrom));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.RichTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.tbRemotePort = new System.Windows.Forms.TextBox();
            this.tbRemoteIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.tbLocalIp = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Dgv_MsgDetail = new System.Windows.Forms.DataGridView();
            this.BoxMsgID = new System.Windows.Forms.ComboBox();
            this.BoxTableName = new System.Windows.Forms.ComboBox();
            this.btnHisSnd = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.Dgv_History = new System.Windows.Forms.DataGridView();
            this.msgGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MsgDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_History)).BeginInit();
            this.msgGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 570);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.msgGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(734, 544);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "監控";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // RichTextBoxConsole
            // 
            this.RichTextBoxConsole.Location = new System.Drawing.Point(6, 21);
            this.RichTextBoxConsole.Name = "RichTextBoxConsole";
            this.RichTextBoxConsole.Size = new System.Drawing.Size(690, 392);
            this.RichTextBoxConsole.TabIndex = 37;
            this.RichTextBoxConsole.Text = "";
            // 
            // tbRemotePort
            // 
            this.tbRemotePort.Enabled = false;
            this.tbRemotePort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemotePort.Location = new System.Drawing.Point(574, 21);
            this.tbRemotePort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbRemotePort.Name = "tbRemotePort";
            this.tbRemotePort.Size = new System.Drawing.Size(90, 22);
            this.tbRemotePort.TabIndex = 4;
            this.tbRemotePort.Text = "9111";
            // 
            // tbRemoteIp
            // 
            this.tbRemoteIp.Enabled = false;
            this.tbRemoteIp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemoteIp.Location = new System.Drawing.Point(448, 21);
            this.tbRemoteIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbRemoteIp.Name = "tbRemoteIp";
            this.tbRemoteIp.Size = new System.Drawing.Size(90, 22);
            this.tbRemoteIp.TabIndex = 3;
            this.tbRemoteIp.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(538, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(76, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Port";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Enabled = false;
            this.tbLocalPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalPort.Location = new System.Drawing.Point(223, 22);
            this.tbLocalPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(90, 22);
            this.tbLocalPort.TabIndex = 1;
            // 
            // tbLocalIp
            // 
            this.tbLocalIp.Enabled = false;
            this.tbLocalIp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalIp.Location = new System.Drawing.Point(97, 22);
            this.tbLocalIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalIp.Name = "tbLocalIp";
            this.tbLocalIp.Size = new System.Drawing.Size(90, 22);
            this.tbLocalIp.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Dgv_MsgDetail);
            this.tabPage3.Controls.Add(this.BoxMsgID);
            this.tabPage3.Controls.Add(this.BoxTableName);
            this.tabPage3.Controls.Add(this.btnHisSnd);
            this.tabPage3.Controls.Add(this.btnHistory);
            this.tabPage3.Controls.Add(this.Dgv_History);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(734, 544);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Dgv_MsgDetail
            // 
            this.Dgv_MsgDetail.AllowUserToAddRows = false;
            this.Dgv_MsgDetail.AllowUserToDeleteRows = false;
            this.Dgv_MsgDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_MsgDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_MsgDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_MsgDetail.Location = new System.Drawing.Point(312, 55);
            this.Dgv_MsgDetail.MultiSelect = false;
            this.Dgv_MsgDetail.Name = "Dgv_MsgDetail";
            this.Dgv_MsgDetail.RowHeadersVisible = false;
            this.Dgv_MsgDetail.RowTemplate.Height = 24;
            this.Dgv_MsgDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_MsgDetail.Size = new System.Drawing.Size(394, 471);
            this.Dgv_MsgDetail.TabIndex = 8;
            // 
            // BoxMsgID
            // 
            this.BoxMsgID.FormattingEnabled = true;
            this.BoxMsgID.Items.AddRange(new object[] {
            "G1MM01",
            "G1MM02",
            "G1MM03",
            "G1MM04",
            "G1MM05",
            "G1MM06",
            "G1MM07",
            "G1MM08",
            "G1MM10",
            "G1MM15",
            "G1MM16",
            "G1MM18",
            "G1MM25"});
            this.BoxMsgID.Location = new System.Drawing.Point(136, 25);
            this.BoxMsgID.Margin = new System.Windows.Forms.Padding(2);
            this.BoxMsgID.Name = "BoxMsgID";
            this.BoxMsgID.Size = new System.Drawing.Size(113, 20);
            this.BoxMsgID.TabIndex = 43;
            // 
            // BoxTableName
            // 
            this.BoxTableName.FormattingEnabled = true;
            this.BoxTableName.Items.AddRange(new object[] {
            "TBL_MMS_ReceiveRecord",
            "TBL_MMS_SendRecord"});
            this.BoxTableName.Location = new System.Drawing.Point(17, 24);
            this.BoxTableName.Margin = new System.Windows.Forms.Padding(2);
            this.BoxTableName.Name = "BoxTableName";
            this.BoxTableName.Size = new System.Drawing.Size(113, 20);
            this.BoxTableName.TabIndex = 42;
            // 
            // btnHisSnd
            // 
            this.btnHisSnd.Location = new System.Drawing.Point(357, 23);
            this.btnHisSnd.Name = "btnHisSnd";
            this.btnHisSnd.Size = new System.Drawing.Size(83, 23);
            this.btnHisSnd.TabIndex = 41;
            this.btnHisSnd.Text = "發送";
            this.btnHisSnd.UseVisualStyleBackColor = true;
            this.btnHisSnd.Click += new System.EventHandler(this.btnHisSnd_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(266, 22);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(83, 23);
            this.btnHistory.TabIndex = 40;
            this.btnHistory.Text = "撈取";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // Dgv_History
            // 
            this.Dgv_History.AllowUserToAddRows = false;
            this.Dgv_History.AllowUserToDeleteRows = false;
            this.Dgv_History.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_History.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_History.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_History.Location = new System.Drawing.Point(17, 55);
            this.Dgv_History.MultiSelect = false;
            this.Dgv_History.Name = "Dgv_History";
            this.Dgv_History.ReadOnly = true;
            this.Dgv_History.RowHeadersVisible = false;
            this.Dgv_History.RowTemplate.Height = 24;
            this.Dgv_History.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_History.Size = new System.Drawing.Size(289, 471);
            this.Dgv_History.TabIndex = 7;
            this.Dgv_History.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_History_CellDoubleClick);
            // 
            // msgGroupBox
            // 
            this.msgGroupBox.Controls.Add(this.RichTextBoxConsole);
            this.msgGroupBox.Location = new System.Drawing.Point(8, 84);
            this.msgGroupBox.Name = "msgGroupBox";
            this.msgGroupBox.Size = new System.Drawing.Size(702, 419);
            this.msgGroupBox.TabIndex = 45;
            this.msgGroupBox.TabStop = false;
            this.msgGroupBox.Text = "訊息";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbRemotePort);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbRemoteIp);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbLocalPort);
            this.groupBox3.Controls.Add(this.tbLocalIp);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(8, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(702, 56);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "連線資訊";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 47;
            this.label9.Text = "本地";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(386, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "遠端 IP";
            // 
            // MMSFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(742, 580);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MMSFrom";
            this.Text = "MMS";
            this.Shown += new System.EventHandler(this.MESForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MsgDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_History)).EndInit();
            this.msgGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbRemotePort;
        private System.Windows.Forms.TextBox tbRemoteIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.TextBox tbLocalIp;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox RichTextBoxConsole;
        private System.Windows.Forms.DataGridView Dgv_History;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnHisSnd;
        private System.Windows.Forms.ComboBox BoxTableName;
        private System.Windows.Forms.ComboBox BoxMsgID;
        private System.Windows.Forms.DataGridView Dgv_MsgDetail;
        private System.Windows.Forms.GroupBox msgGroupBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}

