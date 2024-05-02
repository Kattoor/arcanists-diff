// Decompiled with JetBrains decompiler
// Type: Rectangle
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public struct Rectangle
{
  public static readonly Rectangle Empty;
  private int x;
  private int y;
  private int width;
  private int height;

  public bool IsNull() => this.height == 0 && this.width == 0 && this.y == 0 && this.x == 0;

  public Point Location
  {
    get => new Point(this.X, this.Y);
    set
    {
      this.X = value.x;
      this.Y = value.y;
    }
  }

  public Size Size
  {
    get => new Size(this.Width, this.Height);
    set
    {
      this.Width = value.Width;
      this.Height = value.Height;
    }
  }

  public int X
  {
    get => this.x;
    set => this.x = value;
  }

  public int Y
  {
    get => this.y;
    set => this.y = value;
  }

  public int Width
  {
    get => this.width;
    set => this.width = value;
  }

  public int Height
  {
    get => this.height;
    set => this.height = value;
  }

  public int Left => this.X;

  public int Top => this.Y;

  public int Right => this.X + this.Width;

  public int Bottom => this.Y + this.Height;

  public bool IsEmpty => this.height == 0 && this.width == 0 && this.x == 0 && this.y == 0;

  public void Add(double newx, double newy)
  {
    double x = Math.Min((double) this.x, newx);
    double num1 = Math.Max((double) (this.x + this.width), newx);
    double y = Math.Min((double) this.y, newy);
    double num2 = Math.Max((double) (this.y + this.height), newy);
    this.setRect((int) x, (int) y, (int) (num1 - x), (int) (num2 - y));
  }

  private void setRect(int x, int y, int w, int h)
  {
    this.x = x;
    this.y = y;
    this.width = w;
    this.height = h;
  }

  public Rectangle(int x, int y, int width, int height)
  {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
  }

  public Rectangle(Point location, Size size)
  {
    this.x = location.x;
    this.y = location.y;
    this.width = size.Width;
    this.height = size.Height;
  }

  public static bool operator ==(Rectangle left, Rectangle right)
  {
    return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
  }

  public static bool operator !=(Rectangle left, Rectangle right) => !(left == right);

  public static Rectangle FromLTRB(int left, int top, int right, int bottom)
  {
    return new Rectangle(left, top, right - left, bottom - top);
  }

  public override bool Equals(object obj)
  {
    return obj is Rectangle rectangle && rectangle.X == this.X && rectangle.Y == this.Y && rectangle.Width == this.Width && rectangle.Height == this.Height;
  }

  public bool Contains(int x, int y)
  {
    return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
  }

  public bool Contains(Point pt) => this.Contains(pt.x, pt.y);

  public bool Contains(Rectangle rect)
  {
    return this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y && rect.Y + rect.Height <= this.Y + this.Height;
  }

  public override int GetHashCode()
  {
    return this.X ^ (this.Y << 13 | this.Y >>> 19) ^ (this.Width << 26 | this.Width >>> 6) ^ (this.Height << 7 | this.Height >>> 25);
  }

  public void Inflate(int width, int height)
  {
    this.X -= width;
    this.Y -= height;
    this.Width += 2 * width;
    this.Height += 2 * height;
  }

  public void Inflate(Size size) => this.Inflate(size.Width, size.Height);

  public static Rectangle Inflate(Rectangle rect, int x, int y)
  {
    Rectangle rectangle = rect;
    rectangle.Inflate(x, y);
    return rectangle;
  }

  public void Intersect(Rectangle rect)
  {
    Rectangle rectangle = Rectangle.Intersect(rect, this);
    this.X = rectangle.X;
    this.Y = rectangle.Y;
    this.Width = rectangle.Width;
    this.Height = rectangle.Height;
  }

  public static Rectangle Intersect(Rectangle a, Rectangle b)
  {
    int x = Math.Max(a.X, b.X);
    int num1 = Math.Min(a.X + a.Width, b.X + b.Width);
    int y = Math.Max(a.Y, b.Y);
    int num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
    return num1 >= x && num2 >= y ? new Rectangle(x, y, num1 - x, num2 - y) : Rectangle.Empty;
  }

  public bool IntersectsWith(Rectangle rect)
  {
    return rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height && this.Y < rect.Y + rect.Height;
  }

  public static Rectangle Union(Rectangle a, Rectangle b)
  {
    int x = Math.Min(a.X, b.X);
    int num1 = Math.Max(a.X + a.Width, b.X + b.Width);
    int y = Math.Min(a.Y, b.Y);
    int num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
    return new Rectangle(x, y, num1 - x, num2 - y);
  }

  public void Offset(Point pos) => this.Offset(pos.x, pos.y);

  public void Offset(int x, int y)
  {
    this.X += x;
    this.Y += y;
  }

  public override string ToString()
  {
    return "{X=" + this.X.ToString() + ",Y=" + this.Y.ToString() + ",Width=" + this.Width.ToString() + ",Height=" + this.Height.ToString() + "}";
  }

  internal Rectangle getBounds() => new Rectangle(this.X, this.Y, this.Width, this.Height);

  public void translate(int dx, int dy)
  {
    int x = this.x;
    int num1 = x + dx;
    if (dx < 0)
    {
      if (num1 > x)
      {
        if (this.width >= 0)
          this.width += num1 - int.MinValue;
        num1 = int.MinValue;
      }
    }
    else if (num1 < x)
    {
      if (this.width >= 0)
      {
        this.width += num1 - int.MaxValue;
        if (this.width < 0)
          this.width = int.MaxValue;
      }
      num1 = int.MaxValue;
    }
    this.x = num1;
    int y = this.y;
    int num2 = y + dy;
    if (dy < 0)
    {
      if (num2 > y)
      {
        if (this.height >= 0)
          this.height += num2 - int.MinValue;
        num2 = int.MinValue;
      }
    }
    else if (num2 < y)
    {
      if (this.height >= 0)
      {
        this.height += num2 - int.MaxValue;
        if (this.height < 0)
          this.height = int.MaxValue;
      }
      num2 = int.MaxValue;
    }
    this.y = num2;
  }

  internal bool contains(double x, double y)
  {
    double num1 = x;
    double num2 = y;
    return x >= num1 && y >= num2 && x < num1 + (double) this.width && y < num2 + (double) this.height;
  }

  internal bool intersects(double x, double y, double w, double h)
  {
    if (this.IsEmpty || w <= 0.0 || h <= 0.0)
      return false;
    double num1 = x;
    double num2 = y;
    return x + w > num1 && y + h > num2 && x < num1 + (double) this.width && y < num2 + (double) this.height;
  }
}
