using Autofac;
using FragmentManager.Extensions;
using FragmentManager.Shuriken;
using System.Windows;
using Autofac.Core.Lifetime;
using FragmentManager.Abstractions;
using FragmentManager.Services;

namespace Example
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        private void ConfigureApp()
        {
            this.UseDataTemplatesBuilderByAttributes();
            this.UseShuriken();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureApp();
            MainWindow = AppContainer.Resolve<MainWindow>();
            if (MainWindow != null)
            {
                MainWindow.DataContext = AppContainer.Resolve<MainFragmentViewModel>();
                MainWindow.Show();
            }
        }
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            this.ForceLoadAssemblies();
            builder.Register(context => new MainWindow()).SingleInstance();
        }
    }
}
