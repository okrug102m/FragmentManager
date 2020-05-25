using System.Diagnostics;
using Example.Models;
using Example.Views;
using FragmentManager.Abstractions;
using FragmentManager.Attributes;
using FragmentManager.Shuriken;

namespace Example.ViewModels
{
    [View(typeof(TitleView))]
    public class TitleViewModel: ObservableViewModel
    {
        public IFragmentService FragmentService { get; set; }

        public TitleViewModel(IFragmentService fragmentService, IMessengerService messenger)
        {
            FragmentService = fragmentService;
            FragmentService.CreateFragment<TestViewModel>("First");
            //Sub test
            messenger.Subscribe<TestData>(Update);
        }

        private void Update(TestData data)
        {
            Debug.WriteLine(data.ProgressValue);
        }
    }
}
