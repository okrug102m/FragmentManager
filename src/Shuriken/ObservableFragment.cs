using System.ComponentModel;
using FragmentManager.Abstractions;
using Shuriken;

namespace FragmentManager.Shuriken
{
  public abstract class ObservableFragment : ObservableObject, IFragment, IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
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

    protected ObservableFragment()
      : base()
    {
    }
  }
}
