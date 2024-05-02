

using System.Collections.Generic;

#nullable disable
public class ZCreatureClockworkBomb : ZCreature
{
  public ZSpellClockworkBomb spell;

  public ZCreatureClockworkBomb()
  {
  }

  public ZCreatureClockworkBomb(Creature s) => this.Copy(s);

  public override int ApplyDamage(
    SpellEnum spellEnum,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
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
    this.velocity.x = (FixedInt) 0;
    this.velocity.y = (FixedInt) -1;
  }

  public override void Start()
  {
  }

  internal override void Awake() => this.collider.enabled = false;

  public override IEnumerator<float> Move(bool fromSerialization = false)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    ZCreatureClockworkBomb zcreatureClockworkBomb = this;
    if (num != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    zcreatureClockworkBomb.Fall(false);
    return false;
  }

  public override void SetVelocity(int x, int y)
  {
    this.velocity.x = (FixedInt) 0;
    this.velocity.y = (FixedInt) -1;
  }

  public override void Fall(bool ignoreFlight = false)
  {
    if (this.spell.isMoving)
      return;
    this.velocity.y = (FixedInt) -1;
    this.spell.velocity = this.velocity;
    this.spell.moving = this.game.ongoing.RunSpell(this.spell.SpellMove(false, true));
  }

  public override void StartMoving(bool fromInput = false) => this.Fall(false);

  public override void ApplyHeal(DamageType dt, int damage, ZCreature enemy)
  {
  }
}
