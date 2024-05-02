

#nullable disable
namespace Educative
{
  public class CInfo : Command
  {
    public string message = "Empty";
    public bool bool_onContinue = true;
    public bool bool_pauseGame;

    public CInfo() => this.type = Command.Type.Info;
  }
}
