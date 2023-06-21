namespace GPLManager
{
    partial class frm_1_4_CoilSkipReasonCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_1_4_CoilSkipReasonCode));
            this.dgSkipReasonCode = new System.Windows.Forms.DataGridView();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.txtSkipReason = new System.Windows.Forms.TextBox();
            this.lblSkipReason = new System.Windows.Forms.Label();
            this.lblSkipType = new System.Windows.Forms.Label();
            this.lblSkipReasonCode = new System.Windows.Forms.Label();
            this.txtSkipReasonCode = new System.Windows.Forms.TextBox();
            this.pnlShow = new System.Windows.Forms.Panel();
            this.txtSkipType = new System.Windows.Forms.TextBox();
            this.btnSkipDelete = new System.Windows.Forms.Button();
            this.btnSkipUpdate = new System.Windows.Forms.Button();
            this.btnSkipAdd = new System.Windows.Forms.Button();
            this.pnlSkipReasonCode = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgSkipReasonCode)).BeginInit();
            this.pnlShow.SuspendLayout();
            this.pnlSkipReasonCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSkipReasonCode
            // 
            this.dgSkipReasonCode.AllowUserToAddRows = false;
            this.dgSkipReasonCode.AllowUserToDeleteRows = false;
            this.dgSkipReasonCode.AllowUserToResizeColumns = false;
            this.dgSkipReasonCode.AllowUserToResizeRows = false;
            this.dgSkipReasonCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgSkipReasonCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSkipReasonCode.Location = new System.Drawing.Point(3, 3);
            this.dgSkipReasonCode.Name = "dgSkipReasonCode";
            this.dgSkipReasonCode.RowTemplate.Height = 24;
            this.dgSkipReasonCode.Size = new System.Drawing.Size(1873, 816);
            this.dgSkipReasonCode.TabIndex = 394;
            this.dgSkipReasonCode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSkipReasonCode_CellClick);
            this.dgSkipReasonCode.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSkipReasonCode_CellMouseDoubleClick);
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.lblMainTitle.Location = new System.Drawing.Point(849, 11);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(141, 25);
            this.lblMainTitle.TabIndex = 401;
            this.lblMainTitle.Text = "2-4 跳軋代碼";
            // 
            // txtSkipReason
            // 
            this.txtSkipReason.BackColor = System.Drawing.SystemColors.Window;
            this.txtSkipReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSkipReason.Location = new System.Drawing.Point(567, 33);
            this.txtSkipReason.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSkipReason.MaxLength = 25;
            this.txtSkipReason.Name = "txtSkipReason";
            this.txtSkipReason.Size = new System.Drawing.Size(636, 31);
            this.txtSkipReason.TabIndex = 125;
            // 
            // lblSkipReason
            // 
            this.lblSkipReason.AutoSize = true;
            this.lblSkipReason.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSkipReason.Location = new System.Drawing.Point(471, 41);
            this.lblSkipReason.Name = "lblSkipReason";
            this.lblSkipReason.Size = new System.Drawing.Size(89, 19);
            this.lblSkipReason.TabIndex = 398;
            this.lblSkipReason.Text = "跳軋原因";
            // 
            // lblSkipType
            // 
            this.lblSkipType.AutoSize = true;
            this.lblSkipType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSkipType.Location = new System.Drawing.Point(247, 41);
            this.lblSkipType.Name = "lblSkipType";
            this.lblSkipType.Size = new System.Drawing.Size(49, 19);
            this.lblSkipType.TabIndex = 397;
            this.lblSkipType.Text = "分類";
            // 
            // lblSkipReasonCode
            // 
            this.lblSkipReasonCode.AutoSize = true;
            this.lblSkipReasonCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSkipReasonCode.Location = new System.Drawing.Point(26, 41);
            this.lblSkipReasonCode.Name = "lblSkipReasonCode";
            this.lblSkipReasonCode.Size = new System.Drawing.Size(49, 19);
            this.lblSkipReasonCode.TabIndex = 396;
            this.lblSkipReasonCode.Text = "代碼";
            // 
            // txtSkipReasonCode
            // 
            this.txtSkipReasonCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtSkipReasonCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSkipReasonCode.Location = new System.Drawing.Point(82, 33);
            this.txtSkipReasonCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSkipReasonCode.MaxLength = 2;
            this.txtSkipReasonCode.Name = "txtSkipReasonCode";
            this.txtSkipReasonCode.Size = new System.Drawing.Size(122, 31);
            this.txtSkipReasonCode.TabIndex = 123;
            // 
            // pnlShow
            // 
            this.pnlShow.BackColor = System.Drawing.Color.GreenYellow;
            this.pnlShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlShow.Controls.Add(this.txtSkipType);
            this.pnlShow.Controls.Add(this.btnSkipDelete);
            this.pnlShow.Controls.Add(this.btnSkipUpdate);
            this.pnlShow.Controls.Add(this.btnSkipAdd);
            this.pnlShow.Controls.Add(this.txtSkipReason);
            this.pnlShow.Controls.Add(this.lblSkipReason);
            this.pnlShow.Controls.Add(this.lblSkipType);
            this.pnlShow.Controls.Add(this.lblSkipReasonCode);
            this.pnlShow.Controls.Add(this.txtSkipReasonCode);
            this.pnlShow.Location = new System.Drawing.Point(14, 49);
            this.pnlShow.Name = "pnlShow";
            this.pnlShow.Size = new System.Drawing.Size(1883, 100);
            this.pnlShow.TabIndex = 402;
            // 
            // txtSkipType
            // 
            this.txtSkipType.BackColor = System.Drawing.SystemColors.Window;
            this.txtSkipType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSkipType.Location = new System.Drawing.Point(303, 33);
            this.txtSkipType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSkipType.MaxLength = 2;
            this.txtSkipType.Name = "txtSkipType";
            this.txtSkipType.Size = new System.Drawing.Size(122, 31);
            this.txtSkipType.TabIndex = 1078;
            // 
            // btnSkipDelete
            // 
            this.btnSkipDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSkipDelete.BackgroundImage")));
            this.btnSkipDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkipDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSkipDelete.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSkipDelete.Location = new System.Drawing.Point(1489, 14);
            this.btnSkipDelete.Name = "btnSkipDelete";
            this.btnSkipDelete.Size = new System.Drawing.Size(110, 70);
            this.btnSkipDelete.TabIndex = 1077;
            this.btnSkipDelete.Text = "刪除";
            this.btnSkipDelete.UseVisualStyleBackColor = true;
            // 
            // btnSkipUpdate
            // 
            this.btnSkipUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSkipUpdate.BackgroundImage")));
            this.btnSkipUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkipUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSkipUpdate.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSkipUpdate.Location = new System.Drawing.Point(1369, 14);
            this.btnSkipUpdate.Name = "btnSkipUpdate";
            this.btnSkipUpdate.Size = new System.Drawing.Size(110, 70);
            this.btnSkipUpdate.TabIndex = 400;
            this.btnSkipUpdate.Text = "修改";
            this.btnSkipUpdate.UseVisualStyleBackColor = true;
            // 
            // btnSkipAdd
            // 
            this.btnSkipAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSkipAdd.BackgroundImage")));
            this.btnSkipAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkipAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSkipAdd.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSkipAdd.Location = new System.Drawing.Point(1249, 14);
            this.btnSkipAdd.Name = "btnSkipAdd";
            this.btnSkipAdd.Size = new System.Drawing.Size(110, 70);
            this.btnSkipAdd.TabIndex = 400;
            this.btnSkipAdd.Text = "新增";
            this.btnSkipAdd.UseVisualStyleBackColor = true;
            // 
            // pnlSkipReasonCode
            // 
            this.pnlSkipReasonCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSkipReasonCode.Controls.Add(this.dgSkipReasonCode);
            this.pnlSkipReasonCode.Location = new System.Drawing.Point(14, 155);
            this.pnlSkipReasonCode.Name = "pnlSkipReasonCode";
            this.pnlSkipReasonCode.Size = new System.Drawing.Size(1883, 815);
            this.pnlSkipReasonCode.TabIndex = 403;
            // 
            // frm_2_4_CoilSkipReasonCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1910, 981);
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.pnlShow);
            this.Controls.Add(this.pnlSkipReasonCode);
            this.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_2_4_CoilSkipReasonCode";
            this.Text = "frm_2_4_CoilSkipReasonCode";
            this.Shown += new System.EventHandler(this.Frm_2_3_CoilSkip_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgSkipReasonCode)).EndInit();
            this.pnlShow.ResumeLayout(false);
            this.pnlShow.PerformLayout();
            this.pnlSkipReasonCode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgSkipReasonCode;
        internal System.Windows.Forms.Label lblMainTitle;
        internal System.Windows.Forms.TextBox txtSkipReason;
        internal System.Windows.Forms.Label lblSkipReason;
        internal System.Windows.Forms.Label lblSkipType;
        internal System.Windows.Forms.Label lblSkipReasonCode;
        internal System.Windows.Forms.TextBox txtSkipReasonCode;
        internal System.Windows.Forms.Panel pnlShow;
        internal System.Windows.Forms.Button btnSkipDelete;
        internal System.Windows.Forms.Button btnSkipUpdate;
        internal System.Windows.Forms.Button btnSkipAdd;
        internal System.Windows.Forms.Panel pnlSkipReasonCode;
        internal System.Windows.Forms.TextBox txtSkipType;
    }
}