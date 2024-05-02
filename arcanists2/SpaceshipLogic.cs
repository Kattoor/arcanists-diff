// Decompiled with JetBrains decompiler
// Type: SpaceshipLogic
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SpaceshipLogic : MonoBehaviour
{
  public AnimationCurve curve;
  private float min = -1000f;
  private float max;
  private float cur;
  public float speed = 0.01f;
  private Vector3 pos;
  private float seed;

  private void Start()
  {
    this.max = (float) (Client.game.map.Width + 1000);
    this.seed = (float) Random.Range(-100, 100);
  }

  private void Update()
  {
    if (Client.game == null || Client.game.map == null)
      return;
    this.cur += Time.deltaTime * this.speed;
    this.pos.x = Mathf.Lerp(this.min, this.max, this.cur);
    this.pos.y = (float) ((double) Mathf.PerlinNoise(this.pos.x / 1000f + this.seed, this.pos.x / 500f + this.seed) * 500.0 + (double) Client.game.map.Height + 300.0);
    this.transform.position = this.pos;
    if ((double) this.cur < 1.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
