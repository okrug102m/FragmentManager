using System.ComponentModel;

namespace FragmentManager
{
  public interface IWindowViewModel : IInternalManagedObject, INotifyPropertyChanged
  {
    INavigationService NavigationService { get; }

    IViewModel Current { get; }
  }
}
