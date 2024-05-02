// Decompiled with JetBrains decompiler
// Type: SpellSelection
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
public class SpellSelection : Catalogue
{
  public Inert inert;
  public ClientResources clientResources;
  public SpellSlotButton[] spellSlotbuttons;
  public Image imageElemental;
  public ButtonSpell[] buttonSpells;
  public Image bookIcon;
  public Image prestigeIcon;
  public TMP_Text headerMouseoverSpell;
  public TMP_Text textMouseoverSpell;
  public TMP_Text textMouseoverSpellExtra;
  public TMP_Text textError;
  public Color colorInnerDefault;
  public Color colorInnerHover;
  public Color colorInnerActiveDefault;
  public Color colorInnerActiveHover;
  public Color color1Default;
  public Color color1Hover;
  public Color color1ActiveDefault;
  public Color color1ActiveHover;
  public Color color2Default;
  public Color color2Hover;
  public Color color2ActiveDefault;
  public Color color2ActiveHover;
  public Color color3Default;
  public Color color3Hover;
  public Color color3ActiveDefault;
  public Color color3ActiveHover;
  [NonSerialized]
  public BookOf openBook;

  public static SpellSelection Instance { get; private set; }

  public SettingsPlayer settingsPlayer => Client.settingsPlayer;

  private void Start()
  {
    Inert.Init(this.inert);
    ClientResources.Init(this.clientResources);
    SpellSelection.Instance = this;
    for (int i = 0; i < 16; ++i)
      this.spellSlotbuttons[i].Init(i);
    for (int i = 0; i < 12; ++i)
      this.buttonSpells[i].Init(i);
    this.RefreshList();
    if ((UnityEngine.Object) this.prestigeIcon != (UnityEngine.Object) null)
      this.prestigeIcon.sprite = ClientResources.Instance._iconsPrestige[Mathf.Clamp((int) Client.MyAccount.prestige, 0, 10)];
    DiscordIntergration.Instance?.UpdateActivitySpellSelection();
  }

  public void ClickPrestige() => Controller.ShowPrestigeMenu();

  public void ClickElemental()
  {
    ElementalSelection.Create((RectTransform) this.transform, false, (Action<BookOf>) (b =>
    {
      this.settingsPlayer.fullBook = (byte) (b + 1);
      this.UpdateElementalIcon();
    }), true);
  }

  public void UpdateElementalIcon()
  {
    if ((int) this.settingsPlayer.fullBook >= ClientResources.Instance.spellBookIcons.Length)
      this.settingsPlayer.fullBook = (byte) 0;
    this.imageElemental.sprite = ClientResources.Instance.spellBookIcons[(int) this.settingsPlayer.fullBook];
  }

  public void ToolTip(string s) => MyToolTip.Show(s);

  public void HideToolTip() => MyToolTip.Close();

