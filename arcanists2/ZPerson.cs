

using Hazel;
using Hazel.Tcp;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class ZPerson
{
  public ZGame game;
  public int team = -1;
  public byte id;
  public int localTurn = -1;
  public bool yourTurn;
  public bool ready;
  public bool sendResync;
  public bool offeringDraw;
  public bool offeringRematch;
  public bool winner;
  public short startingRating = 1000;
  public short addedRating;
  public int gainedWands;
  public bool wasFirst;
  public bool MinionMaster;
  public bool BombMaster;
  public bool movedThisTurn;
  public int communeWithNature;
  public float countdown;
  public int lastArmageddon = -100;
  public HashSet<SpellEnum> ritualEnum = new HashSet<SpellEnum>();
  public List<ZCreature> controlled = new List<ZCreature>();
  public List<ZCreature> stolenMinions = new List<ZCreature>();
  public List<ZCreature> takenMinions = new List<ZCreature>();
  public List<ZGame.MinionBookTitan> minionBookTitans = new List<ZGame.MinionBookTitan>();
  internal IEnumerator<float> resyncIE;
  internal bool clientResyncing;
  public bool isFake;
  public int[] towerHealth = new int[11];
  public bool host;
  public Connection connection = (Connection) new TcpConnection();
  public string name = "-";
  public SettingsPlayer settingsPlayer;
  public Account account = new Account();
  public PanelPlayer panelPlayer;
  public List<ZFamiliar> familiars;
  public List<SpellSlot> arcaneGateSpellSlot;
  public Color clientColor = Color.gray;
  public HashSet<SpellSlot> randomSyncedZSpells = new HashSet<SpellSlot>();
  public Torquing torquing;
  public List<GameObject> archMageStaffs;
  public ZCreature bloodBank;
  public int timesOutfitChanged;
  public FamiliarType familiarBook;
  public BookOf ActivateableFamiliar = BookOf.Nothing;
  public bool drainable = true;
  public int waterMultipler = 1;
  public int bid = -1;
  public BanStage bidStage;
  public IAI ai;
  public int inactiveTurns;
  public bool hasCastTheFourSeasons;
  public bool seasonISHoliday;
  public Dictionary<SpellEnum, SpellsCast> spellsCast = new Dictionary<SpellEnum, SpellsCast>();
  public int curDamageDealtInOneAttack;
  public int MaxDamageDealtInOneAttack;
  public int spellsCastTHISTurn;
  internal bool Zombie_Monkey_Killer;
  internal bool FullArcane;
  internal bool didResign;
  internal bool didLeave;
  internal int sactuaryShieldsRemoved;
  internal int bombKillsFromGround;
  private SpellsCast _spellCast;
  private int curMoveID;
  internal int lastMoveID;
  internal int[] familiarLevels;
  public ZPerson.Awards awards = new ZPerson.Awards();

  public ZMyWorld world => this.game.world;

  public ZMap map => this.game.map;

  public bool IsSelectable(int i)
  {
    return i < this.controlled.Count && !ZComponent.IsNull((ZComponent) this.controlled[i]) && this.controlled[i].controllable;
  }

  public int GetMinionCount()
  {
    for (int index = this.stolenMinions.Count - 1; index >= 0; --index)
    {
      if ((ZComponent) this.stolenMinions[index] == (object) null || this.stolenMinions[index].isDead)
        this.stolenMinions.RemoveAt(index);
    }
    return this.controlled.Count + this.stolenMinions.Count;
  }

  public void ReleaseTakenMinion()
  {
    for (int i = 0; i < this.takenMinions.Count; ++i)
    {
      if ((ZComponent) this.takenMinions[i] != (object) null)
      {
        foreach (ZPerson player in this.game.players)
        {
          int index = player.stolenMinions.FindIndex((Predicate<ZCreature>) (z => (ZComponent) z == (object) this.takenMinions[i]));
          if (index >= 0)
          {
            player.stolenMinions.RemoveAt(index);
            player.first()?.panelPlayer?.SetSummons(player);
            return;
          }
        }
      }
      else
      {
        this.takenMinions.RemoveAt(i);
        --i;
      }
    }
  }

  public int GetMaxMinions()
  {
    return 5 + (this.game.originalSpellsOnly ? 0 : (this.familiarLevels[0] + 1) / 2);
  }

  public int MaxRitualCount() => this.familiarLevels[13] >= 5 ? 10 : 3 + this.familiarLevels[13];

  public Connection GetMultiConnection
  {
    get
    {
      return !this.game.isMulti || this.game.GetTeam(this.team).Count <= 0 ? this.connection : this.game.GetTeam(this.team)[0].connection;
    }
  }

  public bool GetMultiConnected
  {
    get
    {
      return !this.game.isMulti || this.game.GetTeam(this.team).Count <= 0 ? this.Connected : this.game.GetTeam(this.team)[0].Connected;
    }
  }

  public bool Connected
  {
    get
    {
      return this.connection != null && this.connection.State == ConnectionState.Connected && this.connection.player.gameNumber == this.game.gameFacts.id;
    }
  }

  public bool ConnectedToOld(int id)
  {
    if (this.connection == null || this.connection.State != ConnectionState.Connected)
      return false;
    return this.connection.player.gameNumber == this.game.gameFacts.id || this.connection.player.gameNumber == id;
  }

  public int GetNextMoveID() => ++this.curMoveID;

  public void ResetMoveID() => this.curMoveID = 0;

  public void UpdateCountdown()
  {
    if ((double) this.countdown <= 0.0 || (double) this.game.serverState.turnTime < (double) this.game.gameFacts.settings.countdownDelay)
      return;
    this.countdown -= Time.deltaTime;
    if ((double) this.countdown > 0.0)
      return;
    this.countdown = 0.0f;
    if (!this.game.isServer || !this.game.countdownLose)
      return;
    this.game.Resign(this, true);
  }

  public void AddGate(SpellSlot s)
  {
    if (this.arcaneGateSpellSlot == null)
      this.arcaneGateSpellSlot = new List<SpellSlot>();
    this.arcaneGateSpellSlot.Add(s);
  }

  public void SetGates(int turn)
  {
    if (this.arcaneGateSpellSlot == null)
      return;
    for (int index = this.arcaneGateSpellSlot.Count - 1; index >= 0; --index)
    {
      if (this.arcaneGateSpellSlot[index] == null || this.arcaneGateSpellSlot[index].isPresent && this.arcaneGateSpellSlot[index].UsedUses > 0)
        this.arcaneGateSpellSlot.RemoveAt(index);
      else
        this.arcaneGateSpellSlot[index].SetTurnFired = turn;
    }
  }

  public ZFamiliar GetFamiliar(BookOf book)
  {
    if (this.familiars == null)
      return (ZFamiliar) null;
    for (int index = this.familiars.Count - 1; index >= 0; --index)
    {
      if (ZComponent.IsNull((ZComponent) this.familiars[index]))
        this.familiars.RemoveAt(index);
      else if (this.familiars[index].bookOf == book)
        return this.familiars[index];
    }
    return (ZFamiliar) null;
  }

  public void RemoveFamiliar(BookOf book)
  {
    if (this.familiars == null)
      return;
    for (int index = this.familiars.Count - 1; index >= 0; --index)
    {
      if ((ZComponent) this.familiars[index] == (object) null)
        this.familiars.RemoveAt(index);
      else if (this.familiars[index].bookOf == book)
      {
        this.familiars[index].soulJar?.collider?.Disable();
        this.familiars[index].soulJar?.SetNull();
        if ((UnityEngine.Object) this.familiars[index].gameObject != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) this.familiars[index].gameObject);
        this.familiars.RemoveAt(index);
      }
    }
  }

  internal void IncreaseLevel() => ++this.familiarLevels[(int) this.ActivateableFamiliar];

  internal int GetLevel(BookOf b)
  {
    return b < BookOf.Arcane || b >= (BookOf) this.familiarLevels.Length ? 0 : this.familiarLevels[(int) b];
  }

  public bool CanOfferDraw() => this.controlled.Count != 0;

  public ZCreature first() => this.controlled.Count > 0 ? this.controlled[0] : (ZCreature) null;

  public bool InWater() => this.controlled.Count != 0 && this.controlled[0].inWater;

  public ZPerson(Connection c)
  {
    this.connection = c;
    this.Init();
  }

  public ZPerson() => this.Init();

  private void Init() => this.familiarLevels = new int[(int) (RandomExtensions.LastBook() + 1)];

  public void IncreaseCastCount(SpellEnum e)
  {
    if (this.spellsCast.TryGetValue(e, out this._spellCast))
      ++this._spellCast.count;
    else
      this.spellsCast[e] = new SpellsCast() { count = 1 };
  }

  public void IncreaseSpellDamage(SpellEnum e, int dmg)
  {
    if (this.spellsCast.TryGetValue(e, out this._spellCast))
      this._spellCast.damage += dmg;
    else
      this.spellsCast[e] = new SpellsCast() { damage = dmg };
  }

  public int GetSpellCast(SpellEnum e)
  {
    return this.spellsCast.TryGetValue(e, out this._spellCast) ? this._spellCast.count : 0;
  }

  public int GetSpellDamage(SpellEnum e)
  {
    return this.spellsCast.TryGetValue(e, out this._spellCast) ? this._spellCast.damage : 0;
  }

  public static void GetAwards(ZGame game, StringBuilder msg)
  {
    List<ZPerson> zpersonList = new List<ZPerson>();
    int num1 = 0;
    for (int index = 0; index < game.players.Count; ++index)
    {
      zpersonList.Add(game.players[index]);
      num1 += (int) zpersonList[index].startingRating;
      zpersonList[index].awards.Finialize();
    }
    if (num1 <= 0)
      return;
    int num2 = num1 / zpersonList.Count;
  }

  public class Awards
  {
    private SpellEnum lastZSpellCast;
    private int spellCastStreak;
    private HashSet<SpellEnum> spellsCasted = new HashSet<SpellEnum>();
    public int numZSpellsCasted;
    private MyLocation posStarted = MyLocation.zero;
    private int hits;
    public int Most_Damage;
    public int Most_Powerful;
    public int Most_Minions;
    public int Most_Kills;
    public int Most_Skillful;
    public int Minion_Master;
    public int Most_Wet;
    public int Most_Useless;
    public int Most_Resilient;
    public int Most_Cruel;
    public int Most_Honourable;
    public int Most_Annoying;
    public int Most_Fit;
    public int Most_Bouncy;
    public int Dragon_Master;
    public int Imp_Master;
    public int Zombie_Monkey_Killer;
    public bool Sniper;
    public bool Black_out_the_sun;
    public bool OnlyMinionDamage = true;
    public int minionMountedDamageSaved;
    public int dragonsSummoned;
    public int Subversion;
    public int throwingArm;
    public int[] spellTypesCasted = new int[13];
    public int[] spellTypesBrought = new int[13];
    public int Healing;

    public bool NoBombsCasted()
    {
      if (this.spellTypesCasted[0] > 0 || this.spellTypesCasted[1] > 0)
        return false;
      for (int index = 3; index < this.spellTypesCasted.Length; ++index)
      {
        if (this.spellTypesCasted[index] > 0)
          return false;
      }
      return true;
    }

    public void Finialize()
    {
      if (this.numZSpellsCasted == 0)
      {
        this.Most_Annoying = 0;
        this.Most_Skillful = 0;
        this.Most_Powerful = 0;
      }
      else
      {
        this.Most_Annoying += this.numZSpellsCasted / this.spellsCasted.Count;
        this.Most_Skillful = this.hits * 100 / this.numZSpellsCasted;
        this.Most_Powerful = this.Most_Damage / this.numZSpellsCasted;
      }
      this.Most_Honourable = this.Most_Cruel;
      this.Most_Useless = this.Most_Damage + this.Healing;
    }

    public void Summoned(int c) => this.Most_Minions += c;

    public void FellInWater() => ++this.Most_Wet;

    public void TurnStartedAt(MyLocation pos) => this.posStarted = pos;

    public void TurnEndedAt(MyLocation pos)
    {
      this.Most_Resilient += (int) Mathd.Abs(pos.x - this.posStarted.x);
    }

    public void OnCasted(Spell s)
    {
      ++this.spellTypesCasted[(int) s.spellType];
      this.Casted(s.spellEnum);
    }

    private void Casted(SpellEnum s)
    {
      ++this.numZSpellsCasted;
      if (!this.spellsCasted.Contains(s))
        this.spellsCasted.Add(s);
      if (s == this.lastZSpellCast)
      {
        ++this.spellCastStreak;
        if (this.spellCastStreak < 2)
          return;
        ++this.Most_Annoying;
      }
      else
      {
        this.spellCastStreak = 0;
        this.lastZSpellCast = s;
      }
    }

    public void Moved() => ++this.Most_Fit;

    public void Bouncy() => ++this.Most_Bouncy;

    public void Kill(ZCreature me, ZCreature enemy)
    {
      if (me.team != enemy.team && !enemy.isPawn)
      {
        ++this.Most_Kills;
      }
      else
      {
        if (enemy.type != CreatureType.Monkey || enemy.race != CreatureRace.Undead)
          return;
        ++this.Zombie_Monkey_Killer;
      }
    }

    public void DealtDamge(
      ZCreature me,
      ZCreature enemy,
      int damage,
      SpellEnum spellEnum,
      ISpellBridge spellRef)
    {
      if (me.team != enemy.team)
      {
        if (damage > 20)
          ++this.hits;
        if (me.isPawn)
          this.Minion_Master += damage;
        this.Most_Damage += damage;
        me.parent.curDamageDealtInOneAttack += damage;
        if (spellRef != null && !spellRef.FromArmageddon)
          me.parent.IncreaseSpellDamage(spellEnum, damage);
        if (me.parent.curDamageDealtInOneAttack > me.parent.MaxDamageDealtInOneAttack)
        {
          me.parent.MaxDamageDealtInOneAttack = me.parent.curDamageDealtInOneAttack;
          if (me.parent.MaxDamageDealtInOneAttack >= 250 && me.game.isRated && !me.game.isClient)
            Achievements.Set(Achievement.Critical_Hit, me.parent.account, me.game);
        }
        if (me.parent.curDamageDealtInOneAttack >= 100 && (this.lastZSpellCast == SpellEnum.Swallowing_Pit || this.lastZSpellCast == SpellEnum.Conductor_Rod) && !me.game.isClient && me.game.isRated)
        {
          if (me.parent.curDamageDealtInOneAttack >= 200 && this.lastZSpellCast == SpellEnum.Conductor_Rod)
            Achievements.Set(Achievement.Dust_to_Dust, me.parent.account, me.game);
          else if (this.lastZSpellCast == SpellEnum.Swallowing_Pit)
            Achievements.Set(Achievement.Miasma, me.parent.account, me.game);
        }
        if (me.type == CreatureType.Imp)
        {
          this.Imp_Master += damage;
        }
        else
        {
          if (me.type != CreatureType.Dragon)
            return;
          this.Dragon_Master += damage;
        }
      }
      else
        this.Most_Cruel += damage;
    }

    public void Heal(ZCreature me, ZCreature enemy, int damage)
    {
      if (me.team != enemy.team)
        return;
      this.Healing += damage;
    }
  }
}
