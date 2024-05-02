

#nullable disable
namespace Educative
{
  public class CPanCamera : Command
  {
    public Point point_area = new Point();
    public bool bool_instant;
    public bool bool_interuptable = true;

    public CPanCamera() => this.type = Command.Type.PanCamera;
  }
}
