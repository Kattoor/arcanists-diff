// Decompiled with JetBrains decompiler
// Type: ScaleWithPlayer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
