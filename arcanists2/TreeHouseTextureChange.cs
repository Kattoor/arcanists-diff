// Decompiled with JetBrains decompiler
// Type: TreeHouseTextureChange
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TreeHouseTextureChange : MonoBehaviour
{
  internal GameSeason current;
  public List<Sprite> seasonTex;
  public SpriteRenderer rends;

  private void Awake() => this.LateUpdate();

  public void ChangeSeason(GameSeason season)
  {
    this.rends.sprite = this.seasonTex[(int) season];
    int num = 0;
    while (num < 4)
      ++num;
  }

  private void LateUpdate()
  {
    if (Client.game == null)
    {
      Object.Destroy((Object) this);
    }
    else
    {
      if (this.current == Client.game.currentSeason)
        return;
      this.current = Client.game.currentSeason;
      this.ChangeSeason(this.current);
    }
  }
}
