using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinformsMVP.Controls.Forms
{
    public interface IBaseFormView<in P> where P : IBaseFormPresenter
    {
        /// <summary>
        /// 發生於表單已關閉時。
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        ///  發生在表單第一次顯示之前。
        /// </summary>
        event EventHandler Load;

        /// <summary>
        ///  發生在表單顯示。
        /// </summary>
        event EventHandler Shown;

        /// <summary>
        /// 取得非同步的方式來更新UI
        /// </summary>
        IAsyncResult BeginInvoke(Delegate method);

        /// <summary>
        /// 同步的方式來更新UI
        /// </summary>        
        object Invoke(Delegate method);

        void SetPresenter(P presenter);
    }
}
