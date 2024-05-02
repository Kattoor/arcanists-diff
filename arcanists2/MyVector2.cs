// Decompiled with JetBrains decompiler
// Type: MyVector2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using UnityEngine;

#nullable disable
public struct MyVector2
{
  public float x;
  public float y;

  public MyVector2(float x, float y)
  {
    this.x = x;
    this.y = y;
  }

  public MyVector2(Vector2 b)
  {
    this.x = b.x;
    this.y = b.y;
  }

  [JsonIgnore]
  public static MyVector2 zero => new MyVector2(0.0f, 0.0f);

  public static implicit operator Vector2(MyVector2 d) => new Vector2(d.x, d.y);

  public static implicit operator MyVector2(Vector2 d) => new MyVector2(d.x, d.y);
}
