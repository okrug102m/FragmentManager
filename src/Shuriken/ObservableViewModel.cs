using System.ComponentModel;
using Shuriken;

namespace FragmentManager.Shuriken
{
  public abstract class ObservableViewModel : ObservableObject, IViewModel, IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
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

    protected ObservableViewModel()
      : base(false)
    {
    }
  }
}
