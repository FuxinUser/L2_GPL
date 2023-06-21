using Akka.Actor;
using AkkaSysBase;
using Autofac;
using Core.Util;
using PLCComm.Actor;
using PLCComm.Config;
using System.Diagnostics;
using System.Windows.Forms;
using WMSComm.View;


/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: 程式啟動程序
 * Reference: 
 * Modified: 
 */
namespace PLCComm
{
    public class Bootstrapper
    {   
        // DI 管理器
        private readonly IContainer _sysContainer = AutofacConfig.SysInject();

        public Bootstrapper()
        {
        }

        /// <summary>
        /// 啟動程式
        /// </summary>
        public void Run()
        {          
            StarFormApp();   
        }

        /// <summary>
        /// 啟用WindwForm
        /// </summary>
        private void StarFormApp()
        {          
            
            var form = CreateMainForm();
            form.FormClosing += (o, e) =>
            {               
                ProcessUtils.KillProcessAndChildren(Process.GetCurrentProcess().Id);
            };
            form.Load += (o, e) => StarAkkaSys();
            Application.Run(form);        
        }

        /// <summary>
        /// 建立與外部連結系統並啟動
        /// </summary>
        private void StarAkkaSys()
        {
            // 建立與外部連結系統
            var akkaSys = _sysContainer.Resolve<ActorSystem>();

            // 註冊物件
            akkaSys.UseAutofac(_sysContainer);
       
            // 建立Manager系統
            var akkManager = _sysContainer.Resolve<ISysAkkaManager>();
            akkManager.CreateActor<PlcMgr>();         
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<PlcCommForm>();        
            return form;
        }

    }
}
