using Autofac;
using Autofac.Core;
using FragmentManager.Abstractions;
using FragmentManager.Attributes;
using System;
using System.Linq;
using System.Windows;

namespace FragmentManager.Extensions
{
    public static class ApplicationDataTemplatesBuilderExt
    {
        public static ResourceDictionary ResourceDictionaryKeys;

        public static void UseDataTemplatesBuilderByAttributes(this ConfigurableApplication app)
    {
        //ViewModels list
      var types = app.AppContainer.ComponentRegistry.Registrations
          .SelectMany(x => x.Services)
          .OfType<IServiceWithType>()
          .Select(x => x.ServiceType)
          .Where(type =>
      {
          if (type.IsAbstract)
              return false;
          if (!typeof (IViewModel).IsAssignableFrom(type))
              return typeof (IFragment).IsAssignableFrom(type);
          return true;
      });
      var resourceDictionary = new ResourceDictionary();
      ResourceDictionaryKeys = new ResourceDictionary();
            ResourceDictionaryKeys.BeginInit();
            resourceDictionary.BeginInit();
      foreach (var type in types)
      {
        ViewAttribute customAttribute = (ViewAttribute) Attribute.GetCustomAttribute(type, typeof (ViewAttribute));
        if (customAttribute != null)
        {
          var tmpDataTemplate = new DataTemplate(type);
          //Get View
          tmpDataTemplate.VisualTree = new FrameworkElementFactory(customAttribute.ViewType);
          var dataTemplate = tmpDataTemplate;
          //add to resources key=ViewModel value=View
          resourceDictionary.Add(new DataTemplateKey(type), dataTemplate);
        }
      }

      resourceDictionary.EndInit();
      app.Resources.MergedDictionaries.Add(resourceDictionary);
    }
  }
}
