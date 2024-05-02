

using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable disable
public class SavedFrame
{
  public class ZGame
  {
    public List<SavedFrame.ZPerson> players = new List<SavedFrame.ZPerson>();
    public List<byte[]> timeline = new List<byte[]>();
    public byte[] mapData;

    public static SavedFrame.ZGame Serialize(myBinaryWriter writer, SavedFrame.ZGame game)
    {
      SavedFrame.ZGame zgame = new SavedFrame.ZGame();
      foreach (SavedFrame.ZPerson player in game.players)
        SavedFrame.ZPerson.Serialize(writer, player);
      return zgame;
    }

    public static SavedFrame.ZGame Deserialize(myBinaryReader reader) => new SavedFrame.ZGame();
  }

  public class ZPerson : SavedFrame.ZComponent
  {
    public List<SavedFrame.ZCreature> creatures = new List<SavedFrame.ZCreature>();
    public SettingsPlayer settingsPlayer;
    public FamiliarType familiar;
    public int familiarLevelArcane;
    public int familiarLevelFlame;
    public int familiarLevelStone;
    public int familiarLevelStorm;
    public int familiarLevelFrost;
    public int familiarLevelUnderdark;
    public int familiarLevelOverlight;
    public int familiarLevelNature;
    public int familiarLevelSeas;
    public int familiarLevelCogs;
    public int familiarLevelSeasons;
    public int familiarLevelIllusion;
    public int familiarLevelBlood;

    public static SavedFrame.ZPerson Serialize(myBinaryWriter writer, SavedFrame.ZPerson person)
    {
      return new SavedFrame.ZPerson();
    }

    public static SavedFrame.ZPerson Deserialize(myBinaryReader reader) => new SavedFrame.ZPerson();
  }

  public class ZCreature : SavedFrame.ZEntity
  {
    public SavedFrame.ZCollider collider;
    public SpellEnum spellEnum;
    public List<SavedFrame.ZSpellSlot> spells = new List<SavedFrame.ZSpellSlot>();
    public int health;
    public int maxHealth;
    public FixedInt speed;
    public EditorFixedInt massMulti;
    public EditorFixedInt minAngle;
    public EditorFixedInt maxAngle;
    internal bool tempFlight;
    public bool pawn;
    public bool flying;
    public bool mountable;
    public bool canMount;
    public bool phantom;
    public bool gliding;
    public bool canMove = true;
    public bool usingGlide = true;
    public Vector2 LongjumpValues = new Vector2(8f, 9f);
    public Vector2 HighjumpValues = new Vector2(2f, 13f);
    public EditorVector2 LongJumpData;
    public EditorVector2 HighJumpData;
    public GameObject deathExplosion;
    public Sprite undeadHead;
    public SavedFrame.ZPerson parent;
    public SavedFrame.ZPerson originalParent;
    public TMP_Text txtHealth;
    public TMP_Text txtShield;
    public CreatureType type;
    public CreatureRace race;
    public bool waterWalking;
    public bool frostWalking;
    public bool moneyBags;
    public int moneyFrames;
    public int pactID;
    public bool removeFlight;
    public float scale = 1f;
    public FixedInt scaleFixed = (FixedInt) 1;
    public bool FullArcane;
    public bool UnaffectedByNaturesWraith;
    public int shield;
    public int team = -1;
    public int mount;
    public int rider;
    public bool inTower;
    public SavedFrame.ZTower tower;
    public bool colliderGameObject;
    public int invulnerable;
    public bool socialDistancing;
    public bool stunned;
    public bool superStunned;
    public bool entangled;
    public bool hasDarkDefenses;
    public bool inWater;
    public bool diesInWater = true;
    public int bleedCounter;
    public Effector stormShield;
    public Effector auraOfDecay;
    public int flightObj;
    public bool shiningPower;
    public int turnProtectionShieldCast = -1;
    public int entangledShield = -1;
    public bool vine_bloom_active;
    public int additionalBleedOutDamage;
    public int prestigeIndex = -1;

    public bool bleeding
    {
      get => this.bleedCounter > 0;
      set => this.bleedCounter = value ? 3 : 0;
    }

    public static SavedFrame.ZCreature Serialize(myBinaryWriter writer, Creature creature)
    {
      SavedFrame.ZCreature zcreature = new SavedFrame.ZCreature();
      SavedFrame.ZCollider.Serialize(writer, creature.collider);
      return zcreature;
    }

    public static SavedFrame.ZCreature Deserialize(myBinaryReader reader)
    {
      return new SavedFrame.ZCreature();
    }
  }

  public class ZEffector : SavedFrame.ZComponent
  {
    public static SavedFrame.ZEffector Serialize(myBinaryWriter writer, Effector effector)
    {
      SavedFrame.ZEffector zeffector = new SavedFrame.ZEffector();
      SavedFrame.ZCollider.Serialize(writer, effector.collider);
      return zeffector;
    }

    public static SavedFrame.ZEffector Deserialize(myBinaryReader reader)
    {
      return new SavedFrame.ZEffector();
    }
  }

  public class ZCollider : SavedFrame.ZComponent
  {
    public MyLocation position;
    public bool enabled;
    public int layer = 256;
    public int offsetX;
    public int offsetY;
    public int radius;
    public List<MyCollider.XY> points;
    public MyCollider.Shape shape;
    public StaticPolygons.CachedPolygonEnums cachedPolygon;

    public static SavedFrame.ZCollider Serialize(myBinaryWriter writer, MyCollider collider)
    {
      return new SavedFrame.ZCollider();
    }

    public static SavedFrame.ZCollider Deserialize(myBinaryReader reader)
    {
      return new SavedFrame.ZCollider();
    }
  }

  public class ZSpell : SavedFrame.ZEntity
  {
    public SpellEnum spellEnum;
    public MyLocation position;
    public int curLifetime;
    public int bounces;
    public SavedFrame.ZEffector effector;

    public static SavedFrame.ZSpell Serialize(myBinaryWriter writer, Spell spell)
    {
      return new SavedFrame.ZSpell();
    }

    public static SavedFrame.ZSpell Deserialize(myBinaryReader reader) => new SavedFrame.ZSpell();
  }

  public class ZSpellSlot : SavedFrame.ZComponent
  {
    public string spell = "";
    public bool syncWithParent;
    public bool isPresent;
    public bool isLevel5;
    public int MaxUses = -1;
    public int RechargeTime;
    public bool EndsTurn = true;
    public int TurnsTillFirstUse;

    public static SavedFrame.ZSpellSlot Serialize(myBinaryWriter writer, SpellSlot slot)
    {
      return new SavedFrame.ZSpellSlot();
    }

    public static SavedFrame.ZSpellSlot Deserialize(myBinaryReader reader)
    {
      return new SavedFrame.ZSpellSlot();
    }
  }

  public class ZTower : SavedFrame.ZComponent
  {
    public MyLocation position;
    public int health;
    public int maxHealth;
    public bool effectorActive;

    public static SavedFrame.ZTower Serialize(myBinaryWriter writer, Tower tower)
    {
      return new SavedFrame.ZTower();
    }

    public static SavedFrame.ZTower Deserialize(myBinaryReader reader) => new SavedFrame.ZTower();
  }

  public class ZEntity : SavedFrame.ZComponent
  {
  }

  public class ZComponent
  {
    public int hashID;
  }
}
