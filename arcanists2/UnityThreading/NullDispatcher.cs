// Decompiled with JetBrains decompiler
// Type: UnityThreading.NullDispatcher
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace UnityThreading
{
  public class NullDispatcher : DispatcherBase
  {
    public static NullDispatcher Null = new NullDispatcher();

    protected override void CheckAccessLimitation()
    {
    }

    internal override void AddTask(Task task) => task.DoInternal();
  }
}
