using Core.Help;
using System;
using System.Configuration;
using WinformsMVP.Controls.Forms;
using Core.Define;
using WinformsMVP.Controls.Forms.Base;
using AkkaSysBase;
using PLCComm.Actor;
using MsgConvert;
using Core.Util;
using DataModel.Common;
using Akka.Actor;
using DBService;
using System.Windows.Forms;
using static DBService.L1Repository.L2L1MsgDBModel;
using DBService.L1Repository;
using MsgConvert.DBTable;
using System.Linq;
/**
* Author: ICSC 余士鵬
* Date: 2019/09/19
* Description: PLC UI
* Reference: 
* Modified: 
*/
namespace WMSComm.View
{
    public partial class PlcCommForm : BaseForm, PlcCommContract.IView
    {

        private ISysAkkaManager _akkaManager;

        public PlcCommContract.IPresenter Presenter { get; set; }
        public void SetPresenter(PlcCommContract.IPresenter presenter)
        {
            Presenter = presenter;
            Presenter.SetView(this);
        }

        public PlcCommForm(ISysAkkaManager akkaManager)
        {
            _akkaManager = akkaManager;
            InitializeComponent();
        }


        private void PlcCommForm_Shown(object sender, EventArgs e)
        {
            InitUI();
        }

        private void InitUI()
        {

            tbLocalIp.Text = IniSystemHelper.Instance.PLCLocalIP;
            tbLocalPort.Text = IniSystemHelper.Instance.PLCLocalPort.ToString();
            tbRemoteIp.Text = IniSystemHelper.Instance.PLCRemoteIP;
            tbRemotePort.Text = IniSystemHelper.Instance.PLCRemotePort.ToString();

            tbCycleRcvLocalIp.Text = IniSystemHelper.Instance.PLCCycleLocalIP;
            tbCycleRcvLocalPort.Text = IniSystemHelper.Instance.PLCCycleLocalPort.ToString();
        }



      

        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }


        #region 發送測試暫存
        private void btn207_Click(object sender, EventArgs e)
        {
            var msg = L1MsgFactory.ConvertEntryTakeOverStartCM();
            SndTell(msg, msg.MessageId.ToString());
        }

        private void btn208_Click(object sender, EventArgs e)
        {
            var msg = L1MsgFactory.ConvertDelTakeOverStartCM();
            SndTell(msg, msg.MessageId.ToString());
        }

        private void btn209_Click(object sender, EventArgs e)
        {
            var msg = L1MsgFactory.ConvertToDeliveryCoilScnCheckResult(2);
            SndTell(msg, msg.MessageId.ToString());
        }

        private void SndTell(object msg, string msgID)
        {
            var sndActor = _akkaManager.GetActor(nameof(PlcSnd));
            var bytes = msg.RawSerialize(false);
           
            var sndMsg = new CommonMsg(bytes.Length.ToString(), msgID, bytes);
            sndActor.Tell(sndMsg);
        }

        #endregion

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var tableName = BoxMsgID.Text;

            if (BoxMsgID.Text.Equals(string.Empty))
            {
                MessageBox.Show("請選擇報文");
                return;
            }


            var sqlStr = $"SELECT * FROM {tableName}";


