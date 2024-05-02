// Decompiled with JetBrains decompiler
// Type: Educative.CAddSpell
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CAddSpell : Command
  {
    public int entity = -2;
    public SpellEnum spellEnum = SpellEnum.Fire_Ball;
    public bool bool_remove;
    public int cooldown = -1;
    public int turnsFirstUse = -1;
    public int maxUses = -1;

    public CAddSpell() => this.type = Command.Type.AddSpell;
  }
}
