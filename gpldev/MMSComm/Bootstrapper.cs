using Akka.Actor;
using AkkaSysBase;
using Autofac;
using Core.Util;
using MMSComm.Actor;
using MMSComm.Config;
using MMSComm.View;
using System.Diagnostics;
using System.Windows.Forms;

namespace MMSComm
{
    public class Bootstrapper
    {
        private readonly IContainer _sysContainer = AutofacConfig.SysInject();

        private Timer Timer { get; set; }

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
            akkManager.CreateActor<MMSMgr>();
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<MMSFrom>();
            return form;
        }

    }
}
