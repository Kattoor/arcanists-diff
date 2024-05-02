

#nullable disable
public static class CombineShort
{
  public static int To(short a, short b) => (int) a | (int) b << 16;

  public static short Low(int a) => (short) a;

  public static short High(int b) => (short) (b >> 16);
}
