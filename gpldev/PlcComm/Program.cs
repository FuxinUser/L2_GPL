using System;
using System.Windows.Forms;

namespace PLCComm
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 啟動程式程序
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

        }
    }
}
