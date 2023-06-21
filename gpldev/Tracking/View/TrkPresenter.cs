using Akka.Actor;
using Core.Help;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Tracking.Actor;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/25
* Description: 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace Tracking.View
{
    public class TrkPresenter : TrkContract.IPresenter
    {

        protected void Invoke(Action action) => View.Invoke(action);

    
        private TrkContract.IView View { get; set; }

    

        public void SetView(BaseFormContract.IView view)
        {
            View = (TrkContract.IView)view;
        }
    }
}
