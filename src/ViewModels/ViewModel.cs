using System.ComponentModel;
using FragmentManager.Abstractions;
using FragmentManager.Models;

namespace FragmentManager.ViewModels
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
