// Decompiled with JetBrains decompiler
// Type: NewSpellSelection
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class NewSpellSelection : MonoBehaviour
{
  public SpellLobbyChange spellLobby;

  private void Awake()
  {
    Client.viewSpellLocks = (ViewSpellLocks) PlayerPrefs.GetInt("prefviewinglocked", 1);
    this.spellLobby.Init(Client.settingsPlayer, (Action<SettingsPlayer>) null);
  }

  private void Start() => DiscordIntergration.Instance?.UpdateActivitySpellSelection();

  public void ClickLoad() => this.spellLobby.ClicOpen();

  public void ClickSave() => this.spellLobby.ClickSave();

  private void Equip()
  {
    Client.settingsPlayer.CopySpells(this.spellLobby.settingsPlayer, true);
    Inert.SaveSettingsPlayer();
  }

  public void ClickMainMenu()
  {
    this.Equip();
    Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
  }

  public void ClickCharacterCreation()
  {
    this.Equip();
    if ((UnityEngine.Object) Controller.Instance == (UnityEngine.Object) null)
      SceneManager.LoadScene("CharacterCreation");
    else
      Controller.Instance.OpenMenu(Controller.Instance.MenuCharacterCreation, false);
  }
}
