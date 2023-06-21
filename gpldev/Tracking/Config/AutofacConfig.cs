using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Autofac;
using Core.Util;
using System;
using Tracking.View;
using Tracking.Service;
using Tracking.Actor;
using Controller.Track;
using Controller.Coil;
using LogSender;

namespace Tracking.Config
{
    public static class AutofacConfig
    {

        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<ITrackingController, TrackingController>();
            _builder.RegisterSelfSingletonProp<ICoilController, CoilController>();
            _builder.RegisterSelfSingletonProp<AggregateService>();
        
            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

          
            // 註冊Form
            _builder.RegisterFrame<TrkContract.IPresenter, TrkPresenter, TrkContract.IView, TrkForm>();


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
                return new TrkMgr(
                      c.Resolve<ISysAkkaManager>(),
                      c.Resolve<ITrackingController>(),
                      c.Resolve<ICoilController>(),
                      c.GetLog<TrkMgr>(a => a.MgrLog)); 
            })  .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            // 註冊
            _builder.Register(c =>
            {
                return new TrkScan(                     
                      c.Resolve<ITrackingController>(),
                      c.Resolve<ICoilController>(),
                      c.Resolve<ICoilController>(),
                      c.GetLog<TrkScan>(a => a.ScnLog));
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
