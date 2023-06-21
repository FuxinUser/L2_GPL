using LogRecord.Form;
using System;
using System.Windows.Forms;

namespace LogRecord
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
            var form = new LogForm();
            new LogPresenter(form);
            Application.Run(form);
        }
    }
}
