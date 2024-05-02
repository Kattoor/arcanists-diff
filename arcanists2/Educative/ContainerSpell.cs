// Decompiled with JetBrains decompiler
// Type: Educative.ContainerSpell
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;

#nullable disable
namespace Educative
{
  public class ContainerSpell
  {
    [MoonSharpHidden]
    public SpellSlot slot;

    [MoonSharpHidden]
    public ContainerSpell(SpellSlot s) => this.slot = s;

    public int uses
    {
      get => this.slot.UsedUses;
      set => this.slot.SetUses = value;
    }

    public int maxUses
    {
      get => this.slot.MaxUses;
      set => this.slot.MaxUses = value;
    }

    public int rechargeTime
    {
      get => this.slot.RechargeTime;
      set => this.slot.RechargeTime = value;
    }

    public int lastTurnFired
    {
      get => this.slot.LastTurnFired;
      set => this.slot.SetTurnFired = value;
    }

    public bool isPresent
    {
      get => this.slot.isPresent;
      set => this.slot.isPresent = value;
    }

    public bool locked
    {
      get => this.slot.disabledturn == 999;
      set => this.slot.disabledturn = value ? 999 : 0;
    }

    public SpellEnum spellEnum => this.slot.spell.spellEnum;

    public CastType castType => this.slot.spell.type;

    public DamageType damageType => this.slot.spell.damageType;

    public BookOf book => this.slot.spell.bookOf;

    public int damage => this.slot.spell.damage;

    public int explosionRadius => this.slot.spell.EXORADIUS;

    public string description
    {
      get => Descriptions.GetSpellDescription(this.slot.spell.name, this.slot, true).description;
    }

    public string descriptionOnly
    {
      get => Descriptions.GetSpellDescription(this.slot.spell.name, this.slot).description;
    }

    public string descriptionExtra
    {
      get => Descriptions.GetSpellDescription(this.slot.spell.name, this.slot).extra;
    }

    public string name => this.slot.spell.name;
  }
}
