

using System.Collections.Generic;
using System.IO;

#nullable disable
public class AllRankingsContainer
{
  public List<JsonGamesLow> low = new List<JsonGamesLow>();
  public List<JsonGamesHigh> high = new List<JsonGamesHigh>();
  public List<JsonGamesParty> party = new List<JsonGamesParty>();

  public byte[] SerializeSelf(byte type)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 68);
        w.Write(type);
        w.Write(this.low.Count);
        for (int index = 0; index < this.low.Count; ++index)
        {
          w.Write(this.low[index].name);
          this.low[index].games.Serialize(w);
        }
        w.Write(this.high.Count);
        for (int index = 0; index < this.high.Count; ++index)
        {
          w.Write(this.high[index].name);
          this.high[index].games.Serialize(w);
        }
        w.Write(this.party.Count);
        for (int index = 0; index < this.party.Count; ++index)
        {
          w.Write(this.party[index].name);
          this.party[index].games.Serialize(w);
        }
      }
      return memoryStream.ToArray();
    }
  }

  public byte[] Serialize()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 68);
        w.Write((byte) 0);
        w.Write(this.low.Count);
        for (int index = 0; index < this.low.Count; ++index)
        {
          w.Write(this.low[index].name);
          this.low[index].games.Serialize(w);
          w.Write(this.high[index].name);
          this.high[index].games.Serialize(w);
          w.Write(this.party[index].name);
          this.party[index].games.Serialize(w);
        }
      }
      return memoryStream.ToArray();
    }
  }

  public static AllRankingsContainer DeserializeSelf(myBinaryReader r)
  {
    AllRankingsContainer rankingsContainer = new AllRankingsContainer();
    int num1 = r.ReadInt32();
    for (int index = 0; index < num1; ++index)
      rankingsContainer.low.Add(new JsonGamesLow()
      {
        name = r.ReadString(),
        games = NewGamesPlayed.Deserialize(r)
      });
    int num2 = r.ReadInt32();
    for (int index = 0; index < num2; ++index)
      rankingsContainer.high.Add(new JsonGamesHigh()
      {
        name = r.ReadString(),
        games = NewGamesPlayed.Deserialize(r)
      });
    int num3 = r.ReadInt32();
    for (int index = 0; index < num3; ++index)
      rankingsContainer.party.Add(new JsonGamesParty()
      {
        name = r.ReadString(),
        games = NewGamesPlayed.Deserialize(r)
      });
    return rankingsContainer;
  }

  public static AllRankingsContainer Deserialize(myBinaryReader r)
  {
    AllRankingsContainer rankingsContainer = new AllRankingsContainer();
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
    {
      rankingsContainer.low.Add(new JsonGamesLow()
      {
        name = r.ReadString(),
        games = NewGamesPlayed.Deserialize(r)
      });
      rankingsContainer.high.Add(new JsonGamesHigh()
      {
        name = r.ReadString(),
        games = NewGamesPlayed.Deserialize(r)
      });
      rankingsContainer.party.Add(new JsonGamesParty()
      {
        name = r.ReadString(),
        games = NewGamesPlayed.Deserialize(r)
      });
    }
    return rankingsContainer;
  }
}
