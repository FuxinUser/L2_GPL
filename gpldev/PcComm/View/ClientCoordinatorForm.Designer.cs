namespace PcComm.View
{
    partial class ClientCoordinatorForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientCoordinatorForm1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.RichTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAskPDI = new System.Windows.Forms.Button();
            this.btnAskMMSSchedule = new System.Windows.Forms.Button();
            this.btnScnFailSelect = new System.Windows.Forms.Button();
            this.btnPDOSnd = new System.Windows.Forms.Button();
            this.btnPrintCheck = new System.Windows.Forms.Button();
            this.btnDefectCoilSnd = new System.Windows.Forms.Button();
            this.btnReturnCoil = new System.Windows.Forms.Button();
            this.btnCoilEnterEnd = new System.Windows.Forms.Button();
            this.btnCoilEnterStar = new System.Windows.Forms.Button();
            this.btnCoilDelete = new System.Windows.Forms.Button();
            this.btnCoilScheduleAdj = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeliveryScn = new System.Windows.Forms.Button();
            this.DeliveryComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeliveryCoilNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEntryScan = new System.Windows.Forms.Button();
            this.EntryComoBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEntryCoilNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.msgGroupBox = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.msgGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(24, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 468);
            this.tabControl1.TabIndex = 32;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.msgGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(516, 442);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "監控";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // RichTextBoxConsole
            // 
            this.RichTextBoxConsole.Location = new System.Drawing.Point(6, 21);
            this.RichTextBoxConsole.Name = "RichTextBoxConsole";
            this.RichTextBoxConsole.Size = new System.Drawing.Size(492, 403);
            this.RichTextBoxConsole.TabIndex = 40;
            this.RichTextBoxConsole.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAskPDI);
            this.groupBox2.Controls.Add(this.btnAskMMSSchedule);
            this.groupBox2.Controls.Add(this.btnScnFailSelect);
            this.groupBox2.Controls.Add(this.btnPDOSnd);
            this.groupBox2.Controls.Add(this.btnPrintCheck);
            this.groupBox2.Controls.Add(this.btnDefectCoilSnd);
            this.groupBox2.Controls.Add(this.btnReturnCoil);
            this.groupBox2.Controls.Add(this.btnCoilEnterEnd);
            this.groupBox2.Controls.Add(this.btnCoilEnterStar);
            this.groupBox2.Controls.Add(this.btnCoilDelete);
            this.groupBox2.Controls.Add(this.btnCoilScheduleAdj);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(42, 20);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(313, 398);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "發送測試";
            // 
            // btnAskPDI
            // 
            this.btnAskPDI.Location = new System.Drawing.Point(41, 281);
            this.btnAskPDI.Name = "btnAskPDI";
            this.btnAskPDI.Size = new System.Drawing.Size(227, 23);
            this.btnAskPDI.TabIndex = 12;
            this.btnAskPDI.Text = "要求三級下發PDI";
            this.btnAskPDI.UseVisualStyleBackColor = true;
            this.btnAskPDI.Click += new System.EventHandler(this.btnAskPDI_Click);
            // 
            // btnAskMMSSchedule
            // 
            this.btnAskMMSSchedule.Location = new System.Drawing.Point(41, 252);
            this.btnAskMMSSchedule.Name = "btnAskMMSSchedule";
            this.btnAskMMSSchedule.Size = new System.Drawing.Size(227, 23);
            this.btnAskMMSSchedule.TabIndex = 11;
            this.btnAskMMSSchedule.Text = "要求三級下發排程";
            this.btnAskMMSSchedule.UseVisualStyleBackColor = true;
            this.btnAskMMSSchedule.Click += new System.EventHandler(this.btnAskMMSSchedule_Click);
            // 
            // btnScnFailSelect
            // 
            this.btnScnFailSelect.Location = new System.Drawing.Point(41, 223);
            this.btnScnFailSelect.Name = "btnScnFailSelect";
            this.btnScnFailSelect.Size = new System.Drawing.Size(227, 23);
            this.btnScnFailSelect.TabIndex = 10;
            this.btnScnFailSelect.Text = " 入口段鋼捲ID更正";
            this.btnScnFailSelect.UseVisualStyleBackColor = true;
            this.btnScnFailSelect.Click += new System.EventHandler(this.btnScnFailSelect_Click);
            // 
            // btnPDOSnd
            // 
            this.btnPDOSnd.Location = new System.Drawing.Point(41, 194);
            this.btnPDOSnd.Name = "btnPDOSnd";
            this.btnPDOSnd.Size = new System.Drawing.Size(227, 23);
            this.btnPDOSnd.TabIndex = 9;
            this.btnPDOSnd.Text = "傳送要求PDO上傳";
            this.btnPDOSnd.UseVisualStyleBackColor = true;
            this.btnPDOSnd.Click += new System.EventHandler(this.btnPDOSnd_Click);
            // 
            // btnPrintCheck
            // 
            this.btnPrintCheck.Location = new System.Drawing.Point(41, 332);
            this.btnPrintCheck.Name = "btnPrintCheck";
            this.btnPrintCheck.Size = new System.Drawing.Size(227, 23);
            this.btnPrintCheck.TabIndex = 8;
            this.btnPrintCheck.Text = "手動列印確認";
            this.btnPrintCheck.UseVisualStyleBackColor = true;
            this.btnPrintCheck.Click += new System.EventHandler(this.btnPrintCheck_Click);
            // 
            // btnDefectCoilSnd
            // 
            this.btnDefectCoilSnd.Location = new System.Drawing.Point(41, 165);
            this.btnDefectCoilSnd.Name = "btnDefectCoilSnd";
            this.btnDefectCoilSnd.Size = new System.Drawing.Size(227, 23);
            this.btnDefectCoilSnd.TabIndex = 6;
            this.btnDefectCoilSnd.Text = "缺陷維護(封鎖標記)";
            this.btnDefectCoilSnd.UseVisualStyleBackColor = true;
            this.btnDefectCoilSnd.Click += new System.EventHandler(this.btnDefectCoilSnd_Click);
            // 
            // btnReturnCoil
            // 
            this.btnReturnCoil.Location = new System.Drawing.Point(41, 136);
            this.btnReturnCoil.Name = "btnReturnCoil";
            this.btnReturnCoil.Size = new System.Drawing.Size(227, 23);
            this.btnReturnCoil.TabIndex = 5;
            this.btnReturnCoil.Text = "鋼捲退回(退料)";
            this.btnReturnCoil.UseVisualStyleBackColor = true;
            this.btnReturnCoil.Click += new System.EventHandler(this.btnReturnCoil_Click);
            // 
            // btnCoilEnterEnd
            // 
            this.btnCoilEnterEnd.Location = new System.Drawing.Point(41, 107);
            this.btnCoilEnterEnd.Name = "btnCoilEnterEnd";
            this.btnCoilEnterEnd.Size = new System.Drawing.Size(227, 23);
            this.btnCoilEnterEnd.TabIndex = 4;
            this.btnCoilEnterEnd.Text = "停止供料";
            this.btnCoilEnterEnd.UseVisualStyleBackColor = true;
            this.btnCoilEnterEnd.Click += new System.EventHandler(this.btnCoilEnterEnd_Click);
            // 
            // btnCoilEnterStar
            // 
            this.btnCoilEnterStar.Location = new System.Drawing.Point(41, 78);
            this.btnCoilEnterStar.Name = "btnCoilEnterStar";
            this.btnCoilEnterStar.Size = new System.Drawing.Size(227, 23);
            this.btnCoilEnterStar.TabIndex = 3;
            this.btnCoilEnterStar.Text = "開始供料";
            this.btnCoilEnterStar.UseVisualStyleBackColor = true;
            this.btnCoilEnterStar.Click += new System.EventHandler(this.btnCoilEnterStar_Click);
            // 
            // btnCoilDelete
            // 
            this.btnCoilDelete.Location = new System.Drawing.Point(41, 49);
            this.btnCoilDelete.Name = "btnCoilDelete";
            this.btnCoilDelete.Size = new System.Drawing.Size(227, 23);
            this.btnCoilDelete.TabIndex = 2;
            this.btnCoilDelete.Text = "鋼捲刪除訊號";
            this.btnCoilDelete.UseVisualStyleBackColor = true;
            this.btnCoilDelete.Click += new System.EventHandler(this.btnCoilDelete_Click);
            // 
            // btnCoilScheduleAdj
            // 
            this.btnCoilScheduleAdj.Location = new System.Drawing.Point(41, 20);
            this.btnCoilScheduleAdj.Name = "btnCoilScheduleAdj";
            this.btnCoilScheduleAdj.Size = new System.Drawing.Size(227, 23);
            this.btnCoilScheduleAdj.TabIndex = 1;
            this.btnCoilScheduleAdj.Text = "鋼捲順序調整訊號";
            this.btnCoilScheduleAdj.UseVisualStyleBackColor = true;
            this.btnCoilScheduleAdj.Click += new System.EventHandler(this.btnCoilScheduleAdj_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(516, 442);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "測試";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(516, 442);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "掃描";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeliveryScn);
            this.groupBox1.Controls.Add(this.DeliveryComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDeliveryCoilNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(58, 180);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(281, 112);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BarCode Scanner出口端";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEntryScan);
            this.groupBox3.Controls.Add(this.EntryComoBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtEntryCoilNo);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(58, 46);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(281, 112);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BarCode Scanner入口端";
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
            this.txtEntryCoilNo.Text = "HE1807002900";
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
            // msgGroupBox
            // 
            this.msgGroupBox.Controls.Add(this.RichTextBoxConsole);
            this.msgGroupBox.Location = new System.Drawing.Point(6, 6);
            this.msgGroupBox.Name = "msgGroupBox";
            this.msgGroupBox.Size = new System.Drawing.Size(504, 430);
            this.msgGroupBox.TabIndex = 46;
            this.msgGroupBox.TabStop = false;
            this.msgGroupBox.Text = "訊息";
            // 
            // ClientCoordinatorForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 506);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientCoordinatorForm1";
            this.Text = "ClientCoordinator";
            this.Load += new System.EventHandler(this.ClientCoordinatorForm1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.msgGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCoilScheduleAdj;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCoilDelete;
        private System.Windows.Forms.Button btnCoilEnterStar;
        private System.Windows.Forms.Button btnCoilEnterEnd;
        private System.Windows.Forms.Button btnReturnCoil;
        private System.Windows.Forms.Button btnDefectCoilSnd;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeliveryScn;
        private System.Windows.Forms.ComboBox DeliveryComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeliveryCoilNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEntryScan;
        private System.Windows.Forms.ComboBox EntryComoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEntryCoilNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPrintCheck;
        private System.Windows.Forms.Button btnPDOSnd;
        private System.Windows.Forms.Button btnScnFailSelect;
        private System.Windows.Forms.Button btnAskMMSSchedule;
        private System.Windows.Forms.Button btnAskPDI;
        private System.Windows.Forms.RichTextBox RichTextBoxConsole;
        private System.Windows.Forms.GroupBox msgGroupBox;
    }
}

