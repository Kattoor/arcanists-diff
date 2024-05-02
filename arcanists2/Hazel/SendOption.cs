// Decompiled with JetBrains decompiler
// Type: Hazel.SendOption
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Hazel
{
  [Flags]
  public enum SendOption : byte
  {
    None = 0,
    Reliable = 1,
    Fragmented = 2,
    FragmentedReliable = Fragmented | Reliable, // 0x03
  }
}
