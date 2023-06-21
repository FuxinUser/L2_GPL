using System;
using System.Runtime.InteropServices;

namespace DataModel.MES
{
    /// <summary>
    /// 心跳電文
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MMS_Heartbeat_Structure
    {
        [MarshalAs(UnmanagedType.Struct)]
        public MMS_Header_Structure Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] End;
    }
}
