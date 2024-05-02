// Decompiled with JetBrains decompiler
// Type: BookExtensions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class BookExtensions
{
  public static Color ToColor(this BookOf b)
  {
    switch (b)
    {
      case BookOf.Arcane:
        return new Color(0.897059f, 0.0f, 1f);
      case BookOf.Flame:
        return new Color(1f, 0.0f, 0.0f);
      case BookOf.Stone:
        return new Color(0.434f, 0.1970201f, 0.0344207f);
      case BookOf.Storm:
        return new Color(1f, 1f, 0.0f);
      case BookOf.Frost:
        return new Color(0.0f, 1f, 1f);
      case BookOf.Underdark:
        return new Color(0.15f, 0.15f, 0.15f);
      case BookOf.OverLight:
        return new Color(1f, 1f, 1f);
      case BookOf.Nature:
        return new Color(0.0f, 0.759f, 0.0f);
      case BookOf.Seas:
        return new Color(0.0f, 0.372f, 1f);
      case BookOf.Cogs:
        return new Color(1f, 0.484f, 0.0f);
      case BookOf.Seasons:
        return new Color(0.2f, 1f, 0.0f);
      case BookOf.Illusion:
        return new Color(0.7137255f, 0.607843161f, 0.7764706f);
      case BookOf.Blood:
        return new Color(0.423529416f, 0.03529412f, 0.145098045f);
      case BookOf.The_Wilds:
        return new Color(0.733333349f, 0.5882353f, 0.34117648f);
      case BookOf.Cosmos:
        return new Color(0.243137255f, 0.0f, 0.356862754f);
      default:
        return Color.white;
    }
  }
}
