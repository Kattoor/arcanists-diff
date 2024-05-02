// Decompiled with JetBrains decompiler
// Type: LinkPfab
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class LinkPfab : MonoBehaviour
{
  public UIOnHover buttonRankings;
  public UIOnHover buttonClans;
  public GameObject newPatch;

  private void Awake()
  {
    if (Client.offlineMode)
    {
      this.buttonRankings.Interactable(false);
      this.buttonClans.Interactable(false);
    }
    this.newPatch.SetActive(PatchMenu.NewVersion);
  }

  public void ContextMenu(string name, Action a)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator("Opens in an External Browser");
    myContextMenu.AddItem(name, a, (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.Rebuild();
  }

  public void ClickDiscordLink()
  {
    this.ContextMenu("Arcanists 2 Discord", (Action) (() => Global.OpenURL(Server.discordLink)));
  }

  public void ClicWebsiteLink()
  {
    this.ContextMenu("Arcanists 2 Website", (Action) (() => Global.OpenURL(Server.websiteLink)));
  }

  public void ClicWikiLink()
  {
    this.ContextMenu("Arcanists 2 wiki", (Action) (() => Global.OpenURL(Server.wikiLink)));
  }

  public void ClickRules() => Controller.ShowPopup(CreditsMenu.Type.Rules);

  public void ClickPatchNotes()
  {
    Controller.ShowPopup(Controller.Instance.MenuPatchNotes);
    this.newPatch.SetActive(false);
  }

  public void ClickCredits() => Controller.ShowPopup(CreditsMenu.Type.Credits);

  public void ClickClans() => Controller.ShowPopup(Controller.Instance.MenuClanList);

  public void ClickRatingsMenu() => Controller.ShowPopup(Controller.Instance.MenuRatings);

  public void ClickAchievements() => Controller.ShowPopup(Controller.Instance.MenuAchievements);

  public void ClickInformation() => Controller.ShowPrestigeMenu();

  public void Hover(string s) => MyToolTip.Show(s);

  public void Leave() => MyToolTip.Close();
}
