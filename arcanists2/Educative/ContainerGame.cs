

using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class ContainerGame
  {
    [MoonSharpHidden]
    public ZGame game;
    [MoonSharpHidden]
    public Tutorial tutorial;

    [MoonSharpHidden]
    public ContainerGame(ZGame game, Tutorial t)
    {
      this.game = game;
      this.tutorial = t;
    }

    public ContainerCreature getUser() => new ContainerCreature(this.game.players[0].first());

    public Table getPlayers(Script script)
    {
      Table players = new Table(script);
      int key = 1;
      foreach (ZPerson player in this.game.players)
      {
        players[(object) key] = (object) new ContainerPlayer(player);
        ++key;
      }
      return players;
    }

    public int getPlayerCount() => this.game.players.Count;

    public Table getCreatures(Script script)
    {
      Table creatures = new Table(script);
      int key = 1;
      foreach (ZPerson player in this.game.players)
      {
        foreach (ZCreature c in player.controlled)
        {
          creatures[(object) key] = (object) new ContainerCreature(c);
          ++key;
        }
      }
      if (this.game._uncontrolledPlayer != null)
      {
        foreach (ZCreature c in this.game._uncontrolledPlayer.controlled)
        {
          creatures[(object) key] = (object) new ContainerCreature(c);
          ++key;
        }
      }
      return creatures;
    }

    public Table findCreatures(Script script, Point point, int radius)
    {
      Table creatures = new Table(script);
      int key = 1;
      foreach (ZMyCollider zmyCollider in this.game.world.OverlapCircleAll(new global::Point((int) point.x, (int) point.y), radius, (ZMyCollider) null, 73984))
      {
        ZCreature creature = zmyCollider.creature;
        if ((ZComponent) creature != (object) null)
        {
          creatures[(object) key] = (object) new ContainerCreature(creature);
          ++key;
        }
        else
        {
          ZTower tower = zmyCollider.tower;
          if ((ZComponent) tower != (object) null && (ZComponent) tower.creature != (object) null)
          {
            creatures[(object) key] = (object) new ContainerCreature(tower.creature);
            ++key;
          }
        }
      }
      return creatures;
    }

    public Table findEffectors(Script script, Point point, int radius)
    {
      Table effectors = new Table(script);
      int key = 1;
      foreach (ZMyCollider zmyCollider in this.game.world.OverlapCircleAll(new global::Point((int) point.x, (int) point.y), radius, (ZMyCollider) null, 3584))
      {
        ZEffector effector = zmyCollider.effector;
        if ((ZComponent) effector != (object) null)
        {
          effectors[(object) key] = (object) new ContainerEffector(effector);
          ++key;
        }
      }
      return effectors;
    }

    public Point LineCast(
      Point a,
      Point b,
      ContainerCreature ignore = null,
      bool includeCreatures = true,
      bool includeEffectors = false,
      bool includePhantom = false)
    {
      int mask = includeCreatures ? 8448 : 0;
      if (includePhantom)
        mask |= 65536;
      if (includeEffectors)
        mask |= 3584;
      Coords coords = this.game.map.bresenhamsLineCast(new Coords((int) a.x, (int) a.y), new Coords((int) b.x, (int) b.y), ignore?.creature, (ZSpell) null, mask);
      return coords == null ? (Point) null : new Point((double) coords.x, (double) coords.y);
    }

    public ContainerCreature LineCastOnlyCreatures(
      Point a,
      Point b,
      ContainerCreature ignore = null,
      bool includeCreatures = true,
      bool includePhantom = false)
    {
      int mask = includeCreatures ? 8448 : 0;
      if (includePhantom)
        mask |= 65536;
      ZCreature c = this.game.map.bresenhamsLineCastOnlyCreatures(new Coords((int) a.x, (int) a.y), new Coords((int) b.x, (int) b.y), ignore?.creature, mask);
      return !((ZComponent) c != (object) null) ? (ContainerCreature) null : new ContainerCreature(c);
    }

    public Point LineCastOnlyTerrain(Point a, Point b)
    {
      Coords coords = this.game.map.bresenhamsLineCastOnlyTerrain(new Coords((int) a.x, (int) a.y), new Coords((int) b.x, (int) b.y));
      return coords == null ? (Point) null : new Point((double) coords.x, (double) coords.y);
    }

    public void ShowInfo(string message, bool onContinue = false, bool pauseGame = false, bool unpauseGame = true)
    {
      this.tutorial.ShowInfo(message, onContinue, pauseGame, unpauseGame);
    }

    public int turn
    {
      get => this.game.turn;
      set => this.game.turn = Mathf.Clamp(value, 0, int.MaxValue);
    }

    public int timeLimit
    {
      get => this.game.MaxTurnTime;
      set
      {
        this.game.sandboxTime = value;
        this.game.MaxTurnTime = value;
        this.game.PlayersMaxTurnTime = (float) value;
        this.game.serverState.turnTime = 0.0f;
      }
    }

    public double totalTimeElapsed
    {
      get => (double) Time.realtimeSinceStartup - (double) this.tutorial.startTime;
    }

    public double turnTime => (double) this.game.serverState.turnTime;

    public int frame => this.game.currentFrame;

    public int random(int lower, int upper) => this.game.RandomInt(lower, upper);

    public bool winOnDeath
    {
      get => this.game.winOnDeath;
      set => this.game.winOnDeath = value;
    }

    public bool allowMovement
    {
      get => this.game.AllowMovement;
      set => this.game.AllowMovement = value;
    }

    public bool allowInput
    {
      get => this.game.AllowInput;
      set => this.game.AllowInput = value;
    }

    public bool allowCallbacks
    {
      get => this.game.AllowCallbacks;
      set => this.game.AllowCallbacks = value;
    }

    public bool allowSkipTurn
    {
      get => Player.Instance.canSkipTurn;
      set => Player.Instance.canSkipTurn = value;
    }

    public bool terrainDestruction
    {
      get => this.game.AllowTerrainDestruction;
      set => this.game.AllowTerrainDestruction = value;
    }

    public bool takeDamage
    {
      get => this.game.players[0].first().invulnerable < 99999;
      set => this.game.players[0].first().invulnerable = value ? 0 : 99999;
    }

    public MapEnum armageddon
    {
      get => this.game.armageddon;
      set
      {
        this.game.armageddon = value;
        this._OnArmageddon();
      }
    }

    public int armageddonTurn
    {
      get => this.game.armageddonTurn;
      set
      {
        this.game.armageddonTurn = value;
        this._OnArmageddon();
      }
    }

    public bool waiting => this.tutorial.waitingForContinue;

    public bool paused
    {
      get => this.game.tutorialPaused;
      set => this.game.tutorialPaused = value;
    }

    public bool busy => this.game.serverState.busy != ServerState.Busy.No;

    public bool ongoing => this.game.ongoing.NumberOfSlowUpdateCoroutines > 0;

    public double gravity
    {
      get => this.game.gravity.ToDouble();
      set => this.game.map.Gravity = FixedInt.Create(value);
    }

    public bool wind
    {
      get => this.game.isWindy;
      set
      {
        this.game.isWindy = value;
        WindIndicatorUI.Instance?.gameObject.SetActive(value);
      }
    }

    public double windDir
    {
      get => this.game.map.windDir.ToDouble();
      set
      {
        this.game.map.windDir = FixedInt.Create(value);
        this.game.map.UpdateWind();
        WindIndicatorUI.Instance?.Refresh();
      }
    }

    public double windPower
    {
      get => this.game.map.windSpeed.ToDouble();
      set
      {
        this.game.map.windSpeed = FixedInt.Create(value);
        this.game.map.UpdateWind();
        WindIndicatorUI.Instance?.Refresh();
      }
    }

    public int width => this.game.map.Width;

    public int height => this.game.map.Height;

    public Point mousePosition
    {
      get => new Point((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public bool mouseOverUI => Player.IsPointerOverGameObject();

    public Point worldToCanvas(Point p)
    {
      return new Point(CanvasMain.WorldToCanvasPosition(new Vector3((float) p.x, (float) p.y)));
    }

    public Point canvasToWorld(Point p)
    {
      return new Point((Vector2) CanvasMain.CanvasToWorldPosition((Vector2) new Vector3((float) p.x, (float) p.y)));
    }

    public Point cameraPosition
    {
      get => this.VectorToPoint(CameraMovement.Instance.transform.position);
      set
      {
        CameraMovement.Instance.KillMovement();
        CameraMovement.Instance.GotoPosition(new Vector3((float) value.x, (float) value.y));
      }
    }

    public double cameraZoom
    {
      get => (double) CameraMovement.Instance.GetZoom();
      set => CameraMovement.Instance.SetZoom((float) value);
    }

    public LuaColor getMapPixel(int x, int y) => LuaColor.From(Client.game.map.GetPixel(x, y));

    public void setMapPixel(int x, int y, LuaColor c)
    {
      Client.game.map.SetPixelColor(x, y, LuaColor.From(c));
    }

    public void drawRectangle(int x, int y, int width, int height, LuaColor c)
    {
      Client.game.map.DrawRectangle(x, y, width, height, LuaColor.From(c));
    }

    public void drawEllipseOutline(
      int x,
      int y,
      int radiusX,
      int radiusY,
      int outlineWidth,
      LuaColor c)
    {
      Client.game.map.DrawEllipseOutline(x, y, radiusX, radiusY, outlineWidth, LuaColor.From(c));
    }

    public void drawEllipse(int x, int y, int radiusX, int radiusY, LuaColor c)
    {
      Client.game.map.DrawEllipse(x, y, radiusX, radiusY, LuaColor.From(c));
    }

    public void blit(ExplosionCutout ex, Point point, bool ignoreAlpha = false, bool apply = true)
    {
      this.game.map.ServerBitBlt((int) ex, (int) point.x, (int) point.y, ignoreAlpha, apply);
    }

    public void blitRotate(
      ExplosionCutout ex,
      Point point,
      double rotation = 0.0,
      bool inFront = true,
      bool apply = true)
    {
      this.game.map.ServerBitBltRotate((int) ex, (int) point.x, (int) point.y, (FixedInt) (float) rotation, inFront, apply);
    }

    public void applyDraw() => Client.game.map.Apply();

    public bool allowBounce
    {
      get => this.game.AllowBounce;
      set => this.game.AllowBounce = value;
    }

    public bool isUsingTouchControls => HUD.UseTouchControls;

    public void clientRefreshSpells()
    {
      if (!((UnityEngine.Object) ClickSpell.Instance != (UnityEngine.Object) null))
        return;
      ClickSpell.Instance.SetSpells();
    }

    public void startCoroutine(Script script, DynValue f, bool runAsCoroutine = true, float seconds = 0.0f)
    {
      HUD.instance.StartCoroutine(this._run(script, f, runAsCoroutine, seconds));
    }

    public void devCommand(string s, Point p = null)
    {
      MyLocation? pos = new MyLocation?();
      if (p != (Point) null)
        pos = new MyLocation?(new MyLocation((int) p.x, (int) p.y));
      Client.DevConsole(s, this.game, pos: pos);
    }

    private IEnumerator _run(Script script, DynValue f, bool runAsCoroutine, float seconds)
    {
      if (!runAsCoroutine)
      {
        while (this.game == Client.game && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
        {
          DynValue dynValue = script.CreateCoroutine(f).Coroutine.Resume();
          if (dynValue.Type != DataType.Boolean || dynValue.Boolean)
          {
            if ((double) seconds > 0.0)
              yield return (object) new WaitForSecondsRealtime(seconds);
            else
              yield return (object) 0;
          }
          else
            break;
        }
      }
      MoonSharp.Interpreter.Coroutine luaCoroutine = script.CreateCoroutine(f).Coroutine;
      while (this.game == Client.game && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
      {
        DynValue dynValue = luaCoroutine.Resume();
        if (dynValue.Type != DataType.Number && dynValue.Type != DataType.YieldRequest)
          break;
        if ((double) seconds > 0.0)
          yield return (object) new WaitForSecondsRealtime(seconds);
        else
          yield return (object) 0;
      }
    }

    private void _OnArmageddon()
    {
      HUD.instance?.SetArmageddonIcon();
      HUD.instance?.NextTurn(Client.game.players[0], true);
    }

    private Point VectorToPoint(Vector3 v) => new Point((double) (int) v.x, (double) (int) v.y);

    public void win()
    {
      this.tutorial?.OnWin();
      HUD.instance?.game?.DelayKill();
    }

    public void lose() => HUD.instance?.game?.DelayKill();

    public void nextTurn() => Client.game.NextTurn();

    public void resetMap()
    {
      int index = Client._tutorial.GetMapID();
      if (index < 0 || index >= ClientResources.Instance._maps.Length)
        index = 0;
      Client.map.CleanUp();
      Client.map.game = this.game;
      if (string.Equals("Empty", Client._tutorial.mapId))
        Client.map.SetMapSprite(this.game, new Color32[Client._tutorial.customWidth * Client._tutorial.customHeight], Client._tutorial.customHeight, Client._tutorial.customWidth);
      else
        Client.map.SetMapSprite(this.game, ClientResources.Instance._maps[index]);
      this.UpdateColldersOnReset();
    }

    public void clearMap(int newWidth = -1, int newHeight = -1)
    {
      if (newWidth != -1)
        Client._tutorial.customWidth = Mathf.Clamp(newWidth, 10, 8192);
      if (newHeight != -1)
        Client._tutorial.customHeight = Mathf.Clamp(newHeight, 10, 8192);
      Color32[] pixels = new Color32[Client._tutorial.customWidth * Client._tutorial.customHeight];
      Client.map.CleanUp();
      Client.map.game = this.game;
      Client.map.SetMapSprite(this.game, pixels, Client._tutorial.customHeight, Client._tutorial.customWidth);
      this.UpdateColldersOnReset();
    }

    private void UpdateColldersOnReset()
    {
      foreach (ZPerson zperson in this.game.players.Concat<ZPerson>((IEnumerable<ZPerson>) new List<ZPerson>()
      {
        this.game._uncontrolledPlayer
      }).Concat<ZPerson>((IEnumerable<ZPerson>) this.game._playersExtended))
      {
        foreach (ZCreature zcreature in zperson.controlled)
        {
          zcreature.collider.Moved();
          zcreature.auraOfDecay?.collider?.Moved();
          zcreature.effectorStaticShield?.collider?.Moved();
          foreach (ZEffector zeffector in zcreature.effectors.Concat<ZEffector>((IEnumerable<ZEffector>) zcreature.destroyableEffectors))
          {
            zeffector?.collider?.Moved();
            zeffector?.spell?.collider?.Moved();
            zeffector?.spell?.colliderB?.Moved();
          }
          foreach (ZMyCollider followingCollider in zcreature.followingColliders)
          {
            followingCollider?.Moved();
            followingCollider?.spell?.collider?.Moved();
            followingCollider?.spell?.colliderB?.Moved();
          }
          if (zcreature.type == CreatureType.Beehive)
          {
            foreach (ZCreature bee in ((ZCreatureBeehive) zcreature).bees)
            {
              bee.collider.Moved();
              bee.auraOfDecay?.collider?.Moved();
              foreach (ZMyCollider followingCollider in bee.followingColliders)
              {
                followingCollider?.Moved();
                followingCollider?.spell?.collider?.Moved();
                followingCollider?.spell?.colliderB?.Moved();
              }
            }
          }
        }
      }
      foreach (ZEffector globalEffector in this.game.globalEffectors)
      {
        globalEffector?.collider?.Moved();
        globalEffector?.spell?.collider?.Moved();
        globalEffector?.spell?.colliderB?.Moved();
      }
    }

    public static ContainerSpell getSpell(string name)
    {
      return new ContainerSpell(new SpellSlot(Inert.GetSpell(name)));
    }

    public static SpellEnum getSpellEnum(string name)
    {
      Spell spell = Inert.GetSpell(name);
      return (UnityEngine.Object) spell != (UnityEngine.Object) null ? spell.spellEnum : SpellEnum.None;
    }

    public static string getSpellName(SpellEnum id)
    {
      Spell spell = Inert.GetSpell(id);
      return !((UnityEngine.Object) spell != (UnityEngine.Object) null) ? "" : spell.name;
    }

    public MapEnum getMapID() => this.game.gameFacts.realMap;

    public static string getMapRealName(MapEnum id) => GameFacts.MapName(id);

    public static string getMapShortName(MapEnum id) => GameFacts.MapShortName(id);

    public string getArmageddonName(MapEnum id)
    {
      return GameFacts.ArmageddonName(id, this.game.gameFacts.GetStyle());
    }

    public void panCamera(Point point, bool interuptable = true)
    {
      CameraMovement.Instance.TutorialLerpToPosition(new Vector3((float) point.x, (float) point.y), interuptable);
    }

    public string getInputString() => Input.inputString;

    public double screenWidth => (double) Controller.Instance.canvasRect.sizeDelta.x;

    public double screenHeight => (double) Controller.Instance.canvasRect.sizeDelta.y;

    public ContainerCreature createCreature(Script script, Summon summon)
    {
      if (summon.useAI && !summon.onPlayersPanel)
        summon.useAI = false;
      ++summon.elemental;
      Settings settings = new Settings(summon.outfit, summon.colors, summon.spells, summon.elemental);
      Spell spell = ContainerGame.getSpell(summon.spell);
      if ((UnityEngine.Object) spell == (UnityEngine.Object) null)
      {
        if (summon.elemental > BookOf.Arcane)
          this.game.gameFacts.SetStyle(GameStyle.Elementals);
        ZPerson zperson1 = Player.SpawnZezima(new MyLocation((int) summon.position.x, (int) summon.position.y), name: summon.name, team: summon.team, spDefault: settings.settings, onPlayerPanel: summon.onPlayersPanel);
        if (summon.onPlayersPanel)
        {
          ZPerson zperson2 = zperson1;
          IAI iai;
          if (!summon.useAI)
          {
            EMPTY_AI emptyAi = new EMPTY_AI();
            emptyAi.stillDoTurn = summon.no_AI_still_use_turn;
            emptyAi.game = zperson1.game;
            iai = (IAI) emptyAi;
          }
          else
          {
            iai = (IAI) new AI();
            iai.parent = zperson1;
            iai.creature = zperson1.first();
            iai.game = zperson1.game;
          }
          zperson2.ai = iai;
          Client.game.players.Add(zperson1);
          zperson1.first()?.UpdateHealthTxt();
        }
        else
          Client.game._playersExtended.Add(zperson1);
        if (summon.playSound)
          AudioManager.Play(Inert.Instance._spells[4].castClip);
        return new ContainerCreature(zperson1.first());
      }
      Spell summonSpell = ClientResources.Instance.summonSpells[0];
      for (int index = 0; index < ClientResources.Instance.summonSpells.Length; ++index)
      {
        if (string.Equals(ClientResources.Instance.summonSpells[index].name, spell.name, StringComparison.OrdinalIgnoreCase))
        {
          summonSpell = ClientResources.Instance.summonSpells[index];
          break;
        }
      }
      ZPerson overrideZPerson = summon.team >= HUD.instance.game.players.Count || !((ZComponent) HUD.instance.game.players[summon.team].first() != (object) null) ? Tutorial.CreatePerson(summon.team, summon.name, settings.settings, summon.onPlayersPanel) : HUD.instance.game.players[summon.team];
      ZCreature c = ZSpell.FireSummon(summonSpell, HUD.instance.game, overrideZPerson.first(), new MyLocation((int) summon.position.x, (int) summon.position.y), 1, overrideZPerson: overrideZPerson);
      if (summon.onPlayersPanel)
      {
        ZPerson zperson = overrideZPerson;
        IAI iai;
        if (!summon.useAI)
        {
          EMPTY_AI emptyAi = new EMPTY_AI();
          emptyAi.stillDoTurn = summon.no_AI_still_use_turn;
          emptyAi.game = overrideZPerson.game;
          iai = (IAI) emptyAi;
        }
        else
        {
          iai = (IAI) new AI();
          iai.parent = overrideZPerson;
          iai.creature = overrideZPerson.first();
          iai.game = overrideZPerson.game;
        }
        zperson.ai = iai;
        if (!this.game.players.Contains(overrideZPerson))
          this.game.players.Add(overrideZPerson);
        HUD.instance.AddPanelPlayer(c);
        c.isPawn = false;
        c.UpdateHealthTxt();
      }
      if (summon.playSound)
        AudioManager.Play(summonSpell.castClip);
      return new ContainerCreature(c);
    }

    public void clearIndicators()
    {
      foreach (ContainerIndicator activeIndicator in this.tutorial.activeIndicators)
      {
        if (activeIndicator != null)
          UnityEngine.Object.Destroy((UnityEngine.Object) activeIndicator.indicator);
      }
      this.tutorial.activeIndicators.Clear();
    }

    public ContainerIndicator createIndicator(
      IndicatorKind kind,
      Point point,
      string hexColor = "#FFFFFF",
      double radius = 18.0,
      double angle = 0.0,
      double lifetime = 0.0)
    {
      for (int index = this.tutorial.activeIndicators.Count - 1; index >= 0; --index)
      {
        if ((UnityEngine.Object) this.tutorial.activeIndicators[index].indicator == (UnityEngine.Object) null)
          this.tutorial.activeIndicators.RemoveAt(index);
      }
      if (this.tutorial.activeIndicators.Count >= 10000)
        throw new Exception("To many indicators");
      GameObject s = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.tutIndicators[Mathf.Clamp((int) kind, 0, ClientResources.Instance.tutIndicators.Count - 1)], new Vector3((float) (int) point.x, (float) (int) point.y), Quaternion.identity, HUD.instance.game.GetMapTransform());
      ContainerIndicator indicator = new ContainerIndicator(this.tutorial, s, kind, radius);
      this.tutorial.activeIndicators.Add(indicator);
      Color color;
      if (ColorUtility.TryParseHtmlString(hexColor, out color))
        s.GetComponentInChildren<SpriteRenderer>().color = color;
      if (kind == IndicatorKind.Area)
        s.transform.localScale = new Vector3((float) radius / 128f, (float) radius / 128f, 1f);
      s.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (float) angle);
      if (lifetime > 0.0)
        UnityEngine.Object.Destroy((UnityEngine.Object) s, (float) lifetime);
      return indicator;
    }

    [MoonSharpHidden]
    public static Spell getSpell(object spellObj, ZCreature creature = null)
    {
      if (spellObj == null)
        return (Spell) null;
      Spell spell = (Spell) null;
      if (spellObj.GetType() == typeof (string))
        spell = Inert.Instance.spells[(string) spellObj];
      else if (spellObj.GetType() == typeof (double) || spellObj.GetType() == typeof (int))
        spell = Inert.GetSpell((SpellEnum) (double) spellObj);
      else if (spellObj.GetType() == typeof (SpellEnum))
        spell = Inert.GetSpell((SpellEnum) spellObj);
      else if (spellObj.GetType() == typeof (ContainerSpell))
        spell = ((ContainerSpell) spellObj).slot.spell;
      else if ((ZComponent) creature != (object) null && (spellObj.GetType() == typeof (double) || spellObj.GetType() == typeof (int)))
        spell = creature.spells[(int) spellObj].spell;
      return spell;
    }
  }
}
