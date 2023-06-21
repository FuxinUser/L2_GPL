using System;
using System.Runtime.InteropServices;
namespace MsgStruct
{
	public class Msg_MMS_Snd	
{[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_CPL_PDO { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Plan_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Out_mat_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  In_mat_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Starttime;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Finishtime;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Shift;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Team;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Out_mat_Outer_Diameter;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  out_mat_inner;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Out_mat_wt;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Out_mat_gs;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Out_mat_Thick;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Out_mat_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Out_mat_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Out_Paper_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Out_Paper_Exit_CODE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Out_Paper_Head_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Out_Paper_Head_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Out_Paper_Tail_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Out_Paper_Tail_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Sleeve_Inner_Exit;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Sleeve_Type_Exit;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Head_PunchHole_Position;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Head_LeaderLength;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Head_Leader_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Head_Leader_Thickness;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Tail_PunchHole_Position;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Tail_LeaderLength;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Tail_Leader_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Tail_Leader_Thickness;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Scraped_Length_Entry;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Scraped_Length_Exit;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Head_Leader_St_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Tail_Leader_St_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Curly_Direction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Base_Surface;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Inspector;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Hold_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Hold_Cause_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Sample_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Trim_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Segement_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  End_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Scrap_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Sample_Frqn_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  No_Leader_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Surface_Accuracy_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Head_Off_Gauge;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Tail_Off_Gauge;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Pre_Grinding_Surface;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Grinding_Count_Out;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Grinding_Count_In;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Appoint_Grinding_Surface;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Sid_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_1_Width_Direction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_1_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_1_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_2_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_2_LengthStart_Direction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_2_LengthEnd_Direction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_3_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_3_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_3_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_4_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_4_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_4_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_5_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_5_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_5_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  DefectPercent_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_6_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_6_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_6_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_7_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_7_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_7_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  DefectCode_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  DefectOrigin_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_8_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_8_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_8_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_9_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_9_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_9_LengthEndDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Code_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Origin_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Side_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Position_10_WidthDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_10_LengthStartDirection;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Defect_Position_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_Level_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Defect_Percent_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Defect_QL_10;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Req_Coil_Schedule { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Requested_Coil_No;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Request_Coil_PDI { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Requested_Coil_No;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Coil_Schedule_Changed { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Number_Of_Coils;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Entry_Coil_No;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Res_For_Coil_PDI { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  RequestedCoilNo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  ProcessResult;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
    public char[]  RejectCause;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Res_For_Coil_Plan_Delete { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  RequestedCoilNo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  ProcessResult;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
    public char[]  RejectCause;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Coil_Schedule_Delete { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  EntryCoilNo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  OperatorId;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  ReasonCode;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Dummy_Coil_Result_Req { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Dummy_Coil_No;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Dummy_Coil_Result_Del { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Dummy_Coil_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  ReasonOfDelete;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  TimeOfDelete;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Pack_Order_Adj { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  NumberOfCoils;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  EntryCoilNo;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Package_Material_Del_Res { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  DEL_FLAG;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  PLAN_FLAG;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  PLAN_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  COIL_NO;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_UnPacking_Order_Adj { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  NumberOfCoils;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  EntryCoilNo;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Packing_PDI_Req { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  NumberOfCoils;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Coil_Reject_Result { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Reject_Coil_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Entry_CoilNo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Plan_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Mode_Of_Reject;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Length_Of_Rejected_Coil;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Weight_Of_Rejected_Coil;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Inner_Diameter_Of_RejectedCoil;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Outer_Diameter_Of_RejectedCoil;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Reason_Of_Reject;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Time_Of_Reject;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Shift_Of_Reject;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Turn_Of_Reject;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Paper_exit_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Paper_Type;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Coil_Loaded_Skid { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Plan_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Entry_Coil_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Loaded_Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Unit_Code;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Equipment_Down_Result_Msg { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Op_Flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Unit_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Prod_Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Prod_Shift_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Prod_Shift_Group;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Stop_Start_Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Stop_End_Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Delay_Location;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
    public char[]  Delay_Location_Desc;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Stop_Elased_Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Delay_Reason_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[]  Delay_Reason_Desc;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
    public char[]  Delay_Remark;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_m;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_e;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_c;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_p;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_u;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_o;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_r;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public char[]  Resp_Depart_Delay_Time_rs;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Energy_Consumption_Info { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Shift_Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Shift_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Group_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Unit_code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_7_;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  PW_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DCW_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IDCW_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  DeW_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NG_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  CA_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Steam_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  N2_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  NH3_G_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  Electrical_Total_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  IW_15;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_9;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_10;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_11;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_12;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_13;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_14;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public char[]  F_ire_W_15;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Packaging_Real_Parameter { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Coil_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Pack_Plan_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Pack_Type_Code_ACT;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public char[]  Finish_Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Mat_Act_WT;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Mat_Gross_WT;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Mat_Weight;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_UnPackaging_Real_Parameter { 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Date;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Time;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SendWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  RcvWho;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  FuncCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Coil_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Pack_Plan_No;

}

}
}