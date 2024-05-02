// Decompiled with JetBrains decompiler
// Type: OptionsMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
    if (!((Object) ClientResources.Instance != (Object) null) || MainMenu.bigIndex <= -1 || !((Object) this.bigImage != (Object) null) || MainMenu.bigIndex >= ClientResources.Instance.MainMenuSprites.Length)
      return;
    this.bigImage.sprite = ClientResources.Instance.MainMenuSprites[MainMenu.bigIndex];
  }

  private void Start()
  {
    if ((Object) this.toggleFollowSpells != (Object) null)
    {
      this.toggleFollowSpells.isOn = Global.GetPrefBool("preffollowtargets", true);
      this.toggleFollowSpells.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleFollowSpells));
    }
    if ((Object) this.toggleRefollowCamera != (Object) null)
    {
      this.toggleRefollowCamera.isOn = Global.GetPrefBool("prefrefollowcamera", true);
      this.toggleRefollowCamera.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleRefollowCamera));
    }
    if ((Object) this.toggleBlood != (Object) null)
    {
      this.toggleBlood.isOn = Client.showBlood;
      this.toggleBlood.onValueChanged.AddListener((UnityAction<bool>) (v =>
      {
        Client.ToggleBlood(v);
        this.toggleBlood.isOn = v;
      }));
    }
    if ((Object) this.toggleFPS != (Object) null)
    {
      this.toggleFPS.isOn = Global.GetPrefBool("preffps", false);
      this.toggleFPS.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleFPS));
    }
    if ((Object) this.toggleLockMeter != (Object) null)
    {
      this.toggleLockMeter.isOn = Global.GetPrefBool("preflockmeter", false);
      this.toggleLockMeter.onValueChanged.AddListener(new UnityAction<bool>(this.LockMeter));
    }
    if ((Object) this.toggleMapPing != (Object) null)
    {
      this.toggleMapPing.isOn = Global.GetPrefBool("prefmapping", true);
      this.toggleMapPing.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleMapPing));
    }
    if ((Object) this.toggleMapPingSound != (Object) null)
    {
      this.toggleMapPingSound.AlwaysOn = !Global.GetPrefBool("prefmappingsound", true);
      this.toggleMapPingSound.onClick.AddListener(new UnityAction(this.ToggleMapPingSound));
    }
    if ((Object) this.toggleHD != (Object) null)
    {
      this.toggleHD.isOn = Global.GetPrefBool("prefhdbg", false);
      this.toggleHD.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleHD));
    }
    if ((Object) this.toggleSkipIntro != (Object) null)
    {
      this.toggleSkipIntro.isOn = Global.GetPrefBool("prefSkipIntro", false);
      this.toggleSkipIntro.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleSkipIntro));
    }
    if ((Object) this.sliderBgColor != (Object) null)
    {
      this.sliderBgColor.SetValue(PlayerPrefs.GetFloat("prefblackBg", 1f));
      this.sliderBgColor.onClick.AddListener((UnityAction<float>) (f =>
      {
        PlayerPrefs.SetFloat("prefblackBg", f);
        CameraMovement.PrefChanged();
      }));
    }
    if ((Object) this.sliderFgColor != (Object) null)
    {
      this.sliderFgColor.SetValue(PlayerPrefs.GetFloat("prefblackFg", PlayerPrefs.GetFloat("prefblackBg", 1f)));
      this.sliderFgColor.onClick.AddListener((UnityAction<float>) (f =>
      {
        PlayerPrefs.SetFloat("prefblackFg", f);
        CameraMovement.PrefChanged();
      }));
    }
    if ((Object) this.toggleNativeKeyboard != (Object) null)
      this.toggleNativeKeyboard.gameObject.SetActive(false);
    if ((Object) this.toggleConfirmResign != (Object) null)
    {
      this.toggleConfirmResign.isOn = !Global.GetPrefBool(HUD.doNotShowResign, false);
      this.toggleConfirmResign.onValueChanged.AddListener((UnityAction<bool>) (v => Global.SetPrefBool(HUD.doNotShowResign, !v)));
    }
    if ((Object) this.toggleConfirmLeave != (Object) null)
    {
      this.toggleConfirmLeave.isOn = !Global.GetPrefBool(HUD.doNotShowLeave, false);
      this.toggleConfirmLeave.onValueChanged.AddListener((UnityAction<bool>) (v => Global.SetPrefBool(HUD.doNotShowLeave, !v)));
    }
    if ((Object) this.toggleOverheadRender != (Object) null)
    {
      this.toggleOverheadRender.isOn = Client.renderEmoji;
      this.toggleOverheadRender.onValueChanged.AddListener((UnityAction<bool>) (v => Client.renderEmoji = v));
    }
    if ((Object) this.toggleOverheadSpectator != (Object) null)
    {
      this.toggleOverheadSpectator.isOn = Client.renderEmojiSpectator;
      this.toggleOverheadSpectator.onValueChanged.AddListener((UnityAction<bool>) (v => Client.renderEmojiSpectator = v));
    }
    if (!((Object) this.toggleOverheadSound != (Object) null))
      return;
    this.toggleOverheadSound.isOn = Client.emojiSound;
    this.toggleOverheadSound.onValueChanged.AddListener((UnityAction<bool>) (v => Client.emojiSound = v));
  }

  private void OnDestroy()
  {
    if (!((Object) OptionsMenu.Instance == (Object) this))
      return;
    OptionsMenu.Instance = (OptionsMenu) null;
  }

  public void ToggleFPS(bool v)
  {
    Global.SetPrefBool("preffps", v);
    FpsToText.Instance?.gameObject.SetActive(v);
  }

  public void ToggleFollowSpells(bool v)
  {
    Global.SetPrefBool("preffollowtargets", v);
    if (!((Object) HUD.instance != (Object) null))
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
    if (!((Object) CameraMovement.Instance != (Object) null))
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

  public void ClickMainMenu()
  {
    Object.Destroy((Object) this.gameObject);
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
