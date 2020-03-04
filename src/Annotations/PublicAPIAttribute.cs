using System;

namespace FragmentManager.Annotations
{
  [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
  public sealed class PublicApiAttribute : Attribute
  {
    [CanBeNull]
    public string Comment { get; private set; }

    public PublicApiAttribute()
    {
    }

    public PublicApiAttribute([NotNull] string comment)
    {
      Comment = comment;
    }
  }
}
