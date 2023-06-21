using DBService;
using DBService.Repository.CoilScheduleDelete;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTblSleeve;
using GPLManager.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    /// <summary>
    /// ComboBox_Type
    /// </summary>
    public enum Cbo_Type
    {
        /// <summary>
        /// 計畫種類
        /// </summary>
        Plan_Sort,
        /// <summary>
        /// 入/出口套筒類型
        /// </summary>
        Sleeve_Type,
        /// <summary>
        /// 入/出口墊紙方式
        /// </summary>
        PAPER_REQ_CODE,
        /// <summary>
        /// 入/出口墊紙種類
        /// </summary>
        Paper_Type,
        /// <summary>
        /// 反修類型
        /// </summary>
        Rework_Type,
        /// <summary>
        /// 訂單/實際/外表面/內表面精度代碼
        /// </summary>
        Surface_Accuracy,
        /// <summary>
        /// 好面朝向
        /// </summary>
        Base_Surface,
        /// <summary>
        /// 開捲方向
        /// </summary>
        Uncoiler_Direction,
        /// <summary>
        /// 取樣要求
        /// </summary>
        Samp,
        /// <summary>
        /// 取樣位置
        /// </summary>
        SAMPLE_FRQN_CODE,
        /// <summary>
        /// 鋼卷來源
        /// </summary>
        Origin,
        /// <summary>
        /// 切邊要求/分捲標記
        /// </summary>
        Trim,
        /// <summary>
        /// 導帶使用
        /// </summary>
        Leader_Usage,
        /// <summary>
        /// 班次
        /// </summary>
        Shift,
        /// <summary>
        /// 班別
        /// </summary>
        Team,
        /// <summary>
        /// 最終卷標記
        /// </summary>
        End,
        /// <summary>
        /// 廢品標記
        /// </summary>
        Scrap,
        /// <summary>
        /// 卷曲方向
        /// </summary>
        Winding_Direction,
        /// <summary>
        /// 翻面標記
        /// </summary>
        FLIP,
        /// <summary>
        /// EventLogLevel
        /// </summary>
        EventLogLevel,
        /// <summary>
        /// 封鎖標記
        /// </summary>
        Hold,
        /// <summary>
        /// 系統 Client/Server
        /// </summary>
        System,
        /// <summary>
        /// 脫脂標記
        /// </summary>
        Skim,
        /// <summary>
        /// 拋光類型
        /// </summary>
        Polishing,
        /// <summary>
        /// 上次/指定研磨面
        /// </summary>
        GRINDING_SURFACE,
        /// <summary>
        /// 正反研磨标记
        /// </summary>
        Grand,
        /// <summary>
        /// 是否有油
        /// </summary>
        Oil,
        /// <summary>
        /// 是否用套筒
        /// </summary>
        SleeveUse,
        /// <summary>
        /// 實際開卷方向
        /// </summary>
        Decoiler,
        /// <summary>
        /// 權限等級
        /// </summary>
        Authority_Class,
        /// <summary>
        /// 缺陷表面區分
        /// </summary>
        DefectSid,
        /// <summary>
        /// 缺陷寬向位置
        /// </summary>
        DefectPosW,
        /// <summary>
        /// 缺陷程度
        /// </summary>
        DefectLevel,
        /// <summary>
        /// 回转方向
        /// </summary>
        Direction,
        /// <summary>
        /// 回退方式
        /// </summary>
        ReturnMode,
        /// <summary>
        /// 降速代碼
        /// </summary>
        Deceleration,
        /// <summary>
        /// 工序代码
        /// </summary>
        ProcessCode
    }
    public class ComboBoxIndexHandler
    {
        string strSql;

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly ComboBoxIndexHandler INSTANCE = new ComboBoxIndexHandler();
        }

        public static ComboBoxIndexHandler Instance { get { return SingletonHolder.INSTANCE; } }

        DataTable dt = new DataTable();
        public void SelectComboBoxItems(Cbo_Type _Type, ComboBox cbo)
        {
            strSql = SqlFactory.SelectComboBoxItems(_Type);
            Fun_SqlQuerry("ComboBox选项");

            if (dt.IsNull()) return;

            cbo.DisplayMember = _Type == Cbo_Type.Sleeve_Type ||
                           _Type == Cbo_Type.PAPER_REQ_CODE ||
                           _Type == Cbo_Type.Paper_Type ||
                            _Type == Cbo_Type.ProcessCode ||
                           _Type == Cbo_Type.Origin ? cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Value) : cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Text);

            cbo.ValueMember = nameof(TBL_ComboBoxItems.Cbo_Value);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }

        /// <summary>
        /// ComboBox选项
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void SelectComboBoxItems(Cbo_Type _Type, ComboBox cbo, bool bolCanNull = false)
        {
             strSql = SqlFactory.SelectComboBoxItems(_Type);
            Fun_SqlQuerry("ComboBox选项");

            if (bolCanNull)
            {
                //增加空白選項
                dt.Rows.Add(new object[] { });
                dt.AcceptChanges();
            }

            if (dt.IsNull()) return;

            cbo.DisplayMember = _Type == Cbo_Type.Sleeve_Type ||
                                _Type == Cbo_Type.PAPER_REQ_CODE ||
                                _Type == Cbo_Type.Paper_Type ||
                                _Type == Cbo_Type.Origin ||
                                _Type == Cbo_Type.Surface_Accuracy ? cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Value) : cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Text);

            cbo.ValueMember = nameof(TBL_ComboBoxItems.Cbo_Value);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dt;
        }

        /// <summary>
        /// ComboBox选项
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void SelectComboBoxItems(DataTable dtGetComboBoxItems, ComboBox cbo, string strValue = nameof(TBL_ComboBoxItems.Cbo_Value), string strDisplay = nameof(TBL_ComboBoxItems.Cbo_Text))
        {
            //string strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxItems(_Type);
            //DataTable dtGetComboBoxItems = DataAccess.Fun_SelectDate(strSql, $"[{ _Type}]ComboBox选项");

            if (dtGetComboBoxItems.IsNull()) return;

            cbo.DisplayMember = strDisplay;

            cbo.ValueMember = strValue;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dtGetComboBoxItems;
        }
        /// <summary>
        /// 搜尋套筒選項
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void SelectSleeve(ComboBox cbo)
        {
            strSql = SqlFactory.SelectSleeveComboBoxItems();
            Fun_SqlQuerry("套筒清单");
            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_LookupTable_Sleeve.Sleeve_Code);
            cbo.ValueMember = nameof(TBL_LookupTable_Sleeve.Sleeve_Code);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }

        /// <summary>
        /// 搜尋墊紙選項
        /// </summary>
        public void SelectPaper(ComboBox cbo)
        {
            strSql = SqlFactory.SelectPaperComboBoxItems();
            Fun_SqlQuerry("垫纸清单");
            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_LookupTable_Paper.Paper_Code);
            cbo.ValueMember = nameof(TBL_LookupTable_Paper.Paper_Code);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }
        /// <summary>
        /// 所有ComeBox選項
        /// </summary>
        /// <param name="cbo"></param>
        public void SelectComboBoxType(ComboBox cbo)
        {
            strSql = SqlFactory.SelectComboBoxType();
            Fun_SqlQuerry("全部ComboBox类型");
            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_ComboBoxItems.Spare);
            cbo.ValueMember = nameof(TBL_ComboBoxItems.Cbo_Type);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }
        public void Fun_SqlQuerry(string strMessage)
        {
            try
            {
                dt = Data_Access.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                //EventLogHandler.Instance.LogInfo(string.Empty, $"{strMessage}查询资料库", $"{strMessage}查询资料库成功");
                //PublicComm.ClientLog.Info($"訊息名稱:{strMessage}查詢資料庫 訊息:{strMessage}查詢資料庫成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug(string.Empty, $"{strMessage}查询资料库", $"{strMessage}查询资料库失败:{ex}");
                PublicComm.ClientLog.Info($"訊息名稱:{strMessage}查詢資料庫 訊息:{strMessage}查询资料库失败:{ex}");
            }
        }

        #region 刪除/退料代碼
       
        public void ComboBox_DeleteCode(ComboBox cbo)
        {
            strSql = SqlFactory.SelectDeleteCode();
            Fun_SqlQuerry("删除代码ComboBox选项");

            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name);
            cbo.ValueMember = nameof(L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }
        public void SelectDeleteUser(ComboBox cbo)
        {
            strSql = SqlFactory.SelectUserList();
            Fun_SqlQuerry("删除操作者ComboBox选项");
            if (dt == null) return;
            if (dt.Rows.Count.Equals(0)) return;
            cbo.DisplayMember = nameof(TBL_AuthorityData.User_ID);
            cbo.ValueMember = nameof(TBL_AuthorityData.User_ID);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }
        public void SelectCoilList(ComboBox cbo)
        {
            strSql = SqlFactory.SelectDeleteCoilList();
            Fun_SqlQuerry("钢卷号清单");
            if (dt == null) return;
            if (dt.Rows.Count.Equals(0)) return;
            cbo.DisplayMember = nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CoilNo);
            cbo.ValueMember = nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CoilNo);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }
        #endregion

        /// <summary>
        /// ComboBox選項說明
        /// </summary>
        /// <param name="_Type"></param>
        public void Fun_SelectComboBoxItemsSpare(Cbo_Type _Type, TextBox textBox)
        {
            textBox.Text = string.Empty;
            strSql = SqlFactory.SelectComboBoxSpare(_Type);
            Fun_SqlQuerry("选项说明");
            if (dt.IsNull()) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                textBox.Text += dt.Rows[i][0].ToString() + Environment.NewLine;
            }
        }

        public void Fun_SleeveSpareDisplay(TextBox textBox)
        {
            textBox.Text = string.Empty;
            strSql = SqlFactory.SelectSleeveComboBoxItems();
            Fun_SqlQuerry("套筒资料");

            if (dt.IsNull()) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strSleeveCode = dt.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code)].ToString() ?? string.Empty;

                string strMaterial = dt.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Material)].Equals("1") ? "钢套筒" :
                              dt.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Material)].Equals("2") ? "纸套筒" : string.Empty;

                string strSleeveWidth = dt.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Width)].ToString() ?? string.Empty;

                string strSleeveThick = dt.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Thick)].ToString() ?? string.Empty;

                string strSleeveWeight = dt.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Weight)].ToString() ?? string.Empty;

                textBox.Text += $"[{strSleeveCode}]-{strMaterial}  宽:{strSleeveWidth}mm  厚:{strSleeveThick}mm  重:{strSleeveWeight}kg" + Environment.NewLine;

            }
        }

        public void Fun_PaperSpareDisplay(TextBox textBox)
        {
            textBox.Text = string.Empty;
            strSql = SqlFactory.SelectPaperComboBoxItems();
            Fun_SqlQuerry("衬纸资料");

            if (dt.IsNull()) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strPaperCode = dt.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code)].ToString() ?? string.Empty;

                string strPaperBaseWt = dt.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Base_Weight)].ToString() ?? string.Empty;

                string strPaperWidth = dt.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Width)].ToString() ?? string.Empty;

                string strPaperThick = dt.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Thick)].ToString() ?? string.Empty;

                string strPaperMaxThick = dt.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Max_Thick)].ToString() ?? string.Empty;

                string strPaperMinThick = dt.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Min_Thick)].ToString() ?? string.Empty;

                textBox.Text += $"[{strPaperCode}] 基重:{strPaperBaseWt}g  宽:{strPaperWidth}mm  厚:{strPaperThick}mm  最大厚:{strPaperMaxThick}mm  最小厚:{strPaperMinThick}mm" + Environment.NewLine;

            }
        }

        /// <summary>
        /// 客戶品質等級群組
        /// </summary>
        /// <param name="cbo"></param>
        public void SelectComboBoxItemsGradeGroups(ComboBox cbo)
        {
            strSql = SqlFactory.SelectGradeGroup();
            Fun_SqlQuerry("客户品质等级群组");

            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_GradeGroups.GradeGroup);
            cbo.ValueMember = nameof(TBL_GradeGroups.GradeGroup);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }
        /// <summary>
        /// 客戶品質等級群組-鋼種
        /// </summary>
        /// <param name="cbo"></param>
        public void SelectComboBoxItemsST(ComboBox cbo)
        {
            strSql = SqlFactory.SelectGradeGroup_StNo();
            Fun_SqlQuerry("客户品质等级群组-钢种");
            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_GradeGroups.SteelGrade);
            cbo.ValueMember = nameof(TBL_GradeGroups.SteelGrade);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
            
        }
        /// <summary>
        /// 客戶品質等級群組-客戶代碼
        /// </summary>
        /// <param name="cbo"></param>
        public void SelectComboBoxItemsCustomer(ComboBox cbo)
        {
            strSql = SqlFactory.SelectGradeGroup_Custmer();
            Fun_SqlQuerry("客户品质等级群组-客户代码");
            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_GradeGroups.CustomerNo);
            cbo.ValueMember = nameof(TBL_GradeGroups.CustomerNo);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
           
        }
        /// <summary>
        /// 研磨模板
        /// </summary>
        /// <param name="cbo"></param>
        public void SelectComboBoxItemsBeltPattern(ComboBox cbo)
        {
            strSql = SqlFactory.SelectBeltPatterns();
            Fun_SqlQuerry("研磨模板");

            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_BeltPatterns.BeltPattern);
            cbo.ValueMember = nameof(TBL_BeltPatterns.BeltPattern);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
           
        }
        /// <summary>
        /// 皮帶種類
        /// </summary>
        /// <param name="cbo"></param>
        public void SelectComboBoxItemsBeltMaterials(ComboBox cbo)
        {
            strSql = SqlFactory.SelectBeltMaterials();
            Fun_SqlQuerry("皮带种类");
            if (dt.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_BeltMaterials.MATERIAL_CODE);
            cbo.ValueMember = nameof(TBL_BeltMaterials.MATERIAL_CODE);
            cbo.DataSource = dt;
            cbo.SelectedValue = string.Empty;
        }

        public int Fun_GetDropDownWidth(ComboBox cob)
        {
            int intWidth = cob.Width;
            int intMaxWidth = 0;
            int intTemp = 0;
            Label lblText = new Label();
            lblText.Font = cob.Font;
            foreach (DataRowView obj in cob.Items)
            {
                lblText.Text = obj.Row[nameof(TBL_ComboBoxItems.Cbo_Text)].ToString();
                intTemp = lblText.PreferredWidth;
                if (intTemp > intMaxWidth)
                {
                    intMaxWidth = intTemp;
                }
            }
            lblText.Dispose();

            if (intWidth >= intMaxWidth)
            {
                return intWidth;
            }
            else
            {
                return intMaxWidth + 15;//15 预留ScrollBar 会占到的宽度 
            }

        }

        /// <summary>
        /// 從指定資料表,取得ComboBox初始設定(顯示的Text,值),可群組排序,可不顯示Value
        /// </summary>
        /// <param name="cb">要設定的ComboBox</param>
        /// <param name="strSelectKey">資料來源指定Value欄位</param>
        /// <param name="strSelectShow">資料來源指定Text欄位</param>
        /// <param name="dtList">資料來源</param>
        /// <param name="bolGroup">true:群組+排序 ; false:不群組排序</param>
        /// <param name="bolShowValueText">true:[Value] Text ; false:Text</param>
        public void Fun_CobDataFromTable(ComboBox cb, string strSelectKey, string strSelectShow, DataTable dtList, bool bolGroup = false, bool bolShowValueText = false)
        {
            DataTable dtCombData = new DataTable();

            if (bolGroup)
            {
                DataTable dtBeforOder = dtList.Copy();

                dtBeforOder = dtBeforOder.AsEnumerable()
                                         .GroupBy(r => new { Col1 = r[strSelectKey] })
                                         .Select(g => g.OrderBy(r => r[strSelectKey]).First())
                                         .CopyToDataTable();
                //先排序
                DataView dv = dtBeforOder.DefaultView;
                dv.Sort = strSelectKey + " ASC ";
                dtCombData = dv.ToTable();
            }
            else
            {
                dtCombData = dtList.Copy();
            }

            List<string> strList = new List<string>();
            if (cb.Items.Count > 0)
            {
                cb.Items.Clear();
            }

            if (dtCombData != null)
            {
                for (int i = 0; i < dtCombData.Rows.Count; i++)
                {
                    string strCOBVALUE = dtCombData.Rows[i][strSelectKey].ToString();
                    string strCOBTEXT;
                    if (bolShowValueText)
                    {
                        strList.Add("[" + strCOBVALUE + "] " + dtCombData.Rows[i][strSelectShow].ToString());
                        strCOBTEXT = "[" + strCOBVALUE + "] " + dtCombData.Rows[i][strSelectShow].ToString();
                    }
                    else
                    {
                        strList.Add(strCOBVALUE);
                        strCOBTEXT = dtCombData.Rows[i][strSelectShow].ToString();
                    }

                    cb.Items.Add(new ComboboxItem(strCOBTEXT, strCOBVALUE));
                }

            }

            //cb.DataSource = strList.ToArray();
            cb.AutoCompleteCustomSource.Clear();
            cb.AutoCompleteCustomSource.AddRange(strList.ToArray());
            cb.DropDownStyle = ComboBoxStyle.DropDownList;//.DropDown;
            cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;//.SuggestAppend;
            cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;

        }

        private class ComboboxItem
        {
            public ComboboxItem(string text, string value)
            {
                Value = value;
                Text = text;
            }
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }
    }
}
