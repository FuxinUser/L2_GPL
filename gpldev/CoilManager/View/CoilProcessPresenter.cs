using System;
using CoilManager.View;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/03
* Description: Coil UI 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace CoilProcess.View
{
    public class CoilProcessPresenter : CoilProcessContract.IPresenter
    {

        protected void Invoke(Action action) => View.Invoke(action);

        private CoilProcessContract.IView View { get; set; }



      
        public void SetView(BaseFormContract.IView view)
        {
            View = (CoilProcessContract.IView)view;
        }
    }
}
