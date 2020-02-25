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
    private readonly Stack<NavigationService.NavigationElement> stack;

    public event Action<IViewModel> OnNavigated;

    private NavigationService.NavigationElement Current
    {
      get
      {
        if (!this.stack.Any<NavigationService.NavigationElement>())
          return (NavigationService.NavigationElement) null;
        return this.stack.Peek();
      }
    }

    public NavigationService(ILifetimeScope navigationLifetimeScope)
    {
      this.navigationLifeTimeScope = navigationLifetimeScope;
      this.stack = new Stack<NavigationService.NavigationElement>(20);
      this.navigationLifeTimeScope.CurrentScopeEnding += new EventHandler<LifetimeScopeEndingEventArgs>(this.NavigationLifeTimeScopeEnding);
    }

    public void NavigateTo<T>(bool saveHistory = false) where T : IViewModel
    {
      if (!saveHistory)
      {
        while (this.stack.Any<NavigationService.NavigationElement>())
          this.ReleaseCurrentElement();
      }
      this.Current?.ViewModel.OnPause();
      NavigationService.NavigationElement element = NavigationService.NavigationElement.CreateElement<T>(this.navigationLifeTimeScope.BeginLifetimeScope((object) typeof (T).Name));
      this.stack.Push(element);
      element.ViewModel.OnCreated();
      Action<IViewModel> onNavigated = this.OnNavigated;
      if (onNavigated == null)
        return;
      onNavigated(element.ViewModel);
    }

    public void Back()
    {
      if (this.stack.Count == 0)
        throw new InvalidOperationException("Navigation stack is empty");
      this.ReleaseCurrentElement();
      this.Current.ViewModel.OnResume();
      Action<IViewModel> onNavigated = this.OnNavigated;
      if (onNavigated == null)
        return;
      onNavigated(this.Current.ViewModel);
    }

    private void NavigationLifeTimeScopeEnding(
      object sender,
      LifetimeScopeEndingEventArgs lifetimeScopeEndingEventArgs)
    {
      while (this.stack.Any<NavigationService.NavigationElement>())
        this.ReleaseCurrentElement();
    }

    private void ReleaseCurrentElement()
    {
      IViewModel viewModel = this.Current.ViewModel;
      viewModel.OnPause();
      viewModel.OnDestroy();
      this.Current.LifetimeScope.Dispose();
      this.stack.Pop();
    }

    private class NavigationElement
    {
      public ILifetimeScope LifetimeScope { get; }

      public IViewModel ViewModel { get; }

      private NavigationElement(ILifetimeScope lifetimeScope, IViewModel viewModel)
      {
        this.LifetimeScope = lifetimeScope;
        this.ViewModel = viewModel;
      }

      public static NavigationService.NavigationElement CreateElement<T>(
        ILifetimeScope lifetimeScope)
        where T : IViewModel
      {
        return new NavigationService.NavigationElement(lifetimeScope, (IViewModel) lifetimeScope.Resolve<T>());
      }
    }
  }
}
