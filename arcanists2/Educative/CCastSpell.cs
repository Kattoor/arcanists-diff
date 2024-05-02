// Decompiled with JetBrains decompiler
// Type: Educative.CCastSpell
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CCastSpell : Command
  {
    public int entity = -2;
    public SpellEnum spellEnum = SpellEnum.Fire_Ball;
    public Point point_target = new Point();
    public float angle;
    public float power = 1f;

    public CCastSpell() => this.type = Command.Type.CastSpell;
  }
}
