

using Discord;
using System;
using UnityEngine;

#nullable disable
public class DiscordIntergration : MonoBehaviour
{
  private Discord.Discord discord;
  private OAuth2Token token;
  private ActivityManager activityManager;
  private Activity activity;
  private DiscordIntergration.Viewing viewing;
  private float _lastTry;
  private long date;

  public static DiscordIntergration Instance { get; private set; }

  private void Awake() => DiscordIntergration.Instance = this;

  private void Start()
  {
    try
    {
      this.discord = new Discord.Discord(633505532753346580L, 1UL);
      this.discord.GetApplicationManager();
      this.activityManager = this.discord.GetActivityManager();
      this.activity = new Activity()
      {
        State = "Login Page",
        Details = "Idle",
        Assets = {
          LargeImage = "logo_big",
          LargeText = "Arcanists 2",
          SmallImage = "logo_orb",
          SmallText = "Arc2"
        },
        Timestamps = {
          Start = this.UnixTimeNow()
        }
      };
      this.activityManager?.UpdateActivity(this.activity, new ActivityManager.UpdateActivityHandler(this.ActivityUpdateCallback));
    }
    catch (Exception ex)
    {
      this.discord = (Discord.Discord) null;
      this.activityManager = (ActivityManager) null;
      DiscordIntergration.Instance = (DiscordIntergration) null;
      this._lastTry = Time.realtimeSinceStartup + 15f;
    }
  }

  private void OnDestroy()
  {
    this.discord?.Dispose();
    if (!((UnityEngine.Object) DiscordIntergration.Instance == (UnityEngine.Object) this))
      return;
    DiscordIntergration.Instance = (DiscordIntergration) null;
  }

  private void Update()
  {
    bool? nullable = this.discord?.RunCallbacks();
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue || (double) this._lastTry >= (double) Time.realtimeSinceStartup)
      return;
    this._lastTry = Time.realtimeSinceStartup + 15f;
    this.Start();
  }

  private void AuthCallback(Result result, ref OAuth2Token oauth2Token)
  {
    Debug.Log((object) ("Token - " + (object) result));
    this.token = oauth2Token;
  }

  private void ActivityUpdateCallback(Result result)
  {
    if (result == Result.Ok)
      return;
    Debug.Log((object) ("Activity - " + (object) result));
  }

  public long UnixTimeNow()
  {
    return (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
  }

  private void NonGame() => this.activity.Party = new ActivityParty();

  private void UpdateActivity()
  {
    try
    {
      this.activityManager.UpdateActivity(this.activity, new ActivityManager.UpdateActivityHandler(this.ActivityUpdateCallback));
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.ToString());
      this.discord = (Discord.Discord) null;
      this.activityManager = (ActivityManager) null;
      DiscordIntergration.Instance = (DiscordIntergration) null;
      this.gameObject.SetActive(false);
    }
  }

  public void UpdateActivity(GameFacts gf, bool spectator, bool resetTime)
  {
    if (this.activityManager == null)
      return;
    if (resetTime || this.viewing != DiscordIntergration.Viewing.GameLobby || this.date == 0L)
      this.date = this.UnixTimeNow();
    this.activity.State = spectator ? "Spectating" : (gf.game == null || !gf.game.isReplay ? (gf.status == (byte) 0 ? (gf.GetRatedMode() ? "Rated Lobby" : "Unrated Lobby") : "In-game") : "Replay");
    this.activity.Details = gf.players[0] + "'s game - " + (object) gf.GetTimeInSeconds() + "s " + GameFacts.MapShortName(gf.GetMapMode()) + (gf.GetTeamMode() ? (object) " teams" : (object) "") + (gf.GetRatedMode() ? (object) " rated" : (object) "");
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.date
    };
    if (gf.status == (byte) 0)
      this.activity.Party = new ActivityParty()
      {
        Size = {
          CurrentSize = gf.players.Count,
          MaxSize = (int) gf.customPlayerCount
        }
      };
    else
      this.activity.Party = new ActivityParty()
      {
        Size = {
          CurrentSize = gf.players.Count,
          MaxSize = gf.players.Count
        }
      };
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.GameLobby;
  }

  public void UpdateActivityLobby()
  {
    if (this.activityManager == null)
      return;
    this.activity.State = "In Lobby";
    this.activity.Details = "Lobby Sitting";
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.Lobby;
  }

  public void UpdateActivitySandbox()
  {
    if (this.activityManager == null)
      return;
    this.activity.State = "Sandbox";
    this.activity.Details = "Learning new tricks";
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.MainMenu;
  }

  public void UpdateActivityTutorial(string n)
  {
    if (this.activityManager == null)
      return;
    int length = n.IndexOf("<");
    if (length >= 0)
      n = n.Substring(0, length);
    this.activity.State = "Tutorial";
    this.activity.Details = n;
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.MainMenu;
  }

  public void UpdateActivityMainMenu()
  {
    if (this.activityManager == null)
      return;
    this.activity.State = "Main Menu";
    this.activity.Details = "Admiring the artwork";
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.MainMenu;
  }

  public void UpdateActivityCharacterCreation()
  {
    if (this.activityManager == null)
      return;
    this.activity.State = "Character Creation";
    this.activity.Details = "Designing an awesome avatar";
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.CharacterCreation;
  }

  public void UpdateActivitySpellSelection()
  {
    if (this.activityManager == null)
      return;
    this.activity.State = "Spell Selection";
    this.activity.Details = "Coming up with a new strat";
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.SpellSelection;
  }

  public void UpdateActivityReplayMenu()
  {
    if (this.activityManager == null)
      return;
    this.activity.State = "Replay Menu";
    this.activity.Details = "Trying to find that replay...";
    this.activity.Timestamps = new ActivityTimestamps()
    {
      Start = this.UnixTimeNow()
    };
    this.NonGame();
    this.UpdateActivity();
    this.viewing = DiscordIntergration.Viewing.ReplayMenu;
  }

  public void UpdateNoUpdate()
  {
    int num = this.activity.State.IndexOf(":");
    string str = "";
    if ((UnityEngine.Object) AchievementsMenu.Instance != (UnityEngine.Object) null)
      str = " : Viewing Achievements";
    else if ((UnityEngine.Object) ClanMenu.Instance != (UnityEngine.Object) null)
      str = " : Viewing Clans";
    else if ((UnityEngine.Object) CreditsMenu.Instance != (UnityEngine.Object) null)
      str = CreditsMenu.Instance.type == CreditsMenu.Type.Credits ? " : Viewing Credits" : " : Viewing Rules";
    else if ((UnityEngine.Object) PatchMenu.Instance != (UnityEngine.Object) null)
      str = " : Viewing Patch Notes";
    if ((UnityEngine.Object) ChessUI.Instance != (UnityEngine.Object) null)
      str += " : Chess";
    this.activity.State = num >= 0 ? this.activity.State.Substring(0, num - 1) + str : this.activity.State + str;
    this.UpdateActivity();
  }

  public enum Viewing
  {
    None,
    MainMenu,
    Lobby,
    GameLobby,
    CharacterCreation,
    SpellSelection,
    Sandbox,
    ReplayMenu,
  }
}
