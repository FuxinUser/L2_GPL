using Akka.Actor;
using LabelPrint.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabelPrint.Model.ZebraModel;

namespace GPLManager
{
    public partial class Frm_PrintLabels : Form
    {
        public string Str_Coil_No;//钢卷号

        public Frm_PrintLabels()
        {
            InitializeComponent();
        }

        private void Frm_PrintLabels_Load(object sender, EventArgs e)
        {
            Txt_Entry_Coil_No.Text = Str_Coil_No;//钢卷号
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            var coilID = Txt_Entry_Coil_No.Text.Trim();
            ZebraCommand msg = new ZebraCommand();
            msg.ZPL = coilID.zplCmd();

            PublicComm.lprSndEdit.Tell(msg);

            //提供回画面记录log
            Str_Coil_No = Txt_Entry_Coil_No.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
