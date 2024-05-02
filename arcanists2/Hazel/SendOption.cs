

using System;

#nullable disable
namespace Hazel
{
  [Flags]
  public enum SendOption : byte
  {
    None = 0,
    Reliable = 1,
    Fragmented = 2,
    FragmentedReliable = Fragmented | Reliable, // 0x03
  }
}
