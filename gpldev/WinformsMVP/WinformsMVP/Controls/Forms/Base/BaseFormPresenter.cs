using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinformsMVP.Controls.Forms.Base;

namespace WinformsMVP.Controls.Forms
{
    /// <summary>
    /// BaseFormPresenter support for view、presenter and auto bind view.
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="P"></typeparam>
    public abstract class BaseFormPresenter<V, P> : IBaseFormPresenter
         where V : IBaseFormView<P>
         where P : IBaseFormPresenter
    {
        protected V View { get; private set; }

        public BaseFormPresenter(V view)
        {
            View = view;
            //View.Presenter = ((P)(object)this);
            View.SetPresenter((P)(object)this);
            // 註冊Form的事件
            View.Load += View_Load;
            View.Closed += View_Closed;
            View.Shown += View_Shown;
        }

        protected virtual void View_Closed(object sender, EventArgs e)
        {
            // 選擇性實作
        }

        protected virtual void View_Load(object sender, EventArgs e)
        {
            // 選擇性實作
        }

        protected virtual void View_Shown(object sender, EventArgs e)
        {
            // 選擇性實作
        }

        /// <summary>
        /// 更新UI
        /// </summary>        
        protected void Invoke(Action action)
        {
            View.Invoke(action);
        }

        public void SetView(BaseFormContract.IView view)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// BaseFormPresenter support for model、view、presenter and auto bind model.
    /// </summary>
    /// <typeparam name="M">Model</typeparam>
    /// <typeparam name="V">View</typeparam>
    /// <typeparam name="P">Presenter</typeparam>
    public abstract class BaseFormPresenter<M, V, P> : BaseFormPresenter<V, P>
         where M : IBaseModel
         where V : IBaseFormView<P>
         where P : IBaseFormPresenter
    {
        protected M Model { get; private set; }

        public BaseFormPresenter(V view, M model) : base(view)
        {
            Model = model;
        }
    }
}
