using Akka.Actor;
using Core.Help;
using DataSetup.Actor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/25
* Description: 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace DataSetup.View
{
    public class DtStpPresenter : DtStpContract.IPresenter
    {
        protected void Invoke(Action action) => View.Invoke(action);

   
        private DtStpContract.IView View { get; set; }

      
        public void SetView(BaseFormContract.IView view)
        {
            View = (DtStpContract.IView)view;
        }
    }
}
