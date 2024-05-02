﻿// Decompiled with JetBrains decompiler
// Type: Spectator
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using Hazel.Tcp;
using System;
using System.IO;
using UnityEngine;

#nullable disable
public class Spectator
{
  public static Connection connection;
  public static bool spectating;
  public const byte MsgJoined = 1;
  public const byte MsgLeft = 2;
  public const byte MsgMove = 3;
  public const byte MsgEmote = 4;
  public const byte MsgResync = 5;
  public const byte MsgEmoji = 6;
  public const byte MsgAction = 7;

  public static bool isConnected
  {
    get => Spectator.connection != null && Spectator.connection.State == ConnectionState.Connected;
  }

  public static Connection GetBoatConnection()
  {
    return !Spectator.isConnected ? Client.connection : Spectator.connection;
  }

  public static void AskToJoinBoat()
  {
    Connection connection = Spectator.isConnected ? Spectator.connection : Client.connection;
    if (connection == null || connection.State != ConnectionState.Connected || connection.player.inBoat || Client.game.resyncing)
      return;
    connection.SendBytes(new byte[2]{ (byte) 83, (byte) 1 });
  }

  public static void AskToLeaveBoat()
  {
    Connection connection = Spectator.isConnected ? Spectator.connection : Client.connection;
    if (connection == null || connection.State != ConnectionState.Connected || !connection.player.inBoat)
      return;
    connection.SendBytes(new byte[2]{ (byte) 83, (byte) 2 });
  }

  public static void AskToMove(int x, int y)
  {
    Connection connection = Spectator.isConnected ? Spectator.connection : Client.connection;
    if (connection == null || connection.State != ConnectionState.Connected || !connection.player.inBoat)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 83);
        myBinaryWriter.Write((byte) 3);
        myBinaryWriter.Write(connection.player.id);
        myBinaryWriter.Write((short) x);
        myBinaryWriter.Write((short) y);
      }
      connection.SendBytes(memoryStream.ToArray());
    }
  }

  public static void GameClientHandler(object sender, DataReceivedEventArgs args)
  {
    ZGame.GameClientHandler(sender, args);
  }

  public static void ConnectToGame()
  {
    Spectator.connection = (Connection) new TcpConnection(new NetworkEndPoint(Client.currentIP, Client.port));
    Spectator.connection.DataReceived += new EventHandler<DataReceivedEventArgs>(Spectator.GameClientHandler);
    Spectator.connection.Disconnected += new EventHandler<DisconnectedEventArgs>(Spectator.Disconnected);
    try
    {
      Spectator.connection.Connect();
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void Disconnect()
  {
    if (Spectator.connection == null)
      return;
    Spectator.connection.Disconnected -= new EventHandler<DisconnectedEventArgs>(Spectator.Disconnected);
    Spectator.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(Spectator.GameClientHandler);
    try
    {
      Spectator.connection.Dispose();
    }
    catch (Exception ex)
    {
      Debug.Log((object) ex);
    }
    Spectator.connection = (Connection) null;
    Client.SendToServer((byte) 52);
  }

  private static void Disconnected(object sender, DisconnectedEventArgs args)
  {
    Spectator.Disconnect();
  }
}