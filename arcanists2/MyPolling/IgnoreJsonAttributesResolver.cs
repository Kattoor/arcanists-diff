

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
