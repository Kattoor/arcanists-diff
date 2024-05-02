

#nullable disable
public class CreatureThorn : Creature
{
  public Spell spell;

  public override ZCreature Get() => (ZCreature) new ZCreatureThorn();
}
