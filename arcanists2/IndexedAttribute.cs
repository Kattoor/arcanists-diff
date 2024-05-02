

using System;

#nullable disable
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
internal sealed class IndexedAttribute : Attribute
{
  public string IndexName { get; }

  public IndexedAttribute(string indexName = null) => this.IndexName = indexName;
}
