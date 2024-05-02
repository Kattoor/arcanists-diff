// Decompiled with JetBrains decompiler
// Type: Server
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using Hazel.Tcp;
using mattmc3.dotmore.Collections.Generic;
using MyPolling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityThreading;

#nullable disable
public class Server : MonoBehaviour
{
  private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
  public static bool CompressMap = true;
  public static string discordLink = "https://discord.gg/PNnYxsc";
  public static string wikiLink = "https://arcanists2.fandom.com/wiki/Arcanists_2_Wiki";
  public static string websiteLink = "https://www.arcanists2.com/";
  public static int raidMode = 1;
  public static int chatMode = 0;
  public static int vpn_block = 88;
  public static byte[] poll;
  public static bool DebugResyncs = true;
  public static bool testing = false;
  public static MyPoll activePoll;
  public static byte[] cachedPollResponses;
  public const string prefPoll = "prefpollID";
  private ConnectionListener listener;
  private WebListener webListenerInsecure;
  public DiscordCommunicator communicator;
  public static OrderedDictionary<Hazel.Connection, Hazel.Connection> _connections = new OrderedDictionary<Hazel.Connection, Hazel.Connection>();
  public static Dictionary<string, Hazel.Connection> _names = new Dictionary<string, Hazel.Connection>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
  public static OrderedDictionary<Hazel.Connection, Hazel.Connection> _MainMenu = new OrderedDictionary<Hazel.Connection, Hazel.Connection>();
  public static OrderedDictionary<Hazel.Connection, Hazel.Connection> _lobby = new OrderedDictionary<Hazel.Connection, Hazel.Connection>();
  public static OrderedDictionary<Hazel.Connection, Hazel.Connection> _unrated = new OrderedDictionary<Hazel.Connection, Hazel.Connection>();
  public static OrderedDictionary<int, GameFacts> _games = new OrderedDictionary<int, GameFacts>();
  public static OrderedDictionary<int, IMiniGame> _miniGames = new OrderedDictionary<int, IMiniGame>();
  public static OrderedDictionary<Hazel.Connection, RatedContainer> _ratedQueue = new OrderedDictionary<Hazel.Connection, RatedContainer>();
  public static OrderedDictionary<int, RatedContainer> _ratedBroadcast = new OrderedDictionary<int, RatedContainer>();
  public static int randomRatedFactsID = 1;
  public static Dictionary<string, Clan> _clans = new Dictionary<string, Clan>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
  public static Dictionary<int, Report> reports = new Dictionary<int, Report>();
  public static Dictionary<string, ValidSpectator> validSpectator = new Dictionary<string, ValidSpectator>();
  public static System.Random random = new System.Random();
  public static float realTimeSinceStartup = 0.0f;
  public static byte[] compressedClanOutfits = (byte[]) null;
  public static byte[] compressedRatedRestrictions = (byte[]) null;
  public static Restrictions _restrictions;
  public static RatedFacts _defaultRatedFacts;
  public bool isReady;
  public static bool allowUnverifiedUsersToTalk = false;
  public static bool allowGameCreation = true;
  public static bool logNetworkErrors = false;
  public static string[] reportableOffenses = new string[14]
  {
    "Seriously Offensive Language",
    "Password scamming",
    "Bug abuse",
    "Staff impersonation",
    "Account sharing/trading",
    "Macroing",
    "Rating minipulation",
    "Encouraging others to break rules",
    "Misuse of customer support",
    "Advertising / website",
    "Breaking real-world laws",
    "Asking for personal details",
    "Offensive account name",
    "Hacking"
  };
  public static Server.ServerSettings settings = new Server.ServerSettings();
  private static Cosmetics _defcos = (Cosmetics) null;
  private static int Next_miniGame_ID = 0;
  private static int Next_Game_ID = 0;
  private static byte errorMsg = 0;
  public static System.Random randomNumberGenerator = new System.Random();
  private static GameFacts tempFacts = (GameFacts) null;
  private static float nextKeepAlive = 60f;
  private static float nextRatedCheck = 0.0f;
  private static float nextBookClub = 0.0f;
  private static byte[] KeepAliveBytes = new byte[1]
  {
    (byte) 34
  };

  internal static StringComparer _caseInsensitiveComparer => StringComparer.OrdinalIgnoreCase;

  public static Server Instance { get; private set; }

  public static bool IsReady
  {
    get
    {
      Server instance = Server.Instance;
      return instance != null && instance.isReady;
    }
  }

  public static Cosmetics DefaultCosmetics
  {
    get
    {
      if (Server._defcos == null)
      {
        Server._defcos = new Cosmetics();
        Server._defcos.SetAll();
        Server._defcos.LockRest();
        Server._defcos.body[65] = false;
        Server._defcos.body[67] = false;
        Server._defcos.body[68] = false;
        Server._defcos.body[69] = false;
        Server._defcos.body[70] = false;
        Server._defcos.body[71] = false;
        Server._defcos.body[76] = false;
        Server._defcos.body[98] = false;
        Server._defcos.body[101] = false;
        Server._defcos.body[SettingsPlayer.strelizia_body] = false;
        Server._defcos.body[SettingsPlayer.quinn_body2] = false;
        Server._defcos.head[59] = false;
        Server._defcos.head[61] = false;
        Server._defcos.head[62] = false;
        Server._defcos.head[63] = false;
        Server._defcos.head[64] = false;
        Server._defcos.head[67] = false;
        Server._defcos.head[78] = false;
        Server._defcos.head[81] = false;
        Server._defcos.leftArm[64] = false;
        Server._defcos.leftArm[65] = false;
        Server._defcos.leftArm[66] = false;
        Server._defcos.leftArm[67] = false;
        Server._defcos.leftArm[105] = false;
        Server._defcos.leftArm[107] = false;
        Server._defcos.leftArm[SettingsPlayer.strelizia_mHand] = false;
        Server._defcos.rightArm[63] = false;
        Server._defcos.rightArm[64] = false;
        Server._defcos.rightArm[65] = false;
        Server._defcos.rightArm[66] = false;
        Server._defcos.rightArm[67] = false;
        Server._defcos.rightArm[68] = false;
        Server._defcos.rightArm[70] = false;
        Server._defcos.rightArm[73] = false;
        Server._defcos.rightArm[77] = false;
        Server._defcos.rightArm[84] = false;
        Server._defcos.rightArm[88] = false;
        Server._defcos.rightArm[119] = false;
        Server._defcos.rightArm[121] = false;
        Server._defcos.rightArm[123] = false;
        Server._defcos.rightArm[SettingsPlayer.strelizia_pHand] = false;
        Server._defcos.rightArm[SettingsPlayer.godsword] = false;
        Server._defcos.rightArm[SettingsPlayer.olympic_torch] = false;
        Server._defcos.rightArm[SettingsPlayer.cosmos_helper] = false;
        Server._defcos.rightArm[157] = false;
        Server._defcos.rightArm[158] = false;
        Server._defcos.rightArm[159] = false;
        Server._defcos.rightArm[160] = false;
        Server._defcos.rightArm[161] = false;
        Server._defcos.rightArm[162] = false;
        Server._defcos.rightArm[163] = false;
        Server._defcos.hat[74] = false;
        Server._defcos.hat[76] = false;
        Server._defcos.hat[78] = false;
        Server._defcos.hat[99] = false;
        Server._defcos.hat[SettingsPlayer.strelizia_hat] = false;
        Server._defcos.beard[57] = false;
        Server._defcos.beard[59] = false;
        Server._defcos.beard[60] = false;
        Server._defcos.beard[62] = false;
        Server._defcos.beard[64] = false;
        Server._defcos.rightArm[164] = false;
        Server._defcos.leftArm[126] = false;
        Server._defcos.hat[160] = false;
        Server._defcos.head[94] = false;
        Server._defcos.body[144] = false;
        Server._defcos.body[88] = false;
        Server._defcos.head[73] = false;
        Server._defcos.leftArm[90] = false;
        Server._defcos.rightArm[106] = false;
        Server._defcos.rightArm[107] = false;
        Server._defcos.beard[75] = false;
        SettingsPlayer.InitUnlockables(Server._defcos);
        Server._defcos.spells.ResetAll();
        Prestige.Unlock(Server._defcos, BookOf.Arcane, true, true);
        Prestige.Unlock(Server._defcos, BookOf.Flame, true);
      }
      return Server._defcos;
    }
  }

