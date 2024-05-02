// Decompiled with JetBrains decompiler
// Type: MyPolling.IgnoreJsonAttributesResolver
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

#nullable disable
namespace MyPolling
{
  internal class IgnoreJsonAttributesResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(
      Type type,
      MemberSerialization memberSerialization)
    {
      IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
      foreach (JsonProperty jsonProperty in (IEnumerable<JsonProperty>) properties)
      {
        jsonProperty.Ignored = false;
        jsonProperty.Converter = (JsonConverter) null;
        jsonProperty.PropertyName = jsonProperty.UnderlyingName;
      }
      return properties;
    }
  }
}
