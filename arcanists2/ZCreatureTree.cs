

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZCreatureTree : ZCreature
{
  public CreatureTree baseTree;
  internal static Coords[] coords = new Coords[279]
  {
    new Coords(-70, 27),
    new Coords(-69, 25),
    new Coords(-68, 24),
    new Coords(-67, 23),
    new Coords(-67, 56),
    new Coords(-66, 22),
    new Coords(-66, 54),
    new Coords(-65, 21),
    new Coords(-65, 53),
    new Coords(-64, 20),
    new Coords(-64, 52),
    new Coords(-63, 19),
    new Coords(-63, 51),
    new Coords(-62, 19),
    new Coords(-62, 51),
    new Coords(-61, 19),
    new Coords(-61, 50),
    new Coords(-60, 19),
    new Coords(-59, 19),
    new Coords(-58, 20),
    new Coords(-57, 21),
    new Coords(-56, 22),
    new Coords(-55, 2),
    new Coords(-54, -1),
    new Coords(-53, -3),
    new Coords(-52, -4),
    new Coords(-51, -5),
    new Coords(-50, -6),
    new Coords(-49, -7),
    new Coords(-48, -7),
    new Coords(-47, -7),
    new Coords(-46, -7),
    new Coords(-45, -7),
    new Coords(-44, -6),
    new Coords(-43, -6),
    new Coords(-42, -5),
    new Coords(-41, -3),
    new Coords(-41, 15),
    new Coords(-40, 0),
    new Coords(-40, 13),
    new Coords(-39, 12),
    new Coords(-38, 11),
    new Coords(-37, 11),
    new Coords(-36, 10),
    new Coords(-36, 81),
    new Coords(-35, 10),
    new Coords(-35, 80),
    new Coords(-34, 10),
    new Coords(-34, 79),
    new Coords(-33, 10),
    new Coords(-33, 78),
    new Coords(-32, 11),
    new Coords(-32, 22),
    new Coords(-31, 12),
    new Coords(-31, 21),
    new Coords(-30, 20),
    new Coords(-29, 19),
    new Coords(-28, 18),
    new Coords(-27, 18),
    new Coords(-26, 17),
    new Coords(-25, 16),
    new Coords(-24, 16),
    new Coords(-23, 15),
    new Coords(-22, 15),
    new Coords(-21, 15),
    new Coords(-21, 30),
    new Coords(-20, 15),
    new Coords(-20, 30),
    new Coords(-19, 15),
    new Coords(-19, 31),
    new Coords(-18, -73),
    new Coords(-18, 16),
    new Coords(-18, 32),
    new Coords(-17, -77),
    new Coords(-17, 16),
    new Coords(-17, 33),
    new Coords(-16, -79),
    new Coords(-16, 17),
    new Coords(-16, 37),
    new Coords(-15, -81),
    new Coords(-15, 19),
    new Coords(-15, 36),
    new Coords(-14, -82),
    new Coords(-14, 36),
    new Coords(-13, -84),
    new Coords(-13, 35),
    new Coords(-12, -85),
    new Coords(-12, 36),
    new Coords(-11, -85),
    new Coords(-11, 36),
    new Coords(-10, -86),
    new Coords(-10, 36),
    new Coords(-10, 44),
    new Coords(-9, -87),
    new Coords(-9, 36),
    new Coords(-9, 46),
    new Coords(-8, -88),
    new Coords(-8, 36),
    new Coords(-8, 48),
    new Coords(-7, -88),
    new Coords(-7, 36),
    new Coords(-7, 47),
    new Coords(-6, -89),
    new Coords(-6, 36),
    new Coords(-6, 46),
    new Coords(-5, -89),
    new Coords(-5, 36),
    new Coords(-5, 45),
    new Coords(-4, -90),
    new Coords(-4, 35),
    new Coords(-4, 45),
    new Coords(-3, -90),
    new Coords(-3, 35),
    new Coords(-3, 45),
    new Coords(-2, -91),
    new Coords(-2, 30),
    new Coords(-2, 46),
    new Coords(-1, -91),
    new Coords(-1, 28),
    new Coords(-1, 47),
    new Coords(0, -91),
    new Coords(0, 27),
    new Coords(0, 48),
    new Coords(1, -91),
    new Coords(1, 26),
    new Coords(1, 50),
    new Coords(2, -92),
    new Coords(2, 25),
    new Coords(2, 52),
    new Coords(3, -92),
    new Coords(3, 25),
    new Coords(3, 54),
    new Coords(4, -92),
    new Coords(4, 25),
    new Coords(4, 54),
    new Coords(5, -92),
    new Coords(5, 25),
    new Coords(5, 53),
    new Coords(6, -92),
    new Coords(6, 25),
    new Coords(6, 52),
    new Coords(7, -92),
    new Coords(7, 25),
    new Coords(7, 51),
    new Coords(8, -92),
    new Coords(8, 26),
    new Coords(8, 51),
    new Coords(9, -92),
    new Coords(9, 27),
    new Coords(9, 51),
    new Coords(10, -92),
    new Coords(10, 27),
    new Coords(10, 51),
    new Coords(11, -92),
    new Coords(11, 28),
    new Coords(11, 52),
    new Coords(12, -92),
    new Coords(12, 28),
    new Coords(12, 52),
    new Coords(13, -92),
    new Coords(13, 29),
    new Coords(13, 53),
    new Coords(14, -92),
    new Coords(14, 29),
    new Coords(14, 55),
    new Coords(15, -92),
    new Coords(15, 29),
    new Coords(15, 58),
    new Coords(16, -92),
    new Coords(16, 29),
    new Coords(16, 57),
    new Coords(17, -92),
    new Coords(17, 29),
    new Coords(17, 54),
    new Coords(18, -92),
    new Coords(18, 29),
    new Coords(19, -92),
    new Coords(19, 29),
    new Coords(20, -92),
    new Coords(20, 29),
    new Coords(21, -92),
    new Coords(21, 29),
    new Coords(22, -92),
    new Coords(22, 29),
    new Coords(23, -92),
    new Coords(23, 28),
    new Coords(24, -92),
    new Coords(24, 28),
    new Coords(24, 51),
    new Coords(25, -92),
    new Coords(25, 28),
    new Coords(25, 54),
    new Coords(25, 56),
    new Coords(25, 72),
    new Coords(26, -92),
    new Coords(26, 27),
    new Coords(26, 57),
    new Coords(27, -92),
    new Coords(27, 27),
    new Coords(27, 59),
    new Coords(28, -92),
    new Coords(28, 27),
    new Coords(28, 60),
    new Coords(29, -92),
    new Coords(29, 26),
    new Coords(30, -92),
    new Coords(30, 26),
    new Coords(31, -92),
    new Coords(31, 25),
    new Coords(32, -91),
    new Coords(32, 24),
    new Coords(33, -91),
    new Coords(33, 23),
    new Coords(34, -91),
    new Coords(34, 23),
    new Coords(35, -91),
    new Coords(35, 22),
    new Coords(36, -90),
    new Coords(36, 20),
    new Coords(37, -90),
    new Coords(37, 19),
    new Coords(38, -89),
    new Coords(38, 17),
    new Coords(39, -89),
    new Coords(39, 15),
    new Coords(40, -88),
    new Coords(41, -88),
    new Coords(42, -87),
    new Coords(43, -86),
    new Coords(44, -85),
    new Coords(45, -85),
    new Coords(46, -84),
    new Coords(46, -14),
    new Coords(47, -82),
    new Coords(47, -8),
    new Coords(48, -81),
    new Coords(48, -3),
    new Coords(49, -79),
    new Coords(49, 1),
    new Coords(50, -77),
    new Coords(50, 4),
    new Coords(51, -73),
    new Coords(51, -58),
    new Coords(51, 6),
    new Coords(52, -53),
    new Coords(52, 9),
    new Coords(53, 10),
    new Coords(54, 9),
    new Coords(55, 8),
    new Coords(56, 8),
    new Coords(57, 7),
    new Coords(58, 7),
    new Coords(58, 30),
    new Coords(59, 7),
    new Coords(59, 21),
    new Coords(60, 7),
    new Coords(60, 21),
    new Coords(60, 31),
    new Coords(61, 7),
    new Coords(61, 21),
    new Coords(61, 31),
    new Coords(62, 8),
    new Coords(62, 20),
    new Coords(62, 32),
    new Coords(63, 9),
    new Coords(63, 20),
    new Coords(63, 33),
    new Coords(64, 10),
    new Coords(64, 20),
    new Coords(64, 34),
    new Coords(65, 21),
    new Coords(65, 35),
    new Coords(66, 21),
    new Coords(66, 36),
    new Coords(67, 22),
    new Coords(67, 37),
    new Coords(68, 22),
    new Coords(69, 23),
    new Coords(70, 25)
  };
  internal static Coords[] top_coords = new Coords[265]
  {
    new Coords(-70, 35),
    new Coords(-69, 38),
    new Coords(-68, 40),
    new Coords(-67, 66),
    new Coords(-67, 42),
    new Coords(-66, 67),
    new Coords(-66, 44),
    new Coords(-65, 68),
    new Coords(-65, 45),
    new Coords(-64, 69),
    new Coords(-64, 47),
    new Coords(-63, 70),
    new Coords(-63, 48),
    new Coords(-62, 71),
    new Coords(-62, 49),
    new Coords(-61, 72),
    new Coords(-61, 50),
    new Coords(-60, 72),
    new Coords(-59, 73),
    new Coords(-58, 73),
    new Coords(-57, 73),
    new Coords(-56, 74),
    new Coords(-55, 74),
    new Coords(-54, 74),
    new Coords(-53, 75),
    new Coords(-52, 75),
    new Coords(-51, 75),
    new Coords(-50, 75),
    new Coords(-49, 76),
    new Coords(-48, 76),
    new Coords(-47, 76),
    new Coords(-46, 76),
    new Coords(-45, 76),
    new Coords(-44, 77),
    new Coords(-43, 77),
    new Coords(-42, 77),
    new Coords(-41, 77),
    new Coords(-41, 13),
    new Coords(-40, 77),
    new Coords(-40, 6),
    new Coords(-39, 77),
    new Coords(-38, 77),
    new Coords(-37, 77),
    new Coords(-36, 88),
    new Coords(-36, 77),
    new Coords(-35, 90),
    new Coords(-35, 78),
    new Coords(-34, 91),
    new Coords(-34, 78),
    new Coords(-33, 92),
    new Coords(-33, 78),
    new Coords(-32, 92),
    new Coords(-32, 22),
    new Coords(-31, 20),
    new Coords(-23, 92),
    new Coords(-22, 92),
    new Coords(-21, 92),
    new Coords(-21, 30),
    new Coords(-20, 91),
    new Coords(-20, 30),
    new Coords(-19, 91),
    new Coords(-19, 29),
    new Coords(-18, 91),
    new Coords(-18, 28),
    new Coords(-18, -63),
    new Coords(-17, 90),
    new Coords(-17, 27),
    new Coords(-17, -59),
    new Coords(-16, 90),
    new Coords(-16, 26),
    new Coords(-16, -56),
    new Coords(-15, 90),
    new Coords(-15, 24),
    new Coords(-15, -53),
    new Coords(-14, 89),
    new Coords(-14, -51),
    new Coords(-13, 89),
    new Coords(-13, -48),
    new Coords(-12, 88),
    new Coords(-12, -47),
    new Coords(-11, 88),
    new Coords(-11, -45),
    new Coords(-10, 87),
    new Coords(-10, 44),
    new Coords(-10, -43),
    new Coords(-9, 87),
    new Coords(-9, 43),
    new Coords(-9, -41),
    new Coords(-8, 86),
    new Coords(-8, 42),
    new Coords(-8, -40),
    new Coords(-7, 85),
    new Coords(-7, 42),
    new Coords(-7, -39),
    new Coords(-6, 85),
    new Coords(-6, 42),
    new Coords(-6, -37),
    new Coords(-5, 87),
    new Coords(-5, 42),
    new Coords(-5, -36),
    new Coords(-4, 89),
    new Coords(-4, 42),
    new Coords(-4, -35),
    new Coords(-3, 90),
    new Coords(-3, 42),
    new Coords(-3, -34),
    new Coords(-2, 91),
    new Coords(-2, 42),
    new Coords(-2, -33),
    new Coords(-1, 92),
    new Coords(-1, 41),
    new Coords(-1, -32),
    new Coords(0, 92),
    new Coords(0, 42),
    new Coords(0, -31),
    new Coords(1, 92),
    new Coords(1, 41),
    new Coords(1, -30),
    new Coords(2, 40),
    new Coords(2, -29),
    new Coords(3, 40),
    new Coords(3, -28),
    new Coords(4, 39),
    new Coords(4, -27),
    new Coords(5, 38),
    new Coords(5, -26),
    new Coords(6, 38),
    new Coords(6, -25),
    new Coords(7, 38),
    new Coords(7, -24),
    new Coords(8, 92),
    new Coords(8, 38),
    new Coords(8, -23),
    new Coords(9, 92),
    new Coords(9, 38),
    new Coords(9, -22),
    new Coords(10, 92),
    new Coords(10, 39),
    new Coords(10, -22),
    new Coords(11, 92),
    new Coords(11, 38),
    new Coords(11, -21),
    new Coords(12, 91),
    new Coords(12, 38),
    new Coords(12, -20),
    new Coords(13, 91),
    new Coords(13, 44),
    new Coords(13, -19),
    new Coords(14, 91),
    new Coords(14, 46),
    new Coords(14, -18),
    new Coords(15, 90),
    new Coords(15, 48),
    new Coords(15, -18),
    new Coords(16, 90),
    new Coords(16, 49),
    new Coords(16, -17),
    new Coords(17, 89),
    new Coords(17, 51),
    new Coords(17, -16),
    new Coords(18, 89),
    new Coords(18, -15),
    new Coords(19, 88),
    new Coords(19, -14),
    new Coords(20, 87),
    new Coords(20, -13),
    new Coords(21, 87),
    new Coords(21, -13),
    new Coords(22, 86),
    new Coords(22, -12),
    new Coords(23, 84),
    new Coords(23, -11),
    new Coords(24, 83),
    new Coords(24, 50),
    new Coords(24, -10),
    new Coords(25, 81),
    new Coords(25, 71),
    new Coords(25, 56),
    new Coords(25, 49),
    new Coords(25, -9),
    new Coords(26, 70),
    new Coords(26, 48),
    new Coords(26, -9),
    new Coords(27, 69),
    new Coords(27, 47),
    new Coords(27, -8),
    new Coords(28, 67),
    new Coords(28, 46),
    new Coords(28, -7),
    new Coords(29, 41),
    new Coords(29, -6),
    new Coords(30, 41),
    new Coords(30, -5),
    new Coords(31, 40),
    new Coords(31, -4),
    new Coords(32, 40),
    new Coords(32, -3),
    new Coords(33, 39),
    new Coords(33, -2),
    new Coords(34, 39),
    new Coords(34, -1),
    new Coords(35, 38),
    new Coords(35, 1),
    new Coords(36, 38),
    new Coords(36, 2),
    new Coords(37, 37),
    new Coords(37, 4),
    new Coords(38, 37),
    new Coords(38, 5),
    new Coords(39, 36),
    new Coords(39, 8),
    new Coords(40, 35),
    new Coords(41, 35),
    new Coords(42, 34),
    new Coords(43, 33),
    new Coords(44, 33),
    new Coords(45, 32),
    new Coords(46, 32),
    new Coords(46, -34),
    new Coords(47, 33),
    new Coords(47, -39),
    new Coords(48, 34),
    new Coords(48, -41),
    new Coords(49, 35),
    new Coords(49, -43),
    new Coords(50, 36),
    new Coords(50, -45),
    new Coords(51, 37),
    new Coords(51, -46),
    new Coords(51, -61),
    new Coords(52, 38),
    new Coords(52, -51),
    new Coords(53, 39),
    new Coords(54, 39),
    new Coords(55, 40),
    new Coords(56, 41),
    new Coords(57, 42),
    new Coords(58, 42),
    new Coords(58, 30),
    new Coords(59, 43),
    new Coords(59, 21),
    new Coords(60, 43),
    new Coords(60, 31),
    new Coords(60, 19),
    new Coords(61, 44),
    new Coords(61, 31),
    new Coords(61, 18),
    new Coords(62, 44),
    new Coords(62, 31),
    new Coords(62, 17),
    new Coords(63, 44),
    new Coords(63, 31),
    new Coords(63, 15),
    new Coords(64, 44),
    new Coords(64, 30),
    new Coords(64, 12),
    new Coords(65, 43),
    new Coords(65, 30),
    new Coords(66, 42),
    new Coords(66, 30),
    new Coords(67, 41),
    new Coords(67, 30),
    new Coords(68, 30),
    new Coords(69, 29),
    new Coords(70, 28)
  };

  public ExplosionCutout cutoutTexture => this.baseTree.cutoutTexture;

  private Texture2D texture => this.baseTree.texture;

  public override int ApplyDamage(
    SpellEnum spellEnum,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
    if (this.isDead)
      return 0;
    if (dt == DamageType.Heal || dt == DamageType.Heal20 || dt == DamageType.Heal50)
    {
      this.health += damage;
      if (this.health > this.maxHealth)
        this.health = this.maxHealth;
      return 0;
    }
    if (this.baseTree.isButterflyJar)
    {
      if (damage <= 0 || (ZComponent) enemy != (object) null && enemy.collider.layer == Inert.mask_ButterflyJar)
        return 0;
      foreach (ZMyCollider zmyCollider in this.game.world.OverlapCircleAll((Point) this.position, 14, this.collider, Inert.mask_ButterflyJar))
      {
        if ((ZComponent) zmyCollider != (object) null && (ZComponent) zmyCollider.creature != (object) null)
        {
          zmyCollider.creature.collider.layer = 256;
          zmyCollider.creature.collider.gameObjectLayer = 8;
          if ((ZComponent) enemy != (object) null && zmyCollider.creature.team != enemy.team && enemy.parent.controlled.Count > 0)
          {
            zmyCollider.creature.SwitchTeams(enemy.parent, true);
            zmyCollider.creature.CreatureMoveSurroundings();
          }
        }
      }
      this.collider?.Disable();
      this.isDead = true;
      this.isNull = true;
      this.auraOfDecay?.SetNull();
      AudioManager.Play(this.baseCreature.clipDeath);
      if ((Object) this.transform != (Object) null && !this.game.resyncing)
        ZComponent.Instantiate<GameObject>(this.baseCreature.deathExplosion, this.transform.position, Quaternion.identity, this.game.GetMapTransform());
      ZComponent.Destroy<GameObject>(this.gameObject);
      ZSpell.ApplyExplosionForce(SpellEnum.Butterfly_Jar, this.world, this.position, 0, Curve.None, 10, 50, (FixedInt) 0, DamageType.None, (ZCreature) this, this.game.turn);
      return 0;
    }
    if (dt == DamageType.Drain && (ZComponent) enemy != (object) null)
    {
      damage = Mathf.Min(damage, enemy.health);
      enemy.DoHeal(damage, dt, enemy);
      if (enemy.health > Mathf.Max(enemy.maxHealth, 250))
        enemy.health = Mathf.Max(enemy.maxHealth, 250);
      enemy.UpdateHealthTxt();
    }
    if (this.health > 900)
      return 0;
    this.health -= damage;
    if (this.health <= 0)
    {
      if ((Object) this.texture != (Object) null)
        this.map.ServerBitBlt((int) this.cutoutTexture, (int) this.position.x, (int) this.position.y, false);
      this.game.forceRysncPause = true;
      this.collider?.Disable();
      this.isDead = true;
      this.isNull = true;
      this.auraOfDecay?.SetNull();
      ZComponent.Destroy<GameObject>(this.gameObject);
    }
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
    this.StartMoving(false);
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
    if (this.moving != null || this.isDead || this.baseTree.isButterflyJar || !this.baseCreature.canMove)
      return;
    if (this.map.CheckTexutureOnlyMap((int) this.position.x, (int) this.position.y, this.texture))
    {
      this.moving = this.game.ongoing.RunSpell(this.TreeFall());
    }
    else
    {
      this.isMoving = false;
      this.velocity.x = (FixedInt) 0;
      this.velocity.y = (FixedInt) 0;
    }
  }

  public override void ApplyHeal(DamageType dt, int damage, ZCreature enemy)
  {
  }

  private IEnumerator<float> TreeFall()
  {
    ZCreatureTree creature = this;
    creature.isMoving = true;
    creature.velocity.x = (FixedInt) 0;
    creature.velocity.y = (FixedInt) 1;
    int x = (int) creature.position.x;
    while (!creature.isDead && creature.position.y > 20)
    {
      int y1 = (int) creature.velocity.y;
      int y2 = (int) creature.position.y;
      for (int index1 = 1; index1 <= y1; ++index1)
      {
        for (int index2 = 0; index2 < ZCreatureTree.coords.Length; ++index2)
        {
          if (!creature.map.CheckPosition(x + ZCreatureTree.coords[index2].x, y2 + ZCreatureTree.coords[index2].y - index1, (ZCreature) creature, Inert.mask_entity_movement))
          {
            if (!creature.isDead)
            {
              creature.position = new MyLocation(x, y2 - index1 + 1);
              creature.collider.Move(creature.position);
            }
            creature.Stop();
            yield break;
          }
          else if (creature.isDead)
          {
            creature.Stop();
            yield break;
          }
        }
      }
      creature.position = new MyLocation(x, y2 - y1);
      creature.collider.Move(creature.position);
      if (creature.velocity.y < 20)
        creature.velocity.y -= creature.map.Gravity;
      creature.game.CreatureMoveSurroundings(creature.position, creature.radius, creature.collider);
      yield return 0.0f;
    }
    creature.ApplyDamage(SpellEnum.Fire_Ball, DamageType.Fire, 100, (ZCreature) null, creature.game.turn, (ISpellBridge) null, false);
  }

  private void Stop()
  {
    this.isMoving = false;
    this.velocity.x = (FixedInt) 0;
    this.velocity.y = (FixedInt) 0;
    this.moving = (IEnumerator<float>) null;
  }
}
