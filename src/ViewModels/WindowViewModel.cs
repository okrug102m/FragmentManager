using System.ComponentModel;
using FragmentManager.Abstractions;
using FragmentManager.Models;

namespace FragmentManager.ViewModels
{
  public abstract class WindowViewModel : BindableBase, IWindowViewModel, IInternalManagedObject, INotifyPropertyChanged
  {
    private IViewModel current;

    public INavigationService NavigationService { get; }

    public IViewModel Current
    {
      get => current;

      private set => SetProperty(ref current, value, nameof (Current));
    }

    protected WindowViewModel(INavigationService navigationService)
    {
      NavigationService = navigationService;
      NavigationService.OnNavigated += NavigationServiceOnOnNavigated;
    }

    public virtual void OnCreated()
    {
    }

    public virtual void OnDestroy()
    {
      NavigationService.OnNavigated -= NavigationServiceOnOnNavigated;
    }

    private void NavigationServiceOnOnNavigated(IViewModel viewModel)
    {
      Current = viewModel;
    }
  }
}
