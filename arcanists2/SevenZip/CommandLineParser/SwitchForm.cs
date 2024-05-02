// Decompiled with JetBrains decompiler
// Type: SevenZip.CommandLineParser.SwitchForm
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace SevenZip.CommandLineParser
{
  public class SwitchForm
  {
    public string IDString;
    public SwitchType Type;
    public bool Multi;
    public int MinLen;
    public int MaxLen;
    public string PostCharSet;

    public SwitchForm(
      string idString,
      SwitchType type,
      bool multi,
      int minLen,
      int maxLen,
      string postCharSet)
    {
      this.IDString = idString;
      this.Type = type;
      this.Multi = multi;
      this.MinLen = minLen;
      this.MaxLen = maxLen;
      this.PostCharSet = postCharSet;
    }

    public SwitchForm(string idString, SwitchType type, bool multi, int minLen)
      : this(idString, type, multi, minLen, 0, "")
    {
    }

    public SwitchForm(string idString, SwitchType type, bool multi)
      : this(idString, type, multi, 0)
    {
    }
  }
}
