

using UnityEngine;

#nullable disable
public class ImbuedAxeRot : MonoBehaviour
{
  public RotateTransformZ rotator;
  public Spell spell;

  private void Start()
  {
  }

  private void Update()
  {
    if (this.spell.serverObj.velocity.x > 0 && (double) this.transform.localScale.x > 0.0)
    {
      this.transform.localScale = new Vector3(-1f, 1f, 1f);
      this.rotator.speed = -500f;
    }
    else
    {
      if (!(this.spell.serverObj.velocity.x < 0) || (double) this.transform.localScale.x >= 0.0)
        return;
      this.transform.localScale = new Vector3(1f, 1f, 1f);
      this.rotator.speed = 500f;
    }
  }
}
