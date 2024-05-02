

#nullable disable
namespace Educative
{
  public class CWaitTime : Command
  {
    public float time;

    public CWaitTime() => this.type = Command.Type.WaitTime;
  }
}
