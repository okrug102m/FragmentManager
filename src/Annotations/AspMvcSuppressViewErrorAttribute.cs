using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public sealed class AspMvcSuppressViewErrorAttribute : Attribute
  {
  }
}
