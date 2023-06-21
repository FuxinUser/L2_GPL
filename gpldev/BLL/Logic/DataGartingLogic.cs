using Core.Define;
using DBService.L1Repository;
using DBService.Level25Repository.L2L25_Alive;
using DBService.Level25Repository.L2L25_DownTime;
using DBService.Level25Repository.L2L25_ENGC;
using DBService.Level25Repository.L2L25_LineSpeedCT;
using DBService.Level25Repository.L2L25_LineTensionCT;
using DBService.Level25Repository.L2L25_LineRunDirectionCT;
using DBService.Level25Repository.L2L25_No1GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No2GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No3GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No4GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No5GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No6GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No1GRAbrasiveBeltSpeedCT;
using DBService.Level25Repository.L2L25_No2GRAbrasiveBeltSpeedCT;
using DBService.Level25Repository.L2L25_No1BrushRollCurrentCT;
using DBService.Level25Repository.L2L25_No2BrushRollCurrentCT;
using DBService.Level25Repository.L2L25_ProcessCT;
using DBService.Level25Repository.L2L25_CurrentPassNumberCT;
using DBService.Level25Repository.L2L25_CurrentSessionCT;
using DBService.Level25Repository.L2L25_GRDLineSpeedCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltSpeedCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltSpeedCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_CoilRejectResult;
using DBService.Repository.CoilCutReocrd;
using DBService.Repository.GrindRecords;
using DBService.Repository.LineFaultRecords;
using DBService.Repository.LineStatus;
using DBService.Repository.StripBrakeSignal;
using DBService.Repository.UmountRecord;
using DBService.Repository.Utility;
using DBService.Repository.WieldRecord;
using DBService.Repository.WorkSchedule;
using MsgConvert.DBTable;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DBService.Repository.GrindRecords.GrindRecordsEntity;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.LineFaultRecords.LineFaultDelRecordsEntity;
using static DBService.Repository.UmountRecord.UmountRecordEntity;
using static DBService.Repository.WieldRecord.WeldRecordEntity;
using static DBService.Repository.WorkSchedule.WorkScheduleEntity;
using static DBService.Repository.Utility.UtilityEntity;
using static DBService.Repository.LineStatus.ProcessDataEntity;


namespace BLL.Logic
{
    public class DataGartingLogic
    {
        private WeldRecordsRepo _weldRecordRepo;
        private GrindRecordsRepo _grindRecordsRepo;
        private ProcessDataRepo _processDataRepo;
        private UtilityRepo _utilityRepo;
        private CoilCutRecordRepo _coilCutRecordRepo;
        private StripBrakeSignalRepo _stripBrakeSignalRepo;
        private LineFaultRecordsRepo _lineFaultRecordsRepo;
        private UmountRecordRepo _umountRecordRepo;
        private WorkScheduleRepo _workScheduleRepo;

        private L1204HisMsgRepo _l1204PresetHisMsgRepo;
        private L1205HisMsgRepo _l1205PresetHisMsgRepo;

