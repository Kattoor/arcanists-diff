// Decompiled with JetBrains decompiler
// Type: SpellButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

#nullable disable
public class SpellButton : MonoBehaviour
{
  public RectTransform rectTransform;
  public RectTransform backdrop;
  public GameObject bg;
  public GameObject zero;
  public Image bgColor;
  public Image image;
  public NicerOutline outline;
  public TMP_Text txtName;
  public TMP_Text txtText;
  public TMP_Text txtError;
  public int index;
  public string nameOfSpell;
  public int error;
  private static Color DarkGray = new Color(0.3f, 0.3f, 0.3f);
  private static Color DarkerGray = new Color(0.15f, 0.15f, 0.15f);

  public void SetVisual(string s)
  {
    this.nameOfSpell = s;
    this.image.sprite = ClientResources.Instance.GetSpellIcon(s);
    this.bg.SetActive(true);
    if (!((Object) this.backdrop != (Object) null))
      return;
    this.backdrop.gameObject.SetActive(true);
  }

  public void ArcaneZero()
  {
    this.zero.SetActive(true);
    this.image.color = ClickSpell.GetColor(SpellButton.DarkGray);
    this.bgColor.color = ClickSpell.GetColor(SpellButton.DarkerGray);
  }

  public void Activate(bool newIcon)
  {
    this.outline.enabled = newIcon;
    if (newIcon)
      this.bgColor.sprite = ClientResources.Instance.spellBGIconNew;
    else
      this.bgColor.sprite = ClientResources.Instance.spellBGIconOld;
  }

  private void OnDisable() => ClickSpell.Instance.OnPointerExit(this);

  public void OnClick() => ClickSpell.Instance.OnClickIndex(this.index);

  public void OnHover() => ClickSpell.Instance.OnPointerEnter(this.index);

  public void OnExit() => ClickSpell.Instance.OnPointerExit(this.index);

  public void SetVisual(string realName, string s, int uses, int rechargeTime, bool maxedSummons)
  {
    this.nameOfSpell = s;
    this.txtName.text = s;
    this.image.sprite = ClientResources.Instance.GetSpellIcon(s);
    if (this.zero.activeInHierarchy)
      this.zero.SetActive(false);
    if (maxedSummons || uses == 0)
    {
      if (uses == 0 && this.error == 0)
        this.error = 108;
      this.txtText.gameObject.SetActive(true);
      this.image.color = ClickSpell.GetColor(SpellButton.DarkGray);
      this.bgColor.color = ClickSpell.GetColor(HUD.useNewSpellBgIcons ? SpellButton.DarkerGray : SpellButton.DarkGray);
      this.txtText.text = rechargeTime > 0 ? rechargeTime.ToString() + "X" : "X";
      this.txtText.color = ClickSpell.GetColor(Color.red);
      this.txtText.alignment = TextAlignmentOptions.Midline;
    }
    else if (rechargeTime > 0)
    {
      if (this.error == 0)
        this.error = 107;
      this.txtText.gameObject.SetActive(true);
      this.image.color = ClickSpell.GetColor(SpellButton.DarkGray);
      this.bgColor.color = ClickSpell.GetColor(HUD.useNewSpellBgIcons ? SpellButton.DarkerGray : SpellButton.DarkGray);
      this.txtText.text = string.Concat((object) rechargeTime);
      this.txtText.color = ClickSpell.GetColor(Color.red);
      this.txtText.alignment = TextAlignmentOptions.Midline;
    }
    else if (uses > 0)
    {
      this.txtText.gameObject.SetActive(true);
      this.image.color = ClickSpell.GetColor(Color.white);
      Image bgColor = this.bgColor;
      Color c;
      if (!HUD.useNewSpellBgIcons)
      {
        c = Color.white;
      }
      else
      {
        Spell spell = Inert.GetSpell(realName);
        c = spell != null ? spell.bookOf.ToColor() : Color.white;
      }
      Color color = ClickSpell.GetColor(c);
      bgColor.color = color;
      this.txtText.text = string.Concat((object) uses);
      this.txtText.color = ClickSpell.GetColor(Color.white);
      this.txtText.alignment = TextAlignmentOptions.BottomRight;
    }
    else
    {
      this.image.color = ClickSpell.GetColor(Color.white);
      Image bgColor = this.bgColor;
      Color c;
      if (!HUD.useNewSpellBgIcons)
      {
        c = Color.white;
      }
      else
      {
        Spell spell = Inert.GetSpell(realName);
        c = spell != null ? spell.bookOf.ToColor() : Color.white;
      }
      Color color = ClickSpell.GetColor(c);
      bgColor.color = color;
      this.txtText.gameObject.SetActive(false);
    }
    this.bg.SetActive(true);
    if (!((Object) this.backdrop != (Object) null))
      return;
    this.backdrop.gameObject.SetActive(true);
  }

  public void Hide()
  {
    this.nameOfSpell = "";
    this.bg.SetActive(false);
    if (!((Object) this.backdrop != (Object) null))
      return;
    this.backdrop.gameObject.SetActive(false);
  }
}
