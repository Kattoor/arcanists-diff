// Decompiled with JetBrains decompiler
// Type: ReportMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

#nullable disable
public class ReportMenu : MonoBehaviour
{
  public TextMeshProUGUI txtCase;
  public TextMeshProUGUI txtName;
  public InputFieldPlus input;
  public TMP_Dropdown dropNames;
  public TMP_Dropdown dropOffense;
  private int caseNumber;

  public static ReportMenu instance { get; private set; }

  private void Awake() => ReportMenu.instance = this;

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) ReportMenu.instance == (UnityEngine.Object) this))
      return;
    ReportMenu.instance = (ReportMenu) null;
  }

  public void Show(string name)
  {
    this.txtName.text = Client.Name;
    this.caseNumber = PlayerPrefs.GetInt("reportNumber", 1);
    this.txtCase.text = this.caseNumber.ToString();
    List<TMP_Dropdown.OptionData> optionDataList1 = new List<TMP_Dropdown.OptionData>();
    foreach (string reportableOffense in Server.reportableOffenses)
      optionDataList1.Add(new TMP_Dropdown.OptionData(reportableOffense));
    this.dropOffense.options = optionDataList1;
    List<TMP_Dropdown.OptionData> optionDataList2 = new List<TMP_Dropdown.OptionData>();
    bool flag = false;
    foreach (KeyValuePair<string, Account> account in Client._accounts)
    {
      if (!string.Equals(account.Key, Client.Name, StringComparison.OrdinalIgnoreCase))
      {
        if (string.Equals(account.Key, name, StringComparison.OrdinalIgnoreCase))
          flag = true;
        optionDataList2.Add(new TMP_Dropdown.OptionData(account.Key));
      }
    }
    if (!flag && !string.IsNullOrEmpty(name) && !string.Equals(name, Client.Name, StringComparison.OrdinalIgnoreCase))
      optionDataList2.Add(new TMP_Dropdown.OptionData(name));
    optionDataList2.Sort((Comparison<TMP_Dropdown.OptionData>) ((a, b) => a.text.CompareTo(b.text)));
    this.dropNames.options = optionDataList2;
    if (string.IsNullOrEmpty(name))
      this.dropNames.value = 0;
    else
      this.dropNames.value = optionDataList2.FindIndex((Predicate<TMP_Dropdown.OptionData>) (a => string.Equals(a.text, name, StringComparison.OrdinalIgnoreCase)));
  }

  public void Close() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  public void Confirm()
  {
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    if (this.dropNames.value < 0 || this.dropNames.value >= this.dropNames.options.Count || this.dropOffense.value < 0 || this.dropOffense.value >= this.dropOffense.options.Count || Client.connection == null || Client.connection.State != ConnectionState.Connected)
      return;
    PlayerPrefs.SetInt("reportNumber", this.caseNumber + 1);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 55);
        myBinaryWriter.Write(this.dropOffense.value);
        myBinaryWriter.Write(this.dropNames.options[this.dropNames.value].text);
        myBinaryWriter.Write(this.input.inputText.text);
      }
      if (Client.MyAccount.accountType.has(AccountType.Perm_Muted))
        return;
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }
}