            try
            {
                var msgSet = DataAccess.GetInstance().Select(sqlStr, DBParaDef.HisDBConn);

                Dgv_History.ColumnHeadersVisible = false;
                Dgv_History.RowHeadersVisible = false;
                if (Dgv_History.Columns.Count > 0)
                {
                    Dgv_History.Columns.Clear();
                }
                Dgv_History.DataSource = msgSet;

                //if (BoxMsgID.Text.Equals("L2L1_204") || BoxMsgID.Text.Equals("L2L1_205"))
                //{
                for (int i = 10; i < Dgv_History.Columns.Count; i++)
                    Dgv_History.Columns[i].Visible = false;
                //}

                Dgv_History.ColumnHeadersVisible = true;

            }
            catch (Exception expection)
            {
                MessageBox.Show(expection.Message);
            }




        }

        private void btnHisSnd_Click(object sender, EventArgs e)
        {

            if (BoxMsgID.Text.Equals(string.Empty))
            {
                MessageBox.Show("請選擇報文");
                return;
            }
            if(Dgv_History == null || Dgv_History.Rows.Count == 0)
            {
                MessageBox.Show("無資料可選");
                return;
            }
            TryFlow(() => SndHisData(BoxMsgID.Text));
        }

        private void SndHisData(string tableName)
        {
           
            var createTime = (DateTime)Dgv_History.SelectedRows[0].Cells[5].Value;

            switch (tableName)
            {
                case nameof(L2L1_202):
                    var repo202 = new L1202HisMsgRepo(DBParaDef.HisDBConn);
                    var data202 = repo202.GetAll().Where(x=>x.CreateTime.CompareTo(createTime)==0).FirstOrDefault();
                    var L1202 = data202.ConvertL1MsgModel("202");
                    SndTell(L1202, "202");
                    break;

                case nameof(L2L1_203):
                    var repo203 = new L1203HisMsgRepo(DBParaDef.HisDBConn);
                    var data203 = repo203.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1203 = data203.ConvertL1MsgModel("203");
                    SndTell(L1203, "203");
                    break;

                case nameof(L2L1_204):
                    var repo204 = new L1204HisMsgRepo(DBParaDef.HisDBConn);
                    var data04 = repo204.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1204 = data04.ConvertL1MsgModel("204");
                    SndTell(L1204, "204");
                    break;


                case nameof(L2L1_205):
                    var repo205 = new L1205HisMsgRepo(DBParaDef.HisDBConn);
                    var coil = Dgv_History.SelectedRows[0].Cells[7].Value;
                    var data05 = repo205.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1205 = data05.ConvertL1MsgModel("205");
                    SndTell(L1205, "205");
                    break;


                case nameof(L2L1_206):
                    var repo206 = new L1206HisMsgRepo(DBParaDef.HisDBConn);
                    var data206 = repo206.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1206 = data206.ConvertL1MsgModel("206");
                    SndTell(L1206, "206");
                    break;

                case nameof(L2L1_207):
                    var repo207 = new L1207HisMsgRepo(DBParaDef.HisDBConn);
                    var data207 = repo207.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1207 = data207.ConvertL1MsgModel("207");
                    SndTell(L1207, "207");
                    break;

                case nameof(L2L1_208):
                    var repo208 = new L1208HisMsgRepo(DBParaDef.HisDBConn);
                    var data208 = repo208.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1208 = data208.ConvertL1MsgModel("208");
                    SndTell(L1208, "208");
                    break;

                case nameof(L2L1_209):
                    var repo209 = new L1209HisMsgRepo(DBParaDef.HisDBConn);
                    var data209 = repo209.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1209 = data209.ConvertL1MsgModel("209");
                    SndTell(L1209, "209");
                    break;

                case nameof(L2L1_210):
                    var repo210 = new L1210HisMsgRepo(DBParaDef.HisDBConn);
                    var data210 = repo210.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1210 = data210.ConvertL1MsgModel("210");
                    SndTell(L1210, "210");
                    break;

                case nameof(L2L1_211):
                    var repo211 = new L1211HisMsgRepo(DBParaDef.HisDBConn);
                    var data211 = repo211.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1211 = data211.ConvertL1MsgModel("211");
                    SndTell(L1211, "211");
                    break;

            }
        }


        /// <summary>
        ///     Try action of flow
        /// </summary>
        protected virtual void TryFlow(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void btnClearBuffer_Click(object sender, EventArgs e)
        {
            var rcvActor = _akkaManager.GetActor(nameof(PlcRcv));
            rcvActor.Tell("Clear BUffer");
        }
    }
}
