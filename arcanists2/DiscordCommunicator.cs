

using Hazel;
using Hazel.Tcp;
using System;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
public class DiscordCommunicator
{
  private ConnectionListener listener;
  private Connection connection;
  public const byte MsgStrikes = 101;
  public const byte MsgChatlogs = 100;
  public const byte MsgCommandSpam = 102;
  public const byte MsgResync = 10;
  public const byte MsgPoll = 103;
  public const byte MsgPinPoll = 104;
  public const byte MsgAccountCreated = 105;
  public const byte MsgAccountBlocked = 106;
  public const byte MsgTournamentGame = 107;
  public const byte MsgPlayerCount = 108;
  public static bool LogResync = true;

  public Connection GetConnection => this.connection;

  public DiscordCommunicator() => this.BroadCast();

  public static byte[] GameToResync(ZGame g, string msg, string who, bool isClient)
  {
    StringBuilder stringBuilder = new StringBuilder("[#").Append(g.gameFacts.id).Append("] ").Append("[ ").Append(g.gameFacts.GetTimeInSeconds()).Append(" ").Append(GameFacts.MapShortName(g.gameFacts.GetMapMode())).Append("]");
    for (int index = 0; index < g.players.Count; ++index)
    {
      stringBuilder.Append("[").Append(g.players[index].name).Append("] ");
      if (index < g.players.Count - 1)
        stringBuilder.Append(", ");
    }
    stringBuilder.Append(" >>> " + msg);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
      {
        writer.Write((byte) 10);
        writer.Write(stringBuilder.ToString());
        writer.Write(g.gameFacts.id);
        writer.Write(Inert.Version);
        writer.Write(isClient);
        writer.Write(who);
        g.gameFacts.ManualSerialize(writer, true);
        writer.Write(g.timeline.Count);
        for (int index = 0; index < g.timeline.Count; ++index)
          writer.Write(g.timeline[index]);
        writer.Write(0);
      }
      return memoryStream.ToArray();
    }
  }

  public void SendResync(ZGame g, string msg)
  {
    if (!DiscordCommunicator.LogResync || this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    byte[] resync = DiscordCommunicator.GameToResync(g, msg, "Server", false);
    this.connection?.SendBytes(resync);
    if (!((UnityEngine.Object) Server.Instance == (UnityEngine.Object) null) && Server.Instance.communicator != null && Server.Instance.communicator.GetConnection != null && Server.Instance.communicator.GetConnection.State == ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream(resync))
    {
      using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
      {
        int num = (int) r.ReadByte();
        DiscordCommunicator.SaveReplayAnyway(r, "(Server)");
      }
    }
  }

  public static void SaveReplayAnyway(myBinaryReader r, string clientName)
  {
    r.ReadString();
    int num = r.ReadInt32();
    byte[] bytes = r.ReadBytes((int) (r.BaseStream.Length - r.BaseStream.Position));
    if (!Directory.Exists(Application.persistentDataPath + "/Replays/"))
      Directory.CreateDirectory(Application.persistentDataPath + "/Replays/");
    File.WriteAllBytes(Application.persistentDataPath + "/Replays/" + num.ToString() + clientName + ".arcreplay", bytes);
  }

  public void SendGame(ZGame g, bool tournament = false)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    StringBuilder stringBuilder = new StringBuilder("[#").Append(g.gameFacts.id).Append("] ").Append("[").Append(g.gameFacts.GetTimeInSeconds()).Append(" ").Append(GameFacts.MapShortName(g.gameFacts.GetMapMode())).Append("]");
    if (tournament)
    {
      for (int index = 0; index < g.players.Count; ++index)
      {
        if (g.players[index].winner)
          stringBuilder.Append("**");
        stringBuilder.Append("[").Append(g.players[index].name).Append("]");
        if (g.players[index].winner)
          stringBuilder.Append("**");
        if (index < g.players.Count - 1)
          stringBuilder.Append(", ");
      }
    }
    else
    {
      for (int index = 0; index < g.players.Count; ++index)
      {
        if (g.players[index].addedRating > (short) 0)
          stringBuilder.Append("**");
        stringBuilder.Append("[").Append(g.players[index].name).Append("] ").Append(g.players[index].startingRating).Append(" {").Append(g.players[index].addedRating > (short) 0 ? "+" : "").Append(g.players[index].addedRating).Append("}");
        if (g.players[index].addedRating > (short) 0)
          stringBuilder.Append("**");
        if (index < g.players.Count - 1)
          stringBuilder.Append(", ");
      }
    }
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
      {
        writer.Write(tournament ? (byte) 107 : (byte) 35);
        writer.Write(stringBuilder.ToString());
        writer.Write(g.gameFacts.id);
        writer.Write(Inert.Version);
        writer.Write(false);
        writer.Write("Server");
        g.gameFacts.ManualSerialize(writer, true);
        writer.Write(g.timeline.Count);
        for (int index = 0; index < g.timeline.Count; ++index)
          writer.Write(g.timeline[index]);
        writer.Write(0);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendCommandSpam(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 102);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendMsg(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 153);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendError(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 207);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendAccountCreation(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 105);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendAccountBlocked(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 106);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendChatLogs()
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 100);
        myBinaryWriter.Write(MyLog.GetAllText());
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendPoll(string who, string responses, bool isPublic)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 103);
        myBinaryWriter.Write(who);
        myBinaryWriter.Write(responses);
        myBinaryWriter.Write(isPublic);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendPinPoll(string title, string responses, bool isPublic)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 103);
        myBinaryWriter.Write(title);
        myBinaryWriter.Write(responses);
        myBinaryWriter.Write(isPublic);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendReport(Report r)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
      {
        writer.Write((byte) 55);
        r.Serialize(writer);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendServerMsg(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 33);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendInGameReportMsg(string s)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 65);
        myBinaryWriter.Write(s);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendResetPassword(ulong id, string pass, string user, string acc)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 48);
        myBinaryWriter.Write(id);
        myBinaryWriter.Write(pass);
        myBinaryWriter.Write(user);
        myBinaryWriter.Write(acc);
      }
      this.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendBytes(byte[] b)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return;
    this.connection?.SendBytes(b);
  }

  public void StopBroadCast()
  {
    if (this.listener == null)
      return;
    this.listener.Dispose();
  }

  public void BroadCast()
  {
    try
    {
      this.listener = (ConnectionListener) new TcpConnectionListener(new NetworkEndPoint("127.0.0.1", 4199));
      this.listener.NewConnection += new EventHandler<NewConnectionEventArgs>(this.NewConnectionHandler);
      this.listener.Start();
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  private void NewConnectionHandler(object sender, NewConnectionEventArgs args)
  {
    this.connection = args.Connection;
    args.Connection.DataReceived += new EventHandler<DataReceivedEventArgs>(this.AuthHandler);
    args.Connection.Disconnected += new EventHandler<DisconnectedEventArgs>(this.Disconnected);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        myBinaryWriter.Write((byte) 1);
      args.Connection.SendBytes(memoryStream.ToArray());
    }
    args.Recycle();
  }

  private void AuthHandler(object sender, DataReceivedEventArgs args)
  {
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() => args.Recycle()));
  }

  private void Disconnected(object sender, DisconnectedEventArgs args)
  {
  }
}
