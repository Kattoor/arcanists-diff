// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.PagedList`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  public class PagedList<T> : List<T>, IPagedList
  {
    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage => this.PageIndex > 0;

    public bool HasNextPage => this.PageIndex * this.PageSize <= this.TotalCount;

    public PagedList(IEnumerable<T> source, int index, int pageSize)
    {
      int num = source.Count<T>();
      this.TotalCount = num;
      this.TotalPages = num / pageSize;
      if (num % pageSize > 0)
        ++this.TotalPages;
      this.PageSize = pageSize;
      this.PageIndex = index;
      this.AddRange((IEnumerable<T>) source.Skip<T>(index * pageSize).Take<T>(pageSize).ToList<T>());
    }
  }
}
