using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Autofac;
using Core.Util;
using LabelPrint.Actor;
using LabelPrint.Printer;
using LabelPrint.Service;
using LabelPrint.View;
using LogSender;
using System;
using System.Net;
using static LabelPrint.Model.ZebraModel;

namespace LabelPrint.Config
{
    public static class AutofacConfig
    {
        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<ZebraStatus>();
            _builder.RegisterSelfSingletonProp<AggregateService>();


            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

            // 註冊Form
            _builder.RegisterFrame<LprContract.IPresenter, LprPresenter, LprContract.IView, LprForm>();

            // 註冊SysIP
            _builder.Register(c =>
            {
                var appSetting = c.Resolve<AppSetting>();
                var akkaIpModel = new AkkaSysIP()
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.LocalIp), appSetting.LocalPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.RemoteIp), appSetting.RemotePort),
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


            // 註冊Printer
            _builder.Register(c =>
            {
                var appSetting = c.Resolve<AppSetting>();
                return new Zebra(appSetting.RemoteIp, appSetting.RemotePort);
            }).PropertiesAutowired()
                 .AsSelf()
                 .SingleInstance();

            //註冊MgrActor
            _builder.Register(c =>
            {
                return new LprMgr(c.Resolve<ISysAkkaManager>(), 
                                  c.GetLog<LprMgr>(a => a.MgrLog));
            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            //註冊SndEditActor
            _builder.Register(c =>
            {
                return new LprSndEdit(
                    c.Resolve<ISysAkkaManager>(), 
                    c.GetLog<LprSndEdit>(a => a.SndEditLog));

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //
            //註冊SndActor
            _builder.Register(c =>
            {
                return new PinterClient(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<LprContract.IPresenter>(),
                    c.Resolve<Zebra>(),
                    c.GetLog<PinterClient>(a => a.SndLog)
                    );

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
