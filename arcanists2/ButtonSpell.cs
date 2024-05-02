// Decompiled with JetBrains decompiler
// Type: ButtonSpell
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class ButtonSpell : MonoBehaviour
{
  public Image image;
  public UIOnHover uihover;
  public UIOnHoverChild uiOuter;
  public UIOnHoverChild uiInner;
  public Image restricted;
  private int index;
  private bool equipped;
  private bool interactable = true;
  private int level;
  private int myRealIndex = -1;

  public void SetSprite(Sprite s, int level, bool equipped, bool interactable, int index = -1)
  {
    this.equipped = equipped;
    this.interactable = interactable;
    this.level = level;
    this.image.sprite = s;
    if (interactable)
      this.image.color = Color.white;
    else
      this.image.color = new Color(0.36f, 0.36f, 0.36f, 1f);
    this.uihover.AlwaysOn = equipped;
    if (index >= 0)
      this.myRealIndex = index;
    if (this.uihover.IsHovering)
      this.Hover();
    else
      this.Leave();
    if (!((Object) this.restricted != (Object) null))
      return;
    if (Prestige.IsUnlocked(Client.MyAccount, this.myRealIndex) || !Client.viewSpellLocks.ViewLocked())
    {
      if (!Restrictions.IsSpellRestricted(this.myRealIndex))
      {
        if (Client.viewSpellLocks.ViewRestricted())
        {
          Restrictions restrictions = Server._restrictions;
          if ((restrictions != null ? (restrictions.CheckRestricted(this.myRealIndex) ? 1 : 0) : 0) != 0)
            goto label_13;
        }
        this.restricted.gameObject.SetActive(false);
        return;
      }
label_13:
      this.restricted.gameObject.SetActive(true);
      this.restricted.color = Color.red;
      this.restricted.sprite = ClientResources.Instance.imgRestricted;
    }
    else
    {
      this.restricted.sprite = Prestige.CanUnlock(Client.MyAccount, this.myRealIndex) == 0 ? (this.myRealIndex % 12 >= 10 ? ClientResources.Instance.imgBuyable3 : ClientResources.Instance.imgBuyable2) : ClientResources.Instance.imgLocked;
      this.restricted.color = Color.white;
      this.restricted.gameObject.SetActive(true);
    }
  }

  public void Init(int i)
  {
    this.index = i;
    this.uihover.onClick.AddListener(new UnityAction(this.Click));
    this.uihover.onEnter.AddListener(new UnityAction(this.Hover));
    this.uihover.onExit.AddListener(new UnityAction(this.Leave));
    this.uihover.onRightClick.AddListener(new UnityAction(this.RightClick));
  }

  public void RightClick()
  {
    Spell spell = this.GetSpell();
    if ((Object) spell == (Object) null || (Object) spell.toSummon == (Object) null || !((Object) spell.toSummon?.GetComponent<Creature>() != (Object) null) || !spell.IsMinionSpell())
      return;
    SpellLobbyChange.Instance?.OpenMinion(spell, spell.toSummon.GetComponent<Creature>());
  }

  public void Click()
  {
    if ((Object) SpellLobbyChange.Instance != (Object) null && (Object) SpellLobbyChange.Instance.viewingMinion != (Object) null)
      SpellLobbyChange.Instance.OpenBook(SpellLobbyChange.Instance.openBook);
    else if ((Object) SpellLobbyChange.Instance != (Object) null && SpellLobbyChange.Instance.viewingExtraSpells != BookOf.Nothing)
      SpellLobbyChange.Instance.OpenBook(SpellLobbyChange.Instance.openBook);
    else if (Client.viewSpellLocks.ViewLocked() && !Prestige.IsUnlocked(Client.MyAccount, this.myRealIndex))
    {
      int i = Prestige.CanUnlock(Client.MyAccount, this.myRealIndex);
      if (i == 0)
        Prestige.AskUnlock(this.myRealIndex);
      else
        MyToolTip.Show(Prestige.SpellErrorCode(i), 3f);
    }
    else if ((Object) SpellSelection.Instance != (Object) null)
    {
      if (SpellSelection.Instance.HasSpell((byte) this.index))
      {
        SpellSelection.Instance.RemoveSpell((byte) this.index);
        this.SetSprite(this.image.sprite, this.level, false, this.interactable);
      }
      else
      {
        if (!SpellSelection.Instance.AddSpell((byte) this.index))
          return;
        this.SetSprite(this.image.sprite, this.level, true, this.interactable);
      }
    }
    else
    {
      if (!((Object) SpellLobbyChange.Instance != (Object) null))
        return;
      if (SpellLobbyChange.Instance.HasSpell((byte) this.index))
      {
        SpellLobbyChange.Instance.RemoveSpell((byte) this.index);
        this.SetSprite(this.image.sprite, this.level, false, this.interactable);
      }
      else
      {
        if (!SpellLobbyChange.Instance.AddSpell((byte) this.index))
          return;
        this.SetSprite(this.image.sprite, this.level, true, this.interactable);
      }
    }
  }

  public void UpdatedFromSpellSelection_FullBook()
  {
    if ((Object) SpellSelection.Instance != (Object) null && SpellSelection.Instance.AddSpell((byte) this.index))
      this.SetSprite(this.image.sprite, this.level, true, this.interactable);
    if (!((Object) SpellLobbyChange.Instance != (Object) null) || !SpellLobbyChange.Instance.AddSpell((byte) this.index))
      return;
    this.SetSprite(this.image.sprite, this.level, true, this.interactable);
  }

  public static string Hover(SettingsPlayer sp, int index)
  {
    Spell spell = Inert.Instance.spells[index];
    if (spell.level == 2)
    {
      byte index1 = (byte) (index - 1);
      bool flag = false;
      for (int index2 = 0; index2 < 16; ++index2)
      {
        if ((int) sp.spells[index2] == (int) index1)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return index1 >= (byte) 120 && index1 < (byte) 132 && sp._spells.SeasonsIsHoliday ? "Level 2:\nRequires the level 1 spell: <color=white>" + Inert.Instance.holidaySpells[(int) index1 % 12].name + "</color>" : "Level 2:\nRequires the level 1 spell: <color=white>" + Inert.Instance.spells[(int) index1].name + "</color>";
    }
    else if (spell.level == 3)
    {
      int num = 0;
      bool flag = false;
      for (int index3 = 0; index3 < sp.spells.Length; ++index3)
      {
        if ((int) sp.spells[index3] < Inert.Instance._spells.Length)
        {
          KeyValuePair<string, Spell> keyValuePair = Inert.Instance.spells.GetItem((int) sp.spells[index3]);
          if (keyValuePair.Value.bookOf == spell.bookOf)
          {
            keyValuePair = Inert.Instance.spells.GetItem((int) sp.spells[index3]);
            if (keyValuePair.Value.level < 3)
            {
              ++num;
              if (num >= 5)
              {
                flag = true;
                break;
              }
            }
          }
        }
      }
      if (!flag)
        return index >= 120 && index < 132 && sp._spells.SeasonsIsHoliday ? "Level 3:\nRequires " + (object) (5 - num) + " more <color=white>Holiday</color> spell" + (num < 4 ? (object) "s" : (object) "") : "Level 3:\nRequires " + (object) (5 - num) + " more <color=white>" + spell.bookOf.ToStringX() + "</color> spell" + (num < 4 ? (object) "s" : (object) "");
    }
    return "";
  }

  public static string AddWandStuff(int myRealIndex)
  {
    if (!Client.viewSpellLocks.ViewLocked() || Prestige.IsUnlocked(Client.MyAccount, myRealIndex))
      return "";
    int i = Prestige.CanUnlock(Client.MyAccount, myRealIndex);
    return i == 0 ? "\n<color=green>Buy with </color><color=white>" + (myRealIndex % 12 >= 10 ? "3" : "2") + "</color><color=green> wands.</color>" : "\n" + Prestige.SpellErrorCode(i);
  }

  public Spell GetSpell()
  {
    if ((Object) SpellLobbyChange.Instance.viewingMinion != (Object) null)
      return this.index == 11 ? (Spell) null : SpellLobbyChange.Instance.viewingMinion.spells[this.index].spell;
    if (SpellLobbyChange.Instance.viewingExtraSpells != BookOf.Nothing)
      return SpellLobbyChange.GetFamiliarSpell(this.index);
    return SpellLobbyChange.Instance.settingsPlayer._spells.SeasonsIsHoliday && SpellLobbyChange.Instance.openBook == BookOf.Seasons ? Inert.Instance.holidaySpells[this.index] : Inert.Instance.spells.GetItem(this.index + (int) SpellLobbyChange.Instance.openBook * 12).Value;
  }

  public void Hover()
  {
    if ((Object) SpellLobbyChange.Instance.viewingMinion != (Object) null)
    {
      if (this.index == 11)
      {
        SpellLobbyChange.Instance.textError.text = "";
        SpellLobbyChange.Instance.ShowDescription("Cancel", this.index);
      }
      else
      {
        Spell spell = SpellLobbyChange.Instance.viewingMinion.spells[this.index].spell;
        int index = this.index;
        SpellLobbyChange.Instance.textError.text = SpellLobbyChange.Instance.viewingMinion.name + "'s spell";
        SpellLobbyChange.Instance.ShowDescription(spell.name, index);
      }
    }
    else if (SpellLobbyChange.Instance.viewingExtraSpells != BookOf.Nothing)
    {
      if (this.index == 11)
      {
        SpellLobbyChange.Instance.ShowDescription("Cancel", this.index);
        SpellLobbyChange.Instance.textError.text = "";
      }
      else
      {
        Spell familiarSpell = SpellLobbyChange.GetFamiliarSpell(this.index);
        int index = this.index;
        SpellLobbyChange.Instance.textError.text = "Familiar Spell - Bring the full book and level up the familiar to obtain";
        SpellLobbyChange.Instance.ShowDescription(familiarSpell.name, index);
      }
    }
    else if ((Object) SpellSelection.Instance != (Object) null)
    {
      KeyValuePair<string, Spell> keyValuePair = Inert.Instance.spells.GetItem(this.index + (int) SpellSelection.Instance.openBook * 12);
      int num = (int) SpellSelection.Instance.openBook * 12 + this.index;
      SpellSelection.Instance.textError.text = ButtonSpell.Hover(Client.settingsPlayer, num) + ButtonSpell.AddWandStuff(num);
      SpellSelection.Instance.ShowDescription(keyValuePair.Key);
    }
    else if ((Object) SpellLobbyChange.Instance != (Object) null)
    {
      if (SpellLobbyChange.Instance.settingsPlayer._spells.SeasonsIsHoliday && SpellLobbyChange.Instance.openBook == BookOf.Seasons)
      {
        Spell holidaySpell = Inert.Instance.holidaySpells[this.index];
        SpellLobbyChange.Instance.txtHeader.text = holidaySpell.name;
        int num = (int) SpellLobbyChange.Instance.openBook * 12 + this.index;
        SpellLobbyChange.Instance.textError.text = ButtonSpell.Hover(SpellLobbyChange.Instance.settingsPlayer, num) + ButtonSpell.AddWandStuff(num);
        SpellLobbyChange.Instance.ShowDescription(holidaySpell.name, num);
      }
      else
      {
        KeyValuePair<string, Spell> keyValuePair = Inert.Instance.spells.GetItem(this.index + (int) SpellLobbyChange.Instance.openBook * 12);
        int num = (int) SpellLobbyChange.Instance.openBook * 12 + this.index;
        SpellLobbyChange.Instance.textError.text = ButtonSpell.Hover(SpellLobbyChange.Instance.settingsPlayer, num) + ButtonSpell.AddWandStuff(num);
        SpellLobbyChange.Instance.ShowDescription(keyValuePair.Key, num);
      }
    }
    if (this.equipped)
    {
      this.uiInner.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      if (this.level == 1)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      else if (this.level == 2)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      else if (this.level == 3)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      else
        Debug.LogError((object) ("Unknown Spell Level: " + (object) this.level));
    }
    else
    {
      this.uiInner.GetImage.color = (Color) new Color32((byte) 212, (byte) 208, (byte) 200, byte.MaxValue);
      if (this.level == 1)
        this.uiOuter.GetImage.color = (Color) new Color32((byte) 157, (byte) 146, (byte) 131, byte.MaxValue);
      else if (this.level == 2)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
      else if (this.level == 3)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, (byte) 128, byte.MaxValue, byte.MaxValue);
      else
        Debug.LogError((object) ("Unknown Spell Level: " + (object) this.level));
    }
  }

  public void Leave()
  {
    MyToolTip.Close();
    if ((Object) SpellSelection.Instance != (Object) null)
      SpellSelection.Instance.LeaveSpell();
    else if ((Object) SpellLobbyChange.Instance != (Object) null)
      SpellLobbyChange.Instance.LeaveSpell();
    if (this.equipped)
    {
      this.uiInner.GetImage.color = (Color) new Color32((byte) 212, (byte) 208, (byte) 200, byte.MaxValue);
      if (this.level == 1)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      else if (this.level == 2)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      else if (this.level == 3)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      else
        Debug.LogError((object) ("Unknown Spell Level: " + (object) this.level));
    }
    else
    {
      this.uiInner.GetImage.color = (Color) new Color32((byte) 212, (byte) 208, (byte) 200, byte.MaxValue);
      if (this.level == 1)
        this.uiOuter.GetImage.color = (Color) new Color32((byte) 157, (byte) 146, (byte) 131, byte.MaxValue);
      else if (this.level == 2)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
      else if (this.level == 3)
        this.uiOuter.GetImage.color = (Color) new Color32(byte.MaxValue, (byte) 128, byte.MaxValue, byte.MaxValue);
      else
        Debug.LogError((object) ("Unknown Spell Level: " + (object) this.level));
    }
  }
}
