// Decompiled with JetBrains decompiler
// Type: SpellButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class SpellButton : MonoBehaviour
{
  public RectTransform rectTransform;
  public RectTransform backdrop;
  public GameObject bg;
  public GameObject zero;
  public Image bgColor;
  public Image image;
  public TMP_Text txtName;
  public TMP_Text txtText;
  public TMP_Text txtError;
  public int index;
  public string nameOfSpell;
  public int error;
  private static Color DarkGray = new Color(0.3f, 0.3f, 0.3f);

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
    this.image.color = SpellButton.DarkGray;
    this.bgColor.color = SpellButton.DarkGray;
  }

  public void OnClick() => ClickSpell.Instance.OnClickIndex(this.index);

  public void OnHover() => ClickSpell.Instance.OnPointerEnter(this.index);

  public void OnExit() => ClickSpell.Instance.OnPointerExit(this.index);

  public void SetVisual(string s, int uses, int rechargeTime, bool maxedSummons)
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
      this.image.color = SpellButton.DarkGray;
      this.bgColor.color = SpellButton.DarkGray;
      this.txtText.text = rechargeTime > 0 ? rechargeTime.ToString() + "X" : "X";
      this.txtText.color = Color.red;
      this.txtText.alignment = TextAlignmentOptions.Midline;
    }
    else if (rechargeTime > 0)
    {
      if (this.error == 0)
        this.error = 107;
      this.txtText.gameObject.SetActive(true);
      this.image.color = SpellButton.DarkGray;
      this.bgColor.color = SpellButton.DarkGray;
      this.txtText.text = string.Concat((object) rechargeTime);
      this.txtText.color = Color.red;
      this.txtText.alignment = TextAlignmentOptions.Midline;
    }
    else if (uses > 0)
    {
      this.txtText.gameObject.SetActive(true);
      this.image.color = Color.white;
      this.bgColor.color = Color.white;
      this.txtText.text = string.Concat((object) uses);
      this.txtText.color = Color.white;
      this.txtText.alignment = TextAlignmentOptions.BottomRight;
    }
    else
    {
      this.image.color = Color.white;
      this.bgColor.color = Color.white;
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
