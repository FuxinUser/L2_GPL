using System;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/25
* Description: 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace LabelPrint.View
{
    public class LprPresenter : LprContract.IPresenter
    {
        protected void Invoke(Action action) => View.Invoke(action);

        private LprContract.IView View { get; set; }

        public void SetView(BaseFormContract.IView view)
        {
            View = (LprContract.IView)view;
        }    
    }
}
