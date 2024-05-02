// Decompiled with JetBrains decompiler
// Type: MyPolling.IgnoreJsonAttributesResolver
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
