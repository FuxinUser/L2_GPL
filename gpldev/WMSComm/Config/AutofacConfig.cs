using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Autofac;
using Core.Util;
using WMSComm.View;
using DataModel.Common;
using DataModel.WMS;
using System.Net;
using WMSComm.Actor;
using WMSComm.Config;
using WMSComm.Model;
using System;
using DataMod.WMS;
using Akka.IO;
using DataModel.MES;
using Controller.MsgPro;
using Core.Help.DumpRawDataHelp;
using LogSender;
using Controller.Sys;

namespace WMSComm
{
    public static class AutofacConfig
    {

        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<WMSMsgTypeAndLengthDic>();
            _builder.RegisterSelfSingletonProp<WMS_ACK_Structure>();
            _builder.RegisterSelfSingletonProp<MMS_Heartbeat_Structure>();          
            _builder.RegisterSelfSingletonProp<SendQueue<ByteString>>();
            _builder.RegisterSelfSingletonProp<AggregateService>();
            _builder.RegisterPerLifetimeProp<ISysController, SysController>();
            _builder.RegisterSelfSingletonProp<IMsgProController, MsgProController>();
            _builder.RegisterSelfSingletonProp<IDumpRawData, DumpWMSMsg>();

            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

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
            // 註冊Form
            _builder.RegisterFrame<WMSContract.IPresenter, WMSPresenter, WMSContract.IView, WMSFrom>();


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
                return new WMSMgr(c.Resolve<ISysAkkaManager>(), c.GetLog<WMSMgr>(a => a.MgrLog));
            })  .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊RcvActor
            _builder.Register(c =>
            {             
                return new WMSRcv(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ISysController>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<WMSRcv>(a => a.RcvLog),
                    c.Resolve<AggregateService>(),
                    c.Resolve<IMsgProController>()
                    );

            })  .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊RcvEditActor
            _builder.Register(c =>
            {
                return new WMSRcvEdit(
                    c.Resolve<AggregateService>(),
                    c.Resolve<IMsgProController>(),
                    c.GetLog<WMSRcvEdit>(a => a.RcvEditLog)                  
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊SndActor
            _builder.Register(c =>
            {
                return new WMSSnd(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ISysController>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<WMSSnd>(a => a.SndLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊SndEditActor
            _builder.Register(c =>
            {
                return new WMSSndEdit(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<IMsgProController>(),
                    c.Resolve<AggregateService>(),
                    c.GetLog<WMSSndEdit>(a => a.SndEditLog)
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
