using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/25
 * Description:  BCSScn View與 Presenter的溝通管道
 * Reference: 
 * Modified: 
 */

namespace BCScnMgr.View
{
    public class BCScnContract
    {
        public interface IView : BaseFormContract.IView
        {

        }



        public interface IPresenter : BaseFormContract.IPresenter
        {
          
        }

    }
}
