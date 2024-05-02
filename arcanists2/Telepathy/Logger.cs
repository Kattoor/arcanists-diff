

using System;

#nullable disable
namespace Telepathy
{
  public static class Logger
  {
    public static Action<string> Log = new Action<string>(Console.WriteLine);
    public static Action<string> LogWarning = new Action<string>(Console.WriteLine);
    public static Action<string> LogError = new Action<string>(Console.Error.WriteLine);
  }
}
