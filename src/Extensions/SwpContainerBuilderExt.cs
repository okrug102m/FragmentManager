using System;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;

namespace FragmentManager.Extensions
{
    internal static class SwpContainerBuilderExt
  {
    internal static void BuildContainer(
      this ConfigurableApplication app,
      Action<ContainerBuilder> buildContainerAction)
    {
      ContainerBuilder builder = new ContainerBuilder();
      buildContainerAction(builder);
      SwpContainerBuilderExt.RegisterSwpDependencies(builder);
      app.AppContainer = builder.Build(ContainerBuildOptions.None);
    }

    private static void RegisterSwpDependencies(ContainerBuilder builder)
    {
      builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
      builder.RegisterType<FragmentService>().As<IFragmentService>().InstancePerLifetimeScope();
      builder.Register<MessageBus>((Func<IComponentContext, MessageBus>) (context => new MessageBus())).As<IMessenger>().InstancePerLifetimeScope();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IViewModel>().InstancePerLifetimeScope();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IFragment>();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IWindowViewModel>().OnActivated((Action<IActivatedEventArgs<object>>) (args => (args.Instance as IWindowViewModel)?.OnCreated())).OnRelease<object, ScanningActivatorData, DynamicRegistrationStyle>((Action<object>) (o => (o as IWindowViewModel)?.OnDestroy()));
    }
  }
}
