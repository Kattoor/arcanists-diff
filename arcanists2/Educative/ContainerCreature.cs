// Decompiled with JetBrains decompiler
// Type: Educative.ContainerCreature
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class ContainerCreature
  {
    [MoonSharpHidden]
    public ZCreature creature;
    [MoonSharpHidden]
    private int _hashCode;

    [MoonSharpHidden]
    public ContainerCreature(ZCreature c)
    {
      this.creature = c;
      this._hashCode = (ZComponent) c != (object) null ? c.GetHashCode() : 0;
    }

    public int team => this.creature.team;

    public string name => this.creature.name;

    public SpellEnum spellEnum => this.creature.spellEnum;

    public int health
    {
      get => this.creature.health;
      set
      {
        this.creature.health = value;
        this.creature.UpdateHealthTxt();
        if (this.creature.health > 0)
          return;
        this.creature.OnDeath(true);
      }
    }

    public int maxHealth
    {
      get => this.creature.maxHealth;
      set => this.creature.maxHealth = value;
    }

    public int healthAndTower => this.creature.GetCurrentHealth();

    public int shield
    {
      get => this.creature.shield;
      set
      {
        this.creature.CreateProtectionShield();
        this.creature.shield = value;
        this.creature.UpdateHealthTxt();
      }
    }

    public int x
    {
      get => this.creature.position.x.ToInt();
      set
      {
        this.creature.MoveToPosition = new MyLocation((FixedInt) value, this.creature.position.y);
        this.creature.Demount();
        this.creature.Fall();
      }
    }

    public int y
    {
      get => this.creature.position.y.ToInt();
      set
      {
        this.creature.MoveToPosition = new MyLocation(this.creature.position.x, (FixedInt) value);
        this.creature.Demount();
        this.creature.Fall();
      }
    }

    public Point position
    {
      get
      {
        MyLocation position = this.creature.position;
        double x = (double) position.x.ToInt();
        position = this.creature.position;
        double y = (double) position.y.ToInt();
        return new Point(x, y);
      }
      set
      {
        this.creature.MoveToPosition = new MyLocation((FixedInt) (float) value.x, (FixedInt) (float) value.y);
        this.creature.Demount();
        this.creature.Fall();
      }
    }

    public ContainerPlayer parent
    {
      get => new ContainerPlayer(this.creature.parent);
      set => this.creature.SwitchTeams(value.person);
    }

    public int weight
    {
      get => (int) ((FixedInt) 100 - this.creature.massMulti * 100);
      set
      {
        this.creature.massMulti = Mathd.Clamp((FixedInt) ((float) (100 - value) / 100f), (FixedInt) 0, (FixedInt) 1);
      }
    }

    public int radius
    {
      get => this.creature.GetRadius();
      set => this.SetRadius(value);
    }

    public bool inTower => (ZComponent) this.creature.tower != (object) null;

    public bool isMoving => this.creature.isMoving;

    public bool stunned
    {
      get => this.creature.stunned;
      set
      {
        if (this.creature.stunned == value)
          return;
        this.creature.stunned = value;
        if (!value || !this.creature.game.isClient)
          return;
        ParticleDucks component = Object.Instantiate<GameObject>(Inert.Instance.ducks, this.creature.transform.position + new Vector3(0.0f, (float) (this.radius + 15)), Quaternion.Euler(-55f, 0.0f, 0.0f), this.creature.transform).GetComponent<ParticleDucks>();
        component.c = this.creature.clientObj;
        this.creature.myducks = component;
      }
    }

    public bool superStunned
    {
      get => this.creature.superStunned;
      set
      {
        if (this.creature.superStunned == value)
          return;
        this.creature.stunned = value;
        this.creature.superStunned = value;
        if (!value || !this.creature.game.isClient)
          return;
        ParticleDucks component = Object.Instantiate<GameObject>(Inert.Instance.ducks, this.creature.transform.position + new Vector3(0.0f, (float) (this.radius + 15)), Quaternion.Euler(-55f, 0.0f, 0.0f), this.creature.transform).GetComponent<ParticleDucks>();
        component.c = this.creature.clientObj;
        this.creature.myducks = component;
        component.SetRed();
      }
    }

    public bool waterWalking
    {
      get => this.creature.waterWalking;
      set => this.creature.waterWalking = value;
    }

    public bool frostWalking
    {
      get => this.creature.frostWalking;
      set => this.creature.frostWalking = value;
    }

    public bool inWater => this.creature.inWater;

    public bool diesInWater
    {
      get => this.creature.diesInWater;
      set => this.creature.diesInWater = value;
    }

    public CreatureType type
    {
      get => this.creature.type;
      set => this.creature.type = value;
    }

    public CreatureRace race
    {
      get => this.creature.race;
      set => this.creature.race = value;
    }

    public bool yourTurn => this.creature.parent.yourTurn;

    public bool isRider => (ZComponent) this.creature.mount != (object) null;

    public bool isMounted => (ZComponent) this.creature.rider != (object) null;

    public bool isDead => (ZComponent) this.creature == (object) null || this.creature.isDead;

    public bool isPawn => this.creature.isPawn;

    public bool isFlying
    {
      get => this.creature.flying;
      set
      {
        this.creature.flying = value;
        this.creature.permenantFlight = value;
      }
    }

    public bool isMountable => this.creature.mountable;

    public bool canMount => this.creature.canMount;

    public bool arcaneMonster
    {
      get => this.creature.FullArcane;
      set
      {
        bool flag = value;
        if (this.creature.FullArcane == flag)
          return;
        this.creature.FullArcane = flag;
        if (!flag)
          return;
        this.creature.TutorialArcaneMonster();
      }
    }

    public bool takeDamage
    {
      get => this.creature.invulnerable < 99999;
      set => this.creature.invulnerable = value ? 0 : 99999;
    }

    public ContainerCreature getRider()
    {
      return !this.isMounted ? (ContainerCreature) null : new ContainerCreature(this.creature.rider);
    }

    public ContainerCreature getMount()
    {
      return !this.isRider ? (ContainerCreature) null : new ContainerCreature(this.creature.mount);
    }

    public ContainerTower getTower()
    {
      return !this.inTower ? (ContainerTower) null : new ContainerTower(this.creature.tower);
    }

    public void LongJump(int dir)
    {
      if (this.isMoving)
        return;
      this.setDirection(dir);
      this.creature.LongJump(0);
    }

    public void HighJump(int dir)
    {
      if (this.isMoving)
        return;
      this.setDirection(dir);
      this.creature.HighJump(0);
    }

    public void Walk(int dir)
    {
      if (this.isMoving)
        return;
      this.setDirection(dir);
      this.creature.animator?.Play(AnimateState.Walk, 0.05f);
      if (dir < 0 || dir == 0 && (double) this.creature.transformscale < 0.0)
        this.creature.MoveLeft();
      else
        this.creature.MoveRight();
    }

    public void setDirection(int dir)
    {
      if (this.isMoving)
        return;
      if (dir > 0)
      {
        if ((double) this.creature.transformscale >= 0.0)
          return;
        this.creature.SetScale((float) dir);
      }
      else
      {
        if (dir >= 0 || (double) this.creature.transformscale <= 0.0)
          return;
        this.creature.SetScale((float) dir);
      }
    }

    public int getFamiliarLevel(BookOf book)
    {
      int index = (int) book;
      return index < 0 || index >= this.creature.parent.familiarLevels.Length ? 0 : this.creature.parent.familiarLevels[index];
    }

    public int getActivateableFamiliarLevel() => this.creature.familiarLevelActivateable;

    public BookOf getActivateableFamiliarBookID() => this.creature.parent.ActivateableFamiliar;

    public void increaseFamiliarLevel(BookOf book = BookOf.Nothing, bool dealDmg = true, bool capAt5 = true)
    {
      int b00k = (int) book;
      if (b00k == -1)
        b00k = (int) this.getActivateableFamiliarBookID();
      if (b00k < 0 || b00k >= this.creature.parent.familiarLevels.Length)
        return;
      if (!capAt5 || this.creature.parent.familiarLevels[b00k] < 5)
        ++this.creature.parent.familiarLevels[b00k];
      if (dealDmg)
        this.creature.DoDamage(20);
      if (this.creature.hasDarkDefenses)
        this.creature.DarkDefenses(true);
      this.creature.parent.familiarBook |= (FamiliarType) (1 << b00k);
      this.creature.game.CreateFamiliar((BookOf) b00k, this.creature.parent);
      if (!this.creature.game.isClient)
        return;
      this.creature.UpdateHealthTxt();
      if (!((Object) Player.Instance != (Object) null) || this.creature.parent != Player.Instance.person)
        return;
      HUD.instance.fullBookTextLevel.text = "Level " + (object) this.creature.familiarLevelActivateable;
    }

    public bool inside(ContainerIndicator a)
    {
      return Mathd.Abs((FixedInt) MyLocation.Distance(this.creature.position, a.position.ToMyLocation())) < (FixedInt) (float) a.radius;
    }

    public bool inside(Point a, double radius)
    {
      return Mathd.Abs((FixedInt) MyLocation.Distance(this.creature.position, a.ToMyLocation())) < (FixedInt) (float) radius;
    }

    public bool overlap(ContainerIndicator a)
    {
      return Mathd.Abs((FixedInt) MyLocation.Distance(this.creature.position, a.position.ToMyLocation())) < (FixedInt) (float) a.radius + this.creature.GetRadius();
    }

    public bool overlap(Point a, double radius = 0.0)
    {
      return Mathd.Abs((FixedInt) MyLocation.Distance(this.creature.position, a.ToMyLocation())) < (FixedInt) (float) radius + this.creature.GetRadius();
    }

    public void die(bool playSound = true) => this.creature?.OnDeath(playSound);

    private void SetRadius(int i)
    {
      int num1 = 18;
      if (this.creature.spellEnum != SpellEnum.None)
      {
        Spell spell = Inert.GetSpell(this.creature.spellEnum);
        if ((Object) spell != (Object) null)
          num1 = spell.radius;
      }
      i = Mathf.Clamp(i, 1, 8192);
      FixedInt fixedInt = (FixedInt) i / num1;
      float num2 = fixedInt.ToFloat();
      if ((Object) this.creature.transform != (Object) null)
        this.creature.transform.localScale = new Vector3(num2, num2, 1f);
      this.creature.radius = i;
      this.creature.zb = MapGenerator.getOutlineArray(this.creature.radius);
      double scale = (double) this.creature.scale;
      this.creature.scale = num2;
      this.creature.scaleFixed = fixedInt;
      this.creature.collider.radius = this.creature.radius;
      this.creature.collider.Moved();
      this.creature.SetScale(1f);
    }

    public ContainerSpell addSpell(object s)
    {
      Spell spell = ContainerGame.getSpell(s);
      if (!((Object) spell != (Object) null))
        return (ContainerSpell) null;
      SpellSlot s1 = new SpellSlot(spell);
      if (s.GetType() == typeof (ContainerSpell))
        s1 = new SpellSlot(((ContainerSpell) s).slot);
      this.creature.spells.Add(s1);
      return new ContainerSpell(s1);
    }

    public void removeSpell(object s)
    {
      Spell spell = ContainerGame.getSpell(s, this.creature);
      if (!((Object) spell != (Object) null))
        return;
      for (int index = 0; index < this.creature.spells.Count; ++index)
      {
        if (this.creature.spells[index].spell.spellEnum == spell.spellEnum)
          this.creature.spells.RemoveAt(index);
      }
    }

    public void removeSpell(ContainerSpell spell)
    {
      for (int index = 0; index < this.creature.spells.Count; ++index)
      {
        if (this.creature.spells[index] == spell.slot)
        {
          this.creature.spells.RemoveAt(index);
          break;
        }
      }
    }

    public Table getSpells(Script script)
    {
      Table spells = new Table(script);
      int key = 1;
      foreach (SpellSlot spell in this.creature.spells)
      {
        spells[(object) key] = (object) new ContainerSpell(spell);
        ++key;
      }
      return spells;
    }

    public ContainerSpell getSpell(object spellObj)
    {
      if (spellObj == null)
        return (ContainerSpell) null;
      SpellSlot s = (SpellSlot) null;
      if (spellObj.GetType() == typeof (string))
      {
        Spell spell = Inert.GetSpell((string) spellObj);
        if ((Object) spell != (Object) null)
          s = this.creature.GetSpellSlot(spell.spellEnum);
      }
      else if (spellObj.GetType() == typeof (double) || spellObj.GetType() == typeof (int))
        s = this.creature.spells[(int) spellObj - 1];
      else if (spellObj.GetType() == typeof (SpellEnum))
        s = this.creature.GetSpellSlot((SpellEnum) spellObj);
      return s == null ? (ContainerSpell) null : new ContainerSpell(s);
    }

    public int getSpellCount() => this.creature.spells.Count;

    public int castedCount(SpellEnum spellID) => this.creature.parent.GetSpellCast(spellID);

    public int castedCountThisTurn() => this.creature.parent.spellsCastTHISTurn;

    public int castedDamage(SpellEnum spellID) => this.creature.parent.GetSpellDamage(spellID);

    public int getMinionCount() => this.creature.parent.GetMinionCount();

    public void castSpell(
      object spellObj,
      Point target,
      double angle = 0.0,
      double power = 1.0,
      Point secTarget = null)
    {
      Spell spell = ContainerGame.getSpell(spellObj, this.creature);
      if (!((Object) spell != (Object) null))
        return;
      if (secTarget == (Point) null)
        secTarget = new Point(-1000.0, -1000.0);
      this.creature.game.serverState.busy = ServerState.Busy.Moving;
      ZSpell.FireWhich(spell, this.creature, this.creature.position, FixedInt.Create(angle), FixedInt.Create(power), new MyLocation((int) target.x, (int) target.y), new MyLocation((int) secTarget.x, (int) secTarget.y));
      if (!((ZComponent) this.creature == (object) Player.Instance?.person?.first()))
        return;
      Player.Instance.UnselectSpell();
    }

    public void fireAt(object spellObj, ContainerCreature target, double anglevariance = 0.0)
    {
      this.fireAt(spellObj, target.position, anglevariance);
    }

    public void fireAt(object spellObj, Point target, double anglevariance = 0.0, Point secTarget = null)
    {
      Spell spell = ContainerGame.getSpell(spellObj, this.creature);
      if (!((Object) spell != (Object) null))
        return;
      MyLocation myLocation = target.ToMyLocation() - this.creature.position;
      myLocation.Normalize();
      FixedInt d = FixedInt.Abs(this.creature.position.x - (FixedInt) (float) target.x);
      FixedInt v = (FixedInt) spell.speedMax;
      if (v <= 0)
        v = (FixedInt) 1000;
      FixedInt fixedInt = spell.affectedByGravity ? ZSpell.AngleToGoDistance(v / 2, -this.creature.game.gravity, d, (FixedInt) (float) target.y - this.creature.position.y, true) : FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
      FixedInt power = (FixedInt) 524288L;
      if (d > 10 && FixedInt.InvalidAngle(fixedInt))
      {
        power = (FixedInt) 1;
        fixedInt = ZSpell.AngleToGoDistance(v, -this.creature.game.gravity, d, (FixedInt) (float) target.y - this.creature.position.y, false);
        if (FixedInt.InvalidAngle(fixedInt))
          fixedInt = (FixedInt) 47185920L;
      }
      if (target.x < this.creature.position.x.ToDouble())
        fixedInt = (FixedInt) 180 - fixedInt;
      if (spell.type == CastType.Power)
        fixedInt += (FixedInt) Random.Range(-(float) anglevariance, (float) anglevariance);
      this.creature.game.serverState.busy = ServerState.Busy.Moving;
      if (secTarget == (Point) null)
        secTarget = new Point(-1000.0, -1000.0);
      ZSpell.FireWhich(spell, this.creature, this.creature.position, fixedInt, power, new MyLocation((int) target.x, (int) target.y), new MyLocation((int) secTarget.x, (int) secTarget.y));
    }

    public Table getEffectors(Script script)
    {
      Table effectors = new Table(script);
      int key = 1;
      foreach (ZEffector effector in this.creature.effectors)
      {
        if (!ZComponent.IsNull((ZComponent) effector))
        {
          effectors[(object) key] = (object) new ContainerEffector(effector);
          ++key;
        }
      }
      foreach (ZEffector destroyableEffector in this.creature.destroyableEffectors)
      {
        if (!ZComponent.IsNull((ZComponent) destroyableEffector))
        {
          effectors[(object) key] = (object) new ContainerEffector(destroyableEffector);
          ++key;
        }
      }
      return effectors;
    }

    public Table getEffectors(Script script, bool destroyable)
    {
      Table effectors = new Table(script);
      int key = 1;
      if (!destroyable)
      {
        foreach (ZEffector effector in this.creature.effectors)
        {
          if (!ZComponent.IsNull((ZComponent) effector))
          {
            effectors[(object) key] = (object) new ContainerEffector(effector);
            ++key;
          }
        }
      }
      if (destroyable)
      {
        foreach (ZEffector destroyableEffector in this.creature.destroyableEffectors)
        {
          if (!ZComponent.IsNull((ZComponent) destroyableEffector))
          {
            effectors[(object) key] = (object) new ContainerEffector(destroyableEffector);
            ++key;
          }
        }
      }
      return effectors;
    }

    public override bool Equals(object obj)
    {
      return !(typeof (ContainerCreature) != obj?.GetType()) && (ZComponent) this.creature == (object) ((ContainerCreature) obj).creature;
    }

    public override int GetHashCode() => this._hashCode;

    public static bool operator ==(ContainerCreature v, ContainerCreature x)
    {
      return (object) x == null ? (object) v == null : (ZComponent) v.creature == (object) x.creature;
    }

    public static bool operator !=(ContainerCreature v, ContainerCreature x)
    {
      return (object) x == null ? v != null : (ZComponent) v.creature != (object) x.creature;
    }
  }
}
