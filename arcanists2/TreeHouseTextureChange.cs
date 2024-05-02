

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
