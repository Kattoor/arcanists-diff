

#nullable disable
public class PastBlits
{
  public int x;
  public int y;
  public int index;
  public bool ignoreAlpha;
  public FixedInt brightness;
  public bool rotate;
  public bool hue;

  public PastBlits()
  {
  }

  public PastBlits(int x, int y, int which, bool i, FixedInt brightness, bool rot = false)
  {
    this.x = x;
    this.y = y;
    this.index = which;
    this.ignoreAlpha = i;
    this.brightness = brightness;
    this.rotate = rot;
  }

  public PastBlits(int x, int y, int which, bool i, FixedInt brightness, bool rot, bool hue)
  {
    this.x = x;
    this.y = y;
    this.index = which;
    this.ignoreAlpha = i;
    this.brightness = brightness;
    this.rotate = rot;
    this.hue = hue;
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write(this.x);
    w.Write(this.y);
    w.Write(this.index);
    w.Write(this.ignoreAlpha);
    w.Write(this.brightness.RawValue);
    w.Write(this.rotate);
    w.Write(this.hue);
  }

  public static PastBlits Deserialize(myBinaryReader r)
  {
    return new PastBlits()
    {
      x = r.ReadInt32(),
      y = r.ReadInt32(),
      index = r.ReadInt32(),
      ignoreAlpha = r.ReadBoolean(),
      brightness = (FixedInt) r.ReadInt64(),
      rotate = r.ReadBoolean(),
      hue = r.ReadBoolean()
    };
  }
}