        private L2L25_AliveRepo _l2l25AliveRepo;
        private L2L25_ENGCRepo _l2l25EngcRepo;
        private L2L25_DownTimeRepo _l2l25_DownTimeRepo;
        private L2L25_LineSpeedCTRepo _L2L25_LineSpeedCTRepo;
        private L2L25_LineTensionCTRepo _L2L25_LineTensionCTRepo;
        private L2L25_LineRunDirectionCTRepo _L2L25_LineRunDirectionCTRepo;
        private L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo _L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo;
        private L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo _L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo;
        private L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo _L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo;
        private L2L25_No4GRAbrasiveBeltMotorCurrentCTRepo _L2L25_No4GRAbrasiveBeltMotorCurrentCTRepo;
        private L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo _L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo;
        private L2L25_No6GRAbrasiveBeltMotorCurrentCTRepo _L2L25_No6GRAbrasiveBeltMotorCurrentCTRepo;
        private L2L25_No1GRAbrasiveBeltSpeedCTRepo _L2L25_No1GRAbrasiveBeltSpeedCTRepo;
        private L2L25_No2GRAbrasiveBeltSpeedCTRepo _L2L25_No2GRAbrasiveBeltSpeedCTRepo;
        private L2L25_No1BrushRollCurrentCTRepo _L2L25_No1BrushRollCurrentCTRepo;
        private L2L25_No2BrushRollCurrentCTRepo _L2L25_No2BrushRollCurrentCTRepo;
        private L2L25_ProcessCTRepo _L2L25_ProcessCTRepo;
        private L2L25_CurrentPassNumberCTRepo _L2L25_CurrentPassNumberCTRepo;
        private L2L25_CurrentSessionCTRepo _L2L25_CurrentSessionCTRepo;
        private L2L25_GRDLineSpeedCTRepo _L2L25_GRDLineSpeedCTRepo;
        private L2L25_No1GRAB_beltKindCTRepo _L2L25_No1GRAB_beltKindCTRepo;
        private L2L25_No1GRAB_beltRoughnessCTRepo _L2L25_No1GRAB_beltRoughnessCTRepo;
        private L2L25_No1GRAB_beltMotorCurrentCTRepo _L2L25_No1GRAB_beltMotorCurrentCTRepo;
        private L2L25_No1GRAB_beltSpeedCTRepo _L2L25_No1GRAB_beltSpeedCTRepo;
        private L2L25_No1GRAB_beltRotateDirectionCTRepo _L2L25_No1GRAB_beltRotateDirectionCTRepo;
        private L2L25_No2GRAB_beltKindCTRepo _L2L25_No2GRAB_beltKindCTRepo;
        private L2L25_No2GRAB_beltRoughnessCTRepo _L2L25_No2GRAB_beltRoughnessCTRepo;
        private L2L25_No2GRAB_beltCurrentCTRepo _L2L25_No2GRAB_beltCurrentCTRepo;
        private L2L25_No2GRAB_beltSpeedCTRepo _L2L25_No2GRAB_beltSpeedCTRepo;
        private L2L25_No2GRAB_beltRotateDirectionCTRepo _L2L25_No2GRAB_beltRotateDirectionCTRepo;
        private L2L25_No3GRAB_beltKindCTRepo _L2L25_No3GRAB_beltKindCTRepo;
        private L2L25_No3GRAB_beltRoughnessCTRepo _L2L25_No3GRAB_beltRoughnessCTRepo;
        private L2L25_No3GRAB_beltCurrentCTRepo _L2L25_No3GRAB_beltCurrentCTRepo;
        private L2L25_No3GRAB_beltRotateDirectionCTRepo _L2L25_No3GRAB_beltRotateDirectionCTRepo;
        private L2L25_No4GRAB_beltKindCTRepo _L2L25_No4GRAB_beltKindCTRepo;
        private L2L25_No4GRAB_beltRoughnessCTRepo _L2L25_No4GRAB_beltRoughnessCTRepo;
        private L2L25_No4GRAB_beltCurrentCTRepo _L2L25_No4GRAB_beltCurrentCTRepo;
        private L2L25_No4GRAB_beltRotateDirectionCTRepo _L2L25_No4GRAB_beltRotateDirectionCTRepo;
        private L2L25_No5GRAB_beltKindCTRepo _L2L25_No5GRAB_beltKindCTRepo;
        private L2L25_No5GRAB_beltRoughnessCTRepo _L2L25_No5GRAB_beltRoughnessCTRepo;
        private L2L25_No5GRAB_beltCurrentCTRepo _L2L25_No5GRAB_beltCurrentCTRepo;
        private L2L25_No5GRAB_beltRotateDirectionCTRepo _L2L25_No5GRAB_beltRotateDirectionCTRepo;
        private L2L25_No6GRAB_beltKindCTRepo _L2L25_No6GRAB_beltKindCTRepo;
        private L2L25_No6GRAB_beltRoughnessCTRepo _L2L25_No6GRAB_beltRoughnessCTRepo;
        private L2L25_No6GRAB_beltCurrentCTRepo _L2L25_No6GRAB_beltCurrentCTRepo;
        private L2L25_No6GRAB_beltRotateDirectionCTRepo _L2L25_No6GRAB_beltRotateDirectionCTRepo;
        private L2L25_CoilRejectResultRepo _L2L25_CoilRejectResultRepo;


