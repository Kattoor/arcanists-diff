

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
