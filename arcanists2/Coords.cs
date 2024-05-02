﻿// Decompiled with JetBrains decompiler
// Type: Coords
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coords
{
  public int x;
  public int y;

  public Coords(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public override string ToString() => "Coords (" + (object) this.x + ", " + (object) this.y + ")";

  public Vector3 ToVector() => new Vector3((float) this.x, (float) this.y);

  public MyLocation ToMyLocation() => new MyLocation(this.x, this.y);

  public float distance => Mathf.Sqrt((float) (this.x * this.x + this.y * this.y));

  public int Distance(Coords b) => Mathf.Abs(b.x - this.x) + Mathf.Abs(b.y - this.y);

  public float DistanceSq(Coords b)
  {
    return Mathf.Sqrt((float) (Mathf.Abs(b.x - this.x) * Mathf.Abs(b.x - this.x) + Mathf.Abs(b.y - this.y) * Mathf.Abs(b.y - this.y)));
  }
}
