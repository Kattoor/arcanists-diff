// Decompiled with JetBrains decompiler
// Type: MovementEffects.Timing
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
namespace MovementEffects
{
  public class Timing : MonoBehaviour
  {
    public float TimeBetweenSlowUpdateCalls = 0.142857149f;
    public bool AdditionalDebugInfo;
    public int NumberOfUpdateCoroutines;
    public int NumberOfFixedUpdateCoroutines;
    public int NumberOfLateUpdateCoroutines;
    public int NumberOfSlowUpdateCoroutines;
    [HideInInspector]
    public double localTime;
    [HideInInspector]
    public float deltaTime;
    private bool _runningUpdate;
    private bool _runningFixedUpdate;
    private bool _runningLateUpdate;
    private bool _runningSlowUpdate;
    private int _nextUpdateProcessSlot;
    private int _nextLateUpdateProcessSlot;
    private int _nextFixedUpdateProcessSlot;
    public int _nextServerUpdateProcessSlot;
    private double _lastUpdateTime;
    private double _lastLateUpdateTime;
    private double _lastFixedUpdateTime;
    private double _lastSlowUpdateTime;
    private ushort _framesSinceUpdate;
    private ushort _expansions = 1;
    private const ushort FramesUntilMaintenance = 64;
    private const int ProcessArrayChunkSize = 64;
    private const int InitialBufferSizeLarge = 256;
    private const int InitialBufferSizeMedium = 64;
    private const int InitialBufferSizeSmall = 8;
    public Action<Exception> OnError;
    public static Func<IEnumerator<float>, Segment, string, IEnumerator<float>> ReplacementFunction;
    private readonly List<Timing.WaitingProcess> _waitingProcesses = new List<Timing.WaitingProcess>();
    private readonly Queue<Exception> _exceptions = new Queue<Exception>();
    private readonly Dictionary<Timing.ProcessIndex, string> _processTags = new Dictionary<Timing.ProcessIndex, string>();
    private readonly Dictionary<string, HashSet<Timing.ProcessIndex>> _taggedProcesses = new Dictionary<string, HashSet<Timing.ProcessIndex>>();
    private IEnumerator<float>[] UpdateProcesses = new IEnumerator<float>[256];
    private IEnumerator<float>[] LateUpdateProcesses = new IEnumerator<float>[8];
    private IEnumerator<float>[] FixedUpdateProcesses = new IEnumerator<float>[64];
    private IEnumerator<float>[] SlowUpdateProcesses = new IEnumerator<float>[64];
    private static Timing _instance;

    public static float LocalTime => (float) Timing.Instance.localTime;

    public static float DeltaTime => Timing.Instance.deltaTime;

    public static bool isNotNull() => (UnityEngine.Object) Timing._instance != (UnityEngine.Object) null;

