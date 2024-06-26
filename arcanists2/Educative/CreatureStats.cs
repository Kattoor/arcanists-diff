﻿

using System;
using System.Collections.Generic;

#nullable disable
namespace Educative
{
  [Serializable]
  public class CreatureStats
  {
    public List<SpellSlot> spells = new List<SpellSlot>();
    public CreatureType type;
    public CreatureRace race;
    public int radius = 5;
    public int health = 250;
    public int maxHealth = 250;
    public FixedInt speed = (FixedInt) 1;
    public EditorVector2 LongJumpData;
    public EditorVector2 HighJumpData;
    public bool affectedByGravity = true;
    public bool flying;
    public bool mountable;
    public bool canMount;
    public bool phantom;
    public bool gliding;
    public bool canMove = true;
    public bool usingGlide = true;
    public bool waterWalking;
    public bool frostWalking;
    public EditorFixedInt massMulti = (EditorFixedInt) 1f;
    public EditorFixedInt minAngle = (EditorFixedInt) -180;
    public EditorFixedInt maxAngle = (EditorFixedInt) 180;
    public int effectorMaxTurnsOverride = -1;
  }
}
