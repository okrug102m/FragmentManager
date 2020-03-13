using System;
using Autofac;
using FragmentManager.Abstractions;
using FragmentManager.Services;

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
      builder.Register(context => new MessageService()).As<IMessengerService>().InstancePerLifetimeScope();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IViewModel>().InstancePerLifetimeScope();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IFragment>();
      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IWindowViewModel>()
          .OnActivated(args => (args.Instance as IWindowViewModel)?
              .OnCreated())
          .OnRelease(o => (o as IWindowViewModel)?.OnDestroy());
    }
  }
}
