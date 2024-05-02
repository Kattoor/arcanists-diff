

using MoonSharp.Interpreter;
using System;
using UnityEngine;

#nullable disable
namespace Educative
{
  [Serializable]
  public class Point
  {
    public double x;
    public double y;

    public Point(double x = 0.0, double y = 0.0)
    {
      this.x = x;
      this.y = y;
    }

    public Point(Vector2 v)
    {
      this.x = (double) v.x;
      this.y = (double) v.y;
    }

    [MoonSharpHidden]
    public MyLocation ToMyLocation()
    {
      return new MyLocation((FixedInt) (float) this.x, (FixedInt) (float) this.y);
    }

    public static double distance(Point a, Point b)
    {
      return (double) MyLocation.Distance(a.ToMyLocation(), b.ToMyLocation());
    }

    public double distance(Point b)
    {
      return (double) MyLocation.Distance(this.ToMyLocation(), b.ToMyLocation());
    }

    public static Point construct(int x, int y) => new Point((double) x, (double) y);

    public Point copy() => new Point(this.x, this.y);

    public Point normalized()
    {
      double num = Math.Max(this.x, this.y);
      return new Point(this.x / num, this.y / num);
    }

    public void normalize()
    {
      double num = Math.Max(this.x, this.y);
      this.x /= num;
      this.y /= num;
    }

    public override bool Equals(object obj)
    {
      return !(typeof (Point) != obj?.GetType()) && this.x == ((Point) obj).x && this.y == ((Point) obj).y;
    }

    public override int GetHashCode() => base.GetHashCode();

    public static bool operator ==(Point v, Point x)
    {
      if ((object) x == null)
        return (object) v == null;
      return x.x == v.x && x.y == v.y;
    }

    public static Point operator -(Point x, double v) => new Point(x.x - v, x.y - v);

    public static Point operator +(Point x, double v) => new Point(x.x + v, x.y + v);

    public static Point operator *(Point x, double v) => new Point(x.x * v, x.y * v);

    public static Point operator /(Point x, double v) => new Point(x.x / v, x.y / v);

    public static Point operator %(Point x, double v) => new Point(x.x % v, x.y % v);

    public static Point operator -(Point x, Point v) => new Point(x.x - v.x, x.y - v.y);

    public static Point operator +(Point x, Point v) => new Point(x.x + v.x, x.y + v.y);

    public static Point operator *(Point x, Point v) => new Point(x.x * v.x, x.y * v.y);

    public static Point operator /(Point x, Point v) => new Point(x.x / v.x, x.y / v.y);

    public static Point operator %(Point x, Point v) => new Point(x.x % v.x, x.y % v.y);

    public static bool operator !=(Point v, Point x)
    {
      if ((object) x == null)
        return v != null;
      return x.x != v.x || x.y != v.y;
    }

    [MoonSharpHidden]
    public override string ToString() => " (" + (object) this.x + ", " + (object) this.y + ")";
  }
}
