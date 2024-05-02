// Decompiled with JetBrains decompiler
// Type: RecycledScrollViewGeneric
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class RecycledScrollViewGeneric : MonoBehaviour
{
  public GameObject pfabChat;
  public RectTransform chatContainer;
  public RectTransform chatContainerScroller;
  public ScrollRect _chatScrollbar;
  public Scrollbar _bar;
  public float itemSize = 28f;
  public int MaxVisible = 29;
  public int MaxSize = 200;
  private bool notmaxed = true;
  private List<IRecycledScrollViewGenericItem> pfabs = new List<IRecycledScrollViewGenericItem>();
  private List<object> list = new List<object>();
  public Action<IRecycledScrollViewGenericItem> onScrolled;
  private int firstVisible;
  private int nextindex;
  private int lastRender = -1;

  public List<IRecycledScrollViewGenericItem> GetPfabs => this.pfabs;

  private void Awake() => this._bar.onValueChanged.AddListener(new UnityAction<float>(this.Scroll));

  public int GetIndex(int i)
  {
    if (this.notmaxed)
      return i;
    i = this.firstIndex() + i;
    if (i >= this.list.Count)
      i -= this.list.Count;
    return i;
  }

  public void Scroll(float v)
  {
    if (this.pfabs.Count < this.MaxVisible)
      return;
    this.firstVisible = (int) Mathf.Clamp((float) (this.list.Count - this.MaxVisible + 1) * (1f - v), 0.0f, (float) (this.list.Count - 1));
    if (this.firstVisible + this.MaxVisible >= this.list.Count)
    {
      this.firstVisible = this.list.Count - this.MaxVisible;
      if (this.firstVisible < 0)
        this.firstVisible = 0;
    }
    this.firstVisible = this.GetIndex(this.firstVisible);
    this.Render();
  }

  private int lastIndex() => this.nextindex == 0 ? this.list.Count - 1 : this.nextindex - 1;

  private int firstIndex()
  {
    return this.notmaxed || this.nextindex == this.list.Count - 1 ? 0 : this.nextindex;
  }

  private void Render()
  {
    if (this.lastRender == this.firstVisible)
      return;
    this.lastRender = this.firstVisible;
    int num = this.firstVisible - this.firstIndex();
    int index1 = this.firstVisible;
    for (int index2 = 0; index2 < this.pfabs.Count; ++index2)
    {
      if (this.list.Count > index1)
      {
        this.pfabs[index2].Init(this.list[index1], num + index2);
        if (!this.pfabs[index2].gameObject.activeSelf)
          this.pfabs[index2].gameObject.SetActive(true);
        ++index1;
      }
      if (index1 >= this.list.Count)
      {
        if (this.list.Count < this.pfabs.Count)
        {
          for (int index3 = index1; index3 < this.pfabs.Count; ++index3)
            this.pfabs[index3].gameObject.SetActive(false);
          return;
        }
        index1 = 0;
      }
    }
    if (this.onScrolled == null || this.pfabs.Count <= 0)
      return;
    this.onScrolled(this.pfabs[0]);
  }

  public void ForceRender()
  {
    int num = this.firstVisible - this.firstIndex();
    int index1 = this.firstVisible;
    for (int index2 = 0; index2 < this.pfabs.Count; ++index2)
    {
      this.pfabs[index2].Init(this.list[index1], num + index2);
      ++index1;
      if (index1 >= this.list.Count)
        index1 = 0;
    }
  }

  public void Clear()
  {
    this.list.Clear();
    this.notmaxed = true;
    this.nextindex = 0;
    this.firstVisible = 0;
    this.lastRender = -1;
    for (int index = 0; index < this.pfabs.Count; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.pfabs[index].gameObject);
    this.pfabs.Clear();
  }

  public void Set(IEnumerable<object> data)
  {
    int count = this.list.Count;
    int index1 = 0;
    foreach (object obj in data)
    {
      if (this.list.Count <= index1)
        this.list.Add(obj);
      else
        this.list[index1] = obj;
      ++index1;
    }
    for (int index2 = count - 1; index2 >= index1; --index2)
      this.list.RemoveAt(index2);
    this.MaxSize = Mathf.Max(this.MaxVisible, this.list.Count);
    this.chatContainerScroller.sizeDelta = new Vector2(this.chatContainerScroller.sizeDelta.x, (float) this.MaxSize * this.itemSize);
    this._chatScrollbar.verticalNormalizedPosition = this._chatScrollbar.verticalNormalizedPosition;
    if (this.firstVisible >= this.list.Count - this.MaxVisible)
    {
      this.firstVisible = this.list.Count - 1 - this.MaxVisible;
      if (this.firstVisible < 0)
        this.firstVisible = 0;
    }
    while (this.pfabs.Count < this.MaxVisible && this.pfabs.Count < this.list.Count)
      this._createPfab((object) null);
    this.lastRender = -1;
    this.Render();
  }

  private void _createPfab(object msg)
  {
    GameObject andApply = Controller.Instance.CreateAndApply(this.pfabChat, (Transform) this.chatContainer);
    IRecycledScrollViewGenericItem component = andApply.GetComponent<IRecycledScrollViewGenericItem>();
    ((RectTransform) andApply.transform).anchoredPosition = new Vector2(0.0f, (float) this.pfabs.Count * -this.itemSize);
    this.pfabs.Add(component);
    if (msg != null)
      component.Init(msg, this.pfabs.Count - 1);
    andApply.SetActive(true);
  }

  public void Add(object msg)
  {
    bool flag = true;
    if (this.list.Count > 11 && (double) this._chatScrollbar.verticalNormalizedPosition > 1.0 / (double) this.list.Count * 3.0)
      flag = false;
    if (this.list.Count < this.MaxSize)
    {
      this.list.Add(msg);
    }
    else
    {
      this.notmaxed = false;
      this.list[this.nextindex] = msg;
      ++this.nextindex;
      if (this.nextindex >= this.list.Count)
        this.nextindex = 0;
    }
    if (this.pfabs.Count >= this.MaxVisible)
    {
      if (flag)
      {
        ++this.firstVisible;
        if (this.firstVisible >= this.list.Count)
          this.firstVisible = 0;
      }
      else if (this.firstVisible != this.firstIndex() - 1)
      {
        if (!this.notmaxed)
          this._bar.value += 1f / (float) (this.MaxSize - this.MaxVisible);
      }
      else
      {
        ++this.firstVisible;
        if (this.firstVisible >= this.list.Count)
          this.firstVisible = 0;
        this.Render();
      }
    }
    else
      this._createPfab(msg);
    if (this.list.Count <= this.MaxSize)
      this.chatContainerScroller.sizeDelta = new Vector2(this.chatContainerScroller.sizeDelta.x, (float) this.list.Count * this.itemSize);
    if (flag)
    {
      this._chatScrollbar.verticalNormalizedPosition = 0.0f;
      this.Render();
    }
    if (!((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null) || !((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null) || ChatBox.Instance.Active)
      return;
    switch (ChatBox.showFade)
    {
      case 0:
        HUD.instance.repeatChatMsg.enabled = true;
        break;
      case 2:
        if (!HUD.YourTurn)
          break;
        goto case 0;
    }
  }
}
