

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZTower : ZComponent
{
  public int id;
  public Tower baseTower;
  public Tower clientObj;
  internal ZEffector effector;
  internal int originX;
  public int MaxHealth = 75;
  public int Health = 75;
  public ZCreature creature;
  public ZEffector sanctuaryEffector;
  private List<Coords> fallPoints;
  private MyLocation _position;
  public ZMyCollider collider;
  internal ZMyCollider secondCollider;
  private bool illusionWasFalling;
  [NonSerialized]
  public bool dead;
  internal bool deaderThenDead;
  public bool firstCast = true;
  [NonSerialized]
  public int delayKillOffset = 1;
  [NonSerialized]
  public int impdmg;

  public static ZTower Create(
    ZGame game,
    Tower c,
    Vector3 position,
    Quaternion q,
    Transform parent)
  {
    ZTower ztower = new ZTower();
    ztower.id = ++game.nextCreatureID;
    ztower.Copy(c, game);
    ztower.SetupClient(c, position, q, parent);
    return ztower;
  }

  private void SetupClient(Tower c, Vector3 position, Quaternion q, Transform parent)
  {
    Tower tower = UnityEngine.Object.Instantiate<Tower>(c, position, q, parent);
    this.clientObj = tower;
    this.gameObject = tower.gameObject;
    this.transform = tower.transform;
    tower.serverObj = this;
    if (!((ZComponent) this.effector != (object) null))
      return;
    this.effector.gameObject = tower.effector.gameObject;
    this.effector.transform = tower.effector.transform;
  }

  public void Copy(Tower c, ZGame game)
  {
    this.baseTower = c;
    this.MaxHealth = c.MaxHealth;
    this.Health = c.Health;
    if ((UnityEngine.Object) c.collider != (UnityEngine.Object) null)
    {
      this.collider = new ZMyCollider();
      this.collider.Copy(c.collider);
      this.collider.tower = this;
    }
    if (!((UnityEngine.Object) c.effector != (UnityEngine.Object) null))
      return;
    this.effector = new ZEffector();
    this.effector.Copy(c.effector, game);
  }

  public void Serialzie(myBinaryWriter writer)
  {
    writer.Write(this.baseTower.name);
    writer.Write(this.position);
    writer.Write(this.MaxHealth);
    writer.Write(this.Health);
    writer.Write(this.originX);
  }

  public static void Deserialize(myBinaryReader reader, ZGame game, ZCreature c)
  {
    string key = reader.ReadString();
    MyLocation myLocation = reader.ReadMyLocation();
    ZTower t = ZTower.Create(game, Inert.Instance.Towers[key], (Vector3) myLocation.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform());
    t.creature = c;
    c.tower = t;
    t.collider.world = t.world;
    t.position = myLocation;
    t.MaxHealth = reader.ReadInt32();
    t.Health = reader.ReadInt32();
    t.originX = reader.ReadInt32();
    ZSpell.TowerApplyEffectors(true, t, c);
  }

  public ZGame game => this.creature.game;

  public ZMyWorld world => this.game.world;

  public ZMap map => this.game.map;

  public TowerType type => this.baseTower.type;

  public int radius => this.baseTower.radius;

  public int lowerRadius => this.baseTower.lowerRadius;

  public int colliderOffset => this.baseTower.colliderOffset;

  public ExplosionCutout cutout => this.baseTower.cutout;

  public int playerOffsetY => this.baseTower.playerOffsetY;

  public static FixedInt AddedOffsetY => Tower.AddedOffsetY;

  public int cutoutOffsetY => this.baseTower.cutoutOffsetY;

  public Texture2D texture => this.baseTower.texture;

  public int firstChildYOffset => this.baseTower.firstChildYOffset;

  private static FixedInt CogTowerRadius => Tower.CogTowerRadius;

  public MyLocation position
  {
    get => this._position;
    set
    {
      this._position = value;
      if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
        this.transform.position = (Vector3) value.ToSinglePrecision();
      this.collider.Move(value);
    }
  }

  public bool IsResistant()
  {
    return (this.firstCast || this.type == TowerType.Stone) && (this.type == TowerType.Stone || !this.game.isElementals) && (this.type == TowerType.Stone && this.game.AllowExpansion || !this.game.isElementals && this.creature.familiar.Has(FamiliarType.Stone) && this.creature.familiarLevelStone > 0);
  }

  public void DelayKill()
  {
    if (this.dead)
      return;
    if (this.Health > 0)
      this.Health = 0;
    this.dead = true;
    this.creature?.UpdateHealthTxt();
    this.game.ongoing.RunCoroutine(this.killDelay());
  }

  private IEnumerator<float> killDelay()
  {
    yield return 0.0f;
    while (!ZComponent.IsNull((ZComponent) this.creature) && !this.creature.isDead && this.creature.isMoving)
      yield return 0.0f;
    if (!ZComponent.IsNull((ZComponent) this.creature) && this.creature.inWater && this.type == TowerType.Arcane && !this.deaderThenDead && this.creature.health > 0)
      this.dead = false;
    else if (!ZComponent.IsNull((ZComponent) this.creature) && this.Health > 0 && !this.deaderThenDead && this.creature.health > 0)
    {
      this.dead = false;
    }
    else
    {
      for (int i = 0; i < this.delayKillOffset; ++i)
        yield return 0.0f;
      this.creature?.DestroyTower();
    }
  }

  public void NextTurn()
  {
    switch (this.type)
    {
      case TowerType.Darkness:
        this.Health += 30;
        if (this.Health > this.MaxHealth)
          this.Health = this.MaxHealth;
        this.creature.UpdateHealthTxt();
        break;
      case TowerType.Light:
        this.creature.ApplyHeal(DamageType.Heal, 10, this.creature);
        break;
      case TowerType.Seasons:
        ZSpell.TreeHouse(this.creature, this.baseTower.FromSpell);
        break;
      case TowerType.Wooden:
        ZSpell.ApplyExplosionForce(SpellEnum.Watchtower, this.creature.world, this.creature.position, 40, Curve.Generic, 50, 50, (FixedInt) 8, DamageType.Wallop, this.creature, this.creature.game.turn);
        this.creature.DestroyTower();
        if (!this.game.isClient || !((UnityEngine.Object) this.creature.transform != (UnityEngine.Object) null))
          break;
        AudioManager.Play(AudioManager.instance.watchtowerDestroyed);
        ZComponent.Instantiate<GameObject>(ClientResources.Instance.woodenExplosion, this.creature.transform.position, Quaternion.identity, this.game.GetMapTransform());
        break;
    }
  }

  public int GetDamageWhenDead(DamageType dt, int damage, ZCreature enemy)
  {
    switch (dt)
    {
      case DamageType.Snow:
        if (this.type == TowerType.Frost || this.type == TowerType.Seasons)
          return 0;
        break;
      case DamageType.Heal:
        this.creature.DoHeal(damage, dt, enemy);
        if (this.creature.health > this.creature.maxHealth)
        {
          damage -= this.creature.health - this.creature.maxHealth;
          this.creature.health = this.creature.maxHealth;
        }
        this.creature.UpdateHealthTxt();
        enemy?.parent?.awards.Heal(enemy, this.creature, damage);
        return 0;
      case DamageType.Water:
        if (this.creature.familiar.Has(FamiliarType.Seas))
        {
          damage -= this.creature.familiarLevelSeas;
          if (damage <= 0)
            return 0;
          break;
        }
        break;
    }
    if (this.type == TowerType.Stone || !this.game.isElementals)
    {
      if (this.type == TowerType.Stone && this.game.AllowExpansion)
        damage -= 5;
      else if (!this.game.isElementals && this.creature.familiar.Has(FamiliarType.Stone) && this.creature.familiarLevelStone > 0)
        damage -= this.creature.StoneDamageReduction;
      if (damage <= 0)
        damage = 1;
    }
    return damage;
  }

  public void ApplyDamage(
    SpellEnum hitBySpell,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null)
  {
    if (this.deaderThenDead || dt == DamageType.NoAllies && (ZComponent) enemy != (object) null && enemy.parent.team == this.creature.parent.team || dt == DamageType.Ignore)
      return;
    if ((ZComponent) enemy != (object) null && !enemy.isPawn && enemy.parent != null && (spellRef == null || !spellRef.FromArmageddon))
      enemy.parent.awards.OnlyMinionDamage = false;
    if (this.creature.invulnerable > 0 && dt != DamageType.Heal || this.dead && (dt != DamageType.Arcane || this.type != TowerType.Arcane))
    {
      if (this.dead)
        this.impdmg += this.GetDamageWhenDead(dt, damage, enemy);
      if (!this.dead || hitBySpell != SpellEnum.Charge)
        return;
      this.delayKillOffset += 3;
    }
    else
    {
      if ((dt == DamageType.Sunder || dt == DamageType.Rake || dt == DamageType.SunderLight) && (ZComponent) enemy != (object) null && this.creature.team == enemy.team)
        return;
      if (dt == DamageType.Arcane && this.type == TowerType.Arcane && (ZComponent) enemy != (object) null && enemy.FullArcane && hitBySpell == SpellEnum.Arcane_Flash)
      {
        damage = this.Health;
        this.Health = 0;
        this.deaderThenDead = true;
        enemy?.achievementParent?.awards.DealtDamge(enemy, this.creature, damage, hitBySpell, spellRef);
        this.creature.UpdateHealthTxt();
        this.DelayKill();
        if (this.game.isClient && (UnityEngine.Object) this.creature.transform != (UnityEngine.Object) null)
          ZComponent.Instantiate<GameObject>(Inert.Instance.fountain, this.creature.transform.position, Quaternion.identity, this.game.GetMapTransform());
        MyLocation target = new MyLocation(this.creature.position.x, (FixedInt) (this.map.Height + 200));
        this.game.ongoing.RunCoroutine(ZSpell.IEnumeratorArcaneArrows(Inert.Instance.spells["Arcane Arrow"], enemy, this.creature.position + new MyLocation(0, 50), Quaternion.identity, (FixedInt) 90, (FixedInt) 1, 25, target));
      }
      else if (dt == DamageType.Heal || dt == DamageType.Heal20)
      {
        if (this.type == TowerType.Stone || !this.game.isElementals)
        {
          if (this.type == TowerType.Stone && this.game.AllowExpansion)
            damage -= 5;
          else if (!this.game.isElementals && this.creature.familiar.Has(FamiliarType.Stone) && this.creature.familiarLevelStone > 0)
            damage -= this.creature.StoneDamageReduction;
          if (damage <= 0)
            damage = 1;
        }
        this.creature.ApplyHeal(dt, damage, enemy);
      }
      else if (dt == DamageType.Arcane && this.type == TowerType.Arcane)
      {
        this.Health += damage;
        if (this.Health > this.MaxHealth)
        {
          damage -= this.Health - this.MaxHealth;
          this.Health = this.MaxHealth;
        }
        this.creature.UpdateHealthTxt();
        enemy?.parent?.awards.Heal(enemy, this.creature, damage);
      }
      else
      {
        switch (dt)
        {
          case DamageType.Snow:
            if (this.type == TowerType.Frost || this.type == TowerType.Holiday || this.type == TowerType.Seasons && this.game.currentSeason == GameSeason.Winter)
            {
              this.Health += Mathf.Max(1, damage / 2);
              if (this.Health > this.MaxHealth)
                this.Health = this.MaxHealth;
              else
                enemy?.parent?.awards.Heal(enemy, this.creature, 1);
              this.creature.UpdateHealthTxt();
              return;
            }
            break;
          case DamageType.Heal:
            this.creature.DoHeal(damage, dt, enemy);
            if (this.creature.health > this.creature.maxHealth)
            {
              damage -= this.creature.health - this.creature.maxHealth;
              this.creature.health = this.creature.maxHealth;
            }
            this.creature.UpdateHealthTxt();
            enemy?.parent?.awards.Heal(enemy, this.creature, damage);
            return;
          case DamageType.Pebble:
            damage <<= 1;
            break;
          case DamageType.Water:
            if (this.creature.familiar.Has(FamiliarType.Seas))
            {
              damage -= this.creature.familiarLevelSeas;
              if (damage <= 0)
                return;
              break;
            }
            break;
          case DamageType.Percentage50:
            damage = (this.Health + this.creature.health) * damage / 100;
            int num1 = 15;
            if ((ZComponent) enemy != (object) null)
              num1 += enemy.additionalBleedOutDamage;
            damage += num1;
            break;
        }
        if ((this.type == TowerType.Stone || !this.game.isElementals) && dt != DamageType.Percentage50)
        {
          if (this.type == TowerType.Stone && this.game.AllowExpansion)
          {
            if (this.creature.shield <= 0)
              damage -= 5;
          }
          else if (!this.game.isElementals && this.creature.familiar.Has(FamiliarType.Stone) && this.creature.familiarLevelStone > 0)
            damage -= !this.game.AllowExpansion || this.creature.shield == 0 ? this.creature.StoneDamageReduction : 0;
          if (damage <= 0)
            damage = 1;
        }
        if (dt == DamageType.Drain && !ZComponent.IsNull((ZComponent) enemy) && !enemy.isDead)
        {
          if ((ZComponent) enemy == (object) this.creature)
            return;
          int damage1 = Mathf.Min(damage, enemy.health);
          if (!this.creature.drainable)
            enemy.DoHeal(damage1);
          else
            enemy.DoHeal(damage1, dt, enemy);
          if (enemy.health > Mathf.Max(enemy.maxHealth, this.game.MaxHealth(enemy)))
            enemy.health = Mathf.Max(enemy.maxHealth, this.game.MaxHealth(enemy));
          enemy.UpdateHealthTxt();
        }
        if (!ZComponent.IsNull((ZComponent) enemy) && !enemy.isDead && enemy.race == CreatureRace.Undead && !enemy.isPawn && dt != DamageType.Drain)
        {
          int damage2 = Mathf.Min(enemy.health, Mathf.Clamp(damage, 0, this.Health)) / 2;
          if (damage2 < 1)
            damage2 = 1;
          enemy.DoHeal(damage2, dt, enemy);
          if (enemy.health > enemy.maxHealth)
          {
            int num2 = damage2 - (enemy.health - enemy.maxHealth);
            enemy.health = enemy.maxHealth;
          }
          enemy.UpdateHealthTxt();
          enemy?.parent?.awards.Heal(enemy, this.creature, damage);
        }
        if (dt == DamageType.Gravity)
        {
          if (hitBySpell == SpellEnum.Gravity_Pulse)
          {
            if (this.creature.type != CreatureType.Cosmic_Horror && this.creature.familiarLevelCosmos < 5)
            {
              this.creature.CreateGravityObj();
              this.creature.appliedGravity = this.creature.parent.localTurn + 3;
            }
          }
          else if (this.creature.type != CreatureType.Cosmic_Horror && this.creature.familiarLevelCosmos < 5)
          {
            this.creature.CreateGravityObj();
            this.creature.appliedGravity = this.creature.parent.localTurn + 2;
          }
        }
        else if (hitBySpell == SpellEnum.Gravity_Well && spellRef != null)
          ZSpell.FireEffector(spellRef.GetBaseSpell, this.creature, this.position, (FixedInt) 0, (FixedInt) 0, true);
        if (this.creature.shield > 0 && dt != DamageType.IgnoreShield && dt != DamageType.Percentage50 && hitBySpell != SpellEnum.Blood_Craze)
        {
          this.creature.HealBloodBank(enemy, Mathf.Min(this.creature.shield, damage), dt);
          if (this.creature.shield >= damage)
          {
            this.creature.entangledShield -= damage;
            if (this.creature.entangled && this.creature.entangledShield < 0)
              this.creature.RemoveEntangle();
            this.creature.shield -= damage;
            enemy?.achievementParent?.awards.DealtDamge(enemy, this.creature, damage, hitBySpell, spellRef);
            if (dt == DamageType.SuperStun)
              this.creature.superStunned = true;
            if (this.creature.superStunned || damage > 0 && (this.game.serverState.busy == ServerState.Busy.No || this.game.serverState.busy == ServerState.Busy.Moving || this.game.serverState.busy == ServerState.Busy.Moving_NoCountdown) && TurnCreated >= this.creature.turnProtectionShieldCast)
              this.creature.OnStunned();
            this.creature.UpdateHealthTxt();
            enemy?.achievementParent?.awards.DealtDamge(enemy, this.creature, damage, hitBySpell, spellRef);
            return;
          }
          damage -= this.creature.shield;
          enemy?.achievementParent?.awards.DealtDamge(enemy, this.creature, damage, hitBySpell, spellRef);
          this.creature.shield = 0;
          this.creature.entangledShield = -1;
        }
        if (this.creature.fusion > 0)
          this.creature.fusion = 0;
        this.Health -= damage;
        if (this.Health > this.MaxHealth)
          this.Health = this.MaxHealth;
        enemy?.achievementParent?.awards.DealtDamge(enemy, this.creature, damage, hitBySpell, spellRef);
        if (this.creature.entangled)
        {
          this.creature.RemoveEntangle();
          if (this.type == TowerType.Illusion && this.Health > 0)
          {
            if (this.creature.velocity.y < 0)
              this.creature.velocity.y = (FixedInt) 1;
            this.ShouldFall();
          }
        }
        if (enemy?.parent == this.creature.parent)
          this.creature.turnFriendlyDmg = this.game.turn;
        if (this.Health <= 0)
        {
          if ((ZComponent) enemy != (object) null)
            this.impdmg = -this.Health;
          this.Health = 0;
          this.creature.UpdateHealthTxt();
          if (hitBySpell == SpellEnum.Charge)
            this.delayKillOffset += 4;
          this.DelayKill();
        }
        else
        {
          this.creature.UpdateHealthTxt();
          if (dt == DamageType.SuperStun)
            this.creature.superStunned = true;
          if (!this.creature.superStunned && (damage <= 0 || this.game.serverState.busy != ServerState.Busy.No && this.game.serverState.busy != ServerState.Busy.Moving && this.game.serverState.busy != ServerState.Busy.Moving_NoCountdown))
            return;
          this.creature.OnStunned();
        }
      }
    }
  }

  private void SetPosition(FixedInt y)
  {
    MyLocation position = this.position with { y = y };
    this.position = position;
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.position = (Vector3) position.ToSinglePrecision();
    position.y += (FixedInt) this.playerOffsetY + ZTower.AddedOffsetY;
    this.creature.MoveToPosition = position;
  }

  public void SetPositionResync(MyLocation pos)
  {
    this.position = pos;
    if (!((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.transform.position = (Vector3) pos.ToSinglePrecision();
  }

  public void SetPosition(MyLocation pos)
  {
    this.position = pos;
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.position = (Vector3) pos.ToSinglePrecision();
    pos.y += (FixedInt) this.playerOffsetY + ZTower.AddedOffsetY;
    this.creature.MoveToPosition = pos;
  }

  public void SetPositionNoTrigger(MyLocation pos)
  {
    this.position = pos;
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.position = (Vector3) pos.ToSinglePrecision();
    pos.y += (FixedInt) this.playerOffsetY + ZTower.AddedOffsetY;
    this.creature.SetPositionNoTrigger(pos);
  }

  private void UpdateFallPoints()
  {
    if (this.type == TowerType.Illusion)
    {
      if (this.illusionWasFalling)
      {
        if (!this.creature.entangledOrGravity)
        {
          this.fallPoints = (List<Coords>) null;
          this.illusionWasFalling = false;
        }
      }
      else if (this.creature.entangledOrGravity)
      {
        this.fallPoints = (List<Coords>) null;
        this.illusionWasFalling = true;
      }
    }
    if (this.type == TowerType.Illusion && !this.creature.entangledOrGravity)
      this.fallPoints = MapGenerator.GetTowerFloatPoints(this.texture, -this.colliderOffset);
    else
      this.fallPoints = MapGenerator.GetTowerFallPoints(this.texture, -this.colliderOffset);
  }

  public bool ShouldFall()
  {
    if (this.type == TowerType.Cosmos && !this.creature.entangledOrGravity || this.creature.inWater || (ZComponent) this.creature.tower == (object) null)
      return false;
    if (this.Health <= 0)
      this.DelayKill();
    this.UpdateFallPoints();
    if (this.creature.velocity.y != 0 || this.creature.isMoving || this.creature.waterWalking && this.position.y <= ZTower.CogTowerRadius && this.position.x >= 0 && this.position.x < this.map.Width && (this.type != TowerType.Illusion || this.creature.entangledOrGravity))
      return false;
    int x = (int) this.position.x;
    int num = (int) this.position.y - 3;
    if (this.type == TowerType.Illusion && !this.creature.entangledOrGravity)
      num = (int) this.position.y + 3;
    for (int index = 0; index < this.fallPoints.Count; ++index)
    {
      if (this.type == TowerType.Illusion && num + this.fallPoints[index].y >= this.map.Height - 3 || !this.map.CheckPosition(x + this.fallPoints[index].x, num + this.fallPoints[index].y, this.creature, Inert.mask_movement_NoEffector))
        return false;
    }
    if (this.type == TowerType.Illusion && !this.creature.entangledOrGravity)
      this.SetPosition(this.position.y + 1);
    else
      this.SetPosition(this.position.y - 1);
    this.creature.moving = this.game.ongoing.RunCoroutine(this.Fall());
    return (ZComponent) this.creature.tower != (object) null;
  }

  public IEnumerator<float> Fall()
  {
    if (this.type == TowerType.Cosmos && !this.creature.entangledOrGravity)
    {
      this.creature.moving = (IEnumerator<float>) null;
      this.creature.isMoving = false;
    }
    else
    {
      if (this.game.serverState.busy == ServerState.Busy.No)
        this.game.serverState.busy = ServerState.Busy.Moving;
      this.UpdateFallPoints();
      this.creature.isMoving = true;
      if (this.type == TowerType.Illusion && !this.creature.entangledOrGravity)
        this.creature.velocity.y = (FixedInt) 1;
      else
        this.creature.velocity.y = (FixedInt) -1;
      yield return 0.0f;
      while (!this.deaderThenDead)
      {
        if (this.creature.velocity.y == 0)
        {
          this.creature.moving = (IEnumerator<float>) null;
          this.creature.isMoving = false;
          if (this.Health > 0)
          {
            yield break;
          }
          else
          {
            this.DelayKill();
            yield break;
          }
        }
        else
        {
          FixedInt x1 = this.position.y;
          FixedInt x2 = this.position.y + this.creature.velocity.y;
          FixedInt y1 = x1;
          int num1 = 1;
          FixedInt y2 = this.creature.velocity.y;
          FixedInt fixedInt = this.creature.velocity.y;
          if (y2 > 1 || y2 < -1)
          {
            num1 = (int) Mathd.Abs(y2) + 1;
            fixedInt = this.creature.velocity.y / num1;
          }
          int x3 = (int) this.position.x;
          while (num1 > 0)
          {
            --num1;
            int num2;
            if (Mathd.Abs(x1) > Mathd.Abs(x2))
            {
              x1 = x2;
              num2 = (int) x1;
            }
            else
              num2 = (int) (fixedInt + x1);
            for (int index = 0; index < this.fallPoints.Count; ++index)
            {
              if (!this.map.CheckPosition(x3 + this.fallPoints[index].x, num2 + this.fallPoints[index].y, this.creature, Inert.mask_entity_movement))
              {
                this.creature.velocity.y = (FixedInt) 0;
                this.SetPosition(y1);
                this.creature.moving = (IEnumerator<float>) null;
                this.creature.isMoving = false;
                this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
                if (this.Health > 0)
                {
                  yield break;
                }
                else
                {
                  this.DelayKill();
                  yield break;
                }
              }
            }
            y1 = x1;
            x1 += fixedInt;
          }
          MyLocation position = this.position;
          this.SetPosition(this.position.y + this.creature.velocity.y);
          this.game.CreatureMoveSurroundings(position, this.radius + 20);
          if (this.type == TowerType.Nature)
            this.sanctuaryEffector.CheckSanctuary();
          if (this.type == TowerType.Illusion && !this.creature.entangledOrGravity)
          {
            if (this.creature.velocity.y < 10)
              this.creature.velocity.y += -this.map.Gravity;
          }
          else
            this.creature.velocity.y += this.map.Gravity;
          if ((FixedInt) (int) this.position.y <= ZTower.CogTowerRadius && (this.type != TowerType.Illusion || this.creature.entangledOrGravity))
          {
            this.creature.moving = (IEnumerator<float>) null;
            this.creature.isMoving = false;
            this.creature.velocity.x = (FixedInt) 0;
            this.creature.velocity.y = (FixedInt) 0;
            if (this.creature.waterWalking)
            {
              if (this.position.x >= 0 && this.position.x <= this.map.Width)
              {
                this.SetPosition(this.position with
                {
                  y = ZTower.CogTowerRadius
                });
                yield break;
              }
            }
            else if ((this.type == TowerType.Frost || this.creature.parent.GetLevel(BookOf.Frost) > 0) && this.position.x >= 0 && this.position.x <= this.map.Width)
            {
              this.map.ServerBitBlt(54, (int) this.position.x, 4, false);
              this.SetPosition(this.position with
              {
                y = ZTower.CogTowerRadius + 8
              });
              this.creature.DoDamage(5);
              this.creature.UpdateHealthTxt();
              if (this.creature.health > 0)
              {
                yield break;
              }
              else
              {
                this.creature.OnDeath(true);
                yield break;
              }
            }
            if (this.impdmg > 0)
              this.creature.DoDamage(this.impdmg);
            this.creature.EnteredWater();
            this.SetPosition(new MyLocation(this.position.x, (FixedInt) -1000));
            this.DelayKill();
            yield break;
          }
          else if (this.type == TowerType.Illusion && !this.creature.entangledOrGravity && this.position.y > this.map.Height - 56)
          {
            this.creature.moving = (IEnumerator<float>) null;
            this.creature.isMoving = false;
            this.creature.velocity.x = (FixedInt) 0;
            this.creature.velocity.y = (FixedInt) 0;
            this.SetPosition(this.position with
            {
              y = (FixedInt) (this.map.Height - 56)
            });
            yield break;
          }
          else
            yield return 0.0f;
        }
      }
      this.creature.isMoving = false;
      if ((ZComponent) this.creature != (object) null && !this.creature.inWater)
        this.creature.Fall();
    }
  }

  public void TowerMoveLeft()
  {
    if (this.type != TowerType.Cogs && this.type != TowerType.Cosmos || this.position.x < this.creature.radius)
      return;
    if (this.type == TowerType.Cosmos && (!this.creature.entangledOrGravity || this.BypassEntangle()))
    {
      this.CosmosMoveLeft();
    }
    else
    {
      List<Coords> towerLeftPoints = MapGenerator.GetTowerLeftPoints(this.texture);
      for (int index = 0; index < towerLeftPoints.Count; ++index)
      {
        if (!this.map.CheckPosition(towerLeftPoints[index].x + (int) this.position.x, towerLeftPoints[index].y + (int) this.position.y + this.firstChildYOffset, this.creature, Inert.mask_entity_movement))
          return;
      }
      this.MoveLeft();
    }
  }

  public void TowerMoveRight()
  {
    if (this.type != TowerType.Cogs && this.type != TowerType.Cosmos || this.position.x > this.game.map.Width - this.creature.radius)
      return;
    if (this.type == TowerType.Cosmos && (!this.creature.entangledOrGravity || this.BypassEntangle()))
    {
      this.CosmosMoveRight();
    }
    else
    {
      List<Coords> towerRightPoints = MapGenerator.GetTowerRightPoints(this.texture);
      for (int index = 0; index < towerRightPoints.Count; ++index)
      {
        if (!this.map.CheckPosition(towerRightPoints[index].x + (int) this.position.x, towerRightPoints[index].y + (int) this.position.y + this.firstChildYOffset, this.creature, Inert.mask_entity_movement))
          return;
      }
      this.MoveRight();
    }
  }

  private bool BypassEntangle() => false;

  public bool TowerMoveDown()
  {
    if (this.type != TowerType.Cosmos)
      return false;
    if ((this.position + new MyLocation(0, -this.CosmosSpeed)).y > this.radius - 20)
    {
      this.UpdateFallPoints();
      this.Check(0, -2);
    }
    return true;
  }

  public bool TowerMoveUp()
  {
    if (this.type != TowerType.Cosmos || this.creature.entangledOrGravity && !this.BypassEntangle())
      return false;
    if ((this.position + new MyLocation(0, this.CosmosSpeed)).y < this.map.Height - this.radius + 10)
    {
      this.fallPoints = MapGenerator.GetTowerFloatPoints(this.texture, -this.colliderOffset);
      this.Check(0, 2);
    }
    return true;
  }

  public bool CosmosMoveLeft()
  {
    if (this.type != TowerType.Cosmos)
      return false;
    if ((this.position + new MyLocation(-this.CosmosSpeed, 0)).x > this.radius - 8)
    {
      this.fallPoints = MapGenerator.GetTowerLeftPoints(this.texture);
      this.Check(-2, 0);
    }
    return true;
  }

  public bool CosmosMoveRight()
  {
    if (this.type != TowerType.Cosmos)
      return false;
    if ((this.position + new MyLocation(this.CosmosSpeed, 0)).x < this.map.Width - this.radius + 8)
    {
      this.fallPoints = MapGenerator.GetTowerRightPoints(this.texture);
      this.Check(2, 0);
    }
    return true;
  }

  private int CosmosSpeed => !this.creature.gravitionalPull ? 2 : 1;

  public bool Check(int offX, int offY)
  {
    int num1 = Mathf.Max(Mathf.Abs(offX), Mathf.Abs(offY));
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      int num2 = (int) this.position.x + (offX != 0 ? (offX > 0 ? index1 : -index1) : 0);
      int num3 = (int) this.position.y + (offY != 0 ? (offY > 0 ? index1 : -index1) : 0);
      for (int index2 = 0; index2 < this.fallPoints.Count; ++index2)
      {
        if (!this.map.CheckPosition(num2 + this.fallPoints[index2].x, num3 + this.fallPoints[index2].y, this.creature, Inert.mask_entity_movement))
        {
          if (index1 > 1)
          {
            int num4 = index1 - 1;
            MyLocation myLocation = new MyLocation((int) this.position.x + (offX != 0 ? (offX > 0 ? num4 : -num4) : 0), (int) this.position.y + (offY != 0 ? (offY > 0 ? num4 : -num4) : 0));
          }
          return false;
        }
      }
    }
    this.SetPosition(new MyLocation(this.position.x + offX, this.position.y + offY));
    this.game.CreatureMoveSurroundings(this.position, this.radius + 20, this.collider);
    if (!this.dead && ZEffector.InEffector(this.collider, EffectorType.Blackhole))
      this.creature.DestroyTower();
    return true;
  }

  public void MoveLeft()
  {
    int x0 = (int) this.position.x - 16;
    int y0 = (int) this.position.y - 12;
    int radius = 6;
    if (this.type == TowerType.Cosmos)
    {
      x0 += 16;
      y0 += 12;
      radius = 10;
    }
    if (this.map.CheckTopLeft(x0, y0, radius, this.creature, Inert.mask_entity_movement) != null || this.map.CheckLeftTop(x0, y0, radius, this.creature, Inert.mask_entity_movement) != null)
      return;
    this.map.CheckLeftBottom(x0, y0, radius, this.creature, Inert.mask_entity_movement);
    Coords coords = this.map.CheckBottomLeft(x0, y0, radius, this.creature, Inert.mask_entity_movement);
    if (coords == null)
    {
      int num = 0;
      for (int index = 1; index < 6; ++index)
      {
        coords = this.map.CheckBottomRight(x0, y0 - index, radius, this.creature, Inert.mask_entity_movement) ?? this.map.CheckBottomLeft(x0, y0 - index, radius, this.creature, Inert.mask_entity_movement);
        if (coords != null)
        {
          num = index;
          break;
        }
      }
      if (coords == null)
      {
        for (int index = 1; index < 6; ++index)
        {
          coords = this.map.CheckBottomLeft(x0 + 36, y0 - index, radius, this.creature, Inert.mask_entity_movement) ?? this.map.CheckBottomRight(x0 + 36, y0 - index, radius, this.creature, Inert.mask_entity_movement);
          if (coords != null)
          {
            num = index;
            break;
          }
        }
        if (num > 1)
        {
          MyLocation pos = this.position + new MyLocation(-1, num * -1);
          if (!(pos.y < this.map.Height - this.radius))
            return;
          this.SetPosition(pos);
          this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
          return;
        }
      }
      if (coords == null && (!this.creature.waterWalking || this.position.y > ZTower.CogTowerRadius || this.position.x <= 0 || this.position.x >= this.map.Width))
      {
        if (this.ShouldFall())
          return;
        this.SetPosition(this.position + new MyLocation(-1, 0));
        this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
        return;
      }
      if (num > 1)
      {
        if (!this.ShouldFall())
        {
          this.SetPosition(this.position + new MyLocation(-1, 0));
        }
        else
        {
          MyLocation pos = this.position + new MyLocation(-1, num * -1);
          if (pos.y < this.map.Height - this.radius)
            this.SetPosition(pos);
        }
      }
      else
        this.SetPosition(this.position + new MyLocation(-1, 0));
    }
    else
    {
      MyLocation pos = this.position + new MyLocation(-1, 1);
      if (pos.y < this.map.Height - this.radius)
        this.SetPosition(pos);
    }
    this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
  }

  public void MoveRight()
  {
    int x0 = (int) this.position.x + 20;
    int y0 = (int) this.position.y - 12;
    int radius = 6;
    if (this.type == TowerType.Cosmos)
    {
      x0 -= 16;
      y0 += 12;
      radius = 10;
    }
    if (this.map.CheckTopRight(x0, y0, radius, this.creature, Inert.mask_entity_movement) != null || this.map.CheckRightTop(x0, y0, radius, this.creature, Inert.mask_entity_movement) != null)
      return;
    this.map.CheckRightBottom(x0, y0, radius, this.creature, Inert.mask_entity_movement);
    Coords coords = this.map.CheckBottomRight(x0, y0, radius, this.creature, Inert.mask_entity_movement);
    if (coords == null)
    {
      int num = 0;
      for (int index = 1; index < 6; ++index)
      {
        coords = this.map.CheckBottomLeft(x0, y0 - index, radius, this.creature, Inert.mask_entity_movement) ?? this.map.CheckBottomRight(x0, y0 - index, radius, this.creature, Inert.mask_entity_movement);
        if (coords != null)
        {
          num = index;
          break;
        }
      }
      if (coords == null)
      {
        for (int index = 1; index < 6; ++index)
        {
          coords = this.map.CheckBottomLeft(x0 - 36, y0 - index, radius, this.creature, Inert.mask_entity_movement) ?? this.map.CheckBottomRight(x0 - 36, y0 - index, radius, this.creature, Inert.mask_entity_movement);
          if (coords != null)
          {
            num = index;
            break;
          }
        }
        if (num > 1)
        {
          MyLocation pos = this.position + new MyLocation(1, num * -1);
          if (!(pos.y < this.map.Height - this.radius))
            return;
          this.SetPosition(pos);
          this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
          return;
        }
      }
      if (coords == null && (!this.creature.waterWalking || this.position.y > ZTower.CogTowerRadius || this.position.x <= 0 || this.position.x >= this.map.Width))
      {
        if (this.ShouldFall())
          return;
        this.SetPosition(this.position + new MyLocation(1, 0));
        this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
        return;
      }
      if (num > 1)
      {
        if (!this.ShouldFall())
        {
          this.SetPosition(this.position + new MyLocation(1, 0));
        }
        else
        {
          MyLocation pos = this.position + new MyLocation(1, num * -1);
          if (pos.y < this.map.Height - this.radius)
            this.SetPosition(pos);
        }
      }
      else
        this.SetPosition(this.position + new MyLocation(1, 0));
    }
    else
    {
      MyLocation pos = this.position + new MyLocation(1, 1);
      if (pos.y < this.map.Height - this.radius)
        this.SetPosition(pos);
    }
    this.game.CreatureMoveSurroundings(this.position, this.radius + 20);
  }
}
