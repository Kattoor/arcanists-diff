// Decompiled with JetBrains decompiler
// Type: Helpers
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

#nullable disable
public static class Helpers
{
  public static X509Certificate2 GetCertificateFromPEMstring(
    string publicCert,
    string privateKey,
    string password)
  {
    byte[] bytesFromPem1 = Helpers.GetBytesFromPEM(publicCert, Helpers.PemStringType.Certificate);
    byte[] bytesFromPem2 = Helpers.GetBytesFromPEM(privateKey, Helpers.PemStringType.RsaPrivateKey);
    return new X509Certificate2(bytesFromPem1)
    {
      PrivateKey = (AsymmetricAlgorithm) Helpers.DecodeRsaPrivateKey(bytesFromPem2)
    };
  }

  public static byte[] GetBytesFromPEM(string pemString, Helpers.PemStringType type)
  {
    string str1;
    string str2;
    switch (type)
    {
      case Helpers.PemStringType.Certificate:
        str1 = "-----BEGIN CERTIFICATE-----";
        str2 = "-----END CERTIFICATE-----";
        break;
      case Helpers.PemStringType.RsaPrivateKey:
        str1 = "-----BEGIN RSA PRIVATE KEY-----";
        str2 = "-----END RSA PRIVATE KEY-----";
        break;
      default:
        return (byte[]) null;
    }
    int startIndex = pemString.IndexOf(str1) + str1.Length;
    int length = pemString.IndexOf(str2, startIndex) - startIndex;
    return Convert.FromBase64String(pemString.Substring(startIndex, length));
  }

  public static RSACryptoServiceProvider DecodeRsaPrivateKey(byte[] privateKeyBytes)
  {
    BinaryReader rd = new BinaryReader((Stream) new MemoryStream(privateKeyBytes));
    try
    {
      switch (rd.ReadUInt16())
      {
        case 33072:
          int num1 = (int) rd.ReadByte();
          break;
        case 33328:
          int num2 = (int) rd.ReadInt16();
          break;
        default:
          return (RSACryptoServiceProvider) null;
      }
      if (rd.ReadUInt16() != (ushort) 258 || rd.ReadByte() != (byte) 0)
        return (RSACryptoServiceProvider) null;
      RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters()
      {
        Flags = CspProviderFlags.NoFlags,
        KeyContainerName = Guid.NewGuid().ToString().ToUpperInvariant(),
        ProviderType = Environment.OSVersion.Version.Major > 5 || Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1 ? 24 : 1
      });
      RSAParameters parameters = new RSAParameters();
      parameters.Modulus = rd.ReadBytes(Helpers.DecodeIntegerSize(rd));
      Helpers.RSAParameterTraits rsaParameterTraits = new Helpers.RSAParameterTraits(parameters.Modulus.Length * 8);
      parameters.Modulus = Helpers.AlignBytes(parameters.Modulus, rsaParameterTraits.size_Mod);
      parameters.Exponent = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_Exp);
      parameters.D = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_D);
      parameters.P = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_P);
      parameters.Q = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_Q);
      parameters.DP = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_DP);
      parameters.DQ = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_DQ);
      parameters.InverseQ = Helpers.AlignBytes(rd.ReadBytes(Helpers.DecodeIntegerSize(rd)), rsaParameterTraits.size_InvQ);
      cryptoServiceProvider.ImportParameters(parameters);
      return cryptoServiceProvider;
    }
    catch (Exception ex)
    {
      return (RSACryptoServiceProvider) null;
    }
    finally
    {
      rd.Close();
    }
  }

  public static int DecodeIntegerSize(BinaryReader rd)
  {
    if (rd.ReadByte() != (byte) 2)
      return 0;
    byte num1 = rd.ReadByte();
    int num2;
    switch (num1)
    {
      case 129:
        num2 = (int) rd.ReadByte();
        break;
      case 130:
        byte num3 = rd.ReadByte();
        num2 = (int) BitConverter.ToUInt16(new byte[2]
        {
          rd.ReadByte(),
          num3
        }, 0);
        break;
      default:
        num2 = (int) num1;
        break;
    }
    while (rd.ReadByte() == (byte) 0)
      --num2;
    rd.BaseStream.Seek(-1L, SeekOrigin.Current);
    return num2;
  }

  public static byte[] AlignBytes(byte[] inputBytes, int alignSize)
  {
    int length = inputBytes.Length;
    if (alignSize == -1 || length >= alignSize)
      return inputBytes;
    byte[] numArray = new byte[alignSize];
    for (int index = 0; index < length; ++index)
      numArray[index + (alignSize - length)] = inputBytes[index];
    return numArray;
  }

  public enum PemStringType
  {
    Certificate,
    RsaPrivateKey,
  }

  internal class RSAParameterTraits
  {
    public int size_Mod = -1;
    public int size_Exp = -1;
    public int size_D = -1;
    public int size_P = -1;
    public int size_Q = -1;
    public int size_DP = -1;
    public int size_DQ = -1;
    public int size_InvQ = -1;

    public RSAParameterTraits(int modulusLengthInBits)
    {
      double num = Math.Log((double) modulusLengthInBits, 2.0);
      switch (num != (double) (int) num ? (int) Math.Pow(2.0, (double) (int) (num + 1.0)) : modulusLengthInBits)
      {
        case 1024:
          this.size_Mod = 128;
          this.size_Exp = -1;
          this.size_D = 128;
          this.size_P = 64;
          this.size_Q = 64;
          this.size_DP = 64;
          this.size_DQ = 64;
          this.size_InvQ = 64;
          break;
        case 2048:
          this.size_Mod = 256;
          this.size_Exp = -1;
          this.size_D = 256;
          this.size_P = 128;
          this.size_Q = 128;
          this.size_DP = 128;
          this.size_DQ = 128;
          this.size_InvQ = 128;
          break;
        case 4096:
          this.size_Mod = 512;
          this.size_Exp = -1;
          this.size_D = 512;
          this.size_P = 256;
          this.size_Q = 256;
          this.size_DP = 256;
          this.size_DQ = 256;
          this.size_InvQ = 256;
          break;
      }
    }
  }
}
