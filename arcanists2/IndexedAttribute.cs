// Decompiled with JetBrains decompiler
// Type: IndexedAttribute
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
internal sealed class IndexedAttribute : Attribute
{
  public string IndexName { get; }

  public IndexedAttribute(string indexName = null) => this.IndexName = indexName;
}
