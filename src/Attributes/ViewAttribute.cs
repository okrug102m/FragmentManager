using System;

namespace FragmentManager.Attributes
{
    /// <summary>
    /// Binding ViewModel to View attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute
    {
        public Type ViewType { get; private set; }

        public ViewAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }
}
