using System.Windows.Forms;

namespace WinformsMVP.Core
{
    public static class ControlUtils
    {
        /// <summary>
        /// 判斷是否跨執行續處理，若跨執行緒處理則進行處理。
        /// </summary>
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            // 確認目前是否跨執行緒
            if (control.InvokeRequired)
            {
                // 已跨執行緒
                control.Invoke(action);
                return;
            }

            action();
        }
    }
}
