// Decompiled with JetBrains decompiler
// Type: ArchmageStaff
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ArchmageStaff : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  [NonSerialized]
  public Creature target;
  [Header("Rotation")]
  public float speed = 2f;
  public int id;
  public bool triStaff;
  public int triIndex;
  private Queue<Vector3> oldPos = new Queue<Vector3>();

  private void Start()
  {
    for (int index = 0; index < 15; ++index)
      this.oldPos.Enqueue(this.target.transform.position);
  }

  private void Update()
  {
    if ((int) this.transform.position.y == 1000 && Client.map.Height > 0)
    {
      if (this.triStaff)
        this.transform.position = new Vector3((float) (Client.map.Width / 2 * this.triIndex), (float) (Client.map.Height + 1000));
      else if (Client.game.gameFacts.settingsPlayer.Count == 1)
        this.transform.position = new Vector3(0.0f, (float) (Client.map.Height + 1000));
      else
        this.transform.position = new Vector3((float) (Client.map.Width / (Client.game.gameFacts.settingsPlayer.Count - 1) * this.id), (float) (Client.map.Height + 1000));
    }
    if ((bool) (UnityEngine.Object) this.target && (bool) (UnityEngine.Object) this.target.transform)
    {
      this.oldPos.Enqueue(this.target.transform.position);
      Vector3 vector3_1 = this.oldPos.Dequeue();
      Coords coords = Client.map.bresenhamsLineCastOnlyTerrain(new Coords((int) this.transform.position.x, (int) this.transform.position.y), new Coords((int) vector3_1.x, (int) vector3_1.y));
      Vector3 vector3_2 = vector3_1 - this.transform.position;
      if (coords == null)
        this.spriteRenderer.size = (Vector2) new Vector3((float) ((double) vector3_2.magnitude - (double) this.target.radius - 25.0), 50f);
      vector3_2.Normalize();
      this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(vector3_2.y, vector3_2.x) * 57.29578f), this.speed * Time.deltaTime);
      if (coords == null)
      {
        if ((double) this.spriteRenderer.color.a >= 0.75)
          return;
        Color color = this.spriteRenderer.color;
        color.a += Time.deltaTime;
        if ((double) color.a > 0.75)
          color.a = 0.75f;
        this.spriteRenderer.color = color;
      }
      else
      {
        if ((double) this.spriteRenderer.color.a <= 0.0)
          return;
        Color color = this.spriteRenderer.color;
        color.a -= Time.deltaTime;
        if ((double) color.a < 0.0)
          color.a = 0.0f;
        this.spriteRenderer.color = color;
      }
    }
    else
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }
}
