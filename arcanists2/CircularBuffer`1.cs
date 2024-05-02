// Decompiled with JetBrains decompiler
// Type: CircularBuffer`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public class CircularBuffer<T>
{
  private T[] list;
  private int curIndex;
  private bool full;

  public CircularBuffer(int count) => this.list = new T[count];

  public void Add(T item)
  {
    this.list[this.curIndex] = item;
    ++this.curIndex;
    if (this.curIndex < this.list.Length)
      return;
    this.full = true;
    this.curIndex = 0;
  }

  public int Count => !this.full ? this.curIndex : this.list.Length;

  public int Capacity => this.list.Length;

  public T this[int index]
  {
    get
    {
      if (!this.full)
        return this.list[index];
      index += this.curIndex;
      return index >= this.list.Length ? this.list[index - this.list.Length] : this.list[index];
    }
  }

  public T GetLast(int offset)
  {
    offset = this.curIndex - offset - 1;
    return offset < 0 ? this.list[offset + this.list.Length] : this.list[offset];
  }
}
