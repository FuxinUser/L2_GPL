using Akka.Actor;
using Akka.Event;
using Autofac;
using Core.Util;
using System;
using AkkaSysBase;
using PcComm.View;
using LogSender;

namespace PcComm.Config
{
    public static class AutofacConfig
    {

        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {

       
            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

          
            // 註冊Form
            _builder.RegisterFrame<ClientCoordinatorContract.IPresenter, ClientCoordinatorPresenter, ClientCoordinatorContract.IView, ClientCoordinatorForm1>();


            // 註冊ActorSys
            _builder.Register(c =>
            {
                return ActorSystem.Create(c.Resolve<AppSetting>().AkkaSysName);
            }).SingleInstance();

            //註冊ActorManager
            _builder.RegisterSelfSingleton<ISysAkkaManager, SysAkkaManager>();

            //註冊MgrActor
            _builder.Register(c =>
            {
                return new PCcom(
                      c.Resolve<ISysAkkaManager>(),                    
                      c.GetLog<PCcom>(a => a.MgrLog)); 
            })  .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();
           
            var container = _builder.Build();
            return container;
        }


        private static ILog GetLog<T>(this IComponentContext context, Func<AppSetting, string> func)
        {
            return new Log(
                LogDef.SysServer,
                typeof(T).Name,
                Logging.GetLogger(context.Resolve<ActorSystem>(), func?.Invoke(context.Resolve<AppSetting>()))
            );
        }
    }
}
