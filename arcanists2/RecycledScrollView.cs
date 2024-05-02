// Decompiled with JetBrains decompiler
// Type: RecycledScrollView
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class RecycledScrollView : MonoBehaviour
{
  public GameObject pfabChat;
  public RectTransform chatContainer;
  public RectTransform chatContainerScroller;
  public ScrollRect _chatScrollbar;
  public Scrollbar _bar;
  public int MaxVisible = 8;
  public const int MaxSize = 200;
  internal int _id;
  private bool notmaxed = true;
  private List<PfabChatMsg> pfabs = new List<PfabChatMsg>();
  private List<PfabChatMsg.contain> list = new List<PfabChatMsg.contain>();
  private int firstVisible;
  private int nextindex;
  private int lastRender = -1;

  public List<PfabChatMsg.contain> GetList => this.list;

  private int GetLastVisible(int firstVisible)
  {
    int lastVisible = firstVisible;
    for (int index = 0; index < this.pfabs.Count; ++index)
    {
      ++lastVisible;
      if (lastVisible >= this.list.Count)
        lastVisible = 0;
    }
    return lastVisible;
  }

  private bool downPassesFirst(int firstVisible)
  {
    int num = firstVisible;
    for (int index = 0; index < this.pfabs.Count; ++index)
    {
      if (num == this.nextindex)
        return true;
      ++num;
      if (num >= this.list.Count)
        num = 0;
    }
    return false;
  }

  private bool upPassesLastIndex(int firstVisible) => firstVisible == this.lastIndex();

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

  public void ScrollDown()
  {
    int num = this.firstVisible - this.firstIndex();
    if (num < 0)
      num += this.list.Count;
    int index = this.GetIndex(num + 1);
    if (this.firstVisible == index || index < 0 || this.downPassesFirst(index))
      return;
    this.firstVisible = index;
    this._bar.SetValueWithoutNotify(Mathf.Clamp((float) (1.0 - (double) num / (double) (this.list.Count - this.MaxVisible + 1)), 0.0f, 1f));
    this.Render();
  }

  public void ScrolUp()
  {
    int num = this.firstVisible - this.firstIndex();
    if (num < 0)
      num += this.list.Count;
    int index = this.GetIndex(num - 1);
    if (this.firstVisible == index || index < 0 || this.upPassesLastIndex(index))
      return;
    this.firstVisible = index;
    this._bar.SetValueWithoutNotify(Mathf.Clamp((float) (1.0 - (double) num / (double) (this.list.Count - this.MaxVisible + 1)), 0.0f, 1f));
    this.Render();
  }

  public void Scroll(float v)
  {
    v = Mathf.Clamp(v, 0.0f, 1f);
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

  public void ForceRender()
  {
    int index1 = this.firstVisible + this.pfabs.Count - 1;
    if (index1 > this.list.Count)
      index1 -= this.list.Count;
    if (index1 > this.firstVisible && index1 < this.list.Count && this.list[this.firstVisible]._id > this.list[index1]._id)
    {
      --this.firstVisible;
      if (this.firstVisible < 0)
        this.firstVisible = this.list.Count - 1;
    }
    this.lastRender = this.firstVisible;
    int index2 = this.firstVisible;
    for (int index3 = 0; index3 < this.pfabs.Count; ++index3)
    {
      this.pfabs[index3].FromContain(this.list[index2]);
      ++index2;
      if (index2 >= this.list.Count)
        index2 = 0;
    }
  }

  private void Render()
  {
    if (this.lastRender == this.firstVisible)
      return;
    int index1 = this.firstVisible + this.pfabs.Count - 1;
    if (index1 > this.list.Count)
      index1 -= this.list.Count;
    if (index1 > this.firstVisible && index1 < this.list.Count && this.list[this.firstVisible]._id > this.list[index1]._id)
    {
      --this.firstVisible;
      if (this.firstVisible < 0)
        this.firstVisible = this.list.Count - 1;
    }
    this.lastRender = this.firstVisible;
    int index2 = this.firstVisible;
    for (int index3 = 0; index3 < this.pfabs.Count; ++index3)
    {
      this.pfabs[index3].FromContain(this.list[index2]);
      ++index2;
      if (index2 >= this.list.Count)
        index2 = 0;
    }
  }

  public void Add(PfabChatMsg.contain contain)
  {
    contain._id = ++this._id;
    bool flag = true;
    if (this.list.Count > 11 && (double) this._chatScrollbar.verticalNormalizedPosition > 1.0 / (double) this.list.Count * 3.0)
      flag = false;
    if (this.list.Count < 200)
    {
      this.list.Add(contain);
    }
    else
    {
      this.notmaxed = false;
      this.list[this.nextindex] = contain;
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
          this._bar.value += 1f / (float) (200 - this.MaxVisible);
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
    {
      GameObject andApply = Controller.Instance.CreateAndApply(this.pfabChat, (Transform) this.chatContainer);
      PfabChatMsg component = andApply.GetComponent<PfabChatMsg>();
      ((RectTransform) andApply.transform).anchoredPosition = new Vector2(0.0f, (float) ((this.chatContainer.childCount - 1) * -24));
      this.pfabs.Add(component);
      component.FromContain(contain);
    }
    if (this.list.Count <= 200)
      this.chatContainerScroller.sizeDelta = new Vector2(this.chatContainerScroller.sizeDelta.x, (float) (this.list.Count * 24));
    if (flag)
    {
      this._chatScrollbar.verticalNormalizedPosition = 0.0f;
      this.Render();
    }
    if (!((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null) || ChatBox.Instance.Active)
      return;
    switch (ChatBox.showFade)
    {
      case 0:
        if (string.Equals(contain.nameOfPlayer, Client.Name))
          break;
        HUD.instance.repeatChatMsg.enabled = true;
        break;
      case 2:
        if (!HUD.YourTurn)
          break;
        goto case 0;
    }
  }

  public void Add(
    string name,
    string icon,
    string msg,
    Color c,
    string RightClickName,
    ChatOrigination origination,
    ContentType contentType,
    object obj)
  {
    PfabChatMsg.contain contain = new PfabChatMsg.contain();
    contain.nameOfPlayer = RightClickName;
    contain.icon = icon;
    contain.origination = origination;
    contain.date = "<color=#666666><mspace=9><align=\"center\">" + DateTime.Now.ToString("h:mm") + "</align></mspace></color> ";
    contain.c = c;
    contain.contentType = contentType;
    contain.obj = obj;
    contain.msg = string.IsNullOrEmpty(name) || contentType != ContentType.STRING ? msg : name + ": " + msg;
    this.Add(contain);
    if (!((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null) || ChatBox.Instance.Active)
      return;
    switch (ChatBox.showFade)
    {
      case 1:
        HUD.instance.chatFade.Add(contain.msg, c);
        break;
      case 2:
        if (HUD.YourTurn)
          break;
        goto case 1;
    }
  }
}
