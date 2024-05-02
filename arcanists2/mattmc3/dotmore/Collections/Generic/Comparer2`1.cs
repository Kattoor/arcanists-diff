// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.Comparer2`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  public class Comparer2<T> : Comparer<T>
  {
    private readonly Comparison<T> _compareFunction;

    public Comparer2(Comparison<T> comparison)
    {
      this._compareFunction = comparison != null ? comparison : throw new ArgumentNullException(nameof (comparison));
    }

    public override int Compare(T arg1, T arg2) => this._compareFunction(arg1, arg2);
  }
}
