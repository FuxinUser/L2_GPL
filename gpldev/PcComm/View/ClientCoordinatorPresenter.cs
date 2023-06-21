using System;
using WinformsMVP.Controls.Forms.Base;

namespace PcComm.View
{
    class ClientCoordinatorPresenter : ClientCoordinatorContract.IPresenter
    {
        protected void Invoke(Action action) => View.Invoke(action);


        private ClientCoordinatorContract.IView View { get; set; }

        public void SetView(BaseFormContract.IView view)
        {
            View = (ClientCoordinatorContract.IView)view;
        }
    }
}
