using System;
using System.Runtime.InteropServices;

namespace DataMod.PLC
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PLCHeader
    {
        [MarshalAs(UnmanagedType.I2)]
        public short MessageLength;
        [MarshalAs(UnmanagedType.I2)]
        public short MessageId;   
    }
}
