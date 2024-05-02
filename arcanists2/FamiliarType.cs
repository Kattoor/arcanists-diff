// Decompiled with JetBrains decompiler
// Type: FamiliarType
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public enum FamiliarType
{
  Nothing = 0,
  Arcane = 1,
  Flame = 2,
  Stone = 4,
  Storm = 8,
  Frost = 16, // 0x00000010
  Underdark = 32, // 0x00000020
  OverLight = 64, // 0x00000040
  Nature = 128, // 0x00000080
  Seas = 256, // 0x00000100
  Cogs = 512, // 0x00000200
  Seasons = 1024, // 0x00000400
  Illusion = 2048, // 0x00000800
  Blood = 4096, // 0x00001000
}
