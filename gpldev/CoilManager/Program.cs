﻿using System;
using System.Windows.Forms;

namespace CoilManager
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

            //啟動
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}