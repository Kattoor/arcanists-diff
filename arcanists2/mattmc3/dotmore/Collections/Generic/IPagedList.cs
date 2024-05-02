// Decompiled with JetBrains decompiler
// Type: mattmc3.dotmore.Collections.Generic.IPagedList
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
