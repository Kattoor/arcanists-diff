// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.BinaryReaderWriter
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  internal class BinaryReaderWriter
  {
    public static async Task ReadExactly(
      int length,
      Stream stream,
      ArraySegment<byte> buffer,
      CancellationToken cancellationToken)
    {
      if (buffer.Count < length)
        throw new InternalBufferOverflowException(string.Format("Unable to read {0} bytes into buffer (offset: {1} size: {2}). Use a larger read buffer", (object) length, (object) buffer.Offset, (object) buffer.Count));
      int num;
      for (int offset = 0; offset < length; offset += num)
      {
        if (stream is NetworkStream networkStream && networkStream.DataAvailable)
          num = stream.Read(buffer.Array, buffer.Offset + offset, length - offset);
        else
          num = await stream.ReadAsync(buffer.Array, buffer.Offset + offset, length - offset, cancellationToken);
        if (num == 0)
          throw new EndOfStreamException(string.Format("Unexpected end of stream encountered whilst attempting to read {0:#,##0} bytes", (object) length));
      }
    }

    public static async Task<ushort> ReadUShortExactly(
      Stream stream,
      bool isLittleEndian,
      ArraySegment<byte> buffer,
      CancellationToken cancellationToken)
    {
      await BinaryReaderWriter.ReadExactly(2, stream, buffer, cancellationToken);
      if (!isLittleEndian)
        Array.Reverse((Array) buffer.Array, buffer.Offset, 2);
      return BitConverter.ToUInt16(buffer.Array, buffer.Offset);
    }

    public static async Task<ulong> ReadULongExactly(
      Stream stream,
      bool isLittleEndian,
      ArraySegment<byte> buffer,
      CancellationToken cancellationToken)
    {
      await BinaryReaderWriter.ReadExactly(8, stream, buffer, cancellationToken);
      if (!isLittleEndian)
        Array.Reverse((Array) buffer.Array, buffer.Offset, 8);
      return BitConverter.ToUInt64(buffer.Array, buffer.Offset);
    }

    public static async Task<long> ReadLongExactly(
      Stream stream,
      bool isLittleEndian,
      ArraySegment<byte> buffer,
      CancellationToken cancellationToken)
    {
      await BinaryReaderWriter.ReadExactly(8, stream, buffer, cancellationToken);
      if (!isLittleEndian)
        Array.Reverse((Array) buffer.Array, buffer.Offset, 8);
      return BitConverter.ToInt64(buffer.Array, buffer.Offset);
    }

    public static void WriteInt(int value, Stream stream, bool isLittleEndian)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      if (BitConverter.IsLittleEndian && !isLittleEndian)
        Array.Reverse((Array) bytes);
      stream.Write(bytes, 0, bytes.Length);
    }

    public static void WriteULong(ulong value, Stream stream, bool isLittleEndian)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      if (BitConverter.IsLittleEndian && !isLittleEndian)
        Array.Reverse((Array) bytes);
      stream.Write(bytes, 0, bytes.Length);
    }

    public static void WriteLong(long value, Stream stream, bool isLittleEndian)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      if (BitConverter.IsLittleEndian && !isLittleEndian)
        Array.Reverse((Array) bytes);
      stream.Write(bytes, 0, bytes.Length);
    }

    public static void WriteUShort(ushort value, Stream stream, bool isLittleEndian)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      if (BitConverter.IsLittleEndian && !isLittleEndian)
        Array.Reverse((Array) bytes);
      stream.Write(bytes, 0, bytes.Length);
    }
  }
}