  public static void StopBroadCast()
  {
    if ((UnityEngine.Object) Server.Instance != (UnityEngine.Object) null && Server.Instance.listener != null)
      Server.Instance.listener.Dispose();
    if ((UnityEngine.Object) Server.Instance != (UnityEngine.Object) null && Server.Instance.webListenerInsecure != null)
      Server.Instance.webListenerInsecure.Dispose();
    foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> connection in Server._connections)
      connection.Key.Close();
  }

  private void Start()
  {
    Server.Instance = this;
    Dispatcher dispatcher = UnityThreadHelper.Dispatcher;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 1);
        myBinaryWriter.Write(Inert.Version);
      }
      Inert.Version_As_Bytes = memoryStream.ToArray();
    }
    Server.BroadCast();
    Server.Next_Game_ID = PlayerPrefs.GetInt("gameID", 0);
    try
    {
      Server.ServerSettings.Load();
    }
    catch (Exception ex)
    {
    }
  }

  public static void BroadCast()
  {
    if (!Application.isBatchMode)
    {
      Server._connections.Clear();
      Server._names.Clear();
      Server._games.Clear();
      Server._MainMenu.Clear();
      Server._lobby.Clear();
      Server._unrated.Clear();
    }
    try
    {
      Server.Instance.listener = (ConnectionListener) new TcpConnectionListener(new NetworkEndPoint(IPAddress.Any, Client.port));
      Server.Instance.listener.NewConnection += new EventHandler<NewConnectionEventArgs>(Server.NewConnectionHandler);
      Server.Instance.webListenerInsecure = new WebListener();
      Server.Instance.webListenerInsecure.NewConnection += new EventHandler<NewConnectionEventArgs>(Server.NewConnectionHandler);
      Server.Instance.listener.Start();
      Server.Instance.webListenerInsecure.Listen(Client.webPortInsecure);
      Server.Instance.communicator = new DiscordCommunicator();
      Application.logMessageReceived += new Application.LogCallback(Server.HandleException);
    }
    catch (Exception ex)
    {
      Debug.Log((object) ex);
    }
  }

  private static void Server_ReceivedError(int arg1, Exception arg2)
  {
    Debug.LogError((object) ("Web Error: " + (object) arg1 + " " + arg2.ToString()));
  }

  private static void Server_ReceivedErrorInsecure(int arg1, Exception arg2)
  {
    Debug.LogError((object) ("Web Error Insecure: " + (object) arg1 + " " + arg2.ToString()));
  }

  private static void HandleException(string condition, string stackTrace, LogType type)
  {
    if (type != LogType.Error && type != LogType.Exception)
      return;
    Server.Instance?.communicator?.SendError(condition + " >>>>> " + stackTrace);
  }

  private void OnDestroy()
  {
    Server.StopBroadCast();
    if (!((UnityEngine.Object) Server.Instance == (UnityEngine.Object) this))
      return;
    Server.Instance = (Server) null;
  }

  public static string GetTime() => "[" + DateTime.Now.ToString("M/d h:mm tt") + "] ";

  private static void SendMessage(Hazel.Connection c, byte b)
  {
    c?.SendBytes(new byte[1]{ b });
  }

  public static void SendInfo(byte[] v)
  {
    foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> connection in Server._connections)
    {
      if (connection.Key.State == ConnectionState.Connected)
        connection.Key.SendBytes(v);
    }
  }

  public static void SendAchievement(Hazel.Connection c, Achievement a)
  {
    try
    {
      c.SendBytes(new byte[2]{ (byte) 51, (byte) a });
    }
    catch (Exception ex)
    {
    }
  }

  private static void SendLobbyMessage(byte[] b)
  {
    foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby)
    {
      if (keyValuePair.Key.State == ConnectionState.Connected)
        keyValuePair.Key.SendBytes(b);
    }
  }

  private static void SendUnratedMessage(GameFacts g, byte[] b)
  {
    for (int index = 0; index < g.connections.Count; ++index)
    {
      if (g.connections[index].State == ConnectionState.Connected)
        g.connections[index].SendBytes(b);
    }
  }

  public static void SendLobbyUnrated(byte[] b)
  {
    foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
    {
      if (keyValuePair.Key.State == ConnectionState.Connected)
        keyValuePair.Key.SendBytes(b);
    }
  }

  public static void UpdateCosmetics(Account a)
  {
    Hazel.Connection c;
    if (Server._names.TryGetValue(a.name, out c))
      Server.UpdateCosmetics(c);
    a.ToFile();
  }

  public static void UpdateCosmetics(Hazel.Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 73);
        c.player.account.cosmetics.Serialize(w);
        w.Write(c.player.account.bonusExperience);
      }
      if (c == null || c.State != ConnectionState.Connected)
        return;
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public static void UpdateAttribute(Hazel.Connection c, byte which, Action<myBinaryWriter> a)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 97);
        myBinaryWriter.Write(which);
        a(myBinaryWriter);
      }
      if (c == null || c.State != ConnectionState.Connected)
        return;
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public static void UpdateAccountInfo(Account a, bool save = true)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 42);
        a.Serialize(w);
      }
      Server.SendInfo(memoryStream.ToArray());
    }
  }

  public static void SyncLobby(Hazel.Connection c, bool lobby = true, bool sendmsg = false)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 10);
        myBinaryWriter.Write(lobby ? -1 : c.player.gameNumber);
        for (int index = Server._connections.Count - 1; index >= 0; --index)
        {
          KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair = Server._connections.GetItem(index);
          if (keyValuePair.Key.State != ConnectionState.Connected)
          {
            keyValuePair = Server._connections.GetItem(index);
            if (keyValuePair.Key.player.gameNumber == -1)
            {
              Dictionary<string, Hazel.Connection> names1 = Server._names;
              keyValuePair = Server._connections.GetItem(index);
              string name1 = keyValuePair.Key.player.account.name;
              if (names1.ContainsKey(name1))
              {
                Dictionary<string, Hazel.Connection> names2 = Server._names;
                keyValuePair = Server._connections.GetItem(index);
                string name2 = keyValuePair.Key.player.account.name;
                names2.Remove(name2);
              }
              Server._connections.RemoveAt(index);
            }
          }
        }
        myBinaryWriter.Write(Server._connections.Count);
        foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> connection in Server._connections)
          connection.Key.player.account.Serialize(myBinaryWriter);
        for (int index = Server._lobby.Count - 1; index >= 0; --index)
        {
          KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair = Server._lobby.GetItem(index);
          if (keyValuePair.Key.State != ConnectionState.Connected)
          {
            string name3 = c.player.account.name;
            keyValuePair = Server._lobby.GetItem(index);
            string name4 = keyValuePair.Key.player.account.name;
            if (!string.Equals(name3, name4, StringComparison.OrdinalIgnoreCase))
            {
              keyValuePair = Server._lobby.GetItem(index);
              Server.RemoveFromLobby(keyValuePair.Key);
            }
            Server._lobby.RemoveAt(index);
          }
        }
        myBinaryWriter.Write(Server._lobby.Count);
        foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby)
          myBinaryWriter.Write(keyValuePair.Key.player.account.name);
        for (int index1 = Server._games.Count - 1; index1 >= 0; --index1)
        {
          GameFacts gg = Server._games.GetItem(index1).Value;
          if ((gg.connections.Count == 0 || gg.connections[0].State != ConnectionState.Connected) && gg.GetInviteMode() != InviteEnum.BookClub)
          {
            if (gg.status == (byte) 0)
            {
              bool flag = false;
              for (int index2 = gg.connections.Count - 1; index2 >= 0; --index2)
              {
                if (gg.connections[index2].State != ConnectionState.Connected)
                {
                  Server.RemoveFromUnrated(gg.connections[index2]);
                  if (gg.connections[index2].player.activeConnection == gg.connections[index2])
                  {
                    gg.connections[index2].player.gameNumber = gg.id;
                    gg.connections[index2].player.location = Location.Disconnecting;
                  }
                  gg.connections.RemoveAt(index2);
                  flag = index2 == 0;
                }
              }
              if (gg.connections.Count == 0)
              {
                if (gg.game != null)
                  gg.game.CleanUp();
                Server.RemoveGameInstance(gg.id);
              }
              else if (flag && !string.IsNullOrEmpty(gg.settings.description))
              {
                gg.settings.description = "";
                Server.SendGameModeUpdate(gg);
              }
            }
            else
            {
              int num = 0;
              for (int index3 = gg.connections.Count - 1; index3 >= 0; --index3)
              {
                if (gg.connections[index3].State != ConnectionState.Connected || gg.connections[index3].player.gameNumber != gg.id)
                  ++num;
              }
              if (num == gg.connections.Count)
              {
                if (gg.game != null)
                  gg.game.CleanUp();
                Server.RemoveGameInstance(gg.id);
              }
            }
          }
        }
        myBinaryWriter.Write(Server._games.Count);
        foreach (KeyValuePair<int, GameFacts> game in Server._games)
          game.Value.ManualSerialize(myBinaryWriter);
        myBinaryWriter.Write(Server._ratedBroadcast.Count);
        foreach (KeyValuePair<int, RatedContainer> keyValuePair in Server._ratedBroadcast)
        {
          myBinaryWriter.Write(keyValuePair.Key);
          keyValuePair.Value.Serialize(myBinaryWriter, false);
        }
        myBinaryWriter.Write("");
        if (Server.activePoll == null)
        {
          myBinaryWriter.Write("");
          myBinaryWriter.Write(-1);
          myBinaryWriter.Write(c.player.account.poll);
        }
        else
        {
          myBinaryWriter.Write(Server.activePoll.name);
          myBinaryWriter.Write(!Server.activePoll.onGoing || c.player.account.poll == Server.activePoll.id ? -1 : Server.activePoll.id);
          myBinaryWriter.Write(c.player.account.poll);
        }
      }
      c.SendBytes(memoryStream.ToArray());
    }
  }

  private static void RemoveFromLobby(Hazel.Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 11);
        myBinaryWriter.Write(c.player.account.name);
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
      {
        if (keyValuePair.Key.State == ConnectionState.Connected)
          keyValuePair.Key.SendBytes(array);
      }
    }
  }

  private static void SendPlayerLocation(Hazel.Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 85);
        myBinaryWriter.Write(c.player.account.name);
        myBinaryWriter.Write(c.player.account.location.Online());
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> connection in Server._connections)
      {
        if (connection.Key.State == ConnectionState.Connected)
          connection.Key.SendBytes(array);
      }
    }
  }

  private static void RemoveFromUnrated(Hazel.Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 8);
        myBinaryWriter.Write(c.player.account.name);
        myBinaryWriter.Write(c.player.gameNumber);
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
      {
        if (keyValuePair.Key.State == ConnectionState.Connected)
          keyValuePair.Key.SendBytes(array);
      }
    }
  }

  private static void AddToUnrated(Hazel.Connection c)
  {
    Server._unrated.Add(c, c);
    GameFacts gameFacts = (GameFacts) null;
    if (!Server._games.TryGetValue(c.player.gameNumber, out gameFacts))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 24);
        myBinaryWriter.Write(c.player.account.name);
        myBinaryWriter.Write(gameFacts.connections.Count);
        for (int index = 0; index < gameFacts.connections.Count; ++index)
          myBinaryWriter.Write(gameFacts.connections[index].player.account.name);
        myBinaryWriter.Write(c.player.gameNumber);
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
      {
        if (keyValuePair.Key.State == ConnectionState.Connected)
          keyValuePair.Key.SendBytes(array);
      }
    }
  }

  private static void AddToLobby(Hazel.Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 5);
        c.player.account.Serialize(w);
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
      {
        if (keyValuePair.Key.State == ConnectionState.Connected)
          keyValuePair.Key.SendBytes(array);
      }
    }
    Server._lobby.Add(c, c);
  }

  public static void OnStoppedSearching(Hazel.Connection c)
  {
    if (c.randomRatedFactsID == 0)
      return;
    Server._ratedBroadcast.Remove(c.randomRatedFactsID);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 6);
        myBinaryWriter.Write(c.randomRatedFactsID);
        myBinaryWriter.Write(false);
      }
      Server.SendLobbyUnrated(memoryStream.ToArray());
    }
    c.randomRatedFactsID = 0;
  }

  public static void OnStartedSearching(Hazel.Connection c, RatedContainer r)
  {
    Server._ratedBroadcast.Add(c.randomRatedFactsID, r);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 6);
        w.Write(c.randomRatedFactsID);
        w.Write(true);
        r.Serialize(w, false);
      }
      Server.SendLobbyUnrated(memoryStream.ToArray());
    }
  }

  public static void MovePlayer(Hazel.Connection c, Location loc)
  {
    if (c.player.spectator)
    {
      try
      {
        c.SendBytes(new byte[1]{ (byte) 52 });
      }
      catch (Exception ex)
      {
      }
      c.player.spectator = false;
    }
    switch (c.player.location)
    {
      case Location.Authenticating:
        c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.AuthHandler);
        break;
      case Location.MainMenu:
        c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.MainMenuHandler);
        Server._MainMenu.Remove(c);
        break;
      case Location.Ingame_Disconnected:
      case Location.Ingame:
        GameFacts gameFacts = (GameFacts) null;
        if (Server._games.TryGetValue(c.player.gameNumber, out gameFacts) && gameFacts.game != null)
          c.DataReceived -= new EventHandler<DataReceivedEventArgs>(gameFacts.game.GameHandler);
        if (loc != Location.LobbyCreateGame)
        {
          c.player.gameNumber = -1;
          break;
        }
        break;
      case Location.Lobby:
        c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.LobbyHandler);
        Server.RemoveFromLobby(c);
        if (c.randomRatedFactsID != 0)
          Server.OnStoppedSearching(c);
        Server._lobby.Remove(c);
        Server._ratedQueue.Remove(c);
        break;
      case Location.LobbyUnrated:
        c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.UnratedHandler);
        GameFacts gg = (GameFacts) null;
        if (Server._games.TryGetValue(c.player.gameNumber, out gg) && gg.status == (byte) 0)
        {
          gg.KillStart();
          bool flag = gg.connections.Count > 0 && gg.connections[0] == c;
          if (gg.connections.Count > 0 && gg.connections.Remove(c))
            Server.RemoveFromUnrated(c);
          if (gg.connections.Count == 0)
          {
            if (gg.game != null)
              gg.game.CleanUp();
            Server._games.Remove(gg.id);
          }
          else if (flag && !string.IsNullOrEmpty(gg.settings.description))
          {
            gg.settings.description = "";
            Server.SendGameModeUpdate(gg);
          }
        }
        c.player.gameNumber = -1;
        Server._unrated.Remove(c);
        break;
      case Location.LobbyRated:
        c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.RatedHandler);
        Server._ratedQueue.Remove(c);
        Server.OnStoppedSearching(c);
        break;
    }
    Location location = c.player.location;
    c.player.location = loc;
    c.player.ready = false;
    switch (loc)
    {
      case Location.MainMenu:
        c.DataReceived += new EventHandler<DataReceivedEventArgs>(Server.MainMenuHandler);
        Server._MainMenu.Add(c, c);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
          {
            w.Write((byte) 20);
            if (location == Location.Authenticating)
            {
              c.player.account.Serialize(w);
              c.player.account.cosmetics.Serialize(w);
              w.Write(c.player.account.bonusExperience);
              w.Write((byte) c.player.account.lobbyChat);
              w.Write((byte) c.player.account.gameChat);
              w.Write((byte) c.player.account.teamChat);
              w.Write((byte) c.player.account.miniGameChat);
              w.Write((byte) c.player.account.friendChat);
              w.Write((byte) c.player.account.inviteChat);
              w.Write((byte) c.player.account.spectatorChat);
              w.Write((byte) c.player.account.clanChat);
              w.Write(c.player.account.state);
              w.Write(c.player.account.wands);
              w.Write(c.player.account.totalWands);
              w.Write(c.player.account.tournamentCoins);
              Account.SerializeList(c.player.account.friends, w);
              Account.SerializeList(c.player.account.ignored, w);
              if (!string.IsNullOrEmpty(c.player.account.clan))
              {
                Clan clan = (Clan) null;
                if (Server._clans.TryGetValue(c.player.account.clan, out clan))
                {
                  w.Write((byte) 1);
                  clan.Serialize(w);
                }
                else
                  w.Write((byte) 0);
              }
              else
                w.Write((byte) 0);
              c.player.account.tutorials.Serialize(w);
            }
          }
          c.SendBytes(memoryStream.ToArray());
          break;
        }
      case Location.Lobby:
        c.DataReceived += new EventHandler<DataReceivedEventArgs>(Server.LobbyHandler);
        Server.AddToLobby(c);
        break;
      case Location.LobbyUnrated:
        c.DataReceived += new EventHandler<DataReceivedEventArgs>(Server.UnratedHandler);
        Server.AddToUnrated(c);
        break;
      case Location.LobbyRated:
        c.DataReceived += new EventHandler<DataReceivedEventArgs>(Server.RatedHandler);
        c.SendBytes(new byte[1]{ (byte) 23 });
        break;
      case Location.LobbyCreateGame:
        c.player.location = Location.LobbyUnrated;
        c.DataReceived += new EventHandler<DataReceivedEventArgs>(Server.UnratedHandler);
        Server._unrated.Add(c, c);
        break;
    }
    if (location.Online() == loc.Online())
      return;
    Server.SendPlayerLocation(c);
  }

  public static void Test(Hazel.Connection c)
  {
  }

  private static void MainMenuHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes.Length == 1 && args.Bytes[0] == (byte) 34)
      return;
    Hazel.Connection c = (Hazel.Connection) sender;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      if (c.player.location == Location.MainMenu)
      {
        using (MemoryStream memoryStream = new MemoryStream(args.Bytes))
        {
          using (myBinaryReader reader = new myBinaryReader((Stream) memoryStream))
          {
            try
            {
              switch (reader.ReadByte())
              {
                case 93:
                  Server.HandleRejoinGame(c, reader);
                  break;
                case 193:
                  Server.MovePlayer(c, Location.Lobby);
                  Server.SyncLobby(c, sendmsg: true);
                  break;
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex.ToString());
            }
          }
        }
      }
      args.Recycle();
    }));
  }

  public static void HandleClanMsg(Hazel.Connection connection, myBinaryReader reader)
  {
  }

  public static void ValidatePrivateChatMessage(
    string n,
    Hazel.Connection c,
    myBinaryReader reader,
    byte[] bytes)
  {
    string key = reader.ReadString();
    string str = reader.ReadString();
    if (str.Length > 600)
    {
      Server.ReturnServerMsg(c, key + "Message was too long.");
    }
    else
    {
      Hazel.Connection connection;
      if (string.Equals(n, c.player.account.name, StringComparison.OrdinalIgnoreCase) && Server._names.TryGetValue(key, out connection) && connection.State == ConnectionState.Connected)
      {
        if (c.player.account.accountType.IsMuted() && !Server.allowUnverifiedUsersToTalk && !connection.player.account.accountType.IsModPlus() || c.player.account.accountType.has(AccountType.Perm_Muted))
        {
          if (c.player.account.discord == 0UL)
            Server.ReturnServerMsg(c, "Verify your discord ID to enable talking. Or use quickchat <sprite name=\"Emoji2_1352\">.");
          else
            Server.ReturnServerMsg(c, "Looks like you're muted, try to behave yourself next time.");
        }
        else
        {
          connection.SendBytes(bytes);
          MyLog.MainLog("[Private] [" + n + "] [" + key + "] " + str);
          c.OnChat("[Private to " + key + "] " + str);
        }
      }
      else
        Server.ReturnServerMsg(c, key + " appears to be offline.");
    }
  }

  public static void ValidateMiniGameChatMessage(
    Hazel.Connection c,
    myBinaryReader reader,
    byte[] bytes,
    IMiniGame miniGame)
  {
    string a = reader.ReadString();
    string str = reader.ReadString();
    if (str.Length > 600 || !string.Equals(a, c.player.account.name))
      return;
    MyLog.MainLog("[Minigame Chat " + (object) miniGame.id + "] [" + a + "] " + str);
    c.OnChat("[Minigame" + (object) miniGame.id + "] " + str);
    miniGame.SendAll(bytes);
  }

  public static void ValidateQuickChat(
    Hazel.Connection c,
    myBinaryReader reader,
    byte[] bytes,
    bool lobby)
  {
  }

  private static void ValidateChatMessage(
    Hazel.Connection c,
    myBinaryReader reader,
    byte[] bytes,
    bool lobby)
  {
    if (Server.chatMode > 0 & lobby && !c.player.account.accountType.IsModPlus())
    {
      if (Server.chatMode > 1)
        Server.ReturnServerMsg(c, "Lobby chat is currently disabled.");
      else
        Server.ReturnServerMsg(c, "Lobby chat is currently limited to Quickchat.");
    }
    else
    {
      string a = reader.ReadString();
      string str = reader.ReadString();
      if (str.Length > 600 || !string.Equals(a, c.player.account.name))
        return;
      if (lobby)
      {
        MyLog.MainLog("[Chat] [" + a + "] " + str);
        Server.SendLobbyMessage(bytes);
        c.OnChat("[Chat] " + str);
      }
      else
      {
        GameFacts g;
        if (!Server._games.TryGetValue(c.player.gameNumber, out g))
          return;
        MyLog.MainLog("[Game #" + (object) c.player.gameNumber + "] [" + a + "] " + str);
        Server.SendUnratedMessage(g, bytes);
        c.OnChat("[Game #" + (object) c.player.gameNumber + "] " + str);
      }
    }
  }

  public static void ValidateClanChat(Hazel.Connection c, myBinaryReader reader, byte[] bytes)
  {
    string a = reader.ReadString();
    string str = reader.ReadString();
    Clan clan;
    if (str.Length > 600 || string.IsNullOrEmpty(c.player.account.clan) || !string.Equals(a, c.player.account.name) || !Server._clans.TryGetValue(c.player.account.clan, out clan))
      return;
    MyLog.MainLog("[" + c.player.account.clan + "] [Clan Chat] [" + a + "] " + str);
    clan.ClanChat(bytes);
    c.OnChat("[Clan] " + str);
  }

  internal static void ValidateShare(
    Hazel.Connection c,
    myBinaryReader reader,
    byte[] args,
    bool lobby,
    bool requirePrivate = false)
  {
    if (c.player.lastShareMsg + 1500L > c.stopwatch.ElapsedMilliseconds)
    {
      Server.ReturnServerMsg(c, "Rate limited :(");
    }
    else
    {
      c.player.lastShareMsg = c.stopwatch.ElapsedMilliseconds;
      if (args.Length > 209715)
        Server.ReturnServerMsg(c, "File size to big.");
      else if (c.player.account.accountType.IsPermMuted())
      {
        Server.ReturnServerMsg(c, "Looks like you're muted, try to behave yourself next time.");
      }
      else
      {
        string a = reader.ReadString();
        string str = reader.ReadString();
        if (!string.Equals(a, c.player.account.name))
          return;
        string b = reader.ReadString();
        if (b.Length > 30 || b.Length == 0)
        {
          Server.ReturnServerMsg(c, "Invalid file name: " + b);
        }
        else
        {
          ContentType contentType = (ContentType) reader.ReadByte();
          if (contentType == ContentType.ClanInvite)
          {
            requirePrivate = true;
            Content.GetContent(reader, contentType);
          }
          if (c.player.account.accountType.IsMuted() && !string.Equals(contentType.ToString(), b))
            Server.ReturnServerMsg(c, "Maybe if you weren't muted.");
          else if (!string.IsNullOrEmpty(str))
          {
            Hazel.Connection connection;
            if (Server._names.TryGetValue(str, out connection))
            {
              if (connection != null && connection.State == ConnectionState.Connected)
              {
                connection.SendBytes(args);
                Server.ReturnServerMsg(c, (contentType == ContentType.ClanInvite ? "Invited " : "Shared with ") + str);
                MyLog.MainLog("[Share Private " + contentType.ToString() + "] [" + a + "] with [" + str + "] " + b);
                c.OnChat("[Share] " + b);
              }
              else
                Server.ReturnServerMsg(c, "Failed to Share - Most likely offline.");
            }
            else if (string.Equals(str, "[Clan]"))
            {
              Clan clan;
              if (!Server._clans.TryGetValue(c.player.account.clan, out clan))
                return;
              clan.Send(c, args);
              MyLog.MainLog("[Share " + contentType.ToString() + " Clan] [" + a + "] " + b);
              Server.ReturnServerMsg(c, "Shared with the clan.");
              c.OnChat("[Share] " + b);
            }
            else
              Server.ReturnServerMsg(c, "Player not online.");
          }
          else
          {
            if (requirePrivate)
              return;
            if (!lobby)
            {
              GameFacts g;
              if (!Server._games.TryGetValue(c.player.gameNumber, out g))
                return;
              Server.SendUnratedMessage(g, args);
              MyLog.MainLog("[Share " + contentType.ToString() + " Game# " + (object) g.id + "] [" + a + "] " + b);
            }
            else
            {
              Server.SendLobbyMessage(args);
              MyLog.MainLog("[Share " + contentType.ToString() + "] [" + a + "] " + b);
            }
          }
        }
      }
    }
  }

  public static void ChangeChatSettings(Account acc, myBinaryReader reader)
  {
    byte num = reader.ReadByte();
    ChatSetting chatSetting = (ChatSetting) reader.ReadByte();
    switch (num)
    {
      case 0:
        acc.lobbyChat = chatSetting;
        break;
      case 1:
        acc.gameChat = chatSetting;
        break;
      case 2:
        acc.friendChat = chatSetting;
        break;
      case 3:
        acc.inviteChat = chatSetting;
        break;
      case 4:
        acc.spectatorChat = chatSetting;
        break;
      case 5:
        acc.clanChat = chatSetting;
        break;
      case 6:
        acc.teamChat = chatSetting;
        break;
      case 7:
        acc.miniGameChat = chatSetting;
        break;
    }
    acc.ToFile();
  }

  public static void ChangeFriendsList(Account c, myBinaryReader reader, byte[] bytes)
  {
    int num = reader.ReadBoolean() ? 1 : 0;
    bool flag = reader.ReadBoolean();
    string str = reader.ReadString();
    if (num != 0)
    {
      if (flag)
      {
        if (c.friends.Count >= 200 || !c.friends.Add(str))
          return;
        c.ToFile();
      }
      else
      {
        if (!c.friends.Remove(str))
          return;
        c.ToFile();
      }
    }
    else if (flag)
    {
      if (c.ignored.Count >= 100 || !c.ignored.Add(str))
        return;
      c.ToFile();
    }
    else
    {
      if (!c.ignored.Remove(str))
        return;
      c.ToFile();
    }
  }

  public static int NextMiniGameID()
  {
    ++Server.Next_miniGame_ID;
    while (Server._miniGames.ContainsKey(Server.Next_miniGame_ID))
      ++Server.Next_miniGame_ID;
    return Server.Next_miniGame_ID;
  }

  public static int NextGameID()
  {
    ++Server.Next_Game_ID;
    while (Server._games.ContainsKey(Server.Next_Game_ID))
      ++Server.Next_Game_ID;
    return Server.Next_Game_ID;
  }

  public static void SendGameCreated(GameFacts gf)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
      {
        writer.Write((byte) 29);
        gf.ManualSerialize(writer);
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
      {
        if (keyValuePair.Key.State == ConnectionState.Connected)
          keyValuePair.Key.SendBytes(array);
      }
    }
  }

  public static void HandleRejoinGame(Hazel.Connection c, myBinaryReader reader)
  {
    int key = reader.ReadInt32();
    GameFacts gg;
    if (!Server._games.TryGetValue(key, out gg) || gg.status != (byte) 2 && gg.status != (byte) 1 || gg.game == null)
      return;
    for (int index = 0; index < gg.connections.Count; ++index)
    {
      if (string.Equals(c.name, gg.connections[index].name))
      {
        ZPerson p = gg.game.players.Find((Predicate<ZPerson>) (z => string.Equals(z.name, c.name)));
        if (p == null)
          break;
        Server.MovePlayer(c, Location.Disconnecting);
        p.didLeave = false;
        c.player.player = p;
        c.player.gameNumber = gg.id;
        p.connection = c;
        c.player.player.connection = c;
        gg.connections[index] = c;
        gg.game._oldData.Remove(c);
        Server.SendStartGame(c, gg);
        gg.game?.SendRejoined(p);
        break;
      }
    }
  }

  private static void LobbyHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes.Length == 1 && args.Bytes[0] == (byte) 34)
      return;
    Hazel.Connection c = (Hazel.Connection) sender;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      if (c.player.location == Location.Lobby)
      {
        using (MemoryStream memoryStream1 = new MemoryStream(args.Bytes))
        {
          using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream1))
          {
            try
            {
              byte num1 = myBinaryReader.ReadByte();
              Server.errorMsg = num1;
              switch (num1)
              {
                case 26:
                  int key1 = myBinaryReader.ReadInt32();
                  GameFacts gameFacts = (GameFacts) null;
                  if (Server._games.TryGetValue(key1, out gameFacts))
                  {
                    if (gameFacts.GetRatedMode() && (c.player.account.discord == 0UL || c.player.account[0].rating < (short) 1000))
                    {
                      if (c.player.account.discord == 0UL)
                      {
                        Server.ReturnServerMsg(c, "Verify your discord ID to play rated games.");
                        break;
                      }
                      Server.ReturnServerMsg(c, "Looks like you're banned from rateds :(");
                      break;
                    }
                    if (gameFacts.status == (byte) 0 && gameFacts.connections.Count < (int) gameFacts.customPlayerCount && c.player.location == Location.Lobby && (!gameFacts.GetRatedMode() || c.player.account.discord != 0UL) && (!gameFacts.connections[0].player.account.ignored.Contains(c.player.account.name) || gameFacts.invitedPlayers.Contains(c.player.account.name)) && (gameFacts.GetInviteMode() == InviteEnum.Open || gameFacts.invitedPlayers.Contains(c.player.account.name) || gameFacts.GetInviteMode() == InviteEnum.Clan && gameFacts.connections.Count > 0 && string.Equals(gameFacts.connections[0].player.account.clan, c.player.account.clan) || gameFacts.GetInviteMode() == InviteEnum.Friends && (gameFacts.connections.Count == 0 || gameFacts.connections[0].player.account.friends.Contains(c.player.account.name)) || gameFacts.GetInviteMode() == InviteEnum.Similar_Rating && gameFacts.connections.Count > 0 && Mathf.Abs((int) gameFacts.connections[0].player.account.similarRating - (int) c.player.account.similarRating) <= 500))
                    {
                      c.player.gameNumber = key1;
                      gameFacts.connections.Add(c);
                      Server.MovePlayer(c, Location.LobbyUnrated);
                      if (gameFacts.GetInviteMode() != InviteEnum.BookClub)
                        gameFacts.invitedPlayers.Remove(c.player.account.name);
                      if (!gameFacts.GetRatedMode())
                      {
                        if (!gameFacts.GetMultiTeamMode())
                          break;
                      }
                      gameFacts.KillStart();
                      for (int index = 0; index < gameFacts.connections.Count; ++index)
                        gameFacts.connections[index].player.ready = false;
                      break;
                    }
                    if (gameFacts.connections.Count > 0)
                    {
                      using (MemoryStream memoryStream2 = new MemoryStream())
                      {
                        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream2))
                        {
                          myBinaryWriter.Write((byte) 59);
                          myBinaryWriter.Write(c.player.account.name);
                        }
                        gameFacts.connections[0].SendBytes(memoryStream2.ToArray());
                        Server.ReturnServerMsg(c, "Asking to join " + gameFacts.connections[0].name + "'s game...");
                        break;
                      }
                    }
                    else
                      break;
                  }
                  else
                    break;
                case 28:
                  Server.MovePlayer(c, Location.MainMenu);
                  break;
                case 29:
                case 39:
                  GameFacts gf1 = new GameFacts();
                  gf1.settings.Deserialize(myBinaryReader);
                  gf1.settings.Clamp();
                  if (c.player.account.accountType.IsMuted() && !string.IsNullOrEmpty(gf1.settings.description))
                    gf1.settings.description = "";
                  if (!string.IsNullOrEmpty(gf1.settings.description))
                    MyLog.MainLog("[Game Description] [" + c.name + "] " + gf1.settings.description);
                  gf1.id = Server.NextGameID();
                  Server._games.Add(gf1.id, gf1);
                  gf1.connections.Add(c);
                  c.player.gameNumber = gf1.id;
                  Server.MovePlayer(c, Location.LobbyCreateGame);
                  if (num1 == (byte) 39 && c.player.account[0].rating >= (short) 1000 && c.player.account.discord != 0UL)
                  {
                    gf1.SetRatedMode(true);
                    gf1.SetTeamMode(TeamEnum.Yes);
                    gf1.SetPlayersPerTeam(PlayersPerTeam.Half);
                    gf1.SetSpectatorMode(true);
                    gf1.ReSetStyle(GameStyle.Arcane_Monster);
                    gf1.ReSetStyle(GameStyle.Sandbox);
                  }
                  else
                    gf1.SetRatedMode(false);
                  Server.SendGameCreated(gf1);
                  break;
                case 36:
                  int key2 = myBinaryReader.ReadInt32();
                  GameFacts gf2 = (GameFacts) null;
                  if (Server._games.TryGetValue(key2, out gf2))
                  {
                    if (gf2.status != (byte) 2)
                    {
                      if (gf2.status != (byte) 1)
                        break;
                    }
                    if (!gf2.GetSpectatorMode())
                    {
                      if (!c.player.account.accountType.isDev())
                        break;
                    }
                    if (gf2.game != null)
                    {
                      if (!c.player.spectator)
                      {
                        byte[] numArray = new byte[25];
                        Server.rngCsp.GetBytes(numArray);
                        c.player.spectator = true;
                        Server.validSpectator[c.player.account.name] = new ValidSpectator()
                        {
                          gameID = gf2.id,
                          name = c.player.account.name,
                          cryp = numArray,
                          isp = c.EndPoint.ToString().Split(':')[0]
                        };
                        using (MemoryStream memoryStream3 = new MemoryStream())
                        {
                          using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream3))
                          {
                            writer.Write((byte) 36);
                            writer.Write(gf2.id);
                            writer.Write(numArray);
                            gf2.ManualSerialize(writer, true);
                          }
                          c.SendBytes(memoryStream3.ToArray());
                          break;
                        }
                      }
                      else
                        break;
                    }
                    else
                      break;
                  }
                  else
                    break;
                case 44:
                  if (Server._ratedQueue.ContainsKey(c))
                  {
                    Server.ReturnServerMsg(c, "Cannot change spells while finding opponents");
                    return;
                  }
                  SettingsPlayer b1 = new SettingsPlayer();
                  b1.Deserialize(myBinaryReader);
                  c.player.settingsPlayer.CopySpells(b1);
                  c.player.settingsPlayer.VerifySpells();
                  c.player.settingsPlayer.ReseralizeSpells(c);
                  break;
                case 45:
                  SettingsPlayer b2 = new SettingsPlayer();
                  b2.Deserialize(myBinaryReader);
                  c.player.settingsPlayer.CopyOutfit(b2);
                  c.player.settingsPlayer.VerifyOutfit(c.player.account.cosmetics, c.player.account);
                  c.SendBytes(args.Bytes);
                  break;
                case 50:
                  myBinaryReader.ReadString();
                  Server.ChangeFriendsList(c.player.account, myBinaryReader, args.Bytes);
                  break;
                case 52:
                  c.player.spectator = false;
                  break;
                case 55:
                  Server.ReceiveReport(c, c.player.account.name, myBinaryReader);
                  break;
                case 57:
                  myBinaryReader.ReadString();
                  Server.ChangeChatSettings(c.player.account, myBinaryReader);
                  break;
                case 60:
                  if (Server._ratedQueue.ContainsKey(c))
                  {
                    Server.ReturnServerMsg(c, "Cannot change elementals while finding opponents");
                    return;
                  }
                  c.player.settingsPlayer.fullBook = myBinaryReader.ReadByte();
                  c.SendBytes(args.Bytes);
                  break;
                case 75:
                  if (c.player.account.accountType.IsMuted())
                    return;
                  byte num2 = myBinaryReader.ReadByte();
                  switch (num2)
                  {
                    case 253:
                    case 254:
                      c.player.account.displayClanPrefix = num2 == (byte) 253 ? (byte) 1 : (byte) 0;
                      break;
                    default:
                      c.player.account.displayedIcon = (int) num2;
                      break;
                  }
                  Server.UpdateAccountInfo(c.player.account);
                  break;
                case 77:
                  Server.ValidateShare(c, myBinaryReader, args.Bytes, true);
                  break;
                case 84:
                  Server.HandleClanMsg(c, myBinaryReader);
                  break;
                case 93:
                  Server.HandleRejoinGame(c, myBinaryReader);
                  break;
                case 150:
                  Server.ValidateClanChat(c, myBinaryReader, args.Bytes);
                  break;
                case 151:
                  Server.ValidatePrivateChatMessage(myBinaryReader.ReadString(), c, myBinaryReader, args.Bytes);
                  break;
                case 153:
                  Server.ValidateChatMessage(c, myBinaryReader, args.Bytes, true);
                  break;
                case 155:
                  Server.ValidateQuickChat(c, myBinaryReader, args.Bytes, true);
                  break;
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) (ex.ToString() + " MSG: " + (object) Server.errorMsg));
            }
          }
        }
      }
      args.Recycle();
    }));
  }

  private static void UnratedHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes.Length == 1 && args.Bytes[0] == (byte) 34)
      return;
    Hazel.Connection c = (Hazel.Connection) sender;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      if (c.player.location == Location.LobbyUnrated)
      {
        using (MemoryStream memoryStream1 = new MemoryStream(args.Bytes))
        {
          using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream1))
          {
            try
            {
              byte tag = myBinaryReader.ReadByte();
              switch (tag)
              {
                case 12:
                  GameFacts gg1 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gg1))
                  {
                    if (gg1.GetInviteMode() != InviteEnum.BookClub)
                    {
                      if (gg1.lastChange + 5.0 >= (double) Time.realtimeSinceStartup)
                      {
                        if (gg1.lastChange > 0.0)
                        {
                          if ((double) Time.realtimeSinceStartup >= 0.0)
                            break;
                        }
                        else
                          break;
                      }
                      if (gg1.status == (byte) 0)
                      {
                        if (gg1.connections.Count > 0)
                        {
                          if (gg1.connections[0] == c)
                          {
                            if (gg1.GetTeamMode() && (!gg1.GetMultiTeamMode() || gg1.GetRatedMode() && gg1.connections.Count != 2))
                            {
                              if (gg1.connections.Count > gg1.GetNumberPlayersPerTeam())
                              {
                                if (gg1.connections.Count % gg1.GetNumberPlayersPerTeam() != 0)
                                  break;
                              }
                              else
                                break;
                            }
                            Server.StartGame(gg1);
                            break;
                          }
                          break;
                        }
                        break;
                      }
                      break;
                    }
                    break;
                  }
                  break;
                case 14:
                  GameFacts gg2 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gg2))
                  {
                    if (gg2.connections.Count == 0 || gg2.connections[0] != c || gg2.GetInviteMode() == InviteEnum.BookClub)
                      return;
                    GameFacts.Message message = (GameFacts.Message) myBinaryReader.ReadByte();
                    int num1 = myBinaryReader.ReadInt32();
                    if (num1 == -1 && message != GameFacts.Message.Remove_Custom_Armageddon || gg2.GetTournamentMode() && message != GameFacts.Message.Tournament && !c.player.account.accountType.IsModPlusTOParticipate())
                      return;
                    switch (message)
                    {
                      case GameFacts.Message.Invite_Mode:
                        if (!System.Enum.IsDefined(typeof (InviteEnum), (object) num1) || num1 == 32768)
                          return;
                        gg2.SetInviteMode((InviteEnum) num1);
                        break;
                      case (GameFacts.Message) 2:
                        return;
                      case GameFacts.Message.Spectators:
                        if (!System.Enum.IsDefined(typeof (SpectatorsEnum), (object) num1) || gg2.GetRatedMode())
                          return;
                        gg2.SetSpectatorMode(!gg2.GetSpectatorMode());
                        break;
                      case (GameFacts.Message) 4:
                        return;
                      case GameFacts.Message.Time:
                        if (!System.Enum.IsDefined(typeof (TimeEnum), (object) num1))
                          return;
                        gg2.SetTimeMode((TimeEnum) num1);
                        gg2.customTime = (ushort) gg2.GetTimeInSeconds();
                        break;
                      case GameFacts.Message.Map:
                        if (!System.Enum.IsDefined(typeof (MapEnum), (object) num1))
                          return;
                        if (num1 == 16777216)
                        {
                          gg2.ActivateAllMaps();
                          break;
                        }
                        gg2.SetMapMode((MapEnum) num1);
                        break;
                      case GameFacts.Message.Team:
                        if (!System.Enum.IsDefined(typeof (TeamEnum), (object) num1) || gg2.GetRatedMode())
                          return;
                        if (num1 == 524288)
                          num1 |= 16777216;
                        gg2.SetTeamMode((TeamEnum) num1);
                        break;
                      case GameFacts.Message.Players:
                        if (!System.Enum.IsDefined(typeof (PlayerEnum), (object) num1))
                          return;
                        gg2.SetPlayerMode((PlayerEnum) num1);
                        gg2.customPlayerCount = (byte) gg2.GetMaxPlayers();
                        break;
                      case GameFacts.Message.PerTeam:
                        if (!System.Enum.IsDefined(typeof (PlayersPerTeam), (object) num1) || gg2.GetRatedMode())
                          return;
                        gg2.SetPlayersPerTeam((PlayersPerTeam) num1);
                        break;
                      case (GameFacts.Message) 10:
                        return;
                      case GameFacts.Message.GameStyle:
                        if (!System.Enum.IsDefined(typeof (GameStyle), (object) num1))
                          return;
                        gg2.ToggleStyle((GameStyle) num1);
                        if (gg2.GetRatedMode())
                        {
                          gg2.ReSetStyle(GameStyle.Arcane_Monster);
                          gg2.ReSetStyle(GameStyle.Sandbox);
                          break;
                        }
                        break;
                      case GameFacts.Message.AddMap:
                        if (!System.Enum.IsDefined(typeof (MapEnum), (object) num1))
                          return;
                        if (num1 == 16777216)
                        {
                          gg2.ActivateAllMaps();
                          break;
                        }
                        MapEnum mapMode = gg2.GetMapMode();
                        gg2.gameModes4 ^= num1;
                        int num2 = (int) gg2.GetMapMode();
                        if (num2 == 0)
                        {
                          gg2.SetMapMode(mapMode);
                          num2 = (int) mapMode;
                        }
                        if ((mapMode & MapEnum.Random) == ~MapEnum.Dont_Mind)
                        {
                          if (!System.Enum.IsDefined(typeof (MapEnum), (object) num2))
                          {
                            gg2.gameModes4 |= 16777216;
                            break;
                          }
                          break;
                        }
                        if (System.Enum.IsDefined(typeof (MapEnum), (object) (num2 ^ 16777216)))
                        {
                          gg2.gameModes4 ^= 16777216;
                          break;
                        }
                        break;
                      case GameFacts.Message.Armageddon:
                        if (num1 != 0 && !System.Enum.IsDefined(typeof (MapEnum), (object) num1))
                          return;
                        if (num1 == 16777216)
                        {
                          gg2.ActivateAlArmageddons();
                          break;
                        }
                        gg2.SetArmageddon((MapEnum) num1);
                        gg2.settings.customArmageddon = (List<SpellEnum>) null;
                        break;
                      case GameFacts.Message.AddArmageddon:
                        if (num1 != 0 && !System.Enum.IsDefined(typeof (MapEnum), (object) num1))
                          return;
                        if (num1 == 16777216)
                        {
                          gg2.ActivateAlArmageddons();
                          break;
                        }
                        MapEnum armageddon = gg2.GetArmageddon();
                        gg2.gameModes3 ^= num1;
                        if ((armageddon & MapEnum.Random) == ~MapEnum.Dont_Mind)
                        {
                          if (armageddon != ~MapEnum.Dont_Mind && !System.Enum.IsDefined(typeof (MapEnum), (object) (MapEnum) gg2.gameModes3))
                            gg2.gameModes3 |= 16777216;
                        }
                        else if (System.Enum.IsDefined(typeof (MapEnum), (object) (MapEnum) (gg2.gameModes3 ^ 16777216)))
                          gg2.gameModes3 ^= 16777216;
                        gg2.settings.customArmageddon = (List<SpellEnum>) null;
                        break;
                      case GameFacts.Message.ElementalLevel:
                        if (num1 < 0)
                        {
                          gg2.ReSetStyle(GameStyle.Elementals);
                          break;
                        }
                        int num3 = Mathf.Clamp(num1, 0, 5);
                        if ((int) gg2.elementalLevel == num3 && gg2.GetStyle().HasStyle(GameStyle.Elementals))
                          return;
                        gg2.elementalLevel = (byte) num3;
                        gg2.SetStyle(GameStyle.Elementals);
                        break;
                      case GameFacts.Message.StartHealth:
                        int num4 = Mathf.Clamp(num1, 1, 500);
                        if ((int) gg2.startHealth == num4)
                          return;
                        gg2.startHealth = (ushort) num4;
                        break;
                      case GameFacts.Message.ArmageddonTurn:
                        int num5 = Mathf.Clamp(num1, 0, 60);
                        if ((int) gg2.armageddonTurn == num5)
                          return;
                        gg2.armageddonTurn = (byte) num5;
                        break;
                      case (GameFacts.Message) 18:
                        return;
                      case GameFacts.Message.MaxTime:
                        int num6 = Mathf.Clamp(num1, -3600, 3600);
                        if ((int) gg2.countdownTime == num6)
                          return;
                        gg2.countdownTime = (short) num6;
                        break;
                      case GameFacts.Message.CustomTime:
                        int num7 = Mathf.Clamp(num1, 5, 120);
                        if ((int) gg2.customTime == num7)
                          return;
                        gg2.customTime = (ushort) num7;
                        gg2.SetTimeMode(gg2.GetClosestTimeToCustom());
                        break;
                      case GameFacts.Message.Delay:
                        int num8 = Mathf.Clamp(num1, 0, 30);
                        if ((int) gg2.countdownDelay == num8)
                          return;
                        gg2.countdownDelay = (byte) num8;
                        break;
                      case GameFacts.Message.CustomPlayerCount:
                        int num9 = Mathf.Clamp(num1, 1, 24);
                        if ((int) gg2.customPlayerCount == num9)
                          return;
                        gg2.customPlayerCount = (byte) num9;
                        break;
                      case GameFacts.Message.Tournament:
                        if ((gg2.GetRatedMode() || !c.player.account.accountType.IsModPlusTOParticipate()) && !gg2.GetTournamentMode())
                          return;
                        gg2.SetTournamentMode(!gg2.GetTournamentMode());
                        gg2.tournamentWasSetByTO = gg2.GetTournamentMode() && c.player.account.accountType.IsModPlusTO();
                        break;
                      case GameFacts.Message.Map_Size:
                        int num10 = Mathf.Clamp((int) CombineBytes.Low(num1), 50, 150);
                        int num11 = Mathf.Clamp((int) CombineBytes.High(num1), 50, 150);
                        gg2.settings.mapWidth = (byte) num10;
                        gg2.settings.mapHeight = (byte) num11;
                        break;
                      case GameFacts.Message.Map_Seed:
                        gg2.settings.mapSeed = num1;
                        gg2.settings.fixedMapSeed = true;
                        break;
                      case GameFacts.Message.Disable_Map_Seed:
                        gg2.settings.mapSeed = 0;
                        gg2.settings.fixedMapSeed = false;
                        break;
                      case GameFacts.Message.GameType:
                        return;
                      case GameFacts.Message.Custom_Armageddon:
                        if (!System.Enum.IsDefined(typeof (SpellEnum), (object) num1))
                          return;
                        Spell spell = Inert.GetSpell((SpellEnum) num1);
                        if ((UnityEngine.Object) spell == (UnityEngine.Object) null || spell.level > 3)
                          return;
                        if (gg2.settings.customArmageddon == null)
                          gg2.settings.customArmageddon = new List<SpellEnum>();
                        if (gg2.settings.customArmageddon.Count >= 5)
                          return;
                        gg2.settings.customArmageddon.Add((SpellEnum) num1);
                        break;
                      case GameFacts.Message.Remove_Custom_Armageddon:
                        if (gg2.settings.customArmageddon == null)
                          return;
                        if (num1 == -1)
                        {
                          gg2.settings.customArmageddon = (List<SpellEnum>) null;
                          break;
                        }
                        if (!gg2.settings.customArmageddon.Remove((SpellEnum) num1))
                          return;
                        if (gg2.settings.customArmageddon.Count == 0)
                        {
                          gg2.settings.customArmageddon = (List<SpellEnum>) null;
                          break;
                        }
                        break;
                      default:
                        return;
                    }
                    if (gg2.GetRatedMode() || gg2.GetMultiTeamMode())
                    {
                      foreach (Hazel.Connection connection in gg2.connections)
                        connection.player.ready = false;
                      gg2.KillStart();
                    }
                    Server.SendGameModeUpdate(gg2);
                    break;
                  }
                  break;
                case 17:
                  GameFacts gameFacts1 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gameFacts1))
                  {
                    c.player.ready = !c.player.ready;
                    gameFacts1.SendReadyState();
                    break;
                  }
                  break;
                case 41:
                  GameFacts gameFacts2 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gameFacts2))
                  {
                    if (gameFacts2.status == (byte) 0)
                    {
                      if (gameFacts2.connections.Count > 0)
                      {
                        if (gameFacts2.connections[0] == c)
                        {
                          string key = myBinaryReader.ReadString();
                          bool flag1 = myBinaryReader.ReadBoolean();
                          bool flag2 = false;
                          if (flag1)
                          {
                            Hazel.Connection connection = (Hazel.Connection) null;
                            if (Server._names.TryGetValue(key, out connection) && connection != null && connection.State == ConnectionState.Connected && connection.player.location == Location.Lobby && gameFacts2.invitedPlayers.Add(key))
                              flag2 = true;
                          }
                          else if (gameFacts2.invitedPlayers.Remove(key))
                            flag2 = true;
                          if (flag2)
                          {
                            using (MemoryStream memoryStream2 = new MemoryStream())
                            {
                              using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream2))
                              {
                                myBinaryWriter.Write((byte) 41);
                                myBinaryWriter.Write(gameFacts2.id);
                                myBinaryWriter.Write(key);
                                myBinaryWriter.Write(flag1);
                              }
                              Server.SendLobbyUnrated(memoryStream2.ToArray());
                              break;
                            }
                          }
                          else
                            break;
                        }
                        else
                          break;
                      }
                      else
                        break;
                    }
                    else
                      break;
                  }
                  else
                    break;
                case 43:
                  GameFacts gameFacts3 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gameFacts3))
                  {
                    if (gameFacts3.status == (byte) 0)
                    {
                      if (gameFacts3.connections.Count > 0)
                      {
                        if (gameFacts3.connections[0] == c)
                        {
                          string key = myBinaryReader.ReadString();
                          Hazel.Connection c1 = (Hazel.Connection) null;
                          if (Server._names.TryGetValue(key, out c1))
                          {
                            if (c1.player.gameNumber == gameFacts3.id)
                            {
                              if (c1.player.location == Location.LobbyUnrated)
                              {
                                Server.MovePlayer(c1, Location.Lobby);
                                break;
                              }
                              break;
                            }
                            break;
                          }
                          break;
                        }
                        break;
                      }
                      break;
                    }
                    break;
                  }
                  break;
                case 44:
                  SettingsPlayer b1 = new SettingsPlayer();
                  b1.Deserialize(myBinaryReader);
                  c.player.settingsPlayer.CopySpells(b1);
                  c.player.settingsPlayer.VerifySpells();
                  c.player.settingsPlayer.ReseralizeSpells(c);
                  break;
                case 45:
                  SettingsPlayer b2 = new SettingsPlayer();
                  b2.Deserialize(myBinaryReader);
                  c.player.settingsPlayer.CopyOutfit(b2);
                  c.player.settingsPlayer.VerifyOutfit(c.player.account.cosmetics, c.player.account);
                  c.SendBytes(args.Bytes);
                  break;
                case 50:
                  myBinaryReader.ReadString();
                  Server.ChangeFriendsList(c.player.account, myBinaryReader, args.Bytes);
                  break;
                case 55:
                  Server.ReceiveReport(c, c.player.account.name, myBinaryReader);
                  break;
                case 57:
                  myBinaryReader.ReadString();
                  Server.ChangeChatSettings(c.player.account, myBinaryReader);
                  break;
                case 60:
                  c.player.settingsPlayer.fullBook = myBinaryReader.ReadByte();
                  c.SendBytes(args.Bytes);
                  break;
                case 75:
                  if (c.player.account.accountType.IsMuted())
                    return;
                  byte num12 = myBinaryReader.ReadByte();
                  switch (num12)
                  {
                    case 253:
                    case 254:
                      c.player.account.displayClanPrefix = num12 == (byte) 253 ? (byte) 1 : (byte) 0;
                      break;
                    default:
                      c.player.account.displayedIcon = (int) num12;
                      break;
                  }
                  c.player.account.displayedIcon = (int) num12;
                  Server.UpdateAccountInfo(c.player.account);
                  break;
                case 77:
                  Server.ValidateShare(c, myBinaryReader, args.Bytes, false);
                  break;
                case 78:
                  new GameFacts().ManualDeserialize(myBinaryReader, true);
                  break;
                case 79:
                  GameFacts gg3 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gg3))
                  {
                    if (gg3.connections.Count == 0 || gg3.connections[0] != c || gg3.GetInviteMode() == InviteEnum.BookClub || gg3.GetTournamentMode())
                      return;
                    Restrictions restrictions = Restrictions.Deserialize(myBinaryReader, (byte) 1);
                    gg3.restrictions = restrictions.AnyRestricted() ? restrictions : (Restrictions) null;
                    Server.SendGameModeUpdate(gg3);
                    break;
                  }
                  break;
                case 80:
                  GameFacts gg4 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gg4))
                  {
                    if (gg4.connections.Count == 0 || gg4.connections[0] != c || gg4.GetInviteMode() == InviteEnum.BookClub)
                      return;
                    int num13 = gg4.GetRatedMode() ? 1 : 0;
                    GameSettings gameSettings = new GameSettings();
                    gameSettings.Deserialize(myBinaryReader);
                    gameSettings.Clamp();
                    gg4.settings = gameSettings;
                    if (c.player.account.accountType.IsMuted() && !string.IsNullOrEmpty(gg4.settings.description))
                      gg4.settings.description = "";
                    if (!string.IsNullOrEmpty(gg4.settings.description))
                      MyLog.MainLog("[Game Description] [" + c.name + "] " + gg4.settings.description);
                    if (num13 != 0)
                    {
                      gg4.SetRatedMode(true);
                      gg4.SetTeamMode(TeamEnum.Yes);
                      gg4.SetPlayersPerTeam(PlayersPerTeam.Half);
                      gg4.SetSpectatorMode(true);
                      gg4.ReSetStyle(GameStyle.Arcane_Monster);
                      gg4.ReSetStyle(GameStyle.Sandbox);
                    }
                    else
                      gg4.SetRatedMode(false);
                    if (gg4.GetRatedMode() || !c.player.account.accountType.IsModPlusTOParticipate())
                      gg4.SetTournamentMode(false);
                    else if (gg4.GetTournamentMode())
                      gg4.tournamentWasSetByTO = c.player.account.accountType.IsModPlusTO();
                    Server.SendGameModeUpdate(gg4);
                    break;
                  }
                  break;
                case 82:
                  GameFacts gg5 = (GameFacts) null;
                  if (Server._games.TryGetValue(c.player.gameNumber, out gg5))
                  {
                    if (gg5.connections.Count == 0 || gg5.connections[0] != c || gg5.GetInviteMode() == InviteEnum.BookClub || gg5.GetTournamentMode())
                      return;
                    string str = myBinaryReader.ReadString();
                    if (str.Length > 40)
                      return;
                    gg5.settings.description = str;
                    if (c.player.account.accountType.IsMuted() && !string.IsNullOrEmpty(str))
                      gg5.settings.description = "";
                    if (!string.IsNullOrEmpty(str))
                      MyLog.MainLog("[Game Description] [" + c.name + "] " + str);
                    Server.SendGameModeUpdate(gg5);
                    break;
                  }
                  break;
                case 84:
                  Server.HandleClanMsg(c, myBinaryReader);
                  break;
                case 150:
                  Server.ValidateClanChat(c, myBinaryReader, args.Bytes);
                  break;
                case 151:
                  Server.ValidatePrivateChatMessage(myBinaryReader.ReadString(), c, myBinaryReader, args.Bytes);
                  break;
                case 153:
                  Server.ValidateChatMessage(c, myBinaryReader, args.Bytes, false);
                  break;
                case 155:
                  Server.ValidateQuickChat(c, myBinaryReader, args.Bytes, false);
                  break;
                case 193:
                  Server.MovePlayer(c, Location.Lobby);
                  break;
                default:
                  Server.GlobalHandler(c, myBinaryReader, args.Bytes, tag);
                  break;
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex.ToString());
            }
          }
        }
      }
      args.Recycle();
    }));
  }

  private static void SendGameModeUpdate(GameFacts gg)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 14);
        w.Write(gg.id);
        gg.settings.Serialize(w);
      }
      byte[] array = memoryStream.ToArray();
      foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> keyValuePair in Server._lobby.Concat<KeyValuePair<Hazel.Connection, Hazel.Connection>>((IEnumerable<KeyValuePair<Hazel.Connection, Hazel.Connection>>) Server._unrated))
      {
        if (keyValuePair.Key.State == ConnectionState.Connected)
          keyValuePair.Key.SendBytes(array);
      }
    }
  }

  public static void RemoveGameInstance(int port)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 98);
        myBinaryWriter.Write(port);
      }
      Server.SendLobbyUnrated(memoryStream.ToArray());
    }
    GameFacts gameFacts;
    if (Server._games.TryGetValue(port, out gameFacts) && gameFacts.game != null)
      gameFacts.game.CleanUp();
    Server._games.Remove(port);
  }

  public static void GlobalHandler(Hazel.Connection c, myBinaryReader reader, byte[] bytes, byte tag)
  {
  }

  private static void RatedHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes.Length == 1 && args.Bytes[0] == (byte) 34)
      return;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      Hazel.Connection connection = (Hazel.Connection) sender;
      if (connection.player.location == Location.LobbyRated)
      {
        using (MemoryStream memoryStream = new MemoryStream(args.Bytes))
        {
          using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
          {
            try
            {
              byte tag = myBinaryReader.ReadByte();
              switch (tag)
              {
                case 44:
                  if (Server._ratedQueue.ContainsKey(connection))
                  {
                    Server.ReturnServerMsg(connection, "Cannot change spells while finding opponents");
                    return;
                  }
                  SettingsPlayer b1 = new SettingsPlayer();
                  b1.Deserialize(myBinaryReader);
                  connection.player.settingsPlayer.CopySpells(b1);
                  Prestige.VerifySpells(connection.player.account, connection.player.settingsPlayer.spells, ref connection.player.settingsPlayer._spells.fullBook);
                  connection.player.settingsPlayer.VerifySpells();
                  connection.player.settingsPlayer.ReseralizeSpells(connection);
                  break;
                case 45:
                  SettingsPlayer b2 = new SettingsPlayer();
                  b2.Deserialize(myBinaryReader);
                  connection.player.settingsPlayer.CopyOutfit(b2);
                  connection.player.settingsPlayer.VerifyOutfit(connection.player.account.cosmetics, connection.player.account);
                  connection.SendBytes(args.Bytes);
                  break;
                case 50:
                  myBinaryReader.ReadString();
                  Server.ChangeFriendsList(connection.player.account, myBinaryReader, args.Bytes);
                  break;
                case 55:
                  Server.ReceiveReport(connection, connection.player.account.name, myBinaryReader);
                  break;
                case 57:
                  myBinaryReader.ReadString();
                  Server.ChangeChatSettings(connection.player.account, myBinaryReader);
                  break;
                case 60:
                  if (Server._ratedQueue.ContainsKey(connection))
                  {
                    Server.ReturnServerMsg(connection, "Cannot change elementals while finding opponents");
                    return;
                  }
                  byte num1 = myBinaryReader.ReadByte();
                  connection.player.settingsPlayer.fullBook = num1;
                  connection.SendBytes(args.Bytes);
                  break;
                case 67:
                  if (myBinaryReader.ReadBoolean())
                  {
                    if (connection.player.account.discord == 0UL)
                    {
                      Server.ReturnServerMsg(connection, "Verify your discord ID to play rated games.");
                      return;
                    }
                    if (connection.player.account.Rating < (short) 1000)
                    {
                      Server.ReturnServerMsg(connection, "Looks like you're banned from rated games for the time being.");
                      return;
                    }
                    if (!Server._ratedQueue.ContainsKey(connection))
                    {
                      myBinaryReader.ReadBoolean();
                      RatedContainer ratedContainer = RatedContainer.Deserialize(myBinaryReader);
                      int num2 = 0;
                      bool flag1 = false;
                      foreach (RatedFacts ratedFacts in ratedContainer.list)
                      {
                        ratedFacts.VerifyGameType(true);
                        ratedFacts.rating = connection.player.account[ratedFacts.gameType].rating;
                        ++num2;
                        bool flag2 = (ratedFacts.extraOptions & 16) != 0;
                        bool flag3;
                        if (ratedFacts.spellOverrides == null)
                        {
                          flag3 = flag2 && Server.OriginalSpellsOnly(GameStyle.Original_Spells_Only, connection.player.settingsPlayer);
                          if (!flag1)
                          {
                            flag1 = true;
                            if (Server._restrictions != null && Server._restrictions.HasRestricted(connection.player.settingsPlayer.spells))
                            {
                              Server.ReturnServerMsg(connection, "Your spell book contains a restricted spell!!!!");
                              return;
                            }
                            if (Server._restrictions != null && Server._restrictions.CheckElemental((int) connection.player.settingsPlayer.Elemental))
                            {
                              Server.ReturnServerMsg(connection, "You're using a restricted elemental!!!!");
                              return;
                            }
                          }
                        }
                        else
                        {
                          ratedFacts.spellOverrides.VerifySpells();
                          if (!Prestige.VerifySpells(connection.player.account, ratedFacts.spellOverrides.spells, ref ratedFacts.spellOverrides.fullBook))
                          {
                            Server.ReturnServerMsg(connection, "Your settings profile {" + (object) num2 + "} contains a locked spell!!!!");
                            return;
                          }
                          flag3 = flag2 && Server.OriginalSpellsOnly(GameStyle.Original_Spells_Only, ratedFacts.spellOverrides);
                          if (Server._restrictions != null && Server._restrictions.HasRestricted(ratedFacts.spellOverrides.spells))
                          {
                            Server.ReturnServerMsg(connection, "Your settings profile {" + (object) num2 + "} contains a restricted spell!!!!");
                            return;
                          }
                          if (Server._restrictions != null && Server._restrictions.CheckElemental((int) ratedFacts.spellOverrides.Elemental))
                          {
                            Server.ReturnServerMsg(connection, "Your settings profile {" + (object) num2 + "} is using a restricted elemental!!!!");
                            return;
                          }
                        }
                        if (flag3)
                        {
                          Server.ReturnServerMsg(connection, "You have original spells enabled, but have non-original spells in your settings profile {" + (object) num2 + "} !!!!");
                          return;
                        }
                      }
                      ratedContainer.diff = 400;
                      Server._ratedQueue.Add(connection, ratedContainer);
                      connection.SendBytes(args.Bytes);
                      break;
                    }
                    break;
                  }
                  if (Server._ratedQueue.ContainsKey(connection))
                  {
                    Server._ratedQueue.Remove(connection);
                    connection.SendBytes(args.Bytes);
                    Server.OnStoppedSearching(connection);
                    break;
                  }
                  break;
                case 75:
                  if (connection.player.account.accountType.IsMuted())
                    return;
                  byte num3 = myBinaryReader.ReadByte();
                  switch (num3)
                  {
                    case 253:
                    case 254:
                      connection.player.account.displayClanPrefix = num3 == (byte) 253 ? (byte) 1 : (byte) 0;
                      break;
                    default:
                      connection.player.account.displayedIcon = (int) num3;
                      break;
                  }
                  connection.player.account.displayedIcon = (int) num3;
                  Server.UpdateAccountInfo(connection.player.account);
                  break;
                case 76:
                  if (Server._ratedQueue.ContainsKey(connection))
                  {
                    Server._ratedQueue.Remove(connection);
                    Server.OnStoppedSearching(connection);
                  }
                  Server.MovePlayer(connection, Location.Lobby);
                  Server.SyncLobby(connection);
                  break;
                case 77:
                  Server.ValidateShare(connection, myBinaryReader, args.Bytes, false, true);
                  break;
                case 84:
                  Server.HandleClanMsg(connection, myBinaryReader);
                  break;
                case 150:
                  Server.ValidateClanChat(connection, myBinaryReader, args.Bytes);
                  break;
                case 151:
                  Server.ValidatePrivateChatMessage(myBinaryReader.ReadString(), connection, myBinaryReader, args.Bytes);
                  break;
                default:
                  Server.GlobalHandler(connection, myBinaryReader, args.Bytes, tag);
                  break;
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex.ToString());
            }
          }
        }
      }
      args.Recycle();
    }));
  }

  public static bool OriginalSpellsOnly(GameStyle styles, SpellsOnly s)
  {
    int num = 119;
    foreach (byte spell in s.spells)
    {
      if ((int) spell > num && spell != byte.MaxValue)
        return true;
    }
    return false;
  }

  public static bool OriginalSpellsOnly(GameStyle styles, SettingsPlayer s)
  {
    if ((styles & GameStyle.Original_Spells_Only) == ~GameStyle.Dont_Mind)
      return false;
    int num = 119;
    foreach (byte spell in s.spells)
    {
      if ((int) spell > num && spell != byte.MaxValue)
        return true;
    }
    return false;
  }

  public static void SendUnratedMsg(GameFacts gg, string msg)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 33);
        myBinaryWriter.Write(msg);
      }
      Server.SendUnratedMessage(gg, memoryStream.ToArray());
    }
  }

  private static void RandomRestrictedSpells(GameFacts gg)
  {
    System.Random randomNumberGenerator = Server.randomNumberGenerator;
    int num1 = gg.GetStyle().HasStyle(GameStyle.Original_Spells_Only) ? 10 : (int) (RandomExtensions.LastBook() + 1);
    bool elementals = gg.GetStyle().HasStyle(GameStyle.Elementals);
    List<byte> level1 = new List<byte>();
    List<byte> level2 = new List<byte>();
    List<byte> level3 = new List<byte>();
    List<int> availElemental = new List<int>();
    int max = num1 * 12;
    if (elementals)
    {
      for (int index = 0; index < num1; ++index)
      {
        if ((gg.settings.restrictions.elementals & 1 << index) != 0)
          availElemental.Add(index);
      }
      if (availElemental.Count == 0)
      {
        elementals = false;
        gg.ReSetStyle(GameStyle.Elementals);
      }
    }
    for (byte index = 0; (int) index < max; ++index)
    {
      if (gg.settings.restrictions.availableSpells[(int) index])
      {
        if (Inert.Instance._spells[(int) index].level == 1)
          level1.Add(index);
        else if (Inert.Instance._spells[(int) index].level == 2)
          level2.Add(index);
        else
          level3.Add(index);
      }
    }
    for (int index1 = 0; index1 < gg.connections.Count; ++index1)
    {
      Server.RandomRestrictedSpells(gg.connections[index1].player.settingsPlayer, gg.settings.restrictions, level1, level2, level3, availElemental, max, elementals);
      if (gg.GetMultiTeamMode())
      {
        int num2 = gg.GetNumberPlayersPerTeam() - 1;
        for (int index2 = 0; index2 < num2; ++index2)
          Server.RandomRestrictedSpells(gg.connections[index1].player.multiSettingsPlayer[index2], gg.settings.restrictions, level1, level2, level3, availElemental, max, elementals);
      }
    }
  }

  internal static void StartGame(GameFacts gg, bool realRated = false)
  {
    gg.CalculateGameType();
    GameStyle style = gg.GetStyle();
    if (style.HasStyle(GameStyle.Wind))
      gg.ReSetStyle(GameStyle.Wind);
    if ((!realRated || !gg.GetRatedMode()) && (style.HasStyle(GameStyle.Original_Spells_Only) || gg.settings.restrictions != null) && !style.HasStyle(GameStyle.Random_Spells))
    {
      bool flag1 = style.HasStyle(GameStyle.Elementals);
      bool flag2 = false;
      for (int index1 = 0; index1 < gg.connections.Count; ++index1)
      {
        Hazel.Connection connection = gg.connections[index1];
        if (style.HasStyle(GameStyle.Original_Spells_Only))
        {
          if (flag1 && connection.player.settingsPlayer.fullBook > (byte) 10)
          {
            flag2 = true;
            Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a non-original elemental - Cannot start");
          }
          if (Server.OriginalSpellsOnly(style, connection.player.settingsPlayer))
          {
            flag2 = true;
            Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a non-original spell - Cannot start");
          }
          if (gg.GetMultiTeamMode() && connection.player.multiSettingsPlayer != null)
          {
            foreach (SettingsPlayer s in connection.player.multiSettingsPlayer)
            {
              if (flag1 && s.fullBook > (byte) 10)
              {
                flag2 = true;
                Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a non-original elemental - Cannot start");
                return;
              }
              if (Server.OriginalSpellsOnly(style, s))
              {
                flag2 = true;
                Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a non-original spell - Cannot start");
                return;
              }
            }
          }
        }
        if (gg.settings.restrictions != null)
        {
          for (int index2 = 0; index2 < connection.player.settingsPlayer.spells.Length; ++index2)
          {
            if (connection.player.settingsPlayer.spells[index2] != byte.MaxValue && !gg.settings.restrictions.availableSpells[(int) connection.player.settingsPlayer.spells[index2]])
            {
              flag2 = true;
              Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a restricted spell - Cannot start");
              break;
            }
          }
          if (flag1 && connection.player.settingsPlayer.fullBook > (byte) 0 && (gg.settings.restrictions.elementals & 1 << (int) connection.player.settingsPlayer.fullBook - 1) == 0)
          {
            flag2 = true;
            Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a restricted elemental - Cannot start");
          }
          if (gg.GetMultiTeamMode() && connection.player.multiSettingsPlayer != null)
          {
            foreach (SettingsPlayer settingsPlayer in connection.player.multiSettingsPlayer)
            {
              foreach (byte spell in settingsPlayer.spells)
              {
                if (spell != byte.MaxValue && !gg.settings.restrictions.availableSpells[(int) spell])
                {
                  flag2 = true;
                  Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a restricted spell - Cannot start");
                  return;
                }
              }
              if (flag1 && settingsPlayer.fullBook > (byte) 0 && (gg.settings.restrictions.elementals & 1 << (int) settingsPlayer.fullBook - 1) == 0)
              {
                flag2 = true;
                Server.SendUnratedMsg(gg, "[Game]  " + connection.name + "  is using a restricted elemental - Cannot start");
                return;
              }
            }
          }
        }
      }
      if (flag2)
        return;
    }
    gg.status = (byte) 1;
    gg.seed = Environment.TickCount;
    if (!gg.settings.fixedMapSeed)
      gg.settings.mapSeed = Server.random.Next();
    gg.serverStartTime = (int) Time.realtimeSinceStartup;
    MapEnum mapMode = gg.GetMapMode();
    if (gg.GetRatedMode())
      gg.ReSetStyle(GameStyle.Arcane_Monster);
    gg.ShufflePlayers();
    if ((mapMode & MapEnum.Random) != ~MapEnum.Dont_Mind || !System.Enum.IsDefined(typeof (MapEnum), (object) mapMode))
    {
      if (mapMode == MapEnum.Random)
      {
        int e = Server.randomNumberGenerator.Next(0, 11);
        gg.realMap = GameFacts.MapFromIndex(e);
        gg.SetMapMode(gg.realMap);
      }
      else
      {
        List<MapEnum> mapEnumList = new List<MapEnum>();
        foreach (MapEnum mapEnum in (MapEnum[]) System.Enum.GetValues(typeof (MapEnum)))
        {
          if ((mapMode & mapEnum) != ~MapEnum.Dont_Mind && mapEnum != MapEnum.Random && mapEnum != MapEnum.Dont_Mind)
            mapEnumList.Add(mapEnum);
        }
        if (mapEnumList.Count > 0)
        {
          int index = Server.randomNumberGenerator.Next(0, mapEnumList.Count);
          gg.realMap = mapEnumList[index];
          gg.SetMapMode(gg.realMap);
        }
        else
        {
          int e = Server.randomNumberGenerator.Next(0, 11);
          gg.realMap = GameFacts.MapFromIndex(e);
          gg.SetMapMode(gg.realMap);
        }
      }
    }
    else
      gg.realMap = mapMode;
    if (style.HasStyle(GameStyle.Random_Spells))
    {
      if (gg.restrictions != null)
      {
        Server.RandomRestrictedSpells(gg);
      }
      else
      {
        int num = gg.GetNumberPlayersPerTeam() - 1;
        for (int index3 = 0; index3 < gg.connections.Count; ++index3)
        {
          Server.RandomSpells(gg.connections[index3].player.settingsPlayer, style);
          if (gg.GetMultiTeamMode())
          {
            for (int index4 = 0; index4 < num; ++index4)
              Server.RandomSpells(gg.connections[index3].player.multiSettingsPlayer[index4], style);
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < gg.connections.Count; ++index)
      {
        gg.connections[index].player.settingsPlayer.VerifySpells();
        if (!gg.GetStyle().HasStyle(GameStyle.Sandbox) || gg.GetRatedMode())
          Prestige.VerifySpells(gg.connections[index].player.account, gg.connections[index].player.settingsPlayer.spells, ref gg.connections[index].player.settingsPlayer._spells.fullBook);
      }
    }
    int numberPlayersPerTeam = gg.GetNumberPlayersPerTeam();
    for (int index5 = 0; index5 < gg.connections.Count; ++index5)
    {
      SettingsPlayer settingsPlayer1 = new SettingsPlayer();
      settingsPlayer1.CopySpells(gg.connections[index5].player.settingsPlayer);
      settingsPlayer1.CopyOutfit(gg.connections[index5].player.settingsPlayer);
      gg.settingsPlayer.Add(settingsPlayer1);
      gg.accounts.Add(gg.connections[index5].player.account);
      gg.players.Add(gg.connections[index5].player.account.name);
      if (gg.GetMultiTeamMode())
      {
        int minValue = gg.settingsPlayer.Count - 1;
        for (int index6 = 0; index6 < numberPlayersPerTeam - 1; ++index6)
        {
          SettingsPlayer settingsPlayer2 = new SettingsPlayer();
          if (index6 < gg.connections[index5].player.multiSettingsPlayer.Count)
          {
            settingsPlayer2.CopySpells(gg.connections[index5].player.multiSettingsPlayer[index6]);
            settingsPlayer2.CopyOutfit(gg.connections[index5].player.multiSettingsPlayer[index6]);
          }
          else
          {
            settingsPlayer2.CopySpells(gg.connections[index5].player.settingsPlayer);
            settingsPlayer2.CopyOutfit(gg.connections[index5].player.settingsPlayer);
          }
          gg.settingsPlayer.Add(settingsPlayer2);
          gg.accounts.Add(gg.connections[index5].player.account);
          gg.players.Add(gg.connections[index5].player.account.name);
        }
        if (gg.GetStyle().HasStyle(GameStyle.Shuffle_Players))
        {
          int maxValue = minValue + numberPlayersPerTeam;
          for (int index7 = minValue; index7 < maxValue; ++index7)
          {
            int index8 = Server.random.Next(minValue, maxValue);
            if (index8 != index7)
            {
              SettingsPlayer settingsPlayer3 = gg.settingsPlayer[index7];
              gg.settingsPlayer[index7] = gg.settingsPlayer[index8];
              gg.settingsPlayer[index8] = settingsPlayer3;
            }
          }
        }
      }
    }
    gg.game = new ZGame();
    gg.game.gameFacts = gg;
    gg.game.ServerStartGame();
    for (int index = 0; index < gg.connections.Count; ++index)
    {
      Server._unrated.Remove(gg.connections[index]);
      if (Server._lobby.ContainsKey(gg.connections[index]))
      {
        Server._lobby.Remove(gg.connections[index]);
        Server.RemoveFromLobby(gg.connections[index]);
      }
      Server.SendStartGame(gg.connections[index], gg);
    }
    gg.SetMapMode(mapMode);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 12);
        myBinaryWriter.Write(gg.id);
      }
      Server.SendLobbyUnrated(memoryStream.ToArray());
    }
  }

  public static void SendStartGame(Hazel.Connection c, GameFacts gg)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
      {
        writer.Write((byte) 70);
        writer.Write(gg.id);
        gg.ManualSerialize(writer, true);
        writer.Write(Server.DebugResyncs);
      }
      c.player.location = Location.Ingame;
      c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.UnratedHandler);
      c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.RatedHandler);
      c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.LobbyHandler);
      c.DataReceived += new EventHandler<DataReceivedEventArgs>(gg.game.GameHandler);
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public static void RandomRestrictedSpells(
    SettingsPlayer settings,
    Restrictions restrictions,
    List<byte> level1,
    List<byte> level2,
    List<byte> level3,
    List<int> availElemental,
    int max,
    bool elementals)
  {
    System.Random randomNumberGenerator = Server.randomNumberGenerator;
    byte num1 = availElemental.Count > 0 ? (byte) (availElemental[randomNumberGenerator.Next(0, availElemental.Count)] + 1) : (byte) 0;
    int num2 = ((int) num1 - 1) * 12;
    int num3 = (int) num1 * 12;
    settings.fullBook = num1;
    HashSet<byte> source = new HashSet<byte>();
    if (restrictions.availableSpells[4])
      source.Add((byte) 4);
    int num4 = 0;
    bool flag = false;
    if (level1.Count < 8)
    {
      for (int index = 0; index < level1.Count; ++index)
      {
        if (level1[index] >= (byte) 156 && level1[index] < (byte) 168)
          flag = true;
        source.Add(level1[index]);
      }
    }
    else
    {
      while (source.Count < 7 && source.Count < level1.Count && num4 < 30)
      {
        ++num4;
        byte num5 = level1[randomNumberGenerator.Next(0, level1.Count)];
        if (!source.Contains(num5) && ((int) num5 < num2 || (int) num5 >= num3))
        {
          if (num5 >= (byte) 156 && num5 < (byte) 168)
            flag = true;
          source.Add(num5);
        }
      }
    }
    if (level2.Count < 8)
    {
      for (int index = 0; index < level2.Count; ++index)
      {
        if (level2[index] >= (byte) 156 && level2[index] < (byte) 168)
          flag = true;
        source.Add(level2[index]);
      }
    }
    else
    {
      int num6 = 0;
      while (source.Count < 14 && source.Count < level1.Count + level2.Count && num6 < 30)
      {
        ++num6;
        byte num7 = level2[randomNumberGenerator.Next(0, level2.Count)];
        if (!source.Contains(num7) && ((int) num7 < num2 || (int) num7 >= num3))
        {
          if (num7 >= (byte) 156 && num7 < (byte) 168)
            flag = true;
          source.Add(num7);
        }
      }
    }
    if (level3.Count < 3)
    {
      for (int index = 0; index < level3.Count; ++index)
      {
        if (flag || Inert.Instance._spells[(int) level3[index]].spellEnum != SpellEnum.Ritual)
          source.Add(level3[index]);
      }
    }
    else
    {
      int num8 = 0;
      while (source.Count < 16 && source.Count < level1.Count + level2.Count + level3.Count && num8 < 30)
      {
        ++num8;
        byte index = level3[randomNumberGenerator.Next(0, level3.Count)];
        if (!source.Contains(index) && ((int) index < num2 || (int) index >= num3) && (flag || Inert.Instance._spells[(int) index].spellEnum != SpellEnum.Ritual))
          source.Add(index);
      }
    }
    if (source.Count < 16 && level1.Count + level2.Count + level3.Count > source.Count)
    {
      for (int index = 0; index < level1.Count && source.Count < 16; ++index)
        source.Add(level1[index]);
      for (int index = 0; index < level2.Count && source.Count < 16; ++index)
        source.Add(level2[index]);
      for (int index = 0; index < level3.Count && source.Count < 16; ++index)
        source.Add(level3[index]);
    }
    List<byte> list = source.ToList<byte>();
    list.Sort();
    int index1;
    for (index1 = 0; index1 < 16 && index1 < list.Count; ++index1)
      settings.spells[index1] = list[index1];
    for (; index1 < 16; ++index1)
      settings.spells[index1] = byte.MaxValue;
  }

  public static void RandomSpells(SettingsPlayer settings, GameStyle styles)
  {
    System.Random randomNumberGenerator = Server.randomNumberGenerator;
    int maxValue = styles.HasStyle(GameStyle.Original_Spells_Only) ? 10 : (int) (RandomExtensions.LastBook() + 1);
    bool flag1 = styles.HasStyle(GameStyle.Elementals);
    bool flag2 = styles.HasStyle(GameStyle.Original_Spells_Only);
    byte num1 = (byte) ((maxValue == 2 ? 0 : (int) (byte) randomNumberGenerator.Next(0, maxValue)) + 1);
    if (!flag1)
      num1 = (byte) 0;
    int num2 = (int) num1 - 1;
    settings.fullBook = num1;
    HashSet<byte> source = new HashSet<byte>() { (byte) 4 };
    bool flag3 = false;
    while (source.Count < 7)
    {
      int num3 = randomNumberGenerator.Next(0, maxValue);
      if (maxValue > 0 & flag1 && num2 == maxValue)
      {
        for (int index = 0; index < 20; ++index)
        {
          num3 = randomNumberGenerator.Next(0, maxValue);
          if (num3 != num2)
            break;
        }
      }
      if (num3 == 13)
        flag3 = true;
      int num4 = num3 == 0 ? randomNumberGenerator.Next(0, 12) : randomNumberGenerator.Next(0, 5) * 2;
      byte num5 = (byte) (num3 * 12 + num4);
      if (!source.Contains(num5) && (!flag2 || num5 != (byte) 11))
        source.Add(num5);
    }
    while (source.Count < 14)
    {
      int num6 = randomNumberGenerator.Next(0, maxValue);
      if (maxValue > 0 & flag1 && num2 == maxValue)
      {
        for (int index = 0; index < 20; ++index)
        {
          num6 = randomNumberGenerator.Next(0, maxValue);
          if (num6 != num2)
            break;
        }
      }
      if (num6 == 13)
        flag3 = true;
      int num7 = num6 == 0 ? randomNumberGenerator.Next(0, 12) : randomNumberGenerator.Next(0, 5) * 2 + 1;
      byte num8 = (byte) (num6 * 12 + num7);
      if (!source.Contains(num8) && num8 != (byte) 141)
        source.Add(num8);
    }
    while (source.Count < 16)
    {
      int num9 = randomNumberGenerator.Next(1, maxValue);
      if (maxValue > 0 & flag1 && num2 == maxValue)
      {
        for (int index = 0; index < 20; ++index)
        {
          num9 = randomNumberGenerator.Next(0, maxValue);
          if (num9 != num2)
            break;
        }
      }
      int num10 = randomNumberGenerator.Next(10, 12);
      byte index1 = (byte) (num9 * 12 + num10);
      if (!source.Contains(index1) && (flag3 || Inert.Instance._spells[(int) index1].spellEnum != SpellEnum.Ritual))
        source.Add(index1);
    }
    List<byte> list = source.ToList<byte>();
    list.Sort();
    for (int index = 0; index < 16; ++index)
      settings.spells[index] = list[index];
  }

  public static void ReceiveReport(Hazel.Connection c, string reporter, myBinaryReader reader)
  {
  }

  public static void VerifyReport(Report report)
  {
  }

  private static void AuthHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes.Length == 1 && args.Bytes[0] == (byte) 34)
      return;
    Hazel.Connection c = (Hazel.Connection) sender;
    byte[] bb = args.Bytes;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      if (c.player.location == Location.Authenticating)
      {
        using (MemoryStream memoryStream1 = new MemoryStream(bb))
        {
          using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream1))
          {
            try
            {
              switch (myBinaryReader.ReadByte())
              {
                case 1:
                  if (Server._connections.ContainsKey(c))
                  {
                    Debug.LogError((object) "Connections already contains connection");
                    Server.SendMessage(c, (byte) 19);
                    break;
                  }
                  myBinaryReader.ReadBytes();
                  myBinaryReader.ReadString();
                  string str = myBinaryReader.ReadString().Trim();
                  myBinaryReader.ReadString();
                  myBinaryReader.ReadBytes();
                  if (string.IsNullOrEmpty(str) || Server._names.ContainsKey(str) || str.Length > 13)
                  {
                    Server.SendMessage(c, (byte) 3);
                    break;
                  }
                  Account account = new Account(str);
                  account.cosmetics = Server.DefaultCosmetics;
                  account.accountType = AccountType.None;
                  Server._names.Add(str, c);
                  c.player.account = account;
                  Server._connections.Add(c, c);
                  Server.MovePlayer(c, Location.MainMenu);
                  break;
                case 37:
                  string name = myBinaryReader.ReadString();
                  byte[] cypo = myBinaryReader.ReadBytes();
                  string endPoint = c.EndPoint.ToString().Split(':')[0];
                  UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
                  {
                    ValidSpectator validSpectator;
                    if (c != null && c.State == ConnectionState.Connected && Server.validSpectator.TryGetValue(name, out validSpectator))
                    {
                      GameFacts gf;
                      if (Global.combareByteArrays(validSpectator.cryp, cypo) && string.Equals(validSpectator.isp, endPoint) && Server._games.TryGetValue(validSpectator.gameID, out gf) && gf.game != null)
                      {
                        Server.validSpectator.Remove(name);
                        c.player.account.name = validSpectator.name;
                        c.player.gameNumber = gf.id;
                        c.player.id = gf.game.GetSpectatorID();
                        c.player.spectator = true;
                        c.DataReceived -= new EventHandler<DataReceivedEventArgs>(Server.AuthHandler);
                        c.DataReceived += new EventHandler<DataReceivedEventArgs>(gf.game.ServerSpectatorHandler);
                        gf.game.RemoveInactiveSpectators();
                        if (gf.game.timeline.Count > 10)
                        {
                          gf.game.SendOldData((ZPerson) null, c, "", onTrue: (Action) (() => gf.game.spectators.Add(c)));
                          return;
                        }
                        gf.game.spectators.Add(c);
                        using (MemoryStream memoryStream2 = new MemoryStream())
                        {
                          using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream2))
                          {
                            myBinaryWriter.Write((byte) 35);
                            myBinaryWriter.Write(gf.game.timeline.Count);
                            for (int index = 0; index < gf.game.timeline.Count; ++index)
                              myBinaryWriter.Write(gf.game.timeline[index]);
                          }
                          c.SendBytes(memoryStream2.ToArray());
                          return;
                        }
                      }
                    }
                    c.Close();
                  }));
                  break;
                case 46:
                  Server.SendMessage(c, (byte) 47);
                  break;
                case 48:
                  Server.SendMessage(c, (byte) 47);
                  break;
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex.ToString());
            }
          }
        }
      }
      args.Recycle();
    }));
  }

  private static void NewConnectionHandler(object sender, NewConnectionEventArgs args)
  {
    try
    {
      bool flag = true;
      args.Connection.randomBytes = new byte[25];
      if (flag)
      {
        args.Connection.DataReceived += new EventHandler<DataReceivedEventArgs>(Server.AuthHandler);
        args.Connection.Disconnected += new EventHandler<DisconnectedEventArgs>(Server.Disconnected);
        Server.rngCsp.GetBytes(args.Connection.randomBytes);
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 1);
          myBinaryWriter.Write(Inert.Version);
          myBinaryWriter.Write(args.Connection.randomBytes);
          myBinaryWriter.Write(flag);
        }
        if (!flag)
          args.Connection.SendBytesAndClose(memoryStream.ToArray());
        else
          args.Connection.SendBytes(memoryStream.ToArray());
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.ToString());
    }
    args.Recycle();
  }

  private static void ApplySeasonalOutfits(Hazel.Connection c)
  {
    switch (Server.settings.season)
    {
      case Server.ServerSettings.Season.Halloween:
        Server.ApplyOutfit(c, SettingsPlayer.seasonHalloween);
        break;
      case Server.ServerSettings.Season.Thanksgiving:
        Server.ApplyOutfit(c, SettingsPlayer.seasonThanksgiving);
        break;
      case Server.ServerSettings.Season.Christmas:
        Server.ApplyOutfit(c, SettingsPlayer.seasonChristmas);
        break;
      case Server.ServerSettings.Season.Easter:
        Server.ApplyOutfit(c, SettingsPlayer.seasonEaster);
        break;
    }
  }

  private static void ApplyOutfit(Hazel.Connection c, List<SettingsPlayer.Seasonal> list)
  {
  }

  private static void ApplyOutfit(Account c, List<SettingsPlayer.Seasonal> list)
  {
  }

  private static void RemoveFromBoat(Hazel.Connection c)
  {
    if (Server._games.TryGetValue(c.player.gameNumber, out Server.tempFacts))
    {
      if (c.player.spectator)
        Server.tempFacts.game?.RemoveInactiveSpectators();
      else
        Server.tempFacts.game?.RemoveFromBoat(c);
    }
    c.player.inBoat = false;
  }

  private static void Disconnected(object sender, DisconnectedEventArgs args)
  {
    try
    {
      Hazel.Connection c = (Hazel.Connection) sender;
      c.Disconnected -= new EventHandler<DisconnectedEventArgs>(Server.Disconnected);
      UnityThreadHelper.Dispatcher?.Dispatch((Action) (() =>
      {
        try
        {
          if (c.player.activeConnection == c)
          {
            if (c.player.inBoat)
              Server.RemoveFromBoat(c);
            if (c.player.location != Location.Ingame)
            {
              Server.MovePlayer(c, Location.Disconnecting);
            }
            else
            {
              c.player.location = Location.Ingame_Disconnected;
              Server.SendPlayerLocation(c);
            }
            c.player.activeConnection = (Hazel.Connection) null;
          }
          if (c.miniGame != null)
            c.miniGame.LogicPlayerLeft(c);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ex);
        }
        try
        {
          Server._connections.Remove(c);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ex);
        }
      }));
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.ToString());
    }
  }

  private void Update()
  {
    Server.realTimeSinceStartup = Time.realtimeSinceStartup;
    if ((double) Time.realtimeSinceStartup > (double) Server.nextRatedCheck)
    {
      Server.nextRatedCheck = Time.realtimeSinceStartup + 10f;
      if (Server._ratedQueue.Count > 1)
        Server.FindRatedMatches();
    }
    if ((double) Time.realtimeSinceStartup <= (double) Server.nextKeepAlive)
      return;
    Server.nextKeepAlive = Time.realtimeSinceStartup + 60f;
    Server.KeepAlive();
  }

  public static short MaxDiff(short rating) => 1000;

  public static void FindRatedMatches()
  {
  }

  private static void KeepAlive()
  {
    List<Hazel.Connection> connectionList = new List<Hazel.Connection>();
    foreach (KeyValuePair<Hazel.Connection, Hazel.Connection> connection in Server._connections)
    {
      if (connection.Key.Statistics.CompareMessagesReceived == connection.Key.Statistics.MessagesReceived)
        connectionList.Add(connection.Key);
      else
        connection.Key.Statistics.CompareMessagesReceived = connection.Key.Statistics.MessagesReceived;
    }
    for (int index = 0; index < connectionList.Count; ++index)
      connectionList[index].Close();
  }

  internal static string RandomString(int count)
  {
    char[] chArray = new char[count];
    for (int index = 0; index < count; ++index)
      chArray[index] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[Server.random.Next(0, 62)];
    return new string(chArray);
  }

  internal static string SanitizeFilename(string filename)
  {
    string pattern = string.Format("[{0}]", (object) Regex.Escape(new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + "./\\"));
    return Regex.Replace(filename, pattern, "_").Trim();
  }

  public static void ReturnMsg(Hazel.Connection c, string user)
  {
    if (c == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 58);
        myBinaryWriter.Write("");
        myBinaryWriter.Write(user);
      }
      c?.SendBytes(memoryStream.ToArray());
    }
  }

  public static void Communicate(Hazel.Connection c, string user)
  {
    if (c == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 58);
        myBinaryWriter.Write("");
        myBinaryWriter.Write(user);
      }
      c?.SendBytes(memoryStream.ToArray());
      if (c == Server.Instance?.communicator?.GetConnection || Server.Instance?.communicator?.GetConnection == null)
        return;
      Server.Instance.communicator.GetConnection.SendBytes(memoryStream.ToArray());
    }
  }

  public static void SendStopFindingOpponents(Hazel.Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 67);
        myBinaryWriter.Write(false);
      }
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public static void ReturnServerMsg(Hazel.Connection c, string user)
  {
    if (c == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 33);
        myBinaryWriter.Write(user);
      }
      c?.SendBytes(memoryStream.ToArray());
    }
  }

  public class ServerSettings
  {
    private const int version = 0;
    public Server.ServerSettings.Season season;

    public void Save()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
          this.Serialize(w);
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/serverSettings.txt", memoryStream.ToArray());
      }
    }

    public static void Load()
    {
      if (!System.IO.File.Exists(Application.persistentDataPath + "/serverSettings.txt"))
        return;
      using (MemoryStream memoryStream = new MemoryStream(System.IO.File.ReadAllBytes(Application.persistentDataPath + "/serverSettings.txt")))
      {
        using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
          Server.settings.Deserialize(r);
      }
    }

    public void Deserialize(myBinaryReader r)
    {
      r.ReadInt32();
      this.season = (Server.ServerSettings.Season) r.ReadInt32();
    }

    public void Serialize(myBinaryWriter w)
    {
      w.Write(0);
      w.Write((int) this.season);
    }

    public enum Season
    {
      None,
      Halloween,
      Thanksgiving,
      Christmas,
      Easter,
    }
  }
}
