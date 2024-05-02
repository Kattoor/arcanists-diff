// Decompiled with JetBrains decompiler
// Type: Recolor
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class Recolor
{
  public Recolor.Colors[] colors = new Recolor.Colors[11]
  {
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors(),
    new Recolor.Colors()
  };

  public bool HasCustom()
  {
    for (int index = 1; index < this.colors.Length; ++index)
    {
      if (!Global.CompareColors(this.colors[0].red, this.colors[index].red) || !Global.CompareColors(this.colors[0].green, this.colors[index].green) || !Global.CompareColors(this.colors[0].blue, this.colors[index].blue) || !Global.CompareColors(this.colors[0].gray, this.colors[index].gray))
        return true;
    }
    return false;
  }

  public void Serialize(myBinaryWriter r)
  {
    r.Write(this.colors.Length);
    for (int index = 0; index < this.colors.Length; ++index)
    {
      r.WriteNoAlpha(this.colors[index].red);
      r.WriteNoAlpha(this.colors[index].green);
      r.WriteNoAlpha(this.colors[index].blue);
      r.WriteNoAlpha(this.colors[index].gray);
    }
  }

  public void Deserialize(myBinaryReader r)
  {
    int num = r.ReadInt32();
    if (num > 11)
      return;
    for (int index = 0; index < num; ++index)
    {
      this.colors[index].red = r.ReadColor32NoAlpha();
      this.colors[index].green = r.ReadColor32NoAlpha();
      this.colors[index].blue = r.ReadColor32NoAlpha();
      this.colors[index].gray = r.ReadColor32NoAlpha();
    }
    for (int index = num; index < this.colors.Length; ++index)
    {
      this.colors[index].red = this.colors[0].red;
      this.colors[index].green = this.colors[0].green;
      this.colors[index].blue = this.colors[0].blue;
      this.colors[index].gray = this.colors[0].gray;
    }
  }

  public Sprite Texture(Sprite spriteOrigonal, Outfit o)
  {
    int index = (int) o;
    if (index >= this.colors.Length)
      index = 0;
    return Recolor.Texture(spriteOrigonal, (Color) this.colors[index].red, (Color) this.colors[index].green, (Color) this.colors[index].blue, (Color) this.colors[index].gray);
  }

  public void Set(Outfit o, ColorType t, Color32 c)
  {
    int index1 = (int) o;
    if (index1 >= this.colors.Length)
      index1 = 0;
    if (index1 < 0)
    {
      for (int index2 = 0; index2 < this.colors.Length; ++index2)
      {
        switch (t)
        {
          case ColorType.Red:
            this.colors[index2].red = c;
            break;
          case ColorType.Green:
            this.colors[index2].green = c;
            break;
          case ColorType.Blue:
            this.colors[index2].blue = c;
            break;
          case ColorType.Gray:
            this.colors[index2].gray = c;
            break;
        }
      }
    }
    else
    {
      switch (t)
      {
        case ColorType.Red:
          this.colors[index1].red = c;
          break;
        case ColorType.Green:
          this.colors[index1].green = c;
          break;
        case ColorType.Blue:
          this.colors[index1].blue = c;
          break;
        case ColorType.Gray:
          this.colors[index1].gray = c;
          break;
      }
    }
  }

  public Color32 Get(Outfit o, ColorType t)
  {
    int index = (int) o;
    if (index < 0 || index >= this.colors.Length)
      index = 0;
    switch (t)
    {
      case ColorType.Red:
        return this.colors[index].red;
      case ColorType.Green:
        return this.colors[index].green;
      case ColorType.Blue:
        return this.colors[index].blue;
      case ColorType.Gray:
        return this.colors[index].gray;
      default:
        return (Color32) Color.white;
    }
  }

  public static Color ZeroAlpha(Color a)
  {
    return a with { a = 0.0f };
  }

  public static Sprite Copy(Sprite spriteorigonal)
  {
    if ((UnityEngine.Object) spriteorigonal == (UnityEngine.Object) null)
      return (Sprite) null;
    Texture2D texture1 = spriteorigonal.texture;
    Texture2D texture2D = new Texture2D((int) spriteorigonal.rect.width, (int) spriteorigonal.rect.height, texture1.format, false, true);
    texture2D.wrapMode = TextureWrapMode.Clamp;
    Color[] colors = texture1.isReadable ? texture1.GetPixels((int) spriteorigonal.rect.x, (int) spriteorigonal.rect.y, (int) spriteorigonal.rect.width, (int) spriteorigonal.rect.height) : texture1.GetReadablePixels((int) spriteorigonal.rect.x, (int) spriteorigonal.rect.y, (int) spriteorigonal.rect.width, (int) spriteorigonal.rect.height);
    texture2D.SetPixels(colors);
    texture2D.Apply(false);
    Texture2D texture2 = texture2D;
    Rect rect1 = spriteorigonal.rect;
    double width = (double) (int) rect1.width;
    rect1 = spriteorigonal.rect;
    double height = (double) (int) rect1.height;
    Rect rect2 = new Rect(0.0f, 0.0f, (float) width, (float) height);
    Vector2 pivot = new Vector2(spriteorigonal.pivot.x / (float) texture2D.width, spriteorigonal.pivot.y / (float) texture2D.height);
    double pixelsPerUnit = (double) spriteorigonal.pixelsPerUnit;
    return Global.AddSprite(Sprite.Create(texture2, rect2, pivot, (float) pixelsPerUnit));
  }

  public static Sprite Texture(
    Sprite spriteorigonal,
    Color red,
    Color green,
    Color blue,
    Color gray)
  {
    Texture2D texture1 = spriteorigonal.texture;
    Rect rect1 = spriteorigonal.rect;
    int width1 = (int) rect1.width;
    rect1 = spriteorigonal.rect;
    int height1 = (int) rect1.height;
    int format = (int) texture1.format;
    Texture2D texture2D = new Texture2D(width1, height1, (TextureFormat) format, false, true);
    texture2D.wrapMode = TextureWrapMode.Clamp;
    Color[] colors = texture1.isReadable ? texture1.GetPixels((int) spriteorigonal.rect.x, (int) spriteorigonal.rect.y, (int) spriteorigonal.rect.width, (int) spriteorigonal.rect.height) : texture1.GetReadablePixels((int) spriteorigonal.rect.x, (int) spriteorigonal.rect.y, (int) spriteorigonal.rect.width, (int) spriteorigonal.rect.height);
    for (int index = 0; index < colors.Length; ++index)
    {
      Color32 color32 = (Color32) colors[index];
      if (color32.a == (byte) 0)
      {
        int num1 = index % texture2D.width;
        int num2 = index / texture2D.width;
        if (num1 > 0 && (double) colors[num2 * texture2D.width + (num1 - 1)].a > 0.0)
          color32 = (Color32) Recolor.ZeroAlpha(colors[num2 * texture2D.width + (num1 - 1)]);
        else if (num1 < texture2D.width - 1 && (double) colors[num2 * texture2D.width + (num1 + 1)].a > 0.0)
          color32 = (Color32) Recolor.ZeroAlpha(colors[num2 * texture2D.width + (num1 + 1)]);
        else if (num2 > 0 && (double) colors[(num2 - 1) * texture2D.width + num1].a > 0.0)
          color32 = (Color32) Recolor.ZeroAlpha(colors[(num2 - 1) * texture2D.width + num1]);
        else if (num2 < texture2D.height - 1 && (double) colors[(num2 + 1) * texture2D.width + num1].a > 0.0)
          color32 = (Color32) Recolor.ZeroAlpha(colors[(num2 + 1) * texture2D.width + num1]);
        else
          continue;
      }
      if ((int) color32.r == (int) color32.g && (int) color32.r == (int) color32.b)
      {
        if (color32.r != (byte) 0)
        {
          if (color32.r > (byte) 128)
          {
            if (color32.r < (byte) 200)
            {
              byte num = (byte) ((uint) color32.r + 55U);
              colors[index] = gray * (Color) new Color32(num, num, num, color32.a);
            }
          }
          else
          {
            byte num = (byte) ((int) color32.r * 2 - 1);
            colors[index] = gray * (Color) new Color32(num, num, num, color32.a);
          }
        }
      }
      else if (color32.r > (byte) 0)
      {
        if (color32.g == (byte) 0 && color32.b == (byte) 0)
          colors[index] = red * (Color) new Color32(color32.r, color32.r, color32.r, color32.a);
      }
      else if (color32.g > (byte) 0)
      {
        if (color32.r == (byte) 0 && color32.b == (byte) 0)
          colors[index] = green * (Color) new Color32(color32.g, color32.g, color32.g, color32.a);
      }
      else if (color32.b > (byte) 0 && color32.r == (byte) 0 && color32.g == (byte) 0)
        colors[index] = blue * (Color) new Color32(color32.b, color32.b, color32.b, color32.a);
    }
    texture2D.SetPixels(colors);
    texture2D.Apply(false);
    Texture2D texture2 = texture2D;
    Rect rect2 = spriteorigonal.rect;
    double width2 = (double) (int) rect2.width;
    rect2 = spriteorigonal.rect;
    double height2 = (double) (int) rect2.height;
    Rect rect3 = new Rect(0.0f, 0.0f, (float) width2, (float) height2);
    Vector2 pivot = new Vector2(spriteorigonal.pivot.x / (float) texture2D.width, spriteorigonal.pivot.y / (float) texture2D.height);
    double pixelsPerUnit = (double) spriteorigonal.pixelsPerUnit;
    return Global.AddSprite(Sprite.Create(texture2, rect3, pivot, (float) pixelsPerUnit));
  }

  public static Sprite PrestigeHat(Sprite spriteorigonal, Color gray)
  {
    Texture2D texture1 = spriteorigonal.texture;
    Rect rect1 = spriteorigonal.rect;
    int width1 = (int) rect1.width;
    rect1 = spriteorigonal.rect;
    int height1 = (int) rect1.height;
    int format = (int) texture1.format;
    Texture2D texture2D1 = new Texture2D(width1, height1, (TextureFormat) format, false, true);
    texture2D1.wrapMode = TextureWrapMode.Clamp;
    Texture2D texture2D2 = texture1;
    Rect rect2 = spriteorigonal.rect;
    int x = (int) rect2.x;
    rect2 = spriteorigonal.rect;
    int y = (int) rect2.y;
    rect2 = spriteorigonal.rect;
    int width2 = (int) rect2.width;
    rect2 = spriteorigonal.rect;
    int height2 = (int) rect2.height;
    Color[] pixels = texture2D2.GetPixels(x, y, width2, height2);
    for (int index = 0; index < pixels.Length; ++index)
    {
      Color32 color32 = (Color32) pixels[index];
      if (color32.a != (byte) 0 && (int) color32.r == (int) color32.g && (int) color32.r == (int) color32.b && color32.r > (byte) 0)
      {
        byte r = color32.r;
        pixels[index] = gray * (Color) new Color32(r, r, r, color32.a);
      }
    }
    texture2D1.SetPixels(pixels);
    texture2D1.Apply(false);
    Texture2D texture2 = texture2D1;
    Rect rect3 = spriteorigonal.rect;
    double width3 = (double) (int) rect3.width;
    rect3 = spriteorigonal.rect;
    double height3 = (double) (int) rect3.height;
    Rect rect4 = new Rect(0.0f, 0.0f, (float) width3, (float) height3);
    Vector2 pivot = new Vector2(spriteorigonal.pivot.x / (float) texture2D1.width, spriteorigonal.pivot.y / (float) texture2D1.height);
    double pixelsPerUnit = (double) spriteorigonal.pixelsPerUnit;
    return Global.AddSprite(Sprite.Create(texture2, rect4, pivot, (float) pixelsPerUnit));
  }

  public class Colors
  {
    public Color32 red = new Color32(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
    public Color32 green = new Color32(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
    public Color32 blue = new Color32(byte.MaxValue, byte.MaxValue, (byte) 51, byte.MaxValue);
    public Color32 gray = new Color32(byte.MaxValue, (byte) 204, (byte) 153, byte.MaxValue);

    public void Copy(Recolor.Colors b)
    {
      this.red = b.red;
      this.green = b.green;
      this.blue = b.blue;
      this.gray = b.gray;
    }

    public Color32 FromIndex(int i)
    {
      switch (i)
      {
        case 0:
          return this.red;
        case 1:
          return this.green;
        case 2:
          return this.blue;
        case 3:
          return this.gray;
        default:
          return (Color32) Color.white;
      }
    }

    public void ToIndex(int i, Color32 c)
    {
      switch (i)
      {
        case 0:
          this.red = c;
          break;
        case 1:
          this.green = c;
          break;
        case 2:
          this.blue = c;
          break;
        case 3:
          this.gray = c;
          break;
      }
    }
  }

  public class Block
  {
    public Color32[] pixels;
    public int originalWidth;
    public int width;
    public int height;
    public int x;
    public int y;

    public Color32 Get(int i)
    {
      return this.pixels[(this.y + i / this.width) * this.originalWidth + i % this.width + this.x];
    }
  }
}
