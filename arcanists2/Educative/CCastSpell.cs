

#nullable disable
namespace Educative
{
  public class CCastSpell : Command
  {
    public int entity = -2;
    public SpellEnum spellEnum = SpellEnum.Fire_Ball;
    public Point point_target = new Point();
    public float angle;
    public float power = 1f;

    public CCastSpell() => this.type = Command.Type.CastSpell;
  }
}
