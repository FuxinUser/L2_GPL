using System;
using System.Runtime.InteropServices;
namespace MsgStruct
{
	public class Msg_MMS_Rcv	
{[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_CPL_PDI { 
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
    public char[]  Plan_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Mat_Seq_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  Plan_Sort;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Entry_Coil_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Entry_Coil_Thick;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Entry_Coil_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Entry_Coil_Weight;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Entry_Coil_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Entry_Coil_Inner;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Entry_Coil_Dcos;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Sleeve_Type_Entry;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Sleeve_Inner_Entry;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Paper_Entry_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  In_Paper_Type_Entry;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  In_Paper_Head_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  In_Paper_Head_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  In_Paper_Tail_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  In_Paper_Tail_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  TS_STAND_MAX;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  TS_STAND_MIN;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  St_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Density;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Rework_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SurfaceFinishingCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Surface_Accuracy_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Base_Surface;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Uncoiler_Direction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  Out_Mat_No;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  PaperExitCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  In_Paper_Type_Exit;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Sleeve_Inner_Exit;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Sleeve_Type_Exit;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Out_strap_num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Leader_Usage;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  SAMP_FLAG;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  SAMPLE_FRQN_CODE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  sample_lot_no;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  CoilOrigin;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  wholebacklog_code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Next_wholebacklog_code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Trim_flag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Out_mat_width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  out_mat_width_max;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Out_mat_width_min;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Out_mat_thickness;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Out_Coil_Inner;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_no;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  order_wt_max;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  order_wt_min;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Order_wt;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  DividingFlag;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  Dividing_num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Orderwt_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Orderwt_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Orderwt_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Orderwt_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Orderwt_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Orderwt_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No_1;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No_2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No_3;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No_4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No_5;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  Order_No_6;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[]  Test_plan_no;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  Leader_Code;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 500)]
    public char[]  Qc_Remark;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  HEAD_OFF_GAUGE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Tail_off_gauge;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  PRE_GRINDING_SURFACE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  GRINDING_COUNT_OUT;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  GRINDING_COUNT_IN;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  APPOINT_GRINDING_SURFACE;
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
public class Msg_Coil_Schedule { 
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
    public char[]  CoilNo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[]  NumberOfCoils;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  EntryCoilNo;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Res_For_No_Coil_Schedule { 
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
public class Msg_Res_For_No_Coil_PDI { 
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
public class Msg_Dummy_Coil_List { 
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
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  Dummy_Coil_Thick;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  Dummy_Coil_Width;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Dummy_Coil_Weight;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  Dummy_Coil_Length;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Dummy_Coil_Inner;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  Dummy_Coil_Diam;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[]  SteelGradeSign;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  SteelGradeCode;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Res_of_Dummy_Coil_List_Req { 
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
public class Msg_OP_Plan_Req_Del { 
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
    public char[]  plan_no;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  OperatorId;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  ReasonCode;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Package_Cmd { 
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
    public char[]  COIL_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  PACK_TYPE_BW;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  PACK_PLAN_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  ORDER_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[]  SG_SIGN;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  ST_no;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public char[]  UNIT_CODE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  DELIVY_DATE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  PLAN_FIN_DATE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  MAT_ACT_THICK;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  MAT_ACT_WIDTH;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  MAT_ACT_LEN;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  MAT_ACT_WT;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public char[]  MAT_GROSS_WT;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  RG_Surface_Direction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public char[]  SG_STD;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  QUALITY_GRADE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  SURFACE_ACCURACY;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public char[]  TRADE_CODE;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  TRIM_FLAG;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  PSC;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  HEAT_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public char[]  ORDER_THICK;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public char[]  ORDER_WIDTH;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public char[]  PAPER_FLAG;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_Package_Material_Del_Req { 
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
    public char[]  FLAG;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  PLAN_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  COIL_NO;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_UnPackage_Plan { 
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
    public char[]  FLAG;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  PLAN_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public char[]  COIL_NO;

}
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class Msg_UnPackage_Cmd { 
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
    public char[]  COIL_NO;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public char[]  PLAN_NO;

}

}
}