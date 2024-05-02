

using System;
using TMPro;
using UnityEngine;

#nullable disable
public class PfabMapPing : MonoBehaviour
{
  public float duration = 2f;
  public SpriteRenderer img;
  public TMP_Text txt;
  public LineRenderer line;
  public SpriteRenderer imgMiniMap;
  public LineRenderer lineMiniMap;
  public int segments = 100;
  public float curRadius = 2f;
  public float maxRadius = 30f;
  public float minRadius = 2f;
  public float speed = 1f;
  private Vector3[] points;
  private Vector3[] points2;

  private void Start()
  {
    this.points = new Vector3[this.segments];
    this.points2 = new Vector3[this.segments];
    this.line.positionCount = this.segments;
    this.lineMiniMap.positionCount = this.segments;
    if (Client.game.resyncing || Client.game.isReplay || !Global.GetPrefBool("prefmappingsound", true))
      return;
    AudioManager.Play(AudioManager.instance.mapPing);
  }

  private void Update()
  {
    this.curRadius += Time.deltaTime * this.speed;
    if ((double) this.curRadius >= (double) this.maxRadius)
      this.curRadius = this.minRadius;
    this.DrawCircle();
    this.duration -= Time.deltaTime;
    if ((double) this.duration <= 0.0)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
    else
    {
      if ((double) this.duration >= 1.0)
        return;
      this.Fade();
    }
  }

  private void Fade()
  {
    Color color = this.txt.color with { a = this.duration };
    this.txt.color = color;
    this.img.color = color;
    this.line.startColor = color;
    this.line.endColor = color;
    this.imgMiniMap.color = color;
    this.lineMiniMap.startColor = color;
    this.lineMiniMap.endColor = color;
  }

  private void DrawCircle()
  {
    float num = (float) (360.0 / (double) (this.segments - 1) * (Math.PI / 180.0));
    float f = num;
    for (int index = 0; index < this.segments; ++index)
    {
      this.points[index] = new Vector3(Mathf.Cos(f) * this.curRadius, Mathf.Sin(f) * this.curRadius);
      this.points2[index] = this.points[index] * 20f;
      f += num;
    }
    this.line.SetPositions(this.points);
    this.lineMiniMap.SetPositions(this.points2);
  }
}
