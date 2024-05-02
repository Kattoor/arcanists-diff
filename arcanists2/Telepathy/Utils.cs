

#nullable disable
namespace Telepathy
{
  public static class Utils
  {
    public static byte[] IntToBytesBigEndian(int value)
    {
      return new byte[4]
      {
        (byte) (value >> 24),
        (byte) (value >> 16),
        (byte) (value >> 8),
        (byte) value
      };
    }

    public static void IntToBytesBigEndianNonAlloc(int value, byte[] bytes)
    {
      bytes[0] = (byte) (value >> 24);
      bytes[1] = (byte) (value >> 16);
      bytes[2] = (byte) (value >> 8);
      bytes[3] = (byte) value;
    }

    public static int BytesToIntBigEndian(byte[] bytes)
    {
      return (int) bytes[0] << 24 | (int) bytes[1] << 16 | (int) bytes[2] << 8 | (int) bytes[3];
    }
  }
}
