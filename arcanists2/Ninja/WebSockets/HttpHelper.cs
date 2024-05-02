

using Ninja.WebSockets.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  public class HttpHelper
  {
    private const string HTTP_GET_HEADER_REGEX = "^GET(.*)HTTP\\/1\\.1";

    public static string CalculateWebSocketKey()
    {
      Random random = new Random((int) DateTime.Now.Ticks);
      byte[] inArray = new byte[16];
      byte[] buffer = inArray;
      random.NextBytes(buffer);
      return Convert.ToBase64String(inArray);
    }

    public static string ComputeSocketAcceptString(string secWebSocketKey)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(secWebSocketKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11");
      return Convert.ToBase64String(SHA1.Create().ComputeHash(bytes));
    }

    public static async Task<string> ReadHttpHeaderAsync(Stream stream, CancellationToken token)
    {
      int length = 16384;
      byte[] buffer = new byte[length];
      int offset = 0;
      while (offset < length)
      {
        int num = await stream.ReadAsync(buffer, offset, length - offset, token);
        offset += num;
        string str = Encoding.UTF8.GetString(buffer, 0, offset);
        if (str.Contains("\r\n\r\n"))
          return str;
        if (num <= 0)
          return string.Empty;
      }
      throw new EntityTooLargeException("Http header message too large to fit in buffer (16KB)");
    }

    public static bool IsWebSocketUpgradeRequest(string header)
    {
      return new Regex("^GET(.*)HTTP\\/1\\.1", RegexOptions.IgnoreCase).Match(header).Success && new Regex("Upgrade: websocket", RegexOptions.IgnoreCase).Match(header).Success;
    }

    public static string GetPathFromHeader(string httpHeader)
    {
      Match match = new Regex("^GET(.*)HTTP\\/1\\.1", RegexOptions.IgnoreCase).Match(httpHeader);
      return match.Success ? match.Groups[1].Value.Trim() : (string) null;
    }

    public static IList<string> GetSubProtocols(string httpHeader)
    {
      Match match = new Regex("Sec-WebSocket-Protocol:(?<protocols>.+)", RegexOptions.IgnoreCase).Match(httpHeader);
      if (!match.Success)
        return (IList<string>) new List<string>();
      if (match.Length > 2048)
        throw new EntityTooLargeException(string.Format("Sec-WebSocket-Protocol exceeded the maximum of length of {0}", (object) 2048));
      return (IList<string>) ((IEnumerable<string>) match.Groups["protocols"].Value.Trim().Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (x => x.Trim())).ToList<string>();
    }

    public static string ReadHttpResponseCode(string response)
    {
      Match match = new Regex("HTTP\\/1\\.1 (.*)", RegexOptions.IgnoreCase).Match(response);
      return match.Success ? match.Groups[1].Value.Trim() : (string) null;
    }

    public static async Task WriteHttpHeaderAsync(
      string response,
      Stream stream,
      CancellationToken token)
    {
      response = response.Trim() + "\r\n\r\n";
      byte[] bytes = Encoding.UTF8.GetBytes(response);
      await stream.WriteAsync(bytes, 0, bytes.Length, token);
    }
  }
}
