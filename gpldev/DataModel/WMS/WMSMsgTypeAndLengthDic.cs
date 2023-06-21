using Core.Define;
using DataMod.WMS.MsgModel;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DataMod.WMS
{
    public class WMSMsgTypeAndLengthDic
    {
        public readonly Dictionary<string, Type> WMSMsgType = new Dictionary<string, Type>
        {
            { WMSSysDef.SndMsgCode.CoilScheduleInfo, typeof(L2_WMS_Snd.GWx1_ScheduleList) },
            { WMSSysDef.SndMsgCode.EntryDeliveryTrk, typeof(L2_WMS_Snd.GWx2_TrackingMap) },
            { WMSSysDef.SndMsgCode.CoilPDO, typeof(L2_WMS_Snd.GWx3_CoilInfo) },
            { WMSSysDef.SndMsgCode.ProdLineCoilReq, typeof(L2_WMS_Snd.GWx5_FeedingRequest_EntryExitReturn) },
            { WMSSysDef.RcvMsgCode.ProDone, typeof(WMS_L2_Rcv.WGx1_CompleteOfFeeding) },
            { WMSSysDef.RcvMsgCode.ProResRequest, typeof(WMS_L2_Rcv.WGx3_RequestResponse) },
        };

        public readonly Dictionary<string, int> WMSMsgLen = new Dictionary<string, int> {
            { WMSSysDef.SndMsgCode.CoilScheduleInfo, Marshal.SizeOf<L2_WMS_Snd.GWx1_ScheduleList>() },
            { WMSSysDef.SndMsgCode.EntryDeliveryTrk, Marshal.SizeOf<L2_WMS_Snd.GWx2_TrackingMap>() },
            { WMSSysDef.SndMsgCode.CoilPDO,  Marshal.SizeOf<L2_WMS_Snd.GWx3_CoilInfo>() },
                 { WMSSysDef.SndMsgCode.HeartBeatCode, Marshal.SizeOf<WMS_Heart_Structure>() },
            { WMSSysDef.SndMsgCode.ProdLineCoilReq,  Marshal.SizeOf<L2_WMS_Snd.GWx5_FeedingRequest_EntryExitReturn>() },
            { WMSSysDef.RcvMsgCode.ProDone,  Marshal.SizeOf<WMS_L2_Rcv.WGx1_CompleteOfFeeding>() },
            { WMSSysDef.RcvMsgCode.ProResRequest, Marshal.SizeOf<WMS_L2_Rcv.WGx3_RequestResponse>() },
            };

    }
}
