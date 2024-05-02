// Decompiled with JetBrains decompiler
// Type: SpellsOnly
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
[Serializable]
public class SpellsOnly
{
  public byte[] spells = new byte[16]
  {
    (byte) 0,
    (byte) 1,
    (byte) 2,
    (byte) 3,
    (byte) 4,
    (byte) 5,
    (byte) 6,
    (byte) 7,
    (byte) 8,
    (byte) 9,
    (byte) 10,
    (byte) 11,
    (byte) 12,
    (byte) 13,
    (byte) 14,
    (byte) 15
  };
  public byte fullBook;
  public byte extraInfo;

  public BookOf Elemental => (BookOf) ((int) this.fullBook - 1);

  public bool SeasonsIsHoliday
  {
    get => ((uint) this.extraInfo & 1U) > 0U;
    set
    {
      if (value)
        this.extraInfo |= (byte) 1;
      else
        this.extraInfo &= (byte) 254;
    }
  }

  public static SpellsOnly Deserialize(myBinaryReader r)
  {
    SpellsOnly spellsOnly = new SpellsOnly();
    spellsOnly.fullBook = r.ReadByte();
    spellsOnly.extraInfo = r.ReadByte();
    for (int index = 0; index < 16; ++index)
      spellsOnly.spells[index] = r.ReadByte();
    return spellsOnly;
  }

  public static SpellsOnly OLDDeserialize(myBinaryReader r)
  {
    SpellsOnly spellsOnly = new SpellsOnly();
    spellsOnly.fullBook = r.ReadByte();
    for (int index = 0; index < 16; ++index)
      spellsOnly.spells[index] = r.ReadByte();
    return spellsOnly;
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write(this.fullBook);
    w.Write(this.extraInfo);
    for (int index = 0; index < 16; ++index)
      w.Write(this.spells[index]);
  }

  public void Copy(SettingsPlayer b)
  {
    this.fullBook = b.fullBook;
    this.extraInfo = b.extraInfo;
    for (int index = 0; index < 16; ++index)
      this.spells[index] = b.spells[index];
  }

  private void DefaultSpells()
  {
    this.spells = new byte[16]
    {
      (byte) 0,
      (byte) 1,
      (byte) 2,
      (byte) 3,
      (byte) 4,
      (byte) 5,
      (byte) 6,
      (byte) 7,
      (byte) 8,
      (byte) 9,
      (byte) 10,
      (byte) 11,
      (byte) 12,
      (byte) 13,
      (byte) 14,
      (byte) 15
    };
  }

  public void VerifySpells()
  {
    HashSet<byte> byteSet = new HashSet<byte>();
    int[] numArray = new int[(int) (RandomExtensions.LastBook() + 1)];
    for (int index1 = 0; index1 < 16; ++index1)
    {
      byte spell = this.spells[index1];
      int index2 = (int) spell / 12;
      if (index2 < 0 || index2 >= numArray.Length)
      {
        this.spells[index1] = byte.MaxValue;
      }
      else
      {
        int num = (int) spell % 12;
        if (num < 10)
        {
          if (num % 2 == 1 && spell >= (byte) 12 && (index1 == 0 || (int) this.spells[index1 - 1] != (int) spell - 1))
          {
            this.DefaultSpells();
            break;
          }
        }
        else if (index2 > 0 && numArray[index2] < 5)
        {
          this.DefaultSpells();
          break;
        }
        if (byteSet.Contains(spell))
        {
          this.DefaultSpells();
          break;
        }
        ++numArray[index2];
        byteSet.Add(spell);
      }
    }
  }
}
