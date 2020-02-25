using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
  public sealed class CollectionAccessAttribute : Attribute
  {
    public CollectionAccessType CollectionAccessType { get; private set; }

    public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
    {
      this.CollectionAccessType = collectionAccessType;
    }
  }
}
