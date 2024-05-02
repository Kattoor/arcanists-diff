

using System;
using UnityEngine;

#nullable disable
namespace Educative
{
  [Serializable]
  public class LuaColor
  {
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public string toHex() => "#" + ColorUtility.ToHtmlStringRGBA((Color) LuaColor.From(this));

    public static LuaColor construct(byte r, byte g, byte b, byte a)
    {
      return new LuaColor() { r = r, g = g, b = b, a = a };
    }

    public static LuaColor construct(string hex)
    {
      Color color = Color.white;
      ColorUtility.TryParseHtmlString(hex, out color);
      Color32 color32 = (Color32) color;
      return new LuaColor()
      {
        r = color32.r,
        g = color32.g,
        b = color32.b,
        a = color32.a
      };
    }

    public static LuaColor From(Color32 c)
    {
      return new LuaColor()
      {
        r = c.r,
        g = c.g,
        b = c.b,
        a = c.a
      };
    }

    public static Color32 From(LuaColor c)
    {
      return new Color32()
      {
        r = c.r,
        g = c.g,
        b = c.b,
        a = c.a
      };
    }
  }
}
