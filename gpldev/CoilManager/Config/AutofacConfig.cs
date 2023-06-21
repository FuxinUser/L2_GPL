using Akka.Actor;
using Akka.Event;
using Autofac;
using Core.Util;
using System;
using Controller.Track;
using Controller.Coil;
using CoilManager.Service;
using CoilManager.View;
using CoilProcess.View;
using AkkaSysBase;
using Controller.Sys;
using CoilManager.Actor;
using LogSender;

namespace CoilManager.Config
{
    public static class AutofacConfig
    {

        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<ITrackingController, TrackingController>();
            _builder.RegisterSelfSingletonProp<ICoilController, CoilController>();
            _builder.RegisterSelfSingletonProp<ISysController, SysController>();
            _builder.RegisterSelfSingletonProp<AggregateService>();
        
            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

          
            // 註冊Form
            _builder.RegisterFrame<CoilProcessContract.IPresenter, CoilProcessPresenter, CoilProcessContract.IView, CoilProcessForm>();


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
                return new CoilMgr(
                      c.Resolve<ISysAkkaManager>(),
                      c.Resolve<ITrackingController>(),
                      c.Resolve<ICoilController>(),
                      c.Resolve<ISysController>(),
                      c.GetLog<CoilMgr>(a => a.MgrLog)); 
            })  .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();


            //註冊ProActor
            _builder.Register(c =>
            {
                return new CoilProActor(
                      c.Resolve<ISysAkkaManager>(),
                      c.Resolve<ICoilController>(),
                      c.Resolve<ICoilController>(),
                      c.GetLog<CoilProActor>(a => a.CoilProLog));
            }).PropertiesAutowired()
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
