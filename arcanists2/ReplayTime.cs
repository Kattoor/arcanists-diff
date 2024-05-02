// Decompiled with JetBrains decompiler
// Type: ReplayTime
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ReplayTime : MonoBehaviour
{
  private byte lastPlayersTurn = byte.MaxValue;
  public TMP_Text text;
  public Image image;

  private void Update()
  {
    if (Client.game == null || (int) this.lastPlayersTurn == (int) Client.game.serverState.playersTurn || (int) Client.game.serverState.playersTurn >= Client.game.players.Count)
      return;
    this.lastPlayersTurn = Client.game.serverState.playersTurn;
    this.text.text = Client.game.players[(int) this.lastPlayersTurn].name;
    this.text.color = Client.game.players[(int) this.lastPlayersTurn].clientColor;
  }
}
