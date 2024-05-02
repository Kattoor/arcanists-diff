// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.WebSocketFrameCommon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
