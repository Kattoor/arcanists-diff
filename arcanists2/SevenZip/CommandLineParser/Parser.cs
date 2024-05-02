// Decompiled with JetBrains decompiler
// Type: SevenZip.CommandLineParser.Parser
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;

#nullable disable
namespace SevenZip.CommandLineParser
{
  public class Parser
  {
    public ArrayList NonSwitchStrings = new ArrayList();
    private SwitchResult[] _switches;
    private const char kSwitchID1 = '-';
    private const char kSwitchID2 = '/';
    private const char kSwitchMinus = '-';
    private const string kStopSwitchParsing = "--";

    public Parser(int numSwitches)
    {
      this._switches = new SwitchResult[numSwitches];
      for (int index = 0; index < numSwitches; ++index)
        this._switches[index] = new SwitchResult();
    }

    private bool ParseString(string srcString, SwitchForm[] switchForms)
    {
      int length1 = srcString.Length;
      if (length1 == 0)
        return false;
      int num1 = 0;
      if (!Parser.IsItSwitchChar(srcString[num1]))
        return false;
      while (num1 < length1)
      {
        if (Parser.IsItSwitchChar(srcString[num1]))
          ++num1;
        int index1 = 0;
        int num2 = -1;
        for (int index2 = 0; index2 < this._switches.Length; ++index2)
        {
          int length2 = switchForms[index2].IDString.Length;
          if (length2 > num2 && num1 + length2 <= length1 && string.Compare(switchForms[index2].IDString, 0, srcString, num1, length2, true) == 0)
          {
            index1 = index2;
            num2 = length2;
          }
        }
        if (num2 == -1)
          throw new Exception("maxLen == kNoLen");
        SwitchResult switchResult = this._switches[index1];
        SwitchForm switchForm = switchForms[index1];
        if (!switchForm.Multi && switchResult.ThereIs)
          throw new Exception("switch must be single");
        switchResult.ThereIs = true;
        num1 += num2;
        int num3 = length1 - num1;
        SwitchType type = switchForm.Type;
        switch (type)
        {
          case SwitchType.PostMinus:
            if (num3 == 0)
            {
              switchResult.WithMinus = false;
              continue;
            }
            switchResult.WithMinus = srcString[num1] == '-';
            if (switchResult.WithMinus)
            {
              ++num1;
              continue;
            }
            continue;
          case SwitchType.LimitedPostString:
          case SwitchType.UnLimitedPostString:
            int minLen = switchForm.MinLen;
            if (num3 < minLen)
              throw new Exception("switch is not full");
            if (type == SwitchType.UnLimitedPostString)
            {
              switchResult.PostStrings.Add((object) srcString.Substring(num1));
              return true;
            }
            string str = srcString.Substring(num1, minLen);
            num1 += minLen;
            for (int index3 = minLen; index3 < switchForm.MaxLen && num1 < length1; ++num1)
            {
              char c = srcString[num1];
              if (!Parser.IsItSwitchChar(c))
              {
                str += c.ToString();
                ++index3;
              }
              else
                break;
            }
            switchResult.PostStrings.Add((object) str);
            continue;
          case SwitchType.PostChar:
            if (num3 < switchForm.MinLen)
              throw new Exception("switch is not full");
            string postCharSet = switchForm.PostCharSet;
            if (num3 == 0)
            {
              switchResult.PostCharIndex = -1;
              continue;
            }
            int num4 = postCharSet.IndexOf(srcString[num1]);
            if (num4 < 0)
            {
              switchResult.PostCharIndex = -1;
              continue;
            }
            switchResult.PostCharIndex = num4;
            ++num1;
            continue;
          default:
            continue;
        }
      }
      return true;
    }

    public void ParseStrings(SwitchForm[] switchForms, string[] commandStrings)
    {
      int length = commandStrings.Length;
      bool flag = false;
      for (int index = 0; index < length; ++index)
      {
        string commandString = commandStrings[index];
        if (flag)
          this.NonSwitchStrings.Add((object) commandString);
        else if (commandString == "--")
          flag = true;
        else if (!this.ParseString(commandString, switchForms))
          this.NonSwitchStrings.Add((object) commandString);
      }
    }

    public SwitchResult this[int index] => this._switches[index];

    public static int ParseCommand(
      CommandForm[] commandForms,
      string commandString,
      out string postString)
    {
      for (int command = 0; command < commandForms.Length; ++command)
      {
        string idString = commandForms[command].IDString;
        if (commandForms[command].PostStringMode)
        {
          if (commandString.IndexOf(idString) == 0)
          {
            postString = commandString.Substring(idString.Length);
            return command;
          }
        }
        else if (commandString == idString)
        {
          postString = "";
          return command;
        }
      }
      postString = "";
      return -1;
    }

    private static bool ParseSubCharsCommand(
      int numForms,
      CommandSubCharsSet[] forms,
      string commandString,
      ArrayList indices)
    {
      indices.Clear();
      int num1 = 0;
      for (int index1 = 0; index1 < numForms; ++index1)
      {
        CommandSubCharsSet form = forms[index1];
        int num2 = -1;
        int length = form.Chars.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          char ch = form.Chars[index2];
          int num3 = commandString.IndexOf(ch);
          if (num3 >= 0)
          {
            if (num2 >= 0 || commandString.IndexOf(ch, num3 + 1) >= 0)
              return false;
            num2 = index2;
            ++num1;
          }
        }
        if (num2 == -1 && !form.EmptyAllowed)
          return false;
        indices.Add((object) num2);
      }
      return num1 == commandString.Length;
    }

    private static bool IsItSwitchChar(char c) => c == '-' || c == '/';
  }
}
