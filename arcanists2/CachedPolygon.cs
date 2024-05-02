﻿// Decompiled with JetBrains decompiler
// Type: CachedPolygon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public class CachedPolygon
{
  public int offset;
  private bool[,] _grid;
  private int half_width;
  private int half_height;

  public bool[,] grid
  {
    set
    {
      this._grid = value;
      this.width = this._grid.GetLength(1);
      this.height = this._grid.GetLength(0);
      this.half_height = (this.height >> 1) - this.offset;
      this.half_width = this.width >> 1;
    }
  }

  public int width { get; private set; }

  public int height { get; private set; }

  public bool Inside(int x, int y)
  {
    x += this.half_width;
    y += this.half_height;
    return x >= 0 && x < this.width && y >= 0 && y < this.height && this._grid[y, x];
  }
}
