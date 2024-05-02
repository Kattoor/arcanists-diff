// Decompiled with JetBrains decompiler
// Type: RatedContainer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

#nullable disable
public class RatedContainer
{
  public List<RatedFacts> list = new List<RatedFacts>();
  public const byte Version = 3;
  public int diff = 400;

  public void IncreaseDiff()
  {
    if (this.diff >= 1000)
      return;
    this.diff += 200;
  }

  public void AddDefault() => this.list.Add(new RatedFacts());

  public static RatedContainer Deserialize(myBinaryReader r)
  {
    RatedContainer ratedContainer = new RatedContainer();
    byte version = r.ReadByte();
    if (version == (byte) 1)
    {
      RatedFacts ratedFacts = new RatedFacts();
      ratedFacts._Deserialize(1, r);
      ratedContainer.list.Add(ratedFacts);
      return ratedContainer;
    }
    int num = r.ReadInt32();
    if (num > 10)
      num = 10;
    for (int index = 0; index < num; ++index)
      ratedContainer.list.Add(RatedFacts.Deserialize((int) version, r));
    if (num == 0)
      ratedContainer.AddDefault();
    return ratedContainer;
  }

  public void Serialize(myBinaryWriter w, bool includeBook = true)
  {
    if (this.list.Count == 0)
      this.AddDefault();
    w.Write((byte) 3);
    w.Write(this.list.Count);
    foreach (RatedFacts ratedFacts in this.list)
      ratedFacts.Serialize(w, includeBook);
  }

  public void SerializeToFile(string s)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        this.Serialize(w);
      File.WriteAllBytes(s, memoryStream.ToArray());
    }
  }

  public static RatedContainer DeserializeFromFile(string s)
  {
    using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(s)))
    {
      using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
        return RatedContainer.Deserialize(r);
    }
  }

  public void Copy(RatedContainer g)
  {
    this.list.Clear();
    for (int index = 0; index < g.list.Count; ++index)
      this.list.Add(g.list[index].Copy());
    if (this.list.Count != 0)
      return;
    this.AddDefault();
  }
}
