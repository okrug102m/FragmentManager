using Autofac;
using FragmentManager.Extensions;
using System;
using System.Windows;
using IContainer = Autofac.IContainer;

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
            this.BuildContainer(new Action<ContainerBuilder>(this.ConfigureContainer));
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.AppContainer.Dispose();
            base.OnExit(e);
        }
    }
}
