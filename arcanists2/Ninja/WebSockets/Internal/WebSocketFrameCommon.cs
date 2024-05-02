

using System;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  internal static class WebSocketFrameCommon
  {
    public const int MaskKeyLength = 4;

    public static void ToggleMask(ArraySegment<byte> maskKey, ArraySegment<byte> payload)
    {
      if (maskKey.Count != 4)
        throw new Exception(string.Format("MaskKey key must be {0} bytes", (object) 4));
      byte[] array1 = payload.Array;
      byte[] array2 = maskKey.Array;
      int offset1 = payload.Offset;
      int count = payload.Count;
      int offset2 = maskKey.Offset;
      for (int index1 = offset1; index1 < count; ++index1)
      {
        int num = index1 - offset1;
        int index2 = offset2 + num % 4;
        array1[index1] = (byte) ((uint) array1[index1] ^ (uint) array2[index2]);
      }
    }
  }
}
