

#nullable disable
namespace Educative
{
  public class CEnableMovement : Command
  {
    public bool bool_canMove;
    public bool bool_terrainDestruction;
    public bool bool_takeDamage;
    public bool bool_canSkipTurn;

    public CEnableMovement() => this.type = Command.Type.EnableMovement;
  }
}
