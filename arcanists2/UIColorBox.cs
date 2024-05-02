// Decompiled with JetBrains decompiler
// Type: UIColorBox
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class UIColorBox : MonoBehaviour
{
  public GameObject pfabDisabled;
  public RectTransform rect_selected;
  public RectTransform rect_hover;
  public Image[] images;
  public UIOnHover[] uihover;
  public int ColorOverride = -1;

  private void Start()
  {
    for (byte index = 0; (int) index < this.uihover.Length; ++index)
    {
      byte e = index;
      this.uihover[(int) index].onClick.AddListener((UnityAction) (() => this.ClickPrimary(e)));
      this.uihover[(int) index].onEnter.AddListener((UnityAction) (() => this.Hover((int) e)));
      this.uihover[(int) index].onExit.AddListener((UnityAction) (() => this.Leave()));
    }
  }

  public void Set(Color[] c)
  {
    int index;
    for (index = 0; index < c.Length; ++index)
    {
      this.images[index].color = c[index];
      this.images[index].gameObject.SetActive(true);
    }
    for (; index < this.images.Length; ++index)
      this.images[index].gameObject.SetActive(false);
  }

  public void FindClosest()
  {
    Color32 a = CharacterCreation.Instance.settingsPlayer.coloring.Get(Outfit.None, (ColorType) this.ColorOverride);
    for (int index = 0; index < this.images.Length; ++index)
    {
      if (Global.CompareColors(a, (Color32) this.images[index].color))
      {
        this.rect_selected.anchoredPosition = this.images[index].rectTransform.anchoredPosition;
        this.rect_selected.gameObject.SetActive(true);
        return;
      }
    }
    this.rect_selected.gameObject.SetActive(false);
  }

  public void Hover(int i)
  {
    this.rect_hover.anchoredPosition = this.images[i].rectTransform.anchoredPosition;
    this.rect_hover.gameObject.SetActive(true);
  }

  public void Leave() => this.rect_hover.gameObject.SetActive(false);

  public void ClickPrimary(byte i)
  {
    if (this.ColorOverride >= 0)
    {
      CharacterCreation.Instance.settingsPlayer.coloring.Set(Outfit.None, (ColorType) this.ColorOverride, (Color32) this.images[(int) i].color);
      CharacterCreation.Instance.EquipAll();
      CharacterCreation.Instance.Changed();
      this.rect_selected.anchoredPosition = this.images[(int) i].rectTransform.anchoredPosition;
      this.rect_selected.gameObject.SetActive(true);
    }
    else
      CharacterCreation.Instance.picker.CurrentColor = this.images[(int) i].color;
  }
}
