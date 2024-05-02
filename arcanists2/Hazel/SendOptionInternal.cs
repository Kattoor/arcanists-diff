

#nullable disable
namespace Hazel
{
  internal enum SendOptionInternal : byte
  {
    Hello = 8,
    Disconnect = 9,
    Acknowledgement = 10, // 0x0A
  }
}
