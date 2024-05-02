

using Hazel;
using Hazel.Tcp;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;
using MovementEffects;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class Tutorial
  {
    public string Name = "Custom Tutorial";
    public string message = "";
    public int lastID;
    public string mapId = "Sandbox";
    public int customWidth = 1920;
    public int customHeight = 960;
    public bool isDefault;
    public bool NOBACKGROUND;
    public int _index;
    public List<Command> commands = new List<Command>();
    public SettingsPlayer settings = SettingsPlayer.DefaultSettings();
    public KeyCode[] keyCodes;
    internal Dictionary<int, ZCreature> creatures = new Dictionary<int, ZCreature>();
    internal IEnumerator<float> ie;
    private int totalEntitiesCreated;
    internal List<ContainerIndicator> activeIndicators = new List<ContainerIndicator>();
    internal Dictionary<int, int> namedVariable = new Dictionary<int, int>();
    internal Dictionary<int, int> commandsByIdentifier = new Dictionary<int, int>();
    internal List<(int firstIndex, int lastIndex)> nestedLoops = new List<(int, int)>();
    internal Action<ContainerCreature> onDeath;
    internal Action<ContainerCreature, ContainerSpell> onCast;
    internal Action<int> onFrame;
    [JsonIgnore]
    [NonSerialized]
    internal bool debugLog;
    [JsonIgnore]
    [NonSerialized]
    internal MoonSharpVsCodeDebugServer server;
    [JsonIgnore]
    [NonSerialized]
    internal Script myscript;
    internal float startTime;
    private List<KeyCode> m_activeInputs = new List<KeyCode>();
    public bool waitingForContinue;

    public static Tutorial Default => new Tutorial();

    public void RemoveCallbacks()
    {
      this.onCast = (Action<ContainerCreature, ContainerSpell>) null;
      this.onDeath = (Action<ContainerCreature>) null;
    }

    public static Tutorial FromCodeOnly(string s)
    {
      Tutorial tutorial = new Tutorial();
      string str1 = "--NAME =";
      int num1 = s.IndexOf(str1, StringComparison.OrdinalIgnoreCase);
      int num2 = 0;
      if (num1 >= 0)
      {
        int num3 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num1 + str1.Length);
        if (num3 > num1)
          tutorial.Name = s.Substring(num1 + str1.Length, num3 - num1 - str1.Length).Trim();
      }
      string str2 = "--DESCRIPTION =";
      int num4 = s.IndexOf(str2, StringComparison.OrdinalIgnoreCase);
      num2 = 0;
      if (num4 >= 0)
      {
        int num5 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num4 + str2.Length);
        if (num5 > num4)
          tutorial.message = s.Substring(num4 + str2.Length, num5 - num4 - str2.Length).Trim();
      }
      string str3 = "--MAP =";
      int num6 = s.IndexOf(str3, StringComparison.OrdinalIgnoreCase);
      num2 = 0;
      if (num6 >= 0)
      {
        int num7 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num6 + str3.Length);
        if (num7 > num6)
          tutorial.mapId = s.Substring(num6 + str3.Length, num7 - num6 - str3.Length).Trim();
      }
      if (string.Equals("Empty", tutorial.mapId))
      {
        string str4 = "--WIDTH =";
        int num8 = s.IndexOf(str4, StringComparison.OrdinalIgnoreCase);
        num2 = 0;
        if (num8 >= 0)
        {
          int num9 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num8 + str4.Length);
          if (num9 > num8)
            tutorial.customWidth = Mathf.Clamp(int.Parse(s.Substring(num8 + str4.Length, num9 - num8 - str4.Length).Trim()), 10, 8192);
        }
        string str5 = "--HEIGHT =";
        int num10 = s.IndexOf(str5, StringComparison.OrdinalIgnoreCase);
        num2 = 0;
        if (num10 >= 0)
        {
          int num11 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num10 + str5.Length);
          if (num11 > num10)
            tutorial.customHeight = Mathf.Clamp(int.Parse(s.Substring(num10 + str5.Length, num11 - num10 - str5.Length).Trim()), 10, 8192);
        }
      }
      string str6 = "--SPELLS =";
      int num12 = s.IndexOf(str6, StringComparison.OrdinalIgnoreCase);
      num2 = 0;
      if (s.IndexOf("--NOBACKGROUND") >= 0)
        tutorial.NOBACKGROUND = true;
      if (num12 >= 0)
      {
        int num13 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num12 + str6.Length);
        if (num13 > num12)
        {
          string str7 = s.Substring(num12 + str6.Length, num13 - num12 - str6.Length).Trim();
          if (str7[0] == '{' && str7[str7.Length - 1] == '}')
          {
            string[] strArray = str7.Substring(1, str7.Length - 2).Split(new char[1]
            {
              ','
            }, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < 16 && index < strArray.Length; ++index)
            {
              string key = strArray[index].Trim();
              if (key.StartsWith("\""))
                key = key.Substring(1, key.Length - 2);
              tutorial.settings.spells[index] = (byte) Inert.Instance.spells.IndexOf(key);
            }
          }
        }
      }
      CScript cscript1 = new CScript();
      cscript1.code = s;
      cscript1.Name = "Script";
      CScript cscript2 = cscript1;
      string str8 = "--DEBUG =";
      int num14 = s.IndexOf(str8, StringComparison.OrdinalIgnoreCase);
      num2 = 0;
      if (num14 >= 0)
      {
        int num15 = s.IndexOfAny(new char[2]{ '\n', '\r' }, num14 + str8.Length);
        if (num15 > num14)
          cscript2.bool_debug = string.Equals(s.Substring(num14 + str8.Length, num15 - num14 - str8.Length).Trim(), "TRUE", StringComparison.OrdinalIgnoreCase);
      }
      tutorial.commands.Add((Command) cscript2);
      return tutorial;
    }

    public string ToCodeOnly(int selectedListIndex = -1)
    {
      if (selectedListIndex == -1)
        selectedListIndex = this.commands.FindIndex((Predicate<Command>) (z => z.type == Command.Type.Script));
      Tutorial tutorial = this;
      if (selectedListIndex == -1)
        return "--NAME = Unnamed\r\n--DESCRIPTION = No Description\r\n--MAP = 0\r\n--SPELLS = {}\r\n--DEBUG = TRUE\r\n\r\n--CODE--\r\n\r\nfunction main()\r\n    while true do\r\n        --Some lua logic goes here\r\n        --Open the API github page for basic documentation and usage\r\n        coroutine.yield(0)\r\n    end\r\nend\r\n";
      if (((CScript) tutorial.commands[selectedListIndex]).code.Contains("--NAME ="))
        return ((CScript) tutorial.commands[selectedListIndex]).code;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("--NAME = ").Append(tutorial.Name);
      stringBuilder.Append("\n--DESCRIPTION = ").Append(tutorial.message);
      stringBuilder.Append("\n--MAP = ").Append(tutorial.mapId);
      stringBuilder.Append("\n--SPELLS = {");
      for (int index = 0; index < 16 && tutorial.settings.spells[index] < byte.MaxValue; ++index)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(Inert.Instance._spells[(int) tutorial.settings.spells[index]].name);
      }
      stringBuilder.Append("}");
      if (selectedListIndex == -1)
      {
        stringBuilder.Append("\n function main()\n\nend");
        return stringBuilder.ToString();
      }
      stringBuilder.Append("\n--DEBUG = ").Append(((CScript) tutorial.commands[selectedListIndex]).bool_debug.ToString().ToUpper());
      stringBuilder.Append("\n\n--CODE--\n\n");
      stringBuilder.Append(((CScript) tutorial.commands[selectedListIndex]).code);
      return stringBuilder.ToString();
    }

    public int GetMapID()
    {
      for (int mapId = 0; mapId < ClientResources.Instance._maps.Length; ++mapId)
      {
        if (string.Equals(ClientResources.Instance._maps[mapId].name, this.mapId))
          return mapId;
      }
      return 0;
    }

    public void ValidateIdentifiers()
    {
      HashSet<int> intSet = new HashSet<int>();
      for (int index = 0; index < this.commands.Count; ++index)
      {
        while (!intSet.Add(this.commands[index].identifier))
        {
          this.commands[index].identifier = this.lastID;
          ++this.lastID;
        }
      }
    }

    public void ClickSandbox(bool test, ChooseJsonDialog.Viewing custom)
    {
      Client._gameFacts = new GameFacts();
      Client._tutorial = this;
      Client.joinedFrom = test ? Client.JoinLocation.TestTutorial : Client.JoinLocation.Tutorial;
      this.isDefault = custom == ChooseJsonDialog.Viewing.Default;
      Controller.Instance.InitMap(true, true);
      this.ie = Timing.RunCoroutine(this.IELoop());
    }

    public void CleanUp()
    {
      if (this.ie != null)
        Timing.KillCoroutines(this.ie);
      this.ie = (IEnumerator<float>) null;
      this.RemoveCallbacks();
      Client._tutorial = (Tutorial) null;
      this.DisposeDebugger();
    }

    public void DisposeDebugger()
    {
      if (this.server != null)
      {
        this.server.Detach(this.myscript);
        this.server.Dispose();
      }
      this.server = (MoonSharpVsCodeDebugServer) null;
    }

    public void HandleKeyUp(Script script, DynValue keyUp)
    {
      List<KeyCode> keyCodeList1 = new List<KeyCode>();
      if (Input.anyKeyDown || Input.anyKey)
      {
        for (int index = 0; index < this.keyCodes.Length; ++index)
        {
          if (Input.GetKey(this.keyCodes[index]))
          {
            this.m_activeInputs.Remove(this.keyCodes[index]);
            this.m_activeInputs.Add(this.keyCodes[index]);
            keyCodeList1.Add(this.keyCodes[index]);
          }
        }
      }
      List<KeyCode> keyCodeList2 = new List<KeyCode>();
      foreach (KeyCode activeInput in this.m_activeInputs)
      {
        keyCodeList2.Add(activeInput);
        if (!keyCodeList1.Contains(activeInput))
        {
          keyCodeList2.Remove(activeInput);
          script.Call(keyUp, new object[1]
          {
            (object) activeInput
          });
        }
      }
      this.m_activeInputs = keyCodeList2;
    }

    public ZCreature GetPlayer() => Client.game.players[0].first();

    public ZCreature GetCreature(int identifier)
    {
      ZCreature creature1 = (ZCreature) null;
      if (identifier < 0 && this.creatures.Count > 0)
      {
        foreach (KeyValuePair<int, ZCreature> creature2 in this.creatures)
        {
          if ((ZComponent) creature2.Value != (object) null)
            return creature2.Value;
        }
      }
      this.creatures.TryGetValue(identifier, out creature1);
      return creature1;
    }

    public List<Tutorial.ReturnEntity> CalculatePossibilities()
    {
      List<Tutorial.ReturnEntity> possibilities = new List<Tutorial.ReturnEntity>();
      possibilities.Add(new Tutorial.ReturnEntity()
      {
        identifier = -2,
        index = -2
      });
      possibilities.Add(new Tutorial.ReturnEntity()
      {
        identifier = -1,
        index = -1
      });
      for (int index = 0; index < this.commands.Count; ++index)
      {
        if (this.commands[index].type == Command.Type.CreateEntity)
          possibilities.Add(new Tutorial.ReturnEntity()
          {
            identifier = this.commands[index].identifier,
            index = index
          });
      }
      return possibilities;
    }

    public List<Tutorial.ReturnEntity> CalculateCommandID()
    {
      List<Tutorial.ReturnEntity> commandId = new List<Tutorial.ReturnEntity>();
      commandId.Add(new Tutorial.ReturnEntity()
      {
        name = "Next",
        identifier = -1,
        index = -1
      });
      for (int index = 0; index < this.commands.Count; ++index)
        commandId.Add(new Tutorial.ReturnEntity()
        {
          name = (index + 1).ToString() + ") " + this.commands[index].Name,
          identifier = this.commands[index].identifier,
          index = index
        });
      return commandId;
    }

    public List<Tutorial.ReturnEntity> CalculateVariables()
    {
      List<Tutorial.ReturnEntity> variables = new List<Tutorial.ReturnEntity>();
      for (int index = 0; index < this.commands.Count; ++index)
      {
        if (this.commands[index].type == Command.Type.IntitializeVariables)
        {
          foreach (CIntitializeVarible.ListVars listVars in ((CIntitializeVarible) this.commands[index]).list)
            variables.Add(new Tutorial.ReturnEntity()
            {
              name = "var - " + listVars.name,
              identifier = listVars.identifier + 1000,
              index = index
            });
        }
      }
      return variables;
    }

    public void CreateCommand(Command c)
    {
      c.identifier = this.lastID;
      this.commands.Add(c);
      ++this.lastID;
    }

    public void InsertCommand(int i, Command c)
    {
      c.identifier = this.lastID;
      this.commands.Insert(i, c);
      ++this.lastID;
      if (i <= 0)
        return;
      if (this.commands[i - 1].type == Command.Type.If || this.commands[i - 1].type == Command.Type.LoopWhileTrue)
        c.tabIndex = this.commands[i - 1].tabIndex + 1;
      else
        c.tabIndex = this.commands[i - 1].tabIndex;
    }

    public int FindCommandByID(int id)
    {
      return this.commands.FindIndex((Predicate<Command>) (x => x.identifier == id));
    }

    public Command GetCommandByID(int id)
    {
      return this.commands.Find((Predicate<Command>) (x => x.identifier == id));
    }

    public byte[] ToBytes()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BsonWriter bsonWriter = new BsonWriter((Stream) memoryStream))
          new JsonSerializer()
          {
            TypeNameHandling = TypeNameHandling.Auto
          }.Serialize((JsonWriter) bsonWriter, (object) this);
        return memoryStream.ToArray();
      }
    }

    public static Tutorial FromBytes(byte[] sent)
    {
      using (MemoryStream memoryStream = new MemoryStream(sent))
      {
        using (BsonReader reader = new BsonReader((Stream) memoryStream))
          return new JsonSerializer()
          {
            TypeNameHandling = TypeNameHandling.Auto
          }.Deserialize<Tutorial>((JsonReader) reader);
      }
    }

    public JObject ToJson()
    {
      return JObject.FromObject((object) this, new JsonSerializer()
      {
        TypeNameHandling = TypeNameHandling.Auto
      });
    }

    public static Tutorial FromJson(JObject j)
    {
      return j.ToObject<Tutorial>(new JsonSerializer()
      {
        TypeNameHandling = TypeNameHandling.Auto,
        Binder = (SerializationBinder) new CustomJsonSerializationBinder("Educative")
      });
    }

    public static Tutorial FromJson(string j)
    {
      return JsonConvert.DeserializeObject<Tutorial>(j, new JsonSerializerSettings()
      {
        TypeNameHandling = TypeNameHandling.Auto,
        Binder = (SerializationBinder) new CustomJsonSerializationBinder("Educative")
      });
    }

    private void _GenerateLoopGotos()
    {
      for (int index = this.commands.Count - 1; index >= 0; --index)
      {
        if (this.commands[index].type == Command.Type.LoopWhileTrue)
          this._FindLoopAtHere(index);
        else if (this.commands[index].type == Command.Type.If)
          ((CIfThen) this.commands[index]).commandID = this._ReturnLoopAtHere(index);
      }
      for (int index = 0; index < this.commands.Count; ++index)
      {
        if (!this.commandsByIdentifier.ContainsKey(this.commands[index].identifier))
          this.commandsByIdentifier.Add(this.commands[index].identifier, index);
      }
    }

    private void _FindLoopAtHere(int start)
    {
      int tabIndex = (int) this.commands[start + 1].tabIndex;
      for (int index1 = start + 2; index1 < this.commands.Count; ++index1)
      {
        if (this.commands[index1].tabIndex < (Command.TabIndex) tabIndex)
        {
          List<Command> commands1 = this.commands;
          int index2 = index1;
          CGoTo cgoTo = new CGoTo();
          cgoTo.identifier = this.lastID++;
          cgoTo.commandID = this.commands[start].identifier;
          commands1.Insert(index2, (Command) cgoTo);
          ((CLoopWhileTrue) this.commands[start]).indexEnded = index1 + 1;
          List<Command> commands2 = this.commands;
          int index3 = index1 + 1;
          CNothing cnothing = new CNothing();
          cnothing.identifier = this.lastID++;
          commands2.Insert(index3, (Command) cnothing);
          return;
        }
      }
      ((CLoopWhileTrue) this.commands[start]).indexEnded = this.commands.Count;
      List<Command> commands = this.commands;
      int count = this.commands.Count;
      CGoTo cgoTo1 = new CGoTo();
      cgoTo1.identifier = this.lastID++;
      cgoTo1.commandID = this.commands[start].identifier;
      commands.Insert(count, (Command) cgoTo1);
    }

    private int _ReturnLoopAtHere(int start)
    {
      int tabIndex = (int) this.commands[start + 1].tabIndex;
      for (int index = start + 2; index < this.commands.Count; ++index)
      {
        if (this.commands[index].tabIndex < (Command.TabIndex) tabIndex)
          return this.commands[index].identifier;
      }
      return -1;
    }

    public void ShowInfo(string message, bool onContinue, bool pauseGame, bool unpauseGame)
    {
      Action action1 = (Action) (() =>
      {
        HUD.instance.turTextbox.text = "";
        HUD.instance.tutPopup.gameObject.SetActive(false);
        this.waitingForContinue = false;
        if (!unpauseGame)
          return;
        Client.game.tutorialPaused = false;
      });
      Action action2 = (Action) (() => HUD.instance.ToggleTutorial());
      HUD.instance.TutorialPopup(message, onContinue ? action1 : action2, onContinue, pauseGame);
      if (!onContinue)
        return;
      this.waitingForContinue = true;
    }

    public void OnWin()
    {
      if (!this.isDefault || this._index < 0 || this._index >= Prestige.tutorialInfo.Count || Client.MyAccount.tutorials[this._index])
        return;
      Client.MyAccount.tutorials[this._index] = true;
      Prestige.Ask((byte) 8, this._index);
    }

    public IEnumerator<float> IELoop()
    {
      Tutorial t = this;
      yield return 0.0f;
      while (Client.game == null)
        yield return 0.0f;
      t._GenerateLoopGotos();
      t.keyCodes = (KeyCode[]) Enum.GetValues(typeof (KeyCode));
      int state = 0;
      int index = 0;
      Action next = (Action) (() =>
      {
        HUD.instance.turTextbox.text = "";
        HUD.instance.tutPopup.gameObject.SetActive(false);
        ++state;
        Client.game.tutorialPaused = false;
      });
      Action toggleTut = (Action) (() => HUD.instance.ToggleTutorial());
      HUD.instance.TutorialPopup("No info? Some tutorial this is!!!!!!!!!!!", toggleTut);
      t.startTime = Time.realtimeSinceStartup;
      while (index < t.commands.Count && (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
      {
        Command command = t.commands[index];
        int z1;
        bool snap;
        ZCreature cre;
        switch (command.type)
        {
          case Command.Type.Info:
            CInfo cinfo = (CInfo) command;
            HUD.instance.TutorialPopup(cinfo.message, cinfo.bool_onContinue ? next : toggleTut, cinfo.bool_onContinue, cinfo.bool_pauseGame);
            z1 = state;
            if (cinfo.bool_onContinue)
            {
              while (state == z1)
                yield return 0.0f;
            }
            ++index;
            break;
          case Command.Type.Move:
            CMove cmove = (CMove) command;
            ZCreature creature1 = t.GetCreature(cmove.entity);
            if ((ZComponent) creature1 != (object) null)
            {
              creature1.MoveToPosition = cmove.point_location.ToMyLocation();
              creature1.Demount();
              creature1.Fall();
            }
            ++index;
            break;
          case Command.Type.CreateEntity:
            ++t.totalEntitiesCreated;
            if (t.totalEntitiesCreated > 100)
            {
              HUD.instance.TutorialPopup("Entity Creation limit reached - Aborting tutorial", toggleTut);
              yield break;
            }
            else
            {
              CCreateEntity ccreateEntity = (CCreateEntity) command;
              if (ccreateEntity.summon == 0)
              {
                ZPerson zperson1 = Player.SpawnZezima(new MyLocation((FixedInt) (float) ccreateEntity.point_location.x, (FixedInt) (float) ccreateEntity.point_location.y), ccreateEntity.bool_OnPlayerPanel, ccreateEntity.Name, ccreateEntity.team, ccreateEntity.settings);
                t.creatures.Add(ccreateEntity.identifier, zperson1.first());
                if (ccreateEntity.bool_OnPlayerPanel)
                {
                  ZPerson zperson2 = zperson1;
                  AI ai = new AI();
                  ai.parent = zperson1;
                  ai.creature = zperson1.first();
                  ai.game = zperson1.game;
                  zperson2.ai = (IAI) ai;
                }
                if (ccreateEntity.bool_playSound)
                  AudioManager.Play(Inert.Instance._spells[4].castClip);
                zperson1.first()?.UpdateHealthTxt();
              }
              else
              {
                Spell summonSpell = ClientResources.Instance.summonSpells[Mathf.Clamp(ccreateEntity.summon - 1, 0, ClientResources.Instance.summonSpells.Length - 1)];
                ZPerson overrideZPerson = ccreateEntity.team >= HUD.instance.game.players.Count || !((ZComponent) HUD.instance.game.players[ccreateEntity.team].first() != (object) null) ? Tutorial.CreatePerson(ccreateEntity.team, ccreateEntity.Name, ccreateEntity.settings, ccreateEntity.bool_OnPlayerPanel) : HUD.instance.game.players[ccreateEntity.team];
                ZCreature c = ZSpell.FireSummon(summonSpell, HUD.instance.game, overrideZPerson.first(), new MyLocation((FixedInt) (float) ccreateEntity.point_location.x, (FixedInt) (float) ccreateEntity.point_location.y), 1, overrideZPerson: overrideZPerson);
                t.creatures.Add(ccreateEntity.identifier, c);
                if (ccreateEntity.bool_OnPlayerPanel)
                {
                  ZPerson zperson = overrideZPerson;
                  AI ai = new AI();
                  ai.parent = overrideZPerson;
                  ai.creature = overrideZPerson.first();
                  ai.game = overrideZPerson.game;
                  zperson.ai = (IAI) ai;
                  HUD.instance.AddPanelPlayer(c);
                  c.isPawn = false;
                  c.UpdateHealthTxt();
                }
                if (ccreateEntity.bool_playSound)
                  AudioManager.Play(summonSpell.castClip);
              }
              ++index;
              break;
            }
          case Command.Type.AddSpell:
            CAddSpell caddSpell = (CAddSpell) command;
            ZCreature creature2 = t.GetCreature(caddSpell.entity);
            if ((ZComponent) creature2 != (object) null)
            {
              if (caddSpell.bool_remove)
              {
                for (int index1 = 0; index1 < creature2.spells.Count; ++index1)
                {
                  if (creature2.spells[index1].spell.spellEnum == caddSpell.spellEnum)
                  {
                    creature2.spells.RemoveAt(index1);
                    Player.Instance.UpdateVisuals();
                    break;
                  }
                }
              }
              else
              {
                Spell spell = Inert.GetSpell(caddSpell.spellEnum);
                if ((UnityEngine.Object) spell != (UnityEngine.Object) null)
                {
                  SpellSlot spellSlot = new SpellSlot(spell);
                  if (caddSpell.cooldown != -1)
                    spellSlot.RechargeTime = caddSpell.cooldown;
                  if (caddSpell.turnsFirstUse != -1)
                    spellSlot.TurnsTillFirstUse = caddSpell.turnsFirstUse;
                  if (caddSpell.maxUses == 0)
                    spellSlot.MaxUses = -1;
                  else if (caddSpell.maxUses != -1)
                    spellSlot.MaxUses = caddSpell.maxUses;
                  creature2.spells.Add(spellSlot);
                  Player.Instance.UpdateVisuals();
                }
                else
                  Debug.LogError((object) ("Could not find spell " + caddSpell.spellEnum.ToString()));
              }
            }
            ++index;
            break;
          case Command.Type.LockSpell:
            CLockSpell clockSpell = (CLockSpell) command;
            ZCreature creature3 = t.GetCreature(clockSpell.entity);
            if ((ZComponent) creature3 != (object) null)
            {
              foreach (SpellSlot spell in creature3.spells)
              {
                if (spell.spell.spellEnum == clockSpell.spellEnum)
                  spell.disabledturn = clockSpell.bool_lock ? 999 : -1;
              }
            }
            ++index;
            break;
          case Command.Type.CastSpell:
            CCastSpell ccastSpell = (CCastSpell) command;
            ZCreature creature4 = t.GetCreature(ccastSpell.entity);
            if ((ZComponent) creature4 != (object) null)
            {
              Spell spell = Inert.GetSpell(ccastSpell.spellEnum);
              if ((UnityEngine.Object) spell != (UnityEngine.Object) null)
              {
                ZSpell.FireWhich(spell, creature4, creature4.position, FixedInt.Create(ccastSpell.angle), FixedInt.Create(ccastSpell.power), ccastSpell.point_target.ToMyLocation(), NullMyLocation.Get());
                if ((ZComponent) creature4 == (object) Player.Instance?.person?.first())
                  Player.Instance.UnselectSpell();
              }
            }
            ++index;
            break;
          case Command.Type.EnableMovement:
            CEnableMovement cenableMovement = (CEnableMovement) command;
            Client.game.AllowMovement = cenableMovement.bool_canMove;
            Client.game.AllowTerrainDestruction = cenableMovement.bool_terrainDestruction;
            Player.Instance.canSkipTurn = cenableMovement.bool_canSkipTurn;
            ZCreature player1 = t.GetPlayer();
            if ((ZComponent) player1 != (object) null)
              player1.invulnerable = cenableMovement.bool_takeDamage ? 0 : 99999;
            ++index;
            break;
          case Command.Type.WaitForDeath:
            CWaitForDeath cwaitForDeath = (CWaitForDeath) command;
            if (cwaitForDeath.entity == -1)
            {
              List<ZCreature> allCreatures = new List<ZCreature>();
              foreach (ZPerson player2 in Client.game.players)
              {
                foreach (ZCreature zcreature in player2.controlled)
                {
                  if ((ZComponent) zcreature != (object) null)
                    allCreatures.Add(zcreature);
                }
              }
              foreach (ZCreature zcreature in Client.game._uncontrolledPlayer.controlled)
              {
                if ((ZComponent) zcreature != (object) null)
                  allCreatures.Add(zcreature);
              }
              foreach (KeyValuePair<int, ZCreature> creature5 in t.creatures)
              {
                if ((ZComponent) creature5.Value != (object) null && !creature5.Value.isDead && !allCreatures.Contains(creature5.Value))
                  allCreatures.Add(creature5.Value);
              }
              snap = false;
              while (!snap)
              {
                foreach (ZCreature zcreature in allCreatures)
                {
                  if ((ZComponent) zcreature == (object) null || zcreature.isDead)
                  {
                    snap = true;
                    break;
                  }
                }
                yield return 0.0f;
              }
              allCreatures.Clear();
              allCreatures = (List<ZCreature>) null;
            }
            else
            {
              cre = t.GetCreature(cwaitForDeath.entity);
              while ((ZComponent) cre != (object) null && !cre.isDead)
                yield return 0.0f;
              cre = (ZCreature) null;
            }
            ++index;
            break;
          case Command.Type.WaitForSpellCast:
            CWaitForSpellCast x1 = (CWaitForSpellCast) command;
            cre = t.GetCreature(x1.entity);
            if ((ZComponent) cre != (object) null)
            {
              z1 = cre.parent.GetSpellCast(x1.spellEnum);
              while (z1 == cre.parent.GetSpellCast(x1.spellEnum))
                yield return 0.0f;
            }
            ++index;
            break;
          case Command.Type.WaitForMoveToArea:
            CWaitForMoveToArea x2 = (CWaitForMoveToArea) command;
            cre = t.GetCreature(x2.entity);
            if ((ZComponent) cre != (object) null)
            {
              MyLocation loc = x2.point_area.ToMyLocation();
              while ((double) MyLocation.Distance(loc, cre.position) > (double) x2.radius * 1.0)
                yield return 0.0f;
            }
            ++index;
            break;
          case Command.Type.WaitForSpellsToFinish:
            while (HUD.instance.game.ongoing.Busy())
              yield return 0.0f;
            ++index;
            break;
          case Command.Type.WaitTime:
            float f = ((CWaitTime) command).time;
            for (float v = 0.0f; (double) v < (double) f; v += Time.deltaTime)
              yield return 0.0f;
            ++index;
            break;
          case Command.Type.PanCamera:
            CPanCamera cpanCamera = (CPanCamera) command;
            CameraMovement.Instance.TutorialLerpToPosition(new Vector3((float) cpanCamera.point_area.x, (float) cpanCamera.point_area.y), cpanCamera.bool_interuptable);
            ++index;
            break;
          case Command.Type.Win:
            t.OnWin();
            HUD instance = HUD.instance;
            if (instance == null)
            {
              yield break;
            }
            else
            {
              ZGame game = instance.game;
              if (game == null)
              {
                yield break;
              }
              else
              {
                game.DelayKill();
                yield break;
              }
            }
          case Command.Type.IfGoTo:
            CIf cif = (CIf) command;
            cif.leftValue.SetEntity(t);
            cif.rightValue.SetEntity(t);
            if (cif.Evaulate())
            {
              for (int index2 = 0; index2 < t.commands.Count; ++index2)
              {
                if (t.commands[index2].identifier == cif.commandID)
                {
                  index = index2 - 1;
                  break;
                }
              }
            }
            ++index;
            break;
          case Command.Type.SetGameVariable:
            CSetGameVariable csetGameVariable = (CSetGameVariable) command;
            csetGameVariable.variable.SetEntity(t);
            csetGameVariable.variable.value = csetGameVariable.variable._number;
            ++index;
            break;
          case Command.Type.CreateIndicator:
            for (int index3 = t.activeIndicators.Count - 1; index3 >= 0; --index3)
            {
              if ((UnityEngine.Object) t.activeIndicators[index3].indicator == (UnityEngine.Object) null)
                t.activeIndicators.RemoveAt(index3);
            }
            if (t.activeIndicators.Count < 10)
            {
              CCreateIndicator ccreateIndicator = (CCreateIndicator) command;
              GameObject s = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.tutIndicators[Mathf.Clamp((int) ccreateIndicator.kind, 0, ClientResources.Instance.tutIndicators.Count - 1)], new Vector3((float) ccreateIndicator.point_point.x, (float) ccreateIndicator.point_point.y), Quaternion.identity, HUD.instance.game.GetMapTransform());
              t.activeIndicators.Add(new ContainerIndicator(t, s, (IndicatorKind) ccreateIndicator.kind, (double) ccreateIndicator.radius));
              Color color;
              if (ColorUtility.TryParseHtmlString(ccreateIndicator.hexColor, out color))
                s.GetComponentInChildren<SpriteRenderer>().color = color;
              if (ccreateIndicator.kind == CCreateIndicator.Kinds.Area)
                s.transform.localScale = new Vector3((float) ccreateIndicator.radius / 128f, (float) ccreateIndicator.radius / 128f, 1f);
              s.transform.localEulerAngles = new Vector3(0.0f, 0.0f, ccreateIndicator.angle);
              if ((double) ccreateIndicator.lifetime > 0.0)
                UnityEngine.Object.Destroy((UnityEngine.Object) s, ccreateIndicator.lifetime);
              ++index;
              break;
            }
            HUD.instance.TutorialPopup("Indicator limit reached - Aborting tutorial", toggleTut);
            yield break;
          case Command.Type.ClearIndicators:
            foreach (ContainerIndicator activeIndicator in t.activeIndicators)
            {
              if (activeIndicator != null)
                UnityEngine.Object.Destroy((UnityEngine.Object) activeIndicator.indicator);
            }
            t.activeIndicators.Clear();
            ++index;
            break;
          case Command.Type.WaitWhileTrue:
            CWaitWhileTrue x3 = (CWaitWhileTrue) command;
            x3.leftValue.SetEntity(t);
            x3.rightValue.SetEntity(t);
            while (x3.Evaulate())
              yield return 0.0f;
            ++index;
            break;
          case Command.Type.LoopWhileTrue:
            CLoopWhileTrue cloopWhileTrue = (CLoopWhileTrue) command;
            cloopWhileTrue.leftValue.SetEntity(t);
            cloopWhileTrue.rightValue.SetEntity(t);
            if (!cloopWhileTrue.Evaulate())
              index = cloopWhileTrue.indexEnded;
            ++index;
            break;
          case Command.Type.GoTo:
            CGoTo cgoTo = (CGoTo) command;
            int num1 = 0;
            if (t.commandsByIdentifier.TryGetValue(cgoTo.commandID, out num1))
            {
              index = num1;
              break;
            }
            ++index;
            break;
          case Command.Type.Armageddon:
            CArmageddon carmageddon = (CArmageddon) command;
            Client.game.armageddon = carmageddon.armageddon;
            Client.game.armageddonTurn = carmageddon.turn;
            HUD.instance?.SetArmageddonIcon();
            HUD.instance?.NextTurn(Client.game.players[0], true);
            ++index;
            break;
          case Command.Type.If:
            CIfThen cifThen = (CIfThen) command;
            cifThen.leftValue.SetEntity(t);
            cifThen.rightValue.SetEntity(t);
            if (cifThen.Evaulate())
            {
              ++index;
              break;
            }
            int num2 = 0;
            if (t.commandsByIdentifier.TryGetValue(cifThen.commandID, out num2))
            {
              index = num2;
              break;
            }
            ++index;
            break;
          case Command.Type.IntitializeVariables:
            foreach (CIntitializeVarible.ListVars listVars in ((CIntitializeVarible) command).list)
              t.namedVariable.Add(listVars.identifier, listVars.value);
            ++index;
            break;
          case Command.Type.Nothing:
            ++index;
            break;
          case Command.Type.NextTurn:
            Client.game.NextTurn();
            ++index;
            break;
          case Command.Type.VariableMaths:
            CAddToVarible caddToVarible = (CAddToVarible) command;
            caddToVarible.varible.SetEntity(t);
            caddToVarible.add.SetEntity(t);
            switch (caddToVarible.algorithm)
            {
              case MyMaths.Addition:
                caddToVarible.varible.value += caddToVarible.add.value;
                break;
              case MyMaths.Subtraction:
                caddToVarible.varible.value -= caddToVarible.add.value;
                break;
              case MyMaths.Multiplcation:
                caddToVarible.varible.value *= caddToVarible.add.value;
                break;
              case MyMaths.Division:
                caddToVarible.varible.value /= caddToVarible.add.value;
                break;
              case MyMaths.Modulus:
                caddToVarible.varible.value %= caddToVarible.add.value;
                break;
            }
            ++index;
            break;
          case Command.Type.Script:
            t.DisposeDebugger();
            CScript cscript = (CScript) command;
            Script script = (Script) null;
            try
            {
              script = cscript.GetScript(t);
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex);
              Client.allowtutorialDebugging = true;
              Tutorial.Print(ex.Message.ToString());
              yield break;
            }
            DynValue function = script.Globals.Get("main");
            if (!function.IsNil())
            {
              DynValue onKeyDown = script.Globals.Get("onKeyDown");
              DynValue onKeyHeld = script.Globals.Get("onKeyHeld");
              DynValue onKeyUp = script.Globals.Get("onKeyUp");
              DynValue onDeath = script.Globals.Get("onDeath");
              DynValue onCast = script.Globals.Get("onSpellCast");
              DynValue onFrame = script.Globals.Get("onFrame");
              DynValue dynValue1 = script.Globals.Get("waitFPS");
              DynValue dynValue2 = script.Globals.Get("wait");
              snap = onKeyDown.IsNotNil();
              bool hasKeyHeld = onKeyHeld.IsNotNil();
              bool hasKeyUp = onKeyUp.IsNotNil();
              if (onDeath.IsNotNil())
                t.onDeath = (Action<ContainerCreature>) (z => script.Call(onDeath, new object[1]
                {
                  (object) z
                }));
              if (onCast.IsNotNil())
                t.onCast = (Action<ContainerCreature, ContainerSpell>) ((z, s) => script.Call(onCast, new object[2]
                {
                  (object) z,
                  (object) s
                }));
              if (onFrame.IsNotNil())
                t.onFrame = (Action<int>) (z => script.Call(onFrame, new object[1]
                {
                  (object) z
                }));
              if (dynValue1.IsNil())
                script.DoString("function waitFPS(iterations)\r\n    iterations = iterations + game.frame\r\n\twhile iterations > game.frame do\r\n\t\tcoroutine.yield(0)\r\n\tend\r\nend");
              if (dynValue2.IsNil())
                script.DoString("function wait(t)\r\n    local e = (game.totalTimeElapsed + t) \r\n    while (e > game.totalTimeElapsed) do\r\n        coroutine.yield(0)  \r\n    end\r\nend");
              DynValue coroutine = script.CreateCoroutine(function);
              while (true)
              {
                try
                {
                  if (snap && Input.anyKeyDown && Client.game.AllowCallbacks)
                  {
                    for (int index4 = 0; index4 < t.keyCodes.Length; ++index4)
                    {
                      if (Input.GetKeyDown(t.keyCodes[index4]))
                        script.Call(onKeyDown, new object[1]
                        {
                          (object) t.keyCodes[index4]
                        });
                    }
                  }
                  if (hasKeyHeld && Input.anyKey && Client.game.AllowCallbacks)
                  {
                    for (int index5 = 0; index5 < t.keyCodes.Length; ++index5)
                    {
                      if (Input.GetKey(t.keyCodes[index5]))
                        script.Call(onKeyHeld, new object[1]
                        {
                          (object) t.keyCodes[index5]
                        });
                    }
                  }
                  if (hasKeyUp && Client.game.AllowCallbacks)
                    t.HandleKeyUp(script, onKeyUp);
                  coroutine.Coroutine.Resume();
                }
                catch (ScriptRuntimeException ex)
                {
                  Debug.LogError((object) ex);
                  Client.allowtutorialDebugging = true;
                  Tutorial.Print(ex.Message.ToString());
                }
                catch (Exception ex)
                {
                  Debug.LogError((object) ex);
                  Client.allowtutorialDebugging = true;
                  Tutorial.Print(ex.Message.ToString());
                }
                while (t.waitingForContinue)
                  yield return 0.0f;
                if (coroutine.Coroutine.State != CoroutineState.Dead)
                  yield return 0.0f;
                else
                  break;
              }
              ++index;
              t.RemoveCallbacks();
              break;
            }
            break;
        }
        yield return 0.0f;
      }
    }

    public static void Print(string s)
    {
      if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null)
        Controller.Instance.ShowChatBox(false);
      ChatBox.Instance?.NewChatMsg(s, (Color) ColorScheme.GetColor(Global.ColorSystem));
    }

    public static ZPerson CreatePerson(
      int team,
      string name,
      SettingsPlayer settings,
      bool onPlayerPanel)
    {
      if (team > 11)
        team = 11;
      GameFacts gameFacts = HUD.instance.game.gameFacts;
      ZPerson person = new ZPerson()
      {
        game = HUD.instance.game,
        id = (byte) team,
        team = team,
        name = team == 0 ? Client.Name : name,
        settingsPlayer = settings
      };
      person.clientColor = TeamColors.GetColor((int) person.id);
      if (onPlayerPanel)
      {
        gameFacts.game.players.Add(person);
        TcpConnection tcpConnection = new TcpConnection();
        gameFacts.connections.Add((Connection) tcpConnection);
        tcpConnection.player.player = person;
        tcpConnection.player.account = person.account;
        person.connection = (Connection) tcpConnection;
      }
      else
        gameFacts.game._playersExtended.Add(person);
      return person;
    }

    public class ReturnEntity
    {
      public int identifier;
      public int index;
      public string name;
    }
  }
}
