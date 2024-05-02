// Decompiled with JetBrains decompiler
// Type: Educative.ContainerEffector
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;
using System;

#nullable disable
namespace Educative
{
  public class ContainerEffector
  {
    [MoonSharpHidden]
    public ZEffector effector;
    private int _hashCode;

    [MoonSharpHidden]
    public ContainerEffector(ZEffector s)
    {
      this.effector = s;
      this._hashCode = (ZComponent) s != (object) null ? s.GetHashCode() : 0;
    }

    public bool isDead => (ZComponent) this.effector == (object) null || this.effector.dead;

    public int x
    {
      get => this.effector.position.x.ToInt();
      set => this.effector.position = new MyLocation((FixedInt) value, this.effector.position.y);
    }

    public int y
    {
      get => this.effector.position.y.ToInt();
      set => this.effector.position = new MyLocation(this.effector.position.x, (FixedInt) value);
    }

    public Point position
    {
      get
      {
        MyLocation position = this.effector.position;
        double x = (double) position.x.ToInt();
        position = this.effector.position;
        double y = (double) position.y.ToInt();
        return new Point(x, y);
      }
      set
      {
        this.effector.position = new MyLocation((FixedInt) (float) value.x, (FixedInt) (float) value.y);
      }
    }

    public int turnsAlive
    {
      get => this.effector.GetTurnsAlive();
      set => this.effector.SetTurnsAlive(value);
    }

    public int maxTurnsAlive
    {
      get => this.effector.MaxTurnsAlive;
      set => this.effector.MaxTurnsAlive = value;
    }

    public bool active
    {
      get => this.effector.active;
      set
      {
        this.effector.active = value;
        this.effector.VisualUpdate();
      }
    }

    public EffectorType type => this.effector.type;

    public void destroy()
    {
      int indexInParent = this.effector.whoSummoned.effectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.effector));
      int index1 = this.effector.whoSummoned.destroyableEffectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.effector));
      int index2 = this.effector.game.globalEffectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.effector));
      if (index1 >= 0)
        indexInParent = index1;
      else if (index2 >= 0)
        indexInParent = index2;
      this.effector.Die(indexInParent, index1 >= 0, index2 >= 0);
      this.effector = (ZEffector) null;
    }

    public void turnPassed()
    {
      int indexInParent = this.effector.whoSummoned.effectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.effector));
      int index1 = this.effector.whoSummoned.destroyableEffectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.effector));
      int index2 = this.effector.game.globalEffectors.FindIndex((Predicate<ZEffector>) (z => (ZComponent) z == (object) this.effector));
      if (index1 >= 0)
        indexInParent = index1;
      else if (index2 >= 0)
        indexInParent = index2;
      this.effector.TurnPassed(indexInParent, index1 >= 0, index2 >= 0);
    }

    public override int GetHashCode() => this._hashCode;
  }
}
