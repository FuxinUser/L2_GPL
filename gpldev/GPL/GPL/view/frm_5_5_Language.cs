using GPLManager.Contrl;
using GPLManager.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static DBService.Repository.LangSwitch.LangSwitchEntity;

namespace GPLManager
{
    public partial class frm_5_5_Language : Form
    {
        //語系
        private LanguageHandler LanguageHand;
        DataTable dtLang_Nav;
        DataTable dtSelectOne;
        DataTable dtBeforEdit;
        bool bolEditStatus;
        public enum Action
        {
            None = 0,
            Insert = 1,
            Update = 2,
            Delete = 3
        }
        private DBHandler DbHand = null;
        //private UIHandler UIHand = null;

        // 給表單用的資料來源
        private Dictionary<string, string> resourceMap;

        // 修改表單時參照用
        private Dictionary<string, string> chnKeyMap;
        private Dictionary<string, string> engKeyMap;
        string queryKey = "";

        // 資源檔的路徑
        string chnFile = "";
        string engFile = "";
       
        public frm_5_5_Language()
        {
            InitializeComponent();
            DbHand = new DBHandler();// dbHand;           
        }
        private void frm_5_5_Language_Load(object sender, EventArgs e)
        {
            PublicForms.Language = this;
            Control[] Frm_5_5_Control = new Control[] {
                Btn_Edit,Btn_Edit_G
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_5_Control, UserSetupHandler.Instance.Frm_5_5);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Frm_5_5_Language_Shown(object sender, EventArgs e)
        {
            Fun_GetData();
            #region Old Resource 
            //string currPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //// currPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(currPath))) + "\\view";
            //currPath = Path.GetDirectoryName((System.Reflection.Assembly.GetEntryAssembly().Location));
            //engFile = Path.Combine(currPath + "\\view", "frm_0_2_Menu.en-US.resx");
            //chnFile = Path.Combine(currPath + "\\view", "frm_0_2_Menu.zh-CN.resx");

            //GetResourceValue();
            #endregion
        }
        private void Fun_GetData()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine($"SELECT * FROM {nameof(TBL_LangSwitch_Nav)}");
            sbSql.AppendLine($"WHERE {nameof(TBL_LangSwitch_Nav.PKey)} Like 'ts%' ");
            //sbSql.AppendLine($"");        
            string strSql = sbSql.ToString();
            
            dtLang_Nav = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"取得{nameof(TBL_LangSwitch_Nav)}资料");

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dtLang_Nav);
            DGVColumnsHandler.Instance.Frm_5_5_LangSwitch_Nav(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);
        }       

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_GetData();
            #region Old Resource 
            //string queryString_chn = Txt_KeywordChinese.Text;
            //string queryString_eng = Txt_KeywordEnglish.Text;
            //List<KeyValuePair<string, string>> dictData = new List<KeyValuePair<string, string>>();

            //if (!string.IsNullOrWhiteSpace(queryString_chn) && Rdb_KeywordChinese.Checked)
            //{
            //    dictData = resourceMap.Where(x => x.Key.ToLower().Contains(queryString_chn.ToLower())).ToList();
            //    Dgv_Info.DataSource = dictData;
            //}
            //else if (!string.IsNullOrWhiteSpace(queryString_eng) && Rdb_KeywordEnglish.Checked)
            //{
            //    dictData = resourceMap.Where(x => x.Value.ToLower().Contains(queryString_eng.ToLower())).ToList();
            //    Dgv_Info.DataSource = dictData;
            //}
            //else
            //{
            //    var sortedDict = from entry in resourceMap orderby entry.Value ascending select entry;
            //    Dgv_Info.DataSource = sortedDict.ToArray();
            //}
            #endregion
        }

        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            //Dgv_Info 將點選的Row存起來
            if (Dgv_Info.CurrentRow == null) { return; }
            if (bolEditStatus) { return; }
            DataRow dr = Fun_GetDataRowFromCurrentRow(Dgv_Info, dtLang_Nav);

            DataTable dt = dtLang_Nav.Clone();
            try
            {
                dt.LoadDataRow(dr.ItemArray, false);
            }
            catch (Exception ee)
            {
                return;
            }
            //Fun_SetSelectOne(dt);
            dtSelectOne = dt.Copy();

            //PublicComm.portal.UIHand.Fun_SetBottonEnabled(Btn_New_G, true);     //新增
            Fun_SetBottonEnabled(Btn_Edit_G, true);    //修改
            //PublicComm.portal.UIHand.Fun_SetBottonEnabled(Btn_Delete_G, true);  //刪除
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {          
            #region Old Resource 
            //if (chnKeyMap[queryKey] != Txt_KeywordChinese.Text)
            //{
            //    UpdateResourceValue(chnFile, Txt_KeywordChinese.Text);
            //}

            //if (engKeyMap[queryKey] != Txt_KeywordEnglish.Text)
            //{
            //    UpdateResourceValue(engFile, Txt_KeywordEnglish.Text);
            //}

            //GetResourceValue();
            #endregion
        }
        private void Btn_Edit_G_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
            if (dtSelectOne.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk($"请选择要修改的项目", "警告资讯", Properties.Resources.dialogWarning, 2);
                Dgv_CurrentRow.Focus();
                return;
            }

            if (Dgv_Info.CurrentRow == null || Dgv_Info.CurrentRow.Index == -1 || Dgv_Info.SelectedRows.Count == 0) return;

            Dgv_CurrentRow.DataSource = null;

            //Fun_TableDisplay(dtSelectOne, Dgv_CurrentRow);
            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_CurrentRow, dtSelectOne);
            DGVColumnsHandler.Instance.Frm_5_5_LangSwitch_Nav(Dgv_CurrentRow);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_CurrentRow);

            //用來設定欄位ReadOnly不可修改            
            Dgv_CurrentRow.Columns[0].ReadOnly = true;
            Dgv_CurrentRow.Rows[0].Cells[0].Style.BackColor = Color.LightGray;

            //Dgv_CurrentRow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            bolEditStatus = true;

            Pnl_CurrentRow.Visible = true;
            Dgv_Info.Enabled = false;

            //Fun_SetBottonEnabled(Btn_New_G, false);       //新增
            //Fun_SetBottonEnabled(Btn_Delete_G, false);    //刪除
            Btn_Save_G.Visible = true;                           //儲存
            Btn_Cancel_G.Visible = true;                         //取消  
        }

        private void Btn_Save_G_Click(object sender, EventArgs e)
        {
            bool bolSaveOK = false;

            if (Btn_New_G.Enabled && Btn_New_G.Visible == true)
            {
                bolSaveOK = Fun_TableDataInsertIntoDataBase(Action.Insert);
            }
            else
            if (Btn_Edit_G.Enabled && Btn_Edit_G.Visible == true)
            {
                bolSaveOK = Fun_TableDataInsertIntoDataBase(Action.Update);
            }

            if (bolSaveOK)
            {
                bolEditStatus = false;
                //Fun_SetBottonEnabled(Btn_New_G, true);    //新增
                Fun_SetBottonEnabled(Btn_Edit_G, true);   //修改
               //Fun_SetBottonEnabled(Btn_Delete_G, true); //删除
                Btn_Save_G.Visible = false;                   //储存
                Btn_Cancel_G.Visible = false;                    //取消

                Pnl_CurrentRow.Visible = false;
                Dgv_Info.Enabled = true;

                //pnl_CurrentRow.Visible = false;
                //bolEditStatus = false;

                Btn_Search.PerformClick();
            }
            else { }
        }

        private void Btn_Cancel_G_Click(object sender, EventArgs e)
        {
            bolEditStatus = false;

            if (Dgv_Info.CurrentRow == null || Dgv_Info.CurrentRow.Index == -1 || Dgv_Info.SelectedRows.Count == 0)
            {
                Fun_SetBottonEnabled(Btn_Edit_G, false);    //修改
                //UIHand.Fun_SetBottonBlueEnabled(Btn_Delete_G, false);  //刪除
            }
            else
            {
                Fun_SetBottonEnabled(Btn_Edit_G, true);     //修改
                //UIHand.Fun_SetBottonBlueEnabled(Btn_Delete_G, true);   //刪除
            }

            //UIHand.Fun_SetBottonBlueEnabled(Btn_New_G, true);     //新增
            Btn_Save_G.Visible = false;     //儲存
            Btn_Cancel_G.Visible = false;   //取消

            Pnl_CurrentRow.Visible = false;
            Dgv_Info.Enabled = true;
        }
        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //if (btn.Name.Equals(Btn_MMS.Name) || btn.Name.Equals(Btn_PrintTag.Name))
            //    btn.BackColor = bolE ? Color.Gold : Color.LightGray;
            //else
                ////Color colorBack;
                btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        /// <summary>
        /// 將選中的 DataGridViewRow 轉成 DataRow.
        /// </summary>
        /// <param name="dgv">來源DataGridView</param>
        /// <param name="dt">來源DataTable</param>
        /// <returns></returns>
        public DataRow Fun_GetDataRowFromCurrentRow(DataGridView dgv, DataTable dt)
        {
            if (dgv.CurrentRow == null) { return null; }
            if (dgv.SelectedRows.Count <= 0) { return null; }
            DataRowView drv = dgv.SelectedRows[0].DataBoundItem as DataRowView;
            int index = dt.Rows.IndexOf(drv.Row);
            DataRow dr = dt.Rows[index];

            return dr;
        }
        ////檢查 DataTable 是否Null 或 沒資料
        //public bool Fun_IsDataTableNull(DataTable dt)
        //{
        //    bool bolNull = false;
        //    if (dt != null && dt.Rows.Count > 0)
        //        bolNull = false;
        //    else
        //        bolNull = true;

        //    return bolNull;
        //}

        /// <summary>
        /// 新增、修改、删除語法
        /// </summary>
        /// <returns></returns>
        public bool Fun_TableDataInsertIntoDataBase(Action action)
        {
            //Log & 事件紀錄 使用
            //strLUT_Name //改为共用
            string eventText = "";

            //PRIMARY KEY 
            string[] strArr = new string[] { "PKey" };

            ///////
            if (bolEditStatus)
            {
                if (Dgv_CurrentRow.CurrentRow == null)
                {
                    //PublicComm.portal.DialogHand.Fun_DialogShowOk("无资料!", PublicConst.DialogInformation, Properties.Resources.dialogInformation, 0);
                    return false;
                }

                if (dtSelectOne == null || dtSelectOne.Rows.Count <= 0)
                {
                    DataRow newRow = dtSelectOne.NewRow();
                    dtSelectOne.Rows.InsertAt(newRow, 0);

                    foreach (string strCol in strArr)
                    {
                        if (string.IsNullOrEmpty(dtSelectOne.Rows[0][strCol].ToString()))
                            dtSelectOne.Rows[0][strCol] = dtLang_Nav.Rows[0][strCol].ToString();
                    }
                }

                DataTable dd = new DataTable();
                dd = dtLang_Nav.Clone();//赋值结构
                foreach (DataGridViewRow dgvr in Dgv_CurrentRow.Rows)
                {
                    DataRow drr = dd.NewRow();
                    drr = (dgvr.DataBoundItem as DataRowView).Row;//微软提供的唯一的转换DataRow
                    dd.Rows.Add(drr.ItemArray);//此处不可是直接dr否则会报错
                }

                foreach (string strCol in strArr)
                {
                    if (string.IsNullOrEmpty(dd.Rows[0][strCol].ToString()))
                        dd.Rows[0][strCol] = dtSelectOne.Rows[0][strCol].ToString();
                }

                DataTable dtAfter = dd.Copy();

                string strPKey = dtAfter.Rows[0][nameof(TBL_LangSwitch_Nav.PKey)].ToString();
                string strZH = dtAfter.Rows[0][nameof(TBL_LangSwitch_Nav.ZH)].ToString();
                string strEN = dtAfter.Rows[0][nameof(TBL_LangSwitch_Nav.EN)].ToString();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($" SELECT * FROM {nameof(TBL_LangSwitch_Nav)} ");
                sb.AppendLine($" WHERE {nameof(TBL_LangSwitch_Nav.PKey)} != N'' ");
                sb.AppendLine($" AND {nameof(TBL_LangSwitch_Nav.PKey)} = N'{strPKey.Trim()}' ");

                //先检查 有没有重复
                string strCheckSql = sb.ToString();
                object objHaveData = Data_Access.Fun_SelectDate(strCheckSql, GlobalVariableHandler.Instance.strConn_GPL, "TBL_LangSwitch_Nav资料");
                //DbHand.Fun_GetObject(strCheckSql, DbHand.strDBConnectString);

                if (action == Action.Update)
                {
                    /*"Edit"*/
                    if (objHaveData == null && string.IsNullOrEmpty(objHaveData.ToString()))
                    {
                        //储存前先至资料库做确认,资料有在
                        StringBuilder sbShow = new StringBuilder();
                        sbShow.AppendLine($"修改[{strPKey}]中英文");
                        sbShow.AppendLine($"查无资料，请重新确认！");
                        DialogHandler.Instance.Fun_DialogShowOk(sbShow.ToString(), "警告资讯", Properties.Resources.dialogWarning, 2);
                        Dgv_CurrentRow.Focus();
                        return false;
                    }

                    ////Updated 時間格式    
                    //DateTime dteUpdated = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    //dtAfter.Rows[0]["UpdateDateTime"] = dteUpdated.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //dtAfter.Rows[0]["UpdatedProc"] = this.ProductName;

                    StringBuilder sbUpdate = new StringBuilder();
                    sbUpdate.AppendLine($"UPDATE [{nameof(TBL_LangSwitch_Nav)}]");
                    //sbUpdate.AppendLine($"SET [PKey] = '{}',");
                    sbUpdate.AppendLine($"SET [{nameof(TBL_LangSwitch_Nav.ZH)}] = '{strZH}', ");
                    sbUpdate.AppendLine($"    [{nameof(TBL_LangSwitch_Nav.EN)}] = '{strEN}',");
                    sbUpdate.AppendLine($"    [{nameof(TBL_LangSwitch_Nav.UpdateUser)}] ='{PublicForms.Main.lblLoginUser.Text}',");
                    sbUpdate.AppendLine($"    [{nameof(TBL_LangSwitch_Nav.UpdateDateTime)}] = '{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}'");
                    //sbUpdate.AppendLine($"    [{nameof(TBL_LangSwitch_Nav.UpdatedProc)}] = '{this.ProductName}'");
                    sbUpdate.AppendLine($"WHERE   ({nameof(TBL_LangSwitch_Nav.PKey)} = '{strPKey}')");

                    //DbHand.Fun_UpdateData(sbUpdate.ToString(), DbHand.strDBConnectString);
                    if (!string.IsNullOrEmpty(sbUpdate.ToString()))
                    {
                        if (!Data_Access.GetInstance().Fun_ExecuteQuery(sbUpdate.ToString(), GlobalVariableHandler.Instance.strConn_GPL, "储存TBL_LangSwitch_Nav", "5-5"))
                        {
                            DialogHandler.Instance.Fun_DialogShowOk($"修改TBL_LangSwitch_Nav失败", "修改TBL_LangSwitch_Nav", null, 3);
                            return false;
                        }
                    }
                    //Log & 事件紀錄
                    eventText = $"User:{PublicForms.Main.lblLoginUser.Text} 修改[{strPKey}]中英文 。 ";
                }

            }
            else
            {
                //if (action == Action.Delete)
                //{               
                //}
            }
            EventLogHandler.Instance.LogInfo("5-5", eventText, eventText);
            PublicComm.ClientLog.Info(eventText);
            PublicComm.akkaLog.Info(eventText);
            EventLogHandler.Instance.EventPush_Message(eventText);
                      
            return true;

        }

        #region Old Resource Fun
        private void UpdateResourceValue(string filePath, string newValue)
        {
            Hashtable resourceEntries = new Hashtable();

            //Get existing resources
            ResXResourceReader reader = new ResXResourceReader(filePath);
            if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Key.ToString().Contains(".Text"))
                    {
                        if (d.Value == null)
                            resourceEntries.Add(d.Key.ToString(), "");
                        else
                            resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                    }
                }
                reader.Close();
            }

            //Modify resources here...
            resourceEntries[queryKey] = newValue;

            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(filePath);

            foreach (String key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }

            resourceWriter.Generate();
            resourceWriter.Close();
        }
        private void GetResourceValue()
        {
            resourceMap = new Dictionary<string, string>();
            engKeyMap = new Dictionary<string, string>();
            chnKeyMap = new Dictionary<string, string>();

            ResXResourceReader rsxr_eng = new ResXResourceReader(engFile);
            ResXResourceReader rsxr_chn = new ResXResourceReader(chnFile);
            List<DictionaryEntry> chnList = rsxr_chn.Cast<DictionaryEntry>().Where(x => x.Key.ToString().Contains(".Text")).ToList();

            RegexOptions options = RegexOptions.None;
            Regex regex_eng = new Regex("[ ]{2,}", options);
            Regex regex_chn = new Regex("[　]{1,}", options);

            foreach (DictionaryEntry eng in rsxr_eng)
            {
                if (eng.Key.ToString().Contains(".Text"))
                {
                    DictionaryEntry chn = chnList.FirstOrDefault(x => x.Key.ToString() == eng.Key.ToString());
                    if (chn.Key != null && chn.Value != null)
                    {
                        string engValue = eng.Value.ToString().Replace(System.Environment.NewLine, string.Empty);
                        engValue = regex_eng.Replace(engValue, " ");

                        string chnValue = chn.Value.ToString().Replace(System.Environment.NewLine, string.Empty);
                        chnValue = regex_chn.Replace(chnValue, "");

                        resourceMap.Add(chnValue, engValue);
                        chnKeyMap.Add(chn.Key.ToString(), chnValue);
                        engKeyMap.Add(eng.Key.ToString(), engValue);
                    }
                }
            }

            var sortedDict = from entry in resourceMap orderby entry.Value ascending select entry;
            Dgv_Info.DataSource = sortedDict.ToArray();
        }

        private void Dgv_Info_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            //switch (e.Column.Name)
            //{
            //    case "Key":
            //        {
            //            e.Column.Width = 500;
            //            break;
            //        }

            //    case "Value":
            //        {
            //            e.Column.Width = 500;
            //            break;
            //        }
            //}
        }
        #endregion

       
    }
}
