using System.Threading.Tasks;
using System.Windows;
using Example.Models;
using Example.Views;
using FragmentManager.Attributes;
using FragmentManager.Shuriken;
using Shuriken;

namespace Example.ViewModels
{
    [View(typeof(TestView))]
    public class TestViewModel:ObservableFragment
    {
        [Observable]
        public TestData Data { get; private set; }

        public TestViewModel()
        {
            Data = new TestData();

            Task.Run(() =>
            {
                while (true)
                {
                    Data.ProgressValue++;
                    Data.ProgressValueSecond += 1.3;
                    Task.Delay(100).Wait();
                }
            });
        }

    }
}
