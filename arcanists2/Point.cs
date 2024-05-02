

#nullable disable
public struct Point
{
  public static readonly Point Empty;
  private int _x;
  private int _y;

  public bool IsEmpty => this._x == 0 && this._y == 0;

  public int x
  {
    get => this._x;
    set => this._x = value;
  }

  public int y
  {
    get => this._y;
    set => this._y = value;
  }

  public Point(int x, int y)
  {
    this._x = x;
    this._y = y;
  }

  public Point(MyLocation m)
  {
    this._x = (int) m.x;
    this._y = (int) m.y;
  }

  public Point(Size sz)
  {
    this._x = sz.Width;
    this._y = sz.Height;
  }

  public static implicit operator Point(MyLocation v) => new Point(v.x.ToInt(), v.y.ToInt());

  public Point(int dw)
  {
    this._x = (int) (short) Point.LOWORD(dw);
    this._y = (int) (short) Point.HIWORD(dw);
  }

  public static explicit operator Size(Point p) => new Size(p.x, p.y);

  public static Point operator +(Point pt, Size sz) => Point.Add(pt, sz);

  public static Point operator *(Point pt, Point sz) => new Point(pt._x * sz._x, pt._y * sz._y);

  public static Point operator -(Point pt, Size sz) => Point.Subtract(pt, sz);

  public static bool operator ==(Point left, Point right) => left.x == right.x && left.y == right.y;

  public static bool operator !=(Point left, Point right) => !(left == right);

  public static Point Add(Point pt, Size sz) => new Point(pt.x + sz.Width, pt.y + sz.Height);

  public static Point Subtract(Point pt, Size sz) => new Point(pt.x - sz.Width, pt.y - sz.Height);

  public override bool Equals(object obj)
  {
    return obj is Point point && point.x == this.x && point.y == this.y;
  }

  public override int GetHashCode() => this._x ^ this._y;

  public void Offset(int dx, int dy)
  {
    this.x += dx;
    this.y += dy;
  }

  public void Offset(Point p) => this.Offset(p.x, p.y);

  public override string ToString() => "{X=" + this.x.ToString() + ",Y=" + this.y.ToString() + "}";

  private static int HIWORD(int n) => n >> 16 & (int) ushort.MaxValue;

  private static int LOWORD(int n) => n & (int) ushort.MaxValue;

  internal void setLocation(double v1, double v2)
  {
    this._x = (int) v1;
    this._y = (int) v2;
  }
}
