using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  public sealed class RazorImportNamespaceAttribute : Attribute
  {
    [NotNull]
    public string Name { get; private set; }

    public RazorImportNamespaceAttribute([NotNull] string name)
    {
      this.Name = name;
    }
  }
}
