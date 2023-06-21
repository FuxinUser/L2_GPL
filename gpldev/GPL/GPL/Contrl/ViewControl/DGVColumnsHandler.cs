using DBService.Repository;
using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.PDI;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using DBService.Repository.ScheduleDelete_CoilReject_Record;
using DBService.Repository.SteelNoToMaterialGrade;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GPLManager.Util;
using static GPLManager.DataBaseTableFactory;
using DBService.Repository.PDO;
using DBService.Repository.Utility;
using DBService.Repository.CoilScheduleDelete;
using DBService.Repository.GradeGroups;
using DBService.Repository.GrindPlan;
using DBService.Repository.BeltPatterns;
using DBService.Repository.Belt;
using DBService.Repository.EventLog;
using DBService.Repository.LineFaultRecords;
using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.BeltMaterials;
using DBService.Repository.BeltSuppliers;
using DBService.Repository.LangSwitch;

namespace GPLManager
{
    public class DGVColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly DGVColumnsHandler INSTANCE = new DGVColumnsHandler();
        }

        public static DGVColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }
        public void Fun_DataGridViewDataDisplay(DataGridView dgv, DataTable dt)
        {
            ColumnsClear(dgv);

            if (dt.IsNull())
            {
                dgv.DataSource = null;
                EventLogHandler.Instance.EventPush_Message($"资料表无清单可显示");
                return;
            }

            dgv.ColumnHeadersVisible = false;
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = dt;

            ColumnsClear(dgv);
        }

        public void Frm_1_1PDISchedule_Dgv_ScheduleColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("序号", "No", 80));
            dgv.Columns.Add(Fun_SetGridViewColumn("计划号", nameof(PDIEntity.TBL_PDI.Plan_No), 150));
           // dgv.Columns.Add(Fun_SetGridViewColumn("顺序号", nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Thick),180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Width), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(PDIEntity.TBL_PDI.In_Coil_Wt), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒类型", nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code), 160));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒内径(mm)", nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸方式", nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code), 160));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸类型", nameof(PDIEntity.TBL_PDI.In_Paper_Code), 160));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口头部垫纸长度", nameof(PDIEntity.TBL_PDI.Head_Paper_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口头部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Head_Paper_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口尾部垫纸长度", nameof(PDIEntity.TBL_PDI.Tail_Paper_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口尾部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Paper_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("内部钢种", nameof(PDIEntity.TBL_PDI.St_No),180));
            dgv.Columns.Add(Fun_SetGridViewColumn("密度(kg/m^3)", nameof(PDIEntity.TBL_PDI.Density)));
            dgv.Columns.Add(Fun_SetGridViewColumn("牌号", nameof(PDIEntity.TBL_PDI.Sg_Sign), 400));
            dgv.Columns.Add(Fun_SetGridViewColumn("返修类型", nameof(PDIEntity.TBL_PDI.Repair_Type), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("订单表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际好面朝向", nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开卷方向", nameof(PDIEntity.TBL_PDI.Uncoil_Direction), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷编号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸种类", nameof(PDIEntity.TBL_PDI.Out_Paper_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒代码", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒直径(mm)", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口绑带方式", nameof(PDIEntity.TBL_PDI.Pack_Mode), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("正反研磨标记", nameof(PDIEntity.TBL_PDI.Grind_Flag), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷来源", nameof(PDIEntity.TBL_PDI.Origin_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("上游工序", nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("下游工序", nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最大值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最小值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标宽度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Width),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("合同号", nameof(PDIEntity.TBL_PDI.Order_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("脱脂标记", nameof(PDIEntity.TBL_PDI.Skim_Flag), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带标记", nameof(PDIEntity.TBL_PDI.Head_Leader_Attached), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部打孔位置", nameof(PDIEntity.TBL_PDI.Head_Hole_Position), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带厚度(mm)", nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带宽度(mm)", nameof(PDIEntity.TBL_PDI.Head_Leader_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带长度(m)", nameof(PDIEntity.TBL_PDI.Head_Leader_Length), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带标记", nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部打孔位置", nameof(PDIEntity.TBL_PDI.Tail_Hole_Position), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带厚度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带长度(m)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Length), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带钢种", nameof(PDIEntity.TBL_PDI.Head_Leader_St_No), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带钢种", nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部未轧制区域(m)", nameof(PDIEntity.TBL_PDI.Head_Off_Gauge), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部未轧制区域(m)", nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷上次研磨面", nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷外表面研磨次数", nameof(PDIEntity.TBL_PDI.Grinding_Count_Out), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷内表面研磨次数", nameof(PDIEntity.TBL_PDI.Grinding_Count_In), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷指定研磨面", nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("内表面精度代码",nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("外表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("抛光类型", nameof(PDIEntity.TBL_PDI.Polishing_Type), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("客户代码", nameof(PDIEntity.TBL_PDI.Order_Cust_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("工序代码", nameof(PDIEntity.TBL_PDI.Process_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("平坦度平均值 ", nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR), 200));

            dgv.Columns[0].Frozen = true;
            dgv.Columns[1].Frozen = true;
            dgv.Columns[2].Frozen = true;
        }

        public void Frm_1_1PDISchedule_Dgv_PDISearchColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("计划号", nameof(PDIEntity.TBL_PDI.Plan_No), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("顺序号", nameof(PDIEntity.TBL_PDI.Mat_Seq_No ), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Thick), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Width), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(PDIEntity.TBL_PDI.In_Coil_Wt), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒类型", nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒内径(mm)", nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸方式", nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸类型", nameof(PDIEntity.TBL_PDI.In_Paper_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口头部垫纸长度", nameof(PDIEntity.TBL_PDI.Head_Paper_Length), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口头部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Head_Paper_Width), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口尾部垫纸长度", nameof(PDIEntity.TBL_PDI.Tail_Paper_Length), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口尾部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Paper_Width), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("内部钢种", nameof(PDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("密度(kg/m^3)", nameof(PDIEntity.TBL_PDI.Density)));
            dgv.Columns.Add(Fun_SetGridViewColumn("牌号", nameof(PDIEntity.TBL_PDI.Sg_Sign), 1000));
            dgv.Columns.Add(Fun_SetGridViewColumn("返修类型", nameof(PDIEntity.TBL_PDI.Repair_Type), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("订单表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际好面朝向", nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开卷方向", nameof(PDIEntity.TBL_PDI.Uncoil_Direction), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷编号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸种类", nameof(PDIEntity.TBL_PDI.Out_Paper_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒代码", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒直径", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口绑带方式", nameof(PDIEntity.TBL_PDI.Pack_Mode), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("正反研磨标记", nameof(PDIEntity.TBL_PDI.Grind_Flag), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷来源", nameof(PDIEntity.TBL_PDI.Origin_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("上游工序", nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("下游工序", nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最大值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最小值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标宽度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("合同号", nameof(PDIEntity.TBL_PDI.Order_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("脱脂标记", nameof(PDIEntity.TBL_PDI.Skim_Flag), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带标记", nameof(PDIEntity.TBL_PDI.Head_Leader_Attached), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部打孔位置", nameof(PDIEntity.TBL_PDI.Head_Hole_Position), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带厚度(mm)", nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带宽度(mm)", nameof(PDIEntity.TBL_PDI.Head_Leader_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带长度(m)", nameof(PDIEntity.TBL_PDI.Head_Leader_Length), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带标记", nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部打孔位置", nameof(PDIEntity.TBL_PDI.Tail_Hole_Position), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带厚度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带长度(m)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Length), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带钢种", nameof(PDIEntity.TBL_PDI.Head_Leader_St_No), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带钢种", nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部未轧制区域(m)", nameof(PDIEntity.TBL_PDI.Head_Off_Gauge), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部未轧制区域(m)", nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷上次研磨面", nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷外表面研磨次数", nameof(PDIEntity.TBL_PDI.Grinding_Count_Out), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷内表面研磨次数", nameof(PDIEntity.TBL_PDI.Grinding_Count_In), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷指定研磨面", nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("内表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("外表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("抛光类型", nameof(PDIEntity.TBL_PDI.Polishing_Type), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("客户代码", nameof(PDIEntity.TBL_PDI.Order_Cust_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("工序代码", nameof(PDIEntity.TBL_PDI.Process_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("平坦度平均值 ", nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR), 200));

            dgv.Columns[0].Frozen = true; 
            dgv.Columns[1].Frozen = true; 
            dgv.Columns[2].Frozen = true;
        }

        public void Frm_1_1PDISchedule_Dgv_DMColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("计划号", nameof(PDIEntity.TBL_PDI.Plan_No), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("顺序号", nameof(PDIEntity.TBL_PDI.Mat_Seq_No ), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Thick), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Width), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(PDIEntity.TBL_PDI.In_Coil_Wt), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter), 230));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒类型", nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒内径(mm)", nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸方式", nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸类型", nameof(PDIEntity.TBL_PDI.In_Paper_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口头部垫纸长度(m)", nameof(PDIEntity.TBL_PDI.Head_Paper_Length), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口头部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Head_Paper_Width), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口尾部垫纸长度(m)", nameof(PDIEntity.TBL_PDI.Tail_Paper_Length), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口尾部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Paper_Width), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("内部钢种", nameof(PDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("密度(kg/m^3)", nameof(PDIEntity.TBL_PDI.Density)));
            dgv.Columns.Add(Fun_SetGridViewColumn("牌号", nameof(PDIEntity.TBL_PDI.Sg_Sign), 1000));
            dgv.Columns.Add(Fun_SetGridViewColumn("返修类型", nameof(PDIEntity.TBL_PDI.Repair_Type), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("订单表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际好面朝向", nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开卷方向", nameof(PDIEntity.TBL_PDI.Uncoil_Direction), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷编号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸种类", nameof(PDIEntity.TBL_PDI.Out_Paper_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒代码", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒直径(mm)", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口绑带方式", nameof(PDIEntity.TBL_PDI.Pack_Mode), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("正反研磨标记", nameof(PDIEntity.TBL_PDI.Grind_Flag), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷来源", nameof(PDIEntity.TBL_PDI.Origin_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("上游工序", nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("下游工序", nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最大值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最小值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标宽度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("合同号", nameof(PDIEntity.TBL_PDI.Order_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("脱脂标记", nameof(PDIEntity.TBL_PDI.Skim_Flag), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带标记", nameof(PDIEntity.TBL_PDI.Head_Leader_Attached), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部打孔位置", nameof(PDIEntity.TBL_PDI.Head_Hole_Position), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带厚度(mm)", nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带宽度(mm)", nameof(PDIEntity.TBL_PDI.Head_Leader_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带长度(m)", nameof(PDIEntity.TBL_PDI.Head_Leader_Length), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带标记", nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部打孔位置", nameof(PDIEntity.TBL_PDI.Tail_Hole_Position), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带厚度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Width), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带长度(m)", nameof(PDIEntity.TBL_PDI.Tail_Leader_Length), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带钢种", nameof(PDIEntity.TBL_PDI.Head_Leader_St_No), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带钢种", nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部未轧制区域(m)", nameof(PDIEntity.TBL_PDI.Head_Off_Gauge), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部未轧制区域(m)", nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷上次研磨面", nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷外表面研磨次数", nameof(PDIEntity.TBL_PDI.Grinding_Count_Out), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷内表面研磨次数", nameof(PDIEntity.TBL_PDI.Grinding_Count_In), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷指定研磨面", nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("内表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("外表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("抛光类型", nameof(PDIEntity.TBL_PDI.Polishing_Type), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("客户代码", nameof(PDIEntity.TBL_PDI.Order_Cust_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("工序代码", nameof(PDIEntity.TBL_PDI.Process_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("平坦度平均值 ", nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR), 200));

            dgv.Columns[0].Frozen = true;
            dgv.Columns[1].Frozen = true;
            dgv.Columns[2].Frozen = true;

        }

        public void Frm_1_2_PDIDetail302Columns(DataGridView dgv)
        {
            //dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", "Coil_ID", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊接电压设定-前火炬(Volts)", "WeldVoltageSettingFrontTorch", 300));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊接电压设定-后火炬(Volts)", "WeldVoltageSettingRearTorch", 300));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊线速度-前火炬(mpm)", "WeldWireSpeedFrontTorch", 250));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊线速度-后火炬(mpm)", "WeldWireSpeedRearTorch", 250));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊接电流(10^-1A)", "WeldCurrent", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("火炬架焊接速度(mmpm)", "TorchCarriageWeldSpeed", 250));
            //dgv.Columns.Add(Fun_SetGridViewColumn("OperatorSide(前部)焊缝(mm)", "OperatorSideFrontWeldGap", 300));
            //dgv.Columns.Add(Fun_SetGridViewColumn("DriveSide(后部)焊缝(mm)", "DriveSideRearWeldGap", 250));
            //dgv.Columns.Add(Fun_SetGridViewColumn("开始注入时间(sec)", "StartPuddleTime", 180));
            //dgv.Columns.Add(Fun_SetGridViewColumn("停止注入时间(sec)", "StopPuddleTime", 180));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊接计划号", "WeldScheduleNumber", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("退火器功率百分比", "AnnealerPowerPercentage", 220));
            //dgv.Columns.Add(Fun_SetGridViewColumn("焊接温度作用点", "WeldTempActPoint", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头段引线焊接点位置", "HeadEndLeaderWeldPointPosition", 250));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头段引线焊接点打孔距离(mm)", "HeadEndLeaderWeldPointDistanceFromPunchHole", 320));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾段引线焊接点位置", "TailEndLeaderWeldPointPosition", 250));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾段引线焊接点打孔距离(mm)", "TailEndLeaderWeldPointDistanceFromPunchHole", 320));
            //dgv.Columns.Add(Fun_SetGridViewColumn("建立时间", "Create_DateTime", 300));
        }

        public void Frm_1_3_DeleteRecordColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CoilNo), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("代码", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.ReasonCode), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("备注", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks), 1000));
            dgv.Columns.Add(Fun_SetGridViewColumn("登入者", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.OperatorId), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("日期", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime), 300));
        }

        public void Frm_2_1TrackingDgv_OffColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)",nameof(PDIEntity.TBL_PDI.In_Coil_Wt)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸方式",nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸类型",nameof(PDIEntity.TBL_PDI.In_Paper_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("内部钢种", nameof(PDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("订单表面精度代碼",nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际表面精度代碼", nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开卷方向", nameof(PDIEntity.TBL_PDI.Uncoil_Direction), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("好面朝向", nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷编号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸类型", nameof(PDIEntity.TBL_PDI.Out_Paper_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒类型", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒外径(mm)", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("綁帶方式", nameof(PDIEntity.TBL_PDI.Pack_Mode)));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷来源", nameof(PDIEntity.TBL_PDI.Origin_Code),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("上游工序", nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("下游工序", nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标宽度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最大值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最小值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)));
        }

        public void Frm_2_1TrackingDgv_OnColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷编号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(PDIEntity.TBL_PDI.In_Coil_Wt)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("切废重量", nameof(PDIEntity.TBL_PDI.Scraped_Weight)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒类型", nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口套筒内径(mm)", nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸方式", nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸类型", nameof(PDIEntity.TBL_PDI.In_Paper_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部垫纸长度(m)", nameof(PDIEntity.TBL_PDI.Head_Paper_Length)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部垫纸宽度(mm)",nameof(PDIEntity.TBL_PDI.Head_Paper_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部垫纸长度(m)", nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部垫纸宽度(mm)", nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("内部钢种", nameof(PDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("密度(kg/m^3)", nameof(PDIEntity.TBL_PDI.Density)));
            dgv.Columns.Add(Fun_SetGridViewColumn("返修类型", nameof(PDIEntity.TBL_PDI.Repair_Type)));
            dgv.Columns.Add(Fun_SetGridViewColumn("订单表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("实际表面精度代码", nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("好面朝向", nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开卷方向",nameof(PDIEntity.TBL_PDI.Uncoil_Direction), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷编号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸种类", nameof(PDIEntity.TBL_PDI.Out_Paper_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒內徑(mm)", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒類型", nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口绑带方式", nameof(PDIEntity.TBL_PDI.Pack_Mode)));
            dgv.Columns.Add(Fun_SetGridViewColumn("试批计划号", nameof(PDIEntity.TBL_PDI.Test_Plan_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷来源", nameof(PDIEntity.TBL_PDI.Origin_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("上游工序",nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("下游工序", nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标宽度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最大值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)));
            dgv.Columns.Add(Fun_SetGridViewColumn("目标厚度最小值(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("合同号", nameof(PDIEntity.TBL_PDI.Order_No), 200));
        }

        /// <summary>
        /// 缺陷
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_Defect_Columns(DataGridView Dgv)
        {
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 200));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(PDIEntity.TBL_PDI.Plan_No), 200));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口卷号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 200));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(1)", nameof(PDIEntity.TBL_PDI.D01_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(1)", nameof(PDIEntity.TBL_PDI.D01_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(1)", nameof(PDIEntity.TBL_PDI.D01_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(1)", nameof(PDIEntity.TBL_PDI.D01_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(1)", nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(1)", nameof(PDIEntity.TBL_PDI.D01_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(1)", nameof(PDIEntity.TBL_PDI.D01_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(1)", nameof(PDIEntity.TBL_PDI.D01_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(1)", nameof(PDIEntity.TBL_PDI.D01_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(2)", nameof(PDIEntity.TBL_PDI.D02_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(2)", nameof(PDIEntity.TBL_PDI.D02_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(2)", nameof(PDIEntity.TBL_PDI.D02_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(2)", nameof(PDIEntity.TBL_PDI.D02_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(2)", nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(2)", nameof(PDIEntity.TBL_PDI.D02_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(2)", nameof(PDIEntity.TBL_PDI.D02_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(2)", nameof(PDIEntity.TBL_PDI.D02_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(2)", nameof(PDIEntity.TBL_PDI.D02_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(3)", nameof(PDIEntity.TBL_PDI.D03_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(3)", nameof(PDIEntity.TBL_PDI.D03_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(3)", nameof(PDIEntity.TBL_PDI.D03_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(3)", nameof(PDIEntity.TBL_PDI.D03_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(3)", nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(3)", nameof(PDIEntity.TBL_PDI.D03_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(3)", nameof(PDIEntity.TBL_PDI.D03_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(3)", nameof(PDIEntity.TBL_PDI.D03_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(3)", nameof(PDIEntity.TBL_PDI.D03_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(4)", nameof(PDIEntity.TBL_PDI.D04_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(4)", nameof(PDIEntity.TBL_PDI.D04_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(4)", nameof(PDIEntity.TBL_PDI.D04_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(4)", nameof(PDIEntity.TBL_PDI.D04_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(4)", nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(4)", nameof(PDIEntity.TBL_PDI.D04_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(4)", nameof(PDIEntity.TBL_PDI.D04_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(4)", nameof(PDIEntity.TBL_PDI.D04_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(4)", nameof(PDIEntity.TBL_PDI.D04_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(5)", nameof(PDIEntity.TBL_PDI.D05_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(5)", nameof(PDIEntity.TBL_PDI.D05_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(5)", nameof(PDIEntity.TBL_PDI.D05_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(5)", nameof(PDIEntity.TBL_PDI.D05_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(5)", nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(5)", nameof(PDIEntity.TBL_PDI.D05_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(5)", nameof(PDIEntity.TBL_PDI.D05_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(5)", nameof(PDIEntity.TBL_PDI.D05_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(5)", nameof(PDIEntity.TBL_PDI.D05_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(6)", nameof(PDIEntity.TBL_PDI.D06_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(6)", nameof(PDIEntity.TBL_PDI.D06_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(6)", nameof(PDIEntity.TBL_PDI.D06_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(6)", nameof(PDIEntity.TBL_PDI.D06_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(6)", nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(6)", nameof(PDIEntity.TBL_PDI.D06_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(6)", nameof(PDIEntity.TBL_PDI.D06_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(6)", nameof(PDIEntity.TBL_PDI.D06_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(6)", nameof(PDIEntity.TBL_PDI.D06_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(7)", nameof(PDIEntity.TBL_PDI.D07_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(7)", nameof(PDIEntity.TBL_PDI.D07_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(7)", nameof(PDIEntity.TBL_PDI.D07_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(7)", nameof(PDIEntity.TBL_PDI.D07_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(7)", nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(7)", nameof(PDIEntity.TBL_PDI.D07_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(7)", nameof(PDIEntity.TBL_PDI.D07_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(7)", nameof(PDIEntity.TBL_PDI.D07_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(7)", nameof(PDIEntity.TBL_PDI.D07_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(8)", nameof(PDIEntity.TBL_PDI.D08_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(8)", nameof(PDIEntity.TBL_PDI.D08_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(8)", nameof(PDIEntity.TBL_PDI.D08_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(8)", nameof(PDIEntity.TBL_PDI.D08_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(8)", nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(8)", nameof(PDIEntity.TBL_PDI.D08_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(8)", nameof(PDIEntity.TBL_PDI.D08_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(8)", nameof(PDIEntity.TBL_PDI.D08_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(8)", nameof(PDIEntity.TBL_PDI.D08_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(9)", nameof(PDIEntity.TBL_PDI.D09_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(9)", nameof(PDIEntity.TBL_PDI.D09_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(9)", nameof(PDIEntity.TBL_PDI.D09_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(9)", nameof(PDIEntity.TBL_PDI.D09_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(9)", nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(9)", nameof(PDIEntity.TBL_PDI.D09_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(9)", nameof(PDIEntity.TBL_PDI.D09_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(9)", nameof(PDIEntity.TBL_PDI.D09_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(9)", nameof(PDIEntity.TBL_PDI.D09_QGrade), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(10)", nameof(PDIEntity.TBL_PDI.D10_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(10)", nameof(PDIEntity.TBL_PDI.D10_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(10)", nameof(PDIEntity.TBL_PDI.D10_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(10)", nameof(PDIEntity.TBL_PDI.D10_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(10)", nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(10)", nameof(PDIEntity.TBL_PDI.D10_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(10)", nameof(PDIEntity.TBL_PDI.D10_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(10)", nameof(PDIEntity.TBL_PDI.D10_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(10)", nameof(PDIEntity.TBL_PDI.D10_QGrade), 150));

            Dgv.Columns[1].Frozen = true;

        }
        public void Frm_2_2_PDOColumns(DataGridView dgv)
        {
            //dgv.Columns.Add(Fun_SetGridViewColumn("客戶代碼", "Order_Cust_Code", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("合同号", "OrderNo", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("计划号", "PlanNo", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷号", "Out_mat_No", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷号", "In_mat_No", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("生产开始时间", "Starttime", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("生产结束时间", "Finishtime", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("班次", "Shift", 100));
            //dgv.Columns.Add(Fun_SetGridViewColumn("班别", "Team", 100));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口卷外径(mm)", "Out_mat_Outer_Diameter"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口卷内径(mm)", "out_mat_inner"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口净重(kg)", "Out_mat_wt"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口毛重(kg)", "Out_mat_gs"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口厚度(mm)", "Out_mat_Thick"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口宽度(mm)", "Out_mat_Width"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口长度(m)", "Out_mat_Length"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸种类", "Paper_Code"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", "Paper_Req_Code"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口头部垫纸长(m)", "Out_Paper_Head_Length", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口头部垫纸宽(mm)", "Out_Paper_Head_Width", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口尾部垫纸长(m)", "Out_Paper_Tail_Length", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口尾部垫纸宽(mm)", "Out_Paper_Tail_Width", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒内径(mm)", "Sleeve_Inner_Exit"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒类型", "Sleeve_Type_Exit"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部打孔位置", "Head_PunchHole_Position"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部导带长度(m)", "Head_LeaderLength"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部导带宽度(mm)", "Head_Leader_Width"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部导带厚度(mm)", "Head_Leader_Thickness"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾部打孔位置", "Tail_PunchHole_Position"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带长度(m)", "Tail_LeaderLength"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带宽度(mm)", "Tail_Leader_Width"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带厚度(mm)", "Tail_Leader_Thickness"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部切废长度(m)", "Scraped_length_entry"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾不切废长度(m)", "Scraped_length_exit"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部导带钢种", "Head_leader_st_no", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带钢种", "Tail_leader_st_no", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("卷曲方向", "Curly_Direction"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("好面朝向", "Base_Surface"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("封锁责任者", "Inspector"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("封锁标记", "Hold_flag", 150));
            //dgv.Columns.Add(Fun_SetGridViewColumn("封锁原因代码", "Hold_cause_code", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("取样标记", "Sample_flag", 150));
            //dgv.Columns.Add(Fun_SetGridViewColumn("切边标记", "trim_flag", 150));
            //dgv.Columns.Add(Fun_SetGridViewColumn("分卷标记", "Segement_Flag", 150));
            //dgv.Columns.Add(Fun_SetGridViewColumn("最终卷标记", "end_flag", 150));
            //dgv.Columns.Add(Fun_SetGridViewColumn("废品标记", "SCRAP_FLAG", 150));
            //dgv.Columns.Add(Fun_SetGridViewColumn("取样位置", "SAMPLE_FRQN_CODE"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("未焊导带代码", "No_Leader_code"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("表面精度代码", "Surface_Accuracy"));
            //dgv.Columns.Add(Fun_SetGridViewColumn("头部未轧制区域", "Head_Off_Gauge", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("尾部未轧制区域", "Tail_Off_Gauge", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("钢卷上次研磨面", "Pre_Grinding_Surface", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("钢卷外表面研磨次数", "Grinding_Count_Out", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("钢卷内表面研磨次数", "Grinding_Count_In", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("钢卷指定研磨面", "Appoint_Grinding_Surface", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("建立时间", "CreateTime", 200));
            //dgv.Columns[54].Visible = false;
        }

        public void Frm_3_1PDOColumns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("上传状态", nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("合同号", nameof(PDOEntity.TBL_PDO.Order_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("计划号", nameof(PDOEntity.TBL_PDO.Plan_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷号", nameof(PDOEntity.TBL_PDO.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷号", nameof(PDOEntity.TBL_PDO.In_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("生产开始时间",  nameof(PDOEntity.TBL_PDO.Start_Time), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("生产结束时间", nameof(PDOEntity.TBL_PDO.Finish_Time), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("班次", nameof(PDOEntity.TBL_PDO.Shift), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("班别", nameof(PDOEntity.TBL_PDO.Team), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口卷外径(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口卷内径(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Inner_Diameter),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口净重(kg)", nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口毛重(kg)", nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Thick),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口宽度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Width),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口长度(m)", nameof(PDOEntity.TBL_PDO.Out_Coil_Length),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(PDOEntity.TBL_PDO.St_No),300));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸种类", nameof(PDOEntity.TBL_PDO.Out_Paper_Code),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口垫纸方式", nameof(PDOEntity.TBL_PDO.Out_Paper_Req_Code ),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口头部垫纸长(m)", nameof(PDOEntity.TBL_PDO.Head_Paper_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口头部垫纸宽(mm)", nameof(PDOEntity.TBL_PDO.Head_Paper_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口尾部垫纸长(m)", nameof(PDOEntity.TBL_PDO.Tail_Paper_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口尾部垫纸宽(mm)", nameof(PDOEntity.TBL_PDO.Tail_Paper_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒内径(mm)", nameof(PDOEntity.TBL_PDO.Out_Sleeve_Diameter),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口套筒类型", nameof(PDOEntity.TBL_PDO.Out_Sleeve_Type_Code),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部打孔位置", nameof(PDOEntity.TBL_PDO.Head_Hole_Position),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带长度(m)", nameof(PDOEntity.TBL_PDO.Head_Leader_Length),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带宽度(mm)", nameof(PDOEntity.TBL_PDO.Head_Leader_Width),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带厚度(mm)", nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部打孔位置", nameof(PDOEntity.TBL_PDO.Tail_Hole_Position),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带长度(m)", nameof(PDOEntity.TBL_PDO.Tail_Leader_Length),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带宽度(mm)", nameof(PDOEntity.TBL_PDO.Tail_Leader_Width),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带厚度(mm)", nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部导带钢种", nameof(PDOEntity.TBL_PDO.Head_Leader_St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部导带钢种", nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("卷曲方向", nameof(PDOEntity.TBL_PDO.Winding_Dire), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("好面朝向", nameof(PDOEntity.TBL_PDO.Better_Surf_Ward_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("封锁责任者", nameof(PDOEntity.TBL_PDO.Hold_Maker)));
            dgv.Columns.Add(Fun_SetGridViewColumn("封锁标记", nameof(PDOEntity.TBL_PDO.Hold_Flag),150));
            dgv.Columns.Add(Fun_SetGridViewColumn("封锁原因代码", nameof(PDOEntity.TBL_PDO.Hold_Cause_Code),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("取样标记", nameof(PDOEntity.TBL_PDO.Sample_Flag), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("最终卷标记", nameof(PDOEntity.TBL_PDO.Final_Coil_Flag),150));
            dgv.Columns.Add(Fun_SetGridViewColumn("废品标记", nameof(PDOEntity.TBL_PDO.Scrap_Flag),150));
            dgv.Columns.Add(Fun_SetGridViewColumn("取样位置", nameof(PDOEntity.TBL_PDO.Sample_Pos_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("表面精度代码", nameof(PDOEntity.TBL_PDO.Surface_Accu_Code)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头部未轧制区域(m)", nameof(PDOEntity.TBL_PDO.Head_Off_Gauge), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾部未轧制区域(m)", nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷上次研磨面", nameof(PDOEntity.TBL_PDO.Pre_Grinding_Surface), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷外表面研磨次数", nameof(PDOEntity.TBL_PDO.Grinding_Count_Out), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷内表面研磨次数", nameof(PDOEntity.TBL_PDO.Grinding_Count_In), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷指定研磨面", nameof(PDOEntity.TBL_PDO.Appoint_Grinding_Surface), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("建立时间", nameof(PDOEntity.TBL_PDO.CreateTime), 200));
            //dgv.Columns[54].Visible = false;
        }

        public void Frm_DetailOpenColumns(DataGridView dgv, string strDataType)
        {
            if (strDataType == "PDO")
            {
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(PDOEntity.TBL_PDO.Plan_No), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷号", nameof(PDOEntity.TBL_PDO.Out_Coil_ID), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷号", nameof(PDOEntity.TBL_PDO.In_Coil_ID), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产开始时间", nameof(PDOEntity.TBL_PDO.Start_Time), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产结束时间", nameof(PDOEntity.TBL_PDO.Finish_Time), 200));
            }
            else if (strDataType == "PDI")
            {
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(PDIEntity.TBL_PDI.Plan_No), 200));               
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("更新时间", nameof(PDIEntity.TBL_PDI.CreateTime), 200));              
            }
            else
            {
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", "Plan_No", 200));
            }
              
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ClearSelection();
        }
        public void Frm_3_3CoilList(DataGridView dgv)
        {
            //dgv.Columns.Add(Fun_SetGridViewColumn("客户代号", nameof(PDOEntity.TBL_PDO.Order_Cust_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("计划号", nameof(PDOEntity.TBL_PDO.Plan_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷号", nameof(PDOEntity.TBL_PDO.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷号", nameof(PDOEntity.TBL_PDO.In_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开始生产时间", nameof(PDOEntity.TBL_PDO.Start_Time), 200));//StartTime
            dgv.Columns.Add(Fun_SetGridViewColumn("结束生产时间", nameof(PDOEntity.TBL_PDO.Finish_Time), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(PDOEntity.TBL_PDO.St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷长度(m)", nameof(PDOEntity.TBL_PDO.Out_Coil_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷宽度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头段C40厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("中段C40厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾段C40厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头段C25厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("中段C25厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾段C25厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷重量(kg)", nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)));
        }

        public void Frm_2_2CoilList(DataGridView dgv)
        {
            ////204欄位
            ////dgv.Columns.Add(Fun_SetGridViewColumn("Date", "Date", 200));
            ////dgv.Columns.Add(Fun_SetGridViewColumn("Time", "Time", 200));
            ////dgv.Columns.Add(Fun_SetGridViewColumn("DateTime", "DateTime", 200));
            ////dgv.Columns.Add(Fun_SetGridViewColumn("PresetPosition", "PresetPosition", 200));
            ////dgv.Columns.Add(Fun_SetGridViewColumn("TestPlanNo", "TestPlanNo", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("CoilId", "CoilId", 200));          
            //dgv.Columns.Add(Fun_SetGridViewColumn("SteelGrade", "SteelGrade", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("CoilLength", "CoilLength", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("CoilThickness", "CoilThickness", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("CoilWidth", "CoilWidth", 200));

            //dgv.Columns.Add(Fun_SetGridViewColumn("FlattenerIntermesh1", "FlattenerIntermesh1", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("FlattenerIntermesh2", "FlattenerIntermesh2", 200));

            //dgv.Columns.Add(Fun_SetGridViewColumn("PassNumberForCoilHeadGrinding", "PassNumberForCoilHeadGrinding", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("PassNumberForCoilCenterGrinding", "PassNumberForCoilCenterGrinding", 200));
            //dgv.Columns.Add(Fun_SetGridViewColumn("PassNumberForCoilTailGrinding", "PassNumberForCoilTailGrinding", 200));

            //PDO PDI欄位
            dgv.Columns.Add(Fun_SetGridViewColumn("客户代号", nameof(PDIEntity.TBL_PDI.Order_Cust_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("开始生产时间", nameof(PDOEntity.TBL_PDO.Start_Time), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("结束生产时间", nameof(PDOEntity.TBL_PDO.Finish_Time), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(PDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Thick), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷长度(m)", nameof(PDOEntity.TBL_PDO.Out_Coil_Length), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷宽度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Width), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头段C40厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("中段C40厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾段C40厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("头段C25厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("中段C25厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("尾段C25厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷重量(kg)", nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)));
        }

        public void Frm_Entry(DataGridView dgv)
        {                                                                                                                                                                                                                                                                               
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Thick), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Width), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(PDIEntity.TBL_PDI.In_Coil_Wt), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口垫纸方式", nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code), 160));
            dgv.Columns.Add(Fun_SetGridViewColumn("内部钢种", nameof(PDIEntity.TBL_PDI.St_No), 160));
            dgv.Columns.Add(Fun_SetGridViewColumn("出口钢卷编号", nameof(PDIEntity.TBL_PDI.Out_Coil_ID), 200));
        }
       
        public void Frm_4_2_Utility(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("接收时间", nameof(UtilityEntity.TBL_Utility.Receive_Time), 280));           
            dgv.Columns.Add(Fun_SetGridViewColumn("班次", nameof(UtilityEntity.TBL_Utility.Shift), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("班别", nameof(UtilityEntity.TBL_Utility.Team), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("压缩空气(nm³)", nameof(UtilityEntity.TBL_Utility.CompressedAir), 350));
            dgv.Columns.Add(Fun_SetGridViewColumn("蒸汽(kg)", nameof(UtilityEntity.TBL_Utility.Steam), 350));
            dgv.Columns.Add(Fun_SetGridViewColumn("间接冷却水(m³)", nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater), 350));
            dgv.Columns.Add(Fun_SetGridViewColumn("冲洗水(m³)", nameof(UtilityEntity.TBL_Utility.RinseWater), 350));

        }

        public void Frm_Dummy(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("计划号", nameof(PDIEntity.TBL_PDI.Plan_No), 120));
            dgv.Columns.Add(Fun_SetGridViewColumn("钢卷编号", nameof(PDIEntity.TBL_PDI.In_Coil_ID), 180));
            dgv.Columns.Add(Fun_SetGridViewColumn("使用次数", "Du_Count", 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷厚度(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Thick)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷宽度(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Width)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷重量(kg)",nameof(PDIEntity.TBL_PDI.In_Coil_Wt)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(PDIEntity.TBL_PDI.In_Coil_Length)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷内径(mm)",nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)));
            dgv.Columns.Add(Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)));

        }

        public void Frm_4_1_LineDelayRecord(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("上传MMS", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS), 120));
            dgv.Columns.Add(Fun_SetGridViewColumn("机组号", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("日期", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("班次", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no),100));
            dgv.Columns.Add(Fun_SetGridViewColumn("班组", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group), 100));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机开始时间", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机结束时间", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机位置", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location), 160));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机位置描述", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc), 400));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机持续时间", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey), 200,true));
            //dgv.Columns.Add(Fun_SetGridViewColumn("故障代码", nameof(LineFaultRecordsModel.TBL_LineFaultRecords.Fault_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机原因代码", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机原因描述", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("停机原因备注", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark), 400));
            dgv.Columns.Add(Fun_SetGridViewColumn("机械部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("电器部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("L3原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("生产部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("正常停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("其他部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o), 300));
            dgv.Columns.Add(Fun_SetGridViewColumn("换辊原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r), 250));
            dgv.Columns.Add(Fun_SetGridViewColumn("磨辊间原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs), 300));

            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("降速代码", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("降速原因", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause), 200));

            //dgv.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)].DefaultCellStyle.Format = "N2";
            //dgv.Columns[5].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss.fff";
            //dgv.Columns[6].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss.fff";
        }

        public void Frm_4_4_GradeGroup(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("钢种代号", nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)));
            dgv.Columns.Add(Fun_SetGridViewColumn("顾客代号", nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)));
            dgv.Columns.Add(Fun_SetGridViewColumn("客户品质等级群组", nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)));
        }

        public void Frm_4_4_GrindPlan(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("客户品质等级群组", nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)));
            dgv.Columns.Add(Fun_SetGridViewColumn("厚度区间(开始)", nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)));
            dgv.Columns.Add(Fun_SetGridViewColumn("厚度区间(结束)", nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)));
        }

        public void Frm_4_4_BeltPatterns(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("皮带参数模板", nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)));
            dgv.Columns.Add(Fun_SetGridViewColumn("道次区间(开始)", nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)));
            dgv.Columns.Add(Fun_SetGridViewColumn("道次区间(结束)", nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)));
        }

        public void Frm_4_5_Belts(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("皮带编号", nameof(BeltAccEntity.TBL_Belts.Belt_No), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("种类", nameof(BeltAccEntity.TBL_Belts.Belt_Type), 120));
            dgv.Columns.Add(Fun_SetGridViewColumn("皮带号数", nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("供应商代码", nameof(TBL_BeltSuppliers.SUPPLIER_NAME), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("材质代码", nameof(BeltAccEntity.TBL_Belts.Material_Code), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("总里程(Belt)", nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("总里程(Strip)", nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("研磨机号", nameof(BeltAccEntity.TBL_Belts.Mount_GR_No), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("", nameof(BeltAccEntity.TBL_Belts.Suppler_Code)));
            dgv.Columns[8].Visible = false;
        }

        public void Frm_4_5_Materials(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("材料代码", nameof(BeltMaterialEntity.TBL_BeltMaterials.MATERIAL_CODE), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("材料名称", nameof(BeltMaterialEntity.TBL_BeltMaterials.MATERIAL_NAME), 400));
        }

        public void Frm_4_5_BeltSuppliers(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("供应商代码", nameof(BeltSuppliersEntity.TBL_BeltSuppliers.SUPPLIER_CODE), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("供应商名称", nameof(BeltSuppliersEntity.TBL_BeltSuppliers.SUPPLIER_NAME), 400));
        }

        public void Frm_4_3_Tension(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade),300));
            dgv.Columns.Add(Fun_SetGridViewColumn("宽度最小值(m)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("宽度最大值(m)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("厚度最小值(mm)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("厚度最大值(mm)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max),200));
            dgv.Columns.Add(Fun_SetGridViewColumn("产线速度", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("收卷机张力", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("修改日期", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime), 360));
        }

        public void Frm_4_3_Flattener(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade),300));
            dgv.Columns.Add(Fun_SetGridViewColumn("厚度最大值(mm)", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("厚度最小值(mm)", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("抗拉最大值", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("抗拉最小值", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("Intermesh 1", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("Intermesh 2", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2), 200));
            dgv.Columns.Add(Fun_SetGridViewColumn("修改日期", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime), 380));
        }

        public void frm_5_1_EventLog(DataGridView dgv)
        {
            dgv.Columns.Add(Fun_SetGridViewColumn("建立时间", nameof(EventLogEntity.TBL_EventLog.CreateTime), 220));
            dgv.Columns.Add(Fun_SetGridViewColumn("系统编号", nameof(EventLogEntity.TBL_EventLog.System_ID), 150));
            dgv.Columns.Add(Fun_SetGridViewColumn("Server子系统名称", nameof(EventLogEntity.TBL_EventLog.Function_Block)));
            dgv.Columns.Add(Fun_SetGridViewColumn("电脑名称", nameof(EventLogEntity.TBL_EventLog.FrameGroup_No), 400));
            dgv.Columns.Add(Fun_SetGridViewColumn("Client画面编号", nameof(EventLogEntity.TBL_EventLog.Frame_No)));
            dgv.Columns.Add(Fun_SetGridViewColumn("事件类别", nameof(EventLogEntity.TBL_EventLog.Event_Type)));
            dgv.Columns.Add(Fun_SetGridViewColumn("事件描述", nameof(EventLogEntity.TBL_EventLog.Event_Description), 700));
            dgv.Columns.Add(Fun_SetGridViewColumn("事件语法", nameof(EventLogEntity.TBL_EventLog.Command), 1000));
            //dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //事件内容 填满剩余的空间
            dgv.Columns[nameof(EventLogEntity.TBL_EventLog.Command)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//.DisplayedCells;
            dgv.Columns[nameof(EventLogEntity.TBL_EventLog.Event_Description)].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[nameof(EventLogEntity.TBL_EventLog.Command)].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.ClearSelection();
        }

        public void Frm_5_2_ScheduleDelete_CoilReject_Code(DataGridView Dgv)
        {
            Dgv.Columns.Add(Fun_SetGridViewColumn("代码编号", nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo), 150,4));
            Dgv.Columns.Add(Fun_SetGridViewColumn("代码", nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code), 150,2));
            Dgv.Columns.Add(Fun_SetGridViewColumn("原因", nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name), 1000,50));
            Dgv.Columns.Add(Fun_SetGridViewColumn("建立者", nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.Create_UserID), 200));
            Dgv.Columns.Add(Fun_SetGridViewColumn("更新时间", "Time", 300));

            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void Frm_5_2_DelayLocation(DataGridView Dgv)
        {
            Dgv.Columns.Add(Fun_SetGridViewColumn("顺序号", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No), 150));
            Dgv.Columns.Add(Fun_SetGridViewColumn("位置代码", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode), 150,6));
            Dgv.Columns.Add(Fun_SetGridViewColumn("位置", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName), 1000,50));
            Dgv.Columns.Add(Fun_SetGridViewColumn("建立者", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Create_UserID), 200));
            Dgv.Columns.Add(Fun_SetGridViewColumn("更新时间", "Time", 300));

            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void Frm_5_2_DelayReasonCode(DataGridView Dgv)
        {
            Dgv.Columns.Add(Fun_SetGridViewColumn("顺序号", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No), 150));
            Dgv.Columns.Add(Fun_SetGridViewColumn("群组代码", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode), 150,1));
            Dgv.Columns.Add(Fun_SetGridViewColumn("群组名称", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName), 600,50));
            Dgv.Columns.Add(Fun_SetGridViewColumn("原因代码", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode), 150,2));
            Dgv.Columns.Add(Fun_SetGridViewColumn("原因名称", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName), 600,50));
            Dgv.Columns.Add(Fun_SetGridViewColumn("负责部门", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department), 600,50));
            Dgv.Columns.Add(Fun_SetGridViewColumn("建立者", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Create_UserID), 200));
            Dgv.Columns.Add(Fun_SetGridViewColumn("更新时间", "Time", 300));

           // Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void Frm_5_2_MaterialGrade(DataGridView Dgv)
        {
            Dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No), 350,8));
            Dgv.Columns.Add(Fun_SetGridViewColumn("屈服强度(MPa) ", nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys), 350));
            Dgv.Columns.Add(Fun_SetGridViewColumn("抗拉强度(MPa)", nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts), 350));
            Dgv.Columns.Add(Fun_SetGridViewColumn("钢种大类", nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade), 350,20));
            Dgv.Columns.Add(Fun_SetGridViewColumn("更新时间", "Time", 350));

            Dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        
        public void Frm_5_2_GradeGroups(DataGridView Dgv)
        {
            Dgv.Columns.Add(Fun_SetGridViewColumn("钢种", nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade), 350,8));
            Dgv.Columns.Add(Fun_SetGridViewColumn("客户代码", nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo), 350,20));
            Dgv.Columns.Add(Fun_SetGridViewColumn("钢种大类", nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup), 350,20));

            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void Frm_5_5_LangSwitch_Nav(DataGridView Dgv)
        {
            Dgv.Columns.Add(Fun_SetGridViewColumn("画面", nameof(LangSwitchEntity.TBL_LangSwitch_Nav.PKey), 300, 20));
            Dgv.Columns.Add(Fun_SetGridViewColumn("中文", nameof(LangSwitchEntity.TBL_LangSwitch_Nav.ZH), 600, 50));
            Dgv.Columns.Add(Fun_SetGridViewColumn("英文", nameof(LangSwitchEntity.TBL_LangSwitch_Nav.EN), 600, 50));
            
            //Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// 清空ColumnsHeader，否則Header會疊加
        /// </summary>
        private void ColumnsClear(DataGridView dgv)
        {
            RowHeadersVisible(dgv);

            if (dgv.Columns.Count > 0)
            {
                dgv.Columns.Clear();
            }
        }
        private void RowHeadersVisible(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
        }
        public void ColumnHeadVisableControl(DataGridView dgv)
        {

            dgv.ColumnHeadersVisible = true;
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體",12,System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體",12,System.Drawing.FontStyle.Bold);
        }
        /// <summary>
        /// 設定DataGridView 標題中文(預設寬度依顯示字元數目計算)
        /// </summary>
        /// <param name="strShow"></param>
        /// <param name="strColumnName"></param>
        /// <returns></returns>
        private DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName)
        {
            // 顯示一個欄位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow
            };
            int intWidth = Encoding.Default.GetBytes(strShow).Length;
            idColumn.Width = 15 * intWidth;
            return idColumn;
        }
        /// <summary>
        /// 設定DataGridView 標題中文(指定寬度)
        /// </summary>
        /// <param name="strShow">要顯示的中文</param>
        /// <param name="strColumnName">欄位原始名稱</param>
        /// <param name="intWidth">指定欄寬</param>
        /// <returns></returns>
        private DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName, int intWidth)
        {
            // 顯示一個欄位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow,
                Width = intWidth
            };
            return idColumn;
        }

        /// <summary>
        /// 設定DataGridView 標題中文(指定寬度)
        /// </summary>
        /// <param name="strShow">要顯示的中文</param>
        /// <param name="strColumnName">欄位原始名稱</param>
        /// <param name="intWidth">指定欄寬</param>
        /// <returns></returns>
        private DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName, int intWidth,bool bolInt  )
        {
            
            // 顯示一個欄位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow,
                Width = intWidth,
                ValueType = typeof(int)
                
            };
            return idColumn;
        }
        /// <summary>
        /// 設定DataGridView 標題中文(指定寬度)
        /// </summary>
        /// <param name="strShow">要顯示的中文</param>
        /// <param name="strColumnName">欄位原始名稱</param>
        /// <param name="intWidth">指定欄寬</param>
        /// <param name="intMaxInputLength">输入字元最大值</param>
        /// <returns></returns>
        public DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName, int intWidth, int intMaxInputLength, bool bolNum = false)
        {
            // 顯示一個欄位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow,
                Width = intWidth,
                MaxInputLength = intMaxInputLength
            };

            if (bolNum)
                idColumn.ValueType = typeof(decimal);

            return idColumn;
        }
    }
}
