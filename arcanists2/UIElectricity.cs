// Decompiled with JetBrains decompiler
// Type: UIElectricity
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

#nullable disable
public class UIElectricity : MonoBehaviour
{
  public UILineRenderer lRend;
  public UILineRenderer glowRend;
  public RectTransform rectGlow;
  public float currentAngle;
  private System.Random random;
  private int seed;

  [ContextMenu("Generate")]
  public void Generate()
  {
    this.Setup(this.lRend.Points[0], this.lRend.Points[this.lRend.Points.Length - 1], true);
  }

  public void Setup(Vector2 start, Vector2 end, bool newRandom)
  {
    if (newRandom)
      this.seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
    this.random = new System.Random(this.seed);
    this.rectGlow.anchoredPosition = end;
    this.lRend.Points = UIElectricity.SmoothLine(this.CreateBolt(start, end), 10f);
    this.glowRend.Points = this.lRend.Points;
  }

  public static Vector2[] SmoothLine(List<Vector2> inputPoints, float segmentSize)
  {
    AnimationCurve animationCurve1 = new AnimationCurve();
    AnimationCurve animationCurve2 = new AnimationCurve();
    Keyframe[] keyframeArray1 = new Keyframe[inputPoints.Count];
    Keyframe[] keyframeArray2 = new Keyframe[inputPoints.Count];
    for (int index = 0; index < inputPoints.Count; ++index)
    {
      keyframeArray1[index] = new Keyframe((float) index, inputPoints[index].x);
      keyframeArray2[index] = new Keyframe((float) index, inputPoints[index].y);
    }
    animationCurve1.keys = keyframeArray1;
    animationCurve2.keys = keyframeArray2;
    for (int index = 0; index < inputPoints.Count; ++index)
    {
      animationCurve1.SmoothTangents(index, 0.0f);
      animationCurve2.SmoothTangents(index, 0.0f);
    }
    List<Vector2> vector2List = new List<Vector2>();
    for (int index1 = 0; index1 < inputPoints.Count; ++index1)
    {
      vector2List.Add(inputPoints[index1]);
      if (index1 + 1 < inputPoints.Count)
      {
        int num = (int) ((double) Vector3.Distance((Vector3) inputPoints[index1], (Vector3) inputPoints[index1 + 1]) / (double) segmentSize);
        for (int index2 = 1; index2 < num; ++index2)
        {
          float time = (float) index2 / (float) num + (float) index1;
          Vector2 vector2 = new Vector2(animationCurve1.Evaluate(time), animationCurve2.Evaluate(time));
          vector2List.Add(vector2);
        }
      }
    }
    return vector2List.ToArray();
  }

  protected List<Vector2> CreateBolt(Vector2 source, Vector2 dest)
  {
    List<Vector2> bolt = new List<Vector2>();
    Vector2 vector2_1 = dest - source;
    Vector2 normalized = new Vector2(vector2_1.y, -vector2_1.x).normalized;
    float magnitude = vector2_1.magnitude;
    List<float> floatList = new List<float>();
    floatList.Add(0.0f);
    int num1 = (int) magnitude / 80;
    for (int index = 0; index < num1; ++index)
      floatList.Add(1f * (float) index / (float) num1);
    bolt.Add(source);
    float num2 = 0.0f;
    for (int index = 1; index < floatList.Count; ++index)
    {
      float num3 = floatList[index];
      float num4 = (float) ((double) magnitude * 0.0083333337679505348 * ((double) num3 - (double) floatList[index - 1]));
      float num5 = (double) num3 > 0.949999988079071 ? (float) (20.0 * (1.0 - (double) num3)) : 1f;
      float num6 = (float) this.random.Next(-8000, 8000) / 100f;
      float num7 = (num6 - (float) (((double) num6 - (double) num2) * (1.0 - (double) num4))) * num5;
      Vector2 vector2_2 = source + num3 * vector2_1 + num7 * normalized;
      bolt.Add(vector2_2);
      num2 = num7;
    }
    bolt.Add(dest);
    return bolt;
  }
}
