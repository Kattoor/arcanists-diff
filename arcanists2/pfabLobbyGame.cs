

using Hazel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class pfabLobbyGame : 
  MonoBehaviour,
  IPointerClickHandler,
  IEventSystemHandler,
  IPointerExitHandler,
  IRecycledScrollViewGenericItem
{
  public TMP_Text txtStatus;
  public TMP_Text txtOwner;
  public TMP_Text txtPlayers;
  public TMP_Text txtRating;
  public TMP_Text txtTime;
  public GameObject obj_gameFacts;
  public GameObject obj_ratedFacts;
  public TMP_Text txtRatedFacts;
  public TMP_Text ratedTime;
  public TMP_Text ratedMaps;
  public TMP_Text ratedTeams;
  public TMP_Text ratedModes;
  public TMP_Text ratedArma;
  public Button buttonJoin;
  public Image buttonJoinImg;
  public Image imgDescription;
  public Image imgInvite;
  public Image imgSpectators;
  public Image imgRated;
  public Image imgGameType;
  public Image imgTime;
  public Image imgMap;
  public Image imgTeam;
  public Image imgArcaneMod;
  public Image imgRandomSpells;
  public Image imgForcedSpells;
  public Image imgElementals;
  public Image imgZeroShield;
  public Image imgHealth;
  public Image imgCountdown;
  public Image imgArmageddon;
  public Image imgFTT;
  public Image imgBid;
  public Image imgWatchtower;
  private GameFacts gameFacts;
  private RatedFacts ratedFacts;
  private static HashSet<string> multiTeam = new HashSet<string>();
  private int last_seconds = -1;

  public GameFacts GetGameFacts => this.gameFacts;

  public RatedFacts GetRatedFacts => this.ratedFacts;

  public void HoverAll()
  {
    if (this.gameFacts != null)
      MyToolTip.Show(this.gameFacts.ToString((ZGame) null));
    else
      MyToolTip.Show(this.ratedFacts.ToString(true));
  }

  public void HoverDescription()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show(this.gameFacts.settings.FilteredDescription());
  }

  public void HoverInvite()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show(this.gameFacts.GetInviteMode().ToString().Replace('_', ' '));
  }

  public void HoverJoin()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Is the door open?");
  }

  public void HoverSpectators()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Allow Spectators: " + this.gameFacts.GetSpectatorMode().ToString().Replace('_', ' '));
  }

  public void HoverRated()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Rated: " + this.gameFacts.GetRatedMode().ToString().Replace('_', ' '));
  }

  public void HoverTime()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Turn time: " + (object) this.gameFacts.customTime + " seconds");
  }

  public void HoverLadderType()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Ladder: " + RatedFacts.GetGameTypeAsStringLong((int) this.gameFacts.gameType));
  }

  public void HoverMap()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Map: " + GameFacts.MapName(this.gameFacts.GetMapMode()));
  }

  public void HoverTeam()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show(this.gameFacts.GetTeamMode() ? "Teams of " + (object) this.gameFacts.GetNumberPlayersPerTeam() : "FFA");
  }

  public void HoverMod()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("Allow Arcane Monster: " + (this.gameFacts.GetAllowArcanePowers() ? "Players with this symbol <sprite=\"AccountIconsAll\" index=240> by their name can use an enhanced version of full arcane" : "No"));
  }

  public void HoverRandomSpells()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show((this.gameFacts.GetStyle() & GameStyle.Random_Spells) == ~GameStyle.Dont_Mind ? "Standard - You choose your own spells" : "Random Spells");
  }

  public void HoverForcedSpells()
  {
    if (this.gameFacts == null)
      return;
    GameStyle gameStyle = this.gameFacts.GetStyle() & GameStyle.Original_Spells_Only;
    MyToolTip.Show(gameStyle == ~GameStyle.Dont_Mind ? "Standard - All spells are useable" : gameStyle.ToString().Replace('_', ' '));
  }

  public void HoverElemental()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show((this.gameFacts.GetStyle() & GameStyle.Elementals) == ~GameStyle.Dont_Mind ? "Standard - Elementals are not active" : "Elementals - You start with a full book along side your 16 chosen spells");
  }

  public void HoverZeroShield()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("<b>Zero Shield</b> - Players CANNOT cast offensive spells the first turn ");
  }

  public void HoverArmageddon()
  {
    if (this.gameFacts == null)
      return;
    List<SpellEnum> customArmageddon = this.gameFacts.settings.customArmageddon;
    // ISSUE: explicit non-virtual call
    if ((customArmageddon != null ? (__nonvirtual (customArmageddon.Count) > 0 ? 1 : 0) : 0) != 0)
      MyToolTip.Show("<b>Armageddon:</b>\n" + HUD.GetCustomArmageddonName(this.gameFacts));
    else
      MyToolTip.Show("<b>Armageddon</b> - " + (this.gameFacts.GetArmageddon() == ~MapEnum.Dont_Mind ? GameFacts.ArmageddonName(this.gameFacts.GetMapMode(), this.gameFacts.GetStyle()) : GameFacts.ArmageddonName(this.gameFacts.GetArmageddon(), this.gameFacts.GetStyle())));
  }

  public void HoverFTT()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("<b>First turn teleport</b> - Players start in the water and get a free teleport on the first turn ");
  }

  public void HoverBid()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("<b>First Turn Bid</b> - Players anonymously bid lifepoints to see who goes first");
  }

  public void HoverHealth()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("<b>Starting Health</b> - " + (object) this.gameFacts.startHealth);
  }

  public void HoverCountdown()
  {
    if (this.gameFacts == null)
      return;
    MyToolTip.Show("<b>Countdown</b> - " + Global.IntToTime((int) this.gameFacts.countdownTime) + " +" + (object) this.gameFacts.countdownDelay + "s delay" + (this.gameFacts.countdownTime < (short) 0 ? (object) "<br>You lose if time runs out" : (object) "<br>You get 5 second turns if time runs out"));
  }

  public void LeaveHover() => MyToolTip.instance?._Close();

  public void Init(string s)
  {
    this.ratedFacts = (RatedFacts) null;
    this.gameFacts = (GameFacts) null;
    this.txtRatedFacts.text = s;
    this.buttonJoin.gameObject.SetActive(false);
    this.buttonJoin.onClick.RemoveAllListeners();
    this.obj_gameFacts.SetActive(false);
    this.obj_ratedFacts.SetActive(true);
  }

  public void Init(RatedFacts r)
  {
    this.ratedFacts = r;
    this.gameFacts = (GameFacts) null;
    r.ToString(p: this);
    this.obj_gameFacts.SetActive(false);
    this.obj_ratedFacts.SetActive(true);
    int index = 1;
    this.txtStatus.text = "Queue";
    this.buttonJoin.gameObject.SetActive(true);
    this.buttonJoin.interactable = true;
    this.buttonJoin.onClick.RemoveAllListeners();
    this.buttonJoin.onClick.AddListener((UnityAction) (() => LobbyMenu.instance.ratedTab.ClickedLobbyQueue(r, false)));
    SpriteState spriteState = this.buttonJoin.spriteState with
    {
      highlightedSprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinHover[index] : ClientResources.Instance.ogjoinHover[index],
      pressedSprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinPressed[index] : ClientResources.Instance.ogjoinPressed[index],
      disabledSprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinDefault[index] : ClientResources.Instance.ogjoinDefault[index]
    };
    this.buttonJoinImg.sprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinDefault[index] : ClientResources.Instance.ogjoinDefault[index];
    this.buttonJoin.spriteState = spriteState;
  }

  public void Init(GameFacts g)
  {
    this.ratedFacts = (RatedFacts) null;
    this.gameFacts = g;
    g.CalculateGameType();
    this.obj_gameFacts.SetActive(true);
    this.obj_ratedFacts.SetActive(false);
    int customPlayerCount = (int) g.customPlayerCount;
    InviteEnum inviteMode = g.GetInviteMode();
    SpriteState spriteState = this.buttonJoin.spriteState;
    int index1 = g.GetRatedMode() ? 1 : 0;
    if (g.GetAllowArcanePowers())
    {
      foreach (string player in g.players)
      {
        if (Client.GetAccount(player).accountType.has(AccountType.Arcane_Monster))
        {
          index1 = 3;
          break;
        }
      }
    }
    if (g.GetTournamentMode())
      index1 = 4;
    spriteState.highlightedSprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinHover[index1] : ClientResources.Instance.ogjoinHover[index1];
    spriteState.pressedSprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinPressed[index1] : ClientResources.Instance.ogjoinPressed[index1];
    spriteState.disabledSprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinDefault[index1] : ClientResources.Instance.ogjoinDefault[index1];
    this.buttonJoinImg.sprite = Controller.Instance.useColorScheme ? ClientResources.Instance.joinDefault[index1] : ClientResources.Instance.ogjoinDefault[index1];
    this.buttonJoin.spriteState = spriteState;
    switch (g.status)
    {
      case 0:
        if (g.players.Count < customPlayerCount)
        {
          if (inviteMode == InviteEnum.Open || inviteMode == InviteEnum.Friends || g.invitedPlayers.Contains(Client.Name) || inviteMode == InviteEnum.Clan && string.Equals(Client.GetAccount(g.players[0]).clan, Client.MyAccount.clan) || inviteMode == InviteEnum.Similar_Rating && Mathf.Abs((int) Client.GetAccount(g.players[0]).similarRating - (int) Client.MyAccount.similarRating) <= 500)
          {
            this.buttonJoin.gameObject.SetActive(true);
            this.buttonJoin.interactable = true;
            this.buttonJoin.onClick.RemoveAllListeners();
            this.buttonJoin.onClick.AddListener((UnityAction) (() => Client.AskTojoinGame(g.id)));
            this.txtStatus.text = inviteMode != InviteEnum.Friends ? "Join" : "Friends Only";
            break;
          }
          this.buttonJoin.gameObject.SetActive(true);
          this.buttonJoin.interactable = true;
          this.buttonJoin.onClick.RemoveAllListeners();
          this.buttonJoin.onClick.AddListener((UnityAction) (() => Client.AskTojoinGame(g.id)));
          this.txtStatus.text = "Ask to Join";
          break;
        }
        this.buttonJoin.interactable = false;
        this.txtStatus.text = "Full";
        break;
      case 1:
      case 2:
        if (g.GetSpectatorMode() || Client.GetAccount(Client.Name).accountType.isDev())
        {
          this.buttonJoin.gameObject.SetActive(true);
          this.buttonJoin.interactable = true;
          this.buttonJoin.onClick.RemoveAllListeners();
          this.buttonJoin.onClick.AddListener((UnityAction) (() => Client.AskToSpectate(g.id)));
          this.txtStatus.text = "Spectate";
          break;
        }
        this.buttonJoin.gameObject.SetActive(false);
        this.txtStatus.text = "Playing";
        break;
      case 3:
        this.buttonJoin.gameObject.SetActive(false);
        this.txtStatus.text = "Concluded";
        break;
    }
    if (g.players.Count == 0)
    {
      this.txtOwner.text = "";
    }
    else
    {
      this.txtOwner.text = g.players[0];
      int count1 = g.players.Count;
      if (g.GetMultiTeamMode())
      {
        pfabLobbyGame.multiTeam.Clear();
        foreach (string player in this.gameFacts.players)
          pfabLobbyGame.multiTeam.Add(player);
        int count2 = pfabLobbyGame.multiTeam.Count;
      }
      this.txtPlayers.text = g.status != (byte) 0 ? g.players.Count.ToString() : g.players.Count.ToString() + "/" + (object) customPlayerCount;
    }
    this.imgGameType.sprite = ClientResources.Instance._gameTypes[g.gameType.Clamp()];
    this.imgSpectators.sprite = ClientResources.Instance._iconsSpectate[g.GetSpectatorMode() ? 1 : 0];
    this.imgRated.sprite = ClientResources.Instance._iconsRated[g.GetRatedMode() ? 1 : 0];
    this.imgMap.sprite = ClientResources.Instance._iconsMap[GameFacts.GetMapIndex(g.GetMapMode())];
    List<SpellEnum> customArmageddon = g.settings.customArmageddon;
    // ISSUE: explicit non-virtual call
    if ((customArmageddon != null ? (__nonvirtual (customArmageddon.Count) > 0 ? 1 : 0) : 0) != 0)
    {
      this.imgArmageddon.sprite = Inert.Instance.ArmageddonIcons[0];
      for (int index2 = 0; index2 < g.settings.customArmageddon.Count; ++index2)
      {
        Spell spell = Inert.GetSpell(g.settings.customArmageddon[index2]);
        if ((UnityEngine.Object) spell != (UnityEngine.Object) null && (spell.level < 4 || GameFacts.AllowCustomArmageddon(spell.spellEnum)))
        {
          this.imgArmageddon.sprite = ClientResources.Instance.GetSpellIcon(spell.name);
          break;
        }
      }
    }
    else
      this.imgArmageddon.sprite = g.GetArmageddon() == ~MapEnum.Dont_Mind ? Inert.Instance.ArmageddonIcons[GameFacts.GetMapIndex(g.GetMapMode())] : Inert.Instance.ArmageddonIcons[GameFacts.GetMapIndex(g.GetArmageddon())];
    this.imgTeam.sprite = ClientResources.Instance._iconsTeam[g.GetTeamMode() ? (g.GetMultiTeamMode() ? 2 : 1) : 0];
    this.imgArcaneMod.gameObject.SetActive(g.GetAllowArcanePowers());
    this.imgForcedSpells.gameObject.SetActive(g.GetStyle().HasStyle(GameStyle.Original_Spells_Only));
    this.imgRandomSpells.gameObject.SetActive(g.GetStyle().HasStyle(GameStyle.Random_Spells));
    this.imgElementals.gameObject.SetActive(g.GetStyle().HasStyle(GameStyle.Elementals));
    this.imgZeroShield.gameObject.SetActive(g.GetStyle().HasStyle(GameStyle.Zero_Shield));
    this.imgFTT.gameObject.SetActive(g.GetStyle().HasStyle(GameStyle.First_Turn_Teleport));
    this.imgWatchtower.gameObject.SetActive(g.GetStyle().HasStyle(GameStyle.Watchtower));
    this.imgBid.gameObject.SetActive(false);
    this.imgCountdown.gameObject.SetActive(g.countdownTime != (short) 0);
    this.imgHealth.gameObject.SetActive(g.startHealth != (ushort) 250);
    this.imgDescription.gameObject.SetActive(!string.IsNullOrEmpty(this.gameFacts.settings.description));
    switch (g.GetInviteMode())
    {
      case InviteEnum.Invite_Only:
        this.imgInvite.sprite = ClientResources.Instance._iconsIvite[0];
        break;
      case InviteEnum.Clan:
        this.imgInvite.sprite = ClientResources.Instance._iconsIvite[1];
        break;
      case InviteEnum.Friends:
        this.imgInvite.sprite = ClientResources.Instance._iconsIvite[2];
        break;
      case InviteEnum.Similar_Rating:
        this.imgInvite.sprite = ClientResources.Instance._iconsIvite[3];
        break;
      case InviteEnum.Open:
        this.imgInvite.sprite = ClientResources.Instance._iconsIvite[4];
        break;
      case InviteEnum.BookClub:
        this.imgInvite.sprite = ClientResources.Instance._iconsIvite[5];
        break;
    }
    switch (g.GetTimeMode())
    {
      case TimeEnum.Five:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[9];
        break;
      case TimeEnum.One_Hundred_Twenty:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[0];
        break;
      case TimeEnum.Ninety:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[1];
        break;
      case TimeEnum.Sixty:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[2];
        break;
      case TimeEnum.Forty_Five:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[3];
        break;
      case TimeEnum.Thirty:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[4];
        break;
      case TimeEnum.Twenty:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[5];
        break;
      case TimeEnum.Ten:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[7];
        break;
      case TimeEnum.Seven:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[8];
        break;
      case TimeEnum.Fifteen:
        this.imgTime.sprite = ClientResources.Instance._iconsTime[6];
        break;
    }
    g.CalculateGameType();
    int num1 = 0;
    for (int index3 = 0; index3 < g.players.Count; ++index3)
    {
      int num2 = (int) Client.GetAccount(g.players[index3])[(int) g.gameType].rating;
      if (num2 < 0)
        num2 = 1000;
      num1 += num2;
    }
    if (g.players.Count > 1)
      num1 /= g.players.Count;
    this.txtRating.text = num1.ToString();
    this.txtTime.text = "";
    this.last_seconds = -1;
    this.FixedUpdate();
  }

  public void OnPointerExit(PointerEventData eventData) => MyToolTip.Close();

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Right || Input.touchSupported)
    {
      if (this.ratedFacts != null)
      {
        MyContextMenu myContextMenu = MyContextMenu.Show();
        myContextMenu.AddItem("Queue as an alternate setting", (Action) (() => LobbyMenu.instance.ratedTab.ClickedLobbyQueue(this.ratedFacts, true)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        myContextMenu.AddItem("Queue with these settings", (Action) (() => LobbyMenu.instance.ratedTab.ClickedLobbyQueue(this.ratedFacts, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        myContextMenu.Rebuild();
      }
      else
      {
        MyContextMenu myContextMenu = MyContextMenu.Show();
        pfabLobbyGame.multiTeam.Clear();
        foreach (string player in this.gameFacts.players)
        {
          if (pfabLobbyGame.multiTeam.Add(player))
          {
            string s = player;
            myContextMenu.AddItem(s, (Action) (() =>
            {
              if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
                HUD.instance.InitPrivateChat(s);
              else
                ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), s);
            }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }
        }
        if (Client.GetAccount(Client.Name).accountType.IsModPlus())
        {
          myContextMenu.AddSeperator();
          myContextMenu.AddItem("ID: " + (object) this.gameFacts.id, (Action) (() => Global.systemCopyBuffer = this.gameFacts.id.ToString()), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
        }
        if (this.gameFacts.players.Contains(Client.Name) && (this.gameFacts.status == (byte) 2 || this.gameFacts.status == (byte) 1))
        {
          myContextMenu.AddSeperator();
          myContextMenu.AddItem("Rejoin Game", (Action) (() => Client.Ask((byte) 93, this.gameFacts.id)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        }
        myContextMenu.Rebuild();
      }
    }
    else if (eventData.button == PointerEventData.InputButton.Middle)
    {
      if (this.gameFacts != null)
        MyToolTip.Show(this.gameFacts.ToString((ZGame) null));
      else
        MyToolTip.Show(this.ratedFacts.ToString(true));
    }
    else
    {
      if (eventData.button != PointerEventData.InputButton.Left)
        return;
      if (this.gameFacts != null)
        MyToolTip.Show(this.gameFacts.ToString((ZGame) null));
      else
        MyToolTip.Show(this.ratedFacts.ToString(true));
    }
  }

  private void FixedUpdate()
  {
    if (this.gameFacts == null || this.gameFacts.status == (byte) 0 || this.gameFacts.status == (byte) 3)
      return;
    int num1 = (int) ((double) Time.realtimeSinceStartup - (double) this.gameFacts.serverStartTime);
    int num2 = num1 % 60;
    if (this.last_seconds == num2)
      return;
    this.last_seconds = num2;
    this.txtTime.text = "<mspace=10>" + (object) (num1 / 60) + "</mspace>:<mspace=10>" + (num2 < 10 ? (object) ("0" + num2.ToString()) : (object) num2.ToString()) + "</mspace>";
  }

  public void Init(object obj, int index)
  {
    switch (obj)
    {
      case RatedFacts _:
        this.Init(obj as RatedFacts);
        break;
      case GameFacts _:
        this.Init(obj as GameFacts);
        break;
      case string _:
        this.Init(obj as string);
        break;
      default:
        throw new Exception("Unknown generic scroll view type: " + obj.GetType().ToString() + " >>> " + obj.ToString());
    }
  }

  [SpecialName]
  GameObject IRecycledScrollViewGenericItem.get_gameObject() => this.gameObject;
}
