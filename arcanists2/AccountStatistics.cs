// Decompiled with JetBrains decompiler
// Type: AccountStatistics
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AccountStatistics
{
  public class PastGames
  {
    public long date;
    public string[] players;
    public int gameModes;
    public int gameModes2;
    public bool winner;

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this.winner);
      w.Write(this.date);
      w.Write(this.gameModes);
      w.Write(this.gameModes2);
      w.Write(this.players.Length);
      for (int index = 0; index < this.players.Length; ++index)
        w.Write(this.players[index]);
    }

    public string GetDate() => DateTime.FromBinary(this.date).ToShortDateString();

    public static AccountStatistics.PastGames Deserialize(myBinaryReader r)
    {
      AccountStatistics.PastGames pastGames = new AccountStatistics.PastGames();
      pastGames.winner = r.ReadBoolean();
      pastGames.date = r.ReadInt64();
      pastGames.gameModes = r.ReadInt32();
      pastGames.gameModes2 = r.ReadInt32();
      int length = r.ReadInt32();
      pastGames.players = new string[length];
      for (int index = 0; index < length; ++index)
        pastGames.players[index] = r.ReadString();
      return pastGames;
    }
  }
}
