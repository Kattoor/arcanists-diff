

#nullable disable
namespace SevenZip
{
  public interface ICodeProgress
  {
    void SetProgress(long inSize, long outSize);
  }
}
