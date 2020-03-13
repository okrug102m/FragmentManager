using Example.ViewModels;
using FragmentManager.Abstractions;
using FragmentManager.ViewModels;

namespace Example
{
    public class MainFragmentViewModel:WindowViewModel
    {
        public MainFragmentViewModel(INavigationService navigationService):base(navigationService)
        {
            navigationService.NavigateTo<TitleViewModel>();
        }
    }
}
