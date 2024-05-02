// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.DictionaryEnumerator`2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
