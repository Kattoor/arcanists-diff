// Decompiled with JetBrains decompiler
// Type: MultiSelectConfig
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class MultiSelectConfig : MonoBehaviour
{
  public SpellImageList spells;
  public SettingsPlayer settings;

  public void ClickChangeSpells()
  {
    SpellLobbyChange.Create(this.settings, (Action<SettingsPlayer>) (s =>
    {
      this.settings.CopySpells(s, true);
      this.spells.SetSpells(s);
    }));
  }

  public void ClickResetSpells()
  {
    this.settings.CopySpells(Client.settingsPlayer, true);
    this.spells.SetSpells(this.settings);
  }

  public void ClickChangeOutfit()
  {
    ChangeOutfitMenu.Create(false, sp: this.settings, onEnd: (Action<SettingsPlayer>) (s =>
    {
      this.settings.CopyOutfit(s);
      this.UpdateOutfit();
    }));
  }

  public void ClickResetOutfit()
  {
    this.settings.CopyOutfit(Client.settingsPlayer);
    this.UpdateOutfit();
  }

  internal void UpdateOutfit()
  {
    this.settings.VerifyOutfit(Client.cosmetics);
    ConfigurePlayer.EquipAll(Client.Name, this.spells.uIPlayerCharacter, this.settings);
  }

  public void ClickElementalIcon()
  {
    ElementalSelection.Create((RectTransform) Controller.Instance.transform, this.settings._spells.SeasonsIsHoliday, (Action<BookOf>) (b =>
    {
      this.settings.fullBook = (byte) (b + 1);
      this.spells.SetSpells(this.settings);
    }), true);
  }
}
