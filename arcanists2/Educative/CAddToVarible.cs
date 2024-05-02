

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
