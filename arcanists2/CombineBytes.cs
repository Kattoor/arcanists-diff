// Decompiled with JetBrains decompiler
// Type: CombineBytes
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class CombineBytes
{
  public static int To(byte a, byte b) => (int) a | (int) b << 8;

  public static byte Low(int a) => (byte) a;

  public static byte High(int b) => (byte) (b >> 8);
}
