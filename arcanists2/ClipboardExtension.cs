// Decompiled with JetBrains decompiler
// Type: ClipboardExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public static class ClipboardExtension
{
  public static void CopyToClipboard(this string str) => Global.systemCopyBuffer = str;

  public static bool IsNull(this UnityEvent x)
  {
    for (int index = 0; index < x.GetPersistentEventCount(); ++index)
    {
      if (x.GetPersistentTarget(index) != (Object) null)
        return false;
    }
    return true;
  }

  public static JObject ToJson<T>(T t)
  {
    return JObject.FromObject((object) t, new JsonSerializer()
    {
      TypeNameHandling = TypeNameHandling.Auto
    });
  }

  public static T FromJson<T>(JObject j)
  {
    return j.ToObject<T>(new JsonSerializer()
    {
      TypeNameHandling = TypeNameHandling.Auto
    });
  }
}
