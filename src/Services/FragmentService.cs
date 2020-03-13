using System;
using System.Collections.Generic;
using Autofac;
using FragmentManager.Abstractions;
using FragmentManager.Models;

namespace FragmentManager.Services
{
  public class FragmentService : IFragmentService, IDisposable
  {
    private readonly ILifetimeScope scope;

    public ObservableDictionary<string, IFragment> Fragments { get; }

    public FragmentService(ILifetimeScope scope)
    {
      Fragments = new ObservableDictionary<string, IFragment>();
      this.scope = scope;
    }

    public T CreateFragment<T>(string key) where T : IFragment
    {
      return (T) CreateFragment(key, typeof (T));
    }

    public object CreateFragment(string key, Type type)
    {
      if (!typeof (IFragment).IsAssignableFrom(type))
        throw new ArgumentException("type is not assignable IFragment");
      IFragment fragment = scope.Resolve(type) as IFragment;
      if (Fragments.Keys.Contains(key))
      {
        Fragments[key]?.OnDestroy();
        Fragments[key] = fragment;
      }
      else
        Fragments.Add(key, fragment);
      return fragment;
    }

    public void SetFragment<T>(string key, T existingFragment) where T : IFragment
    {
      if (Fragments.Keys.Contains(key))
      {
        Fragments[key]?.OnDestroy();
        Fragments[key] = existingFragment;
      }
      else
        Fragments.Add(key, existingFragment);
    }

    public void RemoveFragment(string key)
    {
      if (!Fragments.Keys.Contains(key))
        return;
      Fragments[key].OnDestroy();
      Fragments[key] = null;
    }

    public void Dispose()
    {
      foreach (KeyValuePair<string, IFragment> fragment in Fragments)
        fragment.Value.OnDestroy();
    }
  }
}
