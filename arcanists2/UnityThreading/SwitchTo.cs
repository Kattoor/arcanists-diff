// Decompiled with JetBrains decompiler
// Type: UnityThreading.SwitchTo
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
