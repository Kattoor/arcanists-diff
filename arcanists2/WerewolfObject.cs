// Decompiled with JetBrains decompiler
// Type: WerewolfObject
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WerewolfObject : MonoBehaviour
{
  public SpriteRenderer body;
  public SpriteRenderer leftArm;
  public SpriteRenderer rightArm;
  private List<Sprite> sprites = new List<Sprite>();

  internal void Init(ZPerson p, ZCreature creature, SettingsPlayer settingsPlayer, string name)
  {
    Color32 gray = settingsPlayer.coloring.colors[0].gray;
    settingsPlayer.coloring.colors[0].gray = settingsPlayer.coloring.colors[0].red;
    ClanOutfit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    this.leftArm.sprite = ConfigurePlayer.GetSprite(settingsPlayer.indexLeftHand, ClientResources.Instance._characterLeftHand, settingsPlayer, Outfit.LeftHand, settingsPlayer.textures?[2] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexLeftHand, Outfit.LeftHand));
    this.rightArm.sprite = ConfigurePlayer.GetSprite(settingsPlayer.indexRightHand, ClientResources.Instance._characterRightHand, settingsPlayer, Outfit.RightHand, settingsPlayer.textures?[3] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexRightHand, Outfit.RightHand));
    foreach (SpriteRenderer componentsInChild in this.GetComponentsInChildren<SpriteRenderer>())
    {
      if ((Object) componentsInChild.sprite != (Object) null && componentsInChild.sprite.texture.isReadable)
      {
        ConfigurePlayer.ApplyOutfit(componentsInChild, settingsPlayer);
        this.sprites.Add(componentsInChild.sprite);
      }
    }
    settingsPlayer.coloring.colors[2].gray = gray;
    Inert.AddTorquingAndOrArchStaffs(p, creature, settingsPlayer, true, false);
  }

  private void OnDestroy()
  {
    foreach (Sprite sprite in this.sprites)
    {
      if ((Object) sprite != (Object) null && sprite.texture.isReadable)
        Global.DestroySprite(sprite);
    }
  }
}
