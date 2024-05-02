// Decompiled with JetBrains decompiler
// Type: Educative.Summon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;

#nullable disable
namespace Educative
{
  public class Summon
  {
    public object spell;
    public Point position = new Point(750.0, 750.0);
    public int team = 1;
    public bool onPlayersPanel;
    public bool useAI = true;
    public bool no_AI_still_use_turn;
    public string name = "";
    public bool playSound = true;
    public Table colors;
    public Table spells;
    public Table outfit;
    public BookOf elemental = BookOf.Nothing;

    public static Summon construct(
      object spell = null,
      Point point = null,
      int team = 1,
      Table colors = null,
      bool playSound = true,
      bool onPlayersPanel = false,
      string name = "",
      Table outfit = null,
      Table spells = null,
      BookOf elemental = BookOf.Nothing)
    {
      return new Summon()
      {
        spell = spell,
        position = point != (Point) null ? point : new Point(750.0, 750.0),
        team = team,
        colors = colors,
        outfit = outfit,
        spells = spells,
        elemental = elemental,
        playSound = playSound,
        onPlayersPanel = onPlayersPanel,
        name = name
      };
    }
  }
}
