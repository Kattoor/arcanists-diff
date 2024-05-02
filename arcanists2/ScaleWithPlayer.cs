

using UnityEngine;

#nullable disable
public class ScaleWithPlayer : MonoBehaviour
{
  public Vector2 localScale;

  private void Start() => this.localScale = (Vector2) this.transform.localScale;

  private void Update()
  {
    if (!((Object) Player.Instance != (Object) null) || !((ZComponent) Player.Instance.selected != (object) null) || this.Sign(Player.Instance.selected.transform.localScale.x) == this.Sign(this.localScale.x))
      return;
    this.localScale.x = -this.localScale.x;
    this.transform.localScale = (Vector3) this.localScale;
  }

  private bool Sign(float f) => (double) f > 0.0;
}
