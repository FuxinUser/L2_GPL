using Akka.Actor;
using AkkaSysBase;
using Core.Define;
using Core.Help;
using Core.Util;
using DataModel.Common;
using DBService;
using DBService.MMSRepository;
using DBService.MMSWMSRepository;
using DBService.UnitOfWork;
using MMSComm.Actor;
using MMSComm.Config;
using MMSComm.Service;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

namespace MMSComm.View
{
    public partial class MMSFrom : BaseForm, MESContract.IView
    {

        public MESContract.IPresenter Presenter { get; set; }
        private ISysAkkaManager _akkaManager;


        private DapperDBContext _hisDBContext;
        private int _hisDBMsgIdNum;
        private int _hisDBCreateTimeColumnNum;
        private int _hisDBMsgIDColumnNum;
        private AppSetting _appSetting;
        private AggregateService _aggregateService;
        
        public MMSFrom(ISysAkkaManager akkaManager, AppSetting appsetting, AggregateService aggregateService)
        {
            _akkaManager = akkaManager;
            _appSetting = appsetting;
            _aggregateService = aggregateService;
            InitializeComponent();
        }


        private void MESForm_Shown(object sender, EventArgs e)
        {
            InitUI();

            _hisDBContext = new DapperDBContext(DBParaDef.HisDBConn);
            _hisDBMsgIdNum = 0;
            _hisDBCreateTimeColumnNum = 5;
            _hisDBMsgIDColumnNum = 2;
        }

        private void InitUI()
        {
            tbLocalIp.Text = IniSystemHelper.Instance.MMSLocalIP;
            tbLocalPort.Text = IniSystemHelper.Instance.MMSLocalPort.ToString();
            tbRemoteIp.Text = IniSystemHelper.Instance.MMSRemoteIP;
            tbRemotePort.Text = IniSystemHelper.Instance.MMSRemotePort.ToString();
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

        private void btnHisSnd_Click(object sender, EventArgs e)
        {
            if (Dgv_History == null || Dgv_History.Rows.Count == 0)
            {
                MessageBox.Show("無資料可選");
                return;
            }

            SndHisData(BoxTableName.Text);


        }
        private void SndHisData(string tableName)
        {
            if (Dgv_MsgDetail == null || Dgv_MsgDetail.Rows.Count <= 0)
            {
                MessageBox.Show("請先撈取資料");
                return;
            }

            //if (tableName.Equals("TBL_MMS_ReceiveRecord"))
            //{
            //    MessageBox.Show("接收報文不做發送測試");
            //    return;
            //}



            var id = (int)Dgv_History.SelectedRows[0].Cells[_hisDBMsgIdNum].Value;
            var msgID = (string)Dgv_History.SelectedRows[0].Cells[_hisDBMsgIDColumnNum].Value;
            var str = string.Empty;

            for(int i=0; i < Dgv_MsgDetail.Rows.Count; i++)
            {
             
                var val = Dgv_MsgDetail[2, i].Value.ToString();
                str += val;
            }

            #region Debug用
            var byteData = Encoding.UTF8.GetBytes(str);
            var type = _aggregateService.MsgLengthAndTypeDic.MMSMsgType[msgID.Trim()];
            var msgStruct = MsgAnalUtil.RawDeserialize(byteData, type);

            if (msgStruct == null)
            {
                MessageBox.Show($"資料有錯！修改反解析成{msgID}報文有錯");
                return;
            }

            byteData = _aggregateService.AddEndTag(byteData);
            _aggregateService.DumpRawData.DumpMsg(byteData, _aggregateService.appSetting.SndMsgFilePath);
            var byteStr = Encoding.UTF8.GetString(byteData);
            #endregion

            try
            {
                var sqlStr = $"Update {tableName} SET {nameof(MMS_WMS_MsgRecord.Data)} = '{byteStr}' Where {nameof(MMS_WMS_MsgRecord.Id)} = '{id}'";
                var updateNum = _hisDBContext.Execute(sqlStr);
                MessageBox.Show($"修改更新歷史報文成功{updateNum > 0}");
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);          
            }

            SndTell(byteData);
        }

        private void SndTell(byte[] bytes)
        {
            var sndActor = _akkaManager.GetActor(nameof(MMSSnd));
            var msgID = _aggregateService.DumpRawData.GetMsgID(bytes);
            _aggregateService.DumpRawData.DumpMsg(bytes, _aggregateService.appSetting.SndMsgFilePath);
            var sndMsg = new CommonMsg(bytes.Length.ToString(), msgID, bytes);
            sndActor.Tell(sndMsg);

            MessageBox.Show($"發送報文{msgID}");
        }

    
        private void Dgv_History_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            var tableName = BoxTableName.Text;

            var id = (int)Dgv_History.SelectedRows[0].Cells[_hisDBMsgIdNum].Value;
            var msgID = (string)Dgv_History.SelectedRows[0].Cells[_hisDBMsgIDColumnNum].Value;

            if (tableName.Equals("TBL_MMS_SendRecord"))
            {
                var repo = new MMSSndRepo(DBParaDef.HisDBConn);
                var msgDBData = repo.GetAll().Where(x => x.Id == id).FirstOrDefault();
                var msgByte = _aggregateService.RemoveEndTag(msgDBData.Data.ToByteArray());
                var type = _aggregateService.MsgLengthAndTypeDic.MMSMsgType[msgID.Trim()];
                var msgStruct = MsgAnalUtil.RawDeserialize(msgByte, type);
                LoadDGMsgDetailData(msgStruct);
                return;
            }

            if (tableName.Equals("TBL_MMS_ReceiveRecord"))
            {
                var repo = new MMSRcvRepo(DBParaDef.HisDBConn);
                var msgDBData = repo.GetAll().Where(x => x.Id == id).FirstOrDefault();
                var msgByte = msgDBData.Data.ToByteArray();
                var type = _aggregateService.MsgLengthAndTypeDic.MMSMsgType[msgID.Trim()];
                var msgStruct = MsgAnalUtil.RawDeserialize(msgByte, type);
                LoadDGMsgDetailData(msgStruct);
                return;
            }

        }

        private DataTable GenMMSStructDT(object data, DataTable dt)
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
                        var byteValue = fi.GetValue(data) as byte[];
                        var value = Encoding.UTF8.GetString(byteValue);
                        dt.Rows.Add(new object[] { name, $"{type}({sizeCnt})", value});
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

            Dgv_MsgDetail.DataSource = GenMMSStructDT(msgStruct, dt);
            Dgv_MsgDetail.Columns[0].Width = 200;
            Dgv_MsgDetail.Columns[2].Width = 200;
        }
    
        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }

    }
}
