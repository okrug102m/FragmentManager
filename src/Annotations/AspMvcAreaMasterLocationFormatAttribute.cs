using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  public sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
  {
    [NotNull]
    public string Format { get; private set; }

    public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
    {
      this.Format = format;
    }
  }
}
