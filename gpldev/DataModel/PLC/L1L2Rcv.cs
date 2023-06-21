using Core.Define;
using Core.Util;
using DataMod.WMS.LogicModel;
using System;
using System.Runtime.InteropServices;
namespace MsgStruct
{
    public class L1L2Rcv
    {
        private static DateTime IntConvertDateTime(int Date, int Time)
        {

            try
            {
                var date = CalanderUtil.ConvertDateFormatStr(Date.ToString(), "yyyyMMdd", "yyyy-MM-dd");
                var time = CalanderUtil.ConvertTimeFormatStr(Time.ToString().PadLeft(6, '0'), "HHmmss", "HH:mm:ss");
                var datetime = string.Format("{0} {1}", date, time);
                //return DateTime.Parse(datetime) != null ? DateTime.Parse(datetime) : DBDefine.DefaultTime;
                return DateTime.Parse(datetime);
            }
            catch
            {
                //return DBDefine.DefaultTime;
                throw;
            }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_101_Alive
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_102_PDO
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilNo = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] SteelGrade = new byte[12];
            [MarshalAs(UnmanagedType.I2)]
            public short CoilThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilInnerdiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short Coilouterdiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short Coiltheoreticalweight;
            [MarshalAs(UnmanagedType.I2)]
            public short IsCoilTurned;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveInstallled;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short PaperInstalled;
            [MarshalAs(UnmanagedType.I2)]
            public short DeliveryShearCoilHeadEndCutLength;
            [MarshalAs(UnmanagedType.I2)]
            public short DeliveryShearCoilTailEndCutLength;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeader;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderLength;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeader;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderLength;
            [MarshalAs(UnmanagedType.I4)]
            public int CoilLength;
            [MarshalAs(UnmanagedType.I2)]
            public short DegreasingFlag;
            [MarshalAs(UnmanagedType.I2)]
            public short ActualUncoilDirection;     // 0 : UP, 1 : DOWN
            [MarshalAs(UnmanagedType.R4)]
            public float RecoilerTension;          // ³æ¦ì : KN

