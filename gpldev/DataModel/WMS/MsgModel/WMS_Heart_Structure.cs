using DataModel.WMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataMod.WMS.MsgModel
{
    /// <summary>
    /// 心跳電文
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class WMS_Heart_Structure
    {
        [MarshalAs(UnmanagedType.Struct)]
        public WMS_Header_Structure Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
        public byte[] Text;
    }
}
