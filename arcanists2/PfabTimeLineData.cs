﻿// Decompiled with JetBrains decompiler
// Type: PfabTimeLineData
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class PfabTimeLineData : 
  MonoBehaviour,
  IRecycledScrollViewItem,
  IPointerClickHandler,
  IEventSystemHandler
{
  public TMP_Text _msg;
  public UIOnHover _ui;
  private int index;

  public void Init(string msg, int index)
  {
    if (Client.game.replayPastTimeLine - 1 < index)
    {
      this._msg.color = (Color) ColorScheme.GetColor(Global.ColorLobbyText);
      this._ui.textNormalColor = (Color) ColorScheme.GetColor(Global.ColorLobbyText);
    }
    else
    {
      this._msg.color = Color.red;
      this._ui.textNormalColor = Color.red;
    }
    this._msg.text = msg;
    this.index = index;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Right || eventData.button != PointerEventData.InputButton.Left)
      return;
    Client.game.GoToReplayFrame(this.index);
  }
}