            public string CoilIDNo { get => CoilNo.ToStr(); }
            public int CoilVolumn { get => CoilThickness * CoilWidth * CoilLength; }

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_104_ProData
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I2)]
            public short Linespeed;
            [MarshalAs(UnmanagedType.I2)]
            public short TensionReelSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short ThreadingSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short Linetension;
            [MarshalAs(UnmanagedType.I2)]
            public short Linerundirection;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GRAbrasiveBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GRAbrasiveBeltSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GRAbrasiveBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GRAbrasiveBeltSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GRAbrasiveBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GRAbrasiveBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GRAbrasiveBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GRAbrasiveBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short CoolantOilTankTemperature;
            [MarshalAs(UnmanagedType.I2)]
            public short AlkaliSolutionTankTemperature;
            [MarshalAs(UnmanagedType.I2)]
            public short PrimaryRinseWaterTankTemperature;
            [MarshalAs(UnmanagedType.I2)]
            public short FinishRinseTankTemperature;
            [MarshalAs(UnmanagedType.I2)]
            public short StripDryerTemperature;
            [MarshalAs(UnmanagedType.I2)]
            public short BrushRollCurrent1;
            [MarshalAs(UnmanagedType.I2)]
            public short BrushRollCurrent2;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_105_Trk_Map
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] Pay_OffReel = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] EntryCoilSkidNo1 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] EntryTOP = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] EntryLiftCar = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] TensionReel = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] DeliveryCoilSkidNo1 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] DeliveryCoilSkidNo2 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] DeliveryTOP = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] DeliveryLiftCar = new byte[24];

            public string Entry_Car { get => EntryLiftCar.ToStr(); }
            public string Entry_SK01 { get => EntryCoilSkidNo1.ToStr(); }
            public string Entry_TOP { get => EntryTOP.ToStr(); }
            public string TR { get => TensionReel.ToStr(); }
            public string POR { get => Pay_OffReel.ToStr(); }
            public string Delivery_SK01 { get => DeliveryCoilSkidNo1.ToStr(); }
            public string Delivery_SK02 { get => DeliveryCoilSkidNo2.ToStr(); }
            public string Delivery_TOP { get => DeliveryTOP.ToStr(); }
            public string Delivery_Car { get=> DeliveryLiftCar.ToStr();}

    
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_106_Weld_Data
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short WeldVoltageSetPoint;
            [MarshalAs(UnmanagedType.I2)]
            public short WeldCurrentSetPoint;
            [MarshalAs(UnmanagedType.I2)]
            public short WeldWireSpeedSetPoint;
            [MarshalAs(UnmanagedType.I2)]
            public short TorchCarriageWeldSpeedPetPoint;
            [MarshalAs(UnmanagedType.I2)]
            public short ActualTorchCarriageWeldSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short WeldGapSelected;
            [MarshalAs(UnmanagedType.I2)]
            public short ActualWeldCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short StartPuddleTime;
            [MarshalAs(UnmanagedType.I2)]
            public short StopPuddleTime;
            [MarshalAs(UnmanagedType.I2)]
            public short WeldScheduleNumber;

            public string CoilIDNo { get => CoilID.ToStr(); }

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_107_Grd_Rpt
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short CurrentPassNumber;
            [MarshalAs(UnmanagedType.I2)]
            public short CurrentSession;
            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GRABBeltKind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GRABBeltRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GRABBeltMotorCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GRABBeltSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GRABBeltRotateDirection;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GRABBeltKind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GRABBeltRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GRABBeltCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GRABBeltSpeed;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GRABBeltRotateDirection;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GRABBeltKind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GRABBeltRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GRABBeltCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GRABBeltRotateDirection;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GRABBeltKind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GRABBeltRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GRABBeltCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GRABBeltRotatedirection;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GRABBeltKind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GRABBeltRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GRABBeltCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GRABBeltRotateDirection;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GRABBeltKind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GRABBeltRoughness;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GRABBeltCurrent;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GRABBeltRotateDirection;
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }


        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_108_Defect_Data
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilNo = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short PassNumber;
            [MarshalAs(UnmanagedType.I2)]
            public short CurrentSession;
            [MarshalAs(UnmanagedType.R4)]
            public float LengthFromHeadEnd;
            [MarshalAs(UnmanagedType.I2)]
            public short DefectPosition;
            [MarshalAs(UnmanagedType.I2)]
            public short DefectKind;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] DefectGrade = new byte[2];

            public string CoilID { get => CoilNo.ToStr().Trim(); }
            public string DefectLevel { get => DefectGrade.ToASCCIIStr().Trim(); }

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_109_Belt_ACC_Length
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.R4)]
            public float No1GRAbrBeltAccGriBeltLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No1GRAbrBeltAccGriStLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No2GRAbrBeltAccGriBeltLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No2GRAbrBeltAccGriStLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No3GRAbrBeltAccGriBeltLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No3GRAbrBeltAccGriStLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No4GRAbrBeltAccGriBeltLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No4GRAbrBeltAccGriStLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No5GRAbrBeltAccGriBeltLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No5GRAbrBeltAccGriStLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No6GRAbrBeltAccGriBeltLen;
            [MarshalAs(UnmanagedType.R4)]
            public float No6GRAbrBeltAccGriStLen;

            public float GetBeltAccLength(int no)
            {
                float length = -1.0f;

                switch (no)
                {
                    case 1:
                        length = No1GRAbrBeltAccGriBeltLen;
                        break;
                    case 2:
                        length = No2GRAbrBeltAccGriBeltLen;
                        break;
                    case 3:
                        length = No3GRAbrBeltAccGriBeltLen;
                        break;
                    case 4:
                        length = No4GRAbrBeltAccGriBeltLen;
                        break;
                    case 5:
                        length = No5GRAbrBeltAccGriBeltLen;
                        break;
                    case 6:
                        length = No6GRAbrBeltAccGriBeltLen;
                        break;
                }

                return length;
            }
            public float GetStAccLength(int no)
            {
                float length = -1.0f;

                switch (no)
                {
                    case 1:
                        length = No1GRAbrBeltAccGriStLen;
                        break;
                    case 2:
                        length = No2GRAbrBeltAccGriStLen;
                        break;
                    case 3:
                        length = No3GRAbrBeltAccGriStLen;
                        break;
                    case 4:
                        length = No4GRAbrBeltAccGriStLen;
                        break;
                    case 5:
                        length = No5GRAbrBeltAccGriStLen;
                        break;
                    case 6:
                        length = No6GRAbrBeltAccGriStLen;
                        break;
                }

                return length;
            }

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_110_Coil_Weight
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short CoilWeight;

            public string CoilIDNo { get => CoilID.ToStr(); }

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_111_LineFault
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I2)]
            public short FaultCode;
            [MarshalAs(UnmanagedType.I2)]
            public short StopCategory;
            [MarshalAs(UnmanagedType.I2)]
            public short LineStatus;


            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_112_Utility
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.R4)]
            public float CompressedAir;
            [MarshalAs(UnmanagedType.R4)]
            public float Steam;
            [MarshalAs(UnmanagedType.R4)]
            public float RinseWater;
            [MarshalAs(UnmanagedType.R4)]
            public float IndirectCollingWater;
            [MarshalAs(UnmanagedType.I4)]
            public int spare;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_113_Belt_Change
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I4)]
            public int GRNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] OldABBeltID = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] NewABBeltID = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float OldABBeltAccGriBeltLength;
            [MarshalAs(UnmanagedType.R4)]
            public float OldABBeltAccGriStLength;

            public string OldBeltNo { get => OldABBeltID.ToASCCIIStr().Trim(); }
            public string NewBeltNo { get => NewABBeltID.ToASCCIIStr().Trim(); }

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_114_Coil_Mount
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }            
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_115_Coil_Unmount
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_116_Coil_Weight
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I2)]
            public short LineRunDirection;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_117_Split
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_118_Entry_Start_Condition
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I2)]
            public short ConditionFlag;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_119_Entry_Take_Over_Start
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_120_Entry_Take_Over_End
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            //[MarshalAs(UnmanagedType.I2)]
            //public short Status;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_121_Delivery_Start_Condition
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I2)]
            public short StartCondition;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_122_Delivery_Take_Over_Start
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_123_Delivery_Take_Over_End
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            //[MarshalAs(UnmanagedType.I2)]
            //public short Status;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_124_StripBrakeSignal
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] UncoilerCoilNo = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short UncoilerCoilThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short UncoilerCoilWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short UncoilerCoilLength;
            [MarshalAs(UnmanagedType.I2)]
            public short UncoilerCoilInnerDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short UncoilerCoilOuterDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short UncoilerCoilTheoreticalWeight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] RecoilerCoilNo = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilLength;
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilInnerDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilOuterDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilTheoreticalWeight;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }


            public string PORCoilNo { get => UncoilerCoilNo.ToStr().Trim(); }
            public string TRCoilNo { get => RecoilerCoilNo.ToStr().Trim(); }
            public float TRCoilWeight
            {
                get => RecoilerCoilTheoreticalWeight.ToFloat();
            }

            public bool PORAndTR_IsSameCoilID
            {
                get => PORCoilNo.Equals(TRCoilNo);
            }

        }



        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_125_Share_Cut_Data
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float CutLength;
            [MarshalAs(UnmanagedType.I2)]
            public short CutMode;
            //[MarshalAs(UnmanagedType.R4)]
            //public float DiamRec;
            //[MarshalAs(UnmanagedType.R4)]
            //public float LengthRec;
            [MarshalAs(UnmanagedType.I2)]
            public short RecoilerCoilTheoreticalWeight;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
            public float TRCoilWeight
            {
                get => RecoilerCoilTheoreticalWeight.ToFloat();
            }

        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_126_Coil_Unmount_POR
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float CoilWeight;
            [MarshalAs(UnmanagedType.I4)]
            public int CoilLength;
            [MarshalAs(UnmanagedType.R4)]
            public float Diameter;
            [MarshalAs(UnmanagedType.R4)]
            public float CoiInsideDiam;
            [MarshalAs(UnmanagedType.R4)]
            public float Width;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_127_Coil_ID_Modify_Reply
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;

            [MarshalAs(UnmanagedType.I2)]
            public short ModifyPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.I2)]
            public short ModifyReply;
  
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }

    }
}