  public void ClickMainMenu() => Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);

  public void ClickCharacterCreation()
  {
    if ((UnityEngine.Object) Controller.Instance == (UnityEngine.Object) null)
      SceneManager.LoadScene("CharacterCreation");
    else
      Controller.Instance.OpenMenu(Controller.Instance.MenuCharacterCreation, false);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) SpellSelection.Instance == (UnityEngine.Object) this)
      SpellSelection.Instance = (SpellSelection) null;
    Inert.SaveSettingsPlayer();
  }

  [EnumAction(typeof (BookOf))]
  public void OpenBook(int i) => this.OpenBook((BookOf) i);

  public void OpenBook(BookOf b)
  {
    if (this.openBook != b)
    {
      this.bookIcon.sprite = ClientResources.Instance.spellBookIcons[(int) (b + 1)];
      this.openBook = b;
    }
    int index = (int) b * 12;
    int num = index + 12;
    int spellID = 0;
    while (index < num)
    {
      Spell spell = Inert.Instance.spells.GetItem(index).Value;
      bool equipped = this.HasSpell((byte) spellID);
      this.buttonSpells[spellID].SetSprite(ClientResources.Instance.icons.GetItem(index).Value, spell.level, equipped, equipped || this.settingsPlayer.CanEquipSpell(15, (byte) (spellID + (int) this.openBook * 12)), index);
      ++index;
      ++spellID;
    }
  }

  [EnumAction(typeof (BookOf))]
  public void HoverBookIcon(int i)
  {
    this.textMouseoverSpell.text = Descriptions.GetBookHeader((BookOf) i) + "\nFamiliar - " + Descriptions.GetBookDescription((BookOf) i);
    this.textMouseoverSpellExtra.text = "";
    this.headerMouseoverSpell.text = i == 0 ? "Arcane Book" : "Book of " + ((BookOf) i).ToStringX();
    this.textError.text = "";
  }

  public void HoverSelectedBook()
  {
    if (this.openBook == BookOf.Nothing)
      return;
    int openBook = (int) this.openBook;
    this.textMouseoverSpell.text = Descriptions.GetBookHeader((BookOf) openBook) + "\nFamiliar - " + Descriptions.GetBookDescription((BookOf) openBook);
    this.textMouseoverSpellExtra.text = "";
    this.headerMouseoverSpell.text = openBook == 0 ? "Arcane Book" : "Book of " + ((BookOf) openBook).ToStringX();
    this.textError.text = "";
  }

  public void ClickFullBook()
  {
    int length = this.settingsPlayer.spells.Length;
    for (int index = 0; index < this.settingsPlayer.spells.Length && index < 12; ++index)
    {
      if (this.settingsPlayer.spells[index] != byte.MaxValue)
        --length;
    }
    for (int spellID = 0; spellID < 12 && spellID < length; ++spellID)
    {
      if (SpellSelection.Instance.HasSpell((byte) spellID))
        ++length;
      else
        this.buttonSpells[spellID].UpdatedFromSpellSelection_FullBook();
    }
  }

  public void RefreshList()
  {
    for (int index = 0; index < 16; ++index)
    {
      if (!this.settingsPlayer.CanEquipSpell(index, this.settingsPlayer.spells[index]) && this.settingsPlayer.spells[index] != byte.MaxValue)
      {
        this.RemoveSpellSlot(index, false);
        --index;
      }
    }
    for (int index = 0; index < 16; ++index)
    {
      if (this.settingsPlayer.spells[index] != byte.MaxValue)
        this.spellSlotbuttons[index].SetSprite(ClientResources.Instance.icons.GetItem((int) this.settingsPlayer.spells[index]).Value, (int) this.settingsPlayer.spells[index]);
      else
        this.spellSlotbuttons[index].Empty();
    }
    this.OpenBook(this.openBook);
    this.UpdateElementalIcon();
  }

  public bool HasSpell(byte spellID)
  {
    spellID += (byte) ((uint) this.openBook * 12U);
    for (int index = 0; index < 16; ++index)
    {
      if ((int) this.settingsPlayer.spells[index] == (int) spellID)
        return true;
    }
    return false;
  }

  public bool AddSpell(byte spellID)
  {
    spellID += (byte) ((uint) this.openBook * 12U);
    for (int index = 0; index < 16; ++index)
    {
      if (this.settingsPlayer.spells[index] == byte.MaxValue)
      {
        if (!this.settingsPlayer.CanEquipSpell(index, spellID))
          return false;
        for (; index > 0 && (int) this.settingsPlayer.spells[index - 1] > (int) spellID; --index)
          this.settingsPlayer.spells[index] = this.settingsPlayer.spells[index - 1];
        this.settingsPlayer.spells[index] = spellID;
        this.RefreshList();
        return true;
      }
    }
    return false;
  }

  public void RemoveSpell(byte spellID)
  {
    spellID += (byte) ((uint) this.openBook * 12U);
    for (int i = 0; i < 16; ++i)
    {
      if ((int) this.settingsPlayer.spells[i] == (int) spellID)
      {
        this.RemoveSpellSlot(i, true);
        break;
      }
    }
  }

  public void RemoveSpellSlot(int i, bool refresh)
  {
    for (; i < 15 && this.settingsPlayer.spells[i + 1] < byte.MaxValue; ++i)
      this.settingsPlayer.spells[i] = this.settingsPlayer.spells[i + 1];
    this.settingsPlayer.spells[i] = byte.MaxValue;
    if (!refresh)
      return;
    this.RefreshList();
  }

  public void ClearSpells()
  {
    for (int index = 0; index < this.settingsPlayer.spells.Length; ++index)
      this.settingsPlayer.spells[index] = byte.MaxValue;
    this.settingsPlayer.spells[0] = (byte) 4;
    this.RefreshList();
  }

  public void HoverSpellSlot(int i)
  {
    if (this.settingsPlayer.spells[i] >= byte.MaxValue || (int) this.settingsPlayer.spells[i] >= Inert.Instance.spells.Count)
      return;
    this.ShowDescription(Inert.Instance.spells.GetItem((int) this.settingsPlayer.spells[i]).Key);
    this.textError.text = "";
  }

  public void ShowDescription(string s)
  {
    this.headerMouseoverSpell.text = s;
    (this.textMouseoverSpell.text, this.textMouseoverSpellExtra.text) = Descriptions.GetSpellDescription(s);
  }

  public void LeaveSpell()
  {
    if (Global.OS.Is(OperatingSystem.Android))
      return;
    this.headerMouseoverSpell.text = "";
    this.textMouseoverSpell.text = this.DefaultDescription();
    this.textMouseoverSpellExtra.text = "";
    this.textError.text = "";
  }

  public void ClickSavedSpells(bool v) => ChangeSpellBookMenu.Create(v, this.settingsPlayer);

  private string DefaultDescription()
  {
    return this.openBook != BookOf.Arcane ? "Choose the spells you wish your Arcanist to take into battle.\n\nSelect a spellbook from the top of the screen, then select your spells from the wheel to the left. You can remove spells from your spellbook by clicking on the icons above.\nSome spells are locked until you meet their requirements - select grayed-out spells for more information." : "Choose the spells you wish your Arcanist to take into battle.\nSelect a spellbook from the top of the screen, then select your spells from the wheel to the left. You can remove spells from your spellbook by clicking on the icons above.";
  }
}
