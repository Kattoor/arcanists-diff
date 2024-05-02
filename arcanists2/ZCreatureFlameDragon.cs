// Decompiled with JetBrains decompiler
// Type: ZCreatureFlameDragon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
      this.ApplyHeal(DamageType.Snow, 1, enemy);
    return 0;
  }
}
