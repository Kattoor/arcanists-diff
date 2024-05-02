

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class pfabInvite : MonoBehaviour
{
  public pfabName pfabName;
  public TMP_Text txtStatus;
  public Image buttonImage;
  public UIOnHover button;

  public void Setup(Account a)
  {
    if (Client._gameFacts.invitedPlayers.Contains(a.name))
    {
      this.txtStatus.text = "Cancel";
      if ((Object) this.button != (Object) null)
        this.SetButtons(1);
      else
        this.buttonImage.color = Color.red;
    }
    else
    {
      this.txtStatus.text = "Invite";
      if ((Object) this.button != (Object) null)
        this.SetButtons(0);
      else
        this.buttonImage.color = Color.white;
    }
    this.pfabName.Setup(a);
  }

  private void SetButtons(int i)
  {
    this.button.NormalSprite = ClientResources.Instance.joinDefault[i];
    this.button.HighlightedSprite = ClientResources.Instance.joinHover[i];
    this.button.PressedSprite = ClientResources.Instance.joinPressed[i];
    this.buttonImage.sprite = this.button.IsHovering ? this.button.HighlightedSprite : this.button.NormalSprite;
  }

  public void OnClick()
  {
    if (!((Object) UnratedMenu.instance != (Object) null) || !UnratedMenu.instance.IsFirst)
      return;
    Client.AskToInvite(this.pfabName.name);
  }
}
