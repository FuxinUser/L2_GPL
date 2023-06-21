using Core.Define;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DataMod.MMS
{
    public class MMSTypeAndLengthDic
    {

        // +1 結尾符號長度 1byte (0x0A)
        public readonly Dictionary<string, int> MMSMsgLen = new Dictionary<string, int> {
            // MM->L2
            { MMSSysDef.RcvMsgCode.CoilDummyCode, Marshal.SizeOf<MMSL2Rcv.Msg_Dummy_Coil_List>()+1 },
            { MMSSysDef.RcvMsgCode.CoilScheduleCode, Marshal.SizeOf<MMSL2Rcv.Msg_Coil_Schedule>()+1 },
            { MMSSysDef.RcvMsgCode.CoilPDICode,  Marshal.SizeOf<MMSL2Rcv.Msg_PDI>()+1 },
            { MMSSysDef.RcvMsgCode.ReqProResultCode,  Marshal.SizeOf<MMSL2Rcv.Msg_Product_Result_Request>()+1 },
            { MMSSysDef.RcvMsgCode.CoilRejectResultCode,  Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_Coil_Reject_Result>()+1 },
            { MMSSysDef.RcvMsgCode.ReqDeletePlanNoCode, Marshal.SizeOf<MMSL2Rcv.Msg_Req_Delete_Schedule_Plan>()+1 },
            { MMSSysDef.RcvMsgCode.ResForNoCoilCode, Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_No_Coil_Schedule>()+1 },
            { MMSSysDef.RcvMsgCode.ResForNoCoilPDICode, Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_No_Coil_PDI>()+1 },
            { MMSSysDef.RcvMsgCode.SleeveValueSynCode, Marshal.SizeOf<MMSL2Rcv.Msg_Sleeve_Value_Synchronize>()+1 },
            { MMSSysDef.RcvMsgCode.PaperValueSyncCode, Marshal.SizeOf<MMSL2Rcv.Msg_Paper_Value_Synchronize>()+1 },
             { MMSSysDef.RcvMsgCode.CoilDummyResCode, Marshal.SizeOf<MMSL2Rcv.Msg_Res_of_Dummy_Coil_List_Req>()+1 },
            { MMSSysDef.RcvMsgCode.HeartBeatCode, Marshal.SizeOf<MMSL2Rcv.Msg_HeartBeat>()+1 },
            { MMSSysDef.RcvMsgCode.ResRcvPDO, Marshal.SizeOf<MMSL2Rcv.Msg_Res_RcvPDO>()+1 },
        };


        public readonly Dictionary<string, Type> MMSMsgType = new Dictionary<string, Type> {

            // MM->L2
            { MMSSysDef.RcvMsgCode.CoilDummyCode, typeof(MMSL2Rcv.Msg_Dummy_Coil_List)},
            { MMSSysDef.RcvMsgCode.CoilScheduleCode, typeof(MMSL2Rcv.Msg_Coil_Schedule) },
            { MMSSysDef.RcvMsgCode.CoilPDICode,  typeof(MMSL2Rcv.Msg_PDI) },
            { MMSSysDef.RcvMsgCode.ReqProResultCode,  typeof(MMSL2Rcv.Msg_Product_Result_Request) },
            { MMSSysDef.RcvMsgCode.CoilRejectResultCode,  typeof(MMSL2Rcv.Msg_Res_For_Coil_Reject_Result) },
            { MMSSysDef.RcvMsgCode.ReqDeletePlanNoCode, typeof(MMSL2Rcv.Msg_Req_Delete_Schedule_Plan) },
            { MMSSysDef.RcvMsgCode.ResForNoCoilCode, typeof(MMSL2Rcv.Msg_Res_For_No_Coil_Schedule) },
            { MMSSysDef.RcvMsgCode.ResForNoCoilPDICode, typeof(MMSL2Rcv.Msg_Res_For_No_Coil_PDI) },
            { MMSSysDef.RcvMsgCode.SleeveValueSynCode, typeof(MMSL2Rcv.Msg_Sleeve_Value_Synchronize) },
            { MMSSysDef.RcvMsgCode.PaperValueSyncCode, typeof(MMSL2Rcv.Msg_Paper_Value_Synchronize) },
             { MMSSysDef.RcvMsgCode.CoilDummyResCode, typeof(MMSL2Rcv.Msg_Res_of_Dummy_Coil_List_Req) },
            { MMSSysDef.RcvMsgCode.HeartBeatCode, typeof(MMSL2Rcv.Msg_HeartBeat) },
            { MMSSysDef.RcvMsgCode.ResRcvPDO, typeof(MMSL2Rcv.Msg_Res_RcvPDO) },

            //L2->MM
            { MMSSysDef.SndMsgCode.ReqForCoilSchedCode, typeof(L2MMSSnd.Msg_Req_Coil_Schedule) },
            { MMSSysDef.SndMsgCode.ReqForPDICode, typeof(L2MMSSnd.Msg_Request_Coil_PDI) },
            { MMSSysDef.SndMsgCode.ResForCoilSchedCode, typeof(L2MMSSnd.Msg_Res_For_Coil_Schedule) },
            { MMSSysDef.SndMsgCode.ResForCoilPDICode, typeof(L2MMSSnd.Msg_Res_For_Coil_PDI) },
            { MMSSysDef.SndMsgCode.CoilRejectDataCode, typeof(L2MMSSnd.Msg_Coil_Reject_Result) },
            { MMSSysDef.SndMsgCode.CoilLoadedSkidCode, typeof(L2MMSSnd.Msg_Coil_Loaded_Skid) },
            { MMSSysDef.SndMsgCode.CoilPDOCode, typeof(L2MMSSnd.Msg_PDO) },
            { MMSSysDef.SndMsgCode.EqDownResultCode, typeof(L2MMSSnd.Msg_Equipment_Down_Result_Msg) },
            { MMSSysDef.SndMsgCode.EnergyConsumptionInfoCode, typeof(L2MMSSnd.Msg_Energy_Consumption_Info) },
            { MMSSysDef.SndMsgCode.CoilScheduleChangedCode, typeof(L2MMSSnd.Msg_Coil_Schedule_Changed) },
            { MMSSysDef.SndMsgCode.ReqDummyCoilCode, typeof(L2MMSSnd.Msg_Dummy_Coil_List_Result_Request) },
            { MMSSysDef.SndMsgCode.DelDummyCoilCode, typeof(L2MMSSnd.Msg_Dummy_Coil_List_Delete) },
            { MMSSysDef.SndMsgCode.CoilScheduleDeleteCode, typeof(L2MMSSnd.Msg_Coil_Schedule_Delete) },
            { MMSSysDef.SndMsgCode.ResDeletePlanNoResultCode, typeof(L2MMSSnd.Msg_Res_For_PlanNo_Delete) },

        };

    }
}
