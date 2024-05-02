// Decompiled with JetBrains decompiler
// Type: GameFactsExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using UnityEngine;

#nullable disable
public static class GameFactsExtension
{
  public static void ManualSerialize(
    this GameFacts gf,
    myBinaryWriter writer,
    bool includeSentinels = false)
  {
    writer.Write(gf.id);
    writer.Write(Math.Max(gf.connections.Count, gf.players.Count));
    if (includeSentinels)
    {
      writer.Write((int) gf.realMap);
      if (gf.accounts.Count == gf.players.Count)
      {
        writer.Write(0);
        writer.Write(gf.seed);
        for (int index = 0; index < gf.players.Count; ++index)
        {
          gf.accounts[index].Serialize(writer);
          gf.settingsPlayer[index].Serialize(writer);
        }
      }
      else
      {
        writer.Write(gf.id);
        writer.Write(gf.seed);
        for (int index = 0; index < gf.connections.Count; ++index)
        {
          gf.connections[index].player.account.Serialize(writer);
          gf.connections[index].player.settingsPlayer.Serialize(writer);
        }
      }
    }
    else
    {
      if (gf.players.Count > gf.connections.Count)
      {
        foreach (string player in gf.players)
          writer.Write(player);
      }
      else
      {
        for (int index = 0; index < gf.connections.Count; ++index)
          writer.Write(gf.connections[index].player.account.name);
      }
      writer.Write(gf.invitedPlayers.Count);
      foreach (string invitedPlayer in gf.invitedPlayers)
        writer.Write(invitedPlayer);
    }
    gf.settings.Serialize(writer);
    writer.Write(gf.status);
    writer.Write((int) ((double) Time.realtimeSinceStartup - (double) gf.serverStartTime));
  }

  public static void ManualDeserialize(
    this GameFacts gf,
    myBinaryReader reader,
    bool includeSentinels = false,
    bool fromServer = false)
  {
    gf.game = new ZGame();
    gf.game.gameFacts = gf;
    gf.players.Clear();
    gf.settingsPlayer.Clear();
    gf.invitedPlayers.Clear();
    gf.accounts.Clear();
    gf.connections.Clear();
    gf.id = reader.ReadInt32();
    int num1 = reader.ReadInt32();
    if (includeSentinels)
    {
      gf.realMap = (MapEnum) reader.ReadInt32();
      gf.CreateGameFromGF = true;
      gf.port = reader.ReadInt32();
      gf.seed = reader.ReadInt32();
      for (int index = 0; index < num1; ++index)
      {
        Account account = new Account();
        account.Deserialize(reader);
        gf.players.Add(account.name);
        SettingsPlayer settingsPlayer = new SettingsPlayer();
        settingsPlayer.Deserialize(reader);
        gf.settingsPlayer.Add(settingsPlayer);
        gf.accounts.Add(account);
      }
    }
    else
    {
      for (int index = 0; index < num1; ++index)
      {
        string str = reader.ReadString();
        gf.players.Add(str);
      }
      int num2 = reader.ReadInt32();
      for (int index = 0; index < num2; ++index)
        gf.invitedPlayers.Add(reader.ReadString());
    }
    gf.settings.Deserialize(reader);
    gf.status = reader.ReadByte();
    gf.serverStartTime = (int) ((double) Controller.realtimeSinceStartup - (double) reader.ReadInt32());
    if (!includeSentinels)
      return;
    gf.SetMapMode(gf.realMap);
  }

  public static void ShufflePlayers(this GameFacts gf)
  {
    gf.originalOrder.Clear();
    for (int index = 0; index < gf.connections.Count; ++index)
      gf.originalOrder.Add(gf.connections[index].player.account.name);
    if (!gf.GetStyle().HasStyle(GameStyle.Shuffle_Players) || gf.connections.Count <= 1)
      return;
    for (int index1 = 0; index1 < 7; ++index1)
    {
      for (int index2 = 0; index2 < gf.connections.Count; ++index2)
      {
        int index3 = Server.random.Next(0, gf.connections.Count);
        if (index3 != index2)
        {
          Connection connection = gf.connections[index2];
          gf.connections[index2] = gf.connections[index3];
          gf.connections[index3] = connection;
        }
      }
    }
  }
}
