// Decompiled with JetBrains decompiler
// Type: Educative.CIfThen
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CIfThen : Command
  {
    public TutInt leftValue = new TutInt();
    public TutInt rightValue = new TutInt();
    public MyHeader Comparision = new MyHeader();
    public Comparison comparison;
    internal int commandID = -1;

    public CIfThen() => this.type = Command.Type.If;

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
