// Decompiled with JetBrains decompiler
// Type: Quickchat
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public static class Quickchat
{
  public const int Count = 7;
  private static bool _initialized = false;
  public static List<Quickchat.Command> comGreetings = new List<Quickchat.Command>()
  {
    new Quickchat.Command() { name = "Ahoy!", key = KeyCode.A },
    new Quickchat.Command()
    {
      name = "Good Day.",
      key = KeyCode.G
    },
    new Quickchat.Command() { name = "Hello.", key = KeyCode.H },
    new Quickchat.Command() { name = "hi", key = KeyCode.I },
    new Quickchat.Command()
    {
      name = "How's it going?",
      key = KeyCode.W
    },
    new Quickchat.Command() { name = "Sup?", key = KeyCode.S },
    new Quickchat.Command()
    {
      name = "What's up?",
      key = KeyCode.U
    },
    new Quickchat.Command() { name = "Yo", key = KeyCode.Y },
    new Quickchat.Command()
    {
      name = "[Emoji]",
      value = "{0}",
      options = new Quickchat.Options[1]
      {
        Quickchat.Options.Emoji
      },
      key = KeyCode.E
    }
  };
  public static List<Quickchat.Command> comGoodbye = new List<Quickchat.Command>()
  {
    new Quickchat.Command() { name = "bye", key = KeyCode.B },
    new Quickchat.Command() { name = "cya", key = KeyCode.C },
    new Quickchat.Command()
    {
      name = "Goodbye.",
      key = KeyCode.G
    },
    new Quickchat.Command()
    {
      name = "Good Night.",
      key = KeyCode.N
    }
  };
  public static List<Quickchat.Command> comAnswer = new List<Quickchat.Command>()
  {
    new Quickchat.Command() { name = "Yes.", key = KeyCode.Y },
    new Quickchat.Command() { name = "No.", key = KeyCode.N },
    new Quickchat.Command() { name = "Okay.", key = KeyCode.O },
    new Quickchat.Command() { name = "Maybe.", key = KeyCode.M },
    new Quickchat.Command()
    {
      name = "Unsure.",
      key = KeyCode.U
    },
    new Quickchat.Command()
    {
      name = "Thank you.",
      key = KeyCode.T
    },
    new Quickchat.Command()
    {
      name = "You're Welcome.",
      key = KeyCode.W
    },
    new Quickchat.Command()
    {
      name = "No problem",
      key = KeyCode.P
    },
    new Quickchat.Command()
    {
      name = "I'm doing great.",
      key = KeyCode.D
    },
    new Quickchat.Command()
    {
      name = "I'm doing good.",
      key = KeyCode.I
    },
    new Quickchat.Command()
    {
      name = "I'm doing okay.",
      key = KeyCode.K
    },
    new Quickchat.Command()
    {
      name = "I've been better.",
      key = KeyCode.B
    },
    new Quickchat.Command() { name = "Sorry.", key = KeyCode.R },
    new Quickchat.Command()
    {
      name = "My Apologies.",
      key = KeyCode.G
    },
    new Quickchat.Command()
    {
      name = "Awesome!",
      key = KeyCode.A
    },
    new Quickchat.Command() { name = "Cool!", key = KeyCode.C },
    new Quickchat.Command()
    {
      name = "Of Course.",
      key = KeyCode.F
    },
    new Quickchat.Command()
    {
      name = "That's fine.",
      key = KeyCode.J
    },
    new Quickchat.Command()
    {
      name = "No thank you.",
      key = KeyCode.H
    },
    new Quickchat.Command()
    {
      name = "Never mind.",
      key = KeyCode.V
    },
    new Quickchat.Command()
    {
      name = "My pleasure.",
      key = KeyCode.L
    },
    new Quickchat.Command()
    {
      name = "I can only use quickchat.",
      key = KeyCode.Q
    }
  };
  public static List<Quickchat.Command> comVerify = new List<Quickchat.Command>()
  {
    new Quickchat.Command()
    {
      name = "How do I get verified?",
      key = KeyCode.H
    },
    new Quickchat.Command()
    {
      name = "Verify your account in the main menu top-right corner.",
      key = KeyCode.V
    }
  };
  public static List<Quickchat.Command> comGame = new List<Quickchat.Command>()
  {
    new Quickchat.Command()
    {
      name = "Want to play?",
      key = KeyCode.W
    },
    new Quickchat.Command()
    {
      name = "Would anyone like to play?",
      key = KeyCode.P
    },
    new Quickchat.Command()
    {
      name = "Settings: [Time] [Map] [Style] [Player Count]",
      value = "I play {0}, {1}, {2}, {3}",
      options = new Quickchat.Options[4]
      {
        Quickchat.Options.Time,
        Quickchat.Options.Map,
        Quickchat.Options.Style,
        Quickchat.Options.PlayerCount
      },
      key = KeyCode.S
    },
    new Quickchat.Command()
    {
      name = "Would you like to play [(UN)Rated] with me?",
      value = "Would you like to play {0} with me?",
      options = new Quickchat.Options[1]
      {
        Quickchat.Options.Ranked
      },
      key = KeyCode.U
    },
    new Quickchat.Command()
    {
      name = "Anyone play [(UN)Rated] with me?",
      value = "Anyone play {0} with me?",
      options = new Quickchat.Options[1]
      {
        Quickchat.Options.Ranked
      },
      key = KeyCode.R
    },
    new Quickchat.Command()
    {
      name = "Lets go!",
      key = KeyCode.L
    },
    new Quickchat.Command()
    {
      name = "Searching!",
      key = KeyCode.E
    },
    new Quickchat.Command()
    {
      name = "Going in queue.",
      key = KeyCode.Q
    }
  };
  public static List<Quickchat.Command> comDuringGame = new List<Quickchat.Command>()
  {
    new Quickchat.Command()
    {
      name = "My Fault.",
      key = KeyCode.F
    },
    new Quickchat.Command() { name = "Oops.", key = KeyCode.O },
    new Quickchat.Command()
    {
      name = "Nice job!",
      key = KeyCode.N
    },
    new Quickchat.Command()
    {
      name = "Great shot.",
      key = KeyCode.G
    },
    new Quickchat.Command()
    {
      name = "Good move.",
      key = KeyCode.V
    },
    new Quickchat.Command()
    {
      name = "Moving closer.",
      key = KeyCode.R
    },
    new Quickchat.Command()
    {
      name = "Moving away.",
      key = KeyCode.A
    },
    new Quickchat.Command()
    {
      name = "Playing Offensive.",
      key = KeyCode.E
    },
    new Quickchat.Command()
    {
      name = "Playing Defensive.",
      key = KeyCode.D
    },
    new Quickchat.Command()
    {
      name = "Planned.",
      key = KeyCode.P
    },
    new Quickchat.Command()
    {
      name = "Calculated.",
      key = KeyCode.C
    },
    new Quickchat.Command()
    {
      name = "Need Help.",
      key = KeyCode.H
    },
    new Quickchat.Command()
    {
      name = "Help me out.",
      key = KeyCode.T
    },
    new Quickchat.Command()
    {
      name = "I have you now.",
      key = KeyCode.U
    },
    new Quickchat.Command()
    {
      name = "That's gotta hurt.",
      key = KeyCode.X
    },
    new Quickchat.Command()
    {
      name = "Leave me alone!",
      key = KeyCode.L
    },
    new Quickchat.Command()
    {
      name = "Don't kill me!",
      key = KeyCode.K
    },
    new Quickchat.Command()
    {
      name = "Spell: []",
      value = "Spell: {0}",
      options = new Quickchat.Options[1]
      {
        Quickchat.Options.Spell
      },
      key = KeyCode.S
    },
    new Quickchat.Command()
    {
      name = "Minion: []",
      value = "Minion: {0}",
      options = new Quickchat.Options[1]
      {
        Quickchat.Options.Minion
      },
      key = KeyCode.M
    }
  };
  public static List<Quickchat.Command> comGameEnd = new List<Quickchat.Command>()
  {
    new Quickchat.Command() { name = "GG", key = KeyCode.G },
    new Quickchat.Command()
    {
      name = "Good Game.",
      key = KeyCode.M
    },
    new Quickchat.Command()
    {
      name = "Rematch?",
      key = KeyCode.R
    },
    new Quickchat.Command()
    {
      name = "Down to play again?",
      key = KeyCode.D
    },
    new Quickchat.Command()
    {
      name = "Well played!",
      key = KeyCode.W
    },
    new Quickchat.Command()
    {
      name = "You played well.",
      key = KeyCode.P
    },
    new Quickchat.Command()
    {
      name = "Nice carry.",
      key = KeyCode.N
    },
    new Quickchat.Command()
    {
      name = "That was fun.",
      key = KeyCode.T
    },
    new Quickchat.Command()
    {
      name = "Unlucky.",
      key = KeyCode.U
    },
    new Quickchat.Command()
    {
      name = "Maybe next time.",
      key = KeyCode.X
    }
  };
  private static Dictionary<int, Quickchat.Command> _byID = new Dictionary<int, Quickchat.Command>();
  internal static Quickchat.Command temp;
  public static List<string> PlayerCounts = new List<string>()
  {
    "Any Player Count",
    "1v1",
    "3 Player FFA",
    "4 Player FFA",
    "5 Player FFA",
    "6 Player FFA",
    "6+ Player FFA",
    "2v2",
    "3v3",
    "4v4+"
  };
  public static List<string> Maps = new List<string>();
  public static List<string> Times = new List<string>()
  {
    "Any Time",
    "120s",
    "90s",
    "60s",
    "45s",
    "30s",
    "20s",
    "10s",
    "8s",
    "5s"
  };
  public static List<string> Styles = new List<string>()
  {
    "Standard",
    "Shuffle Players",
    "Elementals",
    "Custom HP",
    "Random Spells",
    "Original Spells",
    "No Movement",
    "Zero Shield",
    "FTT",
    "Wind",
    "Big Map",
    "Zombie Monkey",
    "Watchtower",
    "Arcane Monster",
    "Any Style"
  };
  public static List<string> Ranked = new List<string>()
  {
    "Rated",
    "Unrated"
  };
  public static List<string> Minion = new List<string>();
  public static List<string> Spell = new List<string>();

  public static (string name, List<Quickchat.Command> list, KeyCode key) AllCommands(int i)
  {
    switch (i)
    {
      case 0:
        return ("Greetings", Quickchat.comGreetings, KeyCode.Alpha1);
      case 1:
        return ("Goodbye", Quickchat.comGoodbye, KeyCode.Alpha2);
      case 2:
        return ("Answer", Quickchat.comAnswer, KeyCode.Alpha3);
      case 3:
        return ("Verify", Quickchat.comVerify, KeyCode.Alpha4);
      case 4:
        return ("Game", Quickchat.comGame, KeyCode.Alpha5);
      case 5:
        return ("During Game", Quickchat.comDuringGame, KeyCode.Alpha6);
      case 6:
        return ("Game Ended", Quickchat.comGameEnd, KeyCode.Alpha7);
      default:
        return ("Greetings", Quickchat.comGreetings, KeyCode.Alpha1);
    }
  }

  public static void Initialize()
  {
    if (Quickchat._initialized)
      return;
    Quickchat._initialized = true;
    Quickchat.GetByID(0);
    foreach (KeyValuePair<SpellEnum, global::Spell> spellsEnum in Inert.Instance.spellsEnums)
      Quickchat.Spell.Add(spellsEnum.Value.name);
    Quickchat.Spell.Sort();
    foreach (global::Spell minionSpell in Inert.Instance.MinionSpells)
      Quickchat.Minion.Add(minionSpell.toSummon.name);
    Quickchat.Minion.Sort();
    foreach (MapEnum e in (MapEnum[]) Enum.GetValues(typeof (MapEnum)))
    {
      if (e == MapEnum.Dont_Mind)
        Quickchat.Maps.Add("Any Map");
      else
        Quickchat.Maps.Add(GameFacts.MapName(e));
    }
    Quickchat.Maps.Sort();
  }

  public static bool GetByID(int id)
  {
    if (Quickchat._byID.Count == 0)
    {
      int key = 0;
      for (int i = 0; i < 7; ++i)
      {
        foreach (Quickchat.Command command in Quickchat.AllCommands(i).list)
        {
          command.id = key;
          Quickchat._byID.Add(key, command);
          ++key;
        }
      }
    }
    return Quickchat._byID.TryGetValue(id, out Quickchat.temp);
  }

  public static void Send(int id, int[] options, ChatOrigination destionation, string pri)
  {
    if (destionation == ChatOrigination.Lobby && ((UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null || (UnityEngine.Object) HUD.instance != (UnityEngine.Object) null && !HUD.instance.game.isSpectator))
      destionation = ChatOrigination.Game;
    Quickchat.Data data = new Quickchat.Data()
    {
      name = Client.Name,
      privateDM = pri,
      destination = destionation,
      options = options,
      id = id
    };
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 155);
        data.Serialize(w);
      }
      if (Client.connection != null && (Client.game == null || !Client.game.isSandbox))
      {
        Client.SendChatMsg(memoryStream.ToArray(), 2f);
        if (destionation != ChatOrigination.Private)
          return;
        string command = Quickchat.TryGetCommand(id, options);
        ChatBox.Instance.NewChatMsg("To <sprite name=\"Emoji2_1352\"> " + data.privateDM, command, (Color) ColorScheme.GetColor(Global.ColorSentPrivate), data.privateDM, ChatOrigination.System);
      }
      else
      {
        string command = Quickchat.TryGetCommand(id, options);
        if (command == null)
          return;
        ChatBox.Instance.NewChatMsg("<sprite name=\"Emoji2_1352\"> " + command, (Color) ColorScheme.GetColor(Global.ColorSystem));
      }
    }
  }

  public static string GetDestination(ChatOrigination d)
  {
    switch (d)
    {
      case ChatOrigination.Lobby:
        return "";
      case ChatOrigination.Game:
        return "";
      case ChatOrigination.Private:
        return "From ";
      case ChatOrigination.System:
        return "";
      case ChatOrigination.Clan:
        return "[Clan] ";
      case ChatOrigination.MiniGame:
        return "[Chess] ";
      case ChatOrigination.Team:
        return "[Team] ";
      default:
        return "";
    }
  }

  public static string TryGetCommand(int id, int[] options)
  {
    if (!Quickchat.GetByID(id))
      return (string) null;
    if (Quickchat.temp.options == null || Quickchat.temp.options.Length == 0)
      return Quickchat.temp.value ?? Quickchat.temp.name;
    if (options.Length != Quickchat.temp.options.Length)
      return (string) null;
    string[] strArray = new string[options.Length];
    try
    {
      for (int index = 0; index < Quickchat.temp.options.Length; ++index)
      {
        switch (Quickchat.temp.options[index])
        {
          case Quickchat.Options.None:
            return (string) null;
          case Quickchat.Options.Map:
            strArray[index] = Quickchat.Maps[Mathf.Clamp(options[index], 0, Quickchat.Maps.Count - 1)];
            break;
          case Quickchat.Options.Time:
            strArray[index] = Quickchat.Times[Mathf.Clamp(options[index], 0, Quickchat.Times.Count - 1)];
            break;
          case Quickchat.Options.Style:
            strArray[index] = Quickchat.Styles[Mathf.Clamp(options[index], 0, Quickchat.Styles.Count - 1)];
            break;
          case Quickchat.Options.PlayerCount:
            strArray[index] = Quickchat.PlayerCounts[Mathf.Clamp(options[index], 0, Quickchat.PlayerCounts.Count - 1)];
            break;
          case Quickchat.Options.Ranked:
            strArray[index] = Quickchat.Ranked[Mathf.Clamp(options[index], 0, Quickchat.Ranked.Count - 1)];
            break;
          case Quickchat.Options.Spell:
            strArray[index] = Quickchat.Spell[Mathf.Clamp(options[index], 0, Quickchat.Spell.Count - 1)];
            break;
          case Quickchat.Options.Minion:
            strArray[index] = Quickchat.Minion[Mathf.Clamp(options[index], 0, Quickchat.Minion.Count - 1)];
            break;
          case Quickchat.Options.Emoji:
            strArray[index] = "<sprite name=\"" + EmojiInfo.allEmoji.list[Mathf.Clamp(options[index], 0, EmojiInfo.allEmoji.list.Count - 1)].realName + "\">";
            break;
          default:
            return (string) null;
        }
      }
      return string.Format(Quickchat.temp.value ?? Quickchat.temp.name, (object[]) strArray);
    }
    catch (Exception ex)
    {
    }
    return (string) null;
  }

  public static void SendEmoji(string s)
  {
    Quickchat.Send(Quickchat.AllCommands(0).list[8].id, new int[0], QuickchatUI.targetDestination, QuickchatUI.privateDM);
  }

  public class Data
  {
    public string name;
    public string privateDM;
    public ChatOrigination destination;
    public int id;
    public int[] options;

    public static Quickchat.Data Deserialize(myBinaryReader r)
    {
      Quickchat.Data data = new Quickchat.Data();
      data.name = r.ReadString();
      data.privateDM = r.ReadString();
      data.id = r.ReadInt32();
      data.destination = (ChatOrigination) r.ReadByte();
      data.options = new int[r.ReadInt32()];
      for (int index = 0; index < data.options.Length; ++index)
        data.options[index] = r.ReadInt32();
      return data;
    }

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this.name);
      w.Write(this.privateDM);
      w.Write(this.id);
      w.Write((byte) this.destination);
      w.Write(this.options.Length);
      for (int index = 0; index < this.options.Length; ++index)
        w.Write(this.options[index]);
    }
  }

  public class Command
  {
    public string name;
    public string value;
    public Quickchat.Options[] options;
    public int id;
    public KeyCode key;
  }

  public enum Options
  {
    None,
    Name,
    Map,
    Time,
    Style,
    PlayerCount,
    Ranked,
    Spell,
    Minion,
    Emoji,
  }
}
