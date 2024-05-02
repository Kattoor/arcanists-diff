

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
