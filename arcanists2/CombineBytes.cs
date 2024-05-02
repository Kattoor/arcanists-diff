// Decompiled with JetBrains decompiler
// Type: CombineBytes
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class CombineBytes
{
  public static int To(byte a, byte b) => (int) a | (int) b << 8;

  public static byte Low(int a) => (byte) a;

  public static byte High(int b) => (byte) (b >> 8);
}
