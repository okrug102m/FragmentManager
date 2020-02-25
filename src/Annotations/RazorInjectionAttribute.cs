﻿using System;

namespace FragmentManager.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  public sealed class RazorInjectionAttribute : Attribute
  {
    [NotNull]
    public string Type { get; private set; }

    [NotNull]
    public string FieldName { get; private set; }

    public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
    {
      this.Type = type;
      this.FieldName = fieldName;
    }
  }
}
