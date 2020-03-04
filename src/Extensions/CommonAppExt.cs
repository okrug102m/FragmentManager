using System;
using System.Linq;
using FragmentManager.Annotations;

namespace FragmentManager.Extensions
{
    public static class CommonAppExt
  {
    [PublicApi]
    public static void ForceLoadAssemblies(this ConfigurableApplication app)
    {
      var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
      loadedAssemblies.SelectMany(x => x.GetReferencedAssemblies())
          .Distinct()
          .Where(y => loadedAssemblies.All(a => a.FullName != y.FullName))
          .ToList()
          .ForEach(x => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x)));
    }
  }
}
