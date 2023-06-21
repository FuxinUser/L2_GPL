using Core.Util;
using System;
using System.Runtime.InteropServices;
namespace MsgStruct
{
    public class L2_WMS_Snd
    {

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GWx1_ScheduleList
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Count;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6000)]
            public char[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilNos { get => CoilNo.ToStr(); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GWx2_TrackingMap
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] TopSensor1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] EntryExit1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk21;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk22;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk23;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] TopSensor2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] EntryExit2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk31;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk32;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sk33;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] TopSensor3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] EntryExit3;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] LineSatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
           

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GWx3_CoilInfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] InCoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] OutCoilNo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] OrderNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] PackType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] InnerDia;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] OuterDia;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] CoilThick;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] CoilWidth;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] CoilWeight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] CoilTurn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] CoilContainsOil;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] Next_WholeBackLog_Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Sleeve_InnerDia;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] Sleeve_Thick;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] Sleeve_Width;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilIDNo { get => InCoilNo.ToStr(); }

        }

       


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GWx5_FeedingRequest_EntryExitReturn
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] Flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] SKIDNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] coilTurn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilIDNo { get => CoilNo.ToStr(); }
        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GWx6_ScanCoil
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] SkidNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] Spare; 

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilIDNo { get => CoilNo.ToStr(); }
        }

    }
}