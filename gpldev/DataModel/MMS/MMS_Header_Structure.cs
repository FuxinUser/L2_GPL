using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.MES
{
    /// <summary>電文標頭</summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MMS_Header_Structure
    {
        /// <summary>電文長度</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Length;

        /// <summary>電文號</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Code;

        /// <summary>發送日期</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Date;

        /// <summary>發送時間</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Time;

        /// <summary>發送端描述碼</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] SendWho;

        /// <summary>接收端描述碼</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] RcvWho;

        /// <summary>傳送功能碼</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] FuncCode;
    }
}
