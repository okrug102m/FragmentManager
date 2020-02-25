using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
  public sealed class HtmlAttributeValueAttribute : Attribute
  {
    [NotNull]
    public string Name { get; private set; }

    public HtmlAttributeValueAttribute([NotNull] string name)
    {
      this.Name = name;
    }
  }
}
