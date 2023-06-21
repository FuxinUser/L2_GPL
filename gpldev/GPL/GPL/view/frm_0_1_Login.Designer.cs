namespace GPLManager
{
    partial class Frm_0_1_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_0_1_Login));
            this.Grb_LoginArea = new System.Windows.Forms.GroupBox();
            this.Lbl_SeparationLine = new System.Windows.Forms.Label();
            this.Lbl_SystemName = new System.Windows.Forms.Label();
            this.Pic_Company = new System.Windows.Forms.PictureBox();
            this.Cob_User = new System.Windows.Forms.ComboBox();
            this.Pic_UserPassword = new System.Windows.Forms.PictureBox();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.Btn_Login = new System.Windows.Forms.Button();
            this.Grb_LoginArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_UserPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // Grb_LoginArea
            // 
            this.Grb_LoginArea.BackColor = System.Drawing.Color.Silver;
            this.Grb_LoginArea.Controls.Add(this.Lbl_SeparationLine);
            this.Grb_LoginArea.Controls.Add(this.Lbl_SystemName);
            this.Grb_LoginArea.Controls.Add(this.Pic_Company);
            this.Grb_LoginArea.Controls.Add(this.Cob_User);
            this.Grb_LoginArea.Controls.Add(this.Pic_UserPassword);
            this.Grb_LoginArea.Controls.Add(this.Txt_Password);
            this.Grb_LoginArea.Controls.Add(this.Btn_Login);
            this.Grb_LoginArea.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_LoginArea.ForeColor = System.Drawing.Color.Black;
            this.Grb_LoginArea.Location = new System.Drawing.Point(707, 276);
            this.Grb_LoginArea.Name = "Grb_LoginArea";
            this.Grb_LoginArea.Size = new System.Drawing.Size(497, 419);
            this.Grb_LoginArea.TabIndex = 12;
            this.Grb_LoginArea.TabStop = false;
            // 
            // Lbl_SeparationLine
            // 
            this.Lbl_SeparationLine.BackColor = System.Drawing.Color.Fuchsia;
            this.Lbl_SeparationLine.Location = new System.Drawing.Point(7, 331);
            this.Lbl_SeparationLine.Name = "Lbl_SeparationLine";
            this.Lbl_SeparationLine.Size = new System.Drawing.Size(487, 10);
            this.Lbl_SeparationLine.TabIndex = 19;
            // 
            // Lbl_SystemName
            // 
            this.Lbl_SystemName.BackColor = System.Drawing.Color.Blue;
            this.Lbl_SystemName.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_SystemName.ForeColor = System.Drawing.Color.White;
            this.Lbl_SystemName.Location = new System.Drawing.Point(23, 98);
            this.Lbl_SystemName.Name = "Lbl_SystemName";
            this.Lbl_SystemName.Size = new System.Drawing.Size(449, 37);
            this.Lbl_SystemName.TabIndex = 18;
            this.Lbl_SystemName.Text = "GPL程控电脑系统登入作业";
            this.Lbl_SystemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pic_Company
            // 
            this.Pic_Company.Image = global::GPLManager.Properties.Resources.fuxinLogo;
            this.Pic_Company.Location = new System.Drawing.Point(23, 14);
            this.Pic_Company.Name = "Pic_Company";
            this.Pic_Company.Size = new System.Drawing.Size(449, 81);
            this.Pic_Company.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_Company.TabIndex = 13;
            this.Pic_Company.TabStop = false;
            // 
            // Cob_User
            // 
            this.Cob_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cob_User.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_User.Items.AddRange(new object[] {
            "GPL"});
            this.Cob_User.Location = new System.Drawing.Point(204, 149);
            this.Cob_User.Name = "Cob_User";
            this.Cob_User.Size = new System.Drawing.Size(220, 43);
            this.Cob_User.TabIndex = 3;
            this.Cob_User.DropDownClosed += new System.EventHandler(this.Cob_User_DropDownClosed);
            // 
            // Pic_UserPassword
            // 
            this.Pic_UserPassword.Image = global::GPLManager.Properties.Resources.img_passwd_layout;
            this.Pic_UserPassword.Location = new System.Drawing.Point(33, 149);
            this.Pic_UserPassword.Name = "Pic_UserPassword";
            this.Pic_UserPassword.Size = new System.Drawing.Size(165, 98);
            this.Pic_UserPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_UserPassword.TabIndex = 12;
            this.Pic_UserPassword.TabStop = false;
            this.Pic_UserPassword.DoubleClick += new System.EventHandler(this.Pic_UserPassword_DoubleClick);
            // 
            // Txt_Password
            // 
            this.Txt_Password.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_Password.Location = new System.Drawing.Point(204, 204);
            this.Txt_Password.MaxLength = 8;
            this.Txt_Password.Name = "Txt_Password";
            this.Txt_Password.PasswordChar = '*';
            this.Txt_Password.Size = new System.Drawing.Size(220, 43);
            this.Txt_Password.TabIndex = 2;
            // 
            // Btn_Login
            // 
            this.Btn_Login.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Login.BackgroundImage = global::GPLManager.Properties.Resources.img_login_btn;
            this.Btn_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Login.FlatAppearance.BorderSize = 0;
            this.Btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Login.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Login.ForeColor = System.Drawing.Color.Black;
            this.Btn_Login.Location = new System.Drawing.Point(204, 253);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(220, 70);
            this.Btn_Login.TabIndex = 1;
            this.Btn_Login.Text = "   登  入";
            this.Btn_Login.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Login.UseVisualStyleBackColor = false;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // Frm_0_1_Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1670, 940);
            this.Controls.Add(this.Grb_LoginArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_0_1_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm_0_1_Login";
            this.Shown += new System.EventHandler(this.Frm_0_1_Login_Shown);
            this.Grb_LoginArea.ResumeLayout(false);
            this.Grb_LoginArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_UserPassword)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox Grb_LoginArea;
        internal System.Windows.Forms.Label Lbl_SeparationLine;
        internal System.Windows.Forms.Label Lbl_SystemName;
        internal System.Windows.Forms.PictureBox Pic_Company;
        internal System.Windows.Forms.ComboBox Cob_User;
        internal System.Windows.Forms.PictureBox Pic_UserPassword;
        internal System.Windows.Forms.TextBox Txt_Password;
        internal System.Windows.Forms.Button Btn_Login;
    }
}