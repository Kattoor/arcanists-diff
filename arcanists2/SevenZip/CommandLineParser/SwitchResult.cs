// Decompiled with JetBrains decompiler
// Type: SevenZip.CommandLineParser.SwitchResult
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
