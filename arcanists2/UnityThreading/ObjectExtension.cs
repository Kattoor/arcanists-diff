// Decompiled with JetBrains decompiler
// Type: UnityThreading.ObjectExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
