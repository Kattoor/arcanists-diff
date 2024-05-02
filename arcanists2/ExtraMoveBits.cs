// Decompiled with JetBrains decompiler
// Type: ExtraMoveBits
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class ExtraMoveBits
{
  public static byte Compress(
    bool sprinting,
    bool noGlide,
    bool noIceJump,
    bool highJump,
    bool longJump)
  {
    return (byte) ((sprinting ? 1 : 0) | (noGlide ? 2 : 0) | (noIceJump ? 4 : 0) | (highJump ? 8 : 0) | (longJump ? 16 : 0));
  }

  public static bool IsSprinting(int b) => false;

  public static bool NoGlide(int b) => (b & 2) != 0;

  public static bool NoIceJump(int b) => (b & 4) != 0;

  public static bool HighJump(int b) => (b & 8) != 0;

  public static bool LongJump(int b) => (b & 16) != 0;
}
