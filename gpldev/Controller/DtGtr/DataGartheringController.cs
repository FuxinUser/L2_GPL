using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Coil;
using BLL.Logic;
using Core.Define;
using Core.Util;
using DataMod.Common;
using DBService.L1Repository;
using DBService.Level25Repository.L2L25_Alive;
using LogSender;
using MsgConvert.DBTable;
using MsgStruct;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.WorkSchedule.WorkScheduleEntity;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.GrindRecords.GrindRecordsEntity;

namespace Controller.DtGtr
{
    public class DataGartheringController : IDataGatheringController
    {
        private DataGartingLogic _gartingLogic;
        private CoilProLogic _coilProLogic;
        private ILog _log;


        public DataGartheringController()
        {
        
            _gartingLogic = new DataGartingLogic();
            _coilProLogic = new CoilProLogic();
        }

      
        public void SetLog(ILog log)
        {
            _log = log;
        }

        public void CreateCoilWeld(L1L2Rcv.Msg_106_Weld_Data msg)
        {
            try
            {
                var pdi = _coilProLogic.GetPDI(msg.CoilID.ToStr());
                var entity = msg.ToWeldRecords(pdi.Plan_No);
                var insertNum = _gartingLogic.CreateCoilWeldRecord(entity);
                _log.I($"存取WeldRecord資料至CoilWeld資料", $"新增資料{msg.CoilID.ToStr()} WeldRecord資料至CoilWeld資料 => {insertNum > 0}");
            }
            catch (Exception e)
            {
                _log.E($"存取WeldRecord資料至CoilWeld資料", e.Message.CleanInvalidChar());
            }
        }

        public void CreateGrdRpt(L1L2Rcv.Msg_107_Grd_Rpt msg)
        {
            try
            {
                var pdi = _coilProLogic.GetPDI(msg.CoilID.ToStr());
                var entity = msg.ToTblGrindRecords(pdi.Plan_No);
                var insertNum = _gartingLogic.CreateGrdRpt(entity);
                _log.I($"存取GRDRPT資料", $"新增資料{msg.CoilID.ToStr()}GRDRPT資料 => {insertNum > 0}");
            }
            catch (Exception e)
            {
                _log.E($"存取GRDRPT資料", e.Message.CleanInvalidChar());
            }
        }

        public void CreateProData(L1L2Rcv.Msg_104_ProData msg)
        {
            try
            {
                var insertNum = _gartingLogic.CreateProcessData(msg);
                _log.I($"存取Process資料", $"新增資料Process資料 => {insertNum > 0}");
            }
            catch (Exception e)
            {
                _log.E($"存取Process資料", e.Message.CleanInvalidChar());
            }
        }

        public void CreateUtility(L1L2Rcv.Msg_112_Utility msg, string shift, string team)
        {
            try
            {

                var tbUtility = msg.ToTblUtilityEntity(shift, team);

                var insertNum = _gartingLogic.CreateUtility(tbUtility);
                _log.I($"存取Utility資料", $"新增資料Utility資料 => {insertNum > 0}");
            }
            catch (Exception e)
            {
                _log.E($"存取Utility資料", e.Message.CleanInvalidChar());
            }
        }

        public void CreateStripBrakeSignal(L1L2Rcv.Msg_124_StripBrakeSignal msg)
        {
            try
            {
                var insertNum = _gartingLogic.CreateStripBrakeSignal(msg);
                _log.I($"存取斷帶訊號資料", $"新增斷帶訊號資料 => {insertNum > 0}");
            }
            catch (Exception e)
            {
                _log.E($"存取斷帶訊號資料", e.Message.CleanInvalidChar());
            }

        }


        public void CreateUmountRecord(L1L2Rcv.Msg_124_StripBrakeSignal msg)
        {
            try
            {
                var record = msg.ToTblUmountRecord();
                var insertNum = _gartingLogic.CreateUmountPORRecord(record);
                if (insertNum > 1)
                    _log.I("存取Umount Record資料", $"新增資料{insertNum}筆成功");
            }
            catch (Exception e)
            {

                _log.E("存取Coil Cut資料", e.Message.CleanInvalidChar());
            }
        }

       

        public bool CreateStopLineFaultStart(L1L2Rcv.Msg_111_LineFault msg, string team, int shift)
        {
                
            try
            {
                var fault = msg.ToTblTBLLineFaultRecords(team, shift);
                var insertNum = _gartingLogic.CreateLineFaultRecord(fault);
                _log.I("存取Line Fault資料", $"新增資料{insertNum}筆{(insertNum>0).ToStr()}");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取Line Fault資料錯誤", e.Message.CleanInvalidChar());
                return false;
            }
        }


        public TBL_WorkSchedule GetScheduleByTime(DateTime time)
        {
            try
            {         
                var workSchedule = _gartingLogic.GetScheduleByTime(time);              
                return workSchedule;
            }
            catch (Exception e)
            {
                _log.E($"撈取目前班次股別", e.Message.CleanInvalidChar());
                return null;
            }
        }


        public bool UpdateStopLineFaultEnd(L1L2Rcv.Msg_111_LineFault msg)
        {

            var lineFaultRecord = new TBL_LineFaultRecords();

            // 查停機記錄
            try
            {             
                lineFaultRecord = _gartingLogic.GetLastLineFaultUnfinshRecord();
            }
            catch(Exception e)
            {
                _log.E($"撈取Line Fault資料失敗", e.Message.CleanInvalidChar());
                return false;
            }

            if (lineFaultRecord == null)
            {
                _log.A($"無此停復機紀錄", "未有未結算完成停復機紀錄");
                return false;
            }

            //var preStopEndTime = lineFaultRecord.stop_end_time;
            lineFaultRecord.stop_end_time = DateTime.Now;//msg.DateTime;

            try
            {
                //計算停機持續時間
                var Ts = new TimeSpan(lineFaultRecord.stop_end_time.Ticks - lineFaultRecord.stop_start_time.Ticks);
                lineFaultRecord.stop_elased_timey = ((Ts.Hours * 60) + Ts.Minutes).ToString();
            }
            catch (Exception e)
            {
                throw;            
            }
               

            // 更新
            try
            {            
                var updateNum = _gartingLogic.UpdateFaultRecord(lineFaultRecord);     
                _log.I("更新Line Fault資料", $"更新資料{updateNum}筆成功");
                //新增25Downtime紀錄
                //CreateL25DownTime(lineFaultRecord);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取Line Fault資料", e.Message.CleanInvalidChar());
                return false;
            }

        }

