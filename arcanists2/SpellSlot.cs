

using Newtonsoft.Json;
using System;
using UnityEngine;

#nullable disable
[Serializable]
public class SpellSlot
{
  [JsonIgnore]
  public Spell spell;
  public bool syncWithParent;
  public bool isPresent;
  public bool isLevel5;
  public int MaxUses = -1;
  public int RechargeTime;
  public bool EndsTurn = true;
  public int TurnsTillFirstUse;
  [NonSerialized]
  public int disabledturn = -1;
  private int _lastturnfired = -1;
  private int _useduses;

  public int LastTurnFired => this._lastturnfired;

  public int UsedUses => this._useduses;

  public bool MaxUsesReached() => this._useduses >= this.MaxUses && this.MaxUses > 0;

  public void RefundAUse()
  {
    if (this._useduses > 0)
      --this._useduses;
    this._lastturnfired = -1;
  }

  public int SetUses
  {
    set => this._useduses = value;
  }

  public void AddMaxUse() => ++this.MaxUses;

  public void Fired(ZCreature c)
  {
    this.IncreaseUses();
    this.SetTurnFired = c.parent.localTurn;
    if (!this.isPresent)
      return;
    c.spells.Remove(this);
  }

  public void CopyStats(SpellSlot other)
  {
    this.TurnsTillFirstUse = other.TurnsTillFirstUse;
    this._lastturnfired = other._lastturnfired;
    this._useduses = other._useduses;
    this.MaxUses = other.MaxUses;
    this.isPresent = other.isPresent;
    this.EndsTurn = other.EndsTurn;
    this.syncWithParent = other.syncWithParent;
    if (!((UnityEngine.Object) this.spell != (UnityEngine.Object) null) || this.spell.spellEnum != SpellEnum.Duplication)
      return;
    this._useduses = this.MaxUses;
  }

  public SpellSlot()
  {
    if (!((UnityEngine.Object) this.spell != (UnityEngine.Object) null))
      return;
    this.SetSpell(this.spell);
    Debug.Log((object) "Serialized!!!!!!!!!!");
  }

  public SpellSlot(Spell p) => this.SetSpell(p);

  public void SetSpell(Spell p)
  {
    this.spell = p;
    this.MaxUses = this.spell.MaxUses;
    this.RechargeTime = p.RechargeTime;
    this.EndsTurn = p.EndsTurn;
    this.TurnsTillFirstUse = p.TurnsTillFirstUse;
  }

  public void DecreaseCooldown() => --this.RechargeTime;

  public SpellSlot(SpellSlot s)
  {
    this.spell = s.spell;
    this.MaxUses = s.MaxUses;
    this.RechargeTime = s.RechargeTime;
    this.EndsTurn = s.EndsTurn;
    this.TurnsTillFirstUse = s.TurnsTillFirstUse;
    this._lastturnfired = s._lastturnfired;
    this._useduses = s._useduses;
    this.isPresent = s.isPresent;
    this.syncWithParent = s.syncWithParent;
  }

  public void Serialize(ZGame game, myBinaryWriter writer)
  {
    writer.Write(this.spell.name);
    bool flag = game.xSlot.Contains(this);
    writer.Write(flag);
    if (flag)
      return;
    game.xSlot.Add(this);
    writer.Write(this.MaxUses);
    writer.Write(this.RechargeTime);
    writer.Write(this.EndsTurn);
    writer.Write(this.TurnsTillFirstUse);
    writer.Write(this._lastturnfired);
    writer.Write(this._useduses);
    writer.Write(this.isPresent);
    writer.Write(this.syncWithParent);
  }

  public static SpellSlot Deserialize(ZPerson parent, myBinaryReader reader)
  {
    string s = reader.ReadString();
    if (reader.ReadBoolean())
    {
      ZCreature zcreature = parent.first();
      Spell spell = Inert.GetSpell(s);
      return (ZComponent) zcreature != (object) null && (UnityEngine.Object) spell != (UnityEngine.Object) null ? zcreature.GetSpellSlot(spell.spellEnum) ?? new SpellSlot(spell) : new SpellSlot(Inert.GetSpell(SpellEnum.Arcane_Arrow));
    }
    return new SpellSlot()
    {
      spell = Inert.GetSpell(s),
      MaxUses = reader.ReadInt32(),
      RechargeTime = reader.ReadInt32(),
      EndsTurn = reader.ReadBoolean(),
      TurnsTillFirstUse = reader.ReadInt32(),
      _lastturnfired = reader.ReadInt32(),
      _useduses = reader.ReadInt32(),
      isPresent = reader.ReadBoolean(),
      syncWithParent = reader.ReadBoolean()
    };
  }

  public int SetTurnFired
  {
    set => this._lastturnfired = value;
  }

  public void ResetUses() => this._useduses = 0;

  public void IncreaseUses() => ++this._useduses;
}
