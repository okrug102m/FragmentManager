﻿using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
  public sealed class AspMvcPartialViewAttribute : Attribute
  {
  }
}
