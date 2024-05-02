

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
