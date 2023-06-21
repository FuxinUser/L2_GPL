namespace LabelPrint.View
{
    partial class LprForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LprForm));
            this.RichTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.tbRemotePort = new System.Windows.Forms.TextBox();
            this.tbRemoteIp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.msgGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.tbLocalIp = new System.Windows.Forms.TextBox();
            this.opTab = new System.Windows.Forms.TabPage();
            this.textSample = new System.Windows.Forms.TextBox();
            this.textThick = new System.Windows.Forms.TextBox();
            this.btnPrinterSampleLabel = new System.Windows.Forms.Button();
            this.textCoil = new System.Windows.Forms.TextBox();
            this.btnPrinterCoilLabel = new System.Windows.Forms.Button();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.msgGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.opTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // RichTextBoxConsole
            // 
            this.RichTextBoxConsole.Location = new System.Drawing.Point(11, 18);
            this.RichTextBoxConsole.Name = "RichTextBoxConsole";
            this.RichTextBoxConsole.Size = new System.Drawing.Size(616, 419);
            this.RichTextBoxConsole.TabIndex = 41;
            this.RichTextBoxConsole.Text = "";
            // 
            // tbRemotePort
            // 
            this.tbRemotePort.Enabled = false;
            this.tbRemotePort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemotePort.Location = new System.Drawing.Point(520, 23);
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
            this.tbRemoteIp.Location = new System.Drawing.Point(394, 23);
            this.tbRemoteIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbRemoteIp.Name = "tbRemoteIp";
            this.tbRemoteIp.Size = new System.Drawing.Size(90, 22);
            this.tbRemoteIp.TabIndex = 3;
            this.tbRemoteIp.Text = "127.0.0.1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 1;
            this.label7.Text = "Port";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.opTab);
            this.tabControl2.Location = new System.Drawing.Point(12, 12);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(681, 563);
            this.tabControl2.TabIndex = 48;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.msgGroupBox);
            this.tabPage3.Controls.Add(this.groupBox3);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbRemotePort);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.tbRemoteIp);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbLocalPort);
            this.groupBox3.Controls.Add(this.tbLocalIp);
            this.groupBox3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(627, 56);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "連線資訊";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 47;
            this.label9.Text = "本地";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Port";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Enabled = false;
            this.tbLocalPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalPort.Location = new System.Drawing.Point(199, 22);
            this.tbLocalPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(90, 22);
            this.tbLocalPort.TabIndex = 1;
            // 
            // tbLocalIp
            // 
            this.tbLocalIp.Enabled = false;
            this.tbLocalIp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocalIp.Location = new System.Drawing.Point(71, 22);
            this.tbLocalIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLocalIp.Name = "tbLocalIp";
            this.tbLocalIp.Size = new System.Drawing.Size(90, 22);
            this.tbLocalIp.TabIndex = 0;
            // 
            // opTab
            // 
            this.opTab.Controls.Add(this.textSample);
            this.opTab.Controls.Add(this.textThick);
            this.opTab.Controls.Add(this.btnPrinterSampleLabel);
            this.opTab.Controls.Add(this.textCoil);
            this.opTab.Controls.Add(this.btnPrinterCoilLabel);
            this.opTab.Location = new System.Drawing.Point(4, 22);
            this.opTab.Name = "opTab";
            this.opTab.Padding = new System.Windows.Forms.Padding(3);
            this.opTab.Size = new System.Drawing.Size(673, 537);
            this.opTab.TabIndex = 1;
            this.opTab.Text = "操作測試";
            this.opTab.UseVisualStyleBackColor = true;
            // 
            // textSample
            // 
            this.textSample.Location = new System.Drawing.Point(117, 95);
            this.textSample.Name = "textSample";
            this.textSample.Size = new System.Drawing.Size(100, 22);
            this.textSample.TabIndex = 7;
            this.textSample.Text = "HC20210607001";
            // 
            // textThick
            // 
            this.textThick.Location = new System.Drawing.Point(117, 67);
            this.textThick.Name = "textThick";
            this.textThick.Size = new System.Drawing.Size(100, 22);
            this.textThick.TabIndex = 6;
            this.textThick.Text = "0.12";
            // 
            // btnPrinterSampleLabel
            // 
            this.btnPrinterSampleLabel.Location = new System.Drawing.Point(16, 67);
            this.btnPrinterSampleLabel.Name = "btnPrinterSampleLabel";
            this.btnPrinterSampleLabel.Size = new System.Drawing.Size(95, 23);
            this.btnPrinterSampleLabel.TabIndex = 5;
            this.btnPrinterSampleLabel.Text = "列印樣品標籤";
            this.btnPrinterSampleLabel.UseVisualStyleBackColor = true;
            this.btnPrinterSampleLabel.Click += new System.EventHandler(this.btnPrinterSampleLabel_Click_1);
            // 
            // textCoil
            // 
            this.textCoil.Location = new System.Drawing.Point(117, 30);
            this.textCoil.Name = "textCoil";
            this.textCoil.Size = new System.Drawing.Size(100, 22);
            this.textCoil.TabIndex = 2;
            // 
            // btnPrinterCoilLabel
            // 
            this.btnPrinterCoilLabel.Location = new System.Drawing.Point(16, 28);
            this.btnPrinterCoilLabel.Name = "btnPrinterCoilLabel";
            this.btnPrinterCoilLabel.Size = new System.Drawing.Size(95, 22);
            this.btnPrinterCoilLabel.TabIndex = 1;
            this.btnPrinterCoilLabel.Text = "列印鋼捲編號";
            this.btnPrinterCoilLabel.UseVisualStyleBackColor = true;
            this.btnPrinterCoilLabel.Click += new System.EventHandler(this.btnPrinterCoilLabel_Click);
            // 
            // LprForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 583);
            this.Controls.Add(this.tabControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LprForm";
            this.Text = "LabelPrint";
            this.Shown += new System.EventHandler(this.LprForm_Shown);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.msgGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.opTab.ResumeLayout(false);
            this.opTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox RichTextBoxConsole;
        private System.Windows.Forms.TextBox tbRemotePort;
        private System.Windows.Forms.TextBox tbRemoteIp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox msgGroupBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.TextBox tbLocalIp;
        private System.Windows.Forms.TabPage opTab;
        private System.Windows.Forms.TextBox textCoil;
        private System.Windows.Forms.Button btnPrinterCoilLabel;
        private System.Windows.Forms.TextBox textSample;
        private System.Windows.Forms.TextBox textThick;
        private System.Windows.Forms.Button btnPrinterSampleLabel;
    }
}

