// Decompiled with JetBrains decompiler
// Type: Educative.CAddToVarible
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CAddToVarible : Command
  {
    public TutInt varible = new TutInt();
    public MyHeader VariableModification = new MyHeader();
    public MyMaths algorithm;
    public TutInt add = new TutInt();

    public CAddToVarible() => this.type = Command.Type.VariableMaths;
  }
}
