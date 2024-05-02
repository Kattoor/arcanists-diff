

#nullable disable
namespace UnityThreading
{
  public class SwitchTo
  {
    public static readonly SwitchTo MainThread = new SwitchTo(SwitchTo.TargetType.Main);
    public static readonly SwitchTo Thread = new SwitchTo(SwitchTo.TargetType.Thread);

    public SwitchTo.TargetType Target { get; private set; }

    private SwitchTo(SwitchTo.TargetType target) => this.Target = target;

    public enum TargetType
    {
      Main,
      Thread,
    }
  }
}
