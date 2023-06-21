using System;

namespace Controller
{
    public static class InvaildData
    {
        public static void VaildStrNullOrEmpty(this string vaildTxt, string vaildTxtName, string describe)
        {
            if (string.IsNullOrEmpty(vaildTxt))
                throw new ArgumentNullException(describe + "," + vaildTxtName + "參數為Null或Empty");
        }

        public static void VaildObjectNull(this object vaildObject, string vaildObjectName, string describe)
        {
            if (vaildObject == null)
                throw new ArgumentNullException(describe + "," + vaildObjectName + "參數為Null或Empty");
        }

        public static void VaildIntValueZero(this int vaildInt, string vaildIntName, string describe)
        {
            if (vaildInt == 0)
                throw new ArgumentNullException(describe + "," + vaildIntName + "參數為0");
        }

    }
}
