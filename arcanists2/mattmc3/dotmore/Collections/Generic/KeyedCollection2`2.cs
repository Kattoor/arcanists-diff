// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.KeyedCollection2`2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  public class KeyedCollection2<TKey, TItem> : KeyedCollection<TKey, TItem>
  {
    private const string DelegateNullExceptionMessage = "Delegate passed cannot be null";
    private Func<TItem, TKey> _getKeyForItemFunction;

    public KeyedCollection2(Func<TItem, TKey> getKeyForItemFunction)
    {
      this._getKeyForItemFunction = getKeyForItemFunction != null ? getKeyForItemFunction : throw new ArgumentNullException("Delegate passed cannot be null");
    }

    public KeyedCollection2(
      Func<TItem, TKey> getKeyForItemDelegate,
      IEqualityComparer<TKey> comparer)
      : base(comparer)
    {
      this._getKeyForItemFunction = getKeyForItemDelegate != null ? getKeyForItemDelegate : throw new ArgumentNullException("Delegate passed cannot be null");
    }

    protected override TKey GetKeyForItem(TItem item) => this._getKeyForItemFunction(item);

    public void SortByKeys() => this.SortByKeys((IComparer<TKey>) System.Collections.Generic.Comparer<TKey>.Default);

    public void SortByKeys(IComparer<TKey> keyComparer)
    {
      this.Sort((IComparer<TItem>) new Comparer2<TItem>((Comparison<TItem>) ((x, y) => keyComparer.Compare(this.GetKeyForItem(x), this.GetKeyForItem(y)))));
    }

    public void SortByKeys(Comparison<TKey> keyComparison)
    {
      this.Sort((IComparer<TItem>) new Comparer2<TItem>((Comparison<TItem>) ((x, y) => keyComparison(this.GetKeyForItem(x), this.GetKeyForItem(y)))));
    }

    public void Sort() => this.Sort((IComparer<TItem>) System.Collections.Generic.Comparer<TItem>.Default);

    public void Sort(Comparison<TItem> comparison)
    {
      this.Sort((IComparer<TItem>) new Comparer2<TItem>((Comparison<TItem>) ((x, y) => comparison(x, y))));
    }

    public void Sort(IComparer<TItem> comparer)
    {
      if (!(this.Items is List<TItem> items))
        return;
      items.Sort(comparer);
    }
  }
}
