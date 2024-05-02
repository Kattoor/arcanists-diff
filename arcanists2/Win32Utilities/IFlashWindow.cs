

#nullable disable
namespace Win32Utilities
{
  public interface IFlashWindow
  {
    bool Flash();

    bool Flash(uint count);

    bool Start();

    bool Stop();
  }
}
