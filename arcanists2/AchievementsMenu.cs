// Decompiled with JetBrains decompiler
// Type: AchievementsMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class AchievementsMenu : Catalogue
{
  public RectTransform container;
  public AchievementButton pfabItem;
  public Image img;
  public TMP_Text txtName;
  public TMP_Text txtObtained;
  public TMP_Text txtPoints;
  public TMP_Text txtDescription;
  public Image[] coins;
  public GameObject infoPanel;
  public GameObject statsPanel;
  public Image outfitImage;
  public List<GameObject> outfitImages = new List<GameObject>();
  public bool isStatic;
  public int _viewing = -1;
  private AchievementButton activeButton;

  public static AchievementsMenu Instance { get; private set; }

  private void Awake()
  {
    AchievementsMenu.Instance = this;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  public void OnClick(AchievementButton a, Achievement e)
  {
    if ((UnityEngine.Object) this.activeButton != (UnityEngine.Object) null)
      this.activeButton.button.AlwaysOn = false;
    this.isStatic = !this.isStatic || (Achievement) this._viewing != e;
    this.activeButton = a;
    if (this.isStatic)
      this.activeButton.button.AlwaysOn = true;
    this._viewing = (int) e;
    this.OnEnter(e, true);
  }

  public void OnEnter(Achievement e, bool v = false)
  {
    if (this.isStatic && !v)
      return;
    int index1 = (int) e;
    bool achievement = Client.cosmetics.achievements[index1];
    Achievements.Container container = Achievements.list[index1];
    this.img.sprite = achievement ? ClientResources.Instance._achievementIcons[index1] : ClientResources.Instance._achievementNotObtained;
    this.txtName.text = container.name;
    this.txtDescription.text = container.description;
    this.txtObtained.text = (achievement ? "Obtained!" : "<color=#FF0000FF>Not yet achieved</color>") + (container.ratedOnly ? "\n<color=#555500FF>Rated Only</color>" : "");
    this.txtPoints.text = "Orb points:\n" + container.points.ToString();
    for (int index2 = 0; index2 < this.outfitImages.Count; ++index2)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.outfitImages[index2]);
    this.outfitImages.Clear();
    int o = 0;
    foreach (List<SettingsPlayer.ByAchievement> byAchievementList in SettingsPlayer.LockedByAchievement)
    {
      foreach (SettingsPlayer.ByAchievement byAchievement in byAchievementList)
      {
        if (byAchievement.a == e)
        {
          Image image = UnityEngine.Object.Instantiate<Image>(this.outfitImage, this.outfitImage.transform.parent);
          image.sprite = OutfitButton.GetPreview(SettingsPlayer.GetOutfits(o)[byAchievement.index]);
          image.transform.GetChild(0).GetComponent<TMP_Text>().text = ((OutfitShortName) o).ToString().Replace("_", " ");
          image.gameObject.SetActive(true);
          this.outfitImages.Add(image.gameObject);
        }
      }
      ++o;
    }
    int index3;
    for (index3 = 0; index3 < (int) container.points / 100; ++index3)
      this.coins[index3].gameObject.SetActive(true);
    for (; index3 < this.coins.Length; ++index3)
      this.coins[index3].gameObject.SetActive(false);
    this.statsPanel.SetActive(true);
    this.infoPanel.SetActive(false);
  }

  public void OnExit()
  {
    if (this.isStatic)
      return;
    this.statsPanel.SetActive(false);
    this.infoPanel.SetActive(true);
  }

  private void Start()
  {
    int x = 5;
    int y = -5;
    int num = 0;
    int index = -1;
    foreach (Achievement achievement in (Achievement[]) Enum.GetValues(typeof (Achievement)))
    {
      if (achievement != Achievement.None)
      {
        ++index;
        if (Achievements.list[index].available)
        {
          AchievementButton achievementButton = UnityEngine.Object.Instantiate<AchievementButton>(this.pfabItem, (Transform) this.container);
          achievementButton.achievement = achievement;
          achievementButton.rect.anchoredPosition = new Vector2((float) x, (float) y);
          achievementButton.image.sprite = Client.cosmetics.achievements[index] ? ClientResources.Instance._achievementIcons[index] : ClientResources.Instance._achievementNotObtained;
          achievementButton.gameObject.SetActive(true);
          ++num;
          if (num % 10 == 0)
          {
            x = 5;
            y -= 110;
          }
          else
            x += 110;
        }
      }
    }
  }

  public void ClickMainMenu() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  private void OnDestroy()
  {
    if ((UnityEngine.Object) AchievementsMenu.Instance == (UnityEngine.Object) this)
      AchievementsMenu.Instance = (AchievementsMenu) null;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }
}
