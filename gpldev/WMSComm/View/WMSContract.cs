using WinformsMVP.Controls.Forms.Base;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/09
 * Description:  WMS View 與 Presenter的溝通管道
 * Reference: 
 * Modified: 
 */

namespace WMSComm.View
{
    public class WMSContract
    {

        public interface IView : BaseFormContract.IView
        {
           
        }


        public interface IPresenter : BaseFormContract.IPresenter
        {
        
        }

    }
}
