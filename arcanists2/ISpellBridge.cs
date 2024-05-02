// Decompiled with JetBrains decompiler
// Type: ISpellBridge
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
