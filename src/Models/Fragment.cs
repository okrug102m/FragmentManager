﻿using System.ComponentModel;
using FragmentManager.Abstractions;

namespace FragmentManager.Models
{
  public abstract class Fragment : BindableBase, IFragment, IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
  {
    public virtual void OnCreated()
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }

    public virtual void OnDestroy()
    {
    }
  }
}
