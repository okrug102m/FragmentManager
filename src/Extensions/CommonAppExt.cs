using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FragmentManager.Annotations;

namespace FragmentManager.Extensions
{
  public static class CommonAppExt
  {
    [PublicAPI]
    public static void ForceLoadAssemblies(this ConfigurableApplication app)
    {
      List<Assembly> loadedAssemblies = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).ToList<Assembly>();
      loadedAssemblies.SelectMany<Assembly, AssemblyName>((Func<Assembly, IEnumerable<AssemblyName>>) (x => (IEnumerable<AssemblyName>) x.GetReferencedAssemblies())).Distinct<AssemblyName>().Where<AssemblyName>((Func<AssemblyName, bool>) (y => !loadedAssemblies.Any<Assembly>((Func<Assembly, bool>) (a => a.FullName == y.FullName)))).ToList<AssemblyName>().ForEach((Action<AssemblyName>) (x => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x))));
    }
  }
}