        public bool UpdateStopLineFaultEnd(CheckCrossShiftModel shiftInfo)
        {

            var lineFaultRecord = new TBL_LineFaultRecords();

            // 查停機記錄
            try
            {             
                lineFaultRecord = _gartingLogic.GetLastLineFaultUnfinshRecord();
            }
            catch (Exception e)
            {
                _log.E($"撈取Line Fault資料失敗", e.Message.CleanInvalidChar());
                return false;
            }

          
            lineFaultRecord.stop_end_time = DateTime.Now;

            // 更新
            try
            {
                var updateNum = _gartingLogic.UpdateFaultRecord(lineFaultRecord);
                _log.I("更新Line Fault資料", $"更新資料{updateNum}筆成功");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新Line Fault資料失敗", e.Message.CleanInvalidChar());
                return false;
            }

        }
        public bool AutoUpdateShiftRecord(int nowShift,string Team)
        {
            //取得停機紀錄
            var lineFaultRecord = _gartingLogic.GetLastLineFaultUnfinshRecord();
            if (lineFaultRecord.prod_shift_no != nowShift.ToString())
            {
                //1 < 2 , 2 < 3 ,  3 > 1 
                if (lineFaultRecord.prod_shift_no.Equals("1") || lineFaultRecord.prod_shift_no.Equals("2") || lineFaultRecord.prod_shift_no.Equals("3"))
                {
                    if (Int32.Parse(lineFaultRecord.prod_shift_no) < nowShift) //小於
                    {
                        lineFaultRecord.stop_end_time = DateTime.Now;
                        //計算停機累計時間
                        var Ts = new TimeSpan(lineFaultRecord.stop_end_time.Ticks - lineFaultRecord.stop_start_time.Ticks);
                        lineFaultRecord.stop_elased_timey = ((Ts.Hours * 60) + Ts.Minutes).ToString();

                        var updateNum = _gartingLogic.UpdateFaultRecord(lineFaultRecord);
                        _log.I("更新Line Fault資料", $"更新資料{updateNum}筆成功");
                        var msg = new L1L2Rcv.Msg_111_LineFault();
                        msg.LineStatus = PlcSysDef.Cmd.LineStatusStop;
                        msg.Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                        msg.Time = Int32.Parse(DateTime.Now.ToString("HHmmss"));
                        msg.StopCategory = (short)lineFaultRecord.stop_category;
                        var fault = msg.ToTblTBLLineFaultRecords(lineFaultRecord.prod_shift_group, nowShift);
                        //var workSchedule = GetScheduleByTime(DateTime.Now.AddMinutes(1));
                        fault.prod_shift_no = nowShift.ToString();  /*班次*/
                        fault.prod_shift_group = Team;              /*班别*/
                        fault.prod_time = DateTime.Now;
                        fault.stop_start_time = DateTime.Now.AddSeconds(1);  
                        var insertNum = _gartingLogic.CreateLineFaultRecord(fault);
                        _log.I("新增Line Fault資料", $"新增資料{insertNum}筆{(insertNum > 0).ToStr()}");
                    }
                    else //大於
                    {
                        lineFaultRecord.stop_end_time = DateTime.Now;
                        //計算停機累計時間
                        var Ts = new TimeSpan(lineFaultRecord.stop_end_time.Ticks - lineFaultRecord.stop_start_time.Ticks);
                        lineFaultRecord.stop_elased_timey = ((Ts.Hours * 60) + Ts.Minutes).ToString();

                        var updateNum = _gartingLogic.UpdateFaultRecord(lineFaultRecord);
                        _log.I("更新Line Fault資料", $"更新資料{updateNum}筆成功");
                        var msg = new L1L2Rcv.Msg_111_LineFault();
                        msg.LineStatus = PlcSysDef.Cmd.LineStatusStop;
                        msg.Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                        msg.Time = Int32.Parse(DateTime.Now.ToString("HHmmss"));
                        msg.StopCategory = (short)lineFaultRecord.stop_category;
                        var fault = msg.ToTblTBLLineFaultRecords(lineFaultRecord.prod_shift_group, nowShift);
                        //var workSchedule = GetScheduleByTime(DateTime.Now.AddMinutes(1));
                        fault.prod_shift_no = nowShift.ToString();  /*班次*/
                        fault.prod_shift_group = Team;              /*班别*/
                        fault.stop_start_time = DateTime.Now.AddSeconds(1);
                        var insertNum = _gartingLogic.CreateLineFaultRecord(fault);
                        _log.I("新增Line Fault資料", $"新增資料{insertNum}筆{(insertNum > 0).ToStr()}");
                    }




                }



            }



            return false;
        }
        public bool SaveStopLineFaultStart(CheckCrossShiftModel shiftInfo, int nowShift, string nowDate)
        {
            var lineFaultRecord = new TBL_LineFaultRecords();
       
            // 查停機記錄
            try
            {
                lineFaultRecord = _gartingLogic.GetLineFaultRecord(shiftInfo.StopStartTime.Date.ToString("00000000"));
            }
            catch (Exception e)
            {
                _log.E($"撈取Line Fault資料失敗", e.Message.CleanInvalidChar());
                return false;
            }

            // 生產組別與班次
            try
            {
                var workSchedule = _gartingLogic.GetWorkSchedule(nowShift, nowDate);
                lineFaultRecord.prod_shift_group = workSchedule.Team;            // 當日生產組別 A-甲，B-乙，C-丙，D-丁
            }
            catch (Exception e)
            {
                _log.E($"撈取WorkSchedule資料錯誤", e.Message.CleanInvalidChar());
                return false;
            }



            lineFaultRecord.stop_start_time = DateTime.Now;
            lineFaultRecord.prod_shift_no = nowShift.ToString();          // 目前生產班次 1-夜(24:00-8:00)，2-早(8:00-16:00)，3-中(16:00-24:00)
          

            // 存取
            try
            {                
                var insertNum = _gartingLogic.CreateLineFaultRecord(lineFaultRecord);
                _log.I("存取Line Fault資料", $"新增資料{insertNum}筆成功");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取Line Fault資料", e.Message.CleanInvalidChar());
                return false;
            }

        }
     
