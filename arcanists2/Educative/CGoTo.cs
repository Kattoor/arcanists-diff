

#nullable disable
namespace Educative
{
  public class CGoTo : Command
  {
    public int commandID = -1;

    public CGoTo() => this.type = Command.Type.GoTo;
  }
}
