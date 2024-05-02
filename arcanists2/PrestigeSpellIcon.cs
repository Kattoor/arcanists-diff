// Decompiled with JetBrains decompiler
// Type: PrestigeSpellIcon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class PrestigeSpellIcon : MonoBehaviour
{
  public UIOnHover button;
  public Image image;
  private int index;

  public void Init(int e)
  {
    this.index = e;
    this.name = Inert.Instance._spells[e].name;
    this.image.sprite = ClientResources.Instance.icons[this.name];
  }

  public void OnHover()
  {
    if (!((Object) SpellLobbyChange.Instance != (Object) null))
      return;
    SpellLobbyChange.Instance.HoverSpell(this.name, this.index);
    SpellLobbyChange.Instance.textError.text = ButtonSpell.Hover(SpellLobbyChange.Instance.settingsPlayer, this.index) + ButtonSpell.AddWandStuff(this.index);
  }

  public void OnExit() => SpellLobbyChange.Instance?.HideMouseOver();
}
