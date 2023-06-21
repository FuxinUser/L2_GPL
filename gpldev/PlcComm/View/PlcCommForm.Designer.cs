namespace WMSComm.View
{
    partial class PlcCommForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlcCommForm));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnHisSnd = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.BoxMsgID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Dgv_History = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.tbLocalIp = new System.Windows.Forms.TextBox();
            this.RichTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbRemotePort = new System.Windows.Forms.TextBox();
            this.tbRemoteIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCycleRcvLocalPort = new System.Windows.Forms.TextBox();
            this.tbCycleRcvLocalIp = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClearBuffer = new System.Windows.Forms.Button();
            this.btn209 = new System.Windows.Forms.Button();
            this.btn208 = new System.Windows.Forms.Button();
            this.btn207 = new System.Windows.Forms.Button();
            this.msgGroupBox = new System.Windows.Forms.GroupBox();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_History)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.msgGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnHisSnd);
            this.tabPage3.Controls.Add(this.btnHistory);
            this.tabPage3.Controls.Add(this.BoxMsgID);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.Dgv_History);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(736, 589);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnHisSnd
            // 
            this.btnHisSnd.Location = new System.Drawing.Point(293, 38);
            this.btnHisSnd.Name = "btnHisSnd";
            this.btnHisSnd.Size = new System.Drawing.Size(83, 23);
            this.btnHisSnd.TabIndex = 40;
            this.btnHisSnd.Text = "發送";
            this.btnHisSnd.UseVisualStyleBackColor = true;
            this.btnHisSnd.Click += new System.EventHandler(this.btnHisSnd_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(204, 37);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(83, 23);
            this.btnHistory.TabIndex = 39;
            this.btnHistory.Text = "撈取";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // BoxMsgID
            // 
            this.BoxMsgID.FormattingEnabled = true;
            this.BoxMsgID.Items.AddRange(new object[] {
            "L1L2_102",
            "L1L2_104",
            "L1L2_105",
            "L1L2_106",
            "L1L2_107",
            "L1L2_108",
            "L1L2_109",
            "L1L2_110",
            "L1L2_111",
            "L1L2_112",
            "L1L2_113",
            "L1L2_114",
            "L1L2_115",
            "L1L2_116",
            "L1L2_117",
            "L1L2_118",
            "L1L2_119",
            "L1L2_120",
            "L1L2_121",
            "L1L2_122",
            "L1L2_123",
            "L1L2_124",
            "L1L2_125",
            "L1L2_126",
            "L2L1_202",
            "L2L1_203",
            "L2L1_204",
            "L2L1_205",
            "L2L1_206",
            "L2L1_207",
            "L2L1_208",
            "L2L1_209",
            "L2L1_210",
            "L2L1_211"});
            this.BoxMsgID.Location = new System.Drawing.Point(85, 38);
            this.BoxMsgID.Margin = new System.Windows.Forms.Padding(2);
            this.BoxMsgID.Name = "BoxMsgID";
            this.BoxMsgID.Size = new System.Drawing.Size(113, 20);
            this.BoxMsgID.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(17, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 37;
            this.label3.Text = "報文號 :";
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
            this.Dgv_History.Location = new System.Drawing.Point(20, 78);
            this.Dgv_History.MultiSelect = false;
            this.Dgv_History.Name = "Dgv_History";
            this.Dgv_History.ReadOnly = true;
            this.Dgv_History.RowHeadersVisible = false;
            this.Dgv_History.RowTemplate.Height = 24;
            this.Dgv_History.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_History.Size = new System.Drawing.Size(693, 471);
            this.Dgv_History.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.msgGroupBox);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(736, 589);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "監控";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbLocalPort);
            this.groupBox4.Controls.Add(this.tbLocalIp);
            this.groupBox4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(19, 79);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(325, 56);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "一般報文接收連線設置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "IP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(178, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 8;
            this.label7.Text = "Port";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Enabled = false;
            this.tbLocalPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalPort.Location = new System.Drawing.Point(214, 15);
            this.tbLocalPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(90, 22);
            this.tbLocalPort.TabIndex = 1;
            // 
            // tbLocalIp
            // 
            this.tbLocalIp.Enabled = false;
            this.tbLocalIp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalIp.Location = new System.Drawing.Point(88, 15);
            this.tbLocalIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalIp.Name = "tbLocalIp";
            this.tbLocalIp.Size = new System.Drawing.Size(90, 22);
            this.tbLocalIp.TabIndex = 0;
            // 
            // RichTextBoxConsole
            // 
            this.RichTextBoxConsole.Location = new System.Drawing.Point(12, 21);
            this.RichTextBoxConsole.Name = "RichTextBoxConsole";
            this.RichTextBoxConsole.Size = new System.Drawing.Size(666, 382);
            this.RichTextBoxConsole.TabIndex = 38;
            this.RichTextBoxConsole.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbRemotePort);
            this.groupBox1.Controls.Add(this.tbRemoteIp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(372, 79);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(325, 56);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "遠端連線設置";
            // 
            // tbRemotePort
            // 
            this.tbRemotePort.Enabled = false;
            this.tbRemotePort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemotePort.Location = new System.Drawing.Point(213, 18);
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
            this.tbRemoteIp.Location = new System.Drawing.Point(87, 18);
            this.tbRemoteIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbRemoteIp.Name = "tbRemoteIp";
            this.tbRemoteIp.Size = new System.Drawing.Size(90, 22);
            this.tbRemoteIp.TabIndex = 3;
            this.tbRemoteIp.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remote IP";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbCycleRcvLocalPort);
            this.groupBox2.Controls.Add(this.tbCycleRcvLocalIp);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 19);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(325, 56);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "循回報文接收連線設置";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Port";
            // 
            // tbCycleRcvLocalPort
            // 
            this.tbCycleRcvLocalPort.Enabled = false;
            this.tbCycleRcvLocalPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCycleRcvLocalPort.Location = new System.Drawing.Point(214, 15);
            this.tbCycleRcvLocalPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCycleRcvLocalPort.Name = "tbCycleRcvLocalPort";
            this.tbCycleRcvLocalPort.Size = new System.Drawing.Size(90, 22);
            this.tbCycleRcvLocalPort.TabIndex = 1;
            // 
            // tbCycleRcvLocalIp
            // 
            this.tbCycleRcvLocalIp.Enabled = false;
            this.tbCycleRcvLocalIp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCycleRcvLocalIp.Location = new System.Drawing.Point(88, 15);
            this.tbCycleRcvLocalIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCycleRcvLocalIp.Name = "tbCycleRcvLocalIp";
            this.tbCycleRcvLocalIp.Size = new System.Drawing.Size(90, 22);
            this.tbCycleRcvLocalIp.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 11);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(744, 615);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnClearBuffer);
            this.tabPage2.Controls.Add(this.btn209);
            this.tabPage2.Controls.Add(this.btn208);
            this.tabPage2.Controls.Add(this.btn207);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1211, 589);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "測試用";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClearBuffer
            // 
            this.btnClearBuffer.Location = new System.Drawing.Point(32, 193);
            this.btnClearBuffer.Name = "btnClearBuffer";
            this.btnClearBuffer.Size = new System.Drawing.Size(106, 35);
            this.btnClearBuffer.TabIndex = 41;
            this.btnClearBuffer.Text = "清空Buffer";
            this.btnClearBuffer.UseVisualStyleBackColor = true;
            this.btnClearBuffer.Click += new System.EventHandler(this.btnClearBuffer_Click);
            // 
            // btn209
            // 
            this.btn209.Location = new System.Drawing.Point(32, 117);
            this.btn209.Name = "btn209";
            this.btn209.Size = new System.Drawing.Size(106, 33);
            this.btn209.TabIndex = 2;
            this.btn209.Text = "209報文發送";
            this.btn209.UseVisualStyleBackColor = true;
            this.btn209.Click += new System.EventHandler(this.btn209_Click);
            // 
            // btn208
            // 
            this.btn208.Location = new System.Drawing.Point(32, 78);
            this.btn208.Name = "btn208";
            this.btn208.Size = new System.Drawing.Size(106, 33);
            this.btn208.TabIndex = 1;
            this.btn208.Text = "208報文發送";
            this.btn208.UseVisualStyleBackColor = true;
            this.btn208.Click += new System.EventHandler(this.btn208_Click);
            // 
            // btn207
            // 
            this.btn207.Location = new System.Drawing.Point(32, 39);
            this.btn207.Name = "btn207";
            this.btn207.Size = new System.Drawing.Size(106, 33);
            this.btn207.TabIndex = 0;
            this.btn207.Text = "207報文發送";
            this.btn207.UseVisualStyleBackColor = true;
            this.btn207.Click += new System.EventHandler(this.btn207_Click);
            // 
            // msgGroupBox
            // 
            this.msgGroupBox.Controls.Add(this.RichTextBoxConsole);
            this.msgGroupBox.Location = new System.Drawing.Point(19, 144);
            this.msgGroupBox.Name = "msgGroupBox";
            this.msgGroupBox.Size = new System.Drawing.Size(690, 409);
            this.msgGroupBox.TabIndex = 46;
            this.msgGroupBox.TabStop = false;
            this.msgGroupBox.Text = "訊息";
            // 
            // PlcCommForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(761, 642);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PlcCommForm";
            this.Text = "PLC";
            this.Shown += new System.EventHandler(this.PlcCommForm_Shown);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_History)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.msgGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbRemotePort;
        private System.Windows.Forms.TextBox tbRemoteIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCycleRcvLocalPort;
        private System.Windows.Forms.TextBox tbCycleRcvLocalIp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.RichTextBox RichTextBoxConsole;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn209;
        private System.Windows.Forms.Button btn208;
        private System.Windows.Forms.Button btn207;
        private System.Windows.Forms.ComboBox BoxMsgID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView Dgv_History;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnHisSnd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.TextBox tbLocalIp;
        private System.Windows.Forms.Button btnClearBuffer;
        private System.Windows.Forms.GroupBox msgGroupBox;
    }
}

