// Decompiled with JetBrains decompiler
// Type: SevenZip.Compression.LZ.IInWindowStream
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.IO;

#nullable disable
namespace SevenZip.Compression.LZ
{
  internal interface IInWindowStream
  {
    void SetStream(Stream inStream);

    void Init();

    void ReleaseStream();

    byte GetIndexByte(int index);

    uint GetMatchLen(int index, uint distance, uint limit);

    uint GetNumAvailableBytes();
  }
}
