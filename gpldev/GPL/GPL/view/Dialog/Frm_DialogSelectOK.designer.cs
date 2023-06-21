namespace GPLManager
{
    partial class Frm_DialogSelectOK
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
            this.Btn_Yes = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_No = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Type)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Broder_Top
            // 
            this.Pnl_Broder_Top.BackColor = System.Drawing.Color.Gold;
            // 
            // Pnl_Broder_Left
            // 
            this.Pnl_Broder_Left.BackColor = System.Drawing.Color.Gold;
            // 
            // Pnl_Broder_Right
            // 
            this.Pnl_Broder_Right.BackColor = System.Drawing.Color.Gold;
            // 
            // Pnl_Broder_Bottom
            // 
            this.Pnl_Broder_Bottom.BackColor = System.Drawing.Color.Gold;
            // 
            // Pnl_Border_Two
            // 
            this.Pnl_Border_Two.BackColor = System.Drawing.Color.Gold;
            // 
            // Pnl_Border_One
            // 
            this.Pnl_Border_One.BackColor = System.Drawing.Color.Gold;
            // 
            // Txt_ShowMessage
            // 
            this.Txt_ShowMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // Btn_Yes
            // 
            this.Btn_Yes.BackColor = System.Drawing.Color.LightGreen;
            this.Btn_Yes.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Yes.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Yes.Location = new System.Drawing.Point(51, 247);
            this.Btn_Yes.Name = "Btn_Yes";
            this.Btn_Yes.Size = new System.Drawing.Size(100, 40);
            this.Btn_Yes.TabIndex = 5;
            this.Btn_Yes.Text = "是";
            this.Btn_Yes.UseVisualStyleBackColor = false;
            this.Btn_Yes.Click += new System.EventHandler(this.Btn_Yes_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.LightPink;
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(299, 247);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 40);
            this.Btn_Cancel.TabIndex = 6;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_No
            // 
            this.Btn_No.BackColor = System.Drawing.Color.SteelBlue;
            this.Btn_No.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_No.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_No.Location = new System.Drawing.Point(175, 247);
            this.Btn_No.Name = "Btn_No";
            this.Btn_No.Size = new System.Drawing.Size(100, 40);
            this.Btn_No.TabIndex = 7;
            this.Btn_No.Text = "否";
            this.Btn_No.UseVisualStyleBackColor = false;
            this.Btn_No.Click += new System.EventHandler(this.Btn_No_Click);
            // 
            // Frm_DialogSelectOK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.Btn_No);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Yes);
            this.Name = "Frm_DialogSelectOK";
            this.Text = "Frm_DialogOKCancel";
            this.Controls.SetChildIndex(this.Pnl_Broder_Top, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Right, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Bottom, 0);
            this.Controls.SetChildIndex(this.Pnl_Broder_Left, 0);
            this.Controls.SetChildIndex(this.Pnl_Border_One, 0);
            this.Controls.SetChildIndex(this.Pnl_Border_Two, 0);
            this.Controls.SetChildIndex(this.Lbl_Tile, 0);
            this.Controls.SetChildIndex(this.Pic_Type, 0);
            this.Controls.SetChildIndex(this.Txt_ShowMessage, 0);
            this.Controls.SetChildIndex(this.Btn_Yes, 0);
            this.Controls.SetChildIndex(this.Btn_Cancel, 0);
            this.Controls.SetChildIndex(this.Btn_No, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Type)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Yes;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_No;
    }
}