// Decompiled with JetBrains decompiler
// Type: ZCreatureImp
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ZCreatureImp : ZCreature
{
  public override int ApplyDamage(
    SpellEnum spellEnum,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
    if (dt != DamageType.Arcane)
      return base.ApplyDamage(spellEnum, dt, damage, enemy, TurnCreated, spellRef);
    if ((ZComponent) this.tower != (object) null)
      this.tower.ApplyDamage(spellEnum, dt, damage, enemy, TurnCreated, spellRef);
    else
      this.ApplyHeal(dt, damage, enemy);
    return 0;
  }

  public override void OnDeath(bool playDeathClip = true)
  {
    if (!this.isDead && this.parent != null && (ZComponent) this.parent.first() != (object) null && this.parent.first().FullArcane && !this.parent.game.originalSpellsOnly && (this.spellEnum == SpellEnum.Summon_Imps || this.spellEnum == SpellEnum.Little_Devil))
      ZSpell.FireWhich(Inert.Instance.arcaneMist, this.parent.first(), this.position, (FixedInt) 0, (FixedInt) 0, this.position, NullMyLocation.Get());
    base.OnDeath(true);
  }
}
