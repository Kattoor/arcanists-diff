// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.EqualityComparer2`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  public class EqualityComparer2<T> : EqualityComparer<T>
  {
    private readonly Func<T, T, bool> _equalsFunction;
    private readonly Func<T, int> _hashFunction;

    public EqualityComparer2(Func<T, T, bool> equalsFunction)
      : this(equalsFunction, (Func<T, int>) (o => 0))
    {
    }

    public EqualityComparer2(Func<T, T, bool> equalsFunction, Func<T, int> hashFunction)
    {
      if (equalsFunction == null)
        throw new ArgumentNullException(nameof (equalsFunction));
      if (hashFunction == null)
        throw new ArgumentNullException(nameof (hashFunction));
      this._equalsFunction = equalsFunction;
      this._hashFunction = hashFunction;
    }

    public override bool Equals(T x, T y) => this._equalsFunction(x, y);

    public override int GetHashCode(T obj) => this._hashFunction(obj);
  }
}
