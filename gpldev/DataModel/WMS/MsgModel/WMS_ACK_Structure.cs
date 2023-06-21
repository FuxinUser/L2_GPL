using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataModel.WMS
{
    /// <summary>
    /// 底層應答
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class WMS_ACK_Structure
    {
        [MarshalAs(UnmanagedType.Struct)]
        public WMS_Header_Structure Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public char[] Blank;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public char[] AcceptedOrNot;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] ReceivedMsgID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] ErrorCode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] Blank2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
        public char[] ErrorMsg;
    }
}
