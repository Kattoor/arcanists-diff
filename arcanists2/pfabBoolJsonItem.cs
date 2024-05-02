// Decompiled with JetBrains decompiler
// Type: pfabBoolJsonItem
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class pfabBoolJsonItem : MonoBehaviour
{
  public TMP_Text text;
  public Toggle value;

  public void init(JProperty j, int arrayIndex)
  {
    this.text.text = j.Name.Substring(5);
    foreach (object obj in (IEnumerable<JToken>) j.Values())
      this.value.isOn = string.Equals(obj.ToString(), "True");
    this.value.onValueChanged.AddListener((UnityAction<bool>) (s =>
    {
      j.Value = JToken.Parse("\"" + (s ? "True" : "False") + "\"");
      TutorialEditorMenu.Instance?.Edited();
    }));
  }

  public void initSpellOverride(JProperty j, int arrayIndex)
  {
    this.text.text = j.Name;
    foreach (object obj in (IEnumerable<JToken>) j.Values())
      this.value.isOn = string.Equals(obj.ToString(), "True");
    this.value.onValueChanged.AddListener((UnityAction<bool>) (s =>
    {
      j.Value = JToken.Parse("\"" + (s ? "True" : "False") + "\"");
      SpellOverridesUI.Instance?.Edited();
    }));
  }
}
