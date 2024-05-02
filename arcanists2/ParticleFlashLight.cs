// Decompiled with JetBrains decompiler
// Type: ParticleFlashLight
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ParticleFlashLight : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  [NonSerialized]
  public Creature target;

  private void Update()
  {
    if (!((UnityEngine.Object) this.target != (UnityEngine.Object) null) || !((UnityEngine.Object) this.target.transform != (UnityEngine.Object) null) || !((UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) null) || !((ZComponent) this.target.serverObj != (object) null))
      return;
    if (Client.map.bresenhamsLineCastOnlyTerrain(new Coords((int) this.transform.position.x, (int) this.transform.position.y), new Coords((int) this.transform.position.x, this.target.serverObj.map.Height)) != null)
    {
      if ((double) this.spriteRenderer.color.a >= 0.34999999403953552)
        return;
      Color color = this.spriteRenderer.color;
      color.a += Time.deltaTime;
      if ((double) color.a > 0.34999999403953552)
        color.a = 0.35f;
      this.spriteRenderer.color = color;
    }
    else
    {
      if ((double) this.spriteRenderer.color.a <= 0.0)
        return;
      Color color = this.spriteRenderer.color;
      color.a -= Time.deltaTime;
      if ((double) color.a < 0.0)
        color.a = 0.0f;
      this.spriteRenderer.color = color;
    }
  }
}
