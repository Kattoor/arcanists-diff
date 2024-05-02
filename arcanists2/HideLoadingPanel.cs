// Decompiled with JetBrains decompiler
// Type: HideLoadingPanel
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System.IO;
using UnityEngine;

#nullable disable
public class HideLoadingPanel : MonoBehaviour
{
  private float time;

  private void OnEnable() => this.time = 0.0f;

  private void Update()
  {
    if (Client.game == null)
      return;
    if (Client.game.MAPCREATED && !Client.game.resyncing)
    {
      this.gameObject.SetActive(false);
    }
    else
    {
      float num = Mathf.Clamp(Time.deltaTime, 0.0f, 0.0166f);
      if ((double) num < 0.20000000298023224)
        this.time += num;
      if ((double) this.time <= 20.0 || Client.game.receivedInitialMsg)
        return;
      this.time = -10000f;
      if (Client.connection != null && Client.connection.State == ConnectionState.Connected && !Client.game.isReplay)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
          {
            myBinaryWriter.Write((byte) 64);
            myBinaryWriter.Write("Took to long to respond");
          }
          Client.connection.SendBytes(memoryStream.ToArray());
        }
      }
      else
        Client.game.init_Resync(true);
    }
  }
}
