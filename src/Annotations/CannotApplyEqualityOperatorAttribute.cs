using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
  public sealed class CannotApplyEqualityOperatorAttribute : Attribute
  {
  }
}
