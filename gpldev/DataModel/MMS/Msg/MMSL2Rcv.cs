using System;
using System.Runtime.InteropServices;
using System.Text;
using Core.Define;
using Core.Util;

namespace MsgStruct
{
    public class MMSL2Rcv
    {

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_HeartBeat
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

            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            //public byte[] End;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_PDI
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
            public byte[] Plan_No = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Mat_Plan_Seq_No = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Plan_Type = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] In_Mat_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] In_Mat_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] In_Mat_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] In_Mat_Wt = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] In_Mat_Len = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] In_Mat_Inner_Dia = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] In_Mat_Outer_Dia = new byte[4];
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_Type_Code = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_Diamter = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] St_No = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Density = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accuracy = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Flatness_Avg_Cr = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Better_Surf_Ward_Code = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Uncoil_Direction = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Origin_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Pack_Mode = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Out_Paper_Req_Code = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Out_Paper_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Sleeve_Type_Code = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Sleeve_Diamter = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Out_Mat_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Order_Cust_Code = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Prev_Whole_Backlog_Code = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Next_Whole_Backlog_Code = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Head_Leader_Accached = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Head_Hole_Position = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Leader_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Leader_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Leader_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Tail_Leader_Accached = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Tail_Hole_Position = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Leader_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Leader_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Leader_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Head_Leader_St_No = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Tail_Leader_St_No = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Min_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Max_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Mat_Inner_Dia = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Grinding_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Pack_Type_Code = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Repair_Type = new byte[4];

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

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] Test_Plan_No = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]  //2022.07.01 modify 500 -> 1000 By JiaWei 
            public byte[] Qc_Rmark = new byte[1000];
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code_In = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code_Out = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 500)]
            public byte[] Repair_Remark = new byte[500];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Skim_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Polishing_Type = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] Sg_Sign = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Process_Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Ys_Stand = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Ys_Max = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Ys_Min = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Order_Cust_Ename = new byte[200];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Order_Cust_Cname = new byte[200];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Accu_Desc = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Accuracy_Desc = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Accu_Desc_In = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Accu_Desc_Out = new byte[20];



            public string MsgID { get => Code.ToStr(); }
            public string EntryCoilNo { get => In_Mat_No.ToStr().ClearSpaceStr(); }
            public string PlanNo { get => Plan_No.ToStr(); }
            public string MatSeqNo { get => Mat_Plan_Seq_No.ToStr(); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Sleeve_Value_Synchronize
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Deal_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] SleeveCode = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Out_Mat_Inner_Dia = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Sleeve_Thick = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_Width = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Sleeve_Wt = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Sleeve_Type = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width_Min = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width_Max = new byte[5];
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            //public byte[] End_Tag = new byte[1];

            public string Action { get {

                    var action = string.Empty;

                    switch (Deal_Flag.ToStr())
                    {
                        case MMSSysDef.Cmd.SyncValueInsert:
                            action = "新增";
                            break;
                        case MMSSysDef.Cmd.SyncValueUpdate:
                            action = "更新";
                            break;
                        case MMSSysDef.Cmd.SyncValueDelete:
                            action = "刪除";
                            break;
                        default:
                            action = "無此命令代號";
                            break;
                    }

                    return action;
                
                } }


        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Paper_Value_Synchronize
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Deal_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PaperCode = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Paper_Wt = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Paper_Width = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Min_Thick = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Max_Thick = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Thick = new byte[3];

            public string Action
            {
                get
                {

                    var action = string.Empty;

                    switch (Deal_Flag.ToStr())
                    {
                        case MMSSysDef.Cmd.SyncValueInsert:
                            action = "新增";
                            break;
                        case MMSSysDef.Cmd.SyncValueUpdate:
                            action = "更新";
                            break;
                        case MMSSysDef.Cmd.SyncValueDelete:
                            action = "刪除";
                            break;
                        default:
                            action = "無此命令代號";
                            break;
                    }

                    return action;

                }
            }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Schedule
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] NumberOfCoils;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6000)]
            public byte[] Entry_Coil_No;

            public string MsgID { get => Code.ToStr(); }
            public string CoilNo { get => Coil_No.ToStr(); }
            public int CoilCount { get => NumberOfCoils.ToStr().ToNullable<int>()??0; }
            // 修改時注意不可用Trim...............
            public string EntryCoilNos { get => Encoding.UTF8.GetString(Entry_Coil_No); }

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_No_Coil_Schedule
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
            public byte[] Mat_No;

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_No_Coil_PDI
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
            public byte[] Mat_No;

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Dummy_Coil_List
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
            public byte[] Dummy_Coil_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Weight = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Dummy_Coil_Inner = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Dummy_Coil_Diam = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] SteelGradeSign = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] SteelGradeCode = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Dens_ity = new byte[4];

            public string MsgID { get => Code.ToStr(); }
            public string CoilNoID { get => Dummy_Coil_No.ToStr(); }
            public string DummyCoilThick { get => Dummy_Coil_Thick.ToStr(); }
            public string DummyCoilWidth { get => Dummy_Coil_Width.ToStr(); }
            public string DummyCoilWeight { get => Dummy_Coil_Weight.ToStr(); }
            public string DummyCoilLength { get => Dummy_Coil_Length.ToStr(); }
            public string DummyCoilInner { get => Dummy_Coil_Inner.ToStr(); }
            public string DummyCoilDiam { get => Dummy_Coil_Diam.ToStr(); }
            public string GradeCode { get => SteelGradeCode.ToStr(); }
            public string GradeSign { get => SteelGradeSign.ToStr(); }
            public string Density { get => Dens_ity.ToStr(); }
        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_of_Dummy_Coil_List_Req
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
            public byte[] Dummy_Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Dummy_Deal_Reult;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] Reason;

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_OP_Plan_Req_Del
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
            public byte[] plan_no;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] OperatorId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ReasonCode;

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Package_Cmd
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
            public byte[] COIL_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] PACK_TYPE_BW;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PACK_PLAN_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] ORDER_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] SG_SIGN;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] ST_no;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] UNIT_CODE;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] DELIVY_DATE;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PLAN_FIN_DATE;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] MAT_ACT_THICK;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] MAT_ACT_WIDTH;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] MAT_ACT_LEN;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] MAT_ACT_WT;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] MAT_GROSS_WT;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] RG_Surface_Direction;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public byte[] SG_STD;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] QUALITY_GRADE;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SURFACE_ACCURACY;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] TRADE_CODE;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] TRIM_FLAG;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PSC;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] HEAT_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] ORDER_THICK;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] ORDER_WIDTH;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] PAPER_FLAG;

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Package_Material_Del_Req
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
            public byte[] FLAG;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO;

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_UnPackage_Plan
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
            public byte[] FLAG;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO;

        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_UnPackage_Cmd
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
            public byte[] COIL_NO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO;

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_Coil_Reject_Result
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

            public string MsgID { get => Code.ToStr(); }
            public string RequestedCoilNo { get => Requested_Coil_No.ToStr(); }
            public string ProcessResult{ get => Process_Result.ToStr(); }
            public string RejectCause { get => Reject_Cause.ToStr(); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Product_Result_Request
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

            public string MsgID { get => Code.ToStr(); }
            public string CoilNoID { get => Coil_No.ToStr().ClearSpaceStr(); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Req_Delete_Schedule_Plan
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Operator_Id;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reason_Code;

            public string MsgID { get => Code.ToStr(); }
            public string PlanNo { get => Plan_No.ToStr(); }
            public string OperatorID { get => Operator_Id.ToStr(); }
            public string ReasonCode { get => Reason_Code.ToStr(); }

        }



        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_RcvPDO
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
            public byte[] Respon_Coil_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Respon_Plan_No;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Success_Flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Error_Reason;

            public string MsgID { get => Code.ToStr(); }
            public string ResponCoil_No { get => Respon_Coil_No.ToStr(); }
            public string ResponPlan_No { get => Respon_Plan_No.ToStr(); }
            public string SuccessFlag { get => Success_Flag.ToStr(); }
            public string ErrorReason { get => Error_Reason.ToStr(); }
        }

    }
}