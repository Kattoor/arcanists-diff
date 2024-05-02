// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.IPagedList
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace mattmc3.dotmore.Collections.Generic
{
  internal interface IPagedList
  {
    int TotalCount { get; set; }

    int TotalPages { get; set; }

    int PageIndex { get; set; }

    int PageSize { get; set; }

    bool HasPreviousPage { get; }

    bool HasNextPage { get; }
  }
}
