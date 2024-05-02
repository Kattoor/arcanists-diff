

using Hazel;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class OutfitButton : MonoBehaviour
{
  public Image image;
  public UIOnHover uihover;
  private int index;
  private Outfit outfit;

  internal int Index => this.index;

  public static Sprite GetPreview(Sprite s)
  {
    Sprite sprite = (Sprite) null;
    return ClientResources.Instance._overridePreviews.TryGetValue(s.name, out sprite) ? sprite : s;
  }

  public void SetSprite(Sprite s, Outfit o, int i, bool disabled)
  {
    this.index = i;
    this.outfit = o;
    this.image.sprite = OutfitButton.GetPreview(s);
    float num = Mathf.Min(1f, Mathf.Max((float) s.texture.height / 64f, (float) s.texture.width / 64f));
    this.image.rectTransform.sizeDelta = new Vector2(num * 28f, num * 28f);
    if (this.uihover.interactable != disabled)
      return;
    this.uihover.Interactable(!disabled);
  }

  public void SetCallBack(int i)
  {
    this.index = i;
    this.uihover.onClick.AddListener((UnityAction) (() => CharacterCreation.Instance.SelectIndex(this.index)));
    this.uihover.onRightClick.AddListener((UnityAction) (() => CharacterCreation.Instance.TryBuyTournamentOutfit(this.index)));
    this.uihover.onEnter.AddListener(new UnityAction(this.Hover));
    this.uihover.onExit.AddListener(new UnityAction(this.Leave));
  }

  private string ExtraHoverInfo() => "";

  public void Hover()
  {
    if (this.uihover.interactable)
    {
      CharacterCreation.Instance.outfitHoverObj.anchoredPosition = ((RectTransform) this.transform).anchoredPosition;
      CharacterCreation.Instance.outfitHoverObj.gameObject.SetActive(true);
    }
    Outfit viewing = CharacterCreation.GetViewing(CharacterCreation.Instance.viewing);
    if (viewing >= Outfit.LeftFoot || !CharacterCreation.Instance.optionNoTooltips.isOn || SettingsPlayer.CheckAlwaysUnlocked(viewing, this.index))
      return;
    Achievement index = SettingsPlayer.CheckAchievements(viewing, (int) (byte) this.index);
    if (index != Achievement.None)
    {
      MyToolTip.Show("Achievement required: <#00FFFF>" + Achievements.list[(int) index].name + this.ExtraHoverInfo());
    }
    else
    {
      AccountType accountType = SettingsPlayer.CheckAccountType(viewing, this.index);
      if (accountType != AccountType.None)
      {
        MyToolTip.Show("Account role required: <#FF0000>" + accountType.ToString().Replace("_", " ") + this.ExtraHoverInfo());
      }
      else
      {
        int num1 = SettingsPlayer.CheckPrestige(viewing, this.index);
        int num2 = SettingsPlayer.CheckExperience(viewing, this.index);
        int num3 = SettingsPlayer.CheckTournament(viewing, this.index);
        if (num1 > 0)
          MyToolTip.Show(string.Format("Prestige: <#00FF00>{0}</color>", (object) num1) + this.ExtraHoverInfo());
        else if (num2 > 0)
          MyToolTip.Show(string.Format("Experience Level: <#00FF00>{0}</color>", (object) num2) + this.ExtraHoverInfo());
        else if (num3 > 0)
          MyToolTip.Show(string.Format("<sprite name=\"tcoin\"> Tournament Coins: <#00FF00>{0}</color>", (object) num3) + this.ExtraHoverInfo());
        else if (SettingsPlayer.seasonHalloween.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == this.outfit && z.index == this.index)) != null)
          MyToolTip.Show("Seasonal login reward: <#FF8A00>Halloween</color>" + this.ExtraHoverInfo());
        else if (SettingsPlayer.seasonThanksgiving.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == this.outfit && z.index == this.index)) != null)
          MyToolTip.Show("Seasonal login reward: <#874400>Thanksgiving</color>" + this.ExtraHoverInfo());
        else if (SettingsPlayer.seasonEaster.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == this.outfit && z.index == this.index)) != null)
          MyToolTip.Show("Seasonal login reward: <#4444FF>Easter</color>" + this.ExtraHoverInfo());
        else if (SettingsPlayer.seasonChristmas.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == this.outfit && z.index == this.index)) != null)
        {
          MyToolTip.Show("Seasonal login reward: <#44FF00>Christmas</color>" + this.ExtraHoverInfo());
        }
        else
        {
          if (Client.cosmetics.array[(int) viewing][(int) (byte) this.index])
            return;
          MyToolTip.Show("Locked due to limited availability." + this.ExtraHoverInfo());
        }
      }
    }
  }

  public void Leave()
  {
    CharacterCreation.Instance.outfitHoverObj.gameObject.SetActive(false);
    MyToolTip.Close();
  }
}
