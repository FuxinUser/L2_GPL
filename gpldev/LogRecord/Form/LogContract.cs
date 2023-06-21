using DBService.Repository.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsMVP.Controls.Forms;


/**
 * Author: ICSC余士鵬
 * Date: 2019/10/25
 * Description:  Presenter的溝通管道
 * Reference: 
 * Modified: 
 */

namespace LogRecord.Form
{
    public class LogContract
    {
        public interface IView : IBaseFormView<IPresenter>
        {
            void DisplayLog(EventLogEntity.TBL_EventLog item);

            void DisplayHMILog(EventLogEntity.TBL_EventLog item);
        }

        public interface IPresenter : IBaseFormPresenter
        {
            void MgrActorTell(Object message);
            void InsertLog(EventLogEntity.TBL_EventLog item);
      
        }

    }
}
