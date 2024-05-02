

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
