using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace FragmentManager.Models
{
  public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
  {
    private const string CountString = "Count";
    private const string IndexerName = "Item[]";
    private const string KeysName = "Keys";
    private const string ValuesName = "Values";

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    private IDictionary<TKey, TValue> Dictionary { get; set; }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return Dictionary.GetEnumerator();
    }

    public void AddRange(IDictionary<TKey, TValue> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      if (items.Count <= 0)
        return;
      if (Dictionary.Count > 0)
      {
        if (items.Keys.Any(k => Dictionary.ContainsKey(k)))
          throw new ArgumentException("An item with the same key has already been added.");
        foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
          Dictionary.Add(keyValuePair);
      }
      else
        Dictionary = new Dictionary<TKey, TValue>(items);
      OnCollectionChanged(NotifyCollectionChangedAction.Add, items.ToArray());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Dictionary.GetEnumerator();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = PropertyChanged;
      propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Insert(TKey key, TValue value, bool add)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      TValue obj;
      if (Dictionary.TryGetValue(key, out obj))
      {
        if (add)
          throw new ArgumentException("An item with the same key has already been added.");
        if (Equals(obj, value))
          return;
        Dictionary[key] = value;
        OnCollectionChanged(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, obj));
      }
      else
      {
        Dictionary[key] = value;
        OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
      }
    }

    private void OnPropertyChanged()
    {
      OnPropertyChanged("Count");
      OnPropertyChanged("Item[]");
      OnPropertyChanged("Keys");
      OnPropertyChanged("Values");
    }

    private void OnCollectionChanged()
    {
      OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
      collectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      KeyValuePair<TKey, TValue> changedItem)
    {
      OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
      collectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      KeyValuePair<TKey, TValue> newItem,
      KeyValuePair<TKey, TValue> oldItem)
    {
      OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
      collectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
    {
      OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
      collectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItems));
    }

    public ObservableDictionary()
    {
      Dictionary = new Dictionary<TKey, TValue>();
    }

    public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
    {
      Dictionary = new Dictionary<TKey, TValue>(dictionary);
    }

    public ObservableDictionary(IEqualityComparer<TKey> comparer)
    {
      Dictionary = new Dictionary<TKey, TValue>(comparer);
    }

    public ObservableDictionary(int capacity)
    {
      Dictionary = new Dictionary<TKey, TValue>(capacity);
    }

    public ObservableDictionary(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
    {
      Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
    }

    public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
    }

    public void Add(TKey key, TValue value)
    {
      Insert(key, value, true);
    }

    public bool ContainsKey(TKey key)
    {
      return Dictionary.ContainsKey(key);
    }

    public ICollection<TKey> Keys
    {
      get
      {
        return Dictionary.Keys;
      }
    }

    public bool Remove(TKey key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      TValue obj;
      Dictionary.TryGetValue(key, out obj);
      int num = Dictionary.Remove(key) ? 1 : 0;
      if (num == 0)
        return num != 0;
      OnCollectionChanged();
      return num != 0;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      return Dictionary.TryGetValue(key, out value);
    }

    public ICollection<TValue> Values
    {
      get
      {
        return Dictionary.Values;
      }
    }

    public TValue this[TKey key]
    {
      get => Dictionary[key];
      set => Insert(key, value, false);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      Insert(item.Key, item.Value, true);
    }

    public void Clear()
    {
      if (Dictionary.Count <= 0)
        return;
      Dictionary.Clear();
      OnCollectionChanged();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return Dictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      Dictionary.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return Dictionary.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return Dictionary.IsReadOnly;
      }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return Remove(item.Key);
    }
  }
}
