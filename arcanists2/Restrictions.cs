

using System;
using System.Collections.Generic;

#nullable disable
[Serializable]
public class Restrictions
{
  public BitBools availableSpells = new BitBools();
  public int elementals = -1;
  public int alternateEle = -1;
  public const byte Version = 1;

  public Restrictions() => this.availableSpells.SetAll();

  public static Restrictions Combine(Restrictions a, Restrictions b)
  {
    Restrictions restrictions = new Restrictions();
    for (int index = 0; index < 8; ++index)
      restrictions.availableSpells.array[index] = a.availableSpells.array[index] & b.availableSpells.array[index];
    restrictions.elementals = a.elementals & b.elementals;
    return restrictions;
  }

  public bool AnyRestricted()
  {
    return !this.availableSpells.AllOn() || this.elementals != -1 || this.alternateEle != -1;
  }

  public bool HasRestricted(byte[] spells)
  {
    for (int index = 0; index < 16; ++index)
    {
      if (spells[index] < byte.MaxValue && !this.availableSpells[(int) spells[index]])
        return true;
    }
    return this.elementals != -1;
  }

  public bool CheckRestricted(int i) => i < 0 || i >= 256 || !this.availableSpells[i];

  public bool CheckElemental(int i) => (this.elementals & 1 << i) == 0;

  public static bool IsSpellRestricted(int i)
  {
    if (i < 0 || i >= 256)
      return true;
    return (!Client.viewSpellLocks.ViewRestricted() && !((UnityEngine.Object) RatedMenu.instance != (UnityEngine.Object) null) || Server._restrictions == null || Server._restrictions.availableSpells[i]) && (UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null && Client._gameFacts.settings.restrictions != null && !Client._gameFacts.settings.restrictions.availableSpells[i];
  }

  public static bool IsElementalRestricted(int i)
  {
    return (!Client.viewSpellLocks.ViewRestricted() && !((UnityEngine.Object) RatedMenu.instance != (UnityEngine.Object) null) || Server._restrictions == null || (Server._restrictions.elementals & 1 << i) == 0) && (UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null && Client._gameFacts.settings.restrictions != null && (Client._gameFacts.settings.restrictions.elementals & 1 << i) == 0;
  }

  public bool EleAvailable(BookOf b) => (this.elementals & 1 << (int) (b & (BookOf) 31)) != 0;

  public void Copy(Restrictions r)
  {
    this.availableSpells.Copy(r.availableSpells);
    this.elementals = r.elementals;
  }

  public void Serialize(myBinaryWriter w)
  {
    this.availableSpells.Serialize(w);
    w.Write(this.elementals);
  }

  public void DeserializeInstance(myBinaryReader w)
  {
    this.availableSpells.Deserialize(w);
    this.elementals = w.ReadInt32();
  }

  public static Restrictions Deserialize(myBinaryReader w, byte version)
  {
    Restrictions restrictions = new Restrictions();
    restrictions.DeserializeInstance(w);
    return restrictions;
  }

  public bool ToggleSpell(int i)
  {
    this.availableSpells[i] = !this.availableSpells[i];
    PopupRestrictions.Instance.SpellEnabled(i, this.availableSpells[i]);
    return this.availableSpells[i];
  }

  public bool ToggleBook(int e)
  {
    int num = e * 12;
    bool v = true;
    for (int index = 0; index < 12; ++index)
    {
      if (this.availableSpells[num + index])
      {
        v = false;
        break;
      }
    }
    for (int index = 0; index < 12; ++index)
    {
      this.availableSpells[num + index] = v;
      PopupRestrictions.Instance.SpellEnabled(num + index, v);
    }
    return v;
  }

  public bool ToggleElemental(int i)
  {
    this.elementals ^= 1 << i;
    PopupRestrictions.Instance.ElementalEnabled(i, (this.elementals & 1 << i) != 0);
    return (this.elementals & 1 << i) != 0;
  }

  public bool ToggleCastType(int e)
  {
    --e;
    CastType castType = (CastType) e;
    bool v = true;
    int num = (int) (RandomExtensions.LastBook() + 1) * 12;
    for (int index = 0; index < num; ++index)
    {
      if (Inert.Instance.spells[index].type == castType && this.availableSpells[index])
      {
        v = false;
        break;
      }
    }
    for (int index = 0; index < num; ++index)
    {
      if (Inert.Instance.spells[index].type == castType)
      {
        this.availableSpells[index] = v;
        PopupRestrictions.Instance.SpellEnabled(index, v);
      }
    }
    return v;
  }

  public bool ToggleSpellType(int e)
  {
    --e;
    SpellType spellType = (SpellType) e;
    bool v = true;
    int num = (int) (RandomExtensions.LastBook() + 1) * 12;
    for (int index = 0; index < num; ++index)
    {
      if (Inert.Instance.spells[index].spellType == spellType && this.availableSpells[index])
      {
        v = false;
        break;
      }
    }
    for (int index = 0; index < num; ++index)
    {
      if (Inert.Instance.spells[index].spellType == spellType)
      {
        this.availableSpells[(int) (byte) index] = v;
        PopupRestrictions.Instance.SpellEnabled(index, v);
      }
    }
    return v;
  }

  public int Valid(byte[] spells)
  {
    for (int index = 0; index < spells.Length; ++index)
    {
      if (!this.availableSpells[(int) spells[index]])
        return index;
    }
    return -1;
  }

  public List<int> AllInvalid(byte[] spells)
  {
    List<int> intList = new List<int>();
    for (int index = 0; index < spells.Length; ++index)
    {
      if (!this.availableSpells[(int) spells[index]])
        intList.Add(index);
    }
    return intList;
  }
}
