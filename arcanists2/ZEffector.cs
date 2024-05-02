// Decompiled with JetBrains decompiler
// Type: ZEffector
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZEffector : ZComponent
{
  public Effector clientObj;
  public Effector baseEffector;
  public int id;
  public const int Pickup_Coin = 0;
  public const int Pickup_Music = 1;
  private ZGame _game;
  public ZMyCollider collider;
  private MyLocation _position;
  [NonSerialized]
  public bool followParent;
  public bool halved;
  public EffectorType type;
  public ZCreature whoSummoned;
  public ZCreature infector;
  public ZEffector partner;
  public bool gameObjectActive = true;
  public bool active = true;
  private int turnsAlive;
  public int MaxTurnsAlive = 5;
  public int delayDeath;
  public int variable;
  public int clientSpriteIndex;
  internal ZSpell spell;
  internal bool fromArmageddon;
  internal bool doNotCreateObjectOnResync;
  internal FixedInt rotation = (FixedInt) 0;
  private IEnumerator<float> coro;

  internal bool dead => this.isNull;

  public ZGame game
  {
    get => this._game;
    set
    {
      this._game = value;
      this.TurnCreated = this._game.turn;
      if (this.id != 0)
        return;
      this.id = ++this._game.nextEffectorID;
    }
  }

  public ZMyWorld world => this.game.world;

  public ZMap map => this.game.map;

  public AudioClip soundClip => this.baseEffector?.soundClip;

  public bool effectWhileInactive
  {
    get => (UnityEngine.Object) this.baseEffector != (UnityEngine.Object) null && this.baseEffector.effectWhileInactive;
  }

  public MyLocation positionResync
  {
    get
    {
      if ((ZComponent) this.collider != (object) null)
        return this.collider.position - new MyLocation(this.collider.offsetX, this.collider.offsetY);
      return this.followParent && (ZComponent) this.whoSummoned != (object) null ? this.whoSummoned.position : this._position;
    }
  }

  public MyLocation position
  {
    get
    {
      if ((ZComponent) this.collider != (object) null)
        return this.collider.position;
      return this.followParent && (ZComponent) this.whoSummoned != (object) null ? this.whoSummoned.position : this._position;
    }
    set
    {
      this._position = value;
      this.collider?.Move(value);
      if (!((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
        return;
      this.transform.position = (Vector3) value.ToSinglePrecision();
    }
  }

  public DamageType damageType
  {
    get
    {
      return !((UnityEngine.Object) this.baseEffector != (UnityEngine.Object) null) ? DamageType.None : this.baseEffector.damageType;
    }
  }

  public GameObject explosion => this.baseEffector?.explosion;

  public ZCreature GetInfector
  {
    get
    {
      return !((ZComponent) this.infector != (object) null) || this.infector.isDead ? this.whoSummoned : this.infector;
    }
  }

  public int TurnCreated { get; private set; }

  public int GetTurnsAlive() => this.turnsAlive;

  public void SetTurnsAlive(int i) => this.turnsAlive = i;

  internal static bool HasCreatureSerialization(EffectorType type)
  {
    return type == EffectorType.Butterfly_Jar || type == EffectorType.Forest_Seed || type == EffectorType.Grove_Renewal;
  }

  internal static SpellEnum FromType(EffectorType type)
  {
    switch (type)
    {
      case EffectorType.Forest_Seed:
        return SpellEnum.Forest_Seed;
      case EffectorType.Butterfly_Jar:
        return SpellEnum.Butterfly_Jar;
      case EffectorType.Grove_Renewal:
        return SpellEnum.Grove_Renewal;
      default:
        return SpellEnum.None;
    }
  }

  public void SetRotation()
  {
    if (!((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.rotation.ToFloat());
  }

  public void Serialize(myBinaryWriter writer, bool fromPartner = false)
  {
    if (this.game == null || this.game.xEffector == null || (ZComponent) this == (object) null)
      Debug.Log((object) ("YIKES! " + this.baseEffector.name + " " + (object) this.type + " " + (object) this.position + " " + this.dead.ToString() + " ID: " + (object) this.id));
    bool flag1 = this.game.xEffector.Contains(this);
    writer.Write(flag1);
    writer.Write(this.id);
    if (flag1)
      return;
    this.game.xEffector.Add(this);
    writer.Write((ZComponent) this.spell != (object) null);
    if ((ZComponent) this.spell != (object) null)
      GameSerializer.Serialize(this.spell, writer, true);
    writer.Write((UnityEngine.Object) this.baseEffector != (UnityEngine.Object) null ? this.baseEffector.name : "");
    writer.Write(this.positionResync);
    bool flag2 = typeof (ZEffectorStaticBall) == this.GetType();
    writer.Write(flag2);
    writer.Write(this.doNotCreateObjectOnResync);
    writer.Write((int) this.type);
    if (ZEffector.HasCreatureSerialization(this.type))
    {
      writer.Write(this.collider.creature.health);
      writer.Write(this.collider.creature.id);
      writer.Write(ZGame.GetID(this.whoSummoned));
      writer.Write(this.collider.creature.team);
    }
    if (flag2)
    {
      ZEffectorStaticBall zeffectorStaticBall = (ZEffectorStaticBall) this;
      writer.Write(zeffectorStaticBall.caughtSpells.Count);
      foreach (ZEffectorStaticBall.CaughtSpell caughtSpell in zeffectorStaticBall.caughtSpells)
      {
        writer.Write((ZComponent) caughtSpell.spell != (object) null);
        if ((ZComponent) caughtSpell.spell != (object) null)
        {
          GameSerializer.Serialize(caughtSpell.spell, writer, false);
          writer.Write(caughtSpell.offset);
        }
      }
    }
    writer.Write((ZComponent) this.collider != (object) null);
    this.collider?.Serialize(this.game, writer);
    writer.Write(this.followParent);
    writer.Write(this.halved);
    writer.Write(ZGame.GetID(this.whoSummoned));
    writer.Write(ZGame.GetID(this.infector));
    writer.Write((ZComponent) this.partner != (object) null ? this.partner.id : -1);
    writer.Write(this.gameObjectActive);
    writer.Write(this.active);
    writer.Write(this.turnsAlive);
    writer.Write(this.TurnCreated);
    writer.Write(this.MaxTurnsAlive);
    writer.Write(this.delayDeath);
    writer.Write(this.variable);
    writer.Write(this.clientSpriteIndex);
    writer.Write(ZGame.GetID(this.spell));
    writer.Write(this.fromArmageddon);
    writer.Write(this.rotation.RawValue);
    if (Inert._Version <= 57)
      return;
    writer.Write((ZComponent) this.partner != (object) null && !fromPartner && this.type != EffectorType.Fake_Portal);
    if (!((ZComponent) this.partner != (object) null) || fromPartner || this.type == EffectorType.Fake_Portal)
      return;
    this.partner.Serialize(writer, true);
  }

  public static ZEffector Deserialze(
    ZGame game,
    ZCreature creature,
    myBinaryReader reader,
    ZEffector replacement)
  {
    int num1 = reader.ReadBoolean() ? 1 : 0;
    int num2 = reader.ReadInt32();
    if (num1 != 0)
      return game.helper.GetEffector(num2);
    int num3 = reader.ReadBoolean() ? 1 : 0;
    ZSpell zspell1 = (ZSpell) null;
    if (num3 != 0)
      zspell1 = GameSerializer.DeserializeSpell(game, reader);
    ZEffector z = replacement ?? new ZEffector();
    Effector baseEffector = Inert.GetBaseEffector(reader.ReadString());
    MyLocation pos = reader.ReadMyLocation();
    bool flag1 = reader.ReadBoolean();
    bool flag2 = reader.ReadBoolean();
    EffectorType type = (EffectorType) reader.ReadInt32();
    if (ZEffector.HasCreatureSerialization(type))
    {
      int num4 = reader.ReadInt32();
      int num5 = reader.ReadInt32();
      int id = reader.ReadInt32();
      int num6 = reader.ReadInt32();
      creature = game.helper.GetCreature(id);
      int num7 = (ZComponent) creature == (object) null ? 1 : 0;
      if ((ZComponent) creature == (object) null)
      {
        ZCreature zcreature = new ZCreature();
        zcreature.game = game;
        zcreature.parent = game.players[0];
        zcreature.team = 0;
        creature = zcreature;
      }
      ZCreature creature1 = ZCreatureCreate.CreateCreature(creature.parent, Inert.GetSpell(ZEffector.FromType(type)).toSummon.GetComponent<Creature>(), pos.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform());
      creature1.game = game;
      creature1.id = num5;
      game.helper.creatureID.Add(creature1.id, creature1);
      creature1.position = pos;
      creature1.team = num6;
      creature1.health = num4;
      ZEffector auraOfDecay = creature1.auraOfDecay;
      auraOfDecay.game = creature1.game;
      auraOfDecay.active = false;
      auraOfDecay.whoSummoned = creature;
      auraOfDecay.collider = creature1.collider;
      auraOfDecay.collider.world = auraOfDecay.world;
      auraOfDecay.collider.Initialize(pos, game.world);
      auraOfDecay.collider.creature = creature1;
      creature1.parent = creature.parent;
      z = auraOfDecay;
      if (auraOfDecay.type == EffectorType.Butterfly_Jar && (UnityEngine.Object) creature1.transform != (UnityEngine.Object) null)
        creature1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = creature.parent.clientColor;
      if (num7 != 0 && creature != null)
        creature.SetNull();
    }
    else if (flag2 && (ZComponent) creature != (object) null)
    {
      ZEffector zeffector = creature.effectors.Find((Predicate<ZEffector>) (q => (ZComponent) q != (object) null && q.type == type));
      if ((ZComponent) zeffector != (object) null)
        z = zeffector;
    }
    else if ((ZComponent) zspell1 != (object) null && (ZComponent) zspell1.effector != (object) null && (ZComponent) replacement == (object) null)
      z = zspell1.effector;
    else if ((UnityEngine.Object) baseEffector != (UnityEngine.Object) null && (ZComponent) replacement == (object) null)
    {
      if (flag1)
      {
        ZEffectorStaticBall zeffectorStaticBall = ZEffector.CreateStatic(game, baseEffector, (Vector3) pos.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform());
        z = (ZEffector) zeffectorStaticBall;
        int num8 = reader.ReadInt32();
        for (int index = 0; index < num8; ++index)
        {
          if (reader.ReadBoolean())
          {
            ZSpell zspell2 = GameSerializer.DeserializeSpell(game, reader);
            zeffectorStaticBall.caughtSpells.Add(new ZEffectorStaticBall.CaughtSpell()
            {
              spell = zspell2,
              offset = reader.ReadMyLocation()
            });
          }
        }
      }
      else
        z = ZEffector.Create(game, baseEffector, (Vector3) pos.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform());
    }
    z.game = game;
    if (reader.ReadBoolean())
    {
      if ((ZComponent) z.collider == (object) null)
        z.collider = new ZMyCollider();
      ZMyCollider.Deserialize(ref z.collider, game, reader);
    }
    z._position = pos;
    z.id = num2;
    z.type = type;
    z.doNotCreateObjectOnResync = flag2;
    z.followParent = reader.ReadBoolean();
    z.halved = reader.ReadBoolean();
    int i1 = reader.ReadInt32();
    int i2 = reader.ReadInt32();
    int i3 = reader.ReadInt32();
    z.gameObjectActive = reader.ReadBoolean();
    z.active = reader.ReadBoolean();
    z.turnsAlive = reader.ReadInt32();
    z.TurnCreated = reader.ReadInt32();
    z.MaxTurnsAlive = reader.ReadInt32();
    z.delayDeath = reader.ReadInt32();
    z.variable = reader.ReadInt32();
    z.clientSpriteIndex = reader.ReadInt32();
    int i4 = reader.ReadInt32();
    z.fromArmageddon = reader.ReadBoolean();
    z.rotation = (FixedInt) reader.ReadInt64();
    game.helper.effector_whoSummoned.Add(new ZGame.IDEffector(z, i1));
    game.helper.effector_infector.Add(new ZGame.IDEffector(z, i2));
    game.helper.effector_partners.Add(new ZGame.IDEffector(z, i3));
    game.helper.effector_spell.Add(new ZGame.IDEffector(z, i4));
    game.helper.effectorID.Add(num2, z);
    if (Inert._Version > 57 && reader.ReadBoolean())
      ZEffector.Deserialze(game, creature, reader, (ZEffector) null);
    if (z.type == EffectorType.Prickly_Barrier && game.isClient && (UnityEngine.Object) z.transform != (UnityEngine.Object) null)
    {
      Color clientColor = creature.parent.clientColor with
      {
        a = 0.2f
      };
      z.transform.GetChild(1).GetComponent<SpriteRenderer>().color = clientColor;
    }
    return z;
  }

  public static ZEffector Create(
    ZGame game,
    Effector e,
    Vector3 position,
    Quaternion q,
    Transform parent = null)
  {
    ZEffector zeffector = new ZEffector();
    zeffector.game = game;
    zeffector.id = ++game.nextEffectorID;
    zeffector.Copy(e, game);
    zeffector.CreateClient(e, position, q, parent);
    return zeffector;
  }

  public static ZEffectorStaticBall CreateStatic(
    ZGame game,
    Effector e,
    Vector3 position,
    Quaternion q,
    Transform parent)
  {
    ZEffectorStaticBall zeffectorStaticBall = new ZEffectorStaticBall();
    zeffectorStaticBall.game = game;
    zeffectorStaticBall.Copy(e, game);
    zeffectorStaticBall.CreateClient(e, position, q, parent);
    return zeffectorStaticBall;
  }

  private void CreateClient(Effector e, Vector3 position, Quaternion q, Transform parent)
  {
    Effector effector = UnityEngine.Object.Instantiate<Effector>(e, position, q, parent);
    this.clientObj = effector;
    this.clientObj.serverObj = this;
    this.gameObject = effector.gameObject;
    this.transform = effector.transform;
    effector.serverObj = this;
  }

  public void Copy(Effector c, ZGame game)
  {
    this.baseEffector = c;
    this.MaxTurnsAlive = c.MaxTurnsAlive;
    this.delayDeath = c.delayDeath;
    this.variable = c.variable;
    this.type = c.type;
    if (!((UnityEngine.Object) c.collider != (UnityEngine.Object) null))
      return;
    this.collider = new ZMyCollider();
    this.collider.id = ++game.nextColliderID;
    this.collider.Copy(c.collider);
    this.collider.world = game.world;
    this.collider.effector = this;
  }

  public void Clone(ZGame game, ZEffector c, ZCreature parent)
  {
    this.id = ++game.nextEffectorID;
    this.baseEffector = c.baseEffector;
    this.MaxTurnsAlive = c.MaxTurnsAlive;
    this.delayDeath = c.delayDeath;
    this.variable = c.variable;
    this.type = c.type;
    this.active = c.active;
    this.fromArmageddon = c.fromArmageddon;
    this.rotation = c.rotation;
    this.followParent = c.followParent;
    this.gameObjectActive = c.gameObjectActive;
    this.game = game;
    this.whoSummoned = parent;
    if ((ZComponent) c.collider != (object) null)
    {
      this.collider = new ZMyCollider();
      this.collider.Clone(c.collider, game);
      this.collider.effector = this;
      this.collider.world = game.world;
    }
    int num = (UnityEngine.Object) c.baseEffector != (UnityEngine.Object) null ? 1 : 0;
  }

  public bool KillsHost() => this.type == EffectorType.DieNSpawnVoid;

  public bool MapTransform()
  {
    switch (this.type)
    {
      case EffectorType.Glyph:
      case EffectorType.Flame_Wall:
      case EffectorType.Flame_WallBase:
      case EffectorType.Natures_Wrath:
      case EffectorType.Naplem:
      case EffectorType.Portal:
      case EffectorType.Aura_of_decay:
      case EffectorType.Volcano:
      case EffectorType.Storm:
      case EffectorType.Star_Fall:
      case EffectorType.Maelstrom:
      case EffectorType.Blizzard:
      case EffectorType.Fissure:
      case EffectorType.Frost_Giant_Blizzard:
      case EffectorType.Soul_Jar:
      case EffectorType.Rising_Star:
      case EffectorType.Forest_Seed:
      case EffectorType.Target:
      case EffectorType.Electrostatic_Charge:
      case EffectorType.Life_Dew:
      case EffectorType.Miner_Map:
      case EffectorType.Wormhole:
      case EffectorType.AutumnLeaves:
      case EffectorType.Grove_Renewal:
        return true;
      case EffectorType.Static_Ball:
        return !this.followParent;
      case EffectorType.Conductor_Rod:
        return (ZComponent) this.spell.parent == (object) null;
      default:
        return false;
    }
  }

  public bool Destroyable()
  {
    switch (this.type)
    {
      case EffectorType.Glyph:
        return true;
      case EffectorType.Fire_Shield:
        return true;
      case EffectorType.Flame_Wall:
        return false;
      case EffectorType.Flame_WallBase:
        return false;
      case EffectorType.Wind_Shield:
        return true;
      case EffectorType.Natures_Wrath:
        return false;
      case EffectorType.Naplem:
        return false;
      case EffectorType.Portal:
      case EffectorType.Wormhole:
        return true;
      case EffectorType.Storm_Shield:
        return true;
      case EffectorType.Aura_of_decay:
        return true;
      case EffectorType.Arcane_Energizer:
        return false;
      case EffectorType.Volcano:
        return false;
      case EffectorType.Storm:
        return false;
      case EffectorType.Star_Fall:
        return false;
      case EffectorType.Maelstrom:
        return false;
      case EffectorType.Blizzard:
        return false;
      case EffectorType.Fissure:
        return false;
      case EffectorType.Frost_Giant_Blizzard:
        return false;
      case EffectorType.Lich_Aura_of_decay:
        return true;
      case EffectorType.Soul_Jar:
        return false;
      case EffectorType.Rising_Star:
        return false;
      case EffectorType.Forest_Seed:
      case EffectorType.Grove_Renewal:
        return false;
      case EffectorType.Sanctuary:
        return false;
      case EffectorType.Elf:
        return false;
      case EffectorType.Target:
        return false;
      case EffectorType.Static_Ball:
        return true;
      case EffectorType.Spirit_Shield:
        return true;
      case EffectorType.Dragon_Aura_of_Decay:
        return true;
      case EffectorType.Vortex:
        return true;
      case EffectorType.Magical_Barrier:
        return true;
      case EffectorType.Conductor_Rod:
        return false;
      case EffectorType.Electrostatic_Charge:
        return false;
      case EffectorType.Life_Dew:
        return true;
      case EffectorType.Miner_Map:
        return false;
      default:
        return false;
    }
  }

  public void Destroy(float t)
  {
    this.collider?.Disable();
    this.active = false;
    this.isNull = true;
    ZComponent.Destroy<GameObject>(this.gameObject, t);
  }

  public virtual void Die(int indexInParent, bool destroyable, bool global)
  {
    if (this.dead)
      return;
    if (this.type == EffectorType.AutumnLeaves && this.spell is ZSpellLeaf spell)
      spell.MoveOtherLeaves2();
    this.isNull = true;
    this.active = false;
    if (this.type == EffectorType.Forest_Seed || this.type == EffectorType.Grove_Renewal)
    {
      if ((ZComponent) this.collider != (object) null && (ZComponent) this.collider.creature != (object) null)
        this.collider.creature.health = 1;
      this.collider?.creature?.ApplyDamage(SpellEnum.Fire_Ball, DamageType.None, 100, (ZCreature) null, 0);
    }
    else
    {
      if (this.type == EffectorType.Apparition)
      {
        if (!ZComponent.IsNull((ZComponent) this.whoSummoned))
        {
          this.whoSummoned.effectors.RemoveAt(indexInParent);
          this.whoSummoned.phantom = false;
          this.whoSummoned.flying = false;
          this.whoSummoned.collider.gameObjectLayer = 8;
          this.whoSummoned.collider.layer = 256;
          if (this.whoSummoned.ShouldFall())
            this.whoSummoned.Fall();
          ZComponent.Destroy<GameObject>(this.gameObject);
          if (!this.game.isClient)
            return;
          this.whoSummoned.gameObject?.GetComponent<ColorLerp>()?.Kill();
          return;
        }
      }
      else if (this.type == EffectorType.Glide)
      {
        if (!ZComponent.IsNull((ZComponent) this.whoSummoned))
          this.whoSummoned.gliding = false;
      }
      else if (this.type == EffectorType.Social_Distancing)
      {
        if (!ZComponent.IsNull((ZComponent) this.whoSummoned))
          this.whoSummoned.socialDistancing = false;
      }
      else if (this.type == EffectorType.Duplicate)
      {
        if (!ZComponent.IsNull((ZComponent) this.whoSummoned))
        {
          if (this.whoSummoned.familiarLevelIllusion == 5)
            return;
          this.whoSummoned.OnDeath(true);
          return;
        }
      }
      else if (this.type == EffectorType.Flame_Wall)
        this.spell.isDead = true;
      else if (this.type == EffectorType.Retribution && (ZComponent) this.whoSummoned != (object) null)
        this.whoSummoned.retribution = 0;
      if ((ZComponent) this.collider != (object) null)
        this.collider.Disable();
      if ((ZComponent) this.partner != (object) null && indexInParent >= 0 && this.type != EffectorType.Fake_Portal)
        this.partner.Die(-1, false, global);
      if (ZComponent.IsNull((ZComponent) this.whoSummoned))
      {
        if (this.delayDeath > 0)
          this.Destroy((float) this.delayDeath);
        else
          this.DestroyNoExplosion();
      }
      else
      {
        if (!global && indexInParent > -1)
        {
          if (destroyable)
            this.whoSummoned.destroyableEffectors.RemoveAt(indexInParent);
          else
            this.whoSummoned.effectors.RemoveAt(indexInParent);
        }
        if (this.delayDeath > 0)
        {
          if (this.game != null && this.game.isClient && (UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
          {
            ParticleSystem[] componentsInChildren = this.gameObject.GetComponentsInChildren<ParticleSystem>();
            if (componentsInChildren != null)
            {
              foreach (ParticleSystem particleSystem in componentsInChildren)
                particleSystem.Stop();
            }
          }
          this.Destroy((float) this.delayDeath);
        }
        else
          this.DestroyNoExplosion();
      }
    }
  }

  public void SetWindShieldActive()
  {
    if (this.active)
      return;
    this.active = true;
    this.VisualUpdate();
  }

  public void TurnPassed(int indexInParent, bool destroyable, bool global)
  {
    if (this.dead)
      return;
    ++this.turnsAlive;
    if (this.turnsAlive >= this.MaxTurnsAlive && this.MaxTurnsAlive > 0)
      this.Die(indexInParent, destroyable, global);
    else if (!this.active || this.type == EffectorType.Prickly_Barrier || (this.type == EffectorType.Storm_Shield || this.type == EffectorType.Troll) && this.variable > 0)
    {
      this.active = true;
      switch (this.type)
      {
        case EffectorType.Fire_Shield:
        case EffectorType.Spirit_Shield:
          this.VisualUpdate();
          break;
        case EffectorType.Flame_Wall:
          this.VisualUpdate();
          break;
        case EffectorType.Flame_WallBase:
          if (!this.world.OverlapCircle((Point) this.collider.position, 10, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom))
            break;
          this.EffectCreature((ZCreature) null, true);
          break;
        case EffectorType.Wind_Shield:
          this.VisualUpdate();
          break;
        case EffectorType.Portal:
        case EffectorType.Wormhole:
          if (indexInParent > -1 && this.partner.variable != 666)
          {
            ++this.partner.turnsAlive;
            this.partner.active = true;
          }
          this.CheckPortal();
          break;
        case EffectorType.Storm_Shield:
        case EffectorType.Troll:
        case EffectorType.Myth:
          if (this.type == EffectorType.Storm_Shield)
          {
            this.variable = 25;
            this.VisualUpdate();
          }
          this.Logic_Storm_Shield();
          break;
        case EffectorType.Aura_of_decay:
          this.EffectCreature((ZCreature) null, true);
          break;
        case EffectorType.Volcano:
          this.game.ongoing.RunSpell(this.IEVolcano());
          break;
        case EffectorType.Storm:
          this.active = false;
          MyLocation position1 = this.position;
          if (this.turnsAlive == 1)
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorStorm(this.whoSummoned, position1, -100, 100, 9));
          else if (this.turnsAlive == 2)
          {
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorStorm(this.whoSummoned, position1, -100, 100, 7));
          }
          else
          {
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorStorm(this.whoSummoned, position1, -100, 100, 9));
            this.Die(indexInParent, false, global);
          }
          ZEffector.RechargeElectrostaticCharges(this.game);
          break;
        case EffectorType.Maelstrom:
          this.game.ongoing.RunSpell(this.IEMaelStrom());
          break;
        case EffectorType.Blizzard:
          this.active = false;
          MyLocation position2 = this.position;
          position2.y -= 50;
          this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, Inert.Instance.Snow, position2, -125, 125, 100, true));
          if (this.turnsAlive != 5)
            break;
          this.Die(indexInParent, false, global);
          break;
        case EffectorType.Fissure:
          this.game.ongoing.RunSpell(this.IEFissure());
          break;
        case EffectorType.Frost_Giant_Blizzard:
          this.active = false;
          this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, Inert.Instance.Snow2, new MyLocation(this.whoSummoned.position.x, (FixedInt) (this.map.Height + 300)), -100, 100, 40, true));
          break;
        case EffectorType.Lich_Aura_of_decay:
        case EffectorType.Dragon_Aura_of_Decay:
          if (!ZComponent.IsNull((ZComponent) this.whoSummoned) && (ZComponent) this.whoSummoned.tower != (object) null && this.damageType == DamageType.Infection)
          {
            if (this.variable == this.whoSummoned.parent.localTurn)
            {
              this.active = false;
              --this.turnsAlive;
              break;
            }
            ++this.MaxTurnsAlive;
          }
          this.EffectCreature((ZCreature) null, true);
          if (ZEffector.InSanctuary(this.world, this.position))
          {
            this.active = false;
            this.gameObjectActive = false;
            if (this.game.isClient && (UnityEngine.Object) this.transform != (UnityEngine.Object) null)
            {
              this.gameObject.SetActive(false);
              this.VisualUpdate();
              ZEffector.SpawnVineExplosion(this.transform.position);
            }
          }
          else if ((ZComponent) this.collider != (object) null && !this.collider.enabled)
            this.collider.Enable(this.position);
          if (this.damageType != DamageType.Infection || this.turnsAlive < this.MaxTurnsAlive - 1)
            break;
          this.Die(indexInParent, destroyable, global);
          break;
        case EffectorType.Rising_Star:
          this.active = false;
          if (this.turnsAlive == 1)
          {
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorRisingStar(this.whoSummoned, Inert.Instance.risingStarNormal, this.position, -150, 150, (FixedInt) 30, 1));
            break;
          }
          if (this.turnsAlive == 2)
          {
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorRisingStar(this.whoSummoned, Inert.Instance.risingStarNormal, this.position, -150, 150, (FixedInt) 30, 2));
            break;
          }
          if (this.turnsAlive == 3)
          {
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorRisingStar(this.whoSummoned, Inert.Instance.risingStarNormal, this.position, -150, 150, (FixedInt) 30, 3));
            break;
          }
          if (this.turnsAlive == 4)
          {
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorRisingStar(this.whoSummoned, Inert.Instance.risingStarNormal, this.position, -150, 150, (FixedInt) 30, 4));
            break;
          }
          this.game.ongoing.RunSpell(ZSpell.IEnumeratorRisingStar(this.whoSummoned, Inert.Instance.risingStarHuge, this.position, 0, 0, (FixedInt) 0, 1));
          this.Die(indexInParent, false, global);
          break;
        case EffectorType.Forest_Seed:
          this.active = false;
          if (!ZComponent.IsNull((ZComponent) this.whoSummoned) && this.whoSummoned.parent.localTurn <= this.variable)
            break;
          ++this.variable;
          this.collider.creature.ApplyDamage(SpellEnum.Fire_Ball, DamageType.None, 15, (ZCreature) null, 0);
          break;
        case EffectorType.Elf:
          this.active = false;
          if (ZComponent.IsNull((ZComponent) this.game.targetEffector))
            break;
          this.game.ongoing.RunSpell(ZSpell.IEArrows(this.whoSummoned, Inert.Instance.Arrow, this.game.targetEffector.position, true));
          break;
        case EffectorType.Vine_Bloom:
          MyLocation position3 = this.whoSummoned.position;
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomThorn, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomThorn, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomThorn, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomThorn, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomVine, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomVine, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomVine, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.SetVineThorn66(ZSpell.BaseFire(Inert.Instance.VineBloomVine, this.whoSummoned, position3, Quaternion.identity, Inert.Velocity(this.game.RandomFixedInt(45, 135), this.game.RandomInt(15, 25))));
          this.whoSummoned.OnDeath(true);
          break;
        case EffectorType.English_Summer:
        case EffectorType.Acid_Rain:
          this.active = false;
          MyLocation position4 = this.position;
          position4.y -= 55;
          if (this.type == EffectorType.Acid_Rain)
            position4.y -= 55;
          else
            position4.y -= 55;
          Spell spell1 = this.type == EffectorType.English_Summer ? Inert.Instance.rain : Inert.Instance.acidRain;
          if (this.turnsAlive == 1)
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, spell1, position4, -150, 150, 10));
          if (this.turnsAlive == 2)
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, spell1, position4, -150, 150, 21));
          if (this.turnsAlive == 3)
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, spell1, position4, -150, 150, 32));
          if (this.turnsAlive == 4)
            this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, spell1, position4, -150, 150, 55));
          if (this.turnsAlive != 5)
            break;
          this.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(this.whoSummoned, spell1, position4, -150, 150, 75));
          this.Die(indexInParent, false, global);
          break;
        case EffectorType.Clockwork_Bomb:
          this.active = false;
          if (!ZComponent.IsNull((ZComponent) this.whoSummoned) && this.whoSummoned.parent.localTurn <= this.variable || !this.collider.enabled)
            break;
          ++this.variable;
          if (this.game.RandomInt(0, 3) != 1 && this.variable < 5 && (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.familiarLevelCogs != 5))
            break;
          this.collider.Disable(false);
          this.spell.OnDeath(true);
          this.game.forceRysncPause = true;
          this.DestroyNoExplosion();
          break;
        case EffectorType.Monkey:
          this.active = false;
          if (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.parent == null || this.whoSummoned.parent.controlled.Count <= 0)
            break;
          if (this.game.gameFacts.GetStyle().HasStyle(GameStyle.Zombie_Monkey))
          {
            this.game.ongoing.RunSpell(ZSpell.IEArrows(this.whoSummoned, Inert.Instance.spells["Thorn Bomb"], this.whoSummoned.parent.controlled[0].position, true, 1));
            break;
          }
          this.game.ongoing.RunSpell(ZSpell.IEArrows(this.whoSummoned, Inert.Instance.thornBall, this.whoSummoned.parent.controlled[0].position, true, 1));
          break;
        case EffectorType.Conductor_Rod:
          MyLocation pos1 = (ZComponent) this.spell.parent != (object) null ? this.spell.parent.position : this.position;
          int y1 = (int) pos1.y;
          pos1.y = (FixedInt) this.map.Height;
          int amount = !ZComponent.IsNull((ZComponent) this.whoSummoned) ? (this.whoSummoned.familiarLevelStorm + 1) / 2 + 1 : 1;
          this.game.ongoing.RunSpell(ZSpell.IEnumeratorLightningStrike(this.whoSummoned, pos1, -30, 30, amount, true, (int) pos1.x, y1, this.spell.parent));
          this.active = false;
          if (this.turnsAlive != 2)
            break;
          this.Die(indexInParent, destroyable, global);
          break;
        case EffectorType.Electrostatic_Charge:
          if (this.variable > 0)
            break;
          this.active = false;
          break;
        case EffectorType.Life_Gain:
          this.active = false;
          if (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.health >= this.whoSummoned.maxHealth)
            break;
          this.whoSummoned.DoHeal(this.variable);
          if (this.whoSummoned.health > this.whoSummoned.maxHealth)
            this.whoSummoned.health = this.whoSummoned.maxHealth;
          this.whoSummoned.UpdateHealthTxt();
          break;
        case EffectorType.Call_of_the_Void:
          this.active = false;
          this.variable += 25;
          int num1 = 64;
          if (this.variable == 50)
            num1 = 136;
          else if (this.variable == 75)
            num1 = 208;
          else if (this.variable == 100)
            num1 = 280;
          else if (this.variable == 125)
            num1 = 355;
          ZSpell.ApplyExplosionForce(SpellEnum.Call_of_the_Void, this.world, this.position, this.variable, Curve.None, 10, num1, (FixedInt) 1, DamageType.None, this.whoSummoned, this.game.turn);
          if (this.game.isClient && (UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
          {
            this.game.map.CallOfTheVoid((int) this.position.x, (int) this.position.y, num1);
            ParticleSystem.ShapeModule shape = this.gameObject.GetComponent<ParticleSystem>().shape with
            {
              radius = (float) (num1 - 44)
            };
            ParticleSystem component = this.transform.GetChild(0).GetComponent<ParticleSystem>();
            shape = component.shape with
            {
              radius = (float) num1
            };
            component.emission.rateOverTime = (ParticleSystem.MinMaxCurve) (float) num1;
          }
          if (this.variable == 75 && (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.parent.localTurn < 20))
          {
            this.Die(indexInParent, false, false);
            break;
          }
          if (this.variable == 100 && (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.parent.localTurn < 30))
          {
            this.Die(indexInParent, false, false);
            break;
          }
          if (this.variable != 125)
            break;
          this.Die(indexInParent, false, false);
          break;
        case EffectorType.Dark_Totem:
          this.active = false;
          this.map.ServerBitBlt(13, (int) this.position.x, (int) this.position.y);
          ZSpell.ApplyExplosionForce(SpellEnum.Dark_Totem, this.world, this.position, 0, Curve.None, 10, 75, (FixedInt) 0, DamageType.None, this.whoSummoned, this.game.turn);
          break;
        case EffectorType.DieNSpawnVoid:
          this.active = false;
          if (this.turnsAlive != 3 || ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.isDead)
            break;
          Spell s;
          if (Inert.Instance.TryGetSpell("Call of the Void", out s))
            ZSpell.FireCallOfTheVoid(s, this.whoSummoned.daOriginalTrueParent?.first(), this.whoSummoned.position);
          this.whoSummoned.OnDeath(true);
          break;
        case EffectorType.Acorn:
          this.active = false;
          this.game.map.ServerBitBlt(55, (int) this.position.x + 11, (int) this.position.y + 73, false);
          if (this.turnsAlive == 1 && !ZComponent.IsNull((ZComponent) this.whoSummoned) && this.whoSummoned.parent.GetMaxMinions() > this.whoSummoned.parent.GetMinionCount())
          {
            MyLocation target = this.position + new MyLocation(-8, 85);
            ZCreature zcreature = ZSpell.FireSummon(Inert.GetSpell("Summon Beehive"), this.game, this.whoSummoned, target);
            if ((ZComponent) zcreature != (object) null && !zcreature.isDead)
              zcreature.spells[0].SetTurnFired = zcreature.parent.localTurn;
          }
          if (this.turnsAlive < 3)
            break;
          this.Die(indexInParent, false, global);
          break;
        case EffectorType.Morning_Sun:
          if (this.game.isClient)
            this.ToggleMorningSun();
          this.spell.MoveMorningSun((int) this.position.y, (int) this.position.y);
          break;
        case EffectorType.Remove_Four_Seasons:
          this.active = false;
          if (!ZComponent.IsNull((ZComponent) this.whoSummoned) && (this.whoSummoned.familiarLevelSeasons == 5 || this.turnsAlive < 5 + this.whoSummoned.familiarLevelSeasons))
            break;
          if (!ZComponent.IsNull((ZComponent) this.whoSummoned) && !this.whoSummoned.isDead)
          {
            for (int index = this.whoSummoned.spells.Count - 1; index >= 0; --index)
            {
              if (this.whoSummoned.spells[index].spell.spellEnum == SpellEnum.Seasonal)
              {
                this.whoSummoned.spells.RemoveAt(index);
                break;
              }
            }
          }
          this.isNull = true;
          ZComponent.Destroy<GameObject>(this.gameObject);
          break;
        case EffectorType.Prickly_Barrier:
          if ((ZComponent) this.whoSummoned == (object) null)
            break;
          this.VisualUpdate();
          if (this.whoSummoned.parent.localTurn == this.variable)
          {
            this.active = false;
            this.VisualUpdate();
            ZFlameWallSpell zflameWallSpell = ZSpell.FireFlameWall(Inert.GetSpell(SpellEnum.Prickly_Barrier), this.whoSummoned, new MyLocation((FixedInt) this.game.RandomInt(-150, 150) + this.position.x, (FixedInt) this.game.RandomInt(-150, 150) + this.position.y), (FixedInt) 0, (FixedInt) 0, this.active);
            zflameWallSpell.effector.turnsAlive = this.turnsAlive;
            zflameWallSpell.effector2.turnsAlive = this.turnsAlive;
            break;
          }
          List<ZMyCollider> zmyColliderList1 = this.world.OverlapCircleAll((Point) this.collider.position, this.collider.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
          if (zmyColliderList1.Count > 1)
            zmyColliderList1.Sort((Comparison<ZMyCollider>) ((a, b) => MyLocation.FastDistance(a.position, this.position) - MyLocation.FastDistance(b.position, this.position)));
          foreach (ZMyCollider zmyCollider in zmyColliderList1)
            this.EffectCreature((ZComponent) zmyCollider.tower != (object) null ? zmyCollider.tower.creature : zmyCollider.creature, true);
          ZFlameWallSpell zflameWallSpell1 = ZSpell.FireFlameWall(Inert.GetSpell(SpellEnum.Prickly_Barrier), this.whoSummoned, new MyLocation((FixedInt) this.game.RandomInt(-150, 150) + this.position.x, (FixedInt) this.game.RandomInt(-150, 150) + this.position.y), (FixedInt) 0, (FixedInt) 0, this.active);
          zflameWallSpell1.effector.turnsAlive = this.turnsAlive;
          zflameWallSpell1.effector2.turnsAlive = this.turnsAlive;
          break;
        case EffectorType.Bear:
          this.active = false;
          if (this.turnsAlive < 5)
            break;
          this.isNull = true;
          if (!((ZComponent) this.whoSummoned != (object) null))
            break;
          if ((UnityEngine.Object) this.whoSummoned.transform != (UnityEngine.Object) null)
          {
            ZComponent.Destroy<GameObject>(this.whoSummoned.werewolfObj);
            int childCount = this.whoSummoned.transform.GetChild(0).childCount;
            for (int index = 2; index < childCount; ++index)
              this.whoSummoned.transform.GetChild(0).GetChild(index).gameObject.SetActive(true);
            this.whoSummoned.animator = this.whoSummoned.gameObject.GetComponent<IAnimator>();
          }
          this.whoSummoned.race = this.whoSummoned.baseCreature.race;
          this.whoSummoned.type = this.whoSummoned.baseCreature.type;
          this.whoSummoned.RemoveSpell(SpellEnum.Rampage);
          this.whoSummoned.RemoveSpell(SpellEnum.Swipe);
          this.whoSummoned.UpdateHealthTxt();
          this.whoSummoned.SetRadius(this.whoSummoned.TrueSize);
          this.whoSummoned.HighJumpData.y -= 2;
          this.whoSummoned.LongJumpData.x -= 2;
          this.whoSummoned.TrueSize = -1;
          this.whoSummoned.game.CreatureMoveSurroundings(this.whoSummoned.position, this.whoSummoned.radius);
          Player.Instance?.UpdateVisuals();
          break;
        case EffectorType.DarkBomb:
          ZSpell zspell = ZSpell.FireGenericReturn(Inert.GetSpell(SpellEnum.Dark_Matter_Bomb), this.whoSummoned, this.position, (FixedInt) 90 + this.game.RandomFixedInt(-5, 5), (FixedInt) 0);
          zspell.amount = 666;
          zspell.maxDuration = 60;
          this.Die(indexInParent, destroyable, global);
          break;
        case EffectorType.Shooting_Stars:
          this.game.ongoing.RunSpell(this.IEShootingStars());
          break;
        case EffectorType.Collision_Course:
          this.active = false;
          MyLocation pos2 = (ZComponent) this.spell.parent != (object) null ? this.spell.parent.position : this.position;
          int y2 = (int) pos2.y;
          pos2.y = (FixedInt) (this.map.Height + 1000);
          ZSpell.BaseFire(Inert.GetSpell(SpellEnum.Starfire), this.whoSummoned, pos2, Quaternion.identity, new MyLocation(0, -40), isChild: true);
          if (this.turnsAlive != 2)
            break;
          this.Die(indexInParent, destroyable, global);
          break;
        case EffectorType.Supernova:
          this.game.ongoing.RunSpell(this.IESupernova());
          break;
        case EffectorType.GravityWell:
          this.VisualUpdate();
          break;
        case EffectorType.Boar_Charge:
          this.active = false;
          if ((ZComponent) this.whoSummoned == (object) null)
            break;
          if (this.whoSummoned.team == 24)
          {
            if (this.variable > this.whoSummoned.daOriginalTrueParent.localTurn)
              break;
            this.variable = this.whoSummoned.daOriginalTrueParent.localTurn + 1;
          }
          List<ZMyCollider> zmyColliderList2;
          if (this.whoSummoned.team != 24)
          {
            zmyColliderList2 = this.game.world.OverlapCircleAll(new Point(this.position), 750, this.whoSummoned.collider, Inert.mask_entity_movement | Inert.mask_Phantom);
          }
          else
          {
            zmyColliderList2 = new List<ZMyCollider>();
            zmyColliderList2.Add(this.game.ClosestCreature(this.whoSummoned, false)?.collider);
          }
          List<ZMyCollider> zmyColliderList3 = zmyColliderList2;
          zmyColliderList3.Sort((Comparison<ZMyCollider>) ((a, b) => MyLocation.Distance(a.position, this.whoSummoned.position) - MyLocation.Distance(b.position, this.whoSummoned.position)));
          for (int index = 0; index < zmyColliderList3.Count; ++index)
          {
            ZCreature creature = zmyColliderList3[index].creature;
            if ((ZComponent) creature == (object) null && (ZComponent) zmyColliderList3[index].tower != (object) null)
              creature = zmyColliderList3[index].tower.creature;
            if ((ZComponent) creature != (object) null && creature.parent.team != this.whoSummoned.team)
            {
              Spell spell2 = Inert.GetSpell(SpellEnum.Mine);
              ZSpell.FireMine(spell2, this.whoSummoned, creature.position, -(spell2.maxDuration / 2), true);
              this.whoSummoned.game.ongoing.RunCoroutine(ZSpell.IEWolfJump(this.whoSummoned.game, this.whoSummoned, creature, spell2.maxDuration / 2 + 1, 15), false);
              break;
            }
          }
          break;
        case EffectorType.ApparitionArmageddon:
          this.active = false;
          if (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.parent == null || this.whoSummoned.parent.controlled.Count <= 0)
            break;
          ZSpell.ApparitionArmageddon(this.whoSummoned);
          break;
        case EffectorType.AutumnLeaves:
          this.VisualUpdate();
          break;
        case EffectorType.Grove_Renewal:
          this.active = false;
          if (ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.parent.localTurn > this.variable)
          {
            ++this.variable;
            List<ZMyCollider> zmyColliderList4 = this.world.OverlapCircleAll(new Point((int) this.position.x + 7, (int) this.position.y - 24), 95, this.collider, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
            for (int index = 0; index < zmyColliderList4.Count; ++index)
            {
              if ((ZComponent) zmyColliderList4[index].creature != (object) null)
              {
                int team1 = zmyColliderList4[index].creature.team;
                int? team2 = this.whoSummoned?.parent?.team;
                int valueOrDefault = team2.GetValueOrDefault();
                if (team1 == valueOrDefault & team2.HasValue)
                {
                  zmyColliderList4[index].creature.DoHeal(25);
                  if (zmyColliderList4[index].creature.type == CreatureType.Boar && !zmyColliderList4[index].creature.flying)
                    ZSpell.WhenPigsFly(Inert.GetSpell(SpellEnum.When_Pigs_Fly), zmyColliderList4[index].creature);
                }
              }
            }
            if (this.turnsAlive < this.MaxTurnsAlive - 1 || this.MaxTurnsAlive <= 0)
              break;
            this.Die(indexInParent, destroyable, global);
            break;
          }
          ++this.MaxTurnsAlive;
          break;
        case EffectorType.Stalk:
          this.whoSummoned.controllable = true;
          List<ZMyCollider> zmyColliderList5;
          if (this.whoSummoned.team != 24)
          {
            zmyColliderList5 = this.game.world.OverlapCircleAll(new Point(this.position), 750, this.whoSummoned.collider, Inert.mask_entity_movement | Inert.mask_Phantom);
          }
          else
          {
            zmyColliderList5 = new List<ZMyCollider>();
            zmyColliderList5.Add(this.game.ClosestCreature(this.whoSummoned, false)?.collider);
          }
          List<ZMyCollider> zmyColliderList6 = zmyColliderList5;
          zmyColliderList6.Sort((Comparison<ZMyCollider>) ((a, b) => MyLocation.Distance(a.position, this.whoSummoned.position) - MyLocation.Distance(b.position, this.whoSummoned.position)));
          if (this.game.isClient && !this.game.resyncing)
            AudioManager.Play(AudioManager.instance.stalkAppear);
          ZSpell.FireTeleport(Inert.GetSpell(SpellEnum.Pounce), this.whoSummoned, this.position, (FixedInt) 0, (FixedInt) 0, this.position);
          if (this.whoSummoned.isDead)
            break;
          for (int index = 0; index < zmyColliderList6.Count; ++index)
          {
            ZCreature creature = zmyColliderList6[index].creature;
            if ((ZComponent) creature == (object) null && (ZComponent) zmyColliderList6[index].tower != (object) null)
              creature = zmyColliderList6[index].tower.creature;
            if ((ZComponent) creature != (object) null && creature.parent.team != this.whoSummoned.team)
            {
              this.whoSummoned.game.ongoing.RunSpell(ZSpell.IEWolfJump(this.whoSummoned.game, this.whoSummoned, creature, -1, 25));
              this.Die(indexInParent, destroyable, global);
              return;
            }
          }
          this.Die(indexInParent, destroyable, global);
          break;
        case EffectorType.Rising_Lava:
          this.active = false;
          int variable1 = this.variable;
          int? localTurn1 = this.game.CurrentPlayer()?.localTurn;
          int valueOrDefault1 = localTurn1.GetValueOrDefault();
          if (variable1 < valueOrDefault1 & localTurn1.HasValue)
          {
            ZPerson zperson = this.game.CurrentPlayer();
            this.variable = zperson != null ? zperson.localTurn : 0;
          }
          int width1 = this.map.Width;
          int y3 = this.map.Height * this.variable / 75;
          int num2 = this.map.Width / 2;
          int num3 = 0;
          if (this.halved)
            y3 /= 2;
          Coords coords1 = new Coords(-width1, -y3);
          Coords coords2 = new Coords(width1, y3);
          List<ZMyCollider> zmyColliderList7 = this.world.OverlapRectangleAll(new MyCollider.Bounds()
          {
            x1 = coords1.x + num2,
            x2 = coords2.x + num2,
            y1 = coords1.y + num3,
            y2 = coords2.y + num3
          }, (ZMyCollider) null, Inert.mask_Phantom | Inert.mask_movement_NoEffector);
          for (int index = 0; index < zmyColliderList7.Count; ++index)
          {
            if (zmyColliderList7[index].gameObjectLayer == 13)
              zmyColliderList7[index].tower.ApplyDamage(SpellEnum.Rising_Lava, DamageType.None, this.variable * 2, (ZCreature) null, this.variable);
            else
              zmyColliderList7[index].creature?.ApplyDamage(SpellEnum.Rising_Lava, DamageType.None, this.variable, (ZCreature) null, this.variable);
          }
          this.VisualUpdate();
          break;
        case EffectorType.Phantom:
          this.active = false;
          this.whoSummoned.collider.gameObjectLayer = 16;
          this.whoSummoned.collider.layer = 65536;
          break;
        case EffectorType.Fake_Portal:
          if (ZComponent.IsNull((ZComponent) this.partner) || this.partner.dead)
          {
            this.Die(indexInParent, destroyable, global);
            break;
          }
          this.active = false;
          this.partner.TurnPassed(this.game.globalEffectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.partner)), true, true);
          break;
        case EffectorType.Dense_Fog:
          this.active = false;
          int variable2 = this.variable;
          int? localTurn2 = this.game.CurrentPlayer()?.localTurn;
          int valueOrDefault2 = localTurn2.GetValueOrDefault();
          if (variable2 < valueOrDefault2 & localTurn2.HasValue)
          {
            ZPerson zperson = this.game.CurrentPlayer();
            this.variable = zperson != null ? zperson.localTurn : 0;
          }
          int width2 = this.map.Width;
          int y4 = this.map.Height * this.variable / 75;
          int num4 = this.map.Width / 2;
          int height = this.map.Height;
          if (this.halved)
            y4 /= 2;
          Coords coords3 = new Coords(-width2, -y4);
          Coords coords4 = new Coords(width2, y4);
          List<ZMyCollider> zmyColliderList8 = this.world.OverlapRectangleAll(new MyCollider.Bounds()
          {
            x1 = coords3.x + num4,
            x2 = coords4.x + num4,
            y1 = coords3.y + height,
            y2 = coords4.y + height
          }, (ZMyCollider) null, Inert.mask_Phantom | Inert.mask_movement_NoEffector);
          for (int index = 0; index < zmyColliderList8.Count; ++index)
          {
            if (zmyColliderList8[index].gameObjectLayer == 13)
              zmyColliderList8[index].tower.ApplyDamage(SpellEnum.Dense_Fog, DamageType.None, this.variable * 2, (ZCreature) null, this.variable);
            else
              zmyColliderList8[index].creature?.ApplyDamage(SpellEnum.Dense_Fog, DamageType.None, this.variable, (ZCreature) null, this.variable);
          }
          this.VisualUpdate();
          break;
      }
    }
    else
    {
      if (this.type != EffectorType.Sanctuary || ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.isDead)
        return;
      ZEffector.CheckSanctuary(this.game, this.whoSummoned.position, this.collider.radius);
    }
  }

  public void Moved()
  {
    if (this.type == EffectorType.Sanctuary && (ZComponent) this.whoSummoned != (object) null && !this.whoSummoned.isDead)
      ZEffector.CheckSanctuary(this.game, this.whoSummoned.position, this.collider.radius);
    else if (this.type == EffectorType.Storm_Shield && this.active && !this.dead)
    {
      this.Logic_Storm_Shield();
    }
    else
    {
      if (this.type != EffectorType.Lich_Aura_of_decay && this.type != EffectorType.Dragon_Aura_of_Decay || !this.game.isClient || !((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null) || !this.gameObject.activeSelf)
        return;
      List<ZMyCollider> zmyColliderList = this.game.world.OverlapCircleAll((Point) this.position, this.variable, (ZMyCollider) null, Inert.mask_movement_NoEffector);
      for (int index = 0; index < zmyColliderList.Count; ++index)
      {
        if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
        {
          ZCreature creature = zmyColliderList[index].creature;
          if (!ZComponent.IsNull((ZComponent) creature) && (ZComponent) creature != (object) this.whoSummoned && (creature.race != CreatureRace.Effector || creature.GetType() == typeof (ZCreatureTree)))
            ZEffector.SpawnIndicatorOfDecay(creature, this);
        }
        else if (zmyColliderList[index].gameObjectLayer == 13)
        {
          ZTower tower = zmyColliderList[index].tower;
          if (!ZComponent.IsNull((ZComponent) tower) && (ZComponent) tower.creature != (object) this.whoSummoned)
            ZEffector.SpawnIndicatorOfDecay(tower.creature, this);
        }
      }
    }
  }

  private void Logic_Storm_Shield()
  {
    if (this.variable <= 0 && this.type == EffectorType.Storm_Shield)
      return;
    List<ZMyCollider> zmyColliderList = this.world.OverlapCircleAll((Point) this.position, this.collider.radius, this.whoSummoned?.collider, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    int num = 0;
    if (zmyColliderList.Count <= 0)
      return;
    int variable;
    do
    {
      variable = this.variable;
      ++num;
      for (int index = 0; index < zmyColliderList.Count; ++index)
      {
        if (!((ZComponent) zmyColliderList[index] == (object) null))
        {
          if (zmyColliderList[index].gameObjectLayer == 13)
          {
            ZTower tower = zmyColliderList[index].tower;
            if ((ZComponent) tower != (object) null && (ZComponent) tower.creature != (object) this.whoSummoned)
              this.EffectCreature(tower.creature, true);
          }
          else
          {
            ZCreature creature = zmyColliderList[index].creature;
            if ((ZComponent) creature != (object) null && (ZComponent) creature != (object) this.whoSummoned)
              this.EffectCreature(creature, true);
          }
        }
      }
    }
    while (this.type == EffectorType.Storm_Shield && variable != this.variable && this.variable > 0 && num < 7);
  }

  private void SetVineThorn66(ZSpell s) => s.amount = 66;

  public void CheckPortal() => this.EffectEntity((ZEntity) null);

  public virtual void EffectSpell(ZSpell s)
  {
    if (!this.active && !this.effectWhileInactive || this.dead || (ZComponent) this == (object) null)
      return;
    switch (this.type)
    {
      case EffectorType.Fire_Shield:
        if (!(s.velocity.y <= 0) || MyLocation.Distance(s.position + s.velocity + s.addedVelocity, this.position) >= MyLocation.Distance(s.position, this.position))
          break;
        this.SlowlyDeactivate(60);
        if (s.damageType == DamageType.Snow)
        {
          s.curDuration = 1000;
          break;
        }
        MyLocation myLocation1 = s.position - this.position;
        s.affectedByGravity = true;
        myLocation1.Normalize();
        if (myLocation1.x == 0)
          myLocation1.x = (FixedInt) 10485L;
        ZSpell zspell1 = s;
        zspell1.addedVelocity = zspell1.addedVelocity + myLocation1;
        s.addVelocity = true;
        break;
      case EffectorType.Flame_Wall:
        if (!(s.velocity.y <= 0) || MyLocation.Distance(s.position + s.velocity + s.addedVelocity, this.position) >= MyLocation.Distance(s.position, this.position))
          break;
        this.SlowlyDeactivate();
        if (s.damageType == DamageType.Snow)
        {
          s.curDuration = 1000;
          break;
        }
        MyLocation myLocation2 = s.position - this.position;
        s.affectedByGravity = true;
        myLocation2.Normalize();
        MyLocation myLocation3 = myLocation2 * 1179648L;
        if (myLocation3.x == 0)
          myLocation3.x = (FixedInt) 10485L;
        ZSpell zspell2 = s;
        zspell2.addedVelocity = zspell2.addedVelocity + myLocation3;
        s.addVelocity = true;
        break;
      case EffectorType.Wind_Shield:
        if (MyLocation.Distance(s.position + s.velocity + s.addedVelocity, this.position) >= MyLocation.Distance(s.position, this.position))
          break;
        MyLocation myLocation4 = s.position - this.position;
        s.affectedByGravity = true;
        this.SlowlyDeactivate();
        myLocation4.Normalize();
        myLocation4 *= 1258291L;
        if (myLocation4.x == 0)
          myLocation4.x = (FixedInt) 10485L;
        ZSpell zspell3 = s;
        zspell3.addedVelocity = zspell3.addedVelocity + myLocation4;
        s.addVelocity = true;
        break;
      case EffectorType.Natures_Wrath:
        if (!((ZComponent) s != (object) null) || (double) this.game.serverState.turnTime >= 150.0 && !this.game.isSandbox || s.addVelocity)
          break;
        MyLocation myLocation5 = s.position - this.spell.position;
        s.affectedByGravity = true;
        myLocation5.Normalize();
        MyLocation myLocation6 = myLocation5 * 157286L;
        if (myLocation6.x == 0)
          myLocation6.x = (FixedInt) 10485L;
        if (myLocation6.y > Mathd.Abs(myLocation6.x))
        {
          FixedInt x = myLocation6.x;
          myLocation6.x = myLocation6.y * (x > 0 ? 838860L : -838860L);
        }
        else
          myLocation6.y = Mathd.Abs(myLocation6.x);
        FixedInt fixedInt1 = Mathd.Lerp((FixedInt) 524288L, (FixedInt) 1, ((FixedInt) 2000 - (s.position.y - this.collider.position.y)) / 2000);
        MyLocation myLocation7 = myLocation6 * fixedInt1;
        ZSpell zspell4 = s;
        zspell4.addedVelocity = zspell4.addedVelocity + myLocation7 * 10;
        s.addVelocity = true;
        break;
      case EffectorType.Portal:
      case EffectorType.Wormhole:
        this.EffectEntity((ZEntity) s);
        break;
      case EffectorType.Storm:
        MyLocation myLocation8 = s.position - this.position;
        myLocation8.Normalize();
        myLocation8.y = (FixedInt) 0;
        myLocation8.x *= this.game.RandomFixedInt(0, 1);
        ZSpell zspell5 = s;
        zspell5.addedVelocity = zspell5.addedVelocity + myLocation8;
        s.addVelocity = true;
        break;
      case EffectorType.Blizzard:
        MyLocation myLocation9 = s.position - this.position;
        myLocation9.Normalize();
        myLocation9.y = (FixedInt) 0;
        myLocation9.x *= this.game.RandomFixedInt(0, 1);
        ZSpell zspell6 = s;
        zspell6.addedVelocity = zspell6.addedVelocity + myLocation9;
        s.addVelocity = true;
        break;
      case EffectorType.English_Summer:
      case EffectorType.Acid_Rain:
        MyLocation myLocation10 = s.position - this.position;
        myLocation10.Normalize();
        myLocation10.y = (FixedInt) 0;
        myLocation10.x *= this.game.RandomFixedInt(0, 1);
        ZSpell zspell7 = s;
        zspell7.addedVelocity = zspell7.addedVelocity - myLocation10;
        s.addVelocity = true;
        break;
      case EffectorType.Spirit_Shield:
        if (MyLocation.Distance(s.position + s.velocity + s.addedVelocity, this.position) >= MyLocation.Distance(s.position, this.position))
          break;
        this.SlowlyDeactivate(60);
        MyLocation myLocation11 = s.position - this.position;
        s.affectedByGravity = true;
        myLocation11.Normalize();
        if (this.position.y + 25 < s.position.y)
          myLocation11 *= (FixedInt) 1f;
        else
          myLocation11 *= 262144L;
        if (myLocation11.x == 0)
          myLocation11.x = (FixedInt) 10485L;
        ZSpell zspell8 = s;
        zspell8.addedVelocity = zspell8.addedVelocity + myLocation11;
        s.addVelocity = true;
        break;
      case EffectorType.Vortex:
        if (s.addVelocity || MyLocation.Distance(s.position + s.velocity + s.addedVelocity, this.position) >= MyLocation.Distance(s.position, this.position))
          break;
        MyLocation myLocation12 = s.position - this.position;
        s.affectedByGravity = true;
        s.addVelocity = true;
        s.goToTarget = false;
        long y = ZComponent.IsNull((ZComponent) this.whoSummoned) || this.whoSummoned.familiarLevelIllusion != 5 ? 3145725L : 9437184L;
        if (myLocation12.x > 0)
        {
          if (myLocation12.y > 0)
          {
            ZSpell zspell9 = s;
            zspell9.addedVelocity = zspell9.addedVelocity + new MyLocation((FixedInt) (-y * (long) this.variable), (FixedInt) -y);
            break;
          }
          ZSpell zspell10 = s;
          zspell10.addedVelocity = zspell10.addedVelocity + new MyLocation((FixedInt) (-y * (long) this.variable), (FixedInt) y);
          break;
        }
        if (myLocation12.y > 0)
        {
          ZSpell zspell11 = s;
          zspell11.addedVelocity = zspell11.addedVelocity + new MyLocation((FixedInt) (y * (long) this.variable), (FixedInt) -y);
          break;
        }
        ZSpell zspell12 = s;
        zspell12.addedVelocity = zspell12.addedVelocity + new MyLocation((FixedInt) (y * (long) this.variable), (FixedInt) y);
        break;
      case EffectorType.Magical_Barrier:
        if (!((ZComponent) s != (object) null) || s.maxDuration <= 1 || s.spellEnum == SpellEnum.Rain_of_Clams || !this.collider.OverlapPoint((int) s.position.x, (int) s.position.y))
          break;
        ++this.variable;
        s.maxDuration = 1;
        s.velocity = new MyLocation((FixedInt) 0, -s.game.gravity);
        s.validX = s.pX;
        s.validY = s.pX;
        s.steps = 0;
        if (this.variable < 50)
          break;
        this.Die(this.whoSummoned.effectors.FindIndex((Predicate<ZEffector>) (x => (ZComponent) x == (object) this)), false, false);
        break;
      case EffectorType.Blackhole:
        MyLocation a = s.position - this.position;
        FixedInt fixedInt2 = (FixedInt) MyLocation.Distance(a, MyLocation.zero);
        if (fixedInt2 < 64 + s.radius)
        {
          s.maxDuration = 1;
          break;
        }
        s.affectedByGravity = true;
        a.Normalize();
        MyLocation myLocation13 = a * (((FixedInt) this.collider.radius - fixedInt2) / (this.collider.radius + s.radius));
        if (myLocation13.x == 0)
          myLocation13.x = (FixedInt) 10485L;
        ZSpell zspell13 = s;
        zspell13.addedVelocity = zspell13.addedVelocity - myLocation13;
        s.addVelocity = true;
        break;
      case EffectorType.GravityWell:
        if ((ZComponent) this.whoSummoned == (object) s.toCollideCheck)
          break;
        MyLocation myLocation14 = (this.position - s.position).normalized * s.velocity.magnitude;
        s.velocity = myLocation14;
        this.active = false;
        this.VisualUpdate();
        break;
    }
  }

  public bool PortalLogic(List<ZEntity> colliders)
  {
    bool flag = false;
    for (int index = 0; index < colliders.Count; ++index)
    {
      ZEntity zentity = colliders[index];
      if (zentity.radius > this.collider.radius && this.type != EffectorType.Wormhole || (ZComponent) zentity.asCreature != (object) null && zentity.asCreature.race == CreatureRace.Effector)
      {
        ZCreature asCreature = zentity.asCreature;
        if ((ZComponent) asCreature != (object) null && (ZComponent) asCreature.rider != (object) null && asCreature.rider.radius <= this.collider.radius && asCreature.rider.collider.OverlapCircle((Point) this.collider.position, this.collider.radius))
          zentity = (ZEntity) asCreature.rider;
        else
          continue;
      }
      ZCreature asCreature1 = zentity.asCreature;
      if ((ZComponent) asCreature1 != (object) null)
      {
        if (!((ZComponent) asCreature1.tower != (object) null))
        {
          asCreature1.Demount();
          if (asCreature1.race == CreatureRace.Effector)
            ;
        }
        else
          continue;
      }
      else
        zentity.game.portalUsedThisSpellTurn = true;
      List<ParticleSystem> list = new List<ParticleSystem>();
      zentity.GetComponentsInChildren<ParticleSystem>(false, list);
      foreach (Component component in list)
        component.gameObject.SetActive(false);
      flag = true;
      this.active = false;
      this.partner.active = false;
      zentity.SetPosition(this.partner.position);
      zentity.collider?.Move(this.partner.position);
      zentity.validX = zentity.position.x;
      zentity.validY = zentity.position.y;
      zentity.pX = zentity.validX;
      zentity.pY = zentity.validY;
      if (this.game.isClient)
      {
        this.PortalDisable();
        this.partner.PortalDisable();
      }
      foreach (ParticleSystem particleSystem in list)
        particleSystem?.gameObject.SetActive(true);
    }
    return flag;
  }

  public void PostPortalLogic(List<ZEntity> colliders)
  {
    this.game.CreatureMoveSurroundings(this.position, 100);
    for (int index = 0; index < colliders.Count; ++index)
    {
      if (!ZComponent.IsNull((ZComponent) colliders[index]))
      {
        ZCreature asCreature = colliders[index].asCreature;
        if (!ZComponent.IsNull((ZComponent) asCreature) && asCreature.race != CreatureRace.Effector && asCreature.game != null)
        {
          if (asCreature.type == CreatureType.ClockWorkBomb)
          {
            asCreature.Fall();
          }
          else
          {
            if (this.type == EffectorType.Wormhole && asCreature.familiarLevelCosmos < 5 && asCreature.type != CreatureType.Cosmic_Horror)
            {
              asCreature.CreateGravityObj();
              asCreature.appliedGravity = Mathf.Max(asCreature.appliedGravity, asCreature.parent.localTurn + (asCreature.parent.yourTurn ? 1 : 2));
            }
            asCreature.CreatureMoveSurroundings();
            this.map.CreatureCheckEffectors(asCreature, (int) asCreature.position.x, (int) asCreature.position.y);
            if (!ZComponent.IsNull((ZComponent) asCreature) && asCreature.health > 0 && !asCreature.isMoving && asCreature.ShouldFall())
              asCreature.Fall();
          }
        }
      }
    }
  }

  public void EffectEntity(ZEntity entity)
  {
    if (!this.active || this.dead || (ZComponent) this == (object) null)
      return;
    switch (this.type)
    {
      case EffectorType.Portal:
      case EffectorType.Wormhole:
        List<ZMyCollider> zmyColliderList1 = this.world.OverlapCircleAll((Point) this.position, this.collider.radius, (ZMyCollider) null, 256 | Inert.mask_Phantom);
        List<ZMyCollider> zmyColliderList2 = this.partner.variable == 666 ? new List<ZMyCollider>() : this.world.OverlapCircleAll((Point) this.partner.position, this.collider.radius, (ZMyCollider) null, 256 | Inert.mask_Phantom);
        if (this.turnsAlive == 1 && this.partner.variable != 666)
        {
          int num1 = this.collider.radius - 4;
          int num2 = this.collider.radius - 5;
          ZMyWorld world1 = this.world;
          MyCollider.Bounds bounds1 = new MyCollider.Bounds();
          bounds1.x1 = (int) this.position.x - num2;
          bounds1.x2 = (int) this.position.x + num2;
          bounds1.y1 = (int) this.position.y - num2;
          bounds1.y2 = (int) this.position.y + num2;
          MyCollider.Bounds bounds2 = bounds1;
          ZMyCollider collider1 = this.collider;
          int layer1 = 256 | Inert.mask_Phantom;
          List<ZMyCollider> zmyColliderList3 = world1.OverlapRectangleAll(bounds2, collider1, layer1);
          ZMyWorld world2 = this.world;
          bounds1 = new MyCollider.Bounds();
          bounds1.x1 = (int) this.partner.position.x - num1;
          bounds1.x2 = (int) this.partner.position.x + num1;
          bounds1.y1 = (int) this.partner.position.y - num1;
          bounds1.y2 = (int) this.partner.position.y + num1;
          MyCollider.Bounds bounds3 = bounds1;
          ZMyCollider collider2 = this.collider;
          int layer2 = 256 | Inert.mask_Phantom;
          List<ZMyCollider> zmyColliderList4 = world2.OverlapRectangleAll(bounds3, collider2, layer2);
          foreach (ZMyCollider z in zmyColliderList3)
          {
            if (!ZComponent.IsNull((ZComponent) z) && (ZComponent) z.creature != (object) null && (ZComponent) z.creature.mount == (object) null && !zmyColliderList1.Contains(z))
              zmyColliderList1.Add(z);
          }
          foreach (ZMyCollider z in zmyColliderList4)
          {
            if (!ZComponent.IsNull((ZComponent) z) && (ZComponent) z.creature != (object) null && (ZComponent) z.creature.mount == (object) null && !zmyColliderList2.Contains(z))
              zmyColliderList2.Add(z);
          }
        }
        if (zmyColliderList1.Count > 0 || zmyColliderList2.Count > 0 || (ZComponent) entity != (object) null)
        {
          List<ZEntity> colliders1 = new List<ZEntity>((ZComponent) entity != (object) null ? 1 + zmyColliderList1.Count : zmyColliderList1.Count);
          List<ZEntity> colliders2 = new List<ZEntity>((ZComponent) entity != (object) null ? 1 + zmyColliderList1.Count : zmyColliderList1.Count);
          for (int index = 0; index < zmyColliderList1.Count; ++index)
            colliders1.Add(zmyColliderList1[index].entity);
          if ((ZComponent) entity != (object) null && colliders1.FindIndex((Predicate<ZEntity>) (xx => (ZComponent) xx == (object) entity)) == -1)
            colliders1.Add(entity);
          for (int index1 = 0; index1 < zmyColliderList2.Count; ++index1)
          {
            ZEntity e = zmyColliderList2[index1].entity;
            int index2 = colliders1.FindIndex((Predicate<ZEntity>) (x => (ZComponent) x == (object) e));
            if (index2 >= 0)
            {
              if (MyLocation.Distance(this.position, e.position) > MyLocation.Distance(this.partner.position, e.position))
              {
                colliders1.RemoveAt(index2);
                colliders2.Add(e);
              }
            }
            else
              colliders2.Add(e);
          }
          int num = this.PortalLogic(colliders1) ? 1 : 0;
          bool flag = this.partner.PortalLogic(colliders2);
          if (num != 0)
            this.PostPortalLogic(colliders1);
          if (flag)
            this.partner.PostPortalLogic(colliders2);
        }
        this.VisualUpdate();
        this.partner?.VisualUpdate();
        break;
    }
  }

  public static bool InSanctuary(ZMyWorld world, MyLocation t)
  {
    return world.OverlapPoint(t.x.ToInt(), t.y.ToInt(), 32768);
  }

  public static bool InEffector(ZMyCollider col, EffectorType z)
  {
    foreach (ZMyCollider zmyCollider in col.world.OverlapColliderAll(col, 3584))
    {
      if ((ZComponent) zmyCollider != (object) null && (ZComponent) zmyCollider.effector != (object) null && zmyCollider.effector.type == z)
        return true;
    }
    return false;
  }

  public static void CheckSanctuary(ZMyWorld world, ZCreature c)
  {
    ZMyWorld zmyWorld = world;
    MyLocation position = c.position;
    int x = position.x.ToInt();
    position = c.position;
    int y = position.y.ToInt();
    if (!((ZComponent) zmyWorld.FindColliderAtPoint(x, y, 32768) != (object) null))
      return;
    ZEffector.EffectSanctuary(c.game, c);
  }

  public static void CheckSanctuary(ZGame game, MyLocation position, int radius = 127, bool force = false)
  {
    List<ZMyCollider> zmyColliderList = game.world.OverlapCircleAll((Point) position, radius, (ZMyCollider) null, Inert.mask_all);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZTower tower = zmyColliderList[index].tower;
        if ((ZComponent) tower != (object) null && (force || ZEffector.InSanctuary(tower.creature.game.world, tower.creature.position)))
          ZEffector.EffectSanctuary(tower.creature.game, tower.creature);
      }
      else if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
      {
        ZCreature creature = zmyColliderList[index].creature;
        if (!ZComponent.IsNull((ZComponent) creature) && (force || ZEffector.InSanctuary(creature.game.world, creature.position)))
          ZEffector.EffectSanctuary(creature.game, creature);
      }
      else
      {
        ZEffector effector = zmyColliderList[index].effector;
        if (!ZComponent.IsNull((ZComponent) effector) && effector.Destroyable() && (force || ZEffector.InSanctuary(effector.world, effector.position)))
        {
          effector.collider?.Disable(effector.type != EffectorType.Lich_Aura_of_decay && effector.type != EffectorType.Dragon_Aura_of_Decay);
          effector.active = false;
          if (effector.type == EffectorType.Lich_Aura_of_decay || effector.type == EffectorType.Dragon_Aura_of_Decay)
          {
            effector.gameObjectActive = false;
            if ((UnityEngine.Object) effector.gameObject != (UnityEngine.Object) null)
              effector.gameObject.SetActive(false);
            game.forceRysncPause = true;
          }
          else if (effector.type == EffectorType.Static_Ball)
          {
            ((ZEffectorStaticBall) effector).ReleaseSpells();
            ZComponent.Destroy<GameObject>(effector.gameObject);
          }
          else
          {
            effector.isNull = true;
            if ((effector.type == EffectorType.Portal || effector.type == EffectorType.Wormhole) && (ZComponent) effector.partner != (object) null)
            {
              effector.partner.collider?.Disable();
              effector.partner.active = false;
              effector.partner.isNull = true;
              ZComponent.Destroy<GameObject>(effector.partner.gameObject);
            }
            ZComponent.Destroy<GameObject>(effector.gameObject);
          }
          if ((UnityEngine.Object) effector.transform != (UnityEngine.Object) null)
            ZEffector.SpawnVineExplosion(effector.transform.position);
        }
      }
    }
  }

  public void CheckSanctuary()
  {
    ZEffector.CheckSanctuary(this.game, ZComponent.IsNull((ZComponent) this.whoSummoned) || !((ZComponent) this.whoSummoned.tower != (object) null) ? this.position : this.whoSummoned.tower.position, this.collider.radius);
  }

  public static void SpawnIndicatorOfDecay(ZCreature c, ZEffector e)
  {
    if (!((ZComponent) e != (object) null) || !c.game.isClient || !((UnityEngine.Object) c.indicatorOfDecay == (UnityEngine.Object) null) || c.health <= 0 || c.isDead || e.turnsAlive >= 5 && (e.damageType == DamageType.Infection || c.race == CreatureRace.Effector && c.type != CreatureType.Tree) || !((UnityEngine.Object) c.transform != (UnityEngine.Object) null) || e.damageType == DamageType.Sting && (c.type == CreatureType.Bee || c.type == CreatureType.Beehive) || !((ZComponent) e.whoSummoned == (object) null) && (e.whoSummoned.collider.layer == Inert.mask_Jar || e.whoSummoned.collider.layer == Inert.mask_ButterflyJar) && c.type == CreatureType.Jar)
      return;
    c.indicatorOfDecay = ZComponent.Instantiate<GameObject>(Inert.Instance.indicatorOfDecay, c.transform.position, Quaternion.identity, c.transform);
    float num = (float) c.radius / 64f;
    c.indicatorOfDecay.transform.localScale = new Vector3(num, num, 1f);
    c.indicatorOfDecay.GetComponent<IndicatorOfDecay>().Setup(c, e);
  }

  public static void SpawnVineExplosion(Vector3 t)
  {
    if (!Client.game.isClient || Client.game.resyncing)
      return;
    ZComponent.Instantiate<GameObject>(Inert.Instance.vineExplosion, t, Quaternion.identity, Client.GetMapTransform());
  }

  public void PortalDisable()
  {
    if (!((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.transform.GetChild(0).gameObject.SetActive(false);
    this.transform.GetChild(1).gameObject.SetActive(false);
    this.transform.GetChild(2).gameObject.SetActive(false);
    this.transform.GetChild(3).GetComponent<RotateAroundCenter>().enabled = false;
  }

  public static void RechargeElectrostaticCharges(ZGame game)
  {
    for (int index = game.staticCharge.Count - 1; index >= 0; --index)
    {
      if (!ZComponent.IsNull((ZComponent) game.staticCharge[index]))
        game.staticCharge[index].ReactiavateStaticCharge();
      else
        game.staticCharge.RemoveAt(index);
    }
  }

  public void ReactiavateStaticCharge()
  {
    this.active = true;
    this.variable = 25;
    this.VisualUpdate();
    this.CheckInitialSpawn_Creatures(this.collider.radius);
  }

  public void CheckInitialSpawn_Creatures(int radius)
  {
    List<ZMyCollider> zmyColliderList = this.world.OverlapCircleAll((Point) this.position, radius, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    int variable = this.variable;
    while (true)
    {
      for (int index = 0; index < zmyColliderList.Count; ++index)
      {
        if (!ZComponent.IsNull((ZComponent) zmyColliderList[index]))
        {
          ZCreature creature = zmyColliderList[index].creature;
          if ((ZComponent) creature != (object) null)
          {
            this.EffectCreature(creature);
          }
          else
          {
            ZTower tower = zmyColliderList[index].tower;
            if ((ZComponent) tower != (object) null)
              this.EffectCreature(tower.creature);
          }
        }
      }
      if (this.active && variable != this.variable)
        variable = this.variable;
      else
        break;
    }
  }

  public int GetAuraDamage(DamageType t)
  {
    if (t == DamageType.Arcane && (ZComponent) this.whoSummoned != (object) null && this.whoSummoned.HasArcaneEnergizer)
      return 20;
    return t != DamageType.Heal20 ? 15 : 10;
  }

  public static Spell GetAuraSpell(DamageType t)
  {
    if (t == DamageType.Heal20)
      return Inert.GetSpell("Blood Mist");
    return t != DamageType.Death ? Inert.GetSpell("Arcane Mist") : Inert.GetSpell("Aura of Decay");
  }

  public static Spell GetLichAuraSpell(DamageType t)
  {
    if (t == DamageType.Infection)
      return Inert.GetSpell("Infection");
    return t != DamageType.Sting ? Inert.GetSpell("Lichdom") : Inert.GetSpell("Sting");
  }

  public static void SpawnAuraExplosion(Vector3 pos, DamageType t)
  {
    GameObject t1;
    switch (t)
    {
      case DamageType.Arcane:
        t1 = Inert.Instance.auraOfArcaneExplosion;
        break;
      case DamageType.Heal20:
      case DamageType.Infection:
        t1 = Inert.Instance.auraOfBloodExplosion;
        break;
      case DamageType.Sting:
        t1 = Inert.Instance.auraOfSeasonsExplosion;
        break;
      default:
        t1 = Inert.Instance.auraOfDecayExplosion;
        break;
    }
    Vector3 l = pos;
    Quaternion identity = Quaternion.identity;
    ZComponent.Instantiate<GameObject>(t1, l, identity);
  }

  public MyLocation AuraForce(ZCreature c)
  {
    return new MyLocation(c.position.x + (c.position.x > this.position.x ? 5 : -5), c.position.y - 1);
  }

  public static void SpreadInfection(
    ZCreature infector,
    ZCreature c,
    ZEffector start,
    bool fullBlood = false)
  {
    HashSet<ZCreature> zcreatureSet = new HashSet<ZCreature>();
    List<ZCreature> zcreatureList = new List<ZCreature>();
    int index1 = 0;
    List<(ZCreature, ZEffector)> valueTupleList = new List<(ZCreature, ZEffector)>();
    zcreatureSet.Add(c);
    zcreatureList.Add(c);
    while (true)
    {
      if (!ZComponent.IsNull((ZComponent) c) && !c.isDead && c.race != CreatureRace.Effector && (fullBlood || c.familiarLevelBlood <= 0 || c.isPawn) && (!((ZComponent) start != (object) null) || start.turnsAlive < 5))
      {
        for (int index2 = 0; index2 < c.followingColliders.Count; ++index2)
        {
          if ((ZComponent) c.followingColliders[index2] != (object) null)
          {
            ZEffector effector = c.followingColliders[index2].effector;
            if ((ZComponent) effector != (object) null && effector.type == EffectorType.Lich_Aura_of_decay && effector.damageType == DamageType.Infection)
              goto label_18;
          }
        }
        ZEffector zeffector = ZEffector.Create(c.game, Inert.Instance.infectedBloodAura, c.transform.position, Quaternion.identity, c.transform);
        zeffector.game = c.game;
        zeffector.whoSummoned = c;
        zeffector.infector = infector;
        zeffector.followParent = true;
        zeffector.collider.world = zeffector.world;
        zeffector.collider.Move(c.position);
        c.effectors.Add(zeffector);
        c.followingColliders.Add(zeffector.collider);
        zeffector.active = false;
        c.game.forceRysncPause = true;
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
        {
          c.notScaled.Add((ZCreature.NotScaled) new ZCreature.OneScaled(zeffector.transform));
          if (c.game.isClient && (double) c.scale != 1.0)
          {
            float num = 1f / c.scale;
            zeffector.transform.localScale = new Vector3(num, num, num);
          }
        }
        valueTupleList.Add((c, zeffector));
        List<ZMyCollider> zmyColliderList = zeffector.world.OverlapCircleAll((Point) c.position, zeffector.collider.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
        for (int index3 = 0; index3 < zmyColliderList.Count; ++index3)
        {
          if (zmyColliderList[index3].gameObjectLayer == 13)
          {
            ZTower tower = zmyColliderList[index3].tower;
            if ((ZComponent) tower != (object) null && tower.type != TowerType.Nature && (ZComponent) tower.creature != (object) null && zcreatureSet.Add(tower.creature))
              zcreatureList.Add(tower.creature);
          }
          else
          {
            ZCreature creature = zmyColliderList[index3].creature;
            if ((ZComponent) creature != (object) null && (creature.race != CreatureRace.Effector || creature.GetType() == typeof (ZCreatureTree)) && creature.type != CreatureType.Dryad && zcreatureSet.Add(creature))
              zcreatureList.Add(creature);
          }
        }
      }
label_18:
      if (zcreatureList.Count > index1)
      {
        c = zcreatureList[index1];
        ++index1;
      }
      else
        break;
    }
    foreach ((ZCreature, ZEffector) valueTuple in valueTupleList)
    {
      valueTuple.Item2.TurnPassed(valueTuple.Item1.effectors.Count - 1, false, false);
      if ((ZComponent) start != (object) null)
      {
        valueTuple.Item2.SetTurnsAlive(start.turnsAlive);
        if ((ZComponent) infector != (object) null)
          valueTuple.Item2.MaxTurnsAlive = Mathf.Max(start.turnsAlive + infector.familiarLevelBlood, 5);
      }
    }
  }

  public bool OnTeam(ZCreature c)
  {
    if (!((ZComponent) this.whoSummoned != (object) null))
      return false;
    int? team1 = c.parent?.team;
    int? team2 = this.whoSummoned.parent?.team;
    return team1.GetValueOrDefault() == team2.GetValueOrDefault() & team1.HasValue == team2.HasValue;
  }

  public bool OnTeam(ZPerson c)
  {
    if (!((ZComponent) this.whoSummoned != (object) null))
      return false;
    int team1 = c.team;
    int? team2 = this.whoSummoned.parent?.team;
    int valueOrDefault = team2.GetValueOrDefault();
    return team1 == valueOrDefault & team2.HasValue;
  }

  public bool MorningSunEffect(ZCreature c)
  {
    if (ZComponent.IsNull((ZComponent) c) || c.type != CreatureType.Tree && c.race == CreatureRace.Effector)
      return false;
    FixedInt y = c.type == CreatureType.Tree ? c.position.y + 35 : c.position.y;
    if ((ZComponent) this.whoSummoned != (object) null)
    {
      int? team1 = c.parent?.team;
      int? team2 = this.whoSummoned.parent?.team;
      if (team1.GetValueOrDefault() == team2.GetValueOrDefault() & team1.HasValue == team2.HasValue)
        goto label_5;
    }
    if (c.type != CreatureType.Tree)
    {
      this.active = false;
      if (c.game.isClient)
        this.ToggleMorningSun();
      if (this.spell.position.y != y)
      {
        this.spell.position = new MyLocation(this.position.x, y);
        this.collider.Move(this.spell.position);
      }
      c.ApplyDamage(SpellEnum.Morning_Sun, DamageType.None, 10, this.whoSummoned, this.whoSummoned.parent.localTurn);
      return true;
    }
label_5:
    if (this.spell.position.y != y)
    {
      this.spell.position = new MyLocation(this.position.x, y);
      this.collider.Move(this.spell.position);
    }
    if (c.type == CreatureType.Tree)
      return false;
    this.active = false;
    if (c.game.isClient)
      this.ToggleMorningSun();
    return true;
  }

  public IEnumerator<float> RetroactiveDisableLeaf(int radius)
  {
    MyLocation position = this.position;
    for (int i = 0; i < 60; ++i)
      yield return 0.0f;
    List<ZMyCollider> zmyColliderList = new List<ZMyCollider>();
    HashSet<ZMyCollider> zmyColliderSet = new HashSet<ZMyCollider>();
    int index1 = 0;
    while (this.world != null)
    {
      List<ZMyCollider> list = this.world.OverlapCircleAll((Point) position, radius + 50, this.collider, 512);
      for (int index2 = 0; index2 < list.Count; ++index2)
      {
        if ((ZComponent) list[index2] != (object) null && (ZComponent) list[index2].effector != (object) null && (ZComponent) list[index2].spell != (object) null && list[index2].spell.spellEnum == SpellEnum.Autumn_Leaves && list[index2].effector.active && zmyColliderSet.Add(list[index2]))
        {
          list[index2].effector.active = false;
          list[index2].effector.VisualUpdate();
          zmyColliderList.Add(list[index2]);
        }
      }
      this.world.listPool.ReturnList(list);
      if (index1 >= zmyColliderList.Count || (ZComponent) zmyColliderList[index1] == (object) null)
        break;
      position = zmyColliderList[index1].position;
      ++index1;
    }
  }

  public void EffectCreature(ZCreature c, bool fromTurnStart = false)
  {
    if (this.dead || (ZComponent) this == (object) null)
      return;
    if (!this.active)
    {
      if (this.game.isClient && (this.type == EffectorType.Aura_of_decay || this.type == EffectorType.Lich_Aura_of_decay || this.type == EffectorType.Dragon_Aura_of_Decay) && ((ZComponent) c != (object) this.whoSummoned || this.type == EffectorType.Aura_of_decay))
        ZEffector.SpawnIndicatorOfDecay(c, this);
      else if (this.type == EffectorType.Morning_Sun && this.spell.position.y != c.position.y)
      {
        this.spell.position = new MyLocation(this.position.x, c.position.y);
        this.collider.Move(this.spell.position);
      }
      if (this.type != EffectorType.Miner_Map)
        return;
    }
    if ((ZComponent) c != (object) null && c.race == CreatureRace.Effector && this.type != EffectorType.Sanctuary && this.type != EffectorType.Storm_Shield && c.type != CreatureType.Tree)
      return;
    switch (this.type)
    {
      case EffectorType.None:
        break;
      case EffectorType.Glyph:
        this.active = false;
        int damage = 10;
        if (!ZComponent.IsNull((ZComponent) this.whoSummoned) && this.whoSummoned.HasArcaneEnergizer)
          damage = 15;
        this.map.ServerBitBlt(36, (int) this.position.x, (int) this.position.y);
        if (ZComponent.IsNull((ZComponent) c) || (ZComponent) c != (object) this.whoSummoned || c.type == CreatureType.Imp)
          ZSpell.ApplyExplosionForce(SpellEnum.Arcane_Glyph, this.world, this.position, damage, Curve.Generic, 16, 35, (FixedInt) 4, DamageType.Arcane, this.whoSummoned, this.TurnCreated, spellRef: (ISpellBridge) Inert.GetSpell("Arcane Glyph"), firstHit: c);
        else if ((ZComponent) c != (object) null && c.FullArcane)
        {
          c.ApplyDamage(SpellEnum.Arcane_Glyph, DamageType.Arcane, damage, c, 0, (ISpellBridge) Inert.GetSpell("Arcane Glyph"));
          c.KillMovement();
          c.velocity = new MyLocation(0, -2);
          c.StartMoving(false);
        }
        else if ((ZComponent) c != (object) null && (ZComponent) c.tower != (object) null && c.tower.type == TowerType.Arcane)
          c.tower.ApplyDamage(SpellEnum.Arcane_Gate, DamageType.Arcane, damage, c, 0, (ISpellBridge) Inert.GetSpell("Arcane Glyph"));
        else if ((ZComponent) c != (object) null)
        {
          c.KillMovement();
          c.velocity = new MyLocation(0, -2);
          c.StartMoving(false);
        }
        this.Destroy();
        break;
      case EffectorType.Flame_WallBase:
        this.active = false;
        this.spell.effector2.active = false;
        this.VisualUpdate();
        this.map.ServerBitBlt(5, (int) this.collider.position.x, (int) this.collider.position.y);
        this.spell.ApplyExplosionForce(this.collider.position, firstHit: c);
        this.spell.Fall();
        this.spell.Undie();
        AudioManager.Play(this.spell.explosionClip);
        break;
      case EffectorType.Natures_Wrath:
        if ((ZComponent) c == (object) null || c.type == CreatureType.Tree || c.UnaffectedByNaturesWraith || !c.isMoving || MyLocation.Distance(c.velocity, MyLocation.zero) >= 15 || (double) this.game.serverState.turnTime >= 150.0 && !this.game.isSandbox || !(Mathd.Abs(c.position.x + c.velocity.x - this.position.x) < Mathd.Abs(c.position.x - this.position.x)) || !(Mathd.Abs(c.position.x - this.position.x) > c.radius))
          break;
        if (c.flying)
        {
          if (Mathd.Abs(c.addedVelocity.x) < 15)
          {
            if (c.position.x < this.spell.position.x)
            {
              ZCreature zcreature = c;
              zcreature.addedVelocity = zcreature.addedVelocity + new MyLocation(-15, 0);
            }
            else
            {
              ZCreature zcreature = c;
              zcreature.addedVelocity = zcreature.addedVelocity + new MyLocation(15, 0);
            }
          }
          c.addVelocity = true;
          break;
        }
        MyLocation myLocation1 = c.position - this.spell.position;
        myLocation1.Normalize();
        myLocation1 *= 15728L;
        if (myLocation1.x == 0)
          myLocation1.x = (FixedInt) 10485L;
        if (myLocation1.y > Mathd.Abs(myLocation1.x))
        {
          FixedInt x = myLocation1.x;
          myLocation1.x = myLocation1.y * (x > 0 ? 1 : -1);
          myLocation1.y = Mathd.Abs(x);
        }
        ZCreature zcreature1 = c;
        zcreature1.addedVelocity = zcreature1.addedVelocity + myLocation1;
        c.addVelocity = true;
        break;
      case EffectorType.Naplem:
        this.active = false;
        ZSpell spell1 = this.spell;
        this.map.ServerBitBlt(5, (int) this.position.x, (int) this.position.y);
        spell1.isDead = true;
        spell1.isNull = true;
        ZSpell.ApplyExplosionForce(SpellEnum.Napalm, this.world, this.collider.position, spell1.damage, spell1.forceOverDistance, spell1.radius, spell1.EXORADIUS, spell1.explisiveForce, spell1.damageType, this.whoSummoned, this.TurnCreated, spellRef: (ISpellBridge) Inert.GetSpell("Napalm"), firstHit: c);
        if (this.game.isClient)
          AudioManager.Play(this.soundClip);
        this.DestroyNoExplosion();
        break;
      case EffectorType.Portal:
      case EffectorType.Wormhole:
        this.EffectEntity((ZEntity) c);
        break;
      case EffectorType.Storm_Shield:
      case EffectorType.Electrostatic_Charge:
        if (!((ZComponent) c != (object) null) || c.isDead || !((ZComponent) this.whoSummoned != (object) null) || this.whoSummoned.isDead || c.parent == null || c.parent == this.whoSummoned.parent || c.parent.team == this.whoSummoned.parent.team || c.race == CreatureRace.Effector && !(c.GetType() == typeof (ZCreatureTree)) || c.health <= 0)
          break;
        this.variable -= 5;
        if (this.variable <= 0)
        {
          this.active = false;
          if (this.game.isClient)
          {
            this.VisualUpdate();
            AudioManager.Play(this.soundClip);
          }
        }
        if ((ZComponent) c != (object) null)
          c.ApplyDamage(SpellEnum.Storm_Shield, DamageType.Shock, 5, this.whoSummoned, this.TurnCreated, this.type == EffectorType.Storm_Shield ? (ISpellBridge) Inert.GetSpell("Storm Shield") : (ISpellBridge) Inert.GetSpell("Electrostatic Charge"));
        if ((ZComponent) c != (object) null)
        {
          if (Mathd.Abs(c.position.x - this.whoSummoned.position.x) < 2 && (Mathd.Abs(c.velocity.x) < 3 || c.velocity.x < 0 && c.position.x < c.map.Width / 2 || c.velocity.x > 0 && c.position.x > c.map.Width / 2))
            c.ApplyExplosionForce(10, new MyLocation(c.position.x + (c.position.x > c.map.Width / 2 ? 5 : -5), c.position.y - 10), (FixedInt) 1572864L, 10, Curve.Generic);
          else
            c.ApplyExplosionForce(10, new MyLocation(c.position.x + (c.position.x > this.whoSummoned.position.x ? 5 : -5), c.position.y - 10), (FixedInt) 1572864L, 10, Curve.Generic);
        }
        if (!((ZComponent) c != (object) null))
          break;
        c.StartMoving(false);
        break;
      case EffectorType.Aura_of_decay:
      case EffectorType.Dragon_Aura_of_Decay:
        if (!this.gameObjectActive)
        {
          this.gameObjectActive = true;
          if ((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
            this.gameObject.SetActive(true);
          this.game.forceRysncPause = true;
        }
        List<ZMyCollider> zmyColliderList1 = this.world.OverlapCircleAll((Point) this.position, this.variable, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
        List<ZCreature> zcreatureList1 = new List<ZCreature>();
        Spell auraSpell = ZEffector.GetAuraSpell(this.damageType);
        for (int index = 0; index < zmyColliderList1.Count; ++index)
        {
          if (zmyColliderList1[index].gameObjectLayer == 13)
          {
            ZTower tower = zmyColliderList1[index].tower;
            if ((ZComponent) tower != (object) null && tower.type != TowerType.Nature)
            {
              this.active = false;
              if (this.game.isClient && !this.game.resyncing && (UnityEngine.Object) tower.transform != (UnityEngine.Object) null)
                ZEffector.SpawnAuraExplosion(tower.transform.position, this.damageType);
              if (this.damageType != DamageType.Arcane || (ZComponent) this.whoSummoned != (object) tower.creature || tower.type == TowerType.Arcane || tower.creature.FullArcane || tower.creature.type == CreatureType.Imp || tower.creature.type == CreatureType.Egg)
                tower.ApplyDamage(auraSpell.spellEnum, this.damageType, this.GetAuraDamage(this.damageType), this.whoSummoned, this.TurnCreated, (ISpellBridge) auraSpell);
              ZCreature zcreature2 = this.map.PhysicsCollideCreature(tower.creature, (int) tower.position.x, (int) tower.position.y - 25);
              if (this.damageType == DamageType.Death && (ZComponent) zcreature2 != (object) null && (ZComponent) zcreature2 != (object) this.whoSummoned && (ZComponent.IsNull((ZComponent) zcreature2.tower) || zcreature2.tower.type != TowerType.Nature) && (zcreature2.race != CreatureRace.Effector || zcreature2.GetType() == typeof (ZCreatureTree)) && this.damageType != DamageType.Infection)
                zcreatureList1.Add(zcreature2);
              ZEffector.SpawnIndicatorOfDecay(tower.creature, this);
            }
          }
          else
          {
            ZCreature creature = zmyColliderList1[index].creature;
            if ((ZComponent) creature != (object) null && (creature.race != CreatureRace.Effector || creature.GetType() == typeof (ZCreatureTree)))
            {
              this.active = false;
              if (this.game.isClient && !this.game.resyncing && (UnityEngine.Object) creature.transform != (UnityEngine.Object) null)
                ZEffector.SpawnAuraExplosion(creature.transform.position, this.damageType);
              bool flag = true;
              if ((this.damageType != DamageType.Arcane || (ZComponent) this.whoSummoned != (object) creature || (ZComponent) creature.tower != (object) null && creature.tower.type == TowerType.Arcane || creature.FullArcane || creature.type == CreatureType.Imp || creature.type == CreatureType.Egg) && creature.type != CreatureType.Dryad)
                creature.ApplyDamage(auraSpell.spellEnum, this.damageType, this.GetAuraDamage(this.damageType), this.whoSummoned, this.TurnCreated, (ISpellBridge) auraSpell);
              else
                flag = false;
              if (creature.flying && !creature.isMoving)
                creature.SetVelocity(0, 3);
              else if (ZComponent.IsNull((ZComponent) creature.tower) && ZComponent.IsNull((ZComponent) creature.mount) && !creature.isMoving)
                creature.ApplyExplosionForce(128, this.AuraForce(creature), (FixedInt) 692060L, 128, Curve.None, true);
              if (flag && (creature.race != CreatureRace.Effector || creature.type == CreatureType.Tree))
              {
                ZCreature z = this.map.PhysicsCollideCreature(creature, (int) creature.position.x, (int) creature.position.y - creature.radius - 10);
                if (ZComponent.IsNull((ZComponent) z))
                  z = this.map.PhysicsCollideCreature(creature, (int) creature.position.x + 5, (int) creature.position.y - creature.radius - 10);
                if (ZComponent.IsNull((ZComponent) z))
                  z = this.map.PhysicsCollideCreature(creature, (int) creature.position.x - 5, (int) creature.position.y - creature.radius - 10);
                if (this.damageType == DamageType.Death && (ZComponent) z != (object) null && ((ZComponent) z != (object) this.whoSummoned || this.type == EffectorType.Aura_of_decay) && (ZComponent.IsNull((ZComponent) z.tower) || z.tower.type != TowerType.Nature) && (z.race != CreatureRace.Effector || z.GetType() == typeof (ZCreatureTree)) && this.damageType != DamageType.Infection && z.type != CreatureType.Dryad && (this.damageType != DamageType.Arcane || (ZComponent) this.whoSummoned != (object) z || (ZComponent) z.tower != (object) null && z.tower.type == TowerType.Arcane || z.FullArcane || z.type == CreatureType.Imp || z.type == CreatureType.Egg) && z.type != CreatureType.Dryad)
                {
                  if ((ZComponent) z.rider != (object) null && (ZComponent) creature != (object) z.rider)
                  {
                    zcreatureList1.Add(z);
                    zcreatureList1.Add(z);
                  }
                  zcreatureList1.Add(z);
                }
              }
              ZEffector.SpawnIndicatorOfDecay(creature, this);
            }
          }
        }
        foreach (ZCreature c1 in zcreatureList1)
        {
          if ((ZComponent) c1 != (object) null && c1.health > 0)
          {
            c1.ApplyDamage(auraSpell.spellEnum, this.damageType, this.GetAuraDamage(this.damageType), this.whoSummoned, this.TurnCreated, (ISpellBridge) auraSpell);
            if (!c1.flying && ZComponent.IsNull((ZComponent) c1.tower) && ZComponent.IsNull((ZComponent) c1.mount))
              c1.ApplyExplosionForce(128, this.AuraForce(c1), (FixedInt) 2, 128, Curve.None, true);
          }
        }
        if (!this.game.isClient)
          break;
        this.ToggleAura();
        break;
      case EffectorType.Lich_Aura_of_decay:
        if (!this.gameObjectActive)
        {
          this.gameObjectActive = true;
          if ((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
            this.gameObject.SetActive(true);
          this.game.forceRysncPause = true;
        }
        List<ZMyCollider> zmyColliderList2 = this.world.OverlapCircleAll((Point) this.position, this.variable, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
        Spell lichAuraSpell = ZEffector.GetLichAuraSpell(this.damageType);
        List<ZCreature> zcreatureList2 = new List<ZCreature>();
        for (int index = 0; index < zmyColliderList2.Count; ++index)
        {
          if (zmyColliderList2[index].gameObjectLayer == 13)
          {
            ZTower tower = zmyColliderList2[index].tower;
            if ((ZComponent) tower != (object) null && ((ZComponent) tower.creature != (object) this.whoSummoned || this.damageType == DamageType.Infection) && tower.type != TowerType.Nature)
            {
              this.active = false;
              if (this.game.isClient && !this.game.resyncing && (UnityEngine.Object) tower.transform != (UnityEngine.Object) null)
                ZEffector.SpawnAuraExplosion(tower.transform.position, this.damageType);
              tower.ApplyDamage(lichAuraSpell.spellEnum, this.damageType, lichAuraSpell.damage, this.GetInfector, this.TurnCreated, (ISpellBridge) lichAuraSpell);
              if (this.damageType == DamageType.Infection)
                ZEffector.SpreadInfection(this.GetInfector, tower?.creature, this);
              ZCreature zcreature3 = this.map.PhysicsCollideCreature(tower.creature, (int) tower.position.x, (int) tower.position.y - 25);
              if (this.damageType == DamageType.Death && (ZComponent) zcreature3 != (object) null && ((ZComponent) zcreature3 != (object) this.whoSummoned || this.damageType == DamageType.Infection) && (ZComponent.IsNull((ZComponent) zcreature3.tower) || zcreature3.tower.type != TowerType.Nature) && (zcreature3.race != CreatureRace.Effector || zcreature3.GetType() == typeof (ZCreatureTree)) && this.damageType != DamageType.Infection)
                zcreatureList2.Add(zcreature3);
              ZEffector.SpawnIndicatorOfDecay(tower.creature, this);
            }
          }
          else
          {
            ZCreature creature = zmyColliderList2[index].creature;
            if ((ZComponent) creature != (object) null && ((ZComponent) creature != (object) this.whoSummoned || this.damageType == DamageType.Infection) && (creature.race != CreatureRace.Effector || creature.GetType() == typeof (ZCreatureTree)) && creature.type != CreatureType.Dryad && (this.damageType != DamageType.Sting || creature.type != CreatureType.Bee && creature.type != CreatureType.Beehive || creature.parent.team != this.whoSummoned.parent.team))
            {
              this.active = false;
              if (this.game.isClient && !this.game.resyncing && (UnityEngine.Object) creature.transform != (UnityEngine.Object) null)
                ZEffector.SpawnAuraExplosion(creature.transform.position, this.damageType);
              creature.ApplyDamage(lichAuraSpell.spellEnum, this.damageType, lichAuraSpell.damage, this.GetInfector, this.TurnCreated, (ISpellBridge) lichAuraSpell);
              if (this.damageType == DamageType.Infection)
                ZEffector.SpreadInfection(this.GetInfector, creature, this);
              if (creature.flying && !creature.isMoving)
                creature.SetVelocity(0, 3);
              else if (ZComponent.IsNull((ZComponent) creature.tower) && ZComponent.IsNull((ZComponent) creature.mount) && !creature.isMoving)
                creature.ApplyExplosionForce(128, this.AuraForce(creature), (FixedInt) 1, 128, Curve.None, true);
              if (creature.race != CreatureRace.Effector || creature.type == CreatureType.Tree)
              {
                ZCreature z = this.map.PhysicsCollideCreature(creature, (int) creature.position.x, (int) creature.position.y - creature.radius - 10);
                if (ZComponent.IsNull((ZComponent) z))
                  z = this.map.PhysicsCollideCreature(creature, (int) creature.position.x + 5, (int) creature.position.y - creature.radius - 10);
                if (ZComponent.IsNull((ZComponent) z))
                  z = this.map.PhysicsCollideCreature(creature, (int) creature.position.x - 5, (int) creature.position.y - creature.radius - 10);
                if (this.damageType == DamageType.Death && (ZComponent) z != (object) null && ((ZComponent) z != (object) this.whoSummoned || this.damageType == DamageType.Infection) && (ZComponent.IsNull((ZComponent) z.tower) || z.tower.type != TowerType.Nature) && (z.race != CreatureRace.Effector || z.GetType() == typeof (ZCreatureTree)) && this.damageType != DamageType.Infection && creature.type != CreatureType.Dryad)
                {
                  if ((ZComponent) z.rider != (object) null && (ZComponent) creature != (object) z.rider)
                  {
                    zcreatureList2.Add(z);
                    zcreatureList2.Add(z);
                  }
                  zcreatureList2.Add(z);
                }
              }
              ZEffector.SpawnIndicatorOfDecay(creature, this);
            }
          }
        }
        foreach (ZCreature c2 in zcreatureList2)
        {
          if ((ZComponent) c2 != (object) null && c2.health > 0)
          {
            c2.ApplyDamage(lichAuraSpell.spellEnum, this.damageType, 15, this.GetInfector, this.TurnCreated, (ISpellBridge) lichAuraSpell);
            if (this.damageType == DamageType.Infection)
              ZEffector.SpreadInfection(this.GetInfector, c2, this);
            if (!c2.flying && ZComponent.IsNull((ZComponent) c2.tower) && ZComponent.IsNull((ZComponent) c2.mount))
              c2.ApplyExplosionForce(128, this.AuraForce(c2), (FixedInt) 2, 128, Curve.None, true);
          }
        }
        if (!this.active && this.damageType == DamageType.Sting && (ZComponent) this.whoSummoned != (object) null && !this.whoSummoned.isDead)
          this.whoSummoned.OnStunned();
        if (!this.game.isClient)
          break;
        this.ToggleAura();
        break;
      case EffectorType.Soul_Jar:
        break;
      case EffectorType.Sanctuary:
        ZEffector.EffectSanctuary(this.game, c);
        break;
      case EffectorType.Troll:
        if (!((ZComponent) c != (object) null) || !((ZComponent) c != (object) this.whoSummoned) || c.race == CreatureRace.Effector && c.type != CreatureType.Tree)
          break;
        int variable = this.variable;
        if ((ZComponent) this.whoSummoned != (object) null && c.parent.team == this.whoSummoned.parent.team && this.whoSummoned.type != CreatureType.Water_Troll)
          break;
        this.active = false;
        this.whoSummoned?.animator?.Play(AnimateState.Swing);
        c.ApplyDamage(SpellEnum.Aura_of_Decay, DamageType.None, variable, this.whoSummoned, this.TurnCreated, (ISpellBridge) Inert.GetSpell("Summon Water Trolls"));
        if (!this.game.isClient)
          break;
        AudioManager.Play(this.soundClip);
        break;
      case EffectorType.Dark_Totem:
        break;
      case EffectorType.MoneyBags:
        if ((ZComponent) this.whoSummoned == (object) c)
          break;
        if (this.game.isClient)
        {
          if (this.variable == 0)
            AudioManager.Play(this.soundClip);
          else if (this.variable == 1 && !this.game.resyncing && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null && (double) HUD.instance.pickup_music < 10.0)
          {
            if ((double) HUD.instance.pickup_music <= 0.0)
            {
              AudioManager.instance?.StopAllCoroutines();
              AudioManager.instance?.StartCoroutine(AudioManager.instance.FadePickupMusic(false));
            }
            HUD.instance.pickup_music += 5f;
          }
        }
        this.Destroy();
        break;
      case EffectorType.Life_Dew:
        if (ZComponent.IsNull((ZComponent) c))
          break;
        this.active = false;
        if (!ZComponent.IsNull((ZComponent) this.whoSummoned))
        {
          int? team1 = c.parent?.team;
          int? team2 = this.whoSummoned?.parent?.team;
          if (team1.GetValueOrDefault() == team2.GetValueOrDefault() & team1.HasValue == team2.HasValue)
          {
            if (c.type == CreatureType.Bee)
            {
              this.active = true;
              if (this.variable < 30)
                this.variable += 15;
              c.OnDeath(true);
              break;
            }
            c.DoHeal(40 + this.variable);
            goto label_52;
          }
        }
        if (this.game.currentSeason == GameSeason.Winter)
          ZSpell.OnExplosionWaterBall(Inert.Instance.spells["Ice Ball"], this.game, this.whoSummoned, this.position);
        else
          ZSpell.OnExplosionWaterBall(Inert.Instance.spells["Water Ball"], this.game, this.whoSummoned, this.position);
        if ((ZComponent) c != (object) null && !c.isDead)
        {
          if (c.isMoving)
            c.velocity = (this.position - c.position).normalized * 3;
          c.ApplyDamage(SpellEnum.Life_Dew, DamageType.None, 10, this.whoSummoned, c.game.turn);
        }
label_52:
        if (this.game.isClient)
          AudioManager.Play(this.soundClip);
        this.Destroy();
        break;
      case EffectorType.Morning_Sun:
        this.MorningSunEffect(c);
        break;
      case EffectorType.Miner_Map:
        if (!((ZComponent) c != (object) null) || c.spellEnum != SpellEnum.Summon_Dwarf)
          break;
        this.DestroyNoExplosion();
        break;
      case EffectorType.Myth:
        if (!((ZComponent) c != (object) null) || !((ZComponent) c != (object) this.whoSummoned) || c.parent.team == this.whoSummoned.parent.team || c.race == CreatureRace.Effector && c.type != CreatureType.Tree)
          break;
        this.active = false;
        this.whoSummoned?.animator?.Play(AnimateState.Swing);
        c.ApplyDamage(SpellEnum.Summon_Myth, DamageType.None, this.variable, this.whoSummoned, this.TurnCreated, (ISpellBridge) Inert.GetSpell(SpellEnum.Summon_Myth));
        if (!this.game.isClient)
          break;
        AudioManager.Play(this.soundClip);
        break;
      case EffectorType.Prickly_Barrier:
        if ((ZComponent) c == (object) null || !this.active || c.type == CreatureType.Tree || c.race == CreatureRace.Effector || (ZComponent) c == (object) this.whoSummoned || (ZComponent) this.whoSummoned != (object) null && c.parent == this.whoSummoned.parent)
          break;
        if (this.variable > -666 && (ZComponent) this.whoSummoned != (object) null)
        {
          foreach (ZEffector effector in this.whoSummoned.effectors)
          {
            if ((ZComponent) effector != (object) null && effector.type == EffectorType.Prickly_Barrier)
            {
              effector.active = false;
              effector.variable = this.whoSummoned.parent.localTurn;
              effector.VisualUpdate();
            }
          }
        }
        this.active = false;
        if (this.variable > -666)
          this.variable = this.whoSummoned.parent.localTurn;
        this.VisualUpdate();
        c.ApplyDamage(this.spell.spellEnum, DamageType.None, this.spell.damage, this.whoSummoned, 0, (ISpellBridge) this.spell);
        c.ApplyExplosionForce(this.collider.radius, this.position, this.spell.explisiveForce, this.spell.EXORADIUS, this.spell.forceOverDistance, true);
        AudioManager.Play(this.spell.explosionClip);
        break;
      case EffectorType.Blackhole:
        if ((ZComponent) c == (object) null || c.type == CreatureType.Tree || c.UnaffectedByNaturesWraith || Mathd.Abs(c.addedVelocity.x) > 3 || Mathd.Abs(c.addedVelocity.y) > 3)
          break;
        if ((ZComponent) c.tower != (object) null)
          c.DestroyTower();
        if (!c.isMoving)
          break;
        MyLocation a = c.position - this.position;
        FixedInt fixedInt = (FixedInt) MyLocation.Distance(a, MyLocation.zero);
        if (fixedInt < 64 + c.radius)
        {
          for (int index = this.game.globalEffectors.Count - 1; index >= 0; --index)
          {
            ZEffector globalEffector = this.game.globalEffectors[index];
            if ((ZComponent) globalEffector != (object) null && globalEffector.type == EffectorType.Wormhole && globalEffector.active && (ZComponent) globalEffector.whoSummoned != (object) null && globalEffector.whoSummoned.parent.team == c.parent.team)
            {
              List<ZEntity> colliders = new List<ZEntity>()
              {
                (ZEntity) c
              };
              if (Mathd.Abs(c.velocity.x) > 5)
                c.velocity.x = (FixedInt) (c.velocity.x > 0 ? 5 : -5);
              if (c.velocity.y > 5)
                c.velocity.y = (FixedInt) 5;
              else if (c.velocity.y < -5)
                c.velocity.y = (FixedInt) -5;
              c.velocity.y = -c.velocity.y;
              c.velocity.x = -c.velocity.x;
              if (globalEffector.PortalLogic(colliders))
                globalEffector.PostPortalLogic(colliders);
              if (!((UnityEngine.Object) c.transform != (UnityEngine.Object) null) || c.game.resyncing)
                return;
              ZComponent.Instantiate<GameObject>(this.explosion, c.transform.position, Quaternion.identity, this.game.GetMapTransform());
              return;
            }
          }
          if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null && !c.game.resyncing)
            ZComponent.Instantiate<GameObject>(this.explosion, c.transform.position, Quaternion.identity, this.game.GetMapTransform());
          if (c.isPawn)
          {
            c.OnDeath(true);
            break;
          }
          c.EnteredWater();
          c.pY = (FixedInt) -10010;
          c.validY = (FixedInt) -10010;
          c.velocity.y = (FixedInt) 0;
          break;
        }
        c.affectedByGravity = true;
        a.Normalize();
        MyLocation myLocation2 = a * (Mathd.Max((FixedInt) 1, (FixedInt) (this.collider.radius + 30) - fixedInt) / (this.collider.radius + c.radius + 30));
        if (myLocation2.x == 0)
          myLocation2.x = (FixedInt) 10485L;
        if (!c.isMoving)
          break;
        ZCreature zcreature4 = c;
        zcreature4.addedVelocity = zcreature4.addedVelocity - myLocation2 * 20971L;
        c.addVelocity = true;
        break;
      case EffectorType.Butterfly_Jar:
        break;
      case EffectorType.AutumnLeaves:
        if ((ZComponent) c == (object) null || c.type == CreatureType.Tree)
          break;
        if ((ZComponent) this.whoSummoned != (object) null)
        {
          int? team3 = c.parent?.team;
          int? team4 = this.whoSummoned.parent?.team;
          if (team3.GetValueOrDefault() == team4.GetValueOrDefault() & team3.HasValue == team4.HasValue)
            break;
        }
        this.active = false;
        ZSpell spell2 = this.spell;
        this.map.ServerBitBlt(5, (int) this.position.x, (int) this.position.y);
        spell2.isDead = true;
        spell2.isNull = true;
        ZSpell.ApplyExplosionForce(SpellEnum.Napalm, this.world, this.collider.position, spell2.damage, spell2.forceOverDistance, spell2.radius, spell2.EXORADIUS, spell2.explisiveForce, spell2.damageType, this.whoSummoned, this.TurnCreated, spellRef: (ISpellBridge) Inert.GetSpell("Napalm"), firstHit: c);
        this.game.ongoing.RunCoroutine(this.RetroactiveDisableLeaf(this.collider.radius));
        if (this.game.isClient)
          AudioManager.Play(this.soundClip);
        this.DestroyNoExplosion();
        break;
      case EffectorType.Javelin:
        if (!((ZComponent) this.whoSummoned == (object) c))
          break;
        SpellSlot spellSlot = this.whoSummoned.GetSpellSlot(SpellEnum.Verdant_Javelin);
        if (spellSlot != null)
          spellSlot.SetTurnFired = -1;
        else
          this.whoSummoned.AddPresent(SpellEnum.Verdant_Javelin);
        if (this.game.isClient && !this.game.resyncing && (UnityEngine.Object) this.explosion != (UnityEngine.Object) null)
          ZComponent.Instantiate<GameObject>(this.explosion, this.position.ToSinglePrecision(), Quaternion.identity);
        this.DestroyNoExplosion();
        break;
      default:
        Debug.LogError((object) ("Wrong ZEffector: " + (object) this.type + " trying to Effect: " + c?.name));
        break;
    }
  }

  public static void EffectSanctuary(ZGame game, ZCreature c)
  {
    if (ZComponent.IsNull((ZComponent) c) || c.isDead || c.type == CreatureType.Tree)
      return;
    if (c.GetType() == typeof (ZCreatureThorn))
    {
      c.OnDeath(true);
    }
    else
    {
      int num = 0;
      if (c.destroyableEffectors.Count > 0)
      {
        for (int index = c.destroyableEffectors.Count - 1; index >= 0; --index)
        {
          if (!ZComponent.IsNull((ZComponent) c.destroyableEffectors[index]))
          {
            c.destroyableEffectors[index].collider?.Disable(false);
            if (c.destroyableEffectors[index].type == EffectorType.Static_Ball)
              c.destroyableEffectors[index].Die(index, true, false);
            if (index < c.destroyableEffectors.Count && !ZComponent.IsNull((ZComponent) c.destroyableEffectors[index]))
            {
              c.destroyableEffectors[index].isNull = true;
              ZComponent.Destroy<GameObject>(c.destroyableEffectors[index].gameObject);
              ++num;
            }
            else
              continue;
          }
          game.forceRysncPause = true;
        }
        c.destroyableEffectors.Clear();
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(c.transform.position);
      }
      if (!ZComponent.IsNull((ZComponent) c.auraOfDecay) && c.auraOfDecay.gameObjectActive)
      {
        c.auraOfDecay.active = false;
        c.auraOfDecay.gameObjectActive = false;
        game.forceRysncPause = true;
        c.auraOfDecay.gameObject?.SetActive(false);
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(c.transform.position);
        ++num;
      }
      if ((ZComponent) c.stormShield != (object) null && c.stormShield.type != EffectorType.Troll && c.stormShield.type != EffectorType.Myth)
        c.stormShield = (ZEffector) null;
      c.retribution = 0;
      if (c.hasDarkDefenses)
      {
        ++num;
        c.hasDarkDefenses = false;
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(c.transform.position);
      }
      if (c.shield > 0)
      {
        num += c.shield / 50;
        c.shield = 0;
        c.entangledShield = -1;
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(c.transform.position);
        c.UpdateHealthTxt();
      }
      if (c.entangled)
      {
        c.RemoveEntangle();
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(c.transform.position);
      }
      if (c.gravitionalPull)
      {
        c.appliedGravity = 0;
        if (!c.isMoving && (ZComponent) c.tower != (object) null)
          c.tower.ShouldFall();
        if ((UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(c.transform.position);
      }
      if (c.flying && !c.permenantFlight)
      {
        ++num;
        c.RemoveFlight();
      }
      if (num <= 0 || c.parent == null)
        return;
      ZCreature zcreature = c.game.CurrentCreature();
      if (!((ZComponent) zcreature != (object) null) || zcreature.team == c.team || zcreature.parent == null)
        return;
      zcreature.parent.sactuaryShieldsRemoved += num;
    }
  }

  public void ToggleMorningSun() => this.VisualUpdate();

  public void ToggleAura()
  {
    if ((UnityEngine.Object) this.transform == (UnityEngine.Object) null)
      return;
    if (this.damageType == DamageType.Arcane)
    {
      if (this.active)
      {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
        this.transform.GetChild(1).gameObject.SetActive(true);
      }
      else
      {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        this.transform.GetChild(1).gameObject.SetActive(false);
      }
    }
    else if (this.damageType == DamageType.Heal20 || this.damageType == DamageType.Infection)
    {
      float num = this.damageType != DamageType.Infection || this.turnsAlive < 4 ? 1f : 0.0f;
      if (this.active)
      {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(num, num, num, 0.8f);
        this.transform.GetChild(1).gameObject.SetActive(true);
      }
      else
      {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(num, num, num, this.damageType != DamageType.Infection || this.turnsAlive < 5 ? 0.4f : 0.2f);
        this.transform.GetChild(1).gameObject.SetActive(false);
      }
    }
    else if (this.damageType == DamageType.Sting)
    {
      if (this.active)
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 0.56f, 0.0f, 0.3f);
      else
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 0.56f, 0.0f, 0.15f);
    }
    else if (this.active)
      this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
    else
      this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.15f);
  }

  public void DestroyNoExplosion()
  {
    this.isNull = true;
    this.active = false;
    this.collider?.Disable();
    ZComponent.Destroy<GameObject>(this.gameObject);
    this.game.forceRysncPause = true;
  }

  internal void Destroy()
  {
    this.isNull = true;
    this.active = false;
    this.collider?.Disable();
    if ((UnityEngine.Object) this.explosion != (UnityEngine.Object) null && (UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      ZComponent.Instantiate<GameObject>(this.explosion, this.transform.position, Quaternion.identity);
    ZComponent.Destroy<GameObject>(this.gameObject);
    this.game.forceRysncPause = true;
  }

  public void SlowlyDeactivate(int time = 90)
  {
    if (this.coro != null)
      return;
    this.coro = this.game.ongoing.RunSpell(this.IESlowlyDeactivate(time));
  }

  public IEnumerator<float> IESlowlyDeactivate(int time)
  {
    ZEffector z = this;
    for (int i = 0; i < time; ++i)
      yield return 0.0f;
    if (!z.dead && !ZComponent.IsNull((ZComponent) z))
    {
      z.active = false;
      if (z.type == EffectorType.Flame_Wall)
        z.spell.effector.active = false;
      z.VisualUpdate();
      z.coro = (IEnumerator<float>) null;
    }
  }

  public void VisualUpdate()
  {
    if (!this.game.isClient || (UnityEngine.Object) this.transform == (UnityEngine.Object) null)
      return;
    if (this.rotation != 0)
      this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.rotation.ToFloat());
    switch (this.type)
    {
      case EffectorType.Fire_Shield:
        if (!this.active)
        {
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 100);
          break;
        }
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        break;
      case EffectorType.Flame_Wall:
        if (!this.active)
        {
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 100);
          break;
        }
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        break;
      case EffectorType.Flame_WallBase:
        if (this.active)
          break;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 100);
        break;
      case EffectorType.Naplem:
        if (!this.game.isClient || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
          break;
        this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        this.gameObject.GetComponent<ParticleSystem>().Play();
        break;
      case EffectorType.Portal:
        this.transform.GetChild(0).gameObject.SetActive(this.active);
        this.transform.GetChild(1).gameObject.SetActive(this.active);
        this.transform.GetChild(2).gameObject.SetActive(this.active);
        this.transform.GetChild(3).GetComponent<RotateAroundCenter>().enabled = this.active;
        break;
      case EffectorType.Storm_Shield:
      case EffectorType.Electrostatic_Charge:
        this.transform.GetChild(0).gameObject.SetActive(true);
        if (this.active)
        {
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
          break;
        }
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 100);
        break;
      case EffectorType.Aura_of_decay:
      case EffectorType.Lich_Aura_of_decay:
      case EffectorType.Dragon_Aura_of_Decay:
        this.ToggleAura();
        break;
      case EffectorType.Call_of_the_Void:
        if (!this.game.isClient || !((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null))
          break;
        int radius = 64;
        if (this.variable == 50)
          radius = 136;
        else if (this.variable == 75)
          radius = 208;
        else if (this.variable == 100)
          radius = 280;
        else if (this.variable == 125)
          radius = 355;
        this.game.map.CallOfTheVoid((int) this.position.x, (int) this.position.y, radius);
        this.gameObject.GetComponent<ParticleSystem>().shape.radius = (float) (radius - 44);
        ParticleSystem component = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        component.shape.radius = (float) radius;
        component.emission.rateOverTime = (ParticleSystem.MinMaxCurve) (float) radius;
        break;
      case EffectorType.Morning_Sun:
        if (this.active)
        {
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.399f, 0.4745098f);
          break;
        }
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.399f, 0.1333333f);
        break;
      case EffectorType.Prickly_Barrier:
        if (!this.active)
        {
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 100);
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ClientResources.Instance.prickly_sprites[this.clientSpriteIndex];
          this.transform.GetChild(1).gameObject.SetActive(false);
          break;
        }
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ClientResources.Instance.prickly_sprites[this.clientSpriteIndex];
        this.transform.GetChild(1).gameObject.SetActive(true);
        break;
      case EffectorType.Wormhole:
        bool flag = this.active && this.variable != 666;
        this.transform.GetChild(0).gameObject.SetActive(flag);
        this.transform.GetChild(1).gameObject.SetActive(flag);
        this.transform.GetChild(2).gameObject.SetActive(flag);
        this.transform.GetChild(3).GetComponent<RotateAroundCenter>().enabled = flag;
        break;
      case EffectorType.GravityWell:
        if (!this.active)
        {
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 100);
          break;
        }
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        break;
      case EffectorType.AutumnLeaves:
        if ((ZComponent) this.spell != (object) null)
          this.transform.localScale = new Vector3(this.spell.goToTarget ? 1f : -1f, 1f, 1f);
        this.transform.GetChild(0).gameObject.SetActive(this.active);
        this.transform.GetComponent<SpriteRenderer>().color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, this.active ? byte.MaxValue : (byte) 100);
        break;
      case EffectorType.Rising_Lava:
        float num1 = (float) (this.map.Height * this.variable / 75);
        if (this.halved)
          num1 /= 2f;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2((float) (this.game.map.Width + 2100), 36f);
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().size = new Vector2((float) (this.game.map.Width + 2100), (float) (this.game.map.Height + 1000));
        this.transform.GetChild(0).localPosition = new Vector3(this.transform.GetChild(0).localPosition.x, num1 - 18f);
        this.transform.GetChild(1).localPosition = new Vector3(this.transform.GetChild(1).localPosition.x, num1 - 36f);
        break;
      case EffectorType.Dense_Fog:
        float num2 = (float) -(this.map.Height * this.variable / 75);
        if (this.halved)
          num2 /= 2f;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2((float) (this.game.map.Width + 2100), 36f);
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().size = new Vector2((float) (this.game.map.Width + 2100), (float) (this.game.map.Height + 1000));
        this.transform.GetChild(0).localPosition = new Vector3(this.transform.GetChild(0).localPosition.x, num2 + 18f);
        this.transform.GetChild(1).localPosition = new Vector3(this.transform.GetChild(1).localPosition.x, num2 + 36f);
        break;
    }
  }

  private void FireMaelstrom(Spell s, bool leftSide)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    ZSpell zspell = ZSpell.BaseFire(s, this.whoSummoned, this.position + new MyLocation(leftSide ? -100 : 100, 33), Quaternion.identity, !leftSide ? Inert.Velocity((FixedInt) 100, this.game.RandomFixedInt((FixedInt) s.speedMin, (FixedInt) s.speedMax)) : Inert.Velocity((FixedInt) 80, this.game.RandomFixedInt((FixedInt) s.speedMin, (FixedInt) s.speedMax)), isChild: true);
    zspell.fromArmageddon = this.fromArmageddon;
    zspell.name = "Maelstrom";
  }

  private void FireShootingStar(Spell s, int angleVarience = 0)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    MyLocation myLocation = new MyLocation(this.variable > 0 ? -1000 : this.game.map.Width + 1000, this.game.map.Height + 100);
    ZSpell zspell = ZSpell.BaseFire(s, this.whoSummoned, myLocation, Quaternion.identity, Inert.Velocity(Inert.AngleOfVectors(this.position, myLocation) + (angleVarience == 0 ? (FixedInt) 0 : this.game.RandomFixedInt(-angleVarience, angleVarience)), 40), isChild: true);
    zspell.fromArmageddon = this.fromArmageddon;
    zspell.name = "Shooting Stars";
  }

  private void FireMaelstromMiddle(Spell s)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    ZSpell zspell = ZSpell.BaseFire(s, this.whoSummoned, this.position + new MyLocation(this.game.RandomFixedInt(-30, 30), (FixedInt) 33), Quaternion.identity, Inert.Velocity((FixedInt) 90, this.game.RandomFixedInt((FixedInt) s.speedMin, (FixedInt) s.speedMax) - 2), isChild: true);
    zspell.fromArmageddon = this.fromArmageddon;
    zspell.name = "Maelstrom";
  }

  private void FireFissure(Spell s)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    FixedInt angle = this.game.RandomFixedInt(85, 95);
    ZSpell zspell = ZSpell.BaseFire(s, this.whoSummoned, this.position + new MyLocation(0, 33), Quaternion.identity, Inert.Velocity(angle, this.game.RandomFixedInt((FixedInt) s.speedMax - 25, (FixedInt) s.speedMax + 5)), isChild: true);
    zspell.fromArmageddon = this.fromArmageddon;
    if (!this.game.isClient)
      return;
    zspell.name = "Fissure";
    if (this.game.resyncing)
      return;
    AudioManager.Play(s.castClip);
  }

  private void Fire(Spell s)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    MyLocation myLocation = Inert.Velocity(this.game.RandomFixedInt(80, 100), this.game.RandomFixedInt((FixedInt) s.speedMax - 5, (FixedInt) s.speedMax + 5));
    ZSpell s1 = ZSpell.BaseFire(s, this.whoSummoned, this.position + new MyLocation(0, 33), Quaternion.identity, myLocation, false, true);
    s1.fromArmageddon = this.fromArmageddon;
    s1.maxDuration = (int) this.game.RandomFixedInt(s1.maxDuration / 2, s1.maxDuration);
    ZSpell.UpgradeFullFire(this.whoSummoned, s1);
    s1.SetVelocity(myLocation);
    if (!this.game.isClient)
      return;
    s1.name = "Volcano";
    if (this.game.resyncing)
      return;
    AudioManager.Play(s.castClip);
  }

  private void FireSupernova(Spell s)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    MyLocation myLocation = Inert.Velocity(this.game.RandomFixedInt(0, 360), this.game.RandomFixedInt((FixedInt) s.speedMax - 5, (FixedInt) s.speedMax + 5));
    ZSpell zspell = ZSpell.BaseFire(s, this.whoSummoned, this.position + new MyLocation(0, 33), Quaternion.identity, myLocation, false, true);
    zspell.fromArmageddon = this.fromArmageddon;
    zspell.maxDuration = (int) this.game.RandomFixedInt(zspell.maxDuration / 2, zspell.maxDuration);
    zspell.SetVelocity(myLocation);
    if (!this.game.isClient)
      return;
    zspell.name = "Supernova";
    if (this.game.resyncing)
      return;
    AudioManager.Play(s.castClip);
  }

  private void MakeVolcano(int i)
  {
    if (this.dead || ZComponent.IsNull((ZComponent) this))
      return;
    Texture2D texture2D = Inert.Instance._volcanoTex[i];
    MyLocation position = this.position with
    {
      y = (FixedInt) texture2D.height
    };
    this.position = position;
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.position = (Vector3) position.ToSinglePrecision();
    this.map.ServerBitBlt(61 + i, (int) this.position.x, texture2D.height / 2, false);
    ZSpell.RemoveItemsOnBitBlt(this.game, SpellEnum.Volcano, (int) this.position.x, texture2D.height / 2, i);
  }

  public IEnumerator<float> IEVolcano()
  {
    ZEffector z = this;
    if (!z.dead && !ZComponent.IsNull((ZComponent) z))
    {
      z.active = false;
      Spell Fireball = Inert.Instance.spells["Fire Ball"];
      Spell LavaBomb = Inert.Instance.spells["Lava Bomb"];
      Spell NapalmBomb = Inert.Instance.spells["Napalm Bomb"];
      int i;
      if (z.turnsAlive == 1)
      {
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.MakeVolcano(1);
        for (i = 0; i < 75; ++i)
          yield return 0.0f;
        z.MakeVolcano(1);
      }
      else if (z.turnsAlive == 2)
      {
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        for (i = 0; i < 5; ++i)
          yield return 0.0f;
        z.MakeVolcano(2);
      }
      else if (z.turnsAlive == 3)
      {
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(LavaBomb);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        for (i = 0; i < 5; ++i)
          yield return 0.0f;
        z.MakeVolcano(3);
      }
      else if (z.turnsAlive == 4)
      {
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        z.Fire(LavaBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(LavaBomb);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        z.Fire(LavaBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        for (i = 0; i < 5; ++i)
          yield return 0.0f;
        z.MakeVolcano(4);
      }
      else if (z.turnsAlive == 5)
      {
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        z.Fire(LavaBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(LavaBomb);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        z.Fire(LavaBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.Fire(Fireball);
        z.Fire(Fireball);
        z.Fire(NapalmBomb);
        for (i = 0; i < 5; ++i)
          yield return 0.0f;
        z.MakeVolcano(5);
        // ISSUE: reference to a compiler-generated method
        int index = z.whoSummoned.effectors.FindIndex(new Predicate<ZEffector>(z.\u003CIEVolcano\u003Eb__118_0));
        z.Die(index, false, false);
      }
    }
  }

  public IEnumerator<float> IESupernova()
  {
    ZEffector z = this;
    if (!z.dead && !ZComponent.IsNull((ZComponent) z))
    {
      z.active = false;
      Spell Fireball = Inert.GetSpell(SpellEnum.Star_Dust);
      Spell bomb = Inert.GetSpell(SpellEnum.Star_Ball);
      int max;
      int i;
      if (z.turnsAlive == 1)
      {
        max = 30 + ((ZComponent) z.whoSummoned != (object) null ? z.whoSummoned.familiarLevelCosmos * 2 : 0);
        for (i = 0; i < max; ++i)
        {
          z.FireSupernova(Fireball);
          yield return 0.0f;
        }
      }
      else if (z.turnsAlive == 2)
      {
        max = 30 + ((ZComponent) z.whoSummoned != (object) null ? z.whoSummoned.familiarLevelCosmos * 2 : 0);
        for (i = 0; i < max; ++i)
        {
          z.FireSupernova(Fireball);
          if (i % 3 == 0)
            z.FireSupernova(bomb);
          yield return 0.0f;
        }
      }
      else if (z.turnsAlive == 3)
      {
        max = 35 + ((ZComponent) z.whoSummoned != (object) null ? z.whoSummoned.familiarLevelCosmos * 2 : 0);
        for (i = 0; i < max; ++i)
        {
          z.FireSupernova(Fireball);
          if (i % 9 == 0)
            z.FireSupernova(bomb);
          yield return 0.0f;
        }
      }
      else if (z.turnsAlive == 4)
      {
        max = 40 + ((ZComponent) z.whoSummoned != (object) null ? z.whoSummoned.familiarLevelCosmos * 2 : 0);
        for (i = 0; i < max; ++i)
        {
          z.FireSupernova(Fireball);
          if (i % 9 == 0)
            z.FireSupernova(bomb);
          yield return 0.0f;
        }
      }
      else if (z.turnsAlive == 5)
      {
        max = 45 + ((ZComponent) z.whoSummoned != (object) null ? z.whoSummoned.familiarLevelCosmos * 2 : 0);
        for (i = 0; i < max; ++i)
        {
          z.FireSupernova(Fireball);
          if (i % 9 == 0)
            z.FireSupernova(bomb);
          yield return 0.0f;
        }
        // ISSUE: reference to a compiler-generated method
        int index = z.whoSummoned.effectors.FindIndex(new Predicate<ZEffector>(z.\u003CIESupernova\u003Eb__119_0));
        z.Die(index, false, false);
      }
    }
  }

  public IEnumerator<float> IEFissure()
  {
    ZEffector z = this;
    if (!z.dead && !ZComponent.IsNull((ZComponent) z))
    {
      z.active = false;
      Spell scatterRock = Inert.Instance.spells["Scatter Rock"];
      if (z.turnsAlive == 1)
      {
        z.FireFissure(scatterRock);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireFissure(scatterRock);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireFissure(scatterRock);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireFissure(scatterRock);
      }
      else if (z.turnsAlive == 2)
      {
        for (int i = 0; i < 5; ++i)
        {
          z.FireFissure(scatterRock);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
          z.FireFissure(scatterRock);
          z.FireFissure(scatterRock);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
          z.FireFissure(scatterRock);
          z.FireFissure(scatterRock);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
          z.FireFissure(scatterRock);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
        }
        z.FireFissure(scatterRock);
      }
    }
  }

  public IEnumerator<float> IEShootingStars()
  {
    ZEffector z = this;
    if (!z.dead && !ZComponent.IsNull((ZComponent) z))
    {
      z.active = false;
      Spell spell = Inert.GetSpell(SpellEnum.Starfire);
      if (z.turnsAlive == 1)
        z.FireShootingStar(spell);
      else if (z.turnsAlive == 2)
      {
        z.FireShootingStar(spell, 3);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireShootingStar(spell, 3);
      }
      else
      {
        z.FireShootingStar(spell, 3);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireShootingStar(spell, 3);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireShootingStar(spell, 3);
        // ISSUE: reference to a compiler-generated method
        int index = z.whoSummoned.effectors.FindIndex(new Predicate<ZEffector>(z.\u003CIEShootingStars\u003Eb__121_0));
        z.Die(index, false, false);
      }
    }
  }

  public IEnumerator<float> IEMaelStrom()
  {
    ZEffector z = this;
    if (!z.dead && !ZComponent.IsNull((ZComponent) z))
    {
      z.active = false;
      Spell scatterRock = Inert.Instance.maelstromDrop;
      if (z.turnsAlive == 1)
      {
        z.FireMaelstrom(scatterRock, true);
        z.FireMaelstrom(scatterRock, false);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireMaelstrom(scatterRock, true);
        z.FireMaelstrom(scatterRock, false);
        z.FireMaelstromMiddle(scatterRock);
        z.FireMaelstromMiddle(scatterRock);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireMaelstrom(scatterRock, true);
        z.FireMaelstrom(scatterRock, false);
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
        z.FireMaelstrom(scatterRock, true);
        z.FireMaelstrom(scatterRock, false);
      }
      else if (z.turnsAlive == 2 || z.turnsAlive == 3)
      {
        for (int i = 0; i < 4; ++i)
        {
          z.FireMaelstrom(scatterRock, true);
          z.FireMaelstrom(scatterRock, false);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
          z.FireMaelstrom(scatterRock, true);
          z.FireMaelstrom(scatterRock, false);
          z.FireMaelstromMiddle(scatterRock);
          z.FireMaelstromMiddle(scatterRock);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
          z.FireMaelstrom(scatterRock, true);
          z.FireMaelstrom(scatterRock, false);
          z.FireMaelstromMiddle(scatterRock);
          z.FireMaelstromMiddle(scatterRock);
          yield return 0.0f;
          yield return 0.0f;
          yield return 0.0f;
          z.FireMaelstrom(scatterRock, true);
          z.FireMaelstrom(scatterRock, false);
        }
        if (z.turnsAlive == 3)
        {
          // ISSUE: reference to a compiler-generated method
          int index = z.whoSummoned.effectors.FindIndex(new Predicate<ZEffector>(z.\u003CIEMaelStrom\u003Eb__122_0));
          z.Die(index, false, false);
        }
      }
    }
  }
}
