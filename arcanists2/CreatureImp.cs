

#nullable disable
public class CreatureImp : Creature
{
  public override ZCreature Get() => (ZCreature) new ZCreatureImp();
}
