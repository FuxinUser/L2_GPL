using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static WinformsMVP.Controls.Forms.Base.BaseFormContract;

namespace WinformsMVP.Controls.Forms
{
    public interface IBaseFormPresenter
    {
        /// <summary>
        /// 設定View
        /// </summary>
        void SetView(IView view);
    }
}
