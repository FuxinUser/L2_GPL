using Core.Define;
using Core.Util;
using DataModel.HMIServerCom.Msg;
using NUnit.Framework.Internal.Builders;
using System;
using System.Runtime.InteropServices;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace MsgStruct
{
    public class WMS_L2_Rcv
    {


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class WGx1_CompleteOfFeeding
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
            public char[] SkidNo;
       
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] BarCodeCoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] Spare;

            public string CoilNoID { get=> new string(CoilNo).Trim();}

            public string SkidNoID { get => new string(SkidNo).Trim(); }

            public string ScanNoID { get => new string(BarCodeCoilNo).Trim(); }

           
        
            public L2SystemDef.SKPOS GetEntryPOS()
            {
                if (SkidNo.Equals(WMSSysDef.SkPos.ESK01))
                    return L2SystemDef.SKPOS.Entry_SK01;
       
                return L2SystemDef.SKPOS.EntryTOP;
            }
            public L2SystemDef.SKPOS GetDeliveryPOS()
            {
                if (SkidNo.Equals(WMSSysDef.SkPos.ESK01))
                    return L2SystemDef.SKPOS.Delivery_SK01;

                return L2SystemDef.SKPOS.DeliveryTop;
            }

            public CoilSkPosition CoilSKPos()
            {

                CoilSkPosition pos;

                switch (SkidNo.ToStr())
                {
                    case "1":
                        pos = CoilSkPosition.ESK01;
                        break;

                    default:
                        pos = CoilSkPosition.ETOP;
                        break;
                }

                return pos;
            }

            public int L1202PresetPos { get
                {
                    var pos = -1;

                    switch (SkidNoID)
                    {
                        case WMSSysDef.SkPos.ETOP:
                            pos = PlcSysDef.Pos.Preset202ETOP;
                            break;

                        case WMSSysDef.SkPos.ESK01:
                            pos = PlcSysDef.Pos.Preset202SK1;
                            break;
                    }


                    return pos;

                } }

        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public class WGx3_RequestResponse
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
            public char[] PosFlag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ProcessFlag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ReasonCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Reason;

            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            //public char[] Spare;

            public string ActionResultStr
            {
                get {

                    var actionResult = string.Empty;

                    switch (new string(ProcessFlag))
                    {
                        case "0":
                            actionResult = "回絕";
                            break;

                        case "1":
                            actionResult = "接受";
                            break;
                    }

                    return actionResult;
                }
            }

            public string PositionStr
            {
                get
                {

                    var actionResult = string.Empty;

                    switch (new string(PosFlag))
                    {
                        case "1":
                            actionResult = "入料";
                            break;

                        case "2":
                            actionResult = "出料";
                            break;

                        case "3":
                            actionResult = "退料";
                            break;
                    }

                    return actionResult;
                }
            }

        }



    }
}