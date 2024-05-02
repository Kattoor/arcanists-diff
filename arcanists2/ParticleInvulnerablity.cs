

using UnityEngine;

#nullable disable
public class ParticleInvulnerablity : MonoBehaviour
{
  public Creature c;
  public float speed;
  private Vector3 a = Vector3.zero;

  private void Update()
  {
    if ((Object) this.c == (Object) null || this.c.serverObj.invulnerable <= 0)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      this.a.z += Time.deltaTime * this.speed;
      this.transform.localEulerAngles = this.a;
    }
  }
}
