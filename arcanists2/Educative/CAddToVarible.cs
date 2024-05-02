// Decompiled with JetBrains decompiler
// Type: Educative.CAddToVarible
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
