// Decompiled with JetBrains decompiler
// Type: NewtonsoftExtensions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
public static class NewtonsoftExtensions
{
  private static string currentHeader;
  private static string lastHeader;
  private static List<KeyValuePair<string, JToken>> stack;

  public static string Compare(JObject a, JObject b)
  {
    NewtonsoftExtensions.currentHeader = (string) null;
    NewtonsoftExtensions.lastHeader = (string) null;
    return NewtonsoftExtensions.CompareObjects(a, b, true).ToString();
  }

  public static string GetText(this JToken j)
  {
    string text = "";
    foreach (object obj in (IEnumerable<JToken>) j.Values())
      text = obj.ToString();
    return text;
  }

  internal static StringBuilder CompareObjects(JObject source, JObject target, bool topLayer = false)
  {
    StringBuilder b = new StringBuilder();
    if (topLayer)
    {
      NewtonsoftExtensions.lastHeader = (string) null;
      NewtonsoftExtensions.currentHeader = (string) null;
      NewtonsoftExtensions.stack = new List<KeyValuePair<string, JToken>>();
      b.Append("\n\n");
    }
    try
    {
      string str = source["Name"].ToString();
      if (!string.IsNullOrWhiteSpace(str))
        NewtonsoftExtensions.currentHeader = str;
    }
    catch (Exception ex)
    {
    }
    foreach (KeyValuePair<string, JToken> keyValuePair1 in source)
    {
      if (topLayer)
        NewtonsoftExtensions.currentHeader = keyValuePair1.Key.ToString();
      if (keyValuePair1.Value.Type == JTokenType.Object)
      {
        if (target.GetValue(keyValuePair1.Key) == null)
        {
          NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
          b.Append(keyValuePair1.Key + " not found\n");
        }
        else if (target.GetValue(keyValuePair1.Key).Type != JTokenType.Object)
        {
          NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
          b.Append(keyValuePair1.Key + " is not an object in target\n");
        }
        else
        {
          NewtonsoftExtensions.stack.Add(keyValuePair1);
          b.Append((object) NewtonsoftExtensions.CompareObjects(keyValuePair1.Value.ToObject<JObject>(), target.GetValue(keyValuePair1.Key).ToObject<JObject>()));
          NewtonsoftExtensions.stack.RemoveAt(NewtonsoftExtensions.stack.Count - 1);
        }
      }
      else if (keyValuePair1.Value.Type == JTokenType.Array)
      {
        if (target.GetValue(keyValuePair1.Key) == null)
        {
          NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
          b.Append(keyValuePair1.Key + " not found\n");
        }
        else
        {
          NewtonsoftExtensions.stack.Add(keyValuePair1);
          b.Append((object) NewtonsoftExtensions.CompareArrays(keyValuePair1.Value.ToObject<JArray>(), target.GetValue(keyValuePair1.Key).ToObject<JArray>(), keyValuePair1.Key));
          NewtonsoftExtensions.stack.RemoveAt(NewtonsoftExtensions.stack.Count - 1);
        }
      }
      else
      {
        JToken t1 = keyValuePair1.Value;
        JToken t2 = target.SelectToken(keyValuePair1.Key);
        if (t2 == null)
        {
          NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
          b.Append(keyValuePair1.Key + " not found\n");
        }
        else if (!JToken.DeepEquals(t1, t2))
        {
          NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
          string s = target.Property(keyValuePair1.Key).Value.ToString();
          if (string.Equals(keyValuePair1.Key, "x") || string.Equals(keyValuePair1.Key, "RawValue") || string.Equals(keyValuePair1.Key, "y"))
          {
            long result;
            long.TryParse(s, out result);
            float num = ((FixedInt) result).ToFloat();
            string str1 = num.ToString();
            long.TryParse(keyValuePair1.Value.ToString(), out result);
            StringBuilder stringBuilder = b;
            string[] strArray = new string[7];
            KeyValuePair<string, JToken> keyValuePair2 = NewtonsoftExtensions.stack[NewtonsoftExtensions.stack.Count - 1];
            strArray[0] = keyValuePair2.Key;
            keyValuePair2 = NewtonsoftExtensions.stack[NewtonsoftExtensions.stack.Count - 1];
            strArray[1] = keyValuePair2.Key.Contains("Data") ? " (" + keyValuePair1.Key + ")" : "";
            strArray[2] = ": ";
            num = ((FixedInt) result).ToFloat();
            strArray[3] = num.ToString();
            strArray[4] = " <<  ";
            strArray[5] = str1.Length > 20 ? "" : str1;
            strArray[6] = "\n";
            string str2 = string.Concat(strArray);
            stringBuilder.Append(str2);
          }
          else
            b.Append(keyValuePair1.Key + ": " + (object) keyValuePair1.Value + " <<  " + (s.Length > 20 ? (object) "" : (object) s) + "\n");
        }
      }
    }
    if (topLayer && b.Length < 5)
      b.Append("Identical");
    if (topLayer)
    {
      NewtonsoftExtensions.stack.Clear();
      b.Append("\n\n");
    }
    return b;
  }

  private static void AddHeader(StringBuilder b, string header)
  {
    if (string.IsNullOrEmpty(header) || !(header != NewtonsoftExtensions.lastHeader))
      return;
    NewtonsoftExtensions.lastHeader = header;
    b.Append("<color=#FF0000>").Append(header).Append("</color>\n");
  }

  private static StringBuilder CompareArrays(JArray source, JArray target, string arrayName = "")
  {
    StringBuilder b = new StringBuilder();
    for (int index = 0; index < source.Count && index < target.Count; ++index)
    {
      JToken t1 = source[index];
      if (t1.Type == JTokenType.Object)
      {
        string name = t1.Value<JToken>((object) "Name")?.ToString();
        JToken jtoken = !string.IsNullOrWhiteSpace(name) ? target.Children().First<JToken>((Func<JToken, bool>) (z => string.Equals(z.Value<JToken>((object) "Name")?.ToString(), name))) : (index >= target.Count ? (JToken) new JObject() : target[index]);
        if (jtoken != null)
          b.Append((object) NewtonsoftExtensions.CompareObjects(t1.ToObject<JObject>(), jtoken.ToObject<JObject>()));
      }
      else
      {
        string name = t1.Value<JToken>((object) "Name")?.ToString();
        JToken t2 = !string.IsNullOrWhiteSpace(name) ? target.Children().First<JToken>((Func<JToken, bool>) (z => string.Equals(z.Value<JToken>((object) "Name")?.ToString(), name))) : (index >= target.Count ? (JToken) new JObject() : target[index]);
        if (t2 != null && !JToken.DeepEquals(t1, t2))
        {
          if (string.IsNullOrEmpty(arrayName))
          {
            NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
            b.Append("Index " + (object) index + ": " + (object) t1 + " != " + (object) t2 + "\n");
          }
          else
          {
            NewtonsoftExtensions.AddHeader(b, NewtonsoftExtensions.currentHeader);
            b.Append(arrayName + "[" + (object) index + "]: " + (object) t1 + " != " + (object) t2 + "\n");
          }
        }
      }
    }
    return b;
  }
}
