// Decompiled with JetBrains decompiler
// Type: RandomExtensions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public static class RandomExtensions
{
  private static Dictionary<SpellEnum, int> spellToIndex;

  public static BookOf LastBook() => BookOf.Cosmos;

  public static int Clamp(this ZGame.GameType gameType) => Mathf.Clamp((int) gameType, 0, 2);

  public static string ToStringX(this BookOf b) => b.ToString().Replace("_", " ");

  public static Color32 To32(this Color c)
  {
    return new Color32()
    {
      r = (byte) Mathf.Clamp(Mathf.Round(c.r * (float) byte.MaxValue), 0.0f, (float) byte.MaxValue),
      g = (byte) Mathf.Clamp(Mathf.Round(c.g * (float) byte.MaxValue), 0.0f, (float) byte.MaxValue),
      b = (byte) Mathf.Clamp(Mathf.Round(c.b * (float) byte.MaxValue), 0.0f, (float) byte.MaxValue),
      a = (byte) Mathf.Clamp(Mathf.Round(c.a * (float) byte.MaxValue), 0.0f, (float) byte.MaxValue)
    };
  }

  public static bool Online(this Location loc) => loc > Location.Disconnecting;

  public static void HideIfWebGL(this GameObject g)
  {
  }

  public static List<int> AllIndexesOf(this string str, string value)
  {
    List<int> intList = new List<int>();
    if (string.IsNullOrEmpty(value))
      return intList;
    int startIndex = 0;
    while (true)
    {
      int num = str.IndexOf(value, startIndex);
      if (num != -1)
      {
        intList.Add(num);
        startIndex = num + value.Length;
      }
      else
        break;
    }
    return intList;
  }

  public static bool ContainsSpecialCharacters(this string s)
  {
    return Regex.IsMatch(s, "[^a-zA-Z0-9_. -]");
  }

  public static bool IsFlight(this SpellEnum e)
  {
    return e == SpellEnum.Flight || e == SpellEnum.Whistling_Winds || e == SpellEnum.Vampire_Bat;
  }

  public static bool Has(this FamiliarType f, FamiliarType t) => (f & t) != 0;

  public static int Index(this SpellEnum s)
  {
    if (RandomExtensions.spellToIndex == null)
    {
      RandomExtensions.spellToIndex = new Dictionary<SpellEnum, int>(Inert.Instance.spells.Count);
      for (int index = 0; index < Inert.Instance.spells.Count; ++index)
        RandomExtensions.spellToIndex[Inert.Instance.spells[index].spellEnum] = index;
    }
    int num = 0;
    return RandomExtensions.spellToIndex.TryGetValue(s, out num) ? num : -1;
  }

  public static bool CaseInsensitiveContains(
    this string text,
    string value,
    StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
  {
    return text.IndexOf(value, stringComparison) >= 0;
  }

  public static void SetPosition(this Transform t, MyLocation p)
  {
    t.position = (Vector3) p.ToSinglePrecision();
  }

  public static void DestroyThis(this Component a) => UnityEngine.Object.Destroy((UnityEngine.Object) a);

  public static int Squared(this int x) => x * x;

  public static FixedInt Squared(this FixedInt x) => x * x;

  public static void DestroyChildern(this RectTransform a)
  {
    for (int index = a.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) a.GetChild(index).gameObject);
    a.DetachChildren();
  }

  public static string ToStringX(this byte[] b, string begining)
  {
    StringBuilder stringBuilder = new StringBuilder(begining);
    stringBuilder.Append(" {");
    for (int index = 0; index < b.Length - 1; ++index)
      stringBuilder.Append(b[index]).Append(", ");
    if (b.Length != 0)
      stringBuilder.Append(b[b.Length - 1]);
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }

  public static bool HasStyle(this GameStyle s, GameStyle t) => (s & t) != 0;

  public static void SetNativeSize2(this Image i)
  {
    Sprite sprite = i.sprite;
    if (!((UnityEngine.Object) sprite != (UnityEngine.Object) null))
      return;
    ((RectTransform) i.transform).sizeDelta = new Vector2((float) sprite.texture.width, (float) sprite.texture.height);
  }

  public static void StartColor(this ParticleSystem p, Color c)
  {
    p.main.startColor = (ParticleSystem.MinMaxGradient) c;
  }

  public static Color32[] GetReadablePixels(this Texture2D texture)
  {
    if (texture.isReadable)
      return texture.GetPixels32();
    RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
    Graphics.Blit((Texture) texture, temporary);
    RenderTexture active = RenderTexture.active;
    RenderTexture.active = temporary;
    Texture2D texture2D = new Texture2D(texture.width, texture.height);
    texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) temporary.width, (float) temporary.height), 0, 0);
    texture2D.Apply();
    RenderTexture.active = active;
    RenderTexture.ReleaseTemporary(temporary);
    return texture2D.GetPixels32();
  }

  public static Color[] GetReadablePixels(
    this Texture2D texture,
    int x,
    int y,
    int blockWidth,
    int blockHeight)
  {
    RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
    Graphics.Blit((Texture) texture, temporary);
    RenderTexture active = RenderTexture.active;
    RenderTexture.active = temporary;
    Texture2D texture2D = new Texture2D(texture.width, texture.height);
    texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) temporary.width, (float) temporary.height), 0, 0);
    texture2D.Apply();
    RenderTexture.active = active;
    RenderTexture.ReleaseTemporary(temporary);
    return texture2D.GetPixels(x, y, blockWidth, blockHeight);
  }

  public static bool CompareColors(Color32 a, Color32 b)
  {
    return (int) a.r == (int) b.r && (int) a.g == (int) b.g && (int) a.b == (int) b.b;
  }

  public static Color32 RandomColor(System.Random r)
  {
    return new Color32((byte) r.Next((int) byte.MaxValue), (byte) r.Next((int) byte.MaxValue), (byte) r.Next((int) byte.MaxValue), byte.MaxValue);
  }

  public static bool Is(this OperatingSystem os, OperatingSystem e) => (os & e) != 0;

  public static bool IsNot(this OperatingSystem os, OperatingSystem e)
  {
    return (os & e) == (OperatingSystem) 0;
  }
}
