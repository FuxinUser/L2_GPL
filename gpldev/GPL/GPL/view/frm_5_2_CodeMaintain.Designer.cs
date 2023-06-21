namespace GPLManager
{
    partial class Frm_5_2_CodeMaintain
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
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Cob_Table = new System.Windows.Forms.ComboBox();
            this.Dgv_Table = new System.Windows.Forms.DataGridView();
            this.Pnl_Top = new System.Windows.Forms.Panel();
            this.Lbl_Table_Title = new System.Windows.Forms.Label();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_New = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.Pnl_Bottom = new System.Windows.Forms.Panel();
            this.Dgv_CurrentRow = new System.Windows.Forms.DataGridView();
            this.Pnl_CurrentRow = new System.Windows.Forms.Panel();
            this.Pnl_All = new System.Windows.Forms.Panel();
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Table)).BeginInit();
            this.Pnl_Top.SuspendLayout();
            this.Pnl_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CurrentRow)).BeginInit();
            this.Pnl_CurrentRow.SuspendLayout();
            this.Pnl_All.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.Location = new System.Drawing.Point(469, 11);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search.TabIndex = 1114;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Cob_Table
            // 
            this.Cob_Table.FormattingEnabled = true;
            this.Cob_Table.Location = new System.Drawing.Point(180, 25);
            this.Cob_Table.Name = "Cob_Table";
            this.Cob_Table.Size = new System.Drawing.Size(269, 32);
            this.Cob_Table.TabIndex = 1113;
            // 
            // Dgv_Table
            // 
            this.Dgv_Table.AllowUserToAddRows = false;
            this.Dgv_Table.AllowUserToDeleteRows = false;
            this.Dgv_Table.AllowUserToResizeColumns = false;
            this.Dgv_Table.AllowUserToResizeRows = false;
            this.Dgv_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Table.Location = new System.Drawing.Point(5, 139);
            this.Dgv_Table.Name = "Dgv_Table";
            this.Dgv_Table.ReadOnly = true;
            this.Dgv_Table.RowHeadersVisible = false;
            this.Dgv_Table.RowTemplate.Height = 24;
            this.Dgv_Table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Table.Size = new System.Drawing.Size(1910, 749);
            this.Dgv_Table.TabIndex = 1759;
            this.Dgv_Table.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Table_CellClick);
            // 
            // Pnl_Top
            // 
            this.Pnl_Top.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Top.Controls.Add(this.Btn_Search);
            this.Pnl_Top.Controls.Add(this.Cob_Table);
            this.Pnl_Top.Controls.Add(this.Lbl_Table_Title);
            this.Pnl_Top.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Top.Margin = new System.Windows.Forms.Padding(4);
            this.Pnl_Top.Name = "Pnl_Top";
            this.Pnl_Top.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Top.TabIndex = 1758;
            // 
            // Lbl_Table_Title
            // 
            this.Lbl_Table_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Table_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Table_Title.Location = new System.Drawing.Point(20, 25);
            this.Lbl_Table_Title.Name = "Lbl_Table_Title";
            this.Lbl_Table_Title.Size = new System.Drawing.Size(160, 33);
            this.Lbl_Table_Title.TabIndex = 1112;
            this.Lbl_Table_Title.Text = "维护项目";
            this.Lbl_Table_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Delete.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Delete.Location = new System.Drawing.Point(360, 11);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(150, 60);
            this.Btn_Delete.TabIndex = 1089;
            this.Btn_Delete.Text = "删除";
            this.Btn_Delete.UseVisualStyleBackColor = false;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_New
            // 
            this.Btn_New.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_New.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_New.Location = new System.Drawing.Point(20, 11);
            this.Btn_New.Name = "Btn_New";
            this.Btn_New.Size = new System.Drawing.Size(150, 60);
            this.Btn_New.TabIndex = 1088;
            this.Btn_New.Text = "新增";
            this.Btn_New.UseVisualStyleBackColor = false;
            this.Btn_New.Click += new System.EventHandler(this.Btn_New_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(700, 11);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(150, 60);
            this.Btn_Cancel.TabIndex = 1087;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Visible = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Save.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Save.Location = new System.Drawing.Point(530, 11);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(150, 60);
            this.Btn_Save.TabIndex = 1086;
            this.Btn_Save.Text = "储存";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Visible = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Edit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Edit.Location = new System.Drawing.Point(190, 11);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(150, 60);
            this.Btn_Edit.TabIndex = 1085;
            this.Btn_Edit.Text = "修改";
            this.Btn_Edit.UseVisualStyleBackColor = false;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // Pnl_Bottom
            // 
            this.Pnl_Bottom.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Bottom.Controls.Add(this.Btn_Delete);
            this.Pnl_Bottom.Controls.Add(this.Btn_New);
            this.Pnl_Bottom.Controls.Add(this.Btn_Cancel);
            this.Pnl_Bottom.Controls.Add(this.Btn_Save);
            this.Pnl_Bottom.Controls.Add(this.Btn_Edit);
            this.Pnl_Bottom.Location = new System.Drawing.Point(5, 893);
            this.Pnl_Bottom.Margin = new System.Windows.Forms.Padding(4);
            this.Pnl_Bottom.Name = "Pnl_Bottom";
            this.Pnl_Bottom.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Bottom.TabIndex = 1760;
            // 
            // Dgv_CurrentRow
            // 
            this.Dgv_CurrentRow.AllowUserToAddRows = false;
            this.Dgv_CurrentRow.AllowUserToDeleteRows = false;
            this.Dgv_CurrentRow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_CurrentRow.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_CurrentRow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CurrentRow.Location = new System.Drawing.Point(20, 10);
            this.Dgv_CurrentRow.Name = "Dgv_CurrentRow";
            this.Dgv_CurrentRow.RowHeadersVisible = false;
            this.Dgv_CurrentRow.RowTemplate.Height = 24;
            this.Dgv_CurrentRow.Size = new System.Drawing.Size(1254, 169);
            this.Dgv_CurrentRow.TabIndex = 0;
            this.Dgv_CurrentRow.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Dgv_CurrentRow_DataError);
            // 
            // Pnl_CurrentRow
            // 
            this.Pnl_CurrentRow.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_CurrentRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_CurrentRow.Controls.Add(this.Dgv_CurrentRow);
            this.Pnl_CurrentRow.Location = new System.Drawing.Point(313, 397);
            this.Pnl_CurrentRow.Name = "Pnl_CurrentRow";
            this.Pnl_CurrentRow.Size = new System.Drawing.Size(1294, 189);
            this.Pnl_CurrentRow.TabIndex = 1761;
            this.Pnl_CurrentRow.Visible = false;
            // 
            // Pnl_All
            // 
            this.Pnl_All.AutoScroll = true;
            this.Pnl_All.Controls.Add(this.Pnl_CurrentRow);
            this.Pnl_All.Controls.Add(this.Pnl_Bottom);
            this.Pnl_All.Controls.Add(this.Dgv_Table);
            this.Pnl_All.Controls.Add(this.Pnl_Top);
            this.Pnl_All.Controls.Add(this.Lbl_MainTitle);
            this.Pnl_All.Location = new System.Drawing.Point(0, 0);
            this.Pnl_All.Name = "Pnl_All";
            this.Pnl_All.Size = new System.Drawing.Size(1920, 982);
            this.Pnl_All.TabIndex = 4;
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.BackColor = System.Drawing.Color.Gray;
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1910, 35);
            this.Lbl_MainTitle.TabIndex = 1754;
            this.Lbl_MainTitle.Text = "5-2 代码维护";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_5_2_CodeMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Pnl_All);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Frm_5_2_CodeMaintain";
            this.Text = "Frm_5_2_CodeMaintain";
            this.Load += new System.EventHandler(this.Frm_5_2_CodeMaintain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Table)).EndInit();
            this.Pnl_Top.ResumeLayout(false);
            this.Pnl_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CurrentRow)).EndInit();
            this.Pnl_CurrentRow.ResumeLayout(false);
            this.Pnl_All.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Btn_Search;
        private System.Windows.Forms.ComboBox Cob_Table;
        internal System.Windows.Forms.DataGridView Dgv_Table;
        internal System.Windows.Forms.Panel Pnl_Top;
        internal System.Windows.Forms.Label Lbl_Table_Title;
        internal System.Windows.Forms.Button Btn_Delete;
        internal System.Windows.Forms.Button Btn_New;
        internal System.Windows.Forms.Button Btn_Cancel;
        internal System.Windows.Forms.Button Btn_Save;
        internal System.Windows.Forms.Button Btn_Edit;
        internal System.Windows.Forms.Panel Pnl_Bottom;
        internal System.Windows.Forms.DataGridView Dgv_CurrentRow;
        private System.Windows.Forms.Panel Pnl_CurrentRow;
        private System.Windows.Forms.Panel Pnl_All;
        internal System.Windows.Forms.Label Lbl_MainTitle;
    }
}