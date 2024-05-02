

#nullable disable
namespace Educative
{
  public class CLockSpell : Command
  {
    public int entity = -2;
    public SpellEnum spellEnum = SpellEnum.Fire_Ball;
    public bool bool_lock = true;

    public CLockSpell() => this.type = Command.Type.LockSpell;
  }
}
