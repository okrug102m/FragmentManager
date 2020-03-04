﻿using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  public sealed class AspMvcMasterLocationFormatAttribute : Attribute
  {
    [NotNull]
    public string Format { get; private set; }

    public AspMvcMasterLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }
  }
}
