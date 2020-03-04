using System;
using Autofac;

namespace FragmentManager.Extensions
{
    internal static class FragmentManagerContainerBuilderExt
  {
    internal static void BuildContainer(
      this ConfigurableApplication app,
      Action<ContainerBuilder> buildContainerAction)
    {
      ContainerBuilder builder = new ContainerBuilder();
      buildContainerAction(builder);
      RegisterFragmentManagerDependencies(builder);
      app.AppContainer = builder.Build();
    }

    private static void RegisterFragmentManagerDependencies(ContainerBuilder builder)
    {
      builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
      builder.RegisterType<FragmentService>().As<IFragmentService>().InstancePerLifetimeScope();
      builder.Register(context => new MessageBus()).As<IMessenger>().InstancePerLifetimeScope();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IViewModel>().InstancePerLifetimeScope();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IFragment>();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IWindowViewModel>()
          .OnActivated(args => (args.Instance as IWindowViewModel)?
              .OnCreated())
          .OnRelease(o => (o as IWindowViewModel)?.OnDestroy());
    }
  }
}
