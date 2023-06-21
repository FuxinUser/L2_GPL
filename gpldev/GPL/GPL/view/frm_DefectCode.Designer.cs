namespace GPLManager
{
    partial class frm_DefectCode
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
            this.dgv_AlterList = new System.Windows.Forms.DataGridView();
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AlterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_AlterList
            // 
            this.dgv_AlterList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AlterList.Location = new System.Drawing.Point(12, 31);
            this.dgv_AlterList.Name = "dgv_AlterList";
            this.dgv_AlterList.RowTemplate.Height = 24;
            this.dgv_AlterList.Size = new System.Drawing.Size(912, 237);
            this.dgv_AlterList.TabIndex = 0;
            // 
            // dgv_List
            // 
            this.dgv_List.AllowUserToDeleteRows = false;
            this.dgv_List.AllowUserToResizeColumns = false;
            this.dgv_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_List.Location = new System.Drawing.Point(16, 320);
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.RowTemplate.Height = 24;
            this.dgv_List.Size = new System.Drawing.Size(908, 422);
            this.dgv_List.TabIndex = 1;
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Font = new System.Drawing.Font("新細明體", 15F);
            this.btn_Confirm.Location = new System.Drawing.Point(844, 274);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(80, 30);
            this.btn_Confirm.TabIndex = 4;
            this.btn_Confirm.Text = "儲存";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 15F);
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "修改";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 15F);
            this.label2.Location = new System.Drawing.Point(12, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "清單";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 15F);
            this.button1.Location = new System.Drawing.Point(844, 748);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "離開";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("新細明體", 15F);
            this.button2.Location = new System.Drawing.Point(758, 748);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 8;
            this.button2.Text = "確認";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Font = new System.Drawing.Font("新細明體", 15F);
            this.btn_Edit.Location = new System.Drawing.Point(672, 748);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(80, 30);
            this.btn_Edit.TabIndex = 10;
            this.btn_Edit.Text = "修改";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // frm_DefectCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 788);
            this.Controls.Add(this.btn_Edit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.dgv_List);
            this.Controls.Add(this.dgv_AlterList);
            this.Name = "frm_DefectCode";
            this.Text = "frm_DefectCode";
            this.Load += new System.EventHandler(this.frm_DefectCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AlterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_AlterList;
        private System.Windows.Forms.DataGridView dgv_List;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_Edit;
    }
}