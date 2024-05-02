// Decompiled with JetBrains decompiler
// Type: testing
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class testing : MonoBehaviour
{
  [Header("perlin")]
  public int _octaveCount = 1;
  public float _frecuency = 1f;
  public float _persistence = 0.5f;
  public float threshold = 0.5f;
  [Header("other")]
  public float angle;
  public int x;
  public int y;
  public Texture2D paddingSprite;
  public Image sr;
  public Texture2D texure;
  public Text text;
  public Sprite[] sprites;
  private int count;

  [ContextMenu("GetTop")]
  public void GetTop()
  {
    Color32[] pixels32_1 = this.texure.GetPixels32();
    Texture2D tex = new Texture2D(this.texure.width, this.texure.height);
    Color32[] pixels32_2 = tex.GetPixels32();
    for (int index1 = 0; index1 < this.texure.width; ++index1)
    {
      int num = 0;
      for (int index2 = this.texure.height - 1; index2 >= 0 && num < 5; --index2)
      {
        if (pixels32_1[index1 + tex.width * index2].a != (byte) 0)
        {
          ++num;
          pixels32_2[index1 + tex.width * index2] = pixels32_1[index1 + tex.width * index2];
        }
      }
    }
    tex.SetPixels32(pixels32_2);
    tex.Apply();
    File.WriteAllBytes("SnowOutline.png", tex.EncodeToPNG());
  }

  [ContextMenu("Paint")]
  public void paint()
  {
    RotateImage.Render(new Surface(this.sr.sprite.texture, this.sr.sprite.texture.GetPixels32()), new Surface(this.texure, this.texure.GetPixels32()), this.x, this.y, this.angle, 1, true);
  }

  [ContextMenu("Arc Maps")]
  public void allmap()
  {
    foreach (Sprite sprite in this.sprites)
      this.arcmap(sprite);
  }

  public void arcmap(Sprite z)
  {
    Texture2D texture = z.texture;
    Color32[] pixels32 = texture.GetPixels32();
    using (MemoryStream memoryStream1 = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter1 = new myBinaryWriter((Stream) memoryStream1))
      {
        myBinaryWriter1.Write(texture.width);
        myBinaryWriter1.Write(texture.height);
        using (MemoryStream memoryStream2 = new MemoryStream())
        {
          using (myBinaryWriter myBinaryWriter2 = new myBinaryWriter((Stream) memoryStream2))
          {
            myBinaryWriter2.Write(pixels32.Length);
            for (int index = 0; index < pixels32.Length; ++index)
              myBinaryWriter2.Write(pixels32[index]);
          }
          byte[] buffer = SevenZipHelper.Compress(memoryStream2.ToArray());
          myBinaryWriter1.Write(buffer);
        }
      }
      File.WriteAllBytes(z.name + ".arcmap", memoryStream1.ToArray());
    }
  }

  private void Update()
  {
    if (!((UnityEngine.Object) this.text != (UnityEngine.Object) null))
      return;
    ++this.count;
    this.text.text = this.count.ToString();
  }

  [ContextMenu("Padding")]
  private void padding()
  {
    Color32[] pixels32 = this.paddingSprite.GetPixels32();
    int width = this.paddingSprite.width;
    int height1 = this.paddingSprite.height;
    int nW = width + width / this.x * 2;
    int height2 = height1 + height1 / this.y * 2;
    Color32[] colors = new Color32[nW * height2];
    int num1 = 1;
    Func<int, int, int> func1 = (Func<int, int, int>) ((a, b) => a * width + b);
    Func<int, int, int> func2 = (Func<int, int, int>) ((a, b) => a * nW + b);
    int num2 = 0;
    while (num2 < height1)
    {
      int num3 = 1;
      int num4 = 0;
      while (num4 < width)
      {
        colors[func2(num1, num3)] = pixels32[func1(num2, num4)];
        if (num4 % this.x == this.x - 1 && num3 != 2)
          num3 += 2;
        ++num4;
        ++num3;
      }
      if (num2 % this.y == 0 && num1 != 1)
        num1 += 2;
      ++num2;
      ++num1;
    }
    Texture2D tex = new Texture2D(nW, height2);
    tex.SetPixels32(colors);
    File.WriteAllBytes("New " + this.paddingSprite.name + ".png", tex.EncodeToPNG());
  }

  [ContextMenu("Render Out Center")]
  private void RenderOutCenter()
  {
    Color32[] pixels32_1 = this.texure.GetPixels32();
    Texture2D tex = new Texture2D(this.texure.width, this.texure.height);
    int num1 = tex.width / 2;
    int num2 = tex.height / 2;
    Color32[] pixels32_2 = tex.GetPixels32();
    for (int index = 0; index < pixels32_1.Length; ++index)
      pixels32_2[index] = pixels32_1[index];
    List<Coords> coordsList1 = new List<Coords>();
    List<Coords> coordsList2 = new List<Coords>();
    List<Coords> coordsList3 = new List<Coords>();
    for (int radius = 63; radius >= 1; --radius)
    {
      List<Coords> coordsList4 = coordsList2;
      coordsList2 = MapGenerator.getOutlineArray(radius);
      if (coordsList4.Count > 0)
      {
        foreach (Coords coords1 in coordsList2)
        {
          Coords cur = coords1;
          int num3 = cur.x + num1;
          int num4 = cur.y + num2;
          foreach (Coords b in coordsList4)
          {
            if (coordsList3.Count < 3)
              coordsList3.Add(b);
            else if (cur.Distance(b) < coordsList3[2].Distance(cur))
              coordsList3[2] = b;
            coordsList3.Sort((Comparison<Coords>) ((k, v) => k.Distance(cur) - v.Distance(cur)));
          }
          int num5 = 0;
          int num6 = 0;
          int num7 = 0;
          foreach (Coords coords2 in coordsList3)
          {
            num5 += (int) pixels32_2[(coords2.y + num2) * tex.width + (coords2.x + num1)].r;
            num6 += (int) pixels32_2[(coords2.y + num2) * tex.width + (coords2.x + num1)].g;
            num7 += (int) pixels32_2[(coords2.y + num2) * tex.width + (coords2.x + num1)].b;
          }
          int r = num5 / coordsList3.Count;
          int b1 = num7 / coordsList3.Count;
          int g = num6 / coordsList3.Count;
          pixels32_2[num4 * tex.width + num3] = new Color32((byte) r, (byte) g, (byte) b1, byte.MaxValue);
        }
      }
    }
    tex.SetPixels32(pixels32_2);
    tex.Apply();
    File.WriteAllBytes("renderout.png", tex.EncodeToPNG());
  }
}
