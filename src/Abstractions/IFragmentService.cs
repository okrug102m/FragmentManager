using System;
using FragmentManager.Models;

namespace FragmentManager.Abstractions
{
  public interface IFragmentService : IDisposable
  {
    ObservableDictionary<string, IFragment> Fragments { get; }

        /// <summary>
        /// Create new pair ViewModel to key value
        /// Create a new ViewModel pair to the key value
        /// </summary>
        /// <typeparam name="T">ViewModel</typeparam>
        /// <param name="key">Custom string key</param>
        /// <returns></returns>
        T CreateFragment<T>(string key) where T : IFragment;

    object CreateFragment(string key, Type type);

    void SetFragment<T>(string key, T existingFragment) where T : IFragment;

    void RemoveFragment(string key);
  }
}
