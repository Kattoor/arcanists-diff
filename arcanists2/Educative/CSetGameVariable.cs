

#nullable disable
namespace Educative
{
  public class CSetGameVariable : Command
  {
    public TutInt variable = new TutInt();

    public CSetGameVariable() => this.type = Command.Type.SetGameVariable;
  }
}
