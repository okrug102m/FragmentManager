using System;
using System.Windows;
using Shuriken.Monitoring;

namespace FragmentManager.Shuriken
{
    public static class ShurikenApplicationExt
    {
        private static ApplicationMonitorScope applicationMonitorScope;

        /// <summary>
        /// Расширение, добавлющее функционал Shuriken
        /// </summary>
        /// <param name="app"></param>
        public static void UseShuriken(this ConfigurableApplication app)
        {
            app.Exit += AppOnExit;
            if (applicationMonitorScope != null)
                throw new InvalidOperationException("Application already init");
            applicationMonitorScope = new ApplicationMonitorScope(new WpfNotificationContext(app.Dispatcher));
        }

        private static void AppOnExit(object sender, ExitEventArgs exitEventArgs)
        {
            applicationMonitorScope.Dispose();
        }
  }
}
