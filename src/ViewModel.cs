using System.ComponentModel;

namespace FragmentManager
{
  public abstract class ViewModel : BindableBase, IViewModel, IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
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
