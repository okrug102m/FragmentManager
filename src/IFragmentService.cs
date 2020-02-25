using System;

namespace FragmentManager
{
  public interface IFragmentService : IDisposable
  {
    ObservableDictionary<string, IFragment> Fragments { get; }

    T CreateFragment<T>(string key) where T : IFragment;

    object CreateFragment(string key, Type type);

    void SetFragment<T>(string key, T existingFragment) where T : IFragment;

    void RemoveFragment(string key);
  }
}
