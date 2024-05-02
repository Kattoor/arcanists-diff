

using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable disable
public class ProcessReportMenu : MonoBehaviour
{
  public TextMeshProUGUI txtCaseNumber;
  public TextMeshProUGUI txtDate;
  public TextMeshProUGUI txtInfo;
  public TextMeshProUGUI txtVS;
  public TextMeshProUGUI txtOffense;
  public TextMeshProUGUI txtVerdict;
  public TextMeshProUGUI txtClipboard;
  public TMP_Dropdown dropRequest;
  public TMP_Dropdown dropCases;
  public int selectedCaseID = -1;

  public static ProcessReportMenu instance { get; private set; }

  private void Awake() => ProcessReportMenu.instance = this;

  private void OnDestroy()
  {
    if (!((Object) ProcessReportMenu.instance == (Object) this))
      return;
    ProcessReportMenu.instance = (ProcessReportMenu) null;
  }

  public void Init()
  {
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (Report report in Client.reports)
      optionDataList.Add(new TMP_Dropdown.OptionData("Case: " + report.id.ToString()));
    this.dropCases.options = optionDataList;
    this.dropCases.value = optionDataList.Count - 1;
    if (optionDataList.Count != 0)
      return;
    this.txtClipboard.gameObject.SetActive(false);
    this.SelectCaseIndex(-1);
  }

  public void Close() => Object.Destroy((Object) this.gameObject);

  public void VerdictNotGulity() => this.SendReport("notguilty");

  public void VerdictMute() => this.SendReport("mute");

  public void VerdictRequest() => this.SendReport(this.dropRequest.itemText.text);

  public void SendReport(string x)
  {
    if (this.selectedCaseID != -1)
      Client.SendChatMsg("?closecase:" + this.selectedCaseID.ToString() + ":" + x);
    this.Remove(this.dropCases.value);
  }

  public void SelectCaseIndex(int i)
  {
    if (i < 0 || i >= Client.reports.Count)
    {
      this.selectedCaseID = -1;
      this.txtCaseNumber.text = "";
      this.txtDate.text = "";
      this.txtInfo.text = "";
      this.txtVS.text = "";
      this.txtVerdict.text = "";
      this.txtOffense.text = Client.reports.Count == 0 ? "No reports" : "Error";
    }
    else
    {
      Report report = Client.reports[i];
      this.selectedCaseID = report.id;
      this.txtCaseNumber.text = report.id.ToString();
      this.txtDate.text = report.time;
      this.txtInfo.text = report.extraInfo;
      report.offense = Mathf.Clamp(report.offense, 0, Server.reportableOffenses.Length - 1);
      this.txtOffense.text = Server.reportableOffenses[report.offense];
      this.txtVS.text = report.reporter + " vs " + report.reported;
      this.txtVerdict.text = report.reported + "'s Punishment";
    }
  }

  public void Remove(int i)
  {
    int num = this.dropCases.value;
    List<TMP_Dropdown.OptionData> options = this.dropCases.options;
    if (i < 0 || i >= options.Count)
    {
      this.selectedCaseID = -1;
    }
    else
    {
      Client.reports.RemoveAt(i);
      options.RemoveAt(i);
      this.dropCases.options = options;
      this.dropCases.value = Mathf.Clamp(num, -1, this.dropCases.options.Count - 1);
      this.SelectCaseIndex(this.dropCases.value);
    }
  }
}
