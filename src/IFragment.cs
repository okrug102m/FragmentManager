using System.ComponentModel;

namespace FragmentManager
{
  public interface IFragment : IManagedLifeTimeObject, IInternalManagedObject, INotifyPropertyChanged
  {
  }
}
