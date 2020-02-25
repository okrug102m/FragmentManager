using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
  {
    [NotNull]
    public string Format { get; private set; }

    public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
    {
      this.Format = format;
    }
  }
}
