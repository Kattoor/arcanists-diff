

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class UIPlayerCharacter : MonoBehaviour
{
  public Image leftArm;
  public Image leftFoot;
  public Image body;
  public Image head;
  public Image beard;
  public Image beard2;
  public Image beard3;
  public Image mouth;
  public Image hat;
  public Image rightFoot;
  public Image rightArm;
  public RectTransform pfabScroll;
  public Text pName;
  public Image pBGColor;
  public Text txtRematch;
  public List<Image> spells = new List<Image>();
  public RectTransform[] transforms = new RectTransform[9];
  private Vector2[] startPos = new Vector2[11];
  private Vector2[] wonPos = new Vector2[11];
  private const float distance = 30f;
  private bool initialized;
  private Coroutine coroutine;
  private float t;
  private Vector2 pos = Vector2.zero;
  private const float speed = 3f;
  internal bool disposable;
  internal int myID = 1000;
  private int i;
  [NonSerialized]
  public UIPlayerCharacter.AnimState state;

  [ContextMenu("hi")]
  private void UpdateTransforms()
  {
    List<RectTransform> list = ((IEnumerable<RectTransform>) this.transforms).ToList<RectTransform>();
    Transform child1 = this.transform.GetChild(0).GetChild(5);
    list.Add(child1 as RectTransform);
    Transform child2 = this.transform.GetChild(0).GetChild(6);
    list.Add(child2 as RectTransform);
    this.transforms = list.ToArray();
  }

  public void SetRematchText(string s, bool animate, Color c)
  {
    this.txtRematch.text = s;
    this.txtRematch.color = c;
    this.txtRematch.gameObject.SetActive(true);
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    this.coroutine = (Coroutine) null;
    if (!animate)
      return;
    this.coroutine = HUD.instance.StartCoroutine(this.animateRematchTxt());
  }

  private IEnumerator animateRematchTxt()
  {
    int i = 0;
    float f = 0.0f;
    while (true)
    {
      f += Time.deltaTime * 1.5f;
      if ((double) f >= 1.0)
      {
        f = 0.0f;
        if (i == 3)
        {
          this.txtRematch.text = this.txtRematch.text.Substring(0, this.txtRematch.text.Length - 3);
          i = 0;
        }
        else
        {
          this.txtRematch.text += ".";
          ++i;
        }
      }
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private void Awake()
  {
    if (this.initialized)
      return;
    this.initialized = true;
    this.startPos[0] = this.leftArm.rectTransform.anchoredPosition;
    this.startPos[1] = this.leftFoot.rectTransform.anchoredPosition;
    this.startPos[2] = this.body.rectTransform.anchoredPosition;
    this.startPos[3] = this.head.rectTransform.anchoredPosition;
    this.startPos[4] = this.beard.rectTransform.anchoredPosition;
    this.startPos[5] = this.mouth.rectTransform.anchoredPosition;
    this.startPos[6] = this.hat.rectTransform.anchoredPosition;
    this.startPos[7] = this.rightFoot.rectTransform.anchoredPosition;
    this.startPos[8] = this.rightArm.rectTransform.anchoredPosition;
    this.startPos[9] = this.beard2.rectTransform.anchoredPosition;
    this.startPos[10] = this.beard3.rectTransform.anchoredPosition;
    this.wonPos[0] = this.leftArm.rectTransform.anchoredPosition + new Vector2(0.0f, 20f);
    this.wonPos[1] = this.leftFoot.rectTransform.anchoredPosition + new Vector2(0.0f, 20f);
    this.wonPos[2] = this.body.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[3] = this.head.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[4] = this.beard.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[5] = this.mouth.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[6] = this.hat.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[7] = this.rightFoot.rectTransform.anchoredPosition;
    this.wonPos[8] = this.rightArm.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[9] = this.beard2.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
    this.wonPos[10] = this.beard3.rectTransform.anchoredPosition + new Vector2(0.0f, 30f);
  }

  private void Update()
  {
    if (this.state == UIPlayerCharacter.AnimState.None)
      return;
    if ((this.state & UIPlayerCharacter.AnimState.Up) != UIPlayerCharacter.AnimState.None)
    {
      this.t += Time.deltaTime * 3f;
      if ((double) this.t >= 1.0)
      {
        this.t = 1f;
        this.state = (UIPlayerCharacter.AnimState) ((int) this.state << 1);
      }
      for (int index = 0; index < 9; index += 8)
      {
        this.pos.x = this.startPos[index].x;
        this.pos.y = Mathf.SmoothStep(this.startPos[index].y, this.startPos[index].y + 15f, this.t);
        this.transforms[index].anchoredPosition = this.pos;
      }
    }
    else if ((this.state & UIPlayerCharacter.AnimState.Down) != UIPlayerCharacter.AnimState.None)
    {
      this.t -= Time.deltaTime * 3f;
      if ((double) this.t <= 0.0)
      {
        this.t = 0.0f;
        this.state = (UIPlayerCharacter.AnimState) ((int) this.state << 1);
      }
      for (int index = 0; index < 9; index += 8)
      {
        this.pos.x = this.startPos[index].x;
        this.pos.y = Mathf.SmoothStep(this.startPos[index].y, this.startPos[index].y + 15f, this.t);
        this.transforms[index].anchoredPosition = this.pos;
      }
    }
    else if (this.state == UIPlayerCharacter.AnimState.jump)
    {
      this.t += Time.deltaTime * 3f;
      if ((double) this.t >= 1.0)
      {
        this.t = 1f;
        this.state = (UIPlayerCharacter.AnimState) ((int) this.state << 1);
      }
      for (int index = 0; index < 11; ++index)
      {
        this.pos.x = this.startPos[index].x;
        this.pos.y = Mathf.SmoothStep(this.startPos[index].y, this.wonPos[index].y, this.t);
        this.transforms[index].anchoredPosition = this.pos;
      }
    }
    else if (this.state == UIPlayerCharacter.AnimState.fall)
    {
      this.t -= Time.deltaTime * 3f;
      if ((double) this.t <= 0.0)
      {
        this.t = 0.0f;
        this.state = (UIPlayerCharacter.AnimState) ((int) this.state << 1);
      }
      for (int index = 0; index < 11; ++index)
      {
        this.pos.x = this.startPos[index].x;
        this.pos.y = Mathf.SmoothStep(this.startPos[index].y, this.wonPos[index].y, this.t);
        this.transforms[index].anchoredPosition = this.pos;
      }
    }
    else
      this.state = UIPlayerCharacter.AnimState.Won;
  }

  private void OnDestroy() => this.Cleanup();

  public void Cleanup()
  {
    if (!this.disposable)
      return;
    try
    {
      Global.DestroySprite(this.head?.sprite);
      Global.DestroySprite(this.body?.sprite);
      Global.DestroySprite(this.hat?.sprite);
      Global.DestroySprite(this.leftArm?.sprite);
      Global.DestroySprite(this.rightArm?.sprite);
      Global.DestroySprite(this.beard?.sprite);
      Global.DestroySprite(this.beard2?.sprite);
      Global.DestroySprite(this.beard3?.sprite);
      Global.DestroySprite(this.head?.sprite);
      Global.DestroySprite(this.head?.sprite);
      Global.DestroySprite(this.leftFoot?.sprite);
      Global.DestroySprite(this.rightFoot?.sprite);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  private void SetSprite(Image a, SpriteRenderer b)
  {
    if ((UnityEngine.Object) b == (UnityEngine.Object) null)
      return;
    a.sprite = Recolor.Copy(b.sprite);
    a.rectTransform.pivot = new Vector2(b.sprite.pivot.x / (float) a.sprite.texture.width, b.sprite.pivot.y / (float) a.sprite.texture.height);
  }

  private void Realign(Image a)
  {
    a.SetNativeSize2();
    a.rectTransform.pivot = new Vector2(a.sprite.pivot.x / (float) a.sprite.texture.width, a.sprite.pivot.y / (float) a.sprite.texture.height);
  }

  public void Realign()
  {
    this.Realign(this.body);
    this.Realign(this.head);
    this.Realign(this.beard);
    this.Realign(this.beard2);
    this.Realign(this.beard3);
    this.Realign(this.rightArm);
    this.Realign(this.leftArm);
    this.Realign(this.hat);
    this.Realign(this.leftFoot);
    this.Realign(this.rightFoot);
  }

  public void Copy(ZPerson p)
  {
    this.Cleanup();
    this.disposable = true;
    this.myID = (int) p.id;
    if (p.controlled.Count == 0)
    {
      ConfigurePlayer.EquipAll(this, p.name, p.settingsPlayer);
    }
    else
    {
      ZCreature zcreature = p.controlled[0];
      this.SetSprite(this.leftArm, zcreature.leftArm.sprite.texture.width != 2 || zcreature.leftArm.transform.childCount <= 0 ? zcreature.leftArm : zcreature.leftArm.transform.GetChild(0).GetComponent<SpriteRenderer>());
      this.SetSprite(this.body, zcreature.body);
      this.SetSprite(this.head, zcreature.head);
      this.SetSprite(this.beard, zcreature.beard);
      this.SetSprite(this.beard2, zcreature.beard2);
      this.SetSprite(this.beard3, zcreature.beard3);
      this.mouth.sprite = zcreature.mouth.sprite;
      this.SetSprite(this.hat, zcreature.hat);
      this.SetSprite(this.rightArm, zcreature.rightArm.sprite.texture.width != 2 || zcreature.rightArm.transform.childCount <= 0 ? zcreature.rightArm : zcreature.rightArm.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>());
      this.SetSprite(this.rightFoot, zcreature.rightFoot);
      this.SetSprite(this.leftFoot, zcreature.leftFoot);
    }
    this.Realign();
    this.pName.text = p.name;
    this.mouth.enabled = !SettingsPlayer.HeadNoMouth(p.name, p.settingsPlayer);
    bool flag = (int) p.settingsPlayer.indexBody != SettingsPlayer.sno_body2;
    this.leftArm.enabled = flag;
    this.head.enabled = flag;
    this.beard.enabled = flag;
    this.beard2.enabled = flag;
    this.beard3.enabled = flag;
    this.hat.enabled = flag;
    this.rightArm.enabled = flag;
    this.leftFoot.enabled = flag;
    this.rightFoot.enabled = flag;
    this.pBGColor.color = p.clientColor;
    this.beard.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(p.settingsPlayer.indexBeard) ? 9 : 4);
    int num1 = ConfigurePlayer.BeardInFront(p.settingsPlayer.indexBeard) ? -1 : 0;
    this.beard2.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(p.settingsPlayer.indexBeard2) ? 9 : 5 + num1);
    int num2 = num1 + (ConfigurePlayer.BeardInFront(p.settingsPlayer.indexBeard2) ? -1 : 0);
    this.beard3.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(p.settingsPlayer.indexBeard3) ? 9 : 6 + num2);
    foreach (byte spell1 in p.settingsPlayer.spells)
    {
      if ((int) spell1 < Inert.Instance.spells.Count && spell1 < byte.MaxValue)
      {
        Spell spell2 = !p.seasonISHoliday || spell1 < (byte) 120 || spell1 > (byte) 131 ? Inert.Instance._spells[(int) spell1] : Inert.Instance.holidaySpells[(int) spell1 - 120];
        if (spell2.level == 3)
          this.AddLevel3(spell2);
      }
    }
    if (p.game.isElementals)
    {
      int num3 = (int) p.settingsPlayer.fullBook - 1;
      int num4 = p.game.gameFacts.GetStyle().HasStyle(GameStyle.Original_Spells_Only) ? 9 : (int) RandomExtensions.LastBook();
      if (num3 < 0 || num3 > num4)
        num3 = 0;
      int index1 = num3 * 12;
      int index2 = 0;
      while (index2 < 12)
      {
        Spell spell = !p.seasonISHoliday || num3 != 10 ? Inert.Instance._spells[index1] : Inert.Instance.holidaySpells[index2];
        if (spell.level == 3)
          this.AddLevel3(spell);
        ++index2;
        ++index1;
      }
    }
    foreach (ZGame.MinionBookTitan minionBookTitan in p.minionBookTitans)
      this.AddLevel3(Inert.GetSpell(minionBookTitan.spell));
    this.gameObject.SetActive(true);
  }

  public void AddLevel3FromSerialization(ZPerson p, ZCreature c)
  {
    foreach (SpellSlot spell1 in c.spells)
    {
      Spell spell2 = spell1.spell;
      if (!spell1.isPresent && spell2.level == 3)
        this.AddLevel3(spell2);
    }
    foreach (ZGame.MinionBookTitan minionBookTitan in p.minionBookTitans)
      this.AddLevel3(Inert.GetSpell(minionBookTitan.spell));
  }

  public void AddLevel3(Spell spell)
  {
    if (!((UnityEngine.Object) spell != (UnityEngine.Object) null) || !ClientResources.Instance.icons.ContainsKey(spell.name) || this.spells.FindIndex((Predicate<Image>) (z => (UnityEngine.Object) z != (UnityEngine.Object) null && (UnityEngine.Object) z.sprite != (UnityEngine.Object) null && spell.name.Equals(z.sprite.name))) != -1)
      return;
    if (this.i >= this.spells.Count)
    {
      Image image = UnityEngine.Object.Instantiate<Image>(this.spells[this.i - 1], this.spells[this.i - 1].transform.parent);
      Vector2 vector2_1 = this.spells[this.i - 1].rectTransform.anchoredPosition - this.spells[this.i - 2].rectTransform.anchoredPosition;
      image.rectTransform.anchoredPosition = vector2_1 + this.spells[this.i - 1].rectTransform.anchoredPosition;
      this.spells.Add(image);
      Vector2 vector2_2 = vector2_1 / 2f;
      foreach (Graphic spell1 in this.spells)
        spell1.rectTransform.anchoredPosition -= vector2_2;
    }
    this.spells[this.i].sprite = ClientResources.Instance.GetSpellIcon(spell.name);
    this.spells[this.i].gameObject.SetActive(true);
    ++this.i;
  }

  public void Won() => this.state = UIPlayerCharacter.AnimState.Won;

  public void ShowScrolls(int amount)
  {
    if (amount < 0)
    {
      amount = -amount;
      for (int index = 0; index < amount; ++index)
      {
        RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.pfabScroll, this.pfabScroll.parent);
        rectTransform.anchoredPosition = this.pfabScroll.anchoredPosition + new Vector2((float) (index * 7), 0.0f);
        rectTransform.GetComponent<Image>().sprite = ClientResources.Instance.wandDisabled;
        rectTransform.gameObject.SetActive(true);
      }
    }
    else
    {
      for (int index = 0; index < amount; ++index)
      {
        RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.pfabScroll, this.pfabScroll.parent);
        rectTransform.anchoredPosition = this.pfabScroll.anchoredPosition + new Vector2((float) (index * 7), 0.0f);
        rectTransform.gameObject.SetActive(true);
      }
    }
  }

  private void OnDisable()
  {
    if (this.state == UIPlayerCharacter.AnimState.None)
      return;
    this.state = UIPlayerCharacter.AnimState.None;
    this.StartPosition();
  }

  public void Dead()
  {
    if (!this.initialized)
      this.Awake();
    this.state = UIPlayerCharacter.AnimState.None;
    this.head.rectTransform.anchoredPosition = this.startPos[3] - new Vector2(0.0f, 10f);
    this.hat.rectTransform.anchoredPosition = this.startPos[6] - new Vector2(0.0f, 10f);
    this.mouth.rectTransform.anchoredPosition = this.startPos[5] - new Vector2(0.0f, 10f);
    this.beard.rectTransform.anchoredPosition = this.startPos[4] - new Vector2(0.0f, 10f);
    this.beard2.rectTransform.anchoredPosition = this.startPos[9] - new Vector2(0.0f, 10f);
    this.beard3.rectTransform.anchoredPosition = this.startPos[10] - new Vector2(0.0f, 10f);
    this.leftArm.rectTransform.anchoredPosition = this.startPos[0];
    this.leftFoot.rectTransform.anchoredPosition = this.startPos[1];
    this.body.rectTransform.anchoredPosition = this.startPos[2];
    this.rightFoot.rectTransform.anchoredPosition = this.startPos[7];
    this.rightArm.rectTransform.anchoredPosition = this.startPos[8];
  }

  public void StartPosition()
  {
    if (!this.initialized)
      this.Awake();
    this.t = 0.0f;
    this.head.rectTransform.anchoredPosition = this.startPos[3];
    this.hat.rectTransform.anchoredPosition = this.startPos[6];
    this.mouth.rectTransform.anchoredPosition = this.startPos[5];
    this.beard.rectTransform.anchoredPosition = this.startPos[4];
    this.beard2.rectTransform.anchoredPosition = this.startPos[9];
    this.beard3.rectTransform.anchoredPosition = this.startPos[10];
    this.leftArm.rectTransform.anchoredPosition = this.startPos[0];
    this.leftFoot.rectTransform.anchoredPosition = this.startPos[1];
    this.body.rectTransform.anchoredPosition = this.startPos[2];
    this.rightFoot.rectTransform.anchoredPosition = this.startPos[7];
    this.rightArm.rectTransform.anchoredPosition = this.startPos[8];
  }

  public enum Index
  {
    LeftArm,
    LeftFoot,
    Body,
    Head,
    Beard,
    Mouth,
    Hat,
    RightFoot,
    RightArm,
    Beard2,
    Beard3,
  }

  public enum AnimState
  {
    None = 0,
    Won = 1,
    down1 = 2,
    jump = 4,
    fall = 8,
    up2 = 16, // 0x00000010
    down2 = 32, // 0x00000020
    up3 = 64, // 0x00000040
    down3 = 128, // 0x00000080
    up4 = 256, // 0x00000100
    down4 = 512, // 0x00000200
    up5 = 512, // 0x00000200
    Up = 849, // 0x00000351
    down5 = 1024, // 0x00000400
    Down = 1698, // 0x000006A2
    End = 2048, // 0x00000800
  }
}
