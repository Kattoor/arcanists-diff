

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
