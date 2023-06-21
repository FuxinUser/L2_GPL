using System;
using System.Runtime.InteropServices;

namespace DataMod.BarCode
{

    public class BCSModel
    {

        // Baccode機 -> Server
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public class BCSScanCoil_BS01
        {
            [MarshalAs(UnmanagedType.Struct)]
            public BCSHeader Header;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] CoilNo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] CoilPos;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public class BCSClose_BS02
        {
            [MarshalAs(UnmanagedType.Struct)]
            public BCSHeader Header;
        }


        // Server -> Baccode機
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public class CompareScnResult_SB01
        {
            [MarshalAs(UnmanagedType.Struct)]
            public BCSHeader Header;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] Result;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] CoilNo;
        }

        // Header
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public class BCSHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Message_Id; //

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Message_Length; //

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] Message_DateTime; // DateTime.Now.ToString("yyyyMMddHHmmss").ToCharArray()
        }


      

    }
}
