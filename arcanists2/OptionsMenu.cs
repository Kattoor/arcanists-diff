// Decompiled with JetBrains decompiler
// Type: OptionsMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class OptionsMenu : Catalogue
{
  public Toggle toggleFollowSpells;
  public Toggle toggleRefollowCamera;
  public Toggle toggleFPS;
  public Toggle toggleLockMeter;
  public Toggle toggleHD;
  public Toggle toggleBlood;
  public Toggle toggleSkipIntro;
  public Toggle toggleMapPing;
  public UIOnHover toggleMapPingSound;
  public Toggle toggleNativeKeyboard;
  public Toggle toggleConfirmResign;
  public Toggle toggleConfirmLeave;
  public Toggle toggleOverheadRender;
  public Toggle toggleOverheadSpectator;
  public Toggle toggleOverheadSound;
  public Toggle toggleNewSpellIcons;
  public Toggle toggleNewPanelPlayer;
  public Toggle toggleColoredNames;
  public Image bigImage;
  [Header("Sound")]
  public UIOnSlider sliderSound;
  public UIOnSlider sliderMusic;
  public UIOnSlider sliderTurnStartVolume;
  public UIOnSlider sliderFgColor;
  public UIOnSlider sliderBgColor;

  public static OptionsMenu Instance { get; private set; }

  private void Awake()
  {
    OptionsMenu.Instance = this;
    this.sliderSound.SetValue(PlayerPrefs.GetFloat("prefvolsound", 0.5f));
    this.sliderMusic.SetValue(PlayerPrefs.GetFloat("prefvolmusic", 0.5f));
    this.sliderTurnStartVolume.SetValue(PlayerPrefs.GetFloat("prefturnstartvolume", 0.5f));
    this.sliderSound.onPointerUp.AddListener((UnityAction<float>) (v => AudioManager.instance.InstancePlay(AudioManager.instance.spellBounce, PlayerPrefs.GetFloat("prefvolsound", 0.5f))));
    this.sliderTurnStartVolume.onPointerUp.AddListener((UnityAction<float>) (v => AudioManager.PlayTurnStart()));
    if (!((UnityEngine.Object) ClientResources.Instance != (UnityEngine.Object) null) || MainMenu.bigIndex <= -1 || !((UnityEngine.Object) this.bigImage != (UnityEngine.Object) null) || MainMenu.bigIndex >= ClientResources.Instance.MainMenuSprites.Length)
      return;
    this.bigImage.sprite = ClientResources.Instance.MainMenuSprites[MainMenu.bigIndex];
  }

  private void Start()
  {
    if ((UnityEngine.Object) this.toggleFollowSpells != (UnityEngine.Object) null)
    {
      this.toggleFollowSpells.isOn = Global.GetPrefBool("preffollowtargets", true);
      this.toggleFollowSpells.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleFollowSpells));
    }
    if ((UnityEngine.Object) this.toggleRefollowCamera != (UnityEngine.Object) null)
    {
      this.toggleRefollowCamera.isOn = Global.GetPrefBool("prefrefollowcamera", true);
      this.toggleRefollowCamera.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleRefollowCamera));
    }
    if ((UnityEngine.Object) this.toggleBlood != (UnityEngine.Object) null)
    {
      this.toggleBlood.isOn = Client.showBlood;
      this.toggleBlood.onValueChanged.AddListener((UnityAction<bool>) (v =>
      {
        Client.ToggleBlood(v);
        this.toggleBlood.isOn = v;
      }));
    }
    if ((UnityEngine.Object) this.toggleFPS != (UnityEngine.Object) null)
    {
      this.toggleFPS.isOn = Global.GetPrefBool("preffps", false);
      this.toggleFPS.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleFPS));
    }
    if ((UnityEngine.Object) this.toggleLockMeter != (UnityEngine.Object) null)
    {
      this.toggleLockMeter.isOn = Global.GetPrefBool("preflockmeter", false);
      this.toggleLockMeter.onValueChanged.AddListener(new UnityAction<bool>(this.LockMeter));
    }
    if ((UnityEngine.Object) this.toggleMapPing != (UnityEngine.Object) null)
    {
      this.toggleMapPing.isOn = Global.GetPrefBool("prefmapping", true);
      this.toggleMapPing.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleMapPing));
    }
    if ((UnityEngine.Object) this.toggleMapPingSound != (UnityEngine.Object) null)
    {
      this.toggleMapPingSound.AlwaysOn = !Global.GetPrefBool("prefmappingsound", true);
      this.toggleMapPingSound.onClick.AddListener(new UnityAction(this.ToggleMapPingSound));
    }
    if ((UnityEngine.Object) this.toggleHD != (UnityEngine.Object) null)
    {
      this.toggleHD.isOn = Global.GetPrefBool("prefhdbg", false);
      this.toggleHD.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleHD));
    }
    if ((UnityEngine.Object) this.toggleSkipIntro != (UnityEngine.Object) null)
    {
      this.toggleSkipIntro.isOn = Global.GetPrefBool("prefSkipIntro", false);
      this.toggleSkipIntro.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleSkipIntro));
    }
    if ((UnityEngine.Object) this.sliderBgColor != (UnityEngine.Object) null)
    {
      this.sliderBgColor.SetValue(PlayerPrefs.GetFloat("prefblackBg", 1f));
      this.sliderBgColor.onClick.AddListener((UnityAction<float>) (f =>
      {
        PlayerPrefs.SetFloat("prefblackBg", f);
        CameraMovement.PrefChanged();
      }));
    }
    if ((UnityEngine.Object) this.sliderFgColor != (UnityEngine.Object) null)
    {
      this.sliderFgColor.SetValue(PlayerPrefs.GetFloat("prefblackFg", PlayerPrefs.GetFloat("prefblackBg", 1f)));
      this.sliderFgColor.onClick.AddListener((UnityAction<float>) (f =>
      {
        PlayerPrefs.SetFloat("prefblackFg", f);
        CameraMovement.PrefChanged();
      }));
    }
    if ((UnityEngine.Object) this.toggleNativeKeyboard != (UnityEngine.Object) null)
      this.toggleNativeKeyboard.gameObject.SetActive(false);
    if ((UnityEngine.Object) this.toggleConfirmResign != (UnityEngine.Object) null)
    {
      this.toggleConfirmResign.isOn = !Global.GetPrefBool(HUD.doNotShowResign, false);
      this.toggleConfirmResign.onValueChanged.AddListener((UnityAction<bool>) (v => Global.SetPrefBool(HUD.doNotShowResign, !v)));
    }
    if ((UnityEngine.Object) this.toggleConfirmLeave != (UnityEngine.Object) null)
    {
      this.toggleConfirmLeave.isOn = !Global.GetPrefBool(HUD.doNotShowLeave, false);
      this.toggleConfirmLeave.onValueChanged.AddListener((UnityAction<bool>) (v => Global.SetPrefBool(HUD.doNotShowLeave, !v)));
    }
    if ((UnityEngine.Object) this.toggleOverheadRender != (UnityEngine.Object) null)
    {
      this.toggleOverheadRender.isOn = Client.renderEmoji;
      this.toggleOverheadRender.onValueChanged.AddListener((UnityAction<bool>) (v => Client.renderEmoji = v));
    }
    if ((UnityEngine.Object) this.toggleOverheadSpectator != (UnityEngine.Object) null)
    {
      this.toggleOverheadSpectator.isOn = Client.renderEmojiSpectator;
      this.toggleOverheadSpectator.onValueChanged.AddListener((UnityAction<bool>) (v => Client.renderEmojiSpectator = v));
    }
    if ((UnityEngine.Object) this.toggleOverheadSound != (UnityEngine.Object) null)
    {
      this.toggleOverheadSound.isOn = Client.emojiSound;
      this.toggleOverheadSound.onValueChanged.AddListener((UnityAction<bool>) (v => Client.emojiSound = v));
    }
    if ((UnityEngine.Object) this.toggleNewSpellIcons != (UnityEngine.Object) null)
    {
      this.toggleNewSpellIcons.isOn = Global.GetPrefBool("newspellicons", true);
      this.toggleNewSpellIcons.onValueChanged.AddListener((UnityAction<bool>) (v => HUD.ToggleSpellBgIcons(v)));
    }
    if ((UnityEngine.Object) this.toggleNewPanelPlayer != (UnityEngine.Object) null)
    {
      this.toggleNewPanelPlayer.isOn = Global.GetPrefBool("useNewPanelPlayer", true);
      this.toggleNewPanelPlayer.onValueChanged.AddListener((UnityAction<bool>) (v => HUD.TogglePanelPlayer(v)));
    }
    if (!((UnityEngine.Object) this.toggleColoredNames != (UnityEngine.Object) null))
      return;
    this.toggleColoredNames.isOn = Global.GetPrefBool("prefcolorednames", !HUD.useNewPanelPlayer);
    this.toggleColoredNames.onValueChanged.AddListener((UnityAction<bool>) (v => HUD.ToggleColoredNames(v)));
  }

  public static void ShowHUDContextMenu()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem("Toggle Spell Backgrounds", (Action) (() =>
    {
      bool flag = !Global.GetPrefBool("newspellicons", true);
      Global.SetPrefBool("newspellicons", flag);
      HUD.ToggleSpellBgIcons(flag);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("Toggle Player Panel", (Action) (() =>
    {
      bool flag = !Global.GetPrefBool("useNewPanelPlayer", true);
      Global.SetPrefBool("useNewPanelPlayer", flag);
      HUD.TogglePanelPlayer(flag);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("Toggle Colored Names", (Action) (() =>
    {
      bool flag = !Global.GetPrefBool("prefcolorednames", !HUD.useNewPanelPlayer);
      Global.SetPrefBool("prefcolorednames", flag);
      HUD.ToggleColoredNames(flag);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.Rebuild();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) OptionsMenu.Instance == (UnityEngine.Object) this))
      return;
    OptionsMenu.Instance = (OptionsMenu) null;
  }

  public void Tooltip(string s) => MyToolTip.Show(s);

  public void HideTooltip() => MyToolTip.Close();

  public void ToggleFPS(bool v)
  {
    Global.SetPrefBool("preffps", v);
    FpsToText.Instance?.gameObject.SetActive(v);
  }

  public void ToggleFollowSpells(bool v)
  {
    Global.SetPrefBool("preffollowtargets", v);
    if (!((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null))
      return;
    HUD.instance.FollowSpells = v;
    CameraMovement.FOLLOWTARGETS = HUD.instance.FollowSpells;
    CameraMovement.followTargets.Clear();
  }

  public void ToggleRefollowCamera(bool v)
  {
    CameraMovement.refollowCamera = v;
    Global.SetPrefBool("prefrefollowcamera", CameraMovement.refollowCamera);
    CameraMovement.followTargets.Clear();
  }

  public void LockMeter(bool v)
  {
    Global.SetPrefBool("preflockmeter", v);
    if (!((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null))
      return;
    Player.Instance.LockMeter = v;
  }

  public void ToggleMapPing(bool v) => Global.SetPrefBool("prefmapping", v);

  public void ToggleMapPingSound()
  {
    bool b = !Global.GetPrefBool("prefmappingsound", true);
    Global.SetPrefBool("prefmappingsound", b);
    this.toggleMapPingSound.AlwaysOn = !b;
  }

  public void ToggleHD(bool v)
  {
    Global.SetPrefBool("prefhdbg", v);
    if (!((UnityEngine.Object) CameraMovement.Instance != (UnityEngine.Object) null))
      return;
    CameraMovement.Instance.SetBounds();
  }

  public void ToggleSkipIntro(bool v) => Global.SetPrefBool("prefSkipIntro", v);

  public void UpdateVolumeSound(float f)
  {
    PlayerPrefs.SetFloat("prefvolsound", f);
    AudioManager.UpdateVolumeSound(f);
  }

  public void UpdateVolumeMusic(float f)
  {
    PlayerPrefs.SetFloat("prefvolmusic", f);
    AudioManager.UpdateVolumeMusic(f);
  }

  public void UpdateVolumeTurnStart(float f) => PlayerPrefs.SetFloat("prefturnstartvolume", f);

  public void ClickScreenSize() => Controller.ShowScreenSizeMenu();

  public void ClickChatBoxOnly() => ChatBox._ShowFilterOptions();

  public void ClickMainMenu()
  {
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    if (!ColorSchemeUI.ForceApply)
      return;
    Controller.Instance.ReopenMenu();
    ColorSchemeUI.ForceApply = false;
  }

  public void ClickControls() => Controller.ShowControlsMenu();

  public void ClickPatchNotes() => Controller.ShowPopup(Controller.Instance.MenuPatchNotes);

  public void ClickCredits() => Controller.ShowPopup(CreditsMenu.Type.Credits);

  public void ClickRules() => Controller.ShowPopup(CreditsMenu.Type.Rules);

  public void ClickDiscordLink() => Global.OpenURL(Server.discordLink);

  public void ClicWebsiteLink() => Global.OpenURL(Server.websiteLink);

  public void ClicWikiLink() => Global.OpenURL(Server.wikiLink);

  public void ClickColorScheme() => ColorSchemeUI.Create(Client.colorScheme);
}
