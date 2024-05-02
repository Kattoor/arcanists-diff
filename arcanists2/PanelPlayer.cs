// Decompiled with JetBrains decompiler
// Type: PanelPlayer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class PanelPlayer : MonoBehaviour
{
  public bool newLayout;
  internal RectTransform rect;
  public RectTransform hpBar;
  public TextMeshProUGUI hpText;
  public TextMeshProUGUI nameText;
  public SlicedFilledImage hpBarNew;
  public RectTransform hpbarNewStars;
  public Image newGradient;
  public Image gradReversed;
  public Image head;
  public Image beard;
  public Image beard2;
  public Image beard3;
  public Image hat;
  public Image mouth;
  public Image bgBar;
  public Image bgAll;
  public Image bgStar;
  public GameObject summonObj;
  public TextMeshProUGUI summonText;
  private bool disposable;
  private bool resigned;
  [NonSerialized]
  public bool left;
  private bool defeated;

  public void Copy(PanelPlayer other)
  {
    this.disposable = other.disposable;
    other.disposable = false;
    this.rect = (RectTransform) this.transform;
    this.rect.anchoredPosition = other.rect.anchoredPosition;
    this.name = other.name;
    this.Copy(this.head, other.head);
    this.Copy(this.beard, other.beard);
    this.Copy(this.beard2, other.beard2);
    this.Copy(this.beard3, other.beard3);
    this.Copy(this.hat, other.hat);
    this.Copy(this.mouth, other.mouth);
    this.hpBarNew.fillAmount = other.hpBarNew.fillAmount;
    if ((UnityEngine.Object) this.newGradient != (UnityEngine.Object) null)
    {
      this.newGradient.fillAmount = this.hpBarNew.fillAmount;
      this.gradReversed.fillAmount = 1f - this.newGradient.fillAmount;
    }
    this.hpbarNewStars.anchoredPosition = other.hpbarNewStars.anchoredPosition;
    this.nameText.text = other.nameText.text;
    this.hpText.text = other.hpText.text;
    this.hpBar.sizeDelta = other.hpBar.sizeDelta;
    this.summonText.text = other.summonText.text;
    this.rect.anchoredPosition = other.rect.anchoredPosition;
    this.summonObj.SetActive(other.summonObj.activeSelf);
    this.Colorize(other.bgStar.color);
    this.beard3.transform.SetSiblingIndex(other.beard3.transform.GetSiblingIndex());
    this.beard2.transform.SetSiblingIndex(other.beard2.transform.GetSiblingIndex());
    this.beard.transform.SetSiblingIndex(other.beard.transform.GetSiblingIndex());
    this.nameText.enableWordWrapping = other.nameText.enableWordWrapping;
    this.nameText.overflowMode = other.nameText.overflowMode;
    if (this.newLayout && this.nameText.overflowMode != TextOverflowModes.Ellipsis)
      this.nameText.rectTransform.anchoredPosition = new Vector2(this.nameText.rectTransform.anchoredPosition.x, 0.0f);
    if (other.head.gameObject.activeInHierarchy)
      return;
    this.Disable();
  }

  private void Copy(Image a, Image b)
  {
    a.sprite = b.sprite;
    a.rectTransform.sizeDelta = b.rectTransform.sizeDelta;
    a.rectTransform.anchoredPosition = b.rectTransform.anchoredPosition;
    a.enabled = b.enabled;
  }

  public void MyTurn(bool v)
  {
    if (v)
      this.rect.anchoredPosition = new Vector2(50f, this.rect.anchoredPosition.y);
    else
      this.rect.anchoredPosition = new Vector2(12f, this.rect.anchoredPosition.y);
  }

  private void Dispose()
  {
    if (!this.disposable)
      return;
    this.disposable = false;
    try
    {
      Global.DestroySprite(this.head.sprite);
      Global.DestroySprite(this.beard.sprite);
      Global.DestroySprite(this.hat.sprite);
      Global.DestroySprite(this.beard2.sprite);
      Global.DestroySprite(this.beard3.sprite);
    }
    catch (Exception ex)
    {
    }
  }

  public void OnDestroy() => this.Dispose();

  private void FindCenterPivet(Image a, Sprite s)
  {
    Color32[] pixels32 = s.texture.GetPixels32();
    int y = 0;
    int num1 = s.texture.height;
    int x = 0;
    int num2 = s.texture.width;
    for (int index1 = 0; index1 < s.texture.height; ++index1)
    {
      for (int index2 = 0; index2 < s.texture.width; ++index2)
      {
        if (pixels32[index2 + index1 * s.texture.width].a > (byte) 0)
        {
          y = index1;
          index1 = s.texture.height;
          break;
        }
      }
    }
    for (int index3 = s.texture.height - 1; index3 > 0; --index3)
    {
      for (int index4 = 0; index4 < s.texture.width; ++index4)
      {
        if (pixels32[index4 + index3 * s.texture.width].a > (byte) 0)
        {
          num1 = index3;
          index3 = 0;
          break;
        }
      }
    }
    for (int index5 = 0; index5 < s.texture.width; ++index5)
    {
      for (int index6 = 0; index6 < s.texture.height; ++index6)
      {
        if (pixels32[index5 + index6 * s.texture.width].a > (byte) 0)
        {
          x = index5;
          index5 = s.texture.width;
          break;
        }
      }
    }
    for (int index7 = s.texture.width - 1; index7 > 0; --index7)
    {
      for (int index8 = 0; index8 < s.texture.height; ++index8)
      {
        if (pixels32[index7 + index8 * s.texture.width].a > (byte) 0)
        {
          num2 = index7;
          index7 = 0;
          break;
        }
      }
    }
    Color[] pixels = s.texture.GetPixels(x, y, num2 - x, num1 - y);
    Texture2D texture = new Texture2D(num2 - x, num1 - y);
    texture.SetPixels(pixels);
    texture.Apply();
    a.sprite = Global.AddSprite(Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) texture.width, (float) texture.height), new Vector2(0.5f, 0.5f), s.pixelsPerUnit));
    a.rectTransform.sizeDelta = new Vector2((float) Mathf.Clamp(texture.width, 48, 64), (float) Mathf.Clamp(texture.height, 48, 64));
    a.rectTransform.pivot = new Vector2(0.5f, 0.5f);
  }

  public void UpdateColors() => this.Colorize(this.bgStar.color);

  private void Colorize(Color c)
  {
    this.bgBar.color = c;
    this.bgStar.color = c;
    this.hpBarNew.color = c;
    if (!Global.GetPrefBool("prefcolorednames", !HUD.useNewPanelPlayer))
    {
      this.nameText.color = Color.white;
      if (this.newLayout)
      {
        this.hpText.color = Color.white;
        this.summonText.color = Color.white;
      }
      else
      {
        this.hpText.color = c;
        this.summonText.color = c;
      }
    }
    else
    {
      this.nameText.color = c;
      this.hpText.color = c;
      this.summonText.color = c;
    }
    if (!((UnityEngine.Object) this.newGradient != (UnityEngine.Object) null))
      return;
    this.newGradient.color = ColorHSV.AdjustColor(c, 0.0f, 0.0f, -0.2f);
  }

  public void Init(SettingsPlayer s, string name)
  {
    this.Dispose();
    this.disposable = true;
    ConfigurePlayer.ApplyOutfit(this, s, name);
    this.rect = (RectTransform) this.transform;
    this.Realign();
    if (s.indexHead == (byte) 79)
      this.head.rectTransform.SetAsLastSibling();
    else
      this.head.rectTransform.SetAsFirstSibling();
    this.beard.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard) ? 5 : 1);
    int num1 = ConfigurePlayer.BeardInFront(s.indexBeard) ? -1 : 0;
    this.beard2.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard2) ? 5 : 2 + num1);
    int num2 = num1 + (ConfigurePlayer.BeardInFront(s.indexBeard2) ? -1 : 0);
    this.beard3.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard3) ? 5 : 3 + num2);
  }

  public void Init(string n, SettingsPlayer s, Color c, Creature cre)
  {
    this.disposable = true;
    this.name = n;
    this.nameText.text = n;
    this.Colorize(c);
    ConfigurePlayer.ApplyOutfit(this, s, cre);
    if ((UnityEngine.Object) cre.head != (UnityEngine.Object) null)
    {
      Global.DestroySprite(this.head.sprite);
      this.head.sprite = Recolor.Copy(cre.head.sprite);
    }
    else if ((UnityEngine.Object) cre.body != (UnityEngine.Object) null)
    {
      Global.DestroySprite(this.head.sprite);
      this.head.sprite = Recolor.Copy(cre.body.sprite);
    }
    this.head.enabled = true;
    this.beard.enabled = false;
    this.beard2.enabled = false;
    this.beard3.enabled = false;
    this.hat.enabled = false;
    this.mouth.enabled = false;
    this.rect = (RectTransform) this.transform;
    this.FindCenterPivet(this.head, this.head.sprite);
  }

  public void Init(string n, SettingsPlayer s, Color c, Creature cre, bool player)
  {
    this.disposable = true;
    this.name = n;
    this.nameText.text = n;
    this.Colorize(c);
    ConfigurePlayer.ApplyOutfit(this, s, cre);
    bool flag = (int) s.indexBody != SettingsPlayer.sno_body2;
    this.head.enabled = flag;
    this.beard.enabled = flag;
    this.beard2.enabled = flag;
    this.beard3.enabled = flag;
    this.hat.enabled = flag;
    this.mouth.enabled = !SettingsPlayer.HeadNoMouth(n, s);
    this.rect = (RectTransform) this.transform;
    if ((UnityEngine.Object) cre.head != (UnityEngine.Object) null && cre.head.sortingOrder == -8)
      this.head.rectTransform.SetAsLastSibling();
    this.Realign();
    this.beard.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard) ? 5 : 1);
    int num1 = ConfigurePlayer.BeardInFront(s.indexBeard) ? -1 : 0;
    this.beard2.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard2) ? 5 : 2 + num1);
    int num2 = num1 + (ConfigurePlayer.BeardInFront(s.indexBeard2) ? -1 : 0);
    this.beard3.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard3) ? 5 : 3 + num2);
  }

  public void Init(string n, SettingsPlayer s, Color c)
  {
    this.disposable = true;
    this.name = n;
    this.nameText.text = n;
    this.Colorize(c);
    ConfigurePlayer.ApplyOutfit(this, s, n);
    bool flag = (int) s.indexBody != SettingsPlayer.sno_body2;
    this.head.enabled = flag;
    this.beard.enabled = flag;
    this.beard2.enabled = flag;
    this.beard3.enabled = flag;
    this.hat.enabled = flag;
    this.mouth.enabled = !SettingsPlayer.HeadNoMouth(n, s);
    this.rect = (RectTransform) this.transform;
    this.Realign();
    this.beard.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard) ? 5 : 1);
    int num1 = ConfigurePlayer.BeardInFront(s.indexBeard) ? -1 : 0;
    this.beard2.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard2) ? 5 : 2 + num1);
    int num2 = num1 + (ConfigurePlayer.BeardInFront(s.indexBeard2) ? -1 : 0);
    this.beard3.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(s.indexBeard3) ? 5 : 3 + num2);
  }

  private void Realign(Image a)
  {
    a.SetNativeSize2();
    a.rectTransform.pivot = new Vector2(a.sprite.pivot.x / (float) a.sprite.texture.width, a.sprite.pivot.y / (float) a.sprite.texture.height);
  }

  private void Realign()
  {
    this.Realign(this.head);
    this.Realign(this.beard);
    this.Realign(this.hat);
  }

  public void TurnMod(SettingsPlayer sp, Color c, Creature cre)
  {
    ConfigurePlayer.ApplyOutfit(this, sp, cre);
    this.Colorize(c);
    this.Realign();
  }

  public void TurnUndead(Sprite s, SettingsPlayer sp)
  {
    Global.DestroySprite(this.head.sprite);
    this.head.sprite = s;
    ConfigurePlayer.ApplyOutfit(this.head, sp);
    this.beard.enabled = false;
    this.beard2.enabled = false;
    this.beard3.enabled = false;
    this.hat.enabled = false;
    this.mouth.enabled = false;
    this.Realign();
  }

  public void SetSummons(ZPerson p)
  {
    int num1 = p.controlled.Count - 1;
    int num2 = 4 + (p.GetLevel(BookOf.Arcane) + 1) / 2 - p.stolenMinions.Count;
    if (num1 <= 0 && num2 >= 4)
      this.summonObj.SetActive(false);
    else
      this.summonObj.SetActive(true);
    this.summonText.text = num1.ToString() + "/" + (object) num2;
  }

  public void SetHP(int i, float maxHealth = 250f)
  {
    if (this.defeated)
    {
      if (i <= 0)
        return;
      this.defeated = false;
      this.nameText.text = this.name;
      this.hpText.text = i.ToString();
      this.hpBar.sizeDelta = new Vector2(Mathf.Min(390f, (float) ((double) Mathf.Min((float) i, 500f) / 250.0 * 190.0 + 10.0)), this.hpBar.sizeDelta.y);
      this.hpBarNew.fillAmount = (float) i / maxHealth;
      this.hpbarNewStars.anchoredPosition = new Vector2(this.hpBarNew.fillAmount * this.hpBarNew.rectTransform.sizeDelta.x, 0.0f);
      if ((UnityEngine.Object) this.newGradient != (UnityEngine.Object) null)
      {
        this.newGradient.fillAmount = this.hpBarNew.fillAmount;
        this.gradReversed.fillAmount = 1f - this.newGradient.fillAmount;
      }
      this.Enable();
    }
    else if (i <= 0)
    {
      this.defeated = true;
      this.nameText.text = this.name + " was defeated";
      if (this.left)
        this.nameText.text += " and left";
      this.Disable();
    }
    else
    {
      this.hpText.text = i.ToString();
      this.hpBar.sizeDelta = new Vector2((float) ((double) Mathf.Min((float) i, 500f) / 250.0 * 190.0 + 10.0), this.hpBar.sizeDelta.y);
      this.hpBarNew.fillAmount = (float) i / maxHealth;
      this.hpbarNewStars.anchoredPosition = new Vector2(this.hpBarNew.fillAmount * this.hpBarNew.rectTransform.sizeDelta.x, 0.0f);
      if (!((UnityEngine.Object) this.newGradient != (UnityEngine.Object) null))
        return;
      this.newGradient.fillAmount = this.hpBarNew.fillAmount;
      this.gradReversed.fillAmount = 1f - this.newGradient.fillAmount;
    }
  }

  public void Resigned()
  {
    this.Disable();
    if (this.resigned || this.defeated)
      return;
    this.resigned = true;
    if (this.left)
      this.nameText.text += " and resigned";
    else
      this.nameText.text += " resigned";
  }

  public void Left()
  {
    this.Disable();
    if (this.left)
      return;
    this.left = true;
    if (this.resigned || this.defeated)
      this.nameText.text += " and left";
    else
      this.nameText.text += " has left";
  }

  public void Rejoined()
  {
    this.left = false;
    this.nameText.text = this.nameText.text.Replace(" and left", "");
  }

  private void Disable()
  {
    for (int index = 0; index < this.transform.childCount; ++index)
      this.transform.GetChild(index).gameObject.SetActive(false);
    this.bgAll.enabled = false;
    this.nameText.gameObject.SetActive(true);
    this.nameText.enableWordWrapping = false;
    this.nameText.overflowMode = TextOverflowModes.Overflow;
    if (!this.newLayout)
      return;
    this.nameText.rectTransform.anchoredPosition = new Vector2(this.nameText.rectTransform.anchoredPosition.x, 0.0f);
  }

  private void Enable()
  {
    for (int index = 0; index < this.transform.childCount; ++index)
    {
      if (!this.transform.GetChild(index).CompareTag("Ignore"))
        this.transform.GetChild(index).gameObject.SetActive(true);
    }
    this.bgAll.enabled = true;
    this.nameText.enableWordWrapping = true;
    this.nameText.overflowMode = TextOverflowModes.Ellipsis;
    if (!this.newLayout)
      return;
    this.nameText.rectTransform.anchoredPosition = new Vector2(this.nameText.rectTransform.anchoredPosition.x, 10.5f);
  }
}
