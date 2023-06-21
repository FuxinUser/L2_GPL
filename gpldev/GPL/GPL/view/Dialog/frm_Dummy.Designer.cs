namespace GPLManager
{
    partial class Frm_Dummy
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
            this.dgv_Dummy = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Txt_DummyPlanNo = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_Schedule = new System.Windows.Forms.ComboBox();
            this.radio_InsertFirst = new System.Windows.Forms.RadioButton();
            this.radio_Insert = new System.Windows.Forms.RadioButton();
            this.Txt_DummyCoil = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dummy)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Dummy
            // 
            this.dgv_Dummy.AllowUserToAddRows = false;
            this.dgv_Dummy.AllowUserToDeleteRows = false;
            this.dgv_Dummy.AllowUserToResizeColumns = false;
            this.dgv_Dummy.AllowUserToResizeRows = false;
            this.dgv_Dummy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Dummy.Location = new System.Drawing.Point(0, 0);
            this.dgv_Dummy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_Dummy.Name = "dgv_Dummy";
            this.dgv_Dummy.ReadOnly = true;
            this.dgv_Dummy.RowHeadersWidth = 51;
            this.dgv_Dummy.RowTemplate.Height = 24;
            this.dgv_Dummy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Dummy.Size = new System.Drawing.Size(405, 231);
            this.dgv_Dummy.TabIndex = 0;
            this.dgv_Dummy.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Dummy_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Txt_DummyPlanNo);
            this.groupBox1.Controls.Add(this.btn_Cancel);
            this.groupBox1.Controls.Add(this.btn_OK);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbo_Schedule);
            this.groupBox1.Controls.Add(this.radio_InsertFirst);
            this.groupBox1.Controls.Add(this.radio_Insert);
            this.groupBox1.Controls.Add(this.Txt_DummyCoil);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(15, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 266);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插入过度卷";
            // 
            // Txt_DummyPlanNo
            // 
            this.Txt_DummyPlanNo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_DummyPlanNo.Location = new System.Drawing.Point(278, 14);
            this.Txt_DummyPlanNo.Name = "Txt_DummyPlanNo";
            this.Txt_DummyPlanNo.Size = new System.Drawing.Size(89, 34);
            this.Txt_DummyPlanNo.TabIndex = 8;
            this.Txt_DummyPlanNo.Visible = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.LightPink;
            this.btn_Cancel.Location = new System.Drawing.Point(210, 206);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 40);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.LightGreen;
            this.btn_OK.Location = new System.Drawing.Point(62, 206);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(100, 40);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "之后";
            // 
            // cbo_Schedule
            // 
            this.cbo_Schedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Schedule.FormattingEnabled = true;
            this.cbo_Schedule.Location = new System.Drawing.Point(112, 99);
            this.cbo_Schedule.Name = "cbo_Schedule";
            this.cbo_Schedule.Size = new System.Drawing.Size(207, 39);
            this.cbo_Schedule.TabIndex = 4;
            // 
            // radio_InsertFirst
            // 
            this.radio_InsertFirst.AutoSize = true;
            this.radio_InsertFirst.Location = new System.Drawing.Point(43, 148);
            this.radio_InsertFirst.Name = "radio_InsertFirst";
            this.radio_InsertFirst.Size = new System.Drawing.Size(155, 35);
            this.radio_InsertFirst.TabIndex = 3;
            this.radio_InsertFirst.Text = "插入第四笔";
            this.radio_InsertFirst.UseVisualStyleBackColor = true;
            // 
            // radio_Insert
            // 
            this.radio_Insert.AutoSize = true;
            this.radio_Insert.Checked = true;
            this.radio_Insert.Location = new System.Drawing.Point(43, 100);
            this.radio_Insert.Name = "radio_Insert";
            this.radio_Insert.Size = new System.Drawing.Size(83, 35);
            this.radio_Insert.TabIndex = 2;
            this.radio_Insert.TabStop = true;
            this.radio_Insert.Text = "插入";
            this.radio_Insert.UseVisualStyleBackColor = true;
            // 
            // Txt_DummyCoil
            // 
            this.Txt_DummyCoil.Enabled = false;
            this.Txt_DummyCoil.Location = new System.Drawing.Point(112, 49);
            this.Txt_DummyCoil.Name = "Txt_DummyCoil";
            this.Txt_DummyCoil.Size = new System.Drawing.Size(207, 39);
            this.Txt_DummyCoil.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "选取过度卷";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(811, 287);
            this.panel1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(390, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(413, 268);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_Dummy);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(405, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "过渡卷";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Frm_Dummy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(824, 301);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Dummy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " 插入过度卷";
            this.Load += new System.EventHandler(this.Frm_Dummy_Load);
            this.Shown += new System.EventHandler(this.Frm_Dummy_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dummy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Dummy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_Schedule;
        private System.Windows.Forms.RadioButton radio_InsertFirst;
        private System.Windows.Forms.RadioButton radio_Insert;
        private System.Windows.Forms.TextBox Txt_DummyCoil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox Txt_DummyPlanNo;
    }
}