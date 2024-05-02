// Decompiled with JetBrains decompiler
// Type: MapGenerator
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class MapGenerator
{
  public static Dictionary<int, List<Coords>> cachedRadius = new Dictionary<int, List<Coords>>();
  public static Dictionary<Texture2D, List<Coords>> cachedTowerFallPoints = new Dictionary<Texture2D, List<Coords>>();
  public static Dictionary<Texture2D, List<Coords>> cachedTowerFloatPoints = new Dictionary<Texture2D, List<Coords>>();
  private static Dictionary<Texture2D, List<Coords>> cachedTowerLeftPoints = new Dictionary<Texture2D, List<Coords>>();
  private static Dictionary<Texture2D, List<Coords>> cachedTowerRightPoints = new Dictionary<Texture2D, List<Coords>>();

  public static void drawcircleOutline(int x0, int y0, int radius)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      Client.map.UpdatePixel(x0 + num2, y0 + num1, (Color32) Color.green);
      Client.map.UpdatePixel(x0 + num1, y0 + num2, (Color32) Color.red);
      Client.map.UpdatePixel(x0 + num1, y0 - num2, (Color32) Color.magenta);
      Client.map.UpdatePixel(x0 + num2, y0 - num1, (Color32) Color.gray);
      Client.map.UpdatePixel(x0 - num2, y0 - num1, (Color32) Color.clear);
      Client.map.UpdatePixel(x0 - num1, y0 - num2, (Color32) Color.white);
      Client.map.UpdatePixel(x0 - num1, y0 + num2, (Color32) Color.yellow);
      Client.map.UpdatePixel(x0 - num2, y0 + num1, (Color32) Color.blue);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    Client.map.Apply();
  }

  public static List<Coords> getOutlineArray(int radius)
  {
    List<Coords> outlineArray = new List<Coords>();
    if (MapGenerator.cachedRadius.TryGetValue(radius, out outlineArray))
      return outlineArray;
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    List<List<Coords>> coordsListList = new List<List<Coords>>();
    for (int index = 0; index < 8; ++index)
      coordsListList.Add(new List<Coords>());
    while (num1 >= num2)
    {
      coordsListList[0].Add(new Coords(num2, num1));
      coordsListList[1].Add(new Coords(num1, num2));
      coordsListList[2].Add(new Coords(num1, -num2));
      coordsListList[3].Add(new Coords(num2, -num1));
      coordsListList[4].Add(new Coords(-num2, -num1));
      coordsListList[5].Add(new Coords(-num1, -num2));
      coordsListList[6].Add(new Coords(-num1, num2));
      coordsListList[7].Add(new Coords(-num2, num1));
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    coordsListList[1].Reverse();
    coordsListList[3].Reverse();
    coordsListList[5].Reverse();
    coordsListList[7].Reverse();
    List<Coords> list = coordsListList[0].Concat<Coords>(coordsListList[1].Concat<Coords>(coordsListList[2].Concat<Coords>(coordsListList[3].Concat<Coords>(coordsListList[4].Concat<Coords>(coordsListList[5].Concat<Coords>(coordsListList[6].Concat<Coords>((IEnumerable<Coords>) coordsListList[7]))))))).ToList<Coords>();
    MapGenerator.cachedRadius.Add(radius, list);
    return list;
  }

  public static List<Coords> GetTowerFloatPoints(Texture2D tex, int extraY)
  {
    List<Coords> towerFloatPoints1 = new List<Coords>();
    if (MapGenerator.cachedTowerFloatPoints.TryGetValue(tex, out towerFloatPoints1))
      return towerFloatPoints1;
    List<Coords> towerFloatPoints2 = new List<Coords>();
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int num1 = tex.width / 2;
    int num2 = tex.height / 2 + extraY;
    for (int index1 = 0; index1 < tex.width; ++index1)
    {
      bool flag = true;
      for (int index2 = tex.height - 1; index2 >= 0; --index2)
      {
        if (flag)
        {
          if (pixels32[index1 + index2 * tex.width].a != (byte) 0)
          {
            towerFloatPoints2.Add(new Coords(index1 - num1, index2 - num2));
            flag = false;
          }
        }
        else if (!flag && pixels32[index1 + index2 * tex.width].a == (byte) 0)
          flag = true;
      }
    }
    MapGenerator.cachedTowerFloatPoints.Add(tex, towerFloatPoints2);
    return towerFloatPoints2;
  }

  public static List<Coords> GetTowerFallPoints(Texture2D tex, int extraY)
  {
    List<Coords> towerFallPoints1 = new List<Coords>();
    if (MapGenerator.cachedTowerFallPoints.TryGetValue(tex, out towerFallPoints1))
      return towerFallPoints1;
    List<Coords> towerFallPoints2 = new List<Coords>();
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int num1 = tex.width / 2;
    int num2 = tex.height / 2 + extraY;
    for (int index1 = 0; index1 < tex.width; ++index1)
    {
      bool flag = true;
      for (int index2 = 0; index2 < tex.height; ++index2)
      {
        if (flag)
        {
          if (pixels32[index1 + index2 * tex.width].a != (byte) 0)
          {
            towerFallPoints2.Add(new Coords(index1 - num1, index2 - num2));
            flag = false;
          }
        }
        else if (!flag && pixels32[index1 + index2 * tex.width].a == (byte) 0)
          flag = true;
      }
    }
    MapGenerator.cachedTowerFallPoints.Add(tex, towerFallPoints2);
    return towerFallPoints2;
  }

  public static List<Coords> GetTowerLeftPoints(Texture2D tex)
  {
    if (MapGenerator.cachedTowerLeftPoints.ContainsKey(tex))
      return MapGenerator.cachedTowerLeftPoints[tex];
    List<Coords> towerLeftPoints = new List<Coords>();
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int num1 = tex.width / 2 + 1;
    int num2 = tex.height / 2;
    for (int index1 = 12; index1 < tex.height; ++index1)
    {
      for (int index2 = 0; index2 < tex.width; ++index2)
      {
        if (pixels32[index2 + index1 * tex.width].a != (byte) 0)
        {
          towerLeftPoints.Add(new Coords(index2 - num1, index1 - num2));
          break;
        }
      }
    }
    MapGenerator.cachedTowerLeftPoints[tex] = towerLeftPoints;
    return towerLeftPoints;
  }

  public static List<Coords> GetTowerRightPoints(Texture2D tex)
  {
    if (MapGenerator.cachedTowerRightPoints.ContainsKey(tex))
      return MapGenerator.cachedTowerRightPoints[tex];
    List<Coords> towerRightPoints = new List<Coords>();
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int num1 = tex.width / 2 - 1;
    int num2 = tex.height / 2;
    for (int index1 = 12; index1 < tex.height; ++index1)
    {
      for (int index2 = tex.width - 1; index2 >= 0; --index2)
      {
        if (pixels32[index2 + index1 * tex.width].a != (byte) 0)
        {
          towerRightPoints.Add(new Coords(index2 - num1, index1 - num2));
          break;
        }
      }
    }
    MapGenerator.cachedTowerRightPoints[tex] = towerRightPoints;
    return towerRightPoints;
  }

  public static List<Coords> bresenhamsLine(Coords start, Coords end)
  {
    int num1 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num2 = num1 != 0 ? start.y : start.x;
    int num3 = num1 != 0 ? start.x : start.y;
    int num4 = num1 != 0 ? end.y : end.x;
    int num5 = num1 != 0 ? end.x : end.y;
    int num6 = num2 > num4 ? 1 : 0;
    int num7 = num6 != 0 ? num4 : num2;
    int num8 = num6 != 0 ? num2 : num4;
    int num9 = num6 != 0 ? num5 : num3;
    int num10 = num6 != 0 ? num3 : num5;
    int num11 = num8 - num7;
    int num12 = Mathf.Abs(num10 - num9);
    int num13 = num9 < num10 ? 1 : -1;
    int num14 = num11 / 2;
    int num15 = num9;
    if (num1 != 0)
    {
      List<Coords> coordsList = new List<Coords>();
      for (int y = num7; y <= num8; ++y)
      {
        Coords coords = new Coords(num15, y);
        num14 -= num12;
        if (num14 < 0)
        {
          num15 += num13;
          num14 += num11;
        }
        coordsList.Add(coords);
      }
      return coordsList;
    }
    List<Coords> coordsList1 = new List<Coords>();
    for (int x = num7; x <= num8; ++x)
    {
      Coords coords = new Coords(x, num15);
      num14 -= num12;
      if (num14 < 0)
      {
        num15 += num13;
        num14 -= num11;
      }
      coordsList1.Add(coords);
    }
    return coordsList1;
  }
}
