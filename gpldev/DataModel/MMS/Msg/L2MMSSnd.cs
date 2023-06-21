 using System;
using System.Runtime.InteropServices;
using Core.Util;

namespace MsgStruct
{
    public class L2MMSSnd
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_PDO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] OrderNo = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PlanNo = new byte[10];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Out_Mat_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] In_Mat_No = new byte[20];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] StartTime = new byte[14];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] FinishTime = new byte[14];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Shift = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Team = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] St_No = new byte[8];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Mat_Outer_Diameter = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Mat_Inner = new byte[4];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Thick = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_C40_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mid_C40_Thick = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_C40_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_C25_Thick = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mid_C25_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_C25_Thick = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Wt = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Gross_Wt = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Head_Pass_Num = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Mid_Pass_Num = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Tail_Pass_Num = new byte[2];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] ExitSleeveUseOrNot = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ExitSleeveDiameter = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ExitSleeveCode = new byte[4];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Paper_Req_Code = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Code = new byte[3];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Paper_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Paper_Width = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Paper_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Paper_Width = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D1_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D1_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D1_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D1_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D1_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D2_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D2_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D2_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D2_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D2_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D3_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D3_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D3_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D3_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D3_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D4_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D4_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D4_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D4_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D4_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D5_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D5_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D5_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D5_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D5_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D6_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D6_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D6_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D6_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D6_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D7_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D7_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D7_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D7_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D7_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D8_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D8_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D8_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D8_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D8_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D9_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D9_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D9_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D9_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D9_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D10_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D10_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Winding_Dire = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] BaseSurface = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Inspector = new byte[10];
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Hold_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Hold_Cause_Code = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]      
            public byte[] Sample_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            
            public byte[] Fixed_Wt_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            
            public byte[] End_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Scrap_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Sample_Frqn_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code = new byte[2];
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Head_Hole_Position = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_LeaderLength = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Leader_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Leader_Thickness = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Tail_Hole_Position = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_LeaderLength = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Leader_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Leader_Thickness = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Head_Leader_St_No = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Tail_Leader_St_No = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Off_Gauge = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Off_Gauge = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Pre_Grinding_Surface = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Grinding_Count_Out = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Grinding_Count_In = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Appoint_Grinding_Surface = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Oil_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code_In = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code_Out = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Process_Code = new byte[6];
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            //public byte[] Head_Rough = new byte[4];
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            //public byte[] Mid_Rough = new byte[4];
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            //public byte[] Tail_Rough = new byte[4];    
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Rough_Ra = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mid_Rough_Ra = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Rough_Ra = new byte[5];
           
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Uncoil_Direction = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Recoiler_Actten_Avg = new byte[7];
            //·s¼W
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Rough_Rz = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mid_Rough_Rz = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Rough_Rz = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Rough_Rmax = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mid_Rough_Rmax = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Rough_Rmax = new byte[5];

            public string EntryCoilID { get => In_Mat_No.ToStr(); }
            public string OutCoilID { get => Out_Mat_No.ToStr(); }
            public string MsgID { get => Code.ToStr(); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Req_Coil_Schedule
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Requested_Coil_No;
            public string MsgID { get => Code.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_Coil_Schedule
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Requested_Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Process_Result;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] Reject_Cause;


            public string RequestedCoilNo { get => Requested_Coil_No.ToStr(); }
            public string ProcessResult { get => Process_Result.ToStr(); }
            public string RejectCause { get => Reject_Cause.ToStr(); }
            public string MsgID { get => Code.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Request_Coil_PDI
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Requested_Coil_No;

            public string MsgID { get => Code.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Schedule_Changed
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Number_Of_Coils;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6000)]
            public byte[] Entry_Coil_No;

            public string MsgID { get => Code.ToStr(); }
            public string CoilNo { get => Entry_Coil_No.ToStr(); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_Coil_PDI
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] RequestedCoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] ProcessResult;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] RejectCause;

            public string MsgID { get => Code.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_Coil_Plan_Delete
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] RequestedCoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] ProcessResult;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] RejectCause;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Schedule_Delete
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] OperatorId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ReasonCode;

            public string MsgID { get => Code.ToStr(); }

            public string CoilNo { get => EntryCoilNo.ToStr(); }

        }
       
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Pack_Order_Adj
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] NumberOfCoils;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Package_Material_Del_Res
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] DEL_FLAG;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] PLAN_FLAG;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_UnPacking_Order_Adj
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] NumberOfCoils;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Packing_PDI_Req
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] NumberOfCoils;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Reject_Result
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Reject_Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Entry_CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Plan_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Mode_Of_Reject;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Length_Of_Rejected_Coil;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Weight_Of_Rejected_Coil;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Inner_Diameter_Of_RejectedCoil;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Outer_Diameter_Of_RejectedCoil;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reason_Of_Reject;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] Time_Of_Reject;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Shift_Of_Reject;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Turn_Of_Reject;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Paper_exit_Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Paper_Type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Finally_Tag;


            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D1_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D1_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D1_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D1_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D1_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D1_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D2_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D2_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D2_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D2_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D2_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D2_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D3_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D3_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D3_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D3_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D3_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D3_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D4_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D4_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D4_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D4_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D4_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D4_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D5_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D5_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D5_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D5_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D5_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D5_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D6_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D6_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D6_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D6_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D6_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D6_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D7_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D7_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D7_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D7_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D7_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D7_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D8_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D8_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D8_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D8_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D8_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D8_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D9_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D9_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D9_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D9_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D9_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D9_QGrade = new byte[1];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D10_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D10_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_QGrade = new byte[1];


            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Paper_Length = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Paper_Width = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Paper_Length = new byte[5];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Paper_Width = new byte[5];


            
            // Defect

            public string MsgID { get => Code.ToStr(); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Loaded_Skid
        {

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Plan_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Entry_Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] Loaded_Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Unit_Code;

            public string MsgID { get => Code.ToStr(); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Equipment_Down_Result_Msg
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Op_Flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Unit_Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Prod_Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Prod_Shift_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Prod_Shift_Group;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] Stop_Start_Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] Stop_End_Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Delay_Location;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public byte[] Delay_Location_Desc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Stop_Elased_Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Delay_Reason_Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] Delay_Reason_Desc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Delay_Remark;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_m;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_e;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_c;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_p;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_u;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_o;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_r;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Resp_Depart_Delay_Time_rs;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Deceleration_Cauese;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Deceleration_Code;

            public string MsgID { get => Code.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Energy_Consumption_Info
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Shift_Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Shift_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Group_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Unit_code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_7_;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PW_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DCW_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IDCW_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] DeW_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NG_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CA_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Steam_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] N2_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] NH3_G_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Electrical_Total_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] IW_15;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_14;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] F_ire_W_15;

            public string MsgID { get => Code.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Packaging_Real_Parameter
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Pack_Plan_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Pack_Type_Code_ACT;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] Finish_Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mat_Act_WT;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Mat_Gross_WT;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Mat_Weight;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_UnPackaging_Real_Parameter
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Pack_Plan_No;

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_PlanNo_Delete
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Plan_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] ProcessResult;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] RejectCause;

            public string MsgID { get => Code.ToStr(); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Dummy_Coil_List_Result_Request
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Mat_No = new byte[20];

            public string MsgID { get => Code.ToStr(); }

            public string CoilNo { get => Mat_No.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Dummy_Coil_List_Delete
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Mat_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Reason_Code = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] Delete_Time = new byte[14];

            public string MsgID { get => Code.ToStr(); }

            public string CoilNo { get => Mat_No.ToStr(); }

        }

    }
}
