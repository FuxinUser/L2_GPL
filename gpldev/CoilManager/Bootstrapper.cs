using Akka.Actor;
using AkkaSysBase;
using Autofac;
using CoilManager.Actor;
using CoilManager.Config;
using CoilManager.View;
using Core.Util;
using System.Diagnostics;
using System.Windows.Forms;

namespace CoilManager
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

            akkManager.CreateActor<CoilProActor>();
            akkManager.CreateActor<CoilMgr>();
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<CoilProcessForm>();        
            return form;
        }

    }
}
