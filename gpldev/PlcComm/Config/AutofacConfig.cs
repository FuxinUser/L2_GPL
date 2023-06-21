using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Autofac;
using Core.Util;
using DataModel.Common;
using System.Net;
using System;
using Akka.IO;
using PLCComm.Model;
using WMSComm.View;
using PLCComm.Actor;
using Controller.Sys;
using Controller.MsgPro;
using DataMod.MsgStruct.PLC;
using static MsgStruct.L2L1Snd;
using Core.Help.DumpRawDataHelp;
using LogSender;

namespace PLCComm.Config
{
    public static class AutofacConfig
    {

        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊程式使用相關Model
            _builder.RegisterSelfSingletonProp<PlcMsgTypeAndLengthDic>();
            _builder.RegisterSelfSingletonProp<Msg_201_Alive>();           
            _builder.RegisterSelfSingletonProp<SendQueue<ByteString>>();
            _builder.RegisterSelfSingletonProp<ISysController, SysController>();
            _builder.RegisterSelfSingletonProp<IMsgProController, MsgProController>();
            _builder.RegisterSelfSingletonProp<AggregateService>();
            _builder.RegisterSelfSingletonProp<IDumpRawData, DumpPlcMsg>();


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
            _builder.RegisterFrame<PlcCommContract.IPresenter, PlcCommPresenter, PlcCommContract.IView, PlcCommForm>();


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
                return new PlcMgr(c.Resolve<ISysAkkaManager>(), c.GetLog<PlcMgr>(a => a.MgrLog));
            })  .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊RcvActor
            _builder.Register(c =>
            {
                return new PlcRcv(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ISysController>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<PlcRcv>(a => a.RcvLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();



          

           //註冊CycleRcvActor
            _builder.Register(c =>
            {
                var appSetting = c.Resolve<AppSetting>();
                var CycRcvSysIp = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.CycleRcvLocalIp), appSetting.CycleRcvLocalPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.RemoteIp), appSetting.RemotePort),
                };


                return new PlcCycleRcv(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ISysController>(),
                    CycRcvSysIp,
                    c.GetLog<PlcCycleRcv>(a => a.CycleRcvRcvLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊RcvEditActor
            _builder.Register(c =>
            {
                return new PlcRcvEdit(
                    c.Resolve<ISysController>(),
                    c.Resolve<IMsgProController>(),
                    c.GetLog<PlcRcvEdit>(a => a.RcvEditLog)
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊SndActor
            _builder.Register(c =>
            {
                return new PlcSnd(
                    c.Resolve<AkkaSysIP>(),
                    c.Resolve<ISysController>(),
                    c.GetLog<PlcSnd>(a => a.SndLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊SndEditActor
            _builder.Register(c =>
            {
                return new PlcSndEdit(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<IMsgProController>(),
                    c.Resolve<AggregateService>(),
                    c.GetLog<PlcSndEdit>(a => a.SndEditLog)
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
