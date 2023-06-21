using Akka.Actor;
using System;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/03
* Description: WMS UI 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace WMSComm.View
{
    public class WMSPresenter : WMSContract.IPresenter
    {
        protected void Invoke(Action action) => View.Invoke(action);

       
        private WMSContract.IView View { get; set; }

    
        public void SetView(BaseFormContract.IView view)
        {
            View = (WMSContract.IView)view;
        }
    }
}
