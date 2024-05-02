// Decompiled with JetBrains decompiler
// Type: Educative.CIntitializeVarible
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Educative
{
  public class CIntitializeVarible : Command
  {
    public List<CIntitializeVarible.ListVars> list = new List<CIntitializeVarible.ListVars>();

    public CIntitializeVarible() => this.type = Command.Type.IntitializeVariables;

    [Serializable]
    public class ListVars
    {
      public int identifier = -1;
      public int value;
      public string name = "temp";
    }
  }
}
