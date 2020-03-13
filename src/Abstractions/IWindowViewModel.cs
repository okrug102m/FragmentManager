using System.ComponentModel;

namespace FragmentManager.Abstractions
{
  public interface IWindowViewModel : IInternalManagedObject, INotifyPropertyChanged
  {
    INavigationService NavigationService { get; }

    IViewModel Current { get; }
  }
}
