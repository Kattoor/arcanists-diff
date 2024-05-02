// Decompiled with JetBrains decompiler
// Type: GameStyle
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public enum GameStyle
{
  Dont_Mind = -1, // 0xFFFFFFFF
  Shuffle_Players = 1,
  Elementals = 2,
  CustomHP = 4,
  Random_Spells = 8,
  Original_Spells_Only = 16, // 0x00000010
  No_Movement = 128, // 0x00000080
  Zero_Shield = 256, // 0x00000100
  Bid = 2048, // 0x00000800
  Standard = 4096, // 0x00001000
  First_Turn_Teleport = 8192, // 0x00002000
  Sandbox = 16384, // 0x00004000
  Zombie_Monkey = 32768, // 0x00008000
  Watchtower = 65536, // 0x00010000
  Arcane_Monster = 131072, // 0x00020000
  Wind = 262144, // 0x00040000
}
