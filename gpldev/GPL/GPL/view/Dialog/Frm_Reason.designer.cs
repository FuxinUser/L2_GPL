namespace GPLManager
{
    partial class Frm_Reason
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
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Lbl_Title2 = new System.Windows.Forms.Label();
            this.Cob_Reason = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Type)).BeginInit();
            this.SuspendLayout();
            // 
            // Pic_Type
            // 
            this.Pic_Type.BackgroundImage = global::GPLManager.Properties.Resources.dialogQuestion;
            // 
            // Pnl_Broder_Top
            // 
            this.Pnl_Broder_Top.BackColor = System.Drawing.Color.Gold;
            this.Pnl_Broder_Top.Size = new System.Drawing.Size(444, 5);
            // 
            // Pnl_Broder_Left
            // 
            this.Pnl_Broder_Left.BackColor = System.Drawing.Color.Gold;
            this.Pnl_Broder_Left.Size = new System.Drawing.Size(5, 296);
            // 
            // Pnl_Broder_Right
            // 
            this.Pnl_Broder_Right.BackColor = System.Drawing.Color.Gold;
            this.Pnl_Broder_Right.Location = new System.Drawing.Point(439, 5);
            this.Pnl_Broder_Right.Size = new System.Drawing.Size(5, 301);
            // 
            // Pnl_Broder_Bottom
            // 
            this.Pnl_Broder_Bottom.BackColor = System.Drawing.Color.Gold;
            this.Pnl_Broder_Bottom.Location = new System.Drawing.Point(0, 301);
            this.Pnl_Broder_Bottom.Size = new System.Drawing.Size(439, 5);
            // 
            // Txt_ShowMessage
            // 
            this.Txt_ShowMessage.BackColor = System.Drawing.Color.LemonChiffon;
            this.Txt_ShowMessage.Size = new System.Drawing.Size(376, 36);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.LightPink;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(250, 250);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 40);
            this.Btn_Cancel.TabIndex = 8;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_OK
            // 
            this.Btn_OK.BackColor = System.Drawing.Color.LightGreen;
            this.Btn_OK.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_OK.Location = new System.Drawing.Point(94, 250);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(100, 40);
            this.Btn_OK.TabIndex = 7;
            this.Btn_OK.Text = "确定";
            this.Btn_OK.UseVisualStyleBackColor = false;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Lbl_Title2
            // 
            this.Lbl_Title2.AutoSize = true;
            this.Lbl_Title2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Title2.Location = new System.Drawing.Point(37, 115);
            this.Lbl_Title2.Name = "Lbl_Title2";
            this.Lbl_Title2.Size = new System.Drawing.Size(73, 20);
            this.Lbl_Title2.TabIndex = 5;
            this.Lbl_Title2.Text = "刪除原因";
            // 
            // Cob_Reason
            // 
            this.Cob_Reason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cob_Reason.FormattingEnabled = true;
            this.Cob_Reason.IntegralHeight = false;
            this.Cob_Reason.Location = new System.Drawing.Point(37, 151);
            this.Cob_Reason.Name = "Cob_Reason";
            this.Cob_Reason.Size = new System.Drawing.Size(376, 34);
            this.Cob_Reason.TabIndex = 10;
            // 
            // Frm_Reason
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(444, 306);
            this.Controls.Add(this.Lbl_Title2);
            this.Controls.Add(this.Cob_Reason);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "Frm_Reason";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "刪除原因";
            this.Load += new System.EventHandler(this.Frm_Reason_Load);
            this.Controls.SetChildIndex(this.Btn_OK, 0);
            this.Controls.SetChildIndex(this.Btn_Cancel, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Top, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Right, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Bottom, 0);
            this.Controls.SetChildIndex(this.Pic_Type, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Left, 0);
            this.Controls.SetChildIndex(this.Txt_ShowMessage, 0);
            this.Controls.SetChildIndex(this.Pnl_Border_One, 0);
            this.Controls.SetChildIndex(this.Pnl_Border_Two, 0);
            this.Controls.SetChildIndex(this.Lbl_Tile, 0);
            this.Controls.SetChildIndex(this.Cob_Reason, 0);
            this.Controls.SetChildIndex(this.Lbl_Title2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Type)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Label Lbl_Title2;
        private System.Windows.Forms.ComboBox Cob_Reason;
    }
}