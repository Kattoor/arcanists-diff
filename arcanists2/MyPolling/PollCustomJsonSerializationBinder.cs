

using Newtonsoft.Json;
using System;

#nullable disable
namespace MyPolling
{
  public class PollCustomJsonSerializationBinder : SerializationBinder
  {
    private readonly string _namespaceToTypes;

    public PollCustomJsonSerializationBinder(string namespaceToTypes)
    {
      this._namespaceToTypes = namespaceToTypes;
    }

    public override void BindToName(
      Type serializedType,
      out string assemblyName,
      out string typeName)
    {
      assemblyName = (string) null;
      typeName = serializedType.FullName.Replace(this._namespaceToTypes, string.Empty).Trim('.');
    }

    public override Type BindToType(string assemblyName, string typeName)
    {
      return typeName.StartsWith("MyPolling.") ? Type.GetType(typeName) : (Type) null;
    }
  }
}
