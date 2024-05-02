// Decompiled with JetBrains decompiler
// Type: UnityThreading.ObjectExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace UnityThreading
{
  public static class ObjectExtension
  {
    public static Task RunAsync(this object that, string methodName, params object[] args)
    {
      return (Task) that.RunAsync<object>(methodName, (TaskDistributor) null, args);
    }

    public static Task RunAsync(
      this object that,
      string methodName,
      TaskDistributor target,
      params object[] args)
    {
      return (Task) that.RunAsync<object>(methodName, target, args);
    }

    public static Task<T> RunAsync<T>(this object that, string methodName, params object[] args)
    {
      return that.RunAsync<T>(methodName, (TaskDistributor) null, args);
    }

    public static Task<T> RunAsync<T>(
      this object that,
      string methodName,
      TaskDistributor target,
      params object[] args)
    {
      return Task.Create<T>(that, methodName, args).Run((DispatcherBase) target);
    }
  }
}
