using System;

namespace FragmentManager
{
  public interface INavigationService
  {
    event Action<IViewModel> OnNavigated;

    void NavigateTo<T>(bool saveHistory = false) where T : IViewModel;

    void Back();
  }
}
