// Decompiled with JetBrains decompiler
// Type: Junk.BufferedImage
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace Junk
{
  public class BufferedImage
  {
    public Surface surface;
    private BufferedImage paint;
    private bool paintMode = true;

    public int width => this.surface.width;

    public int height => this.surface.height;

    public Color32[] pixels => this.surface.pixels;

    public BufferedImage GetPaint => this.paint;

    public int getHeight() => this.surface.height;

    public int getWidth() => this.surface.width;

    public Color32 this[int x]
    {
      get => this.surface.pixels[x];
      set => this.surface.pixels[x] = value;
    }

    public BufferedImage(Color32[] pixels, int w, int h)
    {
      this.surface = new Surface(pixels, w, h);
    }

    public BufferedImage(Texture2D tex) => this.surface = new Surface(tex, tex.GetPixels32());

    public BufferedImage(int v1, int v2)
    {
      this.surface = new Surface(new Color32[v1 * v2], v1, v2);
    }

    internal Color32 getRGB(int x, int y)
    {
      return x < 0 || x >= this.surface.width || y < 0 || y >= this.surface.height ? (Color32) Color.clear : this.surface.pixels[x + y * this.surface.width];
    }

    public void setPaint(BufferedImage b) => this.paint = b;

    internal Color32 GetPixel(int x, int y)
    {
      return this.pixels[x + (this.surface.height - y - 1) * this.surface.width];
    }

    internal void SetPixel(int x, int y, Color32 c)
    {
      if (x < 0 || y < 0 || x + (this.surface.height - y - 1) * this.surface.width >= this.pixels.Length)
        Debug.Log((object) (x.ToString() + " " + (object) y + " " + (object) this.surface.height + " " + (object) this.surface.width + " " + (object) this.pixels.Length));
      this.pixels[x + (this.surface.height - y - 1) * this.surface.width] = c;
    }

    internal void SetPixel(int x, int y)
    {
      this.pixels[x + (this.surface.height - y - 1) * this.surface.width] = this.paint.surface.pixels[x % this.paint.surface.width + (this.surface.height - y - 1) % this.paint.surface.height * this.paint.surface.width];
    }

    private void xLine(int x1, int x2, int y)
    {
      Color32 c;
      for (; x1 <= x2; this.SetPixel(x1++, y, c))
        c = Color32.Lerp(new Color32((byte) 119, (byte) 119, (byte) 119, byte.MaxValue), new Color32((byte) 252, (byte) 252, (byte) 252, byte.MaxValue), (float) y / (float) this.surface.height);
    }

    private void yLine(int x, int y1, int y2)
    {
      Color32 c;
      for (; y1 <= y2; this.SetPixel(x, y1++, c))
        c = Color32.Lerp(new Color32((byte) 119, (byte) 119, (byte) 119, byte.MaxValue), new Color32((byte) 252, (byte) 252, (byte) 252, byte.MaxValue), (float) y1 / (float) this.surface.height);
    }

    public void circle(int xc, int yc, int thickness, int outer)
    {
      int num1 = outer - thickness + 1;
      int num2 = outer;
      int num3 = num1;
      int num4 = 0;
      int num5 = 1 - num2;
      int num6 = 1 - num3;
      for (int index1 = 0; index1 < this.height; ++index1)
      {
        Color32 color32 = Color32.Lerp(new Color32((byte) 119, (byte) 119, (byte) 119, (byte) 0), new Color32((byte) 252, (byte) 252, (byte) 252, (byte) 0), (float) num4 / (float) this.height);
        for (int index2 = 0; index2 < this.width; ++index2)
          this.pixels[index2 + index1 * this.width] = color32;
      }
      while (num2 >= num4)
      {
        this.xLine(xc + num3, xc + num2, yc + num4);
        this.yLine(xc + num4, yc + num3, yc + num2);
        this.xLine(xc - num2, xc - num3, yc + num4);
        this.yLine(xc - num4, yc + num3, yc + num2);
        this.xLine(xc - num2, xc - num3, yc - num4);
        this.yLine(xc - num4, yc - num2, yc - num3);
        this.xLine(xc + num3, xc + num2, yc - num4);
        this.yLine(xc + num4, yc - num2, yc - num3);
        ++num4;
        if (num5 < 0)
        {
          num5 += 2 * num4 + 1;
        }
        else
        {
          --num2;
          num5 += 2 * (num4 - num2 + 1);
        }
        if (num4 > num1)
          num3 = num4;
        else if (num6 < 0)
        {
          num6 += 2 * num4 + 1;
        }
        else
        {
          --num3;
          num6 += 2 * (num4 - num3 + 1);
        }
      }
      this.FloodFillCorners();
    }

    private void FloodFillCorners()
    {
      for (int index1 = 0; index1 < this.surface.height; ++index1)
      {
        int num = this.surface.width * index1;
        for (int index2 = 0; index2 < this.surface.width && this.pixels[num + index2].a == (byte) 0; ++index2)
          this.pixels[num + index2] = (Color32) Color.black;
      }
      for (int index3 = 0; index3 < this.surface.height; ++index3)
      {
        int num = this.surface.width * index3;
        for (int index4 = this.surface.width - 1; index4 >= 0 && this.pixels[num + index4].a == (byte) 0; --index4)
          this.pixels[num + index4] = (Color32) Color.black;
      }
    }

    internal void fillRect(int v1, int v2, int v3, int v4)
    {
      v3 += v1;
      v4 += v2;
      Color32[] pixels1 = this.surface.pixels;
      Color32[] pixels2 = this.paint.surface.pixels;
      int height = this.paint.surface.height;
      int width = this.paint.surface.width;
      if (v1 < 0)
        return;
      for (int index1 = v1; index1 <= v3; ++index1)
      {
        for (int index2 = v2; index2 <= v4; ++index2)
        {
          int index3 = index1 + (this.surface.height - index2 - 1) * this.surface.width;
          if (index1 >= 0 && index2 < this.surface.height && index3 >= 0 && index3 < this.surface.pixels.Length)
            pixels1[index3] = !this.paintMode ? (Color32) Color.clear : pixels2[index1 % width + (this.surface.height - index2 - 1) % height * width];
        }
      }
    }

    internal void fillRectWithOffset(int v1, int v2, int v3, int v4, bool clear = false)
    {
      v3 += v1;
      v4 += v2;
      Color32[] pixels1 = this.surface.pixels;
      Color32[] pixels2 = this.paint.surface.pixels;
      int height = this.paint.surface.height;
      int width = this.paint.surface.width;
      if (v1 < 0)
        return;
      int num1 = v1;
      int num2 = 0;
      while (num1 <= v3)
      {
        int num3 = v2;
        int num4 = 0;
        while (num3 <= v4)
        {
          int index = num1 + (this.surface.height - num3 - 1) * this.surface.width;
          if (num1 >= 0 && num3 < this.surface.height && index >= 0 && index < this.surface.pixels.Length)
          {
            if (this.paintMode)
            {
              int num5 = height - num4 - 1;
              while (num5 < 0)
                num5 += height;
              Color32 color32 = pixels2[num2 % width + num5 % height * width];
              if (color32.a > (byte) 0 | clear)
                pixels1[index] = color32;
            }
            else
              pixels1[index] = (Color32) Color.clear;
          }
          ++num3;
          ++num4;
        }
        ++num1;
        ++num2;
      }
    }

    internal void fillRectWithOffsetReversed(int v1, int v2, int v3, int v4, bool clear = false)
    {
      v3 += v1;
      v4 += v2;
      Color32[] pixels1 = this.surface.pixels;
      Color32[] pixels2 = this.paint.surface.pixels;
      int height = this.paint.surface.height;
      int width = this.paint.surface.width;
      if (v1 < 0)
        return;
      int num1 = v1;
      int num2 = 0;
      while (num1 <= v3)
      {
        int num3 = v2;
        int num4 = 0;
        while (num3 <= v4)
        {
          int index = num1 + (this.surface.height - num3 - 1) * this.surface.width;
          if (num1 >= 0 && num3 < this.surface.height && index >= 0 && index < this.surface.pixels.Length)
          {
            if (this.paintMode)
            {
              int num5 = height - num4 - 1;
              while (num5 < 0)
                num5 += height;
              Color32 color32 = pixels2[(width - num2) % width + num5 % height * width];
              if (color32.a > (byte) 0 | clear)
                pixels1[index] = color32;
            }
            else
              pixels1[index] = (Color32) Color.clear;
          }
          ++num3;
          ++num4;
        }
        ++num1;
        ++num2;
      }
    }

    private bool IsPointInPolygon4(List<Coords> polygon, Coords point)
    {
      int index1 = point.x + point.y * this.surface.width;
      if (index1 >= this.surface.pixels.Length || index1 < 0 || this.surface.pixels[index1].a != (byte) 0)
        return false;
      bool flag = false;
      int index2 = polygon.Count<Coords>() - 1;
      for (int index3 = 0; index3 < polygon.Count<Coords>(); ++index3)
      {
        if ((polygon[index3].y < point.y && polygon[index2].y >= point.y || polygon[index2].y < point.y && polygon[index3].y >= point.y) && polygon[index3].x + (point.y - polygon[index3].y) / (polygon[index2].y - polygon[index3].y) * (polygon[index2].x - polygon[index3].x) < point.x)
          flag = !flag;
        index2 = index3;
      }
      return flag;
    }

    private void ClosestInPolygon(List<Coords> poly, Coords point)
    {
      Coords point1 = new Coords(point.x, point.y);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        point1.x = point.x - index1;
        point1.y = point.y - index1;
        int num = 2 + index1 * 2;
        for (int index2 = 0; index2 < num; ++index2)
        {
          if (this.IsPointInPolygon4(poly, point1))
          {
            this.FloodFill(point1.x, point1.y);
            return;
          }
          if (index2 < num)
            ++point1.x;
        }
        for (int index3 = 0; index3 < num; ++index3)
        {
          if (this.IsPointInPolygon4(poly, point1))
          {
            this.FloodFill(point1.x, point1.y);
            return;
          }
          if (index3 < num)
            ++point1.y;
        }
        for (int index4 = 0; index4 < num; ++index4)
        {
          if (this.IsPointInPolygon4(poly, point1))
          {
            this.FloodFill(point1.x, point1.y);
            return;
          }
          if (index4 < num)
            --point1.x;
        }
        for (int index5 = 0; index5 < num; ++index5)
        {
          if (this.IsPointInPolygon4(poly, point1))
          {
            this.FloodFill(point1.x, point1.y);
            return;
          }
          if (index5 < num)
            --point1.y;
        }
      }
    }

    internal void fillPolygon(Polygon p)
    {
      int[] numArray = new int[p.points.Count];
      Rectangle rectangle = p.Bounds();
      int height1 = rectangle.Height;
      int y = rectangle.Y;
      int width1 = rectangle.Width;
      int x = rectangle.X;
      int count = p.points.Count;
      Color32[] pixels = this.paint.surface.pixels;
      int height2 = this.paint.surface.height;
      int width2 = this.paint.surface.width;
      int num1 = 0;
      int num2 = 0;
      for (int index1 = 0; index1 < p.points.Count - 1; ++index1)
      {
        foreach (Coords coords in MapGenerator.bresenhamsLine(p.points[index1], p.points[index1 + 1]))
        {
          int index2 = coords.x + coords.y * this.surface.width;
          if (coords.x >= 0 && coords.y >= 0 && index2 < this.surface.pixels.Length && index2 >= 0)
            this.surface.pixels[index2] = pixels[coords.x % width2 + coords.y % height2 * width2];
        }
        num1 += p.points[index1].x;
        num2 += p.points[index1].y;
      }
      this.ClosestInPolygon(p.points, p.points[0]);
    }

    private void FloodFill(int startX, int startY)
    {
      if (startX < 0 || startY < 0 || startX >= this.width || startY >= this.height)
        return;
      Queue<Coords> coordsQueue = new Queue<Coords>();
      coordsQueue.Enqueue(new Coords(startX, startY));
      this.surface.pixels[startX + startY * this.surface.width] = this.paint.surface.pixels[startX % this.paint.surface.width + startY % this.paint.surface.height * this.paint.surface.width];
      while (coordsQueue.Count > 0)
      {
        Coords coords = coordsQueue.Dequeue();
        int x1 = coords.x + 1;
        int y1 = coords.y;
        if (x1 < this.surface.width && this.surface.pixels[x1 + y1 * this.surface.width].a == (byte) 0)
        {
          coordsQueue.Enqueue(new Coords(x1, y1));
          this.surface.pixels[x1 + y1 * this.surface.width] = this.paint.surface.pixels[x1 % this.paint.surface.width + y1 % this.paint.surface.height * this.paint.surface.width];
        }
        int x2 = coords.x - 1;
        if (x2 >= 0 && this.surface.pixels[x2 + y1 * this.surface.width].a == (byte) 0)
        {
          coordsQueue.Enqueue(new Coords(x2, y1));
          this.surface.pixels[x2 + y1 * this.surface.width] = this.paint.surface.pixels[x2 % this.paint.surface.width + y1 % this.paint.surface.height * this.paint.surface.width];
        }
        int x3 = coords.x;
        int y2 = coords.y + 1;
        if (y2 < this.surface.height && this.surface.pixels[x3 + y2 * this.surface.width].a == (byte) 0)
        {
          coordsQueue.Enqueue(new Coords(x3, y2));
          this.surface.pixels[x3 + y2 * this.surface.width] = this.paint.surface.pixels[x3 % this.paint.surface.width + y2 % this.paint.surface.height * this.paint.surface.width];
        }
        int y3 = coords.y - 1;
        if (y3 >= 0 && this.surface.pixels[x3 + y3 * this.surface.width].a == (byte) 0)
        {
          coordsQueue.Enqueue(new Coords(x3, y3));
          this.surface.pixels[x3 + y3 * this.surface.width] = this.paint.surface.pixels[x3 % this.paint.surface.width + y3 % this.paint.surface.height * this.paint.surface.width];
        }
      }
    }

    internal void setClear() => this.paintMode = false;

    internal void setPaintMode() => this.paintMode = true;

    internal bool GetPaintMode() => this.paintMode;

    internal void drawGrass(BufferedImage gr, int startX, int startY, int srcX, bool behind)
    {
      int num1 = 0;
      if (startY < 0)
      {
        num1 = -startY;
        startY = 0;
      }
      int width1 = gr.surface.width;
      int num2 = startY + gr.surface.height - num1;
      int width2 = this.surface.width;
      int height = this.surface.height;
      if (num2 > height)
        num2 = height;
      if (behind)
      {
        int num3 = startY;
        int num4 = num1;
        while (num3 < num2)
        {
          if (gr.surface.pixels[srcX + num4 * width1].a != (byte) 0 && this.surface.pixels[startX + num3 * width2].a == (byte) 0)
            this.surface.pixels[startX + num3 * width2] = gr.surface.pixels[srcX + num4 * width1];
          ++num3;
          ++num4;
        }
      }
      else
      {
        int num5 = startY;
        int num6 = num1;
        while (num5 < num2)
        {
          if (gr.surface.pixels[srcX + num6 * width1].a != (byte) 0)
            this.surface.pixels[startX + num5 * width2] = gr.surface.pixels[srcX + num6 * width1];
          ++num5;
          ++num6;
        }
      }
    }

    internal void drawImage(BufferedImage gr, int startX, int startY, bool behind)
    {
      int num1 = 0;
      if (startY < 0)
      {
        num1 = -startY;
        startY = 0;
      }
      int width1 = gr.surface.width;
      int num2 = startX - width1 / 2;
      int num3 = num2 + width1;
      int num4 = startY + gr.surface.height - num1;
      int width2 = this.surface.width;
      int height = this.surface.height;
      int num5 = 0;
      if (num2 < 0)
      {
        num5 = -num2;
        num2 = 0;
      }
      if (num3 > width2)
        num3 = width2;
      if (num4 > height)
        num4 = height;
      int num6 = num5;
      if (behind)
      {
        while (num2 < num3)
        {
          int num7 = startY;
          int num8 = num1;
          while (num7 < num4)
          {
            if (gr.surface.pixels[num6 + num8 * width1].a != (byte) 0 && this.surface.pixels[num2 + num7 * width2].a == (byte) 0)
              this.surface.pixels[num2 + num7 * width2] = gr.surface.pixels[num6 + num8 * width1];
            ++num7;
            ++num8;
          }
          ++num2;
          ++num6;
        }
      }
      else
      {
        while (num2 < num3)
        {
          int num9 = startY;
          int num10 = num1;
          while (num9 < num4)
          {
            if (gr.surface.pixels[num6 + num10 * width1].a != (byte) 0)
              this.surface.pixels[num2 + num9 * width2] = gr.surface.pixels[num6 + num10 * width1];
            ++num9;
            ++num10;
          }
          ++num2;
          ++num6;
        }
      }
    }

    internal void drawImageReversed(BufferedImage gr, int startX, int startY, bool behind)
    {
      int num1 = 0;
      if (startY < 0)
      {
        num1 = -startY;
        startY = 0;
      }
      int width1 = gr.surface.width;
      int num2 = startX - width1 / 2;
      int num3 = num2 + width1;
      int num4 = startY + gr.surface.height - num1;
      int width2 = this.surface.width;
      int height = this.surface.height;
      if (num2 < 0)
      {
        Debug.LogError((object) ("X cannot be " + (object) num2));
      }
      else
      {
        if (num3 > width2 || num4 > height)
          return;
        int num5 = width1 - 1;
        if (behind)
        {
          while (num2 < num3)
          {
            int num6 = startY;
            int num7 = num1;
            while (num6 < num4)
            {
              if (gr.surface.pixels[num5 + num7 * width1].a != (byte) 0 && this.surface.pixels[num2 + num6 * width2].a == (byte) 0)
                this.surface.pixels[num2 + num6 * width2] = gr.surface.pixels[num5 + num7 * width1];
              ++num6;
              ++num7;
            }
            ++num2;
            --num5;
          }
        }
        else
        {
          while (num2 < num3)
          {
            int num8 = startY;
            int num9 = num1;
            while (num8 < num4)
            {
              if (gr.surface.pixels[num5 + num9 * width1].a != (byte) 0)
                this.surface.pixels[num2 + num8 * width2] = gr.surface.pixels[num5 + num9 * width1];
              ++num8;
              ++num9;
            }
            ++num2;
            --num5;
          }
        }
      }
    }

    internal void clear()
    {
      for (int index = 0; index < this.surface.pixels.Length; ++index)
        this.surface.pixels[index].a = (byte) 0;
    }

    internal void clearImage(BufferedImage gr)
    {
      for (int index = 0; index < this.surface.pixels.Length; ++index)
      {
        if (gr.surface.pixels[index].a != (byte) 0)
          this.surface.pixels[index].a = (byte) 0;
      }
    }

    internal void drawClearImage(BufferedImage gr)
    {
      for (int index = 0; index < this.surface.pixels.Length; ++index)
      {
        if (this.surface.pixels[index].a == (byte) 0)
          this.surface.pixels[index] = gr.surface.pixels[index];
        else if (gr.surface.pixels[index].a != (byte) 0)
          this.surface.pixels[index].a = (byte) 0;
      }
    }

    internal void clearMisMatch(BufferedImage gr)
    {
      for (int index = 0; index < this.surface.pixels.Length; ++index)
      {
        if (!Global.CompareColors(this.surface.pixels[index], gr.surface.pixels[index]))
          this.surface.pixels[index].a = (byte) 0;
      }
    }

    internal void drawImage(BufferedImage gr)
    {
      for (int index = 0; index < this.surface.pixels.Length; ++index)
      {
        if (this.surface.pixels[index].a == (byte) 0)
          this.surface.pixels[index] = gr.surface.pixels[index];
      }
    }

    internal void drawImageInFront(BufferedImage gr)
    {
      for (int index = 0; index < this.surface.pixels.Length; ++index)
      {
        if (gr.surface.pixels[index].a != (byte) 0)
          this.surface.pixels[index] = gr.surface.pixels[index];
      }
    }
  }
}
