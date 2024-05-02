

#nullable disable
namespace Educative
{
  public class CWaitForDeath : Command
  {
    public int entity = -2;

    public CWaitForDeath() => this.type = Command.Type.WaitForDeath;
  }
}
