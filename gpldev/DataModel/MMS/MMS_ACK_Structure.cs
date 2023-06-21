using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.MES
{
    /// <summary>
    /// 底層應答
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MMS_ACK_Structure
    {
        [MarshalAs(UnmanagedType.Struct)]
        public MMS_Header_Structure Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] Control;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] End;

    }
}
