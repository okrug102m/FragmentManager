﻿using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class AspRequiredAttributeAttribute : Attribute
  {
    [NotNull]
    public string Attribute { get; private set; }

    public AspRequiredAttributeAttribute([NotNull] string attribute)
    {
      Attribute = attribute;
    }
  }
}
