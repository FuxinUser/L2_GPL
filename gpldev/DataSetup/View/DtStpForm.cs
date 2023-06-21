using Core.Define;
using Core.Help;
using System;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

namespace DataSetup.View
{
    public partial class DtStpForm : BaseForm, DtStpContract.IView
    {
        public DtStpContract.IPresenter Presenter { get; set; }

        public DtStpForm(DtStpContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }

        private void DtStpForm_Shown(object sender, EventArgs e)
        {
       
        }        
        
        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}
