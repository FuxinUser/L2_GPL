namespace GPLManager
{
    partial class Frm_RequestPdi
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
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Lbl_Title1 = new System.Windows.Forms.Label();
            this.Txt_CoilNo = new System.Windows.Forms.TextBox();
            this.Lbl_Title2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Type)).BeginInit();
            this.SuspendLayout();
            // 
            // Pic_Type
            // 
            this.Pic_Type.BackgroundImage = global::GPLManager.Properties.Resources.dialogQuestion;
            // 
            // Pnl_Broder_Top
            // 
            this.Pnl_Broder_Top.BackColor = System.Drawing.Color.DarkOrange;
            this.Pnl_Broder_Top.Size = new System.Drawing.Size(444, 5);
            // 
            // Pnl_Broder_Left
            // 
            this.Pnl_Broder_Left.BackColor = System.Drawing.Color.DarkOrange;
            this.Pnl_Broder_Left.Size = new System.Drawing.Size(5, 296);
            // 
            // Pnl_Broder_Right
            // 
            this.Pnl_Broder_Right.BackColor = System.Drawing.Color.DarkOrange;
            this.Pnl_Broder_Right.Location = new System.Drawing.Point(439, 5);
            this.Pnl_Broder_Right.Size = new System.Drawing.Size(5, 301);
            // 
            // Pnl_Broder_Bottom
            // 
            this.Pnl_Broder_Bottom.BackColor = System.Drawing.Color.DarkOrange;
            this.Pnl_Broder_Bottom.Location = new System.Drawing.Point(0, 301);
            this.Pnl_Broder_Bottom.Size = new System.Drawing.Size(439, 5);
            // 
            // Txt_ShowMessage
            // 
            this.Txt_ShowMessage.BackColor = System.Drawing.Color.Bisque;
            this.Txt_ShowMessage.Text = "";
            // 
            // Btn_OK
            // 
            this.Btn_OK.BackColor = System.Drawing.Color.LightGreen;
            this.Btn_OK.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_OK.Location = new System.Drawing.Point(94, 250);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(100, 40);
            this.Btn_OK.TabIndex = 2;
            this.Btn_OK.Text = "確定";
            this.Btn_OK.UseVisualStyleBackColor = false;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.LightPink;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(250, 250);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 40);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Lbl_Title1
            // 
            this.Lbl_Title1.AutoSize = true;
            this.Lbl_Title1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Title1.Location = new System.Drawing.Point(88, 79);
            this.Lbl_Title1.Name = "Lbl_Title1";
            this.Lbl_Title1.Size = new System.Drawing.Size(81, 24);
            this.Lbl_Title1.TabIndex = 7;
            this.Lbl_Title1.Text = "要求PDI";
            // 
            // Txt_CoilNo
            // 
            this.Txt_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_CoilNo.Location = new System.Drawing.Point(88, 143);
            this.Txt_CoilNo.Name = "Txt_CoilNo";
            this.Txt_CoilNo.Size = new System.Drawing.Size(227, 33);
            this.Txt_CoilNo.TabIndex = 6;
            // 
            // Lbl_Title2
            // 
            this.Lbl_Title2.AutoSize = true;
            this.Lbl_Title2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Title2.Location = new System.Drawing.Point(88, 111);
            this.Lbl_Title2.Name = "Lbl_Title2";
            this.Lbl_Title2.Size = new System.Drawing.Size(124, 24);
            this.Lbl_Title2.TabIndex = 5;
            this.Lbl_Title2.Text = "请输入钢卷号";
            // 
            // Frm_RequestPdi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(444, 306);
            this.ControlBox = false;
            this.Controls.Add(this.Lbl_Title1);
            this.Controls.Add(this.Txt_CoilNo);
            this.Controls.Add(this.Lbl_Title2);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "Frm_RequestPdi";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_Z_RequestPdi_Load);
            this.Controls.SetChildIndex(this.Btn_OK, 0);
            this.Controls.SetChildIndex(this.Btn_Cancel, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Top, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Right, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Bottom, 0);
            this.Controls.SetChildIndex(this.Lbl_Tile, 0);
            this.Controls.SetChildIndex(this.Pic_Type, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Left, 0);
            this.Controls.SetChildIndex(this.Txt_ShowMessage, 0);
            this.Controls.SetChildIndex(this.Pnl_Border_One, 0);
            this.Controls.SetChildIndex(this.Pnl_Border_Two, 0);
            this.Controls.SetChildIndex(this.Lbl_Title2, 0);
            this.Controls.SetChildIndex(this.Txt_CoilNo, 0);
            this.Controls.SetChildIndex(this.Lbl_Title1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Type)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Label Lbl_Title1;
        private System.Windows.Forms.TextBox Txt_CoilNo;
        private System.Windows.Forms.Label Lbl_Title2;
    }
}