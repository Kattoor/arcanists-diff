

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZCreatureJavelin : ZCreature
{
  public ZSpell spell;

  private void RemoveEffector()
  {
    if (this.game.isClient && !this.game.resyncing && (Object) this.deathExplosion != (Object) null)
      ZComponent.Instantiate<GameObject>(this.deathExplosion, this.position.ToSinglePrecision(), Quaternion.identity);
    this.auraOfDecay?.DestroyNoExplosion();
  }

  public override int ApplyDamage(
    SpellEnum spellEnum,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
    if (dt == DamageType.Heal || this.health == 0 || this.isDead || ZComponent.IsNull((ZComponent) this))
      return 0;
    this.isDead = true;
    this.isNull = true;
    this.health = 0;
    if ((ZComponent) this.auraOfDecay == (object) null)
      return 0;
    this.collider.Disable();
    this.RemoveEffector();
    ZComponent.Destroy<GameObject>(this.gameObject);
    this.game.forceRysncPause = true;
    return 0;
  }

  public override void ApplyExplosionForce(
    int spellRadius,
    MyLocation origin,
    FixedInt force,
    int radius,
    Curve factor,
    bool startMoving = false)
  {
  }

  public override void Start()
  {
  }

  internal override void Awake()
  {
  }

  public override bool ShouldFall(bool gliding = true, bool ignoreFlight = false) => false;

  public override IEnumerator<float> Move(bool fromSerialization = false)
  {
    yield break;
  }

  public override void SetVelocity(int x, int y)
  {
  }

  public override void Fall(bool ignoreFlight = false)
  {
  }

  public override void StartMoving(bool fromInput = false)
  {
  }

  public override void ApplyHeal(DamageType dt, int damage, ZCreature enemy)
  {
  }
}
