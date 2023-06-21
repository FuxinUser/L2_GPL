namespace WinformsMVP.Controls.Forms.Base
{
    public class BaseFormContract 
    {
        public interface IView : IBaseFormView<IPresenter>
        { 
        
        }
        

        public interface IPresenter : IBaseFormPresenter
        {
         
        }
    }
}
