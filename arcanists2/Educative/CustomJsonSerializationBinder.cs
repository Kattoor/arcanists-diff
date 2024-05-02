// Decompiled with JetBrains decompiler
// Type: Educative.CustomJsonSerializationBinder
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Runtime.Serialization;

#nullable disable
namespace Educative
{
  public class CustomJsonSerializationBinder : SerializationBinder
  {
    private readonly string _namespaceToTypes;

    public CustomJsonSerializationBinder(string namespaceToTypes)
    {
      this._namespaceToTypes = namespaceToTypes;
    }

    public override void BindToName(
      System.Type serializedType,
      out string assemblyName,
      out string typeName)
    {
      assemblyName = (string) null;
      typeName = serializedType.FullName.Replace(this._namespaceToTypes, string.Empty).Trim('.');
    }

    public override System.Type BindToType(string assemblyName, string typeName)
    {
      return typeName.StartsWith("Educative.") ? System.Type.GetType(typeName) : (System.Type) null;
    }
  }
}
