// Decompiled with JetBrains decompiler
// Type: PfabChatMsg
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class PfabChatMsg : 
  MonoBehaviour,
  IPointerClickHandler,
  IEventSystemHandler,
  IPointerEnterHandler,
  IPointerExitHandler,
  IPointerDownHandler
{
  public TMP_Text _msg;
  public string nameOfPlayer = "";
  public Image highlight;
  private PfabChatMsg.contain t;
  private bool hovering;
  private float clickTime;

  public void FromContain(PfabChatMsg.contain t)
  {
    this.nameOfPlayer = t.nameOfPlayer;
    this._msg.text = !ChatBox.showIcons || string.IsNullOrEmpty(t.icon) ? (ChatBox.showDate ? t.date + t.msg : t.msg) : (ChatBox.showDate ? t.date + t.icon + t.msg : t.icon + t.msg);
    this._msg.color = t.c;
    this.t = t;
    if (!this.hovering)
      return;
    this.OnPointerEnter((PointerEventData) null);
  }

  private void DisplayOutfit(Transform t, SettingsPlayer sp)
  {
    UIPlayerCharacter p = UnityEngine.Object.Instantiate<UIPlayerCharacter>(ClientResources.Instance.uiPlayerCharacter, t);
    RectTransform transform = (RectTransform) p.transform;
    transform.localEulerAngles = Vector3.zero;
    transform.SetAnchor(Anchor.MiddleCenter);
    transform.pivot = new Vector2(0.5f, 0.5f);
    transform.anchoredPosition = ((RectTransform) t.GetChild(0)).anchoredPosition + new Vector2(((RectTransform) t.GetChild(0)).sizeDelta.x, 150f);
    transform.localScale = new Vector3(1f, 1f, 1f);
    ConfigurePlayer.EquipAll(this.nameOfPlayer, p, sp);
  }

  private void DisplayTutorial(Transform t, Tutorial sp)
  {
  }

  public void ContextMenu()
  {
    if (this.t != null)
    {
      switch (this.t.contentType)
      {
        case ContentType.Outfit:
          MyContextMenu myContextMenu1 = MyContextMenu.Show();
          this.nameOfPlayer = Server.SanitizeFilename(this.nameOfPlayer);
          if (string.IsNullOrEmpty(this.nameOfPlayer))
            this.nameOfPlayer = Server.RandomString(8);
          string file = "Downloads/" + this.nameOfPlayer;
          SettingsPlayer sp = (SettingsPlayer) this.t.obj;
          myContextMenu1.AddItem("Save @ " + file, (Action) (() =>
          {
            sp.SaveAs(file, false);
            ChatBox.Instance.NewChatMsg("Saved @ " + file, (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          myContextMenu1.AddItem("Save & Equip", (Action) (() =>
          {
            sp.SaveAs(file, false);
            Client.AskToChangeOutfit(sp);
            ChatBox.Instance.NewChatMsg("Saved @ " + file, (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          myContextMenu1.AddItem("Equip", (Action) (() => Client.AskToChangeOutfit(sp)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          myContextMenu1.Rebuild();
          this.DisplayOutfit(myContextMenu1.transform, sp);
          return;
        case ContentType.SpellBook:
          SpellLobbyChange.Create((SettingsPlayer) this.t.obj, (Action<SettingsPlayer>) (s => Client.AskToChangeSpells(s)));
          return;
        case ContentType.ArcTutorial:
          MyContextMenu myContextMenu2 = MyContextMenu.Show();
          this.nameOfPlayer = Server.SanitizeFilename(this.nameOfPlayer);
          if (string.IsNullOrEmpty(this.nameOfPlayer))
            this.nameOfPlayer = Server.RandomString(8);
          myContextMenu2.AddItem("Save @ Downloads/" + this.nameOfPlayer, (Action) (() =>
          {
            Global.SaveTutorial("Downloads/" + this.nameOfPlayer, ((Tutorial) this.t.obj).ToJson().ToString());
            ChatBox.Instance.NewChatMsg("Saved @ Downloads/" + this.nameOfPlayer, (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          myContextMenu2.Rebuild();
          this.DisplayTutorial(myContextMenu2.transform, (Tutorial) this.t.obj);
          return;
        case ContentType.ClanInvite:
          MyContextMenu myContextMenu3 = MyContextMenu.Show();
          ClanInvite z1 = (ClanInvite) this.t.obj;
          if ((double) z1.expire < (double) Time.realtimeSinceStartup)
            myContextMenu3.AddItem("Invitation expired", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
          else if (!string.IsNullOrEmpty(Client.MyAccount.clan))
            myContextMenu3.AddItem(string.Equals(Client.MyAccount.clan, z1.clan) ? "You've already joined the clan" : "You're already in a clan", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
          else
            myContextMenu3.AddItem("Accept invite from the clan [" + z1.clan + "]", (Action) (() => Client.AcceptClanInvite(z1.clan)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          myContextMenu3.Rebuild();
          return;
        case ContentType.MiniGameInvite:
          MyContextMenu myContextMenu4 = MyContextMenu.Show();
          MinigameInvite z2 = (MinigameInvite) this.t.obj;
          if (Client.miniGame != null && Client.miniGame.id == z2.minigameID)
            myContextMenu4.AddSeperator("Already joined this game");
          else if (z2.spectator)
          {
            myContextMenu4.AddItem("Spectate " + z2.from + "'s " + (object) (IMiniGame.GameType) z2.miniGameType + " game", (Action) (() => Client.AcceptSpectateMiniGame(z2.minigameID, true)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }
          else
          {
            myContextMenu4.AddItem("Spectate " + z2.from + "'s " + (object) (IMiniGame.GameType) z2.miniGameType + " game", (Action) (() => Client.AcceptSpectateMiniGame(z2.minigameID, true)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
            myContextMenu4.AddSeperator();
            myContextMenu4.AddItem("Join " + z2.from + "'s " + (object) (IMiniGame.GameType) z2.miniGameType + " game (Spectate if full)", (Action) (() => Client.AcceptSpectateMiniGame(z2.minigameID, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
          }
          myContextMenu4.Rebuild();
          return;
        case ContentType.ColorScheme:
          ColorSchemeUI.Create((ColorScheme) this.t.obj);
          return;
      }
    }
    MyContextMenu.Show(this.nameOfPlayer, this.t.msg);
  }

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
      if (eventData.button != PointerEventData.InputButton.Left || eventData.clickCount != 2)
        return;
      this.LeftClick();
    }
  }

  private void LeftClick()
  {
    if (string.IsNullOrEmpty(this.nameOfPlayer) || string.Equals(this.nameOfPlayer, Client.Name, StringComparison.OrdinalIgnoreCase) || !Client._accounts.ContainsKey(this.nameOfPlayer))
      return;
    if ((UnityEngine.Object) HUD.instance == (UnityEngine.Object) null || Spectator.isConnected)
      ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), this.nameOfPlayer);
    else
      HUD.instance.InitPrivateChat(this.nameOfPlayer);
  }

  private void RightClick() => this.ContextMenu();

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.hovering = true;
    if ((UnityEngine.Object) this.highlight != (UnityEngine.Object) null)
      this.highlight.color = this.highlight.color with
      {
        a = 0.075f
      };
    if (!this._msg.isTextTruncated && !((UnityEngine.Object) ColorSchemeUI.Instance != (UnityEngine.Object) null))
      return;
    MyToolTip.Show(this._msg.text);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    this.hovering = false;
    if ((UnityEngine.Object) this.highlight != (UnityEngine.Object) null)
      this.highlight.color = this.highlight.color with
      {
        a = 0.0f
      };
    MyToolTip.Close();
  }

  public class contain
  {
    public string nameOfPlayer;
    public string icon;
    public string msg;
    public string date;
    public Color c;
    public ChatOrigination origination;
    public ContentType contentType;
    public object obj;
    public int _id;

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this.nameOfPlayer);
      w.Write(this.msg);
      w.Write(this.date);
      w.Write(this.c);
      w.Write((byte) this.origination);
      w.Write((byte) this.contentType);
      Content.ToBytes(w, this.obj, this.contentType);
    }

    public static PfabChatMsg.contain Deserialize(myBinaryReader r)
    {
      PfabChatMsg.contain contain = new PfabChatMsg.contain()
      {
        nameOfPlayer = r.ReadString(),
        msg = r.ReadString(),
        date = r.ReadString(),
        c = r.ReadColor(),
        origination = (ChatOrigination) r.ReadByte(),
        contentType = (ContentType) r.ReadByte()
      };
      contain.obj = Content.GetContent(r, contain.contentType);
      return contain;
    }
  }
}