        public DataGartingLogic()
        {
            _weldRecordRepo = new WeldRecordsRepo(DBParaDef.DBConn);
            _grindRecordsRepo = new GrindRecordsRepo(DBParaDef.DBConn);
            _processDataRepo = new ProcessDataRepo(DBParaDef.DBConn);
            _utilityRepo = new UtilityRepo(DBParaDef.DBConn);
            _stripBrakeSignalRepo = new StripBrakeSignalRepo(DBParaDef.DBConn);
            _lineFaultRecordsRepo = new LineFaultRecordsRepo(DBParaDef.DBConn);
            _coilCutRecordRepo = new CoilCutRecordRepo(DBParaDef.DBConn);
            _umountRecordRepo = new UmountRecordRepo(DBParaDef.DBConn);
            _workScheduleRepo = new WorkScheduleRepo(DBParaDef.DBConn);

            _l1204PresetHisMsgRepo = new L1204HisMsgRepo(DBParaDef.HisDBConn);
            _l1205PresetHisMsgRepo = new L1205HisMsgRepo(DBParaDef.HisDBConn);


            _l2l25AliveRepo = new L2L25_AliveRepo(DBParaDef.Level2_5DBConn);
            _l2l25EngcRepo = new L2L25_ENGCRepo(DBParaDef.Level2_5DBConn);
            _l2l25_DownTimeRepo = new L2L25_DownTimeRepo(DBParaDef.Level2_5DBConn);
            _L2L25_LineSpeedCTRepo = new L2L25_LineSpeedCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_LineTensionCTRepo = new L2L25_LineTensionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_LineRunDirectionCTRepo = new L2L25_LineRunDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo = new L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo = new L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo = new L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No4GRAbrasiveBeltMotorCurrentCTRepo = new L2L25_No4GRAbrasiveBeltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo = new L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No6GRAbrasiveBeltMotorCurrentCTRepo = new L2L25_No6GRAbrasiveBeltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAbrasiveBeltSpeedCTRepo = new L2L25_No1GRAbrasiveBeltSpeedCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAbrasiveBeltSpeedCTRepo = new L2L25_No2GRAbrasiveBeltSpeedCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1BrushRollCurrentCTRepo = new L2L25_No1BrushRollCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2BrushRollCurrentCTRepo = new L2L25_No2BrushRollCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_ProcessCTRepo = new L2L25_ProcessCTRepo(DBParaDef.Level2_5DBConn);

            _L2L25_CurrentPassNumberCTRepo = new L2L25_CurrentPassNumberCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_CurrentSessionCTRepo = new L2L25_CurrentSessionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_LineRunDirectionCTRepo = new L2L25_LineRunDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_GRDLineSpeedCTRepo = new L2L25_GRDLineSpeedCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAB_beltKindCTRepo = new L2L25_No1GRAB_beltKindCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAB_beltRoughnessCTRepo = new L2L25_No1GRAB_beltRoughnessCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAB_beltRotateDirectionCTRepo = new L2L25_No1GRAB_beltRotateDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAB_beltMotorCurrentCTRepo = new L2L25_No1GRAB_beltMotorCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No1GRAB_beltSpeedCTRepo = new L2L25_No1GRAB_beltSpeedCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAB_beltKindCTRepo = new L2L25_No2GRAB_beltKindCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAB_beltRoughnessCTRepo = new L2L25_No2GRAB_beltRoughnessCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAB_beltRotateDirectionCTRepo = new L2L25_No2GRAB_beltRotateDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAB_beltCurrentCTRepo = new L2L25_No2GRAB_beltCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No2GRAB_beltSpeedCTRepo = new L2L25_No2GRAB_beltSpeedCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No3GRAB_beltKindCTRepo = new L2L25_No3GRAB_beltKindCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No3GRAB_beltRoughnessCTRepo = new L2L25_No3GRAB_beltRoughnessCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No3GRAB_beltRotateDirectionCTRepo = new L2L25_No3GRAB_beltRotateDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No3GRAB_beltCurrentCTRepo = new L2L25_No3GRAB_beltCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No4GRAB_beltKindCTRepo = new L2L25_No4GRAB_beltKindCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No4GRAB_beltRoughnessCTRepo = new L2L25_No4GRAB_beltRoughnessCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No4GRAB_beltRotateDirectionCTRepo = new L2L25_No4GRAB_beltRotateDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No4GRAB_beltCurrentCTRepo = new L2L25_No4GRAB_beltCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No5GRAB_beltKindCTRepo = new L2L25_No5GRAB_beltKindCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No5GRAB_beltRoughnessCTRepo = new L2L25_No5GRAB_beltRoughnessCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No5GRAB_beltRotateDirectionCTRepo = new L2L25_No5GRAB_beltRotateDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No5GRAB_beltCurrentCTRepo = new L2L25_No5GRAB_beltCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No6GRAB_beltKindCTRepo = new L2L25_No6GRAB_beltKindCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No6GRAB_beltRoughnessCTRepo = new L2L25_No6GRAB_beltRoughnessCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No6GRAB_beltRotateDirectionCTRepo = new L2L25_No6GRAB_beltRotateDirectionCTRepo(DBParaDef.Level2_5DBConn);
            _L2L25_No6GRAB_beltCurrentCTRepo = new L2L25_No6GRAB_beltCurrentCTRepo(DBParaDef.Level2_5DBConn);



        }

