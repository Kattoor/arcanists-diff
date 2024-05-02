// Decompiled with JetBrains decompiler
// Type: SevenZip.CommandLineParser.SwitchResult
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;

#nullable disable
namespace SevenZip.CommandLineParser
{
  public class SwitchResult
  {
    public bool ThereIs;
    public bool WithMinus;
    public ArrayList PostStrings = new ArrayList();
    public int PostCharIndex;

    public SwitchResult() => this.ThereIs = false;
  }
}
