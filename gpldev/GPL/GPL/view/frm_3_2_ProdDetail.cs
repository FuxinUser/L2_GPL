using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using DataModel.HMIServerCom.Msg;
using Akka.Actor;
using static GPLManager.DataBaseTableFactory;
using System.Linq;
using DBService.Repository.PDO;
using GPLManager.Util;
using System.Text;
using DBService.Repository.LangSwitch;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTblSleeve;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPLManager
{
    public partial class Frm_3_2_ProdDetail : Form
    {
        #region 變數

        //權限設定 功能Btn
        Control[] CtlAuthority;
        //權限設定 欄位不開放
        Control[] CtlNotOpen;
        //權限設定 欄位開放
        Control[] CtlOpen;
        //所有欄位頁籤
        Control[] CtrControlArray;
        //主鍵
        Control[] CtlKeyArray;

        private DataTable dtCoilData;//Cbo_Matcoilid

        string str_Now_FinishTime = "";

        private DataTable dtSelectOne;
        private DataTable dtSelectOne_Def;
        private DataTable dtBeforeEdit;
        private DataTable dtBeforeEdit_Def;
        private string strEditStatus = "Read";
        private bool bolEditStatus = false;
        private bool bolDefectIsNull;
        private bool bolPdoIsNull;

        //語系
        private LanguageHandler LanguageHand;
        DataTable dtLangSwitch_Column;
        #endregion

        public Frm_3_2_ProdDetail()
        {
            InitializeComponent();
        }

        private void Frm_3_2_ProdDetail_Load(object sender, System.EventArgs e)
        {
            PublicForms.PDODetail = this;
            Control[] Frm_3_2_Control = new Control[] {         
            Btn_New,//新增
            Btn_Update,//修改
            Btn_MMS,//上傳MMS
            Btn_PrintTag//列印標籤
            }; //  btn_CopyNew,//複製新增
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_3_2_Control, UserSetupHandler.Instance.Frm_3_2);

            //權限控制項設定
            Fun_InitialAuthority();

            ReadOnlyControl(Tab_PDODetailPage, true);
            ReadOnlyControl(Tab_PDODefectPage, true);
            ////出口鋼卷號ComboBox
            //Fun_Out_mat_ID_ComboBox();
            //combobox欄位
            Fun_ComboBoxItems();
            //缺陷資料說明欄
            TxtDescription();

            //取得栏位
            string strSql = SqlFactory.Frm_3_2_SelectData_DB_PDO("");
            dtSelectOne = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDO资料", "3-2");
            string strSql_Def = SqlFactory.Frm_3_2_SelectDefectData_DB_PDO("");
            dtSelectOne_Def = Data_Access.Fun_SelectDate(strSql_Def, GlobalVariableHandler.Instance.strConn_GPL, "PDO缺陷资料", "3-2");
           
            if (dtSelectOne == null || dtSelectOne.Rows.Count <= 0)
            {
                if (UserSetupHandler.Instance.Frm_3_2.Equals("W"))
                {
                    //新增
                    Fun_SetBottonEnabled(Btn_New, true);
                    //修改
                    Fun_SetBottonEnabled(Btn_Update, false);
                    //上传MMS
                    Fun_SetBottonEnabled(Btn_MMS, true);
                    //列印標籤
                    Fun_SetBottonEnabled(Btn_PrintTag, true);
                }
                
            }

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

            Fun_GetColumnShowText();
           
        }

        private void Frm_3_2_ProdDetail_Shown(object sender, EventArgs e)
        {
           
        }

        private void Fun_InitialAuthority()
        {
            //權限設定 功能Btn
            CtlAuthority = new Control[] { Btn_New, Btn_Update, Btn_Save, Btn_Cancel, Btn_PrintTag, Btn_MMS };
            //權限設定 欄位不開放
            CtlNotOpen = new Control[] {
                        /*合同号*/          Txt_Order_No,
                        /*计划号*/          Txt_Plan_No,
                        /*出口钢卷号*/      Txt_Out_Coil_ID,
                        /*入口钢卷号*/      Txt_In_Coil_ID,
                        /*生产开始时间*/    Dtp_StartTime,
                        /*生产结束时间*/    Dtp_FinishTime,
                        /*班次*/            Cob_Shift,
                        /*班别*/            Cob_Team,
                        /*钢种代码*/        Txt_St_No,
                        /*出口卷外径*/      Txt_Out_Coil_Outer_Diameter,
                        /*出口卷内径*/      Txt_Out_Coil_Inner_Diameter,
                        /*出口卷长度*/      Txt_Out_Coil_Length,
                        /*出口厚度          Txt_Out_Coil_Thick,*/
                        /*出口宽度*/        Txt_Out_Coil_Width,

                        /*出口套筒内径      Txt_Sleeve_Inner_Exit_Diamter,*/
                        /*出口套筒类型      Cob_Sleeve_Type_Exit,*/
                        /*卷曲方向*/        Cob_Winding_Direction,

                        /*头部打孔位置*/    Txt_Head_Hole_Position,
                        /*头部导带长度*/    Txt_Head_Leader_Length,
                        /*头部导带宽度*/    Txt_Head_Leader_Width,
                        /*头部导带厚度*/    Txt_Head_Leader_Thickness,
                        /*头部导带钢种*/    Txt_Head_Leader_St_No,  
                        
                        /*尾部打孔位置*/    Txt_Tail_PunchHole_Position,
                        /*尾部导带长度*/    Txt_Tail_Leader_Length,
                        /*尾部导带宽度*/    Txt_Tail_Leader_Width,
                        /*尾部导带厚度*/    Txt_Tail_Leader_Thickness,
                        /*尾部导带钢种*/    Txt_Tail_Leader_St_No,

                        /*钢卷上次研磨面*/  Cob_Pre_Grinding_Surface,
                        /*头部未轧制区域*/  Txt_Head_Off_Gauge,
                        /*尾部未轧制区域*/  Txt_Tail_Off_Gauge,

                        /*好面朝向          Cob_Base_Surface,*/
                        /*表面精度代码      Cob_Surface_Accuracy_Code,*/
                        /*内表面精度代码    Cob_Surface_Accu_Code_In,*/
                        /*外表面精度代码    Cob_Surface_Accu_Code_Out,*/
                       
                        /*工序工艺码*/      Cob_ProcessCode  };

            //權限設定 欄位開放
            CtlOpen = new Control[] { };
            //所有欄位頁籤
            CtrControlArray = new Control[] { Tab_PDODetailPage, Tab_PDODefectPage };
            //主鍵 Key
            CtlKeyArray = new Control[] { 
                /*计划号*/ Txt_Plan_No, 
                /*出口钢卷号*/ Txt_Out_Coil_ID, 
                /*入口钢卷号*/ Txt_In_Coil_ID, 
                /*生产结束时间*/ Dtp_FinishTime };
        }

        /// <summary>
        /// 取得画面栏位中英显示文字
        /// </summary>
        private void Fun_GetColumnShowText()
        {
            string strSql = SqlFactory.Frm_3_2_Select_LangSwitch_Ctr_List("Frm_3_2_ProdDetail");
            dtLangSwitch_Column = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDO画面栏位清单", "3-2");
        }

        private void Fun_Out_mat_ID_ComboBox()
        {
            string strSql = SqlFactory.Frm_3_2_CoilIDComboBox_DB_PDO();
            dtCoilData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,$"出口卷号清单","3-2");

            if (dtCoilData.IsNull())
            { 
                EventLogHandler.Instance.EventPush_Message("无出口卷清单");
                return;
            }

            Cob_Matcoilid.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Matcoilid.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Matcoilid.DataSource = dtCoilData;
        }

        private void Fun_ComboBoxItems()
        {
            //班次
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift);
            //班別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_Team);
            //出口墊紙種類
            ComboBoxIndexHandler.Instance.SelectPaper(Cob_Paper_Code);
            //出口墊紙方式
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_Paper_Req_Code);
            //出口套筒種類
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Exit);
            //好面朝向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Base_Surface, Cob_Base_Surface);
            //封鎖標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Hold, Cob_Hold_Flag);
            //取樣標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Samp, Cob_Sample_Flag);
            //取樣位置 
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.SAMPLE_FRQN_CODE, Cob_Sample_Frqn_Code);
            //分卷標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Trim, Cob_Fixed_WT_Flag);
            //最終卷標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.End, Cob_End_Flag);
            //廢品標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Scrap, Cob_Scrap_Flag);
            //表面精度代碼
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accuracy_Code);
            //內表面精度代碼
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accu_Code_In);
            //外表面精度代碼
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accu_Code_Out);
            //卷曲方向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Winding_Direction, Cob_Winding_Direction);
            //上次研磨面
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.GRINDING_SURFACE, Cob_Pre_Grinding_Surface);
            //指定研磨面
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.GRINDING_SURFACE, Cob_Appoint_Grinding_Surface);
            //工序代码
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.ProcessCode, Cob_ProcessCode);
            //是否有油
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Oil, Cob_Oil_Flag);
            //是否使用套筒
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.SleeveUse, Cob_Out_Coil_Use_Sleeve_Flag);
            //實際開卷方向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Decoiler, Cob_Decoiler_Direction);

            #region -缺陷資料ComboBox设定-

            #region - Level -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D10);
            #endregion

            #region - Sid -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D10);
            #endregion

            #region - PosW -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D10);
            #endregion

            #endregion
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (strEditStatus != "Read")
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", null, 0);
                return;
            }
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", null, 0);
                return;
            }

            //if (!Cbo_Matcoilid.Text.IsEmpty())
            //    Fun_SelectPDO(Cbo_Matcoilid.Text);
            if (!Txt_Search_Out_Coil_ID.Text.IsEmpty())
                Fun_SelectCoilPDO(Txt_Search_Out_Coil_ID.Text);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }
            //紀錄是否編輯,狀態
            strEditStatus = "New";
            bolEditStatus = true;

            //先備份之前顯示的資料
            dtBeforeEdit = dtSelectOne.Copy();
            dtBeforeEdit_Def = dtSelectOne_Def.Copy();

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //修改
            Fun_SetBottonEnabled(Btn_Update, false);
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, true);
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true;

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDODetailPage, false);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDODefectPage, false);

            //计划号
            Txt_Plan_No.Text = string.Empty;
            //出口鋼卷號
            Txt_Out_Coil_ID.Text = string.Empty;
            Lbl_OutCoil_Defect.Text = string.Empty;

            //分卷标记
            Cob_Fixed_WT_Flag.SelectedIndex = -1;
            //最终卷标记
            Cob_End_Flag.SelectedIndex = -1;

            //Btn淨重計算 啟用
            Btn_Act_WT_Count.Enabled = true;

            //EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 點新增按鈕", "新增动作");

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }

            //修改前先備份資料
            dtBeforeEdit = dtSelectOne.Copy();
            dtBeforeEdit_Def = dtSelectOne_Def.Copy();

            ReadOnlyControl(Tab_PDODetailPage, false);
            ReadOnlyControl(Tab_PDODefectPage, false);

            // UserSetupHandler.Instance.Authority_Class : 1 = Administator ; 2 = Manager ; 3 : Operator 
            if (UserSetupHandler.Instance.Authority_Class != "1")
            {
                ReadOnlyHandler.Instance.ReadOnly(CtlNotOpen, true);
            }

            // 修改状态 key不可改
            //计划号
            Txt_Plan_No.ReadOnly = true;
            Txt_Plan_No.BackColor = Color.Gainsboro;
            //出口鋼卷號
            Txt_Out_Coil_ID.ReadOnly = true;
            Txt_Out_Coil_ID.BackColor = Color.Gainsboro;
            //入口鋼卷號
            Txt_In_Coil_ID.ReadOnly = true;
            Txt_In_Coil_ID.BackColor = Color.Gainsboro;
            ////生產結束時間
            //Dtp_FinishTime.Enabled = false;

            //Btn淨重計算 啟用
            Btn_Act_WT_Count.Enabled = true;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //修改
            Fun_SetBottonEnabled(Btn_Update, true);
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, true);
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //取消
            Btn_Cancel.Visible = true;
            //儲存
            Btn_Save.Visible = true; 

            //紀錄是否編輯,狀態
            strEditStatus = "Edit";
            bolEditStatus = true;
            //if (Txt_Out_mat_No.Text.IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message($"请先查询欲修改之钢卷");
            //    return;
            //} 
            //Fun_CheckUploadFlg();
        }

        private void Btn_Act_WT_Count_Click(object sender, EventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }


            /*
             3-2 PDO畫面在輸入導帶資料後，需要重新計算淨重，

             要新增計算淨重按鈕，按鈕放在淨重旁
              淨重 = pdo毛重(Out_Coil_Groo_WT)-(墊紙重量 + 套桶重量 + 頭段導帶重量 + 尾段導帶重量)
                                                23.2208  + 直接用(不用算)
           */


            //pdo毛重(Out_Coil_Groo_WT)     毛重:Txt_Out_Coil_Gross_WT
            float flo_Out_Coil_Groo_WT ;
            bool bol_Groo_WT =  float.TryParse( Txt_Out_Coil_Gross_WT.Text, out flo_Out_Coil_Groo_WT);

            // =================================
            /*
                襯紙重 計算Ex:
                襯紙方式:[1]整卷墊 , 
                襯紙類型:[02]基重(32 g/m2)
                出口長(575m) 
                襯紙寬(1262mm) 

                襯紙重 = 出口長(m) *寬(m)   *基重(g/m2)
                            575    * 1.262 *32 = 23200.8
                23200.8 / 1000 = 23.2208 (kg)
            */
            //襯紙重 (kg)
            float flo_PaperWt = 0;
            //有使用墊紙 才依墊紙方式 & 墊紙類型  取得 墊紙重量
            if (Cob_Paper_Req_Code.SelectedIndex > 0)
            {
                //出口卷长度:Txt_Out_Coil_Length
                float flo_Out_Coil_Length ;
                float.TryParse(Txt_Out_Coil_Length.Text, out flo_Out_Coil_Length);

                //出口卷宽度:Txt_Out_Coil_Width(mm)
                float flo_Out_Coil_Width ;
                float.TryParse(Txt_Out_Coil_Width.Text, out flo_Out_Coil_Width);

                //出口垫纸方式:Cob_Paper_Req_Code
                string strPaper_Req_Code = Cob_Paper_Req_Code.Text;

                // 依垫纸方式 取  墊紙長度(m)
                float flo_PaperLength = Fun_GetPaperLength(strPaper_Req_Code, flo_Out_Coil_Length);

                //出口垫纸类型:Cob_Paper_Code
                string strPaper_Code = Cob_Paper_Code.Text;
                //依墊紙類型 取得 基重
                float flo_Base_WT = Fun_GetPaperBaseWt(strPaper_Code);

                //襯紙重 (kg)
                flo_PaperWt = ((flo_PaperLength) * (flo_Out_Coil_Width / 1000) * flo_Base_WT) / 1000;
            }
           

            //================================================ 
            //套筒重量 
            float flo_Sleeve_WT = 0;
            //出口套筒使用否: Cob_Out_Coil_Use_Sleeve_Flag
            //有使用 才依套筒類型 取得 套筒重量
            if (Cob_Out_Coil_Use_Sleeve_Flag.SelectedIndex == 0)
            {
                //出口套筒类型 : Cob_Sleeve_Type_Exit
                string str_Sleeve_Type_Exit = Cob_Sleeve_Type_Exit.Text;

                //依套筒類型 取得 套筒重量           
                flo_Sleeve_WT = Fun_GetSleeveWt(str_Sleeve_Type_Exit);
            }

            //   =================================
            /*
                           導帶重量 計算Ex:
                           導帶長度(m)
                           導帶寬度(mm) /1000 => (m)
                           導帶厚度(mm) /1000 => (m)

                           抓PDI 密度(Density)(kg/m³)

                           導帶重量 = 導帶長度(m)*導帶寬度(m)*導帶厚度(m)*密度(kg/m³)
             */
            //導帶長度(m)
            float flo_Head_Leader_Length    , flo_Tail_Leader_Length;
            //導帶寬度(mm)
            float flo_Head_Leader_Width     , flo_Tail_Leader_Width;
            //導帶厚度(mm)
            float flo_Head_Leader_Thickness , flo_Tail_Leader_Thickness;
            //密度(Density)(g/m³)
            float flo_Head_Leader_Density   , flo_Tail_Leader_Density;
                      
            float.TryParse(Txt_Head_Leader_Length.Text, out flo_Head_Leader_Length);
            float.TryParse(Txt_Head_Leader_Width.Text, out flo_Head_Leader_Width);
            float.TryParse(Txt_Head_Leader_Thickness.Text, out flo_Head_Leader_Thickness);           
            flo_Head_Leader_Density = Fun_GetLeader_Density(Txt_Head_Leader_St_No.Text, true);           

            float.TryParse(Txt_Tail_Leader_Length.Text, out flo_Tail_Leader_Length);
            float.TryParse(Txt_Tail_Leader_Width.Text, out flo_Tail_Leader_Width);
            float.TryParse(Txt_Tail_Leader_Thickness.Text, out flo_Tail_Leader_Thickness);  
            flo_Tail_Leader_Density = Fun_GetLeader_Density(Txt_Tail_Leader_St_No.Text, false);

            float flo_Head_Leader_Wt = flo_Head_Leader_Length * (flo_Head_Leader_Width / 1000) * (flo_Head_Leader_Thickness / 1000) * (flo_Head_Leader_Density );
            float flo_Tail_Leader_Wt = flo_Tail_Leader_Length * (flo_Tail_Leader_Width / 1000) * (flo_Tail_Leader_Thickness / 1000) * (flo_Tail_Leader_Density );

            //淨重 = pdo毛重(Out_Coil_Groo_WT) - (墊紙重量 + 套桶重量 + 頭段導帶重量 + 尾段導帶重量)
            float flo_Act_WT =  flo_Out_Coil_Groo_WT - (flo_PaperWt + flo_Sleeve_WT + flo_Head_Leader_Wt+ flo_Tail_Leader_Wt);

            double douWT = 0;
            double.TryParse(flo_Act_WT.ToString(), out douWT);

            //淨重:Txt_Out_Coil_Act_WT    四捨五入:Round    無條件捨去:Floor
            Txt_Out_Coil_Act_WT.Text = Math.Round(douWT).ToString();

        }

        /// <summary>
        /// 依墊紙方式取得墊紙長度
        /// </summary>
        /// <param name="strPaper_Req_Code">墊紙方式代碼</param>
        /// <param name="flo_Out_Coil_Length">出口卷长度</param>
        /// <returns></returns>
        private float Fun_GetPaperLength(string strPaper_Req_Code,float flo_Out_Coil_Length)
        {
            float flo_PaperLength = 0;
            switch (strPaper_Req_Code)
            {
                //不覆膜/不墊紙
                case "0":
                    flo_PaperLength = 0;
                    break;
                //整卷墊
                case "1":
                    flo_PaperLength = flo_Out_Coil_Length;
                    break;
                //頭尾端各50米
                case "2":
                    flo_PaperLength = 100;
                    break;
                //頭尾端各30米
                case "3":
                    flo_PaperLength = 60;
                    break;
                //尾端80米
                case "4":
                    flo_PaperLength = 80;
                    break;  
                //尾端200米
                case "5":
                    flo_PaperLength = 200;
                    break;
                default:
                    flo_PaperLength = 0;
                    break;
            }
            return flo_PaperLength;
        }

        /// <summary>
        /// 依墊紙類型取得墊紙基重
        /// </summary>
        /// <param name="strPaper_Code">墊紙類型代碼</param>       
        /// <returns></returns>
        private float Fun_GetPaperBaseWt(string strPaper_Code)
        {
            float flo_PaperBaseWt = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"SELECT * ");
            sb.AppendLine($"FROM  {nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper)}");
            sb.AppendLine($"WHERE {nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code)} = '{strPaper_Code}'");
            sb.AppendLine($"");
         
            string strSql = sb.ToString();
            DataTable dtData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "LkUpTablePaperData", "3-2");
            //SqlFactory.Frm_3_2_SelectData_DB_PDO(Coil_ID, strPlan_No, strIn_Coil_ID);          

            if (!dtData.IsNull())
            {
                string strPaper_Base_Weight = dtData.Rows[0][
                    nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Base_Weight)].ToString();
                float.TryParse(strPaper_Base_Weight, out flo_PaperBaseWt);
            }
            return flo_PaperBaseWt;
        }

        /// <summary>
        /// 依套筒代碼取得套筒重量
        /// </summary>
        /// <param name="strSleeve_Code">套筒代碼</param>
        /// <returns></returns>
        private float Fun_GetSleeveWt(string strSleeve_Code)
        {
            float flo_SleeveWt = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"SELECT * ");
            sb.AppendLine($"FROM  {nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve)}");
            sb.AppendLine($"WHERE {nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code)} = '{strSleeve_Code}'");
            sb.AppendLine($"");

            string strSql = sb.ToString();
            DataTable dtData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "LkUpTableSleeveData", "3-2");
            //SqlFactory.Frm_3_2_SelectData_DB_PDO(Coil_ID, strPlan_No, strIn_Coil_ID);          

            if (!dtData.IsNull())
            {
                string strSleeve_Weight = dtData.Rows[0][
                    nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Weight)].ToString();
                float.TryParse(strSleeve_Weight, out flo_SleeveWt);
            }
            return flo_SleeveWt;
        }

        /// <summary>
        /// 依導帶鋼種取得導帶密度
        /// </summary>
        /// <param name="strLeader_St_No">導帶鋼種</param>
        /// <param name="bolIsHead">是否為頭段導帶</param>
        /// <returns></returns>
        private float Fun_GetLeader_Density(string strLeader_St_No,bool bolIsHead)
        {
            float flo_Leader_Density ;          
            string strLeaderType = bolIsHead ? Grb_HeadLeader.Text : Grb_TailLeader.Text;
            switch (strLeader_St_No)
            {
                case "301":
                    flo_Leader_Density = bolIsHead ? 7930 : 7930; break;
                case "304":
                    flo_Leader_Density = bolIsHead ? 7930: 7930;  break;               
                case "443":
                    flo_Leader_Density = bolIsHead ? 7740 : 7740; break;             
                case "430":
                    flo_Leader_Density = bolIsHead ? 7700 : 7700; break;
               
                default:
                    PublicComm.ClientLog.Debug($"{strLeaderType} 鋼種:{strLeader_St_No}，查無導帶密度");
                    flo_Leader_Density = bolIsHead ? 0 : 0;
                    break;
            }
            return flo_Leader_Density;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //檢查必填欄位            
            if (!Fun_IsColumnsEmpty()) { return; }

            string strOut_Coil_ID = Txt_Out_Coil_ID.Text.Trim();//1                
            string strFinishTime;//= Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss");//2
            //strFinishTime += $".{Dtp_FinishTime.Value.Millisecond}" ;

            //FinishTime_Str
            strFinishTime = str_Now_FinishTime;//取得之前紀錄 的FinishTime_fff

            string strIn_Coil_ID = Txt_In_Coil_ID.Text.Trim();//3
            string strPlan_No = Txt_Plan_No.Text.Trim();//4
           
            string strCheckSql_PDO = SqlFactory.Frm_3_2_SelectData_DB_PDO(strOut_Coil_ID, strPlan_No, strIn_Coil_ID);
            DataTable dt_Check_PDO = Data_Access.Fun_SelectDate(strCheckSql_PDO,GlobalVariableHandler.Instance.strConn_GPL, "PDO资料");
            bolPdoIsNull = dt_Check_PDO.IsNull();

            //新增
            if (Btn_New.Enabled)
            {
                if (bolPdoIsNull)
                {
                    //無此PDO 可新增
                }
                else
                {
                    //已有PDO 不可新增
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"新增PDO资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");
                    sb.AppendLine($"    出口钢卷号:{strOut_Coil_ID}");
                    sb.AppendLine($"    入口钢卷号:{strIn_Coil_ID}");
                   // sb.AppendLine($"    生产结束时间:{strFinishTime}");
                    sb.AppendLine($"资料已重复，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "新增PDO", null, 3);
                    return;
                }
            }
            else if (Btn_Update.Enabled)
            {
                if (bolPdoIsNull)
                {
                    //無此PDO 無法更新資料
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"修改PDO资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");
                    sb.AppendLine($"    出口钢卷号:{strOut_Coil_ID}");
                    sb.AppendLine($"    入口钢卷号:{strIn_Coil_ID}");
                   // sb.AppendLine($"    生产结束时间:{strFinishTime}");
                    sb.AppendLine($"查无资料，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "修改PDO",null, 3);
                    return;
                }
                else
                {
                    //有此PDO 可修改
                }
            }

            string strCheckSql = SqlFactory.Frm_3_2_SelectDefectData_DB_PDO(strOut_Coil_ID, strPlan_No);
            DataTable dt_Check_Defect = Data_Access.Fun_SelectDate(strCheckSql, GlobalVariableHandler.Instance.strConn_GPL, "PDO缺陷资料");

            bolDefectIsNull = dt_Check_Defect.IsNull();

            string strSql = string.Empty;
            //string strSql_Defect = string.Empty;
            string strSql_Defect = string.Empty;
            DialogResult dialogResult_Save;
            string strFinDeff = "";
            int intEditCount = 0;
            if (Btn_New.Enabled) 
            {
                //PDO(含缺陷)
                strSql = SqlFactory.Frm_3_2_InsertPDO_DB_PDO(); 
            }
            else if (Btn_Update.Enabled) 
            {
                Fun_GetUpdateDataToTable(dtSelectOne);
                Fun_GetUpdateDataToTable_Def(dtSelectOne_Def);
                StringBuilder sbFinDeff = new StringBuilder();
                sbFinDeff.AppendLine($"出口钢卷编号:{strOut_Coil_ID}修改");//strFinishTime, strIn_Coil_ID,strPlan_No
                foreach (DataRow dr in dtBeforeEdit.Rows)
                {
                    foreach (DataRow drS in dtSelectOne.Rows)
                    {
                        foreach (DataColumn dc in dtBeforeEdit.Columns)
                        {
                            foreach (DataColumn dcS in dtSelectOne.Columns)
                            {
                                #region 排除不在畫面上的欄位(內部使用)
                                if (dc.ToString() == "CreateTime") continue;
                                if (dc.ToString() == "Coil_Check_Time") continue;
                                if (dc.ToString() == "Coil_Check_Result") continue;
                                if (dc.ToString() == "PDO_Uploaded_Flag") continue;
                                if (dc.ToString() == "PDO_Uploaded_Time") continue;
                                if (dc.ToString() == "PDO_Uploaded_UserID") continue;
                                if (dc.ToString() == "Exit_Scaned_CoilID") continue;
                                if (dc.ToString() == "Exit_Scaned_UserID") continue;
                                if (dc.ToString() == "Exit_ExportTime") continue;
                                #endregion

                                if (dc.ToString() == dcS.ToString())
                                {
                                    if (dr[dc].ToString() != drS[dcS].ToString())
                                    {
                                        string strTitle = Fun_GetTitle(dc.ColumnName);
                                        sbFinDeff.AppendLine($"{strTitle} : {dr[dc]} => {drS[dcS]}");
                                        intEditCount += 1;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (DataRow dr in dtBeforeEdit_Def.Rows)
                {
                    foreach (DataRow drS in dtSelectOne_Def.Rows)
                    {
                        foreach (DataColumn dc in dtBeforeEdit_Def.Columns)
                        {
                            foreach (DataColumn dcS in dtSelectOne_Def.Columns)
                            {
                               
                                if (dc.ToString() == dcS.ToString())
                                {
                                    if (dr[dc].ToString() != drS[dcS].ToString())
                                    {
                                        string strTitle = Fun_GetTitle_Def(dc.ColumnName);
                                        sbFinDeff.AppendLine($"{strTitle} : {dr[dc]} => {drS[dcS]}");
                                        intEditCount += 1;
                                    }
                                }
                            }
                        }
                    }

                }


                strFinDeff = sbFinDeff.ToString();

                if (intEditCount > 0)
                {
                    dialogResult_Save = DialogHandler.Instance.Fun_DialogShowOkCancel(strFinDeff, "修改PDO", null, 1);
                    if (dialogResult_Save == DialogResult.OK)
                    {
                        ////PDO
                        //strSql = Frm_3_2_SqlFactory.SQL_Update_PDO(strOut_Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
                        //str_Now_FinishTime = strFinishTime;
                        ////缺陷
                        //strSql_Defect = bolDefectIsNull ? Frm_3_2_SqlFactory.SQL_Insert_DefectData() : Frm_3_2_SqlFactory.SQL_Update_DefectData(Txt_Out_Coil_ID.Text, strPlan_No);

                       // if (intEditCount > 0)
                            strSql = SqlFactory.Frm_3_2_SavePDO_DB_PDO(Txt_Out_Coil_ID.Text, Txt_Plan_No.Text, Txt_In_Coil_ID.Text);
                    }
                }

            }

            //if (string.IsNullOrEmpty(strSql)) return;

            if (!string.IsNullOrEmpty(strSql))
            {
                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "储存PDO", "3-2"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"新增/修改PDO失败", "新增/修改PDO", null, 3);
                    return;
                }
            }
            else
            {
                if (Btn_Update.Enabled)
                    DialogHandler.Instance.Fun_DialogShowOk($"PDO无资料异动", "修改PDO", null, 0);
                
            }
           
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDODetailPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDODefectPage, true);

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Update, true);
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, true);
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;

            //Btn淨重計算 不啟用
            Btn_Act_WT_Count.Enabled = false;

            

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;

            string strStatusType = strEditStatus == "New" ? "新增" : strEditStatus == "Edit" ? "修改" : "";
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.AppendLine($"使用者:{PublicForms.Main.lblLoginUser.Text} 出口钢卷编号:{strOut_Coil_ID} PDO异动[{strStatusType}]记录。 ");
            sbMessage.AppendLine($"操作者权限：{UserSetupHandler.Instance.Authority_Class_Show} ");
            sbMessage.Append($"客户端：{GlobalVariableHandler.Instance.getIpAdderss} ");
            sbMessage.AppendLine($"修改时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");

            string strMessage = sbMessage.ToString();
                //$"使用者:{PublicForms.Main.lblLoginUser.Text} 出口钢卷编号:{strOut_Coil_ID} PDO异动[{strStatusType}]记录。 " +
                //$"操作者权限：{UserSetupHandler.Instance.Authority_Class_Show} " +
                //$"客户端：{GlobalVariableHandler.Instance.getIpAdderss} " +
                //$"修改时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ";

           
            if(!string.IsNullOrEmpty(strFinDeff))
                EventLogHandler.Instance.LogInfo("3-2", strMessage  +strFinDeff, $"出口钢卷编号:{strOut_Coil_ID}PDO异动[{strStatusType}]记录");
            else
                EventLogHandler.Instance.LogInfo("3-2", strMessage, $"出口钢卷编号:{strOut_Coil_ID}PDO异动[{strStatusType}]");
            // PublicComm.ClientLog.Info(strMessage);

            PublicComm.ClientLog.Info(strMessage + strFinDeff);
            //$"使用者:{ PublicForms.Main.lblLoginUser.Text} PDO异动[{strStatusType}]记录。" +
            //    $"操作者权限：{UserSetupHandler.Instance.Authority_Class_Show} " +
            //    $"客户端：{GlobalVariableHandler.Instance.getIpAdderss} " +
            //    $"修改时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} " + strFinDeff);
               
            PublicComm.akkaLog.Info(strMessage);
            EventLogHandler.Instance.EventPush_Message(strMessage);

            strEditStatus = "Read";
            bolEditStatus = false;
            Fun_SelectCoilPDO(strOut_Coil_ID, strPlan_No, strIn_Coil_ID);

            ////通知Server PDO已修改過資料
            //SCCommMsg.CS08_WeightInput _WeightInput = new SCCommMsg.CS08_WeightInput
            //{
            //    Source = "CPL1_HMI",
            //    ID = "WeightInput",
            //    Coil_ID = Txt_Out_mat_No.Text.Trim(),
            //    WeightInput = Txt_Out_mat_gs.Text
            //};
            //PublicComm.client.Tell(_WeightInput);
            //PublicComm.ClientLog.Info($"已通知Server <{Txt_Out_mat_No.Text.Trim()}>PDO修改過");
            //PublicComm.akkaLog.Info($"已通知Server <{Txt_Out_mat_No.Text.Trim()}>PDO修改過");
            //EventLogHandler.Instance.EventPush_Message($"已通知Server PDO异动");
            //EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text} 出口钢卷编号:{Txt_Out_mat_No.Text.Trim()} PDO異動", $" 出口钢卷编号:{Txt_Out_mat_No.Text.Trim()}告知Server淨重需重算");


            ////複製新增
            //Fun_SetBottonEnabled(btn_CopyNew, true);
            ////刪除
            //Fun_SetBottonEnabled(Btn_Delete, true);


            //Fun_Out_mat_ID_ComboBox();
        }
        private string Fun_GetTitle(string strColumnName)
        {
            string strLang = LanguageHandler.Instance.DefaultLanguage ? nameof(LangSwitchEntity.TBL_LangSwitch_Ctr.ZH) : nameof(LangSwitchEntity.TBL_LangSwitch_Ctr.EN);
            //string strLang = "ZH";
            string strReturn = strColumnName;
            if (dtLangSwitch_Column != null && dtLangSwitch_Column.Rows.Count > 0)
            {
                DataRow[] drText = dtLangSwitch_Column.Select($"ColumnName = '{strColumnName}'");
                if (drText != null && drText.Length > 0)
                    strReturn = drText[0][strLang].ToString();
            }
            return strReturn;
        }

        private string Fun_GetTitle_Def(string strColumnName)
        {
            string strNo = strColumnName.Remove(3).Remove(0, 1);//01
            string strColumn = strColumnName.Remove(0, 4);//Code

            string strLang = LanguageHandler.Instance.DefaultLanguage ? nameof(LangSwitchEntity.TBL_LangSwitch_Ctr.ZH) : nameof(LangSwitchEntity.TBL_LangSwitch_Ctr.EN);
            //string strLang = "ZH";
            string strReturn = strColumnName;
            if (dtLangSwitch_Column != null && dtLangSwitch_Column.Rows.Count > 0)
            {
                DataRow[] drText = dtLangSwitch_Column.Select($"ColumnName = '{strColumn}'");

                strReturn = drText[0][strLang].ToString() + strNo;
            }
            return strReturn;
        }
        private void Fun_GetUpdateDataToTable(DataTable dtSaveData)
        {
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)] = Txt_Plan_No.Text;

            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Order_No)] = Txt_Order_No.Text; // Key
            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)] = Txt_Out_Coil_ID.Text; // Key
            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)] = Txt_In_Coil_ID.Text; // Key

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Start_Time)] = Dtp_StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Finish_Time)] = Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Shift)] = Cob_Shift.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Team)] = Cob_Team.SelectedValue;

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.St_No)] = Txt_St_No.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)] = Fun_IsEmptySet(Txt_Out_Coil_Outer_Diameter.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Inner_Diameter)] = Fun_IsEmptySet(Txt_Out_Coil_Inner_Diameter.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Theoretical_Weight)] = Fun_IsEmptySet(Txt_Out_Coil_Theoretical_Weight.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)] = Fun_IsEmptySet(Txt_Out_Coil_Act_WT.Text);

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)] = Fun_IsEmptySet(Txt_Out_Coil_Gross_WT.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)] = Fun_IsEmptySet(Txt_Out_Coil_Thick.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Width)] = Fun_IsEmptySet(Txt_Out_Coil_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Length)] = Fun_IsEmptySet(Txt_Out_Coil_Length.Text);
                 
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Rough_Rz)] = Fun_IsEmptySet(Txt_Head_Rough_Rz.Text);//頭部粗糙度
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Rough_Ra)] = Fun_IsEmptySet(Txt_Head_Rough_Ra.Text); //中部粗糙度
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Rough_Rmax)] = Fun_IsEmptySet(Txt_Head_Rough_Rmax.Text);  //尾部粗糙度

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Rough_Rz)] = Fun_IsEmptySet(Txt_Mid_Rough_Rz.Text);//頭部粗糙度
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Rough_Ra)] = Fun_IsEmptySet(Txt_Mid_Rough_Ra.Text); //中部粗糙度
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Rough_Rmax)] = Fun_IsEmptySet(Txt_Mid_Rough_Rmax.Text);  //尾部粗糙度

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Rough_Rz)] = Fun_IsEmptySet(Txt_Tail_Rough_Rz.Text);//頭部粗糙度
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Rough_Ra)] = Fun_IsEmptySet(Txt_Tail_Rough_Ra.Text); //中部粗糙度
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Rough_Rmax)] = Fun_IsEmptySet(Txt_Tail_Rough_Rmax.Text);  //尾部粗糙度

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)] =  Fun_IsEmptySet(Txt_Head_C40_Thick.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)] =   Fun_IsEmptySet(Txt_Mid_C40_Thick.Text );
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)] =  Fun_IsEmptySet(Txt_Tail_C40_Thick.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)] =  Fun_IsEmptySet(Txt_Head_C25_Thick.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)] =   Fun_IsEmptySet(Txt_Mid_C25_Thick.Text );
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)] = Fun_IsEmptySet(Txt_Tail_C25_Thick.Text );

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Pass_Num)] =  Fun_IsEmptySet(Txt_Head_Pass_Num.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Pass_Num)] =   Fun_IsEmptySet(Txt_Mid_Pass_Num.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Pass_Num)] = Fun_IsEmptySet(Txt_Tail_Pass_Num.Text);

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Paper_Code)] = Cob_Paper_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Paper_Req_Code )] = Cob_Paper_Req_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Paper_Length)] =  Fun_IsEmptySet(Txt_Out_Head_Paper_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Paper_Width)] =   Fun_IsEmptySet(Txt_Out_Head_Paper_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Paper_Length)] =  Fun_IsEmptySet(Txt_Out_Tail_Paper_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Paper_Width)] = Fun_IsEmptySet(Txt_Out_Tail_Paper_Width.Text);

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Use_Sleeve_Flag)] = Cob_Out_Coil_Use_Sleeve_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Sleeve_Diameter)] = Fun_IsEmptySet(Txt_Sleeve_Inner_Exit_Diamter.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Sleeve_Type_Code)] = Cob_Sleeve_Type_Exit.SelectedValue;

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Hole_Position)] = Fun_IsEmptySet(Txt_Head_Hole_Position.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Length)] = Fun_IsEmptySet(Txt_Head_Leader_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Width)] = Fun_IsEmptySet(Txt_Head_Leader_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)] = Fun_IsEmptySet(Txt_Head_Leader_Thickness.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Hole_Position)] = Fun_IsEmptySet(Txt_Tail_PunchHole_Position.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)] = Fun_IsEmptySet(Txt_Tail_Leader_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)] = Fun_IsEmptySet(Txt_Tail_Leader_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)] = Fun_IsEmptySet(Txt_Tail_Leader_Thickness.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)] = Txt_Head_Leader_St_No.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)] = Txt_Tail_Leader_St_No.Text;

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Winding_Dire)] = Cob_Winding_Direction.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Better_Surf_Ward_Code)] = Cob_Base_Surface.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Maker)] = Txt_Inspector.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Flag)] = Cob_Hold_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)] = Txt_Hold_Cause_Code.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Flag)] = Cob_Sample_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)] = Cob_Fixed_WT_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)] = Cob_End_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Scrap_Flag)] = Cob_Scrap_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Oil_Flag)] = Cob_Oil_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Pos_Code)] = Cob_Sample_Frqn_Code.SelectedValue;

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code)] = Cob_Surface_Accuracy_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)] = Fun_IsEmptySet(Txt_Head_Off_Gauge.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)] = Fun_IsEmptySet(Txt_Tail_Off_Gauge.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)] = Cob_Surface_Accu_Code_In.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)] = Cob_Surface_Accu_Code_Out.SelectedValue;
           
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Pre_Grinding_Surface)] = Cob_Pre_Grinding_Surface.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Grinding_Count_Out)] = Fun_IsEmptySet(Txt_Grinding_Count_Out.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Grinding_Count_In)] = Fun_IsEmptySet(Txt_Grinding_Count_In.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Appoint_Grinding_Surface)] = Cob_Appoint_Grinding_Surface.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Recoiler_ActTen_Avg)] = Fun_IsEmptySet(Txt_Recoiler_Actten_Avg.Text);

            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.CreateTime)] = GlobalVariableHandler.Instance.getTime;
        }

        private void Fun_GetUpdateDataToTable_Def(DataTable dtSaveData)
        {

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Code)] = Txt_Code_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Origin)] = Txt_Origin_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Sid)] = Cob_Sid_D01.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Pos_W)] = Cob_PosW_D01.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D01.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D01.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Level)] = Cob_Level_D01.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Percent)] = Fun_IsEmptySet(Txt_Percent_D01.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D01_QGrade)] = Txt_QGRADE_D01.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Code)] = Txt_Code_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Origin)] = Txt_Origin_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Sid)] = Cob_Sid_D02.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Pos_W)] = Cob_PosW_D02.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D02.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D02.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Level)] = Cob_Level_D02.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Percent)] = Fun_IsEmptySet(Txt_Percent_D02.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D02_QGrade)] = Txt_QGRADE_D02.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Code)] = Txt_Code_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Origin)] = Txt_Origin_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Sid)] = Cob_Sid_D03.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Pos_W)] = Cob_PosW_D03.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D03.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D03.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Level)] = Cob_Level_D03.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Percent)] = Fun_IsEmptySet(Txt_Percent_D03.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D03_QGrade)] = Txt_QGRADE_D03.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Code)] = Txt_Code_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Origin)] = Txt_Origin_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Sid)] = Cob_Sid_D04.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Pos_W)] = Cob_PosW_D04.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D04.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D04.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Level)] = Cob_Level_D04.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Percent)] = Fun_IsEmptySet(Txt_Percent_D04.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D04_QGrade)] = Txt_QGRADE_D04.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Code)] = Txt_Code_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Origin)] = Txt_Origin_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Sid)] = Cob_Sid_D05.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Pos_W)] = Cob_PosW_D05.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D05.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D05.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Level)] = Cob_Level_D05.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Percent)] = Fun_IsEmptySet(Txt_Percent_D05.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D05_QGrade)] = Txt_QGRADE_D05.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Code)] = Txt_Code_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Origin)] = Txt_Origin_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Sid)] = Cob_Sid_D06.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Pos_W)] = Cob_PosW_D06.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D06.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D06.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Level)] = Cob_Level_D06.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Percent)] = Fun_IsEmptySet(Txt_Percent_D06.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D06_QGrade)] = Txt_QGRADE_D06.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Code)] = Txt_Code_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Origin)] = Txt_Origin_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Sid)] = Cob_Sid_D07.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Pos_W)] = Cob_PosW_D07.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D07.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D07.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Level)] = Cob_Level_D07.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Percent)] = Fun_IsEmptySet(Txt_Percent_D07.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D07_QGrade)] = Txt_QGRADE_D07.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Code)] = Txt_Code_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Origin)] = Txt_Origin_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Sid)] = Cob_Sid_D08.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Pos_W)] = Cob_PosW_D08.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D08.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D08.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Level)] = Cob_Level_D08.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Percent)] = Fun_IsEmptySet(Txt_Percent_D08.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D08_QGrade)] = Txt_QGRADE_D08.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Code)] = Txt_Code_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Origin)] = Txt_Origin_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Sid)] = Cob_Sid_D09.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Pos_W)] = Cob_PosW_D09.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D09.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D09.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Level)] = Cob_Level_D09.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Percent)] = Fun_IsEmptySet(Txt_Percent_D09.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D09_QGrade)] = Txt_QGRADE_D09.Text.Trim();

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Code)] = Txt_Code_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Origin)] = Txt_Origin_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Sid)] = Cob_Sid_D10.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Pos_W)] = Cob_PosW_D10.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Pos_L_Start)] = Fun_IsEmptySet(Txt_Pos_L_Start_D10.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Pos_L_End)] = Fun_IsEmptySet(Txt_Pos_L_End_D10.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Level)] = Cob_Level_D10.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Percent)] = Fun_IsEmptySet(Txt_Percent_D10.Text.Trim());
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.D10_QGrade)] = Txt_QGRADE_D10.Text.Trim();


        }

        private string Fun_IsEmptySet(string strTxt,string strValue = "0")
        {
            if (string.IsNullOrEmpty(strTxt))
                strValue = "0";
            else
                strValue = strTxt;

            return strValue;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }

            if (dtBeforeEdit != null && dtBeforeEdit.Rows.Count > 0)
            {
                string strOut_Coil_ID = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();// Txt_Out_Coil_ID.Text.Trim();//1
                DateTime dTime = DateTime.Parse(dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.Finish_Time)].ToString());
                string strFinishTime = dTime.ToString("yyyy-MM-dd HH:mm:ss");//2  
                //string strFinishTime = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.FinishTime)].ToString();
                // Dtp_FinishTime.ToString();//2
                // Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                string strIn_Coil_ID = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();// Txt_In_Coil_ID.Text.Trim();//3
                string strPlan_No = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();// Txt_Plan_No.Text.Trim();//4
                Fun_SelectCoilPDO(strOut_Coil_ID, strPlan_No, strFinishTime, strIn_Coil_ID);
            }
            else
            {
                Fun_SelectCoilPDO("", "", "", "");
            }

            ReadOnlyControl(Tab_PDODetailPage, true);
            ReadOnlyControl(Tab_PDODefectPage, true);

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            if (string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()))
            {
                //修改
                Fun_SetBottonEnabled(Btn_Update, false);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, false);
                //列印標籤
                Fun_SetBottonEnabled(Btn_PrintTag, true);
            }
            else
            {
                //修改
                Fun_SetBottonEnabled(Btn_Update, true);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, true);
                //列印標籤
                Fun_SetBottonEnabled(Btn_PrintTag, true);
            }
            //取消
            Btn_Cancel.Visible = false; 
            //儲存
            Btn_Save.Visible = false;
            //Btn淨重計算 不啟用
            Btn_Act_WT_Count.Enabled = false;

            //取消编辑后,恢复唯读状态
            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;

        }

        /// <summary>
        /// 處理上傳PDO的回覆 MWW-add 2023.4.25
        /// </summary>
        /// <param name="msg"></param>
        public async void Handle_SC07_PdoUploadedReply(string msg)
        {
            //  非同步處理
            await Task.Run(() => {
                //  建立調跳視窗
                var frm_D = new Frm_DialogOK
                {
                    MaximizeBox = false,                            //  鎖定放大
                    MinimizeBox = false                             //  鎖定縮小
                };
                //  註冊 Shown 事件，處理自動關閉視窗
                frm_D.Shown += async (s, e) => {
                    //  置頂
                    frm_D.TopMost = true;
                    //  等待延遲
                    await Task.Delay(60000);
                    //  關閉本視窗
                    frm_D.Close();
                };
                //  註冊 MouseDown 事件，處理拖曳視窗起始
                frm_D.MouseDown += (s, e) => {
                    if (e.Button == MouseButtons.Left)
                    {
                        frm_D.Dragging = true;
                        frm_D.DragOffset = e.Location;
                    }
                };
                //  註冊 MouseMove 事件，處理拖曳視窗期間
                frm_D.MouseMove += (s, e) => {
                    if (frm_D.Dragging)
                    {
                        var newLocation = frm_D.Location;
                        newLocation.X += e.Location.X - frm_D.DragOffset.X;
                        newLocation.Y += e.Location.Y - frm_D.DragOffset.Y;
                        frm_D.Location = newLocation;
                    }
                };
                //  註冊 MouseUp 事件，處理拖曳視窗結束
                frm_D.MouseUp += (s, e) => {
                    if (e.Button == MouseButtons.Left)
                    {
                        frm_D.Dragging = false;
                        frm_D.DragOffset = Point.Empty;
                    }
                };
                //  傳入要顯示的訊息
                frm_D.DialogShow($"{msg}", "提示资讯", null, 0);
                //  顯示視窗
                frm_D.ShowDialog();
                //  釋放資源
                frm_D.Dispose();
            });
        }

        #region --  欄位ReadOnly & BackgroundColor  --
        private void ReadOnlyControl(TabPage page, bool bolReadOnly)
        {
            ReadOnlyHandler.Instance.ReadOnly(page, bolReadOnly);
        }
        #endregion

        #region -- SelectData --
        public void Fun_SelectCoilPDO(string Coil_ID, string strPlan_No = null, string strFinishTime = null, string strIn_Coil_ID = null)
        {
            string strSql = SqlFactory.Frm_3_2_SelectData_DB_PDO(Coil_ID, strPlan_No, strIn_Coil_ID);
            DataTable dtGetCoilPDO = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"PDO详细资料","3-2");

            if (dtGetCoilPDO.IsNull())
            {
                if (!string.IsNullOrEmpty(Coil_ID.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"查无钢卷号 [{Coil_ID.Trim()}] PDO", "查詢PDO",null, 0);
                }
                //詳細資料頁籤清空
                Fun_ClearDisplay(Tab_PDODetailPage);
                Lbl_OutCoil_Defect.Text = "";
                //缺陷資料頁籤清空
                Fun_ClearDisplay(Tab_PDODefectPage);

                //Fun_ClearGroupBoxText(Grb_HeadLeader);

                //Fun_ClearGroupBoxText(Grb_TailLeader);
                //如果之前沒有顯示資料,複製欄位
                dtSelectOne = dtGetCoilPDO.Clone();
                dtBeforeEdit = dtSelectOne.Copy();
                return;
            }
            DataTable dtSelectBack = new DataTable();

            if (dtGetCoilPDO.Rows.Count > 1)
            {
                Frm_SelectDataOpen frm_SelectOpen = new Frm_SelectDataOpen
                {
                    dtSelectData = dtGetCoilPDO.Copy()
                     ,
                    strDataType = "PDO"
                };
                frm_SelectOpen.ShowDialog();
                frm_SelectOpen.Dispose();

                if (frm_SelectOpen.DialogResult == DialogResult.OK)
                {
                    //string strSql_Select = Frm_3_2_SqlFactory.SQL_Select_PDODetail(frm_SelectOpen.strOut_Coil_ID, frm_SelectOpen.strFinishTime, frm_SelectOpen.strIn_Coil_ID, frm_SelectOpen.strPlan_No);
                    //dtSelectBack = DataAccess.Fun_SelectDate(strSql_Select, "PDODet");
                    dtSelectBack = frm_SelectOpen.dtSelectData.Copy();
                }
            }

            if (dtSelectBack != null && dtSelectBack.Rows.Count > 0)
            {
                Coil_ID = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();
                //DateTime dTime = DateTime.Parse(dtSelectBack.Rows[0]["FinishTime"].ToString());
                //strFinishTime = dTime.ToString("yyyy-MM-dd HH:mm:ss");//2

                //FinishTime_Str
                strFinishTime = dtSelectBack.Rows[0]["FinishTime_Str"].ToString();
                str_Now_FinishTime = strFinishTime;//紀錄現在查詢的FinishTime_fff

                //strFinishTime = dtSelectBack.Rows[0][ nameof(PDOEntity.TBL_PDO.FinishTime)].ToString();
                strIn_Coil_ID = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();
                strPlan_No = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();

                string strSql_Select = SqlFactory.Frm_3_2_SelectData_DB_PDO(Coil_ID, strPlan_No, strIn_Coil_ID);
                dtGetCoilPDO = Data_Access.Fun_SelectDate(strSql_Select, GlobalVariableHandler.Instance.strConn_GPL, "PDO资料", "3-2");//.Fun_SelectDate(strSql_Select, "PDO");
            }

            dtSelectOne = dtGetCoilPDO.Copy();
            Fun_DataDisplay();
            if (UserSetupHandler.Instance.Frm_3_2.Equals("W"))
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_Update, true);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, true);
                //列印標籤
                Fun_SetBottonEnabled(Btn_PrintTag, true);
            }
            //Fun_DefectDataDisplay(Coil_ID);

            strSql = SqlFactory.Frm_3_2_SelectDefectData_DB_PDO(Coil_ID, strPlan_No);
            DataTable dt_Defect = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDO缺陷资料", "3-2");

            if (dt_Defect.IsNull()) 
            {
                bolDefectIsNull = true;
                Fun_ClearDisplay(Tab_PDODefectPage);
                //如果之前沒有顯示資料,複製欄位
                dtSelectOne_Def = dt_Defect.Clone();
                dtBeforeEdit_Def = dtSelectOne_Def.Copy();
                return;
            }

            bolDefectIsNull = false;
            dtSelectOne_Def = dt_Defect.Copy();
            Fun_DefectDataDisplay(dt_Defect);
            
        }

        /// <summary>
        /// 缺陷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Fun_DefectDataDisplay(DataTable dt_Defect)
        {
            Fun_ClearDisplay(Tab_PDODefectPage);
            //if (dtSelectOne_Def.IsNull()) return;

            #region - 填入资料 -

            #region  - Code -
            Txt_Code_D01.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Code)].ToString() ?? string.Empty;
            Txt_Code_D02.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Code)].ToString() ?? string.Empty;
            Txt_Code_D03.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Code)].ToString() ?? string.Empty;
            Txt_Code_D04.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Code)].ToString() ?? string.Empty;
            Txt_Code_D05.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Code)].ToString() ?? string.Empty;
            Txt_Code_D06.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Code)].ToString() ?? string.Empty;
            Txt_Code_D07.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Code)].ToString() ?? string.Empty;
            Txt_Code_D08.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Code)].ToString() ?? string.Empty;
            Txt_Code_D09.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Code)].ToString() ?? string.Empty;
            Txt_Code_D10.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Code)].ToString() ?? string.Empty;
            #endregion

            #region - Origin -
            Txt_Origin_D01.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D02.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D03.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D04.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D05.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D06.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D07.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D08.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D09.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D10.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Origin)].ToString() ?? string.Empty;
            #endregion

            #region - Sid (ComboBox)-
            Cob_Sid_D01.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D02.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D03.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D04.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D05.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D06.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D07.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D08.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D09.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D10.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Sid)].ToString().Trim() ?? string.Empty;
            #endregion

            #region - Pos_W (ComboBox)-
            Cob_PosW_D01.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D02.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D03.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D04.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D05.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D06.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D07.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D08.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D09.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D10.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Pos_W)].ToString().Trim() ?? string.Empty;
            #endregion

            #region - Pos_L_Start - 
            Txt_Pos_L_Start_D01.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D02.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D03.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D04.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D05.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D06.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D07.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D08.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D09.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Pos_L_Start)].ToString()  ?? string.Empty;
            Txt_Pos_L_Start_D10.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Pos_L_Start)].ToString()  ?? string.Empty;
            #endregion

            #region - Pos_L_End -
            Txt_Pos_L_End_D01.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D02.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D03.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D04.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D05.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D06.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D07.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D08.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D09.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Pos_L_End)].ToString()  ?? string.Empty;
            Txt_Pos_L_End_D10.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Pos_L_End)].ToString()  ?? string.Empty;
            #endregion

            #region - Level (ComboBox)-
            Cob_Level_D01.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D02.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D03.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D04.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D05.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D06.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D07.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D08.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D09.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Level)].ToString().Trim() ?? string.Empty;
            Cob_Level_D10.SelectedItem = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Level)].ToString().Trim() ?? string.Empty;
            #endregion

            #region - Percent -
            Txt_Percent_D01.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D02.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D03.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D04.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D05.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D06.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D07.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D08.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D09.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_Percent)].ToString()  ?? string.Empty;
            Txt_Percent_D10.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_Percent)].ToString()  ?? string.Empty;
            #endregion

            #region - QGrade - 
            Txt_QGRADE_D01.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D01_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D02.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D02_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D03.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D03_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D04.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D04_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D05.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D05_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D06.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D06_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D07.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D07_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D08.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D08_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D09.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D09_QGrade)].ToString()  ?? string.Empty;
            Txt_QGRADE_D10.Text = dtSelectOne_Def.Rows[0][nameof(PDOEntity.TBL_PDO.D10_QGrade)].ToString()  ?? string.Empty;
            #endregion

            #endregion

        }

        /// <summary>
        /// 欄位資料
        /// </summary>
        private void Fun_DataDisplay()
        {
            if (dtSelectOne.IsNull()) return;

            //合同號
            Txt_Order_No.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Order_No)].ToString() ?? string.Empty;
            //計畫號
            Txt_Plan_No.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString() ?? string.Empty;
            //出口鋼卷號
            Txt_Out_Coil_ID.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString() ?? string.Empty;
            //入口鋼卷號
            Txt_In_Coil_ID.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString() ?? string.Empty;
            //開始時間
            //Starttime.Text = dtSelectOne.Rows[0][nameof(PDOModel.L2L3_PDO.StartTime)].ToString() ?? string.Empty;
            Dtp_StartTime.Value = Convert.ToDateTime(dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Start_Time)]);
            //結束時間
            //Finishtime.Text = dtSelectOne.Rows[0][nameof(PDOModel.L2L3_PDO.FinishTime)].ToString() ?? string.Empty;
            Dtp_FinishTime.Value = Convert.ToDateTime(dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Finish_Time)]);
            str_Now_FinishTime = dtSelectOne.Rows[0]["FinishTime_Str"].ToString();
            //生产班次
            Cob_Shift.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Shift)].ToString().Trim() ?? string.Empty;
            //生产班组
            Cob_Team.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Team)].ToString().Trim() ?? string.Empty;
            //钢种
            Txt_St_No.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.St_No)].ToString() ?? string.Empty;
            //出口外徑
            Txt_Out_Coil_Outer_Diameter.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)].ToString() ?? string.Empty;
            //出口內徑
            Txt_Out_Coil_Inner_Diameter.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Inner_Diameter)].ToString() ?? string.Empty;
            //出口淨重
            Txt_Out_Coil_Act_WT.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)].ToString() ?? string.Empty;
            //出口毛重
            Txt_Out_Coil_Gross_WT.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)].ToString() ?? string.Empty;
            //出口理论重
            Txt_Out_Coil_Theoretical_Weight.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Theoretical_Weight)].ToString() ?? string.Empty;
            //出口厚度
            Txt_Out_Coil_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)].ToString() ?? string.Empty;
            //出口寬度
            Txt_Out_Coil_Width.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Width)].ToString() ?? string.Empty;
            //出口長度
            Txt_Out_Coil_Length.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Length)].ToString() ?? string.Empty;
            //頭段C40厚度
            Txt_Head_C40_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)].ToString() ?? string.Empty;
            //中段C40厚度
            Txt_Mid_C40_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)].ToString() ?? string.Empty;
            //尾段C40厚度
            Txt_Tail_C40_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)].ToString() ?? string.Empty;
            //頭段C25厚度
            Txt_Head_C25_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)].ToString() ?? string.Empty;
            //中段C25厚度
            Txt_Mid_C25_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)].ToString() ?? string.Empty;
            //尾段C25厚度
            Txt_Tail_C25_Thick.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)].ToString() ?? string.Empty;
            //頭段道次
            Txt_Head_Pass_Num.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Pass_Num)].ToString() ?? string.Empty;
            //中段道次
            Txt_Mid_Pass_Num.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Pass_Num)].ToString() ?? string.Empty;
            //尾段道次
            Txt_Tail_Pass_Num.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Pass_Num)].ToString() ?? string.Empty;
            //出口墊紙类型
            Cob_Paper_Code.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Paper_Code)].ToString().Trim() ?? string.Empty;
            //出口墊紙方式
            Cob_Paper_Req_Code.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Paper_Req_Code )].ToString().Trim() ?? string.Empty;
            //頭段墊紙長度
            Txt_Out_Head_Paper_Length.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Paper_Length)].ToString() ?? string.Empty;
            //頭段墊紙寬度
            Txt_Out_Head_Paper_Width.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Paper_Width)].ToString() ?? string.Empty;
            //尾段墊紙長度
            Txt_Out_Tail_Paper_Length.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Paper_Length)].ToString() ?? string.Empty;
            //尾段墊紙寬度
            Txt_Out_Tail_Paper_Width.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Paper_Width)].ToString() ?? string.Empty;
            //是否使用套筒
            Cob_Out_Coil_Use_Sleeve_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Use_Sleeve_Flag)].ToString().Trim() ?? string.Empty;
            //套筒內徑
            Txt_Sleeve_Inner_Exit_Diamter.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Sleeve_Diameter)].ToString() ?? string.Empty;
            //套筒種類
            Cob_Sleeve_Type_Exit.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Sleeve_Type_Code)].ToString().Trim() ?? string.Empty;
            //頭段打孔位置
            Txt_Head_Hole_Position.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Hole_Position)].ToString() ?? string.Empty;
            //頭段導帶長度
            Txt_Head_Leader_Length.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Length)].ToString() ?? string.Empty;
            //頭段導帶寬度
            Txt_Head_Leader_Width.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Width)].ToString() ?? string.Empty;
            //頭段導帶厚度
            Txt_Head_Leader_Thickness.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)].ToString() ?? string.Empty;
            //尾段打孔位置
            Txt_Tail_PunchHole_Position.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Hole_Position)].ToString() ?? string.Empty;
            //尾段導帶長度
            Txt_Tail_Leader_Length.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)].ToString() ?? string.Empty;
            //尾段導帶寬度
            Txt_Tail_Leader_Width.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)].ToString() ?? string.Empty;
            //尾段導帶厚度
            Txt_Tail_Leader_Thickness.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)].ToString() ?? string.Empty;
            //頭段導帶鋼種
            Txt_Head_Leader_St_No.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)].ToString() ?? string.Empty;
            //尾段導帶鋼種
            Txt_Tail_Leader_St_No.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)].ToString() ?? string.Empty;
            //卷曲方向
            Cob_Winding_Direction.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Winding_Dire)].ToString().Trim() ?? string.Empty;
            //好面朝向
            Cob_Base_Surface.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Better_Surf_Ward_Code)].ToString().Trim() ?? string.Empty;
            //封鎖責任人
            Txt_Inspector.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Maker)].ToString() ?? string.Empty;
            //封鎖標記
            Cob_Hold_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Flag)].ToString().Trim() ?? string.Empty;
            //封鎖代碼
            Txt_Hold_Cause_Code.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)].ToString() ?? string.Empty;
            //取樣標記
            Cob_Sample_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Flag)].ToString().Trim() ?? string.Empty;
            //分卷標記
            Cob_Fixed_WT_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)].ToString().Trim() ?? string.Empty;
            //最終卷標記
            Cob_End_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)].ToString().Trim() ?? string.Empty;
            //廢品標記
            Cob_Scrap_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Scrap_Flag)].ToString().Trim() ?? string.Empty;
            //是否有油
            Cob_Oil_Flag.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Oil_Flag)].ToString().Trim() ?? string.Empty;
            //取樣位置
            Cob_Sample_Frqn_Code.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Pos_Code)].ToString().Trim() ?? string.Empty;
            //表面精度代碼
            Cob_Surface_Accuracy_Code.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code)].ToString().Trim() ?? string.Empty;
            //内表面精度代碼
            //Txt_SURFACE_ACCU_CODE_IN.Text = dtSelectOne.Rows[0][nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_In)].ToString().Trim() ?? string.Empty;
            Cob_Surface_Accu_Code_In.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)].ToString().Trim() ?? string.Empty;
            //外表面精度代碼
            //Txt_SURFACE_ACCU_CODE_OUT.Text = dtSelectOne.Rows[0][nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_Out)].ToString().Trim() ?? string.Empty;
            Cob_Surface_Accu_Code_Out.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)].ToString().Trim() ?? string.Empty;
            //頭段未軋製區域
            Txt_Head_Off_Gauge.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)].ToString() ?? string.Empty;
            //尾段未軋製區域
            Txt_Tail_Off_Gauge.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)].ToString() ?? string.Empty;
            //上次研磨面
            Cob_Pre_Grinding_Surface.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Pre_Grinding_Surface)].ToString().Trim() ?? string.Empty;
            //外表面研磨次數
            Txt_Grinding_Count_Out.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Grinding_Count_Out)].ToString() ?? string.Empty;
            //內表面研磨次數
            Txt_Grinding_Count_In.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Grinding_Count_In)].ToString() ?? string.Empty;
            //指定研磨面
            Cob_Appoint_Grinding_Surface.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Appoint_Grinding_Surface)].ToString().Trim() ?? string.Empty;
            //工序代码
            Cob_ProcessCode.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Process_Code)].ToString().Trim() ?? string.Empty;

            //頭部粗糙度_Rz
            Txt_Head_Rough_Rz.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Rough_Rz)].ToString() ?? string.Empty;
            //頭部粗糙度_Ra
            Txt_Head_Rough_Ra.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Rough_Ra)].ToString() ?? string.Empty;
            //頭部粗糙度_Rmax
            Txt_Head_Rough_Rmax.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Rough_Rmax)].ToString() ?? string.Empty;
            //中部粗糙度_Rz
            Txt_Mid_Rough_Rz.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Rough_Rz)].ToString() ?? string.Empty;
            //中部粗糙度_Ra
            Txt_Mid_Rough_Ra.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Rough_Ra)].ToString() ?? string.Empty;
            //中部粗糙度_Rmax
            Txt_Mid_Rough_Rmax.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Mid_Rough_Rmax)].ToString() ?? string.Empty;
            //尾部粗糙度_Rz
            Txt_Tail_Rough_Rz.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Rough_Rz)].ToString() ?? string.Empty;
            //尾部粗糙度_Ra
            Txt_Tail_Rough_Ra.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Rough_Ra)].ToString() ?? string.Empty;
            //尾部粗糙度_Rmax
            Txt_Tail_Rough_Rmax.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Rough_Rmax)].ToString() ?? string.Empty;
            
            //實際開卷方向
            Cob_Decoiler_Direction.SelectedValue = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Uncoil_Direction)].ToString().Trim() ?? string.Empty;

            //卷取張力平均值
            Txt_Recoiler_Actten_Avg.Text = dtSelectOne.Rows[0][nameof(PDOEntity.TBL_PDO.Recoiler_ActTen_Avg)].ToString().Trim() ?? string.Empty;
        }
                
        /// <summary>
        /// 檢查必填欄位是否空白
        /// </summary>
        /// <param name="intOtherMust">0:一般儲存前檢查 ; 1:上傳MMS前檢查</param>
        /// <returns></returns>
        private bool Fun_IsColumnsEmpty(int intOtherMust = 0)
        {
            //出口钢卷号
            if (string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"出口钢卷号 请勿空白! ", "提示资讯",null, 0);
                Txt_Out_Coil_ID.Focus();
                return false;
            }

            //入口钢卷号
            if (string.IsNullOrEmpty(Txt_In_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"入口钢卷号 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_ID.Focus();
                return false;
            }

            //计划号
            if (string.IsNullOrEmpty(Txt_Plan_No.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"计划号 请勿空白! ", "提示资讯", null, 0);
                Txt_Plan_No.Focus();
                return false;
            }
            
            //分卷标记
            if (string.IsNullOrEmpty(Cob_Fixed_WT_Flag.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"分卷标记 请勿空白! ", "提示资讯", null, 0);
                Cob_Fixed_WT_Flag.Focus();
                return false;
            }

            //最终卷标记
            if (string.IsNullOrEmpty(Cob_End_Flag.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"最终卷标记 请勿空白! ", "提示资讯", null, 0);
                Cob_End_Flag.Focus();
                return false;
            }
            //最终卷标记与分卷标记预设为空白,并检查两个栏位不会同时为0
            if (Cob_Fixed_WT_Flag.SelectedIndex == 0 && Cob_End_Flag.SelectedIndex == 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"分卷标记与最终卷标记 不可同时为0! ", "提示资讯", null, 0);
                Cob_End_Flag.Focus();
                return false;
            }

            if(intOtherMust == 1)
            {
                //合同號   Txt_Order_No
                if (string.IsNullOrEmpty(Txt_Order_No.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"合同号 请勿空白! ", "提示资讯", null, 0);
                    Txt_Order_No.Focus();
                    return false;
                }
                //出口材料内徑    Txt_Out_Coil_Inner_Diameter
                if (string.IsNullOrEmpty(Txt_Out_Coil_Inner_Diameter.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷内径 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Inner_Diameter.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Inner_Diameter.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷内径 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Inner_Diameter.Focus();
                        return false;
                    }
                }
                //出口材料外徑    Txt_Out_Coil_Outer_Diameter
                if (string.IsNullOrEmpty(Txt_Out_Coil_Outer_Diameter.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷外径 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Outer_Diameter.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Outer_Diameter.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷外径 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Outer_Diameter.Focus();
                        return false;
                    }
                }

                //出口材料净重    Txt_Out_Coil_Act_WT
                ////pdo净重小於等於pdi入料重的時候，才可以上抛，否則提示不讓上抛之原因。

                if (string.IsNullOrEmpty(Txt_Out_Coil_Act_WT.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷净重 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Act_WT.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Act_WT.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷净重 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Act_WT.Focus();
                        return false;
                    }
                }

                //出口材料毛重    Txt_Out_Coil_Gross_WT
                if (string.IsNullOrEmpty(Txt_Out_Coil_Gross_WT.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷毛重 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Gross_WT.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Gross_WT.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷毛重 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Gross_WT.Focus();
                        return false;
                    }
                }

                //净重不得大于毛重     (净重:douAct_WT ; 毛重:douGross_WT)
                if (double.TryParse(Txt_Out_Coil_Act_WT.Text.Trim(), out double douAct_WT))
                {
                    if (double.TryParse(Txt_Out_Coil_Gross_WT.Text.Trim(), out double douGross_WT))
                    {
                        if(douAct_WT > douGross_WT)
                        {
                            DialogHandler.Instance.Fun_DialogShowOk($"出口卷净重 大于 出口卷毛重 不可上传! ", "提示资讯", null, 0);
                            //Txt_Out_Coil_Gross_WT.Focus();
                            return false;
                        }

                    }
                }


                //出口材料厚度    Txt_Out_Coil_Thick
                if (string.IsNullOrEmpty(Txt_Out_Coil_Thick.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷厚度 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Thick.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Thick.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷厚度 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Thick.Focus();
                        return false;
                    }
                }
                //出口材料寬度    Txt_Out_Coil_Width
                if (string.IsNullOrEmpty(Txt_Out_Coil_Width.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷宽度 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Width.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Width.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷宽度 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Width.Focus();
                        return false;
                    }
                }
                //出口材料長度    Txt_Out_Coil_Length
                if (string.IsNullOrEmpty(Txt_Out_Coil_Length.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷长度 请勿空白! ", "提示资讯", null, 0);
                    Txt_Out_Coil_Length.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Length.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷长度 请勿小于0! ", "提示资讯", null, 0);
                        Txt_Out_Coil_Length.Focus();
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        private void Tab_PDOControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_PDOControl, e);
        }

        

        #region -- ComboBox說明 --

        /// <summary>
        /// 出口墊紙方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OUT_PAPER_REQ_CODE_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = "出口垫纸方式";
            Fun_SpareDisplay(Cbo_Type.PAPER_REQ_CODE);
        }

        /// <summary>
        /// 出口墊紙类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Paper_Type_Exit_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = "出口垫纸类型";
            Fun_SpareDisplay(Cbo_Type.Paper_Type);
        }

        /// <summary>
        /// 出口套筒類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = "出口套筒类型";
            Fun_SpareDisplay(Cbo_Type.Sleeve_Type);
        }

        private void Btn_Surface_Accuracy_Code_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = "表面精度代码";          
            Fun_SpareDisplay(Cbo_Type.Surface_Accuracy);
        }

        private void Btn_ProcessCode_Dec_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = "工序代码";
            Fun_SpareDisplay(Cbo_Type.ProcessCode);
        }

        private void Fun_SpareDisplay(Cbo_Type cbo_Type)
        {
            Pnl_Spare.Visible = true;
            Pnl_Spare.BringToFront();
            Pnl_Spare.Size = new Size(456, 226);
            Txt_Spare.ScrollBars = ScrollBars.None;
            Txt_Spare.Size = new Size(438, 174);
            Btn_ClosePanel.Location = new Point(415, 3);
            switch (Lbl_Spare_ComboName.Text)
            {
                case "出口垫纸方式":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(1155, 50);  
                    break;

                case "出口垫纸类型":
                    ComboBoxIndexHandler.Instance.Fun_PaperSpareDisplay(Txt_Spare);
                    Pnl_Spare.Location = new Point(1155, 50);
                    Pnl_Spare.Size = new Size(575, 226); 
                    Txt_Spare.Size = new Size(555, 174);
                    Btn_ClosePanel.Location = new Point(532, 3);
                    break;

                case "出口套筒类型":
                    ComboBoxIndexHandler.Instance.Fun_SleeveSpareDisplay(Txt_Spare);
                    Pnl_Spare.Location = new Point(1155, 50);
                    Txt_Spare.ScrollBars = ScrollBars.Both;                   
                    break;
                case "表面精度代码":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(1370, 53);  
                    Txt_Spare.ScrollBars = ScrollBars.Both;          
                    break;
                case "工序代码":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(780, 477);
                    // Txt_Spare.ScrollBars = ScrollBars.Both; 
                    break;

                default:
                    break;
            }
           
        }

        /// <summary>
        /// 關閉說明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ClosePanel_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }

        #endregion

        private void Btn_MMS_Click(object sender, EventArgs e)
        {
            if (strEditStatus != "Read")
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再上传", "上传PDO", null, 0);
                return;
            }
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再上传", "上传PDO", null, 0);
                return;
            }
            if (string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("无钢卷资料可上传", "上传PDO", null, 0);
                return;
            }

            if (!Fun_IsColumnsEmpty(1)) 
                return;

            // MWW 2023/3/8 PDO上傳前檢查是否已上傳最終卷
            string strOut_Coil_ID = Txt_Out_Coil_ID.Text.Trim();
            string strIn_Coil_ID = Txt_In_Coil_ID.Text.Trim();
            string strPlan_No = Txt_Plan_No.Text.Trim();
            string strFixed_WT_Flag = Cob_Fixed_WT_Flag.SelectedIndex.ToString();
            string strMessage = "";

            string strSql_PDO = SqlFactory.SQL_Select_UploadEndFlag(strIn_Coil_ID, strPlan_No);
            DataTable dtGetCoilPDO = Data_Access.Fun_SelectDate(strSql_PDO, GlobalVariableHandler.Instance.strConn_GPL, "已上传最终卷之PDO详细资料", "3-2");
            
            //已上抛過最終卷
            if (dtGetCoilPDO != null && dtGetCoilPDO.Rows.Count > 0)
            {
                strMessage = "此入料母卷" + strIn_Coil_ID + "已有最终卷上传记录，请确认是否需要于产销系统界面修改最终卷标记！" + "\r\n"
                    + "PS：确认于产销系统修改最终卷标记后才可以点击【继续上传】按钮，二级亦需同步修改该栏位资料！";
                DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogEndFlagCheck(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                Fun_Dialog(dialog_Result);
            }
            //未上抛最終卷
            else
            {
                //需上抛的鋼捲，是最終卷
                if (Cob_End_Flag.SelectedIndex == 1)
                {
                    //分卷
                    if (Cob_Fixed_WT_Flag.SelectedIndex == 1)
                    {
                        string strNo = "";
                        strSql_PDO = SqlFactory.SQL_Select_SubvolumeCoil(strIn_Coil_ID, strPlan_No);
                        dtGetCoilPDO = Data_Access.Fun_SelectDate(strSql_PDO, GlobalVariableHandler.Instance.strConn_GPL, "已上抛之子卷PDO详细资料", "3-2");
                        //有上抛過子卷
                        if (dtGetCoilPDO != null && dtGetCoilPDO.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtGetCoilPDO.Rows.Count; i++)
                            {
                                strNo += dtGetCoilPDO.Rows[i]["Out_Coil_ID"].ToString() + "、";
                            }
                            strNo = strNo.Substring(0, strNo.Length - 1);
                            strMessage = "该卷最终卷标记为1，之前上抛的子卷清单有" + strNo + "，请确认是否全部子卷已上抛！";
                            DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogEndFlagCheck(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                            Fun_Dialog(dialog_Result);
                        }
                        //未上抛過子卷
                        else
                        {
                            strMessage = "该卷最终卷标记为1，请确认是否全部子卷已上抛！";
                            DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogEndFlagCheck(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                            Fun_Dialog(dialog_Result);
                        }


                    }
                    else//不分卷
                    {
                        strMessage = "请确定是否要上传PDO资料?";
                        DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                        Fun_Dialog(dialog_Result);
                    }
                }
                else////需上抛的鋼捲，不是最終卷
                {
                    strMessage = "请确定是否要上传PDO资料?";
                    DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                    Fun_Dialog(dialog_Result);
                }
                
            }
            void Fun_Dialog(DialogResult dialog)
            {
                if (dialog.Equals(DialogResult.OK))
                {
                    SCCommMsg.CS06_SendMMSPDO _SendMMSPDO = new SCCommMsg.CS06_SendMMSPDO
                    {
                        Source = "GPL_HMI",
                        ID = "SendMMSPDO",
                        Coil_ID = Txt_Out_Coil_ID.Text.Trim(),
                        In_Coil_ID = Txt_In_Coil_ID.Text.Trim(),
                        OperatorID = PublicForms.Main.lblLoginUser.Text.Trim(),
                        Plan_No = Txt_Plan_No.Text
                        // ,FinishTime = strFinishTime_fff
                    };
                    PublicComm.Client.Tell(_SendMMSPDO);

                    strMessage = $"钢卷号[{Txt_Out_Coil_ID.Text.Trim()}]已通知Server上传MMS";

                    EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}PDO资料确认上传 钢卷编号:{Txt_Out_Coil_ID.Text.Trim()}", $"PDO资料确认上传 钢卷编号:{ Txt_Out_Coil_ID.Text.Trim()}");

                    DialogHandler.Instance.Fun_DialogShowOk(strMessage, "上传PDO", null, 4);

                    EventLogHandler.Instance.EventPush_Message(strMessage);
                    PublicComm.ClientLog.Info(strMessage);
                    PublicComm.akkaLog.Info(strMessage);
                }
            }
            #region 原代碼，無用
            //string strSql = SqlFactory.Frm_3_2_UpdatePDO_Check_DB_PDO(Out_mat_No.Text);
            //DataTable dtUploadFlag = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"PDO上传记录","3-2");

            //if (dtUploadFlag.IsNull() || !dtUploadFlag.Rows[0][nameof(PDOModel.L2L3_PDO.PDO_Uploaded_Flag)].ToString().Equals("1"))
            //{
            //string strOut_Coil_ID = Txt_Out_mat_No.Text.Trim();//1                
            ////string strFinishTime = str_Now_FinishTime;//Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss");//2
            ////string strFinishTime_fff = "";
            ////strFinishTime += $".{Dtp_FinishTime.Value.Millisecond}";
            //string strIn_Coil_ID = Txt_In_mat_No.Text.Trim();//3
            //string strPlan_No = Txt_Plan_No.Text.Trim();//4

            //strMessage = "请确定是否要上传PDO资料?";//Old:PDO只能上传一次，
            //DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);

            //if (dialogR == DialogResult.OK)
            //{
            //    SCCommMsg.CS06_SendMMSPDO _SendMMSPDO = new SCCommMsg.CS06_SendMMSPDO
            //    {
            //        Source = "GPL_HMI",
            //        ID = "SendMMSPDO",
            //        Coil_ID = Txt_Out_Coil_ID.Text.Trim(),
            //        In_Coil_ID = Txt_In_Coil_ID.Text.Trim(),
            //        OperatorID = PublicForms.Main.lblLoginUser.Text.Trim(),
            //        Plan_No = Txt_Plan_No.Text
            //        // ,FinishTime = strFinishTime_fff
            //    };
            //    PublicComm.Client.Tell(_SendMMSPDO);

            //    string strMessage_Ok = $"钢卷号[{Txt_Out_Coil_ID.Text.Trim()}]已通知Server上传MMS";
            //    EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}PDO资料确认上传 钢卷编号:{Txt_Out_Coil_ID.Text.Trim()}", $"PDO资料确认上传 钢卷编号:{ Txt_Out_Coil_ID.Text.Trim()}");

            //    DialogHandler.Instance.Fun_DialogShowOk(strMessage_Ok, "上传PDO", null, 4);

            //    EventLogHandler.Instance.EventPush_Message(strMessage_Ok);
            //    PublicComm.ClientLog.Info(strMessage_Ok);
            //    PublicComm.akkaLog.Info(strMessage_Ok);
            //}
            //}
            //else
            //{
            //    EventLogHandler.Instance.EventPush_Message($" [{Out_mat_No.Text.Trim()}]已上傳過MMS!");
            //    PublicComm.ClientLog.Info($"[{Out_mat_No.Text.Trim()}]已上傳過MMS");
            //}
            #endregion
        }

        private void Btn_BackToList_Click(object sender, EventArgs e)
        {
            Frm_0_0_Main FatherForm = Parent.Parent as Frm_0_0_Main;

            FatherForm.tsMenuItem_3_1.PerformClick();

            //Form FrmChild = new Frm_3_1_ProdCoilList();
            //PublicForms.ProdCoilList = (Frm_3_1_ProdCoilList)FrmChild;

            //foreach (Form form in FatherForm.pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
            //{
            //    if (form.Name.Equals(PublicForms.ProdCoilList.Name))
            //    {
            //        form.Focus();
            //        form.Visible = true;
            //    }
            //    if (form.Name.Equals(PublicForms.PDODetail.Name))
            //    {
            //        form.Visible = false;
            //    }
            //}
        }

        private void Btn_Description_Click(object sender, EventArgs e)
        {
            Pnl_Description.Visible = true;
            Pnl_Description.Location = new Point(403, 117);
            Pnl_Description.Size = new Size(1010, 571);           
        }
       
        private void Btn_Close_Desc_Click(object sender, EventArgs e)
        {
            Pnl_Description.Visible = false;
            Pnl_Description.Location = new Point(1530, 647);
            Pnl_Description.Size = new Size(117, 46);
        }

        /// <summary>
        /// 出口衬纸类型ComboBox选项Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PAPER_CODE_Click(object sender, EventArgs e)
        {
            //出口墊紙種類
            ComboBoxIndexHandler.Instance.SelectPaper(Cob_Paper_Code);
        }

        /// <summary>
        /// 出口套筒类型ComboBox选项Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            //出口套筒種類
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Exit);
        }

        /// <summary>
        /// 出口卷号ComboBox选项Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Matcoilid_Click(object sender, EventArgs e)
        {
            ////出口鋼卷號ComboBox
            //Fun_Out_mat_ID_ComboBox();
        }

        private void Btn_PrintTag_Click(object sender, EventArgs e)
        {
            //if (Txt_Out_mat_No.Text.Trim().IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message($"请查询欲打印标签之钢卷");
            //    return;
            //}

            Frm_PrintLabels frm_Print = new Frm_PrintLabels
            {
                Str_Coil_No = Txt_Out_Coil_ID.Text.Trim()
            };
            frm_Print.ShowDialog();
            frm_Print.Dispose();

            if (frm_Print.DialogResult == DialogResult.OK)
            {
                string strShowText = $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                EventLogHandler.Instance.EventPush_Message(strShowText);
                PublicComm.ClientLog.Info(strShowText);

            }

            ////改由HMI直接列印
            //SCCommMsg.CS07_PrintLabel _PrintLabel = new SCCommMsg.CS07_PrintLabel
            //{
            //    Source = "GPL_HMI",
            //    ID = "PrintLabel",
            //    CoilID = Txt_Out_mat_No.Text.Trim()
            //};
            //PublicComm.Client.Tell(_PrintLabel);
            //EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}打印标签作业", $"打印{Txt_Out_mat_No.Text.Trim()}标签");
            //EventLogHandler.Instance.EventPush_Message($"列印[{Txt_Out_mat_No.Text.Trim()}]标签");
            //PublicComm.ClientLog.Info($"通知Server列印鋼卷號:[{Txt_Out_mat_No.Text.Trim()}]標籤");
        }
        /// <summary>
        /// 缺陷欄位說明
        /// </summary>
        private void TxtDescription()
        {
            #region 缺陷代码
            Txt_Code_Desc.Text = "第一位：缺陷种类（1st Byte，Defect Sort）" + Environment.NewLine
                         + "   S_表面缺陷（Surface）" + Environment.NewLine
                         + "   E_边缘缺陷 (Edge)" + Environment.NewLine
                         + "   F_板形缺陷 (Shape)" + Environment.NewLine
                         + "   W_盘卷缺陷 (Coil)" + Environment.NewLine
                         + "   D_尺寸/重量缺陷 (Size/Weight)" + Environment.NewLine
                         + "   M_物性缺陷 (Physics)" + Environment.NewLine
                         + "   C_化性/耐蚀性缺陷 (Chemistry/ Corrosion Resistance)" + Environment.NewLine
                         + "   其它缺陷(Others)" + Environment.NewLine
                         + "第二位与第三位：缺陷种类序号 01,02，……" + Environment.NewLine
                         + "(2nd  Byte And 3rd Byte,Sequence No,01,02, …)" + Environment.NewLine;
            #endregion
            #region 缺陷来源
            Txt_Origin_Desc.Text = "Sm1:1#炼钢厂(Steel Plant)" + Environment.NewLine
                         + "Rf1:1#Rf" + Environment.NewLine
                         + "Hm1:1#Hsm" + Environment.NewLine
                         + "Ba1:1#Baf" + Environment.NewLine
                         + "Hp1:1#Hapl" + Environment.NewLine
                         + "Rc1:1#Rcl" + Environment.NewLine
                         + "Pa1:1#Pak" + Environment.NewLine
                         + "Rs1:1#Roll_Shop" + Environment.NewLine
                         + "Wh1:1#Warehouse" + Environment.NewLine;
            #endregion
            #region 表面区分
            Txt_Sid_Desc.Text = "T:上(Top)" + Environment.NewLine
                         + "B:下(Bottom)" + Environment.NewLine
                         + "A:上下都有(Both)" + Environment.NewLine;
            #endregion
            #region 宽向位置
            Txt_Pos_Desc.Text = "W:操作側1/4板宽 " + Environment.NewLine
                     + "  (1/4 Strip Width Of Operate Side)" + Environment.NewLine
                     + "D:驅動側1/4板宽 " + Environment.NewLine
                     + "  (1/4 Strip Width Of Driver Side)" + Environment.NewLine
                     + "C:中央1/2位置(Center)" + Environment.NewLine
                     + "B:兩側皆有 (Both Side)" + Environment.NewLine
                     + "A:宽度方向全面皆有(All Side)" + Environment.NewLine;
            #endregion
            #region 长向位置
            Txt_Pos_L_Desc.Text = "比如(Example):" + Environment.NewLine
                         + "缺陷于距带钢头部800m，则为0800" + Environment.NewLine
                         + "Distance Of Strip Head 800m,so the Item is 0800." + Environment.NewLine;
            #endregion
            #region 缺陷程度
            Txt_Level_Desc.Text = "L：轻微(Light)" + Environment.NewLine
                             + "M：中等(Middle)" + Environment.NewLine
                             + "H：严重(Heavy)" + Environment.NewLine
                             + "S：极严重(Serious)" + Environment.NewLine;
            #endregion
            #region 缺陷比例
            Txt_Percent_Desc.Text = "比如(Example):" + Environment.NewLine
                             + "13.14% Record To 131;" + Environment.NewLine
                             + "3.16% Record To 032;" + Environment.NewLine
                             + "100% Record To 000" + Environment.NewLine;
            #endregion
        }

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            if (btn.Name.Equals(Btn_MMS.Name) || btn.Name.Equals(Btn_PrintTag.Name))
                btn.BackColor = bolE ? Color.Gold : Color.LightGray;
            else
                //Color colorBack;
                btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        private void Fun_ClearDisplay(TabPage Page)
        {

            foreach (Control control in Page.Controls.OfType<Control>())
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }

                if (Page.Name.Equals(nameof(Tab_PDODefectPage)))
                {
                    if (control is ComboBox)
                    {
                        control.Text = string.Empty;
                    }
                }

                //if (control is CtrTextBox)
                //{
                //    control.Text = string.Empty;
                //}

                //if (control is CtrNumTextBox)
                //{
                //    control.Text = string.Empty;
                //}
            }
        }
        private void Fun_ClearGroupBoxText(GroupBox group)
        {
            foreach (Control control in group.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }
                //if (control is CtrTextBox)
                //{
                //    control.Text = string.Empty;
                //}

                //if (control is CtrNumTextBox)
                //{
                //    control.Text = string.Empty;
                //}
            }
        }
      
        /// <summary>
        /// PDO匯出功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Export_Click(object sender, EventArgs e)
        {
            if (dtSelectOne == null || dtSelectOne.Rows.Count <= 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"请先查询要汇出的PDO钢卷资料!", "汇出PDO", null, 2);
                return;
            }
            /*
                1.以PDO 出口鋼捲號 查詢 (入口捲號 & 生產開始時間 &生產結束時間)

                2.以PDO(入口捲號 & 生產開始時間 &生產結束時間)至 [FUXIN_GPL_HISTORY]資料庫查詢    [L1L2_107]LEFT JOIN   [L1L2_109] 每部位 每道次最舊的第一筆

                107 取 每部位 每道次最舊的第一筆,
                產速 取 每部位 每道次 所有資料的平均值

                公里数:從109撈
                撈107 最舊的一筆的大於CreateTime的第一筆 No1GRAbrBeltAccGriBeltLen
             */
            string strOut_Coil_ID, strPlan_No ,strIn_Coil_ID, strStartTime, strFinishTime, strIn_Coil_Thick;

            strOut_Coil_ID = !string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()) ? Txt_Out_Coil_ID.Text.Trim() : "";
            strPlan_No = !string.IsNullOrEmpty(Txt_Plan_No.Text.Trim()) ? Txt_Plan_No.Text.Trim() : "";
            strIn_Coil_ID = !string.IsNullOrEmpty(Txt_In_Coil_ID.Text.Trim()) ? Txt_In_Coil_ID.Text.Trim() : "";
            strStartTime = !string.IsNullOrEmpty(Dtp_StartTime.Text.Trim()) ? Dtp_StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            strFinishTime = !string.IsNullOrEmpty(Dtp_FinishTime.Text.Trim()) ? Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            strIn_Coil_Thick = "";
            //strIn_Coil_ID = "CM00Test";
            //strStartTime = "2022-10-03 11:29:15";
            //strFinishTime = "2022-10-03 11:30:11";

            //string strSql_107 = Fun_SelectHis_107_SQL(strIn_Coil_ID, strStartTime, strFinishTime);
            //DataTable dt_His_107 = Data_Access.Fun_SelectDate(strSql_107, GlobalVariableHandler.Instance.strConn_GPL_HISTORY, "His_107资料", "3-2");
            //string strSql_107_AVG = Fun_SelectHis_107_AVG_SQL(strIn_Coil_ID, strStartTime, strFinishTime);
            //DataTable dt_His_107_AVG = Data_Access.Fun_SelectDate(strSql_107_AVG, GlobalVariableHandler.Instance.strConn_GPL_HISTORY, "His_107_AVG资料", "3-2");
            string strSql_GR = Fun_Select_TBL_GrindRecords_SQL(strIn_Coil_ID, strPlan_No);
            DataTable dt_GR = Data_Access.Fun_SelectDate(strSql_GR, GlobalVariableHandler.Instance.strConn_GPL, "TBL_GrindRecords资料", "3-2");
            string strSql_GR_AVG = Fun_Select_TBL_GrindRecords_AVG_SQL(strIn_Coil_ID, strPlan_No);
            DataTable dt_GR_AVG = Data_Access.Fun_SelectDate(strSql_GR_AVG, GlobalVariableHandler.Instance.strConn_GPL, "TBL_GrindRecords_AVG资料", "3-2");

            string strSql_109 = Fun_SelectHis_109_SQL(strStartTime, strFinishTime);
            DataTable dt_His_109 = Data_Access.Fun_SelectDate(strSql_109, GlobalVariableHandler.Instance.strConn_GPL_HISTORY, "His_109资料", "3-2");

            string strSql_pdi = Fun_SelectPDI_In_Coil_Thick_SQL(strIn_Coil_ID, strOut_Coil_ID);
            DataTable dt_pdi = Data_Access.Fun_SelectDate(strSql_pdi, GlobalVariableHandler.Instance.strConn_GPL, "Pdi_In_Coil_Thick资料", "3-2");

            DataTable dt_Select = dt_GR.Clone();

            if (dt_GR == null || dt_GR.Rows.Count <=0)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查无要汇出的PDO钢卷资料!", "汇出PDO", null, 2);
                return;
            }
            DataView dv_GR = new DataView(dt_GR);
            DataTable dt_distinctPass = dv_GR.ToTable(true, "部位", "道次");//1~9  //"Column1", "Column2"

            DataRow[] drArr;
            foreach (DataRow dri in dt_distinctPass.Rows)
            {
                DataRow dr = dt_Select.NewRow();
                drArr = dt_GR.Select($"部位 = {dri["部位"]} AND 道次 = {dri["道次"]}", $"Receive_Time ASC");
                dr = drArr[0];
                dt_Select.ImportRow(dr);
            }
            if (dt_GR_AVG != null && dt_GR_AVG.Rows.Count > 0)
            {
                foreach (DataRow dri in dt_Select.Rows)
                {                    
                    DataRow[] drArr_getAvg = dt_GR_AVG.Select($"部位 = {dri["部位"]} AND 道次 = {dri["道次"]}");
                    dri["avgSpeed"] = drArr_getAvg[0]["avgSpeed"];
                    dri["avgNo1Current"] = drArr_getAvg[0]["avgNo1Current"];
                    dri["avgNo2Current"] = drArr_getAvg[0]["avgNo2Current"];
                    dri["avgNo3Current"] = drArr_getAvg[0]["avgNo3Current"];
                    dri["avgNo4Current"] = drArr_getAvg[0]["avgNo4Current"];
                    dri["avgNo5Current"] = drArr_getAvg[0]["avgNo5Current"];
                    dri["avgNo6Current"] = drArr_getAvg[0]["avgNo6Current"];                   
                }
            }
            //if(dt_pdi != null && dt_pdi.Rows.Count > 0)
            //{
            //    strIn_Coil_Thick = dt_pdi.Rows[0]["In_Coil_Thick"].ToString().Trim();
            //}
            if (dt_His_109 != null || dt_His_109.Rows.Count > 0)
            {
                foreach (DataRow dri in dt_Select.Rows)
                {                   
                    DataRow[] drArr_getBeltLen = dt_His_109.Select($" DateTime >= '{dri["Receive_Time"]}' ", $"DateTime ASC");
                    dri["109_No1BeltLen"] = drArr_getBeltLen[0]["109_No1BeltLen"];
                    dri["109_No2BeltLen"] = drArr_getBeltLen[0]["109_No2BeltLen"];
                    dri["109_No3BeltLen"] = drArr_getBeltLen[0]["109_No3BeltLen"];
                    dri["109_No4BeltLen"] = drArr_getBeltLen[0]["109_No4BeltLen"];
                    dri["109_No5BeltLen"] = drArr_getBeltLen[0]["109_No5BeltLen"];
                    dri["109_No6BeltLen"] = drArr_getBeltLen[0]["109_No6BeltLen"];                    
                }
            }
            
            if (dt_Select != null || dt_Select.Rows.Count > 0)
            {
                string strSampleFile = "../../SampleFile/Export_PdoExcel.xlsx";
                string strFilePath = "Excel/Export_PdoExcel.xlsx";
                try
                {
                    //檢查範本路徑是否已有资料夹
                    FileInfo fi = new FileInfo(strFilePath);
                    if (!fi.Directory.Exists)
                    {
                        fi.Directory.Create();
                    }

                    //檢查範本路徑是否已有檔案存在,沒有就复制
                    if (!File.Exists(strFilePath))
                    {
                        File.Copy(Path.Combine(strSampleFile), Path.Combine(strFilePath), true);
                    }
                }
                catch (FileNotFoundException ffex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk(ffex.Message, $"讯息提示", null, 3);
                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ffex.Message}");
                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ffex.Message}");
                    return;
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                    return;
                }

                string FileName = $"{Txt_Out_Coil_ID.Text.Trim()}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx";

                //以範本档案建立新文件
                SaveFileDialog dialog = new SaveFileDialog();
                //設定檔案標題
                dialog.Title = "汇出Excel文件";
                //設定檔案型別
                dialog.Filter = "Excel Sheet|*.xlsx";
                //設定預設檔案型別顯示順序  
                dialog.FilterIndex = 1;
                //是否自動在檔名中新增副檔名
                dialog.AddExtension = true;
                //是否記憶上次開啟的目錄
                dialog.RestoreDirectory = true;
                //設定預設檔名
                dialog.FileName = FileName;
             

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (File.Exists(dialog.FileName))
                            File.Delete(dialog.FileName);

                        using (FileStream fileStream = File.Open(dialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            //為了 浮水印，改為讀取內建範本
                            IWorkbook workbook;

                            try
                            {
                                using (var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.ReadWrite))
                                {
                                    //strFilePath = "Excel/Export_EXCEL.xlsx"
                                    //if (Path.GetExtension(dialog.FileName).ToLower() == ".xls")
                                    //{
                                    //    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                                    //}
                                    //else
                                    //{
                                    workbook = new XSSFWorkbook(fs);
                                    //}
                                }

                                ISheet sheet = workbook.GetSheetAt(0);//獲取第一個工作表  //workbook.GetSheet("上表面研磨情况");
                                           
                                //取得 第一行Title 的 CellStyle
                                int copyFormatRow = 0;                                
                                Dictionary<int, ICellStyle> dicCellStyle_Title = new Dictionary<int, ICellStyle>();
                                for (int i = 0; i < 5; i++)
                                {
                                    dicCellStyle_Title.Add(i, sheet.GetRow(copyFormatRow).GetCell(i).CellStyle);
                                }
                               // sheet.RemoveRow(sheet.GetRow(copyFormatRow));

                                //取得 第五行 資料列 的 CellStyle
                                int copyFormatRow_value = 5;                               
                                Dictionary<int, ICellStyle> dicCellStyle_value = new Dictionary<int, ICellStyle>();
                                for (int i = 0; i < 26; i++)
                                {
                                    dicCellStyle_value.Add(i, sheet.GetRow(copyFormatRow_value).GetCell(i).CellStyle);
                                }
                                //sheet.RemoveRow(sheet.GetRow(copyFormatRow_value));

                                //將DataSet匯出為Excel
                                int intRowCount_All = dt_Select.Rows.Count;// downTimeTable.Count;//行數
                                var properties = dt_Select.Columns.Count;// downTimeTable.ElementAt(0).GetType().GetProperties();
                                int columnCount = properties;//.Count() - 2;//列數  扣掉error 與 item
                               
                                IRow row;

                                //設定每行每列的單元格, 
                                for (int i = 0; i < intRowCount_All; i++)
                                {                                   
                                    string strSession = dt_Select.Rows[i]["部位"].ToString();//
                                    string strPass = dt_Select.Rows[i]["道次"].ToString();//
                                    int.TryParse(strPass, out int intPass);
                                    //取得目前部位的最大道次
                                    DataRow[] drArr_S = dt_distinctPass.Select($"部位 = {strSession}" , $"道次 DESC");                             
                                    int.TryParse( drArr_S[0]["道次"].ToString(), out int intMaxS);
                                    int intRow_Title = 0;//頭中尾標題列位置
                                    string strCoil_Title = $"{Txt_Out_Coil_ID.Text.Trim()} 研磨情况";
                                    int intRow_value = 0;//頭中尾資料列開始位置
                                    int intRow_last = 0;//頭中尾資料列結束位置 //粗糙度Ra	粗糙度Rz	粗糙度Rmax	厚度
                                    if (strSession == "1")
                                    {                                        
                                        intRow_Title = 0;
                                        strCoil_Title += "（头部）";
                                        intRow_value = 4 + intPass - 1;//row = sheet.CreateRow(4 + i);  
                                        intRow_last = 11;
                                    }
                                    else if (strSession == "2")
                                    {
                                        intRow_Title = 13;
                                        strCoil_Title += "（中部）";
                                        intRow_value = 17 + intPass - 1;//row = sheet.CreateRow(12 + i);
                                        intRow_last = 24;
                                    }
                                    else if (strSession == "3")
                                    {
                                        intRow_Title = 25;
                                        strCoil_Title += "（尾部）";
                                        intRow_value = 29 + intPass - 1;//row = sheet.CreateRow(22 + i);   
                                        intRow_last = 36;
                                    }
                                    else 
                                    {
                                        intRow_Title = 0;
                                        strCoil_Title = "钢卷研磨情况";
                                        intRow_value = 1 + i; //row = sheet.CreateRow(1 + i);   
                                        intRow_last = 11;
                                    }
                                    NPOI.SS.UserModel.ICell cell_Title = Fun_NPOI_SetCell(sheet, intRow_Title, 0, strCoil_Title, NPOI.SS.UserModel.CellType.String);
                                    cell_Title.CellStyle = dicCellStyle_Title[0];

                                    #region 粗糙度Ra	粗糙度Rz	粗糙度Rmax	厚度	

                                    int intCol_last_Ra = 21;
                                    int intCol_last_Rz = 22;
                                    int intCol_last_Rmax = 23;
                                    int intCol_last_PdiT = 24;
                                    int intCol_last_PdoT = 25;

                                    string strCol_last_Ra, strCol_last_Rz, strCol_last_Rmax;
                                    string strCol_last_PdiT, strCol_last_PdoT;
                                    strCol_last_PdiT = (dt_pdi != null && dt_pdi.Rows.Count > 0) ? dt_pdi.Rows[0]["In_Coil_Thick"].ToString() : "";
                                    //strCol_last_PdoT = Txt_Out_Coil_Thick.Text.Trim();//用各部位的C25/C40厚度值

                                    if (intRow_last == 11)
                                    {
                                        strCol_last_Ra = Txt_Head_Rough_Ra.Text;// dt_Select.Rows[i]["Head_Rough_Ra"].ToString();
                                        strCol_last_Rz = Txt_Head_Rough_Rz.Text;//dt_Select.Rows[i]["Head_Rough_Rz"].ToString();
                                        strCol_last_Rmax = Txt_Head_Rough_Rmax.Text;//dt_Select.Rows[i]["Head_Rough_Rmax"].ToString();
                                        strCol_last_PdoT = $"{Txt_Head_C25_Thick.Text}/{Txt_Head_C40_Thick.Text}";
                                    }
                                    else if (intRow_last == 24)
                                    {
                                        strCol_last_Ra = Txt_Mid_Rough_Ra.Text;//dt_Select.Rows[i]["Mid_Rough_Ra"].ToString();
                                        strCol_last_Rz = Txt_Mid_Rough_Rz.Text;//dt_Select.Rows[i]["Mid_Rough_Rz"].ToString();
                                        strCol_last_Rmax = Txt_Mid_Rough_Rmax.Text;//dt_Select.Rows[i]["Mid_Rough_Rmax"].ToString();
                                        strCol_last_PdoT = $"{Txt_Mid_C25_Thick.Text}/{Txt_Mid_C40_Thick.Text}";
                                    }
                                    else if (intRow_last == 36)
                                    {
                                        strCol_last_Ra = Txt_Tail_Rough_Ra.Text;//dt_Select.Rows[i]["Tail_Rough_Ra"].ToString();
                                        strCol_last_Rz = Txt_Tail_Rough_Rz.Text;//dt_Select.Rows[i]["Tail_Rough_Rz"].ToString();
                                        strCol_last_Rmax = Txt_Tail_Rough_Rmax.Text;//dt_Select.Rows[i]["Tail_Rough_Rmax"].ToString();
                                        strCol_last_PdoT = $"{Txt_Tail_C25_Thick.Text}/{Txt_Tail_C40_Thick.Text}";
                                    }
                                    else 
                                    {
                                        strCol_last_Ra = Txt_Head_Rough_Ra.Text;//dt_Select.Rows[i]["Head_Rough_Ra"].ToString();
                                        strCol_last_Rz = Txt_Head_Rough_Rz.Text;//dt_Select.Rows[i]["Head_Rough_Rz"].ToString();
                                        strCol_last_Rmax = Txt_Head_Rough_Rmax.Text;//dt_Select.Rows[i]["Head_Rough_Rmax"].ToString();
                                        strCol_last_PdoT = $"{Txt_Head_C25_Thick.Text}/{Txt_Head_C40_Thick.Text}";
                                    }
                                    NPOI.SS.UserModel.ICell cell_other;

                                    cell_other = Fun_NPOI_SetCell(sheet, intRow_last, intCol_last_Ra, strCol_last_Ra, Fun_NPOI_GetCellType(strCol_last_Ra));
                                    cell_other.CellStyle = dicCellStyle_value[0];

                                    cell_other = Fun_NPOI_SetCell(sheet, intRow_last, intCol_last_Rz, strCol_last_Rz, Fun_NPOI_GetCellType(strCol_last_Rz));
                                    cell_other.CellStyle = dicCellStyle_value[0];

                                    cell_other = Fun_NPOI_SetCell(sheet, intRow_last, intCol_last_Rmax, strCol_last_Rmax, Fun_NPOI_GetCellType(strCol_last_Rmax));
                                    cell_other.CellStyle = dicCellStyle_value[0];

                                    cell_other = Fun_NPOI_SetCell(sheet, intRow_last, intCol_last_PdiT, strCol_last_PdiT, Fun_NPOI_GetCellType(strCol_last_PdiT));
                                    cell_other.CellStyle = dicCellStyle_value[0];

                                    cell_other = Fun_NPOI_SetCell(sheet, intRow_last, intCol_last_PdoT, strCol_last_PdoT, Fun_NPOI_GetCellType(strCol_last_PdoT));
                                    cell_other.CellStyle = dicCellStyle_value[0];

                                    #endregion

                                    string[] strDataCol = new string[] { 
                                         "道次", "No1方向", "avgSpeed",
                                         "avgNo1Current","avgNo2Current","avgNo3Current",
                                         "avgNo4Current","avgNo5Current", "avgNo6Current",
                                         "109_No1BeltLen","No1号数", "109_No2BeltLen","No2号数",
                                         "109_No3BeltLen","No3号数", "109_No4BeltLen","No4号数",
                                         "109_No5BeltLen","No5号数", "109_No6BeltLen","No6号数"   
                                    };
                                    //"部位","產速", "No2方向", "No3方向", "No4方向", "No5方向",  "No6方向",
                                    //  "Head_Rough_Ra","Head_Rough_Rz","Head_Rough_Rmax",
                                    //"Mid_Rough_Ra","Mid_Rough_Rz","Mid_Rough_Rmax",
                                    //"Tail_Rough_Ra","Tail_Rough_Rz","Tail_Rough_Rmax",
                                    //"In_Coil_Thick","Out_Coil_Thick"
                                    //"No1电流", "No2电流", "No3电流", "No4电流","No5电流","No6电流",

                                    //要填入資料的欄位:21
                                    for (int j = 0; j < 21; j++)
                                    {
                                        var value = dt_Select.Rows[i][strDataCol[j]].ToString();
                                        if (strDataCol[j] == "道次")
                                        {
                                            value += "Pass";
                                        }

                                        if (strDataCol[j] == "No1方向")
                                        {
                                            if (value == "0")
                                                value = "正";
                                            else if (value == "1")
                                                value = "反";
                                        }

                                       

                                            NPOI.SS.UserModel.ICell cell_value;//= Fun_NPOI_SetCell(sheet, 25, 0, value, NPOI.SS.UserModel.CellType.String);
                                        if (value == null)
                                        {
                                            //cell.SetCellValue("");
                                            cell_value = Fun_NPOI_SetCell(sheet, 1 + i, j, "", NPOI.SS.UserModel.CellType.String);
                                        }
                                        else
                                        {
                                            NPOI.SS.UserModel.CellType cellType = NPOI.SS.UserModel.CellType.String;
                                            if (double.TryParse(value, out double douValue))
                                                cellType = CellType.Numeric;
                                            else
                                                cellType = CellType.String;

                                            cell_value = Fun_NPOI_SetCell(sheet, intRow_value, j, value, cellType);
                                            cell_value.CellStyle = dicCellStyle_value[0];
                                        }
                                    }                                   
                                }
                                workbook.Write(fileStream);
                            }
                            catch (Exception ex)
                            {
                                DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                                PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出PDO资料失败{ex.Message}");
                                PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出PDO资料失败{ex.Message}");
                                return;
                            }

                        }

                        //log      
                        string strShowText = $"汇出 [{strOut_Coil_ID}] PDO资料 ";

                        DialogHandler.Instance.Fun_DialogShowOk(strShowText + " 成功!", $"讯息提示", null, 4);
                        EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");

                        EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出PDO资料 成功", strShowText);
                        PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");
                        PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");
                    }
                    catch (Exception ex)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                        File.Delete(dialog.FileName);
                        PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出PDO资料失败{ex.Message}");
                        PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出PDO资料失败{ex.Message}");
                        throw;
                    }
                }
                GC.Collect();
            }

        }

       

        public NPOI.SS.UserModel.ICell Fun_NPOI_SetCell(ISheet sheet, int iRow, int iCol, string value, CellType _celltype)
        {
            XSSFRow row;
            NPOI.SS.UserModel.ICell cell = null;
            if (sheet.GetRow(iRow) != null)
                row = (XSSFRow)sheet.GetRow(iRow);
            else
            {
                //int ostatniWiersz = sheet.LastRowNum;
                //row = (HSSFRow)sheet.CreateRow(ostatniWiersz + 1);//這樣會有問題
                row = (XSSFRow)sheet.CreateRow(iRow);//add row
            }
            if (row != null)
            {
                cell = row.GetCell(iCol);
                if (cell == null)
                {
                    cell = row.CreateCell(iCol, _celltype);//add cell
                }
                if (cell != null)
                {
                    //cell.SetCellType ( _celltype);//reset type不用reset也可以
                    if (_celltype == CellType.Numeric)
                        cell.SetCellValue(double.Parse(value));
                    else if (_celltype == CellType.Formula)
                        cell.SetCellFormula(value);
                    else
                        cell.SetCellValue(value);

                   
                    /*
                        CellType 類型 值
                        CELL_TYPE_NUMERIC 數值型 0
                        CELL_TYPE_STRING 字符串型 1
                        CELL_TYPE_FORMULA 公式型 2
                        CELL_TYPE_BLANK 空值 3
                        CELL_TYPE_BOOLEAN 布爾型 4
                        CELL_TYPE_ERROR 錯誤 5
                     */
                }
            }
            return cell;
        }

        private NPOI.SS.UserModel.CellType Fun_NPOI_GetCellType(string strvalue)
        {
            NPOI.SS.UserModel.CellType cellType = NPOI.SS.UserModel.CellType.String;
            if (double.TryParse(strvalue, out double douValue))
                cellType = CellType.Numeric;
            else
                cellType = CellType.String;

            return cellType;
        }
        #region Old 
        public void Fun_NPOI_Open(IWorkbook workbook, ISheet sheet, String fileName)
        {
            try
            {
                FileStream fileStream;

                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本 
                {
                    workbook = new XSSFWorkbook(fileStream); //xlsx數據讀入workbook 
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本 
                {
                    workbook = new HSSFWorkbook(fileStream); //xls數據讀入workbook 
                }
                sheet = workbook.GetSheetAt(0); //獲取第一個工作表 


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Fun_NPOI_Clear(ISheet sheet, int ifromRow)
        {

            //for (int i = (sheet.FirstRowNum + 0); i <= sheet.LastRowNum; i++)   //-- 每一列做迴圈
            //{
            //    HSSFRow row = (HSSFRow)sheet.GetRow(i);  //--不包含 Excel表頭列的 "其他資料列"

            //    if (row != null)
            //    {
            //        if (i >= ifromRow)
            //        {
            //            for (int j = row.FirstCellNum; j < row.LastCellNum; j++)   //-- 每一個欄位做迴圈
            //            {
            //                SetCell(i, j, "", CellType.Blank);
            //                //CellType.Blank);不會清空格式化的cell
            //                //CellType.Formula);清空格式化的cell,也清不是格式化的
            //            }
            //        }
            //    }
            //}
        }

        public void Fun_NPOI_SaveClose(IWorkbook workbook,ISheet sheet, string path)
        {
            //FileStream fs = null;
            //try
            //{
            //    sheet.ForceFormulaRecalculation = true;//更新公式的值 
            //    fs = new FileStream(path, FileMode.Create);
            //    workbook.Write(fs);
            //    fs.Close();

            //}
            //catch (Exception ex)
            //{
            //    if (fs != null)
            //    {
            //        fs.Close();
            //    }
            //    throw ex;
            //}
            //finally
            //{
            //    fileStream.Close();
            //}
        }

        private string Fun_SelectHis_107_109_SQL(string strIn_Coil_ID, string strStartTime, string strFinishTime)
        {
           
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT  L1L2_107.Date, L1L2_107.Time,  ");
            sb.AppendLine($" L1L2_107.DateTime, L1L2_107.CreateTime, L1L2_107.CoilID, ");
            sb.AppendLine($" L1L2_109.DateTime as '109_DateTime', L1L2_109.CreateTime as '109_CreateTime' , ");
            sb.AppendLine($" CurrentPassNumber as '道次', CurrentSession as '部位',  LineSpeed as '產速', ");
            sb.AppendLine($" gpSpeed.平均產速 ,   ");
                          
            sb.AppendLine($" No1GRABBeltRotateDirection as 'No1方向',No1GRABBeltMotorCurrent as 'No1电流', ");
            sb.AppendLine($" No1GRAbrBeltAccGriBeltLen as '109_No1公里数',  No1GRABBeltRoughness as 'No1号数', ");
                          
            sb.AppendLine($" No2GRABBeltRotateDirection as 'No2方向',No2GRABBeltCurrent as 'No2电流', ");
            sb.AppendLine($" No2GRAbrBeltAccGriBeltLen as '109_No2公里数',  No2GRABBeltRoughness as 'No2号数', ");
                          
            sb.AppendLine($" No3GRABBeltRotateDirection as 'No3方向',No3GRABBeltCurrent as 'No3电流', ");
            sb.AppendLine($" No3GRAbrBeltAccGriBeltLen as '109_No3公里数',  No3GRABBeltRoughness as 'No3号数', ");
                          
            sb.AppendLine($" No4GRABBeltRotatedirection as 'No4方向',No4GRABBeltCurrent as 'No4电流', ");
            sb.AppendLine($" No4GRAbrBeltAccGriBeltLen as '109_No4公里数',  No4GRABBeltRoughness as 'No4号数', ");
                          
            sb.AppendLine($" No5GRABBeltRotateDirection as 'No5方向',No5GRABBeltCurrent as 'No5电流', ");
            sb.AppendLine($" No5GRAbrBeltAccGriBeltLen as '109_No5公里数',  No5GRABBeltRoughness as 'No5号数', ");
                          
            sb.AppendLine($" No6GRABBeltRotateDirection as 'No6方向',No6GRABBeltCurrent as 'No6电流', ");
            sb.AppendLine($" No6GRAbrBeltAccGriBeltLen as '109_No6公里数',  No6GRABBeltRoughness as 'No6号数' ");
                          
            sb.AppendLine($" FROM   L1L2_107 ");
            sb.AppendLine($" LEFT JOIN   L1L2_109 on L1L2_107.DateTime = L1L2_109.DateTime ");
                          
            sb.AppendLine($" LEFT JOIN   (SELECT  CurrentPassNumber as '道次', CurrentSession as '部位', AVG(LineSpeed) as '平均產速'  ");
            sb.AppendLine($" 	 FROM    L1L2_107 ");
            sb.AppendLine($" 	 LEFT JOIN   L1L2_109 on L1L2_107.DateTime = L1L2_109.DateTime ");
            sb.AppendLine($" 	 WHERE CoilID = N'{strIn_Coil_ID}'    ");
            sb.AppendLine($" 	 AND (L1L2_107.CreateTime BETWEEN CONVERT(DATETIME, '{strStartTime}', 102) AND CONVERT(DATETIME, '{strFinishTime}', 102)) ");
            sb.AppendLine($" 	 Group by CurrentPassNumber,CurrentSession )  ");
            sb.AppendLine($"      gpSpeed on L1L2_107.CurrentPassNumber = gpSpeed.道次 and L1L2_107.CurrentSession = gpSpeed.部位  ");
                          
            sb.AppendLine($" WHERE CoilID = N'{strIn_Coil_ID}'   ");
            sb.AppendLine($" AND (L1L2_107.CreateTime BETWEEN CONVERT(DATETIME, '{strStartTime}', 102) AND CONVERT(DATETIME, '{strFinishTime}', 102)) ");
            sb.AppendLine($" ORDER BY CoilID, L1L2_107.CreateTime, CurrentSession,CurrentPassNumber ");
            sb.AppendLine($"  ");
            sb.AppendLine($"  ");
        


            string strSql = sb.ToString();
            return strSql;
        }

        private string Fun_SelectHis_107_SQL(string strIn_Coil_ID, string strStartTime, string strFinishTime)
        {
            //strIn_Coil_ID = "CA210282610000";
            //strStartTime = "2022-06-18 04:37:21";
            //strFinishTime = "2022-06-18 17:53:57";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT    L1L2_107.Date, L1L2_107.Time, L1L2_107.DateTime, L1L2_107.CreateTime, L1L2_107.CoilID, ");
            sb.AppendLine($"           CurrentPassNumber as '道次', CurrentSession as '部位',  LineSpeed as '產速', 0 as 'avgSpeed', ");
           
            ////先建欄位,之後再給值
            //sb.AppendLine($"  0.0 as 'In_Coil_Thick',  0.0 as 'Out_Coil_Thick',");//PDI In_Coil_Thick //PDO Out_Coil_Thick
            //sb.AppendLine($"  0.0 as 'Head_Rough_Ra',  0.0 as 'Head_Rough_Rz', 0.0 as 'Head_Rough_Rmax', ");//PDO
            //sb.AppendLine($"  0.0 as 'Mid_Rough_Ra',   0.0 as 'Mid_Rough_Rz',  0.0 as 'Mid_Rough_Rmax', ");//PDO
            //sb.AppendLine($"  0.0 as 'Tail_Rough_Ra',  0.0 as 'Tail_Rough_Rz', 0.0 as 'Tail_Rough_Rmax', ");//PDO

            sb.AppendLine($" 		   No1GRABBeltRotateDirection as 'No1方向',0 as 'No1电流', 0 as '109_No1BeltLen',  No1GRABBeltRoughness as 'No1号数', ");
            sb.AppendLine($" 		   No2GRABBeltRotateDirection as 'No2方向',0 as 'No2电流', 0 as '109_No2BeltLen', No2GRABBeltRoughness as 'No2号数', ");
            sb.AppendLine($" 		   No3GRABBeltRotateDirection as 'No3方向',0 as 'No3电流', 0 as '109_No3BeltLen', No3GRABBeltRoughness as 'No3号数', ");
            sb.AppendLine($" 		   No4GRABBeltRotatedirection as 'No4方向',0 as 'No4电流', 0 as '109_No4BeltLen', No4GRABBeltRoughness as 'No4号数', ");
            sb.AppendLine($" 		   No5GRABBeltRotateDirection as 'No5方向',0 as 'No5电流', 0 as '109_No5BeltLen', No5GRABBeltRoughness as 'No5号数', ");
            sb.AppendLine($" 		   No6GRABBeltRotateDirection as 'No6方向',0 as 'No6电流', 0 as '109_No6BeltLen', No6GRABBeltRoughness as 'No6号数'  ");          
            sb.AppendLine($" FROM       L1L2_107		 ");           
            sb.AppendLine($" WHERE CoilID = N'{strIn_Coil_ID}'    ");
            sb.AppendLine($" AND (L1L2_107.CreateTime BETWEEN CONVERT(DATETIME, '{strStartTime}', 102)  ");
            sb.AppendLine($"      AND CONVERT(DATETIME, '{strFinishTime}', 102)) ");
            sb.AppendLine($" ORDER BY CoilID, L1L2_107.CreateTime, CurrentSession,CurrentPassNumber ");
            sb.AppendLine($"  ");          
        
            string strSql = sb.ToString();
            return strSql;
        }

        private string Fun_SelectHis_107_AVG_SQL(string strIn_Coil_ID, string strStartTime, string strFinishTime)
        {
            //strIn_Coil_ID = "CA210282610000";
            //strStartTime = "2022-06-18 04:37:21";
            //strFinishTime = "2022-06-18 17:53:57";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT  CurrentPassNumber as '道次', CurrentSession as '部位', AVG(LineSpeed) as 'avgSpeed'  ");
            sb.AppendLine($" ,AVG(No1GRABBeltMotorCurrent) as 'No1电流'  ");
            sb.AppendLine($" ,AVG(No2GRABBeltCurrent) as 'No2电流'  ");
            sb.AppendLine($" ,AVG(No3GRABBeltCurrent) as 'No3电流'  ");
            sb.AppendLine($" ,AVG(No4GRABBeltCurrent) as 'No4电流'  ");
            sb.AppendLine($" ,AVG(No5GRABBeltCurrent) as 'No5电流'  ");
            sb.AppendLine($" ,AVG(No6GRABBeltCurrent) as 'No6电流'  ");

            sb.AppendLine($" FROM    L1L2_107			 ");
            sb.AppendLine($" WHERE CoilID = N'{strIn_Coil_ID}'    ");
            sb.AppendLine($" AND (L1L2_107.CreateTime BETWEEN CONVERT(DATETIME, '{strStartTime}', 102)  ");
            sb.AppendLine($"      AND CONVERT(DATETIME, '{strFinishTime}', 102)) ");
            sb.AppendLine($" Group by CurrentPassNumber,CurrentSession ");
     
            string strSql = sb.ToString();
            return strSql;
        }
        #endregion
        private string Fun_Select_TBL_GrindRecords_SQL(string strCoil_ID, string strPlan_No)
        {
            //strIn_Coil_ID = "CA210282610000";
            //strPlan_No = "GP260116";    
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT     Coil_ID, Plan_No,  ");
            sb.AppendLine($" Current_Pass  as '道次', Current_Senssion as '部位', 0 as 'avgSpeed', ");
            sb.AppendLine($" GR1_BELT_KIND, GR1_BELT_PARTICLE_NO as 'No1号数', GR1_BELT_ROTATE_DIR as 'No1方向', 0 as 'avgNo1Current', 0 as '109_No1BeltLen', GR1_BELT_SPEED, ");
            sb.AppendLine($" GR2_BELT_KIND, GR2_BELT_PARTICLE_NO as 'No2号数', GR2_BELT_ROTATE_DIR as 'No2方向', 0 as 'avgNo2Current', 0 as '109_No2BeltLen', GR2_BELT_SPEED, ");
            sb.AppendLine($" GR3_BELT_KIND, GR3_BELT_PARTICLE_NO as 'No3号数', GR3_BELT_ROTATE_DIR as 'No3方向', 0 as 'avgNo3Current', 0 as '109_No3BeltLen',  ");
            sb.AppendLine($" GR4_BELT_KIND, GR4_BELT_PARTICLE_NO as 'No4号数', GR4_BELT_ROTATE_DIR as 'No4方向', 0 as 'avgNo4Current', 0 as '109_No4BeltLen',  ");
            sb.AppendLine($" GR5_BELT_KIND, GR5_BELT_PARTICLE_NO as 'No5号数', GR5_BELT_ROTATE_DIR as 'No5方向', 0 as 'avgNo5Current', 0 as '109_No5BeltLen', ");
            sb.AppendLine($" GR6_BELT_KIND, GR6_BELT_PARTICLE_NO as 'No6号数', GR6_BELT_ROTATE_DIR as 'No6方向', 0 as 'avgNo6Current', 0 as '109_No6BeltLen', ");
            sb.AppendLine($" Receive_Time ");
            sb.AppendLine($" FROM      TBL_GrindRecords ");
            sb.AppendLine($" WHERE Coil_ID = N'{strCoil_ID}'   ");
            sb.AppendLine($" AND Plan_No = N'{strPlan_No}' ");
            //sb.AppendLine($"  ");      
            string strSql = sb.ToString();
            return strSql;
        }

        private string Fun_Select_TBL_GrindRecords_AVG_SQL(string strCoil_ID, string strPlan_No)
        {
            //strIn_Coil_ID = "CA210282610000";
            //strPlan_No = "GP260116";            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT  Current_Pass  as '道次', Current_Senssion as '部位',  ");
            sb.AppendLine($"  AVG(Line_Speed) as 'avgSpeed', ");
            sb.AppendLine($"  FLOOR(AVG(GR1_MOTOR_CURRENT)) as 'avgNo1Current', ");
            sb.AppendLine($"  FLOOR(AVG(GR2_MOTOR_CURRENT)) as 'avgNo2Current', ");
            sb.AppendLine($"  FLOOR(AVG(GR3_MOTOR_CURRENT)) as 'avgNo3Current', ");
            sb.AppendLine($"  FLOOR(AVG(GR4_MOTOR_CURRENT)) as 'avgNo4Current', ");
            sb.AppendLine($"  FLOOR(AVG(GR5_MOTOR_CURRENT)) as 'avgNo5Current', ");
            sb.AppendLine($"  FLOOR(AVG(GR6_MOTOR_CURRENT)) as 'avgNo6Current' ");
            sb.AppendLine($" FROM      TBL_GrindRecords ");
            sb.AppendLine($" WHERE Coil_ID = N'{strCoil_ID}'   ");
            sb.AppendLine($" AND Plan_No = N'{strPlan_No}' ");
            sb.AppendLine($" Group by Current_Pass,Current_Senssion ");
            //sb.AppendLine($"  ");    
            string strSql = sb.ToString();
            return strSql;
        }

        private string Fun_SelectHis_109_SQL( string strStartTime, string strFinishTime)
        {
            //strStartTime = "2022-06-18 04:37:21";
            //strFinishTime = "2022-06-18 17:53:57";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT  Date, Time, DateTime, CreateTime, ");
            sb.AppendLine($" 		 No1GRAbrBeltAccGriBeltLen as '109_No1BeltLen', ");
            sb.AppendLine($" 		 No2GRAbrBeltAccGriBeltLen as '109_No2BeltLen', ");
            sb.AppendLine($" 		 No3GRAbrBeltAccGriBeltLen as '109_No3BeltLen', ");
            sb.AppendLine($" 		 No4GRAbrBeltAccGriBeltLen as '109_No4BeltLen', ");
            sb.AppendLine($" 		 No5GRAbrBeltAccGriBeltLen as '109_No5BeltLen', ");
            sb.AppendLine($" 		 No6GRAbrBeltAccGriBeltLen as '109_No6BeltLen' ");
            sb.AppendLine($" FROM    L1L2_109 ");
            sb.AppendLine($" WHERE  L1L2_109.DateTime  BETWEEN CONVERT(DATETIME, '{strStartTime}', 102) ");
            sb.AppendLine($"        AND CONVERT(DATETIME, '{strFinishTime}', 102)  ");
          
            string strSql = sb.ToString();
            return strSql;
        }

        private string Fun_SelectPDI_In_Coil_Thick_SQL(string strIn_Coil_ID, string strOut_Coil_ID)
        {
            //strStartTime = "2022-06-18 04:37:21";
            //strFinishTime = "2022-06-18 17:53:57";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT  Plan_No,In_Coil_ID,In_Coil_Thick, ");       
            sb.AppendLine($" 		 Out_Coil_ID, CreateTime");          
            sb.AppendLine($" FROM    TBL_PDI ");
            sb.AppendLine($" WHERE  In_Coil_ID = '{strIn_Coil_ID}' ");
            sb.AppendLine($" AND Out_Coil_ID  = '{strOut_Coil_ID}' ");

            string strSql = sb.ToString();
            return strSql;
        }

        #region Cob 事件
        private void Cob_Sample_flag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read") { return; }            
            if (bolEditStatus == false) { return; }

            if (Cob_Sample_Flag.SelectedIndex == 0)
            {
                Cob_Sample_Frqn_Code.SelectedIndex = -1;
            }
        }

        private void Cob_Paper_Req_Code_SelectedValueChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read")
                return;

            if (bolEditStatus == false)
                return;

            if (Cob_Paper_Req_Code.SelectedIndex != -1)
            {
                if (Cob_Paper_Req_Code.SelectedValue.ToString() == "0")
                {
                    Txt_Out_Head_Paper_Length.Text = "0";
                    Txt_Out_Tail_Paper_Length.Text = "0";
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "1")
                {
                    double dou_Coil_Len = 0, dou_Coil_Len_H, dou_Coil_Len_T;

                    if (!string.IsNullOrEmpty(Txt_Out_Coil_Length.Text))
                    {
                        double.TryParse(Txt_Out_Coil_Length.Text, out dou_Coil_Len);
                    }
                    dou_Coil_Len_T = Math.Floor(dou_Coil_Len / 2);
                    dou_Coil_Len_H = dou_Coil_Len- dou_Coil_Len_T;

                    Txt_Out_Head_Paper_Length.Text = dou_Coil_Len_H.ToString();
                    Txt_Out_Tail_Paper_Length.Text = dou_Coil_Len_T.ToString();
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "2")
                {
                    Txt_Out_Head_Paper_Length.Text = "50";
                    Txt_Out_Tail_Paper_Length.Text = "50";
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "3")
                {
                    Txt_Out_Head_Paper_Length.Text = "30";
                    Txt_Out_Tail_Paper_Length.Text = "30";
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "4")
                {
                    Txt_Out_Head_Paper_Length.Text = "0";
                    Txt_Out_Tail_Paper_Length.Text = "80";
                }
                else
                {
                    Txt_Out_Head_Paper_Length.Text = "0";
                    Txt_Out_Tail_Paper_Length.Text = "0";
                }
            }
            else
            {

            }
        }

        private void Cob_Paper_Code_SelectedValueChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read")
                return;

            if (bolEditStatus == false)
                return;

            if (Cob_Paper_Code.SelectedIndex != -1)
            {
                if (Cob_Paper_Code.SelectedValue.ToString() == "01")
                {
                    Txt_Out_Head_Paper_Width.Text = "1020";
                    Txt_Out_Tail_Paper_Width.Text = "1020";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "02")
                {
                    Txt_Out_Head_Paper_Width.Text = "1220";
                    Txt_Out_Tail_Paper_Width.Text = "1220";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "03")
                {
                    Txt_Out_Head_Paper_Width.Text = "1020";
                    Txt_Out_Tail_Paper_Width.Text = "1020";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "04")
                {
                    Txt_Out_Head_Paper_Width.Text = "1220";
                    Txt_Out_Tail_Paper_Width.Text = "1220";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "05")
                {
                    Txt_Out_Head_Paper_Width.Text = "1020";
                    Txt_Out_Tail_Paper_Width.Text = "1020";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "06")
                {
                    Txt_Out_Head_Paper_Width.Text = "1220";
                    Txt_Out_Tail_Paper_Width.Text = "1220";
                }
                else
                {
                    Txt_Out_Head_Paper_Width.Text = "0";
                    Txt_Out_Tail_Paper_Width.Text = "0";
                }
            }
            else
            {

            }
        }

        private void Cob_Fixed_WT_Flag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read")
                return;

            if (bolEditStatus == false)
                return;

           
            if (Cob_Fixed_WT_Flag.SelectedIndex != -1)
            {
                if (Cob_Fixed_WT_Flag.SelectedIndex == 0)
                    Cob_End_Flag.SelectedIndex = 1;
            }
        }

        private void Cob_End_Flag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read")
                return;

            if (bolEditStatus == false)
                return;


            if (Cob_End_Flag.SelectedIndex != -1)
            {
                if (Cob_End_Flag.SelectedIndex == 0)
                    Cob_Fixed_WT_Flag.SelectedIndex = 1;
            }
        }
        #endregion

        #region 調整文字大小...中英切換
        private void Fun_LanguageIsEn_Font14_9(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }
        private void Fun_LanguageIsEn_Font12_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        #endregion

        #region 輸入驗證
        /// <summary>
        /// 僅可輸入數字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //&& (e.KeyChar != (char)Keys.Space) 
            //          數字                                            //backspace                    //Enter             
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))
            {
                e.Handled = true;
            }

        }

        /// <summary>
        /// 僅可輸入數字及1小數點
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_OnlyNumberPoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //&& (e.KeyChar != (char)Keys.Space) 
            //          數字                                            //backspace                    //Enter             
            //if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))
            //{
            //    e.Handled = true;
            //}
            base.OnKeyPress(e);
            if (!(
                (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.' && (this.Text.IndexOf(e.KeyChar) < 0) && (this.Text.Length > 0)) ||
                (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))//&& (e.KeyChar != (char)Keys.Space)
            {               //backspace                    //Enter
                e.Handled = true;
            }
        }

        /// <summary>
        /// 粗糙度_輸入提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rough_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }

            TextBox txtBoxCtr = (TextBox)sender;
            if (string.IsNullOrEmpty(txtBoxCtr.Text)) { return; }

            decimal dcText = Convert.ToDecimal(txtBoxCtr.Text);
            int intIntegerDigit = 5;//整數位數2 +小數位數3
            int intDecimalDigit = 3;//小數位數3

            //實際可輸入的整數位數
            decimal dcReIdg = intIntegerDigit - intDecimalDigit; //2

            string strOne = "1";
            string strDdg = strOne.PadRight(intDecimalDigit + 1, '0');//1000
            string strIdg = strOne.PadRight(intIntegerDigit + 1, '0');//10000

            decimal dcDdg = Convert.ToDecimal(strDdg);
            decimal dcIdg = Convert.ToDecimal(strIdg);
            decimal dcTol = dcText * dcDdg;
            if (intDecimalDigit != 0)
            {
                if (dcTol > dcIdg)
                {
                    string message = $"整数限{ dcReIdg}位，小数限{intDecimalDigit}位!";
                    string title = "提示资讯";
                    DialogHandler.Instance.Fun_DialogShowOk(message, title, null, 0);

                    txtBoxCtr.Focus();
                    return;
                }

                if ((dcTol % 1) != 0)
                {
                    string message = "小数位数限" + intDecimalDigit + "位！";
                    string title = "提示资讯";                   
                    DialogHandler.Instance.Fun_DialogShowOk(message, title, null, 0);

                    txtBoxCtr.Focus();
                    return;
                }
            }
            else
            {
                if (txtBoxCtr.Text.Contains("."))
                {           
                    string message = "该字段无小数位数！" + intDecimalDigit + "位！";
                    string title = "提示资讯";            
                    DialogHandler.Instance.Fun_DialogShowOk(message, title, null, 0);

                    txtBoxCtr.Focus();
                    return;
                }
            }
        }
        #endregion

        #region 目前無用之功能

        /// <summary>
        /// 複製新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CopyNew_Click(object sender, EventArgs e)
        {
            //if (Out_mat_No.Text.IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message("未查询钢卷");
            //    return;
            //}
            //else 
            if (!Txt_Out_Coil_ID.Text.IsEmpty())
            {
                ReadOnlyHandler.Instance.ReadOnly(Tab_PDODetailPage, false);
                ReadOnlyHandler.Instance.ReadOnly(Tab_PDODefectPage, false);
                //新增
                Fun_SetBottonEnabled(Btn_New, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
                //修改
                Fun_SetBottonEnabled(Btn_Update, false);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, false);


                Btn_Cancel.Visible = true; //取消
                Btn_Save.Visible = true;//儲存
                Txt_Out_Coil_ID.Text = string.Empty;
                Txt_In_Coil_ID.Text = string.Empty;
                //Starttime.Text = string.Empty;
                //Finishtime.Text = string.Empty;
            }
            EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}新增复制", "新增复制动作");
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 檢查PDO是否有上傳過，有上傳過不給修改
        /// </summary>
        private void Fun_CheckUploadFlg()
        {

            string strSql = SqlFactory.Frm_3_2_UpdatePDO_Check_DB_PDO(Txt_Out_Coil_ID.Text);
            DataTable dtUploadFlag = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDO上传记录", "3-2");

            if (dtUploadFlag.IsNull())
            {
                Fun_UpdatePDO_Check();
            }
            else if (!dtUploadFlag.Rows[0][0].ToString().Equals("1"))
            {
                Fun_UpdatePDO_Check();
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"[{Txt_Out_Coil_ID.Text.Trim()}]已上传MMS，不允许修改!");
                EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 开启修改出口钢卷编号:{Txt_Out_Coil_ID.Text.Trim() } PDO", $" 出口钢卷编号:{ Txt_Out_Coil_ID.Text.Trim() } PDO已上传至MMS，不允许修改!");
                PublicComm.ClientLog.Info($"鋼卷號:[{Txt_Out_Coil_ID.Text.Trim()}]已上傳至MMS，不允許修改");
                PublicComm.akkaLog.Info($"鋼卷號:[{Txt_Out_Coil_ID.Text.Trim()}]已上傳至MMS，不允許修改");
            }
        }

        private void Fun_UpdatePDO_Check()
        {
            ReadOnlyControl(Tab_PDODetailPage, false);
            ReadOnlyControl(Tab_PDODefectPage, false);

            //複製新增
            Fun_SetBottonEnabled(Btn_CopyNew, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, false);

            Btn_Cancel.Visible = true; //取消
            Btn_Save.Visible = true; //儲存
        }

        #endregion
    }
}
