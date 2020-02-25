using System;
using System.Collections.Generic;
using Autofac;

namespace FragmentManager
{
  public class FragmentService : IFragmentService, IDisposable
  {
    private readonly ILifetimeScope scope;

    public ObservableDictionary<string, IFragment> Fragments { get; }

    public FragmentService(ILifetimeScope scope)
    {
      this.Fragments = new ObservableDictionary<string, IFragment>(5);
      this.scope = scope;
    }

    public T CreateFragment<T>(string key) where T : IFragment
    {
      return (T) this.CreateFragment(key, typeof (T));
    }

    public object CreateFragment(string key, Type type)
    {
      if (!typeof (IFragment).IsAssignableFrom(type))
        throw new ArgumentException("type is not assignable IFragment");
      IFragment fragment = this.scope.Resolve(type) as IFragment;
      if (this.Fragments.Keys.Contains(key))
      {
        this.Fragments[key]?.OnDestroy();
        this.Fragments[key] = fragment;
      }
      else
        this.Fragments.Add(key, fragment);
      return (object) fragment;
    }

    public void SetFragment<T>(string key, T existingFragment) where T : IFragment
    {
      if (this.Fragments.Keys.Contains(key))
      {
        this.Fragments[key]?.OnDestroy();
        this.Fragments[key] = (IFragment) existingFragment;
      }
      else
        this.Fragments.Add(key, (IFragment) existingFragment);
    }

    public void RemoveFragment(string key)
    {
      if (!this.Fragments.Keys.Contains(key))
        return;
      this.Fragments[key].OnDestroy();
      this.Fragments[key] = (IFragment) null;
    }

    public void Dispose()
    {
      foreach (KeyValuePair<string, IFragment> fragment in this.Fragments)
        fragment.Value.OnDestroy();
    }
  }
}
