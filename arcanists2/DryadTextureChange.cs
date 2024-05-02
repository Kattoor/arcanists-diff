// Decompiled with JetBrains decompiler
// Type: DryadTextureChange
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class DryadTextureChange : MonoBehaviour
{
  internal Creature creature;
  internal GameSeason current;
  public List<DryadTextureChange.GroupTextures> seasonTex;
  public List<SpriteRenderer> rends;
  public AnimateDragon dragon;

  public void ChangeSeason(GameSeason season)
  {
    int index1 = (int) season;
    for (int index2 = 0; index2 < 4; ++index2)
      this.rends[index2].sprite = this.seasonTex[index1].sprites[index2];
  }

  public void ChangeAnimator(GameSeason season)
  {
    for (int index = 0; index < this.dragon.sprites.Length; ++index)
      this.dragon.sprites[index] = this.seasonTex[(int) season].sprites[index];
  }

  private void LateUpdate()
  {
    if ((UnityEngine.Object) this.creature == (UnityEngine.Object) null)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }
    else
    {
      if (this.current == this.creature.serverObj.game.currentSeason)
        return;
      this.current = this.creature.serverObj.game.currentSeason;
      if ((UnityEngine.Object) this.dragon != (UnityEngine.Object) null)
        this.ChangeAnimator(this.current);
      else
        this.ChangeSeason(this.current);
    }
  }

  [Serializable]
  public class GroupTextures
  {
    public List<Sprite> sprites;
  }
}
