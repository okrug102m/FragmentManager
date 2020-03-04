using Example.ViewModels;
using FragmentManager;

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
