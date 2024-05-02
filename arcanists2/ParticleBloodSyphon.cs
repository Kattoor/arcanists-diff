// Decompiled with JetBrains decompiler
// Type: ParticleBloodSyphon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleBloodSyphon : MonoBehaviour
{
  public Vector3 start;
  public Vector3 end;
  public Vector3 velocity;
  public ParticleSystem p;
  public GameObject explosion;
  private bool goend = true;
  private float cur;
  public float speed = 1f;

  private void Start()
  {
  }

  private void Update()
  {
    if (this.goend)
    {
      this.cur += Time.deltaTime * this.speed;
      if ((double) this.cur >= 1.0)
      {
        this.cur = 1f;
        this.goend = false;
        Object.Instantiate<GameObject>(this.explosion, this.end, Quaternion.identity, this.transform.parent);
      }
    }
    else
    {
      this.cur -= Time.deltaTime * this.speed;
      if ((double) this.cur <= 0.0)
      {
        this.p.Stop();
        this.enabled = false;
        Object.Destroy((Object) this.gameObject, 1f);
        return;
      }
    }
    this.transform.position = Vector3.Slerp(this.start, this.end, this.cur);
  }
}
