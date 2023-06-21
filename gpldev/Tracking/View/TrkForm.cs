using Core.Define;
using Core.Help;
using System;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

namespace Tracking.View
{
    public partial class TrkForm : BaseForm, TrkContract.IView
    {
        public TrkContract.IPresenter Presenter { get; set; }

        public TrkForm(TrkContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }

        private void TrkForm_Shown(object sender, EventArgs e)
        {
                          
        }
      
        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}
