// Decompiled with JetBrains decompiler
// Type: SevenZip.CommandLineParser.SwitchForm
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
