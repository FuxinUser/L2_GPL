namespace GPLManager
{
    partial class frm_Scan
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
            this.btn_Scan_ok = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.chk_Scan_CoilID = new System.Windows.Forms.CheckBox();
            this.chk_PDI_CoilID = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_Scan_ok
            // 
            this.btn_Scan_ok.Location = new System.Drawing.Point(312, 106);
            this.btn_Scan_ok.Name = "btn_Scan_ok";
            this.btn_Scan_ok.Size = new System.Drawing.Size(75, 34);
            this.btn_Scan_ok.TabIndex = 35;
            this.btn_Scan_ok.Text = "确认";
            this.btn_Scan_ok.UseVisualStyleBackColor = true;
            this.btn_Scan_ok.Click += new System.EventHandler(this.btn_Scan_ok_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.label32.Location = new System.Drawing.Point(12, 72);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(124, 24);
            this.label32.TabIndex = 34;
            this.label32.Text = "扫描钢卷编号";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(12, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(124, 24);
            this.label28.TabIndex = 33;
            this.label28.Text = "鞍座钢卷编号";
            // 
            // chk_Scan_CoilID
            // 
            this.chk_Scan_CoilID.AutoSize = true;
            this.chk_Scan_CoilID.Location = new System.Drawing.Point(142, 78);
            this.chk_Scan_CoilID.Name = "chk_Scan_CoilID";
            this.chk_Scan_CoilID.Size = new System.Drawing.Size(15, 14);
            this.chk_Scan_CoilID.TabIndex = 32;
            this.chk_Scan_CoilID.UseVisualStyleBackColor = true;
            this.chk_Scan_CoilID.CheckedChanged += new System.EventHandler(this.chk_Scan_CoilID_CheckedChanged);
            // 
            // chk_PDI_CoilID
            // 
            this.chk_PDI_CoilID.AutoSize = true;
            this.chk_PDI_CoilID.Location = new System.Drawing.Point(142, 26);
            this.chk_PDI_CoilID.Name = "chk_PDI_CoilID";
            this.chk_PDI_CoilID.Size = new System.Drawing.Size(15, 14);
            this.chk_PDI_CoilID.TabIndex = 31;
            this.chk_PDI_CoilID.UseVisualStyleBackColor = true;
            this.chk_PDI_CoilID.CheckedChanged += new System.EventHandler(this.chk_PDI_CoilID_CheckedChanged);
            // 
            // frm_Scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 152);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.btn_Scan_ok);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.chk_Scan_CoilID);
            this.Controls.Add(this.chk_PDI_CoilID);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frm_Scan";
            this.Text = "扫描结果";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Scan_ok;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label28;
        internal System.Windows.Forms.CheckBox chk_Scan_CoilID;
        internal System.Windows.Forms.CheckBox chk_PDI_CoilID;
    }
}