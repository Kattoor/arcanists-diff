// Decompiled with JetBrains decompiler
// Type: KeySequence
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
