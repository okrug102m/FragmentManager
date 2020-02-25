using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class AspMvcAreaAttribute : Attribute
  {
    [CanBeNull]
    public string AnonymousProperty { get; private set; }

    public AspMvcAreaAttribute()
    {
    }

    public AspMvcAreaAttribute([NotNull] string anonymousProperty)
    {
      this.AnonymousProperty = anonymousProperty;
    }
  }
}
