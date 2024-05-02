// Decompiled with JetBrains decompiler
// Type: WerewolfObject
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WerewolfObject : MonoBehaviour
{
  public AnimateRepeat anim;
  public SpriteRenderer body;
  private List<Sprite> sprites = new List<Sprite>();

  internal void Init(SettingsPlayer settingsPlayer, string name)
  {
    ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    this.body.sprite = ConfigurePlayer.GetSprite(settingsPlayer.indexBody, ClientResources.Instance._characterBody, settingsPlayer, Outfit.Body, settingsPlayer.textures?[0] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.Body));
    foreach (SpriteRenderer componentsInChild in this.GetComponentsInChildren<SpriteRenderer>())
    {
      if ((Object) componentsInChild.sprite != (Object) null && componentsInChild.sprite.texture.isReadable)
      {
        ConfigurePlayer.ApplyOutfit(componentsInChild, settingsPlayer);
        this.sprites.Add(componentsInChild.sprite);
      }
    }
    for (int index = 0; index < this.anim.sprites.Length; ++index)
    {
      Sprite sprite = this.anim.sprites[index];
      if ((Object) sprite != (Object) null && sprite.texture.isReadable)
      {
        this.anim.sprites[index] = ConfigurePlayer.ApplyOutfit(sprite, settingsPlayer);
        this.sprites.Add(this.anim.sprites[index]);
      }
    }
    this.anim.GetSpriteRenderer.sprite = this.anim.sprites[0];
    this.anim.deleteOnDestroy = true;
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
