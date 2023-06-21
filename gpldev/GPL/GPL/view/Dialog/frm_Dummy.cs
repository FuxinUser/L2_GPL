using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository;
using DBService.Repository.PDI;
using GPLManager.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_Dummy : Form
    {

        #region 變數
        string strSql = string.Empty, strGetCoil = string.Empty;
        int intTopCanNotMove;
        DataTable dtGetScheduleCoilList = new DataTable();
        DataTable dtDummy = new DataTable();
        DataTable dtGetSchedule = new DataTable();
        DataRow[] drSeq ;
        decimal Seq_No_Last, Seq_No_Next, DummyCoilSeq_No;
        #endregion

        public Frm_Dummy()
        {
            InitializeComponent();
        }

        private void Frm_Dummy_Load(object sender, EventArgs e)
        {
            if(PublicForms._Dummy == null) PublicForms._Dummy = this;
            //過度鋼卷清單
            Fun_SelectDummyList();
            //ComboBox 排程鋼卷
            SelectSchedule_CoilID();
            if (dtGetSchedule.Rows.Count == 0)
            {
                radio_Insert.Enabled = false;
                radio_InsertFirst.Checked = true;
                radio_InsertFirst.Text = "插入第一笔";
            }
            else if (dtGetSchedule.Rows.Count < 3)
            { 
                radio_InsertFirst.Enabled = false;
            }
            else
            {
                //nothing
            }
        }
        private void Fun_SelectDummyList()
        {
            strSql = SqlFactory.Frm_DummyCoil_DummyList_DB_PDI();
            dtDummy = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"过渡卷清单");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(dgv_Dummy, dtDummy);
            DGVColumnsHandler.Instance.Frm_Dummy(dgv_Dummy);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(dgv_Dummy);
        }
        /// <summary>
        /// ComboBox 排程鋼卷
        /// </summary>
        private void SelectSchedule_CoilID()
        {
            strSql = SqlFactory.Frm_DummyCoil_ScheduleList_DB_Schedule_PDI();
            dtGetScheduleCoilList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"排程钢卷清单");

            if (dtGetScheduleCoilList == null) return;
            if (dtGetScheduleCoilList.Rows.Count.Equals(0)) return;
            
            if (!Fun_TopScheduleLock())
                intTopCanNotMove = 0;
            else
                intTopCanNotMove = 3;

            if(intTopCanNotMove != 0 )
            {
                if(dtGetScheduleCoilList.Rows.Count >= intTopCanNotMove)
                {
                    for (int i = 0; i < intTopCanNotMove; i++)
                    {
                        dtGetScheduleCoilList.Rows[0].Delete();
                        dtGetScheduleCoilList.AcceptChanges();
                    }
                }
                else
                {
                    for (int i = 0; i < dtGetScheduleCoilList.Rows.Count + 1; i++)
                    {
                        dtGetScheduleCoilList.Rows[0].Delete();
                        dtGetScheduleCoilList.AcceptChanges();
                    }
                }
                //dtGetScheduleCoilList.Rows[0].Delete();
                //dtGetScheduleCoilList.Rows[1].Delete();
                //dtGetScheduleCoilList.Rows[2].Delete();
                //dtGetScheduleCoilList.AcceptChanges();
            }
           
            
            cbo_Schedule.DisplayMember = nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID);
            cbo_Schedule.ValueMember = nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID);
            cbo_Schedule.DataSource = dtGetScheduleCoilList;
        }
        /// <summary>
        /// 檢查是否可以調整前3顆排程
        /// </summary>
        /// <returns>false = 0:未鎖 ; true = 1:鎖定  </returns>
        private bool Fun_TopScheduleLock()
        {
            bool bolLock = true;
            string strSql = SqlFactory.Frm_1_1_SQL_Select_SystemSetting_TopScheduleLock();
            DataTable dtTopLock = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "TopScheduleLock");
            if (dtTopLock != null && dtTopLock.Rows.Count > 0)
            {
                bolLock = dtTopLock.Rows[0]["Value"].ToString().Trim() == "0" ? false : true;
            }
            return bolLock;
        }
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            //操作行为记录
            //EventLogHandler.Instance.EventPush_Message($"插入过渡卷确认动作");
            PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:插入過渡捲確認動作");
            if (Txt_DummyCoil.Text.Trim().IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk($"请选择过渡卷!", "插入过渡卷提示", null, 0);
                return;
            }
            if (!radio_Insert.Checked && !radio_InsertFirst.Checked)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"请选择插入位置!", "插入过渡卷提示", null, 0);
                return;
            }
            if (radio_Insert.Checked && string.IsNullOrEmpty(cbo_Schedule.Text))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"请选择插入位置,不能为空!", "插入过渡卷提示", null, 0);
                return;
            }

            

            //string strInDummy_Plan_no = "";

            //要从 dtGetScheduleCoilList 取得插入位置的下一笔钢卷的计划号 给 strInDummy_Plan_no          
            DataRow[] drA;
            int intIndex;
            bool bolOpenLock = false;
            //插入指定位置 之后
            if (radio_Insert.Checked)
            {
                SearchCoilSeq_No(cbo_Schedule.Text);
                //先取得 指定插入的 钢卷 index
                drA = dtGetScheduleCoilList.Select($" {nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)} = '{cbo_Schedule.Text}' ");
                intIndex = dtGetScheduleCoilList.Rows.IndexOf(drA[0]);

                ////如果有下一颗钢卷 , 插入的过度卷的计划号 要更新为下一颗钢卷的计划号 ; 如果没有下一颗钢卷,则计划号不变
                //if (dtGetScheduleCoilList.Rows.Count >= intIndex + 1)                
                //    strInDummy_Plan_no = dtGetSchedule.Rows[intIndex + 1][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString();                 
                //else                
                //    strInDummy_Plan_no = "";      

            }
            else 
            {
                //排程全空時,插入過度捲處理
                if(dtGetSchedule.Rows.Count == 0)
                {
                    DummyCoilSeq_No =  0;
                    bolOpenLock = true;
                }
                else
                {
                    //插入至第四颗钢卷 位置      //if (radio_InsertFirst.Checked)
                    decimal decNo1;
                    //如果已鎖定前三顆鋼捲,dtGetScheduleCoilList會移除前三顆捲號 ;
                    if (intTopCanNotMove > 0)
                    {
                        if (dtGetScheduleCoilList.Rows.Count <= 0)
                        {
                            decNo1 = (decimal)(float.Parse(dtGetSchedule.Rows[dtGetSchedule.Rows.Count - 1][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString()));
                            DummyCoilSeq_No = decNo1 + 1;
                        }
                        else
                        {
                            decNo1 = (decimal)(float.Parse(dtGetSchedule.Rows[2][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString()));
                            decimal decNo2 = (decimal)(float.Parse(dtGetSchedule.Rows[3][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString()));
                            decimal decNoAvg = (decNo1 + decNo2) / 2;
                            //SearchCoilSeq_No();
                            DummyCoilSeq_No = decNoAvg;

                        }
                    }
                    else
                    {
                        //如果沒有鎖定前三顆鋼捲,dtGetScheduleCoilList正常 ; 可以取得第三筆

                        //先取得 第三颗 钢卷 index
                        drA = dtGetScheduleCoilList.Select($" {nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)} = '{dtGetSchedule.Rows[2][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)]}' ");
                        intIndex = dtGetScheduleCoilList.Rows.IndexOf(drA[0]);

                        //取得第三颗钢卷的 Seq_No
                        decNo1 = (decimal)(float.Parse(dtGetSchedule.Rows[2][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString()));

                        //判断 排程资料笔数 有大于等于 4 , 才能以上下钢卷Seq_No 相加 除2 给插入的过度卷 Seq_No ;
                        if (dtGetScheduleCoilList.Rows.Count > intIndex + 1)
                        {
                            decimal decNo2 = (decimal)(float.Parse(dtGetSchedule.Rows[3][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString()));
                            decimal decNoAvg = (decNo1 + decNo2) / 2;
                            //SearchCoilSeq_No();
                            DummyCoilSeq_No = decNoAvg;// (decimal)(float.Parse(dtGetSchedule.Rows[3][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString()) - 0.5);
                                                       //strInDummy_Plan_no = dtGetSchedule.Rows[3][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString();
                        }
                        else
                        {
                            // 如果没有下一颗钢卷,则以第三颗 Seq_No+1 给插入的过度卷 Seq_No , 插入的过度卷计划号 不变
                            DummyCoilSeq_No = decNo1 + 1;
                            //strInDummy_Plan_no = "";
                        }
                    }
                }

                

            }
           
            strSql = SqlFactory.Frm_DummyCoil_InsertDummy_DB_Schedule(Txt_DummyCoil.Text, DummyCoilSeq_No);
           // Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL,"插入过渡卷");
            if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "插入过渡卷"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"使用者:{PublicForms.Main.lblLoginUser.Text} 插入过渡卷失败", "插入过渡卷", null, 3);
                EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 插入过渡卷[{Txt_DummyCoil.Text}]失败");
                Close();
                return;
            }
            else
            {
                //成功 插入過度捲 後,將 TBL_Production_Schedule 的 UpdateTime 都更新
               string strSql_update = SqlFactory.Frm_DummyCoil_UpdateAllScheduleTime();
                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql_update, GlobalVariableHandler.Instance.strConn_GPL, "更新排程UpdateTime"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"使用者:{PublicForms.Main.lblLoginUser.Text} 过渡卷后更新 排程UpdateTime 失败", "更新排程UpdateTime", null, 3);
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 过渡卷后更新 排程UpdateTime 失败");
                    
                }
            }

            //排程為空時,插入過度捲後,解除排程鎖定
            if (bolOpenLock)
            {
                string strSql_TopLock = SqlFactory.Frm_1_1_SQL_Update_SystemSetting_TopScheduleLock("0");
                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql_TopLock, GlobalVariableHandler.Instance.strConn_GPL, "TopLock"))
                {
                    //EventLogHandler.Instance.EventPush_Message($"");
                    return;
                }
            }
           

            //判断 插入的过度卷计划号 不是空值 , 更新该过度卷计划号为 下一笔排程的钢卷的计划号
            if (!string.IsNullOrEmpty(Txt_DummyCoil.Text.Trim()))
            {
                DataRow[] drD = dtDummy.Select($" {nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{Txt_DummyCoil.Text}' ");
                if(drD.Length >1)
                    drD = dtDummy.Select($" {nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{Txt_DummyCoil.Text}' AND {nameof(PDIEntity.TBL_PDI.Plan_No)} = '{Txt_DummyPlanNo.Text}'");

                string strOldDummy_Plan_no = drD[0][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString();

                string strDu_Count = drD[0]["Du_Count"].ToString();
                int.TryParse(strDu_Count, out int intNewDu_Count);
                string strInDummy_Plan_no = "DU"+ (intNewDu_Count+1).ToString().PadLeft(6, '0'); 

                string strSql_Pdi = SqlFactory.Frm_DummyCoil_UpdateDummy_DB_PDI(Txt_DummyCoil.Text, strOldDummy_Plan_no, strInDummy_Plan_no);
                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql_Pdi, GlobalVariableHandler.Instance.strConn_GPL, "插入过渡卷"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"使用者:{PublicForms.Main.lblLoginUser.Text} 变更过渡卷计划号失败", "变更过渡卷计划号", null, 3);
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 变更过渡卷[{Txt_DummyCoil.Text}]计划号失败");
                    Close();
                    return;
                }
            }                 
            //刷新1-1排程
            PublicForms.PDISchl.Fun_ReLoad();
            //通知server 排程有变更
            SCCommMsg.CS03_ScheduleChange Msg = new SCCommMsg.CS03_ScheduleChange
            {
                Source = "GPL_HMI",
                SchStatus = SCCommMsg.ScheduleStatus.ADJUST,
                OperatorID = string.Empty,
                ReasonCode = string.Empty,
                EntryCoilID = string.Empty
            };
            PublicComm.Client.Tell(Msg);

            EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}通知Server排程已调整", "告知Server将已调整完毕的Schedule捞出");
            EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 插入过渡卷[{Txt_DummyCoil.Text}]成功");
            PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:使用者:{PublicForms.Main.lblLoginUser.Text} 插入过渡卷[{Txt_DummyCoil.Text}]成功");
            PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:使用者:{PublicForms.Main.lblLoginUser.Text} 已通知Server插入过渡卷[{Txt_DummyCoil.Text}]");
            PublicComm.akkaLog.Info($"訊息名稱:插入過渡卷 訊息:使用者:{PublicForms.Main.lblLoginUser.Text} 已通知Server插入过渡卷[{Txt_DummyCoil.Text}]");
            Close();
        }
        public void FunGetDataTable(DataTable dtSchedule,string Coil_ID)
        {
            //if (dtSchedule.IsNull()) return;
            dtGetSchedule = dtSchedule;
            strGetCoil = Coil_ID;
        }
        private void SearchCoilSeq_No(string Coil_ID = "")
        {
            if (!Coil_ID.IsEmpty())
            {
                drSeq = dtGetSchedule.Select($"[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = '{Coil_ID}'");
                Seq_No_Last = (decimal)float.Parse(drSeq[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString());
            }
            else
            {
                Seq_No_Last = short.Parse(dtGetSchedule.Rows[2][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString());
            }
            drSeq = dtGetSchedule.Select($"[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] > '{Seq_No_Last}'");
                       
            if(drSeq.Length <=0)
            {
                //如果沒有下一顆鋼捲
                Seq_No_Next = Seq_No_Last + 1;
            }
            else
            {
                //有下一顆鋼捲
                Seq_No_Next = (decimal)float.Parse(drSeq[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString());
            }
            
            DummyCoilSeq_No = (Seq_No_Last + Seq_No_Next) / 2;
        }

        private void Dgv_Dummy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtDummy.IsNull()) return;

            if (dgv_Dummy.DgvIsNull() || dgv_Dummy.CurrentIsNull()) return;
            DataRow dr = dtDummy.Rows[dgv_Dummy.CurrentRow.Index];

            Txt_DummyCoil.Text = dr[nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString().Trim();
            Txt_DummyPlanNo.Text = dr[nameof(PDIEntity.TBL_PDI.Plan_No)].ToString().Trim();
        }

        private void Frm_Dummy_Shown(object sender, EventArgs e)
        {
            cbo_Schedule.SelectedValue = strGetCoil;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
       
    }
}
