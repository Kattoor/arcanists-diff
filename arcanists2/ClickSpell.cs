

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ClickSpell : MonoBehaviour
{
  public TMP_Text selectedSpellNameText;
  public Image staffIcon;
  public Image bar_bg;
  public Image bar_bg2;
  public ClickSpell.bg_image selectedSpellImage;
  public RectTransform secondSpellLine;
  public SpellButton sbPrefab;
  public List<SpellButton> spellButtons;
  public RectTransform backdrop;
  public List<Image> allImages;
  public List<TMP_Text> allText;
  public static float curAlpha = 1f;
  public static bool isTransparent = false;
  private SpellButton active;
  private int elementalOffset;

  public static ClickSpell Instance { get; private set; }

  public void SetBGColor(Color c)
  {
    this.bar_bg.color = c;
    this.bar_bg2.color = c;
  }

  private void Awake()
  {
    ClickSpell.Instance = this;
    this.allImages = ((IEnumerable<Image>) this.GetComponentsInChildren<Image>(true)).ToList<Image>();
    this.allText = ((IEnumerable<TMP_Text>) this.GetComponentsInChildren<TMP_Text>(true)).ToList<TMP_Text>();
    for (int index = this.allImages.Count - 1; index >= 0; --index)
    {
      if (this.allImages[index].CompareTag("Ignore"))
        this.allImages.RemoveAt(index);
    }
    for (int index = this.allText.Count - 1; index >= 0; --index)
    {
      if (this.allText[index].CompareTag("Ignore"))
        this.allText.RemoveAt(index);
    }
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) ClickSpell.Instance == (UnityEngine.Object) this))
      return;
    ClickSpell.Instance = (ClickSpell) null;
  }

  public void ToggleTransparency(bool v)
  {
    ClickSpell.isTransparent = v;
    this.StopAllCoroutines();
    this.StartCoroutine(v ? this.IEShow() : this.IEHide());
  }

  private IEnumerator IEShow()
  {
    yield return (object) null;
    yield return (object) null;
    while ((double) ClickSpell.curAlpha < 1.0)
    {
      ClickSpell.curAlpha += Time.deltaTime * 4f;
      this.SetAlpha();
      yield return (object) null;
    }
    ClickSpell.curAlpha = 1f;
    this.SetAlpha();
  }

  private IEnumerator IEHide()
  {
    yield return (object) null;
    yield return (object) null;
    float f = PlayerPrefs.GetFloat("prefSpellBarTransparency", 0.35f);
    while ((double) ClickSpell.curAlpha > (double) f)
    {
      ClickSpell.curAlpha -= Time.deltaTime * 4f;
      this.SetAlpha();
      yield return (object) null;
    }
    ClickSpell.curAlpha = f;
    this.SetAlpha();
  }

  public void ChangeAlpha(float f)
  {
    if ((double) ClickSpell.curAlpha == 1.0)
      return;
    ClickSpell.curAlpha = f;
    this.SetAlpha();
  }

  public void SetAlpha()
  {
    for (int index = 0; index < this.allImages.Count; ++index)
      this.allImages[index].color = ClickSpell.GetColor(this.allImages[index].color);
    for (int index = 0; index < this.allText.Count; ++index)
      this.allText[index].color = ClickSpell.GetColor(this.allText[index].color);
  }

  public static Color GetColor(Color c)
  {
    c.a = ClickSpell.curAlpha;
    return c;
  }

  public void OnPointerEnter(int i)
  {
    this.active = this.spellButtons[i];
    this.spellButtons[i].bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    this.spellButtons[i].bg.transform.SetAsLastSibling();
    this.spellButtons[i].txtName.gameObject.SetActive(true);
    if (this.spellButtons[i].error == 0)
    {
      this.spellButtons[i].txtError.gameObject.SetActive(false);
    }
    else
    {
      this.spellButtons[i].txtError.text = ClickSpell.ErrorString(this.spellButtons[i].error);
      this.spellButtons[i].txtError.gameObject.SetActive(true);
    }
    if (!HUD.instance.showDescriptions)
      return;
    string text = this.spellButtons[i].txtName.text;
    if (string.IsNullOrEmpty(text))
      return;
    (HUD.instance.textSpellDescription.text, HUD.instance.textSpellExtraText.text) = Descriptions.GetSpellDescription(text, Player.Instance.selected.spells[i]);
    HUD.instance.ShowPanelDescription();
  }

  public void OnPointerExit(int i) => this.OnPointerExit(this.spellButtons[i]);

  public void OnPointerExit(SpellButton a)
  {
    a.bg.transform.localScale = new Vector3(1f, 1f, 1f);
    a.txtName.gameObject.SetActive(false);
    a.txtError.gameObject.SetActive(false);
    HUD.instance.LeaveBook();
    this.active = (SpellButton) null;
  }

  public void MakeSureHidden()
  {
    if (!((UnityEngine.Object) this.active != (UnityEngine.Object) null))
      return;
    this.OnPointerExit(this.active);
  }

  public static void Unselect() => ClickSpell.Instance.OnClickIndex(-1);

  public void OnClickIndex(int i)
  {
    if ((UnityEngine.Object) Player.Instance == (UnityEngine.Object) null || !Player.Instance.person.yourTurn)
    {
      this.selectedSpellNameText.text = "";
      this.selectedSpellImage.Hide();
      this.staffIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }
    else if (HUD.Hidden)
    {
      this.selectedSpellNameText.text = "";
      this.selectedSpellImage.Hide();
      this.staffIcon.color = new Color(1f, 1f, 1f, 0.5f);
      Player.Instance.SetFakeSpell(i);
    }
    else
    {
      Player.Instance.selectingSpell = true;
      if (Player.Instance.selectedSpellIndex == i || (ZComponent) Player.Instance.selected == (object) null || Player.Instance.selected.spells.Count <= i)
        return;
      if (i == -1)
      {
        this.selectedSpellNameText.text = "";
        this.selectedSpellImage.Hide();
        this.staffIcon.color = new Color(1f, 1f, 1f, 0.16f);
      }
      else
      {
        if (!Global.CompareColors((Color32) this.spellButtons[i].image.color, (Color32) Color.white))
          return;
        this.UpdateUI(i);
        Player.Instance.SetSpell(i);
      }
    }
  }

  public void UpdateUI(int i)
  {
    if ((UnityEngine.Object) Player.Instance == (UnityEngine.Object) null)
      return;
    byte spellIndex = 0;
    string spellName = ClickSpell.GetSpellName(Player.Instance.selected.inWater ? Player.Instance.person.first()?.GetAvailableGate(ref spellIndex)?.spell ?? Inert.Instance.waterGate.spell : Player.Instance.selected.spells[i].spell, Player.Instance.selected);
    this.selectedSpellNameText.text = spellName;
    this.selectedSpellImage.SetVisual(spellName);
    AudioManager.Play(AudioManager.instance.clipSelectSpell[(int) Player.Instance.selected.spells[i].spell.bookOf]);
    this.staffIcon.color = Player.Instance.selected.spells[i].spell.bookOf.ToColor();
  }

  public static string GetSpellName(Spell s, ZCreature c)
  {
    if ((ZComponent) c == (object) null)
      return s.name;
    if (s.spellEnum == SpellEnum.Vampire_Bat)
    {
      if (c.flying)
        return "Cancel Flight";
    }
    else
    {
      if (s.spellEnum.IsFlight() && c.flying)
        return "Cancel Flight";
      if (s.spellEnum == SpellEnum.Thunder_Shock && c.isPawn)
        return "Cyclops Shock Attack";
      if (s.spellEnum == SpellEnum.Blood_Lust && c.type == CreatureType.Gargoyle)
        return "Gargoyle Swipe";
      if (s.spellEnum == SpellEnum.Hatch && c.health < 25)
        return "Dragon Hatchling";
      if (s.spellEnum == SpellEnum.Seasonal)
      {
        switch (c.game.currentSeason)
        {
          case GameSeason.Summer:
            return "Summer Weather";
          case GameSeason.Autumn:
            return "Autumn Weather";
          case GameSeason.Winter:
            return "Winter Weather";
          case GameSeason.Spring:
            return "Spring Weather";
          default:
            return "Nothing";
        }
      }
      else if (s.spellEnum == SpellEnum.Dive && c.mountable)
        return "Water Lord Dive";
    }
    return s.name;
  }

  public void SetSpells()
  {
    this.elementalOffset = 0;
    int num = 0;
    if ((UnityEngine.Object) Player.Instance == (UnityEngine.Object) null || (ZComponent) Player.Instance.selected == (object) null || Player.Instance.person.controlled.Count == 0 || Player.Instance.selected.isDead)
      return;
    this.secondSpellLine.anchoredPosition = Player.Instance.selected.inWater || Player.Instance.selected.spells.Count <= 20 ? new Vector2(0.0f, 91f) : (Player.Instance.selected.spells.Count <= 32 ? new Vector2(0.0f, 137f) : new Vector2(0.0f, 198f));
    if (Player.Instance.selected.inWater)
    {
      this.ExtraEffectsInWater();
      num = 1;
    }
    else if (Client.game.ArcaneZero)
    {
      for (; num < Player.Instance.selected.spells.Count; ++num)
      {
        Spell spell = Player.Instance.selected.spells[num].spell;
        this.ExtraEffects(num);
        if (spell.damage > 0 || spell.spellEnum == SpellEnum.Snowball)
          this.spellButtons[num].ArcaneZero();
      }
    }
    else
    {
      for (; num < Player.Instance.selected.spells.Count; ++num)
        this.ExtraEffects(num);
    }
    for (; num < this.spellButtons.Count; ++num)
      this.spellButtons[num].Hide();
  }

  public void ExtraEffectsInWater()
  {
    Spell spell = Inert.Instance.waterGate.spell;
    this.spellButtons[0].SetVisual(spell.name, spell.name, -1, 0, false);
  }

  public void ExtraEffects(int i)
  {
    if ((UnityEngine.Object) Player.Instance == (UnityEngine.Object) null)
      return;
    SpellSlot spell1 = Player.Instance.selected.spells[i];
    ZCreature selected = Player.Instance.selected;
    Spell spell2 = spell1.spell;
    if ((UnityEngine.Object) spell2 == (UnityEngine.Object) null)
      return;
    if (i >= this.spellButtons.Count)
    {
      SpellButton spellButton = UnityEngine.Object.Instantiate<SpellButton>(this.sbPrefab, this.sbPrefab.transform.parent);
      spellButton.index = i;
      this.spellButtons.Add(spellButton);
      this.allImages.Add(spellButton.bgColor);
      this.allImages.Add(spellButton.image);
      this.allText.Add(spellButton.txtText);
      RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.backdrop, this.backdrop.parent);
      spellButton.backdrop = rectTransform;
    }
    float num1 = this.elementalOffset <= 12 || i >= 32 ? (float) i : (float) (i - (this.elementalOffset - 12));
    float y = (double) num1 % 2.0 == 0.0 ? -45.5f : -29f;
    float num2;
    if (spell1.isLevel5 && selected.spells.Count > 23)
    {
      if (this.elementalOffset < 12)
        this.elementalOffset = 12;
      num2 = (float) (this.elementalOffset * 62 + 14);
      y = (this.elementalOffset % 2 == 0 ? -45.5f : -29f) + 62f;
      ++this.elementalOffset;
    }
    else if (selected.spells.Count > 23)
    {
      if (i < 12)
      {
        num2 = (float) ((double) num1 * 62.0 + 14.0);
        y += 62f;
      }
      else if (i >= 32)
      {
        float num3 = (float) ((int) num1 / 16);
        num2 = (float) ((double) num1 % 16.0 * 62.0 + 14.0);
        y += 62f * num3;
      }
      else
        num2 = (float) (((double) num1 - 12.0) * 62.0);
    }
    else
      num2 = (float) (i * 62);
    float x = num2 + 154f;
    this.spellButtons[i].rectTransform.anchoredPosition = new Vector2(x, y);
    if ((UnityEngine.Object) this.spellButtons[i].backdrop != (UnityEngine.Object) null)
      this.spellButtons[i].backdrop.anchoredPosition = new Vector2(x, y);
    this.spellButtons[i].error = 0;
    int uses = -1;
    int num4 = 0;
    if (spell1.MaxUses > 0)
      uses = spell1.MaxUses - spell1.UsedUses;
    int localTurn = Player.Instance.selected.parent.localTurn;
    if (spell1.TurnsTillFirstUse > localTurn)
      num4 = spell1.TurnsTillFirstUse - localTurn;
    if (spell1.LastTurnFired == localTurn || (spell1.RechargeTime > 0 || spell1.LastTurnFired > localTurn) && spell1.LastTurnFired + spell1.RechargeTime >= localTurn && (spell1.TurnsTillFirstUse > localTurn || spell1.LastTurnFired > -1))
      num4 = Mathf.Max(num4, spell1.LastTurnFired >= 0 ? spell1.RechargeTime - (localTurn - (spell1.LastTurnFired + 1)) : -1);
    bool maxedSummons = false;
    if (spell2.type == CastType.Placement && spell2.amount > 0 && Player.Instance.selected.parent.GetMinionCount() + spell2.amount > Player.Instance.selected.parent.GetMaxMinions())
    {
      this.spellButtons[i].error = 101;
      maxedSummons = true;
    }
    else if (spell2.type == CastType.Tower && (ZComponent) Player.Instance.selected.tower != (object) null)
    {
      this.spellButtons[i].error = 109;
      maxedSummons = true;
    }
    if (!selected.inWater && spell1.disabledturn >= Player.Instance.person.localTurn)
    {
      maxedSummons = true;
      if (spell1.disabledturn == 1000000)
      {
        this.spellButtons[i].error = 115;
      }
      else
      {
        this.spellButtons[i].error = 110;
        num4 = Mathf.Max(num4, spell1.LastTurnFired >= 0 ? spell1.disabledturn - Player.Instance.person.localTurn + 1 : -1);
      }
    }
    if (spell2.spellEnum.IsFlight())
    {
      if (!Player.Instance.selected.shiningPower && Player.Instance.selected.flying && !Player.Instance.selected.phantom)
      {
        this.spellButtons[i].SetVisual("Flight", "Cancel Flight", -1, 0, maxedSummons);
        return;
      }
    }
    else if (spell2.type == CastType.Tower && (Player.Instance.selected.flying || (ZComponent) Player.Instance.selected.mount != (object) null || Player.Instance.selected.FullArcane || Player.Instance.selected.radius > 30))
    {
      if (Player.Instance.selected.flying)
        this.spellButtons[i].error = 3;
      else if ((ZComponent) Player.Instance.selected.mount != (object) null)
        this.spellButtons[i].error = 106;
      else if (Player.Instance.selected.radius > 30)
        this.spellButtons[i].error = 2;
      maxedSummons = true;
    }
    if (!maxedSummons)
    {
      int num5 = ClickSpell.Level3(spell2, spell2.spellEnum, Player.Instance.selected, spell1);
      if (num5 != 0)
        this.spellButtons[i].error = num5;
      maxedSummons = num5 != 0;
    }
    if (Player.Instance.selected.phantom)
    {
      if (spell2.bookOf != BookOf.Arcane && spell2.bookOf != BookOf.Illusion && !spell1.isPresent && spell2.spellEnum != SpellEnum.Spirit_Link)
      {
        this.spellButtons[i].error = 104;
        maxedSummons = true;
        uses = 0;
        num4 = 0;
      }
    }
    else if (Player.Instance.selected.race == CreatureRace.Undead && !Player.Instance.selected.pawn)
    {
      if (spell2.bookOf != BookOf.Arcane && spell2.bookOf != BookOf.Underdark && !spell1.isPresent || spell2.type == CastType.Tower)
      {
        this.spellButtons[i].error = spell2.type == CastType.Tower ? 121 : 103;
        maxedSummons = true;
        uses = 0;
        num4 = 0;
      }
    }
    else if (Player.Instance.selected.shiningPower)
    {
      if (spell2.bookOf != BookOf.Arcane && spell2.bookOf != BookOf.OverLight && !spell1.isPresent || spell2.type == CastType.Tower)
      {
        this.spellButtons[i].error = spell2.type == CastType.Tower ? 124 : 102;
        maxedSummons = true;
        uses = 0;
        num4 = 0;
      }
    }
    else if (Player.Instance.selected.race == CreatureRace.Bear && !Player.Instance.selected.pawn && (spell2.bookOf != BookOf.Arcane && spell2.bookOf != BookOf.Druidism && !spell1.isPresent || spell2.type == CastType.Tower))
    {
      this.spellButtons[i].error = spell2.type == CastType.Tower ? 124 : 122;
      maxedSummons = true;
      uses = 0;
      num4 = 0;
    }
    this.spellButtons[i].SetVisual(Player.Instance.selected.spells[i].spell.name, ClickSpell.GetSpellName(Player.Instance.selected.spells[i].spell, Player.Instance.selected), uses, num4, maxedSummons);
  }

  public static string ErrorString(int code)
  {
    switch (code)
    {
      case 1:
        return "Cannot use while in a tower";
      case 2:
        return "You're too fat to use this";
      case 3:
        return "Cannot use while flying";
      case 4:
        return "Apparition/Phantom cannot use flight";
      case 5:
        return "Cannot use flight while gliding";
      case 6:
        return "Cannot glide while flying";
      case 7:
        return "Cannot use while Undead";
      case 8:
        return "Cannot use while an Angel";
      case 9:
        return "Only the minion can use this spell";
      case 10:
        return "Must be on the water to use this spell";
      case 11:
        return "Already Social Distancing";
      case 12:
        return "Cannot use while entangled";
      case 13:
        return "Cannot use while stunned";
      case 14:
        return "Arcane Energizer is already active";
      case 15:
        return "Cannot be used in tandem with Blood Pact";
      case 16:
        return "Cannot be used in tandem with Dark Defenses";
      case 100:
        return "No uses left";
      case 101:
        return "Minion limit reached";
      case 102:
        return "Angel - Only Overlight and Arcane spells are allowed";
      case 103:
        return "Lich - Only Underdark and Arcane spells are allowed";
      case 104:
        return "Phantom - Only Illusion and Arcane spells are allowed";
      case 105:
        return "Arcane Monster - Cannot use";
      case 106:
        return "Cannot tower while mounted";
      case 107:
        return "Not ready yet";
      case 108:
        return "No more uses";
      case 109:
        return "Already in a tower";
      case 110:
        return "Temporarily Disabled";
      case 111:
        return "Only one Blood Bank per player allowed";
      case 112:
        return "Cannot cast spells while in stone form";
      case 113:
        return "No suitable minion to resurrect";
      case 114:
        return "This is not an original spell (OG spells only)";
      case 115:
        return "Your Arcanist requires this spell in their spellbook to cast this spell";
      case 116:
        return "Cannot use after moving";
      case 117:
        return "A season must be active to cast this spell";
      case 118:
        return "Only one Dark Totem per player allowed";
      case 119:
        return "Only one Dryad per player allowed";
      case 120:
        return "What calendar are you running?";
      case 121:
        return "Cannot tower while in Lich form";
      case 122:
        return "Bear Form - Only Druidism and Arcane spells are allowed";
      case 123:
        return "Cannot use while in Bear Form";
      case 124:
        return "Cannot tower while in Bear Form";
      case 125:
        return "Cannot leap while being ridden";
      case 126:
        return "Cannot leap while mounted";
      case (int) sbyte.MaxValue:
        return "Too many bees, don't want to over pollinate!";
      case 128:
        return "Requires 26 or more health.";
      case 129:
        return "Requires a level 5 Cosmos familiar.";
      case 130:
        return "Maximum active rituals reached.";
      case 131:
        return "Maximum shield reached.";
      case 132:
        return "Retribution is already active.";
      case 133:
        return "Limit reached for this minion type.";
      case 134:
        return "Only your Arcanist can cast this spell";
      case 135:
        return "Cannot cast while trdapped inside a jar";
      case 136:
        return "Already has a similar spell active";
      default:
        return "";
    }
  }

  public static int Level3(Spell theSpell, SpellEnum s, ZCreature c, SpellSlot slot)
  {
    if (c.inWater)
      return 0;
    if (c.collider.gameObjectLayer == 21 && (s == SpellEnum.Arcane_Gate || s == SpellEnum.Santas_Magic || s == SpellEnum.Blink || s == SpellEnum.The_ol_swaparoo))
      return 135;
    if (c.type == CreatureType.Gargoyle && (!c.canMove && s != SpellEnum.Stone_Form || s == SpellEnum.Stone_Form && c.race == CreatureRace.Undead && c.canMove))
      return c.race == CreatureRace.Undead ? 7 : 112;
    if (s.IsFlight())
    {
      if ((ZComponent) c.tower != (object) null)
        return 1;
      if (c.radius >= 30 && !c.pawn)
        return 2;
      if (c.phantom)
        return 4;
      return c.gliding ? 5 : 0;
    }
    if (s == SpellEnum.Glide)
    {
      if ((ZComponent) c.tower != (object) null)
        return 1;
      if (c.phantom)
        return 4;
      return c.flying ? 6 : 0;
    }
    if (s == SpellEnum.Shining_Power || s == SpellEnum.Lichdom || s == SpellEnum.Apparition || s == SpellEnum.Bear_Form)
    {
      if ((ZComponent) c.tower != (object) null)
        return 1;
      if (c.race == CreatureRace.Undead)
        return 7;
      if (c.shiningPower)
        return 8;
      if (c.flying)
        return 3;
      if (c.phantom)
        return 4;
      return c.race == CreatureRace.Bear ? 123 : 0;
    }
    if (s == SpellEnum.Flurry || s == SpellEnum.Vine_Bloom)
    {
      if (c.pawn)
      {
        ZCreature zcreature = c.game.CurrentCreature();
        if ((zcreature != null ? (!zcreature.HasSpell(s) ? 1 : 0) : 0) == 0)
          goto label_46;
      }
      return !c.pawn ? 9 : 115;
    }
label_46:
    switch (s)
    {
      case SpellEnum.Dark_Defences:
        return !c.pawn ? 0 : 134;
      case SpellEnum.Vine_Bridge:
        if (c.flying)
          return 3;
        break;
      case SpellEnum.Dive:
        return (int) c.position.y > c.radius + 1 ? 10 : 0;
      case SpellEnum.Social_Distancing:
        return c.socialDistancing ? 11 : 0;
      default:
        if (s == SpellEnum.Arcane_Gate || s == SpellEnum.Santas_Magic || s == SpellEnum.The_ol_swaparoo || s == SpellEnum.Blink)
          return c.entangled ? 12 : 0;
        if (s == SpellEnum.Charge || s == SpellEnum.Mine || s == SpellEnum.Rampage)
          return c.stunned ? 13 : 0;
        if (s == SpellEnum.Arcane_Energiser)
          return c.HasArcaneEnergizer ? 14 : 0;
        if (s == SpellEnum.Frost_Leap)
          return !((ZComponent) c.tower == (object) null) ? 1 : 0;
        if (s == SpellEnum.Dark_Defences)
          return !ZComponent.IsNull((ZComponent) c.pact) && !c.pact.isDead ? 15 : 0;
        if (s == SpellEnum.Blood_Pact)
          return c.hasDarkDefenses ? 16 : 0;
        if (s == SpellEnum.Summon_Bees)
          return !(c.GetType() == typeof (ZCreatureBeehive)) ? (c.parent.GetMaxMinions() <= c.parent.controlled.Count ? 101 : 0) : (((ZCreatureBeehive) c).bees.Count >= 3 ? (int) sbyte.MaxValue : 0);
        if (s == SpellEnum.Blood_Pact)
          return c.health > 25 ? 0 : 128;
        if (s == SpellEnum.Resurrection)
          return c.game.lastMinionToDie.Count <= 0 ? 113 : 0;
        if (s == SpellEnum.Summon_Titan)
          return c.parent.minionBookTitans.Count <= 0 ? 113 : 0;
        if (s == SpellEnum.Drone_Strike)
          return !slot.isPresent && c.familiarLevelCosmos < 5 ? 129 : 0;
        if (s == SpellEnum.Summon_Dragon_Egg)
          return !c.game.originalSpellsOnly ? 0 : 114;
        if (s == SpellEnum.Seasonal)
          return c.game.currentSeason == GameSeason.None ? 117 : 0;
        if (s == SpellEnum.Faiere_Jump && ((ZComponent) c.rider != (object) null || (ZComponent) c.mount != (object) null || c.flying))
        {
          if (c.flying)
            return 3;
          return !((ZComponent) c.rider != (object) null) ? 126 : 125;
        }
        if (s == SpellEnum.Protection_Shield && c.shield >= 150 && !c.game.originalSpellsOnly && c.familiarLevelOverlight == 0)
          return 131;
        if (s == SpellEnum.Retribution && c.retribution > 0)
          return 132;
        if (theSpell.MaxMinionCount > 0)
        {
          int num = 0;
          using (List<ZCreature>.Enumerator enumerator = c.parent.controlled.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ZCreature current = enumerator.Current;
              if ((ZComponent) current != (object) null && current.spellEnum == s)
              {
                ++num;
                if (num >= theSpell.MaxMinionCount)
                  return 133;
              }
            }
            break;
          }
        }
        else
        {
          if (s == SpellEnum.Storm_Shield && (ZComponent) c.stormShield != (object) null && c.stormShield.MaxTurnsAlive > 1000)
            return 136;
          break;
        }
    }
    return 0;
  }

  [Serializable]
  public class bg_image
  {
    public GameObject bg;
    public GameObject zero;
    public Image bgColor;
    public Image image;
    public TMP_Text txtName;
    public TMP_Text txtText;
    public string nameOfSpell;
    private static Color DarkGray = new Color(0.3f, 0.3f, 0.3f);

    public void SetVisual(string s)
    {
      this.nameOfSpell = s;
      this.image.sprite = ClientResources.Instance.GetSpellIcon(s);
      this.bg.SetActive(true);
    }

    public void ArcaneZero()
    {
      this.zero.SetActive(true);
      this.image.color = ClickSpell.bg_image.DarkGray;
      this.bgColor.color = ClickSpell.bg_image.DarkGray;
    }

    public void SetVisual(string s, int uses, int rechargeTime, bool maxedSummons)
    {
      this.nameOfSpell = s;
      this.txtName.text = s;
      this.image.sprite = ClientResources.Instance.GetSpellIcon(s);
      if (this.zero.activeInHierarchy)
        this.zero.SetActive(false);
      if (maxedSummons || uses == 0)
      {
        this.txtText.gameObject.SetActive(true);
        this.image.color = ClickSpell.bg_image.DarkGray;
        this.bgColor.color = ClickSpell.bg_image.DarkGray;
        this.txtText.text = rechargeTime <= 0 || uses == 0 ? "X" : rechargeTime.ToString() + "X";
        this.txtText.color = Color.red;
        this.txtText.alignment = TextAlignmentOptions.Midline;
      }
      else if (rechargeTime > 0)
      {
        this.txtText.gameObject.SetActive(true);
        this.image.color = ClickSpell.bg_image.DarkGray;
        this.bgColor.color = ClickSpell.bg_image.DarkGray;
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
    }

    public void Hide()
    {
      this.nameOfSpell = "";
      this.bg.SetActive(false);
    }
  }
}
