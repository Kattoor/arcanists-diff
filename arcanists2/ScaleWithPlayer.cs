// Decompiled with JetBrains decompiler
// Type: ScaleWithPlayer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
