using System;

namespace FragmentManager
{
  [AttributeUsage(AttributeTargets.Class)]
  public class ViewAttribute : Attribute
  {
    public Type ViewType { get; private set; }

    public ViewAttribute(Type viewType)
    {
      this.ViewType = viewType;
    }
  }
}