    public static Timing Instance
    {
      get
      {
        if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || !(bool) (UnityEngine.Object) Timing._instance.gameObject)
        {
          GameObject gameObject1 = GameObject.Find("Movement Effects");
          System.Type type = System.Type.GetType("MovementEffects.Movement, MovementOverTime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
          if ((UnityEngine.Object) gameObject1 == (UnityEngine.Object) null)
          {
            GameObject gameObject2 = new GameObject();
            gameObject2.name = "Movement Effects";
            GameObject target = gameObject2;
            UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
            if (type != (System.Type) null)
              target.AddComponent(type);
            Timing._instance = target.AddComponent<Timing>();
          }
          else
          {
            if (type != (System.Type) null && (UnityEngine.Object) gameObject1.GetComponent(type) == (UnityEngine.Object) null)
              gameObject1.AddComponent(type);
            Timing._instance = gameObject1.GetComponent<Timing>();
            if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null)
              Timing._instance = gameObject1.AddComponent<Timing>();
          }
        }
        return Timing._instance;
      }
      set => Timing._instance = value;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null)
        Timing._instance = this;
      else
        this.deltaTime = Timing._instance.deltaTime;
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) Timing._instance == (UnityEngine.Object) this))
        return;
      Timing._instance = (Timing) null;
    }

    public void ServerUpdate()
    {
      if (this._nextServerUpdateProcessSlot <= 0)
        return;
      Timing.ProcessIndex processIndex = new Timing.ProcessIndex()
      {
        seg = Segment.SlowUpdate
      };
      Timing.ProcessIndex coindexTo;
      coindexTo.seg = Segment.SlowUpdate;
      this._runningSlowUpdate = true;
      this.UpdateTimeValues(processIndex.seg);
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextServerUpdateProcessSlot; ++processIndex.i)
      {
        if (this.SlowUpdateProcesses[processIndex.i] != null)
        {
          if ((double) Time.realtimeSinceStartup >= (double) this.SlowUpdateProcesses[processIndex.i].Current)
          {
            try
            {
              if (!this.SlowUpdateProcesses[processIndex.i].MoveNext())
                this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.SlowUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.SlowUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.SlowUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.SlowUpdateProcesses[processIndex.i], processIndex.seg, this._processTags.ContainsKey(processIndex) ? this._processTags[processIndex] : (string) null);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                    --processIndex.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex);
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
          }
        }
        if (this.SlowUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.SlowUpdateProcesses[coindexTo.i] = this.SlowUpdateProcesses[processIndex.i];
            this.MoveTag(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextServerUpdateProcessSlot; ++processIndex.i)
      {
        this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.RemoveTag(processIndex);
      }
      this.NumberOfSlowUpdateCoroutines = this._nextServerUpdateProcessSlot = coindexTo.i;
      this._runningSlowUpdate = false;
    }

    private void Update()
    {
      if (this._nextUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.Update
        };
        this._runningUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextUpdateProcessSlot; ++key.i)
        {
          if (this.UpdateProcesses[key.i] != null)
          {
            if (this.localTime >= (double) this.UpdateProcesses[key.i].Current)
            {
              try
              {
                if (!this.UpdateProcesses[key.i].MoveNext())
                  this.UpdateProcesses[key.i] = (IEnumerator<float>) null;
                else if (this.UpdateProcesses[key.i] != null)
                {
                  if (float.IsNaN(this.UpdateProcesses[key.i].Current))
                  {
                    if (Timing.ReplacementFunction == null)
                    {
                      this.UpdateProcesses[key.i] = (IEnumerator<float>) null;
                    }
                    else
                    {
                      this.UpdateProcesses[key.i] = Timing.ReplacementFunction(this.UpdateProcesses[key.i], key.seg, this._processTags.ContainsKey(key) ? this._processTags[key] : (string) null);
                      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                      --key.i;
                    }
                  }
                }
              }
              catch (Exception ex)
              {
                Debug.LogError((object) ex);
                if (this.OnError == null)
                  this._exceptions.Enqueue(ex);
                else
                  this.OnError(ex);
                this.UpdateProcesses[key.i] = (IEnumerator<float>) null;
              }
            }
          }
        }
        this._runningUpdate = false;
      }
      if (++this._framesSinceUpdate > (ushort) 64)
      {
        this._framesSinceUpdate = (ushort) 0;
        this.RemoveUnused();
      }
      if (this._exceptions.Count <= 0)
        return;
      this._exceptions.Dequeue();
    }

    private void FixedUpdate()
    {
      if (this._nextFixedUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.FixedUpdate
        };
        this._runningFixedUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextFixedUpdateProcessSlot; ++key.i)
        {
          if (this.FixedUpdateProcesses[key.i] != null)
          {
            if (this.localTime >= (double) this.FixedUpdateProcesses[key.i].Current)
            {
              try
              {
                if (!this.FixedUpdateProcesses[key.i].MoveNext())
                  this.FixedUpdateProcesses[key.i] = (IEnumerator<float>) null;
                else if (this.FixedUpdateProcesses[key.i] != null)
                {
                  if (float.IsNaN(this.FixedUpdateProcesses[key.i].Current))
                  {
                    if (Timing.ReplacementFunction == null)
                    {
                      this.FixedUpdateProcesses[key.i] = (IEnumerator<float>) null;
                    }
                    else
                    {
                      this.FixedUpdateProcesses[key.i] = Timing.ReplacementFunction(this.FixedUpdateProcesses[key.i], key.seg, this._processTags.ContainsKey(key) ? this._processTags[key] : (string) null);
                      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                      --key.i;
                    }
                  }
                }
              }
              catch (Exception ex)
              {
                Debug.LogError((object) ex);
                if (this.OnError == null)
                  this._exceptions.Enqueue(ex);
                else
                  this.OnError(ex);
                this.FixedUpdateProcesses[key.i] = (IEnumerator<float>) null;
              }
            }
          }
        }
        this._runningFixedUpdate = false;
      }
      if (this._exceptions.Count <= 0)
        return;
      this._exceptions.Dequeue();
    }

    private void LateUpdate()
    {
      if (this._nextLateUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.LateUpdate
        };
        this._runningLateUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextLateUpdateProcessSlot; ++key.i)
        {
          if (this.LateUpdateProcesses[key.i] != null)
          {
            if (this.localTime >= (double) this.LateUpdateProcesses[key.i].Current)
            {
              try
              {
                if (!this.LateUpdateProcesses[key.i].MoveNext())
                  this.LateUpdateProcesses[key.i] = (IEnumerator<float>) null;
                else if (this.LateUpdateProcesses[key.i] != null)
                {
                  if (float.IsNaN(this.LateUpdateProcesses[key.i].Current))
                  {
                    if (Timing.ReplacementFunction == null)
                    {
                      this.LateUpdateProcesses[key.i] = (IEnumerator<float>) null;
                    }
                    else
                    {
                      this.LateUpdateProcesses[key.i] = Timing.ReplacementFunction(this.LateUpdateProcesses[key.i], key.seg, this._processTags.ContainsKey(key) ? this._processTags[key] : (string) null);
                      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                      --key.i;
                    }
                  }
                }
              }
              catch (Exception ex)
              {
                Debug.LogError((object) ex);
                if (this.OnError == null)
                  this._exceptions.Enqueue(ex);
                else
                  this.OnError(ex);
                this.LateUpdateProcesses[key.i] = (IEnumerator<float>) null;
              }
            }
          }
        }
        this._runningLateUpdate = false;
      }
      if (this._exceptions.Count <= 0)
        return;
      this._exceptions.Dequeue();
    }

    private void UpdateTimeValues(Segment segment)
    {
      switch (segment)
      {
        case Segment.Update:
          this.deltaTime = Time.deltaTime;
          this._lastUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastUpdateTime;
          break;
        case Segment.FixedUpdate:
          this.deltaTime = Time.deltaTime;
          this._lastFixedUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastFixedUpdateTime;
          break;
        case Segment.LateUpdate:
          this.deltaTime = Time.deltaTime;
          this._lastLateUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastLateUpdateTime;
          break;
        case Segment.SlowUpdate:
          this.deltaTime = this._lastSlowUpdateTime != 0.0 ? Time.realtimeSinceStartup - (float) this._lastSlowUpdateTime : this.TimeBetweenSlowUpdateCalls;
          this.localTime = this._lastSlowUpdateTime = (double) Time.realtimeSinceStartup;
          break;
      }
    }

    private void SetTimeValues(Segment segment)
    {
      switch (segment)
      {
        case Segment.Update:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastUpdateTime;
          break;
        case Segment.FixedUpdate:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastFixedUpdateTime;
          break;
        case Segment.LateUpdate:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastLateUpdateTime;
          break;
        case Segment.SlowUpdate:
          this.deltaTime = Time.realtimeSinceStartup - (float) this._lastSlowUpdateTime;
          this.localTime = this._lastSlowUpdateTime = (double) Time.realtimeSinceStartup;
          break;
      }
    }

    public void ResetTimeCountOnInstance()
    {
      this.localTime = 0.0;
      this._lastUpdateTime = 0.0;
      this._lastLateUpdateTime = 0.0;
      this._lastFixedUpdateTime = 0.0;
    }

    public static void PauseCoroutines()
    {
      if (!((UnityEngine.Object) Timing._instance != (UnityEngine.Object) null))
        return;
      Timing._instance.PauseCoroutinesOnInstance();
    }

    public void PauseCoroutinesOnInstance() => this.enabled = false;

    public static void ResumeCoroutines()
    {
      if (!((UnityEngine.Object) Timing._instance != (UnityEngine.Object) null))
        return;
      Timing._instance.ResumeCoroutinesOnInstance();
    }

    public void ResumeCoroutinesOnInstance() => this.enabled = true;

    private void RemoveUnused()
    {
      Timing.ProcessIndex processIndex;
      Timing.ProcessIndex coindexTo;
      processIndex.seg = coindexTo.seg = Segment.Update;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextUpdateProcessSlot; ++processIndex.i)
      {
        if (this.UpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.UpdateProcesses[coindexTo.i] = this.UpdateProcesses[processIndex.i];
            this.MoveTag(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextUpdateProcessSlot; ++processIndex.i)
      {
        this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.RemoveTag(processIndex);
      }
      this.NumberOfUpdateCoroutines = this._nextUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.FixedUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextFixedUpdateProcessSlot; ++processIndex.i)
      {
        if (this.FixedUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.FixedUpdateProcesses[coindexTo.i] = this.FixedUpdateProcesses[processIndex.i];
            this.MoveTag(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextFixedUpdateProcessSlot; ++processIndex.i)
      {
        this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.RemoveTag(processIndex);
      }
      this.NumberOfFixedUpdateCoroutines = this._nextFixedUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.LateUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextLateUpdateProcessSlot; ++processIndex.i)
      {
        if (this.LateUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.LateUpdateProcesses[coindexTo.i] = this.LateUpdateProcesses[processIndex.i];
            this.MoveTag(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextLateUpdateProcessSlot; ++processIndex.i)
      {
        this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.RemoveTag(processIndex);
      }
      this.NumberOfLateUpdateCoroutines = this._nextLateUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.SlowUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextServerUpdateProcessSlot; ++processIndex.i)
      {
        if (this.SlowUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.SlowUpdateProcesses[coindexTo.i] = this.SlowUpdateProcesses[processIndex.i];
            this.MoveTag(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextServerUpdateProcessSlot; ++processIndex.i)
      {
        this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.RemoveTag(processIndex);
      }
      this.NumberOfSlowUpdateCoroutines = this._nextServerUpdateProcessSlot = coindexTo.i;
    }

    private void AddTag(string tag, Timing.ProcessIndex coindex)
    {
      this._processTags.Add(coindex, tag);
      if (this._taggedProcesses.ContainsKey(tag))
        this._taggedProcesses[tag].Add(coindex);
      else
        this._taggedProcesses.Add(tag, new HashSet<Timing.ProcessIndex>()
        {
          coindex
        });
    }

    private string RemoveTag(Timing.ProcessIndex coindex)
    {
      if (!this._processTags.ContainsKey(coindex))
        return (string) null;
      string processTag = this._processTags[coindex];
      if (this._taggedProcesses[processTag].Count > 1)
        this._taggedProcesses[processTag].Remove(coindex);
      else
        this._taggedProcesses.Remove(processTag);
      this._processTags.Remove(coindex);
      return processTag;
    }

    private void MoveTag(Timing.ProcessIndex coindexFrom, Timing.ProcessIndex coindexTo)
    {
      this.RemoveTag(coindexTo);
      if (!this._processTags.ContainsKey(coindexFrom))
        return;
      this._taggedProcesses[this._processTags[coindexFrom]].Remove(coindexFrom);
      this._taggedProcesses[this._processTags[coindexFrom]].Add(coindexTo);
      this._processTags.Add(coindexTo, this._processTags[coindexFrom]);
      this._processTags.Remove(coindexFrom);
    }

    public static IEnumerator<float> RunCoroutine(IEnumerator<float> coroutine)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineOnInstance(coroutine, Segment.Update, (string) null) : (IEnumerator<float>) null;
    }

    public static IEnumerator<float> RunCoroutine(IEnumerator<float> coroutine, string tag)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineOnInstance(coroutine, Segment.Update, tag) : (IEnumerator<float>) null;
    }

    public static IEnumerator<float> RunCoroutine(IEnumerator<float> coroutine, Segment timing)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineOnInstance(coroutine, timing) : (IEnumerator<float>) null;
    }

    public static IEnumerator<float> RunCoroutine(
      IEnumerator<float> coroutine,
      Segment timing,
      string tag)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineOnInstance(coroutine, timing, tag) : (IEnumerator<float>) null;
    }

    public IEnumerator<float> RunCoroutineOnInstance(IEnumerator<float> coroutine)
    {
      return coroutine != null ? this.RunCoroutineOnInstance(coroutine, Segment.Update, (string) null) : (IEnumerator<float>) null;
    }

    public IEnumerator<float> RunCoroutineOnInstance(IEnumerator<float> coroutine, string tag)
    {
      return coroutine != null ? this.RunCoroutineOnInstance(coroutine, Segment.Update, tag) : (IEnumerator<float>) null;
    }

    public IEnumerator<float> RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment timing)
    {
      return coroutine != null ? this.RunCoroutineOnInstance(coroutine, timing, (string) null) : (IEnumerator<float>) null;
    }

    public IEnumerator<float> RunCoroutineOnInstance(
      IEnumerator<float> coroutine,
      Segment timing,
      string tag)
    {
      if (coroutine == null)
        return (IEnumerator<float>) null;
      Timing.ProcessIndex processIndex = new Timing.ProcessIndex()
      {
        seg = timing
      };
      switch (timing)
      {
        case Segment.Update:
          if (this._nextUpdateProcessSlot >= this.UpdateProcesses.Length)
          {
            IEnumerator<float>[] updateProcesses = this.UpdateProcesses;
            this.UpdateProcesses = new IEnumerator<float>[this.UpdateProcesses.Length + 64 * (int) this._expansions++];
            for (int index = 0; index < updateProcesses.Length; ++index)
              this.UpdateProcesses[index] = updateProcesses[index];
          }
          processIndex.i = this._nextUpdateProcessSlot++;
          this.UpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (!this._runningUpdate)
          {
            try
            {
              this._runningUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.UpdateProcesses[processIndex.i].MoveNext())
                this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.UpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.UpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.UpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.UpdateProcesses[processIndex.i], timing, this._processTags.ContainsKey(processIndex) ? this._processTags[processIndex] : (string) null);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                    if (this.UpdateProcesses[processIndex.i] != null)
                      this.UpdateProcesses[processIndex.i].MoveNext();
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex);
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningUpdate = false;
            }
          }
          return coroutine;
        case Segment.FixedUpdate:
          if (this._nextFixedUpdateProcessSlot >= this.FixedUpdateProcesses.Length)
          {
            IEnumerator<float>[] fixedUpdateProcesses = this.FixedUpdateProcesses;
            this.FixedUpdateProcesses = new IEnumerator<float>[this.FixedUpdateProcesses.Length + 64 * (int) this._expansions++];
            for (int index = 0; index < fixedUpdateProcesses.Length; ++index)
              this.FixedUpdateProcesses[index] = fixedUpdateProcesses[index];
          }
          processIndex.i = this._nextFixedUpdateProcessSlot++;
          this.FixedUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (!this._runningFixedUpdate)
          {
            try
            {
              this._runningFixedUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.FixedUpdateProcesses[processIndex.i].MoveNext())
                this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.FixedUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.FixedUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.FixedUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.FixedUpdateProcesses[processIndex.i], timing, this._processTags.ContainsKey(processIndex) ? this._processTags[processIndex] : (string) null);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                    if (this.FixedUpdateProcesses[processIndex.i] != null)
                      this.FixedUpdateProcesses[processIndex.i].MoveNext();
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex);
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningFixedUpdate = false;
            }
          }
          return coroutine;
        case Segment.LateUpdate:
          if (this._nextLateUpdateProcessSlot >= this.LateUpdateProcesses.Length)
          {
            IEnumerator<float>[] lateUpdateProcesses = this.LateUpdateProcesses;
            this.LateUpdateProcesses = new IEnumerator<float>[this.LateUpdateProcesses.Length + 64 * (int) this._expansions++];
            for (int index = 0; index < lateUpdateProcesses.Length; ++index)
              this.LateUpdateProcesses[index] = lateUpdateProcesses[index];
          }
          processIndex.i = this._nextLateUpdateProcessSlot++;
          this.LateUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (!this._runningLateUpdate)
          {
            try
            {
              this._runningLateUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.LateUpdateProcesses[processIndex.i].MoveNext())
                this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.LateUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.LateUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.LateUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.LateUpdateProcesses[processIndex.i], timing, this._processTags.ContainsKey(processIndex) ? this._processTags[processIndex] : (string) null);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                    if (this.LateUpdateProcesses[processIndex.i] != null)
                      this.LateUpdateProcesses[processIndex.i].MoveNext();
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex);
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningLateUpdate = false;
            }
          }
          return coroutine;
        case Segment.SlowUpdate:
          if (this._nextServerUpdateProcessSlot >= this.SlowUpdateProcesses.Length)
          {
            IEnumerator<float>[] slowUpdateProcesses = this.SlowUpdateProcesses;
            this.SlowUpdateProcesses = new IEnumerator<float>[this.SlowUpdateProcesses.Length + 64 * (int) this._expansions++];
            for (int index = 0; index < slowUpdateProcesses.Length; ++index)
              this.SlowUpdateProcesses[index] = slowUpdateProcesses[index];
          }
          processIndex.i = this._nextServerUpdateProcessSlot++;
          this.SlowUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (!this._runningSlowUpdate)
          {
            try
            {
              this._runningSlowUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.SlowUpdateProcesses[processIndex.i].MoveNext())
                this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.SlowUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.SlowUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.SlowUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.SlowUpdateProcesses[processIndex.i], timing, this._processTags.ContainsKey(processIndex) ? this._processTags[processIndex] : (string) null);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) null;
                    if (this.SlowUpdateProcesses[processIndex.i] != null)
                      this.SlowUpdateProcesses[processIndex.i].MoveNext();
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ex);
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningSlowUpdate = false;
            }
          }
          return coroutine;
        default:
          return (IEnumerator<float>) null;
      }
    }

    private void CoindexKill(Timing.ProcessIndex coindex)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          this.UpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          break;
        case Segment.FixedUpdate:
          this.FixedUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          break;
        case Segment.LateUpdate:
          this.LateUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          break;
        case Segment.SlowUpdate:
          this.SlowUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          break;
      }
    }

    private bool CoindexMatches(Timing.ProcessIndex coindex, IEnumerator<float> handle)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          return this.UpdateProcesses[coindex.i] == handle;
        case Segment.FixedUpdate:
          return this.FixedUpdateProcesses[coindex.i] == handle;
        case Segment.LateUpdate:
          return this.LateUpdateProcesses[coindex.i] == handle;
        case Segment.SlowUpdate:
          return this.SlowUpdateProcesses[coindex.i] == handle;
        default:
          return false;
      }
    }

    public static void KillAllCoroutines()
    {
      if (!((UnityEngine.Object) Timing._instance != (UnityEngine.Object) null))
        return;
      Timing._instance.KillAllCoroutinesOnInstance();
    }

    public void KillAllCoroutinesOnInstance()
    {
      this.UpdateProcesses = new IEnumerator<float>[256];
      this.NumberOfUpdateCoroutines = 0;
      this._nextUpdateProcessSlot = 0;
      this.LateUpdateProcesses = new IEnumerator<float>[8];
      this.NumberOfLateUpdateCoroutines = 0;
      this._nextLateUpdateProcessSlot = 0;
      this.FixedUpdateProcesses = new IEnumerator<float>[64];
      this.NumberOfFixedUpdateCoroutines = 0;
      this._nextFixedUpdateProcessSlot = 0;
      this.SlowUpdateProcesses = new IEnumerator<float>[64];
      this.NumberOfSlowUpdateCoroutines = 0;
      this._nextServerUpdateProcessSlot = 0;
      this._processTags.Clear();
      this._taggedProcesses.Clear();
      this._waitingProcesses.Clear();
      this._exceptions.Clear();
      this._expansions = (ushort) 1;
      this.ResetTimeCountOnInstance();
    }

    public static int KillCoroutines(IEnumerator<float> coroutine)
    {
      return !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillCoroutinesOnInstance(coroutine) : 0;
    }

    public int KillCoroutinesOnInstance(IEnumerator<float> coroutine)
    {
      int num = 0;
      for (int index = 0; index < this._nextUpdateProcessSlot; ++index)
      {
        if (this.UpdateProcesses[index] == coroutine)
        {
          this.UpdateProcesses[index] = (IEnumerator<float>) null;
          ++num;
        }
      }
      for (int index = 0; index < this._nextFixedUpdateProcessSlot; ++index)
      {
        if (this.FixedUpdateProcesses[index] == coroutine)
        {
          this.FixedUpdateProcesses[index] = (IEnumerator<float>) null;
          ++num;
        }
      }
      for (int index = 0; index < this._nextLateUpdateProcessSlot; ++index)
      {
        if (this.LateUpdateProcesses[index] == coroutine)
        {
          this.LateUpdateProcesses[index] = (IEnumerator<float>) null;
          ++num;
        }
      }
      for (int index = 0; index < this._nextServerUpdateProcessSlot; ++index)
      {
        if (this.SlowUpdateProcesses[index] == coroutine)
        {
          this.SlowUpdateProcesses[index] = (IEnumerator<float>) null;
          ++num;
        }
      }
      for (int index1 = 0; index1 < this._waitingProcesses.Count; ++index1)
      {
        if (this._waitingProcesses[index1].Trigger == coroutine && !this._waitingProcesses[index1].Killed && !this._waitingProcesses[index1].Killed)
        {
          this._waitingProcesses[index1].Killed = true;
          ++num;
        }
        for (int index2 = 0; index2 < this._waitingProcesses[index1].Tasks.Count; ++index2)
        {
          if (this._waitingProcesses[index1].Tasks[index2].Task == coroutine && this._waitingProcesses[index1].Tasks[index2].Task != null)
          {
            this._waitingProcesses[index1].Tasks[index2].Task = (IEnumerator<float>) null;
            ++num;
          }
        }
      }
      return num;
    }

    public static int KillCoroutines(string tag)
    {
      return !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillCoroutinesOnInstance(tag) : 0;
    }

    public int KillCoroutinesOnInstance(string tag)
    {
      int num = 0;
      if (this._taggedProcesses.ContainsKey(tag))
      {
        foreach (Timing.ProcessIndex processIndex in this._taggedProcesses[tag])
        {
          this.CoindexKill(processIndex);
          this._processTags.Remove(processIndex);
          ++num;
        }
        this._taggedProcesses.Remove(tag);
      }
      for (int index1 = 0; index1 < this._waitingProcesses.Count; ++index1)
      {
        if (this._waitingProcesses[index1].TriggerTag == tag && !this._waitingProcesses[index1].Killed && !this._waitingProcesses[index1].Killed)
        {
          this._waitingProcesses[index1].Killed = true;
          ++num;
        }
        for (int index2 = 0; index2 < this._waitingProcesses[index1].Tasks.Count; ++index2)
        {
          if (this._waitingProcesses[index1].Tasks[index2].Tag == tag && this._waitingProcesses[index1].Tasks[index2].Task != null)
          {
            this._waitingProcesses[index1].Tasks[index2].Task = (IEnumerator<float>) null;
            ++num;
          }
        }
      }
      return num;
    }

    public static int KillAllCoroutines(IEnumerator<float> coroutine, string tag)
    {
      return !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillAllCoroutinesOnInstance(coroutine, tag) : 0;
    }

    public int KillAllCoroutinesOnInstance(IEnumerator<float> coroutine, string tag)
    {
      int num = 0;
      if (this._taggedProcesses.ContainsKey(tag))
      {
        foreach (Timing.ProcessIndex processIndex in this._taggedProcesses[tag])
        {
          if (this.CoindexMatches(processIndex, coroutine))
          {
            this.CoindexKill(processIndex);
            this._processTags.Remove(processIndex);
            ++num;
          }
        }
        if (num == this._taggedProcesses[tag].Count)
          this._taggedProcesses.Remove(tag);
      }
      for (int index1 = 0; index1 < this._waitingProcesses.Count; ++index1)
      {
        if (this._waitingProcesses[index1].Trigger == coroutine && this._waitingProcesses[index1].TriggerTag == tag && !this._waitingProcesses[index1].Killed && !this._waitingProcesses[index1].Killed)
        {
          this._waitingProcesses[index1].Killed = true;
          ++num;
        }
        for (int index2 = 0; index2 < this._waitingProcesses[index1].Tasks.Count; ++index2)
        {
          if (this._waitingProcesses[index1].Tasks[index2].Task == coroutine && this._waitingProcesses[index1].Tasks[index2].Tag == tag && this._waitingProcesses[index1].Tasks[index2].Task != null)
          {
            this._waitingProcesses[index1].Tasks[index2].Task = (IEnumerator<float>) null;
            ++num;
          }
        }
      }
      return num;
    }

    public static float WaitUntilDone(IEnumerator<float> otherCoroutine)
    {
      return Timing.WaitUntilDone(otherCoroutine, true, Timing.Instance);
    }

    public static float WaitUntilDone(IEnumerator<float> otherCoroutine, bool warnOnIssue)
    {
      return Timing.WaitUntilDone(otherCoroutine, warnOnIssue, Timing.Instance);
    }

    public static float WaitUntilDone(
      IEnumerator<float> otherCoroutine,
      bool warnOnIssue,
      Timing instance)
    {
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || !(bool) (UnityEngine.Object) instance.gameObject)
        throw new ArgumentNullException();
      if (otherCoroutine == null)
      {
        if (warnOnIssue)
          throw new ArgumentNullException();
        return -1f;
      }
      for (int index1 = 0; index1 < instance._waitingProcesses.Count; ++index1)
      {
        if (instance._waitingProcesses[index1].Trigger == otherCoroutine)
        {
          Timing.WaitingProcess proc = instance._waitingProcesses[index1];
          Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((input, segment, tag) =>
          {
            proc.Tasks.Add(new Timing.WaitingProcess.ProcessData()
            {
              Task = input,
              Tag = tag,
              Segment = segment
            });
            return (IEnumerator<float>) null;
          });
          return float.NaN;
        }
        for (int index2 = 0; index2 < instance._waitingProcesses[index1].Tasks.Count; ++index2)
        {
          if (instance._waitingProcesses[index1].Tasks[index2].Task == otherCoroutine)
          {
            Timing.WaitingProcess proc = new Timing.WaitingProcess()
            {
              Trigger = otherCoroutine
            };
            instance._waitingProcesses[index1].Tasks[index2].Task = instance._StartWhenDone(proc);
            Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((input, segment, tag) =>
            {
              proc.Tasks.Add(new Timing.WaitingProcess.ProcessData()
              {
                Task = input,
                Tag = tag,
                Segment = segment
              });
              instance._waitingProcesses.Add(proc);
              return (IEnumerator<float>) null;
            });
            return float.NaN;
          }
        }
      }
      Timing.WaitingProcess newProcess = new Timing.WaitingProcess()
      {
        Trigger = otherCoroutine
      };
      if (instance.ReplaceCoroutine(otherCoroutine, instance._StartWhenDone(newProcess), out newProcess.TriggerTag))
      {
        Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((input, segment, tag) =>
        {
          newProcess.Tasks.Add(new Timing.WaitingProcess.ProcessData()
          {
            Task = input,
            Tag = tag,
            Segment = segment
          });
          instance._waitingProcesses.Add(newProcess);
          return (IEnumerator<float>) null;
        });
        return float.NaN;
      }
      if (warnOnIssue)
        Debug.LogWarning((object) ("WaitUntilDone cannot hold: The coroutine instance that was passed in was not found.\n" + (object) otherCoroutine));
      return -1f;
    }

    private IEnumerator<float> _StartWhenDone(Timing.WaitingProcess processData)
    {
      bool flag;
      try
      {
        if (processData.Killed)
        {
          this.CloseWaitingProcess(processData);
          flag = false;
        }
        else
        {
          if ((double) processData.Trigger.Current > this.localTime)
          {
            yield return processData.Trigger.Current;
            if (processData.Killed)
            {
              this.CloseWaitingProcess(processData);
              flag = false;
              goto label_9;
            }
          }
          while (processData.Trigger.MoveNext())
          {
            yield return processData.Trigger.Current;
            if (processData.Killed)
            {
              this.CloseWaitingProcess(processData);
              flag = false;
              goto label_9;
            }
          }
          yield break;
        }
label_9:;
      }
      finally
      {
        this.CloseWaitingProcess(processData);
      }
      return flag;
    }

    private void CloseWaitingProcess(Timing.WaitingProcess processData)
    {
      if (!this._waitingProcesses.Contains(processData))
        return;
      this._waitingProcesses.Remove(processData);
      foreach (Timing.WaitingProcess.ProcessData task in processData.Tasks)
        this.RunCoroutineOnInstance(task.Task, task.Segment, task.Tag);
    }

    private bool ReplaceCoroutine(
      IEnumerator<float> coroutine,
      IEnumerator<float> replacement,
      out string tagFound)
    {
      Timing.ProcessIndex coindex;
      for (coindex.i = 0; coindex.i < this._nextUpdateProcessSlot; ++coindex.i)
      {
        if (this.UpdateProcesses[coindex.i] == coroutine)
        {
          coindex.seg = Segment.Update;
          this.UpdateProcesses[coindex.i] = replacement;
          tagFound = this.RemoveTag(coindex);
          return true;
        }
      }
      for (coindex.i = 0; coindex.i < this._nextFixedUpdateProcessSlot; ++coindex.i)
      {
        if (this.FixedUpdateProcesses[coindex.i] == coroutine)
        {
          coindex.seg = Segment.FixedUpdate;
          this.FixedUpdateProcesses[coindex.i] = replacement;
          tagFound = this.RemoveTag(coindex);
          return true;
        }
      }
      for (coindex.i = 0; coindex.i < this._nextLateUpdateProcessSlot; ++coindex.i)
      {
        if (this.LateUpdateProcesses[coindex.i] == coroutine)
        {
          coindex.seg = Segment.LateUpdate;
          this.LateUpdateProcesses[coindex.i] = replacement;
          tagFound = this.RemoveTag(coindex);
          return true;
        }
      }
      for (coindex.i = 0; coindex.i < this._nextServerUpdateProcessSlot; ++coindex.i)
      {
        if (this.SlowUpdateProcesses[coindex.i] == coroutine)
        {
          coindex.seg = Segment.SlowUpdate;
          this.SlowUpdateProcesses[coindex.i] = replacement;
          tagFound = this.RemoveTag(coindex);
          return true;
        }
      }
      tagFound = (string) null;
      return false;
    }

    public static float WaitUntilDone(UnityWebRequest wwwObject)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((input, timing, tag) => Timing._StartWhenDone(wwwObject, input));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      UnityWebRequest www,
      IEnumerator<float> pausedProc)
    {
      while (!www.isDone)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((_param1, _param2, _param3) => pausedProc);
      yield return float.NaN;
    }

    public static float WaitUntilDone(AsyncOperation operation)
    {
      if (operation == null || operation.isDone)
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((input, timing, tag) => Timing._StartWhenDone(operation, input));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      AsyncOperation operation,
      IEnumerator<float> pausedProc)
    {
      while (!operation.isDone)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((_param1, _param2, _param3) => pausedProc);
      yield return float.NaN;
    }

    public static float WaitUntilDone(CustomYieldInstruction operation)
    {
      if (operation == null || !operation.keepWaiting)
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((input, timing, tag) => Timing._StartWhenDone(operation, input));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      CustomYieldInstruction operation,
      IEnumerator<float> pausedProc)
    {
      while (operation.keepWaiting)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, Segment, string, IEnumerator<float>>) ((_param1, _param2, _param3) => pausedProc);
      yield return float.NaN;
    }

    public static float WaitForSeconds(float waitTime)
    {
      if (float.IsNaN(waitTime))
        waitTime = 0.0f;
      return Timing.LocalTime + waitTime;
    }

    public float WaitForSecondsOnInstance(float waitTime)
    {
      if (float.IsNaN(waitTime))
        waitTime = 0.0f;
      return (float) this.localTime + waitTime;
    }

    public static void CallDelayed<TRef>(TRef reference, float delay, Action<TRef> action)
    {
      if (action == null)
        return;
      if ((double) delay >= -1.0 / 1000.0)
        Timing.RunCoroutine(Timing.Instance._CallDelayBack<TRef>(reference, delay, action));
      else
        action(reference);
    }

    public void CallDelayedOnInstance<TRef>(TRef reference, float delay, Action<TRef> action)
    {
      if (action == null)
        return;
      if ((double) delay >= -1.0 / 1000.0)
        this.RunCoroutineOnInstance(this._CallDelayBack<TRef>(reference, delay, action));
      else
        action(reference);
    }

    private IEnumerator<float> _CallDelayBack<TRef>(
      TRef reference,
      float delay,
      Action<TRef> action)
    {
      yield return (float) this.localTime + delay;
      Timing.CallDelayed<TRef>(reference, -1f, action);
    }

    public static void CallDelayed(float delay, Action action)
    {
      if (action == null)
        return;
      if ((double) delay >= -9.9999997473787516E-05)
        Timing.RunCoroutine(Timing.Instance._CallDelayBack(delay, action));
      else
        action();
    }

    public void CallDelayedOnInstance(float delay, Action action)
    {
      if (action == null)
        return;
      if ((double) delay >= -9.9999997473787516E-05)
        this.RunCoroutineOnInstance(this._CallDelayBack(delay, action));
      else
        action();
    }

    private IEnumerator<float> _CallDelayBack(float delay, Action action)
    {
      yield return (float) this.localTime + delay;
      Timing.CallDelayed(-1f, action);
    }

    public static void CallPeriodically(
      float timeframe,
      float period,
      Action action,
      Action onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, period, action, onDone), Segment.Update);
    }

    public void CallPeriodicallyOnInstance(
      float timeframe,
      float period,
      Action action,
      Action onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously(timeframe, period, action, onDone), Segment.Update);
    }

    public static void CallPeriodically(
      float timeframe,
      float period,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, period, action, onDone), timing);
    }

    public void CallPeriodicallyOnInstance(
      float timeframe,
      float period,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously(timeframe, period, action, onDone), timing);
    }

    public static void CallContinuously(float timeframe, Action action, Action onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, 0.0f, action, onDone), Segment.Update);
    }

    public void CallContinuouslyOnInstance(float timeframe, Action action, Action onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously(timeframe, 0.0f, action, onDone), Segment.Update);
    }

    public static void CallContinuously(
      float timeframe,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, 0.0f, action, onDone), timing);
    }

    public void CallContinuouslyOnInstance(
      float timeframe,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously(timeframe, 0.0f, action, onDone), timing);
    }

    private IEnumerator<float> _CallContinuously(
      float timeframe,
      float period,
      Action action,
      Action onDone)
    {
      double startTime = this.localTime;
      while (this.localTime <= startTime + (double) timeframe)
      {
        yield return Timing.WaitForSeconds(period);
        action();
      }
      if (onDone != null)
        onDone();
    }

    public static void CallPeriodically<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, period, action, onDone), Segment.Update);
    }

    public void CallPeriodicallyOnInstance<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, period, action, onDone), Segment.Update);
    }

    public static void CallPeriodically<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, period, action, onDone), timing);
    }

    public void CallPeriodicallyOnInstance<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, period, action, onDone), timing);
    }

    public static void CallContinuously<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, 0.0f, timeframe, action, onDone), Segment.Update);
    }

    public void CallContinuouslyOnInstance<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, 0.0f, timeframe, action, onDone), Segment.Update);
    }

    public static void CallContinuously<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, 0.0f, action, onDone), timing);
    }

    public void CallContinuouslyOnInstance<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      if (action == null)
        return;
      this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, 0.0f, action, onDone), timing);
    }

    private IEnumerator<float> _CallContinuously<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Action<T> onDone = null)
    {
      double startTime = this.localTime;
      while (this.localTime <= startTime + (double) timeframe)
      {
        yield return Timing.WaitForSeconds(period);
        action(reference);
      }
      if (onDone != null)
        onDone(reference);
    }

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public Coroutine StartCoroutine(IEnumerator routine) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public new Coroutine StartCoroutine(string methodName, object value) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public new Coroutine StartCoroutine(string methodName) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public Coroutine StartCoroutine_Auto(IEnumerator routine) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use KillCoroutine instead.", true)]
    public new void StopCoroutine(string methodName)
    {
    }

    [Obsolete("Unity coroutine function, use KillCoroutine instead.", true)]
    public void StopCoroutine(IEnumerator routine)
    {
    }

    [Obsolete("Unity coroutine function, use KillCoroutine instead.", true)]
    public new void StopCoroutine(Coroutine routine)
    {
    }

    [Obsolete("Unity coroutine function, use KillAllCoroutines instead.", true)]
    public new void StopAllCoroutines()
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void Destroy(UnityEngine.Object obj)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void Destroy(UnityEngine.Object obj, float f)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyObject(UnityEngine.Object obj)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyObject(UnityEngine.Object obj, float f)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyImmediate(UnityEngine.Object obj)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyImmediate(UnityEngine.Object obj, bool b)
    {
    }

    [Obsolete("Just.. no.", true)]
    public new static T FindObjectOfType<T>() where T : UnityEngine.Object => default (T);

    [Obsolete("Just.. no.", true)]
    public static UnityEngine.Object FindObjectOfType(System.Type t) => (UnityEngine.Object) null;

    [Obsolete("Just.. no.", true)]
    public new static T[] FindObjectsOfType<T>() where T : UnityEngine.Object => (T[]) null;

    [Obsolete("Just.. no.", true)]
    public static UnityEngine.Object[] FindObjectsOfType(System.Type t) => (UnityEngine.Object[]) null;

    [Obsolete("Just.. no.", true)]
    public new static void print(object message)
    {
    }

    private class WaitingProcess
    {
      public IEnumerator<float> Trigger;
      public string TriggerTag;
      public bool Killed;
      public readonly List<Timing.WaitingProcess.ProcessData> Tasks = new List<Timing.WaitingProcess.ProcessData>();

      public class ProcessData
      {
        public IEnumerator<float> Task;
        public string Tag;
        public Segment Segment;
      }
    }

    private struct ProcessIndex : IEquatable<Timing.ProcessIndex>
    {
      public Segment seg;
      public int i;

      public bool Equals(Timing.ProcessIndex other) => this.seg == other.seg && this.i == other.i;

      public override bool Equals(object other)
      {
        return other is Timing.ProcessIndex other1 && this.Equals(other1);
      }

      public static bool operator ==(Timing.ProcessIndex a, Timing.ProcessIndex b)
      {
        return a.seg == b.seg && a.i == b.i;
      }

      public static bool operator !=(Timing.ProcessIndex a, Timing.ProcessIndex b)
      {
        return a.seg != b.seg || a.i != b.i;
      }

      public override int GetHashCode() => (int) (this.seg - 2) * 715827882 + this.i;
    }
  }
}
