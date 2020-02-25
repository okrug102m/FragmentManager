using System;
using System.ComponentModel;

namespace FragmentManager
{
  public abstract class WindowViewModel : BindableBase, IWindowViewModel, IInternalManagedObject, INotifyPropertyChanged
  {
    private IViewModel current;

    public INavigationService NavigationService { get; }

    public IViewModel Current
    {
      get
      {
        return this.current;
      }
      set
      {
        this.SetProperty<IViewModel>(ref this.current, value, nameof (Current));
      }
    }

    protected WindowViewModel(INavigationService navigationService)
    {
      this.NavigationService = navigationService;
      this.NavigationService.OnNavigated += new Action<IViewModel>(this.NavigationServiceOnOnNavigated);
    }

    public virtual void OnCreated()
    {
    }

    public virtual void OnDestroy()
    {
      this.NavigationService.OnNavigated -= new Action<IViewModel>(this.NavigationServiceOnOnNavigated);
    }

    private void NavigationServiceOnOnNavigated(IViewModel viewModel)
    {
      this.Current = viewModel;
    }
  }
}
