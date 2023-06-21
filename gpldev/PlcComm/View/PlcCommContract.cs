using System;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/09/19
* Description: PLC UI View 與 Presenter的溝通管道
* Reference: 
* Modified: 
*/
namespace WMSComm.View
{
    public class PlcCommContract
    {

        public interface IView : BaseFormContract.IView
        {
           
        }

        public interface IPresenter : BaseFormContract.IPresenter
        {
        
        }       
    }
}
