using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace FragmentManager
{
  public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
  {
    private const string CountString = "Count";
    private const string IndexerName = "Item[]";
    private const string KeysName = "Keys";
    private const string ValuesName = "Values";

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    protected IDictionary<TKey, TValue> Dictionary { get; private set; }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return this.Dictionary.GetEnumerator();
    }

    public void AddRange(IDictionary<TKey, TValue> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      if (items.Count <= 0)
        return;
      if (this.Dictionary.Count > 0)
      {
        if (items.Keys.Any<TKey>((Func<TKey, bool>) (k => this.Dictionary.ContainsKey(k))))
          throw new ArgumentException("An item with the same key has already been added.");
        foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) items)
          this.Dictionary.Add(keyValuePair);
      }
      else
        this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>(items);
      this.OnCollectionChanged(NotifyCollectionChangedAction.Add, (IList) items.ToArray<KeyValuePair<TKey, TValue>>());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.Dictionary.GetEnumerator();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void Insert(TKey key, TValue value, bool add)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      TValue obj;
      if (this.Dictionary.TryGetValue(key, out obj))
      {
        if (add)
          throw new ArgumentException("An item with the same key has already been added.");
        if (object.Equals((object) obj, (object) value))
          return;
        this.Dictionary[key] = value;
        this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, obj));
      }
      else
      {
        this.Dictionary[key] = value;
        this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
      }
    }

    private void OnPropertyChanged()
    {
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnPropertyChanged("Keys");
      this.OnPropertyChanged("Values");
    }

    private void OnCollectionChanged()
    {
      this.OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      KeyValuePair<TKey, TValue> changedItem)
    {
      this.OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, new NotifyCollectionChangedEventArgs(action, (object) changedItem));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      KeyValuePair<TKey, TValue> newItem,
      KeyValuePair<TKey, TValue> oldItem)
    {
      this.OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, new NotifyCollectionChangedEventArgs(action, (object) newItem, (object) oldItem));
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
    {
      this.OnPropertyChanged();
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, new NotifyCollectionChangedEventArgs(action, newItems));
    }

    public ObservableDictionary()
    {
      this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>();
    }

    public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
    {
      this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>(dictionary);
    }

    public ObservableDictionary(IEqualityComparer<TKey> comparer)
    {
      this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>(comparer);
    }

    public ObservableDictionary(int capacity)
    {
      this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>(capacity);
    }

    public ObservableDictionary(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
    {
      this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>(dictionary, comparer);
    }

    public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      this.Dictionary = (IDictionary<TKey, TValue>) new System.Collections.Generic.Dictionary<TKey, TValue>(capacity, comparer);
    }

    public void Add(TKey key, TValue value)
    {
      this.Insert(key, value, true);
    }

    public bool ContainsKey(TKey key)
    {
      return this.Dictionary.ContainsKey(key);
    }

    public ICollection<TKey> Keys
    {
      get
      {
        return this.Dictionary.Keys;
      }
    }

    public bool Remove(TKey key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      TValue obj;
      this.Dictionary.TryGetValue(key, out obj);
      int num = this.Dictionary.Remove(key) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.OnCollectionChanged();
      return num != 0;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      return this.Dictionary.TryGetValue(key, out value);
    }

    public ICollection<TValue> Values
    {
      get
      {
        return this.Dictionary.Values;
      }
    }

    public TValue this[TKey key]
    {
      get
      {
        return this.Dictionary[key];
      }
      set
      {
        this.Insert(key, value, false);
      }
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      this.Insert(item.Key, item.Value, true);
    }

    public void Clear()
    {
      if (this.Dictionary.Count <= 0)
        return;
      this.Dictionary.Clear();
      this.OnCollectionChanged();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return this.Dictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      this.Dictionary.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.Dictionary.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.Dictionary.IsReadOnly;
      }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return this.Remove(item.Key);
    }
  }
}
