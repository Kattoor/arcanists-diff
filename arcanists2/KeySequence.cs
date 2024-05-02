

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
