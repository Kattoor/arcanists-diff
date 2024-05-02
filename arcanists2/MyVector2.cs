

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
