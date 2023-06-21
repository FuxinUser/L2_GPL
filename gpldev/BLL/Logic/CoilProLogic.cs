using Core.Define;
using DBService;
using DBService.Level25Repository.L2L25_CoilPDI;
using DBService.Level25Repository.L2L25_CoilPDO;
using DBService.Level25Repository.L2L25_CoilRejectResult;
using DBService.Repository;
using DBService.Repository.Belt;
using DBService.Repository.BeltPatterns;
using DBService.Repository.BeltPatternsRecords;
using DBService.Repository.CoilScheduleDelete;
using DBService.Repository.DefectData;
using DBService.Repository.GradeGroups;
using DBService.Repository.GrindPlan;
using DBService.Repository.GrindPlanHistory;
using DBService.Repository.GrindRecords;
using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.LookupTblGrindLevel;
using DBService.Repository.LookupTblLineTension;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.LookupTblSleeve;
using DBService.Repository.MaterialGrade;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.ReturnCoil;
using DBService.Repository.ReturnCoilTemp;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using DBService.Repository.SplitCoils;
using DBService.Repository.WieldRecord;
using DBService.Repository.WorkSchedule;
using MsgConvert;
using MsgConvert.DBTable;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static DBService.Repository.BeltPatterns.BeltPatternsEntity;
using static DBService.Repository.BeltPatternsRecords.BeltPatternsRecordEntity;
using static DBService.Repository.CoilRejResultEntity;
using static DBService.Repository.CoilScheduleDelete.CoilScheduleDeleteEntity;
using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.GradeGroups.GradeGroupsEntity;
using static DBService.Repository.MaterialGrade.MaterialGradeRepo;
using static DBService.Repository.GrindPlan.GrindPlanEntity;
using static DBService.Repository.LookupTblGrindLevel.LkUpTableGrindLevelEntity;
using static DBService.Repository.PDI.PDIEntity;
using static DBService.Repository.ReturnCoil.ReturnCoilEntity;
using static DBService.Repository.ScheduleDeleteRecordTempEntity;
using static DBService.Repository.WorkSchedule.WorkScheduleEntity;
using static DBService.Repository.LookupTblSleeve.LkUpTableSleeveEntity;
using static DBService.Repository.LookupTblPaper.LkUpTablePaperEntity;

/**
 Author: ICSC Spyua
 Desc: Coil Pro BLL
 Date: 2019/12/26
*/

namespace BLL.Coil
{
    public class CoilProLogic
    {
        private BeltPatternRepo _beltPatternRepo;
        private BeltAccRepo _beltAccRepo;
        private CoilPDIRepo _coilPDIRepo;
        private CoilPDORepo _coilPDORepo;
        private PDOUploadedReplyRepo _pdoUploadedReplyRepo;
        private CoilRejectResultRepo _coilRejectRepo;
        private DefectDataRepo _defectDataRepo;
        private WeldRecordsRepo _weldRecordsRepo;
        private ProductionScheduleRepo _coilScheduleRepo;
        private GrindPlanHistoryRepo _grindPlanHistoryRepo;
        private GrindRecordsRepo _grindRecordsRepo;

        private MaterialGradeRepo _materialGradeRepo;
        private LkUpTableFlattenerRepo _lktblFlattenerRepo;
        private LkUpTableLineTensionRepo _lktblLineTensionRepo;
        private LkUpTableSlideTrimmerRepo _lktblSlideTrimmerRepo;
        private LkUpTablePaperRepo _lktblPaperRepo;
        private LkUpTableSleeveRepo _lktblSleeveRepo;
        private LkUpTableGrindLevelRepo _lktblGrindLevelRepo;

        private SplitCoilsRepo _splitCoilRepo;
        private ProductionScheduleRepo _productionScheduleRepo;
        private WorkScheduleRepo _workScheduleRepo;
        private BeltPatternsRecordRepo _beltPatternsRecordRepo;
        private CoilScheduleDeleteRepo _coilScheduleDeleteRepo;
        private CoilMapRepo _coilMapRepo;
        private ScheduleDeleteRecordTempRepo _scheduleDeleteRecordTempRepo;
        private ReturnCoilTempRepo _returnCoilTempRepo;

        private L2L25_CoilPDIRepo _l2l25CoiPDIRepo;
        private L2L25_CoilPDORepo _l2l25_CoilPDORepo;
        private L2L25_CoilRejectResultRepo _l2l25_CoilRejectResultRepo;

        public CoilProLogic()
        {
            _coilPDORepo = new CoilPDORepo(DBParaDef.DBConn);
            _coilPDIRepo = new CoilPDIRepo(DBParaDef.DBConn);
            _coilScheduleRepo = new ProductionScheduleRepo(DBParaDef.DBConn);
            _coilRejectRepo = new CoilRejectResultRepo(DBParaDef.DBConn);
            _pdoUploadedReplyRepo = new PDOUploadedReplyRepo(DBParaDef.DBConn);


            _materialGradeRepo = new MaterialGradeRepo(DBParaDef.DBConn);
            _lktblFlattenerRepo = new LkUpTableFlattenerRepo(DBParaDef.DBConn);
            _lktblLineTensionRepo = new LkUpTableLineTensionRepo(DBParaDef.DBConn);
            _lktblSlideTrimmerRepo = new LkUpTableSlideTrimmerRepo(DBParaDef.DBConn);
            _lktblGrindLevelRepo = new LkUpTableGrindLevelRepo(DBParaDef.DBConn);

            _weldRecordsRepo = new WeldRecordsRepo(DBParaDef.DBConn);
            _beltAccRepo = new BeltAccRepo(DBParaDef.DBConn);
            _defectDataRepo = new DefectDataRepo(DBParaDef.DBConn);
            _lktblSleeveRepo = new LkUpTableSleeveRepo(DBParaDef.DBConn);
            _lktblPaperRepo = new LkUpTablePaperRepo(DBParaDef.DBConn);
            _beltPatternRepo = new BeltPatternRepo(DBParaDef.DBConn);
            _grindPlanHistoryRepo = new GrindPlanHistoryRepo(DBParaDef.DBConn);
            _splitCoilRepo = new SplitCoilsRepo(DBParaDef.DBConn);
            _productionScheduleRepo = new ProductionScheduleRepo(DBParaDef.DBConn);
            _grindRecordsRepo = new GrindRecordsRepo(DBParaDef.DBConn);
            _workScheduleRepo = new WorkScheduleRepo(DBParaDef.DBConn);
            _beltPatternsRecordRepo = new BeltPatternsRecordRepo(DBParaDef.DBConn);
            _coilScheduleDeleteRepo = new CoilScheduleDeleteRepo(DBParaDef.DBConn);
            _scheduleDeleteRecordTempRepo = new ScheduleDeleteRecordTempRepo(DBParaDef.DBConn);
            _coilMapRepo = new CoilMapRepo(DBParaDef.DBConn);
            _returnCoilTempRepo = new ReturnCoilTempRepo(DBParaDef.DBConn);

            _l2l25CoiPDIRepo = new L2L25_CoilPDIRepo(DBParaDef.Level2_5DBConn);
            _l2l25_CoilPDORepo = new L2L25_CoilPDORepo(DBParaDef.Level2_5DBConn);
            _l2l25_CoilRejectResultRepo = new L2L25_CoilRejectResultRepo(DBParaDef.Level2_5DBConn);
        }

        #region -- PDI邏輯 --

        /// <summary>
        /// 獲取鋼捲未上線排程資料
        /// </summary>
        //private PDIEntity.TBL_PDI GetPDI(string planNo, string matSeqNo)
        //{

