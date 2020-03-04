using System.Windows;
using Autofac;
using FragmentManager.Extensions;

namespace FragmentManager
{
    public abstract class ConfigurableApplication : Application
    {
        /// <summary>
        /// Внутренний контейнер UI
        /// </summary>
        public IContainer AppContainer { get; internal set; }

        protected ConfigurableApplication()
        {
            this.BuildContainer(ConfigureContainer);
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppContainer.Dispose();
            base.OnExit(e);
        }
    }
}
