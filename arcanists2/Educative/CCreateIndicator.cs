

#nullable disable
namespace Educative
{
  public class CCreateIndicator : Command
  {
    public CCreateIndicator.Kinds kind;
    public float lifetime = -1f;
    public Point point_point = new Point();
    public int radius = 256;
    public float angle;
    public string hexColor = "#ffffffff";

    public CCreateIndicator() => this.type = Command.Type.CreateIndicator;

    public enum Kinds
    {
      Area,
      Point,
      MovingArrow,
      Arrow,
      Meter_Fill,
      Meter_Flash,
      Meter_Bolt,
      Meter_Target,
    }
  }
}
