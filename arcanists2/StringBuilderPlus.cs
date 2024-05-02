// Decompiled with JetBrains decompiler
// Type: StringBuilderPlus
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

#nullable disable
public class StringBuilderPlus
{
  public StringBuilder _stringBuilder;
  public string _subtraction = ",";
  public string _multiplication = " ";
  public string _division = "\n";

  public static StringBuilderPlus operator +(StringBuilderPlus a, StringBuilderPlus b)
  {
    a._stringBuilder.Append(b.ToString());
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, string b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, int b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, float b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, Decimal b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, long b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, char b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, byte b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, short b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, ushort b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, sbyte b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, uint b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, ulong b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, object b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator +(StringBuilderPlus a, char[] b)
  {
    a._stringBuilder.Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, StringBuilderPlus b)
  {
    a._stringBuilder.Append(a._division).Append(b.ToString());
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, string b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, int b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, float b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, Decimal b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, long b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, char b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, byte b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, short b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, ushort b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, sbyte b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, uint b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, ulong b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, object b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator /(StringBuilderPlus a, char[] b)
  {
    a._stringBuilder.Append(a._division).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, StringBuilderPlus b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b.ToString());
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, string b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, int b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, float b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, Decimal b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, long b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, char b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, byte b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, short b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, ushort b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, sbyte b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, uint b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, ulong b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, object b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator *(StringBuilderPlus a, char[] b)
  {
    a._stringBuilder.Append(a._multiplication).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, StringBuilderPlus b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b.ToString());
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, string b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, int b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, float b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, Decimal b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, long b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, char b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, byte b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, short b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, ushort b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, sbyte b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, uint b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, ulong b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, object b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public static StringBuilderPlus operator -(StringBuilderPlus a, char[] b)
  {
    a._stringBuilder.Append(a._subtraction).Append(b);
    return a;
  }

  public StringBuilderPlus Append(bool value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(ulong value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(uint value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(byte value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(string value, int startIndex, int count, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value, startIndex, count);
    return this;
  }

  public StringBuilderPlus Append(string value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(float value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(ushort value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(object value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(char[] value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(char[] value, int startIndex, int charCount, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value, startIndex, charCount);
    return this;
  }

  public StringBuilderPlus Append(sbyte value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(Decimal value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public unsafe StringBuilderPlus Append(char* value, int valueCount, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value, valueCount);
    return this;
  }

  public StringBuilderPlus Append(short value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(int value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(long value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(double value, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(bool value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(char value, string separator, int repeatCount)
  {
    this._stringBuilder.Append(separator).Append(value, repeatCount);
    return this;
  }

  public StringBuilderPlus Append(ulong value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(uint value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(byte value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(
    string value,
    int startIndex,
    int count,
    string separator,
    int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value, startIndex, count);
    return this;
  }

  public StringBuilderPlus Append(string value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(float value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(ushort value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(object value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(char[] value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(
    char[] value,
    int startIndex,
    int charCount,
    string separator,
    int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value, startIndex, charCount);
    return this;
  }

  public StringBuilderPlus Append(sbyte value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(Decimal value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public unsafe StringBuilderPlus Append(
    char* value,
    int valueCount,
    string separator,
    int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value, valueCount);
    return this;
  }

  public StringBuilderPlus Append(short value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(int value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(long value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus Append(double value, string separator, int repeatCount)
  {
    for (int index = 0; index < repeatCount; ++index)
      this._stringBuilder.Append(separator).Append(value);
    return this;
  }

  public StringBuilderPlus() => this._stringBuilder = new StringBuilder();

  public StringBuilderPlus(int capacity) => this._stringBuilder = new StringBuilder(capacity);

  public StringBuilderPlus(string value) => this._stringBuilder = new StringBuilder(value);

  public StringBuilderPlus(int capacity, int maxCapacity)
  {
    this._stringBuilder = new StringBuilder(capacity, maxCapacity);
  }

  public StringBuilderPlus(string value, int capacity)
  {
    this._stringBuilder = new StringBuilder(value, capacity);
  }

  public StringBuilderPlus(string value, int startIndex, int length, int capacity)
  {
    this._stringBuilder = new StringBuilder(value, startIndex, length, capacity);
  }

  public char this[int index]
  {
    get => this._stringBuilder[index];
    set => this._stringBuilder[index] = value;
  }

  public int Capacity
  {
    get => this._stringBuilder.Capacity;
    set => this._stringBuilder.Capacity = value;
  }

  public int Length
  {
    get => this._stringBuilder.Length;
    set => this._stringBuilder.Length = value;
  }

  public int MaxCapacity => this._stringBuilder.MaxCapacity;

  public StringBuilderPlus Append(char value, int repeatCount)
  {
    this._stringBuilder.Append(value, repeatCount);
    return this;
  }

  public StringBuilderPlus Append(bool value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(char value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(ulong value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(uint value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(byte value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(string value, int startIndex, int count)
  {
    this._stringBuilder.Append(value, startIndex, count);
    return this;
  }

  public StringBuilderPlus Append(string value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(float value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(ushort value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(object value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(char[] value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(char[] value, int startIndex, int charCount)
  {
    this._stringBuilder.Append(value, startIndex, charCount);
    return this;
  }

  public StringBuilderPlus Append(sbyte value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(Decimal value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public unsafe StringBuilderPlus Append(char* value, int valueCount)
  {
    this._stringBuilder.Append(value, valueCount);
    return this;
  }

  public StringBuilderPlus Append(short value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(int value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(long value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus Append(double value)
  {
    this._stringBuilder.Append(value);
    return this;
  }

  public StringBuilderPlus AppendFormat(IFormatProvider provider, string format, object arg0)
  {
    this._stringBuilder.AppendFormat(provider, format, arg0);
    return this;
  }

  public StringBuilderPlus AppendFormat(
    IFormatProvider provider,
    string format,
    object arg0,
    object arg1)
  {
    this._stringBuilder.AppendFormat(provider, format, arg0, arg1);
    return this;
  }

  public StringBuilderPlus AppendFormat(
    IFormatProvider provider,
    string format,
    params object[] args)
  {
    this._stringBuilder.AppendFormat(provider, format, args);
    return this;
  }

  public StringBuilderPlus AppendFormat(string format, object arg0)
  {
    this._stringBuilder.AppendFormat(format, arg0);
    return this;
  }

  public StringBuilderPlus AppendFormat(string format, object arg0, object arg1)
  {
    this._stringBuilder.AppendFormat(format, arg0, arg1);
    return this;
  }

  public StringBuilderPlus AppendFormat(string format, object arg0, object arg1, object arg2)
  {
    this._stringBuilder.AppendFormat(format, arg0, arg1, arg2);
    return this;
  }

  public StringBuilderPlus AppendFormat(string format, params object[] args)
  {
    this._stringBuilder.AppendFormat(format, args);
    return this;
  }

  public StringBuilderPlus AppendFormat(
    IFormatProvider provider,
    string format,
    object arg0,
    object arg1,
    object arg2)
  {
    this._stringBuilder.AppendFormat(provider, format, arg0, arg1, arg2);
    return this;
  }

  public StringBuilderPlus AppendLine()
  {
    this._stringBuilder.AppendLine();
    return this;
  }

  public StringBuilderPlus AppendLine(string value)
  {
    this._stringBuilder.AppendLine(value);
    return this;
  }

  public StringBuilderPlus Clear()
  {
    this._stringBuilder.Clear();
    return this;
  }

  public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
  {
    this._stringBuilder.CopyTo(sourceIndex, destination, destinationIndex, count);
  }

  public int EnsureCapacity(int capacity) => this._stringBuilder.EnsureCapacity(capacity);

  public bool Equals(StringBuilderPlus sb) => this._stringBuilder.Equals(sb._stringBuilder);

  public StringBuilderPlus Insert(int index, char[] value, int startIndex, int charCount)
  {
    this._stringBuilder.Insert(index, value, startIndex, charCount);
    return this;
  }

  public StringBuilderPlus Insert(int index, bool value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, byte value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, ulong value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, char[] value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, ushort value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, string value, int count)
  {
    this._stringBuilder.Insert(index, value, count);
    return this;
  }

  public StringBuilderPlus Insert(int index, char value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, uint value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, sbyte value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, object value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, long value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, int value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, short value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, double value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, Decimal value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, float value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Insert(int index, string value)
  {
    this._stringBuilder.Insert(index, value);
    return this;
  }

  public StringBuilderPlus Remove(int startIndex, int length)
  {
    this._stringBuilder.Remove(startIndex, length);
    return this;
  }

  public StringBuilderPlus Replace(char oldChar, char newChar)
  {
    this._stringBuilder.Replace(oldChar, newChar);
    return this;
  }

  public StringBuilderPlus Replace(char oldChar, char newChar, int startIndex, int count)
  {
    this._stringBuilder.Replace(oldChar, newChar, startIndex, count);
    return this;
  }

  public StringBuilderPlus Replace(string oldValue, string newValue)
  {
    this._stringBuilder.Replace(oldValue, newValue);
    return this;
  }

  public StringBuilderPlus Replace(string oldValue, string newValue, int startIndex, int count)
  {
    this._stringBuilder.Replace(oldValue, newValue, startIndex, count);
    return this;
  }

  public override string ToString() => this._stringBuilder.ToString();

  public string ToString(int startIndex, int length)
  {
    return this._stringBuilder.ToString(startIndex, length);
  }
}
