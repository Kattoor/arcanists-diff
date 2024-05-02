// Decompiled with JetBrains decompiler
// Type: EloSystem
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class EloSystem
{
  public static void RatingDebug(short a, short b)
  {
    float num1 = (float) (1.0 / (1.0 + (double) Mathf.Pow(10f, (float) ((int) b - (int) a) / 400f)));
    float num2 = (float) (1.0 / (1.0 + (double) Mathf.Pow(10f, (float) ((int) b - (int) a) / 400f)));
    float num3 = 30f;
    int num4 = (int) a;
    int num5 = (int) b;
    a = (short) ((double) a + (double) num3 * (1.0 - (double) num1));
    b = (short) ((double) b + (double) num3 * -(double) num2);
    Debug.Log((object) (num4.ToString() + ":" + (object) num5 + " < " + (object) num1 + ":" + (object) num2 + "> " + (object) a + ":" + (object) b));
  }

  public static short Rating(short a, short b, bool aWon, ZGame.GameType g)
  {
    float num = 40f;
    if (g > ZGame.GameType.Party || g < ZGame.GameType.LowStandard)
      g = ZGame.GameType.Party;
    return aWon ? (short) ((double) num * (1.0 - 1.0 / (1.0 + (double) Mathf.Pow(10f, (float) ((int) b - (int) a) / 700f)))) : (short) ((double) num * -(1.0 / (1.0 + (double) Mathf.Pow(10f, (float) ((int) b - (int) a) / 700f))));
  }
}
