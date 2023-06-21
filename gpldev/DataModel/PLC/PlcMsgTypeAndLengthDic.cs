using MsgStruct;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DataMod.MsgStruct.PLC
{
    public class PlcMsgTypeAndLengthDic
    {

        public readonly Dictionary<short, int> PlcMsgLen = new Dictionary<short, int> {
            { 101, Marshal.SizeOf<L1L2Rcv.Msg_101_Alive>() },
            { 102, Marshal.SizeOf<L1L2Rcv.Msg_102_PDO>() },
            { 104, Marshal.SizeOf<L1L2Rcv.Msg_104_ProData>() },
            { 105, Marshal.SizeOf<L1L2Rcv.Msg_105_Trk_Map>() },
            { 106, Marshal.SizeOf<L1L2Rcv.Msg_106_Weld_Data>() },
            { 107, Marshal.SizeOf<L1L2Rcv.Msg_107_Grd_Rpt>() },
            { 108, Marshal.SizeOf<L1L2Rcv.Msg_108_Defect_Data>() },
            { 109, Marshal.SizeOf<L1L2Rcv.Msg_109_Belt_ACC_Length>() },
            { 110, Marshal.SizeOf<L1L2Rcv.Msg_110_Coil_Weight>() },
            { 111, Marshal.SizeOf<L1L2Rcv.Msg_111_LineFault>() },
            { 112, Marshal.SizeOf<L1L2Rcv.Msg_112_Utility>() },
            { 113, Marshal.SizeOf<L1L2Rcv.Msg_113_Belt_Change>() },
            { 114, Marshal.SizeOf<L1L2Rcv.Msg_114_Coil_Mount>() },
            { 115, Marshal.SizeOf<L1L2Rcv.Msg_115_Coil_Unmount>() },
            { 116, Marshal.SizeOf<L1L2Rcv.Msg_116_Coil_Weight>() },
            { 117, Marshal.SizeOf<L1L2Rcv.Msg_117_Split>() },
            { 118, Marshal.SizeOf<L1L2Rcv.Msg_118_Entry_Start_Condition>() },
            { 119, Marshal.SizeOf<L1L2Rcv.Msg_119_Entry_Take_Over_Start>() },
            { 120, Marshal.SizeOf<L1L2Rcv.Msg_120_Entry_Take_Over_End>() },
            { 121, Marshal.SizeOf<L1L2Rcv.Msg_121_Delivery_Start_Condition>() },
            { 122, Marshal.SizeOf<L1L2Rcv.Msg_122_Delivery_Take_Over_Start>() },
            { 123, Marshal.SizeOf<L1L2Rcv.Msg_123_Delivery_Take_Over_End>() },
            { 124, Marshal.SizeOf<L1L2Rcv.Msg_124_StripBrakeSignal>() },
            { 125, Marshal.SizeOf<L1L2Rcv.Msg_125_Share_Cut_Data>() },
            { 126, Marshal.SizeOf<L1L2Rcv.Msg_126_Coil_Unmount_POR>() },
            { 127, Marshal.SizeOf<L1L2Rcv.Msg_127_Coil_ID_Modify_Reply>() },
        };




    }
}
