using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/08
* Description: UI View 與 Presenter的溝通管道
* Reference: 
* Modified: 
*/
namespace PcComm.View
{
    public class ClientCoordinatorContract
    {
        public interface IView : BaseFormContract.IView
        {     
       
        }
        public interface IPresenter : BaseFormContract.IPresenter
        {
           
        }       
    }
}
