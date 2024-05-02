

using System;

#nullable disable
namespace SevenZip
{
  internal class DataErrorException : ApplicationException
  {
    public DataErrorException()
      : base("Data Error")
    {
    }
  }
}
