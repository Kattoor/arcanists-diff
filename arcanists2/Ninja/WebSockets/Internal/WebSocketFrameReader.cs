// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.WebSocketFrameReader
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  internal static class WebSocketFrameReader
  {
    public static async Task<WebSocketFrame> ReadAsync(
      Stream fromStream,
      ArraySegment<byte> intoBuffer,
      CancellationToken cancellationToken)
    {
      ArraySegment<byte> smallBuffer = new ArraySegment<byte>(new byte[8]);
      await BinaryReaderWriter.ReadExactly(2, fromStream, smallBuffer, cancellationToken);
      byte num1 = smallBuffer.Array[0];
      byte byte2 = smallBuffer.Array[1];
      byte num2 = 128;
      byte num3 = 15;
      bool isFinBitSet = ((int) num1 & (int) num2) == (int) num2;
      WebSocketOpCode opCode = (WebSocketOpCode) ((int) num1 & (int) num3);
      byte num4 = 128;
      bool isMaskBitSet = ((int) byte2 & (int) num4) == (int) num4;
      int count = (int) await WebSocketFrameReader.ReadLength(byte2, smallBuffer, fromStream, cancellationToken);
      try
      {
        if (isMaskBitSet)
        {
          ArraySegment<byte> maskKey = new ArraySegment<byte>(smallBuffer.Array, 0, 4);
          await BinaryReaderWriter.ReadExactly(maskKey.Count, fromStream, maskKey, cancellationToken);
          await BinaryReaderWriter.ReadExactly(count, fromStream, intoBuffer, cancellationToken);
          WebSocketFrameCommon.ToggleMask(maskKey, new ArraySegment<byte>(intoBuffer.Array, intoBuffer.Offset, count));
          maskKey = new ArraySegment<byte>();
        }
        else
          await BinaryReaderWriter.ReadExactly(count, fromStream, intoBuffer, cancellationToken);
      }
      catch (InternalBufferOverflowException ex)
      {
        throw new InternalBufferOverflowException(string.Format("Supplied buffer too small to read {0} bytes from {1} frame", (object) 0, (object) Enum.GetName(typeof (WebSocketOpCode), (object) opCode)), (Exception) ex);
      }
      return opCode != WebSocketOpCode.ConnectionClose ? new WebSocketFrame(isFinBitSet, opCode, count) : WebSocketFrameReader.DecodeCloseFrame(isFinBitSet, opCode, count, intoBuffer);
    }

    private static WebSocketFrame DecodeCloseFrame(
      bool isFinBitSet,
      WebSocketOpCode opCode,
      int count,
      ArraySegment<byte> buffer)
    {
      WebSocketCloseStatus closeStatus;
      string closeStatusDescription;
      if (count >= 2)
      {
        Array.Reverse((Array) buffer.Array, buffer.Offset, 2);
        int uint16 = (int) BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        closeStatus = !Enum.IsDefined(typeof (WebSocketCloseStatus), (object) uint16) ? WebSocketCloseStatus.Empty : (WebSocketCloseStatus) uint16;
        int index = buffer.Offset + 2;
        int count1 = count - 2;
        closeStatusDescription = count1 <= 0 ? (string) null : Encoding.UTF8.GetString(buffer.Array, index, count1);
      }
      else
      {
        closeStatus = WebSocketCloseStatus.Empty;
        closeStatusDescription = (string) null;
      }
      return new WebSocketFrame(isFinBitSet, opCode, count, closeStatus, closeStatusDescription);
    }

    private static async Task<uint> ReadLength(
      byte byte2,
      ArraySegment<byte> smallBuffer,
      Stream fromStream,
      CancellationToken cancellationToken)
    {
      uint num = (uint) byte2 & (uint) sbyte.MaxValue;
      switch (num)
      {
        case 126:
          num = (uint) await BinaryReaderWriter.ReadUShortExactly(fromStream, false, smallBuffer, cancellationToken);
          break;
        case (uint) sbyte.MaxValue:
          num = (uint) await BinaryReaderWriter.ReadULongExactly(fromStream, false, smallBuffer, cancellationToken);
          if (num > 2147483648U || num < 0U)
            throw new ArgumentOutOfRangeException(string.Format("Payload length out of range. Min 0 max 2GB. Actual {0:#,##0} bytes.", (object) num));
          break;
      }
      return num;
    }
  }
}
