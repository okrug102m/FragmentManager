using Example.Views;
using FragmentManager;
using FragmentManager.Shuriken;

namespace Example.ViewModels
{
    [View(typeof(TitleView))]
    public class TitleViewModel: ObservableViewModel
    {
        public IFragmentService FragmentService { get; set; }

        public TitleViewModel(IFragmentService fragmentService)
        {
            FragmentService = fragmentService;
        }
    }
}
