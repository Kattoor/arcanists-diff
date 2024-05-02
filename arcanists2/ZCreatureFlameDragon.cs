

using UnityEngine;

#nullable disable
public class ZCreatureFlameDragon : ZCreature
{
  public DamageType ImmuneTo = DamageType.Napalm;

  public override int ApplyDamage(
    SpellEnum spellEnum,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
    if (dt != this.ImmuneTo || this.ImmuneTo == DamageType.None)
      return base.ApplyDamage(spellEnum, dt, damage, enemy, TurnCreated, spellRef);
    if (dt == DamageType.Snow)
      this.ApplyHeal(DamageType.Snow, Mathf.Max(1, damage / 2), enemy);
    return 0;
  }
}
