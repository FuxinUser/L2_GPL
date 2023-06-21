using Core.Util;
using System;
using System.Runtime.InteropServices;
namespace MsgStruct
{
    public class L2L1Snd
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
        public class Msg_201_Alive
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
        public class Msg_202_PDI_TM1
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
            public byte[] CoilId = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] SteelGrade = new byte[12];
            [MarshalAs(UnmanagedType.I2)]
            public short CoilThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilLength;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilinnerDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilouterDiameter;

            [MarshalAs(UnmanagedType.I2)]
            public short SleeveInstallledCoil;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveOuterDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short PaperInstalledCoil;
            [MarshalAs(UnmanagedType.I2)]
            public short IncomingCoilwoundDirection;
            [MarshalAs(UnmanagedType.I2)]
            public short ProcessingSurface;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilTuning;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderAttached;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderAttached;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 52)]
            public byte[] TestPlanNo = new byte[52];
            [MarshalAs(UnmanagedType.I2)]
            public short PresetPosition;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_203_PDI_TM2
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
            public short CoilIDConfirmResult;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_204_PDI_TM3
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
            public int PresetPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 52)]
            public byte[] TestPlanNo = new byte[52];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilId = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float Density;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] SteelGrade = new byte[12];
            [MarshalAs(UnmanagedType.I2)]
            public short CoilLength;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilInnerDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilOuterDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveInstallledCoil;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveOuterDiameter;
            [MarshalAs(UnmanagedType.I2)]
            public short PaperInstalledCoil;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilPaperCode;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilPaperType;
            [MarshalAs(UnmanagedType.I2)]
            public short OffGaugeHead;
            [MarshalAs(UnmanagedType.I2)]
            public short OffGaugeTail;
            [MarshalAs(UnmanagedType.I2)]
            public short FlattenerIntermesh1;
            [MarshalAs(UnmanagedType.I2)]
            public short FlattenerIntermesh2;
            [MarshalAs(UnmanagedType.I2)]
            public short DegreasingFlag;
            [MarshalAs(UnmanagedType.I2)]
            public short IncomingCoilWoundDirection;
            [MarshalAs(UnmanagedType.I2)]
            public short ProcessingSurface;
            [MarshalAs(UnmanagedType.I2)]
            public short CoilTurning;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderAttached;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderSteelGrade;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderLength;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderWeldPointPosition;
            [MarshalAs(UnmanagedType.I2)]
            public short HeadEndLeaderWeldPointDist;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderAttached;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderSteelGrade;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderLength;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderWeldPointPosition;
            [MarshalAs(UnmanagedType.I2)]
            public short TailEndLeaderWeldPointDist;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveInstalled;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveThickness;
            [MarshalAs(UnmanagedType.I2)]
            public short SleeveWidth;
            [MarshalAs(UnmanagedType.I2)]
            public short PaperInstalled;
            [MarshalAs(UnmanagedType.I2)]
            public short SleevePaperCode;
            [MarshalAs(UnmanagedType.I2)]
            public short SleevePaperType;
            [MarshalAs(UnmanagedType.I2)]
            public short UnitTension;
            [MarshalAs(UnmanagedType.I2)]
            public short PassNumberForCoilHeadGrinding;
            [MarshalAs(UnmanagedType.I2)]
            public short PassNumberForCoilCenterGrinding;
            [MarshalAs(UnmanagedType.I2)]
            public short PassNumberForCoilTailGrinding;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed1Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind1Head = new byte[2];            
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed1Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind1Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed1Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind1Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet1Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind1Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet1Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind1Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet1Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind1Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection1Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet1Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed1Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind1Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed1Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind1Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed1Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind1Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet1Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind1Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet1Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind1Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet1Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind1Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection1Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet1Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed1Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind1Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed1Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind1Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed1Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind1Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet1Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind1Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet1Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind1Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet1Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind1Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection1Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet1Tail;
            [MarshalAs(UnmanagedType.I2)]

            public short LineSpeed2Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind2Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed2Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind2Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed2Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind2Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet2Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind2Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet2Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind2Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet2Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind2Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection2Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet2Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed2Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind2Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed2Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind2Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed2Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind2Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet2Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind2Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet2Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind2Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet2Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind2Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection2Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet2Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed2Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind2Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed2Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind2Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed2Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind2Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet2Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind2Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet2Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind2Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet2Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind2Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection2Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet2Tail;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed3Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind3Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed3Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind3Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed3Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind3Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet3Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind3Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet3Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind3Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet3Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind3Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection3Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet3Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed3Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind3Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed3Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind3Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed3Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind3Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet3Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind3Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet3Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind3Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet3Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind3Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection3Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet3Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed3Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind3Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed3Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind3Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed3Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind3Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet3Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind3Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet3Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind3Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet3Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind3Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection3Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet3Tail;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed4Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind4Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed4Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind4Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed4Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind4Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet4Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind4Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet4Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind4Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet4Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind4Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection4Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet4Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed4Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind4Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed4Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind4Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed4Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind4Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet4Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind4Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet4Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind4Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet4Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind4Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection4Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet4Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed4Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind4Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed4Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind4Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed4Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind4Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet4Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind4Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet4Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind4Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet4Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind4Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection4Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet4Tail;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_205_PDI_TM3_2
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
            public short PresetPosition;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed5Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind5Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed5Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind5Head = new byte[2];         
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed5Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind5Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet5Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind5Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet5Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind5Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet5Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind5Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection5Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet5Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed5Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind5Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed5Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind5Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed5Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind5Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet5Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind5Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet5Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind5Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet5Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind5Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection5Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet5Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed5Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind5Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed5Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind5Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed5Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind5Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet5Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind5Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet5Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind5Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet5Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind5Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection5Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet5Tail;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed6Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind6Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed6Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind6Head = new byte[2];     
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed6Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind6Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet6Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind6Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet6Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind6Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet6Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind6Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection6Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet6Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed6Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind6Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed6Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind6Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed6Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind6Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet6Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind6Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet6Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind6Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet6Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind6Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection6Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet6Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed6Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind6Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed6Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind6Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed6Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind6Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet6Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind6Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet6Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind6Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet6Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind6Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection6Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet6Tail;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed7Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind7Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed7Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind7Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed7Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind7Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet7Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind7Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet7Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind7Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet7Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind7Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection7Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet7Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed7Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind7Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed7Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind7Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed7Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind7Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet7Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind7Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet7Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind7Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet7Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind7Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection7Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet7Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed7Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind7Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed7Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind7Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed7Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind7Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet7Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind7Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet7Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind7Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet7Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind7Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection7Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet7Tail;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed8Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind8Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed8Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind8Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed8Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind8Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet8Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind8Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet8Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind8Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet8Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind8Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection8Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet8Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed8Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind8Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed8Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind8Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed8Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind8Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet8Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind8Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet8Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind8Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet8Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind8Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection8Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet8Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed8Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind8Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed8Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind8Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed8Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind8Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet8Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind8Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet8Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind8Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet8Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind8Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection8Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet8Tail;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed9Head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind9Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed9Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind9Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed9Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind9Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet9Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind9Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet9Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind9Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet9Head;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind9Head = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection9Head;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet9Head;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed9Center;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind9Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed9Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind9Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed9Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind9Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet9Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind9Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet9Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind9Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet9Center;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind9Center = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection9Center;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet9Center;

            [MarshalAs(UnmanagedType.I2)]
            public short LineSpeed9Tail;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No1GrAbBeltKind9Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRoughness9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltRotatingDirection9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrGrinderMotorCurrentSet9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No1GrAbBeltSpeed9Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No2GrAbBeltKind9Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRoughness9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltRotatingDirection9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrGrinderMotorCurrentSet9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No2GrAbBeltSpeed9Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No3GrAbBeltKind9Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRoughness9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrAbBeltRotatingDirection9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No3GrGrinderMotorCurrentSet9Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No4GrAbBeltKind9Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRoughness9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrAbBeltRotatingDirection9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No4GrGrinderMotorCurrentSet9Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No5GrAbBeltKind9Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRoughness9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrAbBeltRotatingDirection9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No5GrGrinderMotorCurrentSet9Tail;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] No6GrAbBeltKind9Tail = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRoughness9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrAbBeltRotatingDirection9Tail;
            [MarshalAs(UnmanagedType.I2)]
            public short No6GrGrinderMotorCurrentSet9Tail;

          

            [MarshalAs(UnmanagedType.I2)]
            public short Defect_Flag;
     
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_206_Belt_Info
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
            public byte[] ABbeltID = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] ABbeltkind = new byte[2];
            [MarshalAs(UnmanagedType.I2)]
            public short ABbeltroughness;
            [MarshalAs(UnmanagedType.R4)]
            public float ABBeltAccGriBeltLength;
            [MarshalAs(UnmanagedType.R4)]
            public float ABBeltAccGriStLength;
            [MarshalAs(UnmanagedType.I2)]
            public short BeltExistFlag;



            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_207_Entry_Take_Over_Start
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
        public class Msg_208_Delivery_Take_Over_Start_CM
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
        public class Msg_209_Delivery_BC_Confirm
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
            public short CoilIDConfirmResult;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_210_Del_CoilID
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
            public short DeleltePosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilId = new byte[24];

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_211_Modify_Coil_ID
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            /*
                1:POR
                2:No.1 entry skid
                3:Entry TOP
                4:Entry lift car
                5:TR
                6:No.1 delivery skid
                7:No.2 delivery skid
                8:Delivery TOP
                9:Delivery lift car
            */
            [MarshalAs(UnmanagedType.I2)]
            public short ModifyPosition;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilId = new byte[24];

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }
    }
}