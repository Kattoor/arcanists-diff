

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class RecycledScrollViewStandalone : MonoBehaviour
{
  public GameObject pfabChat;
  public RectTransform chatContainer;
  public RectTransform chatContainerScroller;
  public ScrollRect _chatScrollbar;
  public Scrollbar _bar;
  public int MaxVisible = 29;
  public int MaxSize = 200;
  private bool notmaxed = true;
  private List<IRecycledScrollViewItem> pfabs = new List<IRecycledScrollViewItem>();
  private List<string> list = new List<string>();
  private int firstVisible;
  private int nextindex;
  private int lastRender = -1;

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
      this.pfabs[index2].Init(this.list[index1], num + index2);
      ++index1;
      if (index1 >= this.list.Count)
        index1 = 0;
    }
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

  public void Add(string msg)
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
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.pfabChat, (Transform) this.chatContainer);
      IRecycledScrollViewItem component = gameObject.GetComponent<IRecycledScrollViewItem>();
      ((RectTransform) gameObject.transform).anchoredPosition = new Vector2(0.0f, (float) ((this.chatContainer.childCount - 1) * -24));
      this.pfabs.Add(component);
      component.Init(msg, this.pfabs.Count - 1);
      gameObject.SetActive(true);
    }
    if (this.list.Count <= this.MaxSize)
      this.chatContainerScroller.sizeDelta = new Vector2(this.chatContainerScroller.sizeDelta.x, (float) (this.list.Count * 24));
    if (flag)
    {
      this._chatScrollbar.verticalNormalizedPosition = 0.0f;
      this.Render();
    }
    if (!((Object) HUD.instance != (Object) null) || ChatBox.Instance.Active)
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
