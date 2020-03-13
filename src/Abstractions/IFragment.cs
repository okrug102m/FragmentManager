using System.ComponentModel;

namespace FragmentManager.Abstractions
{
  public interface IFragment : IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
  {
  }
}
