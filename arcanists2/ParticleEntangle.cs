// Decompiled with JetBrains decompiler
// Type: ParticleEntangle
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleEntangle : MonoBehaviour
{
  private Creature c;
  public float speed = 10f;
  private Vector3 a = Vector3.zero;
  public bool entangle = true;

  public void SetCreature(Creature c, bool rotate)
  {
    this.c = c;
    if (!rotate)
      return;
    this.Setup();
  }

  private void Update()
  {
    if ((Object) this.c == (Object) null || this.entangle && !this.c.serverObj.entangled || !this.entangle && !this.c.serverObj.gravitionalPull)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      this.a.z += Time.deltaTime * this.speed;
      this.transform.localEulerAngles = this.a;
    }
  }

  private void Setup()
  {
    this.transform.GetChild(0).localPosition = new Vector3(0.0f, (float) (this.c.radius + 2));
    this.transform.GetChild(1).localPosition = new Vector3((float) (this.c.radius + 2), 0.0f);
    this.transform.GetChild(2).localPosition = new Vector3(0.0f, (float) (-this.c.radius - 2));
  }
}
