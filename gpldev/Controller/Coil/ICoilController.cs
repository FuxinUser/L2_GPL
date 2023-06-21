using DataMod.PLC;
using DBService.Repository;
using DBService.Repository.Belt;
using DBService.Repository.BeltPatterns;
using DBService.Repository.GrindPlan;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.ReturnCoil;
using LogSender;
using MsgStruct;
using System;
using System.Collections.Generic;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.PDI.PDIEntity;
using static DBService.Repository.LookupTblSleeve.LkUpTableSleeveEntity;
/**
* Author: ICSC Spyua
* Date: 2020/1/1
* Desc: 鋼捲生產處理API介面
*/

namespace Controller.Coil
{
    public interface ICoilController
    {

        void SetLog(ILog log);

        #region -- PDI相關 --

        bool VaildHasPDI(string coilNo);
        bool VaildHasPDIandPlanNo(string coilNo, string planNo);
        bool VaildHasDummy(string coilNo);
        string GetPDIPlanNo(string entryCoilNo);

        int UpdatePDI(string coilNo, MMSL2Rcv.Msg_PDI mmsPDI);
        void UpdateDummyPDI(string coilNo, PDOEntity.TBL_PDO pdo);
        bool DeleteDummy(string coilNo);
        int CreatePDI(MMSL2Rcv.Msg_PDI mmsPDI);

        void UpdatePDIScrapedWeight(float coilWeight, string entryCoilID);

        // 更新PDI Entry Coil Check旗標
        bool UpdateEntryScanCoilInfo(string scacCoilID, bool entryCoilIDChecked);

        void UpdatePDIStarTime(string coilIDNo);

        void UpdatePDIEndTime(string entryCoil);

        TBL_PDI GetPDI(string entryCoilNo);
        TBL_PDI GetPDIOnly(string PlanNo,string entryCoilNo);
        void CreateDummyCoil(MMSL2Rcv.Msg_Dummy_Coil_List dummyCoil);

        void UpdateIsInfoWMSDown(bool infoDone, string coilIDNo);

        bool CreateL25PDI(TBL_PDI dao);

        #endregion

        #region-- PDO 相關 --

        bool UpdatePDOExCoilIDChecked(string exitCoilNo, string exitCoilIDChecked);

        PDOEntity.TBL_PDO GetPDO(string outMatNo);

        PDOEntity.TBL_PDO GetPDOonly(string planNo, string out_coil_id);

        bool GenEmptyPDO(PDIEntity.TBL_PDI pdi);
        bool GenPDO(L1L2Rcv.Msg_124_StripBrakeSignal msg, PDIEntity.TBL_PDI pdi);

        PDOEntity.TBL_PDO CalculatePDOResult(L1L2Rcv.Msg_102_PDO msg);

        bool InvaildHasPDO(string outCoilNo);

        bool UpdatePDO(PDOEntity.TBL_PDO pdo);

        bool CreatePDO(PDOEntity.TBL_PDO pdo);

        bool CreatePdoUploadedReply(MMSL2Rcv.Msg_Res_RcvPDO mmsResPDO);

        void UpdatePDOWeight(string outCoilID, float coilWt);

        void UpdatePDOFinishTime(string coilNo, DateTime finishTime);

        void UpdateUploadFlag(string planNo,string coilNo, bool upload);

        bool CreateL25PDO(string planNo, string outCoilID);

        
        #endregion

        #region -- 排程相關 -- 

        bool VaildHasCoilSchedule(string coilID);
        bool VaildHasScheduleTemp(string coilID);
        List<string> QueryCoilScheduleIDs(int num);

        List<string> QueryUnscheduleCoils(int num);

        List<string> QueryScheduleRequestCoils(int num);

        IEnumerable<TBL_PDI> QueryCoilScheduleByPlanNo(string planNo);

        int CreateCoilSchedule(string coilID, short SeqN0);
        bool UpdateScheduleStatuts(string coilNo, string statuts);

