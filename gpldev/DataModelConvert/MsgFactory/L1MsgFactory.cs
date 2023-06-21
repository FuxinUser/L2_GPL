using Core.Define;
using Core.Util;
using DataMod.Common;
using DataMod.PLC;
using DBService.Repository.Belt;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using MsgStruct;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using static DataMod.Common.ModifyCoilModel;

namespace MsgConvert
{
    public static class L1MsgFactory
    {

        public static L2L1Snd.Msg_201_Alive L2Alive()
        {
            var msg = new L2L1Snd.Msg_201_Alive()
            {
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1201Alive),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_201_Alive>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
            };
            return msg;
        }

        public static L2L1Snd.Msg_204_PDI_TM3 ConvertToPreset204Msg(this PDIEntity.TBL_PDI pdi, int pos, LkUpTableModel.Preset204 lkTableData)
        {

            if (pdi == null)
                return new L2L1Snd.Msg_204_PDI_TM3();

            var msg = new L2L1Snd.Msg_204_PDI_TM3();


            #region Header
            msg.MessageId = short.Parse(PlcSysDef.SndMsgCode.L1204Preset);
            msg.MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_204_PDI_TM3>();
            msg.Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            msg.Time = Int32.Parse(DateTime.Now.ToString("HHmmss"));
            #endregion
            //
            msg.PresetPosition = pos.ToShort();

            // 作業計劃號
            msg.TestPlanNo = pdi.Test_Plan_No.ToCByteArray(52);
            // 入口鋼捲號
            msg.CoilId = pdi.In_Coil_ID.ToCByteArray(24);
            // 密度
            msg.Density = pdi.Density;
            // 鋼種
            msg.SteelGrade = pdi.St_No.ToCByteArray(12);
            // 入口鋼捲長度
            msg.CoilLength = pdi.In_Coil_Length.ToShort();
            // 入口鋼捲厚度
            msg.CoilThickness = (pdi.In_Coil_Thick * 1000).ToShort();
            // 入口鋼捲寬度
            msg.CoilWidth = pdi.In_Coil_Width.ToShort();
            // 鋼卷內徑
            msg.CoilInnerDiameter = pdi.In_Coil_Inner_Diameter.ToShort();
            // 鋼卷外徑
            msg.CoilOuterDiameter = pdi.In_Coil_Outer_Diameter.ToShort();
            //是否使用套筒(0:N, 1:Y)                                       
            msg.SleeveInstallledCoil = IsSleeveInstallledCoil(pdi.In_Sleeve_Type_Code);
            // 鋼捲內徑
            msg.SleeveOuterDiameter = pdi.In_Sleeve_Type_Code.IsEmpty() ? (short)0 : pdi.In_Sleeve_Diameter.ToShort();
            //是否使用襯紙(0:N, 1:Y)                                                           
            msg.PaperInstalledCoil = IsPaperInstalledCoil(pdi.In_Paper_Req_Code);
            // 入口墊紙方式 GPL 1:全卷
            msg.SleevePaperCode = 1;
            // 頭部未軋製區域
            msg.OffGaugeHead = pdi.Head_Off_Gauge.ToShort();
            // 尾部未軋製區域
            msg.OffGaugeTail = pdi.Tail_Off_Gauge.ToShort();
            // 查表 TBL_LookupTable_Flattener
            msg.FlattenerIntermesh1 = lkTableData.FlatenerDepth1.ToShort();
            msg.FlattenerIntermesh2 = lkTableData.FlatenerDepth2.ToShort();
            // 脫脂標記
            msg.DegreasingFlag = DegreasingFlag(pdi.Skim_Flag);
            // 開捲方向 0:Type A. CCW(下開)   1:Type B. CW (上開)
            msg.IncomingCoilWoundDirection = DetIncomingCoilWoundDirection(pdi.Uncoil_Direction);
            // 加工面 0:Inside.  1:Outside , 2:DUMMY
            if (pdi.Order_No.Trim().Equals("DUMMY"))
            {
                msg.ProcessingSurface = 2;
            }
            else
            { 
                msg.ProcessingSurface = DetProcessingSurface(pdi.Appoint_Grinding_Surface);
            }      
            // 鋼卷是否須轉向-目前先Always 0 
            msg.CoilTurning = DetCoilTuning(pdi.Uncoil_Direction, pdi.Appoint_Grinding_Surface);
            
                       
            //是否使用頭段導帶 (0:N, 1:Y)
            msg.HeadEndLeaderAttached = IsHeadEndLeaderAttached(pdi.Head_Leader_Attached);
            
            if(msg.HeadEndLeaderAttached == PlcSysDef.Cmd.Use)
            {
                // 0:SUS304  1:SUS430
                msg.HeadEndLeaderSteelGrade = GetSteelGrade(pdi.Head_Leader_St_No);
                // 頭段導帶厚度 mm 
                msg.HeadEndLeaderThickness = (pdi.Head_Leader_Thickness * 1000).ToShort();
                // 頭段導帶寬度 m->mm
                msg.HeadEndLeaderWidth = pdi.Head_Leader_Width.ToShort();
                // 頭段導帶長度
                msg.HeadEndLeaderLength = (pdi.Head_Leader_Length * 1000).ToShort();//(short)pdi.Head_Leader_Length;//
                // 頭段導帶焊接點 0 : Before Punch hole 1 : After Punch hole
                msg.HeadEndLeaderWeldPointPosition = DetWeldPointPosition(pdi.Head_Hole_Position);
                // 頭段導帶卡孔位置 default 600mm
                //msg.HeadEndLeaderWeldPointDist = pdi.Head_Hole_Position.ToShort();
                msg.HeadEndLeaderWeldPointDist = 600;
            }

            msg.TailEndLeaderAttached = IsHeadEndLeaderAttached(pdi.Tail_Leader_Attached);

            if (msg.TailEndLeaderAttached == PlcSysDef.Cmd.Use)
            {
                // 0:SUS304  1:SUS430
                msg.TailEndLeaderSteelGrade = GetSteelGrade(pdi.Tail_Leader_St_No);
                // 尾段導帶厚度
                msg.TailEndLeaderThickness = (pdi.Tail_Leader_Thickness * 1000).ToShort();
                // 尾段導帶寬度
                msg.TailEndLeaderWidth = pdi.Tail_Leader_Width.ToShort();
                // 尾段導帶長度 m->mm
                msg.TailEndLeaderLength = (pdi.Tail_Leader_Length * 1000).ToShort();//(pdi.Tail_Leader_Length ).ToShort();
                // 尾段導帶焊接點 0 : Before Punch hole 1 : After Punch hole
                msg.TailEndLeaderWeldPointPosition = DetWeldPointPosition(pdi.Tail_Hole_Position);
                // 尾段導帶卡孔位置 default 600mm
                //msg.TailEndLeaderWeldPointDist = pdi.Tail_Hole_Position.ToShort();
                msg.TailEndLeaderWeldPointDist = 600;
            }

            // 出口套筒使用 有:1 (有使用) 無: 0 (無使用)
            msg.SleeveInstalled = IsSleeveInstalled(pdi.Out_Sleeve_Type_Code);
            // 查表                                                                                    
            msg.SleeveThickness = lkTableData.SleeveThickness.ToShort();
            msg.SleeveWidth = lkTableData.SleeveWidth.ToShort();
            // 出口墊紙使用
            msg.PaperInstalled = IsPaperInstalled(pdi.Out_Paper_Code);
            msg.CoilPaperCode = pdi.Out_Paper_Code.ToNullable<short>() ?? 0;
            msg.CoilPaperType = pdi.Out_Paper_Req_Code.ToNullable<short>() ?? 0;
            // 張力機  查表 TBL_LookupTable_LineTension
            msg.UnitTension = (lkTableData.UnitTension*10).ToShort();

            // 根據工序代碼給值
            msg = ProcessCodePro(pdi, lkTableData, msg);

            return msg;
        }

