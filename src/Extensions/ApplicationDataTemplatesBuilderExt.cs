using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac.Core;

namespace FragmentManager.Extensions
{
  public static class ApplicationDataTemplatesBuilderExt
  {
    public static void UseDataTemplatesBuilderByAttributes(this ConfigurableApplication app)
    {
      IEnumerable<Type> types = app.AppContainer.ComponentRegistry.Registrations.SelectMany<IComponentRegistration, Service>((Func<IComponentRegistration, IEnumerable<Service>>) (x => x.Services)).OfType<IServiceWithType>().Select<IServiceWithType, Type>((Func<IServiceWithType, Type>) (x => x.ServiceType)).Where<Type>((Func<Type, bool>) (type =>
      {
        if (type.IsAbstract)
          return false;
        if (!typeof (IViewModel).IsAssignableFrom(type))
          return typeof (IFragment).IsAssignableFrom(type);
        return true;
      }));
      ResourceDictionary resourceDictionary = new ResourceDictionary();
      resourceDictionary.BeginInit();
      foreach (Type type in types)
      {
        ViewAttribute customAttribute = (ViewAttribute) Attribute.GetCustomAttribute((MemberInfo) type, typeof (ViewAttribute));
        if (customAttribute != null)
        {
          DataTemplate dataTemplate1 = new DataTemplate((object) type);
          dataTemplate1.VisualTree = new FrameworkElementFactory(customAttribute.ViewType);
          DataTemplate dataTemplate2 = dataTemplate1;
          resourceDictionary.Add((object) new DataTemplateKey((object) type), (object) dataTemplate2);
        }
      }
      resourceDictionary.EndInit();
      app.Resources.MergedDictionaries.Add(resourceDictionary);
    }
  }
}
