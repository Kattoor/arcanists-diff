// Decompiled with JetBrains decompiler
// Type: EmptyScene
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class EmptyScene : MonoBehaviour
{
  private void OnGUI()
  {
    if (GUILayout.Button("Load empty"))
      SceneManager.LoadScene("Empty");
    if (!GUILayout.Button("Load main"))
      return;
    SceneManager.LoadScene("mainmenu");
  }
}
