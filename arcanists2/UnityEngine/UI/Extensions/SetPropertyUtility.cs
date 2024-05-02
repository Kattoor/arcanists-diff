﻿// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.Extensions.SetPropertyUtility
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace UnityEngine.UI.Extensions
{
  internal static class SetPropertyUtility
  {
    public static bool SetColor(ref Color currentValue, Color newValue)
    {
      if ((double) currentValue.r == (double) newValue.r && (double) currentValue.g == (double) newValue.g && (double) currentValue.b == (double) newValue.b && (double) currentValue.a == (double) newValue.a)
        return false;
      currentValue = newValue;
      return true;
    }

    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
      if (currentValue.Equals((object) newValue))
        return false;
      currentValue = newValue;
      return true;
    }

    public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
    {
      if ((object) currentValue == null && (object) newValue == null || (object) currentValue != null && currentValue.Equals((object) newValue))
        return false;
      currentValue = newValue;
      return true;
    }
  }
}