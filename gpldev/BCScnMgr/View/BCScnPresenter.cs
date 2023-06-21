using System;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/25
* Description: BCSScn 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace BCScnMgr.View
{
    public class BCScnPresenter : BCScnContract.IPresenter
    {

        protected void Invoke(Action action) => View.Invoke(action);

        private BCScnContract.IView View { get; set; }



        public void SetView(BaseFormContract.IView view)
        {
            View = (BCScnContract.IView)view;
        }
    }
}
