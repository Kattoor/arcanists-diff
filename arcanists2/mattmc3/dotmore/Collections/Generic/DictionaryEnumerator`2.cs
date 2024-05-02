

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  public class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IEnumerator, IDisposable
  {
    private readonly IEnumerator<KeyValuePair<TKey, TValue>> _impl;

    public void Dispose() => this._impl.Dispose();

    public DictionaryEnumerator(IDictionary<TKey, TValue> value)
    {
      this._impl = value.GetEnumerator();
    }

    public void Reset() => this._impl.Reset();

    public bool MoveNext() => this._impl.MoveNext();

    public DictionaryEntry Entry
    {
      get
      {
        KeyValuePair<TKey, TValue> current = this._impl.Current;
        return new DictionaryEntry((object) current.Key, (object) current.Value);
      }
    }

    public object Key => (object) this._impl.Current.Key;

    public object Value => (object) this._impl.Current.Value;

    public object Current => (object) this.Entry;
  }
}
