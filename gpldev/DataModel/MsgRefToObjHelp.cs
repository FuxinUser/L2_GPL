using Core.Define;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Help
{
    public class MsgRefToObjHelp
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly MsgRefToObjHelp INSTANCE = new MsgRefToObjHelp();
        }
        public static MsgRefToObjHelp Instance { get { return SingletonHolder.INSTANCE; } }


        public Type GetPlcStructClassType(string messageID)
        {
            Type t = null;

            switch (messageID)
            {
                case PlcSysDef.RcvMsgCode.L1101Alive:
                    t = typeof(L1L2Rcv.Msg_101_Alive);
                    break;
                case PlcSysDef.RcvMsgCode.L1102PDOInfo:
                    t = typeof(L1L2Rcv.Msg_102_PDO);
                    break;
                case PlcSysDef.RcvMsgCode.L1104ProcessData:
                    t = typeof(L1L2Rcv.Msg_104_ProData);
                    break;
                case PlcSysDef.RcvMsgCode.L1105TrackMap:
                    t = typeof(L1L2Rcv.Msg_105_Trk_Map);
                    break;
                case PlcSysDef.RcvMsgCode.L1106CoilWeldData:
                    t = typeof(L1L2Rcv.Msg_106_Weld_Data);
                    break;
                case PlcSysDef.RcvMsgCode.L1107GrindRecords:
                    t = typeof(L1L2Rcv.Msg_107_Grd_Rpt);
                    break;
                case PlcSysDef.RcvMsgCode.L1108DefectData:
                    t = typeof(L1L2Rcv.Msg_108_Defect_Data);
                    break;
                case PlcSysDef.RcvMsgCode.L1109BeltAccLength:
                    t = typeof(L1L2Rcv.Msg_109_Belt_ACC_Length);
                    break;
                case PlcSysDef.RcvMsgCode.L1110UpdateWt:
                    t = typeof(L1L2Rcv.Msg_110_Coil_Weight);
                    break;
                case PlcSysDef.RcvMsgCode.L1111LineFault:
                    t = typeof(L1L2Rcv.Msg_111_LineFault);
                    break;
                case PlcSysDef.RcvMsgCode.L1112Utility:
                    t = typeof(L1L2Rcv.Msg_112_Utility);
                    break;
                case PlcSysDef.RcvMsgCode.L1113BeltChange:
                    t = typeof(L1L2Rcv.Msg_113_Belt_Change);
                    break;
                case PlcSysDef.RcvMsgCode.L1114CoilMount:
                    t = typeof(L1L2Rcv.Msg_114_Coil_Mount);
                    break;
                case PlcSysDef.RcvMsgCode.L1115CoilUnMount:
                    t = typeof(L1L2Rcv.Msg_115_Coil_Unmount);
                    break;
                case PlcSysDef.RcvMsgCode.L1116CoilWeight:
                    t = typeof(L1L2Rcv.Msg_116_Coil_Weight);
                    break;
                case PlcSysDef.RcvMsgCode.L1117CoilSplit:
                    t = typeof(L1L2Rcv.Msg_117_Split);
                    break;
                case PlcSysDef.RcvMsgCode.L1118EntryStartCondition:
                    t = typeof(L1L2Rcv.Msg_118_Entry_Start_Condition);
                    break;
                case PlcSysDef.RcvMsgCode.L1119EntryTakeOverStart:
                    t = typeof(L1L2Rcv.Msg_119_Entry_Take_Over_Start);
                    break;
                case PlcSysDef.RcvMsgCode.L1120EntryTakeOverEndEntry:
                    t = typeof(L1L2Rcv.Msg_120_Entry_Take_Over_End);
                    break;
                case PlcSysDef.RcvMsgCode.L1121DeliveryStartCondition:
                    t = typeof(L1L2Rcv.Msg_121_Delivery_Start_Condition);
                    break;
                case PlcSysDef.RcvMsgCode.L1122DeliveryTakeOverStartDelivery:
                    t = typeof(L1L2Rcv.Msg_122_Delivery_Take_Over_Start);
                    break;
                case PlcSysDef.RcvMsgCode.L1123DeliveryTakeOverEnd:
                    t = typeof(L1L2Rcv.Msg_123_Delivery_Take_Over_End);
                    break;
                case PlcSysDef.RcvMsgCode.L1124StripBrakeSignal:
                    t = typeof(L1L2Rcv.Msg_124_StripBrakeSignal);
                    break;
                case PlcSysDef.RcvMsgCode.L1125ShareCutData:
                    t = typeof(L1L2Rcv.Msg_125_Share_Cut_Data);
                    break;
                case PlcSysDef.RcvMsgCode.L1126CoilUmountPOR:
                    t = typeof(L1L2Rcv.Msg_126_Coil_Unmount_POR);
                    break;
                case PlcSysDef.RcvMsgCode.L1127CoilIDModifyReply:
                    t = typeof(L1L2Rcv.Msg_127_Coil_ID_Modify_Reply);
                    break;


                // 發送測試 使用
                case PlcSysDef.SndMsgCode.L1202PDI:
                    t = typeof(L2L1Snd.Msg_202_PDI_TM1);
                    break;
                case PlcSysDef.SndMsgCode.L1203EntryScnResult:
                    t = typeof(L2L1Snd.Msg_203_PDI_TM2);
                    break;
                case PlcSysDef.SndMsgCode.L1204Preset:
                    t = typeof(L2L1Snd.Msg_204_PDI_TM3);
                    break;
                case PlcSysDef.SndMsgCode.L1205Preset:
                    t = typeof(L2L1Snd.Msg_205_PDI_TM3_2);
                    break;
                case PlcSysDef.SndMsgCode.L1206BeltInfoMsg:
                    t = typeof(L2L1Snd.Msg_206_Belt_Info);
                    break;
                case PlcSysDef.SndMsgCode.L1207EntryTakeOverStartCMD:
                    t = typeof(L2L1Snd.Msg_207_Entry_Take_Over_Start);
                    break;
                case PlcSysDef.SndMsgCode.L1208DelveryToCMD:
                    t = typeof(L2L1Snd.Msg_208_Delivery_Take_Over_Start_CM);
                    break;
                case PlcSysDef.SndMsgCode.L1209DeliveryScnResult:
                    t = typeof(L2L1Snd.Msg_209_Delivery_BC_Confirm);
                    break;
                case PlcSysDef.SndMsgCode.L1210DelCoil:
                    t = typeof(L2L1Snd.Msg_210_Del_CoilID);
                    break;
                case PlcSysDef.SndMsgCode.L1211ModifyCoilID:
                    t = typeof(L2L1Snd.Msg_211_Modify_Coil_ID);
                    break;

            }


            return t;
        }

    }
}
