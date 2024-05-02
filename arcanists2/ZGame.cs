

using Educative;
using Hazel;
using Hazel.Tcp;
using Junk;
using MovementEffects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityThreading;
using Win32Utilities;

#nullable disable
public class ZGame
{
  public ZMyWorld world = new ZMyWorld();
  public ZMap map = new ZMap();
  private Transform _map;
  public MapEnum armageddon;
  public List<Spell> customArmageddon;
  public int nextCreatureID = 1;
  public int nextSpellID = 1;
  public int nextColliderID = 1;
  public int nextEffectorID = 1;
  public bool receivedInitialMsg;
  public int turn = -1;
  public int armageddonTurn = 10;
  public int seasonTurn = -1;
  public int MaxTurnTime = 60;
  public float PlayersMaxTurnTime = 60f;
  public int shouldUpdateTurnTime;
  public ServerState serverState = new ServerState();
  public bool originalSpellsOnly;
  public bool isWindy;
  public int sandboxTime;
  public bool tutorialPaused;
  public bool winOnDeath = true;
  public bool isSandbox;
  public bool isClient;
  public bool isServer;
  public bool isReplay;
  public bool isTeam;
  public bool isMulti;
  public bool isSpectator;
  public bool isCountdown;
  public bool countdownLose;
  public float countdownTime;
  public bool isTutorial;
  public bool _resyncing;
  public bool AllowDKH;
  public bool firstOceanFury = true;
  public bool hasArcaneMonster;
  public bool AllowMovement = true;
  public bool AllowTerrainDestruction = true;
  public bool AllowInput = true;
  public bool AllowCallbacks = true;
  public bool AllowBounce = true;
  public GameSeason currentSeason;
  public List<ZGame.Resurection> lastMinionToDie = new List<ZGame.Resurection>();
  public PoolElectricity electricityPool;
  public bool MAPCREATED;
  public IsaacCipher random;
  public IEnumerator<float> serverUpdate;
  public IEnumerator<float> resyncUpdate;
  public GameFacts gameFacts;
  public Queue<Action> MoveQue = new Queue<Action>();
  public HashSet<PastBlits> _pastBlits = new HashSet<PastBlits>();
  public ZPerson _uncontrolledPlayer = new ZPerson()
  {
    name = "The Environment",
    id = 26
  };
  public List<ZPerson> _playersExtended = new List<ZPerson>();
  public List<ZPerson> players = new List<ZPerson>();
  public List<ZPerson> team1Players = new List<ZPerson>();
  public List<ZPerson> team2Players = new List<ZPerson>();
  public List<ZPerson> team3Players = new List<ZPerson>();
  public List<ZPerson> team4Players = new List<ZPerson>();
  public List<ZPerson> team5Players = new List<ZPerson>();
  public List<ZPerson> team6Players = new List<ZPerson>();
  public List<ZPerson> team7Players = new List<ZPerson>();
  public List<ZPerson> team8Players = new List<ZPerson>();
  public List<ZPerson> team9Players = new List<ZPerson>();
  public List<ZPerson> team10Players = new List<ZPerson>();
  public List<ZPerson> team11Players = new List<ZPerson>();
  public List<ZPerson> team12Players = new List<ZPerson>();
  public int[] teamIndex = new int[12];
  public int lastTeamsTurn;
  public int TEAM_COUNT;
  public CoroutineInstance ongoing = new CoroutineInstance();
  public List<ZEffector> globalEffectors = new List<ZEffector>();
  public List<ZEffector> windShieldEffectors = new List<ZEffector>();
  public List<ZEffector> elfEffectors = new List<ZEffector>();
  public List<ZEffector> staticCharge = new List<ZEffector>();
  public List<ZCreatureThorn> thorns = new List<ZCreatureThorn>();
  public List<ZCreature> recallDevices = new List<ZCreature>();
  public ZEffector targetEffector;
  public ZEffector dwarfMapEffector;
  public ZEffector naturesWrath;
  public ZEffector blackhole;
  public bool ArcaneZero;
  public bool First_Turn_Teleport;
  public int TurnToLoseArcaneZero;
  internal SpellEnum lastSpellCast = SpellEnum.None;
  internal int sinksThisTurn;
  internal int playersKilledThisTurn;
  internal bool playerKilledByArmageddon;
  internal bool with_the_fishes;
  internal bool portalUsedThisSpellTurn;
  internal bool skimmed_on_water;
  internal int armageddonTurnVariable = -1;
  internal bool decreasePlayer2Cooldowns = true;
  private bool loggedToDiscord;
  public int currentFrame;
  internal ZCreature _lastActiveCreature;
  public List<Connection> spectators = new List<Connection>();
  public int nextSpectatorID = 50;
  public Thread mapCreationThread;
  public List<ZPerson> resigned = new List<ZPerson>();
  private bool sentGameOver;
  private List<ZPerson> tempResignFix = new List<ZPerson>();
  public int totalTurnsCombined;
  private bool firstTurn = true;
  public List<ZCreature> tempList = new List<ZCreature>(5);
  internal int _nextturn;
  private float frameTime = 0.0333333351f;
  public float tickTime;
  private IEnumerator<float> ieKillWait;
  private float timeTillStart;
  private int waitTime = 15;
  private float nextKeepAlive = 360f;
  internal Dictionary<Connection, int> _oldData = new Dictionary<Connection, int>();
  public List<byte[]> timeline = new List<byte[]>();
  private bool resyncOnError = true;
  private bool rematchSent;
  internal List<ZGame.DynamicType> returnData = new List<ZGame.DynamicType>();
  public List<ZGame.TimelineData> timelineList;
  private string replayName = "";
  public static int targetTimelineFrame = 0;
  public static bool replayShowStartPanel = true;
  public int curReplayIndex;
  private string lastReplayFile = "";
  public float replayScrollPos;
  public int replayPastTimeLine;
  private bool replayPaused;
  public bool forceRysncPause;
  public ZGame.SerializationHelper helper;
  public HashSet<ZSpell> xSpell;
  public HashSet<SpellSlot> xSlot;
  public HashSet<ZCreature> xCreature;
  public HashSet<ZEffector> xEffector;
  public HashSet<ZMyCollider> xCollider;
  private bool canSafelyClose;

  public ZGame.GameType gameType => this.gameFacts.gameType;

  public Transform GetMapTransform()
  {
    if ((UnityEngine.Object) this._map == (UnityEngine.Object) null)
      this._map = new GameObject("map").transform;
    return this._map;
  }

  public float DeltaTime => Mathf.Clamp(Time.deltaTime, 0.0f, 0.04f);

  public FixedInt gravity => this.map.Gravity;

  public bool AllowExpansion => !this.originalSpellsOnly;

  public bool isOver => this.serverState.busy == ServerState.Busy.Ended;

  public bool resyncing
  {
    get
    {
      if (this._resyncing)
        return true;
      return this.isReplay && this.curReplayIndex < ZGame.targetTimelineFrame;
    }
    set => this._resyncing = value;
  }

  public bool isElementals => this.gameFacts.GetStyle().HasStyle(GameStyle.Elementals);

  public int MaxHealth(ZCreature c)
  {
    return !((ZComponent) c == (object) null) && !c.isPawn ? (int) this.gameFacts.startHealth : 250;
  }

  public bool isRated => this.gameFacts.GetRatedMode();

  public ZGame()
  {
    this.world.map = this.map;
    this.map.world = this.world;
    this.map.game = this;
  }

  public ZPerson CurrentPlayer()
  {
    return (int) this.serverState.playersTurn >= this.players.Count ? (ZPerson) null : this.players[(int) this.serverState.playersTurn];
  }

  public ZPerson CurrentPlayerNotNull()
  {
    return (int) this.serverState.playersTurn >= this.players.Count ? this.players[0] : this.players[(int) this.serverState.playersTurn];
  }

  public ZCreature CurrentCreature()
  {
    return (int) this.serverState.playersTurn >= this.players.Count ? (ZCreature) null : this.players[(int) this.serverState.playersTurn].first();
  }

  public ZCreature ClientCurrentCreature()
  {
    return ZComponent.IsNull((ZComponent) this._lastActiveCreature) || this._lastActiveCreature.isDead || this._lastActiveCreature.parent == null || !this._lastActiveCreature.parent.yourTurn ? this.CurrentCreature() : this._lastActiveCreature;
  }

  public int GetSpectatorID() => this.nextSpectatorID++;

  public int RandomInt(int min, int max) => max < min ? min : this.random.Next(min, max);

  public FixedInt RandomFixedInt(FixedInt min, FixedInt max)
  {
    return max < min ? min : FixedInt.Create(this.random.Next((int) (min * 100), (int) (max * 100))) / 100;
  }

  public FixedInt RandomFixedInt(long min, long max)
  {
    return max < min ? (FixedInt) min : FixedInt.Create(this.random.Next((int) ((FixedInt) min * 100), (int) ((FixedInt) max * 100))) / 100;
  }

  public FixedInt RandomFixedInt(int min, int max)
  {
    return max < min ? (FixedInt) min : FixedInt.Create(this.random.Next(min * 100, max * 100)) / 100;
  }

  public ZCreature ClosestCreature(ZCreature c, bool friendly)
  {
    ZCreature zcreature1 = (ZCreature) null;
    FixedInt fixedInt1 = (FixedInt) 10000;
    foreach (ZPerson player in c.game.players)
    {
      if (friendly == (player.team == c.parent.team) && !ZComponent.IsNull((ZComponent) player.first()))
      {
        foreach (ZCreature zcreature2 in player.controlled)
        {
          FixedInt fixedInt2 = (FixedInt) MyLocation.Distance(c.position, zcreature2.position);
          if (fixedInt2 < fixedInt1 && (ZComponent) zcreature2 != (object) c)
          {
            fixedInt1 = fixedInt2;
            zcreature1 = zcreature2;
          }
        }
      }
    }
    return zcreature1;
  }

  public void Reset(bool removeListener = true)
  {
    if (removeListener && this.isClient && Client.connection != null)
      Client.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(ZGame.GameClientHandler);
    this._pastBlits.Clear();
    this.players.Clear();
    this.team1Players.Clear();
    this.team2Players.Clear();
    this.team3Players.Clear();
    this.team4Players.Clear();
    this.team5Players.Clear();
    this.team6Players.Clear();
    this.team7Players.Clear();
    this.team8Players.Clear();
    this.team9Players.Clear();
    this.team10Players.Clear();
    this.team11Players.Clear();
    this.team12Players.Clear();
    this.MoveQue.Clear();
    this.ongoing.KillAllCoroutines();
    this.gameFacts.connections.Clear();
    if (this.mapCreationThread != null)
    {
      if (this.mapCreationThread.IsAlive)
        this.mapCreationThread.Abort();
      this.mapCreationThread = (Thread) null;
    }
    if (this.isClient)
    {
      CameraMovement.followTargets.Clear();
      Client._tutorial?.CleanUp();
    }
    this.globalEffectors.Clear();
    this.windShieldEffectors.Clear();
    this.elfEffectors.Clear();
    this.staticCharge.Clear();
    this.thorns.Clear();
    this.recallDevices.Clear();
    if (this.serverUpdate != null)
    {
      Timing.KillCoroutines(this.serverUpdate);
      this.serverUpdate = (IEnumerator<float>) null;
    }
    if (this.resyncUpdate != null)
    {
      Timing.KillCoroutines(this.resyncUpdate);
      this.resyncUpdate = (IEnumerator<float>) null;
    }
    if (this.ieKillWait != null)
      this.ieKillWait = (IEnumerator<float>) null;
    UnityEngine.Resources.UnloadUnusedAssets();
  }

  public void init_Client(ServerState.Busy busy = ServerState.Busy.Not_started)
  {
    Client.Host = false;
    this.turn = -1;
    this.MoveQue.Clear();
    if (this.serverUpdate != null)
      Timing.KillCoroutines(this.serverUpdate);
    this.Awake();
    this.serverState.busy = busy;
    this.serverUpdate = Timing.RunCoroutine(this.ClientUpdate(), Segment.Update);
  }

  public void init_sandbox()
  {
    this.isTeam = false;
    this.isSpectator = false;
    if (!this.resyncing)
      this.timeline.Clear();
    this.isReplay = false;
    Client.Host = true;
    Client.NameOrReplay = Client.Name;
    this.turn = -1;
    this.MoveQue.Clear();
    if (this.serverUpdate != null)
      Timing.KillCoroutines(this.serverUpdate);
    if (this.resyncUpdate != null)
    {
      Timing.KillCoroutines(this.resyncUpdate);
      this.resyncUpdate = (IEnumerator<float>) null;
    }
    this.Awake();
    ZPerson zperson = !(bool) (UnityEngine.Object) Player.Instance ? new ZPerson() : Player.Instance.person;
    zperson.name = Client.Name;
    zperson.account = Client.GetAccount(Client.Name);
    TcpConnection tcpConnection = new TcpConnection();
    zperson.connection = (Connection) tcpConnection;
    tcpConnection.player.player = zperson;
    tcpConnection.player.account = zperson.account;
    this.players.Add(zperson);
    this._playersExtended.Add(zperson);
    zperson.host = true;
    zperson.yourTurn = true;
    this.serverState = new ServerState();
    this.gameFacts.connections.Add((Connection) tcpConnection);
    this.gameFacts.players.Add(zperson.name);
    this.InitUncontrolled();
  }

  public void InitUncontrolled()
  {
    this._uncontrolledPlayer.settingsPlayer = new SettingsPlayer();
    this._uncontrolledPlayer.game = this;
    this._uncontrolledPlayer.team = 24;
    this._playersExtended.Add(this._uncontrolledPlayer);
  }

  private void Awake()
  {
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
    if (this.electricityPool != null || Application.isBatchMode)
      return;
    this.electricityPool = new PoolElectricity();
    this.electricityPool.o = Inert.Instance.electricityPool;
  }

  public bool IsGameOver()
  {
    if (this.isTeam)
    {
      int num1 = 0;
      for (int i = 0; i < this.TEAM_COUNT; ++i)
      {
        foreach (ZPerson zperson in this.GetTeam(i))
        {
          if (zperson.controlled.Count > 0)
          {
            ++num1;
            break;
          }
        }
      }
      if (this.TEAM_COUNT == 1 && num1 == 1)
      {
        int num2 = 0;
        foreach (ZPerson player in this.players)
        {
          if (player.controlled.Count > 0)
          {
            ++num2;
            if (num2 >= 2)
              return false;
          }
        }
      }
      return num1 <= 1;
    }
    int num = 0;
    foreach (ZPerson player in this.players)
    {
      if (player.controlled.Count > 0)
      {
        ++num;
        if (num >= 2)
          return false;
      }
    }
    return true;
  }

