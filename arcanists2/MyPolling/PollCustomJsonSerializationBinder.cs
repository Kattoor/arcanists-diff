// Decompiled with JetBrains decompiler
// Type: MyPolling.PollCustomJsonSerializationBinder
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
