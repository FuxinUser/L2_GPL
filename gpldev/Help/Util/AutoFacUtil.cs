using Autofac;

namespace Core.Util
{
    public static class AutoFacUtil
    {
        public static void RegisterSelfSingleton<Interface, Type>(this ContainerBuilder builder)
        {
            builder.RegisterType<Type>()
              .As<Interface>()
              .AsSelf()
              .SingleInstance();
        }

        public static void RegisterSelfSingleton<Type>(this ContainerBuilder builder)
        {
            builder.RegisterType<Type>()
                .AsSelf()
                .SingleInstance();
        }
      
        public static void RegisterSelfScope<Type>(this ContainerBuilder builder)
        {
            builder.RegisterType<Type>()
              .AsSelf()
              .InstancePerLifetimeScope();
        }
        public static void RegisterSelfSingletonProp<Type>(this ContainerBuilder builder)
        {
            builder.RegisterType<Type>()
                .PropertiesAutowired()
                .AsSelf()
                .SingleInstance();
        }

        public static void RegisterSelfSingletonProp<Interface, Type>(this ContainerBuilder builder)
        {
            builder.RegisterType<Type>()
                .As<Interface>()
                .PropertiesAutowired()
                .AsSelf()
                .SingleInstance();
        }

        public static void RegisterPanelForm<IPresenter, Presenter, IView, View>(this ContainerBuilder builder)
        {
            builder.RegisterType<Presenter>()
                .PropertiesAutowired()
                .As<IPresenter>()
                .AsSelf()
                .SingleInstance();
            builder.RegisterPanelFormView<IView, View, IPresenter>();
        }
        private static void RegisterPanelFormView<IView, View, IPresenter>(this ContainerBuilder builder)    
        {
            builder.RegisterType<View>()
              .As<IView>()
              .AsSelf()
              .SingleInstance();
        }
        public static void RegisterFrame<IPresneter, Presneter, IView, View>(this ContainerBuilder builder)
        {
            builder.RegisterType<Presneter>()
               .PropertiesAutowired()
               .As<IPresneter>()
               .AsSelf()
               .SingleInstance();
            builder.RegisterSelfSingleton<IView, View>();
        }

        public static void RegisterPerLifetimeProp<Interface, Type>(this ContainerBuilder builder)
        {
            builder.RegisterType<Type>()
                .As<Interface>()
                .PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

    }
}
