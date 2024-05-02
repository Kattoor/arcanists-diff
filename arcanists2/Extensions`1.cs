// Decompiled with JetBrains decompiler
// Type: Extensions`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public static class Extensions<T> where T : Enum
{
  public static bool AvailableGameMode(GameFacts.Message msg, out T map, params RatedFacts[] p)
  {
    T[] values = (T[]) Enum.GetValues(typeof (T));
    List<T> objList = new List<T>();
    foreach (T obj in values)
    {
      int int32 = Convert.ToInt32((object) obj);
      if (int32 != -1)
      {
        for (int index = 0; index < p.Length; ++index)
        {
          switch (msg)
          {
            case GameFacts.Message.Time:
              if ((p[index].turnTime & int32) != 0)
                break;
              break;
            case GameFacts.Message.Map:
              if ((p[index].mapStyle & int32) != 0)
                break;
              break;
            case GameFacts.Message.Team:
              if ((p[index].teams & int32) != 0)
                break;
              break;
            case GameFacts.Message.GameStyle:
              int num = p[index].extraOptions & int32;
              break;
          }
        }
        objList.Add(obj);
      }
    }
    if (objList.Count > 0)
    {
      map = objList[UnityEngine.Random.Range(0, objList.Count)];
      return true;
    }
    map = values[0];
    return false;
  }
}
