

using UnityEngine;

#nullable disable
public class AnimateMythEffector : MonoBehaviour
{
  public ZEffector effector;
  public GameObject color;

  private void Update()
  {
    if (ZComponent.IsNull((ZComponent) this.effector) || this.effector.dead)
      return;
    if (this.effector.active && !this.color.activeSelf)
    {
      this.color.SetActive(true);
    }
    else
    {
      if (this.effector.active || !this.color.activeSelf)
        return;
      this.color.SetActive(false);
    }
  }
}
