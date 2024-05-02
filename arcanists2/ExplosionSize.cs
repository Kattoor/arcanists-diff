// Decompiled with JetBrains decompiler
// Type: ExplosionSize
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class ExplosionSize
{
  public static int GetRadius(ExplosionCutout e)
  {
    switch (e)
    {
      case ExplosionCutout.Ninty:
        return 45;
      case ExplosionCutout.Two_Fifty_Four:
        return (int) sbyte.MaxValue;
      case ExplosionCutout.Two_Seventy:
        return 109;
      case ExplosionCutout.Twenty:
        return 10;
      case ExplosionCutout.Forty_Five:
        return 23;
      case ExplosionCutout.Seventy_Five:
        return 38;
      case ExplosionCutout.One_Twenty_Eight:
        return 64;
      case ExplosionCutout.Twenty_Five:
        return 13;
      case ExplosionCutout.One_Fifteen:
        return 58;
      case ExplosionCutout.Two_Hundred:
        return 100;
      case ExplosionCutout.One_Thirty:
        return 65;
      case ExplosionCutout.One_Eighty_Seven:
        return 94;
      case ExplosionCutout.One_O_Eight:
        return 54;
      case ExplosionCutout.One_Forty_Four:
        return 72;
      case ExplosionCutout.One_Sixty_Two:
        return 81;
      case ExplosionCutout.One_Eighty:
        return 90;
      case ExplosionCutout.Two_Twenty_One:
        return 111;
      case ExplosionCutout.Forty:
        return 20;
      case ExplosionCutout.Fifty:
        return 25;
      case ExplosionCutout.Three_Fifty:
        return 175;
      case ExplosionCutout.Three_Hundred:
        return 150;
      case ExplosionCutout.Three_Twenty_Five:
        return 163;
      case ExplosionCutout.Three_Seventy_Five:
        return 188;
      case ExplosionCutout.Four_Hundred:
        return 200;
      case ExplosionCutout.Four_Twenty_Five:
        return 213;
      case ExplosionCutout.Four_Fifty:
        return 225;
      case ExplosionCutout.Four_Seventy_Five:
        return 238;
      case ExplosionCutout.Five_Hundred:
        return 250;
      case ExplosionCutout.Five_Twenty_Five:
        return 263;
      case ExplosionCutout.Five_Fifty:
        return 275;
      case ExplosionCutout.Five_Seventy_Three:
        return 288;
      case ExplosionCutout.Five_Ninty_Six:
        return 298;
      case ExplosionCutout.Six_Twenty_Four:
        return 312;
      case ExplosionCutout.Six_Fifty_Three:
        return 327;
      case ExplosionCutout.Six_Eighty_One:
        return 341;
      case ExplosionCutout.Seven_Ten:
        return 355;
      case ExplosionCutout.Sixty:
        return 30;
      case ExplosionCutout.One_Thirty_Seven:
        return 70;
      case ExplosionCutout.None:
        return 0;
      default:
        Debug.LogError((object) ("Explosion Cutout " + (object) e + " is undefined"));
        return 90;
    }
  }

  public static ExplosionCutout GetImpRadius(int health)
  {
    if (health >= 245)
      return ExplosionCutout.Seven_Ten;
    if (health >= 240)
      return ExplosionCutout.Six_Eighty_One;
    if (health >= 230)
      return ExplosionCutout.Six_Fifty_Three;
    if (health >= 220)
      return ExplosionCutout.Six_Twenty_Four;
    if (health >= 210)
      return ExplosionCutout.Five_Ninty_Six;
    if (health >= 200)
      return ExplosionCutout.Five_Seventy_Three;
    if (health >= 190)
      return ExplosionCutout.Five_Fifty;
    if (health >= 180)
      return ExplosionCutout.Five_Twenty_Five;
    if (health >= 170)
      return ExplosionCutout.Five_Hundred;
    if (health >= 160)
      return ExplosionCutout.Four_Seventy_Five;
    if (health >= 150)
      return ExplosionCutout.Four_Fifty;
    if (health >= 145)
      return ExplosionCutout.Four_Twenty_Five;
    if (health >= 140)
      return ExplosionCutout.Four_Hundred;
    if (health >= 135)
      return ExplosionCutout.Three_Seventy_Five;
    if (health >= 130)
      return ExplosionCutout.Three_Fifty;
    if (health >= 125)
      return ExplosionCutout.Three_Twenty_Five;
    if (health >= 120)
      return ExplosionCutout.Three_Hundred;
    if (health >= 115)
      return ExplosionCutout.Two_Seventy;
    if (health >= 110)
      return ExplosionCutout.Two_Fifty_Four;
    if (health >= 100)
      return ExplosionCutout.Two_Twenty_One;
    if (health >= 90)
      return ExplosionCutout.Two_Hundred;
    if (health >= 80)
      return ExplosionCutout.One_Eighty_Seven;
    if (health >= 70)
      return ExplosionCutout.One_Eighty;
    if (health >= 60)
      return ExplosionCutout.One_Sixty_Two;
    if (health >= 50)
      return ExplosionCutout.One_Forty_Four;
    if (health >= 40)
      return ExplosionCutout.One_Twenty_Eight;
    if (health >= 30)
      return ExplosionCutout.One_Fifteen;
    if (health >= 20)
      return ExplosionCutout.One_O_Eight;
    if (health >= 15)
      return ExplosionCutout.Ninty;
    if (health >= 11)
      return ExplosionCutout.Seventy_Five;
    return health < 7 ? ExplosionCutout.Fifty : ExplosionCutout.Sixty;
  }
}
