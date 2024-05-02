// Decompiled with JetBrains decompiler
// Type: PrestigeLobbyUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class PrestigeLobbyUI : MonoBehaviour
{
  public TMP_Text txtName;
  public TMP_Text txtRating;
  public TMP_Text txtPrestige;
  public TMP_Text txtXp;
  public TMP_Text txtWands;
  public PanelPlayer panelPlayer;
  public Image imgPrestige;

  public static PrestigeLobbyUI Instance { get; private set; }

  public void Awake()
  {
    PrestigeLobbyUI.Instance = this;
    this.Refresh();
    this.RefreshOutfit();
    Client.gameType = (ZGame.GameType) Mathf.Clamp(PlayerPrefs.GetInt("gameType", 0), 0, 2);
  }

  private void Start()
  {
  }

  public void ToggleLadder()
  {
    LobbyMenu instance1 = LobbyMenu.instance;
    if ((instance1 != null ? (instance1.viewing == Viewing.Lobby ? 1 : 0) : 0) == 0)
    {
      UnratedMenu instance2 = UnratedMenu.instance;
      if ((instance2 != null ? (instance2.viewing == Viewing.Lobby ? 1 : 0) : 0) == 0)
        return;
    }
    PrestigeLobbyUI.NextGameType();
  }

  public static void NextGameType()
  {
    ++Client.gameType;
    if (Client.gameType > ZGame.GameType.Party)
      Client.gameType = ZGame.GameType.LowStandard;
    if (Client.gameType < ZGame.GameType.LowStandard)
      Client.gameType = ZGame.GameType.LowStandard;
    PlayerPrefs.SetInt("gameType", (int) Client.gameType);
    if ((Object) PrestigeLobbyUI.Instance != (Object) null)
      PrestigeLobbyUI.Rating(PrestigeLobbyUI.Instance.txtRating);
    Client.RefreshNames();
    LobbyMenu.instance?.RefreshGames();
  }

  public void Refresh(bool rating = true)
  {
    this.txtName.text = Client.Name;
    if (!rating)
    {
      LobbyMenu instance1 = LobbyMenu.instance;
      if ((instance1 != null ? (instance1.viewing == Viewing.Lobby ? 1 : 0) : 0) == 0)
      {
        RatedMenu instance2 = RatedMenu.instance;
        if ((instance2 != null ? (instance2.viewing == Viewing.Lobby ? 1 : 0) : 0) == 0)
          goto label_4;
      }
    }
    PrestigeLobbyUI.Rating(this.txtRating);
label_4:
    this.txtPrestige.text = "Prestige: " + (object) Client.MyAccount.prestige;
    this.txtXp.text = "XP: " + (object) (int) Client.MyAccount.bonusExperience + " Level: " + (object) Client.MyAccount.experience;
    this.txtWands.text = Client.MyAccount.wands.ToString();
    this.imgPrestige.sprite = ClientResources.Instance._iconsPrestige[Mathf.Clamp((int) Client.MyAccount.prestige, 0, 10)];
  }

  public static void Rating(TMP_Text txtRating)
  {
    txtRating.text = "<sprite name=\"LTS\"> " + Client.MyAccount.gameLowTime.ratingString + "<sprite name=\"HTS\"> " + Client.MyAccount.gameHighTime.ratingString + "<sprite name=\"PMO\"> " + Client.MyAccount.gameFun.ratingString;
  }

  public void RefreshOutfit() => this.panelPlayer?.Init(Client.settingsPlayer, Client.Name);

  private void OnDestroy()
  {
    if (!((Object) PrestigeLobbyUI.Instance == (Object) this))
      return;
    PrestigeLobbyUI.Instance = (PrestigeLobbyUI) null;
  }

  public void Hover() => pfabName.HoverRatingIcon(Client.MyAccount);

  public void HideToolTip() => MyToolTip.Close();
}