        bool RemoveScheduleCoilIDs(string coilID);


        string GetFirstCoilSchedule();

        void DeleteAllCoilSchedule();

        void DeleteAllIdleSchedule();
        /// <summary>
        /// 將MMS下發鋼捲生產命令鋼捲順序存至DB
        /// </summary>
        bool SequenceCreateSchedule(string coilCluster, int coilNum);

        bool DeleteCoilSchedule(string coilID);
        bool DeleteCoilScheduleOnly(string coilID);
        bool CreateTempCoilScheduleDelRecord(string coilID, string operatorID="", string reasonCode="");

        bool DelCoilScheduleDelTempRecord(string coilID);
        bool DelCoilRejectTempRecord(string coilID, string planNo);
        bool CreateCoilScheduleDelRecords(string coilID, string operatorId = "", string reasonCode = "", string remarks = "");
        #endregion


        #region --鋼捲回退--
        bool VaildHasCoilRejectTemp(string coilID, string planNo);
        #endregion

        #region --Tracking--
        bool VaildHasCoilMap(string coilID, short skidPos);
        #endregion

        #region -- LookUp Table --

        LkUpTableModel.Preset204 GetPreset204LkTableData(PDIEntity.TBL_PDI pdi);
        #endregion

        #region -- Weld Record || Weld Belt相關 || Defect Data相關--
        bool UpdateBeltAccLength(int mountGrNo, float beltLength, float stLength);

        bool UpdateAccLengthByBeltNo(string beltNo, float beltLength, float stLength);

        bool UpdateGrNoByBeltNo(string beltNo, int grNo);

        bool CreateDefectData(L1L2Rcv.Msg_108_Defect_Data msg);

        bool UpdateDefectData(L1L2Rcv.Msg_108_Defect_Data msg, float starLength);

        BeltAccEntity.TBL_Belts GetBelt(string beltNo);

        L3L2_TBL_DefectData GetPreDefectDataByPlcMsg(L1L2Rcv.Msg_108_Defect_Data msg);

        float GetDefectRecordLastLength();

        IEnumerable<BeltPatternsEntity.TBL_BeltPatterns> QueryBeltPatterns(string coilNo);

        IEnumerable<GrindPlanEntity.TBL_GrindPlan> QueryBeltPlans(string coilNo);

        void CreateGrindPlanHistory(string coilID, string planNo,IEnumerable<GrindPlanEntity.TBL_GrindPlan> beltPlans);

        IEnumerable<L3L2_TBL_DefectData> QueryDefectData(string coilNo, int count);


        #endregion

        #region -- 鋼卷分切 --
        string GetSplitParentCoilNo(string childCoilID);
        string GenSplitChildrenCoilNo(string parentCoilID);
        int GetCntSplitRec(string ParentCoilID);
        bool VaildNewChildCoilNoData(string childCoil, string parentCoil);
        void UpdateHasScrapedFlag(bool hasScraped, string entryCoilID);
        #endregion

        #region -- 套筒墊紙同步處理 --

        bool SyncSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize msg);
        bool SyncPaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize msg);

        #endregion

        #region Preset Record 204,205
        bool CreatePreset204Record(L2L1Snd.Msg_204_PDI_TM3 msg);
        bool CreatePreset205Record(L2L1Snd.Msg_205_PDI_TM3_2 msg);
        #endregion
        ReturnCoilEntity.TBL_RetrunCoil_Temp GetCoilRejctTemp(string coilNo,string planNo);
        CoilRejResultEntity.TBL_CoilRejectResult GetCoilRejectResult(string coilNo);
        bool SyncCoilRejectData(string rejectCoilNo,string plan);

        void UpdateExitCoilGrossWeight(string exitCoilNo, float CoilWeight);

        TBL_LookupTable_Sleeve GetSleeveData(string code);

    }
}
