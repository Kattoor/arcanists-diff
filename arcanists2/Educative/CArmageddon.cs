﻿// Decompiled with JetBrains decompiler
// Type: Educative.CArmageddon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CArmageddon : Command
  {
    public MapEnum armageddon = MapEnum.Grassy_Hills;
    public int turn = 10;

    public CArmageddon() => this.type = Command.Type.Armageddon;
  }
}
