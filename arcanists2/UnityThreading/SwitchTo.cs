// Decompiled with JetBrains decompiler
// Type: UnityThreading.SwitchTo
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
