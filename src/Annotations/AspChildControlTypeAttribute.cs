using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class AspChildControlTypeAttribute : Attribute
  {
    [NotNull]
    public string TagName { get; private set; }

    [NotNull]
    public Type ControlType { get; private set; }

    public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
    {
      this.TagName = tagName;
      this.ControlType = controlType;
    }
  }
}
