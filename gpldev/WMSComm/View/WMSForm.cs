using System;
using System.Configuration;
using WinformsMVP.Controls.Forms;
using Core.Define;
using WinformsMVP.Controls.Forms.Base;
using WMSComm.Model;
using WMSComm.Config;
using AkkaSysBase;
using DBService.UnitOfWork;
using System.Windows.Forms;
using DBService;
using DBService.MMSRepository;
using DBService.MMSWMSRepository;
using System.Linq;
using Core.Util;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using Core.Help;

namespace WMSComm.View
{
    public partial class WMSFrom : BaseForm, WMSContract.IView
    {      
 
        public WMSContract.IPresenter Presenter { get; set; }

        private ISysAkkaManager _akkaManager;
        private DapperDBContext _hisDBContext;
        private int _hisDBMsgIdNum;
        private int _hisDBCreateTimeColumnNum;
        private int _hisDBMsgIDColumnNum;
        private AppSetting _appSetting;
        private AggregateService _aggregateService;

        public WMSFrom(ISysAkkaManager akkaManager, AppSetting appsetting, AggregateService aggregateService)
        {          
            InitializeComponent();
            _akkaManager = akkaManager;
            _appSetting = appsetting;
            _aggregateService = aggregateService;

        }


        private void WMSForm_Shown(object sender, EventArgs e)
        {
            InitUI();
            _hisDBContext = new DapperDBContext(DBParaDef.HisDBConn);
            _hisDBMsgIdNum = 0;
            _hisDBCreateTimeColumnNum = 5;
            _hisDBMsgIDColumnNum = 2;
        }

        private void InitUI()
        {
            //網路環境設定(顯示):
            tbLocalIp.Text = IniSystemHelper.Instance.WMSLocalIP;
            tbLocalPort.Text = IniSystemHelper.Instance.WMSLocalPort.ToString();
            tbRemoteIp.Text = IniSystemHelper.Instance.WMSRemoteIP;
            tbRemotePort.Text = IniSystemHelper.Instance.WMSRemotePort.ToString();

        }
     
        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var tableName = BoxTableName.Text;
            var msgID = BoxMsgID.Text;

            if (BoxTableName.Text.Equals(string.Empty))
            {
                MessageBox.Show("請選擇報文");
                return;
            }
            var where = !msgID.Equals(string.Empty) ? $"where Header = '{msgID}'" : string.Empty;

            var sqlStr = $"SELECT * FROM {tableName} {where} order by {nameof(MMS_WMS_MsgRecord.CreateTime)} desc ";

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

                // Byte Array顯示有問題.先隱藏
                //Dgv_History.Columns[5].Visible = false;

                Dgv_History.ColumnHeadersVisible = true;

            }
            catch (Exception expection)
            {
                MessageBox.Show(expection.Message);
            }
        }

        private void Dgv_History_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var tableName = BoxTableName.Text;

            var id = (int)Dgv_History.SelectedRows[0].Cells[_hisDBMsgIdNum].Value;
            var msgID = (string)Dgv_History.SelectedRows[0].Cells[_hisDBMsgIDColumnNum].Value;

            if (tableName.Equals("TBL_WMS_SendRecord"))
            {
                var repo = new WMSSndRepo(DBParaDef.HisDBConn);
                var msgDBData = repo.GetAll().Where(x => x.Id == id).FirstOrDefault();
                var msgByte = msgDBData.Data.ToByteArray();
                var type = _aggregateService.MsgLengthAndTypeDic.WMSMsgType[msgID.Trim()];
                var msgStruct = MsgAnalUtil.RawDeserialize(msgByte, type);
                LoadDGMsgDetailData(msgStruct);
                return;
            }

            if (tableName.Equals("TBL_WMS_ReceiveRecord"))
            {
                var repo = new WMSRcvRepo(DBParaDef.HisDBConn);
                var msgDBData = repo.GetAll().Where(x => x.Id == id).FirstOrDefault();
                var msgByte = msgDBData.Data.ToByteArray();
                var type = _aggregateService.MsgLengthAndTypeDic.WMSMsgType[msgID.Trim()];
                var msgStruct = MsgAnalUtil.RawDeserialize(msgByte, type);
                LoadDGMsgDetailData(msgStruct);
                return;
            }
        }

        private DataTable GenStructDT(object data, DataTable dt)
        {
            foreach (FieldInfo fi in data.GetType().GetFields())
            {
                var arrType = fi.FieldType.GetElementType();
                int iSizeConst = fi.GetCustomAttribute<MarshalAsAttribute>().SizeConst;

                for (int k = 0; k <= iSizeConst - 1; k++)
                {
                    if (k == iSizeConst - 1)
                    {
                        var name = fi.Name;
                        var type = fi.FieldType.Name;
                        var sizeCnt = iSizeConst;
                        var charValue = fi.GetValue(data) as char[];
                        var value = charValue.ToStr();
                        dt.Rows.Add(new object[] { name, $"{type}({sizeCnt})", value });
                    }
                }
            }

            return dt;
        }

        private void LoadDGMsgDetailData(object msgStruct)
        {

            var dt = new DataTable();
            dt.Columns.Add("Field");
            dt.Columns.Add("Type");
            dt.Columns.Add("Value");

            Dgv_MsgDetail.DataSource = GenStructDT(msgStruct, dt);
            Dgv_MsgDetail.Columns[0].Width = 200;
            Dgv_MsgDetail.Columns[2].Width = 200;
        }
    }
}
