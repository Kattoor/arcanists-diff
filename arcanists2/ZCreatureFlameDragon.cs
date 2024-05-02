// Decompiled with JetBrains decompiler
// Type: ZCreatureFlameDragon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
