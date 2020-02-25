using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
  public sealed class AspMvcActionSelectorAttribute : Attribute
  {
  }
}
