// Decompiled with JetBrains decompiler
// Type: MenuBackgroundUpdater
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MenuBackgroundUpdater : MonoBehaviour
{
  public Image bgImage;
  public Image fgImage;

  private void Awake() => MenuBackgroundUpdater.SwitchBackgrounds(this.bgImage, this.fgImage);

  public static void SwitchBackgrounds(Image bgImage, Image fgImage)
  {
    if (!((Object) bgImage != (Object) null))
      return;
    int e = PlayerPrefs.GetInt("prefsandboxmaps", 0);
    if (e < 0 || e >= ClientResources.Instance._maps.Length)
      e = 0;
    while (e == 11)
      e = Random.Range(0, ClientResources.Instance._maps.Length);
    int mapIndex = GameFacts.GetMapIndex(GameFacts.MapFromIndex(e));
    fgImage.sprite = ClientResources.Instance._scaled_maps[e];
    bgImage.sprite = ClientResources.Instance._scaled_mapBgs[mapIndex];
    if (Client.colorScheme == null)
      return;
    fgImage.color = Client.colorScheme.backgroundColor;
    bgImage.color = Client.colorScheme.backgroundColor;
  }
}
