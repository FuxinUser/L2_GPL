
namespace GPLManager
{
    partial class Frm_SelectDataOpen
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
            this.Pnl_SelectPdo = new System.Windows.Forms.Panel();
            this.Btn_SelectPdo_Cancel = new System.Windows.Forms.Button();
            this.Btn_SelectPdo_OK = new System.Windows.Forms.Button();
            this.Lbl_SelectPdo_Title = new System.Windows.Forms.Label();
            this.Btn_SelectPdo_Close = new System.Windows.Forms.Button();
            this.Pnl_Show_Bottom = new System.Windows.Forms.Panel();
            this.Pnl_Show_Top = new System.Windows.Forms.Panel();
            this.Pnl_Show_Right = new System.Windows.Forms.Panel();
            this.Pnl_Show_Left = new System.Windows.Forms.Panel();
            this.Dgv_SelectPdo = new System.Windows.Forms.DataGridView();
            this.Pnl_SelectPdo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SelectPdo)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_SelectPdo
            // 
            this.Pnl_SelectPdo.BackColor = System.Drawing.Color.PowderBlue;
            this.Pnl_SelectPdo.Controls.Add(this.Btn_SelectPdo_Cancel);
            this.Pnl_SelectPdo.Controls.Add(this.Btn_SelectPdo_OK);
            this.Pnl_SelectPdo.Controls.Add(this.Lbl_SelectPdo_Title);
            this.Pnl_SelectPdo.Controls.Add(this.Btn_SelectPdo_Close);
            this.Pnl_SelectPdo.Controls.Add(this.Pnl_Show_Bottom);
            this.Pnl_SelectPdo.Controls.Add(this.Pnl_Show_Top);
            this.Pnl_SelectPdo.Controls.Add(this.Pnl_Show_Right);
            this.Pnl_SelectPdo.Controls.Add(this.Pnl_Show_Left);
            this.Pnl_SelectPdo.Controls.Add(this.Dgv_SelectPdo);
            this.Pnl_SelectPdo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_SelectPdo.Location = new System.Drawing.Point(0, 0);
            this.Pnl_SelectPdo.Name = "Pnl_SelectPdo";
            this.Pnl_SelectPdo.Size = new System.Drawing.Size(1200, 400);
            this.Pnl_SelectPdo.TabIndex = 1777;
            // 
            // Btn_SelectPdo_Cancel
            // 
            this.Btn_SelectPdo_Cancel.BackColor = System.Drawing.Color.LightPink;
            this.Btn_SelectPdo_Cancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_SelectPdo_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_SelectPdo_Cancel.Location = new System.Drawing.Point(632, 341);
            this.Btn_SelectPdo_Cancel.Name = "Btn_SelectPdo_Cancel";
            this.Btn_SelectPdo_Cancel.Size = new System.Drawing.Size(100, 40);
            this.Btn_SelectPdo_Cancel.TabIndex = 1783;
            this.Btn_SelectPdo_Cancel.Text = "取消";
            this.Btn_SelectPdo_Cancel.UseVisualStyleBackColor = false;
            this.Btn_SelectPdo_Cancel.Click += new System.EventHandler(this.Btn_SelectPdo_Cancel_Click);
            // 
            // Btn_SelectPdo_OK
            // 
            this.Btn_SelectPdo_OK.BackColor = System.Drawing.Color.LightGreen;
            this.Btn_SelectPdo_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_SelectPdo_OK.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_SelectPdo_OK.Location = new System.Drawing.Point(468, 341);
            this.Btn_SelectPdo_OK.Name = "Btn_SelectPdo_OK";
            this.Btn_SelectPdo_OK.Size = new System.Drawing.Size(100, 40);
            this.Btn_SelectPdo_OK.TabIndex = 1782;
            this.Btn_SelectPdo_OK.Text = "确定";
            this.Btn_SelectPdo_OK.UseVisualStyleBackColor = false;
            this.Btn_SelectPdo_OK.Click += new System.EventHandler(this.Btn_SelectPdo_OK_Click);
            // 
            // Lbl_SelectPdo_Title
            // 
            this.Lbl_SelectPdo_Title.AutoSize = true;
            this.Lbl_SelectPdo_Title.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_SelectPdo_Title.Location = new System.Drawing.Point(461, 21);
            this.Lbl_SelectPdo_Title.Name = "Lbl_SelectPdo_Title";
            this.Lbl_SelectPdo_Title.Size = new System.Drawing.Size(278, 31);
            this.Lbl_SelectPdo_Title.TabIndex = 1781;
            this.Lbl_SelectPdo_Title.Text = "请选择要开启的详细资料";
            // 
            // Btn_SelectPdo_Close
            // 
            this.Btn_SelectPdo_Close.BackColor = System.Drawing.Color.OrangeRed;
            this.Btn_SelectPdo_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_SelectPdo_Close.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_SelectPdo_Close.ForeColor = System.Drawing.Color.Black;
            this.Btn_SelectPdo_Close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn_SelectPdo_Close.Location = new System.Drawing.Point(1141, 14);
            this.Btn_SelectPdo_Close.Name = "Btn_SelectPdo_Close";
            this.Btn_SelectPdo_Close.Size = new System.Drawing.Size(45, 45);
            this.Btn_SelectPdo_Close.TabIndex = 1780;
            this.Btn_SelectPdo_Close.Text = "X";
            this.Btn_SelectPdo_Close.UseVisualStyleBackColor = false;
            this.Btn_SelectPdo_Close.Click += new System.EventHandler(this.Btn_SelectPdo_Close_Click);
            // 
            // Pnl_Show_Bottom
            // 
            this.Pnl_Show_Bottom.BackColor = System.Drawing.Color.RoyalBlue;
            this.Pnl_Show_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Pnl_Show_Bottom.Location = new System.Drawing.Point(8, 392);
            this.Pnl_Show_Bottom.Name = "Pnl_Show_Bottom";
            this.Pnl_Show_Bottom.Size = new System.Drawing.Size(1184, 8);
            this.Pnl_Show_Bottom.TabIndex = 1779;
            // 
            // Pnl_Show_Top
            // 
            this.Pnl_Show_Top.BackColor = System.Drawing.Color.RoyalBlue;
            this.Pnl_Show_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pnl_Show_Top.Location = new System.Drawing.Point(8, 0);
            this.Pnl_Show_Top.Name = "Pnl_Show_Top";
            this.Pnl_Show_Top.Size = new System.Drawing.Size(1184, 8);
            this.Pnl_Show_Top.TabIndex = 1776;
            // 
            // Pnl_Show_Right
            // 
            this.Pnl_Show_Right.BackColor = System.Drawing.Color.RoyalBlue;
            this.Pnl_Show_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pnl_Show_Right.Location = new System.Drawing.Point(1192, 0);
            this.Pnl_Show_Right.Name = "Pnl_Show_Right";
            this.Pnl_Show_Right.Size = new System.Drawing.Size(8, 400);
            this.Pnl_Show_Right.TabIndex = 1777;
            // 
            // Pnl_Show_Left
            // 
            this.Pnl_Show_Left.BackColor = System.Drawing.Color.RoyalBlue;
            this.Pnl_Show_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.Pnl_Show_Left.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Show_Left.Name = "Pnl_Show_Left";
            this.Pnl_Show_Left.Size = new System.Drawing.Size(8, 400);
            this.Pnl_Show_Left.TabIndex = 1778;
            // 
            // Dgv_SelectPdo
            // 
            this.Dgv_SelectPdo.AllowUserToAddRows = false;
            this.Dgv_SelectPdo.AllowUserToDeleteRows = false;
            this.Dgv_SelectPdo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_SelectPdo.Location = new System.Drawing.Point(14, 69);
            this.Dgv_SelectPdo.MultiSelect = false;
            this.Dgv_SelectPdo.Name = "Dgv_SelectPdo";
            this.Dgv_SelectPdo.ReadOnly = true;
            this.Dgv_SelectPdo.RowTemplate.Height = 24;
            this.Dgv_SelectPdo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_SelectPdo.Size = new System.Drawing.Size(1172, 266);
            this.Dgv_SelectPdo.TabIndex = 1775;
            this.Dgv_SelectPdo.DoubleClick += new System.EventHandler(this.Dgv_SelectPdo_DoubleClick);
            // 
            // Frm_SelectDataOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 400);
            this.Controls.Add(this.Pnl_SelectPdo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_SelectDataOpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_SelectDataOpen";
            this.Load += new System.EventHandler(this.Frm_SelectDataOpen_Load);
            this.Pnl_SelectPdo.ResumeLayout(false);
            this.Pnl_SelectPdo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SelectPdo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_SelectPdo;
        private System.Windows.Forms.Button Btn_SelectPdo_Cancel;
        private System.Windows.Forms.Button Btn_SelectPdo_OK;
        private System.Windows.Forms.Label Lbl_SelectPdo_Title;
        internal System.Windows.Forms.Button Btn_SelectPdo_Close;
        private System.Windows.Forms.Panel Pnl_Show_Bottom;
        private System.Windows.Forms.Panel Pnl_Show_Top;
        private System.Windows.Forms.Panel Pnl_Show_Right;
        private System.Windows.Forms.Panel Pnl_Show_Left;
        private System.Windows.Forms.DataGridView Dgv_SelectPdo;
    }
}