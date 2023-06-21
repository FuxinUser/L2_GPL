using Core.Handle;
using System.Drawing;
using System.Windows.Forms;

namespace GPLManager
{
    public class DialogHandler
    {
        /// <summary> LogHandler 物件 </summary>
        private readonly LogHandler _log = null;

        ///// <summary> LanguageHelper 物件 </summary>
        //private LanguageHelper _LanguageHelp = null;

        /// <summary>
        ///     建構子
        /// </summary>
        public DialogHandler(LogHandler log = null)
        {
            _log = log ?? new LogHandler();
            //_LanguageHelp = new LanguageHelper();
        }

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly DialogHandler INSTANCE = new DialogHandler();
        }

        public static DialogHandler Instance { get { return SingletonHolder.INSTANCE; } }


        #region Dialog Function

        #endregion
        /// <summary>
        /// DialogOK(0:Information ,1:Question ,2:Warning ,3:Error ,4:CheckOK)
        /// </summary>
        /// <param name="message">內容</param>
        /// <param name="title">標題</param>
        /// <param name="image">圖示</param>
        /// <param name="intStatus">狀態顏色(0:Information ,1:Question ,2:Warning ,3:Error;4:CheckOK) </param>
        /// <returns></returns>
        public DialogResult Fun_DialogShowOk(string message, string title, Image image = null, int intStatus = 0)
        {
            if (image == null)
            {
                switch (intStatus)
                {
                    case 0:
                        image = Properties.Resources.dialogInformation;
                        break;
                    case 1:
                        image = Properties.Resources.dialogQuestion;
                        break;
                    case 2:
                        image = Properties.Resources.dialogWarning;
                        break;
                    case 3:
                        image = Properties.Resources.dialogCancel;
                        break;
                    case 4:
                        image = Properties.Resources.dialogCheck;
                        break;

                    default:
                        image = Properties.Resources.dialogInformation;
                        break;
                }

            }

            Frm_DialogOK frm_D = new Frm_DialogOK();
            frm_D.DialogShow(message, title, image, intStatus);
            frm_D.ShowDialog();
            frm_D.Dispose();
            return frm_D.DialogResult;
        }

        /// <summary>
        /// Frm_DialogOKCancel(0:Information ,1:Question ,2:Warning ,3:Error;4:CheckOK)
        /// </summary>
        /// <param name="message">內容</param>
        /// <param name="title">標題</param>
        /// <param name="image">圖示</param>
        /// <param name="intStatus">狀態顏色(0:Information ,1:Question ,2:Warning ,3:Error;4:CheckOK) </param>
        /// <returns></returns>
        public DialogResult Fun_DialogShowOkCancel(string message, string title, Image image = null, int intStatus = 0)
        {
            Frm_DialogOKCancel frm_D = new Frm_DialogOKCancel();
            frm_D.DialogShow(message, title, image, intStatus);
            frm_D.ShowDialog();
            frm_D.Dispose();
            return frm_D.DialogResult;
        }

        public DialogResult Fun_DialogEndFlagCheck(string message, string title, Image image = null, int intStatus = 0)
        {
            Frm_DialogEndFlagCheck frm_D = new Frm_DialogEndFlagCheck();
            frm_D.DialogShow(message, title, image, intStatus);
            frm_D.ShowDialog();
            frm_D.Dispose();
            return frm_D.DialogResult;
        }

        public DialogResult Fun_DialogShowSelectOk(string message, string title, string strBtn1 = "", string strBtn2 = "", Image image = null, int intStatus = 1)
        {
            Frm_DialogSelectOK frm_D = new Frm_DialogSelectOK();
            frm_D.DialogShow(message, title, "", "", image, intStatus);
            frm_D.ShowDialog();
            frm_D.Dispose();
            return frm_D.DialogResult;
        }
    }
}
