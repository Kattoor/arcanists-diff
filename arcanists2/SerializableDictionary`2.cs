

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
[Serializable]
public class SerializableDictionary<TKey, TValue> : 
  SerializableDictionary,
  ISerializationCallbackReceiver,
  IDictionary<TKey, TValue>,
  ICollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable
{
  [SerializeField]
  private List<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair> list = new List<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair>();
  private Lazy<Dictionary<TKey, uint>> _keyPositions;

  private Dictionary<TKey, uint> KeyPositions => this._keyPositions.Value;

  public SerializableDictionary()
  {
    this._keyPositions = new Lazy<Dictionary<TKey, uint>>(new Func<Dictionary<TKey, uint>>(this.MakeKeyPositions));
  }

  private Dictionary<TKey, uint> MakeKeyPositions()
  {
    int count = this.list.Count;
    Dictionary<TKey, uint> dictionary = new Dictionary<TKey, uint>(count);
    for (int index = 0; index < count; ++index)
      dictionary[this.list[index].Key] = (uint) index;
    return dictionary;
  }

  public void OnBeforeSerialize()
  {
  }

  public void OnAfterDeserialize()
  {
    this._keyPositions = new Lazy<Dictionary<TKey, uint>>(new Func<Dictionary<TKey, uint>>(this.MakeKeyPositions));
  }

  public TValue this[TKey key]
  {
    get => this.list[(int) this.KeyPositions[key]].Value;
    set
    {
      uint index;
      if (this.KeyPositions.TryGetValue(key, out index))
      {
        this.list[(int) index].SetValue(value);
      }
      else
      {
        this.KeyPositions[key] = (uint) this.list.Count;
        this.list.Add(new SerializableDictionary<TKey, TValue>.SerializableKeyValuePair(key, value));
      }
    }
  }

  public ICollection<TKey> Keys
  {
    get
    {
      return (ICollection<TKey>) this.list.Select<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair, TKey>((Func<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair, TKey>) (tuple => tuple.Key)).ToArray<TKey>();
    }
  }

  public ICollection<TValue> Values
  {
    get
    {
      return (ICollection<TValue>) this.list.Select<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair, TValue>((Func<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair, TValue>) (tuple => tuple.Value)).ToArray<TValue>();
    }
  }

  public void Add(TKey key, TValue value)
  {
    if (this.KeyPositions.ContainsKey(key))
      throw new ArgumentException("An element with the same key already exists in the dictionary.");
    this.KeyPositions[key] = (uint) this.list.Count;
    this.list.Add(new SerializableDictionary<TKey, TValue>.SerializableKeyValuePair(key, value));
  }

  public bool ContainsKey(TKey key) => this.KeyPositions.ContainsKey(key);

  public bool Remove(TKey key)
  {
    uint index1;
    if (!this.KeyPositions.TryGetValue(key, out index1))
      return false;
    Dictionary<TKey, uint> keyPositions = this.KeyPositions;
    keyPositions.Remove(key);
    int count = this.list.Count;
    this.list.RemoveAt((int) index1);
    for (uint index2 = index1; (long) index2 < (long) count; ++index2)
      keyPositions[this.list[(int) index2].Key] = index2;
    return true;
  }

  public bool TryGetValue(TKey key, out TValue value)
  {
    uint index;
    if (this.KeyPositions.TryGetValue(key, out index))
    {
      value = this.list[(int) index].Value;
      return true;
    }
    value = default (TValue);
    return false;
  }

  public int Count => this.list.Count;

  public bool IsReadOnly => false;

  public void Add(KeyValuePair<TKey, TValue> kvp) => this.Add(kvp.Key, kvp.Value);

  public void Clear() => this.list.Clear();

  public bool Contains(KeyValuePair<TKey, TValue> kvp) => this.KeyPositions.ContainsKey(kvp.Key);

  public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
  {
    int count = this.list.Count;
    if (array.Length - arrayIndex < count)
      throw new ArgumentException(nameof (arrayIndex));
    int index = 0;
    while (index < count)
    {
      SerializableDictionary<TKey, TValue>.SerializableKeyValuePair serializableKeyValuePair = this.list[index];
      array[arrayIndex] = new KeyValuePair<TKey, TValue>(serializableKeyValuePair.Key, serializableKeyValuePair.Value);
      ++index;
      ++arrayIndex;
    }
  }

  public bool Remove(KeyValuePair<TKey, TValue> kvp) => this.Remove(kvp.Key);

  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
  {
    return this.list.Select<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair, KeyValuePair<TKey, TValue>>(new Func<SerializableDictionary<TKey, TValue>.SerializableKeyValuePair, KeyValuePair<TKey, TValue>>(ToKeyValuePair)).GetEnumerator();

    KeyValuePair<TKey, TValue> ToKeyValuePair(
      SerializableDictionary<TKey, TValue>.SerializableKeyValuePair skvp)
    {
      return new KeyValuePair<TKey, TValue>(skvp.Key, skvp.Value);
    }
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  [Serializable]
  public struct SerializableKeyValuePair
  {
    public TKey Key;
    public TValue Value;

    public SerializableKeyValuePair(TKey key, TValue value)
    {
      this.Key = key;
      this.Value = value;
    }

    public void SetValue(TValue value) => this.Value = value;
  }
}
