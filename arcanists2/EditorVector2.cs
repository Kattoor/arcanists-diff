// Decompiled with JetBrains decompiler
// Type: EditorVector2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
