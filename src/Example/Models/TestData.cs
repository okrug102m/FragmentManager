using Shuriken;

namespace Example.Models
{
    public class TestData:ObservableObject
    {
        [Observable]
        public double ProgressValue { get; set; }

        [Observable]
        public double ProgressValueSecond { get; set; }

        [Observable]
        public bool IsActiv { get; set; }
    }
}
