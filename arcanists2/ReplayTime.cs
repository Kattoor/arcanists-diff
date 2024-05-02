

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
