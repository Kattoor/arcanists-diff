

#nullable disable
namespace Win32Utilities
{
  internal class FlashWindowNoOp : IFlashWindow
  {
    public bool Flash() => false;

    public bool Flash(uint count) => false;

    public bool Start() => false;

    public bool Stop() => false;
  }
}
