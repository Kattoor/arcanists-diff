

#nullable disable
namespace Educative
{
  public class CWaitForMoveToArea : Command
  {
    public int entity = -2;
    public Point point_area = new Point();
    public int radius = 256;

    public CWaitForMoveToArea() => this.type = Command.Type.WaitForMoveToArea;
  }
}
