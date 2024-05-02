// Decompiled with JetBrains decompiler
// Type: Educative.CIntitializeVarible
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
