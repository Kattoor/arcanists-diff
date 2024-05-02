

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class SpellSlotButton : MonoBehaviour
{
  public Image image;
  public UIOnHover uihover;
  public Image restricted;
  private int index;

  public void SetSprite(Sprite s, int index)
  {
    this.image.sprite = s;
    this.image.enabled = true;
    if (!((Object) this.restricted != (Object) null))
      return;
    if ((Object) SpellLobbyChange.Instance != (Object) null && !SpellLobbyChange.Instance.validating)
    {
      this.restricted.gameObject.SetActive(false);
    }
    else
    {
      GameObject gameObject = this.restricted.gameObject;
      int num;
      if (!Restrictions.IsSpellRestricted(index))
      {
        if (Client.viewSpellLocks.ViewRestricted())
        {
          Restrictions restrictions = Server._restrictions;
          num = restrictions != null ? (restrictions.CheckRestricted(index) ? 1 : 0) : 0;
        }
        else
          num = 0;
      }
      else
        num = 1;
      gameObject.SetActive(num != 0);
      if (this.restricted.gameObject.activeSelf)
        this.restricted.sprite = ClientResources.Instance.imgRestricted;
      if (!Client.viewSpellLocks.ViewLocked() || Prestige.IsUnlocked(Client.MyAccount, index))
        return;
      this.restricted.gameObject.SetActive(true);
      this.restricted.sprite = ClientResources.Instance.imgLocked;
    }
  }

  public void Empty()
  {
    this.image.enabled = false;
    this.restricted?.gameObject.SetActive(false);
  }

  public void Init(int i)
  {
    this.index = i;
    this.uihover.onClick.AddListener(new UnityAction(this.ClickRemove));
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

  public static bool IsMinionSpell(int index)
  {
    Spell spell = Inert.Instance._spells[index];
    return (Object) spell.toSummon?.GetComponent<Creature>() != (Object) null && spell.IsMinionSpell();
  }

  public void ClickRemove()
  {
    this.Empty();
    this.Leave();
    SpellSelection.Instance?.RemoveSpellSlot(this.index, true);
    SpellLobbyChange.Instance?.RemoveSpellSlot(this.index, true);
  }

  public Spell GetSpell()
  {
    if (SpellLobbyChange.Instance.settingsPlayer._spells.SeasonsIsHoliday && SpellLobbyChange.Instance.openBook == BookOf.Seasons)
    {
      if (SpellLobbyChange.Instance.settingsPlayer.spells[this.index] < byte.MaxValue && (int) SpellLobbyChange.Instance.settingsPlayer.spells[this.index] < Inert.Instance.spells.Count)
        return Inert.Instance.spells.GetItem((int) SpellLobbyChange.Instance.settingsPlayer.spells[this.index]).Value;
    }
    else if (SpellLobbyChange.Instance.settingsPlayer.spells[this.index] < byte.MaxValue && (int) SpellLobbyChange.Instance.settingsPlayer.spells[this.index] < Inert.Instance.spells.Count)
      return Inert.Instance.spells.GetItem((int) SpellLobbyChange.Instance.settingsPlayer.spells[this.index]).Value;
    return (Spell) null;
  }

  public void Hover()
  {
    SpellSelection.Instance?.HoverSpellSlot(this.index);
    SpellLobbyChange.Instance?.HoverSpellSlot(this.index);
  }

  public void Leave()
  {
    if (Global.OS.Is(OperatingSystem.Android))
      return;
    SpellSelection.Instance?.LeaveSpell();
    SpellLobbyChange.Instance?.LeaveSpell();
  }
}
