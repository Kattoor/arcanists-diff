

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityThreading;

#nullable disable
[ExecuteInEditMode]
public class UnityThreadHelper : MonoBehaviour
{
  private static UnityThreadHelper instance = (UnityThreadHelper) null;
  private static object syncRoot = new object();
  private static Thread mainThread;
  private Dispatcher dispatcher;
  private TaskDistributor taskDistributor;
  private List<ThreadBase> registeredThreads = new List<ThreadBase>();

  public static bool OnMainThread() => UnityThreadHelper.mainThread == Thread.CurrentThread;

  public static void EnsureHelper()
  {
    lock (UnityThreadHelper.syncRoot)
    {
      if (UnityThreadHelper.instance != null)
        return;
      UnityThreadHelper.mainThread = Thread.CurrentThread;
      UnityThreadHelper.instance = UnityEngine.Object.FindObjectOfType(typeof (UnityThreadHelper)) as UnityThreadHelper;
      if (UnityThreadHelper.instance != null)
        return;
      GameObject target = new GameObject("[UnityThreadHelper]");
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
      target.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
      UnityThreadHelper.instance = target.AddComponent<UnityThreadHelper>();
      UnityThreadHelper.instance.EnsureHelperInstance();
    }
  }

  private static UnityThreadHelper Instance
  {
    get
    {
      UnityThreadHelper.EnsureHelper();
      return UnityThreadHelper.instance;
    }
  }

  public static Dispatcher Dispatcher => UnityThreadHelper.Instance.CurrentDispatcher;

  public static TaskDistributor TaskDistributor
  {
    get => UnityThreadHelper.Instance.CurrentTaskDistributor;
  }

  public Dispatcher CurrentDispatcher => this.dispatcher;

  public TaskDistributor CurrentTaskDistributor => this.taskDistributor;

  private void EnsureHelperInstance()
  {
    this.dispatcher = Dispatcher.MainNoThrow ?? new Dispatcher();
    this.taskDistributor = TaskDistributor.MainNoThrow ?? new TaskDistributor("TaskDistributor");
  }

  public static ActionThread CreateThread(Action<ActionThread> action, bool autoStartThread)
  {
    UnityThreadHelper.Instance.EnsureHelperInstance();
    ActionThread thread = new ActionThread((Action<ActionThread>) (currentThread =>
    {
      try
      {
        action(currentThread);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
      }
    }), autoStartThread);
    UnityThreadHelper.Instance.RegisterThread((ThreadBase) thread);
    return thread;
  }

  public static ActionThread CreateThread(Action<ActionThread> action)
  {
    return UnityThreadHelper.CreateThread(action, true);
  }

  public static ActionThread CreateThread(Action action, bool autoStartThread)
  {
    return UnityThreadHelper.CreateThread((Action<ActionThread>) (thread => action()), autoStartThread);
  }

  public static ActionThread CreateThread(Action action)
  {
    return UnityThreadHelper.CreateThread((Action<ActionThread>) (thread => action()), true);
  }

  public static ThreadBase CreateThread(Func<ThreadBase, IEnumerator> action, bool autoStartThread)
  {
    UnityThreadHelper.Instance.EnsureHelperInstance();
    EnumeratableActionThread thread = new EnumeratableActionThread(action, autoStartThread);
    UnityThreadHelper.Instance.RegisterThread((ThreadBase) thread);
    return (ThreadBase) thread;
  }

  public static ThreadBase CreateThread(Func<ThreadBase, IEnumerator> action)
  {
    return UnityThreadHelper.CreateThread(action, true);
  }

  public static ThreadBase CreateThread(Func<IEnumerator> action, bool autoStartThread)
  {
    return UnityThreadHelper.CreateThread((Func<ThreadBase, IEnumerator>) (thread => action()), autoStartThread);
  }

  public static ThreadBase CreateThread(Func<IEnumerator> action)
  {
    return UnityThreadHelper.CreateThread((Func<ThreadBase, IEnumerator>) (thread => action()), true);
  }

  private void RegisterThread(ThreadBase thread)
  {
    if (this.registeredThreads.Contains(thread))
      return;
    this.registeredThreads.Add(thread);
  }

  private void OnDestroy()
  {
    foreach (ThreadBase registeredThread in this.registeredThreads)
      registeredThread.Dispose();
    if (this.dispatcher != null)
      this.dispatcher.Dispose();
    this.dispatcher = (Dispatcher) null;
    if (this.taskDistributor != null)
      this.taskDistributor.Dispose();
    this.taskDistributor = (TaskDistributor) null;
    if (!((UnityEngine.Object) UnityThreadHelper.instance == (UnityEngine.Object) this))
      return;
    UnityThreadHelper.instance = (UnityThreadHelper) null;
  }

  private void Update()
  {
    if (this.dispatcher != null)
      this.dispatcher.ProcessTasks();
    foreach (ThreadBase threadBase in this.registeredThreads.Where<ThreadBase>((Func<ThreadBase, bool>) (thread => !thread.IsAlive)).ToArray<ThreadBase>())
    {
      threadBase.Dispose();
      this.registeredThreads.Remove(threadBase);
    }
  }
}
