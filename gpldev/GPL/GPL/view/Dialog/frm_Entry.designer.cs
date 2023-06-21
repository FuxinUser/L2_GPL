namespace GPLManager
{
    partial class frm_Entry
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
            this.lbSkid = new System.Windows.Forms.Label();
            this.btn_True = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.dgv_off = new System.Windows.Forms.DataGridView();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_off)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSkid
            // 
            this.lbSkid.AutoSize = true;
            this.lbSkid.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbSkid.Location = new System.Drawing.Point(130, 13);
            this.lbSkid.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbSkid.Name = "lbSkid";
            this.lbSkid.Size = new System.Drawing.Size(80, 31);
            this.lbSkid.TabIndex = 1;
            this.lbSkid.Text = "鞍座 : ";
            // 
            // btn_True
            // 
            this.btn_True.BackColor = System.Drawing.Color.LightGreen;
            this.btn_True.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_True.Location = new System.Drawing.Point(469, 220);
            this.btn_True.Name = "btn_True";
            this.btn_True.Size = new System.Drawing.Size(80, 40);
            this.btn_True.TabIndex = 4;
            this.btn_True.Text = "确定";
            this.btn_True.UseVisualStyleBackColor = false;
            this.btn_True.Click += new System.EventHandler(this.Btn_True_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightPink;
            this.btn_Close.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Close.Location = new System.Drawing.Point(566, 220);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 40);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // dgv_off
            // 
            this.dgv_off.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_off.Location = new System.Drawing.Point(12, 50);
            this.dgv_off.MultiSelect = false;
            this.dgv_off.Name = "dgv_off";
            this.dgv_off.ReadOnly = true;
            this.dgv_off.RowHeadersVisible = false;
            this.dgv_off.RowHeadersWidth = 51;
            this.dgv_off.RowTemplate.Height = 24;
            this.dgv_off.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_off.Size = new System.Drawing.Size(634, 164);
            this.dgv_off.TabIndex = 6;
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.Lbl_Title.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(118, 32);
            this.Lbl_Title.TabIndex = 38;
            this.Lbl_Title.Text = "入-插入作业";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkOrange;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 260);
            this.panel5.TabIndex = 50;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkOrange;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(653, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 260);
            this.panel4.TabIndex = 51;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkOrange;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 265);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(658, 5);
            this.panel3.TabIndex = 52;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(658, 5);
            this.panel2.TabIndex = 49;
            // 
            // frm_Entry
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(658, 270);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.dgv_off);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_True);
            this.Controls.Add(this.lbSkid);
            this.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frm_Entry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "入-插入作业";
            this.Load += new System.EventHandler(this.Frm_Entry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_off)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSkid;
        private System.Windows.Forms.Button btn_True;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.DataGridView dgv_off;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}