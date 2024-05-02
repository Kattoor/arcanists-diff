// Decompiled with JetBrains decompiler
// Type: pfabInvite
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
