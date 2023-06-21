using Akka.Actor;
using AkkaSysBase;
using Autofac;
using Core.Util;
using LabelPrint.Actor;
using LabelPrint.Config;
using LabelPrint.View;
using System.Diagnostics;
using System.Windows.Forms;

namespace LabelPrint
{
    public class Bootstrapper
    {
        private readonly IContainer _sysContainer = AutofacConfig.SysInject();

       
        public Bootstrapper()
        {
        }

        // 啟動程式
        public void Run()
        {
            StarFormApp();
        }

        private void StarFormApp()
        {
            // Create Form
            var form = CreateMainForm();
            form.FormClosing += (o, e) =>
            {
                ProcessUtils.KillProcessAndChildren(Process.GetCurrentProcess().Id);
            };
            form.Load += (o, e) => StarAkkaSys();
            Application.Run(form);
        }

        private void StarAkkaSys()
        {
            // Create Actor Sys
            var akkaSys = _sysContainer.Resolve<ActorSystem>();
            akkaSys.UseAutofac(_sysContainer);
            // Create Actor Manager
            var akkManager = _sysContainer.Resolve<ISysAkkaManager>();
            akkManager.CreateActor<LprMgr>();
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<LprForm>();
            return form;
        }

    }
}
