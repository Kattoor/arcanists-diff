

#nullable disable
public class CreatureJavelin : Creature
{
  public Spell spell;

  public override ZCreature Get() => (ZCreature) new ZCreatureJavelin();
}
