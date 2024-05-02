

using ChessConsole;
using Hazel;
using MovementEffects;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public abstract class IMiniGame
{
  public const byte MsgTime = 1;
  public const byte MsgJoinLobby = 2;
  public const byte MsgMove = 3;
  public const byte MsgResign = 4;
  public const byte MsgOfferDraw = 5;
  public const byte MsgGameOver = 6;
  public const byte MsgChat = 7;
  public const byte MsgStartGame = 8;
  public const byte MsgQuit = 9;
  public const byte MsgLeaveLobby = 11;
  public const byte MsgGameSettings = 12;
  public const byte MsgCheat = 13;
  public int status;
  public const int status_not_started = 0;
  public const int status_ongoing = 1;
  public const int status_concluded = 2;
  public const int status_clientBusy = 3;
  public int id;
  public bool isServer;
  public bool isClient;
  public bool isReplay;
  public bool isSpectator;
  public IMiniGame.GameType gameType;
  public GameObject uiObject;
  protected IEnumerator<float> updateLoop;
  public List<IMiniGame.Player> players = new List<IMiniGame.Player>();
  public List<Connection> spectators = new List<Connection>();

  public virtual void ServerHandler(Connection c, myBinaryReader r, byte[] bytes)
  {
    throw new NotImplementedException();
  }

  public virtual void ClientHandler(myBinaryReader r) => throw new NotImplementedException();

  public virtual void Serialize(myBinaryWriter w) => throw new NotImplementedException();

  public virtual void Start(bool newGame) => throw new NotImplementedException();

  public virtual void LogicPlayerLeft(Connection c) => throw new NotImplementedException();

  public abstract void Server_SendCreateGame(Connection c);

  public int MaxPlayers => 2;

  public Connection First
  {
    get
    {
      if (this.players.Count <= 0)
        return (Connection) null;
      return this.players[0]?.connection;
    }
  }

  public Connection Second
  {
    get
    {
      if (this.players.Count <= 1)
        return (Connection) null;
      return this.players[1]?.connection;
    }
  }

  public void KillLoop()
  {
    if (this.updateLoop == null)
      return;
    Timing.KillCoroutines(this.updateLoop);
    this.updateLoop = (IEnumerator<float>) null;
  }

  public void AskStartGame(byte gameType, int whoFirst)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write(gameType);
        myBinaryWriter.Write((byte) 8);
        myBinaryWriter.Write(whoFirst);
      }
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }

  public void AskMove(ChessMove cm)
  {
    this.status = 3;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 87);
        w.Write((byte) 3);
        cm.Serialize(w);
      }
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendServerMove(ChessMove cm)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 87);
        w.Write((byte) 3);
        cm.Serialize(w);
      }
      this.SendAll(memoryStream.ToArray());
    }
  }

  public void AskDraw()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 87);
        myBinaryWriter.Write((byte) 5);
      }
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }

  public void AskResign()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 87);
        myBinaryWriter.Write((byte) 4);
      }
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }

  public void SendLeft(Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 87);
        myBinaryWriter.Write((byte) 11);
        myBinaryWriter.Write(c.name);
      }
      this.SendAll(memoryStream.ToArray());
    }
  }

  public void SendAll(byte[] b)
  {
    foreach (IMiniGame.Player player in this.players)
    {
      if (player != null && player.connection.State == ConnectionState.Connected && player.connection.miniGame == this)
        player.connection.SendBytes(b);
    }
    for (int index = this.spectators.Count - 1; index >= 0; --index)
    {
      if (this.spectators[index] != null && this.spectators[index].State == ConnectionState.Connected && this.spectators[index].miniGame == this)
      {
        this.spectators[index].SendBytes(b);
      }
      else
      {
        Connection spectator = this.spectators[index];
        this.spectators.RemoveAt(index);
        if (spectator != null)
          this.SendLeft(spectator);
      }
    }
  }

  public void SendAll(byte gameType, byte tag, string n, int additionalInfo)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write(gameType);
        myBinaryWriter.Write(tag);
        myBinaryWriter.Write(n);
        myBinaryWriter.Write(additionalInfo);
      }
      this.SendAll(memoryStream.ToArray());
    }
  }

  public bool IsFull() => this.players.Count >= this.MaxPlayers;

  public void OnJoinServer(Connection c, bool forceSpectator)
  {
    if (this.gameType == IMiniGame.GameType.Chess)
    {
      if (c.miniGame != null)
        c.miniGame.LogicPlayerLeft(c);
      c.miniGame = this;
      if (this.players.Count < 2 && this.spectators.Count == 0 && !forceSpectator)
      {
        this.players.Add(new IMiniGame.Player()
        {
          connection = c,
          name = c.name
        });
        this.SendAll((byte) 87, (byte) 2, c.name, 1);
      }
      else
      {
        this.spectators.Add(c);
        this.SendAll((byte) 87, (byte) 2, c.name, -1);
      }
    }
    this.Server_SendCreateGame(c);
    MyLog.MainLog("[Joined Minigame Chat " + (object) c.miniGame.id + "] [" + c.name + "]");
  }

  public void Leave(Connection c)
  {
    if (this.players == null)
      return;
    for (int index = 0; index < this.players.Count; ++index)
    {
      if (this.players[index] != null && this.players[index].connection == c)
      {
        if (this.players[index].connection.miniGame == this)
          this.players[index].connection.SendBytes(new byte[2]
          {
            (byte) 87,
            (byte) 9
          });
        this.players[index].connection.miniGame = (IMiniGame) null;
        this.players[index].connection = (Connection) null;
        this.players.RemoveAt(index);
        MyLog.MainLog("[Left Minigame Chat " + (object) c.miniGame.id + "] [" + c.name + "]");
        break;
      }
    }
  }

  public virtual void Dispose()
  {
    this.KillLoop();
    if (this.players != null)
    {
      for (int index = 0; index < this.players.Count; ++index)
      {
        if (this.players[index] != null && this.players[index].connection != null && this.players[index].connection.State == ConnectionState.Connected && this.players[index].connection.miniGame == this)
        {
          this.players[index].connection.SendBytes(new byte[2]
          {
            (byte) 87,
            (byte) 9
          });
          this.players[index].connection.miniGame = (IMiniGame) null;
          this.players[index].connection = (Connection) null;
        }
      }
    }
    if (this.spectators != null)
    {
      for (int index = 0; index < this.spectators.Count; ++index)
      {
        if (this.spectators[index] != null && this.spectators[index] != null && this.spectators[index].State == ConnectionState.Connected && this.spectators[index].miniGame == this)
        {
          this.spectators[index].SendBytes(new byte[2]
          {
            (byte) 87,
            (byte) 9
          });
          this.spectators[index].miniGame = (IMiniGame) null;
        }
      }
    }
    if ((UnityEngine.Object) this.uiObject != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.uiObject);
    this.spectators?.Clear();
    this.players?.Clear();
    this.uiObject = (GameObject) null;
    if (!this.isServer)
      return;
    Server._miniGames.Remove(this.id);
  }

  public class GameSettings
  {
    public float player1Time = 300f;
    public float player1Delay;
    public float player2Time = 300f;
    public float player2Delay;
    public PlayerColorOptions playAs = PlayerColorOptions.Random;

    public IMiniGame.GameSettings Copy()
    {
      return new IMiniGame.GameSettings()
      {
        player1Time = this.player1Time,
        player1Delay = this.player1Delay,
        player2Time = this.player2Time,
        player2Delay = this.player2Delay
      };
    }

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this.player1Time);
      w.Write(this.player1Delay);
      w.Write(this.player2Time);
      w.Write(this.player2Delay);
      w.Write((int) this.playAs);
    }

    public static IMiniGame.GameSettings Desieralize(myBinaryReader r)
    {
      return new IMiniGame.GameSettings()
      {
        player1Time = r.ReadSingle(),
        player1Delay = r.ReadSingle(),
        player2Time = r.ReadSingle(),
        player2Delay = r.ReadSingle(),
        playAs = (PlayerColorOptions) r.ReadInt32()
      };
    }
  }

  public enum GameType
  {
    Chess,
  }

  public class Player
  {
    public string name;
    public Connection connection;
    public float startTime = 20f;
    public float curTime;
    public float curDelay;
    public const byte Version = 1;
    public bool offeringDraw;
    public bool resigned;
    public bool defeated;
    [NonSerialized]
    public float startTurnTime;

    public void SetStartTime() => this.startTime = this.curTime;

    public void Serialize(myBinaryWriter w)
    {
      w.Write((byte) 1);
      w.Write(this.name);
      w.Write(this.startTime);
      w.Write(this.curTime);
      w.Write(this.offeringDraw);
      w.Write(this.resigned);
      w.Write(this.defeated);
      w.Write(this.curDelay);
    }

    public static IMiniGame.Player Deserialize(myBinaryReader r)
    {
      if (r.ReadByte() == (byte) 0)
        return (IMiniGame.Player) null;
      return new IMiniGame.Player()
      {
        name = r.ReadString(),
        startTime = r.ReadSingle(),
        curTime = r.ReadSingle(),
        offeringDraw = r.ReadBoolean(),
        resigned = r.ReadBoolean(),
        defeated = r.ReadBoolean(),
        curDelay = r.ReadSingle()
      };
    }
  }
}
