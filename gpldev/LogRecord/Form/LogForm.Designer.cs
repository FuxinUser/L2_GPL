namespace LogRecord.Form
{
    partial class LogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.logConsoleRText = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbHMI = new System.Windows.Forms.RichTextBox();
            this.btnClearConsole = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.checkBoxDisDebugInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxIsClearConsole = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // logConsoleRText
            // 
            this.logConsoleRText.BackColor = System.Drawing.Color.Black;
            this.logConsoleRText.Location = new System.Drawing.Point(18, 6);
            this.logConsoleRText.Name = "logConsoleRText";
            this.logConsoleRText.Size = new System.Drawing.Size(1039, 580);
            this.logConsoleRText.TabIndex = 5;
            this.logConsoleRText.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(31, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1088, 633);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.logConsoleRText);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1080, 607);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbHMI);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1080, 607);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HMI";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbHMI
            // 
            this.rtbHMI.BackColor = System.Drawing.Color.Black;
            this.rtbHMI.Location = new System.Drawing.Point(19, 6);
            this.rtbHMI.Name = "rtbHMI";
            this.rtbHMI.Size = new System.Drawing.Size(1039, 580);
            this.rtbHMI.TabIndex = 6;
            this.rtbHMI.Text = "";
            // 
            // btnClearConsole
            // 
            this.btnClearConsole.Location = new System.Drawing.Point(1125, 34);
            this.btnClearConsole.Name = "btnClearConsole";
            this.btnClearConsole.Size = new System.Drawing.Size(124, 45);
            this.btnClearConsole.TabIndex = 7;
            this.btnClearConsole.Text = "清除Console";
            this.btnClearConsole.UseVisualStyleBackColor = true;
            this.btnClearConsole.Click += new System.EventHandler(this.btnClearConsole_Click);
            // 
            // checkBoxDisDebugInfo
            // 
            this.checkBoxDisDebugInfo.AutoSize = true;
            this.checkBoxDisDebugInfo.Location = new System.Drawing.Point(1125, 95);
            this.checkBoxDisDebugInfo.Name = "checkBoxDisDebugInfo";
            this.checkBoxDisDebugInfo.Size = new System.Drawing.Size(103, 16);
            this.checkBoxDisDebugInfo.TabIndex = 8;
            this.checkBoxDisDebugInfo.Text = "顯示Debug資訊";
            this.checkBoxDisDebugInfo.UseVisualStyleBackColor = true;
            this.checkBoxDisDebugInfo.CheckedChanged += new System.EventHandler(this.checkBoxDisDebugInfo_CheckedChanged);
            // 
            // checkBoxIsClearConsole
            // 
            this.checkBoxIsClearConsole.AutoSize = true;
            this.checkBoxIsClearConsole.Location = new System.Drawing.Point(1125, 117);
            this.checkBoxIsClearConsole.Name = "checkBoxIsClearConsole";
            this.checkBoxIsClearConsole.Size = new System.Drawing.Size(110, 16);
            this.checkBoxIsClearConsole.TabIndex = 9;
            this.checkBoxIsClearConsole.Text = "自動清除Console";
            this.checkBoxIsClearConsole.UseVisualStyleBackColor = true;
            this.checkBoxIsClearConsole.CheckedChanged += new System.EventHandler(this.checkBoxIsClearConsole_CheckedChanged);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1310, 647);
            this.Controls.Add(this.checkBoxIsClearConsole);
            this.Controls.Add(this.checkBoxDisDebugInfo);
            this.Controls.Add(this.btnClearConsole);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LogForm";
            this.Text = "Log";
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logConsoleRText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbHMI;
        private System.Windows.Forms.Button btnClearConsole;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox checkBoxDisDebugInfo;
        private System.Windows.Forms.CheckBox checkBoxIsClearConsole;
    }
}

