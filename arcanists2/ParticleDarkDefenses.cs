

using UnityEngine;

#nullable disable
public class ParticleDarkDefenses : MonoBehaviour
{
  public Creature c;

  private void Update()
  {
    if (!((Object) this.c == (Object) null) && this.c.serverObj.hasDarkDefenses)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
