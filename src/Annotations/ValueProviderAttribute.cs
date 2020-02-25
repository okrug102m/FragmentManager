using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
  public sealed class ValueProviderAttribute : Attribute
  {
    [NotNull]
    public string Name { get; private set; }

    public ValueProviderAttribute([NotNull] string name)
    {
      this.Name = name;
    }
  }
}
