

using System.IO;

#nullable disable
namespace Ninja.WebSockets
{
  public interface IBufferPool
  {
    MemoryStream GetBuffer();
  }
}
