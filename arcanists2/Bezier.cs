// Decompiled with JetBrains decompiler
// Type: Bezier
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class Bezier
{
  public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {
    t = Mathf.Clamp01(t);
    float num = 1f - t;
    return num * num * p0 + 2f * num * t * p1 + t * t * p2;
  }

  public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {
    return (float) (2.0 * (1.0 - (double) t)) * (p1 - p0) + 2f * t * (p2 - p1);
  }

  public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
  {
    t = Mathf.Clamp01(t);
    float num = 1f - t;
    return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
  }

  public static Vector3 GetFirstDerivative(
    Vector3 p0,
    Vector3 p1,
    Vector3 p2,
    Vector3 p3,
    float t)
  {
    t = Mathf.Clamp01(t);
    float num = 1f - t;
    return 3f * num * num * (p1 - p0) + 6f * num * t * (p2 - p1) + 3f * t * t * (p3 - p2);
  }
}
