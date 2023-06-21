namespace GPLManager
{
    partial class frm_5_5_Language
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Dgv_Info = new System.Windows.Forms.DataGridView();
            this.Pnl_Top = new System.Windows.Forms.Panel();
            this.Rdb_KeywordEnglish = new System.Windows.Forms.RadioButton();
            this.Rdb_KeywordChinese = new System.Windows.Forms.RadioButton();
            this.Txt_KeywordEnglish = new System.Windows.Forms.TextBox();
            this.Txt_KeywordChinese = new System.Windows.Forms.TextBox();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            this.Pnl_Bottom = new System.Windows.Forms.Panel();
            this.Btn_Cancel_G = new System.Windows.Forms.Button();
            this.Btn_Save_G = new System.Windows.Forms.Button();
            this.Btn_New_G = new System.Windows.Forms.Button();
            this.Btn_Edit_G = new System.Windows.Forms.Button();
            this.Btn_Delete_G = new System.Windows.Forms.Button();
            this.Pnl_CurrentRow = new System.Windows.Forms.Panel();
            this.Dgv_CurrentRow = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Info)).BeginInit();
            this.Pnl_Top.SuspendLayout();
            this.Pnl_Bottom.SuspendLayout();
            this.Pnl_CurrentRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CurrentRow)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_Info
            // 
            this.Dgv_Info.AllowUserToAddRows = false;
            this.Dgv_Info.AllowUserToDeleteRows = false;
            this.Dgv_Info.AllowUserToResizeColumns = false;
            this.Dgv_Info.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Info.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.Dgv_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Info.Location = new System.Drawing.Point(5, 139);
            this.Dgv_Info.Name = "Dgv_Info";
            this.Dgv_Info.ReadOnly = true;
            this.Dgv_Info.RowHeadersVisible = false;
            this.Dgv_Info.RowTemplate.Height = 24;
            this.Dgv_Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Info.Size = new System.Drawing.Size(1910, 751);
            this.Dgv_Info.TabIndex = 1083;
            this.Dgv_Info.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Info_CellClick);
            this.Dgv_Info.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.Dgv_Info_ColumnAdded);
            // 
            // Pnl_Top
            // 
            this.Pnl_Top.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Top.Controls.Add(this.Rdb_KeywordEnglish);
            this.Pnl_Top.Controls.Add(this.Rdb_KeywordChinese);
            this.Pnl_Top.Controls.Add(this.Txt_KeywordEnglish);
            this.Pnl_Top.Controls.Add(this.Txt_KeywordChinese);
            this.Pnl_Top.Controls.Add(this.Btn_Search);
            this.Pnl_Top.Controls.Add(this.Btn_Edit);
            this.Pnl_Top.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Top.Name = "Pnl_Top";
            this.Pnl_Top.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Top.TabIndex = 1082;
            // 
            // Rdb_KeywordEnglish
            // 
            this.Rdb_KeywordEnglish.BackColor = System.Drawing.Color.SkyBlue;
            this.Rdb_KeywordEnglish.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Rdb_KeywordEnglish.ForeColor = System.Drawing.Color.Black;
            this.Rdb_KeywordEnglish.Location = new System.Drawing.Point(495, 25);
            this.Rdb_KeywordEnglish.Name = "Rdb_KeywordEnglish";
            this.Rdb_KeywordEnglish.Size = new System.Drawing.Size(155, 33);
            this.Rdb_KeywordEnglish.TabIndex = 1082;
            this.Rdb_KeywordEnglish.Text = "英文关键字";
            this.Rdb_KeywordEnglish.UseVisualStyleBackColor = false;
            // 
            // Rdb_KeywordChinese
            // 
            this.Rdb_KeywordChinese.BackColor = System.Drawing.Color.SkyBlue;
            this.Rdb_KeywordChinese.Checked = true;
            this.Rdb_KeywordChinese.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Rdb_KeywordChinese.ForeColor = System.Drawing.Color.Black;
            this.Rdb_KeywordChinese.Location = new System.Drawing.Point(20, 25);
            this.Rdb_KeywordChinese.Name = "Rdb_KeywordChinese";
            this.Rdb_KeywordChinese.Size = new System.Drawing.Size(155, 33);
            this.Rdb_KeywordChinese.TabIndex = 1081;
            this.Rdb_KeywordChinese.TabStop = true;
            this.Rdb_KeywordChinese.Text = "中文关键字";
            this.Rdb_KeywordChinese.UseVisualStyleBackColor = false;
            // 
            // Txt_KeywordEnglish
            // 
            this.Txt_KeywordEnglish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_KeywordEnglish.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold);
            this.Txt_KeywordEnglish.Location = new System.Drawing.Point(650, 25);
            this.Txt_KeywordEnglish.Name = "Txt_KeywordEnglish";
            this.Txt_KeywordEnglish.Size = new System.Drawing.Size(300, 34);
            this.Txt_KeywordEnglish.TabIndex = 1080;
            // 
            // Txt_KeywordChinese
            // 
            this.Txt_KeywordChinese.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_KeywordChinese.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_KeywordChinese.Location = new System.Drawing.Point(175, 25);
            this.Txt_KeywordChinese.Name = "Txt_KeywordChinese";
            this.Txt_KeywordChinese.Size = new System.Drawing.Size(300, 33);
            this.Txt_KeywordChinese.TabIndex = 1078;
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.Location = new System.Drawing.Point(970, 11);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search.TabIndex = 291;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.BackColor = System.Drawing.Color.Gold;
            this.Btn_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Edit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Edit.Location = new System.Drawing.Point(1140, 11);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(150, 60);
            this.Btn_Edit.TabIndex = 13;
            this.Btn_Edit.Text = "修改";
            this.Btn_Edit.UseVisualStyleBackColor = false;
            this.Btn_Edit.Visible = false;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_MainTitle.TabIndex = 1081;
            this.Lbl_MainTitle.Text = "5-5 中英文对照";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pnl_Bottom
            // 
            this.Pnl_Bottom.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_Bottom.Controls.Add(this.Btn_Cancel_G);
            this.Pnl_Bottom.Controls.Add(this.Btn_Save_G);
            this.Pnl_Bottom.Controls.Add(this.Btn_New_G);
            this.Pnl_Bottom.Controls.Add(this.Btn_Edit_G);
            this.Pnl_Bottom.Controls.Add(this.Btn_Delete_G);
            this.Pnl_Bottom.Location = new System.Drawing.Point(5, 893);
            this.Pnl_Bottom.Name = "Pnl_Bottom";
            this.Pnl_Bottom.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Bottom.TabIndex = 1768;
            // 
            // Btn_Cancel_G
            // 
            this.Btn_Cancel_G.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Cancel_G.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Cancel_G.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.Btn_Cancel_G.Location = new System.Drawing.Point(360, 10);
            this.Btn_Cancel_G.Name = "Btn_Cancel_G";
            this.Btn_Cancel_G.Size = new System.Drawing.Size(150, 60);
            this.Btn_Cancel_G.TabIndex = 1078;
            this.Btn_Cancel_G.Text = "取消";
            this.Btn_Cancel_G.UseVisualStyleBackColor = false;
            this.Btn_Cancel_G.Visible = false;
            this.Btn_Cancel_G.Click += new System.EventHandler(this.Btn_Cancel_G_Click);
            // 
            // Btn_Save_G
            // 
            this.Btn_Save_G.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Save_G.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Save_G.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.Btn_Save_G.Location = new System.Drawing.Point(190, 10);
            this.Btn_Save_G.Name = "Btn_Save_G";
            this.Btn_Save_G.Size = new System.Drawing.Size(150, 60);
            this.Btn_Save_G.TabIndex = 1077;
            this.Btn_Save_G.Text = "确认";
            this.Btn_Save_G.UseVisualStyleBackColor = false;
            this.Btn_Save_G.Visible = false;
            this.Btn_Save_G.Click += new System.EventHandler(this.Btn_Save_G_Click);
            // 
            // Btn_New_G
            // 
            this.Btn_New_G.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_New_G.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_New_G.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_New_G.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_New_G.Location = new System.Drawing.Point(675, 10);
            this.Btn_New_G.Name = "Btn_New_G";
            this.Btn_New_G.Size = new System.Drawing.Size(150, 60);
            this.Btn_New_G.TabIndex = 295;
            this.Btn_New_G.Text = "新增";
            this.Btn_New_G.UseVisualStyleBackColor = false;
            this.Btn_New_G.Visible = false;
            // 
            // Btn_Edit_G
            // 
            this.Btn_Edit_G.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Edit_G.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Edit_G.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Edit_G.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Edit_G.Location = new System.Drawing.Point(20, 10);
            this.Btn_Edit_G.Name = "Btn_Edit_G";
            this.Btn_Edit_G.Size = new System.Drawing.Size(150, 60);
            this.Btn_Edit_G.TabIndex = 13;
            this.Btn_Edit_G.Text = "修改";
            this.Btn_Edit_G.UseVisualStyleBackColor = false;
            this.Btn_Edit_G.Click += new System.EventHandler(this.Btn_Edit_G_Click);
            // 
            // Btn_Delete_G
            // 
            this.Btn_Delete_G.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Delete_G.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Delete_G.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Delete_G.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Delete_G.Location = new System.Drawing.Point(863, 10);
            this.Btn_Delete_G.Name = "Btn_Delete_G";
            this.Btn_Delete_G.Size = new System.Drawing.Size(150, 60);
            this.Btn_Delete_G.TabIndex = 296;
            this.Btn_Delete_G.Text = "刪除";
            this.Btn_Delete_G.UseVisualStyleBackColor = false;
            this.Btn_Delete_G.Visible = false;
            // 
            // Pnl_CurrentRow
            // 
            this.Pnl_CurrentRow.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_CurrentRow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_CurrentRow.Controls.Add(this.Dgv_CurrentRow);
            this.Pnl_CurrentRow.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Pnl_CurrentRow.Location = new System.Drawing.Point(5, 770);
            this.Pnl_CurrentRow.Name = "Pnl_CurrentRow";
            this.Pnl_CurrentRow.Size = new System.Drawing.Size(1370, 120);
            this.Pnl_CurrentRow.TabIndex = 1767;
            this.Pnl_CurrentRow.Visible = false;
            // 
            // Dgv_CurrentRow
            // 
            this.Dgv_CurrentRow.AllowUserToAddRows = false;
            this.Dgv_CurrentRow.AllowUserToDeleteRows = false;
            this.Dgv_CurrentRow.AllowUserToResizeColumns = false;
            this.Dgv_CurrentRow.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_CurrentRow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.Dgv_CurrentRow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CurrentRow.Location = new System.Drawing.Point(8, 13);
            this.Dgv_CurrentRow.Name = "Dgv_CurrentRow";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_CurrentRow.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.Dgv_CurrentRow.RowTemplate.Height = 24;
            this.Dgv_CurrentRow.Size = new System.Drawing.Size(1350, 90);
            this.Dgv_CurrentRow.TabIndex = 1081;
            // 
            // frm_5_5_Language
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Pnl_Bottom);
            this.Controls.Add(this.Pnl_CurrentRow);
            this.Controls.Add(this.Dgv_Info);
            this.Controls.Add(this.Pnl_Top);
            this.Controls.Add(this.Lbl_MainTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_5_5_Language";
            this.Text = "frm_6_5_Language";
            this.Load += new System.EventHandler(this.frm_5_5_Language_Load);
            this.Shown += new System.EventHandler(this.Frm_5_5_Language_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Info)).EndInit();
            this.Pnl_Top.ResumeLayout(false);
            this.Pnl_Top.PerformLayout();
            this.Pnl_Bottom.ResumeLayout(false);
            this.Pnl_CurrentRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CurrentRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView Dgv_Info;
        internal System.Windows.Forms.Panel Pnl_Top;
        internal System.Windows.Forms.RadioButton Rdb_KeywordEnglish;
        internal System.Windows.Forms.RadioButton Rdb_KeywordChinese;
        internal System.Windows.Forms.TextBox Txt_KeywordEnglish;
        internal System.Windows.Forms.TextBox Txt_KeywordChinese;
        internal System.Windows.Forms.Button Btn_Search;
        internal System.Windows.Forms.Button Btn_Edit;
        internal System.Windows.Forms.Label Lbl_MainTitle;
        internal System.Windows.Forms.Panel Pnl_Bottom;
        internal System.Windows.Forms.Button Btn_Cancel_G;
        internal System.Windows.Forms.Button Btn_Save_G;
        internal System.Windows.Forms.Button Btn_New_G;
        internal System.Windows.Forms.Button Btn_Edit_G;
        internal System.Windows.Forms.Button Btn_Delete_G;
        private System.Windows.Forms.Panel Pnl_CurrentRow;
        internal System.Windows.Forms.DataGridView Dgv_CurrentRow;
    }
}