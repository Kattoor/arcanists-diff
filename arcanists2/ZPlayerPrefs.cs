

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

#nullable disable
public static class ZPlayerPrefs
{
  public static bool useSecure = true;
  private const int Iterations = 555;
  private static string strPassword = "IamZETOwow!123";
  private static string strSalt = "IvmD123A12";
  private static bool hasSetPassword = false;

  public static void DeleteAll() => PlayerPrefs.DeleteAll();

  public static void DeleteKey(string key) => PlayerPrefs.DeleteKey(key);

  public static float GetFloat(string key) => ZPlayerPrefs.GetFloat(key, 0.0f);

  public static float GetFloat(string key, float defaultValue, bool isDecrypt = true)
  {
    float result = defaultValue;
    return float.TryParse(ZPlayerPrefs.GetString(key), out result) ? result : defaultValue;
  }

  public static int GetInt(string key) => ZPlayerPrefs.GetInt(key, 0);

  public static int GetInt(string key, int defaultValue, bool isDecrypt = true)
  {
    int result = defaultValue;
    return int.TryParse(ZPlayerPrefs.GetString(key), out result) ? result : defaultValue;
  }

  public static string GetString(string key)
  {
    return ZPlayerPrefs.Decrypt(ZPlayerPrefs.GetRowString(key), ZPlayerPrefs.strPassword);
  }

  public static string GetRowString(string key)
  {
    ZPlayerPrefs.CheckPasswordSet();
    return PlayerPrefs.GetString(ZPlayerPrefs.Encrypt(key, ZPlayerPrefs.strPassword));
  }

  public static string GetString(string key, string defaultValue)
  {
    return ZPlayerPrefs.Decrypt(ZPlayerPrefs.GetRowString(key, defaultValue), ZPlayerPrefs.strPassword);
  }

  public static string GetRowString(string key, string defaultValue)
  {
    ZPlayerPrefs.CheckPasswordSet();
    return PlayerPrefs.GetString(ZPlayerPrefs.Encrypt(key, ZPlayerPrefs.strPassword), ZPlayerPrefs.Encrypt(defaultValue, ZPlayerPrefs.strPassword));
  }

  public static bool HasKey(string key)
  {
    ZPlayerPrefs.CheckPasswordSet();
    return PlayerPrefs.HasKey(ZPlayerPrefs.Encrypt(key, ZPlayerPrefs.strPassword));
  }

  public static void Save()
  {
    ZPlayerPrefs.CheckPasswordSet();
    PlayerPrefs.Save();
  }

  public static void SetFloat(string key, float value)
  {
    string str = Convert.ToString(value);
    ZPlayerPrefs.SetString(key, str);
  }

  public static void SetInt(string key, int value)
  {
    string str = Convert.ToString(value);
    ZPlayerPrefs.SetString(key, str);
  }

  public static void SetString(string key, string value)
  {
    ZPlayerPrefs.CheckPasswordSet();
    PlayerPrefs.SetString(ZPlayerPrefs.Encrypt(key, ZPlayerPrefs.strPassword), ZPlayerPrefs.Encrypt(value, ZPlayerPrefs.strPassword));
  }

  public static void Initialize(string newPassword, string newSalt)
  {
    ZPlayerPrefs.strPassword = newPassword;
    ZPlayerPrefs.strSalt = newSalt;
    ZPlayerPrefs.hasSetPassword = true;
  }

  private static void CheckPasswordSet()
  {
    if (ZPlayerPrefs.hasSetPassword)
      return;
    Debug.LogWarning((object) "Set Your Own Password & Salt!!!");
  }

  private static byte[] GetIV() => Encoding.UTF8.GetBytes(ZPlayerPrefs.strSalt);

  private static string Encrypt(string strPlain, string password)
  {
    if (!ZPlayerPrefs.useSecure)
      return strPlain;
    try
    {
      DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
      byte[] bytes1 = new Rfc2898DeriveBytes(password, ZPlayerPrefs.GetIV(), 555).GetBytes(8);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, ZPlayerPrefs.GetIV()), CryptoStreamMode.Write))
        {
          memoryStream.Write(ZPlayerPrefs.GetIV(), 0, ZPlayerPrefs.GetIV().Length);
          byte[] bytes2 = Encoding.UTF8.GetBytes(strPlain);
          cryptoStream.Write(bytes2, 0, bytes2.Length);
          cryptoStream.FlushFinalBlock();
          return Convert.ToBase64String(memoryStream.ToArray());
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogWarning((object) ("Encrypt Exception: " + (object) ex));
      return strPlain;
    }
  }

  private static string Decrypt(string strEncript, string password)
  {
    if (!ZPlayerPrefs.useSecure)
      return strEncript;
    try
    {
      using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(strEncript)))
      {
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        byte[] iv = ZPlayerPrefs.GetIV();
        memoryStream.Read(iv, 0, iv.Length);
        byte[] bytes = new Rfc2898DeriveBytes(password, iv, 555).GetBytes(8);
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateDecryptor(bytes, iv), CryptoStreamMode.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) cryptoStream))
            return streamReader.ReadToEnd();
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogWarning((object) ("Decrypt Exception: " + (object) ex));
      return strEncript;
    }
  }
}
