// Decompiled with JetBrains decompiler
// Type: Junk.Polygon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace Junk
{
  public class Polygon
  {
    public List<Coords> points = new List<Coords>();
    public int Height;

    internal void reset() => this.points.Clear();

    internal void addPoint(int x, int y) => this.points.Add(new Coords(x, this.Height - y));

    public Rectangle Bounds()
    {
      Rectangle rectangle = new Rectangle();
      for (int index = 0; index < this.points.Count; ++index)
      {
        if (this.points[index].x > rectangle.Width)
          rectangle.Width = this.points[index].x;
        else if (this.points[index].x < rectangle.X)
          rectangle.X = this.points[index].x;
        if (this.points[index].y > rectangle.Height)
          rectangle.Height = this.points[index].y;
        else if (this.points[index].y < rectangle.Y)
          rectangle.Y = this.points[index].y;
      }
      return rectangle;
    }
  }
}
