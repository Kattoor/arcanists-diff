

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