        public int CreateCoilWeldRecord(TBL_WeldRecords entity)
        {
            try
            {               
                var insertNum = _weldRecordRepo.Insert(entity);
                return insertNum;
            }catch
            {  
                throw;
            }            
        }

        public int CreateGrdRpt(TBL_GrindRecords entity)
        {
            try
            {
                var insertNum = _grindRecordsRepo.Insert(entity);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateProcessData(L1L2Rcv.Msg_104_ProData msg)
        {
            try
            {
                var tbl = msg.ToTblProcessData();
                var insertNum = _processDataRepo.Insert(tbl);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateUtility(TBL_Utility entity)
        {
            try
            {
                
                var insertNum = _utilityRepo.Insert(entity);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateStripBrakeSignal(L1L2Rcv.Msg_124_StripBrakeSignal msg)
        {
            try
            {
                var tbl = msg.ToTblStripBrakeSignal();
                var insertNum = _stripBrakeSignalRepo.Insert(tbl);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        // LineFault相關
        public int CreateLineFaultRecord(TBL_LineFaultRecords faultRecord)
        {
            try
            {
                var insertNum = _lineFaultRecordsRepo.Insert(faultRecord);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateFaultRecord(TBL_LineFaultRecords faultRecord)
        {
            try
            {
                var whereCondition = new string[] { faultRecord.prod_time.ToString("yyyy-MM-dd"),
                                                    faultRecord.stop_start_time.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                                    };

                var insertNum = _lineFaultRecordsRepo.Update(faultRecord, whereCondition);
                
                return insertNum;
            }
            catch
            {
                throw;
            }
        }


        public int CreateCoilCutRecord(L1L2Rcv.Msg_125_Share_Cut_Data msg)
        {
            try
            {
                var tblCoulCutRecortd = msg.ToTblCoilCutRecord();
                var insertNum = _coilCutRecordRepo.Insert(tblCoulCutRecortd);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateUmountPORRecord(TBL_UnmountRecord record)
        {

            try
            {                
                _umountRecordRepo.Delete(new string[] { record.CoilID });
                var insertNum = _umountRecordRepo.Insert(record);                
                return insertNum;      
            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<L2L1MsgDBModel.L2L1_204>  QueryAll204HisMsg()
        {
            try
            {                           
                return _l1204PresetHisMsgRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<L2L1MsgDBModel.L2L1_205> QueryAll205HisMsg()
        {
            try
            {
                return _l1205PresetHisMsgRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public L2L1MsgDBModel.L2L1_204 Get204HisDataByTime(string createTime)
        {
            try
            {
                var data = _l1204PresetHisMsgRepo.Get(new string[] { createTime.ToString() });
                return data;
            }
            catch
            {
                throw;
            }
        }

        public L2L1MsgDBModel.L2L1_205 Get205HisDataByTime(string createTime)
        {
            try
            {
                var data = _l1205PresetHisMsgRepo.Get(new string[] { createTime.ToString() });
                return data;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodTime"> yyyy-MM-dd </param>
        /// <param name="endStarTime">yyyy-MM-dd HH:mm:ss</param>
        /// <param name="uploadStatuts">1:UploadDone 0:UDㄟUpload</param>
        /// <returns></returns>
        public int UpdateLineFaultUploadFlag(string prodTime, string stopStarTime,string stopEndTime, string uploadStatuts)
        {

            var sql = new StringBuilder();
            sql.Append("UPDATE  ");    
            sql.Append(nameof(TBL_LineFaultRecords));
            sql.Append(" SET ");
            sql.Append($" {nameof(TBL_LineFaultRecords.UploadMMS)} = '{uploadStatuts}'");
            sql.Append(" Where ");
            sql.Append($" {nameof(TBL_LineFaultRecords.prod_time)} = '{prodTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultRecords.stop_start_time)} = '{stopStarTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultRecords.stop_end_time)} = '{stopEndTime}'");

            try
            {
                //var tbField = new
                //{
                //    UploadMMS = uploadStatuts,
                //};

                //return _lineFaultRecordsRepo.UpdateField(prodTime, stopStarTime, tbField);
                var data = _lineFaultRecordsRepo.DBContext.Execute(sql.ToString());
                return data;
            }
            catch
            {
                throw;
            }
        }
        public TBL_LineFaultRecords GetLineFaultRecord(string prodTime, string stopStartTime,string stopEndTime)
        {

            var sql = new StringBuilder();
            sql.Append("SELECT * ");
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_LineFaultRecords));
            sql.Append(" Where ");
            sql.Append($" {nameof(TBL_LineFaultRecords.prod_time)} = '{prodTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultRecords.stop_start_time)} = '{stopStartTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultRecords.stop_end_time)} = '{stopEndTime}'");

            try
            {
                //return _lineFaultRecordsRepo.Get(new string[] { prodTime, stopStartTime });
                var data = _lineFaultRecordsRepo.DBContext.Query<TBL_LineFaultRecords>(sql.ToString()).FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
        }
        public TBL_LineFaultRecords GetLineFaultDelRecord(string prodTime, string ShifNo, string ShiftGroup, string stopStartTime, string stopEndTime, string delayLocation)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT * ");
            sql.Append(" FROM ");
            sql.Append(nameof(TBL_LineFaultDelRecords));
            sql.Append(" Where ");
            sql.Append($" {nameof(TBL_LineFaultDelRecords.prod_time)} = '{prodTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultDelRecords.prod_shift_no)} = '{ShifNo}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultDelRecords.prod_shift_group)} = '{ShiftGroup}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultDelRecords.stop_start_time)} = '{stopStartTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultDelRecords.stop_end_time)} = '{stopEndTime}'");
            sql.Append(" And ");
            sql.Append($" {nameof(TBL_LineFaultDelRecords.delay_location)} = '{delayLocation}'");
            try
            {
               
                var data = _lineFaultRecordsRepo.DBContext.Query<TBL_LineFaultRecords>(sql.ToString()).FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
            
        }
        public TBL_WorkSchedule GetWorkSchedule(int shift,  string ShiftDate)
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
                sql.Append($" {nameof(TBL_WorkSchedule.ShiftDate)} = '{time:yyyyMMdd}'" );
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

        public TBL_LineFaultRecords GetLineFaultRecord (string prodTime)
        {
         
            try
            {
                var data = _lineFaultRecordsRepo.GetAll($"{nameof(TBL_LineFaultRecords.prod_time)} = '{prodTime}'").FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
        }

        public TBL_LineFaultRecords GetLastLineFaultUnfinshRecord()
        {

            try
            {
                var sql = new StringBuilder();
                sql.Append($" Select Top (1) * From {nameof(TBL_LineFaultRecords)} ");
                sql.Append($" Where {nameof(TBL_LineFaultRecords.stop_end_time)} is Null ");
                sql.Append($" Order by {nameof(TBL_LineFaultRecords.CreateTime)} DESC ");
                var data = _lineFaultRecordsRepo.DBContext.QueryFirstOrDefault<TBL_LineFaultRecords>(sql.ToString());
                return data;
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25Alive(L2L25_Alive entity)
        {
            try
            {
                return _l2l25AliveRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25Engc(L2L25_ENGC entity)
        {
            try
            {
                return _l2l25EngcRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25DownTime(L2L25_DownTime entity)
        {
            try
            {
                return _l2l25_DownTimeRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<TBL_GrindRecords> QueryGrindData(string CoilID, string PlanNo,int PassNumber,int Session)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(TBL_GrindRecords)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Coil_ID)} = '"+ CoilID + "'");
            strBuilder.Append(" AND ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Plan_No)}  = '" + PlanNo + "'");
            strBuilder.Append(" AND ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Current_Pass)}  = '" + PassNumber + "'");
            strBuilder.Append(" AND ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Current_Senssion)}  = '" + Session + "'");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Receive_Time)} ");
            strBuilder.Append(" ASC ");
            var entitys = _grindRecordsRepo.DBContext.Query<TBL_GrindRecords>(strBuilder.ToString());
            return entitys;
        }
        public IEnumerable<TBL_GrindRecords> QueryGrindData_Total(string CoilID, string PlanNo)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(TBL_GrindRecords)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Coil_ID)} = '" + CoilID + "'");
            strBuilder.Append(" AND ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Plan_No)}  = '" + PlanNo + "'");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(TBL_GrindRecords.Receive_Time)} ");
            strBuilder.Append(" ASC ");
            var entitys = _grindRecordsRepo.DBContext.Query<TBL_GrindRecords>(strBuilder.ToString());
            return entitys;
        }
        public IEnumerable<TBL_ProcessData> QueryProcessData(string starTime, string endTime)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(TBL_ProcessData)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(TBL_ProcessData.Receive_Time)} ");
            strBuilder.Append(" Between ");
            strBuilder.Append($" '{starTime}' ");
            strBuilder.Append(" AND ");
            strBuilder.Append($" '{endTime}' ");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(TBL_ProcessData.Receive_Time)} ");
            strBuilder.Append(" ASC ");

            var entitys = _processDataRepo.DBContext.Query<TBL_ProcessData>(strBuilder.ToString());
            return entitys;
        }
        public int Create25LineSpeedCT(L2L25_LineSpeedCT entity)
        {
            try
            {

                return _L2L25_LineSpeedCTRepo.Insert(entity);

            }
            catch
            {
                throw;
            }
        }
        public int Create25LineTensionCT(L2L25_LineTensionCT entity)
        {
            try
            {
                return _L2L25_LineTensionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25LineRunDirectionCT(L2L25_LineRunDirectionCT entity)
        {
            try
            {
                
                return _L2L25_LineRunDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAbrasiveBeltMotorCurrentCT(L2L25_No1GRAbrasiveBeltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No1GRAbrasiveBeltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAbrasiveBeltMotorCurrentCT(L2L25_No2GRAbrasiveBeltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No2GRAbrasiveBeltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No3GRAbrasiveBeltMotorCurrentCT(L2L25_No3GRAbrasiveBeltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No3GRAbrasiveBeltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No4GRAbrasiveBeltMotorCurrentCT(L2L25_No4GRAbrasiveBeltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No4GRAbrasiveBeltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No5GRAbrasiveBeltMotorCurrentCT(L2L25_No5GRAbrasiveBeltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No5GRAbrasiveBeltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No6GRAbrasiveBeltMotorCurrentCT(L2L25_No6GRAbrasiveBeltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No6GRAbrasiveBeltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAbrasiveBeltSpeedCT(L2L25_No1GRAbrasiveBeltSpeedCT entity)
        {
            try
            {
                return _L2L25_No1GRAbrasiveBeltSpeedCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAbrasiveBeltSpeedCT(L2L25_No2GRAbrasiveBeltSpeedCT entity)
        {
            try
            {
                return _L2L25_No2GRAbrasiveBeltSpeedCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1BrushRollCurrentCT(L2L25_No1BrushRollCurrentCT entity)
        {
            try
            {
                return _L2L25_No1BrushRollCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2BrushRollCurrentCT(L2L25_No2BrushRollCurrentCT entity)
        {
            try
            {
                return _L2L25_No2BrushRollCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25CurrentPassNumberCT(L2L25_CurrentPassNumberCT entity)
        {
            try
            {
                return _L2L25_CurrentPassNumberCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25CurrentSessionCT(L2L25_CurrentSessionCT entity)
        {
            try
            {
                return _L2L25_CurrentSessionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25GRDLineSpeedCT(L2L25_GRDLineSpeedCT entity)
        {
            try
            {
                return _L2L25_GRDLineSpeedCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAB_beltKindCT(L2L25_No1GRAB_beltKindCT entity)
        {
            try
            {
                return _L2L25_No1GRAB_beltKindCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAB_beltRoughnessCT(L2L25_No1GRAB_beltRoughnessCT entity)
        {
            try
            {
                return _L2L25_No1GRAB_beltRoughnessCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAB_beltMotorCurrentCT(L2L25_No1GRAB_beltMotorCurrentCT entity)
        {
            try
            {
                return _L2L25_No1GRAB_beltMotorCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAB_beltSpeedCT(L2L25_No1GRAB_beltSpeedCT entity)
        {
            try
            {
                return _L2L25_No1GRAB_beltSpeedCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No1GRAB_beltRotateDirectionCT(L2L25_No1GRAB_beltRotateDirectionCT entity)
        {
            try
            {
                return _L2L25_No1GRAB_beltRotateDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAB_beltKindCT(L2L25_No2GRAB_beltKindCT entity)
        {
            try
            {
                return _L2L25_No2GRAB_beltKindCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAB_beltRoughnessCT(L2L25_No2GRAB_beltRoughnessCT entity)
        {
            try
            {
                return _L2L25_No2GRAB_beltRoughnessCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAB_beltCurrentCT(L2L25_No2GRAB_beltCurrentCT entity)
        {
            try
            {
                return _L2L25_No2GRAB_beltCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAB_beltSpeedCT(L2L25_No2GRAB_beltSpeedCT entity)
        {
            try
            {
                return _L2L25_No2GRAB_beltSpeedCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No2GRAB_beltRotateDirectionCT(L2L25_No2GRAB_beltRotateDirectionCT entity)
        {
            try
            {
                return _L2L25_No2GRAB_beltRotateDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No3GRAB_beltKindCT(L2L25_No3GRAB_beltKindCT entity)
        {
            try
            {
                return _L2L25_No3GRAB_beltKindCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No3GRAB_beltRoughnessCT(L2L25_No3GRAB_beltRoughnessCT entity)
        {
            try
            {
                return _L2L25_No3GRAB_beltRoughnessCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No3GRAB_beltCurrentCT(L2L25_No3GRAB_beltCurrentCT entity)
        {
            try
            {
                return _L2L25_No3GRAB_beltCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No3GRAB_beltRotateDirectionCT(L2L25_No3GRAB_beltRotateDirectionCT entity)
        {
            try
            {
                return _L2L25_No3GRAB_beltRotateDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No4GRAB_beltKindCT(L2L25_No4GRAB_beltKindCT entity)
        {
            try
            {
                return _L2L25_No4GRAB_beltKindCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No4GRAB_beltRoughnessCT(L2L25_No4GRAB_beltRoughnessCT entity)
        {
            try
            {
                return _L2L25_No4GRAB_beltRoughnessCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No4GRAB_beltCurrentCT(L2L25_No4GRAB_beltCurrentCT entity)
        {
            try
            {
                return _L2L25_No4GRAB_beltCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No4GRAB_beltRotateDirectionCT(L2L25_No4GRAB_beltRotateDirectionCT entity)
        {
            try
            {
                return _L2L25_No4GRAB_beltRotateDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No5GRAB_beltKindCT(L2L25_No5GRAB_beltKindCT entity)
        {
            try
            {
                return _L2L25_No5GRAB_beltKindCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No5GRAB_beltRoughnessCT(L2L25_No5GRAB_beltRoughnessCT entity)
        {
            try
            {
                return _L2L25_No5GRAB_beltRoughnessCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No5GRAB_beltCurrentCT(L2L25_No5GRAB_beltCurrentCT entity)
        {
            try
            {
                return _L2L25_No5GRAB_beltCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No5GRAB_beltRotateDirectionCT(L2L25_No5GRAB_beltRotateDirectionCT entity)
        {
            try
            {
                return _L2L25_No5GRAB_beltRotateDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No6GRAB_beltKindCT(L2L25_No6GRAB_beltKindCT entity)
        {
            try
            {
                return _L2L25_No6GRAB_beltKindCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No6GRAB_beltRoughnessCT(L2L25_No6GRAB_beltRoughnessCT entity)
        {
            try
            {
                return _L2L25_No6GRAB_beltRoughnessCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No6GRAB_beltCurrentCT(L2L25_No6GRAB_beltCurrentCT entity)
        {
            try
            {
                return _L2L25_No6GRAB_beltCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25No6GRAB_beltRotateDirectionCT(L2L25_No6GRAB_beltRotateDirectionCT entity)
        {
            try
            {
                return _L2L25_No6GRAB_beltRotateDirectionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
        public int Create25CoilRejectResult(L2L25_CoilRejectResult entity)
        {
            try
            {
                return _L2L25_CoilRejectResultRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
