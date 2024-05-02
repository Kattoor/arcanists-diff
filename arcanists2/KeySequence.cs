// Decompiled with JetBrains decompiler
// Type: KeySequence
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class KeySequence
{
  public KeyCode[] sequence;
  private int cur;

  public KeySequence(KeyCode[] k) => this.sequence = k;

  public bool Complete()
  {
    if (Input.anyKeyDown)
    {
      if (Input.GetKeyDown(this.sequence[this.cur]))
      {
        ++this.cur;
        if (this.cur == this.sequence.Length)
        {
          this.cur = 0;
          return true;
        }
      }
      else
        this.cur = 0;
    }
    return false;
  }

  public void Reset() => this.cur = 0;
}
