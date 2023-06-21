using DataMod.Common;
using Core.Define;
using DBService.L1Repository;
using LogSender;
using MsgStruct;
using System;
using System.Collections.Generic;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.WorkSchedule.WorkScheduleEntity;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.GrindRecords.GrindRecordsEntity;

namespace Controller.DtGtr
{
    public interface IDataGatheringController
    {
        void SetLog(ILog log);

        void CreateCoilWeld(L1L2Rcv.Msg_106_Weld_Data msg);

        void CreateGrdRpt(L1L2Rcv.Msg_107_Grd_Rpt msg);

        void CreateProData(L1L2Rcv.Msg_104_ProData msg);

        void CreateUtility(L1L2Rcv.Msg_112_Utility msg, string shift, string team);

        void CreateUmountRecord(L1L2Rcv.Msg_124_StripBrakeSignal msg);
        
        void CreateStripBrakeSignal(L1L2Rcv.Msg_124_StripBrakeSignal msg);

        //void SaveStopLineFaultStart(L1L2Rcv.Msg_111_LineFault msg, int nowShift, string nowDate);

        TBL_WorkSchedule GetScheduleByTime(DateTime time);

        bool CreateStopLineFaultStart(L1L2Rcv.Msg_111_LineFault msg, string team, int shift);

        bool UpdateStopLineFaultEnd(L1L2Rcv.Msg_111_LineFault msg);
        CheckCrossShiftModel GetLastLineFaultUnfinshRecord();

        bool UpdateStopLineFaultEnd(CheckCrossShiftModel shiftInfo);
        bool AutoUpdateShiftRecord(int shiftNo,string team);
        bool SaveStopLineFaultStart(CheckCrossShiftModel shiftInfo, int nowShift, string nowDate);
        void CreateCutRecord(L1L2Rcv.Msg_125_Share_Cut_Data msg);

        void CreateUmountRecord(L1L2Rcv.Msg_126_Coil_Unmount_POR msg);

        IEnumerable<L2L1MsgDBModel.L2L1_204> QueryAll204HisMsg();

        IEnumerable<L2L1MsgDBModel.L2L1_205> QueryAll205HisMsg();

        L2L1MsgDBModel.L2L1_204 Get204HisDataByTime(string createTime);

        L2L1MsgDBModel.L2L1_205 Get205HisDataByTime(string createTime);

        TBL_LineFaultRecords GetLineFaultRecord(string prodTime, string stopStartTime,string stopEndTime);

        bool UpdateLineFaultUploadFlag(DateTime prodTime, DateTime stopStartTime, DateTime stopEndTime, bool uploadDone);

        bool CreateL25Alive();

        bool CreateL25Engc(L1L2Rcv.Msg_112_Utility msg);
        bool CreateL25DownTime(TBL_LineFaultRecords dao);

        //TBL_ProcessData QueryProcessDatas(DateTime starTime, DateTime endTime);

        IEnumerable<TBL_ProcessData> QueryProcessDatas(DateTime starTime, DateTime endTime);

        IEnumerable<TBL_GrindRecords> QueryGrindDatas(string CoilID, string PlanNo,int PassNumber ,int Session);

        IEnumerable<TBL_GrindRecords> QueryGrindDatas_Total(string CoilID, string PlanNo);
        ProcessModel CalculateProcessData(IEnumerable<TBL_ProcessData> datas);

        GrindDataModel CalculateGrindData(IEnumerable<TBL_GrindRecords> datas);

        bool Create25ProcessCTData(TBL_PDO pdo, ProcessModel datas);
        bool Create25GrindData(TBL_PDO pdo, GrindDataModel datas,int CurrentPassNumber, int CurrentSession);
    }
}
