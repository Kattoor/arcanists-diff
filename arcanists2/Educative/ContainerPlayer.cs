// Decompiled with JetBrains decompiler
// Type: Educative.ContainerPlayer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class ContainerPlayer
  {
    [MoonSharpHidden]
    public ZPerson person;
    [MoonSharpHidden]
    private int _hashCode;

    [MoonSharpHidden]
    public ContainerPlayer(ZPerson p)
    {
      this.person = p;
      this._hashCode = p != null ? p.GetHashCode() : 0;
    }

    public int localTurn
    {
      get => this.person.localTurn;
      set => this.person.localTurn = Mathf.Clamp(value, 0, int.MaxValue);
    }

    public string name => this.person.name;

    public int team => (int) this.person.id;

    public bool yourTurn => this.person.yourTurn;

    public Table getCreatures(Script script)
    {
      Table creatures = new Table(script);
      int key = 1;
      foreach (ZCreature c in this.person.controlled)
      {
        creatures[(object) key] = (object) new ContainerCreature(c);
        ++key;
      }
      return creatures;
    }

    public ContainerCreature getCreature(int index)
    {
      return new ContainerCreature(this.person.controlled[index - 1]);
    }

    public int getMinionCount() => this.person.GetMinionCount();

    public override int GetHashCode() => this._hashCode;
  }
}
