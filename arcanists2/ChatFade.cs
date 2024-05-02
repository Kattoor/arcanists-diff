// Decompiled with JetBrains decompiler
// Type: ChatFade
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ChatFade : MonoBehaviour
{
  public PfabChatMsgFade pfab;
  private List<PfabChatMsgFade> que = new List<PfabChatMsgFade>();
  private bool yourTurn;

  public void Add(string msg, Color c)
  {
    PfabChatMsgFade pfabChatMsgFade = Object.Instantiate<PfabChatMsgFade>(this.pfab, this.transform);
    Controller.Instance?.ApplyScheme(pfabChatMsgFade.gameObject);
    pfabChatMsgFade.time = Time.realtimeSinceStartup + 5f;
    pfabChatMsgFade.behind.text = msg;
    pfabChatMsgFade.infront.text = msg;
    pfabChatMsgFade.infront.color = c;
    this.que.Add(pfabChatMsgFade);
    this.Refresh();
  }

  private void Refresh()
  {
    float y = (float) (this.que.Count * 30);
    if (!this.yourTurn)
      y -= 120f;
    foreach (Component component in this.que)
    {
      ((RectTransform) component.transform).anchoredPosition = new Vector2(0.0f, y);
      y -= 30f;
    }
  }

  private void Update()
  {
    if (this.que.Count <= 0)
      return;
    if ((double) this.que[0].time < (double) Time.realtimeSinceStartup || this.que.Count > 5)
    {
      this.StartCoroutine(this.Fade(this.que[0]));
      this.que.RemoveAt(0);
    }
    if (this.yourTurn == HUD.YourTurn)
      return;
    this.yourTurn = !this.yourTurn;
    this.Refresh();
  }

  private IEnumerator Fade(PfabChatMsgFade p)
  {
    Color c = p.infront.color;
    Color b = p.bg.color;
    while ((double) c.a > 0.0)
    {
      c.a -= Time.deltaTime * 2f;
      b.a -= Time.deltaTime;
      p.bg.color = b;
      p.infront.color = c;
      yield return (object) new WaitForEndOfFrame();
    }
    Object.Destroy((Object) p.gameObject);
  }
}
