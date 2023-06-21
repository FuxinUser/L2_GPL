using Akka.Actor;
using System;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/03
* Description: MES UI 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace MMSComm.View
{
    public class MESPresenter : MESContract.IPresenter
    {
        protected void Invoke(Action action) => View.Invoke(action);


        private MESContract.IView View { get; set; }


        public void SetView(BaseFormContract.IView view)
        {
            View = (MESContract.IView)view;
        }
    }
}
