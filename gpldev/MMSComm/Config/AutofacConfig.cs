using Akka.Actor;
using Akka.Event;
using Akka.IO;
using AkkaSysBase;
using Autofac;
using Controller.Coil;
using Controller.MsgPro;
using Controller.Sys;
using Core.Help.DumpRawDataHelp;
using Core.Util;
using DataMod.MMS;
using DataModel.Common;
using DataModel.MES;
using LogSender;
using MMSComm.Actor;
using MMSComm.Service;
using MMSComm.View;
using System;
using System.Net;

namespace MMSComm.Config
{
    public static class AutofacConfig
    {
        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<MMS_ACK_Structure>();
            _builder.RegisterSelfSingletonProp<MMSTypeAndLengthDic>();
            _builder.RegisterSelfSingletonProp<ICoilController, CoilController>();
            _builder.RegisterSelfSingletonProp<IMsgProController, MsgProController>();
            _builder.RegisterSelfSingletonProp<SendQueue<ByteString>>();
            _builder.RegisterSelfSingletonProp<AggregateService>();
            _builder.RegisterSelfSingletonProp<IDumpRawData, DumpMMSMsg>();
            _builder.RegisterPerLifetimeProp<ISysController, SysController>();


            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

            // 註冊Form
            _builder.RegisterFrame<MESContract.IPresenter, MESPresenter, MESContract.IView, MMSFrom>();

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

            //註冊MgrActor
            _builder.Register(c =>
            {
                return new MMSMgr(c.Resolve<ISysAkkaManager>(), 
                                  c.GetLog<MMSMgr>(a => a.MgrLog));
            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();


            _builder.Register(c =>
            {
                return new MMSRcv(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<MMSRcv>(a => a.RcvLog),
                    c.Resolve<AggregateService>(),
                    c.Resolve<IMsgProController>(),
                    c.Resolve<ISysController>()
                    );

            }).PropertiesAutowired()
            .AsSelf()
            .InstancePerLifetimeScope();

            //註冊SndActor
            _builder.Register(c =>
            {
                return new MMSSnd(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ISysController>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<MMSSnd>(a => a.SndLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();


            //註冊RcvEditActor
            _builder.Register(c =>
            {
                return new MMSRcvEdit(c.Resolve<IMsgProController>(),
                                      c.GetLog<MMSRcvEdit>(a => a.RcvEditLog));

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();


            //註冊SndEditActor
            _builder.Register(c =>
            {
                return new MMSSndEdit(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ICoilController>(),
                    c.Resolve<IMsgProController>(),
                    c.Resolve<AggregateService>(),
                    c.GetLog<MMSSndEdit>(a => a.SndEditLog));

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
