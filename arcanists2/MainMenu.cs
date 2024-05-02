

using Educative;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MainMenu : Catalogue
{
  public int mapX = 1000;
  public int mapY = 1000;
  public MapEnum mapType;
  public Image bgImage;
  public Image fgImage;
  public UIOnHover buttonMultiplayer;
  public UIOnHover buttonRankings;
  public UIOnHover buttonClans;
  public Image bigImage;
  public static int bigIndex = -1;
  public GameObject buttonVerifyDiscord;
  public GameObject test_server_overlay;
  [Header("Maps")]
  public TMP_Dropdown dropMaps;
  public RectTransform pointTutorialObj;
  [Header("Steam")]
  public GameObject butSteam;

  public static MainMenu Instance { get; private set; }

  private void Awake()
  {
    MainMenu.Instance = this;
    if (Client.offlineMode)
    {
      TMP_Text component = this.buttonMultiplayer.GetComponent<TMP_Text>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.text = "Login";
      else
        this.buttonMultiplayer.GetComponent<Text>().text = "Login";
      this.buttonRankings?.Interactable(false);
      this.buttonClans?.gameObject.SetActive(false);
    }
    if ((UnityEngine.Object) ClientResources.Instance != (UnityEngine.Object) null)
    {
      if (MainMenu.bigIndex == -1)
        MainMenu.bigIndex = new System.Random().Next(0, ClientResources.Instance.MainMenuSprites.Length);
      this.bigImage.sprite = ClientResources.Instance.MainMenuSprites[MainMenu.bigIndex];
    }
    Account account;
    if (!Client.offlineMode && LocalServerConn.UseEncryption && Client._accounts.TryGetValue(Client.Name, out account) && account.discord == 0UL)
      this.buttonVerifyDiscord.SetActive(true);
    this.butSteam.SetActive(!Global.GetPrefBool("steam", false));
  }

  private void Start()
  {
    AudioManager.PlayMusicMainMenu();
    this.PopulateAvailableMaps();
    DiscordIntergration.Instance?.UpdateActivityMainMenu();
    if (!Client.isConnected || Client.MyAccount.prestige != (byte) 0 || Client.joinedFrom != Client.JoinLocation.Mainmenu)
      return;
    for (int index = 0; index < Prestige.tutorialInfo.Count; ++index)
    {
      if (!Client.MyAccount.tutorials[index])
      {
        MyPopup.Show("Completing tutorials will reward you with wands, but only before you prestige for the first time.", this.pointTutorialObj, ClientResources.Instance.headMage);
        break;
      }
    }
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) MainMenu.Instance == (UnityEngine.Object) this))
      return;
    MainMenu.Instance = (MainMenu) null;
  }

  public void ClickTutorialEditor()
  {
    Controller.Instance.OpenMenu(Controller.Instance.MenuTutorialCodeEditor, false);
  }

  public void ClickTutorialEditorOLD()
  {
    Controller.Instance.OpenMenu(Controller.Instance.MenuTutorialEditor, false);
  }

  public void Hover(string s) => MyToolTip.Show(s);

  public void HoverExit() => MyToolTip.Close();

  public void ClickTutorial() => MainMenu.OpenTutorialMenu();

  public static void OpenTutorialMenu()
  {
    ChooseJsonDialog.Create(false, ChooseJsonDialog.Viewing.Default, (Action<string, Tutorial, int>) ((s, tut, _index) =>
    {
      tut._index = _index;
      MainMenu.StartTutorial(tut, false, ChooseJsonDialog.Instance.custom);
    }));
  }

  public void PopulateAvailableMaps()
  {
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    int length = ClientResources.Instance._iconsMapBig.Length;
    ((IEnumerable<MapEnum>) Enum.GetValues(typeof (MapEnum))).ToList<MapEnum>().Sort((Comparison<MapEnum>) ((a, b) => GameFacts.GetMapIndex(a) - GameFacts.GetMapIndex(b)));
    for (int index = 0; index < length; ++index)
      optionDataList.Add(new TMP_Dropdown.OptionData("", ClientResources.Instance._iconsMapBig[index]));
    this.dropMaps.options = optionDataList;
    this.dropMaps.gameObject.SetActive(true);
    this.dropMaps.SetValueWithoutNotify(PlayerPrefs.GetInt("prefsandboxmaps", 0));
  }

  public void SwitchMap(int i)
  {
    PlayerPrefs.SetInt("prefsandboxmaps", i);
    MenuBackgroundUpdater.SwitchBackgrounds(this.bgImage, this.fgImage);
  }

  public void ClickSteam()
  {
    Global.SetPrefBool("steam", true);
    this.butSteam.SetActive(false);
    Global.OpenURL("https://store.steampowered.com/app/2901550/Arcanists/");
  }

  public void ClickSandbox()
  {
    Client._gameFacts = new GameFacts();
    Client.joinedFrom = Client.JoinLocation.Mainmenu;
    Controller.Instance.InitMap(true);
    DiscordIntergration.Instance?.UpdateActivitySandbox();
  }

  public static void StartTutorial(Tutorial t, bool test, ChooseJsonDialog.Viewing custom)
  {
    t.ClickSandbox(test, custom);
    DiscordIntergration.Instance?.UpdateActivityTutorial(t.Name);
    Client.allowtutorialDebugging = false;
  }

  public void ClickMultiplayer()
  {
    Global.SetPrefBool("steam", true);
    if (Client.offlineMode)
    {
      Controller.Instance.OpenMenu(Controller.Instance.MenuLogin, false);
      if (!((UnityEngine.Object) Server.Instance != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) Server.Instance.gameObject);
    }
    else
      Client.AskToJoinLobby();
  }

  public void ClickDiscordLink() => Global.OpenURL(Server.discordLink);

  public void ClicWebsiteLink() => Global.OpenURL(Server.websiteLink);

  public void ClicWikiLink() => Global.OpenURL(Server.wikiLink);

  public void ClickCharacterCreation()
  {
    Controller.Instance.OpenMenu(Controller.Instance.MenuCharacterCreation, false);
  }

  public void ClickSpellSelection()
  {
    Controller.Instance.OpenMenu(Controller.Instance.MenuSpellSelection, false);
  }

  public void ClickOptions() => Controller.ShowSettingsMenu();

  public void ClickSwitchMenu() => Controller.Instance.ActivateOther();

  public void ClickReplay() => Controller.Instance.OpenMenu(Controller.Instance.MenuReplay, false);

  public void ClickRatingsMenu() => Controller.ShowPopup(Controller.Instance.MenuRatings);

  public void ClickAchievements() => Controller.ShowPopup(Controller.Instance.MenuAchievements);

  public void ClickPatchNotes() => Controller.ShowPopup(Controller.Instance.MenuPatchNotes);

  public void ClickCredits() => Controller.ShowPopup(CreditsMenu.Type.Credits);

  public void ClickRules() => Controller.ShowPopup(CreditsMenu.Type.Rules);

  public void ClickClans() => Controller.ShowPopup(Controller.Instance.MenuClanList);

  public void ClickQuit() => Application.Quit();

  public void ClickLogout()
  {
    Client.joinedFrom = Client.JoinLocation.Mainmenu;
    if (Client.connection != null)
    {
      Client.connection.Close();
      Client.connection = (Connection) null;
    }
    Controller.Instance.OpenMenu(Controller.Instance.MenuLogin, false);
  }
}