        public CheckCrossShiftModel GetLastLineFaultUnfinshRecord()
        {
            // 查停機記錄
            try
            {
                var lineFaultRecord = _gartingLogic.GetLastLineFaultUnfinshRecord();

                if (lineFaultRecord == null)
                    return null;

                var shiftInfo = new CheckCrossShiftModel()
                {
                    FaultCode = lineFaultRecord.delay_reason_code.ToNullable<int>()??0,
                    Shift = lineFaultRecord.prod_shift_no.ToNullable<int>() ?? 0,
                    StopStartTime = lineFaultRecord.stop_start_time
                };

                return shiftInfo;
            }
            catch (Exception e)
            {
                _log.E($"撈取Line Fault資料失敗", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public void CreateCutRecord(L1L2Rcv.Msg_125_Share_Cut_Data msg)
        {
            try
            {
                var insertNum = _gartingLogic.CreateCoilCutRecord(msg);
                if (insertNum > 1)
                    _log.I("存取Coil Cut資料", $"新增資料{insertNum}筆成功");
            }
            catch (Exception e)
            {
               
                _log.E("存取Coil Cut資料", e.Message.CleanInvalidChar());
            }
        }

        public void CreateUmountRecord(L1L2Rcv.Msg_126_Coil_Unmount_POR msg)
        {
            try
            {
                var record = msg.ToTblUmountRecord();
                var insertNum = _gartingLogic.CreateUmountPORRecord(record);
                if (insertNum > 1)
                    _log.I("存取Umount Record資料", $"新增資料{insertNum}筆成功");
            }
            catch (Exception e)
            {

                _log.E("存取Coil Cut資料", e.Message.CleanInvalidChar());
            }
        }


        // Get His Msg
        public IEnumerable<L2L1MsgDBModel.L2L1_204> QueryAll204HisMsg()
        {
            try
            {               
                var his204Msg = _gartingLogic.QueryAll204HisMsg();
                _log.I("撈取204歷史報紋資料", $"撈取204歷史報紋資料 => {his204Msg.Count()}筆");
                return his204Msg;
                
            }
            catch (Exception e)
            {
                _log.E("撈取204歷史報紋資料", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public IEnumerable<L2L1MsgDBModel.L2L1_205> QueryAll205HisMsg()
        {
            try
            {
                var his205Msg = _gartingLogic.QueryAll205HisMsg();
                _log.I("撈取205歷史報紋資料", $"撈取205歷史報紋資料 => {his205Msg.Count()}筆");
                return his205Msg;

            }
            catch (Exception e)
            {
                _log.E("撈取205歷史報紋資料", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public L2L1MsgDBModel.L2L1_204 Get204HisDataByTime(string createTime)
        {
            try
            {
                var date = _gartingLogic.Get204HisDataByTime(createTime);
                return date;
            }
            catch (Exception e)
            {
                _log.E("撈取Preset204資料", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public L2L1MsgDBModel.L2L1_205 Get205HisDataByTime(string createTime)
        {
            try
            {
                var date = _gartingLogic.Get205HisDataByTime(createTime);
                return date;
            }
            catch (Exception e)
            {
                _log.E("撈取Preset205資料", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public TBL_LineFaultRecords GetLineFaultRecord(string prodTime, string stopStartTime,string stopEndTime)
        {
            try
            {
                var entity = _gartingLogic.GetLineFaultRecord(prodTime, stopStartTime, stopEndTime);
                _log.I("撈取停復機紀錄", $"撈取{stopStartTime}停復機紀錄");
                return entity;
            }
            catch (Exception e)
            {
                _log.E($"撈取目前班次股別", e.Message.CleanInvalidChar());
                return null;
            }
        }
        public TBL_LineFaultRecords GetLineFaultDelRecord(string prodTime,string ShifNo,string ShiftGroup, string stopStartTime, string stopEndTime,string delayLocation)
        {
            try
            {
                var entity = _gartingLogic.GetLineFaultDelRecord(prodTime, ShifNo, ShiftGroup, stopStartTime, stopEndTime, delayLocation);
                _log.I("撈取停復機紀錄", $"撈取{stopStartTime}停復機紀錄");
                return entity;
            }
            catch (Exception e)
            {
                _log.E($"撈取目前班次股別", e.Message.CleanInvalidChar());
                return null;
            }
        }
        public bool UpdateLineFaultUploadFlag(DateTime prodTime, DateTime stopStartTime, DateTime stopEndTime, bool uploadDone)
        {
            try
            {
                var uploadFinish = uploadDone ? DBParaDef.YES : DBParaDef.NO;
                var prodTimeStr = prodTime.ToString("yyyy-MM-dd");
                var stopStartTimeStr = stopStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var stopEndTimeStr  = stopEndTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var inNum =  _gartingLogic.UpdateLineFaultUploadFlag(prodTimeStr, stopStartTimeStr, stopEndTimeStr, uploadFinish);
                var insertOK = inNum > 0; 
                _log.I("更新停復機紀錄上傳旗標", $"更新停復機紀錄上傳旗標=>{insertOK}");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E($"更新停復機紀錄上傳旗標失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }


        public bool CreateL25Alive()
        {
            try
            {
                var entity = new L2L25_Alive();
                entity.Message_Id = L25SysDef.MsgCode.Msg150Alive;
                entity.Message_Length = L25SysDef.MsgLength.Msg150Alive;
                entity.Date = DateTime.Now.ToString("yyyyMMdd");
                entity.Time = DateTime.Now.ToString("HHmmss");
                var insertNum = _gartingLogic.CreateL25Alive(entity);
                var insertOK = insertNum > 0;
                _log.D("存取L25 Alive訊息", "存取L25 Alive訊息");
                return insertOK;
                //return true;
                //insertNum = _gartingLogic.CreateL25AliveHis(entity);
                //insertOK = insertNum > 0;
                //_log.D("存取L25 Alive歷史訊息", "存取L25 Alive訊息");


            }
            catch (Exception e)
            {
                _log.E($"存取L25 Alive失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool CreateL25Engc(L1L2Rcv.Msg_112_Utility msg)
        {
            msg.VaildObjectNull("msg", "存取 2.5 Utility資料失敗");

            try
            {
                var entity = msg.ToL25EngcEntity();
                var insertNum = _gartingLogic.CreateL25Engc(entity);
                var insertOK = insertNum > 0;
                _log.I("存取2.5 Utility資料", $"新增資料=>{insertOK}");
                return insertOK;
                //return true;
                //insertNum = _gartingLogic.CreateL25EngcHis(entity);
                //insertOK = insertNum > 0;
                //_log.I("存取2.5 Utility歷史資料", $"新增資料=>{insertOK}");


            }
            catch (Exception e)
            {
                _log.E("存取 2.5 Utility資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreateL25DownTime(TBL_LineFaultRecords dao)
        {
            try
            {
                var entity = dao.ToL25DownTimeEntity();
                var inserNum = _gartingLogic.CreateL25DownTime(entity);
                var insertOK = inserNum > 0;
                _log.I("新L25停復機紀錄資料", $"新增=>{insertOK}");
                return insertOK;
                //return true;
                //inserNum = _gartingLogic.CreateL25DownTimeHis(entity);
                //insertOK = inserNum > 0;
                //_log.I("新L25停復機歷史紀錄資料", $"新增=>{insertOK}");



            }
            catch (Exception e)
            {

                _log.E("新增L25停復機資料錯誤", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public IEnumerable<TBL_GrindRecords> QueryGrindDatas(string CoilID, string PlanNo,int PassNumber,int Session)
        {
            try
            {       
                var entitys = _gartingLogic.QueryGrindData(CoilID, PlanNo,PassNumber,Session);
                _log.I("撈取研磨數據GrindData", $"{CoilID},{PlanNo} 共 {entitys.Count()}筆資料");


                return entitys.Count() > 0 ? entitys : null;

            }
            catch (Exception e)
            {
                _log.E("撈取研磨數據GrindData", e.ToString().CleanInvalidChar());
                return null;
            }
        }
        public IEnumerable<TBL_GrindRecords> QueryGrindDatas_Total(string CoilID, string PlanNo)
        {
            try
            {
                var entitys = _gartingLogic.QueryGrindData_Total(CoilID, PlanNo);
                _log.I("撈取研磨數據GrindData", $"{CoilID},{PlanNo} 共 {entitys.Count()}筆資料");


                return entitys.Count() > 0 ? entitys : null;

            }
            catch (Exception e)
            {
                _log.E("撈取研磨數據GrindData", e.ToString().CleanInvalidChar());
                return null;
            }
        }
        public IEnumerable<TBL_ProcessData> QueryProcessDatas(DateTime starTime, DateTime endTime)
        {
            try
            {
                var starTimeStr = starTime.ToString(DBParaDef.TimeFromat);
                var endTimeStr = endTime.ToString(DBParaDef.TimeFromat);

                var entitys = _gartingLogic.QueryProcessData(starTimeStr, endTimeStr);
                _log.I("撈取生產參數ProcessData", $"{starTimeStr} - {endTimeStr} 共 {entitys.Count()}筆資料");


                return entitys.Count() > 0 ? entitys : null;

            }
            catch (Exception e)
            {
                _log.E("撈取生產參數ProcessData錯誤", e.ToString().CleanInvalidChar());
                return null;
            }
        }
        public GrindDataModel CalculateGrindData(IEnumerable<TBL_GrindRecords> datas)
        {
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");

            var dataModel = new GrindDataModel();
            var coilLength = 0.0;
            //Grind
            var Current_Pass = string.Empty;
            var Current_Senssion = string.Empty;
            var Line_Speed = string.Empty;
            var GR1_BELT_KIND = string.Empty;
            var GR1_BELT_PARTICLE_NO = string.Empty;
            var GR1_BELT_ROTATE_DIR = string.Empty;
            var GR1_MOTOR_CURRENT = string.Empty;
            var GR1_BELT_SPEED = string.Empty;
            var GR2_BELT_KIND = string.Empty;
            var GR2_BELT_PARTICLE_NO = string.Empty;
            var GR2_BELT_ROTATE_DIR = string.Empty;
            var GR2_MOTOR_CURRENT = string.Empty;
            var GR2_BELT_SPEED = string.Empty;
            var GR3_BELT_KIND = string.Empty;
            var GR3_BELT_PARTICLE_NO = string.Empty;
            var GR3_BELT_ROTATE_DIR = string.Empty;
            var GR3_MOTOR_CURRENT = string.Empty;
            var GR4_BELT_KIND = string.Empty;
            var GR4_BELT_PARTICLE_NO = string.Empty;
            var GR4_BELT_ROTATE_DIR = string.Empty;
            var GR4_MOTOR_CURRENT = string.Empty;
            var GR5_BELT_KIND = string.Empty;
            var GR5_BELT_PARTICLE_NO = string.Empty;
            var GR5_BELT_ROTATE_DIR = string.Empty;
            var GR5_MOTOR_CURRENT = string.Empty;
            var GR6_BELT_KIND = string.Empty;
            var GR6_BELT_PARTICLE_NO = string.Empty;
            var GR6_BELT_ROTATE_DIR = string.Empty;
            var GR6_MOTOR_CURRENT = string.Empty;
            var dataCnt = 0;

            foreach (TBL_GrindRecords data in datas)
            {
                if (data.Line_Speed == 0)
                    continue;
                var preCoilLength = coilLength;
                coilLength += (double)(data.Line_Speed) / 60;

                var prePos = Convert.ToInt32(preCoilLength);
                var pos = Convert.ToInt32(coilLength);
                //var prePos = preCoilLength;
                //var pos = coilLength;

                if (prePos == pos)
                    continue;

                dataCnt++;
                Current_Pass = Current_Pass + pos + ":" + Convert.ToSingle(data.Current_Pass).GetPoint(4).ToString() + ",";
                Current_Senssion = Current_Senssion + pos + ":" + Convert.ToSingle(data.Current_Senssion).GetPoint(4).ToString() + ",";
                Line_Speed = Line_Speed + pos + ":" + Convert.ToSingle(data.Line_Speed).GetPoint(4).ToString() + ",";
                GR1_BELT_KIND = GR1_BELT_KIND + pos + ":" + data.GR1_BELT_KIND + ",";
                GR1_BELT_PARTICLE_NO = GR1_BELT_PARTICLE_NO + pos + ":" + Convert.ToSingle(data.GR1_BELT_PARTICLE_NO).GetPoint(4).ToString() + ",";
                GR1_BELT_ROTATE_DIR = GR1_BELT_ROTATE_DIR + pos + ":" + Convert.ToSingle(data.GR1_BELT_ROTATE_DIR).GetPoint(4).ToString() + ",";
                GR1_MOTOR_CURRENT = GR1_MOTOR_CURRENT + pos + ":" + Convert.ToSingle(data.GR1_MOTOR_CURRENT).GetPoint(4).ToString() + ",";
                GR1_BELT_SPEED = GR1_BELT_SPEED + pos + ":" + Convert.ToSingle(data.GR1_BELT_SPEED).GetPoint(4).ToString() + ",";
                GR2_BELT_KIND = GR2_BELT_KIND + pos + ":" + data.GR2_BELT_KIND + ",";
                GR2_BELT_PARTICLE_NO = GR2_BELT_PARTICLE_NO + pos + ":" + Convert.ToSingle(data.GR2_BELT_PARTICLE_NO).GetPoint(4).ToString() + ",";
                GR2_BELT_ROTATE_DIR = GR2_BELT_ROTATE_DIR + pos + ":" + Convert.ToSingle(data.GR2_BELT_ROTATE_DIR).GetPoint(4).ToString() + ",";
                GR2_MOTOR_CURRENT = GR2_MOTOR_CURRENT + pos + ":" + Convert.ToSingle(data.GR2_MOTOR_CURRENT).GetPoint(4).ToString() + ",";
                GR2_BELT_SPEED = GR2_BELT_SPEED + pos + ":" + Convert.ToSingle(data.GR2_BELT_SPEED).GetPoint(4).ToString() + ",";
                GR3_BELT_KIND = GR3_BELT_KIND + pos + ":" + data.GR3_BELT_KIND + ",";
                GR3_BELT_PARTICLE_NO = GR3_BELT_PARTICLE_NO + pos + ":" + Convert.ToSingle(data.GR3_BELT_PARTICLE_NO).GetPoint(4).ToString() + ",";
                GR3_BELT_ROTATE_DIR = GR3_BELT_ROTATE_DIR + pos + ":" + Convert.ToSingle(data.GR3_BELT_ROTATE_DIR).GetPoint(4).ToString() + ",";
                GR3_MOTOR_CURRENT = GR3_MOTOR_CURRENT + pos + ":" + Convert.ToSingle(data.GR3_MOTOR_CURRENT).GetPoint(4).ToString() + ",";
                GR4_BELT_KIND = GR4_BELT_KIND + pos + ":" + data.GR4_BELT_KIND + ",";
                GR4_BELT_PARTICLE_NO = GR4_BELT_PARTICLE_NO + pos + ":" + Convert.ToSingle(data.GR4_BELT_PARTICLE_NO).GetPoint(4).ToString() + ",";
                GR4_BELT_ROTATE_DIR = GR4_BELT_ROTATE_DIR + pos + ":" + Convert.ToSingle(data.GR4_BELT_ROTATE_DIR).GetPoint(4).ToString() + ",";
                GR4_MOTOR_CURRENT = GR4_MOTOR_CURRENT + pos + ":" + Convert.ToSingle(data.GR4_MOTOR_CURRENT).GetPoint(4).ToString() + ",";
                GR5_BELT_KIND = GR5_BELT_KIND + pos + ":" + data.GR5_BELT_KIND + ",";
                GR5_BELT_PARTICLE_NO = GR5_BELT_PARTICLE_NO + pos + ":" + Convert.ToSingle(data.GR5_BELT_PARTICLE_NO).GetPoint(4).ToString() + ",";
                GR5_BELT_ROTATE_DIR = GR5_BELT_ROTATE_DIR + pos + ":" + Convert.ToSingle(data.GR5_BELT_ROTATE_DIR).GetPoint(4).ToString() + ",";
                GR5_MOTOR_CURRENT = GR5_MOTOR_CURRENT + pos + ":" + Convert.ToSingle(data.GR5_MOTOR_CURRENT).GetPoint(4).ToString() + ",";
                GR6_BELT_KIND = GR6_BELT_KIND + pos + ":" + data.GR6_BELT_KIND + ",";
                GR6_BELT_PARTICLE_NO = GR6_BELT_PARTICLE_NO + pos + ":" + Convert.ToSingle(data.GR6_BELT_PARTICLE_NO).GetPoint(4).ToString() + ",";
                GR6_BELT_ROTATE_DIR = GR6_BELT_ROTATE_DIR + pos + ":" + Convert.ToSingle(data.GR6_BELT_ROTATE_DIR).GetPoint(4).ToString() + ",";
                GR6_MOTOR_CURRENT = GR6_MOTOR_CURRENT + pos + ":" + Convert.ToSingle(data.GR6_MOTOR_CURRENT).GetPoint(4).ToString() + ",";
            }

            dataModel.TotalLength = coilLength.GetPoint(4).ToString();
            dataModel.DataCnt = dataCnt;
            dataModel.Current_Pass = Current_Pass.TrimEnd(',');
            dataModel.Current_Senssion = Current_Senssion.TrimEnd(',');
            dataModel.Line_Speed = Line_Speed.TrimEnd(',');
            dataModel.GR1_BELT_KIND = GR1_BELT_KIND.TrimEnd(',');
            dataModel.GR1_BELT_PARTICLE_NO = GR1_BELT_PARTICLE_NO.TrimEnd(',');
            dataModel.GR1_BELT_ROTATE_DIR = GR1_BELT_ROTATE_DIR.TrimEnd(',');
            dataModel.GR1_MOTOR_CURRENT = GR1_MOTOR_CURRENT.TrimEnd(',');
            dataModel.GR1_BELT_SPEED = GR1_BELT_SPEED.TrimEnd(',');
            dataModel.GR2_BELT_KIND = GR2_BELT_KIND.TrimEnd(',');
            dataModel.GR2_BELT_PARTICLE_NO = GR2_BELT_PARTICLE_NO.TrimEnd(',');
            dataModel.GR2_BELT_ROTATE_DIR = GR2_BELT_ROTATE_DIR.TrimEnd(',');
            dataModel.GR2_MOTOR_CURRENT = GR2_MOTOR_CURRENT.TrimEnd(',');
            dataModel.GR2_BELT_SPEED = GR2_BELT_SPEED.TrimEnd(',');
            dataModel.GR3_BELT_KIND = GR3_BELT_KIND.TrimEnd(',');
            dataModel.GR3_BELT_PARTICLE_NO = GR3_BELT_PARTICLE_NO.TrimEnd(',');
            dataModel.GR3_BELT_ROTATE_DIR = GR3_BELT_ROTATE_DIR.TrimEnd(',');
            dataModel.GR3_MOTOR_CURRENT = GR3_MOTOR_CURRENT.TrimEnd(',');
            dataModel.GR4_BELT_KIND = GR4_BELT_KIND.TrimEnd(',');
            dataModel.GR4_BELT_PARTICLE_NO = GR4_BELT_PARTICLE_NO.TrimEnd(',');
            dataModel.GR4_BELT_ROTATE_DIR = GR4_BELT_ROTATE_DIR.TrimEnd(',');
            dataModel.GR4_MOTOR_CURRENT = GR4_MOTOR_CURRENT.TrimEnd(',');
            dataModel.GR5_BELT_KIND = GR5_BELT_KIND.TrimEnd(',');
            dataModel.GR5_BELT_PARTICLE_NO = GR5_BELT_PARTICLE_NO.TrimEnd(',');
            dataModel.GR5_BELT_ROTATE_DIR = GR5_BELT_ROTATE_DIR.TrimEnd(',');
            dataModel.GR5_MOTOR_CURRENT = GR5_MOTOR_CURRENT.TrimEnd(',');
            dataModel.GR6_BELT_KIND = GR6_BELT_KIND.TrimEnd(',');
            dataModel.GR6_BELT_PARTICLE_NO = GR6_BELT_PARTICLE_NO.TrimEnd(',');
            dataModel.GR6_BELT_ROTATE_DIR = GR6_BELT_ROTATE_DIR.TrimEnd(',');
            dataModel.GR6_MOTOR_CURRENT = GR6_MOTOR_CURRENT.TrimEnd(',');
            return dataModel;
        }
        public ProcessModel CalculateProcessData(IEnumerable<TBL_ProcessData> datas)
        {
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");

            var dataModel = new ProcessModel();
            var coilLength = 0.0;


            var LinespeedStr = string.Empty;
            var LinerundirectionStr = string.Empty;
            //var TensionReelSpeedStr = string.Empty;
            var LinetensionStr = string.Empty;
            //var ThreadingSpeedStr = string.Empty;
            var No1GRBeltMotorCurrentStr = string.Empty;
            var No1GRBeltSpeedStr = string.Empty;
            var No2GRBeltMotorCurrentStr = string.Empty;
            var No2GRBeltSpeedStr = string.Empty;
            var No3GRBeltMotorCurrentStr = string.Empty;
            var No4GRBeltMotorCurrentStr = string.Empty;
            var No5GRBeltMotorCurrentStr = string.Empty;
            var No6GRBeltMotorCurrentStr = string.Empty;
            //var CoolantOilTankTempStr = string.Empty;
            //var AlkaliSolutionTankTempStr = string.Empty;
            //var PrimaryRinseWaterTankTempStr = string.Empty;
            //var FinishRinseTankTempStr = string.Empty;
            //var StripDryerTempStr = string.Empty;
            var BrushRollCurrent1Str = string.Empty;
            var BrushRollCurrent2Str = string.Empty;
            var dataCnt = 0;

            foreach (TBL_ProcessData data in datas)
            {
                if (data.Line_Speed == 0)
                    continue;

                var preCoilLength = coilLength;
                coilLength += (double)(data.Line_Speed) / 60;

                var prePos = Convert.ToInt32(preCoilLength);
                var pos = Convert.ToInt32(coilLength);

                if (prePos == pos)
                    continue;

                dataCnt++;

                LinespeedStr = LinespeedStr + pos + ":" + Convert.ToSingle(data.Line_Speed).GetPoint(4).ToString() + ",";
                LinerundirectionStr = LinerundirectionStr + pos + ":" + Convert.ToSingle(data.Line_run_direction).GetPoint(4).ToString() + ",";
                //TensionReelSpeedStr = TensionReelSpeedStr + pos + ":" + Convert.ToSingle(data.Tension_Reel_Speed).GetPoint(4).ToString() + ",";
                //ThreadingSpeedStr = ThreadingSpeedStr + pos + ":" + Convert.ToSingle(data.Threading_Speed).GetPoint(4).ToString() + ",";
                LinetensionStr = LinetensionStr  +pos + ":" + Convert.ToSingle(data.Line_Tension).GetPoint(4).ToString() + ",";
                No1GRBeltMotorCurrentStr = No1GRBeltMotorCurrentStr + pos + ":" + Convert.ToSingle(data.GR1_BeltMotor_Current).GetPoint(4).ToString() + ",";
                No1GRBeltSpeedStr = No1GRBeltSpeedStr + pos + ":" + Convert.ToSingle(data.GR1_Belt_Speed).GetPoint(4).ToString() + ",";
                No2GRBeltMotorCurrentStr = No2GRBeltMotorCurrentStr + pos + ":" + Convert.ToSingle(data.GR2_BeltMotor_Current).GetPoint(4).ToString() + ",";
                No2GRBeltSpeedStr = No2GRBeltSpeedStr + pos + ":" + Convert.ToSingle(data.GR2_Belt_Speed).GetPoint(4).ToString() + ",";
                No3GRBeltMotorCurrentStr = No3GRBeltMotorCurrentStr + pos + ":" + Convert.ToSingle(data.GR3_BeltMotor_Current).GetPoint(4).ToString() + ",";
                No4GRBeltMotorCurrentStr = No4GRBeltMotorCurrentStr + pos + ":" + Convert.ToSingle(data.GR4_BeltMotor_Current).GetPoint(4).ToString() + ",";
                No5GRBeltMotorCurrentStr = No5GRBeltMotorCurrentStr + pos + ":" + Convert.ToSingle(data.GR5_BeltMotor_Current).GetPoint(4).ToString() + ",";
                No6GRBeltMotorCurrentStr = No6GRBeltMotorCurrentStr + pos + ":" + Convert.ToSingle(data.GR6_BeltMotor_Current).GetPoint(4).ToString() + ",";
                //CoolantOilTankTempStr = CoolantOilTankTempStr + pos + ":" + Convert.ToSingle(data.CoolantTank_Temp).GetPoint(4).ToString() + ",";
                //AlkaliSolutionTankTempStr = AlkaliSolutionTankTempStr + pos + ":" + Convert.ToSingle(data.AlkaliTank_Temp).GetPoint(4).ToString() + ",";
                //PrimaryRinseWaterTankTempStr = PrimaryRinseWaterTankTempStr + pos + ":" + Convert.ToSingle(data.PrimaryRinseTank_Temp).GetPoint(4).ToString() + ",";
                //FinishRinseTankTempStr = FinishRinseTankTempStr + pos + ":" + Convert.ToSingle(data.FinishRinseTank_Temp).GetPoint(4).ToString() + ",";
                //StripDryerTempStr + pos + ":" + Convert.ToSingle(data.StripDryerTemp).GetPoint(4).ToString() + ",";
                BrushRollCurrent1Str = BrushRollCurrent1Str + pos + ":" + Convert.ToSingle(data.BrushRoll1_Current).GetPoint(4).ToString() + ",";
                BrushRollCurrent2Str = BrushRollCurrent2Str + pos + ":" + Convert.ToSingle(data.BrushRoll2_Current).GetPoint(4).ToString() + ",";

            }

            dataModel.TotalLength = coilLength.GetPoint(4).ToString();
            dataModel.DataCnt = dataCnt;
            dataModel.LinespeedStr = LinespeedStr.TrimEnd(',');
            dataModel.LinerundirectionStr = LinerundirectionStr.TrimEnd(',');
            //dataModel.TensionReelSpeedStr = TensionReelSpeedStr.TrimEnd(',');
            dataModel.LinetensionStr = LinetensionStr.TrimEnd(',');
            //dataModel.ThreadingSpeedStr = ThreadingSpeedStr.TrimEnd(',');
            dataModel.No1GRAbrasiveBeltMotorCurrentStr = No1GRBeltMotorCurrentStr.TrimEnd(',');
            dataModel.No1GRAbrasiveBeltSpeedStr = No1GRBeltSpeedStr.TrimEnd(',');
            dataModel.No2GRAbrasiveBeltMotorCurrentStr = No2GRBeltMotorCurrentStr.TrimEnd(',');
            dataModel.No2GRAbrasiveBeltSpeedStr = No2GRBeltSpeedStr.TrimEnd(',');
            dataModel.No3GRAbrasiveBeltMotorCurrentStr = No3GRBeltMotorCurrentStr.TrimEnd(',');
            dataModel.No4GRAbrasiveBeltMotorCurrentStr = No4GRBeltMotorCurrentStr.TrimEnd(',');
            dataModel.No5GRAbrasiveBeltMotorCurrentStr = No5GRBeltMotorCurrentStr.TrimEnd(',');
            dataModel.No6GRAbrasiveBeltMotorCurrentStr = No6GRBeltMotorCurrentStr.TrimEnd(',');
            //dataModel.CoolantOilTankTemperatureStr = CoolantOilTankTempStr.TrimEnd(',');
            //dataModel.AlkaliSolutionTankTemperatureStr = AlkaliSolutionTankTempStr.TrimEnd(',');
            //dataModel.PrimaryRinseWaterTankTemperatureStr = PrimaryRinseWaterTankTempStr.TrimEnd(',');
            //dataModel.FinishRinseTankTemperatureStr = FinishRinseTankTempStr.TrimEnd(',');
            //dataModel.StripDryerTemperatureStr = StripDryerTempStr.TrimEnd(',');
            dataModel.BrushRollCurrent1Str = BrushRollCurrent1Str.TrimEnd(',');
            dataModel.BrushRollCurrent2Str = BrushRollCurrent2Str.TrimEnd(',');

                return dataModel;
        }
        public bool Create25GrindData(TBL_PDO pdo, GrindDataModel datas,int CurrentPassNumber,int CurrentSession)
        {
            pdo.VaildObjectNull("pdo", "pdo參數錯誤");
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");
            int inserNum = 0;



            try
            {
                inserNum = _gartingLogic.Create25CurrentPassNumberCT(pdo.ToL25CurrentPassNumberCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> CurrentPassNumberCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25CurrentSessionCT(pdo.ToL25CurrentSessionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> CurrentSessionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25GRDLineSpeedCT(pdo.ToL25GRDLineSpeedCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> GRDLineSpeedCT ", $"存取L25-OK");


                inserNum = _gartingLogic.Create25No1GRAB_beltKindCT(pdo.ToL25No1GRAB_beltKindCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAB_beltKindCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No1GRAB_beltRoughnessCT(pdo.ToL25No1GRAB_beltRoughnessCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAB_beltRoughnessCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No1GRAB_beltRotateDirectionCT(pdo.ToL25No1GRAB_beltRotateDirectionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No1GRAB_beltMotorCurrentCT(pdo.ToL25No1GRAB_beltMotorCurrentCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAB_beltMotorCurrentCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No1GRAB_beltSpeedCT(pdo.ToL25No1GRAB_beltSpeedCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAB_beltSpeedCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No2GRAB_beltKindCT(pdo.ToL25No2GRAB_beltKindCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAB_beltKindCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No2GRAB_beltRoughnessCT(pdo.ToL25No2GRAB_beltRoughnessCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAB_beltRoughnessCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No2GRAB_beltRotateDirectionCT(pdo.ToL25No2GRAB_beltRotateDirectionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No2GRAB_beltCurrentCT(pdo.ToL25No2GRAB_beltCurrentCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No2GRAB_beltSpeedCT(pdo.ToL25No2GRAB_beltSpeedCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAB_beltSpeedCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No3GRAB_beltKindCT(pdo.ToL25No3GRAB_beltKindCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No3GRAB_beltKindCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No3GRAB_beltRoughnessCT(pdo.ToL25No3GRAB_beltRoughnessCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No3GRAB_beltRoughnessCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No3GRAB_beltRotateDirectionCT(pdo.ToL25No3GRAB_beltRotateDirectionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No3GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No3GRAB_beltCurrentCT(pdo.ToL25No3GRAB_beltCurrentCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No3GRAB_beltCurrentCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No4GRAB_beltKindCT(pdo.ToL25No4GRAB_beltKindCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No4GRAB_beltKindCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No4GRAB_beltRoughnessCT(pdo.ToL25No4GRAB_beltRoughnessCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No4GRAB_beltRoughnessCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No4GRAB_beltRotateDirectionCT(pdo.ToL25No4GRAB_beltRotateDirectionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No4GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No4GRAB_beltCurrentCT(pdo.ToL25No4GRAB_beltCurrentCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No4GRAB_beltCurrentCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No5GRAB_beltKindCT(pdo.ToL25No5GRAB_beltKindCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No5GRAB_beltKindCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No5GRAB_beltRoughnessCT(pdo.ToL25No5GRAB_beltRoughnessCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No5GRAB_beltRoughnessCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No5GRAB_beltRotateDirectionCT(pdo.ToL25No5GRAB_beltRotateDirectionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No5GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No5GRAB_beltCurrentCT(pdo.ToL25No5GRAB_beltCurrentCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No5GRAB_beltCurrentCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No6GRAB_beltKindCT(pdo.ToL25No6GRAB_beltKindCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No6GRAB_beltKindCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No6GRAB_beltRoughnessCT(pdo.ToL25No6GRAB_beltRoughnessCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No6GRAB_beltRoughnessCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No6GRAB_beltRotateDirectionCT(pdo.ToL25No6GRAB_beltRotateDirectionCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No6GRAB_beltRotateDirectionCT ", $"存取L25-OK");

                inserNum = _gartingLogic.Create25No6GRAB_beltCurrentCT(pdo.ToL25No6GRAB_beltCurrentCTEntity(datas, CurrentPassNumber, CurrentSession));
                if (inserNum > 0)
                    _log.I($"存取L25=> No6GRAB_beltCurrentCT ", $"存取L25-OK");

                var insetOK = inserNum > 0;
                return insetOK;
            }
            catch (Exception e)
            {
                _log.E($"存取L25GrindData {inserNum}失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }
        public bool Create25ProcessCTData(TBL_PDO pdo, ProcessModel datas)
        {
            pdo.VaildObjectNull("pdo", "pdo參數錯誤");
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");

            int inserNum = 0;

            try
            {

                inserNum = _gartingLogic.Create25LineSpeedCT(pdo.ToL25LineSpeedCTEntity(datas));
                if (inserNum > 0 )
                    _log.I($"存取L25=> LineSpeedCT ", $"存取L25-OK");

                _gartingLogic.Create25LineTensionCT(pdo.ToL25LineTensionCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> LineTensionCT ", $"存取L25-OK");

                _gartingLogic.Create25LineRunDirectionCT(pdo.ToL25LineRunDirectionCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> LineRunDirectionCT ", $"存取L25-OK");

                _gartingLogic.Create25No1GRAbrasiveBeltMotorCurrentCT(pdo.ToL25No1GRAbrasiveBeltMotorCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAbrasiveBeltMotorCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No2GRAbrasiveBeltMotorCurrentCT(pdo.ToL25No2GRAbrasiveBeltMotorCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAbrasiveBeltMotorCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No3GRAbrasiveBeltMotorCurrentCT(pdo.ToL25No3GRAbrasiveBeltMotorCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No3GRAbrasiveBeltMotorCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No4GRAbrasiveBeltMotorCurrentCT(pdo.ToL25No4GRAbrasiveBeltMotorCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No4GRAbrasiveBeltMotorCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No5GRAbrasiveBeltMotorCurrentCT(pdo.ToL25No5GRAbrasiveBeltMotorCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No5GRAbrasiveBeltMotorCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No6GRAbrasiveBeltMotorCurrentCT(pdo.ToL25No6GRAbrasiveBeltMotorCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No6GRAbrasiveBeltMotorCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No1GRAbrasiveBeltSpeedCT(pdo.ToL25No1GRAbrasiveBeltSpeedCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1GRAbrasiveBeltSpeedCT ", $"存取L25-OK");

                _gartingLogic.Create25No2GRAbrasiveBeltSpeedCT(pdo.ToL25No2GRAbrasiveBeltSpeedCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2GRAbrasiveBeltSpeedCT ", $"存取L25-OK");

                _gartingLogic.Create25No1BrushRollCurrentCT(pdo.ToL25No1BrushRollCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No1BrushRollCurrentCT ", $"存取L25-OK");

                _gartingLogic.Create25No2BrushRollCurrentCT(pdo.ToL25No2BrushRollCurrentCTEntity(datas));
                if (inserNum > 0)
                    _log.I($"存取L25=> No2BrushRollCurrentCT ", $"存取L25-OK");

                var insetOK = inserNum > 0;

                return insetOK;
            }
            catch (Exception e)
            {
                _log.E($"存取L25ProcessCTData {inserNum}失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }
    }
}
