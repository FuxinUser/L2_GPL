namespace BCScnMgr.View
{
    partial class BCSScnForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCSScnForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeliveryScn = new System.Windows.Forms.Button();
            this.DeliveryComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeliveryCoilNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEntryScan = new System.Windows.Forms.Button();
            this.EntryComoBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEntryCoilNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RichTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.tbLocalIp = new System.Windows.Forms.TextBox();
            this.tbRemotePort = new System.Windows.Forms.TextBox();
            this.tbRemoteIp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.msgGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.opTab = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.msgGroupBox.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.opTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeliveryScn);
            this.groupBox2.Controls.Add(this.DeliveryComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDeliveryCoilNo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(166, 256);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(281, 112);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "BarCode Scanner出口端";
            // 
            // btnDeliveryScn
            // 
            this.btnDeliveryScn.Location = new System.Drawing.Point(194, 27);
            this.btnDeliveryScn.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeliveryScn.Name = "btnDeliveryScn";
            this.btnDeliveryScn.Size = new System.Drawing.Size(51, 24);
            this.btnDeliveryScn.TabIndex = 23;
            this.btnDeliveryScn.Text = "掃描";
            this.btnDeliveryScn.UseVisualStyleBackColor = true;
            this.btnDeliveryScn.Click += new System.EventHandler(this.btnDeliveryScn_Click);
            // 
            // DeliveryComboBox
            // 
            this.DeliveryComboBox.FormattingEnabled = true;
            this.DeliveryComboBox.Items.AddRange(new object[] {
            "DSK1",
            "DSK2",
            "DTOP"});
            this.DeliveryComboBox.Location = new System.Drawing.Point(77, 54);
            this.DeliveryComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.DeliveryComboBox.Name = "DeliveryComboBox";
            this.DeliveryComboBox.Size = new System.Drawing.Size(113, 20);
            this.DeliveryComboBox.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "鋼卷位置 :";
            // 
            // txtDeliveryCoilNo
            // 
            this.txtDeliveryCoilNo.Location = new System.Drawing.Point(77, 27);
            this.txtDeliveryCoilNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeliveryCoilNo.Name = "txtDeliveryCoilNo";
            this.txtDeliveryCoilNo.Size = new System.Drawing.Size(113, 22);
            this.txtDeliveryCoilNo.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "鋼卷編號 :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEntryScan);
            this.groupBox1.Controls.Add(this.EntryComoBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtEntryCoilNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(166, 122);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(281, 112);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BarCode Scanner入口端";
            // 
            // btnEntryScan
            // 
            this.btnEntryScan.Location = new System.Drawing.Point(194, 27);
            this.btnEntryScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntryScan.Name = "btnEntryScan";
            this.btnEntryScan.Size = new System.Drawing.Size(51, 24);
            this.btnEntryScan.TabIndex = 24;
            this.btnEntryScan.Text = "掃描";
            this.btnEntryScan.UseVisualStyleBackColor = true;
            this.btnEntryScan.Click += new System.EventHandler(this.btnEntryScan_Click);
            // 
            // EntryComoBox
            // 
            this.EntryComoBox.FormattingEnabled = true;
            this.EntryComoBox.Items.AddRange(new object[] {
            "ESK1",
            "ESK2",
            "ETOP"});
            this.EntryComoBox.Location = new System.Drawing.Point(77, 54);
            this.EntryComoBox.Margin = new System.Windows.Forms.Padding(2);
            this.EntryComoBox.Name = "EntryComoBox";
            this.EntryComoBox.Size = new System.Drawing.Size(113, 20);
            this.EntryComoBox.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "鋼卷位置 :";
            // 
            // txtEntryCoilNo
            // 
            this.txtEntryCoilNo.Location = new System.Drawing.Point(77, 27);
            this.txtEntryCoilNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtEntryCoilNo.Name = "txtEntryCoilNo";
            this.txtEntryCoilNo.Size = new System.Drawing.Size(113, 22);
            this.txtEntryCoilNo.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "鋼卷編號 :";
            // 
            // RichTextBoxConsole
            // 
            this.RichTextBoxConsole.Location = new System.Drawing.Point(6, 21);
            this.RichTextBoxConsole.Name = "RichTextBoxConsole";
            this.RichTextBoxConsole.Size = new System.Drawing.Size(621, 416);
            this.RichTextBoxConsole.TabIndex = 38;
            this.RichTextBoxConsole.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Port";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Enabled = false;
            this.tbLocalPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalPort.Location = new System.Drawing.Point(199, 21);
            this.tbLocalPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(90, 22);
            this.tbLocalPort.TabIndex = 1;
            // 
            // tbLocalIp
            // 
            this.tbLocalIp.Enabled = false;
            this.tbLocalIp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalIp.Location = new System.Drawing.Point(73, 21);
            this.tbLocalIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalIp.Name = "tbLocalIp";
            this.tbLocalIp.Size = new System.Drawing.Size(90, 22);
            this.tbLocalIp.TabIndex = 0;
            // 
            // tbRemotePort
            // 
            this.tbRemotePort.Enabled = false;
            this.tbRemotePort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemotePort.Location = new System.Drawing.Point(509, 21);
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
            this.tbRemoteIp.Location = new System.Drawing.Point(383, 21);
            this.tbRemoteIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbRemoteIp.Name = "tbRemoteIp";
            this.tbRemoteIp.Size = new System.Drawing.Size(90, 22);
            this.tbRemoteIp.TabIndex = 3;
            this.tbRemoteIp.Text = "127.0.0.1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(473, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 1;
            this.label7.Text = "Port";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.opTab);
            this.tabControl2.Location = new System.Drawing.Point(12, 22);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(681, 563);
            this.tabControl2.TabIndex = 47;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.msgGroupBox);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(673, 537);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "監控";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // msgGroupBox
            // 
            this.msgGroupBox.Controls.Add(this.RichTextBoxConsole);
            this.msgGroupBox.Location = new System.Drawing.Point(6, 88);
            this.msgGroupBox.Name = "msgGroupBox";
            this.msgGroupBox.Size = new System.Drawing.Size(636, 443);
            this.msgGroupBox.TabIndex = 42;
            this.msgGroupBox.TabStop = false;
            this.msgGroupBox.Text = "訊息";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbRemotePort);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.tbRemoteIp);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.tbLocalPort);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.tbLocalIp);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(6, 18);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(627, 56);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "連線資訊";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(321, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 48;
            this.label10.Text = "遠端";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 14);
            this.label11.TabIndex = 47;
            this.label11.Text = "本地";
            // 
            // opTab
            // 
            this.opTab.Controls.Add(this.groupBox1);
            this.opTab.Controls.Add(this.groupBox2);
            this.opTab.Location = new System.Drawing.Point(4, 22);
            this.opTab.Name = "opTab";
            this.opTab.Padding = new System.Windows.Forms.Padding(3);
            this.opTab.Size = new System.Drawing.Size(673, 537);
            this.opTab.TabIndex = 1;
            this.opTab.Text = "操作測試";
            this.opTab.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(356, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "IP";
            // 
            // BCSScnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 599);
            this.Controls.Add(this.tabControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BCSScnForm";
            this.Text = "BarCode";
            this.Load += new System.EventHandler(this.BCSScnForm_Load);
            this.Shown += new System.EventHandler(this.BCSScnForm_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.msgGroupBox.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.opTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeliveryScn;
        private System.Windows.Forms.ComboBox DeliveryComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeliveryCoilNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEntryScan;
        private System.Windows.Forms.ComboBox EntryComoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEntryCoilNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.TextBox tbLocalIp;
        private System.Windows.Forms.TextBox tbRemotePort;
        private System.Windows.Forms.TextBox tbRemoteIp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox RichTextBoxConsole;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox msgGroupBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage opTab;
        private System.Windows.Forms.Label label12;
    }
}

