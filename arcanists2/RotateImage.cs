// Decompiled with JetBrains decompiler
// Type: RotateImage
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public static class RotateImage
{
  private static Color32 purple = (Color32) new Color(254f, 191f, 254f);

  public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
  {
    return Quaternion.Euler(angles) * (point - pivot) + pivot;
  }

  public static MyLocation RotatePoint(
    MyLocation pointToRotate,
    MyLocation centerPoint,
    FixedInt angleInDegrees)
  {
    FixedInt x = angleInDegrees * (FixedInt.Pi / 180);
    FixedInt fixedInt1 = FixedInt.Cos(x);
    FixedInt fixedInt2 = FixedInt.Sin(x);
    return new MyLocation(fixedInt1 * (pointToRotate.x - centerPoint.x) - fixedInt2 * (pointToRotate.y - centerPoint.y) + centerPoint.x, fixedInt2 * (pointToRotate.x - centerPoint.x) + fixedInt1 * (pointToRotate.y - centerPoint.y) + centerPoint.y);
  }

  public static MyLocation RotatePointAroundPivot(
    MyLocation point,
    MyLocation pivot,
    FixedInt angle)
  {
    MyLocation myLocation1 = new MyLocation(FixedInt.Cos(angle), FixedInt.Sin(angle));
    MyLocation myLocation2 = new MyLocation(-FixedInt.Sin(angle), FixedInt.Cos(angle));
    MyLocation myLocation3 = point - pivot;
    myLocation3.x = myLocation1.x * myLocation3.x + myLocation1.y * myLocation3.y;
    myLocation3.y = myLocation2.x * myLocation3.x + myLocation2.y * myLocation3.y;
    return myLocation3 + pivot;
  }

  public static void Render(
    ZMap dst,
    Surface src,
    int x,
    int y,
    float angle,
    int renderMode,
    bool infront,
    bool forceAlpha = true,
    float scale = 1f)
  {
    int x1 = src.width / 2;
    int y1 = src.height / 2;
    double num1 = (double) scale;
    double num2 = Math.PI * (double) angle / 180.0;
    double num3 = -0.75;
    double num4 = num3 + 2.0;
    double num5 = num3 + 3.0;
    double num6 = 5.0 * num3;
    double num7 = 8.0 * num3;
    double num8 = 4.0 * num3;
    int width = dst.Width;
    int height = dst.Height;
    Vector3 pivot = new Vector3((float) x1, (float) y1);
    Vector3 angles = new Vector3(0.0f, 0.0f, -angle);
    Vector3 vector3_1 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, 0.0f), pivot, angles);
    Vector3 vector3_2 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, 0.0f), pivot, angles);
    Vector3 vector3_3 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, (float) src.height), pivot, angles);
    Vector3 vector3_4 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, (float) src.height), pivot, angles);
    int num9 = Math.Max(Math.Max((int) vector3_1.x, (int) vector3_2.x), Math.Max((int) vector3_3.x, (int) vector3_4.x));
    int num10 = Math.Min(Math.Min((int) vector3_1.x, (int) vector3_2.x), Math.Min((int) vector3_3.x, (int) vector3_4.x));
    int num11 = Math.Max(Math.Max((int) vector3_1.y, (int) vector3_2.y), Math.Max((int) vector3_3.y, (int) vector3_4.y));
    for (int index1 = Math.Min(Math.Min((int) vector3_1.y, (int) vector3_2.y), Math.Min((int) vector3_3.y, (int) vector3_4.y)); index1 < num11; ++index1)
    {
      for (int index2 = num10; index2 < num9; ++index2)
      {
        double num12 = (double) (index2 - x1) * Math.Cos(num2) - (double) (index1 - y1) * Math.Sin(num2) + (double) x1;
        double num13 = (double) (index2 - x1) * Math.Sin(num2) + (double) (index1 - y1) * Math.Cos(num2) + (double) y1;
        double a1 = (num12 - (double) x1) / num1 + (double) x1;
        double num14 = (double) y1;
        double a2 = (num13 - num14) / num1 + (double) y1;
        if (renderMode == 0 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int num15 = (int) a1;
          int num16 = (int) a2;
          Color32 c = new Color32();
          double num17 = 0.0;
          double num18 = 0.0;
          double num19 = 0.0;
          double num20 = 0.0;
          for (int index3 = num16 - 1; index3 <= num16 + 2; ++index3)
          {
            for (int index4 = num15 - 1; index4 <= num15 + 2; ++index4)
            {
              double num21 = Math.Abs(a1 - (double) index4);
              double num22 = Math.Abs(a2 - (double) index3);
              double num23 = num21 > 1.0 ? num3 * num21 * num21 * num21 - num6 * num21 * num21 + num7 * num21 - num8 : num4 * num21 * num21 * num21 - num5 * num21 * num21 + 1.0;
              double num24 = num22 > 1.0 ? num3 * num22 * num22 * num22 - num6 * num22 * num22 + num7 * num22 - num8 : num4 * num22 * num22 * num22 - num5 * num22 * num22 + 1.0;
              int x2 = index4;
              if (x2 < 0 || x2 > width - 1)
                x2 = num15;
              int y2 = index3;
              if (y2 < 0 || y2 > height - 1)
                y2 = num16;
              num17 += (double) src.get_Item(x2, y2).a * num23 * num24;
              num18 += (double) src.get_Item(x2, y2).r * num23 * num24;
              num19 += (double) src.get_Item(x2, y2).g * num23 * num24;
              num20 += (double) src.get_Item(x2, y2).b * num23 * num24;
            }
          }
          if (num17 > 254.0)
            num17 = (double) byte.MaxValue;
          if (num17 < 0.0)
            num17 = 0.0;
          if (num18 > 254.0)
            num18 = (double) byte.MaxValue;
          if (num18 < 0.0)
            num18 = 0.0;
          if (num19 > 254.0)
            num19 = (double) byte.MaxValue;
          if (num19 < 0.0)
            num19 = 0.0;
          if (num20 > 254.0)
            num20 = (double) byte.MaxValue;
          if (num20 < 0.0)
            num20 = 0.0;
          c.a = (byte) num17;
          c.r = (byte) num18;
          c.g = (byte) num19;
          c.b = (byte) num20;
          if (forceAlpha)
            c.a = c.a >= (byte) 40 ? byte.MaxValue : (byte) 0;
          if (c.a != (byte) 0)
          {
            if (infront || !dst.BitBltPixelNotAlpha(index2 + x - x1, index1 + y))
              dst.UpdatePixel(index2 + x - x1, index1 + y, c);
          }
          else
            continue;
        }
        if (renderMode == 1 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int x3 = (int) a1;
          if (x3 < 0)
            x3 = 0;
          int x4 = x3 + 1;
          if (x4 > width - 1)
            x4 = x3;
          int y3 = (int) a2;
          if (y3 < 0)
            y3 = 0;
          int y4 = y3 + 1;
          if (y4 > height - 1)
            y4 = y3;
          double num25 = a1 - (double) x3;
          double num26 = 1.0 - num25;
          double num27 = a2 - (double) y3;
          double num28 = 1.0 - num27;
          Color32 c = new Color32();
          Color32 color32_1 = src.get_Item(x3, y3);
          Color32 color32_2 = src.get_Item(x4, y3);
          Color32 color32_3 = src.get_Item(x3, y4);
          Color32 color32_4 = src.get_Item(x4, y4);
          double num29 = (double) color32_1.r * num26 + (double) color32_2.r * num25;
          double num30 = (double) color32_3.r * num26 + (double) color32_4.r * num25;
          c.r = (byte) (num29 * num28 + num30 * num27);
          double num31 = (double) color32_1.g * num26 + (double) color32_2.g * num25;
          double num32 = (double) color32_3.g * num26 + (double) color32_4.g * num25;
          c.g = (byte) (num31 * num28 + num32 * num27);
          double num33 = (double) color32_1.b * num26 + (double) color32_2.b * num25;
          double num34 = (double) color32_3.b * num26 + (double) color32_4.b * num25;
          c.b = (byte) (num33 * num28 + num34 * num27);
          double num35 = (double) color32_1.a * num26 + (double) color32_2.a * num25;
          double num36 = (double) color32_3.a * num26 + (double) color32_4.a * num25;
          c.a = (byte) (num35 * num28 + num36 * num27);
          if (forceAlpha)
            c.a = c.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (c.a != (byte) 0)
          {
            if (infront || !dst.BitBltPixelNotAlpha(index2 + x - x1, index1 + y))
              dst.UpdatePixel(index2 + x - x1, index1 + y, c);
          }
          else
            continue;
        }
        if (renderMode == 2 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int x5 = (int) Math.Round(a1);
          int y5 = (int) Math.Round(a2);
          if (x5 >= 0 && x5 < width && y5 >= 0 && y5 < height)
          {
            Color32 c = src.get_Item(x5, y5);
            if (c.a != (byte) 0 && (infront || !dst.BitBltPixelNotAlpha(index2 + x - x1, index1 + y)))
              dst.UpdatePixel(index2 + x - x1, index1 + y, c);
          }
        }
      }
    }
    dst.Apply();
  }

  public static void Render(
    ZMap dst,
    Surface src,
    int x,
    int y,
    FixedInt angle,
    int renderMode,
    bool infront,
    bool forceAlpha,
    FixedInt scale,
    bool apply = true)
  {
    while (angle < 0)
      angle += 360;
    while (angle > 360)
      angle -= 360;
    int xInt = src.width / 2;
    int yInt = src.height / 2;
    FixedInt fixedInt1 = scale;
    FixedInt x1 = FixedInt.Pi * angle / 180;
    FixedInt fixedInt2 = (FixedInt) -786432L;
    FixedInt fixedInt3 = fixedInt2 + 2;
    FixedInt fixedInt4 = fixedInt2 + 3;
    FixedInt fixedInt5 = (FixedInt) 5 * fixedInt2;
    FixedInt fixedInt6 = (FixedInt) 8 * fixedInt2;
    FixedInt fixedInt7 = (FixedInt) 4 * fixedInt2;
    int width = dst.Width;
    int height = dst.Height;
    MyLocation centerPoint = new MyLocation(xInt, yInt);
    FixedInt angleInDegrees = -angle;
    MyLocation myLocation1 = RotateImage.RotatePoint(new MyLocation(-src.width, -src.height), centerPoint, angleInDegrees);
    MyLocation myLocation2 = RotateImage.RotatePoint(new MyLocation(src.width, -src.height), centerPoint, angleInDegrees);
    MyLocation myLocation3 = RotateImage.RotatePoint(new MyLocation(-src.width, src.height), centerPoint, angleInDegrees);
    MyLocation myLocation4 = RotateImage.RotatePoint(new MyLocation(src.width, src.height), centerPoint, angleInDegrees);
    int num1 = (int) FixedInt.Max(FixedInt.Max((FixedInt) (int) myLocation1.x, (FixedInt) (int) myLocation2.x), FixedInt.Max((FixedInt) (int) myLocation3.x, (FixedInt) (int) myLocation4.x));
    int num2 = (int) FixedInt.Min(FixedInt.Min((FixedInt) (int) myLocation1.x, (FixedInt) (int) myLocation2.x), FixedInt.Min((FixedInt) (int) myLocation3.x, (FixedInt) (int) myLocation4.x));
    int num3 = (int) FixedInt.Max(FixedInt.Max((FixedInt) (int) myLocation1.y, (FixedInt) (int) myLocation2.y), FixedInt.Max((FixedInt) (int) myLocation3.y, (FixedInt) (int) myLocation4.y));
    for (int index1 = (int) FixedInt.Min(FixedInt.Min((FixedInt) (int) myLocation1.y, (FixedInt) (int) myLocation2.y), FixedInt.Min((FixedInt) (int) myLocation3.y, (FixedInt) (int) myLocation4.y)); index1 < num3; ++index1)
    {
      for (int index2 = num2; index2 < num1; ++index2)
      {
        FixedInt fixedInt8 = (FixedInt) (index2 - xInt) * FixedInt.Cos(x1) - (FixedInt) (index1 - yInt) * FixedInt.Sin(x1) + (FixedInt) xInt;
        FixedInt fixedInt9 = (FixedInt) (index2 - xInt) * FixedInt.Sin(x1) + (FixedInt) (index1 - yInt) * FixedInt.Cos(x1) + (FixedInt) yInt;
        FixedInt a1 = (fixedInt8 - (FixedInt) xInt) / fixedInt1 + (FixedInt) xInt;
        FixedInt fixedInt10 = (FixedInt) yInt;
        FixedInt a2 = (fixedInt9 - fixedInt10) / fixedInt1 + (FixedInt) yInt;
        if (renderMode == 0 && a1 >= 0 && a1 < (FixedInt) width && a2 >= 0 && a2 < (FixedInt) height)
        {
          int num4 = (int) a1;
          int num5 = (int) a2;
          Color32 c = new Color32();
          FixedInt fixedInt11 = (FixedInt) 0;
          FixedInt fixedInt12 = (FixedInt) 0;
          FixedInt fixedInt13 = (FixedInt) 0;
          FixedInt fixedInt14 = (FixedInt) 0;
          for (int index3 = num5 - 1; index3 <= num5 + 2; ++index3)
          {
            for (int index4 = num4 - 1; index4 <= num4 + 2; ++index4)
            {
              FixedInt fixedInt15 = FixedInt.Abs(a1 - (FixedInt) index4);
              FixedInt fixedInt16 = FixedInt.Abs(a2 - (FixedInt) index3);
              FixedInt fixedInt17 = fixedInt15 > 1 ? fixedInt2 * fixedInt15 * fixedInt15 * fixedInt15 - fixedInt5 * fixedInt15 * fixedInt15 + fixedInt6 * fixedInt15 - fixedInt7 : fixedInt3 * fixedInt15 * fixedInt15 * fixedInt15 - fixedInt4 * fixedInt15 * fixedInt15 + 1;
              FixedInt fixedInt18 = fixedInt16 > 1 ? fixedInt2 * fixedInt16 * fixedInt16 * fixedInt16 - fixedInt5 * fixedInt16 * fixedInt16 + fixedInt6 * fixedInt16 - fixedInt7 : fixedInt3 * fixedInt16 * fixedInt16 * fixedInt16 - fixedInt4 * fixedInt16 * fixedInt16 + 1;
              int x2 = index4;
              if (x2 < 0 || x2 > width - 1)
                x2 = num4;
              int y1 = index3;
              if (y1 < 0 || y1 > height - 1)
                y1 = num5;
              fixedInt11 += (FixedInt) (int) src.get_Item(x2, y1).a * fixedInt17 * fixedInt18;
              fixedInt12 += (FixedInt) (int) src.get_Item(x2, y1).r * fixedInt17 * fixedInt18;
              fixedInt13 += (FixedInt) (int) src.get_Item(x2, y1).g * fixedInt17 * fixedInt18;
              fixedInt14 += (FixedInt) (int) src.get_Item(x2, y1).b * fixedInt17 * fixedInt18;
            }
          }
          if (fixedInt11 > 254)
            fixedInt11 = (FixedInt) (int) byte.MaxValue;
          if (fixedInt11 < 0)
            fixedInt11 = (FixedInt) 0;
          if (fixedInt12 > 254)
            fixedInt12 = (FixedInt) (int) byte.MaxValue;
          if (fixedInt12 < 0)
            fixedInt12 = (FixedInt) 0;
          if (fixedInt13 > 254)
            fixedInt13 = (FixedInt) (int) byte.MaxValue;
          if (fixedInt13 < 0)
            fixedInt13 = (FixedInt) 0;
          if (fixedInt14 > 254)
            fixedInt14 = (FixedInt) (int) byte.MaxValue;
          if (fixedInt14 < 0)
            fixedInt14 = (FixedInt) 0;
          c.a = (byte) (int) fixedInt11;
          c.r = (byte) (int) fixedInt12;
          c.g = (byte) (int) fixedInt13;
          c.b = (byte) (int) fixedInt14;
          if (forceAlpha)
            c.a = c.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (c.a != (byte) 0)
          {
            int x3 = index2 + x - xInt;
            int y2 = index1 + y;
            if (x3 >= 0 && x3 < dst.Width && y2 >= 0 && y2 < dst.Height && (infront || !dst.BitBltPixelNotAlpha(x3, y2)))
              dst.UpdatePixel(x3, y2, c);
          }
          else
            continue;
        }
        if (renderMode == 1 && a1 >= 0 && a1 < (FixedInt) width && a2 >= 0 && a2 < (FixedInt) height)
        {
          int x4 = (int) a1;
          if (x4 < 0)
            x4 = 0;
          int x5 = x4 + 1;
          if (x5 > width - 1)
            x5 = x4;
          int y3 = (int) a2;
          if (y3 < 0)
            y3 = 0;
          int y4 = y3 + 1;
          if (y4 > height - 1)
            y4 = y3;
          FixedInt fixedInt19 = a1 - (FixedInt) x4;
          FixedInt fixedInt20 = (FixedInt) 1 - fixedInt19;
          FixedInt fixedInt21 = a2 - (FixedInt) y3;
          FixedInt fixedInt22 = (FixedInt) 1 - fixedInt21;
          Color32 c = new Color32();
          Color32 color32_1 = src.get_Item(x4, y3);
          Color32 color32_2 = src.get_Item(x5, y3);
          Color32 color32_3 = src.get_Item(x4, y4);
          Color32 color32_4 = src.get_Item(x5, y4);
          FixedInt fixedInt23 = (FixedInt) (int) color32_1.r * fixedInt20 + (FixedInt) (int) color32_2.r * fixedInt19;
          FixedInt fixedInt24 = (FixedInt) (int) color32_3.r * fixedInt20 + (FixedInt) (int) color32_4.r * fixedInt19;
          c.r = (byte) (int) (fixedInt23 * fixedInt22 + fixedInt24 * fixedInt21);
          FixedInt fixedInt25 = (FixedInt) (int) color32_1.g * fixedInt20 + (FixedInt) (int) color32_2.g * fixedInt19;
          FixedInt fixedInt26 = (FixedInt) (int) color32_3.g * fixedInt20 + (FixedInt) (int) color32_4.g * fixedInt19;
          c.g = (byte) (int) (fixedInt25 * fixedInt22 + fixedInt26 * fixedInt21);
          FixedInt fixedInt27 = (FixedInt) (int) color32_1.b * fixedInt20 + (FixedInt) (int) color32_2.b * fixedInt19;
          FixedInt fixedInt28 = (FixedInt) (int) color32_3.b * fixedInt20 + (FixedInt) (int) color32_4.b * fixedInt19;
          c.b = (byte) (int) (fixedInt27 * fixedInt22 + fixedInt28 * fixedInt21);
          FixedInt fixedInt29 = (FixedInt) (int) color32_1.a * fixedInt20 + (FixedInt) (int) color32_2.a * fixedInt19;
          FixedInt fixedInt30 = (FixedInt) (int) color32_3.a * fixedInt20 + (FixedInt) (int) color32_4.a * fixedInt19;
          c.a = (byte) (int) (fixedInt29 * fixedInt22 + fixedInt30 * fixedInt21);
          if (forceAlpha)
            c.a = c.a >= (byte) 100 ? byte.MaxValue : (byte) 0;
          if (c.a != (byte) 0)
          {
            int x6 = index2 + x - xInt;
            int y5 = index1 + y;
            if (x6 >= 0 && x6 < dst.Width && y5 >= 0 && y5 < dst.Height && (infront || !dst.BitBltPixelNotAlpha(x6, y5)))
              dst.UpdatePixel(x6, y5, c);
          }
          else
            continue;
        }
        if (renderMode == 2 && a1 >= 0 && a1 < (FixedInt) width && a2 >= 0 && a2 < (FixedInt) height)
        {
          int x7 = (int) FixedInt.Round(a1);
          int y6 = (int) FixedInt.Round(a2);
          if (x7 >= 0 && x7 < width && y6 >= 0 && y6 < height)
          {
            Color32 c = src.get_Item(x7, y6);
            if (c.a != (byte) 0)
            {
              int x8 = index2 + x - xInt;
              int y7 = index1 + y;
              if (x8 >= 0 && x8 < dst.Width && y7 >= 0 && y7 < dst.Height && (infront || !dst.BitBltPixelNotAlpha(x8, y7)))
                dst.UpdatePixel(x8, y7, c);
            }
          }
        }
      }
    }
    if (!apply)
      return;
    dst.Apply();
  }

  public static void Render(
    Surface dst,
    Surface src,
    int x,
    int y,
    float angle,
    int renderMode,
    bool infront,
    bool forceAlpha = true,
    float scale = 1f)
  {
    int x1 = src.width / 2;
    int y1 = 0;
    double num1 = (double) scale;
    double num2 = Math.PI * (double) angle / 180.0;
    double num3 = -0.75;
    double num4 = num3 + 2.0;
    double num5 = num3 + 3.0;
    double num6 = 5.0 * num3;
    double num7 = 8.0 * num3;
    double num8 = 4.0 * num3;
    int width = dst.width;
    int height = dst.height;
    Vector3 pivot = new Vector3((float) x1, (float) y1);
    Vector3 angles = new Vector3(0.0f, 0.0f, -angle);
    Vector3 vector3_1 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, 0.0f), pivot, angles);
    Vector3 vector3_2 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, 0.0f), pivot, angles);
    Vector3 vector3_3 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, (float) src.height), pivot, angles);
    Vector3 vector3_4 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, (float) src.height), pivot, angles);
    int num9 = Math.Max(Math.Max((int) vector3_1.x, (int) vector3_2.x), Math.Max((int) vector3_3.x, (int) vector3_4.x));
    int num10 = Math.Min(Math.Min((int) vector3_1.x, (int) vector3_2.x), Math.Min((int) vector3_3.x, (int) vector3_4.x));
    int num11 = Math.Max(Math.Max((int) vector3_1.y, (int) vector3_2.y), Math.Max((int) vector3_3.y, (int) vector3_4.y));
    for (int index1 = Math.Min(Math.Min((int) vector3_1.y, (int) vector3_2.y), Math.Min((int) vector3_3.y, (int) vector3_4.y)); index1 < num11; ++index1)
    {
      for (int index2 = num10; index2 < num9; ++index2)
      {
        double num12 = (double) (index2 - x1) * Math.Cos(num2) - (double) (index1 - y1) * Math.Sin(num2) + (double) x1;
        double num13 = (double) (index2 - x1) * Math.Sin(num2) + (double) (index1 - y1) * Math.Cos(num2) + (double) y1;
        double a1 = (num12 - (double) x1) / num1 + (double) x1;
        double num14 = (double) y1;
        double a2 = (num13 - num14) / num1 + (double) y1;
        if (renderMode == 0 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int num15 = (int) a1;
          int num16 = (int) a2;
          Color32 c = new Color32();
          double num17 = 0.0;
          double num18 = 0.0;
          double num19 = 0.0;
          double num20 = 0.0;
          for (int index3 = num16 - 1; index3 <= num16 + 2; ++index3)
          {
            for (int index4 = num15 - 1; index4 <= num15 + 2; ++index4)
            {
              double num21 = Math.Abs(a1 - (double) index4);
              double num22 = Math.Abs(a2 - (double) index3);
              double num23 = num21 > 1.0 ? num3 * num21 * num21 * num21 - num6 * num21 * num21 + num7 * num21 - num8 : num4 * num21 * num21 * num21 - num5 * num21 * num21 + 1.0;
              double num24 = num22 > 1.0 ? num3 * num22 * num22 * num22 - num6 * num22 * num22 + num7 * num22 - num8 : num4 * num22 * num22 * num22 - num5 * num22 * num22 + 1.0;
              int x2 = index4;
              if (x2 < 0 || x2 > width - 1)
                x2 = num15;
              int y2 = index3;
              if (y2 < 0 || y2 > height - 1)
                y2 = num16;
              num17 += (double) src.get_Item(x2, y2).a * num23 * num24;
              num18 += (double) src.get_Item(x2, y2).r * num23 * num24;
              num19 += (double) src.get_Item(x2, y2).g * num23 * num24;
              num20 += (double) src.get_Item(x2, y2).b * num23 * num24;
            }
          }
          if (num17 > 254.0)
            num17 = (double) byte.MaxValue;
          if (num17 < 0.0)
            num17 = 0.0;
          if (num18 > 254.0)
            num18 = (double) byte.MaxValue;
          if (num18 < 0.0)
            num18 = 0.0;
          if (num19 > 254.0)
            num19 = (double) byte.MaxValue;
          if (num19 < 0.0)
            num19 = 0.0;
          if (num20 > 254.0)
            num20 = (double) byte.MaxValue;
          if (num20 < 0.0)
            num20 = 0.0;
          c.a = (byte) num17;
          c.r = (byte) num18;
          c.g = (byte) num19;
          c.b = (byte) num20;
          if (forceAlpha)
            c.a = c.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
            dst.set_Item(index2 + x - x1, index1 + y, c);
        }
        if (renderMode == 1 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int x3 = (int) a1;
          if (x3 < 0)
            x3 = 0;
          int x4 = x3 + 1;
          if (x4 > width - 1)
            x4 = x3;
          int y3 = (int) a2;
          if (y3 < 0)
            y3 = 0;
          int y4 = y3 + 1;
          if (y4 > height - 1)
            y4 = y3;
          double num25 = a1 - (double) x3;
          double num26 = 1.0 - num25;
          double num27 = a2 - (double) y3;
          double num28 = 1.0 - num27;
          Color32 c = new Color32();
          Color32 color32_1 = src.get_Item(x3, y3);
          Color32 color32_2 = src.get_Item(x4, y3);
          Color32 color32_3 = src.get_Item(x3, y4);
          Color32 color32_4 = src.get_Item(x4, y4);
          double num29 = (double) color32_1.r * num26 + (double) color32_2.r * num25;
          double num30 = (double) color32_3.r * num26 + (double) color32_4.r * num25;
          c.r = (byte) (num29 * num28 + num30 * num27);
          double num31 = (double) color32_1.g * num26 + (double) color32_2.g * num25;
          double num32 = (double) color32_3.g * num26 + (double) color32_4.g * num25;
          c.g = (byte) (num31 * num28 + num32 * num27);
          double num33 = (double) color32_1.b * num26 + (double) color32_2.b * num25;
          double num34 = (double) color32_3.b * num26 + (double) color32_4.b * num25;
          c.b = (byte) (num33 * num28 + num34 * num27);
          double num35 = (double) color32_1.a * num26 + (double) color32_2.a * num25;
          double num36 = (double) color32_3.a * num26 + (double) color32_4.a * num25;
          c.a = (byte) (num35 * num28 + num36 * num27);
          if (forceAlpha)
            c.a = c.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
            dst.set_Item(index2 + x - x1, index1 + y, c);
        }
        if (renderMode == 2 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int x5 = (int) Math.Round(a1);
          int y5 = (int) Math.Round(a2);
          if (x5 >= 0 && x5 < width && y5 >= 0 && y5 < height)
          {
            Color32 c = src.get_Item(x5, y5);
            if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
              dst.set_Item(index2 + x - x1, index1 + y, c);
          }
        }
      }
    }
    dst.Apply();
  }

  public static void RenderOverlay(
    Surface dst,
    Surface src,
    int x,
    int y,
    float angle,
    int renderMode,
    bool infront,
    bool forceAlpha = true,
    float scale = 1f)
  {
    int x1 = src.width / 2;
    int y1 = 0;
    double num1 = (double) scale;
    double num2 = Math.PI * (double) angle / 180.0;
    double num3 = -0.75;
    double num4 = num3 + 2.0;
    double num5 = num3 + 3.0;
    double num6 = 5.0 * num3;
    double num7 = 8.0 * num3;
    double num8 = 4.0 * num3;
    int width = dst.width;
    int height = dst.height;
    Vector3 pivot = new Vector3((float) x1, (float) y1);
    Vector3 angles = new Vector3(0.0f, 0.0f, -angle);
    Vector3 vector3_1 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, 0.0f), pivot, angles);
    Vector3 vector3_2 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, 0.0f), pivot, angles);
    Vector3 vector3_3 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, (float) src.height), pivot, angles);
    Vector3 vector3_4 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, (float) src.height), pivot, angles);
    int num9 = Math.Max(Math.Max((int) vector3_1.x, (int) vector3_2.x), Math.Max((int) vector3_3.x, (int) vector3_4.x));
    int num10 = Math.Min(Math.Min((int) vector3_1.x, (int) vector3_2.x), Math.Min((int) vector3_3.x, (int) vector3_4.x));
    int num11 = Math.Max(Math.Max((int) vector3_1.y, (int) vector3_2.y), Math.Max((int) vector3_3.y, (int) vector3_4.y));
    for (int index1 = Math.Min(Math.Min((int) vector3_1.y, (int) vector3_2.y), Math.Min((int) vector3_3.y, (int) vector3_4.y)); index1 < num11; ++index1)
    {
      for (int index2 = num10; index2 < num9; ++index2)
      {
        double num12 = (double) (index2 - x1) * Math.Cos(num2) - (double) (index1 - y1) * Math.Sin(num2) + (double) x1;
        double num13 = (double) (index2 - x1) * Math.Sin(num2) + (double) (index1 - y1) * Math.Cos(num2) + (double) y1;
        double a1 = (num12 - (double) x1) / num1 + (double) x1;
        double num14 = (double) y1;
        double a2 = (num13 - num14) / num1 + (double) y1;
        if (renderMode == 0 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int num15 = (int) a1;
          int num16 = (int) a2;
          Color32 color32 = new Color32();
          double num17 = 0.0;
          double num18 = 0.0;
          double num19 = 0.0;
          double num20 = 0.0;
          for (int index3 = num16 - 1; index3 <= num16 + 2; ++index3)
          {
            for (int index4 = num15 - 1; index4 <= num15 + 2; ++index4)
            {
              double num21 = Math.Abs(a1 - (double) index4);
              double num22 = Math.Abs(a2 - (double) index3);
              double num23 = num21 > 1.0 ? num3 * num21 * num21 * num21 - num6 * num21 * num21 + num7 * num21 - num8 : num4 * num21 * num21 * num21 - num5 * num21 * num21 + 1.0;
              double num24 = num22 > 1.0 ? num3 * num22 * num22 * num22 - num6 * num22 * num22 + num7 * num22 - num8 : num4 * num22 * num22 * num22 - num5 * num22 * num22 + 1.0;
              int x2 = index4;
              if (x2 < 0 || x2 > width - 1)
                x2 = num15;
              int y2 = index3;
              if (y2 < 0 || y2 > height - 1)
                y2 = num16;
              num17 += (double) src.get_Item(x2, y2).a * num23 * num24;
              num18 += (double) src.get_Item(x2, y2).r * num23 * num24;
              num19 += (double) src.get_Item(x2, y2).g * num23 * num24;
              num20 += (double) src.get_Item(x2, y2).b * num23 * num24;
            }
          }
          if (num17 > 254.0)
            num17 = (double) byte.MaxValue;
          if (num17 < 0.0)
            num17 = 0.0;
          if (num18 > 254.0)
            num18 = (double) byte.MaxValue;
          if (num18 < 0.0)
            num18 = 0.0;
          if (num19 > 254.0)
            num19 = (double) byte.MaxValue;
          if (num19 < 0.0)
            num19 = 0.0;
          if (num20 > 254.0)
            num20 = (double) byte.MaxValue;
          if (num20 < 0.0)
            num20 = 0.0;
          color32.a = (byte) num17;
          color32.r = (byte) num18;
          color32.g = (byte) num19;
          color32.b = (byte) num20;
          if (forceAlpha)
            color32.a = color32.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
          {
            if (color32.a < byte.MaxValue && dst.get_Item(index2 + x - x1, index1 + y).a > (byte) 0)
              dst.set_Item(index2 + x - x1, index1 + y, Global.CombineColors((Color) dst.get_Item(index2 + x - x1, index1 + y), (Color) color32));
            else
              dst.set_Item(index2 + x - x1, index1 + y, color32);
          }
        }
        if (renderMode == 1 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int x3 = (int) a1;
          if (x3 < 0)
            x3 = 0;
          int x4 = x3 + 1;
          if (x4 > width - 1)
            x4 = x3;
          int y3 = (int) a2;
          if (y3 < 0)
            y3 = 0;
          int y4 = y3 + 1;
          if (y4 > height - 1)
            y4 = y3;
          double num25 = a1 - (double) x3;
          double num26 = 1.0 - num25;
          double num27 = a2 - (double) y3;
          double num28 = 1.0 - num27;
          Color32 color32_1 = new Color32();
          Color32 color32_2 = src.get_Item(x3, y3);
          Color32 color32_3 = src.get_Item(x4, y3);
          Color32 color32_4 = src.get_Item(x3, y4);
          Color32 color32_5 = src.get_Item(x4, y4);
          double num29 = (double) color32_2.r * num26 + (double) color32_3.r * num25;
          double num30 = (double) color32_4.r * num26 + (double) color32_5.r * num25;
          color32_1.r = (byte) (num29 * num28 + num30 * num27);
          double num31 = (double) color32_2.g * num26 + (double) color32_3.g * num25;
          double num32 = (double) color32_4.g * num26 + (double) color32_5.g * num25;
          color32_1.g = (byte) (num31 * num28 + num32 * num27);
          double num33 = (double) color32_2.b * num26 + (double) color32_3.b * num25;
          double num34 = (double) color32_4.b * num26 + (double) color32_5.b * num25;
          color32_1.b = (byte) (num33 * num28 + num34 * num27);
          double num35 = (double) color32_2.a * num26 + (double) color32_3.a * num25;
          double num36 = (double) color32_4.a * num26 + (double) color32_5.a * num25;
          color32_1.a = (byte) (num35 * num28 + num36 * num27);
          if (forceAlpha)
            color32_1.a = color32_1.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
          {
            if (color32_1.a < byte.MaxValue && dst.get_Item(index2 + x - x1, index1 + y).a > (byte) 0)
              dst.set_Item(index2 + x - x1, index1 + y, Global.CombineColors((Color) dst.get_Item(index2 + x - x1, index1 + y), (Color) color32_1));
            else
              dst.set_Item(index2 + x - x1, index1 + y, color32_1);
          }
        }
        if (renderMode == 2 && a1 >= 0.0 && a1 < (double) width && a2 >= 0.0 && a2 < (double) height)
        {
          int x5 = (int) Math.Round(a1);
          int y5 = (int) Math.Round(a2);
          if (x5 >= 0 && x5 < width && y5 >= 0 && y5 < height)
          {
            Color32 color32 = src.get_Item(x5, y5);
            if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
            {
              if (color32.a < byte.MaxValue && dst.get_Item(index2 + x - x1, index1 + y).a > (byte) 0)
                dst.set_Item(index2 + x - x1, index1 + y, Global.CombineColors((Color) dst.get_Item(index2 + x - x1, index1 + y), (Color) color32));
              else
                dst.set_Item(index2 + x - x1, index1 + y, color32);
            }
          }
        }
      }
    }
    dst.Apply();
  }

  public static void RenderHUE(
    ZMap dst,
    Surface src,
    int x,
    int y,
    float angle,
    bool infront,
    float h,
    float scale = 1f)
  {
    int x1 = src.width / 2;
    int y1 = 0;
    double num1 = (double) scale;
    double num2 = Math.PI * (double) angle / 180.0;
    int width = dst.Width;
    int height = dst.Height;
    Vector3 pivot = new Vector3((float) x1, (float) y1);
    Vector3 angles = new Vector3(0.0f, 0.0f, -angle);
    Vector3 vector3_1 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, 0.0f), pivot, angles);
    Vector3 vector3_2 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, 0.0f), pivot, angles);
    Vector3 vector3_3 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, (float) src.height), pivot, angles);
    Vector3 vector3_4 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, (float) src.height), pivot, angles);
    int num3 = Math.Max(Math.Max((int) vector3_1.x, (int) vector3_2.x), Math.Max((int) vector3_3.x, (int) vector3_4.x));
    int num4 = Math.Min(Math.Min((int) vector3_1.x, (int) vector3_2.x), Math.Min((int) vector3_3.x, (int) vector3_4.x));
    int num5 = Math.Max(Math.Max((int) vector3_1.y, (int) vector3_2.y), Math.Max((int) vector3_3.y, (int) vector3_4.y));
    for (int index1 = Math.Min(Math.Min((int) vector3_1.y, (int) vector3_2.y), Math.Min((int) vector3_3.y, (int) vector3_4.y)); index1 < num5; ++index1)
    {
      for (int index2 = num4; index2 < num3; ++index2)
      {
        double num6 = (double) (index2 - x1) * Math.Cos(num2) - (double) (index1 - y1) * Math.Sin(num2) + (double) x1;
        double num7 = (double) (index2 - x1) * Math.Sin(num2) + (double) (index1 - y1) * Math.Cos(num2) + (double) y1;
        double num8 = (num6 - (double) x1) / num1 + (double) x1;
        double num9 = (double) y1;
        double num10 = (num7 - num9) / num1 + (double) y1;
        if (num8 >= 0.0 && num8 < (double) width && num10 >= 0.0 && num10 < (double) height)
        {
          int x2 = (int) num8;
          if (x2 < 0)
            x2 = 0;
          int x3 = x2 + 1;
          if (x3 > width - 1)
            x3 = x2;
          int y2 = (int) num10;
          if (y2 < 0)
            y2 = 0;
          int y3 = y2 + 1;
          if (y3 > height - 1)
            y3 = y2;
          double num11 = num8 - (double) x2;
          double num12 = 1.0 - num11;
          double num13 = num10 - (double) y2;
          double num14 = 1.0 - num13;
          Color32 c = new Color32();
          Color32 color32_1 = src.get_Item(x2, y2);
          Color32 color32_2 = src.get_Item(x3, y2);
          Color32 color32_3 = src.get_Item(x2, y3);
          Color32 color32_4 = src.get_Item(x3, y3);
          double num15 = (double) color32_1.r * num12 + (double) color32_2.r * num11;
          double num16 = (double) color32_3.r * num12 + (double) color32_4.r * num11;
          c.r = (byte) (num15 * num14 + num16 * num13);
          double num17 = (double) color32_1.g * num12 + (double) color32_2.g * num11;
          double num18 = (double) color32_3.g * num12 + (double) color32_4.g * num11;
          c.g = (byte) (num17 * num14 + num18 * num13);
          double num19 = (double) color32_1.b * num12 + (double) color32_2.b * num11;
          double num20 = (double) color32_3.b * num12 + (double) color32_4.b * num11;
          c.b = (byte) (num19 * num14 + num20 * num13);
          double num21 = (double) color32_1.a * num12 + (double) color32_2.a * num11;
          double num22 = (double) color32_3.a * num12 + (double) color32_4.a * num11;
          c.a = (byte) (num21 * num14 + num22 * num13);
          c.a = c.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.GetPixel(index2 + x - x1, index1 + y).a == (byte) 0)
          {
            Color32 p = (Color32) ColorHSV.AdjustColor((Color) c, h);
            dst.SetPixelColor(index2 + x - x1, index1 + y, p);
          }
        }
      }
    }
    dst.Apply();
  }

  public static void RenderHUE(
    Surface dst,
    Surface src,
    int x,
    int y,
    float angle,
    bool infront,
    float h,
    float scale = 1f,
    float saturation = -0.5f)
  {
    int x1 = src.width / 2;
    int y1 = 0;
    double num1 = (double) scale;
    double num2 = Math.PI * (double) angle / 180.0;
    int width = dst.width;
    int height = dst.height;
    Vector3 pivot = new Vector3((float) x1, (float) y1);
    Vector3 angles = new Vector3(0.0f, 0.0f, -angle);
    Vector3 vector3_1 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, 0.0f), pivot, angles);
    Vector3 vector3_2 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, 0.0f), pivot, angles);
    Vector3 vector3_3 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, (float) src.height), pivot, angles);
    Vector3 vector3_4 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, (float) src.height), pivot, angles);
    int num3 = Math.Max(Math.Max((int) vector3_1.x, (int) vector3_2.x), Math.Max((int) vector3_3.x, (int) vector3_4.x));
    int num4 = Math.Min(Math.Min((int) vector3_1.x, (int) vector3_2.x), Math.Min((int) vector3_3.x, (int) vector3_4.x));
    int num5 = Math.Max(Math.Max((int) vector3_1.y, (int) vector3_2.y), Math.Max((int) vector3_3.y, (int) vector3_4.y));
    for (int index1 = Math.Min(Math.Min((int) vector3_1.y, (int) vector3_2.y), Math.Min((int) vector3_3.y, (int) vector3_4.y)); index1 < num5; ++index1)
    {
      for (int index2 = num4; index2 < num3; ++index2)
      {
        double num6 = (double) (index2 - x1) * Math.Cos(num2) - (double) (index1 - y1) * Math.Sin(num2) + (double) x1;
        double num7 = (double) (index2 - x1) * Math.Sin(num2) + (double) (index1 - y1) * Math.Cos(num2) + (double) y1;
        double num8 = (num6 - (double) x1) / num1 + (double) x1;
        double num9 = (double) y1;
        double num10 = (num7 - num9) / num1 + (double) y1;
        if (num8 >= 0.0 && num8 < (double) width && num10 >= 0.0 && num10 < (double) height)
        {
          int x2 = (int) num8;
          if (x2 < 0)
            x2 = 0;
          int x3 = x2 + 1;
          if (x3 > width - 1)
            x3 = x2;
          int y2 = (int) num10;
          if (y2 < 0)
            y2 = 0;
          int y3 = y2 + 1;
          if (y3 > height - 1)
            y3 = y2;
          double num11 = num8 - (double) x2;
          double num12 = 1.0 - num11;
          double num13 = num10 - (double) y2;
          double num14 = 1.0 - num13;
          Color32 c1 = new Color32();
          Color32 color32_1 = src.get_Item(x2, y2);
          Color32 color32_2 = src.get_Item(x3, y2);
          Color32 color32_3 = src.get_Item(x2, y3);
          Color32 color32_4 = src.get_Item(x3, y3);
          double num15 = (double) color32_1.r * num12 + (double) color32_2.r * num11;
          double num16 = (double) color32_3.r * num12 + (double) color32_4.r * num11;
          c1.r = (byte) (num15 * num14 + num16 * num13);
          double num17 = (double) color32_1.g * num12 + (double) color32_2.g * num11;
          double num18 = (double) color32_3.g * num12 + (double) color32_4.g * num11;
          c1.g = (byte) (num17 * num14 + num18 * num13);
          double num19 = (double) color32_1.b * num12 + (double) color32_2.b * num11;
          double num20 = (double) color32_3.b * num12 + (double) color32_4.b * num11;
          c1.b = (byte) (num19 * num14 + num20 * num13);
          double num21 = (double) color32_1.a * num12 + (double) color32_2.a * num11;
          double num22 = (double) color32_3.a * num12 + (double) color32_4.a * num11;
          c1.a = (byte) (num21 * num14 + num22 * num13);
          c1.a = c1.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
          {
            Color32 c2 = (Color32) ColorHSV.AdjustColor((Color) c1, h, saturation);
            dst.set_Item(index2 + x - x1, index1 + y, c2);
          }
        }
      }
    }
    dst.Apply();
  }

  public static void RenderHUEReversed(
    Surface dst,
    Surface src,
    int x,
    int y,
    float angle,
    bool infront,
    float h,
    float scale = 1f,
    float saturation = -0.5f)
  {
    int x1 = src.width / 2;
    int y1 = 0;
    double num1 = (double) scale;
    double num2 = Math.PI * (double) angle / 180.0;
    int width = dst.width;
    int height = dst.height;
    Vector3 pivot = new Vector3((float) x1, (float) y1);
    Vector3 angles = new Vector3(0.0f, 0.0f, -angle);
    Vector3 vector3_1 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, 0.0f), pivot, angles);
    Vector3 vector3_2 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, 0.0f), pivot, angles);
    Vector3 vector3_3 = RotateImage.RotatePointAroundPivot(new Vector3(0.0f, (float) src.height), pivot, angles);
    Vector3 vector3_4 = RotateImage.RotatePointAroundPivot(new Vector3((float) src.width, (float) src.height), pivot, angles);
    int num3 = Math.Max(Math.Max((int) vector3_1.x, (int) vector3_2.x), Math.Max((int) vector3_3.x, (int) vector3_4.x));
    int num4 = Math.Min(Math.Min((int) vector3_1.x, (int) vector3_2.x), Math.Min((int) vector3_3.x, (int) vector3_4.x));
    int num5 = Math.Max(Math.Max((int) vector3_1.y, (int) vector3_2.y), Math.Max((int) vector3_3.y, (int) vector3_4.y));
    int num6 = Math.Min(Math.Min((int) vector3_1.y, (int) vector3_2.y), Math.Min((int) vector3_3.y, (int) vector3_4.y));
    int num7 = src.width - 1;
    for (int index1 = num6; index1 < num5; ++index1)
    {
      for (int index2 = num4; index2 < num3; ++index2)
      {
        double num8 = (double) (index2 - x1) * Math.Cos(num2) - (double) (index1 - y1) * Math.Sin(num2) + (double) x1;
        double num9 = (double) (index2 - x1) * Math.Sin(num2) + (double) (index1 - y1) * Math.Cos(num2) + (double) y1;
        double num10 = (num8 - (double) x1) / num1 + (double) x1;
        double num11 = (double) y1;
        double num12 = (num9 - num11) / num1 + (double) y1;
        if (num10 >= 0.0 && num10 < (double) width && num12 >= 0.0 && num12 < (double) height)
        {
          int num13 = (int) num10;
          if (num13 < 0)
            num13 = 0;
          int num14 = num13 + 1;
          if (num14 > width - 1)
            num14 = num13;
          int y2 = (int) num12;
          if (y2 < 0)
            y2 = 0;
          int y3 = y2 + 1;
          if (y3 > height - 1)
            y3 = y2;
          double num15 = num10 - (double) num13;
          double num16 = 1.0 - num15;
          double num17 = num12 - (double) y2;
          double num18 = 1.0 - num17;
          Color32 c1 = new Color32();
          Color32 color32_1 = src.get_Item(num7 - num13, y2);
          Color32 color32_2 = src.get_Item(num7 - num14, y2);
          Color32 color32_3 = src.get_Item(num7 - num13, y3);
          Color32 color32_4 = src.get_Item(num7 - num14, y3);
          double num19 = (double) color32_1.r * num16 + (double) color32_2.r * num15;
          double num20 = (double) color32_3.r * num16 + (double) color32_4.r * num15;
          c1.r = (byte) (num19 * num18 + num20 * num17);
          double num21 = (double) color32_1.g * num16 + (double) color32_2.g * num15;
          double num22 = (double) color32_3.g * num16 + (double) color32_4.g * num15;
          c1.g = (byte) (num21 * num18 + num22 * num17);
          double num23 = (double) color32_1.b * num16 + (double) color32_2.b * num15;
          double num24 = (double) color32_3.b * num16 + (double) color32_4.b * num15;
          c1.b = (byte) (num23 * num18 + num24 * num17);
          double num25 = (double) color32_1.a * num16 + (double) color32_2.a * num15;
          double num26 = (double) color32_3.a * num16 + (double) color32_4.a * num15;
          c1.a = (byte) (num25 * num18 + num26 * num17);
          c1.a = c1.a >= (byte) 150 ? byte.MaxValue : (byte) 0;
          if (infront || dst.get_Item(index2 + x - x1, index1 + y).a == (byte) 0)
          {
            Color32 c2 = (Color32) ColorHSV.AdjustColor((Color) c1, h, saturation);
            dst.set_Item(index2 + x - x1, index1 + y, c2);
          }
        }
      }
    }
    dst.Apply();
  }

  public static Color32 Purpalize(Color32 c, bool log = false)
  {
    float S;
    float V;
    Color.RGBToHSV((Color) c, out float _, out S, out V);
    return (Color32) Color.HSVToRGB(Mathf.Lerp(1f, 0.6f, V), S * 0.5f, V);
  }

  public static Color32 CallOfTheVoid(Color32 c)
  {
    float H;
    float V;
    Color.RGBToHSV((Color) c, out H, out float _, out V);
    float S = (float) ((double) V * 0.5 - 0.15000000596046448);
    if ((double) S < 0.0)
      S = 0.0f;
    return (Color32) Color.HSVToRGB(H, S, V);
  }

  public static Color32 BloodStain(Color32 c)
  {
    float S;
    float V;
    Color.RGBToHSV((Color) c, out float _, out S, out V);
    return (Color32) Color.HSVToRGB(Mathf.Lerp(0.0f, 0.1f, V), S * 0.5f, V);
  }

  public static int ColorToInt(int b, int g, int r) => b | g << 8 | r << 16;

  public static int ColorToInt(Color32 c) => (int) c.b | (int) c.g << 8 | (int) c.r << 16;

  public static Color32 intToColor(int c)
  {
    return new Color32((byte) ((c & 16711680) >> 16), (byte) ((c & 65280) >> 8), (byte) (c & (int) byte.MaxValue), byte.MaxValue);
  }

  public static Vector3 intToVec(int c)
  {
    return new Vector3((float) (c & (int) byte.MaxValue) / (float) byte.MaxValue, (float) ((c & 65280) >> 8) / (float) byte.MaxValue, (float) ((c & 16711680) >> 16) / (float) byte.MaxValue);
  }
}
