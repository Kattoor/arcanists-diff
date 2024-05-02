// Decompiled with JetBrains decompiler
// Type: Hazel.AccountType
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Hazel
{
  public enum AccountType
  {
    None = 0,
    Mod = 1,
    Developer = 2,
    Head_of_Moderation = 4,
    Muted = 8,
    Audio_Director = 16, // 0x00000010
    Arcane_Monster = 32, // 0x00000020
    First_Place = 64, // 0x00000040
    Second_Place = 128, // 0x00000080
    Third_Place = 256, // 0x00000100
    Booster = 512, // 0x00000200
    Robotics_Division = 1024, // 0x00000400
    Contributor = 2048, // 0x00000800
    Backend_Technical_Director = 4096, // 0x00001000
    Tournament_Official = 8192, // 0x00002000
    Wiki_Staff = 16384, // 0x00004000
    Twitch_Streamer = 32768, // 0x00008000
    Youtube_Creator = 65536, // 0x00010000
    Donator = 131072, // 0x00020000
    Asset_Creator = 262144, // 0x00040000
    Audio_Wizard = 524288, // 0x00080000
    Perm_Muted = 1048576, // 0x00100000
    Arch_Donator = 2097152, // 0x00200000
    Lifetime = 4194304, // 0x00400000
    Website_Director = 8388608, // 0x00800000
    Game_Director = 16777216, // 0x01000000
    Art_Director = 33554432, // 0x02000000
    Owner = 67108864, // 0x04000000
    Tournament_Director = 134217728, // 0x08000000
    Press_Account = 268435456, // 0x10000000
    Tourny_Participate = 536870912, // 0x20000000
  }
}
