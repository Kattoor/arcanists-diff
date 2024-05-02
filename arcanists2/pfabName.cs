// Decompiled with JetBrains decompiler
// Type: pfabName
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class pfabName : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler
{
  public TMP_Text txtName;
  public TMP_Text txtRating;
  public Image ready;
  public Image ratingType;
  public TMP_Text txtExperience;
  private Account account;
  public static Color[] colorStatus = new Color[2]
  {
    (Color) new Color32((byte) 73, (byte) 73, (byte) 73, byte.MaxValue),
    (Color) new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue)
  };
  private float clickTime;

  public void Setup(string name, bool ready = false)
  {
    this.name = name;
    this.txtName.text = name;
    if (ready)
      this.ready.gameObject.SetActive(true);
    if (!((UnityEngine.Object) this.ratingType != (UnityEngine.Object) null))
      return;
    this.ratingType.gameObject.SetActive(false);
  }

  public void SetupIgnored(string name, string rate)
  {
    this.name = name;
    this.txtName.text = name;
    this.txtRating.text = rate;
    this.ready.gameObject.SetActive(false);
    if (!((UnityEngine.Object) this.ratingType != (UnityEngine.Object) null))
      return;
    this.ratingType.gameObject.SetActive(false);
  }

  public void Setup(Account a, bool ready = false)
  {
    this.account = a;
    this.name = a.name;
    this.txtName.text = ChatBox.GetAccountIcons(a, true, true) + a.name;
    this.txtRating.text = a.ratingString;
    if ((UnityEngine.Object) this.ratingType != (UnityEngine.Object) null)
    {
      this.ratingType.sprite = ClientResources.Instance.ratingTypes[Client.gameType.Clamp()];
      this.ratingType.gameObject.SetActive(true);
    }
    if ((UnityEngine.Object) this.txtExperience != (UnityEngine.Object) null)
      this.txtExperience.text = a.displayedIcon != 252 || a.accountType.IsMuted() ? "" : a.experience.ToString();
    if (!ready)
      return;
    this.ready.gameObject.SetActive(true);
  }

  public void ContextMenu() => MyContextMenu.Show(this.name);

  public void OnPointerDown(PointerEventData eventData)
  {
    this.clickTime = Time.realtimeSinceStartup;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Right || Input.touchSupported && (double) Time.realtimeSinceStartup - (double) this.clickTime > 0.5)
    {
      this.RightClick();
    }
    else
    {
      if (eventData.button != PointerEventData.InputButton.Left)
        return;
      this.LeftClick();
    }
  }

  private void LeftClick()
  {
    if ((UnityEngine.Object) ColorSchemeUI.Instance != (UnityEngine.Object) null || string.IsNullOrEmpty(this.name) || string.Equals(this.name, Client.Name, StringComparison.OrdinalIgnoreCase))
      return;
    if ((UnityEngine.Object) HUD.instance == (UnityEngine.Object) null || Spectator.isConnected)
      ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), this.name);
    else
      HUD.instance.InitPrivateChat(this.name);
  }

  private void RightClick() => this.ContextMenu();

  public void ClickRatingIcon() => PrestigeLobbyUI.NextGameType();

  public void HoverRatingIcon()
  {
    if (this.account != null)
      pfabName.HoverRatingIcon(this.account);
    else
      MyToolTip.Show(RatedFacts.GetGameTypeAsStringLong((int) Client.gameType) + " Rating");
  }

  public static void HoverRatingIcon(Account account, string extra = null)
  {
    MyToolTip.Show(account.name + "\n<sprite name=\"LTS\"> <#FF0050>" + (object) account.Rating + "</color>\n<sprite name=\"HTS\"> <#00FF1D>" + (object) account.Rating1 + "</color>\n<sprite name=\"PMO\"> <#00B2FF>" + (object) account.Rating2 + "</color>\n" + (extra != null ? (object) extra : (object) ""));
  }

  public void HideTooltip() => MyToolTip.Close();
}
