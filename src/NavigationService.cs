using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core.Lifetime;

namespace FragmentManager
{
  public class NavigationService : INavigationService
  {
    private readonly ILifetimeScope navigationLifeTimeScope;
    private readonly Stack<NavigationElement> stack;

    public event Action<IViewModel> OnNavigated;

    private NavigationElement Current
    {
      get
      {
        if (!stack.Any())
          return null;
        return stack.Peek();
      }
    }

    public NavigationService(ILifetimeScope navigationLifetimeScope)
    {
      navigationLifeTimeScope = navigationLifetimeScope;
      stack = new Stack<NavigationElement>(20);
      navigationLifeTimeScope.CurrentScopeEnding += NavigationLifeTimeScopeEnding;
    }

    public void NavigateTo<T>(bool saveHistory = false) where T : IViewModel
    {
      if (!saveHistory)
      {
        while (stack.Any())
          ReleaseCurrentElement();
      }
      Current?.ViewModel.OnPause();
      NavigationElement element = NavigationElement.CreateElement<T>(navigationLifeTimeScope.BeginLifetimeScope(typeof (T).Name));
      stack.Push(element);
      element.ViewModel.OnCreated();
      Action<IViewModel> onNavigated = OnNavigated;
      onNavigated?.Invoke(element.ViewModel);
    }

    public void Back()
    {
      if (stack.Count == 0)
        throw new InvalidOperationException("Navigation stack is empty");
      ReleaseCurrentElement();
      Current.ViewModel.OnResume();
      Action<IViewModel> onNavigated = OnNavigated;
      onNavigated?.Invoke(Current.ViewModel);
    }

    private void NavigationLifeTimeScopeEnding(
      object sender,
      LifetimeScopeEndingEventArgs lifetimeScopeEndingEventArgs)
    {
      while (stack.Any())
        ReleaseCurrentElement();
    }

    private void ReleaseCurrentElement()
    {
      IViewModel viewModel = Current.ViewModel;
      viewModel.OnPause();
      viewModel.OnDestroy();
      Current.LifetimeScope.Dispose();
      stack.Pop();
    }

    private class NavigationElement
    {
      public ILifetimeScope LifetimeScope { get; }

      public IViewModel ViewModel { get; }

      private NavigationElement(ILifetimeScope lifetimeScope, IViewModel viewModel)
      {
        LifetimeScope = lifetimeScope;
        ViewModel = viewModel;
      }

      public static NavigationElement CreateElement<T>(
        ILifetimeScope lifetimeScope)
        where T : IViewModel
      {
        return new NavigationElement(lifetimeScope, lifetimeScope.Resolve<T>());
      }
    }
  }
}
