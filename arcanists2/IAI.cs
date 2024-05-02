

#nullable disable
public abstract class IAI
{
  public ZGame game;
  public ZPerson parent;
  public ZCreature creature;

  public abstract void DoTurn();
}
