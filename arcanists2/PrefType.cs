

using UnityEngine;

#nullable disable
public struct PrefType
{
  public string name;
  public string type;
  public string value;

  public void Apply()
  {
    switch (this.type)
    {
      case "float":
        PlayerPrefs.SetFloat(this.name, float.Parse(this.value));
        break;
      case "int":
        PlayerPrefs.SetInt(this.name, int.Parse(this.value));
        break;
      case "bool":
        Global.SetPrefBool(this.name, bool.Parse(this.value));
        break;
      case "string":
        PlayerPrefs.SetString(this.name, this.value);
        break;
    }
  }

  public PrefType(string name, string type, string value)
  {
    this.name = name;
    this.type = type;
    this.value = value;
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write(this.name);
    w.Write(this.type);
    w.Write(this.value);
  }

  public static PrefType Deseriralize(myBinaryReader r)
  {
    return new PrefType(r.ReadString(), r.ReadString(), r.ReadString());
  }
}
