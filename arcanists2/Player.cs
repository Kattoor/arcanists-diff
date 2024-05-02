// Decompiled with JetBrains decompiler
// Type: Player
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using Hazel.Tcp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class Player : MonoBehaviour
{
  private ZCreature _selected;
  public int selectedCreatureIndex;
  public int selectedCreaturePlayerOffset;
  public int selectedSpellIndex = -1;
  public Spell selectedSpell;
  private ZPerson _person;
  internal bool canSkipTurn = true;
  public bool skippingTurn;
  public bool jumpIsDetower;
  internal MyLocation spellTarget;
  private GameObject meter;
  internal GameObject[] meter_subs;
  private Image summon_image;
  private Image meterFill;
  private bool targetWasSet;
  private float movementUpdate;
  private float targetUpdate = 0.0166666675f;
  internal MyLocation target = NullMyLocation.Get();
  internal MyLocation secTarget = NullMyLocation.Get();
  public bool LockMeter;
  public bool extendedShot;
  private KeyCode[] keyCodes = new KeyCode[0];
  public List<Player.AssignableHotkey> hotkeys = new List<Player.AssignableHotkey>();
  public bool summoniningDummy;
  public Spell summoniningDummySpell;
  public bool summonOnYourTeam = true;
  public ZPerson summonedPerson;
  private bool hidden;
  [NonSerialized]
  public Vector3 mouseWorldPos;
  [NonSerialized]
  public bool _lastExtendedShot;
  [NonSerialized]
  public Vector3 _LastMouseWorldPos;
  private Vector3 diff;
  [NonSerialized]
  public FixedInt rot_z;
  [NonSerialized]
  public FixedInt power = (FixedInt) -1;
  [NonSerialized]
  public float lastSendTick;
  private KeyCode keyDown;
  private float _softLock;
  private byte[] gameBytes;
  private Pathfinder path = new Pathfinder();
  public bool selectingSpell;
  private bool movedThisTurn;

  public IAnimator animator => this.selected.animator;

  public int myTurn => this.person == null ? 0 : (int) this.person.id;

  public ZCreature selected
  {
    get
    {
      return ZComponent.IsNull((ZComponent) this._selected) || this._selected.isDead ? (ZCreature) null : this._selected;
    }
    set => this._selected = value;
  }

  public ZPerson person
  {
    get
    {
      if (Client.game != null && Client.game.isSandbox && !Client.game.isTutorial)
      {
        ZPerson person = Client.game.CurrentPlayer() ?? this._person;
        if (person.ai != null)
          person = this._person;
        if (Client.curGameID != (int) person.id)
        {
          Client.curGameID = (int) person.id;
          Player.Instance.SelectFirst();
          ClickSpell.Instance?.SetBGColor(person.clientColor);
          SelectionArrow.Instance?.UpdateColor(person.clientColor);
          int activateableFamiliar = (int) person.ActivateableFamiliar;
          if (activateableFamiliar >= 0)
          {
            int familiarLevel = person.familiarLevels[activateableFamiliar];
            HUD.instance.fullBookImg.sprite = activateableFamiliar != 10 || !person.seasonISHoliday ? HUD.instance.fullBookSprites[activateableFamiliar] : ClientResources.Instance.spellBookIconHoliday;
            HUD.instance.fullBookButton.Interactable(familiarLevel < 5);
            HUD.instance.fullBookTextLevel.text = familiarLevel == 0 ? "" : "Level " + (object) familiarLevel;
            HUD.instance.fullBookObj.SetActive(true);
          }
          else
            HUD.instance.fullBookObj.SetActive(false);
        }
        return person;
      }
      if (Client.game != null && Client.game.isMulti)
      {
        ZPerson zperson = Client.game.CurrentPlayer() ?? this._person;
        if (zperson.team == this._person.team && Client.curGameID != (int) zperson.id)
        {
          Client.curGameID = (int) zperson.id;
          Player.Instance.SelectFirst();
          ClickSpell.Instance?.SetBGColor(zperson.clientColor);
          SelectionArrow.Instance?.UpdateColor(zperson.clientColor);
          int activateableFamiliar = (int) zperson.ActivateableFamiliar;
          if (activateableFamiliar >= 0)
          {
            int familiarLevel = zperson.familiarLevels[activateableFamiliar];
            HUD.instance.fullBookImg.sprite = activateableFamiliar != 10 || !zperson.seasonISHoliday ? HUD.instance.fullBookSprites[activateableFamiliar] : ClientResources.Instance.spellBookIconHoliday;
            HUD.instance.fullBookButton.Interactable(familiarLevel < 5);
            HUD.instance.fullBookTextLevel.text = familiarLevel == 0 ? "" : "Level " + (object) familiarLevel;
            HUD.instance.fullBookObj.SetActive(true);
          }
          else
            HUD.instance.fullBookObj.SetActive(false);
          this._person = zperson;
        }
      }
      return this._person;
    }
    set => this._person = value;
  }

  public static Player Instance { get; set; }

  public Transform GetTransform
  {
    get
    {
      return this.person == null || !((ZComponent) this.person.first() != (object) null) || !((UnityEngine.Object) this.person.first().transform != (UnityEngine.Object) null) ? this.transform : this.person.first().transform;
    }
  }

  public void UpdateVisuals() => ClickSpell.Instance.SetSpells();

  public KeyCode GetDown(KeyCode notThis)
  {
    foreach (KeyCode keyCode in this.keyCodes)
    {
      switch (keyCode)
      {
        case KeyCode.Mouse0:
        case KeyCode.Mouse1:
          continue;
        default:
          if (Input.GetKeyDown(keyCode) && keyCode != notThis)
            return keyCode;
          continue;
      }
    }
    return KeyCode.None;
  }

  public KeyCode GetNumberDown()
  {
    KeyCode[] keyCodeArray = new KeyCode[22]
    {
      KeyCode.Alpha0,
      KeyCode.Alpha1,
      KeyCode.Alpha2,
      KeyCode.Alpha3,
      KeyCode.Alpha4,
      KeyCode.Alpha5,
      KeyCode.Alpha6,
      KeyCode.Alpha7,
      KeyCode.Alpha8,
      KeyCode.Alpha9,
      KeyCode.F1,
      KeyCode.F2,
      KeyCode.F3,
      KeyCode.F4,
      KeyCode.F5,
      KeyCode.F6,
      KeyCode.F7,
      KeyCode.F8,
      KeyCode.F9,
      KeyCode.F10,
      KeyCode.F11,
      KeyCode.F12
    };
    foreach (KeyCode key in keyCodeArray)
    {
      if (Input.GetKeyDown(key))
        return key;
    }
    return KeyCode.None;
  }

  public bool InTower(TowerType t)
  {
    return (ZComponent) this.selected != (object) null && (ZComponent) this.selected.tower != (object) null && this.selected.tower.type == t;
  }

  public bool PressingHighJump()
  {
    return hardInput.GetKey("High Jump") || hardInput.GetKey("High Jump 2") || HUD.instance.PressingHighJump;
  }

  public bool PressingLongJump()
  {
    return hardInput.GetKey("Long Jump") || hardInput.GetKey("Long Jump 2") || HUD.instance.PressingLongJump;
  }

  public bool PressingDetower() => hardInput.GetKey("detower") || HUD.instance.PressingDetower;

  public void StartSummoningDummy(Spell s)
  {
    this.UnselectSpell();
    this.summoniningDummy = true;
    this.summoniningDummySpell = s;
    CursorList.Instance.SetCursor(1);
    this.meter_subs[4].transform.position = this.mouseWorldPos;
    this.meter_subs[4].GetComponent<Image>().color = Color.white;
    this.meter_subs[4].SetActive(true);
    Creature component = s.toSummon.GetComponent<Creature>();
    ((RectTransform) this.meter_subs[4].transform).sizeDelta = new Vector2((float) (component.radius * 2), (float) (component.radius * 2));
  }

  public void StopSummoningDummy()
  {
    this.summoniningDummy = false;
    this.summoniningDummySpell = (Spell) null;
    this.UnselectSpell();
  }

  public void NextRecallDevice(bool fromDeath)
  {
    ZCreature selected = this.selected;
    this.NextControlled(audio: false);
    int num = 0;
    while (num < 50 && !ZComponent.IsNull((ZComponent) this.selected) && this.selected.type != CreatureType.Recall_Device && this.selected.isPawn)
    {
      ++num;
      this.NextControlled(audio: false);
    }
    if (!((ZComponent) this.selected != (object) null) || this.selected.type != CreatureType.Recall_Device)
      return;
    AudioManager.Play(this.selected.clientObj.clipSelect);
  }

  public void PreviousRecallDevice()
  {
    ZCreature selected = this.selected;
    this.PreviousControlled(false);
    int num = 0;
    while (num < 50 && !ZComponent.IsNull((ZComponent) this.selected) && this.selected.type != CreatureType.Recall_Device && this.selected.isPawn)
    {
      ++num;
      this.PreviousControlled(false);
    }
    if (!((ZComponent) this.selected != (object) null) || this.selected.type != CreatureType.Recall_Device)
      return;
    AudioManager.Play(this.selected.clientObj.clipSelect);
  }

  public void SelectFirst()
  {
    this.selectedCreatureIndex = 0;
    this.selectedCreaturePlayerOffset = Client.curGameID;
    if (Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count == 0)
    {
      this.selected = (ZCreature) null;
    }
    else
    {
      this.selected = Client.game.players[this.selectedCreaturePlayerOffset].controlled[this.selectedCreatureIndex];
      CameraMovement.Instance.LerpToTransform(this.selected);
      this.UpdateVisuals();
      this.UnselectSpell();
    }
  }

  public void NextControlled(bool fromDeath = false, bool audio = true)
  {
    do
    {
      this.UnselectSpell();
      if (!fromDeath)
        ++this.selectedCreatureIndex;
      fromDeath = false;
      if (this.selectedCreatureIndex >= Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count)
      {
        if (Client.game.isTeam)
        {
          int num = 0;
          do
          {
            ++num;
            ++this.selectedCreaturePlayerOffset;
            if (this.selectedCreaturePlayerOffset >= Client.game.players.Count)
              this.selectedCreaturePlayerOffset = 0;
            if (Client.game.players[this.selectedCreaturePlayerOffset].team == this.person.team)
            {
              if (this.selectedCreaturePlayerOffset == (int) this.person.id)
              {
                this.selectedCreatureIndex = 0;
                break;
              }
              if (Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count > 1)
              {
                this.selectedCreatureIndex = 1;
                break;
              }
            }
          }
          while (num < 50);
          if (num == 50)
            Debug.LogError((object) "Infinite Loop broken in NextControlled");
        }
        else
          this.selectedCreatureIndex = 0;
      }
      if (this.selectedCreatureIndex == 0 && this.selectedCreaturePlayerOffset != (int) this.person.id)
      {
        this.selectedCreaturePlayerOffset = (int) this.person.id;
        if ((ZComponent) Client.game.players[this.selectedCreaturePlayerOffset].first() == (object) null)
        {
          this.selected = (ZCreature) null;
          return;
        }
      }
      if (this.selectedCreaturePlayerOffset >= Client.game.players.Count || this.selectedCreatureIndex >= Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count)
        return;
    }
    while (this.selectedCreatureIndex > 0 && !Client.game.players[this.selectedCreaturePlayerOffset].IsSelectable(this.selectedCreatureIndex));
    this.selected = Client.game.players[this.selectedCreaturePlayerOffset].controlled[this.selectedCreatureIndex];
    CameraMovement.Instance.LerpToTransform(this.selected);
    this.UpdateVisuals();
    this.UnselectSpell();
    if (!audio)
      return;
    AudioManager.Play(this.selected.clientObj.clipSelect);
  }

  public void PreviousControlled(bool audio = true)
  {
    this.UnselectSpell();
label_1:
    if ((ZComponent) this.selected == (object) null)
    {
      this.NextControlled(audio: audio);
    }
    else
    {
      ZCreature zcreature1 = (ZCreature) null;
      int num1 = 0;
      int num2 = 0;
      int selectedCreatureIndex = this.selectedCreatureIndex;
      int creaturePlayerOffset = this.selectedCreaturePlayerOffset;
      for (int index = 0; index < 1000; ++index)
      {
        ++this.selectedCreatureIndex;
        if (this.selectedCreatureIndex >= Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count)
        {
          if (Client.game.isTeam)
          {
            int num3 = 0;
            do
            {
              ++num3;
              ++this.selectedCreaturePlayerOffset;
              if (this.selectedCreaturePlayerOffset >= Client.game.players.Count)
                this.selectedCreaturePlayerOffset = 0;
              if (Client.game.players[this.selectedCreaturePlayerOffset].team == this.person.team)
              {
                if (this.selectedCreaturePlayerOffset == (int) this.person.id)
                {
                  this.selectedCreatureIndex = 0;
                  break;
                }
                if (Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count > 1)
                {
                  this.selectedCreatureIndex = 1;
                  break;
                }
              }
            }
            while (num3 < 50);
            if (num3 == 50)
              Debug.LogError((object) "Infinite Loop broken in Previous Controlled");
          }
          else
            this.selectedCreatureIndex = 0;
        }
        if (this.selectedCreatureIndex == 0 && this.selectedCreaturePlayerOffset != (int) this.person.id)
        {
          this.selectedCreaturePlayerOffset = (int) this.person.id;
          if ((ZComponent) Client.game.players[this.selectedCreaturePlayerOffset].first() == (object) null)
          {
            this.selected = (ZCreature) null;
            break;
          }
        }
        ZCreature zcreature2 = Client.game.players[this.selectedCreaturePlayerOffset].controlled[this.selectedCreatureIndex];
        if ((ZComponent) zcreature2 == (object) this.selected)
        {
          if ((ZComponent) zcreature1 == (object) null)
          {
            this.selectedCreatureIndex = selectedCreatureIndex;
            this.selectedCreaturePlayerOffset = creaturePlayerOffset;
            break;
          }
          this.selected = zcreature1;
          this.selectedCreatureIndex = num1;
          this.selectedCreaturePlayerOffset = num2;
          if (this.selectedCreatureIndex <= 0 || Client.game.players[this.selectedCreaturePlayerOffset].IsSelectable(this.selectedCreatureIndex))
          {
            CameraMovement.Instance.LerpToTransform(this.selected);
            this.UpdateVisuals();
            this.UnselectSpell();
            if (!audio)
              break;
            AudioManager.Play(this.selected.clientObj.clipSelect);
            break;
          }
          goto label_1;
        }
        else
        {
          zcreature1 = zcreature2;
          num1 = this.selectedCreatureIndex;
          num2 = this.selectedCreaturePlayerOffset;
        }
      }
    }
  }

  public void UnselectSpell()
  {
    ClickSpell.Unselect();
    this.SetSpell(-1);
    this.ToggleExtendedShot(hardInput.GetKey("Extended Shot"));
    HUD.instance.PressingFire = false;
    HUD.instance.panelFireControls.SetActive(false);
    MyContextMenu.CloseInstance();
    MyToolTip.Close();
    this._LastMouseWorldPos.x = -9999f;
  }

  public void SetSpell(int i)
  {
    if ((UnityEngine.Object) this.meter_subs[0] == (UnityEngine.Object) null)
      return;
    this.selectedSpellIndex = i;
    for (int index = 0; index < this.meter_subs.Length; ++index)
      this.meter_subs[index].SetActive(false);
    CursorList.Instance.SetCursor(0);
    HUD.instance.panelFireControls.SetActive(true);
    HUD.instance.panelAimControls.SetActive(false);
    if (this.selectedSpellIndex > -1)
    {
      this.target = NullMyLocation.Get();
      this.secTarget = NullMyLocation.Get();
      this.selectedSpell = !this.selected.inWater ? this.selected.spells[this.selectedSpellIndex].spell : Inert.Instance.waterGate.spell;
      if (!HUD.instance.hideOverheadIcons)
        this.selected.selectedSpellPanel.SetActive(true);
      this.selected.selectedSpellIcon.sprite = ClientResources.Instance.icons.GetValue(ClickSpell.GetSpellName(this.selectedSpell, this.selected));
      this.meter_subs[6].transform.GetChild(0).gameObject.SetActive(ZSpell.Controllable(this.selectedSpell, this.person.game));
      this.meter_subs[2].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
      CastType type = this.selectedSpell.type;
      switch (type)
      {
        case CastType.Power:
          this.meter_subs[0].SetActive(true);
          HUD.instance.panelAimControls.SetActive(true);
          break;
        case CastType.TargetOnly:
          CursorList.Instance.SetCursor(1);
          this.meter_subs[6].transform.position = this.mouseWorldPos;
          this.meter_subs[6].SetActive(true);
          if (this.selectedSpell.maxDistance <= 10)
            break;
          int num1 = ZSpell.GetSpellMaxDistance(this.selectedSpell, this.selected) * 2;
          if (this.selectedSpell.spellEnum == SpellEnum.The_ol_swaparoo && this.selected.parent.GetLevel(BookOf.Illusion) == 5)
            num1 = ZSpell.GetSpellMaxDistance(this.selectedSpell, this.selected) * 3;
          this.meter_subs[7].SetActive(true);
          ((RectTransform) this.meter_subs[7].transform).sizeDelta = (Vector2) new Vector3((float) num1, (float) num1, 1f);
          this.meter_subs[7].transform.position = this.selected.transform.position;
          break;
        case CastType.Target_Power:
        case CastType.Target_Placement:
          CursorList.Instance.SetCursor(1);
          this.meter_subs[6].transform.position = this.mouseWorldPos;
          this.meter_subs[6].SetActive(true);
          if (this.selectedSpell.maxDistance <= 10 || this.selectedSpell.spellEnum != SpellEnum.Abduction)
            break;
          this.meter_subs[7].SetActive(true);
          ((RectTransform) this.meter_subs[7].transform).sizeDelta = (Vector2) new Vector3((float) (this.selectedSpell.maxDistance * 2), (float) (this.selectedSpell.maxDistance * 2), 1f);
          this.meter_subs[7].transform.position = this.selected.transform.position;
          break;
        case CastType.Flash:
          this.meter_subs[1].SetActive(true);
          HUD.instance.panelAimControls.SetActive(true);
          break;
        case CastType.Naplem:
        case CastType.Double_Naplem:
          this.meter_subs[2].SetActive(true);
          HUD.instance.panelAimControls.SetActive(true);
          break;
        case CastType.Tower:
          this.meter_subs[3].SetActive(true);
          Sprite sprite1 = this.selectedSpell.toSummon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
          ((RectTransform) this.meter_subs[3].transform).pivot = new Vector2(0.5f, 0.0f);
          ((RectTransform) this.meter_subs[3].transform).sizeDelta = new Vector2((float) sprite1.texture.width, (float) sprite1.texture.height);
          ((RectTransform) this.meter_subs[3].transform).anchoredPosition = new Vector2(0.0f, (float) -this.selected.radius);
          this.meter_subs[3].GetComponent<Image>().sprite = sprite1;
          if (ZSpell.CanFire(this.selectedSpell))
          {
            this.meter_subs[3].GetComponent<Image>().color = Color.green;
            break;
          }
          this.meter_subs[3].GetComponent<Image>().color = Color.red;
          break;
        case CastType.Placement:
          if (this.selectedSpell.spellEnum == SpellEnum.Resurrection || this.selectedSpell.spellEnum == SpellEnum.Summon_Titan)
          {
            this.ContextResurrection();
            break;
          }
          this.ShowSummoningRange(this.selectedSpell, this.selectedSpell.radius, this.selectedSpell.maxDistance, this.selectedSpell.affectedByGravity);
          break;
        case CastType.Flight:
          if (this.selectedSpell.spellEnum == SpellEnum.Miner_Market)
          {
            MinerMarket.ContextMenu(this.selected.minerMarket);
            break;
          }
          if (this.selectedSpell.spellEnum != SpellEnum.Ritual)
            break;
          MyContextMenu m = MyContextMenu.Show();
          m.SetOnCancel(new Action(this.UnselectSpell));
          List<SpellEnum> spellEnumList1 = new List<SpellEnum>()
          {
            SpellEnum.Wild_Mushrooms,
            SpellEnum.Spear_Throw,
            SpellEnum.Imbued_Axes,
            SpellEnum.Ferocious_Strike,
            SpellEnum.Summon_Wolf
          };
          List<SpellEnum> spellEnumList2 = new List<SpellEnum>()
          {
            SpellEnum.Commune_With_Nature,
            SpellEnum.Devils_Snare,
            SpellEnum.Summon_Spirit_Owl,
            SpellEnum.Leap,
            SpellEnum.Summon_Bear
          };
          int num2 = this.person.MaxRitualCount();
          m.AddSeperator("Active rituals: " + (object) this.person.ritualEnum.Count + "/" + (object) num2);
          m.AddSeperator("Level 1 Spells:");
          foreach (SpellEnum s1 in spellEnumList1)
          {
            SpellSlot s = this.selected.GetSpellSlot(s1);
            if (s != null && !this.person.ritualEnum.Contains(s.spell.GetSpellEnum))
              m.AddItemWithImage(s.spell.name, ClientResources.Instance.icons[s.spell.name], (Action) (() =>
              {
                m?.SetOnCancel((Action) null);
                this.ConfirmRitual((int) s.spell.spellEnum);
              }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }
          m.AddSeperator("Level 2 Spells:");
          foreach (SpellEnum s2 in spellEnumList2)
          {
            SpellSlot s = this.selected.GetSpellSlot(s2);
            if (s != null && !this.person.ritualEnum.Contains(s.spell.GetSpellEnum))
              m.AddItemWithImage(s.spell.name, ClientResources.Instance.icons[s.spell.name], (Action) (() =>
              {
                m?.SetOnCancel((Action) null);
                this.ConfirmRitual((int) s.spell.spellEnum);
              }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }
          m.Rebuild();
          break;
        case CastType.Blit:
          this.meter_subs[3].SetActive(true);
          Sprite sprite2 = this.selectedSpell.GetComponent<SpriteRenderer>().sprite;
          ((RectTransform) this.meter_subs[3].transform).pivot = new Vector2(0.5f, 0.5f);
          ((RectTransform) this.meter_subs[3].transform).sizeDelta = new Vector2((float) sprite2.texture.width, (float) sprite2.texture.height);
          this.meter_subs[3].transform.position = this.mouseWorldPos;
          this.meter_subs[3].GetComponent<Image>().sprite = sprite2;
          if (ZSpell.CanFire(this.selectedSpell))
            this.meter_subs[3].GetComponent<Image>().color = Color.green;
          else
            this.meter_subs[3].GetComponent<Image>().color = Color.red;
          if (this.selectedSpell.maxDistance <= 0)
            break;
          this.meter_subs[7].SetActive(true);
          ((RectTransform) this.meter_subs[7].transform).sizeDelta = (Vector2) new Vector3((float) (this.selectedSpell.maxDistance * 2), (float) (this.selectedSpell.maxDistance * 2), 1f);
          this.meter_subs[7].transform.position = this.selected.transform.position;
          break;
        default:
          Debug.Log((object) ("Wrong spell type: " + (object) type + " in Player.SetSpell."));
          break;
      }
    }
    else
    {
      if ((ZComponent) this.selected != (object) null)
        this.selected.selectedSpellPanel.SetActive(false);
      this.selectedSpell = (Spell) null;
    }
  }

  public void ContextResurrection()
  {
    if (this.selectedSpell.spellEnum == SpellEnum.Resurrection)
    {
      MyContextMenu m = MyContextMenu.Show();
      m.SetOnCancel(new Action(this.UnselectSpell));
      int num = 0;
      if (this.person.game.lastMinionToDie.Count == 0)
        m.AddSeperator("No suitable minions to resurrect ");
      else
        m.AddSeperator("Select a minion to resurrect ");
      foreach (ZGame.Resurection resurection in this.person.game.lastMinionToDie)
      {
        Spell spell = Inert.GetSpell(resurection.spell);
        int z = num;
        if ((UnityEngine.Object) spell != (UnityEngine.Object) null && ClientResources.Instance.icons.ContainsKey(spell.name))
          m.AddItemWithImage(spell.name.Replace("Summon ", ""), ClientResources.Instance.icons[spell.name], (Action) (() =>
          {
            m?.SetOnCancel((Action) null);
            if (!((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null))
              return;
            this.ConFirmResurrect(z);
          }), Color.green);
        ++num;
      }
      m.Rebuild();
    }
    else
    {
      if (this.selectedSpell.spellEnum != SpellEnum.Summon_Titan)
        return;
      MyContextMenu m = MyContextMenu.Show();
      m.SetOnCancel(new Action(this.UnselectSpell));
      int num = 0;
      if (this.person.minionBookTitans.Where<ZGame.MinionBookTitan>((Func<ZGame.MinionBookTitan, bool>) (a => !a.used)).Count<ZGame.MinionBookTitan>() == 0)
        m.AddSeperator("No suitable Level 3 minions to Summon");
      else
        m.AddSeperator("Select a minion to Summon");
      foreach (ZGame.MinionBookTitan minionBookTitan in this.person.minionBookTitans)
      {
        Spell spell = Inert.GetSpell(minionBookTitan.spell);
        int z = num;
        if ((UnityEngine.Object) spell != (UnityEngine.Object) null && !minionBookTitan.used && ClientResources.Instance.icons.ContainsKey(spell.name))
          m.AddItemWithImage(spell.name.Replace("Summon ", ""), ClientResources.Instance.icons[spell.name], (Action) (() =>
          {
            m?.SetOnCancel((Action) null);
            if (!((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null))
              return;
            this.ConFirmSummonTitan(z);
          }), Color.green);
        ++num;
      }
      m.Rebuild();
    }
  }

  public void ShowSummoningRange(
    Spell selectedSpell,
    int radius,
    int maxDistance,
    bool affectedByGravity)
  {
    CursorList.Instance.SetCursor(1);
    if (selectedSpell.spellEnum == SpellEnum.Dive)
      this.mouseWorldPos.y = this.selected.type == CreatureType.Kraken ? 0.0f : (float) this.selected.radius;
    if (selectedSpell.spellEnum == SpellEnum.Summon_Kraken)
      this.mouseWorldPos.y = 0.0f;
    this.meter_subs[4].transform.position = this.mouseWorldPos;
    this.meter_subs[4].SetActive(true);
    if (selectedSpell.spellEnum == SpellEnum.Arcane_Gate || selectedSpell.spellEnum == SpellEnum.Santas_Magic || selectedSpell.spellEnum == SpellEnum.Dive || selectedSpell.spellEnum == SpellEnum.Blink)
      ((RectTransform) this.meter_subs[4].transform).sizeDelta = new Vector2((float) ((!((ZComponent) this.selected.tower != (object) null) || this.selected.tower.type != TowerType.Arcane ? this.selected.radius : 32) * 2), (float) ((!((ZComponent) this.selected.tower != (object) null) || this.selected.tower.type != TowerType.Arcane ? this.selected.radius : 32) * 2));
    else if (selectedSpell.spellEnum == SpellEnum.Arcane_Portal)
    {
      int num = this.selected.game.originalSpellsOnly ? 40 : this.selected.radius * 2 + 4;
      ((RectTransform) this.meter_subs[4].transform).sizeDelta = new Vector2((float) num, (float) num);
    }
    else if (selectedSpell.spellEnum == SpellEnum.Wormhole)
    {
      int num = radius * 2 + 2;
      ((RectTransform) this.meter_subs[4].transform).sizeDelta = new Vector2((float) num, (float) num);
    }
    else
    {
      if (selectedSpell.spellEnum == SpellEnum.Summon_Wolf && this.selected.parent.ritualEnum.Contains(SpellEnum.Summon_Wolf))
      {
        int num = 46;
        ((RectTransform) this.meter_subs[4].transform).sizeDelta = new Vector2((float) num, (float) num);
        this.meter_subs[7].SetActive(true);
        ((RectTransform) this.meter_subs[7].transform).sizeDelta = (Vector2) new Vector3((float) (ZSpell.GetSpellMaxDistance(selectedSpell, this.selected) * 2 + 5), (float) (ZSpell.GetSpellMaxDistance(selectedSpell, this.selected) * 2 + 5), 1f);
        this.meter_subs[7].transform.position = this.selected.transform.position + new Vector3(0.0f, !affectedByGravity || maxDistance <= 150 ? 0.0f : (float) (maxDistance - 150));
        return;
      }
      ((RectTransform) this.meter_subs[4].transform).sizeDelta = new Vector2((float) (radius * 2), (float) (radius * 2));
    }
    if (maxDistance > 0)
    {
      this.meter_subs[7].SetActive(true);
      ((RectTransform) this.meter_subs[7].transform).sizeDelta = (Vector2) (selectedSpell.spellEnum == SpellEnum.Duplication || selectedSpell.spellEnum == SpellEnum.Resurrection || selectedSpell.spellEnum == SpellEnum.Summon_Titan ? new Vector3((float) (maxDistance * 2), (float) (maxDistance * 2), 1f) : new Vector3((float) (ZSpell.GetSpellMaxDistance(selectedSpell, this.selected) * 2), (float) (ZSpell.GetSpellMaxDistance(selectedSpell, this.selected) * 2), 1f));
      this.meter_subs[7].transform.position = this.selected.transform.position + new Vector3(0.0f, !affectedByGravity || maxDistance <= 150 ? 0.0f : (float) (maxDistance - 150));
    }
    else if (selectedSpell.spellEnum == SpellEnum.Arcane_Portal)
    {
      int num1 = this.selected.radius - 18;
      this.meter_subs[8].transform.localPosition = !((ZComponent) this.selected.tower != (object) null) || this.selected.tower.type == TowerType.Nature ? (Vector3) this.selected.OverHeadOffset.ToSinglePrecision() + new Vector3(0.0f, (float) (42 + num1)) : (Vector3) this.selected.OverHeadOffset.ToSinglePrecision() + new Vector3(0.0f, (float) (25 + num1));
      this.meter_subs[8].SetActive(true);
      int num2 = this.selected.game.originalSpellsOnly ? 38 : this.selected.radius * 2 + 2;
      ((RectTransform) this.meter_subs[8].transform).sizeDelta = new Vector2((float) num2, (float) num2);
    }
    else
    {
      if (selectedSpell.spellEnum != SpellEnum.Wormhole)
        return;
      int num3 = this.selected.radius - 18;
      this.meter_subs[8].transform.localPosition = !((ZComponent) this.selected.tower != (object) null) || this.selected.tower.type == TowerType.Nature ? (Vector3) this.selected.OverHeadOffset.ToSinglePrecision() + new Vector3(0.0f, (float) (42 + num3)) : (Vector3) this.selected.OverHeadOffset.ToSinglePrecision() + new Vector3(0.0f, (float) (25 + num3));
      this.meter_subs[8].SetActive(true);
      int num4 = radius * 2 + 2;
      ((RectTransform) this.meter_subs[8].transform).sizeDelta = new Vector2((float) num4, (float) num4);
    }
  }

  public void ConFirmResurrect(int i)
  {
    if (i >= this.person.game.lastMinionToDie.Count || (UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null || this.selectedSpell.spellEnum != SpellEnum.Resurrection)
      return;
    Spell spell = Inert.GetSpell(this.person.game.lastMinionToDie[i].spell);
    Creature component = spell.toSummon.GetComponent<Creature>();
    this.rot_z = (FixedInt) i;
    if (!((UnityEngine.Object) spell != (UnityEngine.Object) null))
      return;
    this.ShowSummoningRange(this.selectedSpell, component.radius, spell.maxDistance, spell.affectedByGravity);
  }

  public void ConFirmSummonTitan(int i)
  {
    if (i >= this.person.minionBookTitans.Count || (UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null || this.selectedSpell.spellEnum != SpellEnum.Summon_Titan || this.person.minionBookTitans[i].used)
      return;
    Spell spell = Inert.GetSpell(this.person.minionBookTitans[i].spell);
    Creature component = spell.toSummon.GetComponent<Creature>();
    this.rot_z = (FixedInt) i;
    if (!((UnityEngine.Object) spell != (UnityEngine.Object) null))
      return;
    this.ShowSummoningRange(this.selectedSpell, component.radius, spell.maxDistance, spell.affectedByGravity);
  }

  public void ConfirmRitual(int i)
  {
    if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null)
      return;
    this.spellTarget = new MyLocation(0, i);
    this.SendFire();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) Player.Instance == (UnityEngine.Object) this))
      return;
    Player.Instance = (Player) null;
  }

  private void Awake()
  {
    this.meter = GameObject.FindGameObjectWithTag("meter");
    if ((UnityEngine.Object) this.meter == (UnityEngine.Object) null)
      return;
    this.meter_subs = new GameObject[this.meter.transform.childCount];
    for (int index = 0; index < this.meter_subs.Length; ++index)
      this.meter_subs[index] = this.meter.transform.GetChild(index).gameObject;
    this.meterFill = this.meter_subs[0].transform.GetChild(1).GetComponent<Image>();
    this.LockMeter = Global.GetPrefBool("preflockmeter", false);
    this.keyCodes = (KeyCode[]) Enum.GetValues(typeof (KeyCode));
  }

  private void Start()
  {
    this.UpdateVisuals();
    if ((UnityEngine.Object) SelectionArrow.Instance == (UnityEngine.Object) null)
      UnityEngine.Object.Instantiate<SelectionArrow>(ClientResources.Instance._selectionArrow);
    this.jumpIsDetower = Global.GetPrefBool("prefjumpdetower", false);
  }

  public void Hide_All()
  {
    AudioManager.StopChargeSpell();
    HUD.instance.HideEverything(false);
  }

  public void Show_All()
  {
    this.UpdateVisuals();
    HUD.instance.ShowEverything(false);
  }

  private void Show_Meter(bool b)
  {
    if (this.hidden == b)
      return;
    this.hidden = b;
    if (!b && this.selectedSpellIndex >= 0)
      this.UnselectSpell();
    this.meter.SetActive(b);
  }

  public void ToggleExtendedShot(bool v)
  {
    this.extendedShot = v;
    HUD.instance.buttonAlt.AlwaysOn = v;
    float x = 1f;
    if ((UnityEngine.Object) this.meter_subs[0] == (UnityEngine.Object) null)
      return;
    if (v)
      x = 1f + Player.LinearExtend(Player.GetPowerLevel(this.rot_z), (FixedInt) 1).ToFloat();
    for (int index = 0; index < 2; ++index)
      this.meter_subs[index].transform.localScale = new Vector3(x, 1f, 1f);
  }

  public static FixedInt ClampAngle(FixedInt f)
  {
    return f >= 0 ? (f <= 90 ? Player.Clamp90(f) : (FixedInt) 90 + Player.Clamp90(f - 90)) : (f < -90 ? (FixedInt) -90 - Player.Clamp90(-(f + 90)) : -Player.Clamp90(-f));
  }

  public static Player.PowerLevel GetPowerLevel(FixedInt f)
  {
    if (f >= 0)
    {
      if (!(f <= 90))
        f -= 90;
    }
    else
      f = !(f < -90) ? -f : -(f + 90);
    return !(f > 45) ? (Player.PowerLevel) (int) (f / (FixedInt) 7864320L) : (Player.PowerLevel) (int) (((FixedInt) 90 - f) / (FixedInt) 7864320L);
  }

  public static FixedInt LinearExtend(Player.PowerLevel p, FixedInt radius)
  {
    return (FixedInt) (int) p / (FixedInt) 6291456L * radius;
  }

  public static int LightningExtend(Player.PowerLevel p) => Mathf.Max(2, (int) p);

  public static FixedInt PowerExtend(CastType t, Player.PowerLevel p, FixedInt orig)
  {
    FixedInt fixedInt = (FixedInt) (int) p;
    switch (t)
    {
      case CastType.Power:
      case CastType.Target_Power:
        return orig * (fixedInt / (FixedInt) 12582912L);
      case CastType.Naplem:
      case CastType.Double_Naplem:
        return fixedInt;
      default:
        return (FixedInt) 0;
    }
  }

  private static FixedInt Clamp90(FixedInt f)
  {
    if (f <= (FixedInt) 11796480L)
      return (FixedInt) 7864320L;
    if (f <= (FixedInt) 19660800L)
      return (FixedInt) 15728640L;
    if (f <= (FixedInt) 27525120L)
      return (FixedInt) 23592960L;
    if (f <= (FixedInt) 35389440L)
      return (FixedInt) 31457280L;
    if (f <= (FixedInt) 43253760L)
      return (FixedInt) 39321600L;
    if (f <= (FixedInt) 51118080L)
      return (FixedInt) 47185920L;
    if (f <= (FixedInt) 58982400L)
      return (FixedInt) 55050240L;
    if (f <= (FixedInt) 66846720L)
      return (FixedInt) 62914560L;
    if (f <= (FixedInt) 74711040L)
      return (FixedInt) 70778880L;
    return f <= (FixedInt) 82575360L ? (FixedInt) 78643200L : (FixedInt) 86507520L;
  }

  internal void CenterCamera()
  {
    if (!ZComponent.IsNull((ZComponent) this.selected))
    {
      if (this.selected.type == CreatureType.Beehive)
      {
        ZCreatureBeehive selected = (ZCreatureBeehive) this.selected;
        bool flag = (ZComponent) CameraMovement.Instance.target == (object) this.selected;
        for (int index = 0; index < selected.bees.Count; ++index)
        {
          if (flag && (ZComponent) selected.bees[index] != (object) null)
          {
            CameraMovement.Instance.LerpToTransform(selected.bees[index], true);
            return;
          }
          if ((ZComponent) selected.bees[index] != (object) null && (ZComponent) selected.bees[index] == (object) CameraMovement.Instance.target)
            flag = true;
        }
      }
      CameraMovement.Instance.LerpToTransform(this.selected, true);
    }
    else if (this.person != null && this.person.controlled.Count > 0 && this.person.yourTurn)
    {
      CameraMovement.Instance.LerpToTransform(this.person.controlled[0], true);
    }
    else
    {
      ZCreature v = this.person.game.CurrentCreature();
      if (!((ZComponent) v != (object) null))
        return;
      CameraMovement.Instance.LerpToTransform(v, true);
    }
  }

  public void UpdateMouseWorldPos(bool force = false)
  {
    if (!force && Player.IsPointerOverGameObject() || !force && HUD.UseTouchControls)
      return;
    this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    this.mouseWorldPos.z = 0.0f;
    if (!((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null))
      return;
    if (this.selectedSpell.spellEnum == SpellEnum.Dive)
    {
      this.mouseWorldPos.y = this.selected.type == CreatureType.Kraken ? 0.0f : (float) this.selected.radius;
    }
    else
    {
      if (this.selectedSpell.spellEnum != SpellEnum.Summon_Kraken)
        return;
      this.mouseWorldPos.y = 0.0f;
    }
  }

  public static bool IsPointerOverGameObject(int i = 0)
  {
    if (HUD.UseTouchControls && HUD.instance.PressingOnScreenControl || !EventSystem.current.IsPointerOverGameObject())
      return false;
    GameObject gameObject = StandaloneInputModuleV2.CurrentInput.GameObjectUnderPointer();
    return !((UnityEngine.Object) gameObject != (UnityEngine.Object) null) || !gameObject.CompareTag("Ignore");
  }

  public Vector3 CurrentMousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

  internal static void InitUncontrolled()
  {
    ZPerson uncontrolledPlayer = Client.game._uncontrolledPlayer;
    ZCreature character = Inert.CreateCharacter(uncontrolledPlayer, new SettingsPlayer(), new MyLocation(0, -1000), 24, false);
    character.bg.color = uncontrolledPlayer.clientColor;
    character.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = uncontrolledPlayer.clientColor;
    uncontrolledPlayer.controlled.Insert(0, character);
    character.Fall();
  }

  internal static ZPerson SpawnZezima(
    MyLocation v,
    bool controllable = false,
    string name = "Zezima",
    int team = 1,
    SettingsPlayer spDefault = null,
    bool onPlayerPanel = true)
  {
    ZPerson zperson = new ZPerson();
    zperson.account.prestige = (byte) 5;
    SettingsPlayer sp = spDefault ?? new SettingsPlayer();
    if (spDefault == null)
    {
      string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "SavedSpells" + Path.DirectorySeparatorChar.ToString() + name + ".spellBook";
      SettingsPlayer b1 = File.Exists(path) ? SettingsPlayer.Load(path) : Client.settingsPlayer;
      sp.CopySpells(b1);
      string[] strArray = new string[6];
      strArray[0] = Application.persistentDataPath;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      strArray[1] = directorySeparatorChar.ToString();
      strArray[2] = "SavedOutfits";
      directorySeparatorChar = Path.DirectorySeparatorChar;
      strArray[3] = directorySeparatorChar.ToString();
      strArray[4] = name;
      strArray[5] = ".outfit";
      SettingsPlayer b2 = SettingsPlayer.Load(string.Concat(strArray));
      sp.CopyOutfit(b2);
    }
    zperson.name = name;
    zperson.id = (byte) team;
    zperson.clientColor = TeamColors.GetColor(team);
    zperson.settingsPlayer = sp;
    zperson.game = Client.game;
    zperson.team = team;
    ZCreature character = Inert.CreateCharacter(zperson, sp, v, team, onPlayerPanel);
    character.team = team;
    character.bg.color = zperson.clientColor;
    character.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = zperson.clientColor;
    zperson.controlled.Add(character);
    if (controllable)
    {
      Client.game.players.Add(zperson);
      Client.game._playersExtended.Add(zperson);
      TcpConnection tcpConnection = new TcpConnection();
      tcpConnection.player.player = zperson;
      Client.game.gameFacts.connections.Add((Connection) tcpConnection);
      HUD.FindFullBooks(Client.game, zperson, (int) zperson.id);
    }
    int result = 0;
    if (int.TryParse(HUD.instance.inputDummyHp.text, out result) && result > 0)
    {
      character.health = result;
      character.maxHealth = result;
      character.UpdateHealthTxt();
    }
    character.Fall();
    return zperson;
  }

  internal bool softLock
  {
    get => (double) this._softLock > 0.0;
    set => this._softLock = value ? 0.5f : 0.0f;
  }

  private void Update()
  {
    this.movedThisTurn = false;
    if (this.person == null)
      return;
    if (this.person != null && !this.person.game.AllowInput)
    {
      if (this.softLock)
        return;
      this.softLock = true;
    }
    else
    {
      if (this.softLock)
      {
        this._softLock -= Time.deltaTime;
        if (Input.touchCount == 0 && Input.touchSupported)
          this.softLock = false;
        if (hardInput.GetKey("Move Right") || hardInput.GetKey("Move Left") || this.PressingLongJump() || this.PressingHighJump() || this.PressingDetower() || hardInput.GetKey("Skip Turn") || hardInput.GetKey("Minions") || Input.GetMouseButton(0) || Input.GetMouseButton(1))
          return;
        for (int index = this.hotkeys.Count - 1; index >= 0; --index)
        {
          if (Input.GetKeyDown(this.hotkeys[index].key))
            return;
        }
        this.softLock = false;
      }
      if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null && this.person.yourTurn != HUD.instance.panelMoveControls.activeSelf && (HUD.UseTouchControls || HUD.instance.panelMoveControls.activeSelf))
      {
        if (!this.person.yourTurn)
          HUD.instance.DisableButtons();
        HUD.instance.panelMoveControls.SetActive(this.person.yourTurn);
      }
      if (Client.game.isSandbox && (!Client.game.isTutorial || Client.allowtutorialDebugging))
      {
        if (Input.GetKeyDown(KeyCode.F8))
        {
          if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null)
          {
            Controller.Instance.ShowChatBox(false);
            ChatBox.Instance?.NewChatMsg("", "Welcome to the dev console", (Color) ColorScheme.GetColor(Global.ColorTeamText), "", ChatOrigination.System);
            this.Hide_All();
            this.Show_All();
          }
          else
          {
            Controller.Instance.DestroyChatBox();
            this.Hide_All();
            this.Show_All();
          }
        }
        if ((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null && ChatBox.Instance.Active && ChatBox.Instance.chatInput.enabled && (!Client.game.isTutorial || !Client.allowtutorialDebugging))
          return;
        if (this.summoniningDummy)
        {
          this.UpdateMouseWorldPos();
          this.meter_subs[4].transform.position = this.mouseWorldPos;
          if (Input.GetMouseButtonDown(1))
          {
            this.StopSummoningDummy();
            return;
          }
          if (Player.IsPointerOverGameObject() || !Input.GetMouseButtonDown(0))
            return;
          if (!this.summonOnYourTeam && this.summonedPerson == null)
          {
            this.summonedPerson = this.person.game._uncontrolledPlayer;
            Player.InitUncontrolled();
          }
          AudioManager.PlayFromSource(this.summoniningDummySpell.castClip, AudioManager.instance.sourceCastSpell);
          ZCreature zcreature = ZSpell.FireSummon(this.summoniningDummySpell, HUD.instance.game, this.summonOnYourTeam ? Player.Instance.person.first() : this.summonedPerson.first(), (MyLocation) this.mouseWorldPos, 1);
          if ((ZComponent) zcreature != (object) null)
          {
            int result = 0;
            if (int.TryParse(HUD.instance.inputDummyHp.text, out result) && result > 0)
            {
              zcreature.health = result;
              zcreature.maxHealth = result;
              zcreature.UpdateHealthTxt();
            }
          }
          if (Input.GetKey(KeyCode.LeftShift))
            return;
          this.StopSummoningDummy();
          return;
        }
      }
      if (HUD.instance.Chatting())
      {
        this.softLock = true;
      }
      else
      {
        if (this.person != null && this.person.InWater() && (ZComponent.IsNull((ZComponent) this.selected) || this.selected.type != CreatureType.Recall_Device) && this.person.yourTurn && (UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null && Client.game.ongoing.NumberOfSlowUpdateCoroutines == 0 && Client.game.serverState.busy == ServerState.Busy.No)
        {
          this.selectedCreatureIndex = 0;
          this.selectedCreaturePlayerOffset = (int) this.person.id;
          this.selected = this.person.controlled[0];
          this.UpdateVisuals();
          this.SetSpell(0);
        }
        if (hardInput.GetKeyDown("Center Camera"))
          this.CenterCamera();
        if (Client.game.isReplay || Client.game.resyncing || (UnityEngine.Object) HUD.instance == (UnityEngine.Object) null)
          return;
        if (this.extendedShot && hardInput.GetKeyUp("Extended Shot") && !Input.GetMouseButton(0))
          this.ToggleExtendedShot(false);
        else if (!this.extendedShot && hardInput.GetKeyDown("Extended Shot") && !Input.GetMouseButton(0))
          this.ToggleExtendedShot(true);
        if (((int) Client.game.serverState.playersTurn != Client.curGameID || Client.game.ongoing.NumberOfSlowUpdateCoroutines > 0 || Client.game.serverState.busy != ServerState.Busy.No) && (!Client.game.isSandbox || Client.game.serverState.busy != ServerState.Busy.Starting_Turn))
        {
          this.Hide_All();
        }
        else
        {
          if (ZComponent.IsNull((ZComponent) this.selected) && this.person.yourTurn)
            this.NextControlled();
          if (ZComponent.IsNull((ZComponent) this.selected) || this.selected.isMoving || this.selected.isDead)
          {
            this.Show_Meter(false);
          }
          else
          {
            this.Show_Meter(true);
            Vector3 lastMouseWorldPos = this._LastMouseWorldPos;
            if (!Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0) || !this.LockMeter && !this.extendedShot)
              this.UpdateMouseWorldPos();
            Vector3 zero = Vector3.zero;
            Vector3 vector3 = this.selected.transform.position;
            this.meter.transform.position = vector3;
            if ((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && this.selectedSpell.spellEnum == SpellEnum.Abduction && !this.target.IsNull())
              vector3 = (Vector3) this.target.ToSinglePrecision();
            if ((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && (this.selectedSpell.type == CastType.Placement || this.selectedSpell.type == CastType.Flight || this.selectedSpell.type == CastType.Tower || this.selectedSpell.type == CastType.Blit || this.selectedSpell.type == CastType.TargetOnly || (this.selectedSpell.type == CastType.Target_Power || this.selectedSpell.type == CastType.Target_Placement) && this.target.IsNull()))
            {
              if (this.selectedSpell.type == CastType.Flight || this.selectedSpell.type == CastType.Tower)
              {
                if ((double) this.meter.transform.localEulerAngles.z != 0.0)
                  this.meter.transform.rotation = Quaternion.identity;
              }
              else if (lastMouseWorldPos != this.mouseWorldPos || this._lastExtendedShot != this.extendedShot)
              {
                this._lastExtendedShot = this.extendedShot;
                this._LastMouseWorldPos = this.mouseWorldPos;
                if (this.selectedSpell.spellEnum == SpellEnum.Dive)
                  this.mouseWorldPos.y = this.selected.type == CreatureType.Kraken ? 0.0f : (float) this.selected.radius;
                else if (this.selectedSpell.spellEnum == SpellEnum.Summon_Kraken)
                  this.mouseWorldPos.y = 0.0f;
                int di = this.selectedSpell.type == CastType.Placement || this.selectedSpell.type == CastType.Target_Placement && !this.secTarget.IsNull() ? 4 : (this.selectedSpell.type == CastType.Blit ? 3 : 6);
                this.meter.transform.rotation = Quaternion.identity;
                this.meter_subs[di].transform.position = this.mouseWorldPos;
                if (di == 4 || di == 3)
                {
                  if (ZSpell.CanFire(this.selectedSpell))
                  {
                    if (this.selectedSpell.spellEnum == SpellEnum.Arcane_Portal || this.selectedSpell.spellEnum == SpellEnum.Wormhole)
                      this.meter_subs[8].GetComponent<Image>().color = Color.green;
                    this.meter_subs[di].GetComponent<Image>().color = Color.green;
                    this.CheckCanMount(di);
                  }
                  else
                  {
                    if (this.selectedSpell.spellEnum == SpellEnum.Arcane_Portal || this.selectedSpell.spellEnum == SpellEnum.Wormhole)
                      this.meter_subs[8].GetComponent<Image>().color = Color.red;
                    this.meter_subs[di].GetComponent<Image>().color = Color.red;
                  }
                }
                else
                  ZSpell.CanFire(this.selectedSpell);
              }
            }
            else
            {
              this.diff = this.mouseWorldPos - vector3;
              if (HUD.UseTouchControls)
              {
                if (!Player.IsPointerOverGameObject() && (Input.GetMouseButton(0) || Input.touchCount == 1) && !HUD.instance.PressingOnScreenControl)
                  this.rot_z = Input.GetMouseButton(0) ? (FixedInt) (Mathf.Atan2(this.diff.y, this.diff.x) * 57.29578f) : this.rot_z;
              }
              else
                this.rot_z = (FixedInt) (Mathf.Atan2(this.diff.y, this.diff.x) * 57.29578f);
              if (!ZComponent.IsNull((ZComponent) this.selected))
                this.rot_z = this.selected.ClampAngle(this.rot_z);
              float num = 0.0f;
              if (this.extendedShot && (UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && this.selectedSpell.type != CastType.Placement && this.selectedSpell.type != CastType.Target_Placement)
              {
                this.rot_z = Player.ClampAngle(this.rot_z);
                this.ToggleExtendedShot(true);
                num = (float) ((int) Player.GetPowerLevel(this.rot_z) * 4);
              }
              Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, this.rot_z.ToFloat());
              this.meter_subs[0].transform.rotation = quaternion;
              this.meter_subs[1].transform.rotation = quaternion;
              this.meter_subs[2].transform.rotation = quaternion;
              Transform transform1 = this.meter_subs[0].transform;
              MyLocation myLocation = Global.VelocityRight(this.rot_z, (FixedInt) (this.selected.radius - 5));
              Vector3 singlePrecision1 = (Vector3) myLocation.ToSinglePrecision();
              transform1.localPosition = singlePrecision1;
              this.meter_subs[1].transform.localPosition = this.meter_subs[0].transform.localPosition;
              if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null || this.selectedSpell.spellEnum != SpellEnum.Abduction)
              {
                if ((double) num > 0.0)
                {
                  Transform transform2 = this.meter_subs[2].transform;
                  myLocation = Global.VelocityRight(this.rot_z, (FixedInt) (this.selected.radius - 5) + (FixedInt) num);
                  Vector3 singlePrecision2 = (Vector3) myLocation.ToSinglePrecision();
                  transform2.localPosition = singlePrecision2;
                }
                else
                  this.meter_subs[2].transform.localPosition = this.meter_subs[0].transform.localPosition;
              }
              if ((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && (this.selectedSpell.type == CastType.Naplem || this.selectedSpell.spellEnum == SpellEnum.Abduction))
              {
                if (ZSpell.CanFire(this.selectedSpell))
                {
                  this.meter_subs[2].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                  this.meter_subs[2].transform.GetChild(1).GetComponent<Image>().color = Color.white;
                  this.meter_subs[2].transform.GetChild(2).GetComponent<Image>().color = Color.white;
                }
                else
                {
                  this.meter_subs[2].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                  this.meter_subs[2].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                  this.meter_subs[2].transform.GetChild(2).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                }
              }
            }
            Vector3 mouseWorldPos = this.mouseWorldPos;
            bool flag = true;
            if ((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && (this.selectedSpell.type == CastType.Power || this.selectedSpell.type == CastType.Naplem || this.selectedSpell.type == CastType.Double_Naplem || this.selectedSpell.type == CastType.Flash || (this.selectedSpell.type == CastType.Target_Power || this.selectedSpell.type == CastType.Target_Placement) && !this.target.IsNull()))
            {
              flag = false;
              if (this.rot_z > 90 || this.rot_z < -90)
              {
                if ((double) this.selected.transformscale > 0.0)
                  this.SendFlipFlop();
              }
              else if ((double) this.selected.transformscale < 0.0)
                this.SendFlipFlop();
            }
            if (Client.game.ongoing.NumberOfSlowUpdateCoroutines != 0 || Client.game.serverState.busy != ServerState.Busy.No)
              return;
            if (!this.selected.stunned && !this.selected.inWater && !this.selected.InDarkTotem())
            {
              if ((double) this.movementUpdate > (double) this.targetUpdate || hardInput.GetKeyDown("Move Right") || hardInput.GetKeyDown("Move Left") || HUD.instance.PressingMoveRight || HUD.instance.PressingMoveLeft || this.InTower(TowerType.Cosmos) && (this.PressingLongJump() || this.PressingHighJump()))
              {
                for (; (double) this.movementUpdate >= (double) this.targetUpdate && (ZComponent) this.selected != (object) null && !this.selected.isMoving; this.movementUpdate -= this.targetUpdate)
                {
                  if ((ZComponent) this.selected.mount != (object) null || (ZComponent) this.selected.tower != (object) null && this.selected.tower.type != TowerType.Cogs && this.selected.tower.type != TowerType.Cosmos)
                  {
                    if ((hardInput.GetKey("Move Right") || HUD.instance.PressingMoveRight) && (double) this.lastSendTick < (double) Time.realtimeSinceStartup)
                    {
                      if ((double) this.movementUpdate < (double) this.targetUpdate)
                        this.movementUpdate = this.targetUpdate;
                      this.movementUpdate -= this.targetUpdate;
                      this.selected.SetScale(1f);
                      return;
                    }
                    if ((hardInput.GetKey("Move Left") || HUD.instance.PressingMoveLeft) && (double) this.lastSendTick < (double) Time.realtimeSinceStartup)
                    {
                      if ((double) this.movementUpdate < (double) this.targetUpdate)
                        this.movementUpdate = this.targetUpdate;
                      this.movementUpdate -= this.targetUpdate;
                      this.selected.SetScale(-1f);
                      return;
                    }
                  }
                  else if (this.selected.flying)
                  {
                    if (hardInput.GetKey("Move Right") || HUD.instance.PressingMoveRight)
                    {
                      this.SendMove((byte) 203);
                      this.UnselectSpell();
                    }
                    else if (hardInput.GetKey("Move Left") || HUD.instance.PressingMoveLeft)
                    {
                      this.SendMove((byte) 202);
                      this.UnselectSpell();
                    }
                  }
                  else if (hardInput.GetKey("Move Right") || HUD.instance.PressingMoveRight)
                  {
                    if ((ZComponent) this.selected.tower == (object) null || this.selected.tower.type == TowerType.Cogs || this.selected.tower.type == TowerType.Cosmos)
                    {
                      this.SendMove((byte) 203);
                      this.UnselectSpell();
                    }
                  }
                  else if (hardInput.GetKey("Move Left") || HUD.instance.PressingMoveLeft)
                  {
                    if ((ZComponent) this.selected.tower == (object) null || this.selected.tower.type == TowerType.Cogs || this.selected.tower.type == TowerType.Cosmos)
                    {
                      this.SendMove((byte) 202);
                      this.UnselectSpell();
                    }
                  }
                  else if (this.InTower(TowerType.Cosmos))
                  {
                    if (this.PressingHighJump())
                    {
                      if ((double) this.movementUpdate < (double) this.targetUpdate)
                        return;
                      this.SendMove((byte) 204);
                      this.UnselectSpell();
                    }
                    else if (this.PressingLongJump())
                    {
                      if ((double) this.movementUpdate < (double) this.targetUpdate)
                        return;
                      this.SendMove((byte) 205);
                      this.UnselectSpell();
                    }
                  }
                  if ((double) this.movementUpdate < (double) this.targetUpdate)
                    this.movementUpdate = this.targetUpdate;
                }
              }
              if (hardInput.GetKey("Move Left") || hardInput.GetKey("Move Right") || HUD.instance.PressingMoveRight || HUD.instance.PressingMoveLeft || this.InTower(TowerType.Cosmos) && (this.PressingLongJump() || this.PressingHighJump()))
                this.movementUpdate += Mathf.Clamp(Client.game.DeltaTime, 0.0f, 0.2f);
            }
            if (hardInput.GetKeyDown("Skip Turn") && this.canSkipTurn)
            {
              if (Client.game.isSandbox || Client.game.isTutorial || Client.game.players.Count <= 1 || Global.GetPrefBool("prefskipwarning", false))
                this.SkipTurn();
              else
                HUD.instance.ShowSkipTurnPanel();
            }
            for (int index = this.hotkeys.Count - 1; index >= 0; --index)
            {
              if (ZComponent.IsNull((ZComponent) this.hotkeys[index].creature) || this.hotkeys[index].creature.parent.team != this.person.team)
                this.hotkeys.RemoveAt(index);
              else if (Input.GetKeyDown(this.hotkeys[index].key))
              {
                this.UnselectSpell();
                this.selected = this.hotkeys[index].creature;
                this.selectedCreaturePlayerOffset = Client.game.players.FindIndex((Predicate<ZPerson>) (z => z == this.selected.parent));
                this.selectedCreatureIndex = this.selected.parent.controlled.FindIndex((Predicate<ZCreature>) (z => (ZComponent) z == (object) this.selected));
                CameraMovement.Instance.LerpToTransform(this.selected);
                this.UpdateVisuals();
                AudioManager.Play(this.selected.clientObj.clipSelect);
                if (this.hotkeys[index].slot <= -1 || this.hotkeys[index].slot >= this.selected.spells.Count)
                  return;
                this.SetSpell(this.hotkeys[index].slot);
                return;
              }
            }
            if (hardInput.GetKey("AssignableHotkey"))
            {
              KeyCode numberDown = this.GetNumberDown();
              if (numberDown != KeyCode.None)
              {
                for (int index = this.hotkeys.Count - 1; index >= 0; --index)
                {
                  if (ZComponent.IsNull((ZComponent) this.hotkeys[index].creature) || this.hotkeys[index].creature.parent.team != this.person.team || (ZComponent) this.hotkeys[index].creature == (object) this.selected && this.hotkeys[index].slot == -1)
                    this.hotkeys.RemoveAt(index);
                  else if (this.hotkeys[index].key == numberDown)
                    this.hotkeys.RemoveAt(index);
                }
                this.hotkeys.Add(new Player.AssignableHotkey()
                {
                  creature = this.selected,
                  key = numberDown,
                  slot = -1
                });
                ChatBox.Instance?.NewChatMsg("Added hotkey " + (object) numberDown + " for: " + this.selected.baseCreature.name, (Color) ColorScheme.GetColor(Global.ColorSystem));
                return;
              }
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
              for (int index = this.hotkeys.Count - 1; index >= 0; --index)
              {
                if (ZComponent.IsNull((ZComponent) this.hotkeys[index].creature) || this.hotkeys[index].creature.parent.team != this.person.team || (ZComponent) this.hotkeys[index].creature == (object) this.selected)
                  this.hotkeys.RemoveAt(index);
              }
            }
            if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null && !Player.IsPointerOverGameObject() && !HUD.instance.PressingOnScreenControl)
            {
              Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              if (Input.GetMouseButtonDown(0))
              {
                List<ZMyCollider> zmyColliderList = this.person.world.OverlapPointAll((int) worldPoint.x, (int) worldPoint.y, Inert.mask_entity_movement | Inert.mask_Phantom);
                for (int index1 = 0; index1 < zmyColliderList.Count; ++index1)
                {
                  ZCreature c = zmyColliderList[index1].creature;
                  if ((ZComponent) c == (object) null && (ZComponent) zmyColliderList[index1].tower != (object) null)
                    c = zmyColliderList[index1].tower.creature;
                  if (!((ZComponent) c == (object) null) && c.parent != null && c.parent.team == this.person.team)
                  {
                    this.UnselectSpell();
                    int index2 = Client.game.players.FindIndex((Predicate<ZPerson>) (z => z == c.parent));
                    if (index2 >= 0)
                    {
                      if (c.type == CreatureType.Bee)
                      {
                        foreach (ZCreature zcreature in Client.game.players[index2].controlled)
                        {
                          if (zcreature.type == CreatureType.Beehive)
                          {
                            ZCreatureBeehive zcreatureBeehive = zcreature as ZCreatureBeehive;
                            if ((ZComponent) zcreatureBeehive != (object) null && zcreatureBeehive.bees.FindIndex((Predicate<ZCreature>) (z => (ZComponent) z == (object) c)) >= 0)
                            {
                              c = zcreature;
                              break;
                            }
                          }
                        }
                      }
                      int index3 = Client.game.players[index2].controlled.FindIndex((Predicate<ZCreature>) (z => (ZComponent) z == (object) c));
                      if (index2 >= 0 && index3 >= 0 && c.controllable && (c.isPawn || c.parent == this.person))
                      {
                        if (c.minerMarket != null && c.minerMarket.Has(MinerMarket.Types.Platinum_Climbing_Hooks))
                          this.softLock = true;
                        this.selectedCreaturePlayerOffset = index2;
                        this.selectedCreatureIndex = index3;
                        this.selected = c;
                        CameraMovement.Instance.LerpToTransform(this.selected);
                        this.UpdateVisuals();
                        AudioManager.Play(this.selected.clientObj.clipSelect);
                        return;
                      }
                    }
                  }
                }
              }
            }
            if (hardInput.GetKeyDown("Minions"))
            {
              if (!this.person.InWater())
              {
                if (hardInput.GetKeyDownPrimaryOnly("Minions"))
                  this.NextControlled();
                else
                  this.PreviousControlled();
              }
              else if (hardInput.GetKeyDownPrimaryOnly("Minions"))
                this.NextRecallDevice(false);
              else
                this.PreviousRecallDevice();
            }
            if (!this.selected.stunned && (!this.movedThisTurn || (ZComponent) this.selected.tower == (object) null || this.selected.tower.type != TowerType.Cosmos))
            {
              if (this.selected.minerMarket != null && this.selected.minerMarket.Has(MinerMarket.Types.Platinum_Climbing_Hooks) && (UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null && Input.GetMouseButton(0) && !Player.IsPointerOverGameObject() && (!HUD.UseTouchControls || !HUD.instance.PressingOnScreenControl))
                this.SendMove((byte) 211);
              if ((ZComponent) this.selected.tower != (object) null && this.PressingDetower())
              {
                this.SendMove((byte) 209);
                this.UnselectSpell();
                this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
                return;
              }
              if (this.PressingLongJump() && (double) this.lastSendTick < (double) Time.realtimeSinceStartup && !this.selected.InDarkTotem() && (!this.selected.flying || (ZComponent) this.selected.mount == (object) null))
              {
                if ((ZComponent) this.selected.tower != (object) null)
                {
                  if (this.jumpIsDetower || this.PressingDetower())
                  {
                    this.SendMove((byte) 209);
                    this.UnselectSpell();
                    this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
                    return;
                  }
                  if (!this.InTower(TowerType.Cosmos))
                    ChatBox.Instance?.NewChatMsg("There is now a detower hotkey!!! <b>" + (HUD.UseTouchControls ? "" : hardInput.GetKeyName("detower", false)), (Color) ColorScheme.GetColor(Global.ColorSystem));
                  this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
                  return;
                }
                this.SendMove((byte) 205);
                this.UnselectSpell();
                this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
              }
              if (this.PressingHighJump() && !this.selected.isMoving && (double) this.lastSendTick < (double) Time.realtimeSinceStartup && !this.selected.InDarkTotem())
              {
                if ((ZComponent) this.selected.tower != (object) null)
                {
                  if (this.jumpIsDetower || this.PressingDetower())
                  {
                    this.SendMove((byte) 209);
                    this.UnselectSpell();
                    this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
                    return;
                  }
                  if (!this.InTower(TowerType.Cosmos))
                    ChatBox.Instance?.NewChatMsg("There is now a detower hotkey!!!  <b>" + (HUD.UseTouchControls ? "" : hardInput.GetKeyName("detower", false)), (Color) ColorScheme.GetColor(Global.ColorSystem));
                  this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
                  return;
                }
                this.SendMove((byte) 204);
                this.UnselectSpell();
                this.lastSendTick = Time.realtimeSinceStartup + 0.2f;
              }
            }
            if (hardInput.GetKeyDown("Flip Flop") & flag)
              this.FlipFlop();
            if (Input.GetMouseButtonDown(1) && (UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null)
              this.UnselectSpell();
            if (Player.IsPointerOverGameObject())
              return;
            if (Input.GetMouseButtonUp(0) && !HUD.UseTouchControls)
            {
              this.MouseUp();
              this.keyDown = KeyCode.None;
            }
            if (Input.GetMouseButton(0) || HUD.instance.PressingFire)
            {
              if (!HUD.UseTouchControls || HUD.instance.PressingFire)
              {
                this.MouseHeld();
              }
              else
              {
                this.UpdateMouseWorldPos(true);
                if ((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && this.selectedSpell.type != CastType.Double_Naplem)
                  this.spellTarget = (MyLocation) this.mouseWorldPos;
              }
            }
            else
            {
              if (this.selectingSpell)
                this.selectingSpell = false;
              if ((double) this.meterFill.fillAmount > 0.0)
                this.meterFill.fillAmount = 0.0f;
            }
            if (!Input.GetMouseButtonDown(0))
              return;
            this.keyDown = KeyCode.None;
            if (!HUD.UseTouchControls)
            {
              AudioManager.Play(AudioManager.instance.click);
              this.MouseDown();
            }
            else
            {
              this.UpdateMouseWorldPos(true);
              if ((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null && !HUD.instance.PressingOnScreenControl && this.selectedSpell.type != CastType.Double_Naplem)
                this.spellTarget = (MyLocation) this.mouseWorldPos;
            }
            if (!((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null) || this.selectedSpell.type != CastType.Power)
              return;
            foreach (KeyCode keyCode in this.keyCodes)
            {
              if (keyCode != KeyCode.Mouse0 && Input.GetKeyDown(keyCode) && keyCode != KeyCode.LeftAlt && keyCode != KeyCode.Mouse1)
              {
                this.keyDown = keyCode;
                break;
              }
            }
          }
        }
      }
    }
  }

  public void CheckCanMount(int di)
  {
    if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null || !this.selectedSpell.IsMinionSpell())
      return;
    Creature creature = this.selectedSpell.spellEnum != SpellEnum.Summon_Titan || !(this.rot_z >= 0) || !(this.rot_z < this.person.minionBookTitans.Count) ? this.selectedSpell.toSummon?.GetComponent<Creature>() : Inert.GetSpell(this.person.minionBookTitans[(int) this.rot_z].spell).toSummon.GetComponent<Creature>();
    if ((UnityEngine.Object) creature == (UnityEngine.Object) null)
      return;
    if (creature.type == CreatureType.Dragon && this.extendedShot && this.selected.canMount && creature.mountable && this.person.game.map.TryMount(creature, this.selected, this.selected.team))
      this.meter_subs[di].GetComponent<Image>().color = Color.blue;
    if (!creature.mountable || !creature.flying)
      return;
    List<ZMyCollider> list = this.person.game.world.OverlapCircleAll((Point) (MyLocation) this.mouseWorldPos, creature.radius + 1, (ZMyCollider) null, 256);
    for (int index = 0; index < list.Count; ++index)
    {
      if (!ZComponent.IsNull((ZComponent) list[index].creature) && !list[index].creature.isDead && this.person.game.map.TryMount(creature, list[index].creature, this.selected.team))
      {
        this.meter_subs[di].GetComponent<Image>().color = Color.blue;
        break;
      }
    }
    this.person.game.world.listPool.ReturnList(list);
  }

  internal void SkipTurn()
  {
    this.SendMove((byte) 206);
    this.skippingTurn = true;
    if (Client.game.isServer)
      return;
    Client.game.MoveQue.Enqueue((Action) (() =>
    {
      Client.game.serverState.busy = ServerState.Busy.Between_Turns;
      Client.game.serverState.turnTime = (float) Client.game.MaxTurnTime;
      this.person.yourTurn = false;
    }));
  }

  public void FlipFlop()
  {
    if (ZComponent.IsNull((ZComponent) this.selected) || Client.game.ongoing.NumberOfSlowUpdateCoroutines != 0 || !this.person.yourTurn)
      return;
    if ((double) this.selected.transformscale > 0.0)
      this.SendFlipFlop();
    else
      this.SendFlipFlop();
  }

  internal void MouseDown()
  {
    if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null || !ZSpell.CanFire(this.selectedSpell))
      return;
    CastType type = this.selectedSpell.type;
    switch (type)
    {
      case CastType.Power:
        AudioManager.PlayChargeSpell((int) this.selectedSpell.bookOf);
        return;
      case CastType.TargetOnly:
      case CastType.Blit:
        this.spellTarget = (MyLocation) this.mouseWorldPos;
        if (this.selectedSpell.spellEnum == SpellEnum.Duplication)
        {
          ZCreature zcreature = Client.map.PhysicsCollideCreature((ZCreature) null, (int) this.spellTarget.x, (int) this.spellTarget.y, Inert.mask_Phantom);
          if (!((ZComponent) zcreature != (object) null) || zcreature.type == CreatureType.Tree || zcreature.race == CreatureRace.Effector)
          {
            this.spellTarget = NullMyLocation.Get();
            return;
          }
        }
        this.SendFire();
        this.UnselectSpell();
        return;
      case CastType.Tower:
      case CastType.Flight:
        this.SendFire();
        this.UnselectSpell();
        return;
      case CastType.Placement:
        this.spellTarget = (MyLocation) this.mouseWorldPos;
        this.SendFire();
        this.UnselectSpell();
        return;
      case CastType.Target_Placement:
        if (this.secTarget.IsNull())
        {
          if (HUD.UseTouchControls)
          {
            this.secTarget = (MyLocation) this.meter_subs[6].transform.position;
            HUD.instance?.panelAimControls.SetActive(true);
          }
          else
            this.secTarget = (MyLocation) Camera.main.ScreenToWorldPoint(Input.mousePosition);
          if (!((UnityEngine.Object) this.selectedSpell != (UnityEngine.Object) null) || this.selectedSpell.spellEnum != SpellEnum.Duplication)
            return;
          ZCreature zcreature = this.person.map.PhysicsCollideCreature((ZCreature) null, (int) this.secTarget.x, (int) this.secTarget.y, Inert.mask_Phantom);
          if ((!((ZComponent) zcreature != (object) null) || zcreature.type == CreatureType.Tree ? 0 : (zcreature.race != CreatureRace.Effector ? 1 : 0)) != 0)
          {
            this.ShowSummoningRange(this.selectedSpell, zcreature.radius, ZSpell.GetSpellMaxDistance(this.selectedSpell, this.selected) + zcreature.radius, this.selectedSpell.affectedByGravity);
            return;
          }
          this.secTarget = NullMyLocation.Get();
          return;
        }
        break;
    }
    if (this.target.IsNull())
    {
      if (HUD.UseTouchControls)
      {
        this.target = (MyLocation) this.meter_subs[6].transform.position;
        HUD.instance?.panelAimControls.SetActive(true);
      }
      else
        this.target = (MyLocation) Camera.main.ScreenToWorldPoint(Input.mousePosition);
      this.targetWasSet = false;
      switch (type)
      {
        case CastType.Target_Power:
          if (this.selectedSpell.speedMax == this.selectedSpell.speedMin)
            this.meter_subs[2].SetActive(true);
          else
            this.meter_subs[0].SetActive(true);
          if (this.selectedSpell.spellEnum != SpellEnum.Abduction)
            break;
          this.meter_subs[2].transform.position = (Vector3) this.target.ToSinglePrecision();
          break;
        case CastType.Target_Placement:
          if (!ZSpell.CanFire(this.selectedSpell))
          {
            this.targetWasSet = false;
            this.target = NullMyLocation.Get();
            break;
          }
          if (HUD.UseTouchControls)
            this.target = (MyLocation) this.meter_subs[4].transform.position;
          this.spellTarget = this.target;
          this.SendFire();
          break;
        case CastType.Double_Naplem:
          this.spellTarget = new MyLocation(this.rot_z, (FixedInt) 0);
          this.meter_subs[6].transform.position = this.mouseWorldPos;
          this.meter_subs[6].SetActive(true);
          break;
      }
    }
    else
    {
      switch (type)
      {
        case CastType.Target_Power:
          AudioManager.PlayChargeSpell((int) this.selectedSpell.bookOf);
          break;
        case CastType.Double_Naplem:
          AudioManager.PlayChargeSpell((int) this.selectedSpell.bookOf);
          break;
      }
      this.targetWasSet = true;
    }
  }

  internal void MouseUp()
  {
    if (this.selectingSpell)
    {
      this.selectingSpell = false;
    }
    else
    {
      AudioManager.StopChargeSpell();
      if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null)
        return;
      CastType type = this.selectedSpell.type;
      switch (type)
      {
        case CastType.TargetOnly:
          return;
        case CastType.Target_Power:
        case CastType.Target_Placement:
        case CastType.Double_Naplem:
          if (!this.targetWasSet)
            return;
          break;
        case CastType.Tower:
          return;
        case CastType.Placement:
          return;
        case CastType.Flight:
          return;
        case CastType.Blit:
          return;
      }
      if (!ZSpell.CanFire(this.selectedSpell))
        return;
      this.targetWasSet = false;
      if (type != CastType.Double_Naplem)
        this.spellTarget = this.target;
      this.SendFire();
      this.UnselectSpell();
      this.target = NullMyLocation.Get();
      this.secTarget = NullMyLocation.Get();
    }
  }

  internal void MouseHeld()
  {
    if (this.selectingSpell)
      return;
    if ((UnityEngine.Object) this.selectedSpell == (UnityEngine.Object) null)
    {
      AudioManager.StopChargeSpell();
    }
    else
    {
      CastType type = this.selectedSpell.type;
      if (type == CastType.Placement || (type != CastType.Target_Power && type != CastType.Target_Placement || !this.targetWasSet) && type != CastType.Power || !ZSpell.CanFire(this.selectedSpell) || (double) this.meterFill.fillAmount >= 1.0)
        return;
      this.meterFill.fillAmount += Time.deltaTime * 0.45f;
    }
  }

  private void SendFlipFlop() => this.SendMove((byte) 208);

  private void LogName(byte b)
  {
    if (b != (byte) 204)
    {
      if (b != (byte) 205)
        return;
      Debug.Log((object) "Long Jump");
    }
    else
      Debug.Log((object) "High Jump");
  }

  internal void SendMove(byte b)
  {
    if (this.person.game.tutorialPaused || this.skippingTurn || this.person.game.serverState.busy != ServerState.Busy.No || this.person.game.MoveQue.Count > 0 && b != (byte) 206 || this.selected.inWater && b != (byte) 206)
      return;
    if (this.person.game.ongoing.NumberOfSlowUpdateCoroutines > 0 || this.person.game.MoveQue.Count > 0)
      Debug.Log((object) ("Move que not empty!!!!" + (object) this.person.game.ongoing.NumberOfSlowUpdateCoroutines + " " + (object) this.person.game.MoveQue.Count));
    else if (Client.game.players[this.selectedCreaturePlayerOffset].controlled.Count <= this.selectedCreatureIndex)
    {
      this.NextControlled(true);
    }
    else
    {
      this.movedThisTurn = true;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write(b);
          myBinaryWriter.Write(this.person.id);
          myBinaryWriter.Write(Client.game.gameFacts.id);
          myBinaryWriter.Write(this.person.GetNextMoveID());
          myBinaryWriter.Write((byte) this.selectedCreatureIndex);
          myBinaryWriter.Write((byte) this.selectedCreaturePlayerOffset);
          myBinaryWriter.Write((double) Client.game.players[this.selectedCreaturePlayerOffset].controlled[this.selectedCreatureIndex].transformscale > 0.0 ? (byte) 1 : (byte) 0);
          myBinaryWriter.Write(Client.game.players[this.selectedCreaturePlayerOffset].controlled[this.selectedCreatureIndex].position);
          myBinaryWriter.Write(ExtraMoveBits.Compress(hardInput.GetKey("sprint") || HUD.instance.sprint, hardInput.GetKey("No Glide") || HUD.instance.NoGlide, hardInput.GetKey("NoIceJump") || HUD.instance.NoGlide, this.PressingHighJump(), this.PressingLongJump()));
          if (b == (byte) 211)
          {
            this.UpdateMouseWorldPos(true);
            Vector3 vector3 = this.CurrentMousePos - this.selected.transform.position;
            FixedInt fixedInt = (FixedInt) (Mathf.Atan2(vector3.y, vector3.x) * 57.29578f);
            myBinaryWriter.Write(fixedInt.RawValue);
          }
        }
        Client.SendToGameServer(memoryStream.ToArray());
      }
    }
  }

  internal void SendFire()
  {
    if (this.person.game.ongoing.NumberOfSlowUpdateCoroutines > 0 || this.person.game.MoveQue.Count > 0)
    {
      Debug.Log((object) ("Move que not empty!!!!" + (object) this.person.game.ongoing.NumberOfSlowUpdateCoroutines + " " + (object) this.person.game.MoveQue.Count));
    }
    else
    {
      if (this.person.game.tutorialPaused || this.skippingTurn || this.person.game.MoveQue.Count > 0)
        return;
      Client.game.serverState.busy = ServerState.Busy.Waiting_For_Server_Reply;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 220);
          myBinaryWriter.Write(this.person.id);
          myBinaryWriter.Write(Client.game.gameFacts.id);
          myBinaryWriter.Write(this.person.GetNextMoveID());
          myBinaryWriter.Write((byte) this.selectedCreatureIndex);
          myBinaryWriter.Write((byte) this.selectedCreaturePlayerOffset);
          myBinaryWriter.Write((byte) this.selectedSpellIndex);
          myBinaryWriter.WriteFixed(this.rot_z);
          myBinaryWriter.Write((double) this.selected.transformscale > 0.0);
          myBinaryWriter.Write(this.spellTarget);
          myBinaryWriter.WriteFixed((FixedInt) this.meterFill.fillAmount);
          myBinaryWriter.Write(this.extendedShot);
          myBinaryWriter.Write((int) this.keyDown);
          myBinaryWriter.Write(this.selected.position);
          myBinaryWriter.Write(this.secTarget);
        }
        if (this.selectedSpellIndex < 0 || this.selectedSpellIndex >= this.selected.spells.Count || this.selected.spells[this.selectedSpellIndex].EndsTurn)
        {
          this.UnselectSpell();
          this.selectedCreatureIndex = 0;
          this.selectedCreaturePlayerOffset = (int) this.person.id;
          this.selected = this.person.controlled[0];
        }
        Client.SendToGameServer(memoryStream.ToArray());
      }
      this.Hide_All();
      this.Show_Meter(false);
      this.selectedSpellIndex = -1;
      this.ToggleExtendedShot(hardInput.GetKey("Extended Shot"));
    }
  }

  public class AssignableHotkey
  {
    public ZCreature creature;
    public int slot = -1;
    public KeyCode key;
  }

  public enum PowerLevel
  {
    None,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
  }
}
