// Decompiled with JetBrains decompiler
// Type: pfabEditJsonItem
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class pfabEditJsonItem : MonoBehaviour
{
  public TMP_Text text;
  public TMP_InputField value;

  public void init(JProperty j, int arrayIndex, string customName = null, bool onValuChanged = false)
  {
    this.text.text = customName ?? j.Name;
    foreach (object obj in (IEnumerable<JToken>) j.Values())
      this.value.text = obj.ToString();
    if (string.Equals("radius", j.Name))
    {
      int result = 0;
      int.TryParse(this.value.text, out result);
      MapCoordPicker.Instance?.FromRadius(result);
    }
    if (onValuChanged)
      this.value.onValueChanged.AddListener((UnityAction<string>) (s =>
      {
        s = s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        j.Value = JToken.Parse("\"" + s + "\"");
        TutorialEditorMenu.Instance.Edited();
      }));
    this.value.onEndEdit.AddListener((UnityAction<string>) (s =>
    {
      if (string.Equals(j.Name, "Name"))
      {
        if ((Object) TutorialEditorMenu.Instance.selectedListText != (Object) null)
          TutorialEditorMenu.Instance.selectedListText.text = (TutorialEditorMenu.Instance.selectedListIndex + 1).ToString() + ") " + s;
      }
      else if (string.Equals(j.Name, "x"))
        MapCoordPicker.Instance?.FromX(int.Parse(s));
      else if (string.Equals(j.Name, "y"))
        MapCoordPicker.Instance?.FromY(int.Parse(s));
      else if (string.Equals(j.Name, "radius"))
        MapCoordPicker.Instance?.FromRadius(int.Parse(s));
      s = s.Replace("\\", "\\\\").Replace("\"", "\\\"");
      j.Value = JToken.Parse("\"" + s + "\"");
      TutorialEditorMenu.Instance.Edited();
    }));
  }

  public void initSpellOverride(
    JProperty j,
    int arrayIndex,
    string customName = null,
    bool onValuChanged = false)
  {
    this.text.text = customName ?? j.Name;
    string str1 = "";
    foreach (JToken jtoken in (IEnumerable<JToken>) j.Values())
      str1 += jtoken.ToString();
    this.value.text = str1;
    bool isFixedInt = this.value.text.Contains("\"x\"");
    bool isVector = this.value.text.Contains("\"y\"");
    bool isRawValue = this.value.text.Contains("\"RawValue\"");
    if (isVector)
    {
      MatchCollection matchCollection = Regex.Matches(this.value.text, "\\d+");
      string str2 = "(";
      bool flag = true;
      foreach (Capture capture in matchCollection)
      {
        long result;
        long.TryParse(capture.Value, out result);
        str2 += ((FixedInt) result).ToFloat().ToString();
        if (flag)
        {
          flag = false;
          str2 += ", ";
        }
      }
      this.value.text = str2 + ")";
    }
    else if (isFixedInt | isRawValue)
    {
      foreach (JProperty jproperty in j.Value.Values<JProperty>())
        j = jproperty;
      foreach (object obj in (IEnumerable<JToken>) j.Values())
        this.value.text = obj.ToString();
      long result;
      long.TryParse(this.value.text, out result);
      this.value.text = ((FixedInt) result).ToFloat().ToString();
    }
    if (onValuChanged)
      this.value.onValueChanged.AddListener((UnityAction<string>) (s =>
      {
        s = s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        j.Value = JToken.Parse("\"" + s + "\"");
        SpellOverridesUI.Instance.Edited();
      }));
    this.value.onEndEdit.AddListener((UnityAction<string>) (s =>
    {
      string.Equals(j.Name, "Name");
      s = s.Replace("\\", "\\\\").Replace("\"", "\\\"");
      if (isVector)
      {
        MatchCollection matchCollection = Regex.Matches(s, "\\d+");
        bool flag = true;
        foreach (Capture capture in matchCollection)
        {
          float result;
          float.TryParse(capture.Value, out result);
          foreach (JProperty jproperty in j.Value.Values<JProperty>())
          {
            if (flag && string.Equals("x", jproperty.Name))
            {
              jproperty.Value = (JToken) FixedInt.Create(result).RawValue;
              flag = false;
            }
            else if (!flag && string.Equals("y", jproperty.Name))
              jproperty.Value = (JToken) FixedInt.Create(result).RawValue;
          }
        }
        SpellOverridesUI.Instance.Edited();
      }
      else if (isFixedInt | isRawValue)
      {
        float result;
        float.TryParse(s, out result);
        j.Value = JToken.Parse("\"" + (object) FixedInt.Create(result).RawValue + "\"");
        SpellOverridesUI.Instance.Edited();
      }
      else
      {
        j.Value = JToken.Parse("\"" + s + "\"");
        SpellOverridesUI.Instance.Edited();
      }
    }));
  }
}
