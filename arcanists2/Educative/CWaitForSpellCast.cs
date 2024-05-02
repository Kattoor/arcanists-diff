

#nullable disable
namespace Educative
{
  public class CWaitForSpellCast : Command
  {
    public int entity = -2;
    public SpellEnum spellEnum;

    public CWaitForSpellCast() => this.type = Command.Type.WaitForSpellCast;
  }
}
