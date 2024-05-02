

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
