

using UnityEngine;

#nullable disable
public interface ISpellBridge
{
  string GetName { get; }

  SpellType GetSpellType { get; }

  SpellEnum GetSpellEnum { get; }

  BookOf book { get; }

  bool FromArmageddon { get; }

  ZCreature GetParent { get; }

  bool GetGoToTarget { get; }

  GameObject GetToSummon { get; }

  Spell GetBaseSpell { get; }

  ExplosionCutout GetExplosionCutout { get; }
}
