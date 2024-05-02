// Decompiled with JetBrains decompiler
// Type: Cosmetics
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Cosmetics
{
  public BitBools[] array = new BitBools[8]
  {
    new BitBools(),
    new BitBools(),
    new BitBools(),
    new BitBools(),
    new BitBools(),
    new BitBools(),
    new BitBools(),
    new BitBools()
  };

  public BitBools body => this.array[0];

  public BitBools head => this.array[1];

  public BitBools leftArm => this.array[2];

  public BitBools rightArm => this.array[3];

  public BitBools hat => this.array[4];

  public BitBools beard => this.array[5];

  public BitBools spells => this.array[6];

  public BitBools achievements => this.array[7];

  public override bool Equals(object obj)
  {
    if (!(obj is Cosmetics))
      return base.Equals(obj);
    Cosmetics cosmetics = obj as Cosmetics;
    for (int index = 0; index < this.array.Length; ++index)
    {
      if (!this.array[index].Equals((object) cosmetics.array[index]))
        return false;
    }
    return true;
  }

  public Cosmetics()
  {
  }

  public Cosmetics(Cosmetics c)
  {
    for (int index = 0; index < 8; ++index)
      this.array[index].Copy(c.array[index]);
  }

  public void SetAll()
  {
    for (int index = 0; index < 7; ++index)
      this.array[index].SetAll();
    this.achievements.ResetAll();
  }

  public void LockRest()
  {
    for (int index = 145; index < 256; ++index)
      this.array[0][index] = false;
    for (int index = 95; index < 256; ++index)
      this.array[1][index] = false;
    for (int maxValue = (int) sbyte.MaxValue; maxValue < 256; ++maxValue)
      this.array[2][maxValue] = false;
    for (int index = 165; index < 256; ++index)
      this.array[3][index] = false;
    for (int index = 161; index < 256; ++index)
      this.array[4][index] = false;
    for (int index = 95; index < 256; ++index)
      this.array[5][index] = false;
  }

  public void Copy(Cosmetics b)
  {
    for (int index = 0; index < 8; ++index)
      this.array[index].Copy(b.array[index]);
  }

  public void Serialize(myBinaryWriter w)
  {
    for (int index = 0; index < 8; ++index)
      this.array[index].Serialize(w);
  }

  public static Cosmetics Deserialize(myBinaryReader r)
  {
    Cosmetics cosmetics = new Cosmetics();
    for (int index = 0; index < 8; ++index)
      cosmetics.array[index].Deserialize(r);
    return cosmetics;
  }

  public enum Outfit
  {
    Body,
    Head,
    LeftHand,
    RightHand,
    Hat,
    Beard,
    Spells,
    Achievement,
  }
}
