// Decompiled with JetBrains decompiler
// Type: ImbuedAxeRot
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
