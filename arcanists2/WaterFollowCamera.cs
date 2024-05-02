// Decompiled with JetBrains decompiler
// Type: WaterFollowCamera
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class WaterFollowCamera : MonoBehaviour
{
  private Vector3 pos = Vector3.zero;

  private void Start()
  {
  }

  private void LateUpdate()
  {
    if (Client.game?.map != null)
      this.pos.x = (float) (Client.game.map.Width / 2);
    this.transform.position = this.pos;
  }
}
