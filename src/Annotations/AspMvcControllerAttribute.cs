﻿using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
  public sealed class AspMvcControllerAttribute : Attribute
  {
    [CanBeNull]
    public string AnonymousProperty { get; private set; }

    public AspMvcControllerAttribute()
    {
    }

    public AspMvcControllerAttribute([NotNull] string anonymousProperty)
    {
      this.AnonymousProperty = anonymousProperty;
    }
  }
}
