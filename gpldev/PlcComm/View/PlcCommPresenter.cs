using Akka.Actor;
using System;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/09/19
* Description: PLC UI 邏輯組裝區
* Reference: 
* Modified: 
*/

namespace WMSComm.View
{
    public class PlcCommPresenter : PlcCommContract.IPresenter
    {
        protected void Invoke(Action action) => View.Invoke(action);

        private PlcCommContract.IView View { get; set; }


        public void SetView(BaseFormContract.IView view)
        {
            View = (PlcCommContract.IView)view;
        }

    }

}
