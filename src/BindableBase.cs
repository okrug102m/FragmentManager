using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FragmentManager
{
    public abstract class BindableBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual bool SetProperty<T>(Action<T> storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (Equals(storage, value))
        return false;
      storage(value);
      RaisePropertyChanged(propertyName);
      return true;
    }

    protected virtual bool SetProperty<T>(
      Action<T> storage,
      T value,
      Action onChanged,
      [CallerMemberName] string propertyName = null)
    {
      if (Equals(storage, value))
        return false;
      storage(value);
      onChanged?.Invoke();
      RaisePropertyChanged(propertyName);
      return true;
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (Equals(storage, value))
        return false;
      storage = value;
      RaisePropertyChanged(propertyName);
      return true;
    }

    protected virtual bool SetProperty<T>(
      ref T storage,
      T value,
      Action onChanged,
      [CallerMemberName] string propertyName = null)
    {
      if (Equals(storage, value))
        return false;
      storage = value;
      onChanged?.Invoke();
      RaisePropertyChanged(propertyName);
      return true;
    }

    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
    {
      PropertyChangedEventHandler propertyChanged = PropertyChanged;
      propertyChanged?.Invoke(this, args);
    }
  }
}
