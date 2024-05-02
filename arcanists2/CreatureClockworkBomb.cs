

#nullable disable
public class CreatureClockworkBomb : Creature
{
  public SpellClockworkBomb spell;

  public override ZCreature Get() => (ZCreature) new ZCreatureClockworkBomb();
}
