// Decompiled with JetBrains decompiler
// Type: SpellSnowball
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SpellSnowball : Spell
{
  private float angle;
  private Vector3 velocity = new Vector3(0.0f, 1f);
  private int down;

  public void TestBlit()
  {
    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Surface src = new Surface(this.snowTexture);
    RotateImage.Render(Client.game.map, src, (int) worldPoint.x, (int) worldPoint.y - src.height / 2, (float) (360.0 - (double) Mathf.Atan2(this.velocity.y, this.velocity.x) * 57.295780181884766), 1, true, false);
    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(this.velocity.y, this.velocity.x) * 57.29578f);
    if (this.down == 0)
    {
      this.velocity.x += 0.1f;
      this.velocity.y -= 0.1f;
      if ((double) this.velocity.x >= 1.0)
        this.down = 1;
    }
    else if (this.down == 1)
    {
      this.velocity.x -= 0.1f;
      this.velocity.y -= 0.1f;
      if ((double) this.velocity.x <= 0.0)
        this.down = 2;
    }
    else if (this.down == 2)
    {
      this.velocity.x -= 0.1f;
      this.velocity.y += 0.1f;
      if ((double) this.velocity.x <= -1.0)
        this.down = 3;
    }
    else
    {
      this.velocity.x += 0.1f;
      this.velocity.y += 0.1f;
      if ((double) this.velocity.x >= 0.0)
        this.down = 0;
    }
    this.angle += 15f;
  }
}
