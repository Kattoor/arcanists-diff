// Decompiled with JetBrains decompiler
// Type: Educative.CIf
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CIf : Command
  {
    public TutInt leftValue = new TutInt();
    public TutInt rightValue = new TutInt();
    public MyHeader Comparision = new MyHeader();
    public Comparison comparison;
    public int commandID = -1;

    public CIf() => this.type = Command.Type.IfGoTo;

    public bool Evaulate()
    {
      switch (this.comparison)
      {
        case Comparison.Smaller:
          return this.leftValue.SmallerThen(this.rightValue);
        case Comparison.SmallerOrEqual:
          return this.leftValue.SmallerThenOrEqual(this.rightValue);
        case Comparison.Equal:
          return this.leftValue.EqualTo(this.rightValue);
        case Comparison.BiggerOrEqual:
          return this.leftValue.BiggerThenOrEqual(this.rightValue);
        case Comparison.Bigger:
          return this.leftValue.BiggerThen(this.rightValue);
        case Comparison.NotEqual:
          return this.leftValue.NotEqual(this.rightValue);
        default:
          return false;
      }
    }
  }
}
