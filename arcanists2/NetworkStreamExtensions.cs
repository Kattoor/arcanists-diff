

using System.IO;
using System.Net.Sockets;

#nullable disable
public static class NetworkStreamExtensions
{
  public static int ReadSafely(this NetworkStream stream, byte[] buffer, int offset, int size)
  {
    try
    {
      return stream.Read(buffer, offset, size);
    }
    catch (IOException ex)
    {
      return 0;
    }
  }

  public static bool ReadExactly(this NetworkStream stream, byte[] buffer, int amount)
  {
    int num;
    for (int offset = 0; offset < amount; offset += num)
    {
      int size = amount - offset;
      num = stream.ReadSafely(buffer, offset, size);
      if (num == 0)
        return false;
    }
    return true;
  }
}
