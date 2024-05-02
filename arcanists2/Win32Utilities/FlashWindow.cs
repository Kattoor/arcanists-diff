

#nullable disable
namespace Win32Utilities
{
  public static class FlashWindow
  {
    public static readonly IFlashWindow instance = (IFlashWindow) new FlashWindowWin32();
  }
}
