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
      if (object.Equals((object) storage, (object) value))
        return false;
      storage(value);
      this.RaisePropertyChanged(propertyName);
      return true;
    }

    protected virtual bool SetProperty<T>(
      Action<T> storage,
      T value,
      Action onChanged,
      [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return false;
      storage(value);
      if (onChanged != null)
        onChanged();
      this.RaisePropertyChanged(propertyName);
      return true;
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return false;
      storage = value;
      this.RaisePropertyChanged(propertyName);
      return true;
    }

    protected virtual bool SetProperty<T>(
      ref T storage,
      T value,
      Action onChanged,
      [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return false;
      storage = value;
      if (onChanged != null)
        onChanged();
      this.RaisePropertyChanged(propertyName);
      return true;
    }

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, args);
    }
  }
}
