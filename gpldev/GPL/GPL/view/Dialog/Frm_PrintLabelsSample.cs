using Akka.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_PrintLabelsSample : Form
    {
        public string Str_Coil_No;//钢卷号
        public string Str_Steel_Grade_Sign;//钢种(牌号)
        public string Str_Coil_Thick;//钢卷厚度
        public string Str_Sample_Lot_No;//试批号
        public string Str_Sample_Position;//取样位置
        public string Str_Unit_code;//产线 CAPL
        public Frm_PrintLabelsSample()
        {
            InitializeComponent();
        }

        private void Frm_PrintLabelsSample_Load(object sender, EventArgs e)
        {
            Txt_Entry_Coil_No.Text =    Str_Coil_No;//钢卷号
            Txt_Steel_Grade_Sign.Text = Str_Steel_Grade_Sign;//钢种(牌号)
            Txt_Coil_Thick.Text = Str_Coil_Thick;//钢卷厚度
            Txt_Sample_Lot_No.Text = Str_Sample_Lot_No;//试批号
            Cob_Sample_Position.Text = Str_Sample_Position;//取样位置
             


        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            //PublicComm.lprSndEdit.Tell(new ToLpr.PrintSampleLabel() 
            //{ 
            //    CoilNo = Txt_Entry_Coil_No.Text ,
            //    Thick = Txt_Coil_Thick.Text,
            //    SampleNo = Txt_Sample_Lot_No.Text
            //    //,  Cob_Sample_Position.Text?

            //});

            //ZebraCommand msg = new ZebraCommand();
            //msg.ZPL = Txt_Entry_Coil_No.Text.Trim();

            //PublicComm.lprSndEdit.Tell(msg);

            //EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印标签作业", $"打印{Txt_Entry_Coil_No.Text.Trim()}标签");
            //EventLogHandler.Instance.EventPush_Message($"列印[{Txt_Entry_Coil_No.Text.Trim()}]标签");
            //PublicComm.ClientLog.Info($"通知Server列印鋼卷號:[{Txt_Entry_Coil_No.Text.Trim()}]標籤");


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