  public void SendGameOver(bool force = false)
  {
    if (this.serverState.busy == ServerState.Busy.Ended && !force || this.sentGameOver)
      return;
    this.sentGameOver = true;
    Timing.KillCoroutines(this.serverUpdate);
    this.serverUpdate = (IEnumerator<float>) null;
    this.serverState.busy = ServerState.Busy.Ended;
    if (this.isClient)
    {
      if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
      {
        Player.Instance.Hide_All();
        Player.Instance.UnselectSpell();
      }
      if (SceneManager.GetActiveScene().name == "Game")
      {
        Debug.LogWarning((object) "Game Over in \"Game\" Scene");
      }
      else
      {
        if (!this.isSandbox)
          return;
        this.QuitToMainMenu();
      }
    }
    else
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 13);
          bool[] flagArray = new bool[this.TEAM_COUNT];
          if (this.isTeam)
          {
            for (int i = 0; i < this.TEAM_COUNT; ++i)
            {
              foreach (ZPerson zperson in this.GetTeam(i))
              {
                if (zperson.controlled.Count > 0 && zperson.controlled[0].health > 0 && !this.resigned.Contains(zperson))
                {
                  flagArray[i] = true;
                  break;
                }
              }
              foreach (ZPerson zperson in this.GetTeam(i))
                myBinaryWriter.Write(flagArray[i]);
            }
          }
          else
          {
            foreach (ZPerson player in this.players)
            {
              if (player.controlled.Count > 0 && player.controlled[0].health > 0 && !this.resigned.Contains(player))
                myBinaryWriter.Write(true);
              else
                myBinaryWriter.Write(false);
            }
          }
          StringBuilder msg = new StringBuilder("");
          if (this.isTeam)
          {
            int num = 0;
            for (int index = 0; index < this.TEAM_COUNT; ++index)
            {
              if (flagArray[index])
                ++num;
            }
            if (num > 1 || num == 0)
            {
              msg.Append("Draw\n\n");
            }
            else
            {
              for (int i = 0; i < this.TEAM_COUNT; ++i)
              {
                if (flagArray[i])
                {
                  foreach (ZPerson zperson in this.GetTeam(i))
                    zperson.winner = true;
                  switch (i)
                  {
                    case 0:
                      msg.Append("WINNER - Red Team\n\n");
                      goto label_66;
                    case 1:
                      msg.Append("WINNER - Blue Team\n\n");
                      goto label_66;
                    case 2:
                      msg.Append("WINNER - Green Team\n\n");
                      goto label_66;
                    case 3:
                      msg.Append("WINNER - Brown Team\n\n");
                      goto label_66;
                    case 4:
                      msg.Append("WINNER - Purple Team\n\n");
                      goto label_66;
                    case 5:
                      msg.Append("WINNER - Yellow Team\n\n");
                      goto label_66;
                    default:
                      goto label_66;
                  }
                }
              }
            }
          }
          else
          {
            bool flag = false;
            foreach (ZPerson player in this.players)
            {
              if (player.controlled.Count > 0 && player.controlled[0].health > 0 && !this.resigned.Contains(player))
              {
                player.winner = true;
                msg.Append("WINNER - ").Append(player.name);
                flag = true;
                break;
              }
            }
            if (!flag)
              msg.Append("Draw");
            msg.Append("\n\n");
          }
label_66:
          ZPerson.GetAwards(this, msg);
          myBinaryWriter.Write(msg.ToString());
          foreach (ZPerson player in this.players)
            myBinaryWriter.Write(player.gainedWands);
        }
        foreach (ZPerson player in this.players)
        {
          if (!player.isFake)
          {
            if (player.winner)
              Achievements.OnWin(this, player);
            else
              Achievements.OnLoss(this, player);
          }
        }
        this.SendAllMessage(memoryStream.ToArray());
      }
      foreach (ZPerson player in this.players)
      {
        if (!player.isFake)
          player.account.ToFile();
      }
      Timing.RunCoroutine(this.Cleanup());
    }
  }

  public void QuitToMainMenu(bool save = false, Client.JoinLocation exitToLobby = Client.JoinLocation.Mainmenu, bool forceSave = false)
  {
    this.resyncing = false;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      Time.timeScale = 1f;
      try
      {
        Controller.Instance.DestroyMap();
      }
      catch (Exception ex)
      {
        Debug.Log((object) ex);
      }
      if (this.isSandbox || this.isReplay)
      {
        AudioManager.PlayMusicMainMenu();
        switch (Client.joinedFrom)
        {
          case Client.JoinLocation.TestTutorial:
            if (PlayerPrefs.GetString(TutorialCodeEditor.prefOpened, "_Temporary.arcTutorial2").EndsWith("2"))
            {
              Controller.Instance.OpenMenu(Controller.Instance.MenuTutorialCodeEditor, false);
              break;
            }
            Controller.Instance.OpenMenu(Controller.Instance.MenuTutorialEditor, false);
            break;
          case Client.JoinLocation.Tutorial:
            Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
            MainMenu.OpenTutorialMenu();
            break;
          case Client.JoinLocation.Replay:
            Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
            Controller.Instance.OpenMenu(Controller.Instance.MenuReplay, false);
            break;
          default:
            if (exitToLobby == Client.JoinLocation.Game && this.isSandbox)
            {
              Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
              Client._gameFacts = new GameFacts();
              Client.joinedFrom = Client.JoinLocation.Mainmenu;
              Controller.Instance.InitMap(true);
              return;
            }
            if (Client.isConnected || Client.offlineMode)
            {
              Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
              break;
            }
            Client.ReConnectToServer();
            break;
        }
      }
      else
      {
        if (this.isSpectator)
          Spectator.Disconnect();
        if (this.isSpectator && Client.isConnected)
        {
          Controller.Instance.OpenMenu(Controller.Instance.MenuLobby, true);
        }
        else
        {
          if (Client.isConnected)
          {
            int busy = (int) this.serverState.busy;
          }
          HUD.instance?.SafeExit();
          Client.AskToJoinLobby();
        }
        if (save && Global.GetPrefBool("prefsavereplay", true) | forceSave)
          this.SaveReplay();
      }
      if ((UnityEngine.Object) KeyBindMenu.Instance != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) KeyBindMenu.Instance.gameObject);
      if (!((UnityEngine.Object) ScreenSizeMenu.Instance != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ScreenSizeMenu.Instance.gameObject);
    }));
  }

  public void RemoveResigned()
  {
    for (int index = this.resigned.Count - 1; index >= 0; --index)
    {
      ZPerson zperson = this.resigned[index];
      if (zperson.controlled.Count > 0 && (zperson == this.CurrentPlayer() && zperson.localTurn >= 0 || zperson.game.isMulti))
      {
        zperson.controlled[0].health = 0;
        zperson.controlled[0].OnDeath(true);
        this.resigned.RemoveAt(index);
      }
    }
  }

  public void OnEndTurn(ZPerson p)
  {
    p.curDamageDealtInOneAttack = 0;
    this.lastSpellCast = SpellEnum.None;
    this.sinksThisTurn = 0;
    this.playersKilledThisTurn = 0;
    this.playerKilledByArmageddon = false;
    this.portalUsedThisSpellTurn = false;
    this.skimmed_on_water = false;
    if ((UnityEngine.Object) p.panelPlayer != (UnityEngine.Object) null)
      p.panelPlayer.rect.anchoredPosition = new Vector2(0.0f, p.panelPlayer.rect.anchoredPosition.y);
    ZCreature zcreature = p.first();
    if (ZComponent.IsNull((ZComponent) zcreature) || zcreature.isDead || this.First_Turn_Teleport && p.localTurn < 0)
      return;
    p.awards.TurnEndedAt(zcreature.position);
    if (zcreature.inWater && (p.localTurn > 0 || !this.First_Turn_Teleport))
    {
      if (zcreature.health < 8)
      {
        zcreature.DoDamage(5 * p.waterMultipler);
        if (zcreature.health > 0)
          return;
        this.with_the_fishes = true;
        zcreature.OnDeath(true);
        return;
      }
      zcreature.health = (int) ((FixedInt) zcreature.health * ((FixedInt) 692060L / ((FixedInt) p.waterMultipler * (p.waterMultipler > 1 ? (FixedInt) 524288L : FixedInt.Create(1)))));
      zcreature.UpdateHealthTxt();
      p.SetGates(p.localTurn);
      ++p.waterMultipler;
      if (zcreature.health <= 5)
      {
        zcreature.health = 0;
        zcreature.UpdateHealthTxt();
        this.with_the_fishes = true;
        if (this.isClient && Global.GetPrefBool("prefdeathmsg", true))
          ChatBox.Instance?.NewChatMsg("", Descriptions.GetDrownMessage(zcreature), (Color) ColorScheme.GetColor(Global.ColorWhiteText), "", ChatOrigination.System);
        zcreature.OnDeath(true);
      }
    }
    else
      p.waterMultipler = 1;
    for (int index = p.controlled.Count - 1; index >= 0; --index)
    {
      if (index < p.controlled.Count)
      {
        if (p.controlled[index].tempFlight)
          p.controlled[index].RemoveFlight();
        p.controlled[index].sprinting = 0;
        if (p.controlled[index]._FourSeasonsCastAtEndOfTurn)
        {
          p.controlled[index]._FourSeasonsCastAtEndOfTurn = false;
          ZSpell.FireSeasonal(Inert.Instance.Seasonal, p.controlled[index], p.controlled[index]._FourSeasonsLocation, false);
        }
      }
    }
    if (p.inactiveTurns < 3 || !this.isServer || this.isClient)
      return;
    this.Resign(p, false);
  }

  public void Resign(ZPerson p, bool force)
  {
    if (p == null || !(!this.isRated | force) || p.controlled.Count <= 0 || this.resigned.Contains(p) || p.didResign)
      return;
    this.SendVisualResigned(p, false);
    p.didResign = true;
    if (this.isMulti)
    {
      foreach (ZPerson zperson in this.GetTeam(p.team))
        this.tempResignFix.Add(zperson);
    }
    else
      this.tempResignFix.Add(p);
    if (!p.yourTurn)
      return;
    this.MoveQue.Enqueue((Action) (() =>
    {
      if (p == null || !p.yourTurn)
        return;
      this.serverState.busy = ServerState.Busy.Between_Turns;
      this.serverState.turnTime = this.PlayersMaxTurnTime + 3f;
    }));
  }

  private void SendVisualResigned(ZPerson p, bool left)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 191);
        myBinaryWriter.Write(p.id);
        myBinaryWriter.Write(left);
      }
      this.SendAllMessage(memoryStream.ToArray());
    }
  }

  private void SendResigned(ZPerson p)
  {
    if (p == null || p.controlled.Count == 0 || this.resigned.Contains(p))
      return;
    this.resigned.Add(p);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write(p.didLeave ? (byte) 193 : (byte) 195);
        myBinaryWriter.Write(p.id);
      }
      this.SendAllMessage(memoryStream.ToArray());
    }
  }

  private void SendLeft(ZPerson p)
  {
    if (p == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 193);
        myBinaryWriter.Write(p.id);
      }
      this.SendAllMessage(memoryStream.ToArray());
    }
  }

  internal void SendRejoined(ZPerson p)
  {
    if (p == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 7);
        myBinaryWriter.Write(p.id);
      }
      this.SendAllMessage(memoryStream.ToArray());
      this.timeline.Add(memoryStream.ToArray());
    }
  }

  public void NextSeason()
  {
    ++this.currentSeason;
    if (this.currentSeason > GameSeason.Spring)
      this.currentSeason = GameSeason.Summer;
    if (!this.isClient)
      return;
    HUD.instance?.UpdateSeasonIcon(this.currentSeason);
  }

  public void NextTurn()
  {
    if (this.isOver)
      return;
    int playersTurn = (int) this.serverState.playersTurn;
    this.with_the_fishes = false;
    ++this.totalTurnsCombined;
    if (this.MoveQue.Count > 0 || (int) this.serverState.playersTurn < this.players.Count && this.players[(int) this.serverState.playersTurn].didResign)
    {
      if (!this.isClient && (int) this.serverState.playersTurn < this.players.Count && (ZComponent) this.players[(int) this.serverState.playersTurn].first() != (object) null)
      {
        if (this.players[(int) this.serverState.playersTurn].didResign)
          this.SendResigned(this.players[(int) this.serverState.playersTurn]);
        else
          this.SendResyncMsg(this.players[(int) this.serverState.playersTurn], "move queue was not empty when turn ended");
      }
      this.MoveQue.Clear();
    }
    if (this.tempResignFix.Count > 0)
    {
      foreach (ZPerson p in this.tempResignFix)
        this.SendResigned(p);
      this.tempResignFix.Clear();
    }
    if (this.resigned.Count > 0)
      this.RemoveResigned();
    this.serverState.busy = ServerState.Busy.Starting_Turn;
    this.serverState.turnTime = 0.0f;
    if (this.firstTurn && (int) this.serverState.playersTurn < this.players.Count && !this.isClient)
    {
      this.firstTurn = false;
      this.SendNextTurn(playersTurn);
      this.SendGameTime();
      this.NextTurn(this.players[(int) this.serverState.playersTurn]);
    }
    else
    {
      if (this.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
      {
        Player.Instance.UnselectSpell();
        Player.Instance.selectedCreatureIndex = 0;
        Player.Instance.selectedCreaturePlayerOffset = (int) Player.Instance.person.id;
        if (Player.Instance.person.controlled.Count > 0)
          Player.Instance.selected = Player.Instance.person.controlled[0];
      }
      if ((int) this.serverState.playersTurn < this.players.Count)
      {
        this.OnEndTurn(this.players[(int) this.serverState.playersTurn]);
        this.players[(int) this.serverState.playersTurn].yourTurn = false;
      }
      int num = 0;
      if (this.isTeam)
      {
        bool flag = false;
        int lastTeamsTurn = this.lastTeamsTurn;
        do
        {
          ++this.lastTeamsTurn;
          if (this.lastTeamsTurn >= this.TEAM_COUNT)
            this.lastTeamsTurn = 0;
          if (lastTeamsTurn == this.lastTeamsTurn && this.team2Players.Count > 0)
          {
            this.SendGameOver();
            goto label_46;
          }
          else
          {
            int numberPlayersPerTeam = this.gameFacts.GetNumberPlayersPerTeam();
            for (int index = 0; index < numberPlayersPerTeam; ++index)
            {
              ++this.teamIndex[this.lastTeamsTurn];
              if (this.teamIndex[this.lastTeamsTurn] >= numberPlayersPerTeam)
                this.teamIndex[this.lastTeamsTurn] = 0;
              this.serverState.playersTurn = (byte) (this.teamIndex[this.lastTeamsTurn] + this.lastTeamsTurn * numberPlayersPerTeam);
              if (this.players[(int) this.serverState.playersTurn].controlled.Count > 0)
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              ++num;
            else
              goto label_46;
          }
        }
        while (num <= 25);
        this.SendGameOver();
      }
      else
      {
        do
        {
          ++this.serverState.playersTurn;
          if ((int) this.serverState.playersTurn >= this.players.Count)
            this.serverState.playersTurn = (byte) 0;
          ++num;
          if (num > 25)
          {
            this.SendGameOver();
            break;
          }
        }
        while (this.players[(int) this.serverState.playersTurn].controlled.Count == 0 || this.players[(int) this.serverState.playersTurn].ai != null && this.players[(int) this.serverState.playersTurn].ai.GetType() == typeof (EMPTY_AI) && !((EMPTY_AI) this.players[(int) this.serverState.playersTurn].ai).stillDoTurn);
      }
label_46:
      if (this.isOver)
        return;
      this.SendNextTurn(playersTurn);
      this.SendGameTime();
      if (this.players[(int) this.serverState.playersTurn].ai != null)
        this.players[(int) this.serverState.playersTurn].ai.DoTurn();
      this.NextTurn(this.players[(int) this.serverState.playersTurn]);
    }
  }

  public void NextTurn(ZPerson p, bool first = true)
  {
    if (p.localTurn > this._nextturn)
    {
      this._nextturn = p.localTurn;
      if (this.isWindy)
        this.map.RandomizeWind();
    }
    for (int index = 0; index < this.players.Count; ++index)
      this.players[index].yourTurn = false;
    if (first && this._uncontrolledPlayer.controlled.Count > 0)
    {
      for (int index = this._uncontrolledPlayer.controlled.Count - 1; index >= 0; --index)
      {
        if (ZComponent.IsNull((ZComponent) this._uncontrolledPlayer.controlled[index]) || this._uncontrolledPlayer.controlled[index].isDead)
          this._uncontrolledPlayer.controlled.RemoveAt(index);
        else if (this._uncontrolledPlayer.controlled[index].stunned)
        {
          if (this._uncontrolledPlayer.controlled[index].superStunned)
            this._uncontrolledPlayer.controlled[index].superStunned = false;
          else
            this._uncontrolledPlayer.controlled[index].stunned = false;
        }
      }
    }
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
      Player.Instance.skippingTurn = false;
    p.ResetMoveID();
    if (p.communeWithNature > 0)
      --p.communeWithNature;
    if (this.decreasePlayer2Cooldowns)
    {
      if (this.players.Count != 2 || this.ArcaneZero)
        this.decreasePlayer2Cooldowns = false;
      else if (this.turn == -1)
      {
        this.decreasePlayer2Cooldowns = false;
        ZCreature zcreature = p.first();
        if ((ZComponent) zcreature != (object) null && !zcreature.isDead)
        {
          foreach (SpellSlot spell in zcreature.spells)
          {
            if (spell.TurnsTillFirstUse > 0)
              ++spell.TurnsTillFirstUse;
          }
        }
      }
    }
    ++p.inactiveTurns;
    p.movedThisTurn = false;
    this.PlayersMaxTurnTime = p.GetLevel(BookOf.Cogs) > 0 ? (float) this.MaxTurnTime + (float) p.GetLevel(BookOf.Cogs) * Mathf.Min(this.originalSpellsOnly ? 6f : (float) this.MaxTurnTime / 5f, 6f) : (float) this.MaxTurnTime;
    if (this.isCountdown)
    {
      if ((double) p.countdown > 0.0)
        this.PlayersMaxTurnTime = Mathf.Max(5f, Mathf.Min(this.PlayersMaxTurnTime, p.countdown + (float) this.gameFacts.settings.countdownDelay));
      else if (!this.countdownLose)
        this.PlayersMaxTurnTime = 5f;
    }
    ++p.localTurn;
    p.yourTurn = true;
    p.spellsCastTHISTurn = 0;
    Inert.Instance.waterGate.SetTurnFired = -1;
    if (p.hasCastTheFourSeasons && this.seasonTurn < p.localTurn)
    {
      this.seasonTurn = p.localTurn;
      this.NextSeason();
    }
    if (this.isClient && !this.resyncing && HUD.instance.FollowSpells)
      CameraMovement.FOLLOWTARGETS = true;
    for (int index = this.globalEffectors.Count - 1; index >= 0; --index)
    {
      if (ZComponent.IsNull((ZComponent) this.globalEffectors[index]) || this.globalEffectors[index].dead || ZComponent.IsNull((ZComponent) this.globalEffectors[index].whoSummoned) && p.controlled.Count > 0 && this.globalEffectors[index].KillsHost())
        this.globalEffectors.RemoveAt(index);
      else if (this.globalEffectors[index].type == EffectorType.Portal)
        this.globalEffectors[index].active = false;
    }
    for (int index = this.windShieldEffectors.Count - 1; index >= 0; --index)
    {
      if (ZComponent.IsNull((ZComponent) this.windShieldEffectors[index]))
        this.windShieldEffectors.RemoveAt(index);
      else
        this.windShieldEffectors[index].SetWindShieldActive();
    }
    if (p.controlled.Count > 0 && (ZComponent) p.controlled[0] != (object) null && !p.controlled[0].isDead)
    {
      p.awards.TurnStartedAt(p.controlled[0].position);
      if (!ZComponent.IsNull((ZComponent) p.controlled[0].stormShield))
        p.controlled[0].CreatureMoveSurroundings();
    }
    this.tempList.Clear();
    foreach (ZCreature z in p.controlled)
    {
      if (!ZComponent.IsNull((ZComponent) z) && !z.isDead)
        this.tempList.Add(z);
    }
    for (int index1 = 0; index1 < this.tempList.Count; ++index1)
    {
      if (!ZComponent.IsNull((ZComponent) this.tempList[index1]) && !this.tempList[index1].isDead)
      {
        ZCreature temp = this.tempList[index1];
        if (temp.invulnerable > 0)
          --temp.invulnerable;
        if (temp.superStunned)
          temp.superStunned = false;
        else
          temp.stunned = false;
        if ((ZComponent) temp.tower != (object) null)
          temp.tower.NextTurn();
        if (temp.shiningPower && temp.health < temp.maxHealth && !temp.inWater)
        {
          temp.DoHeal(10);
          if (temp.health > temp.maxHealth)
            temp.health = temp.maxHealth;
        }
        if (temp.type == CreatureType.Gargoyle && !temp.canMove)
        {
          if (temp.maxHealth < 100)
            temp.maxHealth += 25;
          temp.DoHeal(25, DamageType.Heal);
          if (temp.health > temp.maxHealth)
            temp.health = temp.maxHealth;
        }
        if (temp.type == CreatureType.Dragon && temp.race == CreatureRace.Arcane && !temp.FullArcane)
        {
          temp.DoHeal(25);
          if (temp.health > temp.maxHealth)
            temp.health = temp.maxHealth;
        }
        else if (temp.type == CreatureType.DragonJr)
        {
          temp.DoHeal(15);
          if (temp.health > temp.maxHealth)
            temp.health = temp.maxHealth;
        }
        if (temp.fusion > 0)
        {
          if (temp.fusion >= temp.parent.localTurn)
          {
            temp.DoHeal(10);
          }
          else
          {
            temp.fusion = 0;
            temp.UpdateHealthTxt();
          }
        }
        if (!ZComponent.IsNull((ZComponent) this.dwarfMapEffector) && temp.minerMarket != null && !temp.isMoving)
        {
          Spell spell = Inert.GetSpell(SpellEnum.Mine);
          ZSpell.FireMine(spell, temp, this.dwarfMapEffector.position, -(spell.maxDuration / 2));
        }
        if (temp.familiar.Has(FamiliarType.OverLight) && temp.familiarLevelOverlight > 0 && temp.shield < 50)
        {
          if (ZEffector.InSanctuary(temp.game.world, temp.position))
          {
            ZEffector.SpawnVineExplosion(temp.transform.position);
          }
          else
          {
            temp.CreateProtectionShield();
            temp.shield += temp.familiarLevelOverlight;
          }
        }
        if (temp.race == CreatureRace.Undead && temp.isPawn && (temp.type != CreatureType.Gargoyle || temp.canMove))
        {
          temp.DoDamage(5);
          if (temp.health <= 0)
            temp.health = 1;
        }
        else if (temp.type == CreatureType.Vampire && temp.health > 1)
        {
          Coords start = new Coords((int) temp.position.x, (int) temp.position.y);
          Coords end = new Coords((int) temp.position.x, temp.map.Height);
          Coords coords = this.map.bresenhamsLineCast(start, end, temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x - 100, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x + 100, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x - 25, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x + 25, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x - 50, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x + 50, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x - 75, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x + 75, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x - 125, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x + 125, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x - 150, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords != null)
            coords = this.map.bresenhamsLineCast(start, new Coords((int) temp.position.x + 150, temp.map.Height), temp, (ZSpell) null, Inert.mask_entity_movement);
          if (coords == null)
          {
            temp.DoDamage(Mathf.Min(temp.health - 1, 10));
            if (temp.health <= 0)
              temp.health = 1;
          }
        }
        else if (temp.type == CreatureType.Beehive)
        {
          temp.DoHeal(15);
          if (temp.health > temp.maxHealth)
            temp.health = temp.maxHealth;
          temp.OnNextTurn();
        }
        for (int index2 = temp.effectors.Count - 1; index2 >= 0; --index2)
        {
          if (ZComponent.IsNull((ZComponent) temp.effectors[index2]) || temp.effectors[index2].dead)
            temp.effectors.RemoveAt(index2);
          else
            temp.effectors[index2].TurnPassed(index2, false, false);
        }
        for (int index3 = temp.destroyableEffectors.Count - 1; index3 >= 0; --index3)
        {
          if (ZComponent.IsNull((ZComponent) temp.destroyableEffectors[index3]))
            temp.destroyableEffectors.RemoveAt(index3);
          else
            temp.destroyableEffectors[index3].TurnPassed(index3, true, false);
        }
        if (temp.halfHealing > 0)
          --temp.halfHealing;
        if (temp.bleeding && temp.health > 1)
        {
          --temp.bleedCounter;
          temp.DoDamage(Mathf.Min(temp.health - 1, 10));
          ZSpell.RandomBlood(temp.game, temp.position);
        }
        if ((ZComponent) temp != (object) null && !temp.isDead)
          temp.UpdateHealthTxt();
        if (temp.appliedGravity > 0 && !temp.gravitionalPull)
          temp.RemoveGravity();
      }
    }
    for (int index = this.globalEffectors.Count - 1; index >= 0; --index)
    {
      if (ZComponent.IsNull((ZComponent) this.globalEffectors[index]) || this.globalEffectors[index].dead || ZComponent.IsNull((ZComponent) this.globalEffectors[index].whoSummoned) && p.controlled.Count > 0 && this.globalEffectors[index].KillsHost())
      {
        this.globalEffectors.RemoveAt(index);
      }
      else
      {
        if (ZComponent.IsNull((ZComponent) this.globalEffectors[index].whoSummoned) && (ZComponent) p.first() != (object) null)
          this.globalEffectors[index].whoSummoned = p.controlled[0];
        this.globalEffectors[index].TurnPassed(index, false, true);
      }
    }
    ++this.turn;
    if (this.ArcaneZero && (p.localTurn > this.TurnToLoseArcaneZero || this.turn == 1 && this.players.Count == 2))
      this.ArcaneZero = false;
    if (this.gameFacts.settings.customArmageddon != null)
      Armageddon.CustomArmageddon(p);
    else
      Armageddon.NextTurn(p);
    if (this.isClient & first)
    {
      HUD.instance.NextTurn(p);
      if (p.controlled.Count > 0 && (ZComponent) p.controlled[0] != (object) null && !p.controlled[0].isDead)
        CameraMovement.Instance.LerpToTransform(p.controlled[0]);
    }
    else
      this.CheckDraw(p);
    if (!this.isSandbox && !this.isTutorial && p.localTurn > 0)
      this.AllowMovement = !this.gameFacts.GetStyle().HasStyle(GameStyle.No_Movement);
    if ((UnityEngine.Object) p.panelPlayer != (UnityEngine.Object) null)
      p.panelPlayer.rect.anchoredPosition = new Vector2(50f, p.panelPlayer.rect.anchoredPosition.y);
    if (this.resigned.Count <= 0)
      return;
    this.RemoveResigned();
  }

  public void CreatureMoveSurroundings(
    MyLocation pos,
    int radius,
    ZMyCollider col = null,
    bool forceLeaf = false)
  {
    List<ZMyCollider> zmyColliderList = this.world.OverlapCircleAll((Point) pos, radius + 15, col);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
      {
        ZCreature creature = zmyColliderList[index].creature;
        if ((ZComponent) creature != (object) null && (ZComponent) creature.mount == (object) null)
        {
          if (!creature.isMoving && creature.ShouldFall())
            creature.Fall();
        }
        else if ((ZComponent) zmyColliderList[index].spell != (object) null && !zmyColliderList[index].spell.isMoving && zmyColliderList[index].spell.ShouldSpellFall())
          zmyColliderList[index].spell.SpellFall();
      }
      else if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZTower tower = zmyColliderList[index].tower;
        if ((ZComponent) tower != (object) null && !tower.creature.isMoving)
          tower.ShouldFall();
      }
      else
      {
        ZSpell spell = zmyColliderList[index].spell;
        if (!ZComponent.IsNull((ZComponent) spell))
        {
          if (!spell.isMoving && !spell.isDead)
          {
            if (forceLeaf && spell.spellLogic == SpellLogic.Leaf)
              spell.Fall();
            else if (spell.ShouldSpellFall())
              spell.SpellFall();
          }
        }
        else
        {
          ZCreature creature = zmyColliderList[index].creature;
          if (!ZComponent.IsNull((ZComponent) creature) && creature.ShouldFall())
            creature.Fall();
        }
      }
    }
  }

  public IEnumerator<float> castWait(float time)
  {
    for (int i = 0; (double) i < (double) time; ++i)
      yield return 0.0f;
  }

  public IEnumerator<float> castWaitOnDeath(float time)
  {
    for (int i = 0; (double) i < (double) time; ++i)
      yield return 0.0f;
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && ((ZComponent) Player.Instance.selected == (object) null || Player.Instance.selected.isDead))
    {
      if ((ZComponent) Player.Instance.person.first() != (object) null && Player.Instance.person.first().inWater)
        Player.Instance.NextRecallDevice(true);
      else
        Player.Instance.NextControlled(true);
    }
  }

  public void DelayKill()
  {
    if (this.ieKillWait != null || this.serverState.busy == ServerState.Busy.Ended)
      return;
    this.ieKillWait = this.ongoing.RunSpell(this.waitToQuit());
  }

  private IEnumerator<float> waitToQuit()
  {
    if (this.serverState.busy != ServerState.Busy.Ended)
    {
      this.serverState.busy = ServerState.Busy.Ended;
      yield return 0.0f;
      while (this.ongoing.NumberOfSlowUpdateCoroutines > 1)
        yield return 0.0f;
      this.ieKillWait = (IEnumerator<float>) null;
      this.SendGameOver(true);
    }
  }

  private void SortByBidFFA()
  {
    this.players.Sort((Comparison<ZPerson>) ((a, b) => b.bid - a.bid));
    byte num = 0;
    foreach (ZPerson player in this.players)
    {
      int id = (int) player.id;
      player.id = num;
      ++num;
      if (player.bid > 0 && (ZComponent) player.first() != (object) null)
        player.first().health -= player.bid;
      player.first()?.UpdateHealthTxt();
      if (this.isClient && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
      {
        HUD.instance.AdjustPlayersPanel(player);
        HUD.instance.uiPlayerCharacters[id].myID = (int) player.id;
      }
    }
    if (this.isClient && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
      HUD.instance.uiPlayerCharacters.Sort((Comparison<UIPlayerCharacter>) ((a, b) => a.myID - b.myID));
    if (!this.isClient || !((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || Player.Instance.person == null)
      return;
    Client.curGameID = (int) Player.Instance.person.id;
  }

  private bool CheckBanStage(BanStage s)
  {
    for (int index = 0; index < this.players.Count; ++index)
    {
      if (this.players[index].bidStage == s)
        return false;
    }
    return true;
  }

  private void HandleBanTime()
  {
    this.serverState.turnTime -= this.DeltaTime;
    if (this.isServer)
      this.SendGameTime();
    if (!this.isClient || !((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null))
      return;
    HUD.instance.time_txt.text = ((int) this.serverState.turnTime).ToString();
  }

  private void HandleClientOutOfBanTime()
  {
    if (!this.isClient || !((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null))
      return;
    this.serverState.turnTime = 0.0f;
    HUD.instance.time_txt.text = "0";
  }

  private IEnumerator<float> BanStagesUpdate()
  {
    this.serverState.turnTime = 60f;
    while ((double) this.serverState.turnTime > 0.0 || !this.isServer)
    {
      int num = this.CheckBanStage(BanStage.Stage0) ? 1 : 0;
      this.HandleBanTime();
      if (num != 0)
      {
        this.HandleClientOutOfBanTime();
        break;
      }
      yield return 0.0f;
    }
    if (this.isServer)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 192);
          myBinaryWriter.Write(0);
          int num = -1;
          for (int index = 0; index < this.players.Count; ++index)
          {
            if (this.players[index].bid > num)
              num = this.players[index].bid;
            myBinaryWriter.Write(this.players[index].bid > -1 ? this.players[index].bid : 0);
          }
        }
        this.SendAllMessage(memoryStream.ToArray());
      }
    }
    if (this.isServer)
      this.serverUpdate = Timing.RunCoroutine(this.FixedUpdate());
  }

  private IEnumerator<float> BidUpdate()
  {
    ZGame zgame = this;
    zgame.serverState.turnTime = 35f;
    bool allbid = true;
    while ((double) zgame.serverState.turnTime > 0.0 || !zgame.isServer)
    {
      zgame.serverState.turnTime -= zgame.DeltaTime;
      allbid = true;
      for (int index = 0; index < zgame.players.Count; ++index)
      {
        if (zgame.players[index].bid == -1)
          allbid = false;
      }
      if (zgame.isServer)
        zgame.SendGameTime();
      if (zgame.isClient && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
        HUD.instance.time_txt.text = ((int) zgame.serverState.turnTime).ToString();
      if (allbid)
      {
        if (zgame.isClient && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
        {
          zgame.serverState.turnTime = 0.0f;
          HUD.instance.time_txt.text = "0";
          break;
        }
        break;
      }
      yield return 0.0f;
    }
    if (!zgame.isServer)
    {
      zgame.serverUpdate = Timing.RunCoroutine(zgame.ClientUpdate());
      if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
        HUD.instance.timeHeader.text = "Turn:";
      HUD.instance?.BidPanel.SetActive(false);
    }
    else
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 192);
          myBinaryWriter.Write((byte) 0);
          int highestBid = -1;
          for (int index = 0; index < zgame.players.Count; ++index)
          {
            if (zgame.players[index].bid > highestBid)
              highestBid = zgame.players[index].bid;
            myBinaryWriter.Write(zgame.players[index].bid > -1 ? zgame.players[index].bid : 0);
          }
          if (!zgame.isTeam && zgame.players.Count > 2)
            zgame.SortByBidFFA();
          List<ZPerson> list = zgame.players.Where<ZPerson>((Func<ZPerson, int, bool>) ((p, v) => p.bid == highestBid)).ToList<ZPerson>();
          if (list.Count > 0)
          {
            int index1 = Server.random.Next(0, list.Count);
            zgame.serverState.playersTurn = list[index1].id;
            myBinaryWriter.Write((int) zgame.serverState.playersTurn);
            if ((zgame.isTeam || zgame.players.Count < 3) && (ZComponent) zgame.players[(int) zgame.serverState.playersTurn].first() != (object) null)
              zgame.players[(int) zgame.serverState.playersTurn].first().health -= zgame.players[(int) zgame.serverState.playersTurn].bid;
            if (zgame.isTeam)
            {
              try
              {
                // ISSUE: reference to a compiler-generated method
                int index2 = zgame.GetTeam(zgame.players[(int) zgame.serverState.playersTurn].team).FindIndex(new Predicate<ZPerson>(zgame.\u003CBidUpdate\u003Eb__170_1));
                if (index2 > 0)
                {
                  for (int index3 = 0; index3 < zgame.teamIndex.Length; ++index3)
                    zgame.teamIndex[index3] = index2;
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
          else
            myBinaryWriter.Write(-1);
        }
        zgame.SendAllMessage(memoryStream.ToArray());
      }
      if (allbid && zgame.isServer)
      {
        for (zgame.serverState.turnTime = Mathf.Min(zgame.serverState.turnTime, 2f); (double) zgame.serverState.turnTime > 0.0; zgame.serverState.turnTime -= zgame.DeltaTime)
          yield return 0.0f;
      }
      zgame.serverUpdate = Timing.RunCoroutine(zgame.FixedUpdate());
    }
  }

  private IEnumerator<float> ClientUpdate()
  {
    while (true)
    {
      while (!this.resyncing)
      {
        this.tickTime += this.DeltaTime;
        if ((double) this.tickTime >= (double) this.frameTime)
        {
          this.tickTime -= this.frameTime;
          this.world?.listPool.Clear();
          SandPool.Instance?.Reset();
          this.ongoing.ServerUpdate();
        }
        if (this.MoveQue.Count > 0 && this.ongoing.NumberOfSlowUpdateCoroutines == 0)
        {
          this.MoveQue.Dequeue()();
          if (this.MoveQue.Count > 5 && this.ongoing.NumberOfSlowUpdateCoroutines == 0 && this.serverState.busy == ServerState.Busy.No)
            this.MoveQue.Dequeue()();
        }
        if (this.serverState.busy == ServerState.Busy.Starting_Turn)
        {
          if (this.ongoing.NumberOfSlowUpdateCoroutines == 0)
          {
            this.serverState.busy = ServerState.Busy.No;
            if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.myTurn == (int) this.serverState.playersTurn)
              Player.Instance.Show_All();
          }
        }
        else if (this.serverState.busy == ServerState.Busy.Moving)
        {
          if (!this.ongoing.isRunningSpell)
            this.serverState.turnTime += this.DeltaTime;
          if (this.isCountdown && (double) this.serverState.turnTime < (double) this.PlayersMaxTurnTime)
            this.CurrentPlayer()?.UpdateCountdown();
          if ((double) this.serverState.turnTime >= (double) this.PlayersMaxTurnTime)
            this.serverState.busy = ServerState.Busy.Between_Turns;
          else if (this.ongoing.NumberOfSlowUpdateCoroutines == 0)
          {
            if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.myTurn == (int) this.serverState.playersTurn)
              Player.Instance.Show_All();
            this.serverState.busy = ServerState.Busy.No;
          }
        }
        else if (this.serverState.busy == ServerState.Busy.Moving_NoCountdown)
        {
          if (this.ongoing.NumberOfSlowUpdateCoroutines == 0)
          {
            if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.myTurn == (int) this.serverState.playersTurn)
              Player.Instance.Show_All();
            this.serverState.busy = ServerState.Busy.No;
          }
        }
        else if (this.serverState.busy == ServerState.Busy.No || this.serverState.busy == ServerState.Busy.Waiting_For_Server_Reply)
        {
          if (!this.ongoing.isRunningSpell)
            this.serverState.turnTime += this.DeltaTime;
          if (this.isCountdown && (double) this.serverState.turnTime < (double) this.PlayersMaxTurnTime)
            this.CurrentPlayer()?.UpdateCountdown();
          if ((double) this.serverState.turnTime >= (double) this.PlayersMaxTurnTime)
            this.serverState.busy = ServerState.Busy.Between_Turns;
        }
        else if (this.serverState.busy == ServerState.Busy.Between_Turns)
        {
          if (!this.ongoing.isRunningSpell)
            this.serverState.turnTime += this.DeltaTime;
          if (this.ongoing.NumberOfSlowUpdateCoroutines == 0 && this.players[(int) this.serverState.playersTurn].InWater() && (double) this.serverState.turnTime < (double) this.PlayersMaxTurnTime + (this.isClient ? 0.0 : 2.0) && !this.resigned.Contains(this.players[(int) this.serverState.playersTurn]))
          {
            this.players[(int) this.serverState.playersTurn].yourTurn = true;
            this.serverState.busy = ServerState.Busy.No;
          }
        }
        if (this.shouldUpdateTurnTime != (int) this.serverState.turnTime)
        {
          this.shouldUpdateTurnTime = (int) this.serverState.turnTime;
          this.SendGameTime();
        }
        yield return 0.0f;
      }
      yield return 0.0f;
    }
  }

  private bool isReadyToStart()
  {
    if (!this.MAPCREATED)
      return false;
    foreach (ZPerson player in this.players)
    {
      if (player == player.connection.player.player && (!player.Connected || !player.ready) && player.controlled.Count > 0 && !player.didResign && !player.isFake)
        return false;
    }
    return true;
  }

  public void SendStatus(byte status)
  {
    this.gameFacts.status = status;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 30);
        myBinaryWriter.Write(this.gameFacts.id);
        myBinaryWriter.Write(status);
      }
      Server.SendLobbyUnrated(memoryStream.ToArray());
    }
  }

  public IEnumerator<float> FixedUpdate()
  {
    this.nextKeepAlive = Time.realtimeSinceStartup + 60f;
    while (true)
    {
      while (this.serverState.busy == ServerState.Busy.Not_started)
      {
        this.timeTillStart += this.DeltaTime;
        if ((double) this.timeTillStart > (double) this.waitTime || this.isClient && this.players[0].controlled.Count > 0 || this.isReadyToStart())
        {
          this.SendStatus((byte) 2);
          this.serverState.busy = ServerState.Busy.Between_Turns;
          if ((int) this.serverState.playersTurn < this.players.Count && this.players[(int) this.serverState.playersTurn].didResign)
          {
            this.NextTurn();
            break;
          }
          break;
        }
        yield return 0.0f;
        this.nextKeepAlive = Time.realtimeSinceStartup + 60f;
      }
      this.tickTime += this.DeltaTime;
      if ((double) this.tickTime >= (double) this.frameTime)
      {
        this.tickTime -= this.frameTime;
        this.world?.listPool.Clear();
        SandPool.Instance?.Reset();
        ++this.currentFrame;
        if (this.isTutorial && Client._tutorial.onFrame != null)
          Client._tutorial.onFrame(this.currentFrame);
        this.ongoing.ServerUpdate();
      }
      while (this.tutorialPaused)
        yield return 0.0f;
      if (this.serverState.busy == ServerState.Busy.Between_Turns)
      {
        if (!this.ongoing.isRunningSpell)
          this.serverState.turnTime += this.DeltaTime;
        if (this.ongoing.NumberOfSlowUpdateCoroutines == 0)
        {
          if ((int) this.serverState.playersTurn < this.players.Count && this.players[(int) this.serverState.playersTurn].InWater() && (double) this.serverState.turnTime < (double) this.PlayersMaxTurnTime + (this.isClient ? 0.0 : 2.0) && (this.players[(int) this.serverState.playersTurn].localTurn > -1 || !this.First_Turn_Teleport) && !this.resigned.Contains(this.players[(int) this.serverState.playersTurn]))
          {
            this.players[(int) this.serverState.playersTurn].yourTurn = true;
            this.serverState.busy = ServerState.Busy.No;
          }
          else
            this.NextTurn();
        }
      }
      else if (this.serverState.busy == ServerState.Busy.Moving)
      {
        if (!this.ongoing.isRunningSpell)
          this.serverState.turnTime += !this.isSandbox || this.sandboxTime != 0 ? this.DeltaTime : -this.DeltaTime;
        if (this.isCountdown && (double) this.serverState.turnTime < (double) this.PlayersMaxTurnTime)
          this.CurrentPlayer()?.UpdateCountdown();
        if ((double) this.serverState.turnTime >= (double) this.PlayersMaxTurnTime + (this.isClient ? 0.0 : 3.0))
          this.serverState.busy = ServerState.Busy.Between_Turns;
        else if (this.ongoing.NumberOfSlowUpdateCoroutines == 0)
        {
          if (this.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.myTurn == (int) this.serverState.playersTurn)
            Player.Instance.Show_All();
          this.serverState.busy = ServerState.Busy.No;
        }
      }
      else if (this.serverState.busy == ServerState.Busy.Moving_NoCountdown)
      {
        if (this.ongoing.NumberOfSlowUpdateCoroutines == 0)
        {
          if (this.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.myTurn == (int) this.serverState.playersTurn)
            Player.Instance.Show_All();
          this.serverState.busy = ServerState.Busy.No;
        }
      }
      else if (this.serverState.busy == ServerState.Busy.No || this.serverState.busy == ServerState.Busy.Waiting_For_Server_Reply)
      {
        if (!this.ongoing.isRunningSpell)
          this.serverState.turnTime += !this.isSandbox || this.sandboxTime != 0 ? this.DeltaTime : -this.DeltaTime;
        if (this.isCountdown && (double) this.serverState.turnTime < (double) this.PlayersMaxTurnTime + 0.20000000298023224)
          this.CurrentPlayer()?.UpdateCountdown();
        if ((double) this.serverState.turnTime >= (double) this.PlayersMaxTurnTime + (this.isClient ? 0.0 : 3.0))
          this.NextTurn();
        else if (this.MoveQue.Count > 0 && this.ongoing.NumberOfSlowUpdateCoroutines == 0)
          this.MoveQue.Dequeue()();
      }
      else if (this.serverState.busy == ServerState.Busy.Starting_Turn && this.ongoing.NumberOfSlowUpdateCoroutines == 0)
      {
        if (!this.isClient)
        {
          this.SendByte((byte) 199);
          this.serverState.busy = ServerState.Busy.No;
        }
        else
          this.serverState.busy = ServerState.Busy.No;
      }
      if (this.shouldUpdateTurnTime != (int) this.serverState.turnTime)
      {
        this.shouldUpdateTurnTime = (int) this.serverState.turnTime;
        this.SendGameTime();
      }
      yield return 0.0f;
    }
  }

  public void CreateFamiliar(BookOf b00k, ZPerson p, bool CREATE_FAMILIAR = true)
  {
    if (this.isClient & CREATE_FAMILIAR)
    {
      if (b00k == BookOf.Seasons)
        AudioManager.Play(AudioManager.instance.clipHarmony[Mathf.Clamp(p.GetLevel(b00k) - 1, 0, 4)]);
      else
        AudioManager.Play(AudioManager.instance.clipChargeFamiliar[(int) b00k]);
    }
    switch (b00k)
    {
      case BookOf.Seasons:
        ZCreature zcreature1 = p.first();
        if (p.seasonISHoliday)
        {
          if (p.GetLevel(b00k) == 1)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Gift of Giving")), b00k);
          }
          else
          {
            foreach (SpellSlot spell in p.first().spells)
            {
              if (spell.spell.spellEnum == SpellEnum.Gift_of_Giving)
              {
                ++spell.MaxUses;
                break;
              }
            }
          }
          for (int index = 0; index < zcreature1.spells.Count; ++index)
          {
            if (zcreature1.spells[index].MaxUses > 0 && zcreature1.spells[index].spell.bookOf == BookOf.Seasons && zcreature1.spells[index].spell.level != 3)
              ++zcreature1.spells[index].MaxUses;
          }
          zcreature1.SetScale(1f);
          if (this.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && p == Player.Instance.person)
            Player.Instance.UnselectSpell();
          FixedInt fixedInt = (FixedInt) p.GetLevel(BookOf.Seasons) * (FixedInt) 78643L + 1;
          float num = fixedInt.ToFloat();
          if (!zcreature1.FullArcane)
            zcreature1.massMulti = (FixedInt) 1 - FixedInt.Create(p.GetLevel(BookOf.Seasons)) * 20971L;
          if (zcreature1.race == CreatureRace.Werewolf)
          {
            zcreature1.TrueSize = (int) (fixedInt * 18);
            return;
          }
          if ((UnityEngine.Object) zcreature1.transform != (UnityEngine.Object) null)
            zcreature1.transform.localScale = new Vector3(num, num, 1f);
          zcreature1.radius = (int) (fixedInt * 18);
          zcreature1.zb = MapGenerator.getOutlineArray(zcreature1.radius);
          double scale = (double) zcreature1.scale;
          zcreature1.scale = num;
          zcreature1.scaleFixed = fixedInt;
          zcreature1.collider.radius = zcreature1.radius;
          zcreature1.collider.Moved();
          zcreature1.SetScale(1f);
          if ((UnityEngine.Object) zcreature1.overheadCanvas != (UnityEngine.Object) null)
            ((RectTransform) zcreature1.overheadCanvas.transform).anchoredPosition = zcreature1.ClientOverHeadOffset.ToSinglePrecision() * (1f / num);
          if ((ZComponent) zcreature1.auraOfDecay != (object) null && this.isClient)
            zcreature1.auraOfDecay.transform.localScale = new Vector3(1f / num, 1f / num, 1f / num);
          this.forceRysncPause = true;
          return;
        }
        if (p.GetLevel(b00k) == 1)
          p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Butterfly Jar")), b00k);
        using (List<SpellSlot>.Enumerator enumerator = zcreature1.spells.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SpellSlot current = enumerator.Current;
            if (current.spell.spellEnum == SpellEnum.Life_Dew)
            {
              ++current.MaxUses;
              break;
            }
          }
          break;
        }
      case BookOf.Illusion:
        List<ZCreature> zcreatureList = new List<ZCreature>();
        ZCreature zcreature2 = p.first();
        if (p.GetLevel(b00k) == 1)
          p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Blink")), b00k);
        else if (p.GetLevel(b00k) == 5)
        {
          foreach (SpellSlot spell in p.first().spells)
          {
            if (spell.spell.spellEnum == SpellEnum.Color_Spray)
            {
              spell.MaxUses = -1;
              break;
            }
          }
        }
        if (this.isTeam)
        {
          foreach (ZPerson zperson in this.GetTeam(p.team))
          {
            foreach (ZCreature zcreature3 in zperson.controlled)
            {
              if ((ZComponent) zcreature3 != (object) zcreature2 && zcreature3.health < zcreature3.maxHealth)
                zcreatureList.Add(zcreature3);
            }
          }
        }
        else
        {
          foreach (ZCreature zcreature4 in p.controlled)
          {
            if ((ZComponent) zcreature4 != (object) zcreature2 && zcreature4.health < zcreature4.maxHealth)
              zcreatureList.Add(zcreature4);
          }
        }
        if (zcreatureList.Count > 0)
        {
          zcreatureList.Sort((Comparison<ZCreature>) ((b, a) => a.maxHealth - a.health - (b.maxHealth - b.health)));
          int damage = 20;
          while (damage > 0 && zcreatureList.Count > 0)
          {
            ZCreature zcreature5 = zcreatureList[0];
            zcreature5.DoHeal(damage);
            damage = 0;
            if (zcreature5.health >= zcreature5.maxHealth)
            {
              damage = zcreature5.health - zcreature5.maxHealth;
              zcreature5.health = zcreature5.maxHealth;
              zcreatureList.RemoveAt(0);
            }
            if (this.isClient)
              zcreature5.UpdateHealthTxt();
          }
        }
        ZCreature zcreature6 = p.first();
        FixedInt fixedInt1 = (FixedInt) 1 - (FixedInt) ((long) p.GetLevel(BookOf.Illusion) * 125829L);
        float num1 = fixedInt1.ToFloat();
        if (!zcreature6.FullArcane)
          zcreature6.massMulti = (FixedInt) 1 - FixedInt.Create(p.GetLevel(BookOf.Illusion)) * 20971L;
        if (zcreature6.race == CreatureRace.Werewolf)
        {
          zcreature6.TrueSize = (int) (fixedInt1 * 18);
          break;
        }
        if ((UnityEngine.Object) zcreature6.transform != (UnityEngine.Object) null)
          zcreature6.transform.localScale = new Vector3(num1, num1, 1f);
        zcreature6.radius = (int) (fixedInt1 * 18);
        zcreature6.zb = MapGenerator.getOutlineArray(zcreature6.radius);
        double scale1 = (double) zcreature6.scale;
        zcreature6.scale = num1;
        zcreature6.scaleFixed = fixedInt1;
        zcreature6.collider.radius = zcreature6.radius;
        zcreature6.collider.Moved();
        zcreature6.SetScale(1f);
        if ((UnityEngine.Object) zcreature6.overheadCanvas != (UnityEngine.Object) null)
          ((RectTransform) zcreature6.overheadCanvas.transform).anchoredPosition = zcreature6.ClientOverHeadOffset.ToSinglePrecision() * (1f / num1);
        if ((ZComponent) zcreature6.auraOfDecay != (object) null && this.isClient && (UnityEngine.Object) zcreature6.auraOfDecay.transform.parent == (UnityEngine.Object) zcreature6.transform)
          zcreature6.auraOfDecay.transform.localScale = new Vector3(1f / num1, 1f / num1, 1f / num1);
        this.forceRysncPause = true;
        if ((ZComponent) zcreature6.tower == (object) null)
        {
          if ((ZComponent) zcreature6.mount != (object) null)
            zcreature6.RiderMoveToPosition = zcreature6.mount.position + Global.GetMountOffset(zcreature6.mount.transformscale, zcreature6.mount.type);
          else
            zcreature6.InstantFall();
        }
        zcreature6.CreatureMoveSurroundings();
        break;
    }
    if (!this.originalSpellsOnly)
    {
      switch (b00k)
      {
        case BookOf.Arcane:
          if (p.GetLevel(b00k) == 1)
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Little Devil")), b00k);
          else
            p.controlled[0].GetSpellSlot(SpellEnum.Little_Devil)?.DecreaseCooldown();
          if (p.GetLevel(b00k) == 2)
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Arcane Mist")), b00k);
          else if (p.GetLevel(b00k) == 5)
          {
            ZCreature zcreature7 = p.first();
            for (int index = 0; index < zcreature7.effectors.Count; ++index)
            {
              if (zcreature7.effectors[index].type == EffectorType.Arcane_Energizer)
                zcreature7.effectors[index].MaxTurnsAlive = 9000;
            }
          }
          if (p.game.isClient)
          {
            PanelPlayer panelPlayer = p.panelPlayer;
            if (panelPlayer != null)
            {
              panelPlayer.SetSummons(p);
              break;
            }
            break;
          }
          break;
        case BookOf.Flame:
          if (p.GetLevel(b00k) == 5)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Meteor Shower")), b00k);
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Fire Wave")), b00k);
            break;
          }
          break;
        case BookOf.Stone:
          if (p.GetLevel(b00k) == 1)
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Stepping Stone")), b00k);
          else if (p.GetLevel(b00k) == 3)
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Summon Mountain Goat")), b00k);
          using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SpellSlot current = enumerator.Current;
              if (current.spell.spellEnum == SpellEnum.Stepping_Stone)
              {
                --current.RechargeTime;
                break;
              }
            }
            break;
          }
        case BookOf.Storm:
          if (p.GetLevel(b00k) == 5)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Whistling Winds")), b00k);
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Electrostatic Charge")), b00k);
            using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpellSlot current = enumerator.Current;
                if (current.spell.spellEnum == SpellEnum.Wind_Shield)
                {
                  current.MaxUses = -1;
                  current.RechargeTime = 6;
                  break;
                }
              }
              break;
            }
          }
          else
            break;
        case BookOf.Frost:
          p.controlled[0].GetSpellSlot(SpellEnum.Ice_Shield)?.AddMaxUse();
          if (p.GetLevel(b00k) == 1)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Frost Leap")), b00k);
            break;
          }
          break;
        case BookOf.OverLight:
          using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SpellSlot current = enumerator.Current;
              if (current.spell.spellEnum == SpellEnum.Rising_Star)
              {
                current.MaxUses = -1;
                current.RechargeTime = 4;
                break;
              }
            }
            break;
          }
        case BookOf.Nature:
          if (p.GetLevel(b00k) == 5)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Forestation")), b00k);
            using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpellSlot current = enumerator.Current;
                if (current.spell.spellEnum == SpellEnum.Vine_Bomb)
                  ++current.MaxUses;
                else if (current.spell.spellEnum == SpellEnum.Thorn_Bomb)
                  ++current.MaxUses;
                else if (current.spell.spellEnum == SpellEnum.Vine_Bridge)
                  ++current.MaxUses;
              }
              break;
            }
          }
          else
            break;
        case BookOf.Seas:
          if (p.GetLevel(b00k) == 1)
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.GetSpell(SpellEnum.Brine_Burst)), b00k);
          if (p.GetLevel(b00k) == 5)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.Instance.spells.GetValue("Summon Kraken")), b00k);
            break;
          }
          break;
        case BookOf.Cogs:
          if (p.yourTurn)
            this.PlayersMaxTurnTime += Mathf.Min((float) this.MaxTurnTime / 5f, 6f);
          HUD.instance?.UpdateTime();
          if (p.GetLevel(b00k) == 1)
          {
            p.first().AddLevel5Spell(new SpellSlot(Inert.GetSpell(SpellEnum.Time_Dilation)), BookOf.Cogs);
          }
          else
          {
            SpellSlot spellSlot = p.first().GetSpellSlot(SpellEnum.Time_Dilation);
            if (spellSlot != null)
              ++spellSlot.MaxUses;
          }
          if (p.GetLevel(b00k) == 5)
          {
            using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpellSlot current = enumerator.Current;
                if (current.spell.spellEnum == SpellEnum.Cog_Fall)
                  current.RechargeTime = 2;
              }
              break;
            }
          }
          else if (p.GetLevel(b00k) == 1 || p.GetLevel(b00k) == 3)
          {
            using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpellSlot current = enumerator.Current;
                if (current.spell.spellEnum == SpellEnum.Cog_Fall)
                {
                  --current.RechargeTime;
                  break;
                }
              }
              break;
            }
          }
          else
            break;
        case BookOf.Blood:
          if (p.GetLevel(b00k) == 1)
          {
            p.drainable = false;
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.GetSpell(SpellEnum.Blood_Craze)), b00k);
            break;
          }
          if (p.GetLevel(b00k) == 3)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.GetSpell(SpellEnum.Blood_Mist)), b00k);
            break;
          }
          if (p.GetLevel(b00k) == 5)
          {
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.GetSpell(SpellEnum.Blood_Pact)), b00k);
            using (List<SpellSlot>.Enumerator enumerator = p.first().spells.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpellSlot current = enumerator.Current;
                if (current.spell.spellEnum == SpellEnum.Barrage_of_Bones)
                  --current.RechargeTime;
              }
              break;
            }
          }
          else
            break;
        case BookOf.The_Wilds:
          if (p.GetLevel(b00k) == 5)
            p.first().GetSpellSlot(SpellEnum.Ritual).EndsTurn = false;
          if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && (UnityEngine.Object) Player.Instance.selectedSpell != (UnityEngine.Object) null)
          {
            int selectedSpellIndex = Player.Instance.selectedSpellIndex;
            Player.Instance.UnselectSpell();
            Player.Instance.SetSpell(selectedSpellIndex);
            break;
          }
          break;
        case BookOf.Cosmos:
          p.first().massMulti = (FixedInt) 1 - (FixedInt) p.GetLevel(b00k) * (FixedInt) 157286L;
          if (p.GetLevel(b00k) == 1)
            p.controlled[0].AddLevel5Spell(new SpellSlot(Inert.GetSpell(SpellEnum.Gravity_Well)), b00k);
          if (p.GetLevel(b00k) == 5)
          {
            using (List<ZCreature>.Enumerator enumerator = p.controlled.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                ZCreature current = enumerator.Current;
                if ((ZComponent) current != (object) null && current.type == CreatureType.Cosmic_Horror)
                {
                  int spellIndex = current.GetSpellIndex(SpellEnum.Summon_Drone);
                  if (spellIndex >= 0)
                    current.spells[spellIndex] = p.first().GetSpellSlot(SpellEnum.Summon_Drone);
                }
              }
              break;
            }
          }
          else
            break;
      }
    }
    if (b00k == BookOf.Seas)
    {
      foreach (ZCreature zcreature8 in p.controlled)
        zcreature8.waterWalking = true;
    }
    ZFamiliar familiar = p.GetFamiliar(b00k);
    if (!ZComponent.IsNull((ZComponent) familiar))
    {
      if (familiar.bookOf != BookOf.Underdark)
        return;
      MyLocation pos = !p.first().inWater || p.localTurn > 0 || !p.game.First_Turn_Teleport ? p.first().position : new MyLocation(p.map.Width / 2, p.map.Height);
      if ((UnityEngine.Object) familiar.transform != (UnityEngine.Object) null)
        familiar.transform.position = (Vector3) pos.ToSinglePrecision();
      ZEffector effector = familiar.effector;
      effector.variable += 20;
      effector.collider?.Move(pos);
      ZSpellSoulJar soulJar = familiar.soulJar;
      soulJar.SetPosition = pos;
      soulJar.ShouldFall(true, false);
    }
    else
    {
      if ((UnityEngine.Object) Inert.Instance.familiars[(int) b00k] == (UnityEngine.Object) null)
        return;
      if (p.familiars == null)
        p.familiars = new List<ZFamiliar>();
      ZFamiliar zfamiliar = ZFamiliar.Create(p.controlled[0], Inert.Instance.familiars[(int) b00k].GetComponent<Familiar>());
      p.familiars.Add(zfamiliar);
      zfamiliar.creature = p.controlled[0];
      zfamiliar.bookOf = b00k;
      if (zfamiliar.bookOf == BookOf.Underdark)
      {
        MyLocation pos = !p.first().inWater || p.localTurn > 0 || !p.game.First_Turn_Teleport ? p.first().position : new MyLocation(p.map.Width / 2, p.map.Height);
        if ((UnityEngine.Object) zfamiliar.transform != (UnityEngine.Object) null)
          zfamiliar.transform.position = (Vector3) pos.ToSinglePrecision();
        ZEffector effector = zfamiliar.effector;
        effector.variable = 20;
        effector.collider.world = p.world;
        effector.game = p.game;
        effector.collider.Move(pos);
        ZSpellSoulJar soulJar = zfamiliar.soulJar;
        soulJar.collider = effector.collider;
        soulJar.collider.world = p.world;
        soulJar.game = p.game;
        soulJar.parent = p;
        soulJar.position = pos;
        soulJar.ShouldFall(true, false);
        if (!this.isClient || !((UnityEngine.Object) zfamiliar.transform != (UnityEngine.Object) null))
          return;
        ConfigurePlayer.ApplyOutfit(zfamiliar.transform.GetChild(0).GetComponent<SpriteRenderer>(), p.settingsPlayer);
      }
      else if (zfamiliar.bookOf == BookOf.Nature)
        ((FamiliarNature) zfamiliar.clientObj).UpdatePosition();
      else if (zfamiliar.bookOf == BookOf.Cogs)
      {
        zfamiliar.clientObj.ColorFamiliar();
      }
      else
      {
        if (zfamiliar.bookOf != BookOf.Frost || p.GetLevel(BookOf.Frost) != 1 || !p.InWater() || p.localTurn <= 0 && this.First_Turn_Teleport)
          return;
        ZCreature zcreature9 = p.first();
        if (!((ZComponent) zcreature9 != (object) null) || !(zcreature9.position.x > 0) || !(zcreature9.position.x < this.map.Width))
          return;
        zcreature9.MoveToPosition = new MyLocation(zcreature9.position.x, (FixedInt) zcreature9.radius);
        zcreature9.Fall();
        if (!this.isClient || !((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null))
          return;
        Player.Instance.UnselectSpell();
      }
    }
  }

  public void SendOldDataOlD(ZPerson p, Connection c, string err, bool wait = true, Action onTrue = null)
  {
    if (p.resyncIE != null)
      return;
    p.resyncIE = Timing.RunCoroutine(this.WaitForResync(p, c, err, wait, onTrue));
  }

  public void SendOldData(ZPerson p, Connection c, string err, bool wait = true, Action onTrue = null)
  {
    if (p == null)
    {
      Timing.RunCoroutine(this.WaitForSerialization(p, c, err, wait, onTrue));
    }
    else
    {
      if (p.clientResyncing)
        return;
      p.ready = false;
      p.clientResyncing = true;
      p.resyncIE = Timing.RunCoroutine(this.WaitForSerialization(p, c, err, wait, onTrue));
    }
  }

  private IEnumerator<float> WaitForSerialization(
    ZPerson p,
    Connection c,
    string err,
    bool wait,
    Action onTrue)
  {
    while (this.ongoing.NumberOfSlowUpdateCoroutines > 0 && c.State == ConnectionState.Connected && this.serverState.busy != ServerState.Busy.Ended && this.gameFacts != null && c.player.gameNumber == this.gameFacts.id)
      yield return 0.0f;
    int offset = 0;
    if (!this._oldData.TryGetValue(c, out offset))
      offset = 0;
    int count = this.timeline.Count;
    int len = count - offset;
    if (c.State != ConnectionState.Connected || this.serverState.busy == ServerState.Busy.Ended || this.gameFacts == null || c.player.gameNumber != this.gameFacts.id)
    {
      if (p != null)
      {
        p.clientResyncing = false;
        p.resyncIE = (IEnumerator<float>) null;
      }
    }
    else if (len == 0)
    {
      if (p != null)
      {
        p.clientResyncing = false;
        p.resyncIE = (IEnumerator<float>) null;
      }
    }
    else
    {
      c.SendBytes(this.SerializeAll(c, offset, len, (SpellOverrides) null));
      this._oldData[c] = count;
      if (p != null)
      {
        p.ready = true;
        p.clientResyncing = false;
        p.resyncIE = (IEnumerator<float>) null;
      }
      Action action = onTrue;
      if (action != null)
        action();
    }
  }

  private IEnumerator<float> WaitForResync(
    ZPerson p,
    Connection c,
    string err,
    bool wait,
    Action onTrue)
  {
    if (wait)
    {
      while (this.ongoing.NumberOfSlowUpdateCoroutines > 0 && c.State == ConnectionState.Connected && this.serverState.busy != ServerState.Busy.Ended && this.gameFacts != null && c.player.gameNumber == this.gameFacts.id)
        yield return 0.0f;
    }
    int num1 = 0;
    if (!this._oldData.TryGetValue(c, out num1))
      num1 = 0;
    int count = this.timeline.Count;
    int num2 = count - num1;
    if (c.State != ConnectionState.Connected || this.serverState.busy == ServerState.Busy.Ended || this.gameFacts == null || c.player.gameNumber != this.gameFacts.id)
      p.resyncIE = (IEnumerator<float>) null;
    else if (num2 == 0)
    {
      p.resyncIE = (IEnumerator<float>) null;
    }
    else
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 64);
          myBinaryWriter.Write(num1);
          myBinaryWriter.Write(num2);
          for (int index = num1; index < count; ++index)
            myBinaryWriter.Write(this.timeline[index]);
          myBinaryWriter.Write(err);
          if (this.ongoing.NumberOfSlowUpdateCoroutines <= 0)
          {
            int num3 = wait ? 1 : 0;
          }
          myBinaryWriter.Write(false);
        }
        if (c != null)
        {
          if (c.State == ConnectionState.Connected)
            c.SendBytes(memoryStream.ToArray());
        }
      }
      this._oldData[c] = count;
      Action action = onTrue;
      if (action != null)
        action();
      p.resyncIE = (IEnumerator<float>) null;
    }
  }

  public void SendResyncMsg(ZPerson p, string err, bool wait = true, Action onTrue = null)
  {
    if (this.isSpectator)
      HUD.instance?.OnSpectatorShouldResync(err);
    if (this.isMulti && this.GetTeam(p.team).Count > 0)
      p = this.GetTeam(p.team)[0];
    if (!p.Connected)
      return;
    Connection connection = p.connection;
    if (!this.isClient && connection != null && connection.State == ConnectionState.Connected)
    {
      this.SendOldData(p, connection, err, wait, onTrue);
    }
    else
    {
      if (this.isServer || this.isReplay || this.isSandbox || Client.connection == null || Client.connection.State != ConnectionState.Connected)
        return;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 64);
          myBinaryWriter.Write(err);
        }
        Client.connection.SendBytes(memoryStream.ToArray());
      }
    }
  }

  public void SendByte(byte m)
  {
    this.SendAllMessage(new byte[1]{ m });
  }

  public void SendNextTurn(int lastPlayersTurn)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 198);
        myBinaryWriter.Write(this.serverState.playersTurn);
        myBinaryWriter.Write(this.turn);
        myBinaryWriter.Write(lastPlayersTurn);
        myBinaryWriter.Write(lastPlayersTurn < this.players.Count ? this.players[lastPlayersTurn].countdown : 0.0f);
      }
      this.SendAllMessage(memoryStream.ToArray());
    }
  }

  public void SendGameTime()
  {
    if (!this.isClient && (this.serverState.busy == ServerState.Busy.No || this.serverState.busy == ServerState.Busy.Moving))
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 15);
          myBinaryWriter.Write(this.serverState.playersTurn);
          myBinaryWriter.Write(this.serverState.turnTime);
          myBinaryWriter.Write(this.players[(int) this.serverState.playersTurn].countdown);
        }
        if (this.players[(int) this.serverState.playersTurn].GetMultiConnected)
          this.players[(int) this.serverState.playersTurn].GetMultiConnection.SendBytes(memoryStream.ToArray());
      }
    }
    if (!this.isClient || !((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null) || this.resyncing)
      return;
    HUD.instance.lastPlayersTurn = byte.MaxValue;
    HUD.instance.UpdateTime();
  }

  public void SendTeamMessage(byte[] b, int team)
  {
    foreach (ZPerson zperson in this.GetTeam(team))
    {
      if (zperson.Connected)
        zperson.connection?.SendBytes(b);
    }
  }

  public void SendAllMessageNoSpecOrTimeLine(byte[] b)
  {
    foreach (ZPerson player in this.players)
    {
      if (player.Connected)
        player.connection?.SendBytes(b);
    }
  }

  public void SendSpectators(byte[] b)
  {
    for (int index = this.spectators.Count - 1; index >= 0; --index)
    {
      if (this.spectators[index] != null && this.spectators[index].State == ConnectionState.Connected)
        this.spectators[index].SendBytes(b);
      else if (this.spectators[index] != null && this.spectators[index].player.inBoat)
      {
        Connection spectator = this.spectators[index];
        this.spectators.RemoveAt(index);
        this.RemoveFromBoat(spectator);
      }
      else
        this.spectators.RemoveAt(index);
    }
  }

  public void SendAllMessage(byte[] b)
  {
    foreach (ZPerson player in this.players)
    {
      if (player.Connected && player.ready)
        player.connection?.SendBytes(b);
      else if (player.connection != null && player.connection.player.inBoat)
        this.RemoveFromBoat(player.connection);
    }
    if (b[0] == (byte) 153 || b[0] == (byte) 33)
      return;
    if (b[0] != (byte) 15)
      this.timeline.Add(b);
    for (int index = this.spectators.Count - 1; index >= 0; --index)
    {
      if (this.spectators[index] != null && this.spectators[index].State == ConnectionState.Connected)
        this.spectators[index].SendBytes(b);
      else if (this.spectators[index] != null && this.spectators[index].player.inBoat)
      {
        Connection spectator = this.spectators[index];
        this.spectators.RemoveAt(index);
        this.RemoveFromBoat(spectator);
      }
      else
        this.spectators.RemoveAt(index);
    }
  }

  public void SendAllMessageNoRemoveBoat(byte[] b)
  {
    foreach (ZPerson player in this.players)
    {
      if (player.Connected)
        player.connection?.SendBytes(b);
    }
    if (b[0] == (byte) 153 || b[0] == (byte) 33)
      return;
    if (b[0] != (byte) 15)
      this.timeline.Add(b);
    for (int index = this.spectators.Count - 1; index >= 0; --index)
    {
      if (this.spectators[index] != null && this.spectators[index].State == ConnectionState.Connected)
        this.spectators[index].SendBytes(b);
    }
  }

  public void RemoveInactiveSpectators()
  {
    for (int index = this.spectators.Count - 1; index >= 0; --index)
    {
      if (this.spectators[index] == null || this.spectators[index].State != ConnectionState.Connected)
      {
        if (this.spectators[index] != null && this.spectators[index].player.inBoat)
        {
          Connection spectator = this.spectators[index];
          this.spectators.RemoveAt(index);
          this.RemoveFromBoat(spectator);
        }
        else
          this.spectators.RemoveAt(index);
      }
    }
  }

  public static byte[] CopyByteArray(byte[] b)
  {
    byte[] numArray = new byte[b.Length];
    for (int index = 0; index < b.Length; ++index)
      numArray[index] = b[index];
    return numArray;
  }

  public byte[] ServerCopyByteArray(byte[] b)
  {
    if (this.isClient)
      return (byte[]) null;
    byte[] numArray = new byte[b.Length];
    for (int index = 0; index < b.Length; ++index)
      numArray[index] = b[index];
    return numArray;
  }

  public void SandBoxOrOnline()
  {
    WindIndicatorUI.Instance?.gameObject.SetActive(this.isWindy);
    if (!this.isWindy)
      return;
    this.map.RandomizeWind();
  }

  public void CreatePlayers(GameFacts g)
  {
    this.random = new IsaacCipher(new int[1]{ g.seed });
    this.isTeam = g.GetTeamMode();
    this.isMulti = g.GetMultiTeamMode();
    this.originalSpellsOnly = this.gameFacts.GetStyle().HasStyle(GameStyle.Original_Spells_Only);
    this.isWindy = this.gameFacts.GetStyle().HasStyle(GameStyle.Wind);
    this.ArcaneZero = g.GetStyle().HasStyle(GameStyle.Zero_Shield);
    this.First_Turn_Teleport = g.GetStyle().HasStyle(GameStyle.First_Turn_Teleport) || g.realMap == MapEnum.Dark_Fortress;
    this.TurnToLoseArcaneZero = this.First_Turn_Teleport ? 1 : 0;
    this.MaxTurnTime = (int) g.settings.customTime;
    this.TEAM_COUNT = Mathf.Max(1, g.players.Count / g.GetNumberPlayersPerTeam());
    if (this.isMulti && !this.isTeam)
      this.isMulti = false;
    this.AllowDKH = this.originalSpellsOnly;
    if (!this.isSandbox && !this.isTutorial)
    {
      this.armageddonTurn = (int) g.settings.armageddonTurn;
      this.isCountdown = g.settings.countdownTime != (short) 0;
      this.countdownLose = g.settings.countdownTime < (short) 0;
      this.countdownTime = (float) Mathf.Abs((int) g.settings.countdownTime);
    }
    if (this.isClient)
      HUD.instance.panelCountdown.SetActive(this.isCountdown);
    MapEnum armageddon = g.GetArmageddon();
    this.armageddon = armageddon != ~MapEnum.Dont_Mind ? (!Enum.IsDefined(typeof (MapEnum), (object) armageddon) ? GameFacts.GetRandomMap(this, (int) armageddon, g.realMap) : armageddon) : g.realMap;
    this.SandBoxOrOnline();
    for (int index = 0; index < g.players.Count; ++index)
    {
      ZPerson p = new ZPerson();
      p.countdown = (float) Mathf.Abs((int) this.gameFacts.settings.countdownTime);
      p.game = this;
      p.account = g.accounts[index];
      if (this.isClient)
      {
        TcpConnection tcpConnection = new TcpConnection();
        g.connections.Add((Connection) tcpConnection);
        tcpConnection.player.player = p;
        tcpConnection.player.account = p.account;
        p.connection = (Connection) tcpConnection;
      }
      else if (!this.isMulti || index % g.GetNumberPlayersPerTeam() == 0)
      {
        p.connection = this.isMulti ? g.connections[index / g.GetNumberPlayersPerTeam()] : g.connections[index];
        p.connection.player.player = p;
        p.startingRating = p.connection.player.account[(int) g.gameType].rating;
        p.connection.player.id = index;
      }
      else
        p.isFake = true;
      p.id = (byte) index;
      p.team = this.isTeam ? index / g.GetNumberPlayersPerTeam() : index;
      p.name = g.players[index];
      p.settingsPlayer = g.settingsPlayer[index];
      ZCreature character = Inert.CreateCharacter(p, g.settingsPlayer[index], new MyLocation(0, 1000), index);
      if (this.gameFacts.settings.startHealth != (ushort) 250)
      {
        character.health = (int) this.gameFacts.settings.startHealth;
        character.maxHealth = character.health;
        character.UpdateHealthTxt();
      }
      character.team = p.team;
      p.controlled.Add(character);
      this.players.Add(p);
      this._playersExtended.Add(p);
      if (this.isTeam)
        this.GetTeam(p.team).Add(p);
      if (!this.originalSpellsOnly)
        UnityEngine.Object.Instantiate<BigBubble>(ClientResources.Instance.big_bubble, character.transform.position, Quaternion.identity, character.transform).creature = character;
    }
    this.InitUncontrolled();
    if (!this.isClient)
      return;
    this.ScalePlayersPanel(false, 1f);
  }

  public void OnSetup()
  {
  }

  public void ScalePlayersPanel(bool forceBig, float min = 0.5f)
  {
    float num1 = 1f;
    float num2 = forceBig ? -843f : -265f;
    float y = ((RectTransform) HUD.instance.panelPlayersPanel.GetChild(HUD.instance.panelPlayersPanel.childCount - 1)).anchoredPosition.y;
    if ((double) y < (double) num2)
      num1 = num2 / y;
    if (!forceBig && (double) num1 > (double) min)
      num1 = min;
    HUD.instance.panelPlayersPanel.localScale = new Vector3(num1, num1, num1);
  }

  public List<ZPerson> GetTeam(int i)
  {
    switch (i)
    {
      case 0:
        return this.team1Players;
      case 1:
        return this.team2Players;
      case 2:
        return this.team3Players;
      case 3:
        return this.team4Players;
      case 4:
        return this.team5Players;
      case 5:
        return this.team6Players;
      case 6:
        return this.team7Players;
      case 7:
        return this.team8Players;
      case 8:
        return this.team9Players;
      case 9:
        return this.team10Players;
      case 10:
        return this.team11Players;
      case 11:
        return this.team12Players;
      default:
        return (List<ZPerson>) null;
    }
  }

  public bool TeamAlive(List<ZPerson> team, ZPerson notThis)
  {
    for (int index = 0; index < team.Count; ++index)
    {
      if (team[index] != notThis && team[index].controlled.Count > 0 && !team[index].didResign)
        return true;
    }
    return false;
  }

  public void RemoveFamiliars()
  {
    foreach (ZPerson player in this.players)
    {
      player.drainable = true;
      player.familiarBook = FamiliarType.Nothing;
      for (int index = 0; index < player.familiarLevels.Length; ++index)
        player.familiarLevels[index] = 0;
      if ((ZComponent) player.first() != (object) null)
      {
        player.first().waterWalking = false;
        player.first().frostWalking = false;
      }
      if (player.familiars != null)
      {
        foreach (ZFamiliar familiar in player.familiars)
        {
          if ((ZComponent) familiar != (object) null)
          {
            familiar.isNull = true;
            UnityEngine.Object.Destroy((UnityEngine.Object) familiar.gameObject);
          }
        }
        player.familiars = (List<ZFamiliar>) null;
      }
    }
    if (!this.isClient)
      return;
    HUD.instance.fullBookObj.SetActive(false);
  }

  public void GameHandler(ZPerson p, byte[] b, bool fromServer, bool ignoreOwn = true)
  {
    this.resyncOnError = true;
    using (MemoryStream memoryStream1 = new MemoryStream(b))
    {
      using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream1))
      {
        try
        {
          byte num1 = myBinaryReader.ReadByte();
          byte num2 = b.Length <= 1 || !Global.HasPlayerID(num1) ? (byte) 0 : myBinaryReader.ReadByte();
          if (num1 > (byte) 200)
          {
            if ((int) p.id != (int) this.serverState.playersTurn && !fromServer)
            {
              if (num1 == (byte) 206 || num1 == (byte) 220)
                return;
              this.SendResyncMsg(p, "your turn already ended");
            }
            else
            {
              int num3 = myBinaryReader.ReadInt32();
              int moveID = myBinaryReader.ReadInt32();
              int id = this.gameFacts.id;
              if (num3 != id && !this.isReplay)
                return;
              byte selectedIndex = myBinaryReader.ReadByte();
              byte playerOffset = myBinaryReader.ReadByte();
              if (((!this.isClient ? 0 : (num1 < (byte) 220 ? 1 : 0)) & (fromServer ? 1 : 0) & (ignoreOwn ? 1 : 0)) != 0 && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && p == Player.Instance.person)
              {
                if (num1 != (byte) 207)
                  return;
                this.init_Resync();
              }
              else
              {
                switch (num1)
                {
                  case 201:
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn || p.controlled.Count == 0 || p.localTurn <= 0 && this.First_Turn_Teleport)
                      {
                        this.SendResyncMsg(p, "was not your turn - When using familiar");
                      }
                      else
                      {
                        ZCreature zcreature = p.controlled[0];
                        if (zcreature.familiarLevelActivateable >= 5 || zcreature.familiar == FamiliarType.Nothing || zcreature.health <= 20)
                          this.SendResyncMsg(p, "unable to process - When using familiar");
                        else if (zcreature.FullArcane)
                        {
                          if (zcreature.health <= 100)
                          {
                            this.SendResyncMsg(p, "full arcane less then 100 hp - When using familiar");
                          }
                          else
                          {
                            zcreature.DoDamage(100);
                            this.RemoveFamiliars();
                            if (!this.isClient)
                              this.SendAllMessage(b);
                            if (!this.isClient)
                              return;
                            zcreature.UpdateHealthTxt();
                          }
                        }
                        else
                        {
                          p.IncreaseLevel();
                          zcreature.DoDamage(20);
                          if (zcreature.hasDarkDefenses)
                            zcreature.DarkDefenses(true);
                          if (!this.isClient)
                            this.SendAllMessage(b);
                          this.CreateFamiliar(p.ActivateableFamiliar, p);
                          if (!this.isClient)
                            return;
                          zcreature.UpdateHealthTxt();
                          if (!((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || p != Player.Instance.person)
                            return;
                          HUD.instance.fullBookTextLevel.text = "Level " + (object) zcreature.familiarLevelActivateable;
                        }
                      }
                    }));
                    break;
                  case 202:
                    ZPerson person1 = p;
                    float scaleX1 = myBinaryReader.ReadByte() > (byte) 0 ? 1f : -1f;
                    MyLocation validPos1 = myBinaryReader.ReadMyLocation();
                    int extraBits1 = (int) myBinaryReader.ReadByte();
                    bool sprinting1 = ExtraMoveBits.IsSprinting(extraBits1) && this.AllowExpansion;
                    byte[] bites1 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person1.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could not move left - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                            return;
                          person1 = this.players[(int) playerOffset];
                        }
                        if (person1.controlled.Count <= (int) selectedIndex)
                          return;
                        ZCreature cre = person1.controlled[(int) selectedIndex];
                        if (cre.position.x != validPos1.x || cre.position.y != validPos1.y)
                        {
                          this.SendResyncMsg(p, "invalid position on move right #id" + (object) moveID + ": " + (object) validPos1 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                          {
                            if (this.loggedToDiscord)
                              return;
                            this.loggedToDiscord = true;
                            Server.Instance?.communicator?.SendResync(this, "Invalid position on move right #id " + (object) moveID + ": " + (object) validPos1 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                          }));
                        }
                        else
                        {
                          if (ZComponent.IsNull((ZComponent) cre) || cre.stunned || !this.AllowMovement || p.InWater() && cre.type != CreatureType.Recall_Device || !fromServer && !p.yourTurn || !cre.canMove && !cre.flying || !cre.controllable || cre.InDarkTotem())
                            return;
                          this.serverState.busy = ServerState.Busy.No;
                          if (!this.isClient)
                          {
                            this.SendAllMessage(bites1);
                          }
                          else
                          {
                            this._lastActiveCreature = cre;
                            if (cre.pawn || !cre.flying || cre.entangledOrGravity)
                              cre.animator?.Play(AnimateState.Walk, 0.05f);
                            if (CameraMovement.FOLLOWTARGETS && (ZComponent) CameraMovement.Instance.target != (object) cre.transform && !CameraMovement.Instance.IsfollowingBee(cre))
                              CameraMovement.Instance.LerpToTransform(cre);
                          }
                          cre.parent.movedThisTurn = true;
                          cre.OnMoved();
                          if (cre.flying && !cre.entangledOrGravity)
                          {
                            cre.velocity = new MyLocation((FixedInt) ((double) scaleX1 < 0.0 ? -11010048L : -6291456L), (FixedInt) 0);
                            cre.SetScale(-1f);
                            cre.moving = this.ongoing.RunCoroutine(cre.FlyMove(true));
                          }
                          else
                          {
                            if (cre.type == CreatureType.Wolf)
                              sprinting1 = true;
                            if (!sprinting1)
                              cre.sprinting = 0;
                            else if (cre.sprinting > 0)
                              cre.sprinting = 0;
                            else
                              --cre.sprinting;
                            cre.MoveLeft(extraBits1);
                            if (!sprinting1 || cre.isMoving)
                              return;
                            cre.MoveLeft(extraBits1);
                          }
                        }
                      }
                    }));
                    break;
                  case 203:
                    ZPerson person2 = p;
                    float scaleX2 = myBinaryReader.ReadByte() > (byte) 0 ? 1f : -1f;
                    MyLocation validPos2 = myBinaryReader.ReadMyLocation();
                    int extraBits2 = (int) myBinaryReader.ReadByte();
                    bool sprinting2 = ExtraMoveBits.IsSprinting(extraBits2) && this.AllowExpansion;
                    byte[] bites2 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person2.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could not move right - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                            return;
                          person2 = this.players[(int) playerOffset];
                        }
                        if (person2.controlled.Count <= (int) selectedIndex)
                          return;
                        ZCreature cre = person2.controlled[(int) selectedIndex];
                        if (cre.position.x != validPos2.x || cre.position.y != validPos2.y)
                        {
                          this.SendResyncMsg(p, "invalid position on move left #id" + (object) moveID + ": " + (object) validPos2 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                          {
                            if (this.loggedToDiscord)
                              return;
                            this.loggedToDiscord = true;
                            Server.Instance?.communicator?.SendResync(this, "Invalid position on move left #id " + (object) moveID + ": " + (object) validPos2 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                          }));
                        }
                        else
                        {
                          if (ZComponent.IsNull((ZComponent) cre) || cre.stunned || !this.AllowMovement || p.InWater() && cre.type != CreatureType.Recall_Device || !fromServer && !p.yourTurn || !cre.canMove && !cre.flying || !cre.controllable || cre.InDarkTotem())
                            return;
                          this.serverState.busy = ServerState.Busy.No;
                          if (!this.isClient)
                          {
                            this.SendAllMessage(bites2);
                          }
                          else
                          {
                            this._lastActiveCreature = cre;
                            if (cre.pawn || !cre.flying || cre.entangledOrGravity)
                              cre.animator?.Play(AnimateState.Walk, 0.05f);
                            if (CameraMovement.FOLLOWTARGETS && (ZComponent) CameraMovement.Instance.target != (object) cre.transform && !CameraMovement.Instance.IsfollowingBee(cre))
                              CameraMovement.Instance.LerpToTransform(cre);
                          }
                          cre.parent.movedThisTurn = true;
                          cre.OnMoved();
                          if (cre.flying && !cre.entangledOrGravity)
                          {
                            cre.velocity = new MyLocation((FixedInt) ((double) scaleX2 > 0.0 ? 9437184L : 4194304L), (FixedInt) 0);
                            cre.SetScale(1f);
                            cre.moving = this.ongoing.RunCoroutine(cre.FlyMove(true));
                          }
                          else
                          {
                            if (cre.type == CreatureType.Wolf)
                              sprinting2 = true;
                            if (!sprinting2)
                              cre.sprinting = 0;
                            else if (cre.sprinting < 0)
                              cre.sprinting = 0;
                            else
                              ++cre.sprinting;
                            cre.MoveRight(extraBits2);
                            if (!sprinting2 || cre.isMoving)
                              return;
                            cre.MoveRight(extraBits2);
                          }
                        }
                      }
                    }));
                    break;
                  case 204:
                    ZPerson person3 = p;
                    float scaleX3 = myBinaryReader.ReadByte() > (byte) 0 ? 1f : -1f;
                    MyLocation validPos3 = myBinaryReader.ReadMyLocation();
                    int extraBits3 = (int) myBinaryReader.ReadByte();
                    bool sprinting3 = ExtraMoveBits.IsSprinting(extraBits3) && this.AllowExpansion;
                    byte[] bites3 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person3.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could high jump - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                            return;
                          person3 = this.players[(int) playerOffset];
                        }
                        if (person3.controlled.Count <= (int) selectedIndex)
                          return;
                        ZCreature cre = person3.controlled[(int) selectedIndex];
                        if (cre.position.x != validPos3.x || cre.position.y != validPos3.y)
                        {
                          this.SendResyncMsg(p, "invalid position on high jump #id" + (object) moveID + ": " + (object) validPos3 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                          {
                            if (this.loggedToDiscord)
                              return;
                            this.loggedToDiscord = true;
                            Server.Instance?.communicator?.SendResync(this, "Invalid position on high jump #id " + (object) moveID + ": " + (object) validPos3 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                          }));
                        }
                        else
                        {
                          if (ZComponent.IsNull((ZComponent) cre) || cre.stunned || !this.AllowMovement || p.InWater() && cre.type != CreatureType.Recall_Device || !fromServer && !p.yourTurn || !cre.canMove && !cre.flying || !cre.controllable || cre.InDarkTotem())
                            return;
                          if ((ZComponent) cre.tower != (object) null)
                          {
                            if (cre.tower.type != TowerType.Cosmos)
                              return;
                            if (!this.isClient)
                              this.SendAllMessage(bites3);
                            cre.tower.TowerMoveUp();
                          }
                          else
                          {
                            this.serverState.busy = ServerState.Busy.Moving;
                            cre.parent.movedThisTurn = true;
                            cre.OnMoved();
                            if (cre.pawn || !cre.flying || cre.entangledOrGravity)
                              cre.animator?.Play(AnimateState.Jump);
                            if (!this.isClient)
                            {
                              this.SendAllMessage(bites3);
                            }
                            else
                            {
                              this._lastActiveCreature = cre;
                              if (CameraMovement.FOLLOWTARGETS && (ZComponent) CameraMovement.Instance.target != (object) cre.transform && !CameraMovement.Instance.IsfollowingBee(cre))
                                CameraMovement.Instance.LerpToTransform(cre);
                            }
                            if ((double) cre.transformscale != (double) scaleX3)
                              cre.SetScale(scaleX3);
                            if (!sprinting3)
                              cre.sprinting = 0;
                            cre.DoHighJump(extraBits3);
                            cre.sprinting = 0;
                          }
                        }
                      }
                    }));
                    break;
                  case 205:
                    ZPerson person4 = p;
                    float scaleX4 = myBinaryReader.ReadByte() > (byte) 0 ? 1f : -1f;
                    MyLocation validPos4 = myBinaryReader.ReadMyLocation();
                    int extraBits4 = (int) myBinaryReader.ReadByte();
                    bool sprinting4 = ExtraMoveBits.IsSprinting(extraBits4) && this.AllowExpansion;
                    byte[] bites4 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person4.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could not long jump - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                            return;
                          person4 = this.players[(int) playerOffset];
                        }
                        if (person4.controlled.Count <= (int) selectedIndex)
                          return;
                        ZCreature cre = person4.controlled[(int) selectedIndex];
                        if (cre.position.x != validPos4.x || cre.position.y != validPos4.y)
                        {
                          this.SendResyncMsg(p, "invalid position on long jump #id" + (object) moveID + ": " + (object) validPos4 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                          {
                            if (this.loggedToDiscord)
                              return;
                            this.loggedToDiscord = true;
                            Server.Instance?.communicator?.SendResync(this, "Invalid position on long jump #id " + (object) moveID + ": " + (object) validPos4 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                          }));
                        }
                        else
                        {
                          if (ZComponent.IsNull((ZComponent) cre) || cre.stunned || !this.AllowMovement || p.InWater() && cre.type != CreatureType.Recall_Device || !fromServer && !p.yourTurn || !cre.canMove && !cre.flying || !cre.controllable || cre.InDarkTotem())
                            return;
                          if ((ZComponent) cre.tower != (object) null)
                          {
                            if (cre.tower.type != TowerType.Cosmos)
                              return;
                            if (!this.isClient)
                              this.SendAllMessage(bites4);
                            cre.tower.TowerMoveDown();
                          }
                          else
                          {
                            this.serverState.busy = ServerState.Busy.Moving;
                            cre.parent.movedThisTurn = true;
                            cre.OnMoved();
                            if (cre.pawn || !cre.flying || cre.entangledOrGravity)
                              cre.animator?.Play(AnimateState.Jump);
                            if (!this.isClient)
                              this.SendAllMessage(bites4);
                            else if (CameraMovement.FOLLOWTARGETS && (ZComponent) CameraMovement.Instance.target != (object) cre.transform && !CameraMovement.Instance.IsfollowingBee(cre))
                              CameraMovement.Instance.LerpToTransform(cre);
                            if ((double) cre.transformscale != (double) scaleX4)
                              cre.SetScale(scaleX4);
                            if (!sprinting4)
                              cre.sprinting = 0;
                            cre.DoLongJump(extraBits4);
                            cre.sprinting = 0;
                          }
                        }
                      }
                    }));
                    break;
                  case 206:
                    if (!this.isServer)
                      break;
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      if (p == null || !fromServer && !p.yourTurn)
                        return;
                      p.inactiveTurns = 0;
                      this.serverState.busy = ServerState.Busy.Between_Turns;
                      this.serverState.turnTime = this.PlayersMaxTurnTime + 3f;
                      p.yourTurn = false;
                      this.MoveQue.Clear();
                    }));
                    break;
                  case 208:
                    ZPerson person5 = p;
                    float scaleX5 = myBinaryReader.ReadByte() > (byte) 0 ? -1f : 1f;
                    MyLocation validPos5 = myBinaryReader.ReadMyLocation();
                    byte[] bites5 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person5.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could not flip flop - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                          {
                            this.SendResyncMsg(p, "could not flip flop - Invalid player index");
                            return;
                          }
                          person5 = this.players[(int) playerOffset];
                        }
                        if (person5.controlled.Count <= (int) selectedIndex)
                        {
                          this.SendResyncMsg(p, "could not flip flop - Invalid minion index");
                        }
                        else
                        {
                          ZCreature cre = person5.controlled[(int) selectedIndex];
                          if (cre.position.x != validPos5.x || cre.position.y != validPos5.y)
                            this.SendResyncMsg(p, "invalid position on flip flop #id" + (object) moveID + ": " + (object) validPos5 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                            {
                              if (this.loggedToDiscord)
                                return;
                              this.loggedToDiscord = true;
                              Server.Instance?.communicator?.SendResync(this, "Invalid position on flip flop #id " + (object) moveID + ": " + (object) validPos5 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                            }));
                          else if (ZComponent.IsNull((ZComponent) cre) || p.InWater() && cre.type != CreatureType.Recall_Device)
                          {
                            this.SendResyncMsg(p, "could not flip flop");
                          }
                          else
                          {
                            if (!this.isClient)
                              this.SendAllMessage(bites5);
                            else
                              this._lastActiveCreature = cre;
                            cre.DoFlipFlop(scaleX5);
                            cre.sprinting = 0;
                          }
                        }
                      }
                    }));
                    break;
                  case 209:
                    ZPerson person6 = p;
                    int num4 = (int) myBinaryReader.ReadByte();
                    MyLocation validPos6 = myBinaryReader.ReadMyLocation();
                    byte[] bites6 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person6.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could not flip flop - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                          {
                            this.SendResyncMsg(p, "could not flip flop - Invalid player index");
                            return;
                          }
                          person6 = this.players[(int) playerOffset];
                        }
                        if (person6.controlled.Count <= (int) selectedIndex)
                        {
                          this.SendResyncMsg(p, "could not flip flop - Invalid minion index");
                        }
                        else
                        {
                          ZCreature cre = person6.controlled[(int) selectedIndex];
                          if (cre.position.x != validPos6.x || cre.position.y != validPos6.y)
                            this.SendResyncMsg(p, "invalid position on flip flop #id" + (object) moveID + ": " + (object) validPos6 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                            {
                              if (this.loggedToDiscord)
                                return;
                              this.loggedToDiscord = true;
                              Server.Instance?.communicator?.SendResync(this, "Invalid position on detower #id " + (object) moveID + ": " + (object) validPos6 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                            }));
                          else if (ZComponent.IsNull((ZComponent) cre) || p.InWater() && cre.type != CreatureType.Recall_Device)
                          {
                            this.SendResyncMsg(p, "could not flip flop");
                          }
                          else
                          {
                            if ((ZComponent) cre.tower == (object) null)
                              return;
                            if (!this.isClient)
                              this.SendAllMessage(bites6);
                            else
                              this._lastActiveCreature = cre;
                            cre.DestroyTower();
                            cre.sprinting = 0;
                          }
                        }
                      }
                    }));
                    break;
                  case 211:
                    ZPerson person7 = p;
                    float scaleX6 = myBinaryReader.ReadByte() > (byte) 0 ? 1f : -1f;
                    MyLocation validPos7 = myBinaryReader.ReadMyLocation();
                    byte extraMoveBits = myBinaryReader.ReadByte();
                    FixedInt rot_z1 = (FixedInt) myBinaryReader.ReadInt64();
                    byte[] bites7 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person7.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn)
                      {
                        this.SendResyncMsg(p, "could not controlled jump - not your turn");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                            return;
                          person7 = this.players[(int) playerOffset];
                        }
                        if (person7.controlled.Count <= (int) selectedIndex)
                          return;
                        ZCreature cre = person7.controlled[(int) selectedIndex];
                        if (cre.position.x != validPos7.x || cre.position.y != validPos7.y)
                          this.SendResyncMsg(p, "invalid position on controlled jump #id" + (object) moveID + ": " + (object) validPos7 + " expected: " + (object) cre.position, onTrue: (Action) (() =>
                          {
                            if (this.loggedToDiscord)
                              return;
                            this.loggedToDiscord = true;
                            Server.Instance?.communicator?.SendResync(this, "Invalid position on controlled jump #id " + (object) moveID + ": " + (object) validPos7 + " expected: " + (object) cre.position + " inwater: " + cre.inWater.ToString());
                          }));
                        else if (cre.minerMarket == null || !cre.minerMarket.Has(MinerMarket.Types.Platinum_Climbing_Hooks))
                        {
                          this.SendResyncMsg(p, "This minion cannot use controlled jumps :(");
                        }
                        else
                        {
                          if (ZComponent.IsNull((ZComponent) cre) || cre.stunned || !this.AllowMovement || p.InWater() && cre.type != CreatureType.Recall_Device || !fromServer && !p.yourTurn || !cre.canMove && !cre.flying || !cre.controllable || cre.InDarkTotem() || (ZComponent) cre.tower != (object) null)
                            return;
                          this.serverState.busy = ServerState.Busy.Moving;
                          cre.parent.movedThisTurn = true;
                          cre.OnMoved();
                          if (cre.pawn || !cre.flying || cre.entangledOrGravity)
                            cre.animator?.Play(AnimateState.Jump);
                          if (!this.isClient)
                            this.SendAllMessage(bites7);
                          else if (CameraMovement.FOLLOWTARGETS && (ZComponent) CameraMovement.Instance.target != (object) cre.transform && !CameraMovement.Instance.IsfollowingBee(cre))
                            CameraMovement.Instance.LerpToTransform(cre);
                          if ((double) cre.transformscale != (double) scaleX6)
                            cre.SetScale(scaleX6);
                          cre.velocity = Inert.Velocity(rot_z1, cre.GetHighJumpY(!ExtraMoveBits.NoIceJump((int) extraMoveBits)) + (cre.minerMarket.Has(MinerMarket.Types.Light) ? 1 : 0));
                          cre.DoControlledJump((int) extraMoveBits);
                          cre.sprinting = 0;
                        }
                      }
                    }));
                    break;
                  case 220:
                    ZPerson person8 = p;
                    byte selectedZSpell = myBinaryReader.ReadByte();
                    FixedInt rot_z2 = (FixedInt) myBinaryReader.ReadInt64();
                    float scaleX7 = myBinaryReader.ReadBoolean() ? 1f : -1f;
                    MyLocation target = myBinaryReader.ReadMyLocation();
                    FixedInt power = (FixedInt) myBinaryReader.ReadInt64();
                    bool extended = myBinaryReader.ReadBoolean();
                    myBinaryReader.ReadInt32();
                    MyLocation validPos8 = myBinaryReader.ReadMyLocation();
                    MyLocation secTarget = myBinaryReader.ReadMyLocation();
                    power = Mathd.Clamp(power, (FixedInt) 10485L, (FixedInt) 1);
                    byte[] bites8 = this.ServerCopyByteArray(b);
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      person8.lastMoveID = moveID;
                      p.inactiveTurns = 0;
                      if (!fromServer && !p.yourTurn || p.controlled.Count == 0)
                      {
                        this.SendResyncMsg(p, "was not your turn - When casting a spell");
                      }
                      else
                      {
                        if (this.isTeam)
                        {
                          if ((int) playerOffset >= this.players.Count || this.players[(int) playerOffset].team != p.team || (int) playerOffset != (int) p.id && selectedIndex == (byte) 0)
                          {
                            this.SendResyncMsg(p, "invalid team - When casting a spell");
                            return;
                          }
                          person8 = this.players[(int) playerOffset];
                        }
                        if (person8.controlled.Count <= (int) selectedIndex)
                        {
                          this.SendResyncMsg(p, "minion index mismatch - When casting a spell");
                        }
                        else
                        {
                          ZCreature creature = person8.controlled[(int) selectedIndex];
                          if (!creature.inWater && creature.spells.Count <= (int) selectedZSpell)
                          {
                            this.SendResyncMsg(p, "server spell count does not match");
                          }
                          else
                          {
                            if (!creature.controllable)
                              return;
                            SpellSlot spellSlot = creature.inWater ? creature.GetAvailableGate(ref selectedZSpell) ?? Inert.Instance.waterGate : creature.spells[(int) selectedZSpell];
                            Spell spell = spellSlot.spell;
                            if ((UnityEngine.Object) spell == (UnityEngine.Object) null)
                            {
                              this.SendResyncMsg(p, "spell is null - When casting a spell");
                            }
                            else
                            {
                              int localTurn = creature.parent.localTurn;
                              if (spellSlot.MaxUses >= 0 && spellSlot.UsedUses >= spellSlot.MaxUses && !creature.inWater)
                                this.SendResyncMsg(p, "uses mismatch - When casting a spell");
                              else if (spellSlot.TurnsTillFirstUse > localTurn && !creature.inWater)
                                this.SendResyncMsg(p, "not enough turns have passed to cast this spell");
                              else if ((spellSlot.LastTurnFired == localTurn && (!creature.flying || !spell.spellEnum.IsFlight()) || (spellSlot.RechargeTime > 0 || spellSlot.LastTurnFired > localTurn) && spellSlot.LastTurnFired + spellSlot.RechargeTime >= localTurn && (!spellSlot.spell.spellEnum.IsFlight() || !creature.flying) && (spellSlot.TurnsTillFirstUse > localTurn || spellSlot.LastTurnFired > -1)) && !creature.inWater)
                              {
                                this.SendResyncMsg(p, "unable to cast this turn " + (object) spellSlot.spell + " ltf: " + (object) spellSlot.LastTurnFired + " flying: " + creature.flying.ToString() + " recharge: " + (object) spellSlot.RechargeTime + " curTurn: " + (object) localTurn + " ttfs: " + (object) spellSlot.TurnsTillFirstUse);
                              }
                              else
                              {
                                if (!creature.inWater)
                                {
                                  if (creature.phantom && spell.bookOf != BookOf.Arcane && spell.bookOf != BookOf.Illusion)
                                  {
                                    this.SendResyncMsg(p, "spell unavailable when phantom");
                                    return;
                                  }
                                  if (creature.race == CreatureRace.Undead && !creature.pawn)
                                  {
                                    if (spell.bookOf != BookOf.Arcane && spell.bookOf != BookOf.Underdark || spell.type == CastType.Tower)
                                    {
                                      this.SendResyncMsg(p, "spell unavailable when undead");
                                      return;
                                    }
                                  }
                                  else if (creature.shiningPower)
                                  {
                                    if (spell.bookOf != BookOf.Arcane && spell.bookOf != BookOf.OverLight)
                                    {
                                      this.SendResyncMsg(p, "spell unavailable when an angel");
                                      return;
                                    }
                                  }
                                  else if (spell.type == CastType.Tower && (creature.flying || (ZComponent) creature.mount != (object) null || creature.radius > 30))
                                  {
                                    this.SendResyncMsg(p, "tower not valid in the current state");
                                    return;
                                  }
                                  if (!creature.inWater && spellSlot.disabledturn >= creature.parent.localTurn)
                                  {
                                    this.SendResyncMsg(p, "spell is disabled");
                                    return;
                                  }
                                }
                                if (ClickSpell.Level3(spell, spell.spellEnum, creature, spellSlot) != 0 && !creature.inWater)
                                  this.SendResyncMsg(p, "spell could not be cast");
                                else if (this.ArcaneZero && (spell.damage > 0 || spell.spellEnum == SpellEnum.Snowball) && !creature.inWater)
                                  this.SendResyncMsg(p, "Zero shield was active (damaging spell)");
                                else if (creature.position.x != validPos8.x || creature.position.y != validPos8.y)
                                {
                                  this.SendResyncMsg(p, "invalid position when casting: " + (object) validPos8 + " expected: " + (object) creature.position, onTrue: (Action) (() =>
                                  {
                                    if (this.loggedToDiscord)
                                      return;
                                    this.loggedToDiscord = true;
                                    Server.Instance?.communicator?.SendResync(this, "Invalid position: " + (object) validPos8 + " expected: " + (object) creature.position + " inwater: " + creature.inWater.ToString() + " spell: " + (object) spell.spellEnum + " (" + (object) selectedZSpell + ") Busy: " + (object) this.serverState.busy + " turn time: " + (object) this.serverState.turnTime);
                                  }));
                                }
                                else
                                {
                                  if (selectedIndex != (byte) 0 && p.InWater() && creature.type != CreatureType.Recall_Device)
                                    return;
                                  if ((double) creature.transformscale != (double) scaleX7)
                                    creature.SetScale(scaleX7);
                                  if (!this.isClient)
                                  {
                                    if (!ZSpell.ServerCanFire(spell, (int) target.x, (int) target.y, (int) secTarget.x, (int) secTarget.y, creature, rot_z2, power, new MyLocation(target.x, target.y)))
                                    {
                                      this.SendResyncMsg(p, "spell could not be fired");
                                      return;
                                    }
                                    this.SendAllMessage(bites8);
                                  }
                                  if (creature.inWater)
                                  {
                                    if (!this.First_Turn_Teleport || creature.parent.localTurn > 0)
                                    {
                                      if ((spell.spellEnum == SpellEnum.Arcane_Gate || spell.spellEnum == SpellEnum.Santas_Magic) && (spellSlot.LastTurnFired > localTurn - 5 && spellSlot.LastTurnFired > -1 || creature.parent.arcaneGateSpellSlot == null))
                                      {
                                        if (creature.health < 8)
                                        {
                                          creature.DoDamage(5);
                                          creature.UpdateHealthTxt();
                                          if (creature.health <= 0)
                                          {
                                            this.with_the_fishes = true;
                                            if (this.isClient && Global.GetPrefBool("prefdeathmsg", true))
                                              ChatBox.Instance?.NewChatMsg("", Descriptions.GetDrownMessage(creature), (Color) ColorScheme.GetColor(Global.ColorWhiteText), "", ChatOrigination.System);
                                            creature.OnDeath(true);
                                          }
                                        }
                                        else
                                        {
                                          creature.health = (int) ((FixedInt) creature.health * (FixedInt) 692060L);
                                          creature.UpdateHealthTxt();
                                        }
                                      }
                                      spellSlot.SetTurnFired = localTurn;
                                      Inert.Instance.waterGate.SetTurnFired = localTurn;
                                    }
                                  }
                                  else
                                  {
                                    if (!this.isSandbox || this.isTutorial)
                                      spellSlot.IncreaseUses();
                                    if (!spellSlot.spell.spellEnum.IsFlight() || !creature.flying)
                                    {
                                      if (!spellSlot.EndsTurn || spellSlot.RechargeTime > 0)
                                        spellSlot.SetTurnFired = localTurn;
                                    }
                                    else
                                      spellSlot.SetTurnFired = Mathf.Max(localTurn - 4, spellSlot.LastTurnFired);
                                  }
                                  if (spellSlot.EndsTurn)
                                  {
                                    this.serverState.busy = ServerState.Busy.Between_Turns;
                                    p.yourTurn = false;
                                  }
                                  else
                                  {
                                    this.serverState.busy = ServerState.Busy.Moving;
                                    p.movedThisTurn = true;
                                  }
                                  if (creature.fusion > 0)
                                  {
                                    creature.fusion = 0;
                                    creature.UpdateHealthTxt();
                                  }
                                  rot_z2 = creature.ClampAngle(rot_z2);
                                  FixedInt force = (FixedInt) (creature.radius - 5);
                                  MyLocation position = creature.position;
                                  if (spell.type == CastType.Naplem)
                                    power = (FixedInt) 0;
                                  if (extended && spell.type != CastType.Placement && spell.type != CastType.Target_Placement)
                                  {
                                    rot_z2 = Player.ClampAngle(rot_z2);
                                    Player.PowerLevel powerLevel = Player.GetPowerLevel(rot_z2);
                                    power += Player.PowerExtend(spell.type, powerLevel, power);
                                    force += Player.LinearExtend(powerLevel, (FixedInt) creature.radius);
                                    if (spell.type == CastType.Flash)
                                    {
                                      if (spell.spellEnum == SpellEnum.Arcane_Flash)
                                        position += Global.VelocityRight(rot_z2, force);
                                      else
                                        position += Global.VelocityRight(rot_z2, force / 6);
                                    }
                                  }
                                  if (spell.type == CastType.Naplem || spell.type == CastType.Flash || spell.type == CastType.Power || spell.type == CastType.Target_Power || spell.type == CastType.Double_Naplem)
                                    position += Global.VelocityRight(rot_z2, force);
                                  if (creature.invulnerable > 0 && creature.invulnerable < 1000)
                                    creature.invulnerable = 0;
                                  for (int index = this.windShieldEffectors.Count - 1; index >= 0; --index)
                                  {
                                    if (ZComponent.IsNull((ZComponent) this.windShieldEffectors[index]))
                                      this.windShieldEffectors.RemoveAt(index);
                                    else
                                      this.windShieldEffectors[index].SetWindShieldActive();
                                  }
                                  if (spellSlot.EndsTurn && !this.resyncing)
                                    this.ongoing.RunCoroutine(this.castWait(110f));
                                  if (creature.fusion > 0)
                                  {
                                    creature.fusion = 0;
                                    creature.UpdateHealthTxt();
                                  }
                                  if (this.isClient && !this.resyncing && (spellSlot.EndsTurn || (UnityEngine.Object) Player.Instance == (UnityEngine.Object) null || !Player.Instance.person.yourTurn))
                                    HUD.instance.CastSpell(spell, creature);
                                  if (this.isClient && !this.resyncing && HUD.instance.FollowSpells)
                                  {
                                    CameraMovement.FOLLOWTARGETS = true;
                                    CameraMovement.Instance.activeTarget = (IFollowTarget) null;
                                    CameraMovement.followTargets.Clear();
                                    if (spell.type == CastType.Placement || spell.type == CastType.Blit)
                                      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowPosition(target.ToSinglePrecision()));
                                    else if (spell.type == CastType.Tower || spell.type == CastType.Flight)
                                      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowPosition(creature.position.ToSinglePrecision()));
                                  }
                                  this.forceRysncPause = true;
                                  person8.curDamageDealtInOneAttack = 0;
                                  person8.awards.OnCasted(spell);
                                  this.lastSpellCast = spell.spellEnum;
                                  this.portalUsedThisSpellTurn = false;
                                  this.sinksThisTurn = 0;
                                  ++person8.spellsCastTHISTurn;
                                  if (!creature.isPawn)
                                    person8.IncreaseCastCount(spell.spellEnum);
                                  ZSpell.FireWhich(spell, creature, position, rot_z2, power, target, secTarget, (int) selectedZSpell, extended);
                                  if (this.isTutorial && Client._tutorial.onCast != null)
                                    Client._tutorial.onCast(new ContainerCreature(creature), new ContainerSpell(spellSlot));
                                  if (!spellSlot.isPresent)
                                    return;
                                  spellSlot.IncreaseUses();
                                  creature?.spells.RemoveAt((int) selectedZSpell);
                                }
                              }
                            }
                          }
                        }
                      }
                    }));
                    break;
                }
              }
            }
          }
          else if (this.isClient)
          {
            switch (num1)
            {
              case 1:
                if (this.isReplay)
                  break;
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                  using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream2))
                  {
                    if (this.isSpectator)
                    {
                      if (Client.game != null)
                        Client.game.resyncing = true;
                      myBinaryWriter.Write((byte) 37);
                      myBinaryWriter.Write(Client.Name);
                    }
                    else
                      myBinaryWriter.Write((byte) 1);
                    myBinaryWriter.Write(Client.cryp);
                  }
                  if (Spectator.isConnected)
                  {
                    Spectator.connection.SendBytes(Client.FirstEncrypt(memoryStream2.ToArray()));
                    break;
                  }
                  Client.connection.SendBytes(memoryStream2.ToArray());
                  break;
                }
              case 7:
                p = Client.game.players[(int) myBinaryReader.ReadByte()];
                p.didLeave = false;
                p.panelPlayer?.Rejoined();
                break;
              case 10:
                if (Global.GetPrefBool("prefsavereplay", true))
                  this.SaveReplay();
                Controller.Instance.DestroyMap();
                Client.SyncLobby(myBinaryReader);
                break;
              case 13:
                this.serverState.busy = ServerState.Busy.Ended;
                bool[] alive = new bool[this.players.Count];
                for (int index = 0; index < this.players.Count; ++index)
                  alive[index] = myBinaryReader.ReadBoolean();
                string msg = myBinaryReader.ReadString();
                foreach (ZPerson player in this.players)
                  player.gainedWands = myBinaryReader.ReadInt32();
                bool aWinner = false;
                UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
                {
                  for (int index = 0; index < this.players.Count; ++index)
                  {
                    if (alive[index])
                    {
                      HUD.instance.uiPlayerCharacters[index].Won();
                      HUD.instance.panelStartColor.color = this.players[index].clientColor;
                      aWinner = true;
                    }
                    else
                      HUD.instance.uiPlayerCharacters[index].Dead();
                    if (this.players[index].gainedWands > 0 && string.Equals(this.players[index].name, Client.Name))
                      ChatBox.Instance?.NewChatMsg("You received <color=white>" + (object) this.players[index].gainedWands + "</color> wand" + (this.players[index].gainedWands > 1 ? (object) "s!" : (object) "!"), (Color) ColorScheme.GetColor(Global.ColorSystem));
                    HUD.instance.uiPlayerCharacters[index].ShowScrolls(this.players[index].gainedWands);
                  }
                  if (!aWinner)
                    HUD.instance.panelStartColor.color = new Color(0.245283f, 0.245283f, 0.245283f, 0.9019f);
                  HUD.instance.panelPause.SetActive(false);
                  HUD.instance.buttonText.text = "Return to Lobby";
                  HUD.instance.awardsText.text = msg;
                  HUD.instance.button.onClick.RemoveAllListeners();
                  HUD.instance.button.onClick.AddListener(new UnityAction(HUD.instance.ClickSaveExitNotForced));
                  HUD.instance.button.gameObject.SetActive(true);
                  if (!this.isReplay)
                    HUD.instance.buttonSaveGameOver.SetActive(true);
                  HUD.instance.ShowStartPanel();
                  HUD.instance.awardsText.gameObject.SetActive(true);
                  HUD.instance.HideEverything(true);
                  HUD.instance.InitQuiz();
                  if (this.isSpectator)
                    return;
                  for (int i = 0; i < this.players.Count; ++i)
                    HUD.instance.UpdateRematch(i);
                }));
                break;
              case 15:
                int index1 = (int) myBinaryReader.ReadByte();
                float num5 = myBinaryReader.ReadSingle();
                float num6 = myBinaryReader.ReadSingle();
                if (this.serverState.busy != ServerState.Busy.No && this.serverState.busy != ServerState.Busy.Moving)
                  break;
                if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person.yourTurn)
                {
                  if ((double) num5 > (double) this.serverState.turnTime + 1.0)
                    this.serverState.turnTime = num5 - 0.95f;
                  if ((double) num6 >= (double) Player.Instance.person.countdown)
                    break;
                  Player.Instance.person.countdown = num6;
                  break;
                }
                if ((double) Math.Abs(num5 - this.serverState.turnTime) > 0.5)
                  this.serverState.turnTime = num5;
                if ((double) num6 >= (double) this.players[index1].countdown)
                  break;
                this.players[index1].countdown = num6;
                break;
              case 16:
                this.HandleMap(myBinaryReader);
                break;
              case 17:
                break;
              case 35:
                this.receivedInitialMsg = true;
                this.timeline.Clear();
                int num7 = myBinaryReader.ReadInt32();
                for (int index2 = 0; index2 < num7; ++index2)
                  this.timeline.Add(myBinaryReader.ReadBytes());
                if (this.isSpectator)
                {
                  this.resyncing = true;
                  this.resyncUpdate = Timing.RunCoroutine(this.Resync());
                  break;
                }
                this.init_Resync();
                break;
              case 64:
                this.receivedInitialMsg = true;
                int index3 = myBinaryReader.ReadInt32();
                int num8 = myBinaryReader.ReadInt32();
                this.timeline.RemoveRange(index3, this.timeline.Count - index3);
                for (int index4 = 0; index4 < num8; ++index4)
                  this.timeline.Add(myBinaryReader.ReadBytes());
                string str1 = myBinaryReader.ReadString();
                if (!string.IsNullOrEmpty(str1))
                  ChatBox.Instance?.NewChatMsg("", "Resync because - " + str1, (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
                bool flag1 = myBinaryReader.ReadBoolean();
                try
                {
                  if (flag1)
                  {
                    this.serverState.playersTurn = myBinaryReader.ReadByte();
                    if (myBinaryReader.ReadInt32() == this.map._pastBits.Count)
                    {
                      int num9 = myBinaryReader.ReadInt32();
                      if (num9 == this.players.Count)
                      {
                        for (int index5 = 0; index5 < num9; ++index5)
                        {
                          this.players[index5].yourTurn = myBinaryReader.ReadBoolean();
                          List<ZCreature> controlled = this.players[index5].controlled;
                          int num10 = myBinaryReader.ReadInt32();
                          if (num10 == controlled.Count)
                          {
                            for (int index6 = 0; index6 < num10; ++index6)
                            {
                              bool flag2 = myBinaryReader.ReadBoolean();
                              if (flag2 == ((ZComponent) controlled[index6] != (object) null))
                              {
                                if (flag2)
                                {
                                  controlled[index6].KillMovement();
                                  MyLocation pos = myBinaryReader.ReadMyLocation();
                                  controlled[index6].position = pos;
                                  controlled[index6].collider?.Move(pos);
                                  controlled[index6].health = myBinaryReader.ReadInt32();
                                  ZCreature rider = controlled[index6].rider;
                                  controlled[index6].rider = (ZCreature) null;
                                  controlled[index6].SetScale(myBinaryReader.ReadSingle());
                                  controlled[index6].rider = rider;
                                  controlled[index6].retribution = myBinaryReader.ReadInt32();
                                  int num11 = myBinaryReader.ReadInt32();
                                  if (num11 > 0)
                                    controlled[index6].CreateProtectionShield();
                                  controlled[index6].shield = num11;
                                  bool flag3 = myBinaryReader.ReadBoolean();
                                  controlled[index6].stunned = flag3;
                                  if (flag3)
                                    controlled[index6].OnStunned();
                                  if (myBinaryReader.ReadBoolean())
                                  {
                                    int index7 = myBinaryReader.ReadInt32();
                                    int index8 = myBinaryReader.ReadInt32();
                                    controlled[index6].mount = this.players[index7].controlled[index8];
                                    controlled[index6].mount.rider = controlled[index6];
                                  }
                                  else if ((ZComponent) controlled[index6].mount != (object) null)
                                  {
                                    controlled[index6].mount.rider = (ZCreature) null;
                                    controlled[index6].mount = (ZCreature) null;
                                  }
                                  if (myBinaryReader.ReadBoolean())
                                  {
                                    controlled[index6].tower.SetPositionResync(myBinaryReader.ReadMyLocation());
                                    controlled[index6].tower.Health = myBinaryReader.ReadInt32();
                                  }
                                  int num12 = myBinaryReader.ReadInt32();
                                  if (controlled[index6].effectors.Count == num12)
                                  {
                                    for (int index9 = 0; index9 < num12; ++index9)
                                    {
                                      bool flag4 = myBinaryReader.ReadBoolean();
                                      if (flag4 == ((ZComponent) controlled[index6].effectors[index9] != (object) null))
                                      {
                                        if (flag4)
                                        {
                                          controlled[index6].effectors[index9].position = myBinaryReader.ReadMyLocation();
                                          controlled[index6].effectors[index9].active = myBinaryReader.ReadBoolean();
                                          controlled[index6].effectors[index9].variable = myBinaryReader.ReadInt32();
                                          controlled[index6].effectors[index9].VisualUpdate();
                                        }
                                      }
                                      else
                                        goto label_136;
                                    }
                                    int num13 = myBinaryReader.ReadInt32();
                                    if (controlled[index6].destroyableEffectors.Count == num13)
                                    {
                                      for (int index10 = 0; index10 < num13; ++index10)
                                      {
                                        bool flag5 = myBinaryReader.ReadBoolean();
                                        if (flag5 == ((ZComponent) controlled[index6].destroyableEffectors[index10] != (object) null))
                                        {
                                          if (flag5)
                                          {
                                            controlled[index6].destroyableEffectors[index10].position = myBinaryReader.ReadMyLocation();
                                            controlled[index6].destroyableEffectors[index10].active = myBinaryReader.ReadBoolean();
                                            controlled[index6].destroyableEffectors[index10].variable = myBinaryReader.ReadInt32();
                                            controlled[index6].destroyableEffectors[index10].VisualUpdate();
                                          }
                                        }
                                        else
                                          goto label_136;
                                      }
                                      int num14 = myBinaryReader.ReadInt32();
                                      if (controlled[index6].followingColliders.Count == num14)
                                      {
                                        for (int index11 = 0; index11 < num14; ++index11)
                                        {
                                          bool flag6 = myBinaryReader.ReadBoolean();
                                          if (flag6 == ((ZComponent) controlled[index6].followingColliders[index11] != (object) null))
                                          {
                                            if (flag6)
                                              controlled[index6].followingColliders[index11].Move(myBinaryReader.ReadMyLocation());
                                            if (flag6 && (ZComponent) controlled[index6].followingColliders[index11].effector != (object) null)
                                            {
                                              controlled[index6].followingColliders[index11].effector.position = controlled[index6].followingColliders[index11].position;
                                              controlled[index6].followingColliders[index11].effector.active = myBinaryReader.ReadBoolean();
                                              controlled[index6].followingColliders[index11].effector.variable = myBinaryReader.ReadInt32();
                                              controlled[index6].followingColliders[index11].effector.VisualUpdate();
                                            }
                                          }
                                          else
                                            goto label_136;
                                        }
                                        controlled[index6].UpdateHealthTxt();
                                      }
                                      else
                                        goto label_136;
                                    }
                                    else
                                      goto label_136;
                                  }
                                  else
                                    goto label_136;
                                }
                              }
                              else
                                goto label_136;
                            }
                          }
                          else
                            goto label_136;
                        }
                        int num15 = myBinaryReader.ReadInt32();
                        if (this.globalEffectors.Count == num15)
                        {
                          for (int index12 = 0; index12 < num15; ++index12)
                          {
                            bool flag7 = myBinaryReader.ReadBoolean();
                            if (flag7 == ((ZComponent) this.globalEffectors[index12] != (object) null))
                            {
                              if (flag7)
                              {
                                this.globalEffectors[index12].active = myBinaryReader.ReadBoolean();
                                this.globalEffectors[index12].variable = myBinaryReader.ReadInt32();
                                this.globalEffectors[index12].VisualUpdate();
                              }
                            }
                            else
                              goto label_136;
                          }
                          ChatBox.Instance?.NewChatMsg("", "Fast Resync Successful.", (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
                          break;
                        }
                      }
                    }
                  }
                }
                catch (Exception ex)
                {
                  Debug.LogError((object) ex);
                }
label_136:
                if (this.isSpectator)
                {
                  this.resyncing = true;
                  this.resyncUpdate = Timing.RunCoroutine(this.Resync());
                  break;
                }
                this.init_Resync();
                break;
              case 73:
                Client.cosmetics = Cosmetics.Deserialize(myBinaryReader);
                Client.bonusPrestige = myBinaryReader.ReadSingle();
                Client.MyAccount.cosmetics = Client.cosmetics;
                break;
              case 77:
                Client.RecieveShare(myBinaryReader);
                break;
              case 83:
                this.resyncOnError = false;
                switch (myBinaryReader.ReadByte())
                {
                  case 1:
                    int id1 = myBinaryReader.ReadInt32();
                    Account other = new Account();
                    other.Deserialize(myBinaryReader);
                    Client._accounts[other.name] = other;
                    if (string.Equals(other.name, Client.Name))
                      Client.MyAccount.Copy(other);
                    SettingsPlayer sp = new SettingsPlayer();
                    sp.DeserializeBasicOutfit(myBinaryReader);
                    Vector2 pos3 = myBinaryReader.ReadVector2();
                    BoatSpectators.Create(other.name);
                    BoatSpectators.Instance.CreateCharacter(id1, other.name, (Vector3) pos3, sp);
                    return;
                  case 2:
                    BoatSpectators.Instance?.Remove(myBinaryReader.ReadInt32());
                    return;
                  case 3:
                    BoatSpectators.Instance?.MoveCharacter(myBinaryReader.ReadInt32(), (int) myBinaryReader.ReadInt16(), (int) myBinaryReader.ReadInt16());
                    return;
                  case 4:
                    myBinaryReader.ReadInt32();
                    return;
                  case 5:
                    return;
                  case 6:
                    BoatSpectators.Instance?.OnEmoji(myBinaryReader.ReadInt32(), myBinaryReader.ReadInt32());
                    return;
                  default:
                    return;
                }
              case 92:
                string t1 = myBinaryReader.ReadString();
                FixedInt angle1 = (FixedInt) myBinaryReader.ReadInt64();
                MyLocation pos1 = myBinaryReader.ReadMyLocation();
                this.MoveQue.Enqueue((Action) (() => Client.DevConsole(t1, this, new FixedInt?(angle1), new MyLocation?(pos1))));
                break;
              case 98:
                int key = myBinaryReader.ReadInt32();
                if (!Client._games.Remove(key))
                  break;
                UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
                {
                  if (!((UnityEngine.Object) LobbyMenu.instance != (UnityEngine.Object) null))
                    return;
                  LobbyMenu.instance.RefreshGames();
                }));
                break;
              case 100:
                try
                {
                  if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
                  {
                    if (!Player.Instance.person.sendResync)
                    {
                      if (this.timeline.Count > 0)
                      {
                        if (!this.isReplay)
                        {
                          if (Server.DebugResyncs)
                          {
                            Player.Instance.person.sendResync = true;
                            Client.connection.SendBytes(DiscordCommunicator.GameToResync(this, "Client responding to resync", Client.Name, true));
                          }
                        }
                      }
                    }
                  }
                }
                catch (Exception ex)
                {
                }
                this.receivedInitialMsg = true;
                GameFacts gf = new GameFacts();
                gf.ManualDeserialize(myBinaryReader, true, true);
                int index13 = myBinaryReader.ReadInt32();
                myBinaryReader.ReadInt32();
                int num16 = myBinaryReader.ReadInt32();
                if (index13 == 0)
                  this.timeline.Clear();
                else
                  this.timeline.RemoveRange(index13, this.timeline.Count - index13);
                for (int index14 = 0; index14 < num16; ++index14)
                  this.timeline.Add(myBinaryReader.ReadBytes());
                byte[] data1 = myBinaryReader.ReadBytes();
                this.init_Deserialize(gf, this.timeline, data1, true);
                break;
              case 151:
                string RightClickName1 = myBinaryReader.ReadString();
                string str2 = myBinaryReader.ReadString();
                string msg1 = myBinaryReader.ReadString();
                if (msg1.Length > 600 || str2.Length > 200 || RightClickName1.Length > 200)
                  break;
                ChatBox.Instance?.NewChatMsg("From " + RightClickName1, msg1, (Color) ColorScheme.GetColor(Global.ColorReceivedPrivate), RightClickName1, ChatOrigination.Private);
                break;
              case 152:
                string RightClickName2 = myBinaryReader.ReadString();
                string msg2 = myBinaryReader.ReadString();
                if (msg2.Length > 600 || RightClickName2.Length > 200)
                  break;
                ChatBox.Instance.NewChatMsg("[Team] " + RightClickName2, msg2, (Color) ColorScheme.GetColor(Global.ColorTeamText), RightClickName2, ChatOrigination.Team);
                break;
              case 153:
                string str3 = myBinaryReader.ReadString();
                string msg3 = myBinaryReader.ReadString();
                if (str3.Length > 200 || msg3.Length > 600)
                  break;
                ChatBox.Instance.NewChatMsg(str3, msg3, (Color) ColorScheme.GetColor(Global.ColorGameText), str3, ChatOrigination.Game);
                break;
              case 154:
                int index15 = myBinaryReader.ReadInt32();
                int type = myBinaryReader.ReadInt32();
                Vector2 pos4 = myBinaryReader.ReadVector2();
                string name = this.players[index15].name;
                if (this.resyncing || Client.IsIgnored(name))
                  break;
                ClientResources.Instance.CreatePing(type, name, this.players[index15].clientColor, pos4);
                break;
              case 155:
                Quickchat.Data data2 = Quickchat.Data.Deserialize(myBinaryReader);
                string command = Quickchat.TryGetCommand(data2.id, data2.options);
                if (command == null)
                  break;
                ChatBox.Instance.NewChatMsg(Quickchat.GetDestination(data2.destination) + "<sprite name=\"Emoji2_1352\"> " + data2.name, command, (Color) ((UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null ? ColorScheme.GetColor(Global.ColorGameText) : ColorScheme.GetColor(data2.destination)), data2.name, data2.destination);
                break;
              case 157:
                int index16 = myBinaryReader.ReadInt32();
                int index17 = myBinaryReader.ReadInt32();
                ZCreature zcreature1 = this.players[index16].first();
                if (zcreature1 == null)
                  break;
                zcreature1.clientObj?.OnEmoji(index17);
                break;
              case 191:
                bool flag8 = myBinaryReader.ReadBoolean();
                if (this.serverState.busy == ServerState.Busy.Ended)
                  break;
                if (this.isMulti)
                {
                  using (List<ZPerson>.Enumerator enumerator = this.GetTeam(p.team).GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      ZPerson current = enumerator.Current;
                      if (current.controlled.Count > 0 || !flag8)
                        current.panelPlayer.Resigned();
                      if (flag8)
                        current.panelPlayer.Left();
                    }
                    break;
                  }
                }
                else
                {
                  if (p.controlled.Count > 0 || !flag8)
                    p.panelPlayer.Resigned();
                  if (!flag8)
                    break;
                  p.panelPlayer.Left();
                  break;
                }
              case 192:
                for (int index18 = 0; index18 < this.players.Count; ++index18)
                  this.players[index18].bid = myBinaryReader.ReadInt32();
                int index19 = myBinaryReader.ReadInt32();
                if (!this.isTeam && this.players.Count > 2)
                {
                  this.SortByBidFFA();
                  break;
                }
                if (index19 == -1 || !((ZComponent) this.players[index19].first() != (object) null) || this.players[index19].bid <= 0)
                  break;
                this.players[index19].first().health -= this.players[index19].bid;
                this.players[index19].first().UpdateHealthTxt();
                break;
              case 193:
              case 195:
                if (this.isMulti)
                {
                  foreach (ZPerson zperson in this.GetTeam(p.team))
                  {
                    ZPerson x = zperson;
                    if (x.controlled.Count > 0 && this.serverState.busy != ServerState.Busy.Ended)
                    {
                      x.panelPlayer.Resigned();
                      if (num1 == (byte) 193)
                        x.panelPlayer.Left();
                      x.didResign = true;
                      this.MoveQue.Enqueue((Action) (() => this.resigned.Add(x)));
                    }
                    else
                      p.panelPlayer.left = true;
                  }
                }
                else if (p.controlled.Count > 0 && this.serverState.busy != ServerState.Busy.Ended)
                {
                  p.panelPlayer.Resigned();
                  if (num1 == (byte) 193)
                    p.panelPlayer.Left();
                  p.didResign = true;
                  this.MoveQue.Enqueue((Action) (() => this.resigned.Add(p)));
                }
                else
                  p.panelPlayer.left = true;
                if (this.isSpectator || this.serverState.busy != ServerState.Busy.Ended)
                  break;
                HUD.instance.UpdateRematch((int) p.id);
                break;
              case 194:
                p.offeringDraw = myBinaryReader.ReadBoolean();
                if (this.isMulti)
                {
                  foreach (ZPerson zperson in this.GetTeam(p.team))
                    zperson.offeringDraw = p.offeringDraw;
                }
                HUD.instance.UpdateDraws();
                break;
              case 196:
                if (this.isSpectator)
                  break;
                p.offeringRematch = myBinaryReader.ReadByte() != (byte) 0;
                if (this.isMulti)
                {
                  foreach (ZPerson zperson in this.GetTeam(p.team))
                    zperson.offeringRematch = p.offeringRematch;
                }
                HUD.instance.UpdateRematch((int) p.id);
                break;
              case 197:
                ZCreature zcreature2 = p.first();
                if (ZComponent.IsNull((ZComponent) zcreature2) || zcreature2.isDead)
                  break;
                SettingsPlayer settingsPlayer1 = new SettingsPlayer();
                settingsPlayer1.Deserialize(myBinaryReader);
                p.settingsPlayer.CopyOutfit(settingsPlayer1);
                if (!zcreature2.FullArcane)
                {
                  p.clientColor = (int) settingsPlayer1.indexBody != SettingsPlayer.sno_body2 ? TeamColors.GetColor((int) p.id) : new Color(0.5f, 0.5f, 0.5f);
                  zcreature2.gameObject.GetComponent<ConfigurePlayer>().EquipAll(p.name, p.settingsPlayer);
                  p.panelPlayer.Init(p.name, p.settingsPlayer, p.clientColor, zcreature2.clientObj, true);
                }
                else
                {
                  zcreature2.gameObject.GetComponent<ConfigurePlayer>().ModEquipAll(p.name, p.settingsPlayer, ClientResources.Instance.ModColors);
                  p.panelPlayer.Init(p.name, p.settingsPlayer, p.clientColor, zcreature2.clientObj, true);
                  p.panelPlayer.TurnMod(settingsPlayer1, ClientResources.Instance.ModColors[0], zcreature2.clientObj);
                }
                HUD.instance.uiPlayerCharacters[(int) p.id].Copy(p);
                zcreature2.bg.color = p.clientColor;
                zcreature2.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = p.clientColor;
                zcreature2.txtHealth.color = p.clientColor;
                zcreature2.ColorPrestigeHat();
                if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person == p)
                  ClickSpell.Instance.SetBGColor(p.clientColor);
                Inert.AddTorquingAndOrArchStaffs(p, zcreature2, settingsPlayer1, false);
                if (!this.resyncing && (UnityEngine.Object) zcreature2.transform != (UnityEngine.Object) null)
                {
                  UnityEngine.Object.Instantiate<GameObject>(Inert.Instance.fountain, zcreature2.transform.position, Quaternion.identity, zcreature2.game.GetMapTransform());
                  AudioManager.Play(ClientResources.Instance.clipChangeSpellBook);
                }
                foreach (ZCreature zcreature3 in p.controlled)
                {
                  if ((UnityEngine.Object) zcreature3.bg != (UnityEngine.Object) null)
                    zcreature3.bg.color = p.clientColor;
                  if ((UnityEngine.Object) zcreature3.miniMapBg != (UnityEngine.Object) null)
                    zcreature3.miniMapBg.color = p.clientColor;
                  if ((UnityEngine.Object) zcreature3.txtHealth != (UnityEngine.Object) null)
                    zcreature3.txtHealth.color = p.clientColor;
                }
                zcreature2.animator?.Play(AnimateState.Stop);
                break;
              case 198:
                int turn = this.turn;
                int t2 = myBinaryReader.ReadInt32();
                byte pt = num2;
                int lastPlayersTurn = myBinaryReader.ReadInt32();
                float cd = myBinaryReader.ReadSingle();
                this.MoveQue.Enqueue((Action) (() =>
                {
                  ++this.totalTurnsCombined;
                  if (this.resigned.Count > 0)
                    this.RemoveResigned();
                  if (this.serverState.busy == ServerState.Busy.Ended)
                    return;
                  if ((int) this.serverState.playersTurn < this.players.Count)
                  {
                    this.OnEndTurn(this.players[(int) this.serverState.playersTurn]);
                    if (lastPlayersTurn < this.players.Count)
                      this.players[lastPlayersTurn].countdown = cd;
                  }
                  this.turn = t2;
                  this.serverState.busy = ServerState.Busy.Starting_Turn;
                  this.serverState.playersTurn = pt;
                  this.serverState.turnTime = 0.0f;
                  Player.Instance?.UpdateVisuals();
                  this.NextTurn(p);
                  if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
                  {
                    Player.Instance.UnselectSpell();
                    if ((int) pt != Player.Instance.myTurn)
                      return;
                    Player.Instance.selectedCreatureIndex = 0;
                    Player.Instance.selectedCreaturePlayerOffset = (int) Player.Instance.person.id;
                    if (Player.Instance.person.controlled.Count > 0)
                    {
                      Player.Instance.selected = Player.Instance.person.controlled[0];
                      Player.Instance.UpdateVisuals();
                    }
                    if (!this.resyncing && Global.GetPrefBool("prefflashchatturn", true))
                      FlashWindow.instance.Flash();
                    if (this.resyncing || this.isReplay || this.players.Count <= 1)
                      return;
                    AudioManager.PlayTurnStart();
                  }
                  else
                    HUD.instance?.HideEverything(false);
                }));
                break;
              case 199:
                break;
              default:
                if (Client.GlobalHandler(num1, myBinaryReader) || Global.IsGameMsg(num1))
                  break;
                Client.LobbyHandler(b);
                break;
            }
          }
          else
          {
            switch (num1)
            {
              case 10:
                if (p.sendResync || !Server.DebugResyncs)
                  break;
                p.sendResync = true;
                Server.Instance?.communicator?.SendBytes(b);
                if (!((UnityEngine.Object) Server.Instance == (UnityEngine.Object) null) && Server.Instance.communicator != null && Server.Instance.communicator.GetConnection != null && Server.Instance.communicator.GetConnection.State == ConnectionState.Connected)
                  break;
                DiscordCommunicator.SaveReplayAnyway(myBinaryReader, p.name);
                break;
              case 17:
                if (p.ready)
                  break;
                p.ready = true;
                if (p.clientResyncing || p.connection == null || p.connection.player.gameNumber != this.gameFacts.id || p.connection == null || p.connection.State != ConnectionState.Connected)
                  break;
                for (int index20 = 0; index20 < this.timeline.Count; ++index20)
                  p.connection?.SendBytes(this.timeline[index20]);
                break;
              case 50:
                myBinaryReader.ReadString();
                Server.ChangeFriendsList(p.connection.player.account, myBinaryReader, b);
                break;
              case 55:
                Server.ReceiveReport(p.connection, p.name, myBinaryReader);
                break;
              case 57:
                myBinaryReader.ReadString();
                Server.ChangeChatSettings(p.connection.player.account, myBinaryReader);
                break;
              case 64:
                string str4 = myBinaryReader.ReadString();
                this.SendResyncMsg(p, "Client - " + str4);
                break;
              case 71:
                if (this.serverState.busy == ServerState.Busy.Not_started)
                  break;
                this.SendResyncMsg(p, "Connected late", false);
                break;
              case 77:
                Server.ValidateShare(p.connection, myBinaryReader, b, false);
                break;
              case 83:
                if (!((ZComponent) p.first() == (object) null) && !p.first().isDead && !this.resigned.Contains(p))
                  break;
                this.SpectatorLogic(p.connection, b, myBinaryReader);
                break;
              case 84:
                Server.HandleClanMsg(p.connection, myBinaryReader);
                break;
              case 87:
              case 88:
              case 89:
              case 90:
                Server.GlobalHandler(p.connection, myBinaryReader, b, num1);
                break;
              case 92:
                if (!p.connection.player.account.accountType.isDev() || !string.Equals(p.connection.name, "pur3 extreme"))
                  break;
                string t3 = myBinaryReader.ReadString();
                FixedInt angle2 = (FixedInt) myBinaryReader.ReadInt64();
                MyLocation pos2 = myBinaryReader.ReadMyLocation();
                this.MoveQue.Enqueue((Action) (() => Client.DevConsole(t3, this, new FixedInt?(angle2), new MyLocation?(pos2))));
                this.SendAllMessage(b);
                break;
              case 100:
                break;
              case 150:
                Server.ValidateClanChat(p.connection, myBinaryReader, b);
                break;
              case 151:
                string str5 = myBinaryReader.ReadString();
                if (!string.Equals(str5, p.name))
                  break;
                Server.ValidatePrivateChatMessage(str5, p.connection, myBinaryReader, b);
                break;
              case 152:
                string str6 = myBinaryReader.ReadString();
                string str7 = myBinaryReader.ReadString();
                if (str7.Length > 600 || str6.Length > 200 || !(str6 == p.name))
                  break;
                this.SendTeamMessage(b, p.team);
                MyLog.MainLog("[Team " + this.gameFacts.id.ToString() + "] [" + str6 + "] " + str7);
                p.GetMultiConnection?.OnChat("[Team " + this.gameFacts.id.ToString() + "] " + str7);
                break;
              case 153:
                string a = myBinaryReader.ReadString();
                string str8 = myBinaryReader.ReadString();
                if (str8.Length > 600 || a.Length > 200 || !string.Equals(a, p.name))
                  break;
                this.SendAllMessage(b);
                MyLog.MainLog("[Game " + this.gameFacts.id.ToString() + "] [" + a + "] " + str8);
                p.connection.OnChat("[Game #" + this.gameFacts.id.ToString() + "]" + str8);
                break;
              case 154:
                int num17 = myBinaryReader.ReadInt32();
                if (!this.isTeam || p.connection.player.lastShareMsg + 100L >= p.connection.stopwatch.ElapsedMilliseconds || num17 != (int) p.id)
                  break;
                p.connection.player.lastShareMsg = p.connection.stopwatch.ElapsedMilliseconds;
                this.SendTeamMessage(b, p.team);
                break;
              case 155:
                Server.ValidateQuickChat(p.connection, myBinaryReader, b, false);
                break;
              case 157:
                int index21 = myBinaryReader.ReadInt32();
                myBinaryReader.ReadInt32();
                if (index21 < 0 || index21 >= this.players.Count)
                  break;
                if (p.GetMultiConnection.player.account.discord == 0UL)
                {
                  Server.ReturnServerMsg(p.GetMultiConnection, "Verify your discord ID to enable using Emoji.");
                  break;
                }
                if (p.GetMultiConnection.player.account.accountType.IsMuted())
                {
                  Server.ReturnServerMsg(p.GetMultiConnection, "Looks like you're muted, try to behave yourself next time.");
                  break;
                }
                ZPerson player1 = this.players[index21];
                if ((int) player1.id != (int) p.id && (!this.isMulti || player1.team != p.team))
                  break;
                this.SendAllMessage(b);
                break;
              case 192:
                if (p.bid >= 0)
                  break;
                byte num18 = myBinaryReader.ReadByte();
                if ((int) num18 > (int) this.gameFacts.startHealth - 1)
                  num18 = (byte) ((uint) this.gameFacts.startHealth - 1U);
                p.bid = (int) num18;
                break;
              case 193:
              case 195:
                this.SendVisualResigned(p, num1 == (byte) 193);
                if (this.serverState.busy == ServerState.Busy.Ended)
                  this.SendLeft(p);
                if (p.controlled.Count > 0 && !this.resigned.Contains(p) && !p.didResign)
                {
                  p.didResign = true;
                  myBinaryReader.ReadInt32();
                  if (this.isMulti)
                  {
                    foreach (ZPerson zperson in this.GetTeam(p.team))
                      this.tempResignFix.Add(zperson);
                  }
                  else
                    this.tempResignFix.Add(p);
                  if (p.yourTurn)
                    this.MoveQue.Enqueue((Action) (() =>
                    {
                      if (p == null || !p.yourTurn)
                        return;
                      this.serverState.busy = ServerState.Busy.Between_Turns;
                      this.serverState.turnTime = this.PlayersMaxTurnTime + 3f;
                    }));
                }
                if (num1 != (byte) 193)
                  break;
                if (this.isMulti)
                {
                  using (List<ZPerson>.Enumerator enumerator = this.GetTeam(p.team).GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      ZPerson current = enumerator.Current;
                      current.didLeave = true;
                      current.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
                      current.connection.player.gameNumber = -1;
                      if (current.connection.State == ConnectionState.Connected)
                      {
                        Server.MovePlayer(current.connection, Location.Lobby);
                        Server.SyncLobby(current.connection);
                      }
                    }
                    break;
                  }
                }
                else
                {
                  p.didLeave = true;
                  p.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
                  p.connection.player.gameNumber = -1;
                  Server.MovePlayer(p.connection, Location.Lobby);
                  Server.SyncLobby(p.connection);
                  break;
                }
              case 194:
                if (!p.CanOfferDraw() || this.serverState.busy == ServerState.Busy.Ended || this.ieKillWait != null)
                  break;
                int num19 = p.offeringDraw ? 1 : 0;
                p.offeringDraw = myBinaryReader.ReadBoolean();
                int num20 = p.offeringDraw ? 1 : 0;
                if (num19 == num20)
                  break;
                if (this.isMulti)
                {
                  foreach (ZPerson zperson in this.GetTeam(p.team))
                    zperson.offeringDraw = p.offeringDraw;
                }
                if (p.offeringDraw)
                  this.CheckDraw(p);
                this.SendAllMessage(b);
                break;
              case 196:
                if (this.serverState.busy != ServerState.Busy.Ended)
                  break;
                int num21 = p.offeringRematch ? 1 : 0;
                p.offeringRematch = myBinaryReader.ReadByte() != (byte) 0;
                int num22 = p.offeringRematch ? 1 : 0;
                if (num21 == num22)
                  break;
                if (this.isMulti)
                {
                  foreach (ZPerson zperson in this.GetTeam(p.team))
                    zperson.offeringRematch = p.offeringRematch;
                }
                if (p.offeringRematch)
                  UnityThreadHelper.Dispatcher.Dispatch2((Action) (() => this.CheckRematch()));
                this.SendAllMessage(b);
                break;
              case 197:
                if (!p.account.cosmetics.achievements[(int) SettingsPlayer.Achievement_GameOutfit] || !p.yourTurn || p.timesOutfitChanged >= 10)
                  break;
                SettingsPlayer settingsPlayer2 = new SettingsPlayer();
                settingsPlayer2.Deserialize(myBinaryReader);
                settingsPlayer2.VerifyOutfit(p.account.cosmetics, p.account);
                ++p.timesOutfitChanged;
                using (MemoryStream memoryStream3 = new MemoryStream())
                {
                  using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream3))
                  {
                    w.Write((byte) 197);
                    w.Write(num2);
                    settingsPlayer2.Serialize(w);
                  }
                  this.SendAllMessage(memoryStream3.ToArray());
                  break;
                }
              case 199:
                if (this.serverState.busy != ServerState.Busy.Starting_Turn)
                  break;
                break;
              default:
                Server.GlobalHandler(p.connection, myBinaryReader, b, num1);
                break;
            }
          }
        }
        catch (Exception ex)
        {
          if (!this.resyncOnError)
            return;
          Debug.LogError((object) ex.ToString());
          this.SendResyncMsg(p, ex.Message, false);
        }
      }
    }
  }

  internal void SpectatorLogic(Connection c, byte[] args, myBinaryReader reader)
  {
  }

  public void HandleMap(byte[] b)
  {
    using (MemoryStream memoryStream = new MemoryStream(b))
    {
      using (myBinaryReader reader = new myBinaryReader((Stream) memoryStream))
      {
        int num = (int) reader.ReadByte();
        this.HandleMap(reader);
      }
    }
  }

  public void HandleMap(myBinaryReader reader)
  {
    this.receivedInitialMsg = true;
    int width = reader.ReadInt32();
    int height = reader.ReadInt32();
    if (reader.ReadBoolean())
    {
      using (MemoryStream memoryStream = new MemoryStream(CLZF2.Decompress(reader.ReadBytes())))
      {
        using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
        {
          int length = myBinaryReader.ReadInt32();
          this.map.deserializedPixels = new Color32[length];
          for (int index = 0; index < length; ++index)
            this.map.deserializedPixels[index] = myBinaryReader.ReadColor32();
        }
      }
    }
    else
    {
      int length = reader.ReadInt32();
      this.map.deserializedPixels = new Color32[length];
      for (int index = 0; index < length; ++index)
        this.map.deserializedPixels[index] = reader.ReadColor32();
    }
    this.map.SetMapSprite(Client.game, this.map.deserializedPixels, height, width);
  }

  public void RemoveFromBoat(Connection c)
  {
    if (!c.player.inBoat)
      return;
    c.player.inBoat = false;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 83);
        myBinaryWriter.Write((byte) 2);
        myBinaryWriter.Write(c.player.id);
      }
      this.SendAllMessageNoRemoveBoat(memoryStream.ToArray());
    }
  }

  private bool CheckDraw(ZPerson p)
  {
    int num = 0;
    foreach (ZPerson player in this.players)
    {
      if (player.offeringDraw || player.controlled.Count == 0 || player.controlled[0].health <= 0 || !player.GetMultiConnected)
        ++num;
    }
    if (num != this.players.Count && p.localTurn < (this.isCountdown ? 200 : 60))
      return false;
    foreach (ZPerson player in this.players)
    {
      if (player.controlled.Count > 0)
        player.controlled[0].health = 0;
    }
    this.DelayKill();
    return true;
  }

  public bool CheckRematch(bool forceRematch = false)
  {
    if (this.rematchSent)
      return false;
    int num1 = 0;
    bool flag = false;
    foreach (ZPerson player in this.players)
    {
      if (player.offeringRematch && player.GetMultiConnected)
      {
        ++num1;
        flag = true;
      }
      else if (!player.Connected)
        ++num1;
    }
    if (!(num1 == this.players.Count | forceRematch))
      return false;
    if (flag)
    {
      if (!this.gameFacts.GetTeamMode())
        this.gameFacts.SetRatedMode(false);
      int num2 = this.gameFacts.GetRatedMode() ? 1 : 0;
      this.gameFacts.SetTournamentMode(false);
      if (num2 != 0)
        this.gameFacts.SetRatedMode(true);
      List<Tuple<Connection, int>> tupleList = new List<Tuple<Connection, int>>();
      foreach (ZPerson player in this.players)
      {
        ZPerson x = player;
        if (x.connection != null)
          x.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
        if (x.offeringRematch && x.Connected)
          tupleList.Add(new Tuple<Connection, int>(x.connection, this.gameFacts.originalOrder.FindIndex((Predicate<string>) (z => string.Equals(z, x.name)))));
      }
      tupleList.Sort((Comparison<Tuple<Connection, int>>) ((a, b) => a.Item2 - b.Item2));
      this.gameFacts.players.Clear();
      this.gameFacts.connections.Clear();
      this.gameFacts.settingsPlayer.Clear();
      this.gameFacts.accounts.Clear();
      this.gameFacts.status = (byte) 0;
      GameFacts gf = new GameFacts();
      gf.id = Server.NextGameID();
      gf.settings.Copy(this.gameFacts.settings);
      this.CleanUp();
      foreach (Tuple<Connection, int> tuple in tupleList)
      {
        tuple.Item1.player.gameNumber = gf.id;
        gf.connections.Add(tuple.Item1);
      }
      Server._games.Add(gf.id, gf);
      Server.SendGameCreated(gf);
      GameFacts gameFacts = this.gameFacts;
      this.gameFacts = gf;
      for (int index = 0; index < this.players.Count; ++index)
      {
        ZPerson player = this.players[index];
        if (player.ConnectedToOld(gameFacts.id))
        {
          Server.MovePlayer(player.connection, player.offeringRematch ? Location.LobbyCreateGame : Location.Lobby);
          Server.SyncLobby(player.connection, !player.offeringRematch);
          if (player.connection.player.gameNumber == gameFacts.id)
            player.connection.player.gameNumber = -1;
          if (player.connection.player.player == this.players[index])
            player.connection.player.player = (ZPerson) null;
          player.connection = (Connection) null;
        }
      }
      this.gameFacts = gameFacts;
      this.canSafelyClose = true;
      Server.RemoveGameInstance(this.gameFacts.id);
    }
    else
    {
      this.canSafelyClose = true;
      this.CloseGame();
    }
    this.canSafelyClose = true;
    this.rematchSent = true;
    return true;
  }

  internal void GetData(byte[] b, params ZGame.ReturnType[] r)
  {
    this.returnData.Clear();
    using (MemoryStream memoryStream = new MemoryStream(b))
    {
      using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
      {
        int num = (int) myBinaryReader.ReadByte();
        for (int index = 0; index < r.Length; ++index)
        {
          switch (r[index])
          {
            case ZGame.ReturnType._byte:
              this.returnData.Add(new ZGame.DynamicType(myBinaryReader.ReadByte()));
              break;
            case ZGame.ReturnType._int:
              this.returnData.Add(new ZGame.DynamicType(myBinaryReader.ReadInt32()));
              break;
            case ZGame.ReturnType._string:
              this.returnData.Add(new ZGame.DynamicType(myBinaryReader.ReadString()));
              break;
            case ZGame.ReturnType._bool:
              this.returnData.Add(new ZGame.DynamicType(myBinaryReader.ReadBoolean()));
              break;
          }
        }
      }
    }
  }

  public void SaveReplay()
  {
    if (this.isReplay || this.timeline.Count == 0)
      return;
    string path1 = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "SavedReplays" + Path.DirectorySeparatorChar.ToString();
    if (!Directory.Exists(path1))
      Directory.CreateDirectory(path1);
    string str1 = "";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.Random_Spells))
      str1 += "RND";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.Elementals))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "ELE";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.First_Turn_Teleport))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "TEL";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.Zero_Shield))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "ZR0";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.Watchtower))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "WT";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.Original_Spells_Only))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "OG";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.Arcane_Monster))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "MON";
    if (this.gameFacts.GetStyle().HasStyle(GameStyle.No_Movement))
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "NO";
    if (this.gameFacts.GetMultiTeamMode())
      str1 = str1 + (str1.Length > 1 ? "_" : "") + "MUL";
    if (str1.Length == 0)
      str1 = "SD";
    if (str1.Length > 12)
    {
      int length = str1.LastIndexOf('_', 13, 12);
      if (length > 0)
        str1 = str1.Substring(0, length);
    }
    string str2 = "";
    HashSet<string> stringSet = new HashSet<string>();
    for (int index = 0; index < this.gameFacts.players.Count; ++index)
    {
      if (stringSet.Add(this.gameFacts.players[index]))
      {
        if (str2.Length > 0)
          str2 += "_";
        str2 = this.gameFacts.players[index].Length <= 3 ? str2 + this.gameFacts.players[index] : str2 + this.gameFacts.players[index].Substring(0, 3);
      }
    }
    this.replayName = this.gameFacts.id.ToString("0000000000") + "_" + DateTime.Now.ToString("MM-dd-tt-hh-mm") + "_" + (object) this.gameFacts.GetTimeInSeconds() + "s_" + GameFacts.MapShortName(this.gameFacts.realMap) + "_" + str1 + "_" + (object) stringSet.Count + "p_" + str2;
    string path2 = path1 + this.replayName + ".arcreplay";
    try
    {
      using (FileStream fileStream = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.None))
      {
        using (myBinaryWriter writer = new myBinaryWriter((Stream) fileStream))
        {
          writer.Write(Inert.Version);
          writer.Write(this.isClient && !this.isSpectator);
          writer.Write(this.isClient ? Client.Name : "Server");
          this.gameFacts.ManualSerialize(writer, true);
          writer.Write(this.timeline.Count);
          foreach (byte[] buffer in this.timeline)
            writer.Write(buffer);
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    this.timeline.Clear();
  }

  public static void init_Replay(string file)
  {
    try
    {
      using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (myBinaryReader reader = new myBinaryReader((Stream) fileStream))
        {
          reader.ReadString();
          reader.ReadBoolean();
          reader.ReadString();
          GameFacts gameFacts = new GameFacts();
          gameFacts.ManualDeserialize(reader, true);
          Client._gameFacts = gameFacts;
          Client.game = gameFacts.game;
          Client.game.timelineList = new List<ZGame.TimelineData>();
          int num = reader.ReadInt32();
          for (int index = 0; index < num; ++index)
          {
            try
            {
              Client.game.timeline.Add(reader.ReadBytes());
              if (Client.game.timeline[index][0] == (byte) 198)
              {
                Client.game.GetData(Client.game.timeline[index], ZGame.ReturnType._byte, ZGame.ReturnType._int);
                Client.game.timelineList.Add(new ZGame.TimelineData()
                {
                  description = (Client.game.returnData[1].ToInt() + 2).ToString() + ". " + gameFacts.players[Client.game.returnData[0].ToInt()],
                  frame = index
                });
              }
              else if (Client.game.timeline[index][0] == (byte) 220)
              {
                if (ZGame.GetFiredSpellName(gameFacts, Client.game.timeline[index]) != null)
                  Client.game.GetData(Client.game.timeline[index], ZGame.ReturnType._byte, ZGame.ReturnType._int);
              }
            }
            catch (Exception ex)
            {
              break;
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    Client.game.lastReplayFile = file;
    Client.game.isSandbox = false;
    Client.game.isServer = false;
    Client.game.isClient = true;
    Client.game.isSpectator = ReplayMenu.isSpectator;
    Client.game.isReplay = true;
    Client.Host = false;
    Client.game.turn = -1;
    Client.game.MoveQue.Clear();
    if (Client.game.serverUpdate != null)
      Timing.KillCoroutines(Client.game.serverUpdate);
    if (Client.game.resyncUpdate != null)
    {
      Timing.KillCoroutines(Client.game.resyncUpdate);
      Client.game.resyncUpdate = (IEnumerator<float>) null;
    }
    Client.game.Awake();
    Client.game.serverState = new ServerState();
    Client.joinedFrom = Client.JoinLocation.Replay;
    Controller.Instance.InitMap(false);
  }

  public static string GetFiredSpellName(GameFacts f, byte[] b)
  {
    using (MemoryStream memoryStream = new MemoryStream(b))
    {
      using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
      {
        try
        {
          byte b1 = myBinaryReader.ReadByte();
          if (b.Length > 1 && Global.HasPlayerID(b1))
          {
            int num1 = (int) myBinaryReader.ReadByte();
          }
          myBinaryReader.ReadInt32();
          myBinaryReader.ReadInt32();
          int num2 = (int) myBinaryReader.ReadByte();
          byte index1 = myBinaryReader.ReadByte();
          byte index2 = myBinaryReader.ReadByte();
          FixedInt fixedInt = (FixedInt) myBinaryReader.ReadInt64();
          myBinaryReader.ReadBoolean();
          myBinaryReader.ReadMyLocation();
          FixedInt x = (FixedInt) myBinaryReader.ReadInt64();
          myBinaryReader.ReadBoolean();
          myBinaryReader.ReadInt32();
          myBinaryReader.ReadMyLocation();
          myBinaryReader.ReadMyLocation();
          if (num2 == 0 && f.settingsPlayer[(int) index1].spells.Length > (int) index2)
            return Inert.Instance._spells[(int) f.settingsPlayer[(int) index1].spells[(int) index2]].name;
          Mathd.Clamp(x, (FixedInt) 10485L, (FixedInt) 1);
        }
        catch (Exception ex)
        {
        }
      }
    }
    return (string) null;
  }

  public void GoToReplayFrame(int f)
  {
    ZGame.targetTimelineFrame = this.timelineList[f].frame;
    if (this.timelineList[f].frame < this.curReplayIndex || !this.isReplay)
    {
      ZGame.replayShowStartPanel = false;
      Timing.RunCoroutine(this.redoReplay());
    }
    else
      HUD.instance.LoadingPanel.SetActive(true);
  }

  private IEnumerator<float> redoReplay()
  {
    Controller.Instance.DestroyMap();
    yield return 0.0f;
    Controller.Instance.OpenMenu(Controller.Instance.RewindingPanel, false);
    yield return 0.0f;
    ZGame.init_Replay(this.lastReplayFile);
  }

  public IEnumerator<float> PushReplay()
  {
    ZGame game = this;
    bool nextTurn = false;
    game.replayPastTimeLine = 0;
    HUD.instance.replayTimeline.MaxSize = game.timelineList.Count;
    // ISSUE: reference to a compiler-generated method
    HUD.instance.replayTimeline._bar.onValueChanged.AddListener(new UnityAction<float>(game.\u003CPushReplay\u003Eb__232_0));
    game.replayPaused = false;
    Time.timeScale = 1f;
    ChatBox.Instance?.NewChatMsg("", "Hold F1 to take control right before the next attack", (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
    ChatBox.Instance?.NewChatMsg("", "Press F2 to take control right away", (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
    ChatBox.Instance?.NewChatMsg("", "Hold F3 to take control on the next turn", (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
    foreach (ZGame.TimelineData timeline in game.timelineList)
      HUD.instance.replayTimeline.Add(timeline.description);
    yield return 0.0f;
    if (!ZGame.replayShowStartPanel)
      HUD.instance.replayTimeline._chatScrollbar.normalizedPosition = new Vector2(0.0f, game.replayScrollPos);
    int safeFreeze = 0;
    game.curReplayIndex = 0;
    if (!ZGame.replayShowStartPanel)
      HUD.instance.panelStart.SetActive(false);
    while (game.timeline.Count > game.curReplayIndex || game.MoveQue.Count > 0 || game.ongoing.NumberOfSlowUpdateCoroutines > 0)
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        game.replayPaused = true;
        Time.timeScale = 0.0f;
        if (Input.GetKeyDown(KeyCode.Space))
          yield return 0.0f;
      }
      else if (!game.resyncing && (Input.GetKey(KeyCode.F1) && game.timeline[game.curReplayIndex][0] == (byte) 220 || Input.GetKeyDown(KeyCode.F2) || Input.GetKey(KeyCode.F3) & nextTurn || Input.GetKey(KeyCode.F4)))
      {
        game.isReplay = false;
        game.isSpectator = false;
        game.isSandbox = true;
        game.isServer = true;
        game.isClient = true;
        foreach (ZPerson player in game.players)
        {
          ZCreature zcreature = player.first();
          if ((ZComponent) zcreature != (object) null && !zcreature.isDead)
          {
            zcreature.spells.Clear();
            for (int index = 0; index < 16; ++index)
            {
              SettingsPlayer settingsPlayer = Client.settingsPlayer;
              if (Inert.Instance.spells.Count > (int) settingsPlayer.spells[index] && settingsPlayer.spells[index] < byte.MaxValue)
              {
                SpellSlot spellSlot = new SpellSlot(!player.seasonISHoliday || settingsPlayer.spells[index] < (byte) 120 || settingsPlayer.spells[index] > (byte) 131 ? Inert.Instance._spells[(int) settingsPlayer.spells[index]] : Inert.Instance.holidaySpells[(int) settingsPlayer.spells[index] - 120]);
                zcreature.spells.Add(spellSlot);
              }
            }
          }
        }
        HUD.FindFullBooks(game);
        Client.NameOrReplay = game.CurrentPlayer().name;
        HUD.instance.buttonShowSpells.GetComponent<UIOnHover>().Interactable(true);
        HUD.instance.FindPlayer();
        game.serverUpdate = Timing.RunCoroutine(game.FixedUpdate(), Segment.Update);
        HUD.instance.buttonHideChat.gameObject.SetActive(false);
        if ((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null && (UnityEngine.Object) ChatBox.Instance.textSpellCasted != (UnityEngine.Object) null)
        {
          HUD.instance.textSpellCasted.rectTransform.SetParent(HUD.instance.transform.GetChild(2));
          ChatBox.Instance.textSpellCasted = (TMP_Text) null;
        }
        Controller instance = Controller.Instance;
        if (instance == null)
        {
          yield break;
        }
        else
        {
          instance.DestroyChatBox();
          yield break;
        }
      }
      while (game.replayPaused)
      {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          game.replayPaused = false;
          Time.timeScale = 1f;
        }
        yield return 0.0f;
      }
      if (!HUD.instance.panelPause.activeSelf && !HUD.instance.panelStart.activeSelf && !game._resyncing)
      {
        if (game.MoveQue.Count == 0 && game.ongoing.NumberOfSlowUpdateCoroutines == 0)
        {
          byte[] b = game.timeline[game.curReplayIndex];
          if (game.replayPastTimeLine < game.timelineList.Count && game.curReplayIndex >= game.timelineList[game.replayPastTimeLine].frame)
          {
            ++game.replayPastTimeLine;
            HUD.instance.replayTimeline.ForceRender();
          }
          ++game.curReplayIndex;
          int index = b.Length == 1 || !Global.HasPlayerID(b[0]) ? 0 : (int) b[1];
          if (index < game.players.Count)
          {
            ZPerson player = game.players[index];
            game.GameHandler(player, b, true, false);
            nextTurn = b[0] == (byte) 198;
          }
        }
        game.tickTime += game.DeltaTime;
        if ((double) game.tickTime >= (double) game.frameTime)
        {
          game.tickTime -= game.frameTime;
          game.world?.listPool.Clear();
          SandPool.Instance?.Reset();
          game.ongoing.ServerUpdate();
        }
        if (game.MoveQue.Count > 0 && game.ongoing.NumberOfSlowUpdateCoroutines == 0)
          game.MoveQue.Dequeue()();
      }
      ++safeFreeze;
      if (game.curReplayIndex > ZGame.targetTimelineFrame || game.forceRysncPause || safeFreeze > 1000 || game.curReplayIndex % 100 == 0)
        yield return 0.0f;
      if (game.forceRysncPause)
        game.forceRysncPause = false;
      if (safeFreeze > 100)
        safeFreeze = 0;
    }
    game.timeline.Clear();
    if ((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null)
    {
      ChatBox.Instance.NewChatMsg("[Replay]", "End of Replay", (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
      if (!ChatBox.Instance.Active)
        ChatBox.Instance.SetActive(true);
    }
  }

  public void init_Resync(bool force = false)
  {
    if (Application.isBatchMode || !force && (this.resyncing || this.isReplay || this.serverState.busy == ServerState.Busy.Ended))
      return;
    this.resyncing = true;
    this.turn = -1;
    this.serverState.busy = ServerState.Busy.Not_started;
    ZGame game = Client.game;
    Controller.Instance.DestroyMap(true, false);
    this.Reset(false);
    Client.game = new ZGame();
    Client.game.isClient = true;
    Client.game.resyncing = true;
    Client.game.turn = -1;
    Client.game.serverState.busy = ServerState.Busy.Not_started;
    if (game != null)
    {
      Client.game.isSpectator = game.isSpectator;
      Client.game.timeline = game.timeline;
      game.timeline = (List<byte[]>) null;
      Client.game.gameFacts = game.gameFacts;
    }
    else
      Client.game.gameFacts = Client._gameFacts;
    Client._gameFacts.game = Client.game;
    Client.joinedFrom = Client.JoinLocation.Game;
    Controller.Instance.InitMap(false);
    Client.SomeSetup();
    Client.game.resyncUpdate = Timing.RunCoroutine(Client.game.Resync());
  }

  public void init_Deserialize(GameFacts gf, List<byte[]> timeline, byte[] data, bool force = false)
  {
    if (Application.isBatchMode || !force && (this.resyncing || this.isReplay || this.serverState.busy == ServerState.Busy.Ended))
      return;
    this.resyncing = true;
    this.turn = -1;
    this.serverState.busy = ServerState.Busy.Not_started;
    ZGame game = Client.game;
    Controller.Instance.DestroyMap(true, false);
    this.Reset();
    Client.game = new ZGame();
    Client.game.isClient = true;
    Client.game.resyncing = true;
    Client.game.turn = -1;
    if (Spectator.connection == null && Client.connection != null)
    {
      Client.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(ZGame.GameClientHandler);
      Client.connection.DataReceived += new EventHandler<DataReceivedEventArgs>(ZGame.GameClientHandler);
    }
    Client.game.serverState.busy = ServerState.Busy.Not_started;
    if (game != null)
      Client.game.isSpectator = game.isSpectator;
    Client.game.gameFacts = gf;
    Client.game.gameFacts.game = Client.game;
    Client.game.timeline = timeline;
    Client._gameFacts.game = Client.game;
    Client.joinedFrom = Client.JoinLocation.Game;
    Controller.Instance.InitMap(false);
    Client.game.resyncUpdate = Timing.RunCoroutine(ZGame.StartGame(data, timeline.Count));
  }

  public IEnumerator<float> Resync(int startIndex = 0, bool bypassWating = false)
  {
    int replayIndex = startIndex;
    if (bypassWating)
    {
      yield return 0.0f;
      yield return 0.0f;
      if (!this.isSpectator)
      {
        for (float i = 0.0f; (double) i < 2.0; i += Time.deltaTime)
          yield return 0.0f;
      }
    }
    int safeGuard = 0;
    while (replayIndex < this.timeline.Count)
    {
      ++safeGuard;
      if (this.MoveQue.Count == 0 && this.ongoing.NumberOfSlowUpdateCoroutines == 0)
      {
        byte[] b = this.timeline[replayIndex];
        ++replayIndex;
        int index = (b.Length == 1 ? 1 : (!Global.HasPlayerID(b[0]) ? 1 : 0)) != 0 ? 0 : (int) b[1];
        if (index < this.players.Count)
        {
          ZPerson player = this.players[index];
          if (b.Length > 1 && b[0] != (byte) 153 && b[0] != (byte) 152)
            this.GameHandler(player, b, true, false);
        }
      }
      this.world?.listPool.Clear();
      SandPool.Instance?.Reset();
      this.ongoing.ServerUpdate();
      if (this.MoveQue.Count > 0 && this.ongoing.NumberOfSlowUpdateCoroutines == 0)
        this.MoveQue.Dequeue()();
      if (this.forceRysncPause)
      {
        this.forceRysncPause = false;
        yield return 0.0f;
      }
      else if (replayIndex % 100 == 0)
        yield return 0.0f;
      else if (safeGuard > 1000)
      {
        safeGuard = 0;
        yield return 0.0f;
      }
    }
    this.resyncing = false;
    this.map.Apply();
    if (this.isReplay)
      this.timeline.Clear();
  }

  public void GameHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes[0] == (byte) 34)
      return;
    if (args.Bytes[0] == (byte) 156)
      Server.Test((Connection) sender);
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      ZPerson player1 = ((Connection) sender).player.player;
      if (player1 != null)
      {
        if (args.Bytes.Length == 1 || !Global.HasPlayerID(args.Bytes[0]) || (int) args.Bytes[1] == (int) player1.id)
          this.GameHandler(player1, args.Bytes, false);
        else if (this.isMulti && Global.HasPlayerID(args.Bytes[0]) && args.Bytes.Length > 1 && (int) args.Bytes[1] < this.players.Count)
        {
          ZPerson player2 = this.players[(int) args.Bytes[1]];
          if (player2.team == player1.team)
            this.GameHandler(player2, args.Bytes, false);
        }
      }
      args.Recycle();
    }));
  }

  public void ServerSpectatorHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes[0] == (byte) 34 || args.Bytes.Length > 200)
      return;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      Connection c = (Connection) sender;
      using (MemoryStream memoryStream = new MemoryStream(args.Bytes))
      {
        using (myBinaryReader reader = new myBinaryReader((Stream) memoryStream))
        {
          try
          {
            if (reader.ReadByte() == (byte) 83)
              this.SpectatorLogic(c, args.Bytes, reader);
          }
          catch (Exception ex)
          {
          }
        }
      }
      args.Recycle();
    }));
  }

  public static void GameClientHandler(object sender, DataReceivedEventArgs args)
  {
    if (args.Bytes.Length == 1 && args.Bytes[0] == (byte) 34)
      return;
    UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
    {
      if (Client.game == null)
        return;
      if (args.Bytes.Length > 1 && args.Bytes[0] != (byte) 15 && args.Bytes[0] != (byte) 35 && !Global.IsChatMsg(args.Bytes[0]) && args.Bytes[0] != (byte) 85 && args.Bytes[0] != (byte) 84 && args.Bytes[0] != (byte) 42 && args.Bytes[0] != (byte) 64 && args.Bytes[0] != (byte) 98 && args.Bytes[0] != (byte) 100 && args.Bytes[0] != (byte) 96)
        Client.game.timeline.Add(ZGame.CopyByteArray(args.Bytes));
      if (!Client.game.resyncing || args.Bytes[0] == (byte) 35 || args.Bytes[0] == (byte) 100)
      {
        bool flag = args.Bytes.Length == 1 || !Global.HasPlayerID(args.Bytes[0]);
        if (!flag && (int) args.Bytes[1] >= Client.game.players.Count)
        {
          Debug.LogWarning((object) args.Bytes.ToStringX("Bigger then Players"));
        }
        else
        {
          if (Client.game.players.Count == 0)
          {
            if (args.Bytes.Length != 0)
              Debug.Log((object) args.Bytes[0]);
            Client.game.GameHandler((ZPerson) null, args.Bytes, true);
            args.Recycle();
            return;
          }
          ZPerson player = Client.game.players[flag ? 0 : (int) args.Bytes[1]];
          Client.game.GameHandler(player, args.Bytes, true);
        }
      }
      else if (args.Bytes.Length > 1 && Global.IsChatMsg(args.Bytes[0]))
        Client.game.GameHandler((ZPerson) null, args.Bytes, true);
      args.Recycle();
    }));
  }

  public static IEnumerator<float> StartGame(byte[] g, int startingTimeLineCount)
  {
    yield return 0.0f;
    try
    {
      ZGame.Deserialize(g, Client.game);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Client.game.init_Resync(true);
      yield break;
    }
    if (Client.game.timeline.Count > startingTimeLineCount)
    {
      Client.game.resyncing = true;
      Client.game.resyncUpdate = Timing.RunCoroutine(Client.game.Resync(startingTimeLineCount, true));
    }
    else
    {
      Client.game.resyncing = false;
      Client.game.map.Apply();
    }
    yield return 0.0f;
  }

  public byte[] SerializeAll(Connection c, int offset, int len, SpellOverrides sp)
  {
    try
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
        {
          writer.Write((byte) 100);
          this.gameFacts.ManualSerialize(writer, true);
          writer.Write(offset);
          writer.Write(len);
          writer.Write(this.timeline.Count - offset);
          for (int index = offset; index < this.timeline.Count; ++index)
            writer.Write(this.timeline[index]);
          writer.Write(this.Serialize(sp));
        }
        return memoryStream.ToArray();
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.ToString());
    }
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 64);
        myBinaryWriter.Write(offset);
        myBinaryWriter.Write(len);
        for (int index = offset; index < this.timeline.Count; ++index)
          myBinaryWriter.Write(this.timeline[index]);
        myBinaryWriter.Write("Fast Resync Failed :(");
        myBinaryWriter.Write(false);
      }
      return memoryStream.ToArray();
    }
  }

  public byte[] Serialize(SpellOverrides sp)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter writer = new myBinaryWriter((Stream) memoryStream))
        this._Serialize(writer, sp);
      return memoryStream.ToArray();
    }
  }

  public void _Serialize(myBinaryWriter writer, SpellOverrides sp)
  {
    this.xSpell = new HashSet<ZSpell>();
    this.xSlot = new HashSet<SpellSlot>();
    this.xCreature = new HashSet<ZCreature>();
    this.xEffector = new HashSet<ZEffector>();
    this.xCollider = new HashSet<ZMyCollider>();
    writer.Write(sp != null);
    if (sp != null)
      writer.Write(sp.ToBytes());
    writer.Write(this.gravity.RawValue);
    writer.Write(this.map.wind);
    writer.Write(this.map.windSpeed.RawValue);
    writer.Write(this.map.windDir.RawValue);
    writer.Write(this.turn);
    writer.Write(this.armageddonTurn);
    writer.Write((int) this.armageddon);
    writer.Write(this.seasonTurn);
    writer.Write(this.MaxTurnTime);
    writer.Write(this.PlayersMaxTurnTime);
    writer.Write(this.shouldUpdateTurnTime);
    this.serverState.Serialize(writer);
    writer.Write(this.originalSpellsOnly);
    writer.Write(this.isWindy);
    writer.Write(this.sandboxTime);
    writer.Write(this.tutorialPaused);
    writer.Write(this.winOnDeath);
    writer.Write(this.isTeam);
    writer.Write(this.isMulti);
    writer.Write(this.isCountdown);
    writer.Write(this.countdownLose);
    writer.Write(this.countdownTime);
    writer.Write(this.isTutorial);
    writer.Write(this.AllowDKH);
    writer.Write(this.firstOceanFury);
    writer.Write(this.hasArcaneMonster);
    writer.Write(this.AllowMovement);
    writer.Write(this.AllowTerrainDestruction);
    writer.Write(this.AllowInput);
    writer.Write(this.AllowCallbacks);
    writer.Write((int) this.currentSeason);
    writer.Write(this.TEAM_COUNT);
    writer.Write(this.lastMinionToDie.Count);
    for (int index = 0; index < this.lastMinionToDie.Count; ++index)
    {
      writer.Write((int) this.lastMinionToDie[index].spell);
      writer.Write(this.lastMinionToDie[index].position);
    }
    writer.Write(this.MAPCREATED);
    writer.Write(this.map._pastBits.Count);
    for (int index = 0; index < this.map._pastBits.Count; ++index)
      this.map._pastBits[index].Serialize(writer);
    if (Inert._Version > 57)
    {
      writer.Write(this._nextturn);
      GameSerializer.SerializePerson(this, this._uncontrolledPlayer, writer);
    }
    writer.Write(this.players.Count);
    foreach (ZPerson player in this.players)
      GameSerializer.SerializePerson(this, player, writer);
    foreach (ZPerson player in this.players)
      this.PostSerialize(player, writer);
    writer.Write(this.globalEffectors.Count);
    for (int index = 0; index < this.globalEffectors.Count; ++index)
    {
      if ((ZComponent) this.globalEffectors[index] == (object) null)
      {
        writer.Write(false);
      }
      else
      {
        writer.Write(true);
        this.globalEffectors[index].Serialize(writer);
      }
    }
    writer.Write(this.thorns.Count);
    for (int index = 0; index < this.thorns.Count; ++index)
    {
      if ((ZComponent) this.thorns[index] == (object) null)
      {
        writer.Write(false);
      }
      else
      {
        writer.Write(true);
        writer.Write((ZComponent) this.thorns[index].spell?.parent != (object) null ? this.thorns[index].spell.parent.id : -1);
        ZCreatureCreate.Serialize((ZCreature) this.thorns[index], writer);
      }
    }
    this.SerializeList(writer, this._playersExtended);
    this.SerializeList(writer, this.team1Players);
    this.SerializeList(writer, this.team2Players);
    this.SerializeList(writer, this.team3Players);
    this.SerializeList(writer, this.team4Players);
    this.SerializeList(writer, this.team5Players);
    this.SerializeList(writer, this.team6Players);
    this.SerializeList(writer, this.team7Players);
    this.SerializeList(writer, this.team8Players);
    this.SerializeList(writer, this.team9Players);
    this.SerializeList(writer, this.team10Players);
    this.SerializeList(writer, this.team11Players);
    this.SerializeList(writer, this.team12Players);
    this.SerializeList(writer, this.windShieldEffectors);
    this.SerializeList(writer, this.elfEffectors);
    this.SerializeList(writer, this.staticCharge);
    this.SerializeList(writer, this.recallDevices);
    writer.Write((ZComponent) this.targetEffector != (object) null);
    if ((ZComponent) this.targetEffector != (object) null)
      this.targetEffector.Serialize(writer);
    writer.Write((ZComponent) this.dwarfMapEffector != (object) null);
    if ((ZComponent) this.dwarfMapEffector != (object) null)
      this.dwarfMapEffector.Serialize(writer);
    writer.Write((ZComponent) this.naturesWrath != (object) null);
    if ((ZComponent) this.naturesWrath != (object) null)
      this.naturesWrath.Serialize(writer);
    writer.Write((ZComponent) this.blackhole != (object) null);
    if ((ZComponent) this.blackhole != (object) null)
      this.blackhole.Serialize(writer);
    writer.Write(this.ArcaneZero);
    writer.Write(this.First_Turn_Teleport);
    writer.Write(this.TurnToLoseArcaneZero);
    writer.Write(this.skimmed_on_water);
    writer.Write(this.armageddonTurnVariable);
    writer.Write(this.decreasePlayer2Cooldowns);
    writer.Write(this.nextCreatureID);
    writer.Write(this.nextSpellID);
    writer.Write(this.nextColliderID);
    writer.Write(this.nextEffectorID);
    this.random.Serialize(writer);
    writer.Write(this.spectators.Count);
    for (int index = 0; index < this.spectators.Count; ++index)
    {
      if (this.spectators[index] == null || !this.spectators[index].player.inBoat)
      {
        writer.Write(false);
      }
      else
      {
        writer.Write(true);
        writer.Write(this.spectators[index].name);
        writer.Write(this.spectators[index].player.botPos);
        writer.Write(this.spectators[index].player.id);
        this.spectators[index].player.settingsPlayer.SerializeBasicOutfit(writer);
      }
    }
  }

  public static void Deserialize(byte[] bytes, ZGame game)
  {
    using (MemoryStream memoryStream = new MemoryStream(bytes))
    {
      using (myBinaryReader reader = new myBinaryReader((Stream) memoryStream))
        game._Deserialize(reader);
    }
  }

  private void _Deserialize(myBinaryReader reader)
  {
    this.helper = new ZGame.SerializationHelper();
    if (reader.ReadBoolean())
      Inert.Instance.Apply(SpellOverrides.FromBytes(reader.ReadBytes()));
    for (int index = 0; index < this.timeline.Count; ++index)
    {
      if (this.timeline[index][0] == (byte) 16)
      {
        this.HandleMap(this.timeline[index]);
        break;
      }
    }
    this.random = new IsaacCipher(new int[1]
    {
      this.gameFacts.seed
    });
    this.gameFacts.game = this;
    this.map.Gravity = (FixedInt) reader.ReadInt64();
    this.map.wind = reader.ReadMyLocation();
    this.map.windSpeed = (FixedInt) reader.ReadInt64();
    this.map.windDir = (FixedInt) reader.ReadInt64();
    this.turn = reader.ReadInt32();
    this.armageddonTurn = reader.ReadInt32();
    this.armageddon = (MapEnum) reader.ReadInt32();
    this.seasonTurn = reader.ReadInt32();
    this.MaxTurnTime = reader.ReadInt32();
    this.PlayersMaxTurnTime = reader.ReadSingle();
    this.shouldUpdateTurnTime = reader.ReadInt32();
    this.serverState.DeSerialize(reader);
    this.originalSpellsOnly = reader.ReadBoolean();
    this.isWindy = reader.ReadBoolean();
    this.sandboxTime = reader.ReadInt32();
    this.tutorialPaused = reader.ReadBoolean();
    this.winOnDeath = reader.ReadBoolean();
    this.isTeam = reader.ReadBoolean();
    this.isMulti = reader.ReadBoolean();
    this.isCountdown = reader.ReadBoolean();
    this.countdownLose = reader.ReadBoolean();
    this.countdownTime = reader.ReadSingle();
    this.isTutorial = reader.ReadBoolean();
    this.AllowDKH = reader.ReadBoolean();
    this.firstOceanFury = reader.ReadBoolean();
    this.hasArcaneMonster = reader.ReadBoolean();
    this.AllowMovement = reader.ReadBoolean();
    this.AllowTerrainDestruction = reader.ReadBoolean();
    this.AllowInput = reader.ReadBoolean();
    this.AllowCallbacks = reader.ReadBoolean();
    this.currentSeason = (GameSeason) reader.ReadInt32();
    this.TEAM_COUNT = reader.ReadInt32();
    int num1 = reader.ReadInt32();
    for (int index = 0; index < num1; ++index)
      this.lastMinionToDie.Add(new ZGame.Resurection()
      {
        spell = (SpellEnum) reader.ReadInt32(),
        position = reader.ReadMyLocation()
      });
    this.MAPCREATED = reader.ReadBoolean();
    if (this.hasArcaneMonster)
      this.map.Purpalize();
    int num2 = reader.ReadInt32();
    for (int index = 0; index < num2; ++index)
    {
      PastBlits p = PastBlits.Deserialize(reader);
      this.map._pastBits.Add(p);
      this.map.DoPastBlit(p);
    }
    if (Inert._Version > 57)
    {
      this._nextturn = reader.ReadInt32();
      this._uncontrolledPlayer = GameSerializer.DeserializePerson(this, (int) this._uncontrolledPlayer.id, reader, false);
    }
    int num3 = reader.ReadInt32();
    for (int i = 0; i < num3; ++i)
      GameSerializer.DeserializePerson(this, i, reader, true);
    int i1 = 0;
    foreach (ZPerson player in this.players)
    {
      this.PostDeserialize(player, reader);
      if ((UnityEngine.Object) player.panelPlayer == (UnityEngine.Object) null)
      {
        if (player.game.isClient)
          player.clientColor = (int) player.settingsPlayer.indexBody != SettingsPlayer.sno_body2 ? TeamColors.GetColor(i1) : new Color(0.5f, 0.5f, 0.5f);
        HUD.instance.AddPanelPlayer(player);
        if (player.localTurn < 0 && (ZComponent) player.first() != (object) null && (UnityEngine.Object) player.first().transform != (UnityEngine.Object) null && !player.game.originalSpellsOnly)
          UnityEngine.Object.Instantiate<BigBubble>(ClientResources.Instance.big_bubble, player.first().transform.position, Quaternion.identity, player.first().transform).creature = player.first();
      }
      if ((UnityEngine.Object) player.panelPlayer != (UnityEngine.Object) null)
      {
        if (player.controlled.Count == 0)
          player.panelPlayer.SetHP(-1);
        if (player.didResign)
          player.panelPlayer.Resigned();
        if (player.didLeave)
          player.panelPlayer.Left();
        player.panelPlayer.SetSummons(player);
      }
      ++i1;
    }
    this.elfEffectors.Clear();
    this.thorns.Clear();
    this.recallDevices.Clear();
    this.windShieldEffectors.Clear();
    this.globalEffectors.Clear();
    int num4 = reader.ReadInt32();
    for (int index = 0; index < num4; ++index)
    {
      if (reader.ReadBoolean())
        this.globalEffectors.Add(ZEffector.Deserialze(this, (ZCreature) null, reader, (ZEffector) null));
    }
    int num5 = reader.ReadInt32();
    for (int index = 0; index < num5; ++index)
    {
      if (reader.ReadBoolean())
      {
        ZCreature creature = this.helper.GetCreature(reader.ReadInt32());
        ZCreature zcreature = ZCreatureCreate.Deserialize(this.players[0], reader, index);
        ZCreatureThorn zcreatureThorn = (ZCreatureThorn) zcreature;
        zcreatureThorn.spell.position = zcreature.position;
        zcreatureThorn.spell.parent = creature;
        this.thorns.Add(zcreatureThorn);
      }
    }
    this.DeserializeList(reader, this._playersExtended);
    this.DeserializeList(reader, this.team1Players);
    this.DeserializeList(reader, this.team2Players);
    this.DeserializeList(reader, this.team3Players);
    this.DeserializeList(reader, this.team4Players);
    this.DeserializeList(reader, this.team5Players);
    this.DeserializeList(reader, this.team6Players);
    this.DeserializeList(reader, this.team7Players);
    this.DeserializeList(reader, this.team8Players);
    this.DeserializeList(reader, this.team9Players);
    this.DeserializeList(reader, this.team10Players);
    this.DeserializeList(reader, this.team11Players);
    this.DeserializeList(reader, this.team12Players);
    this.DeserializeList(reader, this.windShieldEffectors);
    this.DeserializeList(reader, this.elfEffectors);
    this.DeserializeList(reader, this.staticCharge);
    this.DeserializeList(reader, this.recallDevices);
    this.targetEffector = reader.ReadBoolean() ? ZEffector.Deserialze(this, (ZCreature) null, reader, (ZEffector) null) : (ZEffector) null;
    this.dwarfMapEffector = reader.ReadBoolean() ? ZEffector.Deserialze(this, (ZCreature) null, reader, (ZEffector) null) : (ZEffector) null;
    this.naturesWrath = reader.ReadBoolean() ? ZEffector.Deserialze(this, (ZCreature) null, reader, (ZEffector) null) : (ZEffector) null;
    this.blackhole = reader.ReadBoolean() ? ZEffector.Deserialze(this, (ZCreature) null, reader, (ZEffector) null) : (ZEffector) null;
    this.ArcaneZero = reader.ReadBoolean();
    this.First_Turn_Teleport = reader.ReadBoolean();
    this.TurnToLoseArcaneZero = reader.ReadInt32();
    this.skimmed_on_water = reader.ReadBoolean();
    this.armageddonTurnVariable = reader.ReadInt32();
    this.decreasePlayer2Cooldowns = reader.ReadBoolean();
    this.nextCreatureID = reader.ReadInt32();
    this.nextSpellID = reader.ReadInt32();
    this.nextColliderID = reader.ReadInt32();
    this.nextEffectorID = reader.ReadInt32();
    this.random = IsaacCipher.Deserialize(reader);
    int num6 = reader.ReadInt32();
    for (int index = 0; index < num6; ++index)
    {
      if (reader.ReadBoolean())
      {
        string name = reader.ReadString();
        int num7 = reader.ReadInt32();
        int id = reader.ReadInt32();
        SettingsPlayer sp = new SettingsPlayer();
        sp.DeserializeBasicOutfit(reader);
        BoatSpectators.Create(name);
        BoatSpectators.Instance.CreateCharacter(id, name, BoatSpectators.Instance.GetValidPosition((int) CombineShort.Low(num7), (int) CombineShort.High(num7)), sp);
      }
    }
    if ((UnityEngine.Object) BoatSpectators.Instance != (UnityEngine.Object) null)
      BoatSpectators.Instance?.OnActivation();
    foreach (ZGame.ID2 idMount in this.helper.id_mounts)
    {
      idMount.creature.mount = this.helper.GetCreature(idMount.index);
      if ((ZComponent) idMount.creature.mount != (object) null)
        idMount.creature.mount.rider = idMount.creature;
    }
    foreach (ZGame.ID2 idPact in this.helper.id_pacts)
      idPact.creature.pact = this.helper.GetCreature(idPact.index);
    foreach (ZGame.ID2 idOldParent in this.helper.id_oldParents)
      idOldParent.creature.originalParent = this.helper.GetPlayer(idOldParent.index);
    foreach (ZGame.ID2 id2 in this.helper.id_daOgParent)
    {
      id2.creature.daOriginalTrueParent = this.helper.GetPlayer(id2.index);
      if (id2.creature.isPawn && id2.creature.daOriginalTrueParent != null)
      {
        if (id2.creature.spellEnum == SpellEnum.None)
        {
          id2.creature.gameObject.GetComponent<ConfigurePlayer>()?.EquipAll(id2.creature.daOriginalTrueParent.name, id2.creature.daOriginalTrueParent.settingsPlayer);
          Inert.AddTorquingAndOrArchStaffs(id2.creature.daOriginalTrueParent, id2.creature, id2.creature.daOriginalTrueParent.settingsPlayer, true, false);
        }
        else
        {
          ZSpell.Recolor(this, id2.creature);
          if (id2.creature.team == 24)
          {
            if ((UnityEngine.Object) id2.creature.txtHealth != (UnityEngine.Object) null)
              id2.creature.txtHealth.color = id2.creature.daOriginalTrueParent.clientColor;
            if ((UnityEngine.Object) id2.creature.bg != (UnityEngine.Object) null)
              id2.creature.bg.gameObject.SetActive(false);
          }
        }
      }
    }
    foreach (ZGame.ID2 id2 in this.helper.id_stormShield)
      id2.creature.stormShield = this.helper.GetEffector(id2.index);
    foreach (ZGame.ID2 id2 in this.helper.id_effectorStaticShield)
    {
      ZEffector effector = this.helper.GetEffector(id2.index);
      id2.creature.effectorStaticShield = (ZComponent) effector != (object) null ? (ZEffectorStaticBall) effector : (ZEffectorStaticBall) null;
    }
    foreach (ZGame.ID2 id2 in this.helper.id_Aura)
      id2.creature.auraOfDecay = this.helper.GetEffector(id2.index);
    foreach (ZGame.ID2 idFollowCollider in this.helper.id_followColliders)
    {
      ZMyCollider collider = this.helper.GetCollider(idFollowCollider.index);
      if ((ZComponent) collider != (object) null && !idFollowCollider.creature.followingColliders.Contains(collider))
      {
        if ((UnityEngine.Object) collider.transform != (UnityEngine.Object) null)
          collider.transform.SetParent(idFollowCollider.creature.transform);
        idFollowCollider.creature.followingColliders.Add(collider);
      }
    }
    foreach (ZGame.IDEffector effectorPartner in this.helper.effector_partners)
      effectorPartner.creature.partner = this.helper.GetEffector(effectorPartner.index);
    foreach (ZGame.IDEffector idEffector in this.helper.effector_whoSummoned)
      idEffector.creature.whoSummoned = this.helper.GetCreature(idEffector.index);
    foreach (ZGame.IDEffector idEffector in this.helper.effector_infector)
      idEffector.creature.infector = this.helper.GetCreature(idEffector.index);
    foreach (ZGame.IDEffector idEffector in this.helper.effector_spell)
      idEffector.creature.spell = this.helper.Getspell(idEffector.index);
    foreach (ZGame.IDCollider colRefCreature in this.helper.col_ref_creatures)
      colRefCreature.creature.creature = this.helper.GetCreature(colRefCreature.index);
    foreach (ZGame.IDCollider colRefEffector in this.helper.col_ref_effectors)
      colRefEffector.creature.effector = this.helper.GetEffector(colRefEffector.index);
    foreach (ZGame.IDCollider colRefSpell in this.helper.col_ref_spells)
      colRefSpell.creature.spell = this.helper.Getspell(colRefSpell.index);
    foreach (ZGame.IDSpell idSpell in this.helper.spell_extraCheck)
      idSpell.creature.extraCheck = this.helper.GetCreature(idSpell.index);
    foreach (ZGame.IDSpell idSpell in this.helper.spell_parent)
      idSpell.creature.parent = this.helper.GetCreature(idSpell.index);
    foreach (ZGame.IDSpell idSpell in this.helper.spell_hit)
      idSpell.creature.hitCreature = this.helper.GetCreature(idSpell.index);
    foreach (ZGame.IDSpell idSpell in this.helper.spell_effector)
      idSpell.creature.effector = this.helper.GetEffector(idSpell.index);
    foreach (KeyValuePair<int, ZEffector> keyValuePair in this.helper.effectorID)
    {
      if ((ZComponent) keyValuePair.Value != (object) null && keyValuePair.Value.followParent && (UnityEngine.Object) keyValuePair.Value.transform != (UnityEngine.Object) null && (ZComponent) keyValuePair.Value.whoSummoned != (object) null && !keyValuePair.Value.doNotCreateObjectOnResync)
        keyValuePair.Value.transform.SetParent(keyValuePair.Value.whoSummoned.transform);
      if ((ZComponent) keyValuePair.Value != (object) null)
      {
        keyValuePair.Value.VisualUpdate();
        if (keyValuePair.Value.type == EffectorType.Aura_of_decay || keyValuePair.Value.type == EffectorType.Lich_Aura_of_decay)
        {
          List<ZMyCollider> zmyColliderList = this.world.OverlapCircleAll(new Point((int) keyValuePair.Value.position.x, (int) keyValuePair.Value.position.y), keyValuePair.Value.collider.radius, (ZMyCollider) null, Inert.mask_entity_movement | Inert.mask_Phantom);
          for (int index = 0; index < zmyColliderList.Count; ++index)
          {
            if ((ZComponent) zmyColliderList[index].creature != (object) null)
              ZEffector.SpawnIndicatorOfDecay(zmyColliderList[index].creature, keyValuePair.Value);
            else if ((ZComponent) zmyColliderList[index].tower != (object) null && (ZComponent) zmyColliderList[index].tower.creature != (object) null)
              ZEffector.SpawnIndicatorOfDecay(zmyColliderList[index].tower.creature, keyValuePair.Value);
          }
        }
        else if (keyValuePair.Value.type == EffectorType.Vortex)
        {
          if (this.isClient && keyValuePair.Value.variable > 0)
            keyValuePair.Value.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (keyValuePair.Value.type == EffectorType.Conductor_Rod)
        {
          keyValuePair.Value.transform?.GetChild(1).gameObject.SetActive(true);
          keyValuePair.Value.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -90f);
        }
      }
    }
    foreach (KeyValuePair<int, ZMyCollider> keyValuePair in this.helper.colliderID)
      keyValuePair.Value.Initialize(keyValuePair.Value.position, this.world);
    UnityEngine.Resources.UnloadUnusedAssets();
    HUD.instance.FindPlayer();
    HUD.instance.SetupStartPanel();
    int index1 = 0;
    foreach (ZPerson player in this.players)
    {
      if (player != null && (ZComponent) player.first() != (object) null)
        HUD.instance.uiPlayerCharacters[index1].AddLevel3FromSerialization(player, player.first());
      if ((UnityEngine.Object) player.panelPlayer != (UnityEngine.Object) null && player.yourTurn)
        player.panelPlayer.rect.anchoredPosition = new Vector2(50f, player.panelPlayer.rect.anchoredPosition.y);
      ++index1;
    }
    Client.game.init_Client(this.serverState.busy);
    if ((UnityEngine.Object) CameraMovement.Instance != (UnityEngine.Object) null && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
    {
      CameraMovement.Instance.GotoPosition(Player.Instance.GetTransform.position);
      CameraMovement.Instance.target = Player.Instance.person.first();
      CameraMovement.Instance.state = CameraState.Follow;
    }
    HUD.instance.SetupMiniCamera();
    HUD.instance.SetArmageddonIcon();
    WindIndicatorUI.Instance?.Refresh();
    WindIndicatorUI.Instance?.gameObject.SetActive(this.isWindy);
    HUD.instance.txtGameID.text = "Game ID: #" + this.gameFacts.id.ToString();
    if (this.isTeam)
      HUD.instance.AddVSPanel();
    if (!Client.game.isSpectator)
    {
      if ((UnityEngine.Object) Player.Instance == (UnityEngine.Object) null)
        Debug.LogError((object) ("!!!!! NULL !!!!!!!" + (object) Client.game.players.Count + " " + (object) Client._gameFacts.players.Count));
      else if (Player.Instance.person != null && (ZComponent) Player.Instance.person.first() != (object) null && Player.Instance.person.ActivateableFamiliar != BookOf.Nothing)
      {
        BookOf activateableFamiliar = Player.Instance.person.ActivateableFamiliar;
        ZCreature zcreature = Player.Instance.person.first();
        if (this.isClient)
          HUD.ClientChangeElementalStaff(Player.Instance.person, activateableFamiliar, true);
        HUD.instance.fullBookImg.sprite = activateableFamiliar != BookOf.Seasons || !zcreature.parent.seasonISHoliday ? HUD.instance.fullBookSprites[(int) activateableFamiliar] : ClientResources.Instance.spellBookIconHoliday;
        HUD.instance.fullBookObj.SetActive(true);
        if (!Global.GetPrefBool("prefhasclickedfamiliar", false))
          HUD.instance.familiarHowTo.SetActive(true);
      }
    }
    this.resyncing = false;
  }

  public void SerializeList(myBinaryWriter w, List<ZPerson> list)
  {
    w.Write(list.Count);
    for (int index = 0; index < list.Count; ++index)
      w.Write(ZGame.GetID(list[index]));
  }

  public void DeserializeList(myBinaryReader r, List<ZPerson> list)
  {
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
      list.Add(this.helper.GetPlayer(r.ReadInt32()));
  }

  public void SerializeList(myBinaryWriter w, List<ZEffector> list)
  {
    w.Write(list.Count);
    for (int index = 0; index < list.Count; ++index)
      w.Write(ZGame.GetID(list[index]));
  }

  public void DeserializeList(myBinaryReader r, List<ZEffector> list)
  {
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
      list.Add(this.helper.GetEffector(r.ReadInt32()));
  }

  public void SerializeList(myBinaryWriter w, List<ZCreatureThorn> list)
  {
    w.Write(list.Count);
    for (int index = 0; index < list.Count; ++index)
      w.Write(ZGame.GetID((ZCreature) list[index]));
  }

  public void DeserializeList(myBinaryReader r, List<ZCreatureThorn> list)
  {
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
      list.Add(this.helper.GetThorn(r.ReadInt32()));
  }

  public void SerializeList(myBinaryWriter w, List<ZCreature> list)
  {
    w.Write(list.Count);
    for (int index = 0; index < list.Count; ++index)
      w.Write(ZGame.GetID(list[index]));
  }

  public void DeserializeList(myBinaryReader r, List<ZCreature> list)
  {
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
      list.Add(this.helper.GetCreature(r.ReadInt32()));
  }

  public static int GetID(ZCreature z) => !((ZComponent) z == (object) null) ? z.id : -1;

  public static int GetID(ZPerson z) => z != null ? (int) z.id : -1;

  public static int GetID(ZEffector z) => !((ZComponent) z == (object) null) ? z.id : -1;

  public static int GetID(ZSpell z) => !((ZComponent) z == (object) null) ? z.id : -1;

  public static int GetID(ZMyCollider z) => !((ZComponent) z == (object) null) ? z.id : -1;

  public void CloseGame()
  {
    Server.RemoveGameInstance(this.gameFacts.id);
    List<Connection> connectionList = new List<Connection>();
    foreach (ZPerson player in this.players)
    {
      if (player.Connected && player.connection.player.location == Location.Ingame)
      {
        player.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
        Server.MovePlayer(player.connection, Location.Lobby);
        connectionList.Add(player.connection);
        if (player.connection.player.player == player)
          player.connection.player.player = (ZPerson) null;
      }
      else if (player.connection != null && player.connection.State != ConnectionState.Connected && player.connection.player.gameNumber == this.gameFacts.id)
      {
        player.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
        player.connection.player.gameNumber = -1;
        if (player.connection.player.player == player)
          player.connection.player.player = (ZPerson) null;
      }
      else if (player.connection != null)
      {
        player.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
        if (player.connection.player.player == player)
          player.connection.player.player = (ZPerson) null;
      }
    }
    foreach (Connection c in connectionList)
      Server.SyncLobby(c);
    connectionList.Clear();
    this.CleanUp();
  }

  private IEnumerator<float> Cleanup()
  {
    this.serverState.busy = ServerState.Busy.Ended;
    this.SendStatus((byte) 3);
    for (int i = 0; i < 30; ++i)
    {
      for (float e = 0.0f; (double) e < 1.0; e += Time.deltaTime)
        yield return 0.0f;
      if (this.canSafelyClose || this.CheckRematch())
        yield break;
    }
    if (!this.CheckRematch(true))
    {
      try
      {
        this.CloseGame();
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
      }
    }
  }

  public void PlayerLeft(string name)
  {
    int index = this.players.FindIndex((Predicate<ZPerson>) (a => string.Equals(a.name, name)));
    if (index == -1)
      return;
    ZPerson player = this.players[index];
    player.didLeave = true;
    this.Resign(player);
  }

  public void Resign(ZPerson p)
  {
    if (p.controlled.Count > 0 && !p.didResign)
    {
      p.didResign = true;
      if (p == null || !p.yourTurn)
      {
        this.tempResignFix.Add(p);
        return;
      }
      this.serverState.busy = ServerState.Busy.Between_Turns;
      this.serverState.turnTime = this.PlayersMaxTurnTime + 3f;
    }
    this.checkToClose(2);
  }

  private bool checkToClose(int min)
  {
    int num = this.IsGameOver() ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.SendGameOver();
    return num != 0;
  }

  public void CleanUp(bool resyncing = false)
  {
    if ((UnityEngine.Object) this._map == (UnityEngine.Object) null || (UnityEngine.Object) this._map.gameObject == (UnityEngine.Object) null || this.map == null)
      return;
    foreach (Creature componentsInChild in this._map.transform.GetComponentsInChildren<Creature>())
    {
      if ((ZComponent) componentsInChild.serverObj != (object) null)
        componentsInChild.serverObj.isNull = true;
    }
    foreach (Spell componentsInChild in this._map.transform.GetComponentsInChildren<Spell>())
    {
      if ((ZComponent) componentsInChild.serverObj != (object) null)
        componentsInChild.serverObj.isNull = true;
    }
    UnityEngine.Object.Destroy((UnityEngine.Object) this._map.gameObject);
    if (this.serverUpdate != null)
      Timing.KillCoroutines(this.serverUpdate);
    this.ongoing.KillAllCoroutines();
    this.map?.CleanUp();
    this.world.map = (ZMap) null;
    this.map.world = (ZMyWorld) null;
    this.world = (ZMyWorld) null;
    this.map = (ZMap) null;
    if (this.gameFacts != null)
      this.gameFacts.game = (ZGame) null;
    if (this.isServer && !this.isClient)
    {
      foreach (ZPerson player in this.players)
      {
        if (player.connection != null)
          player.connection.DataReceived -= new EventHandler<DataReceivedEventArgs>(this.GameHandler);
      }
    }
    for (int index = this.spectators.Count - 1; index >= 0; --index)
    {
      Connection spectator = this.spectators[index];
      if (spectator != null && spectator.State == ConnectionState.Connected)
      {
        spectator.player.inBoat = false;
        spectator?.Close();
      }
    }
    this.spectators.Clear();
    this.MoveQue?.Clear();
    if (this.isClient)
    {
      CameraMovement.followTargets.Clear();
      Client._tutorial?.CleanUp();
    }
    if (!this.isClient || resyncing || Client.connection == null || this.isSpectator)
      return;
    Client.RemoveAllDataReceived();
    Client.connection.DataReceived += new EventHandler<DataReceivedEventArgs>(Client.LobbyHandler);
  }

  public void ServerStartGame()
  {
    this.isSandbox = false;
    this.isServer = true;
    this.isClient = false;
    this.CreatePlayers(this.gameFacts);
    this.serverState.playersTurn = (byte) new System.Random().Next(this.players.Count);
    this.players[(int) this.serverState.playersTurn].wasFirst = true;
    int numberPlayersPerTeam = this.gameFacts.GetNumberPlayersPerTeam();
    if (this.isTeam)
    {
      int num1 = (int) this.serverState.playersTurn % numberPlayersPerTeam;
      int num2 = num1 - 1;
      if (num2 < 0)
        num2 = numberPlayersPerTeam - 1;
      this.lastTeamsTurn = (int) this.serverState.playersTurn / numberPlayersPerTeam;
      for (int index = 0; index < this.teamIndex.Length; ++index)
        this.teamIndex[index] = index <= this.lastTeamsTurn ? num1 : num2;
    }
    this.map.doneCallback = (Action) (() =>
    {
      HUD.FindFullBooks(this);
      this.serverUpdate = Timing.RunCoroutine(this.FixedUpdate(), Segment.Update);
    });
    string path = Application.streamingAssetsPath + Path.DirectorySeparatorChar.ToString() + "Maps" + Path.DirectorySeparatorChar.ToString();
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    string[] array = ((IEnumerable<string>) Directory.GetFiles(path, "*.arcmap")).Concat<string>((IEnumerable<string>) Directory.GetFiles(path, "*.png")).ToArray<string>();
    int index1 = new System.Random().Next(0, array.Length);
    string file = array[index1];
    if (file.EndsWith(".arcmap"))
      Global.ReadArcMap(file);
    else
      Global.RedPngMap(file);
  }

  public void OnMapGeneratedCompleted() => this.SendAllMessage(this.map.serializedMap);

  public enum GameType
  {
    LowStandard,
    HighStandard,
    Party,
    All,
  }

  public class Resurection
  {
    public SpellEnum spell;
    public MyLocation position;
  }

  public class MinionBookTitan
  {
    public SpellEnum spell;
    public bool used;
  }

  public class TimelineData
  {
    public string description;
    public int frame;
  }

  public enum ReturnType
  {
    _byte,
    _int,
    _string,
    _bool,
  }

  public class DynamicType
  {
    public ZGame.ReturnType t;
    public byte b;
    public int i;
    public string s;
    public bool bl;

    public DynamicType(byte b)
    {
      this.b = b;
      this.t = ZGame.ReturnType._byte;
    }

    public DynamicType(int b)
    {
      this.i = b;
      this.t = ZGame.ReturnType._int;
    }

    public DynamicType(string b)
    {
      this.s = b;
      this.t = ZGame.ReturnType._string;
    }

    public DynamicType(bool b)
    {
      this.bl = b;
      this.t = ZGame.ReturnType._bool;
    }

    public override string ToString()
    {
      switch (this.t)
      {
        case ZGame.ReturnType._byte:
          return this.b.ToString();
        case ZGame.ReturnType._int:
          return this.i.ToString();
        case ZGame.ReturnType._string:
          return this.s.ToString();
        case ZGame.ReturnType._bool:
          return this.bl.ToString();
        default:
          return "Error";
      }
    }

    public int ToInt()
    {
      switch (this.t)
      {
        case ZGame.ReturnType._byte:
          return (int) this.b;
        case ZGame.ReturnType._int:
          return this.i;
        case ZGame.ReturnType._string:
          int result = 0;
          int.TryParse(this.s, out result);
          return result;
        case ZGame.ReturnType._bool:
          return !this.bl ? 0 : 1;
        default:
          return 0;
      }
    }
  }

  public struct ID2
  {
    public ZCreature creature;
    public int index;

    public ID2(ZCreature z, int i)
    {
      this.creature = z;
      this.index = i;
    }
  }

  public struct IDEffector
  {
    public ZEffector creature;
    public int index;

    public IDEffector(ZEffector z, int i)
    {
      this.creature = z;
      this.index = i;
    }
  }

  public struct IDSpell
  {
    public ZSpell creature;
    public int index;

    public IDSpell(ZSpell z, int i)
    {
      this.creature = z;
      this.index = i;
    }
  }

  public struct IDCollider
  {
    public ZMyCollider creature;
    public int index;

    public IDCollider(ZMyCollider z, int i)
    {
      this.creature = z;
      this.index = i;
    }
  }

  public class SerializationHelper
  {
    public List<ZGame.ID2> id_mounts = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_pacts = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_oldParents = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_daOgParent = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_stormShield = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_effectorStaticShield = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_Aura = new List<ZGame.ID2>();
    public List<ZGame.ID2> id_followColliders = new List<ZGame.ID2>();
    public List<ZGame.IDEffector> effector_partners = new List<ZGame.IDEffector>();
    public List<ZGame.IDEffector> effector_whoSummoned = new List<ZGame.IDEffector>();
    public List<ZGame.IDEffector> effector_infector = new List<ZGame.IDEffector>();
    public List<ZGame.IDEffector> effector_spell = new List<ZGame.IDEffector>();
    public List<ZGame.IDCollider> col_ref_creatures = new List<ZGame.IDCollider>();
    public List<ZGame.IDCollider> col_ref_effectors = new List<ZGame.IDCollider>();
    public List<ZGame.IDCollider> col_ref_spells = new List<ZGame.IDCollider>();
    public List<ZGame.IDSpell> spell_extraCheck = new List<ZGame.IDSpell>();
    public List<ZGame.IDSpell> spell_parent = new List<ZGame.IDSpell>();
    public List<ZGame.IDSpell> spell_hit = new List<ZGame.IDSpell>();
    public List<ZGame.IDSpell> spell_effector = new List<ZGame.IDSpell>();
    public Dictionary<int, ZSpell> spellID = new Dictionary<int, ZSpell>();
    public Dictionary<int, ZEffector> effectorID = new Dictionary<int, ZEffector>();
    public Dictionary<int, ZCreature> creatureID = new Dictionary<int, ZCreature>();
    public Dictionary<int, ZCreatureThorn> thornID = new Dictionary<int, ZCreatureThorn>();
    public Dictionary<int, ZMyCollider> colliderID = new Dictionary<int, ZMyCollider>();
    public Dictionary<int, ZPerson> playerID = new Dictionary<int, ZPerson>();

    public ZPerson GetPlayer(int id)
    {
      ZPerson player;
      this.playerID.TryGetValue(id, out player);
      return player;
    }

    public ZCreatureThorn GetThorn(int id)
    {
      ZCreatureThorn thorn;
      this.thornID.TryGetValue(id, out thorn);
      return thorn;
    }

    public ZCreature GetCreature(int id)
    {
      ZCreature creature;
      this.creatureID.TryGetValue(id, out creature);
      return creature;
    }

    public ZSpell Getspell(int id)
    {
      ZSpell zspell;
      this.spellID.TryGetValue(id, out zspell);
      return zspell;
    }

    public ZEffector GetEffector(int id)
    {
      ZEffector effector;
      this.effectorID.TryGetValue(id, out effector);
      return effector;
    }

    public ZMyCollider GetCollider(int id)
    {
      ZMyCollider collider;
      this.colliderID.TryGetValue(id, out collider);
      return collider;
    }
  }
}
