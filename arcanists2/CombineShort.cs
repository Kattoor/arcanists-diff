// Decompiled with JetBrains decompiler
// Type: CombineShort
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class CombineShort
{
  public static int To(short a, short b) => (int) a | (int) b << 16;

  public static short Low(int a) => (short) a;

  public static short High(int b) => (short) (b >> 16);
}
