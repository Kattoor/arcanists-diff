// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.WebSocketFrameWriter
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  internal static class WebSocketFrameWriter
  {
    private static readonly Random _random = new Random((int) DateTime.Now.Ticks);

    public static void Write(
      WebSocketOpCode opCode,
      ArraySegment<byte> fromPayload,
      MemoryStream toStream,
      bool isLastFrame,
      bool isClient)
    {
      MemoryStream memoryStream = toStream;
      byte num1 = (byte) ((isLastFrame ? 128 : 0) | (int) (byte) opCode);
      memoryStream.WriteByte(num1);
      byte num2 = isClient ? (byte) 128 : (byte) 0;
      if (fromPayload.Count < 126)
      {
        byte num3 = (byte) ((uint) num2 | (uint) (byte) fromPayload.Count);
        memoryStream.WriteByte(num3);
      }
      else if (fromPayload.Count <= (int) ushort.MaxValue)
      {
        byte num4 = (byte) ((uint) num2 | 126U);
        memoryStream.WriteByte(num4);
        BinaryReaderWriter.WriteUShort((ushort) fromPayload.Count, (Stream) memoryStream, false);
      }
      else
      {
        byte num5 = (byte) ((uint) num2 | (uint) sbyte.MaxValue);
        memoryStream.WriteByte(num5);
        BinaryReaderWriter.WriteULong((ulong) fromPayload.Count, (Stream) memoryStream, false);
      }
      if (isClient)
      {
        byte[] numArray = new byte[4];
        WebSocketFrameWriter._random.NextBytes(numArray);
        memoryStream.Write(numArray, 0, numArray.Length);
        WebSocketFrameCommon.ToggleMask(new ArraySegment<byte>(numArray, 0, numArray.Length), fromPayload);
      }
      memoryStream.Write(fromPayload.Array, fromPayload.Offset, fromPayload.Count);
    }
  }
}