        //    try
        //    {
        //        return _coilPDIRepo.Get(new string[] { planNo, matSeqNo });
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public PDIEntity.TBL_PDI GetPDIOnly(string planNo,string InCoilNo)
        {
            //sql.Append("Select");
            //sql.Append(" * ");
            //sql.Append(" FROM ");
            //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener));
            //sql.Append(" Where ");
            //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max) + ">" + coilThickness);
            //sql.Append(" and ");
            //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min) + "<=" + coilThickness);
            //sql.Append(" and ");
            //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max) + ">" + stripYieldStress);
            //sql.Append(" and ");
            //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min) + "<=" + stripYieldStress);
            //sql.Append(" and ");
            //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade) + "=" + $"'{matericalGrade}'");

            //return _lktblFlattenerRepo.DBContext.Query<LkUpTableFlattenerEntity.TBL_LookupTable_Flattener>(sql.ToString()).FirstOrDefault();


            try
            {
                var sql = new StringBuilder();
                sql.Append("Select TOP(1) * ");
                sql.Append(" FROM ");
                sql.Append(nameof(TBL_PDI));
                sql.Append(" Where ");
                sql.Append(nameof(TBL_PDI.Plan_No) + "='" + planNo +"'");
                sql.Append(" AND ");
                sql.Append(nameof(TBL_PDI.In_Coil_ID) + "='" + InCoilNo + "'");
                sql.Append(" ORDER BY CreateTime DESC ");
                return _coilPDIRepo.DBContext.Query <PDIEntity.TBL_PDI>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public PDIEntity.TBL_PDI GetPDI(string entryCoilNo)
        {
            try
            {
                return _coilPDIRepo.GetAll($"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{entryCoilNo}'")
                                   .OrderByDescending(pdi => pdi.CreateTime)
                                   .FirstOrDefault();
            }
            catch 
            {
                throw;
            }
        }

      
        public int CreatePDI(TBL_PDI entity)
        {
         
            try
            {
                var insertNum = _coilPDIRepo.Insert(entity);
                return insertNum;
            }
            catch 
            {    
                throw;
            }

        }


        public int UpdatePDI(string coilNo, TBL_PDI entity)
        {
        
            // 更新 PDI Table
            try
            {
                //var updateNum = _coilPDIRepo.Update(entity, new string[] { coilNo });
                var updateNum = _coilPDIRepo.Update(entity, new string[] {  coilNo, entity.Plan_No });
                //{ pdo.Out_Coil_ID, pdo.In_Coil_ID }
                return updateNum;
            }
            catch 
            {
                throw;
            }

        }
        public int UpdateDummy(string coilNo,PDOEntity.TBL_PDO pdo)
        {
            //var pdi = dummyCoil.ToTblCoilPDI();
            //pdi.Order_No = "DUMMY";   //合同號改為DUMMY,讓三級判斷
            try
            {
                string sql = string.Empty;
                sql = "UPDATE ";
                sql += nameof(TBL_PDI);
                sql += " SET ";
                //sql += nameof(TBL_PDI.Plan_No) + "='" + pdi.Plan_No + "',";
                //sql += nameof(TBL_PDI.Mat_Seq_No) + "='" + pdi.Mat_Seq_No + "',";
                //sql += nameof(TBL_PDI.Is_Dummy_Coil) + "='" + pdi.Is_Dummy_Coil + "',";
                sql += nameof(TBL_PDI.In_Coil_Thick) + "='" + pdo.Out_Coil_Thick + "',";
                sql += nameof(TBL_PDI.In_Coil_Width) + "='" + pdo.Out_Coil_Width + "',";
                sql += nameof(TBL_PDI.In_Coil_Wt) + "='" + pdo.Out_Coil_Act_WT + "',";
                sql += nameof(TBL_PDI.In_Coil_Length) + "='" + pdo.Out_Coil_Length + "',";
                sql += nameof(TBL_PDI.In_Coil_Inner_Diameter) + "='" + pdo.Out_Coil_Inner_Diameter + "',";
                sql += nameof(TBL_PDI.In_Coil_Outer_Diameter) + "='" + pdo.Out_Coil_Outer_Diameter + "'";
                //sql += nameof(TBL_PDI.Density) + "='" + pdi.Density + "',";
                //sql += nameof(TBL_PDI.St_No) + "='" + pdi.St_No + "',";
                //sql += nameof(TBL_PDI.Sg_Sign) + "='" + pdi.Sg_Sign + "',";
                sql += " WHERE ";
                sql += nameof(TBL_PDI.In_Coil_ID) + "='" + coilNo.Trim() + "'";
                sql += " AND Order_No = 'DUMMY' ";
                return _coilPDIRepo.DBContext.ExecuteScalar<int>(sql);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteDummy(string coilNo)
        {
            return _coilPDIRepo.DeleteDummy(new string[] { coilNo });
        }

        public bool VaildHasPDI(string coilNo)
        {
            return _coilPDIRepo.HasData(new string[] { coilNo });
        }
        public bool VaildHasPDIandPlanNo(string coilNo,string planNo)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($"select count(1) ");
                sql.Append($"from {DBColumnDef.PDITbl}");
                sql.Append($" Where {DBColumnDef.PDIEntryMatNo} = '{coilNo}' ");
                sql.Append($" AND Plan_No = '{planNo}' ");
                return _coilPDORepo.DBContext.ExecuteScalar<bool>(sql.ToString());
            }
            catch
            {
                throw;
            }
        }

        public bool VaildHasDummy(string coilNo)
        {
            return _coilPDIRepo.HasDummy(new string[] { coilNo });
        }
        public string GetPDIPlanNoByEntryCoilID (string coilNo)
        {
            try
            {
                return _coilPDIRepo.GetSpeicDataByEntryCoilID(nameof(PDIEntity.TBL_PDI.Plan_No), coilNo);
            }
            catch 
            {
                throw;
            }
        }

        public string GetOutMatNoByEntryCoilID(string entryCoilID)
        {
            try
            {
                return _coilPDIRepo.GetSpeicDataByEntryCoilID(nameof(PDIEntity.TBL_PDI.Out_Coil_ID), entryCoilID);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDIScrapedWeight(float weight, string coilNo)
        {
            try
            {
                return _coilPDIRepo.UpdateScrapedWeight(weight, coilNo);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateEntryScanCoilInfo(string coilNo, bool entryCoilIDChecked)
        {
            try
            {
                var isCheck = entryCoilIDChecked ? L2SystemDef.CheckCoilNo : L2SystemDef.UnCheckedCoilNo;
                return _coilPDIRepo.UpdateEntryScanCoilInfo(coilNo, isCheck);
            }
            catch
            {
                throw;                
            }
        }

        public int UpdatePDIStarTime(string coilIDNo)
        {
            try
            {
                return _coilPDIRepo.UpdateStarTime(coilIDNo);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDIFinishTime(string coilID)
        {
            try
            {
                return _coilPDIRepo.UpdateFinishTime(coilID);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDIEndTime(string coilIDNo)
        {
            try
            {
                return _coilPDIRepo.UpdateEndTime(coilIDNo);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateIsInfoWMSDown(bool infoDone, string coilIDNo)
        {
            try
            {
                var done = infoDone ? "1" : "0";
                return _coilPDIRepo.UpdateIsInfoWMSDown(done, coilIDNo);
            }
            catch
            {
                throw;
            }
        }

        public int CreateDummyCoilPDI(MMSL2Rcv.Msg_Dummy_Coil_List dummyCoil)
        {

            var pdi = dummyCoil.ToTblCoilPDI();
            pdi.Order_No = "DUMMY";   //合同號改為DUMMY,讓三級判斷
            try
            {
                var insertNum = _coilPDIRepo.Insert(pdi);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public PDIEntity.TBL_PDI GetPDIByInCoil(string inCoilNo)
        {
            try
            {
                //return _coilPDIRepo.GetAll($"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{inCoilNo}' ").FirstOrDefault();
                var sql = new StringBuilder();
                sql.Append("Select TOP(1) * ");
                sql.Append(" FROM ");
                sql.Append(nameof(TBL_PDI));
                sql.Append(" Where ");
                sql.Append(nameof(TBL_PDI.In_Coil_ID) + "='" + inCoilNo + "'");
                sql.Append(" ORDER BY CreateTime DESC ");
                return _coilPDIRepo.DBContext.Query<PDIEntity.TBL_PDI>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public PDIEntity.TBL_PDI GetPDIByOutCoil(string outCoilNo)
        {
            try
            {
                return _coilPDIRepo.GetAll($"{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)} = '{outCoilNo}'  ORDER BY CreateTime desc").FirstOrDefault();               
            }
            catch
            {
                throw;
            }
        }


        public int CreateL25PDI(L2L25_CoilPDI tb)
        {

            try
            {
                return _l2l25CoiPDIRepo.Insert(tb);
            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region  -- PDO 邏輯 -- 

        public PDOEntity.TBL_PDO GetPDO(string outMatNo)
        {
            try
            {
                //return _coilPDORepo.Get(new string[] { outMatNo});
                var whereStr = new StringBuilder();
                whereStr.Append($"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{outMatNo}' ORDER BY CreateTime DESC  ");
                return _coilPDORepo.GetAll(whereStr.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public PDOEntity.TBL_PDO GetPDOonly(string planNo,string out_coil_id)
        {
            try
            {
                var whereStr = new StringBuilder();
                whereStr.Append($"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{out_coil_id}'");
                whereStr.Append(" AND ");
                whereStr.Append($"{nameof(PDOEntity.TBL_PDO.Plan_No)} = '{planNo}'");

                return _coilPDORepo.GetAll(whereStr.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }


        public int UpdateExitCoilIDChecked(string exitCoilNo, string exitCoilIDChecked)
        {
            try
            {
                return _coilPDORepo.UpdateExitCoilIDChecked(exitCoilNo, exitCoilIDChecked);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateExitCoilGrossWeight(string exitCoilNo, float CoilWeight)
        {
            try
            {
                return _coilPDORepo.UpdatePDOCoilGrossWeight(exitCoilNo, CoilWeight);
            }
            catch
            {
                throw;
            }
        }

        public int NewPDOData(PDOEntity.TBL_PDO pdo)
        {
            try
            {
                return _coilPDORepo.Insert(pdo);
            }
            catch
            {
                throw;
            }
        }
        //MMG110：反饋PDO是否處理成功   MWW-added 2023.04.25
        public int CreatePDOUploadedReply(PDOUploadedReplyEntity.TBL_PDOUploadedReply pdoUploadedReply)
        {
            try
            {
                return _pdoUploadedReplyRepo.Insert(pdoUploadedReply);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDO(PDOEntity.TBL_PDO pdo)
        {
            try
            {
                return _coilPDORepo.Update(pdo, new string[] { pdo.Out_Coil_ID, pdo.In_Coil_ID });
            }
            catch
            {
                throw;
            }
        }

        public bool VaildHasPDO(string outCoilNo)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($"select count(1) ");
                sql.Append($"from {DBColumnDef.PDOTbl}");
                sql.Append($" Where {DBColumnDef.PDOOutMatNo} = '{outCoilNo}' ");
                return _coilPDORepo.DBContext.ExecuteScalar<bool>(sql.ToString());
            }
            catch
            {
                throw;
            }
        }

        public int UpdateExitCoilWt(string planNo,string exitCoilNo, float coilWt, float coilPureWt)
        {
            try
            {
                return _coilPDORepo.UpdatePDOCoilWt(planNo,exitCoilNo, coilWt, coilPureWt);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDOFinishTime(string coilNo, DateTime dateTime)
        {

            var dbObj = new
            {
                FinishTime = dateTime
            };

            try
            {
                return _coilPDORepo.DBContext.Update(nameof(PDOEntity.TBL_PDO), dbObj, $"{nameof(PDOEntity.TBL_PDO.In_Coil_ID)} = '{coilNo}'");
            }
            catch
            {
                throw;
            }

        }

        public int UpdateUploadFlag(string planNo,string exitCoilNo, bool upload)
        {
            try
            {
                var uploadOk = upload ? "1" : "0";
                return _coilPDORepo.UpdateUploadFlag(planNo,exitCoilNo, uploadOk);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25PDO(L2L25_CoilPDO tb)
        {
            try
            {
                return _l2l25_CoilPDORepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- Schedule邏輯 --

        public List<string> GetUnScheduleCoilIDs(int num)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($" Select TOP {num} CoilSched.{DBColumnDef.CoilSchedCoilID} ");
                sql.Append($" FROM {DBColumnDef.CoilSchedTbl} CoilSched LEFT JOIN {DBColumnDef.PDITbl} PDI ");
                sql.Append($" ON CoilSched.{DBColumnDef.CoilSchedCoilID} = PDI.{DBColumnDef.PDIEntryMatNo}");
                //sql.Append(" WHERE TBL_Production_Schedule.Schedule_Status IN ('N','R','F','I')" );
                //只搜尋排程狀態為 (N-新鋼捲  R-要求入料  F-已入料  I-身分確認成功 )
                sql.Append($" WHERE CoilSched.{DBColumnDef.CoilSchedScheduleStatus} IN ('N','R','F','I') ");
                //sql.Append($" Where {DBColumnDef.PDIIsDummyCoil} = '0' ");

                return _coilScheduleRepo.DBContext.Query<string>(sql.ToString()).ToList();
  
            }catch
            {
                throw;
            }
          
        }

        public List<string> GetScheduleRequestCoilIDs(int num)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($" Select TOP {num} CoilSched.{DBColumnDef.CoilSchedCoilID} ");
                sql.Append($" FROM {DBColumnDef.CoilSchedTbl} CoilSched LEFT JOIN {DBColumnDef.PDITbl} PDI ");
                sql.Append($" ON CoilSched.{DBColumnDef.CoilSchedCoilID} = PDI.{DBColumnDef.PDIEntryMatNo}");
                //sql.Append(" WHERE TBL_Production_Schedule.Schedule_Status IN ('N','R','F','I')" );
                //只搜尋排程狀態為 (N-新鋼捲  R-要求入料  F-已入料  I-身分確認成功 )
                //sql.Append($" WHERE CoilSched.{DBColumnDef.CoilSchedScheduleStatus} IN ('R') ");
                sql.Append($" Where {DBColumnDef.CoilSchedScheduleStatus} = 'R' ");

                return _coilScheduleRepo.DBContext.Query<string>(sql.ToString()).ToList();

            }
            catch
            {
                throw;
            }

        }

        public IEnumerable<TBL_PDI> QueryScheduleCoilIDByPlanNo(string planNo)
        {
            //var sql = new StringBuilder();
            //sql.Append($"Select * ");
            //sql.Append($" FROM {DBColumnDef.CoilSchedTbl} CoilSched LEFT JOIN {DBColumnDef.PDITbl} PDI ");
            //sql.Append($" ON CoilSched.{DBColumnDef.CoilSchedCoilID} = PDI.{DBColumnDef.PDIEntryMatNo}");
            //sql.Append($" Where {DBColumnDef.PDIPlanNo} = '{planNo}' ");
            try
            {
                //var coilIDs = _coilPDIRepo.DBContext.Query<L3L2_PDI>(sql.ToString());
                var pdis = _coilPDIRepo.GetAll($"{nameof(TBL_PDI.Plan_No)} = '{planNo}'");

                return pdis;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<string> QueryScheduleCoilIDByPlanNoAndNotInfoWMS(string planNo, bool isInfoWMS)
        {

            var infoWMSDone = isInfoWMS ? "1" : "0";

            var sql = new StringBuilder();
            sql.Append($"Select CoilSched.{DBColumnDef.CoilSchedCoilID} ");
            sql.Append($" FROM {DBColumnDef.CoilSchedTbl} CoilSched LEFT JOIN {DBColumnDef.PDITbl} PDI ");
            sql.Append($" ON CoilSched.{DBColumnDef.CoilSchedCoilID} = PDI.{DBColumnDef.PDIEntryMatNo}");
            sql.Append($" Where {DBColumnDef.PDIPlanNo} = '{planNo}' AND {DBColumnDef.Is_Info_WMS_Down} = '{infoWMSDone}' ");
            try
            {
                var coilIDs = _coilPDIRepo.DBContext.Query<string>(sql.ToString());
                return coilIDs;
            }
            catch
            {
                throw;
            }
        }


        public TBL_Production_Schedule GetCoilSchedule(string coilID) 
        {
            try{
                var coilSchedule = _coilScheduleRepo.Get(new string[] { coilID});
                return coilSchedule;

            }
            catch 
            { 
                throw;
            }
        }
          
        public bool VaildHasCoilSchedule(string coilID)
        {
            try
            {
                return _coilScheduleRepo.HasData(new string[] { coilID});
            }
            catch 
            {                
                throw;
            }
        }

        public List<string> QueryCollScheduleIDs(int selectCount)
        {
            var sql = new StringBuilder();
            //sql.Append($" Select TOP {selectCount} CoilSched.{DBColumnDef.CoilSchedCoilID} ");
            sql.Append($" Select TOP {selectCount} (Coil_ID + SPACE(20-dataLength(coil_ID))) as Coil_ID ");
            sql.Append($" FROM {DBColumnDef.CoilSchedTbl} CoilSched LEFT JOIN {DBColumnDef.PDITbl} PDI ");
            sql.Append($" ON CoilSched.{DBColumnDef.CoilSchedCoilID} = PDI.{DBColumnDef.PDIEntryMatNo}");
            sql.Append($" Where {DBColumnDef.PDIIsDummyCoil} = '0' ");
            sql.Append($" ORDER BY ");
            sql.Append($" [{DBColumnDef.CoilSchedSeqNo}] ");

            var pdiList = _coilScheduleRepo.DBContext.Query<string>(sql.ToString());
            return pdiList.ToList();
        }

        public int CreateCoilSchedule(string coilid, short seqNo, string updateSource = "0")
        {
            var coilSchedule = new TBL_Production_Schedule
            {
                Coil_ID = coilid,
                Seq_No = seqNo,
                Update_Source = updateSource,
            };


            try
            {
                return _coilScheduleRepo.Insert(coilSchedule);
            }
            catch
            {
                throw;
            }

        }

        public int GetCoilScheduleTotalCountFromDB()
        {
            try
            {
                return _coilScheduleRepo.GetTotalNum();
            }
            catch
            {     
                throw;
            }
        }
        public int UpdateScheduleSeqNo(string coilNo, short seqNo)
        {
            try
            {
                return _coilScheduleRepo.UpdateScheduleSeqNo(coilNo, seqNo);
            }
            catch
            {
                throw;
            }
        }
        public int UpdateScheduleStatus(string coilNo, string statuts)
        {
            try
            {
                return _coilScheduleRepo.UpdateScheduleStatus(coilNo, statuts);
            }
            catch
            {
                throw;
            }
        }
        public int GetSeqNo(string coilID)
        {
            string sql = $"SELECT {nameof(TBL_Production_Schedule.Seq_No)} FROM {nameof(TBL_Production_Schedule)} WHERE {nameof(TBL_Production_Schedule.Coil_ID)} = '{coilID}'";

            try
            {
                var seqNo = _coilPDIRepo.DBContext.QuerySingleOrDefault<int>(sql);
                return seqNo;
            }
            catch
            {
                throw;
            }
        }
        public string GetScheduleStatuts(string coilID)
        {
            string sql = $"SELECT {nameof(TBL_Production_Schedule.Schedule_Status)} FROM {nameof(TBL_Production_Schedule)} WHERE {nameof(TBL_Production_Schedule.Coil_ID)} = '{coilID}'";

            try
            {
                var statuts = _coilPDIRepo.DBContext.QuerySingleOrDefault<string>(sql);
                return statuts;
            }
            catch
            {
                throw;
            }
        }
        public string GetFirstCoilSchedule()
        {
            string sql = " Select Top(1) Coil_ID From "; 
            sql += $"  {nameof(TBL_Production_Schedule)} ";
            sql += " where ";
            sql += $" {nameof(TBL_Production_Schedule.Schedule_Status)}  NOT IN ('{CoilDef.ScheduleStatuts.Producing_Statuts}','{CoilDef.ScheduleStatuts.EntryCoilDone_Statuts}','{CoilDef.ScheduleStatuts.ProduceDone_Statuts}','{CoilDef.ScheduleStatuts.ReturnCoil_Statuts}') ";
            sql += " ORDER BY Seq_No  ASC";


            try
            {
                var coilID = _coilPDIRepo.DBContext.QuerySingleOrDefault<string>(sql);
                return coilID;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteCoildSchedsAfterSeqNo(int seqNo)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" Delete ");
            sqlBuilder.Append($"  {nameof(TBL_Production_Schedule)} ");
            sqlBuilder.Append(" where ");
            sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Seq_No)} >= {seqNo} ");
            sqlBuilder.Append(" AND ");
            sqlBuilder.Append(" ( ");
            sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.ScheduleStatuts.NewCoil_Statuts}' ");
            sqlBuilder.Append(" OR ");
            sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.ScheduleStatuts.RequestEntryCoil_Statuts}' ");
            sqlBuilder.Append(" ) ");
            try
            {
                var deleteNum = _coilPDIRepo.DBContext.Execute(sqlBuilder.ToString());
                return deleteNum;
            }
            catch
            {
                throw;
            }           
        }

        public int DeleteAllSchedule()
        {         
            try
            {
                var deleteNum = _coilScheduleRepo.Delete(null);
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteAllIdleSchedule()
        {
            try
            {
                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append(" DELETE ");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append($" {nameof(TBL_Production_Schedule)}  ");
                sqlBuilder.Append(" Where ");
                sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.ScheduleStatuts.NewCoil_Statuts}' ");
                sqlBuilder.Append(" OR ");
                sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.ScheduleStatuts.RequestEntryCoil_Statuts}' ");


                //var deleteNum = _coilScheduleRepo.Delete(null);
                var deleteNum = _coilScheduleRepo.DBContext.Execute(sqlBuilder.ToString());
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteSchedule(string coilID)
        {
            try
            {
                var deleteNum = _coilScheduleRepo.Delete(new string[] {coilID});

                if (deleteNum > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteScheduleOnly(string coilID)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($"Delete  ");
                sql.Append($"from {DBColumnDef.CoilSchedTbl}");
                sql.Append($" Where {DBColumnDef.CoilSchedCoilID} = '{coilID}' ");
                sql.Append($" AND Schedule_Status = 'P' ");           
                return _coilScheduleDeleteRepo.DBContext.ExecuteScalar<bool>(sql.ToString());
            }

            catch
            {
                throw;
            }
        }
        public int CreateSchedule(string coilid, short seqNo, string updateSource = "0")
        {
            var coilSchedule = new TBL_Production_Schedule
            {
                Coil_ID = coilid,
                Seq_No = seqNo,
                Schedule_Status = CoilDef.ScheduleStatuts.NewCoil_Statuts,
                Update_Source = updateSource,
                UpdateTime = DateTime.Now,
            };

            try
            {
                return _coilScheduleRepo.Insert(coilSchedule);
            }
            catch
            {
                throw;
            }

        }

        public void CreateSchedules(DataTable dt)
        {
            try
            {
                _coilScheduleRepo.DBContext.SqlBulkCopy(dt, nameof(CoilScheduleEntity.TBL_Production_Schedule));
                //_coilScheduleRepo.DBContext.BulkInsert<CoilScheduleModel.L3L2_Production_Schedule>(schedules, nameof(CoilScheduleModel.L3L2_Production_Schedule));
            }
            catch
            {
                throw;
            }
        }

        public int CreateDelScheduleRecordTemp(L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp recordTemp)
        {
            try
            {
                var insertNum = _scheduleDeleteRecordTempRepo.Insert(recordTemp);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public bool VaildHasScheduleTemp(string coilID)
        {
            try
            {
                //return _scheduleDeleteRecordTempRepo.HasData(new string[] { coilID });
                return _returnCoilTempRepo.HasData(new string[] { coilID });
            }
            catch
            {
                throw;
            }
        }
        public bool VaildHasCoilRejectTemp(string coilID,string planNo)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(" FROM ");
                sql.Append($" {nameof(TBL_RetrunCoil_Temp)}  ");
                sql.Append(" Where ");
                sql.Append($" {nameof(TBL_RetrunCoil_Temp.OriPDI_Out_Coil_ID)} = '{coilID}' ");
                sql.Append(" AND ");
                sql.Append(" ( ");
                sql.Append($" {nameof(TBL_RetrunCoil_Temp.Plan_No)} = '{planNo}' ");
                sql.Append(" ) ");
                return _returnCoilTempRepo.DBContext.ExecuteScalar<bool>(sql.ToString());

                //return _scheduleDeleteRecordTempRepo.HasData(new string[] { coilID });
                //return _returnCoilTempRepo.HasData(new string[] { coilID });
            }
            catch
            {
                throw;
            }
        }
        public bool VaidHasCoilMap(string coilID,short skidPos)
        {
            try
            {
                string SQLwhere ;
                switch (skidPos)
                {
                    case PlcSysDef.Pos.ESK01:
                        SQLwhere = " Entry_SK01 " + " = '" + coilID + "' ";
                        break;
                    case PlcSysDef.Pos.ETOP:
                        SQLwhere = " Entry_TOP " + " = '" + coilID + "' ";
                        break;          
                    default:
                        SQLwhere = " 1 = 1 ";
                        break;
                };

                var sql = new StringBuilder();
                sql.Append($"select count(*)  ");
                sql.Append($"from ");
                sql.Append(nameof(CoilMapEntity.TBL_CoilMap));
                sql.Append($" Where "+ SQLwhere );
                //var test = _coilMapRepo.DBContext.ExecuteScalar<int>(sql.ToString());
                return _coilMapRepo.DBContext.ExecuteScalar<bool>(sql.ToString());
            }

            catch
            {
                throw;
            }

        }
        public int DeleteDelScheduleTempRecord(string coilID)
        {
            try
            {
                var insertNum = _scheduleDeleteRecordTempRepo.Delete(new string[] { coilID});
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        //DelCoilRejectTempRecord
        public bool DelCoilRejectTempRecord(string coilID,string planNo)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(" FROM ");
                sql.Append($" {nameof(TBL_RetrunCoil_Temp)}  ");
                sql.Append(" Where ");
                sql.Append($" {nameof(TBL_RetrunCoil_Temp.OriPDI_Out_Coil_ID)} = '{coilID}' ");
                sql.Append(" AND ");
                sql.Append(" ( ");
                sql.Append($" {nameof(TBL_RetrunCoil_Temp.Plan_No)} = '{planNo}' ");
                sql.Append(" ) ");

                return _returnCoilTempRepo.DBContext.ExecuteScalar<bool>(sql.ToString());

             
            }
            catch
            {
                throw;
            }
        }
        public int CreateDelScheduleRecord(TBL_CoilScheduleDelete delDecord)
        {
            try
            {
                var insertNum = _coilScheduleDeleteRepo.Insert(delDecord);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- 退料實機 --

        public TBL_RetrunCoil_Temp GetCoilRejectTemp(string rejectCoilNo,string planNo)
        {
            try
            {
                //var entity = _returnCoilTempRepo.GetAll()
                //                                .Where(x => x.Reject_Coil_No.Equals(rejectCoilNo))
                //                                .OrderByDescending(x => x.CreateTime)
                //                                .FirstOrDefault();

                //var entity = _returnCoilTempRepo.Get(new string[] { rejectCoilNo });
                //return entity;
                var sql = new StringBuilder();
                sql.Append("Select TOP(1)");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(TBL_RetrunCoil_Temp));
                sql.Append(" Where ");
                sql.Append(nameof(TBL_RetrunCoil_Temp.Reject_Coil_No) + "=" + $"'{rejectCoilNo}'");
                sql.Append(" And ");
                sql.Append(nameof(TBL_RetrunCoil_Temp.Plan_No) + "=" + $"'{planNo}'");
                return _returnCoilTempRepo.DBContext.Query<TBL_RetrunCoil_Temp>(sql.ToString()).FirstOrDefault();

            }
            catch
            {
                throw;
            }
        }

        public int DeleteCoilRejectTemp(string rejectCoilNo)
        {
            try
            {
                return _returnCoilTempRepo.Delete(new string[] { rejectCoilNo });
            }
            catch
            {
                throw;
            }
        }

        public TBL_CoilRejectResult GetCoilRejectResult(string coilNo)
        {
            try
            {
                var coilReject = _coilRejectRepo.Get(new string[] { coilNo });
                return coilReject;

            }
            catch 
            {
                throw;
            }
        }

        public int CreateCoilRejectResult(TBL_CoilRejectResult entity)
        {
            try
            {
                return _coilRejectRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int CreateL25CoilRejectResult(L2L25_CoilRejectResult entity)
        {
            try
            {
                return _l2l25_CoilRejectResultRepo.Insert(entity);
            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region --  Lok Up Table --

        // 鋼種大類
        public string GetMatericalGrade(string stNo)
        {
            try
            {
                var grade = _materialGradeRepo.Get(new string[] { stNo });

                return grade != null ? grade.Material_Grade : string.Empty;
            }
            catch
            {
                throw;
            }
        }

        public MaterialGradeEntity.TBL_SteelNoToMaterialGrade GetYield_Stress(string stNo)
        {
            try
            {

                var sql = new StringBuilder();
                sql.Append("Select TOP(1)");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade));
                sql.Append(" Where ");
                sql.Append(nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No) + "=" + $"'{stNo}'");
                return _materialGradeRepo.DBContext.Query<MaterialGradeEntity.TBL_SteelNoToMaterialGrade>(sql.ToString()).FirstOrDefault();


            }
            catch
            {
                throw;
            }
        }

        public LkUpTableFlattenerEntity.TBL_LookupTable_Flattener GetFlatterBySYandThick(string matericalGrade, int stripYieldStress, float coilThickness)
        {
            try
            {
                var sql = new StringBuilder();

                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener));
                sql.Append(" Where ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max) + ">" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min) + "<=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max) + ">" + stripYieldStress);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min) + "<=" + stripYieldStress);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade) + "=" + $"'{matericalGrade}'");

                return _lktblFlattenerRepo.DBContext.Query<LkUpTableFlattenerEntity.TBL_LookupTable_Flattener>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public LkUpTableLineTensionEntity.TBL_LookupTable_LineTension GetLineTensionByGradeAndThick(string matericalGrade, float coilThickness, float coilWidth,string ProcessType = "C")
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("Select TOP(1)");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension));
                sql.Append(" Where ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max) + ">" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min) + "<=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max) + ">" + coilWidth);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min) + "<=" + coilWidth);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade) + "=" + $"'{matericalGrade}'");
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.ProcessType) + "=" + $"'{ProcessType}'");

                return _lktblLineTensionRepo.DBContext.Query<LkUpTableLineTensionEntity.TBL_LookupTable_LineTension>(sql.ToString()).FirstOrDefault();
                
            }
            catch
            {
                throw;
            }
        }
        public LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer GetLineSideTrimmerByGradeAndThick(string coilGrade, float coilThickness)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer));
                sql.Append(" Where ");
                sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max) + ">=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min) + "<=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.SteelGrade) + "=" + "'GradeA'");

                return _lktblSlideTrimmerRepo.DBContext.QuerySingleOrDefault<LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer>(sql.ToString());

            }
            catch
            {
                throw;
            }
        }


        // 墊紙資料
        public LkUpTablePaperEntity.TBL_LookupTable_Paper GetPaperData(string code)
        {
            try
            {
                return _lktblPaperRepo.Get(new string[] { code });
            }
            catch
            {
                throw;
            }
        }
        
        // 套筒資料
        public LkUpTableSleeveEntity.TBL_LookupTable_Sleeve GetSleeveData(string code)
        {
            try
            {
                return _lktblSleeveRepo.Get(new string[] { code });
            }
            catch
            {
                throw;
            }
        }

        public TBL_LookupTableGrindLevel GetGrindLevel(string code)
        {

            try
            {
                return _lktblGrindLevelRepo.Get(new string[] { code });
            }
            catch
            {
                throw;
            }

        }

        public TBL_LookupTableGrindLevel GetGrindLevel(string outerGrade, string innerGrade)
        {

            try
            {
                return _lktblGrindLevelRepo.GetAll($"{nameof(TBL_LookupTableGrindLevel.OuterGrade)} = '{outerGrade}' AND {nameof(TBL_LookupTableGrindLevel.InnerGrade)} = '{outerGrade}'").FirstOrDefault();
            }
            catch
            {
                throw;
            }

        }


        #endregion

        #region -- Weld Record --

        public int CreateWeldRecords(WeldRecordEntity.TBL_WeldRecords tblWeldRecord)
        {
            try
            {
                return _weldRecordsRepo.Insert(tblWeldRecord);
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region -- Belt相關 --
        public int UpdateBeltAccLengthByGrNo(int mountGrNo, float beltLength, float stLength)
        {
            try
            {
                return _beltAccRepo.UpdateBeltAccLengthByGrNo(mountGrNo, beltLength, stLength);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateAccLengthByBeltNo(string beltNo,  float beltLength, float stLength)
        {
            try
            {
                return _beltAccRepo.UpdateAccLengthByBeltNo(beltNo, beltLength, stLength);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateGrNoByBeltNo(string beltNo, int grNo)
        {
            try
            {
                return _beltAccRepo.UpdateGrNoByBeltNo(beltNo, grNo);
            }
            catch
            {
                throw;
            }
        }

        public BeltAccEntity.TBL_Belts GetBelt(string beltNo)
        {
            try
            {
                return _beltAccRepo.Get(new string[] { beltNo });
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TBL_BeltPatterns> QueryBeltPatterns(string coilNo)
        {



            var sql = new StringBuilder();
            sql.Append(" Select ");

            sql.Append("GrindPlan." + nameof(TBL_GrindPlan.Pass_Section) + "," +
                       "GrindPlan." + nameof(TBL_GrindPlan.LineSpeed) + "," +
                       "GrindPlan." + nameof(TBL_GrindPlan.PassNumber) + "," +
                       "Patterns.*");
            
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_PDI) + " AS " + " pdi ");
      
            sql.Append(" Left join ");
            sql.Append(nameof(TBL_GradeGroups) + " AS " + " GradeGroup ");
            sql.Append(" on ");
            sql.Append($" pdi."+ nameof(TBL_PDI.Order_Cust_Code)+ "=" + "GradeGroup."+ nameof(TBL_GradeGroups.CustomerNo));
            sql.Append(" and "); 
            sql.Append(" pdi."+ nameof(TBL_PDI.St_No)+ "=" + "GradeGroup." + nameof(TBL_GradeGroups.SteelGrade));

            sql.Append(" Left join ");
            sql.Append(nameof(TBL_GrindPlan) + " AS " + " GrindPlan ");
            sql.Append(" on ");
            sql.Append(" GrindPlan."+nameof(TBL_GrindPlan.GradeGroup)+"=" +"GradeGroup."+nameof(TBL_GradeGroups.GradeGroup));
            sql.Append(" and ");
            sql.Append(" pdi."+ nameof(TBL_PDI.In_Coil_Thick));
            sql.Append(" between ");
            sql.Append(" GrindPlan."+nameof(TBL_GrindPlan.Thickness_From)+ " and " + "GrindPlan."+nameof(TBL_GrindPlan.Thickness_To));


            sql.Append(" Left join ");
            sql.Append(nameof(TBL_BeltPatterns) + " AS " + " Patterns ");
            sql.Append(" on ");
            sql.Append(" Patterns."+nameof(TBL_BeltPatterns.BeltPattern)+"=" +"GrindPlan."+nameof(TBL_GrindPlan.BeltPattern));

            sql.Append(" Where ");
            sql.Append(" pdi."+nameof(TBL_PDI.In_Coil_ID)+ "=" + "'" + coilNo + "'" );

            #region Old Code
            //var sql = new StringBuilder();
            //sql.Append(" Select ");



            //sql.Append(" FROM ");
            //sql.Append(nameof(CoilPDIModel.TBL_PDI) + " AS " + " pdi ");

            //sql.Append(" Left join ");
            //sql.Append(nameof(GradeGroupsModel.TBL_GradeGroups) + " AS " + " GradeGroup ");
            //sql.Append(" on ");
            //sql.Append($" pdi.Order_Cust_Code = GradeGroup.CustomerNo ");
            //sql.Append(" and ");
            //sql.Append(" pdi.St_No = GradeGroup.SteelGrade ");

            //sql.Append(" Left join ");
            //sql.Append(nameof(GrindPlanModel.TBL_GrindPlan) + " AS " + " GrindPlan ");
            //sql.Append(" on ");
            //sql.Append(" GrindPlan.GradeGroup = GradeGroup.GradeGroup ");
            //sql.Append(" and ");
            //sql.Append(" pdi.In_Mat_Thick ");
            //sql.Append(" between ");
            //sql.Append(" GrindPlan.Thickness_From and GrindPlan.Thickness_To ");


            //sql.Append(" Left join ");
            //sql.Append(nameof(BeltPatternsModel.TBL_BeltPatterns) + " AS " + " Patterns ");
            //sql.Append(" on ");
            //sql.Append(" Patterns.BeltPattern = GrindPlan.BeltPattern ");

            //sql.Append(" Where ");
            //sql.Append($" pdi.In_Mat_No = '{coilNo}' ")
            #endregion

            try
            {
                var beltPatterns = _beltPatternRepo.DBContext.Query<BeltPatternsEntity.TBL_BeltPatterns>(sql.ToString());
                return beltPatterns;
            }
            catch
            {
                throw;
            }

        }

        public IEnumerable<BeltPatternsEntity.TBL_BeltPatterns> QueryBeltPasses(string coilNo)
        {
          
            #region Sql 組成
            var sql = new StringBuilder();
            sql.Append(" Select ");
            sql.Append(" GrindPlan."+ nameof(TBL_GrindPlan.Pass_Section)+ "," +  
                       "GrindPlan."+nameof(TBL_GrindPlan.PassNumber));
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_PDI) + " AS " + " pdi ");

            sql.Append(" Left join ");
            sql.Append(nameof(TBL_GradeGroups) + " AS " + " GradeGroup ");
            sql.Append(" on ");
            sql.Append(" pdi." + nameof(TBL_PDI.Order_Cust_Code) + "=" + "GradeGroup."+nameof(TBL_GradeGroups.CustomerNo));
            sql.Append(" and ");
            sql.Append(" pdi."+ nameof(TBL_PDI.St_No) +"="+ "GradeGroup."+nameof(TBL_GradeGroups.SteelGrade));

            sql.Append(" Left join ");
            sql.Append(nameof(TBL_GrindPlan) + " AS " + " GrindPlan ");
            sql.Append(" on ");
            sql.Append(" GrindPlan."+nameof(TBL_GrindPlan.GradeGroup)+ "=" + "GradeGroup."+nameof(TBL_GradeGroups.GradeGroup));
            sql.Append(" and ");
            sql.Append(" pdi." + nameof(TBL_PDI.In_Coil_Thick));
            sql.Append(" between ");
            sql.Append(" GrindPlan." + nameof(TBL_GrindPlan.Thickness_From) + " and " + "GrindPlan." + nameof(TBL_GrindPlan.Thickness_To));

            sql.Append(" Where ");
            sql.Append(" pdi." + nameof(TBL_PDI.In_Coil_ID) + "=" + "'" + coilNo + "'");
            #endregion

            #region Old Code
            //var sql = new StringBuilder();
            //sql.Append(" Select ");
            //sql.Append(" GrindPlan.Pass_Section, GrindPlan.PassNumber ");
            //sql.Append(" FROM ");
            //sql.Append(nameof(TBL_PDI) + " AS " + " pdi ");

            //sql.Append(" Left join ");
            //sql.Append(nameof(TBL_GradeGroups) + " AS " + " GradeGroup ");
            //sql.Append(" on ");
            //sql.Append(" pdi.Order_Cust_Code = GradeGroup.CustomerNo ");
            //sql.Append(" and ");
            //sql.Append(" pdi.St_No = GradeGroup.SteelGrade ");

            //sql.Append(" Left join ");
            //sql.Append(nameof(TBL_GrindPlan) + " AS " + " GrindPlan ");
            //sql.Append(" on ");
            //sql.Append(" GrindPlan.GradeGroup = GradeGroup.GradeGroup ");
            //sql.Append(" and ");
            //sql.Append(" pdi.In_Mat_Thick ");
            //sql.Append(" between ");
            //sql.Append(" GrindPlan.Thickness_From and GrindPlan.Thickness_To ");

            //sql.Append(" Where ");
            //sql.Append($" pdi.In_Mat_No = '{coilNo}' ");
            #endregion

            try
            {
                var beltPatterns = _beltPatternRepo.DBContext.Query<BeltPatternsEntity.TBL_BeltPatterns>(sql.ToString());
                return beltPatterns;
            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<TBL_BeltPatterns> QueryBeltPatternsByBelt(string beltPattern)
        {

            #region Sql 組成
            var sql = new StringBuilder();
            sql.Append(" Select ");
            sql.Append(" * ");
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_BeltPatterns));
            sql.Append(" Where ");
            sql.Append($" {nameof(TBL_BeltPatterns.BeltPattern)} = '{beltPattern}' ");
            #endregion
            try
            {
                var beltPatterns = _beltPatternRepo.DBContext.Query<TBL_BeltPatterns>(sql.ToString());
                return beltPatterns;
            }
            catch
            {
                throw;
            }
        }



        public IEnumerable<GrindPlanEntity.TBL_GrindPlan> QueryBeltPlans(string coilNo)
        {

            #region Sql 組成
            var sql = new StringBuilder();
            sql.Append(" Select ");
            sql.Append(" GrindPlan.* ");
            sql.Append(" FROM ");
            sql.Append(nameof(PDIEntity.TBL_PDI) + " AS " + " pdi ");

            sql.Append(" Left join ");
            sql.Append(nameof(GradeGroupsEntity.TBL_GradeGroups) + " AS " + " GradeGroup ");
            sql.Append(" on ");
            sql.Append(" pdi.Order_Cust_Code = GradeGroup.CustomerNo ");
            sql.Append(" and ");
            sql.Append(" pdi.St_No = GradeGroup.SteelGrade ");

            sql.Append(" Left join ");
            sql.Append(nameof(GrindPlanEntity.TBL_GrindPlan) + " AS " + " GrindPlan ");
            sql.Append(" on ");
            sql.Append(" GrindPlan.GradeGroup = GradeGroup.GradeGroup ");
            sql.Append(" and ");
            sql.Append(" pdi.In_Mat_Thick ");
            sql.Append(" between ");
            sql.Append(" GrindPlan.Thickness_From and GrindPlan.Thickness_To ");

            sql.Append(" Where ");
            sql.Append($" pdi.In_Mat_No = '{coilNo}' ");
            #endregion
            try
            {
                var beltPatterns = _beltPatternRepo.DBContext.Query<GrindPlanEntity.TBL_GrindPlan>(sql.ToString());
                return beltPatterns;
            }
            catch
            {
                throw;
            }
        }


        public int CreateGrindPlanHistory(GrindPlanHistoryEntity.TBL_GrindPlanHistory grindPlanHistory)
        {

            try
            {
                return _grindPlanHistoryRepo.Insert(grindPlanHistory);
            }
            catch
            {
                throw;
            }
        }

        public int CreateBeltPatternsRecord(TBL_BeltPatterns_Records record)
        {
            try
            {
                return _beltPatternsRecordRepo.Insert(record);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- Defect Data --

        public int CreateDefectData(L3L2_TBL_DefectData defectData)
        {
            try
            {
                return _defectDataRepo.Insert(defectData);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateDefectData(L3L2_TBL_DefectData defectData)
        {
            try
            {

                
                return _defectDataRepo.Update(defectData, new string[] { defectData.CoilID, 
                                                                         defectData.DefectCode.ToString(), 
                                                                         defectData.DefectPositionWidthDirection,
                                                                         defectData.DefectLevel  });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 取得最後一比Defect Data
        /// </summary>
        public L3L2_TBL_DefectData GetDefectData(L1L2Rcv.Msg_108_Defect_Data msg)
        {

            var DefectPositionWidthDirection = msg.DefectPosition.TransferMMSDeffectPosWidthType();
            try
            {
                //var  list = _defectDataRepo.GetAll($"{nameof(CoilDefectDataModel.L3L2_TBL_DefectData.CoilID)} = '{msg.CoilID}'");
                //return list.Count() != 0 ? list.OrderByDescending(i => i.DefectPoLengthEndDirection).FirstOrDefault() : new CoilDefectDataModel.L3L2_TBL_DefectData();
                var sql = new StringBuilder();
                sql.Append($"{nameof(L3L2_TBL_DefectData.CoilID)} = '{msg.CoilID}'");
                sql.Append(" AND ");
                sql.Append($"{nameof(L3L2_TBL_DefectData.DefectLevel)} = '{msg.DefectLevel}'");
                sql.Append(" AND ");
                sql.Append($"{nameof(L3L2_TBL_DefectData.DefectCode)} = '{msg.DefectKind}'");
                sql.Append(" AND ");
                sql.Append($"{nameof(L3L2_TBL_DefectData.DefectPositionWidthDirection)} = '{DefectPositionWidthDirection}'");

                var list = _defectDataRepo.GetAll(sql.ToString());
                return list.FirstOrDefault();
                //return list.Count() != 0 ? list.OrderByDescending(i => i.DefectPoLengthEndDirection).FirstOrDefault() : new CoilDefectDataModel.L3L2_TBL_DefectData();
            }
            catch
            {
                throw;
            }
        }


        public float GetDefectRecordLastLength()
        {
            try
            {
                var list = _defectDataRepo.GetAll();
                return list.Count() != 0 ? list.OrderByDescending(i => i.DefectPoLengthEndDirection).FirstOrDefault().DefectPoLengthEndDirection : 0;

            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<L3L2_TBL_DefectData> QueryDefect(string coilNo, int count)
        {         
            try
            {
                var sql = new StringBuilder();
                sql.Append($" Select TOP {count} ");
                sql.Append($" * FROM {nameof(L3L2_TBL_DefectData)} ");
                sql.Append($" Where {nameof(L3L2_TBL_DefectData.CoilID)} = '{coilNo}' ");
                sql.Append($" order by {nameof(L3L2_TBL_DefectData.CreateTime)} DESC ");
                var defectDatas = _defectDataRepo.DBContext.Query<L3L2_TBL_DefectData>(sql.ToString());
                return defectDatas;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- 鋼卷分切 --

        public string GetSplitParentCoilNo(string coilID)
        {
            try
            {
                var splitCoil = _splitCoilRepo.Get(new string[] { coilID });

                return splitCoil==null ? string.Empty : splitCoil.ParentCoil_ID;
            }
            catch
            {
                throw;
            }
        }

        public int GetParentCnt(string parentCoilID)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(SplitCoilsEntity.TBL_SplitCoils));
                sql.Append(" Where ");
                sql.Append(nameof(SplitCoilsEntity.TBL_SplitCoils.ParentCoil_ID) + "=" + "'" + parentCoilID + "'");
                return _splitCoilRepo.DBContext.Query<SplitCoilsEntity.TBL_SplitCoils>(sql.ToString()).Count();
            }
            catch
            {
                throw;
            }
        }
        public SplitCoilsEntity.TBL_SplitCoils GetPreSplitCoilTime(string parentCoilID, int SplitCnt)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("SELECT a1.CreateTime");
                sql.Append(" FROM (");
                sql.Append(" SELECT ROW_NUMBER() OVER(ORDER BY Coil_ID) AS ROWID ");
                sql.Append(" ,Coil_ID,Plan_No,ParentCoil_ID,CreateTime ");
                sql.Append(" FROM ");
                sql.Append(nameof(SplitCoilsEntity.TBL_SplitCoils));
                sql.Append(" WHERE ");
                sql.Append(" ParentCoil_ID " + "=" + "'" + parentCoilID.Trim() + "'");
                sql.Append(" ) a1");
                sql.Append(" WHERE ");
                sql.Append(" a1.ROWID " + "=" + "'" + (SplitCnt-1).ToString() + "'");
                //sql.Append(nameof(SplitCoilsEntity.TBL_SplitCoils.ParentCoil_ID) + "=" + "'" + parentCoilID + "'");
                return _splitCoilRepo.DBContext.Query<SplitCoilsEntity.TBL_SplitCoils>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }



        public int CreateChildCoilData(string childCoil, string parentCoil)
        {
            var data = new SplitCoilsEntity.TBL_SplitCoils
            {
                Coil_ID = childCoil,
                ParentCoil_ID = parentCoil
            };

            try
            {
                var insertNum = _splitCoilRepo.Insert(data);
                return insertNum;
            }
            catch
            {
                throw;
            }

        }
        public string SplitCoilPro(string parentCoilID, int childrenCoilCnt)
        {
            string out_Coil_ID = string.Empty;
            //鋼捲是否變號
            string sql = "SELECT TOP(1) out_Coil_ID";
            sql += " FROM ";
            sql += $"  {nameof(TBL_PDI)} ";
            sql += " Where ";
            sql += $"  {nameof(TBL_PDI.In_Coil_ID)}  = '{parentCoilID}'";
            sql += " ORDER BY CreateTime desc ";

            try
            {
                out_Coil_ID = _coilPDIRepo.DBContext.QuerySingleOrDefault<string>(sql);
            }
            catch
            {
                throw;
            }

            //鋼捲變號處理
            if (out_Coil_ID.Trim() != parentCoilID.Trim())
            {
                parentCoilID = out_Coil_ID.Trim();
            }

            var pcoilStrBuilder = new StringBuilder(parentCoilID);
            var childNo = (childrenCoilCnt + 1).ToString();
            // 第一次分切
            if (pcoilStrBuilder[parentCoilID.Length - 4].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 4, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 4, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第二次分切
            if (pcoilStrBuilder[parentCoilID.Length - 3].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 3, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 3, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第三次分切
            if (parentCoilID[parentCoilID.Length - 2].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 2, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 2, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第四次分切
            if (parentCoilID[parentCoilID.Length - 1].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 1, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 1, childNo);
                return pcoilStrBuilder.ToString();
            }
            return string.Empty;

        }
        public int UpdateHasScrapedFlag(bool hasScraped, string entryCoilNo)
        {
            try
            {
                return _coilPDIRepo.UpdateHasScrapedFlag(hasScraped ? "1" : "0", entryCoilNo);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region -- 套筒墊紙同步處理 --
        //public bool ExistSleeveCode(MMSL2Rcv.Msg_Sleeve_Value_Synchronize sleeveValue)
        public bool ExistSleeveCode(string code)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT * ");
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_LookupTable_Sleeve));
            sql.Append(" Where ");
            sql.Append($" {nameof(TBL_LookupTable_Sleeve.Sleeve_Code)} = '{code}'");
            var data = _lktblSleeveRepo.DBContext.Query<TBL_LookupTable_Sleeve>(sql.ToString()).FirstOrDefault();

            if (data != null)
            {
                return true;
            }
            else
            { 
               return false;
            }

           
        }
        public int CreateSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize sleeveValue)
        {
            var value = sleeveValue.ToTblLKSleeve();
            try
            {
                var insertNum = _lktblSleeveRepo.Insert(value);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int UpdateSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize sleeveValue)
        {
            var value = sleeveValue.ToTblLKSleeve();
            try
            {
                var updateNum = _lktblSleeveRepo.Update(value, new string[] { value.Sleeve_Code});
                return updateNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteSleeveValue(string code)
        {
           
            try
            {
                var deleteNum = _lktblSleeveRepo.Delete(new string[] { code });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        public bool ExistPaperValue(string code)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT * ");
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_LookupTable_Paper));
            sql.Append(" Where ");
            sql.Append($" {nameof(TBL_LookupTable_Paper.Paper_Code)} = '{code}'");
            var data = _lktblPaperRepo.DBContext.Query<TBL_LookupTable_Paper>(sql.ToString()).FirstOrDefault();

            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public int CreatePaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize paperValue)
        {
            var value = paperValue.ToTblLKPaper();
            try
            {
                var insertNum = _lktblPaperRepo.Insert(value);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int UpdatePaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize paperValue)
        {
            var value = paperValue.ToTblLKPaper();
            try
            {
                var updateNum = _lktblPaperRepo.Update(value, new string[] { value.Paper_Code });
                return updateNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeletePaperValue(string code)
        {

            try
            {
                var deleteNum = _lktblPaperRepo.Delete(new string[] { code });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }



        #endregion

        #region -- Grind相關 --

        public IEnumerable<GrindRecordsEntity.TBL_GrindRecords> QueryGrindRecords(string coilNo,string Plan_No)
        {
            try
            {
                return _grindRecordsRepo.GetAll($"{nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)} = '{coilNo}' AND {nameof(GrindRecordsEntity.TBL_GrindRecords.Plan_No)} = '{Plan_No}'");
            }
            catch
            {
                throw;
            }
        }


        public int GetGR6LastRecordsParticleNo(string coilNo)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($" SELECT {nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_PARTICLE_NO)} FROM {nameof(GrindRecordsEntity.TBL_GrindRecords)}");
                sql.Append($" Where {nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)} = '{coilNo}'");
                sql.Append($" order by {nameof(GrindRecordsEntity.TBL_GrindRecords.Receive_Time)} DESC ");
                return _grindRecordsRepo.DBContext.Query<int>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }


        public string ParitcleCode(int particleNo)
        {
            if (particleNo < 100)
                return "80#";
            
            if (particleNo >= 100 && particleNo < 149)
                return "No.3";

            return "No.4";
        }

        #endregion

        #region -- 班次相關 -- 
        public TBL_WorkSchedule GetWorkSchedule(int shift, string ShiftDate)
        {

            try
            {
                var data = _workScheduleRepo.GetAll($"{nameof(TBL_WorkSchedule.Shift)} = '{shift}' AND {nameof(TBL_WorkSchedule.ShiftDate)} = '{ShiftDate}'").FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
        }

        public TBL_WorkSchedule GetScheduleByTime(DateTime time)
        {

            try
            {
                var sql = new StringBuilder();
                sql.Append("SELECT * ");
                sql.Append(" FROM ");
                sql.Append(nameof(TBL_WorkSchedule));
                sql.Append(" Where ");
                sql.Append($" {nameof(TBL_WorkSchedule.ShiftDate)} = '{time:yyyyMMdd}'");
                sql.Append(" And ");

                switch (time.ToString("HH:mm"))
                {
                    case "00:00":
                    case "04:00":
                    case "08:00":
                    case "12:00":
                    case "16:00":
                    case "20:00":
                        sql.Append($" {nameof(TBL_WorkSchedule.ShiftStartTime)} < '{time.AddMinutes(1).ToString("HH:mm")}'");
                        break;
                    default:
                        sql.Append($" {nameof(TBL_WorkSchedule.ShiftStartTime)} <= '{time.ToString("HH:mm")}'");
                        break;
                }

                sql.Append(" And ");
                sql.Append($" {nameof(TBL_WorkSchedule.ShiftEndTime)} >= '{time.ToString("HH:mm")}'");
                var data = _workScheduleRepo.DBContext.Query<TBL_WorkSchedule>(sql.ToString()).FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
        }


        #endregion
    }
}
