using Example.Models;
using Example.Views;
using FragmentManager.Attributes;
using FragmentManager.Shuriken;
using Shuriken;
using System.Threading.Tasks;
using FragmentManager.Abstractions;

namespace Example.ViewModels
{
    [View(typeof(TestView))]
    public class TestViewModel:ObservableFragment
    {
        private readonly IMessengerService _messenger;

        [Observable]
        public TestData Data { get; private set; }

        public Command StartCommand { get; set; }

        public Command StopCommand { get; set; }

        public TestViewModel(IMessengerService messenger)
        {
            _messenger = messenger;
            Data = new TestData();

            Start();

            StartCommand = new Command(() => Data.IsActiv = true);

            StopCommand = new Command(() => Data.IsActiv = false);
        }

        private void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (Data.IsActiv)
                    {
                        Data.ProgressValue++;
                        Data.ProgressValueSecond += 1.3;
                        //Pub test
                        _messenger.Publish(Data);
                    }
                    
                    Task.Delay(100).Wait();
                }
            });
        }

    }
}
