using System;
using WinformsMVP.Controls.Forms.Base;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/25
 * Description:  Presenter的溝通管道
 * Reference: 
 * Modified: 
 */

namespace DataSetup.View
{
    public class DtStpContract
    {
        public interface IView : BaseFormContract.IView
        {
         
        }

        public interface IPresenter : BaseFormContract.IPresenter
        {
          
        }

    }
}
