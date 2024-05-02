// Decompiled with JetBrains decompiler
// Type: FamiliarFlame
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FamiliarFlame : Familiar
{
  public float maxDistance = 4f;
  public float speed = 1f;

  private void Start()
  {
    if (ZComponent.IsNull((ZComponent) this.creature))
      return;
    this.transform.position = (Vector3) this.creature.position.ToSinglePrecision() + new Vector3(-9f, -8f);
  }

  protected override void LateUpdate()
  {
    if (ZComponent.IsNull((ZComponent) this.creature) || this.creature.isDead)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      if (!this.creature.game.isClient)
        return;
      Vector3 offset = this.GetOffset();
      if ((double) this.creature.transformscale > 0.0)
        offset.x = -offset.x;
      Vector3 vector3 = (Vector3) this.creature.position.ToSinglePrecision() + offset;
      if ((double) this.transform.localScale.x < 0.0 && (double) this.creature.transformscale > 0.0)
        this.transform.localScale = new Vector3(1f, 1f, 1f);
      else if ((double) this.transform.localScale.x > 0.0 && (double) this.creature.transformscale < 0.0)
        this.transform.localScale = new Vector3(-1f, 1f, 1f);
      if ((double) Vector3.Distance(vector3, this.transform.position) <= (double) this.maxDistance)
        return;
      this.transform.position = Vector3.Lerp(this.transform.position, vector3, Time.deltaTime * this.speed);
    }
  }
}
