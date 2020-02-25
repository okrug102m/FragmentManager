using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.All)]
  public sealed class LocalizationRequiredAttribute : Attribute
  {
    public bool Required { get; private set; }

    public LocalizationRequiredAttribute()
      : this(true)
    {
    }

    public LocalizationRequiredAttribute(bool required)
    {
      this.Required = required;
    }
  }
}
