// Decompiled with JetBrains decompiler
// Type: NumberAwareStringComparer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
public class NumberAwareStringComparer : IComparer<string>
{
  private static readonly Regex numberRegex = new Regex("\\d+");

  public int Compare(string x, string y) => NumberAwareStringComparer._Compare(x, y);

  public static int _Compare(string x, string y)
  {
    (int, string) numericPart1 = NumberAwareStringComparer.GetNumericPart(x);
    (int, string) numericPart2 = NumberAwareStringComparer.GetNumericPart(y);
    if (string.IsNullOrEmpty(numericPart1.Item2) || string.IsNullOrEmpty(numericPart2.Item2) || numericPart1.Item1 != numericPart2.Item1)
      return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
    int num = int.Parse(numericPart1.Item2).CompareTo(int.Parse(numericPart2.Item2));
    return num == 0 ? string.Compare(x, y, StringComparison.OrdinalIgnoreCase) : num;
  }

  private static (int, string) GetNumericPart(string input)
  {
    Match match = NumberAwareStringComparer.numberRegex.Match(input);
    return match.Success ? (match.Index, match.Value) : (0, (string) null);
  }
}
