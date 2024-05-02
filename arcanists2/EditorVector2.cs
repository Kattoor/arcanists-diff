// Decompiled with JetBrains decompiler
// Type: EditorVector2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class EditorVector2
{
  public long x;
  public long y;

  public static implicit operator EditorVector2(Vector2 v)
  {
    return new EditorVector2()
    {
      x = (long) ((Decimal) v.x * 1048576M),
      y = (long) ((Decimal) v.y * 1048576M)
    };
  }

  public MyLocation To() => new MyLocation((FixedInt) this.x, (FixedInt) this.y);
}
