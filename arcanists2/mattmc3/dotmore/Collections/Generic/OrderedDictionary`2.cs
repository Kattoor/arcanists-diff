// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.OrderedDictionary`2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  public class OrderedDictionary<TKey, TValue> : 
    IOrderedDictionary<TKey, TValue>,
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable,
    IOrderedDictionary,
    ICollection,
    IDictionary
  {
    private KeyedCollection2<TKey, KeyValuePair<TKey, TValue>> _keyedCollection;

    public TValue this[TKey key]
    {
      get => this.GetValue(key);
      set => this.SetValue(key, value);
    }

    public TValue this[int index]
    {
      get => this.GetItem(index).Value;
      set => this.SetItem(index, value);
    }

    public int Count => this._keyedCollection.Count;

    public ICollection<TKey> Keys
    {
      get
      {
        return (ICollection<TKey>) this._keyedCollection.Select<KeyValuePair<TKey, TValue>, TKey>((Func<KeyValuePair<TKey, TValue>, TKey>) (x => x.Key)).ToList<TKey>();
      }
    }

    public ICollection<TValue> Values
    {
      get
      {
        return (ICollection<TValue>) this._keyedCollection.Select<KeyValuePair<TKey, TValue>, TValue>((Func<KeyValuePair<TKey, TValue>, TValue>) (x => x.Value)).ToList<TValue>();
      }
    }

    public IEqualityComparer<TKey> Comparer { get; private set; }

    public OrderedDictionary() => this.Initialize();

    public OrderedDictionary(IEqualityComparer<TKey> comparer) => this.Initialize(comparer);

    public OrderedDictionary(IOrderedDictionary<TKey, TValue> dictionary)
    {
      this.Initialize();
      foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
        this._keyedCollection.Add(keyValuePair);
    }

    public OrderedDictionary(
      IOrderedDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
    {
      this.Initialize(comparer);
      foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
        this._keyedCollection.Add(keyValuePair);
    }

    public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      this.Initialize();
      foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
        this._keyedCollection.Add(keyValuePair);
    }

    public OrderedDictionary(
      IEnumerable<KeyValuePair<TKey, TValue>> items,
      IEqualityComparer<TKey> comparer)
    {
      this.Initialize(comparer);
      foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
        this._keyedCollection.Add(keyValuePair);
    }

    private void Initialize(IEqualityComparer<TKey> comparer = null)
    {
      this.Comparer = comparer;
      if (comparer != null)
        this._keyedCollection = new KeyedCollection2<TKey, KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, TValue>, TKey>) (x => x.Key), comparer);
      else
        this._keyedCollection = new KeyedCollection2<TKey, KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, TValue>, TKey>) (x => x.Key));
    }

    public void Add(TKey key, TValue value)
    {
      this._keyedCollection.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public void Clear() => this._keyedCollection.Clear();

    public void Insert(int index, TKey key, TValue value)
    {
      this._keyedCollection.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
    }

    public int IndexOf(TKey key)
    {
      return this._keyedCollection.Contains(key) ? this._keyedCollection.IndexOf(this._keyedCollection[key]) : -1;
    }

    public bool ContainsValue(TValue value) => this.Values.Contains(value);

    public bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer)
    {
      return this.Values.Contains<TValue>(value, comparer);
    }

    public bool ContainsKey(TKey key) => this._keyedCollection.Contains(key);

    public KeyValuePair<TKey, TValue> GetItem(int index)
    {
      if (index < 0 || index >= this._keyedCollection.Count)
        throw new ArgumentException("The index was outside the bounds of the dictionary: " + (object) index);
      return this._keyedCollection[index];
    }

    public void SetItem(int index, TValue value)
    {
      if (index < 0 || index >= this._keyedCollection.Count)
        throw new ArgumentException("The index is outside the bounds of the dictionary: " + (object) index);
      KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this._keyedCollection[index].Key, value);
      this._keyedCollection[index] = keyValuePair;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return this._keyedCollection.GetEnumerator();
    }

    public bool Remove(TKey key) => this._keyedCollection.Remove(key);

    public void RemoveAt(int index)
    {
      if (index < 0 || index >= this._keyedCollection.Count)
        throw new ArgumentException("The index was outside the bounds of the dictionary: " + (object) index);
      this._keyedCollection.RemoveAt(index);
    }

    public TValue GetValue(TKey key)
    {
      if (!this._keyedCollection.Contains(key))
        throw new ArgumentException("The given key is not present in the dictionary: " + (object) key);
      return this._keyedCollection[key].Value;
    }

    public void SetValue(TKey key, TValue value)
    {
      KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
      int index = this.IndexOf(key);
      if (index > -1)
        this._keyedCollection[index] = keyValuePair;
      else
        this._keyedCollection.Add(keyValuePair);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      if (this._keyedCollection.Contains(key))
      {
        value = this._keyedCollection[key].Value;
        return true;
      }
      value = default (TValue);
      return false;
    }

    public void SortKeys() => this._keyedCollection.SortByKeys();

    public void SortKeys(IComparer<TKey> comparer) => this._keyedCollection.SortByKeys(comparer);

    public void SortKeys(Comparison<TKey> comparison)
    {
      this._keyedCollection.SortByKeys(comparison);
    }

    public void SortValues() => this.SortValues((IComparer<TValue>) System.Collections.Generic.Comparer<TValue>.Default);

    public void SortValues(IComparer<TValue> comparer)
    {
      this._keyedCollection.Sort((Comparison<KeyValuePair<TKey, TValue>>) ((x, y) => comparer.Compare(x.Value, y.Value)));
    }

    public void SortValues(Comparison<TValue> comparison)
    {
      this._keyedCollection.Sort((Comparison<KeyValuePair<TKey, TValue>>) ((x, y) => comparison(x.Value, y.Value)));
    }

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => this.Add(key, value);

    bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => this.ContainsKey(key);

    ICollection<TKey> IDictionary<TKey, TValue>.Keys => this.Keys;

    bool IDictionary<TKey, TValue>.Remove(TKey key) => this.Remove(key);

    bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
    {
      return this.TryGetValue(key, out value);
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values => this.Values;

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      get => this[key];
      set => this[key] = value;
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      this._keyedCollection.Add(item);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear() => this._keyedCollection.Clear();

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
      return this._keyedCollection.Contains(item);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int arrayIndex)
    {
      this._keyedCollection.CopyTo(array, arrayIndex);
    }

    int ICollection<KeyValuePair<TKey, TValue>>.Count => this._keyedCollection.Count;

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      return this._keyedCollection.Remove(item);
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new DictionaryEnumerator<TKey, TValue>((IDictionary<TKey, TValue>) this);
    }

    void IOrderedDictionary.Insert(int index, object key, object value)
    {
      this.Insert(index, (TKey) key, (TValue) value);
    }

    void IOrderedDictionary.RemoveAt(int index) => this.RemoveAt(index);

    object IOrderedDictionary.this[int index]
    {
      get => (object) this[index];
      set => this[index] = (TValue) value;
    }

    void IDictionary.Add(object key, object value) => this.Add((TKey) key, (TValue) value);

    void IDictionary.Clear() => this.Clear();

    bool IDictionary.Contains(object key) => this._keyedCollection.Contains((TKey) key);

    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new DictionaryEnumerator<TKey, TValue>((IDictionary<TKey, TValue>) this);
    }

    bool IDictionary.IsFixedSize => false;

    bool IDictionary.IsReadOnly => false;

    ICollection IDictionary.Keys => (ICollection) this.Keys;

    void IDictionary.Remove(object key) => this.Remove((TKey) key);

    ICollection IDictionary.Values => (ICollection) this.Values;

    object IDictionary.this[object key]
    {
      get => (object) this[(TKey) key];
      set => this[(TKey) key] = (TValue) value;
    }

    void ICollection.CopyTo(Array array, int index)
    {
      ((ICollection) this._keyedCollection).CopyTo(array, index);
    }

    int ICollection.Count => this._keyedCollection.Count;

    bool ICollection.IsSynchronized => ((ICollection) this._keyedCollection).IsSynchronized;

    object ICollection.SyncRoot => ((ICollection) this._keyedCollection).SyncRoot;
  }
}
