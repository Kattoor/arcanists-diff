

#nullable disable
public static class CombineBytes
{
  public static int To(byte a, byte b) => (int) a | (int) b << 8;

  public static byte Low(int a) => (byte) a;

  public static byte High(int b) => (byte) (b >> 8);
}