        private static L2L1Snd.Msg_204_PDI_TM3 ProcessCodePro(PDIEntity.TBL_PDI pdi, LkUpTableModel.Preset204 lkTableData, L2L1Snd.Msg_204_PDI_TM3 preset)
        {


            return preset;
        }


        public static L2L1Snd.Msg_204_PDI_TM3 Empty204PresetMsg(string coilID)
        {
            var emptyMsg = new L2L1Snd.Msg_204_PDI_TM3()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1204Preset),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_204_PDI_TM3>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion
            };


            foreach (FieldInfo fi in emptyMsg.GetType().GetFields())
            {
                if (fi.FieldType == typeof(char[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(emptyMsg, "".PadRight(ma.SizeConst).ToCharArray());
                }
            }

            emptyMsg.CoilId = coilID.ToCByteArray(24);

            return emptyMsg;
        }
        public static L2L1Snd.Msg_205_PDI_TM3_2 Empty205PresetMsg()
        {
            var emptyMsg = new L2L1Snd.Msg_205_PDI_TM3_2()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1205Preset),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_205_PDI_TM3_2>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion

            };

            foreach (FieldInfo fi in emptyMsg.GetType().GetFields())
            {
                if (fi.FieldType == typeof(char[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(emptyMsg, "".PadRight(ma.SizeConst).ToCharArray());
                }
            }

            return emptyMsg;
        }

        #region Preset資料判別
        private static short GetSteelGrade(string stNo)
        {
            short grade = 0;

            switch (stNo)
            {
                case "SUS304":
                    grade = 0;
                    break;
                case "SUS430":
                    grade = 1;
                    break;
            }
            return grade;
        }
        private static short IsSleeveInstallledCoil(string entrySleeveCode)
        {
            return entrySleeveCode.Equals(string.Empty) ? (short)0 : (short)1;
        }
        private static short IsPaperInstalledCoil(string paperReqCode)
        {
            return paperReqCode.Equals(string.Empty) ? (short)0 : (short)1;
        }
        private static short DegreasingFlag(string skimFlag)
        {
            return skimFlag.Equals(string.Empty) ? (short)0 : (short)1;
        }
        private static short IsHeadEndLeaderAttached(string headLeaderAttached)
        {
            return headLeaderAttached.Equals(MMSSysDef.Cmd.NotUse) ? (short)0 : (short)1;
        }
        private static short DetWeldPointPosition(float holePos)
        {
            return holePos < 600f ? (short)0 : (short)1;
        }
        private static short IsSleeveInstalled(string entrySleeveCode)
        {
            return entrySleeveCode.Equals(string.Empty) ? (short)0 : (short)1;
        }
        private static short IsPaperInstalled(string outPaperCode)
        {
            return outPaperCode.Equals(string.Empty) ? (short)0 : (short)1;
        }
        private static short DetProcessingSurface(string decoilerDirection)
        {
            return decoilerDirection.Equals(MMSSysDef.Cmd.UnCoilUpStr) ? (short)0 : (short)1; // 加工面 0:Inside.  1:Outside
        }
        private static short DetIncomingCoilWoundDirection(string decoilerDirection)
        {
            return decoilerDirection.Equals(MMSSysDef.Cmd.UnCoilUpStr) ? (short)1 : (short)0; // 開捲方向 0:Type A. CCW   1:Type B. CW
        }

        private static short DetCoilTuning(string uncoilDirection, string appointGrindingSurface)
        {
            if (uncoilDirection.Trim().Equals(MMSSysDef.Cmd.UnCoilUpStr) && appointGrindingSurface.Trim().Equals(MMSSysDef.Cmd.InSide))
                return 1;

            if (uncoilDirection.Trim().Equals(MMSSysDef.Cmd.UnCoilDownStr) && appointGrindingSurface.Trim().Equals(MMSSysDef.Cmd.OutSide))
                return 1;

            return 0;
        }

        #endregion

        public static L2L1Snd.Msg_205_PDI_TM3_2 ConvertToPreset205Msg(this PDIEntity.TBL_PDI pdi, int pos)
        {

            if (pdi == null)
                return new L2L1Snd.Msg_205_PDI_TM3_2();

            var preset205 = new L2L1Snd.Msg_205_PDI_TM3_2()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1205Preset),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_205_PDI_TM3_2>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion

                PresetPosition = (short)pos,

              
            };


            return preset205;
        }



        public static L2L1Snd.Msg_202_PDI_TM1 ConvertToPreset202Msg(this PDIEntity.TBL_PDI pdi, int skNo)
        {

            if (pdi == null)
                return new L2L1Snd.Msg_202_PDI_TM1();

            var msg = new L2L1Snd.Msg_202_PDI_TM1();

            #region Header
            msg.MessageId = short.Parse(PlcSysDef.SndMsgCode.L1202PDI);
            msg.MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_202_PDI_TM1>();
            msg.Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            msg.Time = Int32.Parse(DateTime.Now.ToString("HHmmss"));
            #endregion

            // Data 待填
            msg.CoilId = pdi.In_Coil_ID.ToCByteArray(24);
            msg.SteelGrade = pdi.St_No.ToCByteArray(12);
            msg.CoilThickness = (short)(pdi.In_Coil_Thick * 1000);
            msg.CoilWidth = (short)pdi.In_Coil_Width;
            msg.CoilLength = (short)pdi.In_Coil_Length;
            msg.CoilinnerDiameter = (short)pdi.In_Coil_Inner_Diameter;
            msg.CoilouterDiameter = (short)pdi.In_Coil_Outer_Diameter;


            //if (!pdi.In_Sleeve_Type_Code.Equals(string.Empty))
            //{
            //    msg.SleeveInstallledCoil = PlcSysDef.Cmd.Use;
            //    msg.SleeveOuterDiameter = (short)pdi.Out_Sleeve_Diamter;
            //}
            //else
            //{
            //    msg.SleeveInstallledCoil = PlcSysDef.Cmd.NotUse;
            //    msg.SleeveOuterDiameter = 0;
            //}
            switch (pdi.In_Sleeve_Type_Code.Trim())
            {
                case "00":
                    msg.SleeveInstallledCoil = PlcSysDef.Cmd.NotUse;
                    msg.SleeveOuterDiameter = 0;
                    break;
                case "0":
                    msg.SleeveInstallledCoil = PlcSysDef.Cmd.NotUse;
                    msg.SleeveOuterDiameter = 0;
                    break;
                case "":
                    msg.SleeveInstallledCoil = PlcSysDef.Cmd.NotUse;
                    msg.SleeveOuterDiameter = 0;
                    break;
                default:
                    msg.SleeveInstallledCoil = PlcSysDef.Cmd.Use;
                    msg.SleeveOuterDiameter = (short)pdi.Out_Sleeve_Diamter;
                    break;
            }



            msg.PaperInstalledCoil = !pdi.Pack_Type_Code.Equals(string.Empty) ? PlcSysDef.Cmd.Use : PlcSysDef.Cmd.NotUse;
            msg.IncomingCoilwoundDirection = pdi.Uncoil_Direction.Equals(MMSSysDef.Cmd.UnCoilUpStr) ? PlcSysDef.Cmd.CWType : PlcSysDef.Cmd.CCWType;
            msg.ProcessingSurface = pdi.Uncoil_Direction.Equals(MMSSysDef.Cmd.UnCoilUpStr) ? PlcSysDef.Cmd.Outside : PlcSysDef.Cmd.Inside;
            msg.CoilTuning = DetCoilTuning(pdi.Uncoil_Direction, pdi.Appoint_Grinding_Surface); // 待確定
       
            msg.HeadEndLeaderAttached = pdi.Head_Leader_Attached.Equals(MMSSysDef.Cmd.Use) ? PlcSysDef.Cmd.Use : PlcSysDef.Cmd.NotUse;
            msg.TailEndLeaderAttached = pdi.Tail_Leader_Attached.Equals(MMSSysDef.Cmd.Use) ? PlcSysDef.Cmd.Use : PlcSysDef.Cmd.NotUse;
            msg.TestPlanNo = pdi.Plan_No.ToCByteArray(52);
            msg.PresetPosition = (short)skNo;

            return msg;
        }

        public static L2L1Snd.Msg_210_Del_CoilID ConvertToDelCoilIDMsg(this DeleteResult deleteResult)
        {
            var msg = new L2L1Snd.Msg_210_Del_CoilID()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1210DelCoil),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_210_Del_CoilID>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion

                DeleltePosition = deleteResult.DelPosition,
                CoilId = deleteResult.CoilId.ToCByteArray(24),

            };

            return msg;
        }

        public static L2L1Snd.Msg_203_PDI_TM2 ConvertToCoilScnCheckResult(short coilcheck)
        {
            var msg = new L2L1Snd.Msg_203_PDI_TM2()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1203EntryScnResult),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_203_PDI_TM2>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion
                CoilIDConfirmResult = coilcheck
            };
            return msg;
        }

        public static L2L1Snd.Msg_207_Entry_Take_Over_Start ConvertEntryTakeOverStartCM()
        {
            var msg = new L2L1Snd.Msg_207_Entry_Take_Over_Start()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1207EntryTakeOverStartCMD),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_207_Entry_Take_Over_Start>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion                
            };
            return msg;
        }

        public static L2L1Snd.Msg_208_Delivery_Take_Over_Start_CM ConvertDelTakeOverStartCM()
        {
            var msg = new L2L1Snd.Msg_208_Delivery_Take_Over_Start_CM()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1208DelveryToCMD),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_208_Delivery_Take_Over_Start_CM>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion                
            };
            return msg;
        }

        public static L2L1Snd.Msg_209_Delivery_BC_Confirm ConvertToDeliveryCoilScnCheckResult(short coilcheck)
        {
            var msg = new L2L1Snd.Msg_209_Delivery_BC_Confirm()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1209DeliveryScnResult),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_209_Delivery_BC_Confirm>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion
                CoilIDConfirmResult = coilcheck
            };
            return msg;
        }

        public static L2L1Snd.Msg_206_Belt_Info ConvertToBeltInfo(this BeltAccEntity.TBL_Belts belt)
        {
            var msg = new L2L1Snd.Msg_206_Belt_Info()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1206BeltInfoMsg),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_206_Belt_Info>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion

                ABbeltID = belt.Belt_No.ToCByteArray(24),
                ABbeltkind = belt.Material_Code.ToCByteArray(2),
                //ABbeltroughness = (short)belt.Total_Grind_Length_Belt,
                ABbeltroughness = belt.Belt_Particle_Number,
                ABBeltAccGriStLength = belt.Total_Grind_Length_Strip,
                ABBeltAccGriBeltLength = belt.Total_Grind_Length_Belt
            };
            return msg;
        }

        public static L2L1Snd.Msg_211_Modify_Coil_ID ConvertToModifyCoilInfo(this ModifyCoilModel.ModifyResult modifyCoilInfo)
        {
            var msg = new L2L1Snd.Msg_211_Modify_Coil_ID()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1211ModifyCoilID),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_211_Modify_Coil_ID>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion
                ModifyPosition = modifyCoilInfo.ModifyPosition,
                CoilId = modifyCoilInfo.CoilId.ToCByteArray(24)
            };
            return msg;
        }



    }
}
