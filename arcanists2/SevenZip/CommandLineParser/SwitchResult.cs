﻿

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
