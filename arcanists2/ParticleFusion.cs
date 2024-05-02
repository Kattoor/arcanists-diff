

using UnityEngine;

#nullable disable
public class ParticleFusion : MonoBehaviour
{
  internal ZCreature creature;

  private void Update()
  {
    if (!((ZComponent) this.creature == (object) null) && this.creature.fusion > 0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
