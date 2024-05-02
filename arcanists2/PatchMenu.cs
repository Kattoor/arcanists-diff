// Decompiled with JetBrains decompiler
// Type: PatchMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class PatchMenu : MonoBehaviour
{
  public static PatchMenu Instance;
  public TMP_Text txtNotes;
  public TMP_Dropdown dropdown;
  public const string prefLastOpened = "";

  private void Awake() => PatchMenu.Instance = this;

  private void OnDestroy()
  {
    if (!((Object) PatchMenu.Instance == (Object) this))
      return;
    PatchMenu.Instance = (PatchMenu) null;
  }

  public static bool NewVersion
  {
    get
    {
      return SpellOverrides.serverObj != null && SpellOverrides.serverObj.global.changelog != null && !string.Equals(SpellOverrides.serverObj.global.version, PlayerPrefs.GetString("", "")) && SpellOverrides.serverObj.global.changelog.Length > 10;
    }
  }

  private void Start()
  {
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    if (SpellOverrides.serverObj != null && SpellOverrides.serverObj.global.changelog != null && SpellOverrides.serverObj.global.changelog.Length > 10)
    {
      PlayerPrefs.SetString("", SpellOverrides.serverObj.global.version);
      optionDataList.Add(new TMP_Dropdown.OptionData("Patch: " + SpellOverrides.serverObj.global.version ?? ""));
    }
    foreach (ChangeLog.PatchNotes patchNotes in ChangeLog.list)
      optionDataList.Add(new TMP_Dropdown.OptionData("(" + patchNotes.version + ") " + patchNotes.date));
    this.dropdown.options = optionDataList;
    this.dropdown.SetValueWithoutNotify(0);
    this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.Select));
    this.Select(0);
  }

  public void Select(int i)
  {
    if (SpellOverrides.serverObj != null && SpellOverrides.serverObj.global.changelog != null && SpellOverrides.serverObj.global.changelog.Length > 10)
    {
      if (i == 0)
      {
        this.txtNotes.text = SpellOverrides.serverObj.global.changelog;
        return;
      }
      --i;
    }
    if (i < 0 || i >= ChangeLog.list.Count)
      i = 0;
    this.txtNotes.text = ChangeLog.list[i].notes;
  }

  public void ClickMainMenu() => Object.Destroy((Object) this.gameObject);
}
