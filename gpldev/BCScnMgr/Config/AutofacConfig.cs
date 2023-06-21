using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Autofac;
using BCScnMgr.View;
using Controller.Coil;
using Controller.Track;
using Core.Util;
using LogSender;
using System;
using System.Net;

namespace BCScnMgr.Config
{
    public static class AutofacConfig
    {
        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<ICoilController, CoilController>();
            _builder.RegisterSelfSingletonProp<ITrackingController, TrackingController>();

            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

            // 註冊Form
            _builder.RegisterFrame<BCScnContract.IPresenter, BCScnPresenter, BCScnContract.IView, BCSScnForm>();

            // 註冊SysIP
            _builder.Register(c =>
            {
                var appSetting = c.Resolve<AppSetting>();
                var akkaIpModel = new AkkaSysIP()
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.LocalIp), appSetting.LocalPort),
                    //RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.RemoteIp), appSetting.RemotePort),
                };
                return akkaIpModel;
            }).SingleInstance();

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
                return new BCSMgr(c.Resolve<ISysAkkaManager>(), 
                                  c.GetLog<BCSMgr>(a => a.MgrLog));
            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();


            // 註冊Conn
            _builder.Register(c =>
            {
                return new BCScnConn(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<BCScnConn>(a => a.ConnLog)
                    );

            }).PropertiesAutowired()
            .AsSelf()
            .InstancePerLifetimeScope();

            
            //註冊RcvEditActor
            _builder.Register(c =>
            {
                return new BCScnRcvEdit(
                     c.Resolve<ISysAkkaManager>(),
                     c.Resolve<ICoilController>(),
                     c.Resolve<ITrackingController>(),
                    c.GetLog<BCScnRcvEdit>(a => a.RcvEditLog));

            }).PropertiesAutowired()
                 .AsSelf()
                 .InstancePerLifetimeScope();




            //註冊SndEditActor
            _builder.Register(c =>
            {
                return new BCScnSndEdit(
                    c.Resolve<ISysAkkaManager>(),
                    c.GetLog<BCScnSndEdit>(a => a.RcvEditLog));

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
