

#nullable disable
public class CreatureFlameDragon : Creature
{
  public DamageType ImmuneTo = DamageType.Napalm;

  public override ZCreature Get()
  {
    return (ZCreature) new ZCreatureFlameDragon()
    {
      ImmuneTo = this.ImmuneTo
    };
  }
}
