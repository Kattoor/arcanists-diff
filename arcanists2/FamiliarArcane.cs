// Decompiled with JetBrains decompiler
// Type: FamiliarArcane
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FamiliarArcane : Familiar
{
  private bool back;
  private float t;
  public float speed = 1f;
  public float maxScale = 1f;
  public float minScale = 1f;

  private void Start()
  {
    if (ZComponent.IsNull((ZComponent) this.creature))
      return;
    this.transform.position = (Vector3) this.creature.position.ToSinglePrecision();
  }

  protected override void LateUpdate()
  {
    if (ZComponent.IsNull((ZComponent) this.creature))
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      if (!this.creature.game.isClient)
        return;
      if (this.back)
      {
        this.t -= Time.deltaTime * this.speed;
        if ((double) this.t <= 0.0)
        {
          this.back = false;
          if (!this.creature.isMoving)
            this.UpdatePosition();
        }
      }
      else
      {
        this.t += Time.deltaTime * this.speed;
        if ((double) this.t > 1.0)
          this.back = true;
      }
      float num = Mathf.SmoothStep(this.minScale, this.maxScale, this.t);
      this.transform.localScale = new Vector3(num, num);
    }
  }

  public void UpdatePosition()
  {
    this.transform.position = (Vector3) this.creature.position.ToSinglePrecision() + this.GetOffset();
  }
}
