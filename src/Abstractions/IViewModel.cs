using System.ComponentModel;

namespace FragmentManager.Abstractions
{
  public interface IViewModel : IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
  {
  }
}
