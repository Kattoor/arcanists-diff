

#nullable disable
namespace Educative
{
  public class CArmageddon : Command
  {
    public MapEnum armageddon = MapEnum.Grassy_Hills;
    public int turn = 10;

    public CArmageddon() => this.type = Command.Type.Armageddon;
  }
}
